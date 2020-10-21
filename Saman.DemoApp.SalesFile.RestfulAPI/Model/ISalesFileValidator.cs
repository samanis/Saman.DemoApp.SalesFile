using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Model
{
    public interface ISalesFileValidator
    {
        void ValidateSalesFile(ISalesFile salesFile);
    }
}
