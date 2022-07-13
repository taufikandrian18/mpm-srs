using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPMSRS.Helpers.Utilities;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Interfaces.FCM;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MPMSRS.Controllers
{
    [Route("api/visiting-detail-reports")]
    [ApiController]
    public class VisitingDetailReportApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private FileServices _fileService;
        private INotificationService _notification;

        public VisitingDetailReportApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, FileServices fs, INotificationService notification)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _fileService = fs;
            _notification = notification;
        }

        [Authorize]
        [HttpGet("get-all-visiting-detail-reports", Name = "GetAllVisitingDetailReports")]
        public async Task<IActionResult> GetAllVisitingDetailReports([FromQuery] Guid visitingId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitings = await _repository.VisitingDetailReport.GetAllVisitingDetailReport(visitingId, pageSize, pageIndex);
                _logger.LogInfo($"Returned all visiting detail report from database.");

                //var visitingsResult = _mapper.Map<IEnumerable<VisitingDto>>(visitings);

                return Ok(visitings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisitingDetailReports action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-visiting-detail-report-list", Name = "GetAllVisitingDetailReportList")]
        public async Task<IActionResult> GetAllVisitingDetailReportList([FromQuery] Guid visitingId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitings = await _repository.VisitingDetailReport.GetVisitingDetailReportIdentification(visitingId, pageSize, pageIndex);
                _logger.LogInfo($"Returned all visiting detail report list from database.");

                //var visitingsResult = _mapper.Map<IEnumerable<VisitingDto>>(visitings);

                return Ok(visitings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisitingDetailReportList action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-detail-by-id", Name = "GetVisitingDetailReportId")]
        public async Task<IActionResult> GetVisitingDetailReportId([FromQuery] Guid visitingDetailReportId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitingDetailReport = await _repository.VisitingDetailReport.GetVisitingDetailReportByVisitingDetailReportId(visitingDetailReportId);
                if (visitingDetailReport == null)
                {
                    _logger.LogError($"Visiting with id: {visitingDetailReportId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Visiting with id: {visitingDetailReportId}");
                    //var VisitingResult = _mapper.Map<VisitingDto>(visiting);
                    return Ok(visitingDetailReport);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingDetailReportId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-visiting-detail-report", Name = "InsertVisitingDetailReport")]
        public async Task<IActionResult> CreateVisitingDetailReport([FromBody] VisitingDetailReportListForCreationDto visitingDetailReportDto)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingDetailReportDto == null)
                {
                    _logger.LogError("Visiting detail report object sent from client is null.");
                    return BadRequest("Visiting detail report object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting detail report object sent from client.");
                    return BadRequest("Invalid model object");
                }

                VisitingDetailReportForCreationDto vdrObj = new VisitingDetailReportForCreationDto();
                vdrObj.VisitingId = visitingDetailReportDto.VisitingId;
                vdrObj.NetworkId = visitingDetailReportDto.NetworkId;
                vdrObj.DivisionId = visitingDetailReportDto.DivisionId;
                vdrObj.VisitingDetailReportProblemIdentification = visitingDetailReportDto.VisitingDetailReportProblemIdentification;
                vdrObj.CorrectiveActionProblemIdentification = visitingDetailReportDto.CorrectiveActionProblemIdentification;
                vdrObj.VisitingDetailReportStatus = visitingDetailReportDto.VisitingDetailReportStatus;
                vdrObj.VisitingDetailReportDeadline = visitingDetailReportDto.VisitingDetailReportDeadline;
                vdrObj.VisitingDetailReportFlagging = visitingDetailReportDto.VisitingDetailReportFlagging;
                vdrObj.CreatedAt = visitingDetailReportDto.CreatedAt;
                vdrObj.CreatedBy = visitingDetailReportDto.CreatedBy;
                vdrObj.UpdatedAt = visitingDetailReportDto.UpdatedAt;
                vdrObj.UpdatedBy = visitingDetailReportDto.UpdatedBy;

                var VisitingDetailReportEntity = _mapper.Map<VisitingDetailReports>(vdrObj);
                _repository.VisitingDetailReport.CreateVisitingDetailReport(VisitingDetailReportEntity);
                _repository.Save();
                var createdVisitingDetailReport = _mapper.Map<VisitingDetailReportDtoModel>(VisitingDetailReportEntity);

                List<string> tempNamaPC = new List<string>();

                if (visitingDetailReportDto.PCList != null)
                {
                    if (visitingDetailReportDto.PCList.Count() > 0)
                    {
                        foreach (var sub in visitingDetailReportDto.PCList)
                        {
                            VisitingDetailReportProblemCategoryForCreationDto visitingDetailReportPCObj = new VisitingDetailReportProblemCategoryForCreationDto();
                            visitingDetailReportPCObj.ProblemCategoryId = sub.ProblemCategoryId;
                            visitingDetailReportPCObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                            visitingDetailReportPCObj.VisitingDetailReportPCName = sub.VisitingDetailReportPCName;
                            visitingDetailReportPCObj.CreatedAt = sub.CreatedAt;
                            visitingDetailReportPCObj.CreatedBy = sub.CreatedBy;
                            visitingDetailReportPCObj.UpdatedAt = sub.UpdatedAt;
                            visitingDetailReportPCObj.UpdatedBy = sub.UpdatedBy;
                            var visitingDetailReportPCEntity = _mapper.Map<VisitingDetailReportProblemCategories>(visitingDetailReportPCObj);
                            _repository.VisitingDetailReportProblemCategory.CreateVisitingDetailReportProblemCategories(visitingDetailReportPCEntity);
                            _repository.Save();

                            tempNamaPC.Add(visitingDetailReportPCObj.VisitingDetailReportPCName);
                        }
                    }
                }

                if (visitingDetailReportDto.PICList != null)
                {
                    if (visitingDetailReportDto.PICList.Count() > 0)
                    {
                        foreach (var sub in visitingDetailReportDto.PICList)
                        {
                            VisitingDetailReportPicForCreationDto visitingDetailReportPICObj = new VisitingDetailReportPicForCreationDto();
                            visitingDetailReportPICObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                            visitingDetailReportPICObj.EmployeeId = sub.EmployeeId;
                            visitingDetailReportPICObj.CreatedAt = sub.CreatedAt;
                            visitingDetailReportPICObj.CreatedBy = sub.CreatedBy;
                            visitingDetailReportPICObj.UpdatedAt = sub.UpdatedAt;
                            visitingDetailReportPICObj.UpdatedBy = sub.UpdatedBy;
                            var visitingDetailReportPICEntity = _mapper.Map<VisitingDetailReportPICs>(visitingDetailReportPICObj);
                            _repository.VisitingDetailReportPIC.CreateVisitingDetailReportPIC(visitingDetailReportPICEntity);
                            _repository.Save();

                            //disini push notif pica ke yang di tandai dengan body p1,p2,p3

                            if (!sub.EmployeeId.Equals(userId))
                            {
                                var namaPC = "";
                                foreach (var datum in tempNamaPC)
                                {
                                    namaPC += datum;
                                }

                                var createdByName = await _repository.User.GetUserById(Guid.Parse(vdrObj.CreatedBy));

                                NotificationModel mod = new NotificationModel();
                                var deviceId = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(sub.EmployeeId);
                                mod.DeviceId = deviceId.Token;
                                mod.Title = "Anda telah ditandai PICA seseorang";
                                mod.Body = "ditandai oleh " + createdByName.DisplayName + ", " + namaPC + ", " + vdrObj.CorrectiveActionProblemIdentification + ", " + vdrObj.VisitingDetailReportProblemIdentification + ", " + vdrObj.VisitingDetailReportDeadline + "";
                                mod.ScreenID = createdVisitingDetailReport.VisitingDetailReportId.ToString();
                                mod.Screen = "PICA";

                                await _notification.SendNotification(mod);

                                PushNotifications pnDto = new PushNotifications();
                                pnDto.PushNotificationId = Guid.NewGuid();
                                pnDto.EmployeeId = userId;
                                pnDto.RecipientId = sub.EmployeeId;
                                pnDto.PushNotificationTitle = mod.Title;
                                pnDto.PushNotificationBody = mod.Body;
                                pnDto.ScreenID = createdVisitingDetailReport.VisitingDetailReportId.ToString();
                                pnDto.Screen = "PICA";
                                pnDto.IsRead = false;
                                pnDto.CreatedAt = sub.CreatedAt;
                                pnDto.CreatedBy = sub.CreatedBy;
                                pnDto.UpdatedAt = sub.UpdatedAt;
                                pnDto.UpdatedBy = sub.UpdatedBy;

                                _repository.PushNotification.CreatePushNotification(pnDto);
                                _repository.Save();

                            }
                        }
                    }
                }

                if (visitingDetailReportDto.AttachmentList != null)
                {
                    if (visitingDetailReportDto.AttachmentList.Count() > 0)
                    {
                        foreach (var sub in visitingDetailReportDto.AttachmentList)
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

                            VisitingDetailReportAttachmentForCreationDto visitingDetailReportAttachmentObj = new VisitingDetailReportAttachmentForCreationDto();
                            visitingDetailReportAttachmentObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                            visitingDetailReportAttachmentObj.AttachmentId = blobId;
                            visitingDetailReportAttachmentObj.CreatedAt = sub.CreatedAt;
                            visitingDetailReportAttachmentObj.CreatedBy = sub.CreatedBy;
                            visitingDetailReportAttachmentObj.UpdatedAt = sub.UpdatedAt;
                            visitingDetailReportAttachmentObj.UpdatedBy = sub.UpdatedBy;
                            var visitingDetailReportAttachmentEntity = _mapper.Map<VisitingDetailReportAttachments>(visitingDetailReportAttachmentObj);
                            _repository.VisitingDetailReportAttachment.CreateVisitingDetailReportAttachment(visitingDetailReportAttachmentEntity);
                            _repository.Save();
                        }
                    }
                }

                CorrectiveActionForCreationDto correctiveActionObj = new CorrectiveActionForCreationDto();
                correctiveActionObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                if (visitingDetailReportDto.PCList != null)
                {
                    correctiveActionObj.CorrectiveActionName = visitingDetailReportDto.PCList[0].VisitingDetailReportPCName;
                    if (visitingDetailReportDto.PCList.Count() == 2)
                    {
                        correctiveActionObj.CorrectiveActionName = visitingDetailReportDto.PCList[0].VisitingDetailReportPCName + " - " + visitingDetailReportDto.PCList[1].VisitingDetailReportPCName;
                    }
                    if (visitingDetailReportDto.PCList.Count() == 3)
                    {
                        correctiveActionObj.CorrectiveActionName = visitingDetailReportDto.PCList[0].VisitingDetailReportPCName + " - " + visitingDetailReportDto.PCList[1].VisitingDetailReportPCName + " - " + visitingDetailReportDto.PCList[2].VisitingDetailReportPCName;
                    }
                }
                else
                {
                    correctiveActionObj.CorrectiveActionName = "temuan";
                }
                correctiveActionObj.ProgressBy = "";
                correctiveActionObj.ValidateBy = "";
                correctiveActionObj.CorrectiveActionDetail = "";
                correctiveActionObj.CreatedAt = visitingDetailReportDto.CreatedAt;
                correctiveActionObj.CreatedBy = visitingDetailReportDto.CreatedBy;
                correctiveActionObj.UpdatedAt = visitingDetailReportDto.UpdatedAt;
                correctiveActionObj.UpdatedBy = visitingDetailReportDto.UpdatedBy;
                var correctiveActionEntity = _mapper.Map<CorrectiveActions>(correctiveActionObj);
                _repository.CorrectiveAction.CreateCorrectiveActions(correctiveActionEntity);
                _repository.Save();

                var visiting = await _repository.Visiting.GetVisitingById(visitingDetailReportDto.VisitingId);
                if (visiting != null)
                {
                    //disini push notif ke visiting created by

                    var namaPC = "";
                    foreach (var datum in tempNamaPC)
                    {
                        namaPC += datum;
                    }

                    var createdByName = await _repository.User.GetUserById(Guid.Parse(visiting.CreatedBy));
                    var createdByNamePICA = await _repository.User.GetUserById(Guid.Parse(vdrObj.CreatedBy));

                    NotificationModel mod = new NotificationModel();
                    var deviceId = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(createdByName.EmployeeId);
                    mod.DeviceId = deviceId.Token;
                    mod.Title = "Ada Pica Baru di salah satu kunjungan anda";
                    mod.Body = "ditandai oleh " + createdByNamePICA.DisplayName + ", " + namaPC + ", " + vdrObj.CorrectiveActionProblemIdentification + ", " + vdrObj.VisitingDetailReportProblemIdentification + ", " + vdrObj.VisitingDetailReportDeadline + "";
                    mod.ScreenID = createdVisitingDetailReport.VisitingDetailReportId.ToString();
                    mod.Screen = "PICA";

                    await _notification.SendNotification(mod);

                    PushNotifications pnDto = new PushNotifications();
                    pnDto.PushNotificationId = Guid.NewGuid();
                    pnDto.EmployeeId = userId;
                    pnDto.RecipientId = createdByName.EmployeeId;
                    pnDto.PushNotificationTitle = mod.Title;
                    pnDto.PushNotificationBody = mod.Body;
                    pnDto.ScreenID = createdVisitingDetailReport.VisitingDetailReportId.ToString();
                    pnDto.Screen = "PICA";
                    pnDto.IsRead = false;
                    pnDto.CreatedAt = visitingDetailReportDto.CreatedAt;
                    pnDto.CreatedBy = visitingDetailReportDto.CreatedBy;
                    pnDto.UpdatedAt = visitingDetailReportDto.UpdatedAt;
                    pnDto.UpdatedBy = visitingDetailReportDto.UpdatedBy;

                    _repository.PushNotification.CreatePushNotification(pnDto);
                    _repository.Save();
                }

                return CreatedAtRoute("InsertVisitingDetailReport", new { id = createdVisitingDetailReport.VisitingDetailReportId }, createdVisitingDetailReport);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateVisitingDetailReport action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-visiting-detail-report", Name = "UpdateVisitingDetailReport")]
        public async Task<IActionResult> UpdateVisitingDetailReport([FromBody] VisitingDetailReportListForUpdateDto visitingDetailReportDto)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingDetailReportDto == null)
                {
                    _logger.LogError("Visiting detail report object sent from client is null.");
                    return BadRequest("Visiting detail report object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting detail report object sent from client.");
                    return BadRequest("Invalid model object");
                }

                VisitingDetailReportForUpdateDto vdrObj = new VisitingDetailReportForUpdateDto();
                vdrObj.VisitingDetailReportId = visitingDetailReportDto.VisitingDetailReportId;
                vdrObj.VisitingId = visitingDetailReportDto.VisitingId;
                vdrObj.NetworkId = visitingDetailReportDto.NetworkId;
                vdrObj.DivisionId = visitingDetailReportDto.DivisionId;
                vdrObj.VisitingDetailReportProblemIdentification = visitingDetailReportDto.VisitingDetailReportProblemIdentification;
                vdrObj.CorrectiveActionProblemIdentification = visitingDetailReportDto.CorrectiveActionProblemIdentification;
                vdrObj.VisitingDetailReportStatus = visitingDetailReportDto.VisitingDetailReportStatus;
                vdrObj.VisitingDetailReportDeadline = visitingDetailReportDto.VisitingDetailReportDeadline;
                vdrObj.VisitingDetailReportFlagging = visitingDetailReportDto.VisitingDetailReportFlagging;
                vdrObj.CreatedAt = visitingDetailReportDto.CreatedAt;
                vdrObj.CreatedBy = visitingDetailReportDto.CreatedBy;
                vdrObj.UpdatedAt = visitingDetailReportDto.UpdatedAt;
                vdrObj.UpdatedBy = visitingDetailReportDto.UpdatedBy;

                var VisitingDetailReportEntity = _mapper.Map<VisitingDetailReports>(vdrObj);
                _repository.VisitingDetailReport.UpdateVisitingDetailReport(VisitingDetailReportEntity);
                _repository.Save();
                var updatedVisitingDetailReport = _mapper.Map<VisitingDetailReportDtoModel>(VisitingDetailReportEntity);

                List<string> tempNamaPC = new List<string>();

                if (visitingDetailReportDto.PCList.Count() > 0)
                {
                    var visitingDetailReportPC = await _repository.VisitingDetailReportProblemCategory.GetVisitingDetailReportPCByVisitingDetailReportIdWithoutPage(visitingDetailReportDto.VisitingDetailReportId);
                    _repository.VisitingDetailReportProblemCategory.DeleteVisitingDetailReportPCByVisitingDetailReportId(visitingDetailReportPC.ToList());
                    _repository.Save();
                    foreach (var sub in visitingDetailReportDto.PCList)
                    {
                        VisitingDetailReportProblemCategoryForCreationDto visitingDetailReportPCObj = new VisitingDetailReportProblemCategoryForCreationDto();
                        visitingDetailReportPCObj.ProblemCategoryId = sub.ProblemCategoryId;
                        visitingDetailReportPCObj.VisitingDetailReportId = updatedVisitingDetailReport.VisitingDetailReportId;
                        visitingDetailReportPCObj.VisitingDetailReportPCName = sub.VisitingDetailReportPCName;
                        visitingDetailReportPCObj.CreatedAt = sub.CreatedAt;
                        visitingDetailReportPCObj.CreatedBy = sub.CreatedBy;
                        visitingDetailReportPCObj.UpdatedAt = sub.UpdatedAt;
                        visitingDetailReportPCObj.UpdatedBy = sub.UpdatedBy;
                        var visitingDetailReportPCEntity = _mapper.Map<VisitingDetailReportProblemCategories>(visitingDetailReportPCObj);
                        _repository.VisitingDetailReportProblemCategory.CreateVisitingDetailReportProblemCategories(visitingDetailReportPCEntity);
                        _repository.Save();

                        tempNamaPC.Add(visitingDetailReportPCObj.VisitingDetailReportPCName);
                    }
                }

                var visitingDetailReportPIC = await _repository.VisitingDetailReportPIC.GetVisitingDetailReportPICByVisitingDetailReportIdWithoutPage(visitingDetailReportDto.VisitingDetailReportId);
                if (visitingDetailReportPIC != null)
                {
                    if (visitingDetailReportPIC.Count() > 0)
                    {
                        _repository.VisitingDetailReportPIC.DeleteVisitingDetailReportPICByVisitingDetailReportId(visitingDetailReportPIC.ToList());
                        _repository.Save();
                    }
                }

                if (visitingDetailReportDto.PICList != null)
                {
                    if (visitingDetailReportDto.PICList.Count() > 0)
                    {
                        foreach (var sub in visitingDetailReportDto.PICList)
                        {
                            VisitingDetailReportPicForCreationDto visitingDetailReportPICObj = new VisitingDetailReportPicForCreationDto();
                            visitingDetailReportPICObj.VisitingDetailReportId = updatedVisitingDetailReport.VisitingDetailReportId;
                            visitingDetailReportPICObj.EmployeeId = sub.EmployeeId;
                            visitingDetailReportPICObj.CreatedAt = sub.CreatedAt;
                            visitingDetailReportPICObj.CreatedBy = sub.CreatedBy;
                            visitingDetailReportPICObj.UpdatedAt = sub.UpdatedAt;
                            visitingDetailReportPICObj.UpdatedBy = sub.UpdatedBy;
                            var visitingDetailReportPICEntity = _mapper.Map<VisitingDetailReportPICs>(visitingDetailReportPICObj);
                            _repository.VisitingDetailReportPIC.CreateVisitingDetailReportPIC(visitingDetailReportPICEntity);
                            _repository.Save();

                            //disini push notif pica ke yang di tandai dengan body p1,p2,p3

                            if (!sub.EmployeeId.Equals(userId))
                            {
                                var namaPC = "";
                                foreach (var datum in tempNamaPC)
                                {
                                    namaPC += datum;
                                }

                                var createdByName = await _repository.User.GetUserById(Guid.Parse(vdrObj.CreatedBy));

                                NotificationModel mod = new NotificationModel();
                                var deviceId = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(sub.EmployeeId);
                                mod.DeviceId = deviceId.Token;
                                mod.Title = "Anda telah ditandai PICA seseorang";
                                mod.Body = "ditandai oleh " + createdByName.DisplayName + ", " + namaPC + ", " + vdrObj.CorrectiveActionProblemIdentification + ", " + vdrObj.VisitingDetailReportProblemIdentification + ", " + vdrObj.VisitingDetailReportDeadline + "";
                                mod.ScreenID = updatedVisitingDetailReport.VisitingDetailReportId.ToString();
                                mod.Screen = "PICA";

                                await _notification.SendNotification(mod);

                                PushNotifications pnDto = new PushNotifications();
                                pnDto.PushNotificationId = Guid.NewGuid();
                                pnDto.EmployeeId = userId;
                                pnDto.RecipientId = sub.EmployeeId;
                                pnDto.PushNotificationTitle = mod.Title;
                                pnDto.PushNotificationBody = mod.Body;
                                pnDto.ScreenID = updatedVisitingDetailReport.VisitingDetailReportId.ToString();
                                pnDto.Screen = "PICA";
                                pnDto.IsRead = false;
                                pnDto.CreatedAt = sub.CreatedAt;
                                pnDto.CreatedBy = sub.CreatedBy;
                                pnDto.UpdatedAt = sub.UpdatedAt;
                                pnDto.UpdatedBy = sub.UpdatedBy;

                                _repository.PushNotification.CreatePushNotification(pnDto);
                                _repository.Save();
                            }
                        }
                    }
                }

                if (visitingDetailReportDto.AttachmentList != null)
                {
                    if (visitingDetailReportDto.AttachmentList.Count() > 0)
                    {
                        var visitingDetailReportAttachment = await _repository.VisitingDetailReportAttachment.GetVisitingDetailReportAttachmentByVisitingDetailReportIdWithoutPage(visitingDetailReportDto.VisitingDetailReportId);
                        _repository.VisitingDetailReportAttachment.DeleteVisitingDetailReportAttachmentByVisitingDetailReportId(visitingDetailReportAttachment.ToList());
                        _repository.Save();

                        foreach (var sub in visitingDetailReportDto.AttachmentList)
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

                            VisitingDetailReportAttachmentForCreationDto visitingDetailReportAttachmentObj = new VisitingDetailReportAttachmentForCreationDto();
                            visitingDetailReportAttachmentObj.VisitingDetailReportId = updatedVisitingDetailReport.VisitingDetailReportId;
                            visitingDetailReportAttachmentObj.AttachmentId = blobId;
                            visitingDetailReportAttachmentObj.CreatedAt = sub.CreatedAt;
                            visitingDetailReportAttachmentObj.CreatedBy = sub.CreatedBy;
                            visitingDetailReportAttachmentObj.UpdatedAt = sub.UpdatedAt;
                            visitingDetailReportAttachmentObj.UpdatedBy = sub.UpdatedBy;
                            var visitingDetailReportAttachmentEntity = _mapper.Map<VisitingDetailReportAttachments>(visitingDetailReportAttachmentObj);
                            _repository.VisitingDetailReportAttachment.CreateVisitingDetailReportAttachment(visitingDetailReportAttachmentEntity);
                            _repository.Save();
                        }
                    }
                }

                var visiting = await _repository.Visiting.GetVisitingById(vdrObj.VisitingId);
                if (visiting != null)
                {
                    //disini push notif ke visiting created by

                    var namaPC = "";
                    foreach (var datum in tempNamaPC)
                    {
                        namaPC += datum;
                    }

                    var createdByName = await _repository.User.GetUserById(Guid.Parse(visiting.CreatedBy));
                    var createdByNamePICA = await _repository.User.GetUserById(Guid.Parse(vdrObj.CreatedBy));

                    NotificationModel mod = new NotificationModel();
                    var deviceId = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(createdByName.EmployeeId);
                    mod.DeviceId = deviceId.Token;
                    mod.Title = "Ada Pica Baru di salah satu kunjungan anda";
                    mod.Body = "ditandai oleh " + createdByNamePICA.DisplayName + ", " + namaPC + ", " + vdrObj.CorrectiveActionProblemIdentification + ", " + vdrObj.VisitingDetailReportProblemIdentification + ", " + vdrObj.VisitingDetailReportDeadline + "";
                    mod.ScreenID = updatedVisitingDetailReport.VisitingDetailReportId.ToString();
                    mod.Screen = "PICA";

                    await _notification.SendNotification(mod);

                    PushNotifications pnDto = new PushNotifications();
                    pnDto.PushNotificationId = Guid.NewGuid();
                    pnDto.EmployeeId = userId;
                    pnDto.RecipientId = createdByName.EmployeeId;
                    pnDto.PushNotificationTitle = mod.Title;
                    pnDto.PushNotificationBody = mod.Body;
                    pnDto.ScreenID = updatedVisitingDetailReport.VisitingDetailReportId.ToString();
                    pnDto.Screen = "PICA";
                    pnDto.IsRead = false;
                    pnDto.CreatedAt = visitingDetailReportDto.CreatedAt;
                    pnDto.CreatedBy = visitingDetailReportDto.CreatedBy;
                    pnDto.UpdatedAt = visitingDetailReportDto.UpdatedAt;
                    pnDto.UpdatedBy = visitingDetailReportDto.UpdatedBy;

                    _repository.PushNotification.CreatePushNotification(pnDto);
                    _repository.Save();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVisitingDetailReport action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-visiting-detail-report-deadline/{id}")]
        public async Task<IActionResult> UpdateVisitingDetailReportDeadline([FromQuery] Guid id, [FromBody] VisitingDetailReportDeadlineUpdateDto visitingDetailReport)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingDetailReport == null)
                {
                    _logger.LogError("Visiting Detail Report object sent from client is null.");
                    return BadRequest("Visiting Detail Report object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting Detail Report object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var VisitingDetailReportEntity = await _repository.VisitingDetailReport.GetVisitingDetailReportById(id);
                if (VisitingDetailReportEntity == null)
                {
                    _logger.LogError($"Visiting Detail Report with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(visitingDetailReport, VisitingDetailReportEntity);
                _repository.VisitingDetailReport.UpdateVisitingDetailReport(VisitingDetailReportEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVisitingDetailReportDeadline action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-visiting-detail-report/{id}")]
        public async Task<IActionResult> DeleteVisitingDetailReport([FromQuery] Guid id, [FromBody] VisitingDetailReportForDeleteDto visitingDetailReport)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingDetailReport == null)
                {
                    _logger.LogError("Visiting detail report object sent from client is null.");
                    return BadRequest("Visiting detail report object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting detail report object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var visitingDetailReportEntity = await _repository.VisitingDetailReport.GetVisitingDetailReportById(id);
                if (visitingDetailReportEntity == null)
                {
                    _logger.LogError($"Visiting detail report with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(visitingDetailReport, visitingDetailReportEntity);
                _repository.VisitingDetailReport.DeleteVisitingDetailReport(visitingDetailReportEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVisitingDetailReport action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("visiting-detail-report-import-excel", Name = "ImportVisitingDetailReportExcel")]
        public async Task<IActionResult> VisitingDetailReportImportExcel([FromQuery] VisitingDetailReportListForCreationImportExcelDto visitingDetailReportDto, [FromForm] IEnumerable<IFormFile> files)
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

                if (visitingDetailReportDto != null)
                {
                    if (files.Count() > 0)
                    {
                        var file = files.FirstOrDefault();
                        if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                        {
                            _logger.LogError("file format sent from client is invalid.");
                            return BadRequest("file format object is invalid");
                        }

                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);

                            using (var package = new ExcelPackage(stream))
                            {
                                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                                if (worksheet == null)
                                {
                                    _logger.LogError("file template format sent from client is invalid.");
                                    return BadRequest("file template format object is invalid");
                                }

                                var rowCount = worksheet.Dimension.Rows;

                                for (int row = 2; row <= rowCount; row++)
                                {
                                    var createdBy = worksheet.Cells[row, 1].Value?.ToString().Trim();
                                    var kategori = worksheet.Cells[row, 2].Value?.ToString().Trim();
                                    var kategori1Prb = worksheet.Cells[row, 3].Value?.ToString().Trim();
                                    var Kategori2Prb = worksheet.Cells[row, 4].Value?.ToString().Trim();
                                    var kategori3Prb = worksheet.Cells[row, 5].Value?.ToString().Trim();
                                    var detail = worksheet.Cells[row, 6].Value?.ToString().Trim();
                                    var correctiveAction = worksheet.Cells[row, 7].Value?.ToString().Trim();
                                    var userRelated = worksheet.Cells[row, 8].Value?.ToString().Trim();
                                    var divisiRelated = worksheet.Cells[row, 9].Value?.ToString().Trim();
                                    var deadline = worksheet.Cells[row, 10].Value?.ToString().Trim();
                                    var progress = worksheet.Cells[row, 11].Value?.ToString().Trim();
                                    var tglProgress = worksheet.Cells[row, 12].Value?.ToString().Trim();
                                    var progressBy = worksheet.Cells[row, 13].Value?.ToString().Trim();
                                    var statusProgress = worksheet.Cells[row, 14].Value?.ToString().Trim();


                                    if (createdBy == null || kategori == null || detail == null || correctiveAction == null || userRelated == null || divisiRelated == null || deadline == null || statusProgress == null)
                                    {
                                        _logger.LogError($"data in row: {row}, can't be empty.");
                                        return BadRequest($"data in row: {row}, can't be empty.");
                                    }
                                    else
                                    {
                                        VisitingDetailReportForCreationDto vdrObj = new VisitingDetailReportForCreationDto();
                                        vdrObj.VisitingId = visitingDetailReportDto.VisitingId;
                                        vdrObj.NetworkId = visitingDetailReportDto.NetworkId;
                                        vdrObj.DivisionId = visitingDetailReportDto.DivisionId;
                                        vdrObj.VisitingDetailReportProblemIdentification = detail;
                                        vdrObj.CorrectiveActionProblemIdentification = correctiveAction;
                                        vdrObj.VisitingDetailReportStatus = statusProgress.ToLower();
                                        vdrObj.VisitingDetailReportDeadline = DateTime.Parse(deadline);
                                        vdrObj.VisitingDetailReportFlagging = kategori.ToLower();
                                        vdrObj.CreatedAt = thisDate;
                                        var createdTmp = await _repository.User.GetUserIdByUsername(createdBy);
                                        if(createdTmp == null)
                                        {
                                            _logger.LogError($"User createdBy in row : {row}, is undefined.");
                                            return BadRequest($"User createdBy in row: {row}, is undefined.");
                                        }
                                        else
                                        {
                                            vdrObj.CreatedBy = createdTmp.ToString();
                                            vdrObj.UpdatedAt = thisDate;
                                            vdrObj.UpdatedBy = createdTmp.ToString();
                                        }

                                        var VisitingDetailReportEntity = _mapper.Map<VisitingDetailReports>(vdrObj);
                                        _repository.VisitingDetailReport.CreateVisitingDetailReport(VisitingDetailReportEntity);
                                        _repository.Save();
                                        var createdVisitingDetailReport = _mapper.Map<VisitingDetailReportDtoModel>(VisitingDetailReportEntity);

                                        if (string.IsNullOrEmpty(kategori1Prb) || string.IsNullOrEmpty(Kategori2Prb) || string.IsNullOrEmpty(kategori3Prb))
                                        {
                                            if(!string.IsNullOrEmpty(kategori1Prb))
                                            {
                                                VisitingDetailReportProblemCategoryForCreationDto visitingDetailReportPCObj = new VisitingDetailReportProblemCategoryForCreationDto();
                                                var idTmp = await _repository.ProblemCategory.GetProblemCategoryIdByProblemCategoryName(kategori1Prb);
                                                if(idTmp == null)
                                                {
                                                    _logger.LogError($"Kategori problem 1 in row : {row}, is undefined.");
                                                    return BadRequest($"Kategori problem 1 in row: {row}, is undefined.");
                                                }
                                                else
                                                {
                                                    visitingDetailReportPCObj.ProblemCategoryId = idTmp;
                                                    visitingDetailReportPCObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                                                    visitingDetailReportPCObj.VisitingDetailReportPCName = kategori1Prb;
                                                    visitingDetailReportPCObj.CreatedAt = thisDate;
                                                    visitingDetailReportPCObj.CreatedBy = vdrObj.CreatedBy;
                                                    visitingDetailReportPCObj.UpdatedAt = thisDate;
                                                    visitingDetailReportPCObj.UpdatedBy = vdrObj.UpdatedBy;
                                                    var visitingDetailReportPCEntity = _mapper.Map<VisitingDetailReportProblemCategories>(visitingDetailReportPCObj);
                                                    _repository.VisitingDetailReportProblemCategory.CreateVisitingDetailReportProblemCategories(visitingDetailReportPCEntity);
                                                    _repository.Save();
                                                }
                                            }
                                            if(!string.IsNullOrEmpty(Kategori2Prb))
                                            {
                                                VisitingDetailReportProblemCategoryForCreationDto visitingDetailReportPCObj = new VisitingDetailReportProblemCategoryForCreationDto();
                                                var idTmp = await _repository.ProblemCategory.GetProblemCategoryIdByProblemCategoryName(Kategori2Prb);
                                                if (idTmp == null)
                                                {
                                                    _logger.LogError($"Kategori problem 1 in row : {row}, is undefined.");
                                                    return BadRequest($"Kategori problem 1 in row: {row}, is undefined.");
                                                }
                                                else
                                                {
                                                    visitingDetailReportPCObj.ProblemCategoryId = idTmp;
                                                    visitingDetailReportPCObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                                                    visitingDetailReportPCObj.VisitingDetailReportPCName = Kategori2Prb;
                                                    visitingDetailReportPCObj.CreatedAt = thisDate;
                                                    visitingDetailReportPCObj.CreatedBy = vdrObj.CreatedBy;
                                                    visitingDetailReportPCObj.UpdatedAt = thisDate;
                                                    visitingDetailReportPCObj.UpdatedBy = vdrObj.UpdatedBy;
                                                    var visitingDetailReportPCEntity = _mapper.Map<VisitingDetailReportProblemCategories>(visitingDetailReportPCObj);
                                                    _repository.VisitingDetailReportProblemCategory.CreateVisitingDetailReportProblemCategories(visitingDetailReportPCEntity);
                                                    _repository.Save();
                                                }
                                            }
                                            if(!string.IsNullOrEmpty(kategori3Prb))
                                            {
                                                VisitingDetailReportProblemCategoryForCreationDto visitingDetailReportPCObj = new VisitingDetailReportProblemCategoryForCreationDto();
                                                var idTmp = await _repository.ProblemCategory.GetProblemCategoryIdByProblemCategoryName(kategori3Prb);
                                                if (idTmp == null)
                                                {
                                                    _logger.LogError($"Kategori problem 1 in row : {row}, is undefined.");
                                                    return BadRequest($"Kategori problem 1 in row: {row}, is undefined.");
                                                }
                                                else
                                                {
                                                    visitingDetailReportPCObj.ProblemCategoryId = idTmp;
                                                    visitingDetailReportPCObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                                                    visitingDetailReportPCObj.VisitingDetailReportPCName = kategori3Prb;
                                                    visitingDetailReportPCObj.CreatedAt = thisDate;
                                                    visitingDetailReportPCObj.CreatedBy = vdrObj.CreatedBy;
                                                    visitingDetailReportPCObj.UpdatedAt = thisDate;
                                                    visitingDetailReportPCObj.UpdatedBy = vdrObj.UpdatedBy;
                                                    var visitingDetailReportPCEntity = _mapper.Map<VisitingDetailReportProblemCategories>(visitingDetailReportPCObj);
                                                    _repository.VisitingDetailReportProblemCategory.CreateVisitingDetailReportProblemCategories(visitingDetailReportPCEntity);
                                                    _repository.Save();
                                                }
                                            }
                                        }

                                        if (userRelated != null)
                                        {
                                            var userPIC = userRelated.Split(',').ToArray();
                                            if (userPIC.Count() > 0)
                                            {
                                                foreach (var sub in userPIC)
                                                {
                                                    VisitingDetailReportPicForCreationDto visitingDetailReportPICObj = new VisitingDetailReportPicForCreationDto();
                                                    visitingDetailReportPICObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                                                    var createdPCTmp = await _repository.User.GetUserIdByUsername(sub);
                                                    if (createdPCTmp == null)
                                                    {
                                                        _logger.LogError($"User Related in row : {row}, is undefined.");
                                                        return BadRequest($"User Related in row: {row}, is undefined.");
                                                    }
                                                    else
                                                    {
                                                        visitingDetailReportPICObj.EmployeeId = createdPCTmp;
                                                        visitingDetailReportPICObj.CreatedAt = thisDate;
                                                        visitingDetailReportPICObj.CreatedBy = vdrObj.CreatedBy;
                                                        visitingDetailReportPICObj.UpdatedAt = thisDate;
                                                        visitingDetailReportPICObj.UpdatedBy = vdrObj.UpdatedBy;
                                                        var visitingDetailReportPICEntity = _mapper.Map<VisitingDetailReportPICs>(visitingDetailReportPICObj);
                                                        _repository.VisitingDetailReportPIC.CreateVisitingDetailReportPIC(visitingDetailReportPICEntity);
                                                        _repository.Save();
                                                    }
                                                }
                                            }
                                        }

                                        CorrectiveActionForCreationDto correctiveActionObj = new CorrectiveActionForCreationDto();
                                        correctiveActionObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                                        correctiveActionObj.CorrectiveActionName = "";
                                        if (vdrObj.VisitingDetailReportFlagging != "temuan")
                                        {
                                            if (string.IsNullOrEmpty(kategori1Prb) || string.IsNullOrEmpty(Kategori2Prb) || string.IsNullOrEmpty(kategori3Prb))
                                            {
                                                if (!string.IsNullOrEmpty(kategori1Prb))
                                                {
                                                    correctiveActionObj.CorrectiveActionName += kategori1Prb + " ";
                                                }
                                                if (!string.IsNullOrEmpty(Kategori2Prb))
                                                {
                                                    correctiveActionObj.CorrectiveActionName += Kategori2Prb + " ";
                                                }
                                                if (!string.IsNullOrEmpty(kategori3Prb))
                                                {
                                                    correctiveActionObj.CorrectiveActionName += kategori3Prb + " ";
                                                }
                                            }
                                            else
                                            {
                                                correctiveActionObj.CorrectiveActionName = "temuan";
                                            }
                                        }
                                        else
                                        {
                                            correctiveActionObj.CorrectiveActionName = "temuan";
                                        }
                                        var progressByTmp = Guid.Empty;
                                        if (!string.IsNullOrEmpty(progressBy))
                                        {
                                            progressByTmp = await _repository.User.GetUserIdByUsername(progressBy);
                                        }
                                        if (progressByTmp == null)
                                        {
                                            _logger.LogError($"Progress By in row : {row}, is undefined.");
                                            return BadRequest($"Progress By in row: {row}, is undefined.");
                                        }
                                        else
                                        {
                                            if(progressByTmp == Guid.Empty)
                                            {
                                                correctiveActionObj.ProgressBy = "";
                                            }
                                            else
                                            {
                                                correctiveActionObj.ProgressBy = progressByTmp.ToString();
                                            }
                                            correctiveActionObj.ValidateBy = "";
                                            if(!string.IsNullOrEmpty(progress))
                                            {
                                                correctiveActionObj.CorrectiveActionDetail = progress;
                                            }
                                            else
                                            {
                                                correctiveActionObj.CorrectiveActionDetail = "";
                                            }
                                            correctiveActionObj.CreatedAt = thisDate;
                                            correctiveActionObj.CreatedBy = vdrObj.CreatedBy;
                                            correctiveActionObj.UpdatedAt = thisDate;
                                            correctiveActionObj.UpdatedBy = vdrObj.UpdatedBy;
                                            var correctiveActionEntity = _mapper.Map<CorrectiveActions>(correctiveActionObj);
                                            _repository.CorrectiveAction.CreateCorrectiveActions(correctiveActionEntity);
                                            _repository.Save();
                                        }
                                    }
                                }
                            }
                        }

                        return NoContent();
                    }
                    else
                    {
                        _logger.LogError("files form object sent from client is null.");
                        return BadRequest("files form object is null");
                    }
                }
                else
                {
                    _logger.LogError("Visiting detail report object sent from client is null.");
                    return BadRequest("Visiting detail report object is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside VisitingDetailReportImportExcel action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("export-excel-all-visiting-detail-reports")]
        public async Task<ActionResult> ExportExcelAllVisitingDetailReports([FromQuery] string status, string area, string query)
        {
            try
            {
                DateTime now = DateTime.Now;

                string excelName = "LaporanPICAContents-" + now + ".xlsx";

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("LaporanPICAContents");

                    var task = await _repository.VisitingDetailReport.GetAllVisitingDetailReportExportExcel(status, area, query);

                    workSheet.Row(1).Height = 20;
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(1).Style.Font.Bold = true;
                    workSheet.Cells[1, 1].Value = "No";
                    workSheet.Cells[1, 2].Value = "Lokasi Kunjungan";
                    workSheet.Cells[1, 3].Value = "Kota Kunjungan";
                    workSheet.Cells[1, 4].Value = "AHM Code";
                    workSheet.Cells[1, 5].Value = "MD Code";
                    workSheet.Cells[1, 6].Value = "Jenis Kunjungan";
                    workSheet.Cells[1, 7].Value = "Mulai Kunjungan";
                    workSheet.Cells[1, 8].Value = "Akhir Kunjungan";
                    workSheet.Cells[1, 9].Value = "PICA Status";
                    workSheet.Cells[1, 10].Value = "PICA Problem Identification";
                    workSheet.Cells[1, 11].Value = "CA Problem Identification";
                    workSheet.Cells[1, 12].Value = "Jenis PICA";
                    workSheet.Cells[1, 13].Value = "PICA Deadline";
                    workSheet.Cells[1, 14].Value = "PICA Tagged";
                    workSheet.Cells[1, 15].Value = "PICA Problem Categories";
                    workSheet.Cells[1, 16].Value = "PICA Attachments";

                    int row = 2;
                    foreach (var result in task.ToList())
                    {
                        workSheet.Cells[row, 1].Value = row - 1;
                        workSheet.Cells[row, 2].Value = result.DealerName;
                        workSheet.Cells[row, 3].Value = result.NetworkCity;
                        workSheet.Cells[row, 4].Value = result.AhmCode;
                        workSheet.Cells[row, 5].Value = result.MDCode;
                        workSheet.Cells[row, 6].Value = result.VisitingTypeName;
                        workSheet.Cells[row, 7].Value = result.VisitingStartDate;
                        workSheet.Cells[row, 8].Value = result.VisitingEndDate;
                        workSheet.Cells[row, 9].Value = result.VisitingDetailReportStatus;
                        workSheet.Cells[row, 10].Value = result.VisitingDetailReportProblemIdentification;
                        workSheet.Cells[row, 11].Value = result.CorrectiveActionProblemIdentification;
                        workSheet.Cells[row, 12].Value = result.VisitingDetailReportFlagging;
                        workSheet.Cells[row, 13].Value = result.VisitingDetailReportDeadline.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var picName = "";
                        foreach (var datum in result.VisitingDetailReportPIC)
                        {
                            picName += datum.EmployeeName + ", ";
                        }
                        if (picName.Trim() != "")
                        {
                            picName = picName.Trim().Substring(0, picName.Length - 2);
                        }
                        workSheet.Cells[row, 14].Value = picName;
                        var pcName = "";
                        foreach (var datum in result.VisitingDetailReportPC)
                        {
                            pcName += datum.VisitingDetailReportPCName + ", ";
                        }
                        if (pcName.Trim() != "")
                        {
                            pcName = pcName.Trim().Substring(0, pcName.Length - 2);
                        }
                        workSheet.Cells[row, 15].Value = pcName;
                        var urlName = "";
                        foreach (var datum in result.VisitingDetailReportAttachment)
                        {
                            var url = await _repository.VisitingDetailReport.GetAttachmentUrl(datum.AttachmentId);
                            urlName += url + ", ";
                        }
                        if (urlName.Trim() != "")
                        {
                            urlName = urlName.Trim().Substring(0, urlName.Length - 2);
                        }
                        workSheet.Cells[row, 16].Value = urlName;
                        row++;
                    }

                    //workSheet.Column(1).AutoFit();
                    //workSheet.Column(2).AutoFit();
                    //workSheet.Column(3).AutoFit();
                    //workSheet.Column(4).AutoFit();
                    //workSheet.Column(5).AutoFit();
                    //workSheet.Column(6).AutoFit();
                    //workSheet.Column(7).AutoFit();
                    //workSheet.Column(8).AutoFit();
                    package.Save();
                }

                stream.Position = 0;

                return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = excelName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside ExportExcelAllVisitingDetailReports action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("export-excel-visiting-detail-report-by-id")]
        public async Task<ActionResult> ExportExcelVisitingDetailReportById([FromQuery] Guid visitingDetailReportId)
        {
            try
            {
                DateTime now = DateTime.Now;

                string excelName = "LaporanPICAByIDContents-" + now + ".xlsx";

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("LaporanPICAByIDContents");

                    var task = await _repository.VisitingDetailReport.GetVisitingDetailReportByVisitingDetailReportId(visitingDetailReportId);

                    workSheet.Row(1).Height = 20;
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(1).Style.Font.Bold = true;
                    workSheet.Cells[1, 1].Value = "No";
                    workSheet.Cells[1, 2].Value = "Lokasi Kunjungan";
                    workSheet.Cells[1, 3].Value = "PICA Status";
                    workSheet.Cells[1, 4].Value = "PICA Problem Identification";
                    workSheet.Cells[1, 5].Value = "CA Problem Identification";
                    workSheet.Cells[1, 6].Value = "Jenis PICA";
                    workSheet.Cells[1, 7].Value = "PICA Deadline";
                    workSheet.Cells[1, 8].Value = "PICA Tagged";
                    workSheet.Cells[1, 9].Value = "PICA Problem Categories";
                    workSheet.Cells[1, 10].Value = "PICA Attachments";

                    int row = 2;

                    var networkDealer = await _repository.Network.GetNetworksById(task.Network.NetworkId);

                    workSheet.Cells[row, 1].Value = row - 1;
                    workSheet.Cells[row, 2].Value = networkDealer.DealerName;
                    workSheet.Cells[row, 3].Value = task.VisitingDetailReportStatus;
                    workSheet.Cells[row, 4].Value = task.VisitingDetailReportProblemIdentification;
                    workSheet.Cells[row, 5].Value = task.CorrectiveActionProblemIdentification;
                    workSheet.Cells[row, 6].Value = task.VisitingDetailReportFlagging;
                    workSheet.Cells[row, 7].Value = task.VisitingDetailReportDeadline.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var picName = "";
                    foreach (var datum in task.VisitingDetailReportPIC)
                    {
                        picName += datum.EmployeeName + ", ";
                    }
                    if (picName.Trim() != "")
                    {
                        picName = picName.Trim().Substring(0, picName.Length - 2);
                    }
                    workSheet.Cells[row, 8].Value = picName;
                    var pcName = "";
                    foreach (var datum in task.VisitingDetailReportPC)
                    {
                        pcName += datum.VisitingDetailReportPCName + ", ";
                    }
                    if (pcName.Trim() != "")
                    {
                        pcName = pcName.Trim().Substring(0, pcName.Length - 2);
                    }
                    workSheet.Cells[row, 9].Value = pcName;
                    var urlName = "";
                    foreach (var datum in task.VisitingDetailReportAttachment)
                    {
                        var url = await _repository.VisitingDetailReport.GetAttachmentUrl(datum.AttachmentId);
                        urlName += url + ", ";
                    }
                    if (urlName.Trim() != "")
                    {
                        urlName = urlName.Trim().Substring(0, urlName.Length - 2);
                    }
                    workSheet.Cells[row, 10].Value = urlName;

                    //workSheet.Column(1).AutoFit();
                    //workSheet.Column(2).AutoFit();
                    //workSheet.Column(3).AutoFit();
                    //workSheet.Column(4).AutoFit();
                    //workSheet.Column(5).AutoFit();
                    //workSheet.Column(6).AutoFit();
                    //workSheet.Column(7).AutoFit();
                    //workSheet.Column(8).AutoFit();
                    package.Save();
                }

                stream.Position = 0;

                return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = excelName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside ExportExcelVisitingDetailReportById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
