using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestClient
{
    public class HttpRequestClient<RespObj> : IDisposable
    {
        public HttpWebRequest Request;
        public HttpRequestClient(string url)
        {
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            Request = (HttpWebRequest)WebRequest.Create(url);
            //Request.ServerCertificateValidationCallback +=
            //        (sender, cert, chain, error) =>
            //        {
            //            return cert.GetCertHashString() == "xxxxxxxxxxxxxxxx";
            //        };
        }

        public RespObj Post(string data, PostType postType, string contentType = null)
        {
            try
            {
                Request.Method = "POST";
                Request.ContentType = contentType ?? "application/json";
                if (postType == PostType.Byte)
                {
                    byte[] byteArray = Encoding.ASCII.GetBytes(data);
                    Request.ContentLength = byteArray.Length;
                    //using (Stream inStream = Request.GetRequestStream())
                    //{
                    //    inStream.Write(byteArray, 0, byteArray.Length);
                    //}
                    Stream inStream = Request.GetRequestStream();
                    inStream.Write(byteArray, 0, byteArray.Length);
                    inStream.Close();
                }
                else
                {
                    Request.ContentLength = data.Length;
                    //using (Stream inStream = Request.GetRequestStream())
                    //{
                    //    //inStream.Write(byteArray, 0, byteArray.Length);
                    //    StreamWriter writeStream = new StreamWriter(inStream);
                    //    writeStream.Write(data);
                    //    writeStream.Flush();
                    //}
                    Stream inStream = Request.GetRequestStream();
                    StreamWriter writeStream = new StreamWriter(inStream);
                    writeStream.Write(data);
                    writeStream.Flush();
                }
                var response = (HttpWebResponse)Request.GetResponse();
                using (Stream rpRead = response.GetResponseStream())
                {
                    StreamReader stReader = new StreamReader(rpRead, Encoding.ASCII);
                    string outPutRD = stReader.ReadToEnd().Trim();
                    RespObj resp = JsonConvert.DeserializeObject<RespObj>(outPutRD);
                    return resp;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string PostString(string data, PostType postType, string contentType = null)
        {
            try
            {
                Request.Method = "POST";
                Request.ContentType = contentType ?? "application/json";
                if (postType == PostType.Byte)
                {
                    byte[] byteArray = Encoding.ASCII.GetBytes(data);
                    Request.ContentLength = byteArray.Length;
                    Stream inStream = Request.GetRequestStream();
                    inStream.Write(byteArray, 0, byteArray.Length);
                    inStream.Close();
                }
                else
                {
                    Request.ContentLength = data.Length;
                    Stream inStream = Request.GetRequestStream();
                    StreamWriter writeStream = new StreamWriter(inStream);
                    writeStream.Write(data);
                    writeStream.Flush();
                }
                var response = (HttpWebResponse)Request.GetResponse();
                using (Stream rpRead = response.GetResponseStream())
                {
                    StreamReader stReader = new StreamReader(rpRead, Encoding.ASCII);
                    string outPutRD = stReader.ReadToEnd().Trim();
                    return outPutRD;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RespObj Get()
        {
            try
            {
                Request.Method = "GET";
                var response = (HttpWebResponse)Request.GetResponse();
                using (Stream rpRead = response.GetResponseStream())
                {
                    StreamReader stReader = new StreamReader(rpRead, Encoding.ASCII);
                    string outPutRD = stReader.ReadToEnd().Trim();
                    RespObj resp = JsonConvert.DeserializeObject<RespObj>(outPutRD);
                    return resp;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void IDisposable.Dispose()
        {

        }
    }

    public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public TrustAllCertificatePolicy()
        {
        }

        public bool CheckValidationResult(System.Net.ServicePoint sp, System.Security.Cryptography.X509Certificates.X509Certificate cert, System.Net.WebRequest req, int problem)
        {
            return true;
        }
    }

    public enum PostType
    {
        String,
        Byte
    }
}
