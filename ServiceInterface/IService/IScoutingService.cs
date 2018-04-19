using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Coach;
using Games.NBall.Entity.Response.Revelation;
using Games.NBall.Entity.Response.Scouting;
using Games.NBall.Entity.Response;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IScoutingService
    {
        /// <summary>
        /// 获取球探信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        ScoutingInfoResponse GetScoutingInfo(Guid managerId);

        /// <summary>
        /// 球探抽卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="scoutingId"></param>
        /// <returns></returns>
        [OperationContract]
        ScoutingLotteryResponse ScoutingLottery(Guid managerId, int scoutingId, bool hasTask, int count, bool isAutoDec);

        /// <summary>
        /// 获取用户转盘信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        GetTurntableInfoResponse GetManagerInfo(Guid managerId);

        /// <summary>
        /// 转盘抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        TurntableLuckDrawResponse TurntableLuckDraw(Guid managerId);

        /// <summary>
        /// 重置转盘
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        TurntableResetResponse ResetTurntable(Guid managerId);

        #region 球星启示录
        /// <summary>
        /// 根据经理ID获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationGetManagerResponse RevelationGetManagerId(Guid managerId);

        /// <summary>
        /// 开始一个大关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="markId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationStartMarkResponse StartMark(Guid managerId, int markId);

        /// <summary>
        /// 根据关卡获取关卡信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationGetIsGeneralResponse GetMarkInfo(Guid managerId);

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns></returns>
        [OperationContract]
        RevelationTheGameResponse RevelationTheGame(Guid managerId);

        /// <summary>
        /// 获取翻牌信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationDrawResponse GetDrawInfo(Guid managerId, Guid drawId);

        /// <summary>
        /// 翻牌结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationDrawResultResponse DrawResult(Guid managerId, Guid drawId);

        /// <summary>
        /// 增加士气
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationAddChallengeResponse RevelationAddMorale(Guid managerId);

        /// <summary>
        /// 重置关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse RevelationResetMark(Guid managerId);

        /// <summary>
        /// 获取勇气商城信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationGetShopResponse RevelationGetShopInfo(Guid managerId);

        /// <summary>
        /// 勇气商城兑换物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationPurchaseItemsResponse RevelationPurchaseItems(Guid managerId, int idx);

        /// <summary>
        /// 勇气商城刷新物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationGetShopResponse RevelationShopRefresh(Guid managerId);
        #endregion

        #region 教练

        /// <summary>
        /// 获取教练列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        CoachGetListResponse GetCoachList(Guid managerId);

        /// <summary>
        /// 激活教练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        [OperationContract]
        CoachGetInfoResponse ActivationCoach(Guid managerId, int coachId);

        /// <summary>
        /// 替换启用的教练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        [OperationContract]
        CoachGetInfoResponse ReplaceCoach(Guid managerId, int coachId);

        /// <summary>
        /// 教练经验升级
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        [OperationContract]
        CoachGetInfoResponse CoachUpgrade(Guid managerId, int coachId);

        /// <summary>
        /// 教练技能升级
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        [OperationContract]
        CoachGetInfoResponse CoachSkillUpgrade(Guid managerId, int coachId);

        /// <summary>
        /// 教练升星
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        [OperationContract]
        CoachGetInfoResponse CoachStarUpgrade(Guid managerId, int coachId);

        #endregion

    }
}
