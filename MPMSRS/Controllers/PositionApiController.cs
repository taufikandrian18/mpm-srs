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
    [Route("api/networks")]
    [ApiController]
    public class PositionApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public PositionApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("get-all-positions", Name = "GetPositionkAll")]
        public async Task<IActionResult> GetAllPositions(int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var positions = await _repository.Position.GetAllPositions(pageSize,pageIndex);
                _logger.LogInfo($"Returned all positions from database.");

                var positionsResult = _mapper.Map<IEnumerable<PositionDto>>(positions);

                return Ok(positionsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllPositions action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-position-by-id/{id}", Name = "PositionById")]
        public async Task<IActionResult> GetPositionById([FromQuery] Guid positionId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var position = await _repository.Position.GetPositionsById(positionId);
                if (position == null)
                {
                    _logger.LogError($"Position with id: {positionId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned position with id: {positionId}");
                    var positionResult = _mapper.Map<PositionDto>(position);
                    return Ok(positionResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPositionById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-position", Name = "InsertPosition")]
        public IActionResult CreatePosition([FromBody] PositionCreationForDto position)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (position == null)
                {
                    _logger.LogError("Position object sent from client is null.");
                    return BadRequest("Position object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Position object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var positionEntity = _mapper.Map<Positions>(position);
                _repository.Position.CreatePosition(positionEntity);
                _repository.Save();
                var createdPosition = _mapper.Map<PositionDto>(positionEntity);
                return CreatedAtRoute("PositionById", new { id = createdPosition.PositionId }, createdPosition);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreatePosition action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-position/{id}")]
        public async Task<IActionResult> UpdatePosition([FromQuery] Guid id, [FromBody] PositionUpdateForDto position)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (position == null)
                {
                    _logger.LogError("Position object sent from client is null.");
                    return BadRequest("Position object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid position object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var positionEntity = await _repository.Position.GetPositionsById(id);
                if (positionEntity == null)
                {
                    _logger.LogError($"Position with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(position, positionEntity);
                _repository.Position.UpdatePosition(positionEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePosition action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-position/{id}")]
        public async Task<IActionResult> DeletePosition([FromQuery] Guid id, [FromBody] PositionDeleteForDto position)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (position == null)
                {
                    _logger.LogError("Position object sent from client is null.");
                    return BadRequest("Position object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid position object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var positionEntity = await _repository.Position.GetPositionsById(id);
                if (positionEntity == null)
                {
                    _logger.LogError($"Position with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(position, positionEntity);
                _repository.Position.DeletePosition(positionEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePosition action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
