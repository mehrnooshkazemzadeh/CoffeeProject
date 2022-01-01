using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static Framework.Core.Tools.IpProcessor;

namespace Framework.Core.Tools
{
    public interface IIPProcessor
    {
        IpInfo IpInfoValue { get; set; }
        void SetIpInfoValue(HttpContext context);
    }
}
