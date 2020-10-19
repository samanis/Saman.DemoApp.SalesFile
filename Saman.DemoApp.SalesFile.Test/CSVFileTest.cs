using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.IO;
using Xunit;

namespace Saman.DemoApp.SalesFile.Test
{
    
    public class CSVFileTest
    {
        string _csvSaleString;
        string _fileName;
        DateTime _uploadDateTime;
        CSVSalesFile _cvsSalesFile;

        public CSVFileTest()
        {
            using (var sr = new StreamReader("Dealertrack-CSV-Example.csv"))
            {
                // Read the stream as a string, and write the string to the console.
                _csvSaleString = sr.ReadToEnd();
            }
            _fileName = $"{DateTime.UtcNow}.csv";
            _uploadDateTime = DateTime.UtcNow;
            _cvsSalesFile = new CSVSalesFile(_csvSaleString, _uploadDateTime, _fileName);
        }

        [Fact]
        public void File_Cant_Be_Empty()
        {
            Exception exception = Assert.Throws<ArgumentException>(() => new CSVSalesFile(string.Empty, _uploadDateTime, _fileName));
            Assert.Equal("File is empty or null", exception.Message);
        }

        [Fact]
        public void File_Should_Have_Header_And_Content()
        {
            Exception exception = Assert.Throws<Exception>(() => new CSVSalesFile("DealNumber,CustomerName,DealershipName,Vehicle,Price,Date", _uploadDateTime, _fileName));
            Assert.Equal("CSV file has no content", exception.Message);
        }

        [Fact]
        public void Ctor_Assigned_FileName_As_Expected()
        {
          
            Assert.Equal(_fileName, _cvsSalesFile.FileName);
        }

        [Fact]
        public void Ctor_Assigned_FileContent_As_Expected()
        {

            Assert.Equal(_csvSaleString, _cvsSalesFile.FileContent);
        }

        [Fact]
        public void Ctor_Assigned_UploadDateTime_As_Expected()
        {

            Assert.Equal(_uploadDateTime, _cvsSalesFile.UploadedDateTime);
        }

        [Fact]
        public void Ctor_Assigned_SalesFileType_As_Expected()
        {

            Assert.Equal(SalesFileType.CSV, _cvsSalesFile.SalesFileType);
        }

    }
}
