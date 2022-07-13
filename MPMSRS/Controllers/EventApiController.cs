using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Interfaces.FCM;

namespace MPMSRS.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private INotificationService _notification;

        public EventApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, INotificationService notification)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _notification = notification;
        }

        [Authorize]
        [HttpGet("get-all-events", Name = "GetEventsAll")]
        public async Task<IActionResult> GetAllEvents([FromQuery] string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var events = await _repository.Event.GetAllEvents(pageSize, pageIndex, status, startDate, endDate, area, query);
                _logger.LogInfo($"Returned all events from database.");

                //var visitingsResult = _mapper.Map<IEnumerable<VisitingDto>>(visitings);

                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEvents action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-by-id/{id}", Name = "EventById")]
        public async Task<IActionResult> GetEventById([FromQuery] Guid eventId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var events = await _repository.Event.GetEventDetailById(eventId);
                if (events == null)
                {
                    _logger.LogError($"Event with id: {eventId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Event with id: {eventId}");
                    //var VisitingResult = _mapper.Map<VisitingDto>(visiting);
                    return Ok(events);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-is-online/{id}", Name = "EventIsOnline")]
        public async Task<IActionResult> GetEventIsOnline([FromQuery] Guid eventId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var events = await _repository.Event.GetEventById(eventId);
                if (events == null)
                {
                    _logger.LogError($"Event with id: {eventId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Event with id: {eventId}");
                    return Ok(events.IsOnline);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventIsOnline action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-status-by-people-login-id", Name = "GetEventStatusByPeopleLoginId")]
        public async Task<IActionResult> GetEventStatusByPeopleLoginId([FromQuery] string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var events = await _repository.Event.GetAllEventByPeopleLoginId(userId, status, pageSize, pageIndex, startDate, endDate, area, query);
                _logger.LogInfo($"Returned all event by login id from database.");

                //var userNetworksResult = _mapper.Map<IEnumerable<NetworkDto>>(userNetworks);

                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventStatusByPeopleLoginId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-status-by-create-login-id", Name = "GetEventStatusByCreateLoginId")]
        public async Task<IActionResult> GetEventStatusByCreateLoginId([FromQuery] string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var events = await _repository.Event.GetAllEventByCreateLoginId(userId, status, pageSize, pageIndex, startDate, endDate, area, query);
                _logger.LogInfo($"Returned all visiting by login id from database.");

                //var userNetworksResult = _mapper.Map<IEnumerable<NetworkDto>>(userNetworks);

                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventStatusByCreateLoginId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-event", Name = "InsertEvent")]
        public IActionResult CreateEvent([FromBody] EventForCreationDto events)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (events == null)
                {
                    _logger.LogError("Event object sent from client is null.");
                    return BadRequest("Event object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dateNow = DateTime.Now;

                var EventEntity = _mapper.Map<Events>(events);
                _repository.Event.CreateEvent(EventEntity);
                _repository.Save();
                var createdEvent = _mapper.Map<EventDto>(EventEntity);

                if (events.EmployeeId.Count() > 0)
                {
                    foreach (var sub in events.EmployeeId)
                    {
                        EventPeopleForCreationDto eventPeopleObj = new EventPeopleForCreationDto();
                        eventPeopleObj.EventId = createdEvent.EventId;
                        eventPeopleObj.EmployeeId = Guid.Parse(sub);
                        eventPeopleObj.CreatedAt = dateNow;
                        eventPeopleObj.CreatedBy = events.CreatedBy;
                        eventPeopleObj.UpdatedAt = dateNow;
                        eventPeopleObj.UpdatedBy = events.UpdatedBy;
                        var eventPeopleEntity = _mapper.Map<EventPeoples>(eventPeopleObj);
                        _repository.EventPeople.CreateEventPeople(eventPeopleEntity);
                        _repository.Save();
                    }
                }

                return CreatedAtRoute("EventById", new { id = createdEvent.EventId }, createdEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEvent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-event/{id}")]
        public async Task<IActionResult> UpdateEvent([FromQuery] Guid id, [FromBody] EventForUpdateDto events)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (events == null)
                {
                    _logger.LogError("Event object sent from client is null.");
                    return BadRequest("Event object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var EventEntity = await _repository.Event.GetEventById(id);
                if (EventEntity == null)
                {
                    _logger.LogError($"Event with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                var dateNow = DateTime.Now;

                _mapper.Map(events, EventEntity);
                _repository.Event.UpdateEvent(EventEntity);
                _repository.Save();
                var updatedEvent = _mapper.Map<EventDto>(EventEntity);

                if (events.EmployeeId.Count() > 0)
                {
                    var eventPeoples = await _repository.EventPeople.GetVisitingPeopleByEventIdWithoutPage(id);
                    _repository.EventPeople.DeleteEventPeopleByEventId(eventPeoples.ToList());
                    _repository.Save();
                    foreach (var sub in events.EmployeeId)
                    {
                        EventPeopleForCreationDto eventPeopleObj = new EventPeopleForCreationDto();
                        eventPeopleObj.EventId = updatedEvent.EventId;
                        eventPeopleObj.EmployeeId = Guid.Parse(sub);
                        eventPeopleObj.CreatedAt = dateNow;
                        eventPeopleObj.CreatedBy = events.CreatedBy;
                        eventPeopleObj.UpdatedAt = dateNow;
                        eventPeopleObj.UpdatedBy = events.UpdatedBy;
                        var eventPeopleEntity = _mapper.Map<EventPeoples>(eventPeopleObj);
                        _repository.EventPeople.CreateEventPeople(eventPeopleEntity);
                        _repository.Save();
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEvent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-event/{id}")]
        public async Task<IActionResult> DeleteEvent([FromQuery] Guid id, [FromBody] EventForDeletionDto events)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (events == null)
                {
                    _logger.LogError("Event object sent from client is null.");
                    return BadRequest("Event object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var eventEntity = await _repository.Event.GetEventById(id);
                if (eventEntity == null)
                {
                    _logger.LogError($"Event with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(events, eventEntity);
                _repository.Event.DeleteEvent(eventEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEvent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
