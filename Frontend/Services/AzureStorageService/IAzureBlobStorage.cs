using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.AzureStorageService
{
    public interface IAzureBlobStorage
    {
        Task<string> UploadFileToBlob(IFormFile file);
    }
}
