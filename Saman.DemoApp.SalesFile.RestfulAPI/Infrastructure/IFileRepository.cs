using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public interface IFileRepository
    {
        public void InsertFileContent(CSVSalesFile salesFile);
        public CSVSalesFile GetById(string id);
    }
}
