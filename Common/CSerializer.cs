using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Common
{
    public class CSerializer
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static string SerializeToString<T>(T t)
        {
            MemoryStream ms = new MemoryStream();
            System.Xml.XmlWriterSettings xws = new System.Xml.XmlWriterSettings();
            xws.Encoding = new UTF8Encoding(false);//Encoding.UTF8;// 
            xws.Indent = true;

            using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(ms, xws))
            {
                System.Xml.Serialization.XmlSerializer xs =
                    new System.Xml.Serialization.XmlSerializer(t.GetType());
                xs.Serialize(xw, t);

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="s">对象序列化后的Xml字符串</param>
        /// <returns></returns>
        public static T DeserializeFromString<T>(string s)
        {
            using (StringReader sr = new StringReader(s))
            {
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static void SerializeToFile<T>(T t, string fileName)
        {
            using (TextWriter tw = new StreamWriter(fileName))
            {
                System.Xml.Serialization.XmlSerializer xz =
                    new System.Xml.Serialization.XmlSerializer(t.GetType());
                xz.Serialize(tw, t);
                tw.Close();
            }
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeFromFile<T>(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(T));
                T t = (T)serializer.Deserialize(fs);
                fs.Close();

                return t;
            }
        }
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static string SerializeObj<T>(T t)
        {
            IFormatter obj = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            obj.Serialize(ms, t);

            BinaryReader br = new BinaryReader(ms);
            ms.Position = 0;
            byte[] bs = br.ReadBytes((int)ms.Length);
            ms.Close();
            return Convert.ToBase64String(bs);
        }

        /// <summary>
        /// Serializes the obj to byte.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        public static byte[] SerializeObjToByte<T>(T t)
        {
            byte[] returnValue = null;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter obj = new BinaryFormatter();
                obj.Serialize(ms, t);
                returnValue = ms.GetBuffer();
                ms.Close();
            }
            return returnValue;
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="s">对象序列化后的Xml字符串</param>
        /// <returns></returns>
        public static T DeserializeObj<T>(string str)
        {
            IFormatter obj = new BinaryFormatter();
            byte[] bs = Convert.FromBase64String(str);
            MemoryStream ms = new MemoryStream();
            ms.Write(bs, 0, bs.Length);
            ms.Position = 0;
            T t = (T)obj.Deserialize(ms);
            ms.Close();
            return t;
        }


    }
}
