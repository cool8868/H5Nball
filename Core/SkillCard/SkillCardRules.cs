using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
//using Games.NBall.Core.Active;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Config.Custom;
//using Games.NBall.Entity.Response.Frame;
using Games.NBall.Entity.Response.SkillCard;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Core.Item;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.SkillCard
{
    public class SkillCardRules
    {
        #region 启动
        public static void StartService()
        {
            SkillCardCache.Instance();
        }
        #endregion

        /// <summary>
        /// 更新技能 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static MessageCode SetSkillMapByManagerLevel(NbManagerEntity manager, DbTransaction transaction)
        {
            var bag = SkillCardConvert.GetSkillBagWrap(manager.Idx);

            var skillList = SkillCardCache.Instance().GetSkillCardByManagerLevel(manager.Level);
            var unLearnedList = new List<DTOSkillSetItem>();
            foreach (var entity in skillList)
            {
                if (!bag.SetList.ContainsKey(entity.SkillRoot))
                {
                    DTOSkillSetItem onItem  = SkillCardConvert.GetNewSkillCardOn(entity);
                    unLearnedList.Add(onItem);
                }
            }
            
            if (unLearnedList.Count > 0)
            {
                string onItemMap = FlatTextFormatter.ListToText(unLearnedList, SkillBagWrap.SPLITSect, SkillBagWrap.SPLITUnit);
                int errorCode = 0;
                NbManagerskillbagMgr.Add(manager.Idx, onItemMap, bag.RawBag.RowVersion, ref errorCode, transaction);
                if (errorCode != (int) MessageCode.Success)
                {
                    return (MessageCode) errorCode;
                }
            }
            return MessageCode.Success;
        }
        #region 技能升级
        /// <summary>
        /// 技能升级
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static RootResponse<DTOSkillSetView> UseSkillExp(Guid mid, string cid)
        {
            if (string.IsNullOrEmpty(cid) || !FrameUtil.CheckChar22Id(cid))
                return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillInvalidCid);
            
            //var package = ItemCore.Instance.GetPackage(mid, EnumTransactionType.MixSkillExpCard);
            //if (package == null)
            //    return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.NbParameterError);

            //var item = package.GetByItemCode(310110);
            //if (item == null)
            //    return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillExpNotFind);
            
            var bag = SkillCardConvert.GetSkillBagWrap(mid);
            var onlib = bag.SetList;
            var dstItem = onlib.Values.FirstOrDefault(i => i.ItemId == cid);
            if (null == dstItem)
                return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillMissCard);
            string rawCode = dstItem.ItemCode;
            if (dstItem.Cfg.SkillLevel <= 0)
                dstItem.Cfg.SkillLevel = 1;
            if (dstItem.Cfg.SkillLevel >= SkillCardConfig.SKILLCardMaxCardLevel)
                return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillMixOverCardLevel);
            var config = SkillCardConfig.GetSkillUpgrade(dstItem.Cfg.SkillLevel + 1, dstItem.Cfg.SkillClass);
            if(config == null)
                return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillMissConfig);

            var manager = ManagerCore.Instance.GetManager(mid);
            if (manager == null)
                return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.AdMissManager);
            if (manager.Coin < config.Coin)
                return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.LackofCoin);
            int newLevel = 1;
            if (InnerDealMix(dstItem, out newLevel))
            {
                string setSkills = string.Empty;
                if (dstItem.ItemCode != rawCode)//升级了
                {
                    setSkills = bag.SetSkillsTextFromLib();
                    MemcachedFactory.SolutionClient.Delete(mid);
                }
                var errCode = InnerSaveMixNew(manager, mid, bag, config.Coin, setSkills);
                if (errCode == MessageCode.Success)
                {
                    var response = GetSkillSetInfo(mid);
                    response.Data.Coin = manager.Coin;
                    return response;
                }
            }

            return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.FailUpdate);
        }

        static bool InnerDealMix(DTOSkillCardItem dst, out int newLevel)
        {
            newLevel = 0;
            if (null == dst || !SkillCardConvert.FillSkillCardConfig(dst, true))
                return false;
            newLevel = dst.Cfg.SkillLevel+1;
            //int addExp = dst.Exp + 10;
            //if (!SkillCardCache.Instance().TryCheckSkillLevel(dst.Cfg.SkillClass, addExp, out newLevel))
            //    return false;
            //if (dst.Cfg.SkillLevel == newLevel)
            //{
            //    dst.Exp = addExp;
            //    return true;
            //}
            string newCode = string.Empty;
            if (!SkillCardCache.Instance().TryGetSkillCode(dst.Cfg.SkillRoot, newLevel, out newCode))
                return false;
           // dst.Exp = addExp;
            dst.ItemCode = newCode;
            SkillCardConvert.FillSkillCardConfig(dst, true);
            return true;
        }

        static MessageCode InnerSaveMix(Guid mid, ItemPackageFrame pack, SkillBagWrap bag, ItemInfoEntity mixItems, string setSkills = null)
        {
            string itemMap = mixItems.ItemId.ToString();
            int errorCode = (int)MessageCode.FailUpdate;
            using (var tranMgr = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                tranMgr.BeginTransaction();
                do
                {
                    if (null != pack && !pack.Save(tranMgr.TransactionObject))
                        break;
                    NbManagerskillbagMgr.MixUpTran(false, mid, setSkills, bag.SetMap, itemMap,bag.RawBag.RowVersion, ref errorCode);
                }
                while (false);
                if (errorCode == (int)MessageCode.Success)
                    tranMgr.Commit();
                else
                    tranMgr.Rollback();
            }
            itemMap = null;
            return (MessageCode)errorCode;
        }

        static MessageCode InnerSaveMixNew(NbManagerEntity manager,Guid mid, SkillBagWrap bag, int coin,string setSkills = null)
        {
            int errorCode = (int)MessageCode.FailUpdate;
            using (var tranMgr = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                tranMgr.BeginTransaction();
                do
                {
                    var mess = ManagerCore.Instance.CostCoin(manager, coin, EnumCoinConsumeSourceType.SkillUpgrade,
                        ShareUtil.GenerateComb().ToString(), tranMgr.TransactionObject);
                    if (mess != MessageCode.Success)
                        break;
                    NbManagerskillbagMgr.MixUpTran(false, mid, setSkills, bag.SetMap, "", bag.RawBag.RowVersion, ref errorCode);
                }
                while (false);
                if (errorCode == (int)MessageCode.Success)
                    tranMgr.Commit();
                else
                    tranMgr.Rollback();
            }
            return (MessageCode)errorCode;
        }
        #endregion

        #region 设置列表
        public static RootResponse<DTOSkillSetView> GetSkillSetInfo(Guid mid)
        {
            var bag = SkillCardConvert.GetSkillBagWrap(mid);
            int managerLevel = (int)FrameConvert.GetWorthValue(mid, EnumWorthType.ManagerLevel);
            var data = new DTOSkillSetView();
            data.MaxSetCells = GetMaxSkillCells(managerLevel);
            var form = MatchDataHelper.GetSolution(mid);
            if(null!=form)
            {
                data.FormId = form.FormationId;
                data.PidStr = form.PlayerString.TrimEnd();
            }
            data.SetList = bag.GetShowSet();
            data.CntSetCells = bag.CntSetNum;
            return ResponseHelper.CreateRoot<DTOSkillSetView>(data);
        }
        #endregion

        #region 技能设置
        public static RootResponse<DTOSkillSetView> SkillSet(Guid mid, string cids,bool hasTask)
        {
            if (string.IsNullOrEmpty(cids) || cids.Length >= 400)
                return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillSetInvalidArgs);
            string[] skills = cids.Split(',');
            if (skills.Length != SkillCardConfig.SKILLCardMAXSkillCellSize)
                return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillSetInvalidArgs);
            int managerLevel = (int)FrameConvert.GetWorthValue(mid, EnumWorthType.ManagerLevel);
            int maxCells = GetMaxSkillCells(managerLevel);
            var bag = SkillCardConvert.GetSkillBagWrap(mid);
            var onlib = new Dictionary<string, DTOSkillSetItem>(bag.SetList.Count);
            foreach (var item in bag.SetList.Values)
            {
                item.Index = 0;
                onlib[item.ItemId] = item;
            }
            int cntCells = 0;
            DTOSkillSetItem setItem = null;
            var dicChk = new Dictionary<string, byte>(skills.Length);
            for (int i = 0; i < skills.Length; ++i)
            {
                if (skills[i] == string.Empty)
                    continue;
                if (!onlib.TryGetValue(skills[i], out setItem))
                    return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillMissCard);
                if (!SkillCardConvert.FillSkillCardConfig(setItem, false))
                    return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillMissConfig);
                if (dicChk.ContainsKey(setItem.Cfg.SkillRoot))
                    return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillSetLimitRepeat);
                ++cntCells;
                setItem.Index = i + 1;
                dicChk[setItem.Cfg.SkillRoot] = 0;
                skills[i] = setItem.ItemCode;
            }
            if (cntCells > maxCells)
                return ResponseHelper.CreateRoot<DTOSkillSetView>(MessageCode.SkillSetLackofCells);
            string setSkills = string.Join(",", skills);
            string setMap = bag.SetMap;
            MemcachedFactory.SolutionClient.Delete(mid);
            int errorCode = 0;
            NbManagerskillbagMgr.Set(mid, setSkills, null, setMap, bag.RawBag.RowVersion, ref errorCode);
            if (errorCode != (int)MessageCode.Success)
                return ResponseHelper.CreateRoot<DTOSkillSetView>(errorCode);
            var data = new DTOSkillSetView();
            data.MaxSetCells = maxCells;
            data.CntSetCells = cntCells;
            data.SetList = bag.GetShowSet();
            //if (hasTask)
            //{
                data.PopMsg = TaskHandler.Instance.SkillSet(mid);
            //}
            //data.CardList = bag.GetShowBag();
            return ResponseHelper.CreateRoot<DTOSkillSetView>(data);
        }



        #endregion


        #region 存储新学技能

        public static ManagerskillNewResponse SaveNewSkills(Guid managerId,string skills)
        {
            var managerSkillNew = GetSkillNewEntity(managerId);
            if (managerSkillNew == null)
                return ResponseHelper.InvalidParameter<ManagerskillNewResponse>();
            managerSkillNew.NewSkills = skills;
            managerSkillNew.UpdateTime = DateTime.Now;
            if (!ManagerskillNewMgr.Update(managerSkillNew))
                return ResponseHelper.Create<ManagerskillNewResponse>(MessageCode.FailUpdate);
            var response = ResponseHelper.CreateSuccess<ManagerskillNewResponse>();
            response.Data = managerSkillNew;

            return response;
        }

        public static ManagerskillNewEntity GetSkillNewEntity(Guid managerId)
        {
            var entity = ManagerskillNewMgr.GetById(managerId);
            if (entity == null)
            {
                entity = new ManagerskillNewEntity()
                {
                    ManagerId = managerId,
                    NewSkills = "",
                    UpdateTime = DateTime.Now,
                    RowTime = DateTime.Now
                };
                if (ManagerskillNewMgr.Insert(entity))
                    return entity;
                else
                    return null;
            }
            return entity;
        }

        #endregion

        #region 获取新学技能列表

        public static ManagerskillNewResponse GetNewSkills(Guid managerId)
        {
            var managerSkillNew = GetSkillNewEntity(managerId);
            if (managerSkillNew == null)
                return ResponseHelper.InvalidParameter<ManagerskillNewResponse>();

            var response = ResponseHelper.CreateSuccess<ManagerskillNewResponse>();
            response.Data = managerSkillNew;
            return response;
        }


        #endregion







        #region Native
        static int GetMaxSkillCells(int managerLevel)
        {
            //if (managerLevel <= 0)
                return SkillCardConfig.SKILLCardRAWSkillCellSize;
           // int val = (int)managerLevel / 5 + SkillCardConfig.SKILLCardRAWSkillCellSize;
           // return Math.Min(SkillCardConfig.SKILLCardMAXSkillCellSize, val);
        }
        static string GetFormPidStr(Guid managerId)
        {
            var form = MatchDataHelper.GetSolution(managerId);
            if (null == form)
                return string.Empty;
            return form.PlayerString;
        }
        static List<ItemInfoEntity> GetPackItemList(Guid mid, ItemPackageFrame pack = null)
        {
            var packList = new List<ItemInfoEntity>();
            if (null == pack)
                pack = ItemCore.Instance.GetPackageWithoutShadow(mid);
            DicMallItemDataEntity mallCfg = null;
            foreach (var item in pack.GetItemsByType(0))
            {
                if (item.ItemType != (int)EnumItemType.MallItem)
                    continue;
                mallCfg = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(item.ItemCode);
                if (null == mallCfg || mallCfg.EffectType != (int)EnumMallEffectType.SkillCardExp)
                    continue;
                packList.Add(item);
            }
            return packList;
        }
        #endregion
    }
}
