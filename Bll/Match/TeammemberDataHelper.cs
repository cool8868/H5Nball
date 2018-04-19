using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Bll.Cache;
using Games.NBall.Common;
using Games.NBall.Core.Coach;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;

namespace Games.NBall.Bll.Frame
{
    public class TeammemberDataHelper
    {
        /// <summary>
        /// 填充球员数据
        /// 装备数据，副卡加成，成长加点，阵型加成,等级加成
        /// </summary>
        /// <param name="buffPack"></param>
        /// <param name="homeFlag"></param>
        public static void FillTeammemberData(DTOBuffPack buffPack, bool homeFlag, string siteId = "")
        {
            try
            {
                var managerSBMList = new List<string>();
                buffPack.SetSBM(homeFlag, managerSBMList);

                var members = buffPack.GetRawMembers(homeFlag);
                var buffPlayers = buffPack.GetBuffPlayers(homeFlag);
                //套装字典 套装id->数量
                Dictionary<int, List<int>> suitDic = new Dictionary<int, List<int>>();
                //套装id->套装类型
                Dictionary<int, int> suitTypeDic = new Dictionary<int, int>();
                foreach (var teammember in members.Values)
                {
                    var buffPlayer = buffPlayers[teammember.Idx];
                    buffPlayer.SBMList = new List<string>();
                    buffPlayer.Strength = teammember.Strength;
                    buffPlayer.Level = teammember.Level;
                    buffPlayer.ArousalLv = teammember.ArousalLv;
                    ////球星技能
                    buffPlayer.StarSkill = CacheFactory.PlayersdicCache.GetStarSkill(buffPlayer.AsPid, buffPlayer.Strength, teammember.ArousalLv);
                    //装备和副卡 徽章
                    FillEquipData(buffPlayer, teammember.Equipment, ref suitDic, ref suitTypeDic);
                    //成长
                    FillTeammemberGrowData(teammember, buffPlayer);
                    //等级
                    FillTeammemberLevelData(teammember, buffPlayer);
                    //球员星级
                    TheStarPlayerCardData(teammember, buffPlayer);
                    //球星潜力
                    FillPlayerCardData(buffPlayer, teammember);
                }
                //套装
                FillSuitData(suitDic, suitTypeDic, ref managerSBMList);
                //阵型加成
                var solution = buffPack.GetSolution(homeFlag);
                FillFormationData(solution.FormationId, solution.FormationLevel, ref managerSBMList);
                //教练加成
                FillCoachData(buffPack.GetMid(homeFlag), managerSBMList, siteId);

            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "TeammemberDataHelper:FillTeammemberData");
            }
        }

        public static void FillCoachData(Guid managerId, List<string> managerSBMList, string siteId)
        {
             //CoachManagerMgr.GetById(managerId, siteId);
             var coach = new CoachFrame(managerId);
             if (null == coach.Entity || coach.Entity.EnableCoachId <= 0)
                return;
            foreach (var item in coach.GetCoachList())
            {
                managerSBMList.Add(
               string.Format("{0}_{1}.{2}.{3}", (int)EnumSBMType.Coach, item.CoachId, item.CoachStar, item.CoachLevel));
            }

            if (coach.Entity.EnableCoachSkillLevel > 0)
                managerSBMList.Add(string.Concat(BuffCache.WILDChar4LiveSkill, "H", coach.Entity.EnableCoachId.ToString("000"), "_" + coach.Entity.EnableCoachSkillLevel));
        }
        //public static void FillConstellationData(Guid managerId, List<string> managerSBMList, List<DTOBuffPlayer> buffPlayers, string siteId)
        //{
        //    var constellations = ConstellationAttrMgr.GetConstellationSkillInfo(managerId, siteId);
        //    if (null == constellations || constellations.Count <= 0)
        //        return;
        //    ConfigConstellationattrEntity cfg;
        //    double buffPer;
        //    foreach (var item in constellations)
        //    {
        //        if (!item.IsActivate)
        //            continue;
        //        if (item.SkillLevel > 0)
        //            managerSBMList.Add(string.Concat(BuffCache.WILDChar4LiveSkill, "X", item.ConstellationId.ToString("000"), "_" + item.SkillLevel));
        //        if (item.ConstellationLevel <= 0)
        //            continue;
        //        cfg = CacheFactory.ConstellationCache.GetAttrBuff(item.ConstellationId, item.ConstellationLevel, out buffPer);
        //        if (null == cfg)
        //            continue;
        //        if (cfg.BuffId < 1000)
        //            managerSBMList.Add(BuffCache.Instance().GetVarySkillCode(cfg.BuffId, 0, buffPer));
        //        else
        //        {
        //            foreach (var bp in buffPlayers)
        //            {
        //                bp.AddMatchBuff(0, buffPer, cfg.BuffId);
        //            }
        //        }
        //    }
        //}
        public static void FillFormationData(int formationId, int formationLevel, ref List<string> managerSBMList)
        {
            //var sbm = CacheFactory.ManagerDataCache.GetSkillbuff(EnumSBMType.Formation, formationId);
            //foreach (var s in sbm)
            //{
            //    managerSBMList.Add(s + "." + formationLevel);
            //}
        }

        public static void FillTeammemberGrowData(TeammemberEntity teammember, DTOBuffPlayer buffPlayer)
        {
            //var teammemberProps = BuffUtil.GetTeammemberProps(teammember);
            //var growCache = CacheFactory.TeammemberCache.GetGrow(teammember.GrowLevel);
            //double growPlus = 0;
            //if (growCache != null)
            //{
            //    growPlus = growCache.PlusPercent;
            //    if (growPlus > 0)
            //    {
            //        growPlus = growPlus / 100.00;
            //    }
            //}
            //for (int i = 0; i < buffPlayer.Props.Length; ++i)
            //{
            //    buffPlayer.Props[i].Point += teammemberProps[i];
            //    buffPlayer.Props[i].Percent += growPlus;
            //}
        }

        public static void FillTeammemberLevelData(TeammemberEntity teammember, DTOBuffPlayer buffPlayer)
        {
            var teammemberProps = BuffUtil.GetTeammemberProps(teammember);
            var level = teammember.Level;

            double levelPlus = 0;
            if (level > 1)
            {
                levelPlus = (level - 1) / 100.00;
            }
            for (int i = 0; i < buffPlayer.Props.Length; ++i)
            {
                buffPlayer.Props[i].Point += teammemberProps[i];
                buffPlayer.Props[i].Percent += levelPlus;
            }
        }

        public static void FillEquipData(DTOBuffPlayer buffPlayer, EquipmentUsedEntity equipment, ref Dictionary<int, List<int>> suitDic, ref Dictionary<int, int> suitTypeDic)
        {
            try
            {
                var strengthPlus = CacheFactory.TeammemberCache.GetStrengthPlus(buffPlayer.Strength);

                

                for (int i = 0; i < buffPlayer.Props.Length; ++i)
                {
                    buffPlayer.Props[i].Percent += strengthPlus / 100.00;
                }

                if (equipment != null)
                {
                    var equipmentProperty = equipment.Property;
                    var itemDic = CacheFactory.ItemsdicCache.GetEquipmentByItemCode(equipment.ItemCode);
                    if (buffPlayer.OnFlag && itemDic != null && itemDic.SuitId > 0)
                    {
                        var suitCode = itemDic.Idx % 1000;
                        if (suitDic.ContainsKey(itemDic.SuitId))
                        {
                            var list = suitDic[itemDic.SuitId];
                            if (list == null)
                                list = new List<int>();
                            if (!list.Contains(suitCode))
                                list.Add(suitCode);
                            suitDic[itemDic.SuitId] = list;
                        }
                        else
                        {
                            suitDic.Add(itemDic.SuitId, new List<int>() { suitCode });
                            suitTypeDic.Add(itemDic.SuitId, itemDic.SuitType);
                        }
                    }
                    double equipPlus = 0;
                    foreach (var plus in equipmentProperty.PropertyPluses)
                    {
                        equipPlus = plus.PlusValue * (1 + equipmentProperty.Level * 0.1);
                        if (plus.PlusType == (int)EnumPlusType.Percent)
                            buffPlayer.Props[plus.PropertyId - 1].Percent += equipPlus / 100.00;
                        else
                        {
                            buffPlayer.Props[plus.PropertyId - 1].Point += equipPlus;
                        }
                    }
                    if (equipmentProperty.EquipmentSlots != null)
                    {
                        foreach (var equipmentSlot in equipmentProperty.EquipmentSlots)
                        {
                            if (equipmentSlot.BallSoul != null)
                            {
                                //var sbm = CacheFactory.ManagerDataCache.GetSkillbuff(EnumSBMType.BallSoul, equipmentSlot.BallSoul.ItemCode);
                                //buffPlayer.SBMList.AddRange(sbm);
                            }
                        }
                    }
                  
                  
                }
               
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "TeammemberDataHelper:FillEquipData");
            }

        }

        public static void FillSuitData(Dictionary<int, List<int>> suitDic, Dictionary<int, int> suitTypeDic, ref List<string> managerSBMList)
        {
            //计算套装效果
            foreach (var suit in suitDic)
            {
                var suitId = suit.Key;
                var suitType = suitTypeDic[suitId];
                var suitList = suit.Value;
                if (suitList != null)
                {
                    var count = suitList.Count;
                    switch (suitType)
                    {
                        case (int)EnumSuitType.ThreeSuit:
                            AddSuitBuff(managerSBMList, suitId, count, 3);
                            break;
                        case (int)EnumSuitType.FiveSuit:
                            if (AddSuitBuff(managerSBMList, suitId, count, 3))
                            {
                                AddSuitBuff(managerSBMList, suitId, count, 5);
                            }
                            break;
                        case (int)EnumSuitType.SevenSuit:
                            if (AddSuitBuff(managerSBMList, suitId, count, 3))
                            {
                                if (AddSuitBuff(managerSBMList, suitId, count, 5))
                                {
                                    AddSuitBuff(managerSBMList, suitId, count, 7);
                                }
                            }
                            break;
                    }
                }
            }
        }
        public static void FillPlayerCardData(DTOBuffPlayer buffPlayer, TeammemberEntity teammember)
        {
            try
            {
                if (null == teammember.PlayerCard || null == teammember.PlayerCard.Property)
                    return;
                var cardProp = teammember.PlayerCard.Property;
                if (null == cardProp.Potential)
                    return;
                ConfigPotentialEntity cfg;
                double point, percent;
                foreach (var item in cardProp.Potential)
                {
                    cfg = PlayersdicCache.Instance.GetPotentialConfig(item.Level, item.Idx);
                    if (null == cfg)
                        continue;
                    if (cfg.BuffType == 2)
                    {
                        point = 0;
                        percent = Convert.ToDouble(item.Buff) / 100d;
                    }
                    else
                    {
                        point = Convert.ToDouble(item.Buff);
                        percent = 0;
                    }
                    if (cfg.BuffId < 1000)
                        buffPlayer.SBMList.Add(BuffCache.Instance().GetVarySkillCode(cfg.BuffId, point, percent));
                    else
                        buffPlayer.AddMatchBuff(point, percent, cfg.BuffId);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "TeammemberDataHelper:FillPlayerCardData");
            }
        }

        public static void TheStarPlayerCardData(TeammemberEntity teammember, DTOBuffPlayer buffPlayer)
        {
            if (null == teammember.PlayerCard || null == teammember.PlayerCard.Property)
                return;
            var cardProp = teammember.PlayerCard.Property;
            if (0 == cardProp.TheStar || cardProp.TheStar > 5)
                return;

            var teammemberProps = BuffUtil.GetTeammemberProps(teammember);
            double growPlus = 0;

            growPlus = 15 * cardProp.TheStar;
            if (growPlus > 0)
            {
                growPlus = growPlus / 100.00;
            }
            for (int i = 0; i < buffPlayer.Props.Length; ++i)
            {
                buffPlayer.Props[i].Point += teammemberProps[i];
                buffPlayer.Props[i].Percent += growPlus;
            }
        }

        public static bool AddSuitBuff(List<string> sbmList, int suitId, int curCount, int checkCount)
        {
            if (curCount >= checkCount)
            {
                sbmList.AddRange(CacheFactory.ManagerDataCache.GetSkillbuff(EnumSBMType.Suit, suitId, checkCount.ToString()));
                return true;
            }
            return false;
        }

        public static void CalPropertyCount(TeammemberEntity entity)
        {
            entity.PropertyPoint = CalPropertyCount(entity.Level, entity.UsedProperty);
        }


        public static int CalPropertyCount(int level, int usedProperty)
        {
            var prop = 10 - usedProperty;
            if (prop < 0)
                prop = 0;
            return prop;
        }

        public static void CalMaxGrow(TeammemberEntity entity, TeammemberGrowEntity grow, string siteId = "")
        {
            int growLevel = 1;
            if (grow == null)
                grow = TeammemberGrowMgr.GetById(entity.Idx, siteId);
            if (grow != null)
            {
                growLevel = grow.GrowLevel;
            }
            var playerCache = CacheFactory.PlayersdicCache.GetPlayer(entity.PlayerId);
        }

    }
}
