using Microsoft.EntityFrameworkCore;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Repositories
{
    public class SalesFileDBContext : DbContext
    {
        public SalesFileDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UploadedSalesFile> SalesFiles { get; set; }
    }
}
