using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestClient
{
    public static class Proxy
    {
        public static void centralProxy(string url, int port)
        {
            NetworkCredential networkCredential = new NetworkCredential();
            WebProxy pry = new WebProxy(url, port);
            pry.Credentials = networkCredential;
            WebRequest.DefaultWebProxy = pry;
        }
    }
}
