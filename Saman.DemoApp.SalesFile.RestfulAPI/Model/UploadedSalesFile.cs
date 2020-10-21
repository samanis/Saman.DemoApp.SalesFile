using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Model
{
    public class UploadedSalesFile : ISalesFile
    {
        ISalesFileValidator _salesFileValidator;
        public UploadedSalesFile(string fileContent, DateTime uploadedDateTime, string fileName, 
            ISalesFileValidator salesFileValidator, SalesFileType salesFileType)
        {
            FileContent = fileContent ?? throw new ArgumentNullException(nameof(fileContent));
            UploadedDateTime = uploadedDateTime;
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            _salesFileValidator = salesFileValidator ?? throw new ArgumentNullException(nameof(salesFileValidator));
            SalesFileType = salesFileType;
            BasicValidation();
            Validate();
        }

        protected UploadedSalesFile()
        {
        }

        public string FileContent { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public SalesFileType SalesFileType { get; }

        public string FileName { get; set; }

        public void Validate()
        {
            _salesFileValidator.ValidateSalesFile(this);
        }

        public int Id { get; set; }

        private void BasicValidation()
        {
            if (string.IsNullOrEmpty(FileContent.Trim()))
                throw new ArgumentException("File is empty or null");

        }

        
    }

}
