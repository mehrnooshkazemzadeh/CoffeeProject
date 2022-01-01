using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;

namespace Framework.Core.Tools
{
    public class IpProcessor:IIPProcessor
    {

        public IpInfo IpInfoValue { get; set; }

        public IpProcessor()
        {

        }
        [DebuggerNonUserCode]
        public void SetIpInfoValue(HttpContext context)
        {
            try
            {
                IpInfoValue = new IpInfo();
                var info = new WebClient().DownloadString("http://ipinfo.io/" + context.Connection.RemoteIpAddress.ToString());
                IpInfoValue = JsonConvert.DeserializeObject<IpInfo>(info);
                //var myRI1 = new RegionInfo(ipInfo.Country);
                //ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                IpInfoValue.Country = null;
            }
        }
    }
}
