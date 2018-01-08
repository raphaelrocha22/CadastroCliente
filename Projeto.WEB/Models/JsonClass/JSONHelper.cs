using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace Projeto.WEB.Models.JsonClass
{
    public class JSONHelper
    {
        public static string GetJSONString(string cnpj)
        {
            HttpWebRequest request =
            (HttpWebRequest)WebRequest.Create($"https://www.receitaws.com.br/v1/cnpj/{cnpj}");
            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(
                    stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static T GetObjectFromJSONString<T>(string json) where T : new()
        {
            using (MemoryStream stream = new MemoryStream(
                Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }

    }
}