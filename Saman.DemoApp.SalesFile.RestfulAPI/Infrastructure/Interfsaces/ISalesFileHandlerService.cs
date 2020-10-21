using Microsoft.AspNetCore.Http;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Interfsaces
{
    public interface ISalesFileHandlerService
    {
        public void HandleUploadedSalesFile(IFormFile formFile, DateTime uploadedDateTime);
        public ISalesFile RetirveFile(int id);
    }
}
