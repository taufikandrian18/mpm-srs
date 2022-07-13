using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using OfficeOpenXml;

namespace MPMSRS.Controllers
{
    [Route("api/networks")]
    [ApiController]
    public class NetworkApiController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public NetworkApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("get-all-networks", Name = "GetNetworkAll")]
        public async Task<IActionResult> GetAllNetworks([FromQuery] int pageSize, int pageIndex, string area, string query)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var networks = await _repository.Network.GetAllNetworks(pageSize, pageIndex, area, query);
                _logger.LogInfo($"Returned all networks from database.");

                //var networksResult = _mapper.Map<IEnumerable<NetworkDto>>(networks);

                return Ok(networks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllNetworks action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-all-network-city", Name = "GetNetworCitykAll")]
        public async Task<IActionResult> GetAllNetworkCity()
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var networks = await _repository.Network.GetAllNetworkCity();
                _logger.LogInfo($"Returned all network city from database.");

                //var networksResult = _mapper.Map<IEnumerable<NetworkDto>>(networks);

                return Ok(networks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllNetworkCity action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-network-by-id/{id}", Name = "NetworkById")]
        public async Task<IActionResult> GetNetworkById([FromQuery] Guid networkId)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var network = await _repository.Network.GetNetworksById(networkId);
                if (network == null)
                {
                    _logger.LogError($"Network with id: {networkId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Network with id: {networkId}");
                    var networkResult = _mapper.Map<NetworkDto>(network);
                    return Ok(networkResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetNetworkById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-network-by-md-code/{mdCode}", Name = "NetworkByMDCode")]
        public async Task<IActionResult> GetNetworkByMDCode([FromQuery] string mdCode)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var network = await _repository.Network.GetNetworksByMDCode(mdCode.Trim());
                if (network == null)
                {
                    _logger.LogError($"Network with MD Code: {mdCode}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Network with MD Code: {mdCode}");
                    //var networkResult = _mapper.Map<NetworkDto>(network);
                    return Ok(network);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetNetworkByMDCode action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-network-by-ahm-code/{ahmCode}", Name = "NetworkByAHMCode")]
        public async Task<IActionResult> GetNetworkByAHMCode([FromQuery] string ahmCode)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var network = await _repository.Network.GetNetworksByAhmCode(ahmCode.Trim());
                if (network == null)
                {
                    _logger.LogError($"Network with AHM Code: {ahmCode}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Network with AHM Code: {ahmCode}");
                    //var networkResult = _mapper.Map<NetworkDto>(network);
                    return Ok(network);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetNetworkByAHMCode action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("get-network-by-dealer-name/{dealerName}", Name = "NetworkByDealerName")]
        public async Task<IActionResult> GetNetworkByDealerName([FromQuery] string dealerName)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var network = await _repository.Network.GetNetworksByNamaDealer(dealerName.Trim());
                if (network == null)
                {
                    _logger.LogError($"Network with Dealer Name: {dealerName}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Network with Dealer Name: {dealerName}");
                    //var networkResult = _mapper.Map<NetworkDto>(network);
                    return Ok(network);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetNetworkByDealerName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("insert-network", Name = "InsertNetwork")]
        public IActionResult CreateNetwork([FromBody] NetworkForCreationDto network)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (network == null)
                {
                    _logger.LogError("Network object sent from client is null.");
                    return BadRequest("Network object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid network object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var networkEntity = _mapper.Map<Networks>(network);
                _repository.Network.CreateNetwork(networkEntity);
                _repository.Save();
                var createdNetwork = _mapper.Map<NetworkDto>(networkEntity);
                return CreatedAtRoute("NetworkById", new { id = createdNetwork.NetworkId }, createdNetwork);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateNetwork action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPut("update-network/{id}")]
        public async Task<IActionResult> UpdateNetwork([FromQuery] Guid id, [FromBody] NetworkForUpdateDto network)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (network == null)
                {
                    _logger.LogError("Network object sent from client is null.");
                    return BadRequest("Network object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid network object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var networkEntity = await _repository.Network.GetNetworksById(id);
                if (networkEntity == null)
                {
                    _logger.LogError($"Network with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(network, networkEntity);
                _repository.Network.UpdateNetwork(networkEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateNetwork action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpDelete("delete-network/{id}")]
        public async Task<IActionResult> DeleteNetwork([FromQuery] Guid id, [FromBody] NetworkForDeleteDto network)
        {
            try
            {
                // using header {bearer (token)}
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                if (network == null)
                {
                    _logger.LogError("Network object sent from client is null.");
                    return BadRequest("Network object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid network object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var networkEntity = await _repository.Network.GetNetworksById(id);
                if (networkEntity == null)
                {
                    _logger.LogError($"Network with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(network, networkEntity);
                _repository.Network.DeleteNetwork(networkEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteNetwork action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("import-excel-network-coordinates", Name = "ImportNetworkCoordinateExcel")]
        public async Task<IActionResult> NetworkCoordinateImportExcel([FromForm] IEnumerable<IFormFile> files)
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

                if (files.Count() > 0)
                {
                    var file = files.FirstOrDefault();
                    if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogError("file format sent from client is invalid.");
                        return BadRequest("file format object is invalid");
                    }
                    else
                    {
                        var listPC = new List<ProblemCategoryDto>();
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);

                            using (var package = new ExcelPackage(stream))
                            {
                                ExcelWorksheet worksheet = package.Workbook.Worksheets["KoordinateOutlet"];
                                if (worksheet == null)
                                {
                                    _logger.LogError("file template format sent from client is invalid.");
                                    return BadRequest("file template format object is invalid");
                                }
                                else
                                {
                                    var rowCount = worksheet.Dimension.Rows;

                                    for (int row = 2; row <= rowCount; row++)
                                    {
                                        var mdCode = worksheet.Cells[row, 3].Value?.ToString().Trim();
                                        var latitude = worksheet.Cells[row, 8].Value?.ToString().Trim();
                                        var longitude = worksheet.Cells[row, 9].Value?.ToString().Trim();

                                        if (mdCode == null || latitude == null || longitude == null)
                                        {
                                            _logger.LogError($"data in row: {row}, can't be empty.");
                                            return BadRequest($"data in row: {row}, can't be empty.");
                                        }
                                        else
                                        {
                                            //di check apakah mdCode dari excel kosong atau tidak
                                            if (!string.IsNullOrEmpty(mdCode))
                                            {
                                                //jika tidak kosong mdCode di check ke database ada atau tidak
                                                var networkEntity = await _repository.Network.GetNetworksByMDCode(mdCode);
                                                if (networkEntity != null)
                                                {
                                                    foreach (var item in networkEntity)
                                                    {
                                                        item.NetworkLatitude = latitude;
                                                        item.NetworkLongitude = longitude;
                                                        _repository.Network.UpdateNetwork(item);
                                                        _repository.Save();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                _logger.LogError($"data in row: {row}, can't be empty.");
                                                return BadRequest($"data in row: {row}, can't be empty.");
                                            }
                                        }
                                    }
                                }
                            }

                            return NoContent();
                        }
                    }
                }
                else
                {
                    _logger.LogError("files form object sent from client is null.");
                    return BadRequest("files form object is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside NetworkCoordinateImportExcel action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
