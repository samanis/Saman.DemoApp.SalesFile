using Microsoft.AspNetCore.Http;
using System;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public interface IAppService
    {
        public void HandleUploadedSalesFile(IFormFile formFile, DateTime uploadedDateTime);
    }
}
