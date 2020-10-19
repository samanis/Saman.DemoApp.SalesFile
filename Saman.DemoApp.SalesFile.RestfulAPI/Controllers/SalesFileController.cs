using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalesFileController : ControllerBase
    {
        IAppService _appService;

        public SalesFileController(IAppService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [HttpPost]
        public IActionResult Upload()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                _appService.HandleUploadedSalesFile(file, DateTime.UtcNow);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult GetFile()
        {
            return Ok("GetFile");
        }
    }
}