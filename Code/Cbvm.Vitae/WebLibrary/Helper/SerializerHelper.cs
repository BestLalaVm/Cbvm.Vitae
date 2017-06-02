using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WebLibrary.Helper
{
    public static class SerializerHelper
    {
        private static JavaScriptSerializer _scriptSerializer = new JavaScriptSerializer();

        public static string Serialize<T>(T t)
        {
            var json = new StringBuilder();
            _scriptSerializer.Serialize(t, json);
            return json.ToString();
        }

        public static T Deserialize<T>(string json)
        {
            return _scriptSerializer.Deserialize<T>(json);
        }

        public static string MD5Password(string password)
        {
            var instance = MD5.Create();
            var bytePassword = Encoding.UTF8.GetBytes(password);
            var data = instance.ComputeHash(bytePassword);
            StringBuilder sb = new StringBuilder();
            for (var index = 0; index < data.Length; index++)
            {
                sb.Append(data[index].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
