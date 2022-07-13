using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMSRS.Helpers.Utilities;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Interfaces.FCM;
using MPMSRS.Services.Repositories.Auth;

namespace MPMSRS.Controllers
{
    [Route("api/event-corrective-actions")]
    [ApiController]
    public class EventCAApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private DatabaseRefreshTokenRepository _services;
        private FileServices _fileService;
        private INotificationService _notification;

        public EventCAApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, DatabaseRefreshTokenRepository service, FileServices fs, INotificationService notification)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _services = service;
            _fileService = fs;
            _notification = notification;
        }

        [Authorize]
        [HttpGet("get-event-corrective-action-by-roles", Name = "GetEventCorrectiveActionByRoleAll")]
        public async Task<IActionResult> GetAllEventCorrectiveActionRole([FromQuery] int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var companyId = await _repository.User.GetCompanyIdByEmployeeId(userId);
                if (companyId.Contains("MPM"))
                {
                    var caList = await _repository.EventCA.GetListCAByMainDealer(userId, pageSize, pageIndex, status, startDate, endDate, area, query);

                    _logger.LogInfo($"Returned all event corrective action from database.");

                    //var caResult = _mapper.Map<IEnumerable<CorrectiveActionDto>>(caList);

                    return Ok(caList);
                }
                else
                {
                    var caList = await _repository.EventCA.GetListCAByEventMasterDataLocation(companyId.Trim(), pageSize, pageIndex, status, startDate, endDate, area, query);

                    _logger.LogInfo($"Returned all event corrective action from database.");

                    //var caResult = _mapper.Map<IEnumerable<CorrectiveActionDto>>(caList);

                    return Ok(caList);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEventCorrectiveActionRole action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-list-event-corrective-action-by-tagged-pic", Name = "GetAllEventCorrectiveActionByPICTagged")]
        public async Task<IActionResult> GetAllEventCorrectiveActionByPICTagged([FromQuery] int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var correctiveAction = await _repository.EventCA.GetListCAByPICTagged(userId, pageSize, pageIndex, status, startDate, endDate, area, query);
                if (correctiveAction == null)
                {
                    _logger.LogError($"Event Corrective Action with user id: {userId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned event corrective action with user id: {userId}");
                    //var correctiveActionResult = _mapper.Map<CorrectiveActionDto>(correctiveAction);
                    return Ok(correctiveAction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEventCorrectiveActionByPICTagged action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-list-user-of-event-corrective-actions", Name = "GetAllUserEventCorrectiveActions")]
        public async Task<IActionResult> GetAllUserEventCorrectiveActions([FromQuery] int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var correctiveAction = await _repository.EventCA.GetListAllUserCA(pageSize, pageIndex, status, startDate, endDate, area, query);
                if (correctiveAction == null)
                {
                    _logger.LogError("Event Corrective Action, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo("Returned all users event corrective actions");
                    //var correctiveActionResult = _mapper.Map<CorrectiveActionDto>(correctiveAction);
                    return Ok(correctiveAction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUserEventCorrectiveActions action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-list-event-corrective-action-by-master-data", Name = "GetAllEventCorrectiveActionByMasterData")]
        public async Task<IActionResult> GetAllCorrectiveActionByMasterData([FromQuery] Guid eventMasterDataId, string status, int pageSize, int pageIndex, string sortBy)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var correctiveAction = await _repository.EventCA.GetListCAByEventMasterData(eventMasterDataId, pageSize, pageIndex, status, sortBy);
                if (correctiveAction == null)
                {
                    _logger.LogError($"Event Corrective Action with Event Master Data id: {eventMasterDataId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned event corrective action with Event Master Data id: {eventMasterDataId}");
                    //var correctiveActionResult = _mapper.Map<CorrectiveActionDto>(correctiveAction);
                    return Ok(correctiveAction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCorrectiveActionByMasterData action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-corrective-action-by-id/{id}", Name = "EventCorrectiveActionById")]
        public async Task<IActionResult> GetEventCorrectiveActionById([FromQuery] Guid eventCAId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var correctiveAction = await _repository.EventCA.GetEventCorrectiveActionDetailById(eventCAId);
                if (correctiveAction == null)
                {
                    _logger.LogError($"Event Corrective Action with id: {eventCAId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned event corrective action with id: {eventCAId}");
                    //var correctiveActionResult = _mapper.Map<CorrectiveActionDto>(correctiveAction);
                    return Ok(correctiveAction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventCorrectiveActionById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-event-corrective-action/{id}")]
        public async Task<IActionResult> UpdateEventCorrectiveAction([FromQuery] Guid id, [FromBody] EventCAForUpdateDto eventCA)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventCA == null)
                {
                    _logger.LogError("Event Corrective Action object sent from client is null.");
                    return BadRequest("Event Corrective Action object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event Corrective Action object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var caEntity = await _repository.EventCA.GetEventCorrectiveActionById(id);
                if (caEntity == null)
                {
                    _logger.LogError($"Event Corrective Action with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var eventDetailReportEntity = await _repository.EventDetailReport.GetEventDetailReportById(eventCA.EventDetailReportId);
                eventDetailReportEntity.EventDetailReportStatus = eventCA.EventDetailReportStatus;
                _repository.EventDetailReport.UpdateEventDetailReport(eventDetailReportEntity);

                if (eventCA.AttachmentList != null)
                {
                    if (eventCA.AttachmentList.Count() > 0)
                    {
                        var correctiveActionAttachment = await _repository.EventCAAttachment.GetEventCAAttachmentByEventCAIdWithoutPage(eventCA.EventCAId);
                        _repository.EventCAAttachment.DeleteEventCAAttachmentByEventCAId(correctiveActionAttachment.ToList());
                        _repository.Save();

                        foreach (var sub in eventCA.AttachmentList)
                        {
                            var url = "";
                            var mime = "";
                            var blobId = Guid.Empty;

                            if (!string.IsNullOrEmpty(sub.ImageUrl))
                            {
                                url = sub.ImageUrl;
                                mime = System.IO.Path.GetExtension(url);
                                blobId = await _fileService.InsertBlob(url, mime);
                            }

                            EventCorrectivActionAttachmentForCreationDto correctiveActionAttachmentObj = new EventCorrectivActionAttachmentForCreationDto();
                            correctiveActionAttachmentObj.EventCAId = eventCA.EventCAId;
                            correctiveActionAttachmentObj.AttachmentId = blobId;
                            correctiveActionAttachmentObj.CreatedAt = sub.CreatedAt;
                            correctiveActionAttachmentObj.CreatedBy = sub.CreatedBy;
                            correctiveActionAttachmentObj.UpdatedAt = sub.UpdatedAt;
                            correctiveActionAttachmentObj.UpdatedBy = sub.UpdatedBy;
                            var correctiveActionAttachmentEntity = _mapper.Map<EventCAAttachments>(correctiveActionAttachmentObj);
                            _repository.EventCAAttachment.CreateEventCAAttachment(correctiveActionAttachmentEntity);
                            _repository.Save();
                        }
                    }
                }

                caEntity.ValidateBy = eventCA.ValidateBy;
                caEntity.ProgressBy = eventCA.ProgressBy;
                caEntity.EventCADetail = eventCA.EventCADetail;
                //_mapper.Map(correctiveAction, caEntity);
                _repository.EventCA.UpdateEventCorrectiveActions(caEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEventCorrectiveAction action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
