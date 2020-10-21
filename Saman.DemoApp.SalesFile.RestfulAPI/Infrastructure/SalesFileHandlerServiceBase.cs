using Microsoft.AspNetCore.Http;
using Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Interfsaces;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public class SalesFileHandlerServiceBase : ISalesFileHandlerService
    {
        IFileRepository<int> _fileRepository;
        INewUploadedFileEventHandler _newUploadedFileEventHandler;

        public SalesFileHandlerServiceBase(IFileRepository<int> fileRepository, INewUploadedFileEventHandler newUploadedFileEventHandler)
        {
            _newUploadedFileEventHandler = newUploadedFileEventHandler ?? throw new ArgumentNullException(nameof(newUploadedFileEventHandler));
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        }

        public void HandleUploadedSalesFile(IFormFile formFile, DateTime uploadedDateTime, ISalesFileValidator salesFileValidator, SalesFileType salesFileType)
        {
            try
            {
                string fileContent = ReadContentFromIFormFile(formFile);
                var SalesFile = new UploadedSalesFile(fileContent, uploadedDateTime, GenerateFileName(uploadedDateTime),salesFileValidator, salesFileType);
                _fileRepository.InsertFileContent(SalesFile);
                NewFileUploadedEvent newFileUploadedEvent = new NewFileUploadedEvent(SalesFile.FileName, SalesFile.UploadedDateTime);
                _newUploadedFileEventHandler.SendNewFileUploadedEvent(newFileUploadedEvent);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not save file or send message", ex);
            }
        }

        public ISalesFile RetirveFile(int id)
        {
            return _fileRepository.GetById(id);
        }

        protected static string ReadContentFromIFormFile(IFormFile formFile)
        {
            string fileContent;

            using (var reader = new StreamReader(formFile.OpenReadStream(), Encoding.GetEncoding("iso-8859-1")))
            {
                fileContent = reader.ReadToEnd();
            }

            return fileContent;
        }

        protected static string GenerateFileName(DateTime dateTime)
        {
            return $"{dateTime.ToString("yyyyMMddThhmmssfffff", CultureInfo.CreateSpecificCulture("en-ca"))}.csv";
        }

       
    }
}
