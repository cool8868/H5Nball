using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Core
{
    public class GenerateASFile
    {
        private static readonly string _clientDataPath = AppDomain.CurrentDomain.BaseDirectory + "ClientData\\";

        

        /// <summary>
        /// 获得指定类型的所有公有属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertyInfo(Type type)
        {
            var properties = type.GetProperties();
              
            return properties;
        }

        /// <summary>
        /// 获得属性泛型类型的类型实参
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public static Type GetPropertyType(Type type, string keyname)
        {
            var properties = GetPropertyInfo(type);
            Type entityType = null;
            foreach (var propertyinfo in properties)
            {
                if (propertyinfo.Name == keyname)
                {
                    if (propertyinfo.PropertyType.IsGenericType)
                    {
                        entityType = propertyinfo.PropertyType.GetGenericArguments()[0];
                        break;
                    }
                    else if (propertyinfo.PropertyType.IsClass && propertyinfo.PropertyType != typeof(string))
                    {
                        entityType = propertyinfo.PropertyType;
                        break;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return entityType;
        }


        /// <summary>
        /// 生成DBxxxData.as文件
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="dicName"></param>
        /// <param name="typeList"></param>
        public static void CreateFileDbData(PropertyInfo[] properties, string dicName, List<string> typeList)
        {
            StringBuilder headStr = new StringBuilder();
            headStr.AppendFormat("package com.wonderful.ball.database" + "\r\n"
                                                   + "{{" + "\r\n"
                                                   + "\t" + "public class DB{0}Data" + "\r\n"
                                                   + "\t" +"{{" + "\r\n", dicName);

            StringBuilder varStr = new StringBuilder();

            for (int i = 0; i < properties.Length; i++)
            {
                varStr.Append("\t\t" + "public var " + properties[i].Name + ":" + typeList[i] + ";" + "\r\n");
            }

            StringBuilder funcHeadStr = new StringBuilder();
            funcHeadStr.AppendFormat("\r\n\t\t" + "public function DB{0}Data($jsonObj:Object)" + "\r\n" + "\t\t" + "{{" + "\r\n", dicName);

            StringBuilder funcBodyStr = new StringBuilder();
            foreach (var pro in properties)
            {
                funcBodyStr.Append("\t\t\t" + pro.Name + "= $jsonObj." + pro.Name + ";\r\n");
            }

            headStr.Append(varStr);
            headStr.Append(funcHeadStr);
            headStr.Append(funcBodyStr);
            headStr.Append("\t\t}" + "\r\n" + "\t}" + "\r\n" + "}");


            try
            {
                if (!Directory.Exists(_clientDataPath))
                {
                    Directory.CreateDirectory(_clientDataPath);
                }
                var filename = "DB" + dicName + "Data.as";
                var path = _clientDataPath + filename;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                using (StreamWriter stream = new StreamWriter(path, false, Encoding.UTF8))
                {
                    stream.Write(headStr);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// 生成DBxxx.as文件
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="dicName"></param>
        public static void CreateFileDb(PropertyInfo[] properties, string dicName, string content)
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("package com.wonderful.ball.database" + "\r\n"
                                                   + "{{" + "\r\n"
                                                   + "\t" + "import com.wonderful.ball.utils.HashMap;" + "\r\n\t" + "public class DB{0}" + "\r\n"
                                                   + "\t" + "{{" + "\r\n", dicName);
            
            str.Append("\t\t" + "private static var _db:HashMap;" + "\r\n");

            str.Append("\t\t" + "private static function get db():HashMap" + "\r\n\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "if (_db == null)" + "\r\n");
            str.Append("\t\t\t\t" + "_db = DBGlobal.getDbAsHashmap(");

            List<string> paramList = GetParameterlist(content);
            StringBuilder param = new StringBuilder();
            foreach (var p in paramList)
            {
                param.Append("\"" + p + "\",");
            }
            param.Remove(param.ToString().LastIndexOf(','), 1);
            param.Append(");" + "\r\n");

            str.Append(param);
            str.Append("\t\t\t" + "return _db;" + "\r\n" + "\t\t" + "}" + "\r\n\r\n");

            str.Append("\t\t" + "public static function getData($id:int):DB" + dicName + "Data\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "if (!db.containsKey($id))" + "\r\n");
            str.Append("\t\t\t\t" + "return null;" + "\r\n");
            str.Append("\t\t\t" + "var data:* = _db.get($id);" + "\r\n");
            str.Append("\t\t\t" + "if (data is DB" + dicName + "Data)" + "\r\n");
            str.Append("\t\t\t\t" + "return data as DB" + dicName + "Data;" + "\r\n");
            str.Append("\t\t\t" + "var data2:DB" + dicName + "Data = new DB" + dicName + "Data(data);" + "\r\n");
            str.Append("\t\t\t" + "_db.put($id, data2);" + "\r\n");
            str.Append("\t\t\t" + "return data2;" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n" + "\t" + "}" + "\r\n" + "}");

            try
            {
                if (!Directory.Exists(_clientDataPath))
                {
                    Directory.CreateDirectory(_clientDataPath);
                }
                var filename = "DB" + dicName + ".as";
                var path = _clientDataPath + filename;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                using (StreamWriter stream = new StreamWriter(path, false, Encoding.UTF8))
                {
                    stream.Write(str);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static List<string> GetParameterlist(string content)
        {
            string[] param = content.Trim(' ').Split(',');
            return param.ToList();
        }
    }
}
