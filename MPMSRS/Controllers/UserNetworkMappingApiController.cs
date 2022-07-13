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
using MPMSRS.Services.Interfaces.Auth;
using MPMSRS.Services.Interfaces.FCM;

namespace MPMSRS.Controllers
{
    [Route("api/user-network-mappings")]
    [ApiController]
    public class UserNetworkMappingApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private INotificationService _notification;

        public UserNetworkMappingApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IRefreshTokenRepository refreshTokenRepository, INotificationService notification)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
            _notification = notification;
        }

        [Authorize]
        [HttpGet("get-all-user-network-mappings", Name = "GetUserNetworkMappingAll")]
        public async Task<IActionResult> GetAllUserNetworkMappings([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var userNetworks = await _repository.UserNetworkMapping.GetAllUserNetworkMappings(pageSize,pageIndex);
                _logger.LogInfo($"Returned all user network mappings from database.");

                var userNetworksResult = _mapper.Map<IEnumerable<UserNetworkMappingDto>>(userNetworks);

                return Ok(userNetworksResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUserNetworkMappings action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-user-network-mapping-by-login-id", Name = "GetUserNetworkMappingByLoginId")]
        public async Task<IActionResult> GetAllUserNetworkMappingByLoginId([FromQuery] int pageSize, int pageIndex, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var userNetworks = await _repository.UserNetworkMapping.GetNetworksByEmployeeId(userId, pageSize, pageIndex, area, query);
                _logger.LogInfo($"Returned all network by login id from database.");

                //var userNetworksResult = _mapper.Map<IEnumerable<NetworkDto>>(userNetworks);

                return Ok(userNetworks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUserNetworkMappingByLoginId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-user-network-mapping-by-employee-id", Name = "GetUserNetworkMappingByEmployeeId")]
        public async Task<IActionResult> GetAllUserNetworkMappingByEmployeeId([FromQuery] Guid employeeId, int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var userNetworks = await _repository.UserNetworkMapping.GetUserNetworkObjectByEmpId(employeeId, pageSize, pageIndex);
                _logger.LogInfo($"Returned all network by employee id from database.");

                //var userNetworksResult = _mapper.Map<IEnumerable<NetworkDto>>(userNetworks);

                return Ok(userNetworks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUserNetworkMappingByLoginId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-user-network-mapping-by-id/{id}", Name = "UserNetworkMappingById")]
        public async Task<IActionResult> GetUserNetworkMappingById([FromQuery] Guid userNetworkMappingId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var userNetwork = await _repository.UserNetworkMapping.GetUserNetworkById(userNetworkMappingId);
                if (userNetwork == null)
                {
                    _logger.LogError($"User Network Mapping with id: {userNetworkMappingId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned User Network Mapping with id: {userNetworkMappingId}");
                    var userNetworkResult = _mapper.Map<UserNetworkMappingDto>(userNetwork);
                    return Ok(userNetworkResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserNetworkMappingById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-user-network-mapping", Name = "InsertUserNetworkMapping")]
        public async Task<IActionResult> CreateUserNetworkMapping([FromBody] UserNetworkMappingForCreationMapDto userNetworkMapping)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (userNetworkMapping == null)
                {
                    _logger.LogError("User Network Mapping object sent from client is null.");
                    return BadRequest("User Network Mapping object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user network mapping object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dateNow = DateTime.Now;

                var userNetworks = await _repository.UserNetworkMapping.GetUserNetworkByEmployeeId(userNetworkMapping.EmployeeId,1,100);
                if (userNetworks.Count() > 0)
                {
                    _logger.LogError($"Network id with employee id: {userNetworkMapping.EmployeeId} is already assigned in database.");
                    return BadRequest($"Network id with employee id: {userNetworkMapping.EmployeeId} is already assigned in database.");
                }

                if (userNetworkMapping.NetworkId.Count() > 0)
                {
                    foreach (var sub in userNetworkMapping.NetworkId)
                    {
                        //tambah validasi network
                        var checkNetwork = await _repository.UserNetworkMapping.GetUserNetworkByNetworkId(Guid.Parse(sub), userNetworkMapping.EmployeeId);
                        if(checkNetwork == null)
                        {
                            UserNetworkMappingForCreationDto userNetworkObj = new UserNetworkMappingForCreationDto();
                            userNetworkObj.NetworkId = Guid.Parse(sub);
                            userNetworkObj.EmployeeId = userNetworkMapping.EmployeeId;
                            userNetworkObj.CreatedAt = dateNow;
                            userNetworkObj.CreatedBy = userNetworkMapping.CreatedBy;
                            userNetworkObj.UpdatedAt = dateNow;
                            userNetworkObj.UpdatedBy = userNetworkMapping.UpdatedBy;
                            var userNetworkEntity = _mapper.Map<UserNetworkMappings>(userNetworkObj);
                            _repository.UserNetworkMapping.CreateUserNetwork(userNetworkEntity);
                            _repository.Save();
                        }
                    }

                    return CreatedAtRoute("UserById", new { id = userNetworkMapping.NetworkId }, userNetworkMapping);
                }
                else
                {
                    _logger.LogError("User Network Mapping Network Id sent from client is null.");
                    return BadRequest("User Network Mapping Network Id is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUserNetworkMapping action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-user-network-mapping/{id}")]
        public async Task<IActionResult> UpdateUserNetworkMapping([FromQuery] Guid id, [FromBody] UserNetworkMappingForUpdateDto userNetworkMapping)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (userNetworkMapping == null)
                {
                    _logger.LogError("User Network Mapping object sent from client is null.");
                    return BadRequest("User Network Mapping object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user network mapping object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var userNetworkEntity = await _repository.UserNetworkMapping.GetUserNetworkById(id);
                if (userNetworkEntity == null)
                {
                    _logger.LogError($"User Network Mapping with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(userNetworkMapping, userNetworkEntity);
                _repository.UserNetworkMapping.UpdateUserNetwork(userNetworkEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUserNetworkMapping action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-user-network-mapping/{id}")]
        public async Task<IActionResult> DeleteUserNetworkMapping([FromQuery] Guid id, [FromBody] UserNetworkMappingForDeleteDto userNetworkMapping)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (userNetworkMapping == null)
                {
                    _logger.LogError("User Network Mapping object sent from client is null.");
                    return BadRequest("User Network Mapping object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user network mapping object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var userNetworkEntity = await _repository.UserNetworkMapping.GetUserNetworkById(id);
                if (userNetworkEntity == null)
                {
                    _logger.LogError($"User Network Mapping with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(userNetworkMapping, userNetworkEntity);
                _repository.UserNetworkMapping.DeleteUserNetwork(userNetworkEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUserNetworkMapping action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
