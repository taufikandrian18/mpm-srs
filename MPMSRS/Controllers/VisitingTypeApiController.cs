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

namespace MPMSRS.Controllers
{
    [Route("api/visiting-types")]
    [ApiController]
    public class VisitingTypeApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public VisitingTypeApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("get-all-visiting-types", Name = "GetVisitingTypesAll")]
        public async Task<IActionResult> GetAllVisitingTypes([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingTypes = await _repository.VisitingType.GetAllVisitingTypes(pageSize,pageIndex);
                _logger.LogInfo($"Returned all visiting types from database.");

                var visitingTypesResult = _mapper.Map<IEnumerable<VisitingTypeDto>>(visitingTypes);

                return Ok(visitingTypesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisitingTypes action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-type-by-id/{id}", Name = "VisitingTypeById")]
        public async Task<IActionResult> GetVisitingTypeById([FromQuery] Guid visitingTypeId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingType = await _repository.VisitingType.GetVisitingTypeById(visitingTypeId);
                if (visitingType == null)
                {
                    _logger.LogError($"Visiting Type with id: {visitingTypeId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Visiting Type with id: {visitingTypeId}");
                    var VisitingTypeResult = _mapper.Map<VisitingTypeDto>(visitingType);
                    return Ok(VisitingTypeResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetNetworkById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-visiting-type", Name = "InsertVisitingType")]
        public IActionResult CreateVisitingType([FromBody] VisitingTypeForCreationDto visitingType)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingType == null)
                {
                    _logger.LogError("Visiting Type object sent from client is null.");
                    return BadRequest("Visiting Type object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting Type object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var VisitingTypeEntity = _mapper.Map<VisitingTypes>(visitingType);
                _repository.VisitingType.CreateVisitingType(VisitingTypeEntity);
                _repository.Save();
                var createdVisitingType = _mapper.Map<VisitingTypeDto>(VisitingTypeEntity);
                return CreatedAtRoute("VisitingTypeById", new { id = createdVisitingType.VisitingTypeId }, createdVisitingType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateVisitingType action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-visiting-type/{id}")]
        public async Task<IActionResult> UpdateVisitingType([FromQuery] Guid id, [FromBody] VisitingTypeForUpdateDto visitingType)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingType == null)
                {
                    _logger.LogError("Visiting Type object sent from client is null.");
                    return BadRequest("Visiting Type object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting Type object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var VisitingTypeEntity = await _repository.VisitingType.GetVisitingTypeById(id);
                if (VisitingTypeEntity == null)
                {
                    _logger.LogError($"Visiting Type with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(visitingType, VisitingTypeEntity);
                _repository.VisitingType.UpdateVisitingType(VisitingTypeEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVisitingType action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-visiting-type/{id}")]
        public async Task<IActionResult> DeleteVisitingType([FromQuery] Guid id, [FromBody] VisitingTypeForDeleteDto visitingType)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingType == null)
                {
                    _logger.LogError("Visiting Type object sent from client is null.");
                    return BadRequest("Visiting Type object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting Type object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var visitingTypeEntity = await _repository.VisitingType.GetVisitingTypeById(id);
                if (visitingTypeEntity == null)
                {
                    _logger.LogError($"Visiting Type with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(visitingType, visitingTypeEntity);
                _repository.VisitingType.DeleteVisitingType(visitingTypeEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVisitingType action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
