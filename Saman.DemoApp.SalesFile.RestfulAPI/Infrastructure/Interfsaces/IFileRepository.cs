using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Interfsaces
{
    public interface IFileRepository<T>
    {
        public void InsertFileContent(UploadedSalesFile salesFile);
        public UploadedSalesFile GetById(T id);
    }
}
