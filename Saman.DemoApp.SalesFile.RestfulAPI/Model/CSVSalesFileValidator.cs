using System;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Model
{
    public class CSVSalesFileValidator : ISalesFileValidator
    {
        public void ValidateSalesFile(ISalesFile salesFile)
        {
            var fileLines = salesFile.FileContent.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            if(salesFile.SalesFileType != SalesFileType.CSV)
            {
                throw new Exception("File type should be CSV");
            }
            if (fileLines.Length < 2)
            {
                throw new Exception("CSV file has no content");
            }
        }
    }
}
