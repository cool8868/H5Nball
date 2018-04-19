using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Games.NBall.WebClient.Util
{
    public static class JsonUtil
    {
        const string s_jsonStart = "{";
        const string s_jsonEnd = "}";

        public static string JsonSerializer(object t)
        {
            var ser = new DataContractJsonSerializer(t.GetType());
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, t);
                ms.Position = 0;
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        public static string JsonSerializer<T>(T t)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, t);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        public static T JsonDeserializer<T>(string json) where T : class
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return ser.ReadObject(ms) as T;
            }
        }

        public static bool MaybeJson(string input)
        {
            return input.StartsWith(s_jsonStart) && input.EndsWith(s_jsonEnd);
        }
        public static string GetJsonValue(object json, string propName)
        {
            var jo = json as JObject;
            return GetJsonValue(jo, propName);
        }

        public static string GetJsonValue(JObject jo, string propName)
        {
            if (null == jo)
                return string.Empty;
            var jv = jo.GetValue(propName, StringComparison.OrdinalIgnoreCase) as JValue;
            if (null == jv)
                return string.Empty;
            return jv.Value.ToString();
        }
    }
}
