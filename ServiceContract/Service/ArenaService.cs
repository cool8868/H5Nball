using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
//using Games.NBall.Core.Active;
using Games.NBall.Core;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Turntable;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Activity;
//using Games.NBall.Entity.Response.Active;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Ad;
using Games.NBall.Entity.Response.Manager;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall,
        AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class ArenaService : IArenaService
    {
        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId, int arenaType, string zoneName = "")
        {
            return ResponseHelper.TryCatch(() => ArenaTeammemberCore.Instance.SolutionAndTeammemberResponse(managerId, arenaType, zoneName));
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
            return ResponseHelper.TryCatch(() => ArenaTeammemberCore.Instance.UpFormation(managerId, arenaType, byIndex, teammemberId));
        }

        /// <summary>
        /// 替换上场
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="teammemberId"></param>
        /// <param name="byTeammemberId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse ReplacePlayer(Guid managerId, int arenaType, Guid teammemberId, Guid byTeammemberId)
        {
            return ResponseHelper.TryCatch(() => ArenaTeammemberCore.Instance.ReplacePlayer(managerId, arenaType, teammemberId, byTeammemberId));
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
            return ResponseHelper.TryCatch(() => ArenaTeammemberCore.Instance.SetSolution(managerId, arenaType, formationId));
        }

        /// <summary>
        /// 获取竞技场商城
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetArenaShopResponse GetShopInfoResponse(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => ArenaTeammemberCore.Instance.GetShopInfoResponse(managerId));
        }

        /// <summary>
        /// 兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exIndex"></param>
        /// <returns></returns>
        public ArenaExChangeResponse ExChange(Guid managerId, int exIndex)
        {
            return ResponseHelper.TryCatch(() => ArenaTeammemberCore.Instance.ExChange(managerId, exIndex));
        }

        /// <summary>
        /// 刷新竞技场商城
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetArenaShopResponse RefreshShop(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => ArenaTeammemberCore.Instance.RefreshShop(managerId));
        }

        public NBSolutionInfoResponse ArenaGoOffStage(Guid managerId, int arenaType)
        {
            return ResponseHelper.TryCatch(() => ArenaTeammemberCore.Instance.ArenaGoOffStage(managerId, arenaType));
        }

        #region 点球

        /// <summary>
        /// 获取排名列表
        /// </summary>
        /// <returns></returns>
        public PenaltyKickRankResponse GetRank(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => PenaltyKickCore.Instance.GetRank(managerId));
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetPenaltyKickInfoResponse GetInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => PenaltyKickCore.Instance.GetInfo(managerId));
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetPenaltyKickInfoResponse Join(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => PenaltyKickCore.Instance.Join(managerId));
        }

        /// <summary>
        /// 开始射门
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PenaltyKickShootResponse Shoot(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => PenaltyKickCore.Instance.Shoot(managerId));
        }

        /// <summary>
        /// 积分兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public PenaltyKickExChangeResponse PenaltyKickExChange(Guid managerId, int itemCode)
        {
            return ResponseHelper.TryCatch(() => PenaltyKickCore.Instance.ExChange(managerId, itemCode));
        }

        /// <summary>
        /// 刷新点球兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PenaltyKickExChangeResponse RefreshExChange(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => PenaltyKickCore.Instance.RefreshExChange(managerId));
        }

        /// <summary>
        /// 活动提示接口
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PenaltyKickGetActivityInfoResponse GetActivityInfo(Guid managerId)
        {
           return ResponseHelper.TryCatch(() => PenaltyKickCore.Instance.GetActivityInfo(managerId));
        }

        #endregion
    }
}
