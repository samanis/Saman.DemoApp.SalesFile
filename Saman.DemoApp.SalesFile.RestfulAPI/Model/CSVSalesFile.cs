using System;
using System.Collections.Generic;
using System.Linq;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Model
{
    public class CSVSalesFile : SalesFileBase
    {
        public CSVSalesFile(string fileContent, DateTime uploadedDateTime, string fileName) : base(fileContent, uploadedDateTime, fileName)
        {
        }

        public override SalesFileType SalesFileType => SalesFileType.CSV;

        public override void Validate()
        {
            var fileLines = this.FileContent.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            if (fileLines.Length < 2)
            {
                throw new Exception("CSV file has no content");
            }
        }
    }
}
