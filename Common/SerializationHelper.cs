using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using ProtoBuf;

namespace Games.NBall.Common
{
    /// <summary>
    /// 序列化
    /// </summary>
    public static class SerializationHelper
    {
        /// <summary>
        /// Json序列化
        /// </summary>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        /// <summary>
        /// Jason反序列化
        /// </summary>
        public static T FromJson<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }

            T instance = JsonConvert.DeserializeObject<T>(json);
            return instance;
        }

        public static List<T> FromJsonList<T>(string json)
        { 
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<T>();
            }
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ArraySegment<byte> SerializeString(string value)
        {
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes((string)value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToByte<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<T>(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T FromByte<T>(byte[] data)
        {
            
            T obj = default(T);
            if (data.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    obj = Serializer.Deserialize<T>(ms);
                }
            }
            return obj;
        }

        /// <summary>
        /// XML序列化成字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string XmlSerialiaze<T>(T obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringBuilder sbr = new StringBuilder();
            using (TextWriter wr = new StringWriter(sbr))
            {
                xs.Serialize(wr, obj);
                wr.Flush();
                wr.Close();
            }

            return sbr.ToString();
        }

        /// <summary>
        /// XML反序列化路径
        /// </summary>
        /// <param name="xmlStr">XML序列化的字符串</param>
        /// <returns>反序列化的对象</returns>
        public static T XmlDeserialize<T>(string xmlStr)
        {
            using (TextReader reader = new StringReader(xmlStr))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    return (T)xs.Deserialize(reader);
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// 序列化到二进制文件
        /// </summary>
        /// <returns></returns>
        public static void ToBinFile(object obj, string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        /// <summary>
        /// 从二进制文件反序列化
        /// </summary>
        public static T FromBinFile<T>(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            T obj = (T)formatter.Deserialize(stream);
            stream.Close();
            return obj;
        }
    }
}
