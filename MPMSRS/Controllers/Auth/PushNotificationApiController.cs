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

namespace MPMSRS.Controllers.Auth
{
    [Route("api/push-notifications")]
    [ApiController]
    public class PushNotificationApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public PushNotificationApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("get-all-push-notifications", Name = "GetPushNotificationAll")]
        public async Task<IActionResult> GetAllPushNotifications([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var pushNotifications = await _repository.PushNotification.GetAllPushNotifications(pageSize, pageIndex);
                _logger.LogInfo($"Returned all push notifications from database.");

                var pushNotificationResult = _mapper.Map<IEnumerable<PushNotificationDto>>(pushNotifications);

                return Ok(pushNotificationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllPushNotifications action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-push-notification-by-sender-id", Name = "PushNotificationBySenderId")]
        public async Task<IActionResult> GetPushNotificationBySenderId([FromQuery] Guid senderId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var pushNotification = await _repository.PushNotification.GetPushNotificationBySenderId(senderId);
                if (pushNotification == null)
                {
                    _logger.LogError($"Push notification with sender id: {senderId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned push notification with sender id: {senderId}");
                    var PushNotificationResult = _mapper.Map<PushNotificationDto>(pushNotification);
                    return Ok(PushNotificationResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPushNotificationBySenderId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-push-notification-by-recipient-id", Name = "PushNotificationByRecipientId")]
        public async Task<IActionResult> GetPushNotificationByRecipientId([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var pushNotification = await _repository.PushNotification.GetAllPushNotificationByRecipientId(userId,pageSize,pageIndex);
                if (pushNotification == null)
                {
                    _logger.LogError($"Push notification with recipient id: {userId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned push notification with recipient id: {userId}");
                    var PushNotificationResult = _mapper.Map<IEnumerable<PushNotificationDto>>(pushNotification);
                    return Ok(PushNotificationResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPushNotificationByRecipientId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-count-push-notification-by-recipient-id", Name = "PushNotificationCountByRecipientId")]
        public async Task<IActionResult> GetCountPushNotificationByRecipientId([FromQuery] bool isRead)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var pushNotification = await _repository.PushNotification.GetPushNotificationByRecipientId(userId, isRead);
                if (pushNotification == null)
                {
                    _logger.LogError($"Push notification with recipient id: {userId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned push notification with recipient id: {userId}");
                    var PushNotificationResult = _mapper.Map<IEnumerable<PushNotificationDto>>(pushNotification);
                    return Ok(PushNotificationResult.Count());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPushNotificationByRecipientId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-push-notification", Name = "InsertPushNotification")]
        public IActionResult CreatePushNotification([FromBody] PushNotificationForCreationDto pushNotification)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (pushNotification == null)
                {
                    _logger.LogError("Push Notification object sent from client is null.");
                    return BadRequest("Push Notification object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Push Notification object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var PushNotificationEntity = _mapper.Map<PushNotifications>(pushNotification);
                _repository.PushNotification.CreatePushNotification(PushNotificationEntity);
                _repository.Save();
                var createdPushNotification = _mapper.Map<PushNotificationDto>(PushNotificationEntity);
                return CreatedAtRoute("PushNotificationById", new { id = createdPushNotification.PushNotificationId }, createdPushNotification);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreatePushNotification action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-push-notification/{id}")]
        public async Task<IActionResult> UpdatePushNotification([FromQuery] Guid id, [FromBody] PushNotificationForUpdateDto pushNotification)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (pushNotification == null)
                {
                    _logger.LogError("Push Notification object sent from client is null.");
                    return BadRequest("Push Notification object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Push Notification object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var PushNotificationEntity = await _repository.PushNotification.GetPushNotificationById(id);
                if (PushNotificationEntity == null)
                {
                    _logger.LogError($"Push Notification with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(pushNotification, PushNotificationEntity);
                _repository.PushNotification.UpdatePushNotification(PushNotificationEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePushNotification action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-push-notification/{id}")]
        public async Task<IActionResult> DeletePushNotification([FromQuery] Guid id, [FromBody] PushNotificationForDeletionDto pushNotification)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (pushNotification == null)
                {
                    _logger.LogError("Push Notification object sent from client is null.");
                    return BadRequest("Push Notification object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Push Notification object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var pushNotificationEntity = await _repository.PushNotification.GetPushNotificationById(id);
                if (pushNotificationEntity == null)
                {
                    _logger.LogError($"Push Notification with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(pushNotification, pushNotificationEntity);
                _repository.PushNotification.DeletePushNotification(pushNotificationEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePushNotification action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
