using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Interfsaces;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalesFileController : ControllerBase
    {
        ISalesFileHandlerService _appService;

        public SalesFileController(ISalesFileHandlerService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [HttpPost]
        public IActionResult UploadFile()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                if (file != null)
                {
                    _appService.HandleUploadedSalesFile(file, DateTime.UtcNow);
                    return Created("", "sales file uploaded");
                }
                return BadRequest("Null request sent");
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        [HttpGet("{id}")]
        public IActionResult GetFile(int id)
        {
            var retrievedFile = _appService.RetirveFile(id);
            if (retrievedFile is null)
                return NotFound();
            return Ok(_appService.RetirveFile(id));
        }
    }
}