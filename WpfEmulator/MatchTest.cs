using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Games.NBall.WpfEmulator
{
    public class UnitOnLineTest
    {
        public delegate void CreateDelegate(string serverName, string serverState, string matchResponse, string lotteryResponse);

        public UnitOnLineTest(string serverName, string serverUrl, string account, CreateDelegate createDelegate)
        {
            _serverName = serverName;
            _serverUrl = serverUrl;
            _account = account;
            _createDelegate = createDelegate;
        }


        private string _serverName;
        private string _serverUrl;
        private string _account;
        private CreateDelegate _createDelegate;
        private string  _serverState = "";
        private string _matchId = "";
        private Cookie _cookie;
        private string _matchResponse = "";
        private string _lotteryResponse = "";

        public void StartMatch()
        {
            try
            {
                if (SignInRequest())
                {
                    //Thread.Sleep(2000);
                    if (CheckHasLottery())
                    {
                        //Thread.Sleep(2000);
                        LotteryRequest();
                        //Thread.Sleep(2000);
                        TourMatchRequest();
                        Thread.Sleep(2000);
                        LotteryRequest();
                    }
                    else
                    {
                        TourMatchRequest();
                        Thread.Sleep(2000);
                        LotteryRequest();
                    }
                    _createDelegate(_serverName, _serverState, _matchResponse, _lotteryResponse);
                }
            }
            catch (Exception ex)
            {
                _createDelegate(_serverName, _serverState, _matchResponse, _lotteryResponse);
            }
            
        }

        private bool SignInRequest()
        {
            string requestUrl = _serverUrl + "passport.aspx?account=" + _account;
            string actionManagerInfo = _serverUrl + "Manager.do?action=managerinfo&random=0.2898396132513881";

            var response = Request(requestUrl);
            var responseManagerInfo = Request(actionManagerInfo);
            if (!CheckResponseCode(responseManagerInfo))
            {
                _createDelegate(_serverName, "没有注册经理", _matchResponse, _lotteryResponse);
                return false;
            }
            if (CheckResponseCode(response) && CheckResponseCode(responseManagerInfo))
            {
                _serverState = "已登录";
            }
            else 
            {
                _serverState = "未登录";
            }
            return true;
        }

        private void TourMatchRequest()
        {
            string matchRequestUrl = _serverUrl + "Tour.do?action=tourfight&ht=1&gtri=195069&stageId=101";
            _matchResponse = Request(matchRequestUrl);
            _matchId = getMatchId(_matchResponse);
        }

        private void LotteryRequest()
        {
            string lotteryAction = _serverUrl + "Tour.do?action=lottery&matchId=" + _matchId;
            _lotteryResponse = Request(lotteryAction);
        }
        private string Request(string requestUrl)
        {
            string responseText = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "GET";
                request.CookieContainer = new CookieContainer();
                if (_cookie != null)
                    request.CookieContainer.Add(_cookie);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
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
            return responseText;
        }

        private string getMatchId(string response)
        {
            string matchid = response.Substring(28, 36);
            return matchid;
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

        private bool CheckHasLottery()
        {
            string getleaguelist = _serverUrl + "Tour.do?action=getleaguelist";
            string getleaguelistResponse = Request(getleaguelist);
            if (CheckResponseCode(getleaguelistResponse))
            {
                if (getleaguelistResponse.Contains("\"HasLottery\":true,"))
                {
                    string id = getleaguelistResponse.Substring(64, 36);
                    _matchId = id;
                    return true;
                }
            }
            return false;
        }

    }

    public class UnitTestEntity
    {
        public UnitTestEntity(string serverName, string serverState, string matchResponse, string lotteryResponse)
        {
            ServerName = serverName;
            ServerState = serverState;
            MatchResponse = matchResponse;
            LotteryResponse = lotteryResponse;
        }

        public string ServerName { get; set; }
        public string ServerState { get; set; }
        public string MatchResponse { get; set; }
        public string LotteryResponse { get; set; }

    }
}
