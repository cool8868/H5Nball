using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Coach;
using Games.NBall.Core.Revelation;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Coach;
using Games.NBall.Entity.Response.Revelation;
using Games.NBall.Entity.Response.Scouting;
using Games.NBall.ServiceContract.IService;
using Games.NBall.Entity.Response;
using Games.NBall.Core.Turntable;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class ScoutingService:IScoutingService
    {
        /// <summary>
        /// 获取球探信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ScoutingInfoResponse GetScoutingInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => ScoutingCore.Instance.GetScoutingInfo(managerId));
        }

        /// <summary>
        /// 球探抽卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="scoutingId"></param>
        /// <returns></returns>
        public ScoutingLotteryResponse ScoutingLottery(Guid managerId, int scoutingId, bool hasTask, int count, bool isAutoDec)
        {
            //if (!ManagerUtil.CheckFunction(managerId, EnumOpenFunction.Scouting))
            //    return ResponseHelper.InvalidFunction<ScoutingLotteryResponse>();
            return ResponseHelper.TryCatch(() => ScoutingCore.Instance.ScoutingLottery(managerId,scoutingId,hasTask,count,isAutoDec));
        }

         /// <summary>
        /// 获取用户转盘信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetTurntableInfoResponse GetManagerInfo(Guid managerId) 
        {
            return ResponseHelper.TryCatch(() => TurntableCore.Instance.GetManagerInfo(managerId));
        }

        /// <summary>
        /// 转盘抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TurntableLuckDrawResponse TurntableLuckDraw(Guid managerId) 
        {
            return ResponseHelper.TryCatch(() => TurntableCore.Instance.TurntableLuckDraw(managerId));
        }

        /// <summary>
        /// 重置转盘
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TurntableResetResponse ResetTurntable(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => TurntableCore.Instance.ResetTurntable(managerId));
        }

        #region 球星启示录
        /// <summary>
        /// 根据经理ID获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetManagerResponse RevelationGetManagerId(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationGetManagerId(managerId));
        }

        /// <summary>
        /// 开始一个大关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="markId"></param>
        /// <returns></returns>
        public RevelationStartMarkResponse StartMark(Guid managerId, int markId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.StartMark(managerId, markId));
        }

        /// <summary>
        /// 根据关卡获取关卡信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetIsGeneralResponse GetMarkInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.GetMarkInfo(managerId));
        }

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns></returns>
        public RevelationTheGameResponse RevelationTheGame(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationTheGame(managerId));
        }

        /// <summary>
        /// 获取翻牌信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        public RevelationDrawResponse GetDrawInfo(Guid managerId, Guid drawId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.GetDrawInfo(managerId, drawId));
        }

        /// <summary>
        /// 翻牌结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        public RevelationDrawResultResponse DrawResult(Guid managerId, Guid drawId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.DrawResult(managerId, drawId));
        }

        /// <summary>
        /// 增加士气
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationAddChallengeResponse RevelationAddMorale(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationAddMorale(managerId));
        }

        /// <summary>
        /// 重置关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse RevelationResetMark(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationResetMark(managerId));
        }

        /// <summary>
        /// 获取勇气商城信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetShopResponse RevelationGetShopInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationGetShopInfo(managerId));
        }

        /// <summary>
        /// 勇气商城兑换物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public RevelationPurchaseItemsResponse RevelationPurchaseItems(Guid managerId, int idx)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationPurchaseItems(managerId, idx));
        }


        /// <summary>
        /// 勇气商城刷新物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetShopResponse RevelationShopRefresh(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationShopRefresh(managerId));
        }

        #endregion

        #region 教练

        /// <summary>
        /// 获取教练列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public CoachGetListResponse GetCoachList(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CoachCore.Instance.GetCoachList(managerId));
        }

        /// <summary>
        /// 激活教练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse ActivationCoach(Guid managerId, int coachId)
        {
            return ResponseHelper.TryCatch(() => CoachCore.Instance.ActivationCoach(managerId, coachId));
        }

        /// <summary>
        /// 替换启用的教练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse ReplaceCoach(Guid managerId, int coachId)
        {
            return ResponseHelper.TryCatch(() => CoachCore.Instance.ReplaceCoach(managerId, coachId));
        }

        /// <summary>
        /// 教练经验升级
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse CoachUpgrade(Guid managerId, int coachId)
        {
            return ResponseHelper.TryCatch(() => CoachCore.Instance.CoachUpgrade(managerId, coachId));
        }

        /// <summary>
        /// 教练技能升级
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse CoachSkillUpgrade(Guid managerId, int coachId)
        {
            return ResponseHelper.TryCatch(() => CoachCore.Instance.CoachSkillUpgrade(managerId, coachId));
        }

        /// <summary>
        /// 教练升星
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse CoachStarUpgrade(Guid managerId, int coachId)
        {
            return ResponseHelper.TryCatch(() => CoachCore.Instance.CoachStarUpgrade(managerId, coachId));
        }

        #endregion
    }
}
