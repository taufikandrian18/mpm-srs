using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Repositories.Auth;

namespace MPMSRS.Controllers
{
    [Route("api/divisions")]
    [ApiController]
    public class DivisionApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private DatabaseRefreshTokenRepository _services;

        public DivisionApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, DatabaseRefreshTokenRepository service)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _services = service;
        }

        [Authorize]
        [HttpGet("get-all-divisions", Name = "GetDivisionsAll")]
        public async Task<IActionResult> GetAllDivisions([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }
                var divisions = await _repository.Division.GetAllDivisions(pageSize,pageIndex);
                _logger.LogInfo($"Returned all divisions from database.");

                var divisionResult = _mapper.Map<IEnumerable<DivisionDto>>(divisions);

                return Ok(divisionResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllDivisions action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-division-by-id/{id}", Name = "DivisionById")]
        public async Task<IActionResult> GetDivisionById([FromQuery] Guid divisionId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var division = await _repository.Division.GetDivisionById(divisionId);
                if (division == null)
                {
                    _logger.LogError($"Division with id: {divisionId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Division with id: {divisionId}");
                    var divisionResult = _mapper.Map<DivisionDto>(division);
                    return Ok(divisionResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetDivisionById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-division", Name = "InsertDivision")]
        public IActionResult CreateDivision([FromBody] DivisionCreationForDto division)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (division == null)
                {
                    _logger.LogError("Division object sent from client is null.");
                    return BadRequest("Division object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Division object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var divisionEntity = _mapper.Map<Divisions>(division);
                _repository.Division.CreateDivision(divisionEntity);
                _repository.Save();
                var createdDivision = _mapper.Map<DivisionDto>(divisionEntity);
                return CreatedAtRoute("DivisionById", new { id = createdDivision.DivisionId }, createdDivision);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateDivision action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-division/{id}")]
        public async Task<IActionResult> UpdateDivision([FromQuery] Guid id, [FromBody] DivisionUpdateForDto division)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (division == null)
                {
                    _logger.LogError("Division object sent from client is null.");
                    return BadRequest("Division object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Division object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var divisionEntity = await _repository.Division.GetDivisionById(id);
                if (divisionEntity == null)
                {
                    _logger.LogError($"Division with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(division, divisionEntity);
                _repository.Division.UpdateDivision(divisionEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateDivision action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-division/{id}")]
        public async Task<IActionResult> DeleteDivision([FromQuery] Guid id, [FromBody] DivisionDeleteForDto division)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (division == null)
                {
                    _logger.LogError("Division object sent from client is null.");
                    return BadRequest("Division object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Division object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var divisionEntity = await _repository.Division.GetDivisionById(id);
                if (divisionEntity == null)
                {
                    _logger.LogError($"Division with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(division, divisionEntity);
                _repository.Division.DeleteDivision(divisionEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteDivision action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
