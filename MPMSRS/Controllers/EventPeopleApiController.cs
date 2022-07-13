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
    [Route("api/event-peoples")]
    [ApiController]
    public class EventPeopleApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public EventPeopleApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IRefreshTokenRepository refreshTokenRepository)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [Authorize]
        [HttpGet("get-all-event-peoples", Name = "GetEventPeopleAll")]
        public async Task<IActionResult> GetAllEventPeoples([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var eventPeoples = await _repository.EventPeople.GetAllEventPeoples(pageSize, pageIndex);
                _logger.LogInfo($"Returned all event peoples from database.");

                var eventPeopleResult = _mapper.Map<IEnumerable<EventPeopleDto>>(eventPeoples);

                return Ok(eventPeopleResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEventPeoples action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-people-by-employee-id", Name = "GetEventPeopleByEmployeeId")]
        public async Task<IActionResult> GetEventPeopleByEmployeeId([FromQuery] Guid employeeId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var eventPeoples = await _repository.EventPeople.GetEventPeopleByEmployeeId(employeeId);
                _logger.LogInfo($"Returned event people by employee id from database.");

                var eventPeoplesResult = _mapper.Map<IEnumerable<EventPeopleDto>>(eventPeoples);

                return Ok(eventPeoplesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventPeopleByEmployeeId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-people-by-event-id", Name = "GeteventPeopleByEventId")]
        public async Task<IActionResult> GetEventPeopleByEventId([FromQuery] Guid eventId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var eventPeoples = await _repository.EventPeople.GetEventPeopleByEventId(eventId, pageSize, pageIndex);
                _logger.LogInfo($"Returned event people by event id from database.");

                //var visitingPeoplesResult = _mapper.Map<IEnumerable<VisitingPeopleDto>>(visitingPeoples);

                return Ok(eventPeoples);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventPeopleByEventId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-people-by-id/{id}", Name = "GetEventPeopleById")]
        public async Task<IActionResult> GetEventPeopleById([FromQuery] Guid eventPeopleId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var eventPeople = await _repository.EventPeople.GetEventPeopleById(eventPeopleId);
                if (eventPeople == null)
                {
                    _logger.LogError($"Event People with id: {eventPeopleId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Event People with id: {eventPeopleId}");
                    var eventPeopleResult = _mapper.Map<EventPeopleDto>(eventPeople);
                    return Ok(eventPeopleResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventPeopleById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-event-people", Name = "InsertEventPeople")]
        public IActionResult CreateEventPeople([FromBody] EventPeopleCreationMapDto eventPeople)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventPeople == null)
                {
                    _logger.LogError("Event People object sent from client is null.");
                    return BadRequest("Event People object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event People object sent from client.");
                    return BadRequest("Invalid model object");
                }

                if (eventPeople.EmployeeId.Count() > 0)
                {
                    foreach (var sub in eventPeople.EmployeeId)
                    {
                        EventPeopleForCreationDto eventPeopleObj = new EventPeopleForCreationDto();
                        eventPeopleObj.EmployeeId = Guid.Parse(sub);
                        eventPeopleObj.EventId = eventPeople.EventId;
                        eventPeopleObj.CreatedAt = eventPeople.CreatedAt;
                        eventPeopleObj.CreatedBy = eventPeople.CreatedBy;
                        eventPeopleObj.UpdatedAt = eventPeople.UpdatedAt;
                        eventPeopleObj.UpdatedBy = eventPeople.UpdatedBy;
                        var eventPeopleEntity = _mapper.Map<EventPeoples>(eventPeopleObj);
                        _repository.EventPeople.CreateEventPeople(eventPeopleEntity);
                        _repository.Save();
                    }
                    return CreatedAtRoute("EventPeopleById", new { id = eventPeople.EmployeeId }, eventPeople);
                }
                else
                {
                    _logger.LogError("Event People Employee Id sent from client is null.");
                    return BadRequest("Event People Employee Id is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEventPeople action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-event-people/{id}")]
        public async Task<IActionResult> UpdateEventPeople([FromQuery] Guid id, [FromBody] EventPeopleForUpdateDto eventPeople)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventPeople == null)
                {
                    _logger.LogError("Event People object sent from client is null.");
                    return BadRequest("Event People object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid event people object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var eventPeopleEntity = await _repository.EventPeople.GetEventPeopleById(id);
                if (eventPeopleEntity == null)
                {
                    _logger.LogError($"Event People with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(eventPeople, eventPeopleEntity);
                _repository.EventPeople.UpdateEventPeople(eventPeopleEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEventPeople action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-event-people/{id}")]
        public async Task<IActionResult> DeleteEventPeople([FromQuery] Guid id, [FromBody] EventPeopleForDeletionDto eventPeople)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventPeople == null)
                {
                    _logger.LogError("Event People object sent from client is null.");
                    return BadRequest("Event People object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid event people object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var eventPeopleEntity = await _repository.EventPeople.GetEventPeopleById(id);
                if (eventPeopleEntity == null)
                {
                    _logger.LogError($"Event People with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(eventPeople, eventPeopleEntity);
                _repository.EventPeople.DeleteEventPeople(eventPeopleEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEventPeople action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
