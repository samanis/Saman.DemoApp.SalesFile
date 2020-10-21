using Microsoft.AspNetCore.Http;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public class SalesFileFactory
    {
        public UploadedSalesFile CreateSalesFile(IFormFile formFile, DateTime uploadedDateTime)
        {
            string extension = Path.GetExtension(formFile.FileName);
            switch (extension)
            {
                case ".csv":
                    {
                        string fileContent = ReadContentFromIFormFile(formFile);
                        var salesFile = new UploadedSalesFile(fileContent, uploadedDateTime, 
                            $"{GenerateFileName(uploadedDateTime)}.csv", new CSVSalesFileValidator(),SalesFileType.CSV);
                        return salesFile;
                    }
                default:
                    throw new ApplicationException("Uploaded file type does not supported");
            }
        }

        private static string ReadContentFromIFormFile(IFormFile formFile)
        {
            string fileContent;

            using (var reader = new StreamReader(formFile.OpenReadStream(), Encoding.GetEncoding("iso-8859-1")))
            {
                fileContent = reader.ReadToEnd();
            }

            return fileContent;
        }

        private static string GenerateFileName(DateTime dateTime)
        {
            return $"{dateTime.ToString("yyyyMMddThhmmssfffff", CultureInfo.CreateSpecificCulture("en-ca"))}";
        }
    }
}
