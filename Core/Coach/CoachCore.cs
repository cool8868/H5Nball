using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Coach;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Coach
{
    public class CoachCore
    {
        #region 初始化

        public CoachCore(int p)
        {

        }

        #endregion

        #region 单例

        public static CoachCore Instance
        {
            get { return SingletonFactory<CoachCore>.SInstance; }
        }

        #endregion

        #region 获取教练

        /// <summary>
        /// 获取教练信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public CoachFrame GetFrame(Guid managerId)
        {
            return new CoachFrame(managerId);
        }

        /// <summary>
        /// 获取教练列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public CoachGetListResponse GetCoachList(Guid managerId)
        {
            var response = new CoachGetListResponse();
            response.Data = new CoachGetListEntity();
            try
            {
                var frame = GetFrame(managerId);
                response.Data.CoachList = frame.GetCoachList();
                response.Data.EnableCoachId = frame.Entity.EnableCoachId;
                response.Data.SumExp = frame.Entity.HaveExp;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取教练列表", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #region 激活教练

        /// <summary>
        /// 激活教练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse ActivationCoach(Guid managerId, int coachId)
        {
            var response = new CoachGetInfoResponse();
            response.Data = new CoachGetInfoEntity();
            try
            {
                //获取教练配置
                var coachConfig = CacheFactory.CoachCache.GetCoachInfo(coachId);
                if (coachConfig == null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.NbParameterError);
                var frame = GetFrame(managerId);
                var coachInfo = frame.GetCoachInfo(coachId);
                if (coachInfo != null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.AlreadyHaveCoach);
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CoachActivation);
                //获取激活配置
                var starConfig = CacheFactory.CoachCache.GetCoachStarInfo(coachId, 0);
                if (starConfig == null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.NbParameterError);
                var itemCount = package.GetItemNumber(coachConfig.DebrisCode);
                //教练碎片不足
                if (itemCount < starConfig.ConsumeDebris)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.CoachDebrisNot);
                //激活教练
                if (!frame.TheActivation(coachId))
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.CoachAlreadyNotActivation);
                var messageCode = package.Delete(coachConfig.DebrisCode, starConfig.ConsumeDebris);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<CoachGetInfoResponse>(messageCode);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (!frame.Save(transactionManager.TransactionObject))
                            break;
                        messCode = MessageCode.Success;

                    } while (false);
                    if (messCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        package.Shadow.Save();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<CoachGetInfoResponse>((int) MessageCode.NbUpdateFail);
                    }
                }

                MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, true);
                response.Data.Kpi = ManagerCore.Instance.GetKpi(managerId);
                response.Data.CoachInfo = frame.GetCoachInfo(coachId);
                response.Data.EnableCoachId = frame.Entity.EnableCoachId;
                response.Data.SumExp = frame.Entity.HaveExp;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("激活教练", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #region 更换教练

        /// <summary>
        /// 更换教练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse ReplaceCoach(Guid managerId, int coachId)
        {
            var response = new CoachGetInfoResponse();
            response.Data = new CoachGetInfoEntity();
            try
            {
                var frame = GetFrame(managerId);
                //教练相同
                if (frame.Entity.EnableCoachId != coachId)
                {
                    //教练未激活
                    if (!frame.IsHaveCoach(coachId))
                        return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.CoachNotAlready);
                    if (!frame.ReplaceCoach(coachId))
                        return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.ChangeCoachFail);
                    if (!frame.Save())
                        return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.ChangeCoachFail);
                }
                response.Data.CoachInfo = frame.GetCoachInfo(coachId);
                response.Data.EnableCoachId = frame.Entity.EnableCoachId;
                response.Data.SumExp = frame.Entity.HaveExp;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("更换教练", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #region 教练升级

        /// <summary>
        /// 教练升级
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse CoachUpgrade(Guid managerId, int coachId)
        {
            var response = new CoachGetInfoResponse();
            response.Data = new CoachGetInfoEntity();
            try
            {
                var frame = GetFrame(managerId);
                var info = frame.GetCoachInfo(coachId);
                if (info == null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.CoachNotAlready);
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.NbParameterError);
                if (info.CoachLevel >= manager.Level)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.LackofManagerLevel);
                if (frame.Entity == null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.NbParameterError);
                if (frame.Entity.HaveExp <= 0)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.CoachExpNot);
                var costExp = frame.CoachUpgarde(coachId);
                if (!frame.Save())
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.NbUpdateFail);
                //插入扣除记录
                if (costExp > 0)
                    CoachManagerMgr.CostExpRecord(managerId, costExp);
                response.Data.CoachInfo = frame.GetCoachInfo(coachId);
                response.Data.EnableCoachId = frame.Entity.EnableCoachId;
                response.Data.SumExp = frame.Entity.HaveExp;

                MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, true);
                response.Data.Kpi = ManagerCore.Instance.GetKpi(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("教练升级", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 教练技能升级
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse CoachSkillUpgrade(Guid managerId, int coachId)
        {
            var response = new CoachGetInfoResponse();
            response.Data = new CoachGetInfoEntity();
            try
            {
                var frame = GetFrame(managerId);
                var info = frame.GetCoachInfo(coachId);
                if (info == null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.CoachNotAlready);
                if (info.IsMaxSkillLevel)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.CoachHaveMaxLevel);
                if (info.SkillLevel >= info.CoachLevel)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.CoachLevelNot);
                //获取升级配置
                var coachConfig = CacheFactory.CoachCache.GetCoachUpgradeInfo(info.SkillLevel);
                //获取最高可以升到多少级
                var starConfig = CacheFactory.CoachCache.GetCoachStarInfo(coachId, info.CoachStar);
                if (coachConfig == null || starConfig == null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.NbParameterError);
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.NbParameterError);
                if (manager.Coin < coachConfig.UpgradeSkillCoin)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.LackofCoin);
                int maxSkillLevel = starConfig.MaxLevel;
                frame.CoachSkillUpgrade(coachId, maxSkillLevel);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messCode = MessageCode.NbUpdateFail;
                    do
                    {
                        messCode = ManagerCore.Instance.CostCoin(manager, coachConfig.UpgradeSkillCoin,
                            EnumCoinConsumeSourceType.CoachModel, ShareUtil.GenerateComb().ToString(),
                            transactionManager.TransactionObject);
                        if (messCode != MessageCode.Success)
                            break;
                        if (!frame.Save(transactionManager.TransactionObject))
                            break;
                        messCode = MessageCode.Success;

                    } while (false);
                    if (messCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<CoachGetInfoResponse>((int)MessageCode.NbUpdateFail);
                    }
                }
                response.Data.CoachInfo = frame.GetCoachInfo(coachId);
                response.Data.EnableCoachId = frame.Entity.EnableCoachId;
                response.Data.SumExp = frame.Entity.HaveExp;

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("教练技能升级", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 教练升星
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse CoachStarUpgrade(Guid managerId, int coachId)
        {
            var response = new CoachGetInfoResponse();
            response.Data = new CoachGetInfoEntity();
            try
            {
                var frame = GetFrame(managerId);
                var info = frame.GetCoachInfo(coachId);
                if (info == null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.CoachNotAlready);
                if (info.IsMaxStar)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.MaxTheStar);
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CoachActivation);
                var coachConfig = CacheFactory.CoachCache.GetCoachInfo(coachId);
                var starConfig = CacheFactory.CoachCache.GetCoachStarInfo(coachId, info.CoachStar + 1);
                if (coachConfig == null || starConfig ==null)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.NbParameterError);
                //升星需要的物品
                var itemCode = coachConfig.DebrisCode;
                var itemCount = starConfig.ConsumeDebris;
                var haveCount = package.GetItemNumber(itemCode);
                if (haveCount < itemCount)
                    return ResponseHelper.Create<CoachGetInfoResponse>(MessageCode.ItemCountInvalid);
                var messageCode = package.Delete(itemCode, itemCount);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<CoachGetInfoResponse>(messageCode);
                messageCode = frame.CoachStarUpgrade(coachId); 
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<CoachGetInfoResponse>(messageCode);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (!frame.Save(transactionManager.TransactionObject))
                            break;
                        messCode = MessageCode.Success;

                    } while (false);
                    if (messCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        package.Shadow.Save();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<CoachGetInfoResponse>((int)MessageCode.NbUpdateFail);
                    }
                }

                MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, true);
                response.Data.Kpi = ManagerCore.Instance.GetKpi(managerId);
                response.Data.CoachInfo = frame.GetCoachInfo(coachId);
                response.Data.EnableCoachId = frame.Entity.EnableCoachId;
                response.Data.SumExp = frame.Entity.HaveExp;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("教练升星", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

    }
}
