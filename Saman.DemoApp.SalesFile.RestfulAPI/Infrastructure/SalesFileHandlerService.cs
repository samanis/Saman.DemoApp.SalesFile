using Microsoft.AspNetCore.Http;
using Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Interfsaces;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public class SalesFileHandlerService : ISalesFileHandlerService
    {
        IFileRepository<int> _fileRepository;
        INewUploadedFileEventHandler _newUploadedFileEventHandler;

        public SalesFileHandlerService(IFileRepository<int> fileRepository, INewUploadedFileEventHandler newUploadedFileEventHandler)
        {
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            _newUploadedFileEventHandler = newUploadedFileEventHandler ?? throw new ArgumentNullException(nameof(newUploadedFileEventHandler));
        }

        public void HandleUploadedSalesFile(IFormFile formFile, DateTime uploadedDateTime)
        {
            try
            {
                SalesFileFactory salesFileFactory = new SalesFileFactory();
                var salesFile = salesFileFactory.CreateSalesFile(formFile, uploadedDateTime);
                _fileRepository.InsertFileContent(salesFile);
                NewFileUploadedEvent newFileUploadedEvent = new NewFileUploadedEvent(salesFile.FileName, salesFile.UploadedDateTime);
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
    }
}
