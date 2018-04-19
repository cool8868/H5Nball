using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Item;
using Games.NBall.WpfEmulator.Entity;
using Newtonsoft.Json;
using WpfEmulator;

namespace Games.NBall.WpfEmulator.Core
{
    public class RequestHelper
    {
        private static Cookie _formCookie;
        //private static string _serverUrl = "http://localhost:9000/";
        private static string _serverUrl;
        private static string _chatWebUrl = ConfigurationManager.AppSettings["ChatWebUrl"];
        private static string _chatChannelId = ConfigurationManager.AppSettings["ChatChannelId"];
        public static string _cookie;

        #region Facade
        public static void Initialize()
        {
            var isLocal = ConfigurationManager.AppSettings["IsDev"];
            if (isLocal == "1")
            {
                _serverUrl = "http://localhost:8033/";
            }
            else if (isLocal == "2")
            {
                _serverUrl = "http://180.150.178.193/";
            }
            else if (isLocal == "99")
            {
                _serverUrl = "http://u17s1.dingpamao.net/";
            }
            else if (isLocal == "100")
            {
                _serverUrl = "http://renrens1.dingpamao.net/";
            }
            else
            {
                _serverUrl = "http://180.150.178.193:" + isLocal + "/";
            }
        }

        public static T Request<T>(string module, string action)
        {
            return Request<T>(module, action, null);
        }

        public static T Request<T>(string module, string action, WpfRequestParameter requestParameter)
        {
            string responseText= Request(module,action,requestParameter);
            if (typeof (T).Name == "ItemPackageDataResponse")
            {
                var jsonEntity =JsonConvert.DeserializeObject<T>(responseText);

                var entity = jsonEntity as ItemPackageDataResponse;
                if (entity.Code == (int) MessageCode.Success)
                {
                    BuildPackageData(entity.Data, responseText);
                }
                return jsonEntity;
            }
            else if (typeof(T).Name == "StrengthResponse")
            {
                var jsonEntity = JsonConvert.DeserializeObject<T>(responseText);
                var entity = jsonEntity as StrengthResponse;
                if (entity.Code == (int)MessageCode.Success)
                {
                    BuildPackageData(entity.Data.Package,responseText);
                }
                return jsonEntity;
            }
            else if (typeof(T).Name == "SynthesisResponse")
            {
                var jsonEntity = JsonConvert.DeserializeObject<T>(responseText);
                var entity = jsonEntity as SynthesisResponse;
                if (entity.Code == (int)MessageCode.Success)
                {
                    BuildPackageData(entity.Data.Package, responseText);
                }
                return jsonEntity;
            }
            else if (typeof(T).Name == "DecomposeResponse")
            {
                var jsonEntity = JsonConvert.DeserializeObject<T>(responseText);
                var entity = jsonEntity as DecomposeResponse;
                if (entity.Code == (int)MessageCode.Success)
                {
                    BuildPackageData(entity.Data.Package, responseText);
                }
                return jsonEntity;
            }
            else if (typeof (T).Name == "MailDataResponse")
            {
                var jsonEntity = JsonConvert.DeserializeObject<T>(responseText);
                var entity = jsonEntity as MailDataResponse;
                if (entity.Code == (int)MessageCode.Success)
                {
                    BuildAttachmentData(entity.Data, responseText);
                }
                return jsonEntity;
            }
            else if (typeof(T).Name == "EquipmentActionResponse")
            {
                var jsonEntity = JsonConvert.DeserializeObject<T>(responseText);
                var entity = jsonEntity as EquipmentActionResponse;
                if (entity.Code == (int)MessageCode.Success)
                {
                    BuildEquipmentData(entity.Data.ItemInfo, responseText);
                }
                return jsonEntity;
            }
            else if (typeof (T).Name == "LoginResult")
            {
                var jsonEntity = JsonConvert.DeserializeObject<T>(responseText);
                var entity = jsonEntity as LoginResult;
                RequestHelper._cookie = entity.Cookie;
                return jsonEntity;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(responseText);
            }
        }

        public static void BuildPackageData(ItemPackageData entity, string itemString)
        {
            if (entity.Items.Count <= 0)
            {
                return;
            }
            int start = 0;
            int end = 0;
            foreach (var item in entity.Items)
            {
                start = itemString.IndexOf("ItemProperty") + 14;
                end = itemString.IndexOf("GridIndex") - 2;
                var ss = itemString.Substring(start, end - start);
                switch (item.ItemType)
                {
                    case (int)EnumItemType.PlayerCard:
                        item.ItemProperty = JsonConvert.DeserializeObject<PlayerCardProperty>(ss);
                        break;
                    case (int)EnumItemType.Equipment:
                        item.ItemProperty = JsonConvert.DeserializeObject<EquipmentProperty>(ss);
                        break;
                    case (int)EnumItemType.MallItem:
                        item.ItemProperty = JsonConvert.DeserializeObject<MallItemProperty>(ss);
                        break;
               
                }
                itemString = itemString.Substring(end + 9);
            }
        }

        public static void BuildEquipmentData(ItemInfoEntity entity, string itemString)
        {
            if (entity==null)
            {
                return;
            }
            int start = 0;
            int end = 0;

            start = itemString.IndexOf("ItemProperty") + 14;
            end = itemString.IndexOf("GridIndex") - 2;
            var ss = itemString.Substring(start, end - start);
            entity.ItemProperty = JsonConvert.DeserializeObject<EquipmentProperty>(ss);
        }

        public static void BuildAttachmentData(MailDataEntity entity, string itemString)
        {
            if (entity.Mails.Count <= 0)
            {
                return;
            }
            int start = 0;
            int end = 0;
            foreach (var item in entity.Mails)
            {
                if(item.MailAttachment==null || item.MailAttachment.Attachments==null)
                    continue;
                int count = item.MailAttachment.Attachments.Count;
                item.MailAttachment.Attachments=new List<AttachmentEntity>(count);
                start = itemString.IndexOf("Attachments") + 14;
                itemString = itemString.Substring(start);
                for (int i = 0; i < count; i++)
                {
                    //{"AttachmentType":2,"Count":150},{"AttachmentType":1,"Count":150}
                    start = itemString.IndexOf("AttachmentType")+16;
                    var iType =Convert.ToInt32(GetSS(itemString, start, ",",out end));

                    start = itemString.IndexOf("{");
                    end = itemString.IndexOf("}")+1;
                    var ss = itemString.Substring(start,end);
                    switch (iType)
                    {
                        case (int)EnumAttachmentType.Coin:
                            item.MailAttachment.Attachments.Add(JsonConvert.DeserializeObject<AttachmentEntity>(ss));
                            break;
                        case (int)EnumAttachmentType.Point:
                            item.MailAttachment.Attachments.Add(JsonConvert.DeserializeObject<AttachmentEntity>(ss));
                            break;
                        case (int)EnumAttachmentType.NewItem:
                            item.MailAttachment.Attachments.Add(JsonConvert.DeserializeObject<AttachmentItemEntity>(ss));
                            break;
                        case (int)EnumAttachmentType.UsedPlayerCard:
                            var propertyString = GetAttachmentProperty(itemString, out end);
                            var attachmentCount = GetAttachmentCount(itemString, end);
                            end = itemString.IndexOf("\"Count\"") + 8 + attachmentCount.Length+1;
                            ss = itemString.Substring(start, end - start);
                            var usedItem = JsonConvert.DeserializeObject<AttachmentUsedItemEntity>(ss);
                            usedItem.ItemProperty = JsonConvert.DeserializeObject<PlayerCardUsedEntity>(propertyString);
                            item.MailAttachment.Attachments.Add(usedItem);
                            break;
                        case (int)EnumAttachmentType.UsedEquipment:
                            var propertyString1 = GetAttachmentProperty(itemString, out end);
                            var attachmentCount1 = GetAttachmentCount(itemString, end);
                            end = itemString.IndexOf("\"Count\"") + 8 + attachmentCount1.Length+1;
                            ss = itemString.Substring(start, end - start);
                            var usedItem1 = JsonConvert.DeserializeObject<AttachmentUsedItemEntity>(ss);
                            usedItem1.ItemProperty = JsonConvert.DeserializeObject<EquipmentUsedEntity>(propertyString1);
                            item.MailAttachment.Attachments.Add(usedItem1);
                            break;
                        case (int)EnumAttachmentType.Equipment:
                            var propertyString2 = GetEquipmentProperty(itemString, out end);
                            end = itemString.IndexOf("AttachmentType") - 2;
                            var attachmentCount2 = GetAttachmentCount(itemString, end);
                            end = itemString.IndexOf("\"Count\"") + 8 + attachmentCount2.Length + 1;
                            ss = itemString.Substring(start, end - start);
                            var equipmentItem = JsonConvert.DeserializeObject<AttachmentEquipmentEntity>(ss);
                            equipmentItem.EquipmentProperty = JsonConvert.DeserializeObject<EquipmentProperty>(propertyString2);
                            item.MailAttachment.Attachments.Add(equipmentItem);
                            break;
                    }
                    itemString = itemString.Substring(end + 1);
                }
            }
        }



        static string GetAttachmentCount(string itemString,int end)
        {
            var temp = itemString.Substring(end);
            var start = temp.IndexOf("Count") + 7;
            end = temp.IndexOf("}");
            return temp.Substring(start, end - start);
        }

        static string GetEquipmentProperty(string itemString, out int end)
        {
            var start = itemString.IndexOf("EquipmentProperty") + 19;
            end = itemString.IndexOf("ItemCode") - 2;
            return itemString.Substring(start, end - start);
        }

        static string GetAttachmentProperty(string itemString,out int end)
        {
            var start = itemString.IndexOf("ItemProperty") + 14;
            end = itemString.IndexOf("AttachmentType") -2;
            return itemString.Substring(start, end-start);
        }

        static string GetSS(string itemString, int start,string endChar,out int endIndex)
        {
            var ss = itemString.Substring(start);
            endIndex = ss.IndexOf(endChar);
            return ss.Substring(0, endIndex);
        }

        public static string Request(string module, string action)
        {
            return Request(module, action, null);
        }

        public static string Request(string module, string action, WpfRequestParameter requestParameter)
        {
            string parameter = "";
            if(requestParameter!=null) 
                parameter=requestParameter.ToString();
            string requestMethod = string.Format("{0}{1}.do?action={2}&{3}&ck={4}", _serverUrl, module, action, parameter, _cookie);
            requestMethod = requestMethod.TrimEnd('&');
            return Request(requestMethod);
        }

        public static string Request(string serverUrl, string module, string action, string parameters)
        {
            string requestMethod = string.Format("{0}{1}.do?action={2}&{3}&ck={4}", serverUrl, module, action, parameters,_cookie);
            return Request(requestMethod);
        }

        public static string RequestPassport(string account)
        {
            string requestMethod = _serverUrl+"Passport.aspx?account=" + account;
            return Request(requestMethod);
        }

        public static string RequestPassport(string serveUrl, string account)
        {
            string requestMethod = serveUrl + "Passport.aspx?account=" + account;
            return Request(requestMethod);
        }

        public static string RequestChat(Guid managerId,string msg)
        {
            string requestMethod =string.Format("{0}Post_ChatSendMessage.aspx?channelId={1}&ManagerId={2}&Msg={3}", _chatWebUrl , _chatChannelId,managerId,msg);
            return Request(requestMethod);
        }

        public static byte[] GetProcess(string module, string action, WpfRequestParameter requestParameter,out int code)
        {
            code = -1;
            byte[] arraryByte = null;
            string parameter = "";
            if (requestParameter != null)
                parameter = requestParameter.ToString();
            string requestMethod = string.Format("{0}{1}.do?action={2}&{3}", _serverUrl, module, action, parameter);
            requestMethod = requestMethod.TrimEnd('&');
            DateTime requestTime = DateTime.Now;
            Stopwatch watch= new Stopwatch();
            watch.Start();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestMethod);
                //request.Timeout = 5000;
                request.Method = "GET";
                request.CookieContainer = new CookieContainer();
                if (_formCookie != null)
                    request.CookieContainer.Add(_formCookie);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                var len = response.ContentLength;
                if (len < 40)
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    var rcode = reader.ReadToEnd();
                    reader.Close();
                    var x = JsonConvert.DeserializeObject<MessageCodeResponse>(rcode);
                    code = x.Code;
                }
                else
                {
                    MemoryStream stmMemory = new MemoryStream();
                    byte[] buffer = new byte[len];
                    int i;
                    while ((i = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stmMemory.Write(buffer, 0, i);
                    }
                    arraryByte = stmMemory.ToArray();
                    code = 0;
                    stmMemory.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ApiTestCore.MainWindow.RequestLog(requestMethod, requestTime);
                ApiTestCore.MainWindow.ResponseLog("get match cause exception:"+  ex.Message,DateTime.Now,0);
                code = -1;
                return null;
            }

            var totalTime = watch.ElapsedMilliseconds;
            watch.Stop();
            ApiTestCore.MainWindow.RequestLog(requestMethod,requestTime);
            ApiTestCore.MainWindow.ResponseLog("get match,code("+code+")", requestTime, totalTime);
            return arraryByte;
        }
        #endregion

        #region encapsulation

        static string Request(string requestMethod,bool needLog=true)
        {
            string responseText = "";
            DateTime requestTime = DateTime.Now;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestMethod);
                //request.Timeout = 5000;
                request.Method = "GET";
                request.CookieContainer = new CookieContainer();
                if (_formCookie!=null)
                    request.CookieContainer.Add(_formCookie);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                if (response.Cookies["h5nb_form"] != null)
                    _formCookie = response.Cookies["h5nb_form"];
                

                responseText = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                responseText = "{\"Code\":-1,\"Message\":\"cause exception," + ex.Message + "\"}";
            }
            var totalTime = watch.ElapsedMilliseconds;
            watch.Stop();

                ApiTestCore.MainWindow.RequestLog(requestMethod, requestTime);
                ApiTestCore.MainWindow.ResponseLog(responseText, DateTime.Now, totalTime);
            
            return responseText;
        }


        static string BuildParameter(Dictionary<string, string> pDictionary)
        {
            string s = "";
            if (pDictionary != null)
            {
                foreach (var p in pDictionary)
                {
                    s = s+ p.Key + "=" + p.Value + "&";
                }
                s = s.TrimEnd('&');
            }
            return s;
        }
        #endregion
    }
}
