using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MPMSRS.Models.Entities;

namespace MPMSRS.Helpers.Utilities
{
    public class FileServices
    {
        private readonly RepositoryContext _context;
        private readonly string _azureConnectionString;

        public FileServices(RepositoryContext context, IConfiguration configuration)
        {
            _context = context;
            _azureConnectionString = configuration["AzureStorageBlob:AzureConnectionString"];
        }

        public async Task<Guid> InsertBlob(string url, string mime)
        {
            var newGuid = Guid.NewGuid();

            var getName = url;

            var createBlob = new Attachments
            {
                AttachmentId = newGuid,
                AttachmentUrl = getName,
                AttachmentMime = mime.ToLower(),
                CreatedAt = DateTime.Now,
            };

            _context.Attachments.Add(createBlob);
            await _context.SaveChangesAsync();
            return newGuid;
        }

        public async Task<string> uploadFileUrl(IFormFile file)
        {
            var thisDate = DateTime.Now;
            var container = new BlobContainerClient(_azureConnectionString, "upload-container");
            var createResponse = await container.CreateIfNotExistsAsync();
            //check container sudah terbuat apa belum?
            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var blob = container.GetBlobClient(file.FileName);
            var test = await blob.ExistsAsync();
            // check apakah blob sudah ada di container?
            if (test)
                blob = container.GetBlobClient(file.Name+"_"+thisDate.ToString("yyyyMMddHHmmss") +"_"+file.FileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            using (var fileStream = file.OpenReadStream())
            {
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
            }
            return blob.Uri.ToString();
        }
    }
}
