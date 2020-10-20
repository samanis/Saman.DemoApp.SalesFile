using Microsoft.EntityFrameworkCore;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public class CSVSalesFileDBContext : DbContext
    {
        public CSVSalesFileDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CSVSalesFile> CSVSalesFiles { get; set; }
    }
}
