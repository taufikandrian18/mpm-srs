using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Repositories.Auth;
using OfficeOpenXml;

namespace MPMSRS.Controllers
{
    [Route("api/ranks")]
    [ApiController]
    public class RankApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private DatabaseRefreshTokenRepository _services;

        public RankApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, DatabaseRefreshTokenRepository service)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _services = service;
        }

        [Authorize]
        [HttpGet("get-all-ranks", Name = "GetRanksAll")]
        public async Task<IActionResult> GetAllRanks([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var ranks = await _repository.Rank.GetAllRanks(pageSize, pageIndex);
                _logger.LogInfo($"Returned all ranks from database.");

                var ranksResult = _mapper.Map<IEnumerable<RankDto>>(ranks);

                return Ok(ranksResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRanks action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-rank-by-id/{id}", Name = "RankById")]
        public async Task<IActionResult> GetRankById([FromQuery] Guid rankId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var rank = await _repository.Rank.GetRankById(rankId);
                if (rank == null)
                {
                    _logger.LogError($"Rank with id: {rankId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned rank with id: {rankId}");
                    var rankResult = _mapper.Map<RankDto>(rank);
                    return Ok(rankResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRankById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-rank", Name = "InsertRank")]
        public IActionResult CreateRank([FromBody] RankForCreationDto rank)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (rank == null)
                {
                    _logger.LogError("Rank object sent from client is null.");
                    return BadRequest("Rank object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid rank object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var rankEntity = _mapper.Map<Ranks>(rank);
                _repository.Rank.CreateRank(rankEntity);
                _repository.Save();
                var createdRole = _mapper.Map<RankDto>(rankEntity);
                return CreatedAtRoute("RankById", new { id = createdRole.RankId }, createdRole);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateRank action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-rank/{id}")]
        public async Task<IActionResult> UpdateRank([FromQuery] Guid id, [FromBody] RankForUpdateDto rank)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (rank == null)
                {
                    _logger.LogError("Rank object sent from client is null.");
                    return BadRequest("Rank object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid rank object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var rankEntity = await _repository.Rank.GetRankById(id);
                if (rankEntity == null)
                {
                    _logger.LogError($"Rank with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(rank, rankEntity);
                _repository.Rank.UpdateRank(rankEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateRank action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-rank/{id}")]
        public async Task<IActionResult> DeleteRank([FromQuery] Guid id, [FromBody] RankForDeletionDto rank)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (rank == null)
                {
                    _logger.LogError("Rank object sent from client is null.");
                    return BadRequest("Rank object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Rank object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var rankEntity = await _repository.Rank.GetRankById(id);
                if (rankEntity == null)
                {
                    _logger.LogError($"Rank with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(rank, rankEntity);
                _repository.Rank.DeleteRank(rankEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteRank action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("rank-import-excel", Name = "ImportRankExcel")]
        public async Task<IActionResult> RankImportExcel([FromForm] IEnumerable<IFormFile> files)
        {
            try
            {
                var thisDate = DateTime.Now;
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (files.Count() > 0)
                {
                    List<RankDto> listOfRank = new List<RankDto>();
                    var file = files.FirstOrDefault();
                    if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogError("file format sent from client is invalid.");
                        return BadRequest("file format object is invalid");
                    }

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                            if (worksheet == null)
                            {
                                _logger.LogError("file template format sent from client is invalid.");
                                return BadRequest("file template format object is invalid");
                            }

                            var rowCount = worksheet.Dimension.Rows;

                            for (int row = 2; row <= rowCount; row++)
                            {
                                var userStaff = worksheet.Cells[row, 1].Value?.ToString().Trim();
                                var userStaffName = worksheet.Cells[row, 2].Value?.ToString().Trim();
                                var userManager = worksheet.Cells[row, 3].Value?.ToString().Trim();
                                var userManagerName = worksheet.Cells[row, 4].Value?.ToString().Trim();
                                var userGM = worksheet.Cells[row, 5].Value?.ToString().Trim();
                                var userGMName = worksheet.Cells[row, 6].Value?.ToString().Trim();
                                var userBOD = worksheet.Cells[row, 7].Value?.ToString().Trim();
                                var userBODName = worksheet.Cells[row, 8].Value?.ToString().Trim();
                                var divisionName = worksheet.Cells[row, 9].Value?.ToString().Trim();


                                RankForCreationDto rankObj = new RankForCreationDto();
                                rankObj.UserBOD = userBOD;
                                rankObj.UserBODName = userBODName;
                                rankObj.UserGM = userGM;
                                rankObj.UserGMName = userGMName;
                                rankObj.UserManager = userManager;
                                rankObj.UserManagerName = userManagerName;
                                rankObj.UserStaff = userStaff;
                                rankObj.UserStaffName = userStaffName;
                                rankObj.DivisionName = divisionName;
                                rankObj.CreatedAt = thisDate;
                                rankObj.CreatedBy = userId.ToString();
                                rankObj.UpdatedAt = thisDate;
                                rankObj.UpdatedBy = userId.ToString();

                                var rankEntity = _mapper.Map<Ranks>(rankObj);
                                _repository.Rank.Create(rankEntity);
                                _repository.Save();
                                var createdRank = _mapper.Map<RankDto>(rankEntity);
                                listOfRank.Add(createdRank);
                            }
                        }
                    }

                    return CreatedAtRoute("RankById", new { id = listOfRank.Select(x => x.RankId)}, listOfRank);
                }
                else
                {
                    _logger.LogError("files form object sent from client is null.");
                    return BadRequest("files form object is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside RankImportExcel action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
