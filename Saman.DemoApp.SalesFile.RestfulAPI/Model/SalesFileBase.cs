using System;
using System.Collections.Generic;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Model
{
    public abstract class SalesFileBase : ISalesFile
    {
        protected SalesFileBase(string fileContent, DateTime uploadedDateTime, string fileName)
        {
            FileContent = fileContent ?? throw new ArgumentNullException(nameof(fileContent));
            UploadedDateTime = uploadedDateTime;
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            BasicValidation();
            Validate();
        }

        public string FileContent { get; set; }
        public DateTime UploadedDateTime { get; set; }
        //public string[] FileLines { get; private set; }
        public abstract SalesFileType SalesFileType { get; }
        public string FileName { get; set; }

        public abstract void Validate();

        private void BasicValidation()
        {
            if (string.IsNullOrEmpty(FileContent.Trim()))
                throw new ArgumentException("File is empty or null");

        }

        
    }

}
