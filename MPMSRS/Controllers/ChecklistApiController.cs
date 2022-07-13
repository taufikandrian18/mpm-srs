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
    [Route("api/checklists")]
    [ApiController]
    public class ChecklistApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private FileServices _fileService;
        private INotificationService _notification;

        public ChecklistApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, FileServices fs, INotificationService notification)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _fileService = fs;
            _notification = notification;
        }

        [Authorize]
        [HttpGet("get-all-checklists", Name = "GetAllChecklistss")]
        public async Task<IActionResult> GetAllChecklists([FromQuery] Guid visitingId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var checklists = await _repository.Checklist.GetAllChecklists(visitingId, pageSize, pageIndex);
                _logger.LogInfo($"Returned all checklists from database.");

                //var visitingsResult = _mapper.Map<IEnumerable<VisitingDto>>(visitings);

                return Ok(checklists);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllChecklists action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-checklist-list", Name = "GetAllChecklistList")]
        public async Task<IActionResult> GetAllChecklistList([FromQuery] Guid visitingId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var checklists = await _repository.Checklist.GetChecklistIdentification(visitingId, pageSize, pageIndex);
                _logger.LogInfo($"Returned all checklist list from database.");

                //var visitingsResult = _mapper.Map<IEnumerable<VisitingDto>>(visitings);

                return Ok(checklists);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllChecklistList action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-checklist-by-id", Name = "GetChecklistId")]
        public async Task<IActionResult> GetChecklistId([FromQuery] Guid checklistId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var checklist = await _repository.Checklist.GetChecklistByChecklistId(checklistId);
                if (checklist == null)
                {
                    _logger.LogError($"Checklist with id: {checklistId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Checklist with id: {checklistId}");
                    //var VisitingResult = _mapper.Map<VisitingDto>(visiting);
                    return Ok(checklist);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetChecklistId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-checklist", Name = "InsertChecklist")]
        public async Task<IActionResult> CreateChecklist([FromBody] ChecklistListForCreationDto checklistDto)
        {
            try
            {
                var thisDate = DateTime.Now.Date;
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (checklistDto == null)
                {
                    _logger.LogError("Checklist object sent from client is null.");
                    return BadRequest("Checklist object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Checklist object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var urlMst = "";
                var mimeMst = "";
                var blobIdMst = Guid.Empty;

                if (!string.IsNullOrEmpty(checklistDto.ImageUrl))
                {
                    urlMst = checklistDto.ImageUrl;
                    mimeMst = System.IO.Path.GetExtension(urlMst);
                    blobIdMst = await _fileService.InsertBlob(urlMst, mimeMst);
                }

                ChecklistForCreationDto vdrObj = new ChecklistForCreationDto();
                vdrObj.VisitingId = checklistDto.VisitingId;
                vdrObj.NetworkId = checklistDto.NetworkId;
                vdrObj.AttachmentId = blobIdMst;
                vdrObj.ChecklistItem = checklistDto.ChecklistItem;
                vdrObj.ChecklistIdentification = checklistDto.ChecklistIdentification;
                vdrObj.ChecklistActualCondition = checklistDto.ChecklistActualCondition;
                vdrObj.ChecklistActualDetail = checklistDto.ChecklistActualDetail;
                vdrObj.ChecklistFix = checklistDto.ChecklistFix;
                vdrObj.ChecklistStatus = checklistDto.ChecklistStatus;
                vdrObj.ChecklistDeadline = checklistDto.ChecklistDeadline;
                vdrObj.CreatedAt = checklistDto.CreatedAt;
                vdrObj.CreatedBy = checklistDto.CreatedBy;
                vdrObj.UpdatedAt = checklistDto.UpdatedAt;
                vdrObj.UpdatedBy = checklistDto.UpdatedBy;

                var ChecklistEntity = _mapper.Map<Checklists>(vdrObj);
                _repository.Checklist.Create(ChecklistEntity);
                _repository.Save();
                var createdChecklist = _mapper.Map<ChecklistDto>(ChecklistEntity);

                if (checklistDto.PICList != null)
                {
                    if (checklistDto.PICList.Count() > 0)
                    {
                        foreach (var sub in checklistDto.PICList)
                        {
                            ChecklistPicForCreationDto checklistPICObj = new ChecklistPicForCreationDto();
                            checklistPICObj.ChecklistId = createdChecklist.ChecklistId;
                            checklistPICObj.EmployeeId = sub.EmployeeId;
                            checklistPICObj.CreatedAt = sub.CreatedAt;
                            checklistPICObj.CreatedBy = sub.CreatedBy;
                            checklistPICObj.UpdatedAt = sub.UpdatedAt;
                            checklistPICObj.UpdatedBy = sub.UpdatedBy;
                            var checklistPICEntity = _mapper.Map<ChecklistPICs>(checklistPICObj);
                            _repository.ChecklistPIC.CreateChecklistPIC(checklistPICEntity);
                            _repository.Save();
                        }
                    }
                }

                if (checklistDto.EvidenceList != null)
                {
                    if (checklistDto.EvidenceList.Count() > 0)
                    {
                        foreach (var sub in checklistDto.EvidenceList)
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

                            ChecklistEvidenceForCreationDto checklistEvidenceObj = new ChecklistEvidenceForCreationDto();
                            checklistEvidenceObj.ChecklistId = createdChecklist.ChecklistId;
                            checklistEvidenceObj.AttachmentId = blobId;
                            checklistEvidenceObj.CreatedAt = sub.CreatedAt;
                            checklistEvidenceObj.CreatedBy = sub.CreatedBy;
                            checklistEvidenceObj.UpdatedAt = sub.UpdatedAt;
                            checklistEvidenceObj.UpdatedBy = sub.UpdatedBy;
                            var checklistEvidenceEntity = _mapper.Map<ChecklistEvidences>(checklistEvidenceObj);
                            _repository.ChecklistEvidence.CreateChecklistEvidence(checklistEvidenceEntity);
                            _repository.Save();
                        }
                    }
                }

                if (vdrObj.ChecklistStatus.ToLower().Trim() == "exist not good" || vdrObj.ChecklistStatus.ToLower().Trim() == "not exist")
                {
                    var userCreated = await _repository.User.GetUserById(Guid.Parse(checklistDto.CreatedBy));
                    VisitingDetailReportForCreationDto chkObj = new VisitingDetailReportForCreationDto();
                    chkObj.VisitingId = checklistDto.VisitingId;
                    chkObj.NetworkId = checklistDto.NetworkId;
                    chkObj.DivisionId = userCreated.DivisionId.HasValue ? userCreated.DivisionId.Value : Guid.Empty;
                    chkObj.VisitingDetailReportProblemIdentification = checklistDto.ChecklistItem;
                    chkObj.CorrectiveActionProblemIdentification = checklistDto.ChecklistFix; 
                    chkObj.VisitingDetailReportStatus = checklistDto.ChecklistStatus;
                    chkObj.VisitingDetailReportDeadline = checklistDto.ChecklistDeadline.HasValue ? checklistDto.ChecklistDeadline.Value : DateTime.Now.Date;
                    chkObj.VisitingDetailReportFlagging = "checklist";
                    chkObj.CreatedAt = checklistDto.CreatedAt;
                    chkObj.CreatedBy = checklistDto.CreatedBy;
                    chkObj.UpdatedAt = checklistDto.UpdatedAt;
                    chkObj.UpdatedBy = checklistDto.UpdatedBy;

                    var VisitingDetailReportEntity = _mapper.Map<VisitingDetailReports>(vdrObj);
                    _repository.VisitingDetailReport.CreateVisitingDetailReport(VisitingDetailReportEntity);
                    _repository.Save();
                    var createdVisitingDetailReport = _mapper.Map<VisitingDetailReportDtoModel>(VisitingDetailReportEntity);

                    List<string> tempNamaPC = new List<string>();

                    List<ProblemCategoryDto> listPCDto = new List<ProblemCategoryDto>();

                    List<string> tempNameChecklistPC = new List<string>();
                    tempNameChecklistPC.Add(checklistDto.ChecklistIdentification);
                    tempNameChecklistPC.Add(checklistDto.ChecklistActualCondition);
                    tempNameChecklistPC.Add(checklistDto.ChecklistActualDetail);

                    var indexPC = 0;
                    if (checklistDto.ChecklistIdentification != "" && checklistDto.ChecklistActualCondition != "" && checklistDto.ChecklistActualDetail != "")
                    {
                        foreach (var sub in tempNameChecklistPC)
                        {
                            var checkPC = await _repository.ProblemCategory.GetProblemCategoryIdByProblemCategoryName(sub.Trim());
                            if (checkPC == null || checkPC == Guid.Empty)
                            {
                                ProblemCategoryCreationForDto newPC = new ProblemCategoryCreationForDto();
                                newPC.ProblemCategoryName = sub;
                                newPC.ParentId = null;
                                newPC.ChildId = null;
                                newPC.CreatedAt = thisDate;
                                newPC.CreatedAt = checklistDto.CreatedAt;
                                newPC.CreatedBy = checklistDto.CreatedBy;
                                newPC.UpdatedAt = checklistDto.UpdatedAt;
                                newPC.UpdatedBy = checklistDto.UpdatedBy;

                                var pcEntity = _mapper.Map<ProblemCategories>(newPC);
                                _repository.ProblemCategory.CreateProblemCategory(pcEntity);
                                _repository.Save();
                                var createdProblemCategory = _mapper.Map<ProblemCategoryDto>(pcEntity);
                                listPCDto.Add(createdProblemCategory);
                            }
                            else
                            {
                                ProblemCategoryCreationForDto newPC = new ProblemCategoryCreationForDto();
                                newPC.ProblemCategoryName = sub;
                                newPC.ParentId = await _repository.ProblemCategory.GetProblemCategoryIdByProblemCategoryName(tempNameChecklistPC[indexPC - 1]);
                                newPC.ChildId = null;
                                newPC.CreatedAt = thisDate;
                                newPC.CreatedAt = checklistDto.CreatedAt;
                                newPC.CreatedBy = checklistDto.CreatedBy;
                                newPC.UpdatedAt = checklistDto.UpdatedAt;
                                newPC.UpdatedBy = checklistDto.UpdatedBy;

                                var pcEntity = _mapper.Map<ProblemCategories>(newPC);
                                _repository.ProblemCategory.CreateProblemCategory(pcEntity);
                                _repository.Save();
                                var createdProblemCategory = _mapper.Map<ProblemCategoryDto>(pcEntity);
                                listPCDto.Add(createdProblemCategory);
                            }
                            indexPC++;
                        }
                    }
                    if (listPCDto != null)
                    {
                        if (listPCDto.Count() > 0)
                        {
                            foreach (var sub in listPCDto)
                            {
                                VisitingDetailReportProblemCategoryForCreationDto visitingDetailReportPCObj = new VisitingDetailReportProblemCategoryForCreationDto();
                                visitingDetailReportPCObj.ProblemCategoryId = sub.ProblemCategoryId;
                                visitingDetailReportPCObj.VisitingDetailReportId = createdVisitingDetailReport.VisitingDetailReportId;
                                visitingDetailReportPCObj.VisitingDetailReportPCName = sub.ProblemCategoryName;
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

                    if (checklistDto.PICList != null)
                    {
                        if (checklistDto.PICList.Count() > 0)
                        {
                            foreach (var sub in checklistDto.PICList)
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
                                    mod.Body = "ditandai oleh " + createdByName.DisplayName + ", " + namaPC + ", " + checklistDto.ChecklistFix + ", " + vdrObj.ChecklistIdentification + ", " + vdrObj.ChecklistDeadline + "";

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

                    if (checklistDto.EvidenceList != null)
                    {
                        if (checklistDto.EvidenceList.Count() > 0)
                        {
                            foreach (var sub in checklistDto.EvidenceList)
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
                    if (listPCDto != null)
                    {
                        correctiveActionObj.CorrectiveActionName = listPCDto[0].ProblemCategoryName;
                        if (listPCDto.Count() == 2)
                        {
                            correctiveActionObj.CorrectiveActionName = listPCDto[0].ProblemCategoryName + " - " + listPCDto[1].ProblemCategoryName;
                        }
                        if (listPCDto.Count() == 3)
                        {
                            correctiveActionObj.CorrectiveActionName = listPCDto[0].ProblemCategoryName + " - " + listPCDto[1].ProblemCategoryName + " - " + listPCDto[2].ProblemCategoryName;
                        }
                    }
                    else
                    {
                        correctiveActionObj.CorrectiveActionName = "checklist";
                    }
                    correctiveActionObj.ProgressBy = "";
                    correctiveActionObj.ValidateBy = "";
                    correctiveActionObj.CorrectiveActionDetail = "";
                    correctiveActionObj.CreatedAt = checklistDto.CreatedAt;
                    correctiveActionObj.CreatedBy = checklistDto.CreatedBy;
                    correctiveActionObj.UpdatedAt = checklistDto.UpdatedAt;
                    correctiveActionObj.UpdatedBy = checklistDto.UpdatedBy;
                    var correctiveActionEntity = _mapper.Map<CorrectiveActions>(correctiveActionObj);
                    _repository.CorrectiveAction.CreateCorrectiveActions(correctiveActionEntity);
                    _repository.Save();
                }

                return CreatedAtRoute("InsertChecklist", new { id = createdChecklist.ChecklistId }, createdChecklist);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateChecklist action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-checklist", Name = "UpdateChecklist")]
        public async Task<IActionResult> UpdateChecklist([FromBody] ChecklistListForUpdateDto checklistDto)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (checklistDto == null)
                {
                    _logger.LogError("Checklist object sent from client is null.");
                    return BadRequest("Checklist object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Checklist object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var urlMst = "";
                var mimeMst = "";
                var blobIdMst = Guid.Empty;

                if (!string.IsNullOrEmpty(checklistDto.ImageUrl))
                {
                    urlMst = checklistDto.ImageUrl;
                    mimeMst = System.IO.Path.GetExtension(urlMst);
                    blobIdMst = await _fileService.InsertBlob(urlMst, mimeMst);
                }

                ChecklistForUpdateDto vdrObj = new ChecklistForUpdateDto();
                vdrObj.ChecklistId = checklistDto.ChecklistId;
                vdrObj.NetworkId = checklistDto.NetworkId;
                vdrObj.AttachmentId = blobIdMst;
                vdrObj.ChecklistItem = checklistDto.ChecklistItem;
                vdrObj.ChecklistIdentification = checklistDto.ChecklistIdentification;
                vdrObj.ChecklistActualCondition = checklistDto.ChecklistActualCondition;
                vdrObj.ChecklistActualDetail = checklistDto.ChecklistActualDetail;
                vdrObj.ChecklistFix = checklistDto.ChecklistFix;
                vdrObj.ChecklistStatus = checklistDto.ChecklistStatus;
                vdrObj.ChecklistDeadline = checklistDto.ChecklistDeadline;
                vdrObj.CreatedAt = checklistDto.CreatedAt;
                vdrObj.CreatedBy = checklistDto.CreatedBy;
                vdrObj.UpdatedAt = checklistDto.UpdatedAt;
                vdrObj.UpdatedBy = checklistDto.UpdatedBy;

                var ChecklistEntity = _mapper.Map<Checklists>(vdrObj);
                _repository.Checklist.UpdateChecklist(ChecklistEntity);
                _repository.Save();
                var updatedChecklist = _mapper.Map<ChecklistDto>(ChecklistEntity);

                var checklistPIC = await _repository.ChecklistPIC.GetChecklistPICByChecklistIdWithoutPage(checklistDto.ChecklistId);
                if (checklistPIC != null)
                {
                    if (checklistPIC.Count() > 0)
                    {
                        _repository.ChecklistPIC.DeleteChecklistPICByChecklistId(checklistPIC.ToList());
                        _repository.Save();
                    }
                }

                if (checklistDto.PICList != null)
                {
                    if (checklistDto.PICList.Count() > 0)
                    {
                        foreach (var sub in checklistDto.PICList)
                        {
                            ChecklistPicForCreationDto checklistPICObj = new ChecklistPicForCreationDto();
                            checklistPICObj.ChecklistId = updatedChecklist.ChecklistId;
                            checklistPICObj.EmployeeId = sub.EmployeeId;
                            checklistPICObj.CreatedAt = sub.CreatedAt;
                            checklistPICObj.CreatedBy = sub.CreatedBy;
                            checklistPICObj.UpdatedAt = sub.UpdatedAt;
                            checklistPICObj.UpdatedBy = sub.UpdatedBy;
                            var checklistPICEntity = _mapper.Map<ChecklistPICs>(checklistPICObj);
                            _repository.ChecklistPIC.CreateChecklistPIC(checklistPICEntity);
                            _repository.Save();
                        }
                    }
                }

                if (checklistDto.EvidenceList != null)
                {
                    if (checklistDto.EvidenceList.Count() > 0)
                    {
                        var checklistAttachment = await _repository.ChecklistEvidence.GetChecklistEvidenceByChecklistIdWithoutPage(checklistDto.ChecklistId);
                        _repository.ChecklistEvidence.DeleteChecklistEvidenceByChecklistId(checklistAttachment.ToList());
                        _repository.Save();

                        foreach (var sub in checklistDto.EvidenceList)
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

                            ChecklistEvidenceForCreationDto checklistEvidenceObj = new ChecklistEvidenceForCreationDto();
                            checklistEvidenceObj.ChecklistId = updatedChecklist.ChecklistId;
                            checklistEvidenceObj.AttachmentId = blobId;
                            checklistEvidenceObj.CreatedAt = sub.CreatedAt;
                            checklistEvidenceObj.CreatedBy = sub.CreatedBy;
                            checklistEvidenceObj.UpdatedAt = sub.UpdatedAt;
                            checklistEvidenceObj.UpdatedBy = sub.UpdatedBy;
                            var checklistEvidenceEntity = _mapper.Map<ChecklistEvidences>(checklistEvidenceObj);
                            _repository.ChecklistEvidence.CreateChecklistEvidence(checklistEvidenceEntity);
                            _repository.Save();
                        }
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateChecklist action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-checklist-deadline/{id}")]
        public async Task<IActionResult> UpdateChecklistDeadline([FromQuery] Guid id, [FromBody] ChecklistDeadlineUpdateDto checklist)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (checklist == null)
                {
                    _logger.LogError("Checklist object sent from client is null.");
                    return BadRequest("Checklist object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Checklist object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var checklistEntity = await _repository.Checklist.GetChecklistById(id);
                if (checklistEntity == null)
                {
                    _logger.LogError($"Checklist with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(checklist, checklistEntity);
                _repository.Checklist.UpdateChecklist(checklistEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateChecklistDeadline action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-checklist/{id}")]
        public async Task<IActionResult> DeleteChecklist([FromQuery] Guid id, [FromBody] ChecklistForDeleteDto checklist)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (checklist == null)
                {
                    _logger.LogError("Checklist object sent from client is null.");
                    return BadRequest("Checklist object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Checklist object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var checklistEntity = await _repository.Checklist.GetChecklistById(id);
                if (checklistEntity == null)
                {
                    _logger.LogError($"Checklist with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(checklist, checklistEntity);
                _repository.Checklist.DeleteChecklist(checklistEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteChecklist action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
