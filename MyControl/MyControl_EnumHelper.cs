using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;

namespace Games.MyControl
{
    public class CacheDataHelper
    {
        #region Cache

        private static Dictionary<string, List<StatusList>> _enumDic;
        private Dictionary<string, string> _responseNameDic;

        #endregion

        #region Singleton
        private static CacheDataHelper _instance;
        private static object _lockObj = new object();
        public static CacheDataHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheDataHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region .ctor
        public CacheDataHelper()
        {
            InitCache();
        }
        #endregion

        public List<StatusList> GetEnumData(string name)
        {
            var key = name.ToLower();
            if (_enumDic.ContainsKey(key))
                return _enumDic[key];
            else
            {
                List<StatusList> list = null;
                if (key == "app")
                {
                    list = BuildTableData("Select * From All_App");
                }
                else if (key == "logfunction")
                {
                    list = BuildTableData("Select * From All_LogFunction");
                }
                else if (key == "itemname")
                {
                    list = BuildTableData("Select ItemCode As Idx,ItemName As Name From Dic_Item");
                }
                else
                {
                    list = BuildEnumData(name);
                }
                _enumDic.Add(key, list);
                return list;
            }
        }

        void InitCache()
        {
            _enumDic = new Dictionary<string, List<StatusList>>();
            BuildResponseNameDic();
        }

        public string GetEnumDescription(string name, int key)
        {
            return GetEnumDescription(name, key.ToString());
        }

        public string GetEnumDescription(string name,string key)
        {
            var list = GetEnumData(name);
            if (list != null)
            {
                var entity = list.Find(d => d.Value == key);
                if (entity != null)
                    return entity.Text;
            }
            return key;
        }

        #region ExportEnumData
        string GetConfigDBConnection()
        {
            return ConnectionFactory.Instance.GetConnectionString("Share", "Config");
        }

        List<StatusList> BuildTableData(string sql)
        {
            var appDataSet = MyControl_SqlHelper.ExecuteDataset(GetConfigDBConnection(), CommandType.Text, sql);
            if (appDataSet != null && appDataSet.Tables.Count > 0)
            {
                List<StatusList> list = new List<StatusList>();
                foreach (DataRow row in appDataSet.Tables[0].Rows)
                {
                    list.Add(new StatusList(row["Idx"].ToString(), row["Name"].ToString()));
                }
                return list;
            }
            return new List<StatusList>(0);
        }

        List<StatusList> BuildEnumData(string name)
        {
            string fullName = GetFullName(name, _responseNameDic);
            if (!string.IsNullOrEmpty(fullName))
            {
                Assembly assembly = Assembly.Load("Games.NBall.Entity");
                Type type = assembly.GetType(fullName);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                var fullpath = System.Web.HttpContext.Current.Server.MapPath("\\Games.NBall.Entity.XML");
                XmlNodeList xmlNodeListByXpath = XMLHelper.GetXmlNodeListByXpath(fullpath, "doc/members/member");
                if ((xmlNodeListByXpath != null) && (xmlNodeListByXpath.Count > 0))
                {
                    foreach (XmlNode node in xmlNodeListByXpath)
                    {
                        string key = node.Attributes["name"].Value;

                        if (key.StartsWith("F:" + type.FullName))
                        {
                            key = key.Replace("F:" + type.FullName + ".", "");

                            XmlNode node2 = null;
                            string str3 = "";
                            if (node.ChildNodes.Count > 0)
                            {
                                node2 = node.ChildNodes[0];
                                if ((node2 != null) && (node2.ChildNodes.Count > 0))
                                {
                                    node2 = node2.ChildNodes[0];
                                }
                            }
                            if ((node2 != null) && (node2.Value != null))
                            {
                                str3 = node2.Value.Replace("\r\n", "").Replace(" ", "");
                            }
                            dictionary.Add(key, str3);
                        }
                    }
                }
                List<StatusList> list2 = new List<StatusList>();
                foreach (FieldInfo info in type.GetFields())
                {
                    if (info.FieldType == type)
                    {
                        StatusList item = new StatusList();
                        item.Text = info.Name;

                        if (dictionary.ContainsKey(item.Text))
                        {
                            item.Text = dictionary[item.Text];
                        }
                        else
                        {
                            item.Text = "";
                        }
                        item.Value = ((int)(Enum.Parse(type, info.Name))).ToString();
                        list2.Add(item);
                    }
                }
                return list2;
            }
            return null;
        }

        void BuildResponseNameDic()
        {
            _responseNameDic = new Dictionary<string, string>();

                Assembly assembly = Assembly.Load("Games.NBall.Entity");

                
                foreach (var exportedType in assembly.ExportedTypes)
                {
                    string key = "";
                    if (exportedType.GenericTypeArguments.Length == 1)
                        key = exportedType.Name + "[" + exportedType.GenericTypeArguments[0].Name + "]";

                    else
                        key = exportedType.Name;
                    if (!_responseNameDic.ContainsKey(key))
                        _responseNameDic.Add(key, exportedType.FullName);
                }
            
        }
        string GetFullName(string cfgName,Dictionary<string, string> dicNames)
        {
            string fullName = "";
            if (!cfgName.EndsWith("]"))
            {
                if (dicNames.TryGetValue(cfgName, out fullName))
                    return fullName;
            }
            var spilts = cfgName.TrimEnd(']').Split('[');
            if (spilts.Length != 2)
                return "";
            string a, b;
            if (!dicNames.TryGetValue(spilts[0], out a) || !dicNames.TryGetValue(spilts[1], out b))
                return "";
            fullName = string.Concat(a, "[", b, "]");
            return fullName;
        }
        #endregion
    }
}
