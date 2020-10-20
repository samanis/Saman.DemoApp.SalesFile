using Microsoft.Extensions.Configuration;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Linq;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public class SQLDBFileRepository : IFileRepository<int>
    {
        CSVSalesFileDBContext _context;

        public SQLDBFileRepository(CSVSalesFileDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CSVSalesFile GetById(int id)
        {
            return _context.CSVSalesFiles.Single(x => x.Id == id);
        }

        public void InsertFileContent(CSVSalesFile salesFile)
        {
            _context.CSVSalesFiles.Add(salesFile);
            _context.SaveChanges();
        }
    }
}
