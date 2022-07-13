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
using MPMSRS.Services.Repositories.Auth;

namespace MPMSRS.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private DatabaseRefreshTokenRepository _services;

        public RoleApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, DatabaseRefreshTokenRepository service)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _services = service;
        }

        [Authorize]
        [HttpGet("get-all-roles", Name = "GetRolesAll")]
        public async Task<IActionResult> GetAllRoles([FromQuery] int pageSize, int pageIndex)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var roles = await _repository.Role.GetAllRoles(pageSize,pageIndex);
                _logger.LogInfo($"Returned all roles from database.");

                var rolesResult = _mapper.Map<IEnumerable<RoleDto>>(roles);

                return Ok(rolesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllRoles action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-role-by-id/{id}", Name = "RoleById")]
        public async Task<IActionResult> GetRoleById([FromQuery] Guid roleId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var role = await _repository.Role.GetRoleById(roleId);
                if (role == null)
                {
                    _logger.LogError($"Role with id: {roleId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned role with id: {roleId}");
                    var roleResult = _mapper.Map<RoleDto>(role);
                    return Ok(roleResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRoleById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-role", Name = "InsertRole")]
        public IActionResult CreateRole([FromBody] RoleForCreationDto role)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (role == null)
                {
                    _logger.LogError("Role object sent from client is null.");
                    return BadRequest("Role object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid role object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var roleEntity = _mapper.Map<Roles>(role);
                _repository.Role.CreateRole(roleEntity);
                _repository.Save();
                var createdRole = _mapper.Map<RoleDto>(roleEntity);
                return CreatedAtRoute("RoleById", new { id = createdRole.RoleId }, createdRole);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-role/{id}")]
        public async Task<IActionResult> UpdateRole([FromQuery] Guid id, [FromBody] RoleForUpdateDto role)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (role == null)
                {
                    _logger.LogError("Role object sent from client is null.");
                    return BadRequest("Role object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid role object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var roleEntity = await _repository.Role.GetRoleById(id);
                if (roleEntity == null)
                {
                    _logger.LogError($"Role with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(role, roleEntity);
                _repository.Role.UpdateRole(roleEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateRole action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-role/{id}")]
        public async Task<IActionResult> DeleteRole([FromQuery] Guid id, [FromBody] RoleForDeleteDto role)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (role == null)
                {
                    _logger.LogError("Role object sent from client is null.");
                    return BadRequest("Role object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Role object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var roleEntity = await _repository.Role.GetRoleById(id);
                if (roleEntity == null)
                {
                    _logger.LogError($"Role with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(role, roleEntity);
                _repository.Role.DeleteRole(roleEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteRole action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
