using Microsoft.AspNetCore.Http;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public interface IAppService
    {
        public void HandleUploadedSalesFile(IFormFile formFile, DateTime uploadedDateTime);
        public SalesFileBase RetirveFile(int id);
    }
}
