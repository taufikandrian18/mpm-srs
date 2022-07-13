using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMSRS.Helpers.Utilities;
using MPMSRS.Models.VM;

namespace MPMSRS.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaApiController : ControllerBase
    {
        private FileServices _fileService;

        public MediaApiController(FileServices fs)
        {
            _fileService = fs;
        }

        [Authorize]
        [HttpPost("upload-media", Name = "UploadMedia")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var url = new AttachmentDto();
                if (file.Length > 0)
                {
                    url.AttachmentUrl = await _fileService.uploadFileUrl(file);
                    url.AttachmentMime = System.IO.Path.GetExtension(url.AttachmentUrl);
                    return Ok(url);
                }
                return BadRequest("User tidak mengirimkan file");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPost("upload-bulk-media", Name = "UploadBulkMedia")]
        public async Task<IActionResult> UploadBulk()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.ToList();
                var url = new List<AttachmentDto>();
                if (file.Count() > 0)
                {
                    foreach(var item in file)
                    {
                        var tempUrl = new AttachmentDto();
                        tempUrl.AttachmentUrl = await _fileService.uploadFileUrl(item);
                        tempUrl.AttachmentMime = System.IO.Path.GetExtension(tempUrl.AttachmentUrl);
                        url.Add(tempUrl);
                    }
                    return Ok(url);
                }
                return BadRequest("User tidak mengirimkan file");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
