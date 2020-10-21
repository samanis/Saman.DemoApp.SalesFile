using Microsoft.Extensions.Configuration;
using Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Interfsaces;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Linq;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Repositories
{
    public class SQLDBFileRepository : IFileRepository<int>
    {
        SalesFileDBContext _context;

        public SQLDBFileRepository(SalesFileDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UploadedSalesFile GetById(int id)
        {
            return _context.SalesFiles.Single(x => x.Id == id);
        }

        public void InsertFileContent(UploadedSalesFile salesFile)
        {
            _context.SalesFiles.Add(salesFile);
            _context.SaveChanges();
        }
    }
}
