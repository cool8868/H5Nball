using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using NbManagerEntity = Games.NBall.WebServerFacade.NwWebService.NbManagerEntity;
using Games.NBall.WebServerFacade.NwWebService;



namespace Games.NBall.WebServerFacade
{
    public class WebServerHandler
    {
     

        public static int AddCoin(string zoneId, Guid managerId, int coin)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.AddCoin(managerId, coin);
            }
            catch (Exception e)
            {
                LogHelper.Insert(e);
                return -1;
            }
        }
        public static int AddCoin2(string zoneId, Guid managerId, int coin,int sourceType)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                
                return ws.AddCoin2(managerId, coin,sourceType);
            }
            catch (Exception e)
            {
                LogHelper.Insert(e);
                return -1;
            }
        }

        public static int BuyPointShipments(string platformCode, string platformZoneCode,string managerId,string orderId,string billingId, decimal cash,int mallCode)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                //return -1;
                return ws.BuyPointShipments(managerId, orderId, billingId, cash, mallCode);
            }
            catch (Exception e)
            {
                LogHelper.Insert(e);
                return -1;
            }
        }
        public static NbManagerEntity IsRegist(string platformCode, string platformZoneCode, String openId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);

                return ws.IsRegist(openId, platformZoneCode);
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static NbManagerEntity IsRegistByName(string platformCode, string platformZoneCode, string name)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);

                return ws.IsRegistByName(name, platformZoneCode);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static NbManagerEntity[] IsRegistByNameList(string platformCode, string platformZoneCode, string data)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                return ws.IsRegistByNameList(data);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static int CheckActive(string zoneId, string account)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.CheckActive(account);
            }
            catch (Exception e)
            {
                LogHelper.Insert(e);
                return -1;
            }
        }

        public static int CheckActive(string platformCode, string platformZoneCode, string account)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                return ws.CheckActive(account);
            }
            catch (Exception e)
            {
                LogHelper.Insert(e);
                return -1;
            }
        }


        public static int Charge(string zoneId, string account, EnumChargeSourceType sourceType, int cash, int point, int bonus, string orderId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.Charge(account, (int)sourceType, cash, point, bonus, orderId);
            }
            catch (Exception e)
            {
                LogHelper.Insert(e);
                return -1;
            }
        }


      

        public static int AttachmentReceive(string zoneId, Guid managerId, int recordId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.AttachmentReceive(managerId, recordId);
            }
            catch (Exception e)
            {
                LogHelper.Insert(e);
                return -1;
            }
        }


        public static int TxBuyItem(string openId, string payItem, string token, string billno,
                             string version, string zoneId, string amt, string payamt_coins
                             , string pubacct_payamt_coins, int exchangeRate, string platformCode)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, zoneId);
                return ws.TxBuyItem(openId, payItem, token, billno
                    , version, zoneId, amt, payamt_coins, pubacct_payamt_coins, exchangeRate);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -1;
            }
        }


        public static int TxTaskStep(string account, int activityId, int activityStep, string cmd,
                                              string billno, string platformZoneCode, string platformCode)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                return ws.TxTaskStep(account, activityId, activityStep, cmd, billno);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -1;
            }
        }


        public static bool KickSession(string zoneId, Guid managerId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.KickSession(managerId);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }

        public static bool LockUserUnexpect(string zoneId, Guid managerId, string adminName, string memo)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.LockUserUnexpect(managerId, adminName, memo);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }

        public static string ResetCache(string zoneId, int cacheType)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.ResetCache(cacheType);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "exception:"+ex.Message;
            }
        }

       
        public static string CrowdStart(string zoneId, DateTime startTime, DateTime endTime)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.CrowdStart(startTime,endTime);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "WSS exception:" + ex.Message;
            }
        }

        public static string PeakStart(string zoneId, DateTime startTime, DateTime endTime)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.PeakStart(startTime, endTime);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "WSS exception:" + ex.Message;
            }
        }

        public static string CrowdSendPrize(string zoneId, int crowdId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                return ws.CrowdSendPrize(crowdId);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "WSS exception:"+ex.Message;
            }
        }

        public static NbManagerEntity[] GetManagerList(string account, string platformZoneCode, string platformCode)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode,platformZoneCode);
                return ws.GetManagerList(account);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return null;
            }
        }


       

       

        public static int AddManagerData(Guid managerId, int prizeExp, int prizeCoin, int prizeSophisticate,string zoneId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebService(zoneId);
                var code = ws.AddManagerData(managerId,prizeExp,prizeCoin,prizeSophisticate);
                return (int) code;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -1;
            }
        }

        public static int GetOnlineCount(string platformZoneCode, string platformCode)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                return ws.GetOnlineCount();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return -1;
            }
        }
        //存a8回话id
        public static bool SetSessionId(string platformCode, string platformZoneCode, string openId, string sessionId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                return ws.SetSessionId(openId,sessionId);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        //存a8回话id
        public static string GetSessionId(string platformCode, string platformZoneCode, string openId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                return ws.GetSessionId(openId);
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }



        public static bool LockUserUnexpect(string platformZoneCode, string platformCode, Guid managerId, string adminName, string memo)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                return ws.LockUserUnexpect(managerId, adminName, memo);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }

        public static bool CheckLockStateNDate(string platformZoneCode, string platformCode, Guid managerId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                return ws.CheckLockStateNDate(managerId);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }

        public static bool BreakLock(string platformZoneCode, string platformCode, Guid managerId, string GMName, string memo, string zoneId)
        {
            try
            {
                NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
                return ws.BreakLock(managerId, GMName, memo, zoneId);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }
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
        public static bool SetStartGameEntity(string platformCode,string platformZoneCode,string openId, string state, string serverId, string pf, string sessionId, string jsNeed, string nickName = "",string common="")
        {
            NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, platformZoneCode);
            return ws.SetStartGameEntity(openId, state, serverId, pf, sessionId, jsNeed, nickName,common);
        }
        /// <summary>
        /// 获得渠道玩家数据
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static  Games.NBall.WebServerFacade.NwWebService.A8csdkStartgameEntity GetStartgameEntity(string openId,string platformCode,string serverId="1")
        {
            NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, serverId);
            return ws.GetStartgameEntity(openId);
        }

        public static int SendItemByShare(string platformCode, string serverId,string name,int type)
        {
            NwWebService.NwWebService ws = WebServiceFactory.GetWebServicePlatform(platformCode, serverId);
            return ws.SendItemByShare(name, type);
        }


    }
}
