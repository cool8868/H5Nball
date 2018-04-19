using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IOnlineService
    {
        /// <summary>
        /// 刷新在线时间
        /// </summary>
        /// <param name="managerId"></param>
        [OperationContract(IsOneWay = true)]
        void RiseOnlineTime(Guid managerId);

        [OperationContract]
        int GetIndulgeMinutes(string account, Guid managerId);

        [OperationContract]
        int GetOnlineCount();

        [OperationContract]
        //a8接口存sessionId
        bool SetSessionId(string openId, string sessionId);
      
        //a8接口拿sessionId
        [OperationContract]
        string GetSessionId(string openId);

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
       /// <param name="common"></param>
       /// <returns></returns>
        [OperationContract]
        bool SetStartGameEntity(string openId, string state, string serverId, string pf, string sessionId, string jsNeed,
            string nickName = "", string common = "");

        /// <summary>
        /// 获得渠道玩家数据
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
       [OperationContract]
        A8csdkStartgameEntity GetStartgameEntity(string openId);
        
        /// <summary>
        /// 获取用户登录记录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        [OperationContract]
        GetUserLoginRecordResponse GetUserLoginRecord(string account, string platform);

        /// <summary>
        /// 获取平台所有区信息
        /// </summary>
        /// <param name="platfrom"></param>
        /// <returns></returns>
        [OperationContract]
        GetAllZoneListResponse GetAllZoneInfo(string platfrom);

        /// <summary>
        /// 无限期封停用户
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="GMName"></param>
        /// <param name="memo"></param>
        /// <returns></returns>

        [OperationContract]
        bool LockUserUnexpect(Guid managerId, string GMName, string memo);

        /// <summary>
        /// 解封用户
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="GMName">GM名字</param>
        /// <param name="memo">解封说明</param>
        /// <returns></returns>
        [OperationContract]
        bool BreakLock(Guid managerId, string GMName, string memo, string zoneId);

        /// <summary>
        /// 踢线
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <returns>在线标记,True为当前在线</returns>
          [OperationContract]
        bool KickSession(Guid managerId);

       
    }
}
