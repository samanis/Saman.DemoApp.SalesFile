﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Interfsaces
{
    public interface INewUploadedFileEventHandler
    {
        public void SendNewFileUploadedEvent(NewFileUploadedEvent newFileUploadedEvent);
    }
}
