using Azure.Storage.Blobs;
using Frontend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.AzureStorageService
{
    // service that helps working with Azure blob storage
    public class AzureStorageService : IAzureBlobStorage
    {

        private readonly string ConnectionString;

        private readonly string Container;

        public AzureStorageService(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("StorageAccountConnectionString");
            Container = config.GetConnectionString("ContainerName");
        }

        public async Task<string> UploadFileToBlob(IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(ConnectionString);

            var containerClient = blobServiceClient.GetBlobContainerClient(Container);

            var blobClient = containerClient.GetBlobClient(file.FileName);
            await blobClient.UploadAsync(file.OpenReadStream());
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
