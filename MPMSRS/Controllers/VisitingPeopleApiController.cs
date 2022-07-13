using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Interfaces.Auth;

namespace MPMSRS.Controllers
{
    [Route("api/visiting-peoples")]
    [ApiController]
    public class VisitingPeopleApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public VisitingPeopleApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IRefreshTokenRepository refreshTokenRepository)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [Authorize]
        [HttpGet("get-all-visiting-peoples", Name = "GetVisitingPeopleAll")]
        public async Task<IActionResult> GetAllVisitingPeoples([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingPeoples = await _repository.VisitingPeople.GetAllVisitingPeoples(pageSize, pageIndex);
                _logger.LogInfo($"Returned all visiting peoples from database.");

                var visitingPeopleResult = _mapper.Map<IEnumerable<VisitingPeopleDto>>(visitingPeoples);

                return Ok(visitingPeopleResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisitingPeoples action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-people-by-employee-id", Name = "GetVisitingPeopleByEmployeeId")]
        public async Task<IActionResult> GetVisitingPeopleByEmployeeId([FromQuery] Guid employeeId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingPeoples = await _repository.VisitingPeople.GetVisitingPeopleByEmployeeId(employeeId);
                _logger.LogInfo($"Returned visiting people by employee id from database.");

                var visitingPeoplesResult = _mapper.Map<IEnumerable<VisitingPeopleDto>>(visitingPeoples);

                return Ok(visitingPeoplesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingPeopleByEmployeeId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-people-by-visiting-id", Name = "GetVisitingPeopleByVisitingId")]
        public async Task<IActionResult> GetVisitingPeopleByVisitingId([FromQuery] Guid visitingId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingPeoples = await _repository.VisitingPeople.GetVisitingPeopleByVisitingId(visitingId, pageSize, pageIndex);
                _logger.LogInfo($"Returned visiting people by visiting id from database.");

                //var visitingPeoplesResult = _mapper.Map<IEnumerable<VisitingPeopleDto>>(visitingPeoples);

                return Ok(visitingPeoples);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingPeopleByEmployeeId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-people-by-id/{id}", Name = "GetVisitingPeopleById")]
        public async Task<IActionResult> GetVisitingPeopleById([FromQuery] Guid visitingPeopleId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingPeople = await _repository.VisitingPeople.GetVisitingPeopleById(visitingPeopleId);
                if (visitingPeople == null)
                {
                    _logger.LogError($"Visiting People with id: {visitingPeopleId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Visiting People with id: {visitingPeopleId}");
                    var visitingPeopleResult = _mapper.Map<VisitingPeopleDto>(visitingPeople);
                    return Ok(visitingPeopleResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingPeopleById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-visiting-people", Name = "InsertVisitingPeople")]
        public IActionResult CreateVisitingPeople([FromBody] VisitingPeopleCreationMapDto visitingPeople)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingPeople == null)
                {
                    _logger.LogError("Visiting People object sent from client is null.");
                    return BadRequest("Visiting People object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting People object sent from client.");
                    return BadRequest("Invalid model object");
                }

                if (visitingPeople.EmployeeId.Count() > 0)
                {
                    foreach (var sub in visitingPeople.EmployeeId)
                    {
                        VisitingPeopleCreationDto visitingPeopleObj = new VisitingPeopleCreationDto();
                        visitingPeopleObj.EmployeeId = Guid.Parse(sub);
                        visitingPeopleObj.VisitingId = visitingPeople.VisitingId;
                        visitingPeopleObj.CreatedAt = visitingPeople.CreatedAt;
                        visitingPeopleObj.CreatedBy = visitingPeople.CreatedBy;
                        visitingPeopleObj.UpdatedAt = visitingPeople.UpdatedAt;
                        visitingPeopleObj.UpdatedBy = visitingPeople.UpdatedBy;
                        var visitingPeopleEntity = _mapper.Map<VisitingPeoples>(visitingPeopleObj);
                        _repository.VisitingPeople.CreateVisitingPeople(visitingPeopleEntity);
                        _repository.Save();
                    }
                    return CreatedAtRoute("UserById", new { id = visitingPeople.EmployeeId }, visitingPeople);
                }
                else
                {
                    _logger.LogError("Visiting People Employee Id sent from client is null.");
                    return BadRequest("Visiting People Employee Id is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateVisitingPeople action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-visiting-people/{id}")]
        public async Task<IActionResult> UpdateVisitingPeople([FromQuery] Guid id, [FromBody] VisitingPeopleUpdateDto visitingPeople)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingPeople == null)
                {
                    _logger.LogError("Visiting People object sent from client is null.");
                    return BadRequest("Visiting People object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid visiting people object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var visitingPeopleEntity = await _repository.VisitingPeople.GetVisitingPeopleById(id);
                if (visitingPeopleEntity == null)
                {
                    _logger.LogError($"Visiting People with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(visitingPeople, visitingPeopleEntity);
                _repository.VisitingPeople.UpdateVisitingPeople(visitingPeopleEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVisitingPeople action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-visiting-people/{id}")]
        public async Task<IActionResult> DeleteVisitingPeople([FromQuery] Guid id, [FromBody] VisitingPeopleDeleteDto visitingPeople)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingPeople == null)
                {
                    _logger.LogError("Visiting People object sent from client is null.");
                    return BadRequest("Visiting People object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid visiting people object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var visitingPeopleEntity = await _repository.VisitingPeople.GetVisitingPeopleById(id);
                if (visitingPeopleEntity == null)
                {
                    _logger.LogError($"Visiting People with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(visitingPeople, visitingPeopleEntity);
                _repository.VisitingPeople.DeleteVisitingPeople(visitingPeopleEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVisitingPeople action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
