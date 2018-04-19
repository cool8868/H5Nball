using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Activity;
//using Games.NBall.Entity.Response.Active;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Ad;
using Games.NBall.Entity.Response.Manager;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IArenaService
    {
        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        [OperationContract]
        NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId, int arenaType, string zoneName = "");

        /// <summary>
        /// 上阵
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="byIndex"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        [OperationContract]
        NBSolutionInfoResponse UpFormation(Guid managerId, int arenaType, int byIndex, Guid teammemberId);

        /// <summary>
        /// 替换上场
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="teammemberId"></param>
        /// <param name="byTeammemberId"></param>
        /// <returns></returns>
        [OperationContract]
        NBSolutionInfoResponse ReplacePlayer(Guid managerId, int arenaType, Guid teammemberId, Guid byTeammemberId);

        /// <summary>
        /// 设置阵型
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="formationId"></param>
        /// <returns></returns>
        [OperationContract]
        NBSolutionInfoResponse SetSolution(Guid managerId, int arenaType, int formationId);

        /// <summary>
        /// 获取竞技场商城
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        GetArenaShopResponse GetShopInfoResponse(Guid managerId);

        /// <summary>
        /// 兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exIndex"></param>
        /// <returns></returns>
        [OperationContract]
        ArenaExChangeResponse ExChange(Guid managerId, int exIndex);

        /// <summary>
        /// 刷新竞技场商城
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        GetArenaShopResponse RefreshShop(Guid managerId);

        /// <summary>
        /// 竞技场球员下阵
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <returns></returns>
        [OperationContract]
        NBSolutionInfoResponse ArenaGoOffStage(Guid managerId, int arenaType);

        #region 点球

        /// <summary>
        /// 获取排名列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PenaltyKickRankResponse GetRank(Guid managerId);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        GetPenaltyKickInfoResponse GetInfo(Guid managerId);

        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        GetPenaltyKickInfoResponse Join(Guid managerId);

        /// <summary>
        /// 开始射门
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        PenaltyKickShootResponse Shoot(Guid managerId);

        /// <summary>
        /// 积分兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        [OperationContract]
        PenaltyKickExChangeResponse PenaltyKickExChange(Guid managerId, int itemCode);

        /// <summary>
        /// 刷新点球兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        PenaltyKickExChangeResponse RefreshExChange(Guid managerId);

        /// <summary>
        /// 活动提示接口
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        PenaltyKickGetActivityInfoResponse GetActivityInfo(Guid managerId);

        #endregion
    }
}
