using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Model
{
    public interface ISalesFile
    {
        string FileContent { get; }
        DateTime UploadedDateTime { get; }
        SalesFileType SalesFileType { get; }

        string FileName { get; set; }

        void Validate();
    }
}
