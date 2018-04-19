using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.PostResult;
using Games.NBall.Entity.Share;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.UAFacade;
using Games.NBall.UAFacade.UABll;
using Games.NBall.WebClient.Util;
using Games.NBall.WebClient.Weibo;
using Games.NBall.WebServerFacade;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NS_OpenApiV3;
using NS_SnsNetWork;

namespace Games.NBall.NB_Web.Command
{
    public class ManagerCommand : BaseCommand
    {
        public void Dispatch(string action)
        {
            if (action == "login")
            {
                string cookie = "";
                if (doLogin(ref cookie))
                {
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);
                    HttpContext.Current.Response.Write("{\"Code\":0,\"Cookie\":\"" + cookie + "\"}");
                    return;
                }
                else
                {
                    OutputHelper.Output(MessageCode.LoginNoUser);
                    return;
                }
            }
            if (action == "lg")
            {
                var code = Games.NBall.UAFacade.UAFactory.Instance.Adapter.doLoginNew();
                if (code == (int) MessageCode.Success)
                {
                    code = (int) MessageCode.Success;
                    string cookie = "";
                    if (doLoginNew(ref cookie))
                    {
                        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);
                        HttpContext.Current.Response.Write("{\"Code\":0,\"Cookie\":\"" + cookie + "\"}");
                        return;
                    }
                }
                OutputHelper.Output(code);
                return;
            }
            else if (action == "gur" || action == "gpl" || action == "gac")
            {
                if (action == "gur")
                {
                    var account = GetParam("ac");

                    var platform = GetParam("pf");
                    var longType = GetParam("longType");
                    var resp = UserLoginCore.Instance.GetUserLoginRecord(account, platform, longType);
                    OutputHelper.Output(resp);
                    return;
                }
                else if (action == "gpl")
                {
                    var platform = GetParam("pf");
                    var resp = UserLoginCore.Instance.GetAllZoneInfo(platform);
                    OutputHelper.Output(resp);
                    return;
                }
                else if (action == "gac")
                {
                    var platform = GetParam("pf");
                    var ac = AnnouncementCore.Instance.GetPlatformAnnouncement(platform);
                    OutputHelper.Output(ac);
                    return;
                }
            }
            else
            {
                if (action == "managerinfo"
                || action == "register" || action == "grs"
                || action == "se" || action == "un")
                {
                    if (ValidatorAccountOnly() == false)
                        return;
                }
                else
                {
                    if (Validator() == false)
                        return;
                }
            }
            switch (action)
            {
                case "register":
                    Register();
                    break;
                case "managerinfo":
                    ManagerInfo();
                    break;
                case "grs":
                    var response = reader.GetRegisterSolution();
                    OutputHelper.Output(response);
                    break;
                case "rs":
                    var response1 = reader.ResumeStamina(UserAccount.ManagerId);
                    OutputHelper.Output(response1);
                    break;

                case "md":
                    var managerId = GetParamGuid("m");
                    string siteId = GetParam("siteid");
                    if (!CheckParam(managerId))
                        return;
                    if (string.IsNullOrEmpty(siteId))
                    {
                        var md = reader.GetManagerDetailInfo(managerId, siteId);
                        if (md.Code == (int) MessageCode.Success)
                            md.Data.LadderRank = ladderClient.GetMyLadderRank(managerId);
                        OutputHelper.Output(md);
                    }
                    //else
                    //{
                    //    var md = CrossClient.GetManagerDetailInfo(managerId, siteId);
                    //    if (md.Code == (int)MessageCode.Success)
                    //        md.Data.LadderRank = WebServerHandler.GetMyLadderRank(siteId, managerId);
                    //    OutputHelper.Output(md);
                    //}
                    break;

                case "hl":
                    var managerId2 = GetParamGuid("m");
                    if (!CheckParam(managerId2))
                        return;
                    var hl = reader.GetManagerHonorList(managerId2);
                    OutputHelper.Output(hl);
                    break;
                case "ho":
                    onlineClient.RiseOnlineTime(UserAccount.ManagerId);
                    OutputHelper.Output(0);
                    break;
                    
                case "gf":
                    var gf = reader.GetFunctionList(UserAccount.ManagerId);
                    OutputHelper.Output(gf);
                    break;
                case "vi":
                    var vi = reader.GetVipData(UserAccount.ManagerId);
                    OutputHelper.Output(vi);
                    break;
                case "viat":
                    var viat = reader.DailyAttendVip(UserAccount.ManagerId);
                    OutputHelper.Output(viat);
                    break;
                case "ul":
                    var logo = GetParamInt("l");
                    if (!CheckParam(logo))
                        return;
                    //var itemId = GetParamGuid("ti");
                    //if (!CheckParam(itemId))
                    //    return;
                    var ul = reader.UpdateLogo(UserAccount.ManagerId, logo.ToString());
                    OutputHelper.Output(ul);
                    return;
                case "upl":
                    Upl();
                    break;
                case "se":
                    SelectManager();
                    break;
                case "un":
                    UpdateName();
                    break;
                case "ba":
                    BindAccount();
                    break;
                case "gs":
                    var gs = reader.GiftStamina(UserAccount.ManagerId);
                    OutputHelper.Output(gs);
                    break;
                case "dr":
                    DeleteRole();
                    break;
                case "ga":
                    GetManagerAllFunctionNumber();
                    break;

                case "gai": //签到信息
                    var gai = reader.GetDailyAttendanceInfo(UserAccount.ManagerId);
                    OutputHelper.Output(gai);
                    break;
                case "ap": //签到奖励
                    var ap = reader.AttendancePrize(UserAccount.ManagerId);
                    OutputHelper.Output(ap);
                    break;
                case"istxvip"://是否是玩吧达人
                    //var istxvip=reader.
                    break;
                case"egretInfo":
                    EgretInfo();
                    break;
                case "st":
                    var st = reader.ShareTask(UserAccount.ManagerId);
                    OutputHelper.Output(st);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly ManagerClient reader = new ManagerClient();
        private readonly OnlineClient onlineClient = new OnlineClient();
        private readonly LadderClient ladderClient = new LadderClient();

        private readonly ActivityClient activityClient = new ActivityClient();


        private void Upl()
        {
            var nickName = GetParam("n");
            if (string.IsNullOrEmpty(nickName))
            {
                OutputHelper.Output(MessageCode.RegisterRealNameValid);
                return;
            }
            if (!IsCN(nickName))
            {
                OutputHelper.Output(MessageCode.RegisterRealNameValid);
                return;
            }
            var cert = GetParam("c");
            if (string.IsNullOrEmpty(cert))
            {
                OutputHelper.Output(MessageCode.RegisterCertIdValid);
                return;
            }
            if (!isIdCard(cert))
            {
                OutputHelper.Output(MessageCode.RegisterCertIdValid);
                return;
            }
            var upl = reader.UpdateIndulgeInfo(UserAccount.Account, UserAccount.ManagerId, nickName, cert,
                GetBirthday(cert));
            OutputHelper.Output(upl);
        }

        private void BindAccount()
        {
            var bindCode = GetParamGuid("b");
            if (!CheckParam(bindCode))
                return;

            var ba = reader.BindAccount(bindCode, UserAccount.Account, UserAccount.Name, UserAccount.ManagerId);
            OutputHelper.Output(ba);
        }

        private void DeleteRole()
        {
            var bindCode = GetParamGuid("b");
            if (!CheckParam(bindCode))
                return;

            var dr = reader.DeleteRole(UserAccount.Account, bindCode);
            OutputHelper.Output(dr);
        }

        private void GetManagerAllFunctionNumber()
        {
            var dr = reader.GetManagerAllFunctionNumber(UserAccount.ManagerId);
            OutputHelper.Output(dr);
        }

        private void UpdateName()
        {
            //var mid = GetParamGuid("g");
            //if (!CheckParam(mid))
            //    return;
            //var oldName = GetParam("o");
            //if (!CheckParam(oldName))
            //    return;
            //var newName = GetParam("n");
            //if (!CheckParam(newName))
            //    return;
            //var un = reader.UpdateName(mid, oldName, newName);
            var name = GetParam("n");
            var un = reader.UpdateName(UserAccount.ManagerId, name);


            try
            {
               
                var manager = reader.GetManagerInfo(UserAccount.ManagerId,false);
                //if(ShareUtil.IsH5A8)
                //UA_A8.UserAction("createrole",manager.Data.Manager.Account,"","",manager.Data.Manager);

            }
            catch (Exception ex)
            {
                
            }
            OutputHelper.Output(un);
        }

        #region SelectManager

        private void SelectManager()
        {
            var managerId = GetParamGuid("g");
            if (!CheckParam(managerId))
                return;
            var info = reader.SelectManager(UserAccount.Account, managerId,GetIp(),
                UAFactory.Instance.IsTx);
            var sessionId = ShareUtil.GenerateComb().ToString();
            if (info.Code == (int) MessageCode.Success)
            {
                if (CheckLockState(info.Data.Manager.Idx))
                {
                    OutputHelper.Output(MessageCode.LoginOnlineLock);
                    return;
                }

                //写入登录信息
                var cookie = UAHelper.SetFormsAuthentication(UserAccount.Account, info.Data.Manager.Idx,
                    info.Data.Manager.Name, 1, sessionId);
                OnlineMgr.LoginSession(info.Data.Manager.Idx, sessionId);
                onlineClient.RiseOnlineTime(info.Data.Manager.Idx);
                info.Data.Cookie = cookie;
            }
            OutputHelper.Output(info);
        }

        #endregion

        #region ManagerInfo

        private void ManagerInfo()
        {
            if (UserAccount.ManagerId == Guid.Empty) //登录
            {
                var info = reader.GetManagerInfoByAccount(UserAccount.Account,GetIp(),
                    UAFactory.Instance.IsTx);

                var sessionId = ShareUtil.GenerateComb().ToString();
                if (info.Code == (int) MessageCode.Success)
                {
                    if (info.Data.NeedSelect == false)
                    {
                        if (info.Data.ManagerInfo == null)
                        {
                            string playerName = "";
                            string logo ="1";
                            int templateId = 1; 
                            var createData = reader.CreateManager(UserAccount.Account, playerName, logo, templateId,
                                UAHelper.GetRealIP());
                            if (createData.Code != (int)MessageCode.Success)
                            {
                                info.Code = createData.Code;
                                OutputHelper.Output(info);
                                return;
                            }
                            info = reader.GetManagerInfoByAccount(UserAccount.Account, GetIp(),
                                UAFactory.Instance.IsTx);
                        }

                        if (CheckLockState(info.Data.ManagerInfo.Manager.Idx))
                        {
                            OutputHelper.Output(MessageCode.LoginOnlineLock);
                            return;
                        }

                        //写入登录信息
                        string cookie = UAHelper.SetFormsAuthentication(UserAccount.Account,
                            info.Data.ManagerInfo.Manager.Idx,
                            info.Data.ManagerInfo.Manager.Name, 1, sessionId);
                        OnlineMgr.LoginSession(info.Data.ManagerInfo.Manager.Idx, sessionId);
                        onlineClient.RiseOnlineTime(info.Data.ManagerInfo.Manager.Idx);
                        info.Data.Cookie = cookie;
                    }
                }
                OutputHelper.Output(info);
            }
            else
            {
                if (CheckLockState(UserAccount.ManagerId))
                {
                    OutputHelper.Output(MessageCode.LoginOnlineLock);
                    return;
                }
                var info = reader.GetManagerInfo(UserAccount.ManagerId, UAFactory.Instance.IsTx);
                info.Data.Cookie = "";
                OutputHelper.Output(info);
            }

        }

        private bool CheckLockState(Guid managerId)
        {
            return OnlineCore.CheckLockState(managerId);
        }

        #endregion

        #region Register

        private void Register()
        {
            string playerName = GetParam("name");
            string logo = GetParam("logo");
            int templateId = 1; // GetParamInt("ti");
            if (string.IsNullOrEmpty(playerName))
            {
                OutputHelper.Output(MessageCode.RegisterNameIsEmpty);
                return;
            }

            var createData = reader.CreateManager(UserAccount.Account, playerName, logo, templateId,
                UAHelper.GetRealIP());
            if (createData.Code == (int) MessageCode.Success)
            {
                var sessionId = ShareUtil.GenerateComb().ToString();
                var cookie = UAHelper.SetFormsAuthentication(UserAccount.Account, createData.Data, playerName, 1, sessionId);
                OnlineMgr.LoginSession(createData.Data, sessionId);
                onlineClient.RiseOnlineTime(createData.Data);
            }
            OutputHelper.Output(createData);
        }

        #endregion
        

        static bool IsCN(string strInput)
        {
            Regex reg = new Regex("^[\u4e00-\u9fa5]+$");

            return reg.IsMatch(strInput);
        }
        public static bool isIdCard(string idStr)
        {
            string date = "", Ai = "";
            string verify = "10x98765432";
            int[] Wi = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            string[] area = { "", "", "", "", "", "", "", "", "", "", "", "北京", "天津", "河北", "山西", "内蒙古", "", "", "", "", "", "辽宁", "吉林", "黑龙江", "", "", "", "", "", "", "", "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", "", "", "", "河南", "湖北", "湖南", "广东", "广西", "海南", "", "", "", "重庆", "四川", "贵州", "云南", "西藏", "", "", "", "", "", "", "陕西", "甘肃", "青海", "宁夏", "新疆", "", "", "", "", "", "台湾", "", "", "", "", "", "", "", "", "", "香港", "澳门", "", "", "", "", "", "", "", "", "国外" };
            string[] re = Regex.Split(idStr, @"^(\d{2})\d{4}(((\d{2})(\d{2})(\d{2})(\d{3}))|((\d{4})(\d{2})(\d{2})(\d{3}[x\d])))$", RegexOptions.IgnoreCase);
            if (re.Length != 9) return false;
            int ProvId = int.Parse(re[1]);
            if (ProvId >= area.Length || area[ProvId] == "") return false;
            if (re[2].Length == 12)
            {
                Ai = idStr.Substring(0, 17);
                date = re[4] + "-" + re[5] + "-" + re[6];
            }
            else
            {
                Ai = idStr.Substring(0, 6) + "19" + idStr.Substring(6);
                date = "19" + re[4] + "-" + re[5] + "-" + re[6];
            }
            try
            {
                DateTime.Parse(date);
            }
            catch
            {
                return false;
            }
            int sum = 0;
            for (int i = 0; i <= 16; i++)
            {
                sum += int.Parse(Ai.Substring(i, 1)) * Wi[i];
            }
            Ai += verify.Substring(sum % 11, 1);
            return (idStr.Length == 15 || idStr.Length == 18 && idStr.ToLower() == Ai);
        }

        public DateTime GetBirthday(string certId)
        {
            var defaultDate = new DateTime(2014, 1, 5);
            var birthday = new StringBuilder();
            if (String.IsNullOrEmpty(certId) || certId.Trim().Length == 0)
            {
                return defaultDate;
            }
            if (certId.Trim().Length == 15)
            {
                birthday =
                    birthday.Append("19")
                            .Append(certId.Substring(6, 2))
                            .Append("-")
                            .Append(certId.Substring(8, 2))
                            .Append("-")
                            .Append(certId.Substring(10, 2));
            }
            else if (certId.Trim().Length == 18)
            {
                birthday =
                    birthday.Append(certId.Substring(6, 4))
                            .Append("-")
                            .Append(certId.Substring(10, 2))
                            .Append("-")
                            .Append(certId.Substring(12, 2));
            }
            else
            {
                return defaultDate;
            }
            var result = defaultDate;
            if (DateTime.TryParse(birthday.ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultDate;
            }
        }


        public string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        private bool doLogin(ref string cookie)
        {
            try
            {
                var platformId = GetParam("platform");
                var userName = GetParamNOUrlDecode("username");
                //if (userName.Length > 0 && userName.Contains("h5_zhiqu"))
                //    return false;
                var user = NbUserMgr.GetByAccount(userName, GetIp(), DateTime.Today.AddDays(-1),
                    DateTime.Today, 0);
                var sessionId = ShareUtil.GenerateComb().ToString();
                if (user != null)
                {
                    UserAccountEntity userAccountEntity = new UserAccountEntity(user.Account, Guid.Empty, "", 1,
                        platformId, sessionId);
                    userAccountEntity.ExtraData = "d|pengyou|f";


                    var info = reader.GetManagerInfoByAccount(userAccountEntity.Account, GetIp(),
                        UAFactory.Instance.IsTx);
                    if (info.Code == (int) MessageCode.Success)
                    {
                        if (info.Data.NeedSelect == false)
                        {
                            if (info.Data.ManagerInfo == null)
                            {
                                string playerName = "";
                                string logo = "1";
                                int templateId = 1; 
                                var createData = reader.CreateManager(UserAccount.Account, playerName, logo, templateId,
                                    UAHelper.GetRealIP());
                                if (createData.Code != (int)MessageCode.Success)
                                {
                                    info.Code = createData.Code;
                                    OutputHelper.Output(info);
                                }
                                info = reader.GetManagerInfoByAccount(UserAccount.Account, GetIp(),
                                    UAFactory.Instance.IsTx);;
                            }

                            if (CheckLockState(info.Data.ManagerInfo.Manager.Idx))
                            {
                                OutputHelper.Output(MessageCode.LoginOnlineLock);
                            }
                            //写入登录信息
                            cookie = UAHelper.SetFormsAuthentication(userAccountEntity.Account,
                                info.Data.ManagerInfo.Manager.Idx,
                                info.Data.ManagerInfo.Manager.Name, 1, sessionId);
                            OnlineMgr.LoginSession(info.Data.ManagerInfo.Manager.Idx, sessionId);
                            onlineClient.RiseOnlineTime(info.Data.ManagerInfo.Manager.Idx);
                            info.Data.Cookie = cookie;
                            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, cookie));
                            
                            return true;
                        }
                        return true;
                    }
                    else
                    {
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Account,
                            DateTime.Now,
                            DateTime.Now.AddDays(10),
                            false, userAccountEntity.ToString(),
                            FormsAuthentication
                                .FormsCookiePath);
                        cookie = FormsAuthentication.Encrypt(ticket);
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, cookie));
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                cookie = ex.ToString();
                return true;
            }
        }

        private void EgretInfo()
        {
            var token = GetParam("token");
            var time = ShareUtil.GetTimeTick(DateTime.Now);
            var appId =(int) UAEnum.EgretAppId;
            var appkey = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.EgretAppKey);
            var sign =CryptHelper.GetMD5("token=" + token + "time=" + time+ "appId=" + appId+appkey);
            var str = "token=" + token + "&time=" + time+ "&appId=" + appId+"&sign="+sign;
            var url = UAFactory.Instance.GetRedirectURL(UAFactory.Instance.FactoryName, "useraction");
            var result =UAHelper.HttpPost(url, str);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var r1 = serializer.Deserialize<PostResult>(result);
            if (r1.Code == 0)
            {
                try
                {
                    var r2 = serializer.Deserialize<EgretInfo>(result);
                    var response = new EgretInfoResponse();
                    response.Code = 0;
                    response.Data = r2;
                    OutputHelper.Output(response);
                }
                catch (Exception ex)
                {
                    
                    
                }
              
            }
            else
            {
                OutputHelper.OutputParameterError();
            }

        }

        private bool doLoginNew(ref string cookie)
        {
            try
            {
                TxLogininfoEntity tLoginInfo = null;
                var platformId = GetParam("platform");
                var userName = GetParam("openid");
                if (ShareUtil.IsQunHei)
                    userName = GetParam("username");
                //if (userName.Length>0 && userName.Contains("h5_zhiqu"))
                //    return false;
                var user = NbUserMgr.GetByAccount(userName, GetIp(), DateTime.Today.AddDays(-1),
                    DateTime.Today, 0);
                var sessionId = ShareUtil.GenerateComb().ToString();
                if (user != null)
                {
                    UserAccountEntity userAccountEntity = new UserAccountEntity(user.Account, Guid.Empty, "", 1,
                        platformId, sessionId);
                    userAccountEntity.ExtraData = "d|pengyou|f";


                    var info = reader.GetManagerInfoByAccount(userAccountEntity.Account, GetIp(),
                        UAFactory.Instance.IsTx);
                    if (info.Code == (int)MessageCode.Success)
                    {
                        if (info.Data.NeedSelect == false)
                        {
                            if (ShareUtil.IsTx)
                            {
                                string openKey = GetParam("openkey");
                                string pf = GetParam("pf");
                                if (string.IsNullOrEmpty(pf))
                                    pf = "wanba_ts";
                                string platform = GetParam("platform");
                                //userip
                                //sig
                                int appId = UAFactory.Instance.TxAppId;
                                string appKey = UAFactory.Instance.TxAppKey;
                                var result = WbUserInfo(userName,openKey,pf,platform,appId,appKey);
                                if (result != 0)
                                    OutputHelper.Output(result);
                                tLoginInfo = new TxLogininfoEntity(userName, openKey, pf, platform, "",
                                    "", DateTime.Now, DateTime.Now);
                            }
                            if (info.Data.ManagerInfo == null)
                            {
                                string playerName = "";
                                string logo = "1";
                                int templateId = 1;
                                var createData = reader.CreateManager(UserAccount.Account, playerName, logo, templateId,
                                    UAHelper.GetRealIP());
                                if (createData.Code != (int)MessageCode.Success)
                                {
                                    info.Code = createData.Code;
                                    OutputHelper.Output(info);
                                }
                                info = reader.GetManagerInfoByAccount(UserAccount.Account, GetIp(),
                                    UAFactory.Instance.IsTx); ;
                            }
                            try
                            {
                                if(ShareUtil.IsH5A8)
                                UA_A8.UserAction("entergame", info.Data.ManagerInfo.Manager.Account, "", "",
                                    info.Data.ManagerInfo.Manager);
                            }
                            catch (Exception)
                            {

                            }
                            if (CheckLockState(info.Data.ManagerInfo.Manager.Idx))
                            {
                                OutputHelper.Output(MessageCode.LoginOnlineLock);
                            }
                            //写入登录信息
                            cookie = UAHelper.SetFormsAuthentication(userAccountEntity.Account,
                                info.Data.ManagerInfo.Manager.Idx,
                                info.Data.ManagerInfo.Manager.Name, 1, sessionId);
                            OnlineMgr.LoginSession(info.Data.ManagerInfo.Manager.Idx, sessionId);
                            onlineClient.RiseOnlineTime(info.Data.ManagerInfo.Manager.Idx);
                            info.Data.Cookie = cookie;
                            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, cookie));
                            var isSubscribe = GetParamBool("isSubscribe");
                            if (isSubscribe)//关注
                            {
                                activityClient.DoShare(info.Data.ManagerInfo.Manager.Idx,4);
                            }
                            if (ShareUtil.IsTx)
                            {
                               TxLogininfoMgr.InsertUpdate(tLoginInfo);
                            }
                            return true;
                        }
                        return true;
                    }
                    else
                    {
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Account,
                            DateTime.Now,
                            DateTime.Now.AddDays(10),
                            false, userAccountEntity.ToString(),
                            FormsAuthentication
                                .FormsCookiePath);
                        cookie = FormsAuthentication.Encrypt(ticket);
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, cookie));
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                cookie = ex.ToString();
                return true;
            }
        }

        public int WbUserInfo(string openId,string openKey,string pf,string platform,int appId,string appKey)
        {
            try
            {
                OpenApiV3 sdk = new OpenApiV3(openId, openKey, appId, pf, appKey, UAFactory.Instance.OpenUALog);
                sdk.SetUserIp(UAHelper.GetRealIP());
                RstArray result = GetUserInfo(sdk);
                if (result.Ret != 0)
                {
                    return result.Ret;
                }
                var json = JsonConvert.DeserializeObject(result.Msg) as JObject;
                if (json == null)
                {
                    return 1;
                }
                //var nickName = JsonUtil.GetJsonValue(json, "nickname");
                //var logo = JsonUtil.GetJsonValue(json, "figureurl");
                //var extraData = BuildExtraData(openkey, pf, pfkey);
                if (pf == "wanba_ts")
                {

                    var result1 = GetWanBaUserInfo(sdk, "1");
                    if (result1.Ret != 0)
                    {
                        return result1.Ret;
                    }
                    var json1 = JsonConvert.DeserializeObject(result1.Msg) as JObject;
                    if (json1 == null)
                    {
                        return 1;
                    }
                    var data = json1["data"];
                    if (data == null)
                        return 1;

                    var is_vip = JsonUtil.GetJsonValue(data[0], "is_vip").ToLower(); //是否开通游戏达人
                    var vip_level = JsonUtil.GetJsonValue(data[0], "vip_level"); //达人等级
                    //var score = JsonUtil.GetJsonValue(data[0], "score"); //用户积分
                    //var expiredtime = JsonUtil.GetJsonValue(data[0], "vip_level"); //	达人过期时间
                    TxYellowvipMgr.Add(ShareUtil.GetWanBaVipAccount(openId), is_vip == "true", false, false,
                        ConvertHelper.ConvertToInt(vip_level), "");
                }
                return 0;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("腾讯获取用户", ex);
                return -1;
            }
        }

        RstArray GetUserInfo(OpenApiV3 sdk)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            string script_name = "/v3/user/get_info";
            return sdk.Api(script_name, param);
        }
        RstArray GetWanBaUserInfo(OpenApiV3 sdk,string zoneType)
        {
            //zoneType  1安卓 2ios
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["zoneid"] = zoneType;
            string script_name = "/v3/user/get_playzone_userinfo";
            return sdk.Api(script_name, param);
        }
    }
  
}