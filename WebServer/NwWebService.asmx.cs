using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using Games.NBall.Bll;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Core.Mail;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Manager;
using Games.NBall.ServiceContract.Client;
using Games.NBall.ServiceContract.Client.Client;

namespace Games.NBall.WebServer
{
    /// <summary>
    /// NwWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://nball.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class NwWebService : System.Web.Services.WebService
    {
        private readonly AdminClient adminClient = new AdminClient();
        private readonly ManagerClient managerClient = new ManagerClient();
        private readonly OnlineClient onlineClient = new OnlineClient();
        private readonly MallClient mallClient= new MallClient();
        /// <summary>
        /// 储存渠道玩家数据
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="state"></param>
        /// <param name="serverId"></param>
        /// <param name="pf"></param>
        /// <param name="sessionId"></param>
        /// <param name="jsNeed"></param>
        /// <param name="nickName"></param>
        /// <returns></returns>

        [WebMethod]
        public bool SetStartGameEntity(string openId, string state, string serverId, string pf, string sessionId, string jsNeed, string nickName = "",string common="")
        {
            return onlineClient.SetStartGameEntity(openId, state, serverId, pf, sessionId, jsNeed, nickName,common);
        }
        /// <summary>
        /// 获得渠道玩家数据
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>

        [WebMethod]
        public A8csdkStartgameEntity GetStartgameEntity(string openId)
        {
            return onlineClient.GetStartgameEntity(openId);
        }

        [WebMethod]
        public int AddCoin(Guid managerId, int coin)
        {
            try
            {
                return (int)adminClient.AddCoin(managerId, coin);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -100;
            }
            
        }
        [WebMethod]
        public int AddCoin2(Guid managerId, int coin,int sourceType)
        {
            try
            {
                return (int)adminClient.AddCoin2(managerId, coin,sourceType);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -100;
            }

        }

        [WebMethod]
        public int Charge(string account, int sourceType, int cash, int point, int bonus, string orderId)
        {
            try
            {
                return (int)adminClient.Charge(account, sourceType, cash, point, bonus, orderId);

            }
            catch (Exception)
            {

                return -1;
            }
        }

        [WebMethod]
        public int BuyPointShipments(string managerId, string orderId, string billingId,decimal cash,int mallCode)
        {
            try
            {
                var response = mallClient.BuyPointShipments(managerId, orderId, billingId, cash, mallCode);
                return response.Code;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -1;
            }
        }

        [WebMethod]
        public NbManagerEntity IsRegist(String openId, string serverNo)
        {
            try
            {
                return managerClient.IsRegist( openId,  serverNo);
            }
            catch (Exception)
            {

                return null;
            }
        }
        [WebMethod]
        public NbManagerEntity IsRegistByName(string name, string serverNo)
        {
            try
            {
                return managerClient.IsRegistByName(name, serverNo);
            }
            catch (Exception)
            {

                return null;
            }
        }
        [WebMethod]
        public List<NbManagerEntity> IsRegistByNameList(string data)
        {
            try
            {
                return managerClient.IsRegistByNameList(data);
            }
            catch (Exception)
            {

                return null;
            }
        }

        [WebMethod]
        public List<NbManagerEntity> GetManagerList(string account)
        {
            try
            {
                return NbManagerMgr.GetByAccount(account);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return null;
            }
        }
            
        [WebMethod]
        public int CheckActive(string account)
        {
            try
            {
                return (int)adminClient.CheckActive(account);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -100;
            }
        }

        [WebMethod]
        public int AttachmentReceive(Guid managerId, int recordId)
        {
            try
            {
                var response = MailCore.Instance.AttachmentReceive(managerId,recordId);
                return response.Code;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -1;
            }
        }

        [WebMethod]
        public int TxBuyItem(string openId, string payItem, string token, string billno,
                             string version, string zoneId, string amt, string payamt_coins
                             , string pubacct_payamt_coins, int exchangeRate)
        {
            try
            {
                //var response = MallCore.Instance.TxBuyItem(openId,payItem,token,billno
                //    ,version,zoneId,amt,payamt_coins,pubacct_payamt_coins,exchangeRate);
                //return response.Code;
                return -2;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -1;
            }
        }

        [WebMethod]
        public int TxTaskStep(string account, int activityId, int activityStep, string cmd,
                                              string billno)
        {
            try
            {
                //var response = ActivityCore.Instance.TxTaskStep(account,activityId,activityStep,cmd,billno);
                //return response.Code;
                return -2;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -1;
            }
        }


        [WebMethod]
        public bool KickSession(Guid managerId)
        {
            try
            {
                return OnlineCore.KickSession(managerId);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }
        [WebMethod]
        public bool LockUserUnexpect(Guid managerId, string adminName, string memo)
        {
            try
            {
                return OnlineCore.LockUserUnexpect(managerId,adminName,memo);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }
        [WebMethod]
        public string ResetCache(int cacheType)
        {

            return "exception:";
        }


        [WebMethod]
        public string CrowdStart(DateTime startTime, DateTime endTime)
        {
                return "WS cause exception:";
        }


        [WebMethod]
        public string PeakStart(DateTime startTime, DateTime endTime)
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "WS cause exception:" + ex.Message;
            }
        }


        [WebMethod]
        public string CrowdSendPrize(int crowdId)
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "WS cause exception:" + ex.Message;
            }
        }



        [WebMethod]
        public MessageCode AddManagerData(Guid managerId, int prizeExp, int prizeCoin, int prizeSophisticate)
        {
            try
            {
                return adminClient.AddManagerData(managerId, prizeExp, prizeCoin, prizeSophisticate);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return MessageCode.Exception;
            }
        }

        [WebMethod]
        public int GetOnlineCount()
        {
            try
            {
                return onlineClient.GetOnlineCount();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -1;
            }
        }
        [WebMethod]
        //存a8会话id
        public bool SetSessionId(string openId,string sessionId)
        {
            try
            {
                return onlineClient.SetSessionId(openId, sessionId);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        [WebMethod]
        //拿a8会话id
        public string GetSessionId(string openId)
        {
            try
            {
                return onlineClient.GetSessionId(openId);
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }


        [WebMethod]
        public bool CheckLockStateNDate(Guid managerId)
        {
            try
            {
                return OnlineCore.CheckLockState(managerId);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }

        [WebMethod]
        public bool BreakLock(Guid managerId, string GMName, string memo, string zoneId)
        {
             try
            {
                return OnlineCore.BreakLock(managerId, GMName, memo, zoneId);
            }
             catch (Exception ex)
             {
                 LogHelper.Insert(ex);
                 return false;
             }
        }
        /// <summary>
        ///  分享礼包
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [WebMethod]
        public int SendItemByShare(string name, int type)
        {
            try
            {
                return managerClient.SendItemByShare(name, type);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
