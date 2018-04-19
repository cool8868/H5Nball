using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Games.NBall.WpfEmulator.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Zone
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="account"></param>
        /// <param name="loginMsgDelegate"></param>
        /// <param name="responseMsgDelegate"></param>
        public Zone(string name, string url, string account, CreateMsgDelegate loginMsgDelegate, CreateMsgDelegate responseMsgDelegate)
        {
            Name = name;
            Url = url;
            Account = account;
            _loginMsgDelegate = loginMsgDelegate;
            _responseMsgDelegate = responseMsgDelegate;
        }

        /// <summary>
        /// 
        /// </summary>
        public delegate void CreateMsgDelegate(string response);

        /// <summary>
        /// 平台名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 比赛ID
        /// </summary>
        public string MatchId { get; set; }

        private readonly CreateMsgDelegate _loginMsgDelegate;
        private readonly CreateMsgDelegate _responseMsgDelegate;
        private Cookie _cookie;

        /// <summary>
        /// 登陆
        /// </summary>
        public void Login()
        {
            var resp = RequestPassport();
            var respManagerInfo = ActionRequest("Manager", "managerinfo", "");
            if (resp.Contains("\"Code\":0,") && respManagerInfo.Contains("\"Code\":0,"))
            {
                _loginMsgDelegate(Name + " 登陆成功: " + "\r\n" + respManagerInfo + "\r\n");
            }
            else
            {
                _loginMsgDelegate(Name + "！！！登录失败: " + "\r\n" + respManagerInfo + "\r\n");
            }
        }

        /// <summary>
        /// 方法调用
        /// </summary>
        /// <param name="module"></param>
        /// <param name="action"></param>
        /// <param name="parameters"></param>
        public void MethodRequest(string module, string action, string parameters)
        {
            string resp = "";

            if (module == "Tour" && action == "tourfight")
            {
                //巡回赛是否有为抽的卡
                if (CheckHasLottery())
                {
                    string temp = parameters;
                    parameters = MatchId;
                    //先处理掉未抽的卡
                    var v = ActionRequest("Tour", "lottery", "matchId=" + parameters);
                    if (v.Contains("\"Code\":0,"))
                    {
                        resp = ActionRequest(module, action, temp);
                        SetMatchId(resp);
                        _responseMsgDelegate(Name + "拥有赛后抽卡未抽...执行抽卡中..." + "\r\n" + "抽卡成功：" + v + "\r\n" + "Action: " + action + "\r\n" + resp +"\r\n");
                        
                    }
                    else
                    {
                        _responseMsgDelegate(Name + "！！！抽卡失败：" + "\r\n" + v + "\r\n");
                    }       
                }
                else
                {
                    resp = ActionRequest(module, action, parameters);
                    _responseMsgDelegate(Name + "  Action: " + action + "\r\n" + resp + "\r\n");
                    SetMatchId(resp);
                }
            }
            else if (module == "Tour" && action == "lottery")
            {
                if (string.IsNullOrEmpty(MatchId))
                {
                    if (!CheckHasLottery())
                    {
                        _responseMsgDelegate(Name + "  Action: " + action + "\r\n" + "请先去比赛" + "\r\n");
                    }
                    else
                    {
                        parameters = "matchId=" + MatchId;
                        resp = ActionRequest(module, action, parameters);
                        _responseMsgDelegate(Name + "  Action: " + action + "\r\n" + resp + "\r\n");
                    }
                }
            }
            else
            {
                resp = ActionRequest(module, action, parameters);
                _responseMsgDelegate(Name + "  Action: " + action + "\r\n" + resp + "\r\n");
            }
        }

        /// <summary>
        /// 接口请求
        /// </summary>
        /// <param name="module"></param>
        /// <param name="action"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string ActionRequest(string module, string action, string parameters)
        {
            //return RequestHelper.Request(Url, module, action, parameters);
            string requestMethod = string.Format("{0}{1}.do?action={2}&{3}", Url, module, action, parameters);
            return Request(requestMethod);
        }

        /// <summary>
        /// 登陆接口
        /// </summary>
        /// <returns></returns>
        private string RequestPassport()
        {
            string requestMethod = Url + "Passport.aspx?account=" + Account;
            return Request(requestMethod);
        }

        private string Request(string requestMethod)
        {
            string responseText = "";
            DateTime requestTime = DateTime.Now;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestMethod);
                //request.Timeout = 5000;
                request.Method = "GET";
                request.CookieContainer = new CookieContainer();
                if (_cookie != null)
                    request.CookieContainer.Add(_cookie);

                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                if (response.Cookies["nb_form"] != null)
                    _cookie = response.Cookies["nb_form"];
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

            //ApiTestCore.MainWindow.RequestLog(requestMethod, requestTime);
            //ApiTestCore.MainWindow.ResponseLog(responseText, DateTime.Now, totalTime);

            return responseText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckHasLottery()
        {
            string getleaguelistResponse = ActionRequest("Tour", "getleaguelist", "");
            if (CheckResponseCode(getleaguelistResponse))
            {
                if (getleaguelistResponse.Contains("\"HasLottery\":true,"))
                {
                    string id = getleaguelistResponse.Substring(64, 36);
                    MatchId = id;
                    return true;
                }
            }
            return false;
        }

        private bool CheckResponseCode(string response)
        {
            //{"Code":0,
            if (response.Contains("\"Code\":0,"))
            {
                return true;
            }
            return false;
        }

        private void SetMatchId(string response)
        {
            if (CheckResponseCode(response))
            {
                MatchId = response.Substring(28, 36);
            }
        }
    }
}
