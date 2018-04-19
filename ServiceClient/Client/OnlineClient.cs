using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class OnlineClient
    {
        private static IOnlineService proxy = ServiceProxy<IOnlineService>.Create("NetTcp_IOnlineService");
        /// <summary>
        /// 刷新在线时间
        /// </summary>
        /// <param name="managerId"></param>
        public void RiseOnlineTime(Guid managerId)
        {
            proxy.RiseOnlineTime(managerId);
        }

        public int GetOnlineCount()
        {
            return proxy.GetOnlineCount();
        }

        public int GetIndulgeMinutes(string account, Guid managerId)
        {
            return proxy.GetIndulgeMinutes(account, managerId);
        }
        //a8接口存sessionId
        public  bool SetSessionId(string openId, string sessionId)
        {
            return proxy.SetSessionId(openId, sessionId);
        }
        //a8接口拿sessionId
        public  string GetSessionId(string openId)
        {
            return proxy.GetSessionId(openId);
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
        public bool SetStartGameEntity(string openId, string state, string serverId, string pf, string sessionId, string jsNeed, string nickName = "", string common = "")
        {
            return proxy.SetStartGameEntity(openId, state, serverId, pf, sessionId, jsNeed, nickName,common);
        }
        /// <summary>
        /// 获得渠道玩家数据
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public A8csdkStartgameEntity GetStartgameEntity(string openId)
        {
            return proxy.GetStartgameEntity(openId);
        }

        /// <summary>
        /// 获取用户登录记录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        public GetUserLoginRecordResponse GetUserLoginRecord(string account, string platform)
        {
            return proxy.GetUserLoginRecord(account, platform);
        }

        /// <summary>
        /// 获取平台所有区信息
        /// </summary>
        /// <param name="platfrom"></param>
        /// <returns></returns>
        public GetAllZoneListResponse GetAllZoneInfo(string platfrom)
        {
            return proxy.GetAllZoneInfo(platfrom);
        }
        /// <summary>
        /// 无限期封停用户
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="GMName"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public bool LockUserUnexpect(Guid managerId, string GMName, string memo)
        {

            return proxy.LockUserUnexpect(managerId, GMName, memo);

        }

        /// <summary>
        /// 解封用户
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="GMName">GM名字</param>
        /// <param name="memo">解封说明</param>
        /// <returns></returns>
        public bool BreakLock(Guid managerId, string GMName, string memo, string zoneId)
        {
            return proxy.BreakLock(managerId, GMName, memo, zoneId);

        }
        /// <summary>
        /// 踢线
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <returns>在线标记,True为当前在线</returns>
        public bool KickSession(Guid managerId)
        {
            return proxy.KickSession(managerId);

        }
       
    }
}
