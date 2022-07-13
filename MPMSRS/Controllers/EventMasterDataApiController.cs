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
    [Route("api/event-master-datas")]
    [ApiController]
    public class EventMasterDataApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public EventMasterDataApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("get-all-event-master-datas", Name = "GetEventMasterDataAll")]
        public async Task<IActionResult> GetAllEventMasterDatas([FromQuery] int pageSize, int pageIndex, string location)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var eventMasterDatas = await _repository.EventMasterData.GetAllEventMasterDatas(pageSize, pageIndex, location);
                _logger.LogInfo($"Returned all event master datas from database.");

                var eventMasterDataResult = _mapper.Map<IEnumerable<EventMasterDataDto>>(eventMasterDatas);

                return Ok(eventMasterDataResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEventMasterDatas action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-master-data-by-id/{id}", Name = "EventMasterDataById")]
        public async Task<IActionResult> GetEventMasterDataById([FromQuery] Guid eventMasterDataId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var eventMasterData = await _repository.EventMasterData.GetEventMasterDataById(eventMasterDataId);
                if (eventMasterData == null)
                {
                    _logger.LogError($"Event master data with id: {eventMasterDataId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Event master data with id: {eventMasterDataId}");
                    var EventMasterDataResult = _mapper.Map<EventMasterDataDto>(eventMasterData);
                    return Ok(EventMasterDataResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventMasterDataById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-event-master-data", Name = "InsertEventMasterData")]
        public IActionResult CreateEventMasterData([FromBody] EventMasterDataCreationDto eventMasterData)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventMasterData == null)
                {
                    _logger.LogError("Event Master Data object sent from client is null.");
                    return BadRequest("Event Master Data object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event Master Data object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var EventMasterDataEntity = _mapper.Map<EventMasterDatas>(eventMasterData);
                _repository.EventMasterData.CreateEventMasterData(EventMasterDataEntity);
                _repository.Save();
                var createdEventMasterData = _mapper.Map<EventMasterDataDto>(EventMasterDataEntity);
                return CreatedAtRoute("EventMasterDataById", new { id = createdEventMasterData.EventMasterDataId }, createdEventMasterData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEventMasterData action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-event-master-data/{id}")]
        public async Task<IActionResult> UpdateEventMasterData([FromQuery] Guid id, [FromBody] EventMasterDataUpdateDto eventMasterData)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventMasterData == null)
                {
                    _logger.LogError("Event Master Data object sent from client is null.");
                    return BadRequest("Event Master Data object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event Master Data object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var EventMasterDataEntity = await _repository.EventMasterData.GetEventMasterDataById(id);
                if (EventMasterDataEntity == null)
                {
                    _logger.LogError($"Event Master Data with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(eventMasterData, EventMasterDataEntity);
                _repository.EventMasterData.UpdateEventMasterData(EventMasterDataEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEventMasterData action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-event-master-data/{id}")]
        public async Task<IActionResult> DeleteEventMasterData([FromQuery] Guid id, [FromBody] EventMasterDataDeletionDto eventMasterData)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventMasterData == null)
                {
                    _logger.LogError("Event Master Data object sent from client is null.");
                    return BadRequest("Event Master Data object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event Master Data object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var EventMasterDataEntity = await _repository.EventMasterData.GetEventMasterDataById(id);
                if (EventMasterDataEntity == null)
                {
                    _logger.LogError($"Event Master Data with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(eventMasterData, EventMasterDataEntity);
                _repository.EventMasterData.DeleteEventMasterData(EventMasterDataEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEventMasterData action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
