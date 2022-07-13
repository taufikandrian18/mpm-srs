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
using MPMSRS.Services.Repositories.Auth;

namespace MPMSRS.Controllers
{
    [Route("api/corrective-actions")]
    [ApiController]
    public class CorrectiveActionApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private DatabaseRefreshTokenRepository _services;
        private FileServices _fileService;
        private INotificationService _notification;

        public CorrectiveActionApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, DatabaseRefreshTokenRepository service, FileServices fs, INotificationService notification)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _services = service;
            _fileService = fs;
            _notification = notification;
        }

        [Authorize]
        [HttpGet("get-corrective-action-by-roles", Name = "GetCorrectiveActionByRoleAll")]
        public async Task<IActionResult> GetAllCorrectiveActionRole([FromQuery] Guid divisionId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
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
                if(companyId.Contains("MPM"))
                {
                    var caList = await _repository.CorrectiveAction.GetListCAByMainDealer(userId, divisionId, pageSize, pageIndex, status, startDate, endDate, area, query);

                    _logger.LogInfo($"Returned all corrective action from database.");

                    //var caResult = _mapper.Map<IEnumerable<CorrectiveActionDto>>(caList);

                    return Ok(caList);
                }
                else
                {
                    var caList = await _repository.CorrectiveAction.GetListCAByMDCode(companyId.Trim(), divisionId, pageSize, pageIndex, status, startDate, endDate, area, query);

                    _logger.LogInfo($"Returned all corrective action from database.");

                    //var caResult = _mapper.Map<IEnumerable<CorrectiveActionDto>>(caList);

                    return Ok(caList);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRoles action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-list-corrective-action-by-tagged-pic", Name = "GetAllCorrectiveActionByPICTagged")]
        public async Task<IActionResult> GetAllCorrectiveActionByPICTagged([FromQuery] Guid divisionId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var correctiveAction = await _repository.CorrectiveAction.GetListCAByPICTagged(userId, divisionId, pageSize, pageIndex, status, startDate, endDate, area, query);
                if (correctiveAction == null)
                {
                    _logger.LogError($"Corrective Action with user id: {userId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned corrective action with user id: {userId}");
                    //var correctiveActionResult = _mapper.Map<CorrectiveActionDto>(correctiveAction);
                    return Ok(correctiveAction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCorrectiveActionByPICTagged action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-list-user-of-corrective-actions", Name = "GetAllUserCorrectiveActions")]
        public async Task<IActionResult> GetAllUserCorrectiveActions([FromQuery] Guid divisionId, int pageSize, int pageIndex, string status, string startDate, string endDate, string area, string query, string sortByDeadline)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var correctiveAction = await _repository.CorrectiveAction.GetListAllUserCA(divisionId, pageSize, pageIndex, status, startDate, endDate, area, query, sortByDeadline);
                if (correctiveAction == null)
                {
                    _logger.LogError("Corrective Action, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo("Returned all users corrective actions");
                    //var correctiveActionResult = _mapper.Map<CorrectiveActionDto>(correctiveAction);
                    return Ok(correctiveAction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUserCorrectiveActions action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-list-corrective-action-by-network", Name = "GetAllCorrectiveActionByNetwork")]
        public async Task<IActionResult> GetAllCorrectiveActionByNetwork([FromQuery] Guid networkId, Guid divisionId, string status, int pageSize, int pageIndex, string sortBy, string sortByDeadline)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var correctiveAction = await _repository.CorrectiveAction.GetListCAByNetwork(networkId, divisionId, pageSize, pageIndex, status, sortBy, sortByDeadline);
                if (correctiveAction == null)
                {
                    _logger.LogError($"Corrective Action with Network id: {networkId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned corrective action with Network id: {networkId}");
                    //var correctiveActionResult = _mapper.Map<CorrectiveActionDto>(correctiveAction);
                    return Ok(correctiveAction);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCorrectiveActionByNetwork action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-corrective-action-by-id/{id}", Name = "CorrectiveActionById")]
        public async Task<IActionResult> GetCorrectiveActionById([FromQuery] Guid correctiveActionId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var correctiveAction = await _repository.CorrectiveAction.GetCorrectiveActionDetailById(correctiveActionId);
                if (correctiveAction == null)
                {
                    _logger.LogError($"Corrective Action with id: {correctiveActionId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned corrective action with id: {correctiveActionId}");
                    //var correctiveActionResult = _mapper.Map<CorrectiveActionDto>(correctiveAction);
                    return Ok(correctiveAction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRoleById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-count-corrective-action-on-progress", Name = "GetCountCorrectiveActionOnProgress")]
        public async Task<IActionResult> GetCountCorrectiveActionOnProgress()
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var jmlCA = await _repository.CorrectiveAction.GetCountCAOnProgressCreatedBy(userId);
                _logger.LogInfo($"Returned count corrective action created by : {userId}");
                return Ok(jmlCA);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCountCorrectiveActionOnProgress action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-percentage-corrective-action-done", Name = "GetPercentageCorrectiveActionDone")]
        public async Task<IActionResult> GetPercentageCorrectiveActionDone()
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var percentageCA = await _repository.CorrectiveAction.GetPercentageCA();
                _logger.LogInfo($"Returned percentage by int corrective action which is done");
                return Ok(percentageCA);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPercentageCorrectiveActionDone action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-corrective-action/{id}")]
        public async Task<IActionResult> UpdateCorrectiveAction([FromQuery] Guid id, [FromBody] CorrectiveActionForUpdateDto correctiveAction)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (correctiveAction == null)
                {
                    _logger.LogError("Corrective Action object sent from client is null.");
                    return BadRequest("Corrective Action object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Corrective Action object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var caEntity = await _repository.CorrectiveAction.GetCorrectiveActionById(id);
                if (caEntity == null)
                {
                    _logger.LogError($"Corrective Action with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var visitingDetailReportEntity = await _repository.VisitingDetailReport.GetVisitingDetailReportById(correctiveAction.VisitingDetailReportId);
                visitingDetailReportEntity.VisitingDetailReportStatus = correctiveAction.VisitingDetailReportStatus;
                _repository.VisitingDetailReport.UpdateVisitingDetailReport(visitingDetailReportEntity);

                if (correctiveAction.AttachmentList != null)
                {
                    if (correctiveAction.AttachmentList.Count() > 0)
                    {
                        var correctiveActionAttachment = await _repository.CorrectiveActionAttachment.GetCorrectiveActionAttachmentByCorrectiveActionIdWithoutPage(correctiveAction.CorrectiveActionId);
                        _repository.CorrectiveActionAttachment.DeleteCorrectiveActionAttachmentByCorrectiveActionId(correctiveActionAttachment.ToList());
                        _repository.Save();

                        foreach (var sub in correctiveAction.AttachmentList)
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

                            CorrectivActionAttachmentForCreationDto correctiveActionAttachmentObj = new CorrectivActionAttachmentForCreationDto();
                            correctiveActionAttachmentObj.CorrectiveActionId = correctiveAction.CorrectiveActionId;
                            correctiveActionAttachmentObj.AttachmentId = blobId;
                            correctiveActionAttachmentObj.CreatedAt = sub.CreatedAt;
                            correctiveActionAttachmentObj.CreatedBy = sub.CreatedBy;
                            correctiveActionAttachmentObj.UpdatedAt = sub.UpdatedAt;
                            correctiveActionAttachmentObj.UpdatedBy = sub.UpdatedBy;
                            var correctiveActionAttachmentEntity = _mapper.Map<CorrectiveActionAttachments>(correctiveActionAttachmentObj);
                            _repository.CorrectiveActionAttachment.CreateCorrectiveActionAttachment(correctiveActionAttachmentEntity);
                            _repository.Save();
                        }
                    }
                }

                caEntity.ValidateBy = correctiveAction.ValidateBy;
                caEntity.ProgressBy = correctiveAction.ProgressBy;
                caEntity.CorrectiveActionDetail = correctiveAction.CorrectiveActionDetail;
                //_mapper.Map(correctiveAction, caEntity);
                _repository.CorrectiveAction.UpdateCorrectiveActions(caEntity);
                _repository.Save();

                //var networkObj = await _repository.Network.GetNetworksById(visitingDetailReportEntity.NetworkId);
                //if (networkObj != null)
                //{
                //    var listEmployeeDealer = await _repository.User.GetUserByCompanyId(networkObj.MDCode.Trim());
                //    if (listEmployeeDealer.Count() > 0)
                //    {
                //        foreach (var datum in listEmployeeDealer)
                //        {
                //            //disini push notif dari pica created by ke list employee dealer bahwa ada pica/ca baru

                //            var createdByNamePICA = await _repository.User.GetUserById(Guid.Parse(visitingDetailReportEntity.CreatedBy));

                //            NotificationModel mod = new NotificationModel();
                //            var deviceId = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(datum.EmployeeId);
                //            mod.DeviceId = deviceId.Token;
                //            mod.Title = "Dealer mempunyai CA Aktif Baru";
                //            mod.Body = "dibuat oleh " + createdByNamePICA.DisplayName + ", " + caEntity.CorrectiveActionName + ", " + visitingDetailReportEntity.VisitingDetailReportProblemIdentification + ", " + caEntity.CorrectiveActionDetail + ", "+ visitingDetailReportEntity.VisitingDetailReportDeadline + "";

                //            await _notification.SendNotification(mod);
                //        }
                //    }
                //}

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCorrectiveAction action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
