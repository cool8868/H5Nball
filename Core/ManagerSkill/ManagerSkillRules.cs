using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.ManagerSkill;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.ManagerSkill
{
    public class ManagerSkillRules
    {
        #region 启动
        public static void StartService()
        {
            ManagerSkillCache.Instance();
        }
        #endregion

        #region 经理技能

        /// <summary>
        /// 玩家自己加天赋点.
        /// </summary>
        /// <param name="managerId">经理的guid.</param>
        /// <param name="skillCode">所加的技能编号.</param>
        /// <returns></returns>
        /// <remarks>
        /// 1.检查经理是否还有可用技能点
        /// 2.判断经理类型与该技能所属经理类型是否匹配
        /// 3.判断经理级别是否满足该技能要求
        /// 4.检查是否已学过该技能
        /// |--是 判断已加点是否小于该天赋点最多可加的点数
        /// 5.检查前置技能要求是否满足
        /// 6.检查在本系投入的技能点是否够
        /// </remarks>
        /// <history>
        /// [fuxiaogang]     2010-1-13 10:07     Created
        /// </history>
        public static ManagerTreeResponse AssignManagerSkill(Guid managerId, string skillCode)
        {
            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (managerExtra == null || manager == null)
                return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.AdMissManager);
            if (managerExtra.SkillType <= 1)
                return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.FirstTransfer);
            //1.是否还有可用技能点
            if (managerExtra.SkillPoint <= 0) // 技能点不足，无法加点天赋树
                return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.TalentHitLimitNumber);

            List<NbManagertreeEntity> skills = null;
            NbManagertreeEntity managertree = null;
            MessageCode returnCode = CheckManagerSkillCondition(manager, managerExtra, out skills, out managertree, skillCode);
            if (returnCode != MessageCode.Success)
                return ResponseHelper.Create<ManagerTreeResponse>(returnCode);
            var skillInfo = CacheFactory.ManagerDataCache.GetSkillTree(skillCode);
            var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(managerId);
            bool todoFlag = skillInfo.Opener != (int)EnumSkillDriveType.Passive;
            string talentsText = string.Empty;
            if (todoFlag)
            {
                lib.TodoTalents.Remove(skillCode);
                for (int i = 0; i <= skillInfo.MaxPoint; i++)
                {
                    lib.TodoTalents.Remove(string.Concat(skillCode, "_", i));
                }
                lib.TodoTalents[string.Concat(skillCode, "_", managertree.Points)] = 0;
                //talentsText = lib.TodoTalentsText;
            }
            else
            {
                lib.NodoTalents.Remove(skillCode);
                for (int i = 0; i <= skillInfo.MaxPoint; i++)
                {
                    lib.NodoTalents.Remove(string.Concat(skillCode, "_", i));
                }
                lib.NodoTalents[string.Concat(skillCode, "_", managertree.Points)] = 0;
                //talentsText = lib.NodoTalentsText;
            }
            foreach (var item in skills)
            {
                if (lib.NodoTalents.ContainsKey(item.SkillCode))
                {
                    lib.NodoTalents.Remove(item.SkillCode);
                    lib.NodoTalents[item.SkillCode + "_" + item.Points] = 0;
                }
                if (lib.TodoTalents.ContainsKey(item.SkillCode))
                {
                    lib.TodoTalents.Remove(item.SkillCode);
                    lib.TodoTalents[item.SkillCode + "_" + item.Points] = 0;
                }
            }
            var delDic = new Dictionary<string, byte>(); ;
            foreach (var item in lib.NodoTalents)
            {
                if (!item.Key.Contains('_'))
                    delDic[item.Key] = 0;
            }
            foreach (var item in lib.TodoTalents)
            {
                if (!item.Key.Contains('_'))
                    delDic[item.Key] = 1;
            }
            foreach (var kvp in delDic)
            {
                if (kvp.Value == 0)
                    lib.NodoTalents.Remove(kvp.Key);
                else
                    lib.TodoTalents.Remove(kvp.Key);
            }
            delDic.Clear();
            talentsText = todoFlag ? lib.TodoTalentsText : lib.NodoTalentsText;

            if (returnCode == MessageCode.Success)
            {
                bool isSuccess = SaveManagerSkillData(managerId, 1, managertree, lib, todoFlag, talentsText);
                if (isSuccess)
                {
                    TaskHandler.Instance.TalentSelect(managerId);
                    MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, true);
                    var rst = GetManagerTree(managerId);
                    if (null != rst)
                    {
                        if (todoFlag)
                        {
                            rst.Data.Kpi = -1;
                        }
                        else
                        {
                            var buff = BuffDataCore.Instance().RebuildMembers(managerId);
                            rst.Data.Kpi = buff.Kpi;
                        }
                    }
                    return rst;
                }
                return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.NbUpdateFail);
            }
            return ResponseHelper.Create<ManagerTreeResponse>(returnCode);
        }

        private static MessageCode CheckManagerSkillCondition(NbManagerEntity manager,NbManagerextraEntity managerExtra,out List<NbManagertreeEntity> skills, out NbManagertreeEntity managerTree, string skillCode)
        {
            skills = null;
            managerTree = null;

            //List<NbManagertreeEntity> skills;//该经理天赋树

            try
            {
                skills = NbManagertreeMgr.GetByManagerId(manager.Idx);
                var skillInfo = CacheFactory.ManagerDataCache.GetSkillTree(skillCode);

                #region check 2 3
                if (skillInfo == null)
                    return MessageCode.NbParameterError;

                //2.判断经理类型与该技能所属经理类型是否匹配
                if (managerExtra.SkillType != skillInfo.ManagerType)
                {
                    return MessageCode.MSkillTypeNotMatching;
                }

                //3.判断经理级别是否满足该技能要求
                if (manager.Level < skillInfo.RequireManagerLevel)
                {
                    return MessageCode.TalentHitLackofManagerLevel;
                }
                #endregion

                #region 该技能已学会,更新该技能点数
                foreach (var entity in skills)
                {
                    if (entity.SkillCode == skillCode)
                    {
                        if (entity.Points < skillInfo.MaxPoint) //该天赋点最多可加的点数
                        {
                            //更新技能点数
                            managerExtra.SkillPoint--;

                            //更新天赋树
                            entity.Points++;
                            managerTree = entity.Clone();
                            return MessageCode.Success;
                        }
                        else//该技能已达到可加的最大点数,无法再增加
                        {
                            return MessageCode.MSkillMaxPoint;
                        }
                    }
                }
                #endregion

                #region 该技能点未点过,检查前置条件，保存
                //check条件
                #region 前置技能是否已经达到要求 ,不满足就返回
                if (skillInfo.ConditionList != null && skillInfo.ConditionList.Count > 0)
                {
                    foreach (RequireCondition condition in skillInfo.ConditionList)
                    {
                        bool isMatch = false;
                        foreach (var entity in skills)
                        {
                            if (entity.SkillCode == condition.SkillCode)
                            {
                                if (entity.Points >= condition.Point) //前置技能点数不足
                                {
                                    isMatch = true;
                                }
                            }
                        }
                        if (!isMatch)
                        {
                            return MessageCode.MSkillNotRequireSeries;
                        }
                    }
                }
                #endregion

                #region 本系技能投入的技能点是否足够,不满足就返回
                if (skillInfo.ConditionPoint > 0)
                {
                    int hasPoint = 0;
                    foreach (var entity in skills)
                    {
                        if ( CacheFactory.ManagerDataCache.GetSkillTree(entity.SkillCode).Series == skillInfo.Series)
                        {
                            hasPoint += entity.Points;
                            if (hasPoint >= skillInfo.ConditionPoint) //已满足条件不需要再投入
                            {
                                break;
                            }
                        }
                    }
                    if (hasPoint < skillInfo.ConditionPoint)//本系技能投入的技能点不够
                    {
                        return MessageCode.MSkillNotRequireSeries;
                    }
                }
                #endregion

                //更新技能点数
                managerExtra.SkillPoint--;
                if (managerTree == null)
                {
                    managerTree = new NbManagertreeEntity();
                    managerTree.Idx = 0;
                    managerTree.ManagerId = manager.Idx;
                    managerTree.Points = 1;
                    managerTree.SkillCode = skillCode;
                    managerTree.Status = 0;
                    managerTree.RowTime = DateTime.Now;
                    managerTree.UpdateTime = DateTime.Now;
                }

                return MessageCode.Success;

                #endregion

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Check-ManagerSkill", ex.Message);
                return MessageCode.Exception;
            }
        }

        /// <summary>
        /// 获取技能树.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [fuxiaogang]     2010-1-13 15:08     Created
        /// </history>
        public static ManagerTreeResponse GetManagerTree(Guid managerId)
        {
            ManagerTreeResponse response = new ManagerTreeResponse();
            try
            {
                var managertrees = NbManagertreeMgr.GetByManagerId(managerId);
                var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
                var use = Games.NBall.Bll.Frame.ManagerUtil.GetSkillUseWrap(managerId);
                response.Data = new ManagerTree();
                response.Data.TreeList = managertrees;
                response.Data.UseToDoSkill = use.SetTalents;
                response.Data.SkillPoint = managerExtra.SkillPoint;
                response.Data.SkillType = managerExtra.SkillType;
                response.Data.Point = PayCore.Instance.GetPoint(managerId);
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetManagerTree", ex);
                return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 获取经理技能，不包含被动技能.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [fuxiaogang]     2010-1-21 18:15     Created
        /// </history>
        public static List<NbManagertreeEntity> GetManagerSkills(Guid managerId)
        {
            var managertrees = NbManagertreeMgr.GetByManagerId(managerId);
            var resultList = new List<NbManagertreeEntity>();
            try
            {
                //技能类型:0,被动技能;1,主动技能;2主动被动兼有
                foreach (var item in managertrees)
                {
                    var config = CacheFactory.ManagerDataCache.GetSkillTree(item.SkillCode);
                    if (config != null && config.Opener > 0)
                        resultList.Add(item);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetManagerSkills", ex.Message);
            }
            return resultList;
        }

        /// <summary>
        /// 设置天赋类型
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ManagerTreeResponse SetManagerTreeSkillType(Guid managerId, int type)
        {
            MessageCodeResponse response = new MessageCodeResponse();
            try
            {
                if (type < 2 || type > 4)
                    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.NbParameterError);
                var manager = ManagerCore.Instance.GetManager(managerId);
                var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
                if (manager == null || managerExtra == null)
                    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.MissManager);
                if (manager.Level < 10)
                    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.TalentHitLackofManagerLevel);
                if(managerExtra.SkillType >1)
                    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.HaveFirstTransfer);
                if(!NbManagerextraMgr.SetSkillType(managerId,type))
                    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.NbUpdateFail);
                return GetManagerTree(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SetManagerTreeSkillType", ex);
                return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 设置主动天赋
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="tids"></param>
        /// <returns></returns>
        public static ManagerTreeResponse SetSkillTree(Guid mid, string tids)
        {
            tids = tids ?? string.Empty;
            var talents = tids.Split(FlatTextFormatter.SPLITUnit);
            if (talents.Length > 2)
                return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.InvalidArgs);
            if (talents.Length == 2 && !string.IsNullOrEmpty(talents[0]) && talents[0] == talents[1])
                return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.TalentSetLimitRepeat);
            var use = Games.NBall.Bll.Frame.ManagerUtil.GetSkillUseWrap(mid);
            var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(mid);
            long managerLevel = FrameConvert.GetWorthValue(mid, EnumWorthType.ManagerLevel);
            SyncTalentPoint(lib.Raw, (int)managerLevel);
            var setTalents = use.SetTalents;
            var todoTalents = lib.TodoTalents;
            if (talents.Length > 0)
                setTalents[0] = talents[0];
            if (talents.Length > 1)
                setTalents[1] = talents[1];
            bool findFlag = false;
            foreach (string skill in setTalents)
            {
                if (string.IsNullOrEmpty(skill))
                    continue;
                //if (!todoTalents.ContainsKey(skill))
                //    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.TalentSetMiss);
                findFlag = false;
                foreach (var todoSkill in lib.TodoTalents)
                {
                    if (todoSkill.Key.StartsWith(skill, true, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        findFlag = true;
                        break;
                    }
                }
                if (!findFlag)
                    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.TalentSetMiss);
            }
            string setTalentsText = use.SetTalentsText;
            int errorCode = 0;
            ManagerskillUseMgr.SetTalent(mid, setTalentsText, use.Raw.RowVersion, ref errorCode);
            if (errorCode != (int)MessageCode.Success)
                return ResponseHelper.Create<ManagerTreeResponse>(errorCode);
            MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(mid, true);
            return GetManagerTree(mid);
        }

        private static bool SaveManagerSkillData(Guid managerId, int deductPoint, NbManagertreeEntity managertree, ManagerSkillLibWrap lib, bool todoFlag,string talentsText)
        {
            if (managertree == null) //check
                return false;
            bool isSuccess = false;
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.NbUpdateFail;
                    do
                    {
                        int errorCode = 0;
                        ManagerskillUseMgr.HitTalent(managerId, lib.Raw.SyncTalentPoint, 0, todoFlag, talentsText, lib.Raw.RowVersion, ref errorCode, transactionManager.TransactionObject);
                        if (errorCode != (int)MessageCode.Success)
                            break;
                        if (
                            !NbManagerextraMgr.ToDeductSkillPoint(managerId, deductPoint,
                                transactionManager.TransactionObject))
                            break;
                        if (managertree.Idx == 0)
                        {
                            if (!NbManagertreeMgr.Insert(managertree, transactionManager.TransactionObject))
                                break;
                        }
                        else
                        {
                            if (!NbManagertreeMgr.Update(managertree, transactionManager.TransactionObject))
                                break;
                        }
                        isSuccess = true;
                        break;
                    } while (false);
                    if (isSuccess)
                        transactionManager.Commit();
                    else
                        transactionManager.Rollback();
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("HonorMallTransaction-SaveCardExchangeData", ex.Message);
            }
            return isSuccess;
        }

        #endregion

        #region 洗点转职 By Kevin
      
        #region 天赋重置
        /// <summary>
        /// 重置天赋
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public static ManagerTreeResponse ResetSkillPoint(Guid managerId)
        {
            MessageCodeResponse rst = new MessageCodeResponse();
            try
            {
                var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (null == manager || managerExtra == null)
                    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.MissManager);
                var point = PayCore.Instance.GetPoint(managerId);
                if (point < ManagerSkillConfig.MSKILLPriceE4ResetTalent)
                    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.NbPointShortage);
                var sumSkillPoint = CacheFactory.ManagerDataCache.GetSumSkillPointByLevel(manager.Level);
                if (managerExtra.SkillType <=1)
                    return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.TalentResetMiss);
                int addSkillPoint = sumSkillPoint - managerExtra.SkillPoint;
                var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(managerId);
                int managerHash = ShareUtil.GetTableMod(managerId);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.NbUpdateFail;
                    do
                    {
                        int errorCode = 0;
                        ManagerskillUseMgr.ResetTalent(managerId, managerHash,manager.Account, 0, 0, ShareUtil.GenerateComb(), 0,new byte[0],
                0, null, lib.Raw.RowVersion, ref errorCode);
                        if (errorCode != (int) MessageCode.Success)
                            break;
                        messageCode = PayCore.Instance.GambleConsume(managerId, ManagerSkillConfig.MSKILLPriceE4ResetTalent,
                            ShareUtil.GenerateComb(), EnumConsumeSourceType.ResetTree,
                            transactionManager.TransactionObject);
                        if (messageCode != MessageCode.Success)
                            break;
                        if (
                            !NbManagerextraMgr.AddSkillPoint(managerId, addSkillPoint,
                                transactionManager.TransactionObject))
                            break;
                        if (!NbManagerextraMgr.SetSkillType(managerId,1, transactionManager.TransactionObject))
                            break;
                        if (!NbManagertreeMgr.DeleteManagerTree(managerId, transactionManager.TransactionObject))
                            break;
                        messageCode = MessageCode.Success;
                        break;
                    } while (false);
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, true);
                    }
                    else
                        transactionManager.Rollback();
                }
                var rep = GetManagerTree(managerId);
                rep.Code = (int)MessageCode.Success;
                if (null != rep.Data)
                {
                    var buff = BuffDataCore.Instance().RebuildMembers(managerId);
                    rep.Data.Kpi = buff.Kpi;
                }
                return rep;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SkillRules:ResetSkillPoint", ex.Message);
                return ResponseHelper.Create<ManagerTreeResponse>(MessageCode.NbParameterError);
            }
        }
        #endregion

        #region 后台天赋重置
        public static bool ResetSkillPoint4GM(Guid managerId)
        {
            try
            {
                var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (null == manager || managerExtra == null)
                    return false;
                
                var sumSkillPoint = CacheFactory.ManagerDataCache.GetSumSkillPointByLevel(manager.Level);
                if (managerExtra.SkillType <=1)
                    return false;
                int addSkillPoint = sumSkillPoint - managerExtra.SkillPoint;
                var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(managerId);
                int managerHash = ShareUtil.GetTableMod(managerId);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.NbUpdateFail;
                    do
                    {
                        int errorCode = 0;
                        ManagerskillUseMgr.ResetTalent(managerId, managerHash, manager.Account, 0, 0, ShareUtil.GenerateComb(), 0, new byte[0],
                0, null, lib.Raw.RowVersion, ref errorCode);
                        if (errorCode != (int)MessageCode.Success)
                            break;
                        if (!NbManagerextraMgr.AddSkillPoint(managerId, addSkillPoint,
                                transactionManager.TransactionObject))
                            break;
                        if (!NbManagertreeMgr.DeleteManagerTree(managerId, transactionManager.TransactionObject))
                            break;
                        if (!NbManagerextraMgr.SetSkillType(managerId, 1, transactionManager.TransactionObject))
                            break;
                        messageCode = MessageCode.Success;
                        break;
                    } while (false);
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, true);
                    }
                    else
                        transactionManager.Rollback();
                }
                return true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SkillRules:ResetSkillPoint4GM", ex.Message);
                return false;
            }
        }
        #endregion

        #endregion

        #region 天赋列表
        public static
            RootResponse<DTOTalentView> GetTalentList(Guid mid)
        {
            var worthSrc = FrameConvert.GetWorthSource(mid, true, true);
            int managerLevel = 0;
            if (null != worthSrc.Manager)
                managerLevel = worthSrc.Manager.Level;
            var worths = FrameConvert.GetWorthList(mid, worthSrc, EnumWorthType.Gold);
            var use = Games.NBall.Bll.Frame.ManagerUtil.GetSkillUseWrap(mid);
            var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(mid);
            SyncTalentPoint(lib.Raw, managerLevel);
            var data = ManagerSkillConvert.ConvertToTalentView(lib, use);
            data.WorthList = worths;
            return ResponseHelper.CreateRoot<DTOTalentView>(data);
        }


        public static bool HasUnusedTalent(Guid mid)
        {
            var worthSrc = FrameConvert.GetWorthSource(mid, true, true);
            int managerLevel = 0;
            if (null != worthSrc.Manager)
                managerLevel = worthSrc.Manager.Level;
            var use = Games.NBall.Bll.Frame.ManagerUtil.GetSkillUseWrap(mid);
            var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(mid);
            SyncTalentPoint(lib.Raw, managerLevel);
            var data = ManagerSkillConvert.ConvertToTalentView(lib, use);

            if (data.MaxTalentPoint > data.CntTalentPoint)
                return true;
            return false;
        }

        #endregion

        #region 选取天赋
        public static RootResponse<DTOTalentView> HitTalent(Guid mid, string tid, bool hasTask)
        {
            DicManagertalentEntity cfg;
            if (!ManagerSkillCache.Instance().TryGetTalent(tid, out cfg))
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentMissConfig);
            var manager = NbManagerMgr.GetById(mid);
            if (null == manager)
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.MissManager);
            //获取已经学到的天赋和意志
            var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(mid);
            if (lib.NodoTalents.ContainsKey(tid) || lib.TodoTalents.ContainsKey(tid))
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentHitExisits);
            //未达到开发条件
            if (manager.Level < cfg.ReqManagerLevel)
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentHitLackofManagerLevel);
            List<string> steps;
            //获取天赋选取阶段号
            if (!ManagerSkillCache.Instance().TryGetStepTalents(cfg.StepNo, out steps))
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentMissConfig);
            foreach (string skill in steps)//查找本阶段有没有选取天赋
            {
                if (skill == tid)
                    continue;
                if (lib.NodoTalents.ContainsKey(skill) || lib.TodoTalents.ContainsKey(skill))
                    return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentHitLimitStep);
            }
            //当前经理等级可学习的天赋
            if (!SyncTalentPoint(lib.Raw, manager.Level))
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentMissConfig);
            int skillPoint = lib.Raw.MaxTalentPoint;
            //技能点
            if (lib.NodoTalents.Count + lib.TodoTalents.Count >= skillPoint)
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentHitLimitNumber);
            //是否是主动技能
            bool todoFlag = cfg.TodoFlag();
            string talentsText = string.Empty;
            if (todoFlag)
            {
                lib.TodoTalents[tid] = 0;
                talentsText = lib.TodoTalentsText;
            }
            else
            {
                lib.NodoTalents[tid] = 0;
                talentsText = lib.NodoTalentsText;
            }
            using (var tranMgr = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                tranMgr.BeginTransaction();
                int errorCode = (int)MessageCode.FailUpdate;
                do
                {
                    ManagerskillUseMgr.HitTalent(mid, lib.Raw.SyncTalentPoint, skillPoint, todoFlag, talentsText, lib.Raw.RowVersion, ref errorCode, tranMgr.TransactionObject);
                    if (errorCode != (int)MessageCode.Success)
                        break;
                }
                while (false);
                if (errorCode != (int)MessageCode.Success)
                {
                    tranMgr.Rollback();
                    return ResponseHelper.CreateRoot<DTOTalentView>(errorCode);
                }
                tranMgr.Commit();
            }
            var data = new DTOTalentView();
            data.MaxTalentPoint = skillPoint;
            data.CntTalentPoint = lib.NodoTalents.Count + lib.TodoTalents.Count;
            var list = new List<string>(1);
            list.Add(tid);
            if (todoFlag)
                data.TodoTalents = list;
            else
                data.NodoTalents = list;

            data.PopMsg = TaskHandler.Instance.TalentSelect(mid);

            SyncKpi(mid, tid);
            return ResponseHelper.CreateRoot<DTOTalentView>(data);
        }
        #endregion

        #region 设置天赋
        public static RootResponse<DTOTalentView> SetTalent(Guid mid, string tids)
        {
            tids = tids ?? string.Empty;
            var talents = tids.Split(FlatTextFormatter.SPLITUnit);
            if (talents.Length > 2)
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.InvalidArgs);
            if (talents.Length == 2 && !string.IsNullOrEmpty(talents[0]) && talents[0] == talents[1])
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentSetLimitRepeat);
            var use = Games.NBall.Bll.Frame.ManagerUtil.GetSkillUseWrap(mid);
            var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(mid);
            long managerLevel = FrameConvert.GetWorthValue(mid, EnumWorthType.ManagerLevel);
            SyncTalentPoint(lib.Raw, (int)managerLevel);
            var setTalents = use.SetTalents;
            var todoTalents = lib.TodoTalents;
            if (talents.Length > 0)
                setTalents[0] = talents[0];
            if (talents.Length > 1)
                setTalents[1] = talents[1];
            foreach (string skill in setTalents)
            {
                if (string.IsNullOrEmpty(skill))
                    continue;
                if (!todoTalents.ContainsKey(skill))
                    return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentSetMiss);
            }
            string setTalentsText = use.SetTalentsText;
            int errorCode = 0;
            ManagerskillUseMgr.SetTalent(mid, setTalentsText, use.Raw.RowVersion, ref errorCode);
            if (errorCode != (int)MessageCode.Success)
                return ResponseHelper.CreateRoot<DTOTalentView>(errorCode);
            var data = ManagerSkillConvert.ConvertToTalentView(lib, use);
            return ResponseHelper.CreateRoot<DTOTalentView>(data);
        }
        #endregion

        #region 重置天赋
        public static RootResponse<DTOTalentView> ResetTalent(Guid mid)
        {
            var worthSrc = FrameConvert.GetWorthSource(mid, true, true);
            int managerLevel = 0;
            if (null != worthSrc.Manager)
                managerLevel = worthSrc.Manager.Level;
            var worths = FrameConvert.GetWorthList(mid, worthSrc, EnumWorthType.Gold);
            worths[0].CostValue = ManagerSkillConfig.MSKILLPriceE4ResetTalent;
            if (worths[0].LackFlag)
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.LackofGold);
            var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(mid);
            if (lib.NodoTalents.Count + lib.TodoTalents.Count == 0)
                return ResponseHelper.CreateRoot<DTOTalentView>(MessageCode.TalentResetMiss);
            SyncTalentPoint(lib.Raw, managerLevel);
            int managerHash = ShareUtil.GetTableMod(mid);
            int costGoldItemNo = ManagerSkillConfig.MSKILLGoldItem4ResetTalent;
            int errorCode = 0;
            ManagerCore.Instance.DeleteCache(mid);
            ManagerskillUseMgr.ResetTalent(mid, managerHash, worthSrc.Manager.Account, worths[0].CostValue, costGoldItemNo, ShareUtil.GenerateComb(), 0, worthSrc.Account.RowVersion,
                0, null, lib.Raw.RowVersion, ref errorCode);
            if (errorCode != (int)MessageCode.Success)
                return ResponseHelper.CreateRoot<DTOTalentView>(errorCode);
            worths[0].Cost();
            lib.NodoTalents.Clear();
            lib.TodoTalents.Clear();
            var data = ManagerSkillConvert.ConvertToTalentView(lib, null);
            data.WorthList = worths;
            return ResponseHelper.CreateRoot<DTOTalentView>(data);
        }
        #endregion

        #region 意志列表
        public static RootResponse<DTOWillView> GetWillList(Guid mid)
        {
            var packRaw = ItemPackageMgr.GetById(mid);
            if (null == packRaw)
                return ResponseHelper.CreateRoot<DTOWillView>(MessageCode.MissManager);
            var ownCards = GetOwnCards(new ItemPackageFrame(packRaw));
            var putCards = GetPutCards(mid);
            var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(mid);
            var dicHigh = lib.HighWills;
            var dicLow = lib.LowWills;
            var setWills = Games.NBall.Bll.Frame.ManagerUtil.GetSkillUseWrap(mid).SetWills;
            DTOWillItem willItem = null;
            Dictionary<int, DTOWillPartItem> willPut = null;
            var highWills = new List<DTOWillItem>();
            var lowWills = new List<DTOWillItem>();
            string willCode = string.Empty;
            bool enableFlag = false;
            foreach (var cfg in ManagerSkillCache.Instance().GetWillList())
            {
                willCode = cfg.SkillCode;
                if (cfg.HighFlag())
                    enableFlag = dicHigh.TryGetValue(willCode, out willItem);
                else
                {
                    enableFlag = dicLow.ContainsKey(willCode);
                    willItem = null;
                }
                putCards.TryGetValue(willCode, out willPut);
                if (enableFlag)
                    willItem = FillWillEnable(willCode, willItem, willPut);
                else
                    willItem = FillWillFlag(cfg, willItem, willPut, ownCards);
                if (null == willItem)
                    continue;
                if (!cfg.HighFlag())
                {
                    lowWills.Add(willItem);
                    continue;
                }
                if (enableFlag && setWills.ContainsKey(willCode))
                    willItem.EnableState = 2;
                highWills.Add(willItem);
            }
            ownCards.Clear();
            putCards.Clear();
            dicHigh.Clear();
            dicLow.Clear();
            var data = new DTOWillView();
            data.HighWills = highWills;
            data.LowWills = lowWills;
            return ResponseHelper.CreateRoot<DTOWillView>(data);
        }
        #endregion

        #region 收集意志
        public static RootResponse<DTOWillItemView> PutWill(Guid mid, string wid, Guid cid, bool hasTask)
        {
            bool succFlag = false;
            TransactionShadow shadow = null;
            try
            {
                DicManagerwillEntity cfg;
                if (!ManagerSkillCache.Instance().TryGetWill(wid, out cfg))
                    return ResponseHelper.CreateRoot<DTOWillItemView>(MessageCode.WillMissConfig);
                var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(mid);
                if (lib.LowWills.ContainsKey(wid) || lib.HighWills.ContainsKey(wid))
                    return ResponseHelper.CreateRoot<DTOWillItemView>(MessageCode.WillPutExists);
                shadow = new TransactionShadow(mid, Games.NBall.Entity.Enums.Shadow.EnumTransactionType.WillCollectCard);
                ItemPackageFrame pack = null;
                ItemInfoEntity card = null;
                PlayerCardProperty cardProp = null;
                do
                {
                    var packRaw = ItemPackageMgr.GetById(mid);
                    if (null == packRaw)
                        break;
                    pack = new ItemPackageFrame(packRaw, shadow);
                    card = pack.GetItem(cid);
                    if (null != card)
                        cardProp = card.ItemProperty as PlayerCardProperty;
                }
                while (false);
                if (null == card || null == cardProp)
                    return ResponseHelper.CreateRoot<DTOWillItemView>(MessageCode.WillPutMissCard);
                if(cardProp.IsMain)
                    return ResponseHelper.CreateRoot<DTOWillItemView>(MessageCode.WillCardMain);
                if (card.Status == (int)EnumItemStatus.Locked)
                    return ResponseHelper.CreateRoot<DTOWillItemView>(MessageCode.ItemIsLocked);
                //检查球员卡训练状态
                pack.CheckPlayerTrain();
                if(cardProp.IsTrain)
                    return ResponseHelper.CreateRoot<DTOWillItemView>(MessageCode.TeammemberTrainSkill);
                int itemCode = card.ItemCode;
                var src = ManagerSkillConvert.GetWillSrcWrap(mid, wid);
                if (src.PartList.ContainsKey(itemCode))
                    return ResponseHelper.CreateRoot<DTOWillItemView>(MessageCode.WillPutExistsCard);
                int reqStrength = 0;
                if (!cfg.DicPid.TryGetValue(itemCode, out reqStrength))
                    return ResponseHelper.CreateRoot<DTOWillItemView>(MessageCode.WillPutUnfitCard);
                if (cardProp.Strength < reqStrength)
                    return ResponseHelper.CreateRoot<DTOWillItemView>(MessageCode.WillPutUnfitCardStrength);
                src.PartList[itemCode] = new DTOWillPartItem()
                {
                    ItemId = FrameUtil.GenChar32Id(card.ItemId),
                    ItemCode = itemCode,
                    PutStrength = cardProp.Strength,
                };
                bool todoFlag = cfg.HighFlag();
                string partMap = src.PartListText;
                string libWills = string.Empty;
                string useWills = string.Empty;
                var ownCards = GetOwnCards(pack);
                var data = FillWillFlag<DTOWillItemView>(cfg, null, src.PartList, ownCards);
                do
                {
                    if (data.EnableState == 0)
                        break;
                    if (todoFlag)
                    {
                        lib.HighWills[wid] = data;
                        libWills = lib.HigthWillsText;
                    }
                    else
                    {
                        lib.LowWills[wid] = 0;
                        libWills = lib.LowWillsText;
                    }
                    if (!todoFlag)
                        break;
                    var use = Games.NBall.Bll.Frame.ManagerUtil.GetSkillUseWrap(mid);
                    if (use.SetWills.Count >= lib.Raw.MaxWillNumber)
                        break;
                    data.EnableState = 2;
                    use.SetWills[wid] = 0;
                    useWills = use.SetWillsText;
                }
                while (false);
                pack.Delete(card);
                using (var tranMgr = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    tranMgr.BeginTransaction();
                    int errorCode = (int)MessageCode.FailUpdate;
                    do
                    {
                        if (!pack.Save(tranMgr.TransactionObject))
                            break;
                        ManagerskillUseMgr.PutWill(mid, wid, partMap, data.EnableState, src.Raw.RowVersion,
                            lib.Raw.MaxWillNumber, todoFlag, useWills, libWills, lib.Raw.RowVersion, ref errorCode, tranMgr.TransactionObject);
                    }
                    while (false);
                    if (errorCode != (int)MessageCode.Success)
                    {
                        tranMgr.Rollback();
                        return ResponseHelper.CreateRoot<DTOWillItemView>(errorCode);
                    }
                    tranMgr.Commit();
                }
                if (!todoFlag && data.EnableState == 1
                    || todoFlag && data.EnableState == 2)
                    SyncKpi(mid, wid);
                if (hasTask)
                {
                    if (data.EnableState > 0)
                    {
                        int cntHigh = lib.HighWills.Count;
                        int cntLow = lib.LowWills.Count;
                       
                    }
                }
                succFlag = true;
                return ResponseHelper.CreateRoot<DTOWillItemView>(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (null != shadow && succFlag)
                        shadow.Save();
                }
                catch { }
            }
        }
        #endregion

        #region 设置意志
        public static MessageCodeResponse SetWill(Guid mid, string wid, bool enableFlag)
        {
            DicManagerwillEntity cfg;
            if (!ManagerSkillCache.Instance().TryGetWill(wid, out cfg))
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.WillMissConfig);
            if (!cfg.HighFlag())
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.WillSetUnable);
            var lib = Games.NBall.Bll.Frame.ManagerUtil.GetSkillLibWrap(mid);
            if (!lib.HighWills.ContainsKey(wid))
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.WillSetMiss);
            var use = Games.NBall.Bll.Frame.ManagerUtil.GetSkillUseWrap(mid);
            if (enableFlag)
            {
                use.SetWills[wid] = 0;
                if (use.SetWills.Count > lib.Raw.MaxWillNumber)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.WillSetLimitNumber);
            }
            else
                use.SetWills.Remove(wid);
            string useWills = use.SetWillsText;
            int errorCode = 0;
            ManagerskillUseMgr.SetWill(mid, useWills, use.Raw.RowVersion, ref errorCode);
            if (errorCode != (int)MessageCode.Success)
                return ResponseHelper.Create<MessageCodeResponse>(errorCode);
            SyncKpi(mid, wid);
            return ResponseHelper.CreateSuccess<MessageCodeResponse>();
        }
        #endregion

        #region 意志任务
        ///// <summary>
        ///// 扫描意志任务
        ///// </summary>
        ///// <param name="mid">经理id</param>
        ///// <returns></returns>
        //public List<PopMessageEntity> CheckWillTask(Guid mid)
        //{
        //    int lowNum, highNum;
        //    bool allFlag;
        //    CheckWillNum(out lowNum, out highNum, out allFlag, mid);
        //    var rst = TaskHandler.Instance.LowWillCount(mid, lowNum);
        //    var lst = TaskHandler.Instance.HighWillCount(mid, highNum);
        //    if (null == rst)
        //        rst = new List<PopMessageEntity>();
        //    if (null != lst)
        //        rst.AddRange(lst);
        //    if (!allFlag)
        //        return rst;
        //    lst = TaskHandler.Instance.AllWill(mid);
        //    if (null != lst)
        //        rst.AddRange(lst);
        //    return rst;
        //}
        ///// <summary>
        ///// 获取已收集的意志数目
        ///// </summary>
        ///// <param name="lowNum">已收集的低级意志数目</param>
        ///// <param name="highNum">已收集的高级意志数目</param>
        ///// <param name="allFlag">是否全部收集完成</param>
        ///// <param name="mid">经理Id</param>
        //public void CheckWillNum(out int lowNum, out int highNum, out bool allFlag, Guid mid)
        //{
        //    lowNum = highNum = 0;
        //    allFlag = false;
        //    var lib = ManagerUtil.GetSkillLibWrap(mid);
        //    lowNum = lib.LowWills.Count;
        //    highNum = lib.HighWills.Count;
        //    allFlag = lowNum >= ManagerSkillCache.Instance().CountLowWill
        //                && highNum >= ManagerSkillCache.Instance().CountHighWill;
        //}
        #endregion

        #region Native
        static bool SyncTalentPoint(ManagerskillLibEntity lib, int managerLevel)
        {
            int nextLevel = lib.SyncTalentPoint;
            int skillPoint = lib.MaxTalentPoint;
            if (managerLevel < nextLevel)
                return true;
            if (!ManagerSkillCache.Instance().CheckTalentPoint(managerLevel, out skillPoint, out nextLevel))
                return false;
            lib.SyncTalentPoint = nextLevel;
            lib.MaxTalentPoint = skillPoint;
            return true;
        }
        static Dictionary<int, int> GetOwnCards(ItemPackageFrame pack)
        {
            var list = pack.GetItemsByType((int)EnumItemType.PlayerCard);
            if (null == list)
                return new Dictionary<int, int>(0);
            var dic = new Dictionary<int, int>(list.Count);
            list.ForEach(i => dic[i.ItemCode] = 0);
            list.Clear();
            return dic;
        }
        static Dictionary<string, Dictionary<int, DTOWillPartItem>> GetPutCards(Guid managerId)
        {
            var list = ManagerskillWillsrcMgr.GetWillSrcList(managerId);
            var dic = new Dictionary<string, Dictionary<int, DTOWillPartItem>>(list.Count);
            foreach (var item in list)
            {
                dic[item.SkillCode] = new ManagerWillSrcWrap(item).PartList;
            }
            list.Clear();
            return dic;
        }
        static DTOWillItem FillWillEnable(string willCode, DTOWillItem will, Dictionary<int, DTOWillPartItem> putCards)
        {
            if (null == will)
            {
                will = new DTOWillItem()
                {
                    WillCode = willCode,
                };
            }
            will.EnableState = 1;
            will.HintFlag = false;
            if (null != putCards)
                will.PartList = putCards.Values.ToList();
            return will;
        }
        static T FillWillFlag<T>(DicManagerwillEntity cfg, T will, Dictionary<int, DTOWillPartItem> putCards, Dictionary<int, int> ownCards)
            where T : DTOWillItem, new()
        {
            int enableState = 1;
            bool hintFlag = false;
            foreach (int itemCode in cfg.DicPid.Keys)
            {
                if (null != putCards && putCards.ContainsKey(itemCode))
                    continue;
                enableState = 0;
                if (!ownCards.ContainsKey(itemCode))
                    continue;
                hintFlag = true;
                if (null == putCards)
                    putCards = new Dictionary<int, DTOWillPartItem>();
                putCards[itemCode] = new DTOWillPartItem()
                {
                    ItemCode = itemCode,
                    PutStrength = 0,
                };
            }
            if (null == putCards)
                return null;
            if (null == will)
                will = new T() { WillCode = cfg.SkillCode };
            will.EnableState = enableState;
            will.HintFlag = hintFlag;
            will.PartList = putCards.Values.ToList();
            return will;
        }
        static void SyncKpi(Guid managerId, string skillCode)
        {
            DicSkillEntity cfg;
            if (!BuffCache.Instance().TryGetSkill(out cfg, skillCode))
                return;
            if ((cfg.AsLiveFlag & EnumSkillLiveFlag.Firm) > 0)
                KpiHandler.Instance.RebuildKpi(managerId, false);
        }
        #endregion

    }
}
