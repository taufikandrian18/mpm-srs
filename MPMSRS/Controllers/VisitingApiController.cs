using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MPMSRS.Controllers
{
    [Route("api/visitings")]
    [ApiController]
    public class VisitingApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private INotificationService _notification;

        public VisitingApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, INotificationService notification)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _notification = notification;
        }

        [Authorize]
        [HttpGet("get-all-visitings", Name = "GetVisitingsAll")]
        public async Task<IActionResult> GetAllVisitings([FromQuery] string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query, string createdName, string visitingPeopleName)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitings = await _repository.Visiting.GetAllVisitings(pageSize, pageIndex, status, startDate, endDate, area, query, createdName, visitingPeopleName);
                _logger.LogInfo($"Returned all visitings from database.");

                //var visitingsResult = _mapper.Map<IEnumerable<VisitingDto>>(visitings);

                return Ok(visitings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisitings action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-by-id/{id}", Name = "VisitingById")]
        public async Task<IActionResult> GetVisitingById([FromQuery] Guid visitingId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visiting = await _repository.Visiting.GetVisitingDetailById(visitingId);
                if (visiting == null)
                {
                    _logger.LogError($"Visiting with id: {visitingId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Visiting with id: {visitingId}");
                    //var VisitingResult = _mapper.Map<VisitingDto>(visiting);
                    return Ok(visiting);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-is-online/{id}", Name = "VisitingIsOnline")]
        public async Task<IActionResult> GetVisitingIsOnline([FromQuery] Guid visitingId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visiting = await _repository.Visiting.GetVisitingById(visitingId);
                if (visiting == null)
                {
                    _logger.LogError($"Visiting with id: {visitingId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Visiting with id: {visitingId}");
                    return Ok(visiting.IsOnline);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-status-by-people-login-id", Name = "GetVisitingStatusByPeopleLoginId")]
        public async Task<IActionResult> GetVisitingStatusByPeopleLoginId([FromQuery] string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitings = await _repository.Visiting.GetAllVisitingByPeopleLoginId(userId, status, pageSize, pageIndex, startDate, endDate, area, query);
                _logger.LogInfo($"Returned all visiting by login id from database.");

                //var userNetworksResult = _mapper.Map<IEnumerable<NetworkDto>>(userNetworks);

                return Ok(visitings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingStatusByPeopleLoginId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-status-by-create-login-id", Name = "GetVisitingStatusByCreateLoginId")]
        public async Task<IActionResult> GetVisitingStatusByCreateLoginId([FromQuery] string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitings = await _repository.Visiting.GetAllVisitingByCreateLoginId(userId, status, pageSize, pageIndex, startDate, endDate, area, query);
                _logger.LogInfo($"Returned all visiting by login id from database.");

                //var userNetworksResult = _mapper.Map<IEnumerable<NetworkDto>>(userNetworks);

                return Ok(visitings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingStatusByCreateLoginId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-visiting-status-by-create-division-id", Name = "GetVisitingStatusByCreateDivisionId")]
        public async Task<IActionResult> GetVisitingStatusByCreateDivisionId([FromQuery] string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitings = await _repository.Visiting.GetAllVisitingByManagerDivisionId(userId, status, pageSize, pageIndex, startDate, endDate, area, query);
                _logger.LogInfo($"Returned all visiting by create division id from database.");

                //var userNetworksResult = _mapper.Map<IEnumerable<NetworkDto>>(userNetworks);

                return Ok(visitings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisitingStatusByCreateDivisionId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-visiting-reports", Name = "GetAllVisitingReports")]
        public async Task<IActionResult> GetAllVisitingReports([FromQuery] string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var visitings = await _repository.Visiting.GetAllVisitingReports(status, pageSize, pageIndex, startDate, endDate, area, query);
                _logger.LogInfo($"Returned all visiting Reports from database.");

                //var userNetworksResult = _mapper.Map<IEnumerable<NetworkDto>>(userNetworks);

                return Ok(visitings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisitingReports action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-count-visiting-on-going", Name = "GetCountVisitingOnGoing")]
        public async Task<IActionResult> GetCountVisitingOnGoing()
        {
            try
            {
                var ThisDate = DateTime.Now.Date;
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var jmlVisiting = await _repository.Visiting.GetAllListCountVisitingOnGoingByDate(userId,ThisDate);
                _logger.LogInfo($"Returned count visiting on this date created by : {userId}");
                return Ok(jmlVisiting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCountVisitingOnGoing action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-visiting", Name = "InsertVisiting")]
        public async Task<IActionResult> CreateVisiting([FromBody] VisitingForCreationDto visiting)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visiting == null)
                {
                    _logger.LogError("Visiting object sent from client is null.");
                    return BadRequest("Visiting object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dateNow = DateTime.Now;

                var VisitingEntity = _mapper.Map<Visitings>(visiting);
                _repository.Visiting.CreateVisiting(VisitingEntity);
                _repository.Save();
                var createdVisiting = _mapper.Map<VisitingDto>(VisitingEntity);

                if (visiting.EmployeeId.Count() > 0)
                {
                    foreach (var sub in visiting.EmployeeId)
                    {
                        VisitingPeopleCreationDto visitingPeopleObj = new VisitingPeopleCreationDto();
                        visitingPeopleObj.VisitingId = createdVisiting.VisitingId;
                        visitingPeopleObj.EmployeeId = Guid.Parse(sub);
                        visitingPeopleObj.CreatedAt = dateNow;
                        visitingPeopleObj.CreatedBy = visiting.CreatedBy;
                        visitingPeopleObj.UpdatedAt = dateNow;
                        visitingPeopleObj.UpdatedBy = visiting.UpdatedBy;
                        var visitingPeopleEntity = _mapper.Map<VisitingPeoples>(visitingPeopleObj);
                        _repository.VisitingPeople.CreateVisitingPeople(visitingPeopleEntity);
                        _repository.Save();


                        //disini bikin push notif ke tiap orang yang di tandai oleh si created by
                        NotificationModel mod = new NotificationModel();
                        var deviceId = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(visitingPeopleObj.EmployeeId);
                        mod.DeviceId = deviceId.Token;
                        mod.Title = "Kunjungan Baru";
                        mod.Body = "Anda telah ditandai kunjungan oleh seseorang. ";
                        mod.ScreenID = createdVisiting.VisitingId.ToString();
                        mod.Screen = "Visiting";

                        await _notification.SendNotification(mod);

                        PushNotifications pnDto = new PushNotifications();
                        pnDto.PushNotificationId = Guid.NewGuid();
                        pnDto.EmployeeId = userId;
                        pnDto.RecipientId = Guid.Parse(sub);
                        pnDto.PushNotificationTitle = mod.Title;
                        pnDto.PushNotificationBody = mod.Body;
                        pnDto.ScreenID = createdVisiting.VisitingId.ToString();
                        pnDto.Screen = "Visiting";
                        pnDto.IsRead = false;
                        pnDto.CreatedAt = dateNow;
                        pnDto.CreatedBy = visiting.CreatedBy;
                        pnDto.UpdatedAt = dateNow;
                        pnDto.UpdatedBy = visiting.CreatedBy;

                        _repository.PushNotification.CreatePushNotification(pnDto);
                        _repository.Save();
                    }
                }

                return CreatedAtRoute("VisitingById", new { id = createdVisiting.VisitingId }, createdVisiting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateVisiting action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-visiting-bulk-approved-by-gm")]
        public async Task<IActionResult> UpdateVisitingBulkApprovedByGM([FromBody] VisitingForApprovedBulkDto visitingList)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visitingList == null)
                {
                    _logger.LogError("Visiting list object sent from client is null.");
                    return BadRequest("Visiting list object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting list object sent from client.");
                    return BadRequest("Invalid model object");
                }

                if(visitingList.ListVisitingUpdateBulk.Count() > 0)
                {
                    foreach(var datum in visitingList.ListVisitingUpdateBulk)
                    {
                        var VisitingEntity = await _repository.Visiting.GetVisitingById(datum.VisitingId);
                        if (VisitingEntity == null)
                        {
                            _logger.LogError($"Visiting with id: {datum.VisitingId}, hasn't been found in db.");
                            return NotFound();
                        }

                        VisitingEntity.VisitingStatus = datum.VisitingStatus;
                        VisitingEntity.ApprovedByGM = userId.ToString();

                        _repository.Visiting.UpdateVisiting(VisitingEntity);
                        _repository.Save();
                    }
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVisitingBulkApprovedByGM action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-visiting/{id}")]
        public async Task<IActionResult> UpdateVisiting([FromQuery] Guid id, [FromBody] VisitingForUpdateDto visiting)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visiting == null)
                {
                    _logger.LogError("Visiting object sent from client is null.");
                    return BadRequest("Visiting object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var VisitingEntity = await _repository.Visiting.GetVisitingById(id);
                if (VisitingEntity == null)
                {
                    _logger.LogError($"Visiting with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                var dateNow = DateTime.Now;

                _mapper.Map(visiting, VisitingEntity);
                _repository.Visiting.UpdateVisiting(VisitingEntity);
                _repository.Save();
                var updatedVisiting = _mapper.Map<VisitingDto>(VisitingEntity);

                if (visiting.EmployeeId.Count() > 0)
                {
                    var visitingPeoples = await _repository.VisitingPeople.GetVisitingPeopleByVisitingIdWithoutPage(id);
                    _repository.VisitingPeople.DeleteVisitingPeopleByVisitingId(visitingPeoples.ToList());
                    _repository.Save();
                    foreach (var sub in visiting.EmployeeId)
                    {
                        VisitingPeopleCreationDto visitingPeopleObj = new VisitingPeopleCreationDto();
                        visitingPeopleObj.VisitingId = updatedVisiting.VisitingId;
                        visitingPeopleObj.EmployeeId = Guid.Parse(sub);
                        visitingPeopleObj.CreatedAt = dateNow;
                        visitingPeopleObj.CreatedBy = visiting.CreatedBy;
                        visitingPeopleObj.UpdatedAt = dateNow;
                        visitingPeopleObj.UpdatedBy = visiting.UpdatedBy;
                        var visitingPeopleEntity = _mapper.Map<VisitingPeoples>(visitingPeopleObj);
                        _repository.VisitingPeople.CreateVisitingPeople(visitingPeopleEntity);
                        _repository.Save();


                        //disini bikin push notif

                        NotificationModel mod = new NotificationModel();
                        var deviceId = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(visitingPeopleObj.EmployeeId);
                        mod.DeviceId = deviceId.Token;
                        mod.Title = "Kunjungan Baru";
                        mod.Body = "Anda telah ditandai kunjungan oleh seseorang. ";
                        mod.ScreenID = updatedVisiting.VisitingId.ToString();
                        mod.Screen = "Visiting";

                        await _notification.SendNotification(mod);

                        PushNotifications pnDto = new PushNotifications();
                        pnDto.PushNotificationId = Guid.NewGuid();
                        pnDto.EmployeeId = userId;
                        pnDto.RecipientId = Guid.Parse(sub);
                        pnDto.PushNotificationTitle = mod.Title;
                        pnDto.PushNotificationBody = mod.Body;
                        pnDto.ScreenID = updatedVisiting.VisitingId.ToString();
                        pnDto.Screen = "Visiting";
                        pnDto.IsRead = false;
                        pnDto.CreatedAt = dateNow;
                        pnDto.CreatedBy = visiting.CreatedBy;
                        pnDto.UpdatedAt = dateNow;
                        pnDto.UpdatedBy = visiting.CreatedBy;

                        _repository.PushNotification.CreatePushNotification(pnDto);
                        _repository.Save();
                    }
                }

                if(visiting.VisitingStatus.Trim() == "waiting approval")
                {
                    var visitingDetailReports = await _repository.VisitingDetailReport.GetVisitingDetailReportByVisitingIdWithoutPage(id);
                    _repository.VisitingDetailReport.UpdateBulkVisitingDetailReport(visitingDetailReports.ToList());
                    _repository.Save();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVisiting action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-visiting-approval/{id}")]
        public async Task<IActionResult> UpdateVisitingApproval([FromQuery] Guid id, [FromBody] VisitingForUpdateApprovalDto visiting)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visiting == null)
                {
                    _logger.LogError("Visiting object sent from client is null.");
                    return BadRequest("Visiting object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var VisitingEntity = await _repository.Visiting.GetVisitingById(id);
                if (VisitingEntity == null)
                {
                    _logger.LogError($"Visiting with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                var dateNow = DateTime.Now;

                VisitingEntity.VisitingComment = visiting.VisitingComment;
                VisitingEntity.VisitingStatus = visiting.VisitingStatus;
                VisitingEntity.ApprovedByGM = visiting.ApprovedByGM;
                VisitingEntity.ApprovedByManager = visiting.ApprovedByManager;
                VisitingEntity.UpdatedAt = visiting.UpdatedAt;
                VisitingEntity.UpdatedBy = visiting.UpdatedBy;
                _repository.Visiting.UpdateVisiting(VisitingEntity);
                _repository.Save();

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVisitingApproval action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-visiting/{id}")]
        public async Task<IActionResult> DeleteVisiting([FromQuery] Guid id, [FromBody] VisitingForDeleteDto visiting)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (visiting == null)
                {
                    _logger.LogError("Visiting object sent from client is null.");
                    return BadRequest("Visiting object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visiting object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var visitingEntity = await _repository.Visiting.GetVisitingById(id);
                if (visitingEntity == null)
                {
                    _logger.LogError($"Visiting with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(visiting, visitingEntity);
                _repository.Visiting.DeleteVisiting(visitingEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVisiting action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("export-excel-visiting-reports")]
        public async Task<ActionResult> ExportExcelVisitingReports([FromQuery] string status, int pageSize, int pageIndex, string startDate, string endDate, string area, string query)
        {
            try
            {
                DateTime now = DateTime.Now;

                string excelName = "LaporanKunjunganContents-" + now + ".xlsx";

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("LaporanKunjunganContents");

                    var task = await _repository.Visiting.GetAllVisitingExportExcel(status, pageSize, pageIndex, startDate, endDate, area, query);

                    workSheet.Row(1).Height = 20;
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(1).Style.Font.Bold = true;
                    workSheet.Cells[1, 1].Value = "No";
                    workSheet.Cells[1, 2].Value = "Lokasi Kunjungan";
                    workSheet.Cells[1, 3].Value = "Nama Karyawan";
                    workSheet.Cells[1, 4].Value = "Mulai";
                    workSheet.Cells[1, 5].Value = "Selesai";
                    workSheet.Cells[1, 6].Value = "Durasi";
                    workSheet.Cells[1, 7].Value = "Catatan Kunjungan";
                    workSheet.Cells[1, 8].Value = "Status";

                    int row = 2;
                    foreach (var result in task.ToList())
                    {
                        workSheet.Cells[row, 1].Value = row - 1;
                        workSheet.Cells[row, 2].Value = result.DealerName;
                        workSheet.Cells[row, 3].Value = result.CreatedByName;
                        workSheet.Cells[row, 4].Value = result.StartDate.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        workSheet.Cells[row, 5].Value = result.EndDate.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        workSheet.Cells[row, 6].Value = (result.EndDate - result.StartDate).TotalDays.ToString();
                        var pcName = "";
                        foreach(var datum in result.VisitingDetailReport)
                        {
                            foreach(var nm in datum.VisitingDetailReportPC)
                            {
                                pcName += nm + ", ";
                            }
                            pcName = pcName.Trim().Substring(0, pcName.Length - 2);
                            pcName += " | ";
                        }
                        workSheet.Cells[row, 7].Value = pcName;
                        workSheet.Cells[row, 8].Value = result.VisitingStatus;
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
                _logger.LogError($"Something went wrong inside ExportExcelVisitingReports action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
