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
    [Route("api/user-fcm-tokens")]
    [ApiController]
    public class UserFcmTokenApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public UserFcmTokenApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("get-all-user-fcm-tokens", Name = "GetUserFcmTokenAll")]
        public async Task<IActionResult> GetAllUserFcmTokens([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var userFcmTokens = await _repository.UserFcmToken.GetAllUserFcmTokens(pageSize, pageIndex);
                _logger.LogInfo($"Returned all user fcm token from database.");

                var userFcmTokensResult = _mapper.Map<IEnumerable<UserFcmTokenDto>>(userFcmTokens);

                return Ok(userFcmTokensResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUserFcmTokens action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-user-fcm-token-by-employee-id", Name = "UserFcmTokenByEmployeeId")]
        public async Task<IActionResult> GetUserFcmTokenByEmployeeId([FromQuery] Guid employeeId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var userFcmToken = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(employeeId);
                if (userFcmToken == null)
                {
                    _logger.LogError($"User fcm token with employee id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user fcm token with employee id: {employeeId}");
                    //var userResult = _mapper.Map<UserDto>(user);
                    return Ok(userFcmToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserFcmTokenByEmployeeId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-user-fcm-token", Name = "InsertUserFcmToken")]
        public IActionResult CreateUserFcmToken([FromBody] UserFcmTokenForCreationDto userFcmToken)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (userFcmToken == null)
                {
                    _logger.LogError("User Fcm Token object sent from client is null.");
                    return BadRequest("User Fcm Token object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user Fcm Token object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var userFcmTokenEntity = _mapper.Map<UserFcmTokens>(userFcmToken);
                _repository.UserFcmToken.CreateUserFcmToken(userFcmTokenEntity);
                _repository.Save();
                var createdUserFcmToken = _mapper.Map<UserFcmTokenDto>(userFcmTokenEntity);
                return CreatedAtRoute("UserFcmTokenById", new { id = createdUserFcmToken.UserFcmTokenId }, createdUserFcmToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUserFcmToken action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-user-fcm-token")]
        public async Task<IActionResult> UpdateUserFcmToken([FromBody] UserFcmTokenForUpdateDto userFcmToken)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (userFcmToken == null)
                {
                    _logger.LogError("User Fcm Token object sent from client is null.");
                    return BadRequest("User Fcm Token object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user fcm token object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var userFcmTokenEntity = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(userFcmToken.EmployeeId);
                if (userFcmTokenEntity == null)
                {
                    _logger.LogError($"User Fcm Token with employee id: {userFcmTokenEntity.EmployeeId}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(userFcmToken, userFcmTokenEntity);
                _repository.UserFcmToken.UpdateUserFcmToken(userFcmTokenEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUserFcmToken action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-user-fcm-token")]
        public async Task<IActionResult> DeleteUserFcmToken([FromQuery] Guid employeeId, [FromBody] UserFcmTokenForDeletionDto userFcmToken)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (userFcmToken == null)
                {
                    _logger.LogError("User Fcm Token object sent from client is null.");
                    return BadRequest("User Fcm Token object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user fcm token object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var userFcmTokenEntity = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(employeeId);
                if (userFcmTokenEntity == null)
                {
                    _logger.LogError($"User Fcm Token with employee id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(userFcmToken, userFcmTokenEntity);
                _repository.UserFcmToken.DeleteUserFcmToken(userFcmTokenEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUserFcmToken action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
