using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.WpfEmulator.Entity;
using Newtonsoft.Json;
using WpfEmulator;

namespace Games.NBall.WpfEmulator.Core
{
    public class EmulatorHelper
    {
        public static readonly string TestAccount = "Dte3%)@%L.,jU(ieQ";
        private static readonly string _configPath = AppDomain.CurrentDomain.BaseDirectory + "Config\\";
        private static readonly string _clientDataPath = AppDomain.CurrentDomain.BaseDirectory + "ClientData\\";

        public static readonly string MessageFileName = "Message.txt";
        public static readonly string FormationFileName = "Formation.txt";
        public static readonly string RequestConfigFileName = "RequestConfig.txt";
        public static readonly string RequestConfigDebugFileName = "RequestConfig_Debug.txt";
        public static readonly string ApiConfigFileName = "ApiConfig.xml";
        public static readonly string ItemtipFileName = "ItemTip.txt";
        public static readonly string SkilltipFileName = "SkillTip.txt";
        public static readonly string DescriptionDicFileName = "DescriptionDic.txt";
        public static readonly string DescriptionDicFileName1 = "DescriptionDic1.txt";
        public static readonly string DescriptionPlayerKpi = "DescriptionPlayerKpi.txt";
        public static readonly string DescriptionDicDebugFileName = "DescriptionDic_Debug.txt";
        public static readonly string DescriptionDicDebugFileName1 = "DescriptionDic_Debug1.txt";
        public static readonly string NameLibraryFileName = "NameLibrary.txt";
        public static readonly string NpcDicFileName = "NpcDic.txt";
        public static readonly string NpcDataFileName = "NpcData.txt";
        private static readonly DateTime _createTime = Convert.ToDateTime("2013-1-1 00:00:00." + ConfigurationManager.AppSettings["StaticDataVersion"]);
        public static readonly string PlayertipFileName = "PlayerTip.txt";
        public delegate void VoidAction();

        #region Config
        public static string SaveXml<T>(T t, string configFileName)
        {
            try
            {
                if (!Directory.Exists(_configPath))
                {
                    Directory.CreateDirectory(_configPath);
                }
                var xmlpath = _configPath + configFileName;
                if (File.Exists(xmlpath))
                {
                    File.Delete(xmlpath);
                }
                using (FileStream stream = new FileStream(xmlpath, FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stream, t);
                    return xmlpath;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "";
            }
        }

        public static T LoadXml<T>(string configFileName)
        {
            try
            {
                if (!Directory.Exists(_configPath))
                {
                    Directory.CreateDirectory(_configPath);
                }
                var xmlpath = _configPath + configFileName;
                if (File.Exists(xmlpath))
                {

                    using (FileStream stream = new FileStream(xmlpath, FileMode.Open))
                    {
                        using (XmlReader reader = XmlReader.Create(stream))
                        {
                            var serializer = new XmlSerializer(typeof(T));
                            return (T)serializer.Deserialize(reader);
                        }
                    }
                }
                return default(T);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                throw ex;
            }
        }
        #endregion

        #region ClientData
        public static string SaveConfig<T>(T t, string configFileName, Dictionary<string, Dictionary<string, string>> summary=null)
        {
            try
            {
                if (!Directory.Exists(_clientDataPath))
                {
                    Directory.CreateDirectory(_clientDataPath);
                }
                var xmlpath = _clientDataPath + configFileName;
                if (File.Exists(xmlpath))
                {
                    File.Delete(xmlpath);
                }
                //using (FileStream stream = new FileStream(xmlpath, FileMode.Create))
                //{
                    //var serializer = new XmlSerializer(typeof(T));
                    //serializer.Serialize(stream, t);
                //    return xmlpath;
                //}

                using (StreamWriter stream = new StreamWriter(xmlpath, false))
                {
                    JsonConvert.EntitySummaryDic = summary;
                    stream.Write(JsonConvert.SerializeObject(t));
                }
                File.SetCreationTime(xmlpath, _createTime);
                return xmlpath;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "";
            }
        }
      

        public static T LoadConfig<T>(string configFileName)
        {
            try
            {
                if (!Directory.Exists(_clientDataPath))
                {
                    Directory.CreateDirectory(_clientDataPath);
                }
                var xmlpath = _clientDataPath + configFileName;
                if (File.Exists(xmlpath))
                {
                    FileInfo fileInfo = new FileInfo(xmlpath);
                    if (fileInfo.CreationTime != _createTime)
                        return default(T);
                    //using (FileStream stream = new FileStream(xmlpath, FileMode.Open))
                    //{
                        //using (XmlReader reader = XmlReader.Create(stream))
                        //{
                        //    var serializer = new XmlSerializer(typeof(T));
                        //    return (T)serializer.Deserialize(reader);
                        //}
                    //}
                    using (StreamReader reader = new StreamReader(xmlpath))
                    {
                        string s = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(s);
                    }
                
                }
                return default(T);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                throw ex;
            }
        }

        public static void InitATestRequestConfig()
        {
           
        }
        #endregion

        #region BuildMessage

        public static string BuildErrorinfo(MessageCode code)
        {
            return BuildErrorinfo("", code);
        }

        /// <summary>
        /// 构造消息
        /// eg: [memo]message(code:code)
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string BuildErrorinfo(string memo, MessageCode code)
        {
            var msg = GetMessage(code);
            if(!string.IsNullOrEmpty(memo))
                return string.Format("[{2}]{0}(code:{1}).", msg, (int)code,memo);
            return string.Format("{0}(code:{1}).", msg, (int) code);
        }

        public static string BuildErrorinfo( int code)
        {
            return BuildErrorinfo("", (MessageCode) code);
        }

        public static string BuildErrorinfo(string memo, int code)
        {
            return BuildErrorinfo(memo, (MessageCode) code);
        }

        public static string GetMessage(MessageCode code)
        {
            return GetMessage((int) code);
        }

        public static string GetMessage(int code)
        {
            string s = "";
            CacheHelper.Instance.MessageCodeDic.TryGetValue(code, out s);
            return s;
        }
        #endregion

        public static string GetCurrencyTypeName(int currencyType)
        {
            return GetMessageFromDic(CacheHelper.Instance.MessageCurrencyTypeDic, currencyType);
        }

        public static string GetMessageFromDic(Dictionary<int, string> dic, int key)
        {
            string s = "";
            dic.TryGetValue(key, out s);
            return s;
        }
        #region Position
        public static string GetPositionStrByPid(int pid)
        {
           
                return "未知";
        }

        public static string GetPositionStr(int position)
        {
            return GetMessageFromDic(CacheHelper.Instance.MessagePositionDic, position);
        }

        public static string GetPositionAbbreviation(int position)
        {
            switch (position)
            {
                case (int)EnumPosition.Goalkeeper:
                    return "GK";
                case (int)EnumPosition.Fullback:
                    return "CB";
                case (int)EnumPosition.Midfielder:
                    return "CF";
                case (int)EnumPosition.Forward:
                    return "AMF";
            }
            return "";
        }
        #endregion

        #region Property
        public static string GetPropertyStr(int property)
        {
            return GetMessageFromDic(CacheHelper.Instance.MessagePropertyDic, property);
        }
        #endregion

        #region ItemType
        public static string GetItemTypeStr(int itemtype)
        {
            return GetMessageFromDic(CacheHelper.Instance.MessageItemTypeDic, itemtype);
        }


        public static string GetItemSubTypeStr(EnumItemType itemType, int subType)
        {
            return GetItemSubTypeStr((int) itemType, subType);
        }

        public static string GetItemSubTypeStr(int itemType, int subType)
        {
            string des = "";
            switch (itemType)
            {
                case (int)EnumItemType.PlayerCard:
                    des = GetMessageFromDic(CacheHelper.Instance.MessagePlayerCardLevelDic,subType);
                    break;
                case (int)EnumItemType.Equipment:
                    des = GetMessageFromDic(CacheHelper.Instance.MessageEquipmentQuarityDic, subType);
                    break;
            }
            return des;
        }
        #endregion

        #region BuildPropertyPlus
        public static string BuildPropertyPlus(PropertyPlusEntity plusEntity)
        {
            string pName = EmulatorHelper.GetPropertyStr(plusEntity.PropertyId);
            if (plusEntity.PlusType == (int)EnumPlusType.Abs)
            {
                return string.Format("{0} +{1}", pName, plusEntity.PlusValue);
            }
            return string.Format("{0} +{1}%", pName, plusEntity.PlusValue);
        }
        #endregion

        #region GetItemName

       


        public static string GetItemName(int itemCode,int itemType)
        {
            string name = "";
            switch (itemType)
            {
                
            }
            return name;
        }


        public static string GetItemNameByLink(int linkId, int itemType)
        {
            string name = "";
           
            return name;
        }

        public static int GetPlayerPosition(int pid)
        {
           
            return 0;
        }

        public static string GetPlayerName(int pid)
        {
            
            return "";
        }

      


        public static string GetTalentName(string skillCode)
        {
            if (string.IsNullOrEmpty(skillCode))
                return "";
            if (CacheHelper.Instance.Talent.ContainsKey(skillCode))
            {
                return CacheHelper.Instance.Talent[skillCode].SkillName;
            }
            return skillCode;
        }

        public static string GetWillName(string skillCode)
        {
            if (string.IsNullOrEmpty(skillCode))
                return "";
            if (CacheHelper.Instance.Will.ContainsKey(skillCode))
            {
                return CacheHelper.Instance.Will[skillCode].SkillName;
            }
            return skillCode;
        }

        public static string GetTalentNames(string skillCodes)
        {
            if (string.IsNullOrEmpty(skillCodes))
                return "";
            var ss = skillCodes.Split(',');
            string r = "";
            foreach (var s in ss)
            {
                r += GetTalentName(s) + ",";
            }
            return r.TrimEnd(',');
        }

        public static string GetWillNames(string skillCodes)
        {
            if (string.IsNullOrEmpty(skillCodes))
                return "";
            var ss = skillCodes.Split(',');
            string r = "";
            foreach (var s in ss)
            {
                r += GetWillName(s)+",";
            }
            return r.TrimEnd(',');
        }

        public static string GetManagerSkillNames(string skillCodes)
        {
            if (string.IsNullOrEmpty(skillCodes))
                return "";
            var s = GetWillNames(skillCodes);
            return GetTalentNames(s);
        }
        #endregion

        #region GetDescription
        #endregion

        public static SolidColorBrush GetCardColor(int subType)
        {
            switch (subType)
            {
                case 1:
                    return new SolidColorBrush(Colors.Gold);
                     
                case 2:
                    return new SolidColorBrush(Colors.DarkOrange);
                     
                case 3:
                    return new SolidColorBrush(Colors.Purple);
                     
                case 4:
                    return new SolidColorBrush(Colors.Blue);
                     
                case 5:
                    return new SolidColorBrush(Colors.Green);
                     
                default:
                    return new SolidColorBrush(Colors.White);
                    
            }
        }

        public static bool IsChecked(CheckBox chk)
        {
            return chk.IsChecked.HasValue && chk.IsChecked.Value;
        }

        public static bool IsChecked(RadioButton chk)
        {
            return chk.IsChecked.HasValue && chk.IsChecked.Value;
        }
    }
}
