using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankManager
{
    class RequestSender
    {
        public static object Execute(string uri, string requestString)
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            byte[] data = Encoding.UTF8.GetBytes(requestString);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.GetRequestStream().Write(data, 0, data.Length);
            WebResponse response = request.GetResponse();
            object result;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
        public static string Execute(string uri)
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            byte[] data = Encoding.UTF8.GetBytes("");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.GetRequestStream().Write(data, 0, data.Length);
            WebResponse response = request.GetResponse();
            string result;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = (reader.ReadToEnd());
                }
            }
            return result;
        }
    }
}
