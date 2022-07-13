using System;
using System.Collections.Generic;
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
using MPMSRS.Services.Interfaces.Auth;
using MPMSRS.Services.Repositories.Auth;

namespace MPMSRS.Controllers.Auth
{
    [Route("api/users")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private DatabaseRefreshTokenRepository _services;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private FileServices _fileService;

        public UserApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, DatabaseRefreshTokenRepository service, IRefreshTokenRepository refreshTokenRepository, FileServices fs)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _services = service;
            _refreshTokenRepository = refreshTokenRepository;
            _fileService = fs;
        }

        [Authorize]
        [HttpGet("get-all-users", Name = "GetUserAll")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var users = await _repository.User.GetAllUsers(pageSize,pageIndex);
                _logger.LogInfo($"Returned all users from database.");

                var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);

                return Ok(usersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-user-by-id", Name = "UserById")]
        public async Task<IActionResult> GetUserById([FromQuery] Guid employeeId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var user = await _repository.User.GetUserByIdWithFullField(employeeId);
                if (user == null)
                {
                    _logger.LogError($"User with id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with id: {employeeId}");
                    //var userResult = _mapper.Map<UserDto>(user);
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-user-by-id-detail", Name = "UserByIdDetail")]
        public async Task<IActionResult> GetUserByIdDetail([FromQuery] Guid employeeId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var user = await _repository.User.GetUserByIdDetail(employeeId);
                if (user == null)
                {
                    _logger.LogError($"User with id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with id: {employeeId}");
                    //var userResult = _mapper.Map<UserDto>(user);
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserByIdDetail action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-user-by-role/{roleId}", Name = "UserByRoleId")]
        public async Task<IActionResult> GetUserByRoleId([FromQuery] Guid roleId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var user = await _repository.User.GetUserByRole(roleId, pageSize, pageIndex);
                if (user == null)
                {
                    _logger.LogError($"User with role id: {roleId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with role id: {roleId}");
                    var userResult = _mapper.Map<IEnumerable<UserDto>>(user);
                    return Ok(userResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserByRoleId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-user-by-role-id-and-division-id", Name = "UserByRoleIdAndDivisionId")]
        public async Task<IActionResult> GetUserByRoleIdAndDivisionId([FromQuery] Guid roleId, Guid divisionId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var user = await _repository.User.GetUserByRoleIdAndDivisionId(roleId, divisionId, pageSize, pageIndex);
                if (user == null)
                {
                    _logger.LogError($"User with role id: {roleId} and division id: {divisionId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with role id: {roleId} and division id: {divisionId}");
                    var userResult = _mapper.Map<IEnumerable<UserDto>>(user);
                    return Ok(userResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserByRoleIdAndDivisionId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-user", Name = "InsertUser")]
        public IActionResult CreateUser([FromBody] UserForCreationDto user)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var userEntity = _mapper.Map<Users>(user);
                _repository.User.CreateUser(userEntity);
                _repository.Save();
                var createdUser = _mapper.Map<UserDto>(userEntity);
                return CreatedAtRoute("UserById", new { id = createdUser.EmployeeId }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-users/refresh-tokens", Name = "GetUserRefreshTokens")]
        public async Task<IActionResult> GetUserRefreshTokens()
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }
                var users = await _services.GetByEmployeeId(userId);
                _logger.LogInfo($"Returned Token By EmployeeId from database.");

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-user-profile-picture")]
        public async Task<IActionResult> UpdateUserProfilePicture([FromQuery] UserProdilePictureForUpdateDto user, IFormFile files)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var userEntity = await _repository.User.GetUserById(user.EmployeeId);
                if (userEntity == null)
                {
                    _logger.LogError($"User with id: {user.EmployeeId}, hasn't been found in db.");
                    return NotFound();
                }

                var url = await _fileService.uploadFileUrl(files);
                var mime = System.IO.Path.GetExtension(url);
                var blobId = Guid.Empty;

                if (!string.IsNullOrEmpty(url))
                {
                    blobId = await _fileService.InsertBlob(url, mime);
                }

                userEntity.AttachmentId = blobId;
                userEntity.UpdatedBy = userId.ToString();
                userEntity.UpdatedAt = DateTime.Now.Date;

                _repository.User.UpdateUser(userEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserForUpdateDto user)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var userEntity = await _repository.User.GetUserById(user.EmployeeId);
                if (userEntity == null)
                {
                    _logger.LogError($"User with id: {user.EmployeeId}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(user, userEntity);
                _repository.User.UpdateUser(userEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser([FromQuery] Guid employeeId, [FromBody] UserForDeleteDto user)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var userEntity = await _repository.User.GetUserById(employeeId);
                if (userEntity == null)
                {
                    _logger.LogError($"User with id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(user, userEntity);
                _repository.User.DeleteUser(userEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
