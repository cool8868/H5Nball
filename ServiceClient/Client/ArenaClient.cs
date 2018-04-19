using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Activity;
//using Games.NBall.Entity.Response.Active;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Ad;
using Games.NBall.Entity.Response.Manager;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class ArenaClient
    {
        private static IArenaService proxy = ServiceProxy<IArenaService>.Create("NetTcp_IArenaService");

        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId, int arenaType, string zoneName = "")
        {
            return proxy.SolutionAndTeammemberResponse(managerId, arenaType, zoneName);
        }

        /// <summary>
        /// 上阵
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="byIndex"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse UpFormation(Guid managerId, int arenaType, int byIndex, Guid teammemberId)
        {
            return proxy.UpFormation(managerId, arenaType, byIndex, teammemberId);
        }

        /// <summary>
        /// 替换上场
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="teammemberId"></param>
        /// <param name="byTeammemberId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse ReplacePlayer(Guid managerId, int arenaType, Guid teammemberId,
            Guid byTeammemberId)
        {
            return proxy.ReplacePlayer(managerId, arenaType, teammemberId, byTeammemberId);
        }

        /// <summary>
        /// 设置阵型
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="formationId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SetSolution(Guid managerId, int arenaType, int formationId)
        {
            return proxy.SetSolution(managerId, arenaType, formationId);
        }

        /// <summary>
        /// 获取竞技场商城
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetArenaShopResponse GetShopInfoResponse(Guid managerId)
        {
            return proxy.GetShopInfoResponse(managerId);
        }

        /// <summary>
        /// 兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exIndex"></param>
        /// <returns></returns>
        public ArenaExChangeResponse ExChange(Guid managerId, int exIndex)
        {
            return proxy.ExChange(managerId, exIndex);
        }

        /// <summary>
        /// 刷新竞技场商城
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetArenaShopResponse RefreshShop(Guid managerId)
        {
            return proxy.RefreshShop(managerId);
        }


        public NBSolutionInfoResponse ArenaGoOffStage(Guid managerId, int arenaType)
        {
            return proxy.ArenaGoOffStage(managerId, arenaType);
        }

        #region 点球

        /// <summary>
        /// 获取排名列表
        /// </summary>
        /// <returns></returns>
        public PenaltyKickRankResponse GetRank(Guid managerId)
        {
            return proxy.GetRank(managerId);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetPenaltyKickInfoResponse GetInfo(Guid managerId)
        {
            return proxy.GetInfo(managerId);
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetPenaltyKickInfoResponse Join(Guid managerId)
        {
            return proxy.Join(managerId);
        }

        /// <summary>
        /// 开始射门
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PenaltyKickShootResponse Shoot(Guid managerId)
        {
            return proxy.Shoot(managerId);
        }

        /// <summary>
        /// 积分兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public PenaltyKickExChangeResponse PenaltyKickExChange(Guid managerId, int itemCode)
        {
            return proxy.PenaltyKickExChange(managerId, itemCode);
        }

        /// <summary>
        /// 刷新点球兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PenaltyKickExChangeResponse RefreshExChange(Guid managerId)
        {
            return proxy.RefreshExChange(managerId);
        }


        /// <summary>
        /// 活动提示接口
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PenaltyKickGetActivityInfoResponse GetActivityInfo(Guid managerId)
        {
            return proxy.GetActivityInfo(managerId);
        }

        #endregion
    }
}
