using Microsoft.AspNetCore.Http;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public class AppService : IAppService
    {
        IFileRepository _fileRepository;
        INewUploadedFileEventHandler _newUploadedFileEventHandler;

        public AppService(IFileRepository fileRepository, INewUploadedFileEventHandler newUploadedFileEventHandler)
        {
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            _newUploadedFileEventHandler = newUploadedFileEventHandler ?? throw new ArgumentNullException(nameof(newUploadedFileEventHandler));
        }


        public void HandleUploadedSalesFile(IFormFile formFile, DateTime uploadedDateTime)
        {
            try
            {
                string fileContent = ReadContentFromIFormFile(formFile);
                CSVSalesFile cSVSalesFile = new CSVSalesFile(fileContent, uploadedDateTime, GenerateFileName(uploadedDateTime));
                _fileRepository.InsertFileContent(cSVSalesFile);
                NewFileUploadedEvent newFileUploadedEvent = new NewFileUploadedEvent(cSVSalesFile.FileName, cSVSalesFile.UploadedDateTime);
                _newUploadedFileEventHandler.SendNewFileUploadedEvent(newFileUploadedEvent);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Applicatrion Service", ex);
            }

        }       

        private static string ReadContentFromIFormFile(IFormFile formFile)
        {
            string fileContent;
           
            using (var reader = new StreamReader(formFile.OpenReadStream(),Encoding.GetEncoding("iso-8859-1")))
            {
                fileContent = reader.ReadToEnd();
            }

            return fileContent;
        }

        private string GenerateFileName(DateTime dateTime)
        {
            return$"{dateTime.ToString("yyyyMMddThhmmssfffff", CultureInfo.CreateSpecificCulture("en-ca"))}.csv";
        }

        public SalesFileBase RetirveFile(string id)
        {
            return _fileRepository.GetById(id);
        }
    }
}
