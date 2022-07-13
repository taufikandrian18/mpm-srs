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
using OfficeOpenXml.Style;

namespace MPMSRS.Controllers
{
    [Route("api/problem-categories")]
    [ApiController]
    public class ProblemCategoryApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private DatabaseRefreshTokenRepository _services;

        public ProblemCategoryApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, DatabaseRefreshTokenRepository service)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _services = service;
        }

        [Authorize]
        [HttpGet("get-all-problem-categories", Name = "GetProblemCategoriesAll")]
        public async Task<IActionResult> GetAllProblemCategories([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var problemCategories = await _repository.ProblemCategory.GetAllProblemCategories(pageSize,pageIndex);
                _logger.LogInfo($"Returned all problem categories from database.");

                var pcResult = _mapper.Map<IEnumerable<ProblemCategoryDto>>(problemCategories);

                List<ProblemCategoryDtoView> pcPaginatedResult = new List<ProblemCategoryDtoView>();

                foreach(var item in pcResult)
                {
                    var tmpData = await _repository.UserProblemCategoryMapping.GetUserProblemCategoryListView(item.ProblemCategoryId);
                    ProblemCategoryDtoView pcPaginatedObj = new ProblemCategoryDtoView();
                    pcPaginatedObj.ProblemCategoryId = item.ProblemCategoryId;
                    pcPaginatedObj.ProblemCategoryName = item.ProblemCategoryName;
                    pcPaginatedObj.ParentId = item.ParentId;
                    pcPaginatedObj.ParentName = await _repository.ProblemCategory.GetProblemCategoryNameById(item.ParentId.HasValue ? item.ParentId.Value : Guid.Empty);
                    pcPaginatedObj.ChildId = item.ChildId;
                    pcPaginatedObj.ChildName = await _repository.ProblemCategory.GetProblemCategoryNameById(item.ChildId.HasValue ? item.ChildId.Value : Guid.Empty);
                    pcPaginatedObj.CreatedAt = item.CreatedAt;
                    pcPaginatedObj.CreatedBy = item.CreatedBy;
                    pcPaginatedObj.UpdatedAt = item.UpdatedAt;
                    pcPaginatedObj.UpdatedBy = item.UpdatedBy;
                    pcPaginatedObj.ListEmployees = tmpData.ToList();
                    pcPaginatedResult.Add(pcPaginatedObj);
                }    

                return Ok(pcPaginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllProblemCategories action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-problem-category-by-id/{id}", Name = "ProblemCategoryById")]
        public async Task<IActionResult> GetProblemCategoryById([FromQuery] Guid problemCategoryId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var pc = await _repository.ProblemCategory.GetProblemCategoryById(problemCategoryId);
                if (pc == null)
                {
                    _logger.LogError($"Problem Category with id: {problemCategoryId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Problem Category with id: {problemCategoryId}");
                    var pcResult = _mapper.Map<ProblemCategoryDto>(pc);

                    var tmpData = await _repository.UserProblemCategoryMapping.GetUserProblemCategoryListView(pcResult.ProblemCategoryId);
                    ProblemCategoryDtoView pcPaginatedObj = new ProblemCategoryDtoView();
                    pcPaginatedObj.ProblemCategoryId = pcResult.ProblemCategoryId;
                    pcPaginatedObj.ProblemCategoryName = pcResult.ProblemCategoryName;
                    pcPaginatedObj.ParentId = pcResult.ParentId;
                    pcPaginatedObj.ParentName = await _repository.ProblemCategory.GetProblemCategoryNameById(pcResult.ParentId.HasValue ? pcResult.ParentId.Value : Guid.Empty);
                    pcPaginatedObj.ChildId = pcResult.ChildId;
                    pcPaginatedObj.ChildName = await _repository.ProblemCategory.GetProblemCategoryNameById(pcResult.ChildId.HasValue ? pcResult.ChildId.Value : Guid.Empty);
                    pcPaginatedObj.CreatedAt = pcResult.CreatedAt;
                    pcPaginatedObj.CreatedBy = pcResult.CreatedBy;
                    pcPaginatedObj.UpdatedAt = pcResult.UpdatedAt;
                    pcPaginatedObj.UpdatedBy = pcResult.UpdatedBy;
                    pcPaginatedObj.ListEmployees = tmpData.ToList();
                    return Ok(pcPaginatedObj);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetProblemCategoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-problem-category", Name = "InsertProblemCategory")]
        public IActionResult CreateProblemCategory([FromBody] ProblemCategoryCreationForDto problemCategory)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (problemCategory == null)
                {
                    _logger.LogError("Problem Category object sent from client is null.");
                    return BadRequest("Problem Category object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Problem Category object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var pcEntity = _mapper.Map<ProblemCategories>(problemCategory);
                _repository.ProblemCategory.CreateProblemCategory(pcEntity);
                _repository.Save();
                var createdProblemCategory = _mapper.Map<ProblemCategoryDto>(pcEntity);

                if (problemCategory.ListEmployees != null)
                {
                    if (problemCategory.ListEmployees.Count() > 0)
                    {
                        foreach (var sub in problemCategory.ListEmployees)
                        {
                            UserProblemCategoryMappingForCreationDto UserPCObj = new UserProblemCategoryMappingForCreationDto();
                            UserPCObj.ProblemCategoryId = createdProblemCategory.ProblemCategoryId;
                            UserPCObj.EmployeeId = sub.EmployeeId;
                            UserPCObj.CreatedAt = createdProblemCategory.CreatedAt;
                            UserPCObj.CreatedBy = createdProblemCategory.CreatedBy;
                            UserPCObj.UpdatedAt = createdProblemCategory.UpdatedAt;
                            UserPCObj.UpdatedBy = createdProblemCategory.UpdatedBy;
                            var userPCMappingEntity = _mapper.Map<UserProblemCategoryMappings>(UserPCObj);
                            _repository.UserProblemCategoryMapping.CreateUserProblemCategory(userPCMappingEntity);
                            _repository.Save();
                        }
                    }
                }

                return CreatedAtRoute("ProblemCategoryById", new { id = createdProblemCategory.ProblemCategoryId }, createdProblemCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProblemCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-problem-category/{id}")]
        public async Task<IActionResult> UpdateProblemCategory([FromQuery] Guid id, [FromBody] ProblemCategoryUpdateForDto problemCategory)
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

                if (problemCategory == null)
                {
                    _logger.LogError("Problem Category object sent from client is null.");
                    return BadRequest("Problem Category object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Problem Category object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var pcEntity = await _repository.ProblemCategory.GetProblemCategoryById(id);
                if (pcEntity == null)
                {
                    _logger.LogError($"Problem Category with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(problemCategory, pcEntity);
                _repository.ProblemCategory.UpdateProblemCategory(pcEntity);
                _repository.Save();

                var userProblemCategoryPIC = await _repository.UserProblemCategoryMapping.GetUserProblemCategoryListByProblemCategoryId(pcEntity.ProblemCategoryId);
                if (userProblemCategoryPIC != null)
                {
                    if (userProblemCategoryPIC.Count() > 0)
                    {
                        _repository.UserProblemCategoryMapping.DeleteUserProblemCategoryMappingByProblemCategoryId(userProblemCategoryPIC.ToList());
                        _repository.Save();
                    }
                }

                if (problemCategory.ListEmployees != null)
                {
                    if (problemCategory.ListEmployees.Count() > 0)
                    {
                        foreach (var sub in problemCategory.ListEmployees)
                        {
                            UserProblemCategoryMappingForCreationDto UserPCObj = new UserProblemCategoryMappingForCreationDto();
                            UserPCObj.ProblemCategoryId = pcEntity.ProblemCategoryId;
                            UserPCObj.EmployeeId = sub.EmployeeId;
                            UserPCObj.CreatedAt = thisDate;
                            UserPCObj.CreatedBy = userId.ToString();
                            UserPCObj.UpdatedAt = thisDate;
                            UserPCObj.UpdatedBy = userId.ToString();
                            var userPCMappingEntity = _mapper.Map<UserProblemCategoryMappings>(UserPCObj);
                            _repository.UserProblemCategoryMapping.CreateUserProblemCategory(userPCMappingEntity);
                            _repository.Save();
                        }
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProblemCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-problem-category/{id}")]
        public async Task<IActionResult> DeleteProblemCategory([FromQuery] Guid id, [FromBody] ProblemCategoryDeleteForDto problemCategory)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (problemCategory == null)
                {
                    _logger.LogError("Problem Category object sent from client is null.");
                    return BadRequest("Problem Category object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Problem Category object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var pcEntity = await _repository.ProblemCategory.GetProblemCategoryById(id);
                if (pcEntity == null)
                {
                    _logger.LogError($"Problem Category with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(problemCategory, pcEntity);
                _repository.ProblemCategory.DeleteProblemCategory(pcEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProblemCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("export-excel-problem-categories")]
        public ActionResult ExportExcel()
        {
            DateTime now = DateTime.Now;
            string excelName = "ProblemCategoryContents-" + now + ".xlsx";

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("ProblemCategoryContents");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "Problem Name";
                workSheet.Cells[1, 2].Value = "Parent Name";

                //workSheet.Column(1).AutoFit();
                //workSheet.Column(2).AutoFit();
                package.Save();
            }

            stream.Position = 0;

            return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = excelName
            };
        }

        [Authorize]
        [HttpPost("import-excel-problem-categories", Name = "ImportProblemCategoryExcel")]
        public async Task<IActionResult> ProblemCategoryImportExcel([FromForm] IEnumerable<IFormFile> files)
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
                    var file = files.FirstOrDefault();
                    if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogError("file format sent from client is invalid.");
                        return BadRequest("file format object is invalid");
                    }
                    else
                    {
                        var listPC = new List<ProblemCategoryDto>();
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);

                            using (var package = new ExcelPackage(stream))
                            {
                                ExcelWorksheet worksheet = package.Workbook.Worksheets["ProblemCategoryContents"];
                                if (worksheet == null)
                                {
                                    _logger.LogError("file template format sent from client is invalid.");
                                    return BadRequest("file template format object is invalid");
                                }
                                else
                                {
                                    var rowCount = worksheet.Dimension.Rows;

                                    for (int row = 2; row <= rowCount; row++)
                                    {
                                        var problemName = worksheet.Cells[row, 1].Value?.ToString().Trim();
                                        var parentName = worksheet.Cells[row, 2].Value?.ToString().Trim();

                                        if (problemName == null)
                                        {
                                            _logger.LogError($"data in row: {row}, can't be empty.");
                                            return BadRequest($"data in row: {row}, can't be empty.");
                                        }
                                        else
                                        {
                                            //di check apakah parent name dari excel kosong atau tidak
                                            if (!string.IsNullOrEmpty(parentName))
                                            {
                                                //jika tidak kosong parent name di check ke database ada atau tidak
                                                var checkPC = await _repository.ProblemCategory.GetProblemCategoryIdByProblemCategoryName(parentName);
                                                if (checkPC == Guid.Empty)
                                                {
                                                    //jika tidak ada di check apakah dia di input di row pertama atau bukan
                                                    if (row == 2)
                                                    {
                                                        _logger.LogError($"problem category with name : {parentName}, not found in database.");
                                                        return BadRequest($"problem category with name : {parentName}, not found in database.");
                                                    }
                                                    else
                                                    {
                                                        //jika parent name yang telah diisi tidak ada di database dan bukan di row pertama,
                                                        //maka parent name akan di check di ke problem name sebelumnya ada atau tidak
                                                        for (int i = 2; i <= row; i++)
                                                        {
                                                            //jika setelah di check ternyata parent name yang telah diisi dan menemukan di problem name sebelumnya
                                                            //maka data current row akan di input ke database
                                                            var currProblemName = worksheet.Cells[i, 1].Value?.ToString().Trim();
                                                            if (parentName == currProblemName)
                                                            {
                                                                //di input ke database dengan
                                                                var getParentGUID = await _repository.ProblemCategory.GetProblemCategoryIdByProblemCategoryName(currProblemName);

                                                                ProblemCategoryCreationForDto problemCategory = new ProblemCategoryCreationForDto();

                                                                problemCategory.ProblemCategoryName = problemName;
                                                                problemCategory.ParentId = getParentGUID;
                                                                problemCategory.CreatedAt = thisDate;
                                                                problemCategory.CreatedBy = userId.ToString();
                                                                problemCategory.UpdatedAt = thisDate;
                                                                problemCategory.UpdatedBy = userId.ToString();

                                                                var pcEntity = _mapper.Map<ProblemCategories>(problemCategory);
                                                                _repository.ProblemCategory.CreateProblemCategory(pcEntity);
                                                                _repository.Save();
                                                            }
                                                            else
                                                            {
                                                                _logger.LogError($"problem category with name : {parentName}, not found in files, Please check again");
                                                                return BadRequest($"problem category with name : {parentName}, not found in files, Please check again");
                                                            }

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //jika ada di database maka problem name akan di input ke database dengan parent id yang telah di dapat di checkPC

                                                    ProblemCategoryCreationForDto problemCategory = new ProblemCategoryCreationForDto();

                                                    problemCategory.ProblemCategoryName = problemName;
                                                    problemCategory.ParentId = checkPC;
                                                    problemCategory.CreatedAt = thisDate;
                                                    problemCategory.CreatedBy = userId.ToString();
                                                    problemCategory.UpdatedAt = thisDate;
                                                    problemCategory.UpdatedBy = userId.ToString();

                                                    var pcEntity = _mapper.Map<ProblemCategories>(problemCategory);
                                                    _repository.ProblemCategory.CreateProblemCategory(pcEntity);
                                                    _repository.Save();
                                                }
                                            }
                                            else
                                            {
                                                ProblemCategoryCreationForDto problemCategory = new ProblemCategoryCreationForDto();

                                                problemCategory.ProblemCategoryName = problemName;
                                                problemCategory.CreatedAt = thisDate;
                                                problemCategory.CreatedBy = userId.ToString();
                                                problemCategory.UpdatedAt = thisDate;
                                                problemCategory.UpdatedBy = userId.ToString();

                                                var pcEntity = _mapper.Map<ProblemCategories>(problemCategory);
                                                _repository.ProblemCategory.CreateProblemCategory(pcEntity);
                                                _repository.Save();
                                            }
                                        }
                                    }
                                }
                            }

                            return NoContent();
                        }
                    }
                }
                else
                {
                    _logger.LogError("files form object sent from client is null.");
                    return BadRequest("files form object is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside ProblemCategoryImportExcel action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
