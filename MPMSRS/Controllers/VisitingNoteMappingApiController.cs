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
using MPMSRS.Services.Interfaces.Auth;

namespace MPMSRS.Controllers
{
    [Route("api/visiting-note-mappings")]
    [ApiController]
    public class VisitingNoteMappingApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public VisitingNoteMappingApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IRefreshTokenRepository refreshTokenRepository)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [Authorize]
        [HttpGet("get-all-visiting-note-mappings", Name = "GetVisitingNoteMappingAll")]
        public async Task<IActionResult> GetAllVisitingNoteMappings([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingNoteMappings = await _repository.VisitingNoteMapping.GetAllVisitingNoteMappings(pageSize, pageIndex);
                _logger.LogInfo($"Returned all visiting note mappings from database.");

                var visitingNoteMappingResult = _mapper.Map<IEnumerable<VisitingNoteMappingDto>>(visitingNoteMappings);

                return Ok(visitingNoteMappingResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisitingNoteMappings action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-visiting-note-mapping-by-visiting-id", Name = "GetVisitingNoteMappingByVisitingId")]
        public async Task<IActionResult> GetAllVisitingNoteMappingByVisitingId([FromQuery] Guid visitingId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingNoteMappings = await _repository.VisitingNoteMapping.GetVisitingNoteMappingByVisitingId(visitingId, pageSize, pageIndex);
                _logger.LogInfo($"Returned all visiting note mapping by visiting id from database.");

                var visitingNoteMappingResult = _mapper.Map<IEnumerable<VisitingNoteMappingDto>>(visitingNoteMappings);

                return Ok(visitingNoteMappingResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisitingNoteMappingByVisitingId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-visiting-note-mapping-by-employee-id", Name = "GetVisitingNoteMappingByEmployeeId")]
        public async Task<IActionResult> GetAllVisitingNoteMappingByEmployeeId([FromQuery] Guid employeeId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingNoteMappings = await _repository.VisitingNoteMapping.GetVisitingNoteMappingByUserId(employeeId);
                _logger.LogInfo($"Returned all visiting note by employee id from database.");

                var visitingNoteMappingResult = _mapper.Map<IEnumerable<VisitingNoteMappingDto>>(visitingNoteMappings);

                return Ok(visitingNoteMappingResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisitingNoteMappingByEmployeeId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-note-mapping-by-id/{id}", Name = "GetVisitingNoteMappingById")]
        public async Task<IActionResult> GetVisitingNoteMappingById([FromQuery] Guid visitingNoteMappingId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingNoteMappings = await _repository.VisitingNoteMapping.GetVisitingNoteMappingById(visitingNoteMappingId);
                if (visitingNoteMappings == null)
                {
                    _logger.LogError($"Visiting Note Mapping with id: {visitingNoteMappingId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Visiting Note Mapping with id: {visitingNoteMappingId}");
                    var visitingNoteMappingResult = _mapper.Map<VisitingNoteMappingDto>(visitingNoteMappings);
                    return Ok(visitingNoteMappingResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingNoteMappingById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-visiting-note-mapping", Name = "InsertVisitingNoteMapping")]
        public IActionResult CreateVisitingNoteMapping([FromBody] VisitingNoteMappingForCreationDto visitingNoteMapping)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingNoteMapping == null)
                {
                    _logger.LogError("Visiting Note Mapping object sent from client is null.");
                    return BadRequest("Visiting object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting Note Mapping object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var visitingNoteMappingEntity = _mapper.Map<VisitingNoteMappings>(visitingNoteMapping);
                _repository.VisitingNoteMapping.CreateVisitingNoteMapping(visitingNoteMappingEntity);
                _repository.Save();
                var createdVisitingNoteMapping = _mapper.Map<VisitingNoteMappingDto>(visitingNoteMappingEntity);
                return CreatedAtRoute("VisitingById", new { id = createdVisitingNoteMapping.VisitingNoteMappingId }, createdVisitingNoteMapping);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateVisitingNoteMapping action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-visiting-note-mapping/{id}")]
        public async Task<IActionResult> UpdateVisitingNoteMapping([FromQuery] Guid id, [FromBody] VisitingNoteMappingForUpdateDto visitingNoteMapping)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingNoteMapping == null)
                {
                    _logger.LogError("Visiting Note Mapping object sent from client is null.");
                    return BadRequest("Visiting Note Mapping object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid visiting note mapping object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var visitingNoteMappingEntity = await _repository.VisitingNoteMapping.GetVisitingNoteMappingById(id);
                if (visitingNoteMappingEntity == null)
                {
                    _logger.LogError($"Visiting Note Mapping with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(visitingNoteMapping, visitingNoteMappingEntity);
                _repository.VisitingNoteMapping.UpdateVisitingNoteMapping(visitingNoteMappingEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVisitingNoteMapping action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-visiting-note-mapping/{id}")]
        public async Task<IActionResult> DeleteVisitingNoteMapping([FromQuery] Guid id, [FromBody] VisitingNoteMappingForDeleteDto visitingNoteMapping)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingNoteMapping == null)
                {
                    _logger.LogError("Visiting Note Mapping object sent from client is null.");
                    return BadRequest("Visiting Note Mapping object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid visiting note mapping object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var visitingNoteMappingEntity = await _repository.VisitingNoteMapping.GetVisitingNoteMappingById(id);
                if (visitingNoteMappingEntity == null)
                {
                    _logger.LogError($"Visiting Note Mapping with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(visitingNoteMapping, visitingNoteMappingEntity);
                _repository.VisitingNoteMapping.DeleteVisitingNoteMapping(visitingNoteMappingEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVisitingNoteMapping action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
