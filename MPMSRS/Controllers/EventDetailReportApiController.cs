using System;
using System.Collections.Generic;
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

namespace MPMSRS.Controllers
{
    [Route("api/event-detail-reports")]
    [ApiController]
    public class EventDetailReportApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private FileServices _fileService;
        private INotificationService _notification;

        public EventDetailReportApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, FileServices fs, INotificationService notification)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _fileService = fs;
            _notification = notification;
        }

        [Authorize]
        [HttpGet("get-all-event-detail-reports", Name = "GetAllEventDetailReports")]
        public async Task<IActionResult> GetAllEventDetailReports([FromQuery] Guid eventId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var events = await _repository.EventDetailReport.GetAllEventDetailReport(eventId, pageSize, pageIndex);
                _logger.LogInfo($"Returned all event detail report from database.");

                //var visitingsResult = _mapper.Map<IEnumerable<VisitingDto>>(visitings);

                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEventDetailReports action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-event-detail-report-list", Name = "GetAllEventDetailReportList")]
        public async Task<IActionResult> GetAllEventDetailReportList([FromQuery] Guid eventId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var events = await _repository.EventDetailReport.GetEventDetailReportIdentification(eventId, pageSize, pageIndex);
                _logger.LogInfo($"Returned all event detail report list from database.");

                //var visitingsResult = _mapper.Map<IEnumerable<VisitingDto>>(visitings);

                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEventDetailReportList action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-event-detail-by-id", Name = "GetEventDetailReportId")]
        public async Task<IActionResult> GetEventDetailReportId([FromQuery] Guid eventDetailReportId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var eventDetailReport = await _repository.EventDetailReport.GetEventDetailReportByEventDetailReportId(eventDetailReportId);
                if (eventDetailReport == null)
                {
                    _logger.LogError($"Event detail report with id: {eventDetailReportId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Event detail report with id: {eventDetailReportId}");
                    //var VisitingResult = _mapper.Map<VisitingDto>(visiting);
                    return Ok(eventDetailReport);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEventDetailReportId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-event-detail-report", Name = "InsertEventDetailReport")]
        public async Task<IActionResult> CreateEventDetailReport([FromBody] EventDetailReportForCreationDto eventDetailReportDto)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventDetailReportDto == null)
                {
                    _logger.LogError("Event detail report object sent from client is null.");
                    return BadRequest("Event detail report object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event detail report object sent from client.");
                    return BadRequest("Invalid model object");
                }

                EventDetailReportForCreationDtoModel edrObj = new EventDetailReportForCreationDtoModel();
                edrObj.EventId = eventDetailReportDto.EventId;
                edrObj.EventMasterDataId = eventDetailReportDto.EventMasterDataId;
                edrObj.DivisionId = eventDetailReportDto.DivisionId;
                edrObj.EventDetailReportProblemIdentification = eventDetailReportDto.EventDetailReportProblemIdentification;
                edrObj.EventCAProblemIdentification = eventDetailReportDto.EventCAProblemIdentification;
                edrObj.EventDetailReportStatus = eventDetailReportDto.EventDetailReportStatus;
                edrObj.EventDetailReportDeadline = eventDetailReportDto.EventDetailReportDeadline;
                edrObj.EventDetailReportFlagging = eventDetailReportDto.EventDetailReportFlagging;
                edrObj.CreatedAt = eventDetailReportDto.CreatedAt;
                edrObj.CreatedBy = eventDetailReportDto.CreatedBy;
                edrObj.UpdatedAt = eventDetailReportDto.UpdatedAt;
                edrObj.UpdatedBy = eventDetailReportDto.UpdatedBy;

                var EventDetailReportEntity = _mapper.Map<EventDetailReports>(edrObj);
                _repository.EventDetailReport.CreateEventDetailReport(EventDetailReportEntity);
                _repository.Save();
                var createdEventDetailReport = _mapper.Map<EventDetailReportDtoModel>(EventDetailReportEntity);

                List<string> tempNamaPC = new List<string>();

                if (eventDetailReportDto.PCList != null)
                {
                    if (eventDetailReportDto.PCList.Count() > 0)
                    {
                        foreach (var sub in eventDetailReportDto.PCList)
                        {
                            EventDetailReportProblemCategoryForCreationDto eventDetailReportPCObj = new EventDetailReportProblemCategoryForCreationDto();
                            eventDetailReportPCObj.ProblemCategoryId = sub.ProblemCategoryId;
                            eventDetailReportPCObj.EventDetailReportId = createdEventDetailReport.EventDetailReportId;
                            eventDetailReportPCObj.EventDetailReportPCName = sub.EventDetailReportPCName;
                            eventDetailReportPCObj.CreatedAt = sub.CreatedAt;
                            eventDetailReportPCObj.CreatedBy = sub.CreatedBy;
                            eventDetailReportPCObj.UpdatedAt = sub.UpdatedAt;
                            eventDetailReportPCObj.UpdatedBy = sub.UpdatedBy;
                            var eventDetailReportPCEntity = _mapper.Map<EventDetailReportProblemCategories>(eventDetailReportPCObj);
                            _repository.EventDetailReportProblemCategory.CreateEventDetailReportProblemCategories(eventDetailReportPCEntity);
                            _repository.Save();

                            tempNamaPC.Add(eventDetailReportPCObj.EventDetailReportPCName);
                        }
                    }
                }

                if (eventDetailReportDto.PICList != null)
                {
                    if (eventDetailReportDto.PICList.Count() > 0)
                    {
                        foreach (var sub in eventDetailReportDto.PICList)
                        {
                            EventDetailReportPicForCreationViewDto eventDetailReportPICObj = new EventDetailReportPicForCreationViewDto();
                            eventDetailReportPICObj.EventDetailReportId = createdEventDetailReport.EventDetailReportId;
                            eventDetailReportPICObj.EmployeeId = sub.EmployeeId;
                            eventDetailReportPICObj.CreatedAt = sub.CreatedAt;
                            eventDetailReportPICObj.CreatedBy = sub.CreatedBy;
                            eventDetailReportPICObj.UpdatedAt = sub.UpdatedAt;
                            eventDetailReportPICObj.UpdatedBy = sub.UpdatedBy;
                            var eventDetailReportPICEntity = _mapper.Map<EventDetailReportPICs>(eventDetailReportPICObj);
                            _repository.EventDetailReportPIC.CreateEventDetailReportPIC(eventDetailReportPICEntity);
                            _repository.Save();
                        }
                    }
                }

                if (eventDetailReportDto.AttachmentList != null)
                {
                    if (eventDetailReportDto.AttachmentList.Count() > 0)
                    {
                        foreach (var sub in eventDetailReportDto.AttachmentList)
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

                            EventDetailReportAttachmentForCreationViewDto eventDetailReportAttachmentObj = new EventDetailReportAttachmentForCreationViewDto();
                            eventDetailReportAttachmentObj.EventDetailReportId = createdEventDetailReport.EventDetailReportId;
                            eventDetailReportAttachmentObj.AttachmentId = blobId;
                            eventDetailReportAttachmentObj.CreatedAt = sub.CreatedAt;
                            eventDetailReportAttachmentObj.CreatedBy = sub.CreatedBy;
                            eventDetailReportAttachmentObj.UpdatedAt = sub.UpdatedAt;
                            eventDetailReportAttachmentObj.UpdatedBy = sub.UpdatedBy;
                            var eventDetailReportAttachmentEntity = _mapper.Map<EventDetailReportAttachments>(eventDetailReportAttachmentObj);
                            _repository.EventDetailReportAttachment.CreateEventDetailReportAttachment(eventDetailReportAttachmentEntity);
                            _repository.Save();
                        }
                    }
                }

                EventCAForCreationDto correctiveActionObj = new EventCAForCreationDto();
                correctiveActionObj.EventDetailReportId = createdEventDetailReport.EventDetailReportId;
                if (eventDetailReportDto.PCList != null)
                {
                    correctiveActionObj.EventCAName = eventDetailReportDto.PCList[0].EventDetailReportPCName;
                    if (eventDetailReportDto.PCList.Count() == 2)
                    {
                        correctiveActionObj.EventCAName = eventDetailReportDto.PCList[0].EventDetailReportPCName + " - " + eventDetailReportDto.PCList[1].EventDetailReportPCName;
                    }
                    if (eventDetailReportDto.PCList.Count() == 3)
                    {
                        correctiveActionObj.EventCAName = eventDetailReportDto.PCList[0].EventDetailReportPCName + " - " + eventDetailReportDto.PCList[1].EventDetailReportPCName + " - " + eventDetailReportDto.PCList[2].EventDetailReportPCName;
                    }
                }
                else
                {
                    correctiveActionObj.EventCAName = "temuan";
                }
                correctiveActionObj.ProgressBy = "";
                correctiveActionObj.ValidateBy = "";
                correctiveActionObj.EventCADetail = "";
                correctiveActionObj.CreatedAt = eventDetailReportDto.CreatedAt;
                correctiveActionObj.CreatedBy = eventDetailReportDto.CreatedBy;
                correctiveActionObj.UpdatedAt = eventDetailReportDto.UpdatedAt;
                correctiveActionObj.UpdatedBy = eventDetailReportDto.UpdatedBy;
                var correctiveActionEntity = _mapper.Map<EventCAs>(correctiveActionObj);
                _repository.EventCA.CreateEventCorrectiveActions(correctiveActionEntity);
                _repository.Save();

                return CreatedAtRoute("InsertEventDetailReport", new { id = createdEventDetailReport.EventDetailReportId }, createdEventDetailReport);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEventDetailReport action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-event-detail-report", Name = "UpdateEventDetailReport")]
        public async Task<IActionResult> UpdateEventDetailReport([FromBody] EventDetailReportForUpdateDto eventDetailReportDto)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventDetailReportDto == null)
                {
                    _logger.LogError("Event detail report object sent from client is null.");
                    return BadRequest("Event detail report object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event detail report object sent from client.");
                    return BadRequest("Invalid model object");
                }

                EventDetailReportForUpdateDtoModel edrObj = new EventDetailReportForUpdateDtoModel();
                edrObj.EventDetailReportId = eventDetailReportDto.EventDetailReportId;
                edrObj.EventId = eventDetailReportDto.EventId;
                edrObj.EventMasterDataId = eventDetailReportDto.EventMasterDataId;
                edrObj.DivisionId = eventDetailReportDto.DivisionId;
                edrObj.EventDetailReportProblemIdentification = eventDetailReportDto.EventDetailReportProblemIdentification;
                edrObj.EventCAProblemIdentification = eventDetailReportDto.EventCAProblemIdentification;
                edrObj.EventDetailReportStatus = eventDetailReportDto.EventDetailReportStatus;
                edrObj.EventDetailReportDeadline = eventDetailReportDto.EventDetailReportDeadline;
                edrObj.EventDetailReportFlagging = eventDetailReportDto.EventDetailReportFlagging;
                edrObj.CreatedAt = eventDetailReportDto.CreatedAt;
                edrObj.CreatedBy = eventDetailReportDto.CreatedBy;
                edrObj.UpdatedAt = eventDetailReportDto.UpdatedAt;
                edrObj.UpdatedBy = eventDetailReportDto.UpdatedBy;

                var EventDetailReportEntity = _mapper.Map<EventDetailReports>(edrObj);
                _repository.EventDetailReport.UpdateEventDetailReport(EventDetailReportEntity);
                _repository.Save();
                var updatedEventDetailReport = _mapper.Map<EventDetailReportDtoModel>(EventDetailReportEntity);

                List<string> tempNamaPC = new List<string>();

                if (eventDetailReportDto.PCList.Count() > 0)
                {
                    var eventDetailReportPC = await _repository.EventDetailReportProblemCategory.GetEventDetailReportPCByEventDetailReportIdWithoutPage(eventDetailReportDto.EventDetailReportId);
                    _repository.EventDetailReportProblemCategory.DeleteEventDetailReportPCByEventDetailReportId(eventDetailReportPC.ToList());
                    _repository.Save();
                    foreach (var sub in eventDetailReportDto.PCList)
                    {
                        EventDetailReportProblemCategoryForCreationDto eventDetailReportPCObj = new EventDetailReportProblemCategoryForCreationDto();
                        eventDetailReportPCObj.ProblemCategoryId = sub.ProblemCategoryId;
                        eventDetailReportPCObj.EventDetailReportId = updatedEventDetailReport.EventDetailReportId;
                        eventDetailReportPCObj.EventDetailReportPCName = sub.EventDetailReportPCName;
                        eventDetailReportPCObj.CreatedAt = sub.CreatedAt;
                        eventDetailReportPCObj.CreatedBy = sub.CreatedBy;
                        eventDetailReportPCObj.UpdatedAt = sub.UpdatedAt;
                        eventDetailReportPCObj.UpdatedBy = sub.UpdatedBy;
                        var eventDetailReportPCEntity = _mapper.Map<EventDetailReportProblemCategories>(eventDetailReportPCObj);
                        _repository.EventDetailReportProblemCategory.CreateEventDetailReportProblemCategories(eventDetailReportPCEntity);
                        _repository.Save();

                        tempNamaPC.Add(eventDetailReportPCObj.EventDetailReportPCName);
                    }
                }

                var eventDetailReportPIC = await _repository.EventDetailReportPIC.GetEventDetailReportPICByEventDetailReportIdWithoutPage(eventDetailReportDto.EventDetailReportId);
                if (eventDetailReportPIC != null)
                {
                    if (eventDetailReportPIC.Count() > 0)
                    {
                        _repository.EventDetailReportPIC.DeleteEventDetailReportPICByEventDetailReportId(eventDetailReportPIC.ToList());
                        _repository.Save();
                    }
                }

                if (eventDetailReportDto.PICList != null)
                {
                    if (eventDetailReportDto.PICList.Count() > 0)
                    {
                        foreach (var sub in eventDetailReportDto.PICList)
                        {
                            EventDetailReportPicForCreationViewDto eventDetailReportPICObj = new EventDetailReportPicForCreationViewDto();
                            eventDetailReportPICObj.EventDetailReportId = updatedEventDetailReport.EventDetailReportId;
                            eventDetailReportPICObj.EmployeeId = sub.EmployeeId;
                            eventDetailReportPICObj.CreatedAt = sub.CreatedAt;
                            eventDetailReportPICObj.CreatedBy = sub.CreatedBy;
                            eventDetailReportPICObj.UpdatedAt = sub.UpdatedAt;
                            eventDetailReportPICObj.UpdatedBy = sub.UpdatedBy;
                            var eventDetailReportPICEntity = _mapper.Map<EventDetailReportPICs>(eventDetailReportPICObj);
                            _repository.EventDetailReportPIC.CreateEventDetailReportPIC(eventDetailReportPICEntity);
                            _repository.Save();
                        }
                    }
                }

                if (eventDetailReportDto.AttachmentList != null)
                {
                    if (eventDetailReportDto.AttachmentList.Count() > 0)
                    {
                        var eventDetailReportAttachment = await _repository.EventDetailReportAttachment.GetEventDetailReportAttachmentByEventDetailReportIdWithoutPage(eventDetailReportDto.EventDetailReportId);
                        _repository.EventDetailReportAttachment.DeleteEventDetailReportAttachmentByEventDetailReportId(eventDetailReportAttachment.ToList());
                        _repository.Save();

                        foreach (var sub in eventDetailReportDto.AttachmentList)
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

                            EventDetailReportAttachmentForCreationViewDto eventDetailReportAttachmentObj = new EventDetailReportAttachmentForCreationViewDto();
                            eventDetailReportAttachmentObj.EventDetailReportId = updatedEventDetailReport.EventDetailReportId;
                            eventDetailReportAttachmentObj.AttachmentId = blobId;
                            eventDetailReportAttachmentObj.CreatedAt = sub.CreatedAt;
                            eventDetailReportAttachmentObj.CreatedBy = sub.CreatedBy;
                            eventDetailReportAttachmentObj.UpdatedAt = sub.UpdatedAt;
                            eventDetailReportAttachmentObj.UpdatedBy = sub.UpdatedBy;
                            var eventDetailReportAttachmentEntity = _mapper.Map<EventDetailReportAttachments>(eventDetailReportAttachmentObj);
                            _repository.EventDetailReportAttachment.CreateEventDetailReportAttachment(eventDetailReportAttachmentEntity);
                            _repository.Save();
                        }
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEventDetailReport action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-event-detail-report/{id}")]
        public async Task<IActionResult> DeleteEventDetailReport([FromQuery] Guid id, [FromBody] EventDetailReportForDeletionDto eventDetailReport)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (eventDetailReport == null)
                {
                    _logger.LogError("Event detail report object sent from client is null.");
                    return BadRequest("Event detail report object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Event detail report object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var eventDetailReportEntity = await _repository.EventDetailReport.GetEventDetailReportById(id);
                if (eventDetailReportEntity == null)
                {
                    _logger.LogError($"Event detail report with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(eventDetailReport, eventDetailReportEntity);
                _repository.EventDetailReport.DeleteEventDetailReport(eventDetailReportEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEventDetailReport action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
