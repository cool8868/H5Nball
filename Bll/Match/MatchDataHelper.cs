using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Model.TranIn;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Dal;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Exceptions;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.NBall.Custom.Teammember;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;

namespace Games.NBall.Bll.Match
{
    public class MatchDataHelper
    {
        static string _botName = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.BotName);
        static int _maxVeteranCount = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.MaxVeteranCount);
        #region Manger
        public static int GetManagerKpi(Guid managerId, string siteId = "")
        {
            var buffView = BuffDataCore.Instance().GetMembers(managerId, true, siteId);
            if (buffView != null)
                return buffView.Kpi;
            return 0;
        }

        public static void UpdateManagerKpi(Guid managerId, int kpi)
        {
            var manager = MemcachedFactory.ManagerClient.Get<NbManagerEntity>(managerId);
            if (manager != null)
            {
                manager.Kpi = kpi;
                MemcachedFactory.ManagerClient.Set(manager.Idx, manager);
            }
        }

        public static NbManagerEntity GetManager(Guid managerId, bool containKpi = false, bool containTourLeague = false, string siteId = "")
        {
            try
            {
                var manager = MemcachedFactory.ManagerClient.Get<NbManagerEntity>(managerId);
                if (manager == null)
                {
                    manager = NbManagerMgr.GetById(managerId, siteId);
                    if (manager != null)
                    {
                        if (containTourLeague)
                            FillTourLeague(manager);
                        //if (!string.IsNullOrEmpty(siteId))
                        //{
                            //manager.Name = ShareUtil.GetCrossManagerNameByZoneId(siteId, manager.Name);
                        //}
                        MemcachedFactory.ManagerClient.Set(manager.Idx, manager);
                    }
                }
                if (manager != null)
                {
                    if (containKpi)
                    {
                        manager.Kpi = GetManagerKpi(managerId, siteId);
                    }
                    
                    if (containTourLeague && manager.TourLeague <= 0)
                    {
                        FillTourLeague(manager);
                        MemcachedFactory.ManagerClient.Set(manager.Idx, manager);
                    }
                }
                return manager;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetManager", "get manager data fail,mid:" + managerId, ex.StackTrace);
                throw ex;
            }

        }
        public static List<TeammemberEntity> GetRawMembers4View(Guid managerId, DTOBuffMemberView buffView, bool withHire = false, string siteId = "")
        {
            var dicMembers = buffView.RawMembers;
            List<TeammemberEntity> rst = null;
            if (null == dicMembers || dicMembers.Count == 0)
            {
                var members = GetRawMembers(managerId, true, siteId);
                if (null == members)
                    return null;
                if (withHire)
                    return members;
                rst = new List<TeammemberEntity>(members.Count);
                foreach (var item in members)
                {
                    if (item.IsHirePlayer)
                        continue;
                    rst.Add(item);
                }
                return rst;
            }
            if (withHire)
                return dicMembers.Values.ToList();
            rst = new List<TeammemberEntity>(dicMembers.Count);
            foreach (var item in dicMembers.Values)
            {
                if (item.IsHirePlayer)
                    continue;
                rst.Add(item);
            }
            return rst;
        }
        public static Dictionary<Guid, TeammemberEntity> GetDicRawMembers4View(Guid managerId, DTOBuffMemberView buffView, string siteId = "")
        {
            Dictionary<Guid, TeammemberEntity> dicMembers = buffView.RawMembers;
            if (null == dicMembers || dicMembers.Count == 0)
            {
                var members = GetRawMembers(managerId, true, siteId);
                if (null == members)
                    return null;
                dicMembers = members.ToDictionary(i => i.Idx, i => i);
            }
            return dicMembers;
        }
        public static List<TeammemberEntity> GetRawMembers(Guid managerId, bool syncFlag = true, string siteId = "")
        {
            try
            {
                List<TeammemberEntity> list = null;
                if (syncFlag)
                    list = MemcachedFactory.TeammembersClient.Get<List<TeammemberEntity>>(managerId);
                if (null != list && list.Count > 0)
                    return list;
                list = TeammemberMgr.GetByManager(managerId, ShareUtil.GetTableMod(managerId), siteId);
                
                TeammemberGrowEntity grow = null;
                var growList = TeammemberGrowMgr.GetByManager(managerId, siteId);
                foreach (var entity in list)
                {
                    if (!entity.IsHirePlayer)
                    {
                        grow = growList.Find(d => d.Idx == entity.Idx);
                        if (grow != null)
                            entity.GrowLevel = grow.GrowLevel;
                    }
                    if (entity.UsedPlayerCard != null && entity.UsedPlayerCard.Length > 0)
                    {
                        entity.PlayerCard = SerializationHelper.FromByte<PlayerCardUsedEntity>(entity.UsedPlayerCard);
                    }
                    if (entity.UsedEquipment != null && entity.UsedEquipment.Length > 0)
                        entity.Equipment = SerializationHelper.FromByte<EquipmentUsedEntity>(entity.UsedEquipment);
                    TeammemberDataHelper.CalPropertyCount(entity);
                    if (entity.IsHirePlayer)
                        TeammemberDataHelper.CalMaxGrow(entity,null);
                    else
                        TeammemberDataHelper.CalMaxGrow(entity, grow, siteId);
                    entity.RawProperty = new TeammemberPropertyEntity(entity);
                }
                if (syncFlag && list.Count > 0)
                    MemcachedFactory.TeammembersClient.Set(managerId, list);
                return list;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetRawMembers", string.Format("mid:{0}", managerId), ex);
                throw ex;
            }
        }

        public static List<TeammemberEntity> GetRawMembersArena(Guid managerId, ArenaTeammemberFrame arenaFrame, bool syncFlag = true, string siteId = "")
        {
            try
            {
                var clientKey = arenaFrame.ArenaType.ToString() + managerId.ToString();
                List<TeammemberEntity> list = null;
                if (syncFlag)
                {
                    list = MemcachedFactory.ArenaTeammembersClient.Get<List<TeammemberEntity>>(clientKey);
                }
                if (null != list && list.Count > 0)
                    return list;
                list = arenaFrame.GetTeammebmerList();

                if (syncFlag && list.Count > 0)
                    MemcachedFactory.ArenaTeammembersClient.Set(clientKey, list);
                return list;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetRawMembers", string.Format("mid:{0},arenaType:{1}", managerId,(int)arenaFrame.ArenaType), ex);
                throw ex;
            }
        }
        #endregion

        #region Teammember

        public static double GetTeammemberKpi(Guid managerId, Guid teammemberId)
        {
            var teammember = GetTeammember(managerId, teammemberId);
            return teammember == null ? 0 : teammember.Kpi;
        }

        public static TeammemberEntity GetTeammember(Guid managerId, Guid teammemberId, bool withHire = false)
        {
            var list = GetRawMembers(managerId);
            if (list != null)
            {
                var entity = list.Find(d => d.Idx == teammemberId);
                if (entity != null)
                {
                    if (!withHire && entity.IsHirePlayer)
                        return null;
                    BuildTeammemberProperty(entity);
                    return entity;
                }
            }
            return null;
        }

        /// <summary>
        /// 序列化球员卡和装备
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public static List<TeammemberEntity> GetTeammembers(Guid managerId, DTOBuffMemberView buffView = null, bool withHire = false, string siteId = "")
        {
            if (null == buffView)
                buffView = BuffDataCore.Instance().GetMembers(managerId, true, siteId);
            var members = GetRawMembers4View(managerId, buffView, withHire, siteId);
            if (members != null)
            {
                foreach (var entity in members)
                {
                    BuildTeammemberProperty(entity, buffView);
                }
            }
            return members;
        }
        #endregion

        #region Solution
        /// <summary>
        /// 获取阵型，计算阵型等级，和球员位置，技能
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public static NbSolutionEntity GetSolution(Guid managerId, string siteId = "")
        {
            bool memFlag = string.IsNullOrEmpty(siteId);
            NbSolutionEntity entity = null;
            if (memFlag)
                entity = MemcachedFactory.SolutionClient.Get<NbSolutionEntity>(managerId);
            if (entity == null)
            {
                entity = NbSolutionMgr.GetById(managerId, siteId);
                if (entity == null)
                    return null;
                var formationData = new FormationDataListEntity(entity.FormationData);
                entity.FormationLevel = formationData.GetLevel(entity.FormationId);
                var formationDetails = CacheFactory.FormationCache.GetFormationDetail(entity.FormationId);
                var playerIdList = FrameUtil.CastIntList(entity.PlayerString, ',');

                ////获取租借球员str，如果与实际阵容的str不同，就用虚拟的替换当前的，不改变实际阵容的str
                //var virtualPlayerIdList = FrameUtil.CastIntList(entity.HirePlayerString, ',');
                //for (int i = 0; i < playerIdList.Count; i++)
                //{
                //    for (int j = 0; j < virtualPlayerIdList.Count; j++)
                //    {
                //        if (playerIdList[i] != virtualPlayerIdList[j] && virtualPlayerIdList[j] != 0)
                //        {
                //            playerIdList[i] = virtualPlayerIdList[j];
                //        }
                //    }
                //}
                var skillList = new string[11];
                if (!string.IsNullOrEmpty(entity.SkillString))
                {
                    skillList = entity.SkillString.Split(',');
                }
                entity.PlayerDic = new Dictionary<int, SolutionPlayerEntity>(playerIdList.Count);
                for (int i = 0; i < playerIdList.Count; i++)
                {
                    entity.PlayerDic.Add(playerIdList[i], MatchDataUtil.BuildSolutionPlayer(playerIdList, formationDetails, playerIdList[i], skillList[i]));
                }
                if (memFlag)
                    MemcachedFactory.SolutionClient.Set(managerId, entity);
            }
            return entity;
        }
        #endregion

        #region SolutionArena
        /// <summary>
        /// 获取阵型，计算阵型等级，和球员位置，技能
        /// </summary>
        /// <param name="arenaTeammemberFrame"></param>
        /// <returns></returns>
        public static NbSolutionEntity GetArenaSolution(ArenaTeammemberFrame arenaTeammemberFrame)
        {
            var cacheKey = arenaTeammemberFrame.ArenaType.ToString() + arenaTeammemberFrame.ManagerId.ToString();
            var entity = MemcachedFactory.ArenaSolutionClient.Get<NbSolutionEntity>(cacheKey);
            if (entity == null)
            {
                entity = NbSolutionMgr.GetById(arenaTeammemberFrame.ManagerId, arenaTeammemberFrame.ZoneName);
                if (entity == null)
                    return null;
                entity.FormationId = arenaTeammemberFrame.SolutionId;
                var formationData = new FormationDataListEntity(entity.FormationData);
                entity.FormationLevel = formationData.GetLevel(entity.FormationId);
                var formationDetails = CacheFactory.FormationCache.GetFormationDetail(entity.FormationId);
                var playerIdList = arenaTeammemberFrame.PlayerList;
                var skillList = arenaTeammemberFrame.SkillList;
               
                entity.PlayerDic = new Dictionary<int, SolutionPlayerEntity>(playerIdList.Count);
                for (int i = 0; i < playerIdList.Count; i++)
                {
                    if (playerIdList[i] == 0)
                        continue;
                    entity.PlayerDic.Add(playerIdList[i],
                        MatchDataUtil.BuildSolutionPlayer(playerIdList, formationDetails, playerIdList[i], skillList[i]));
                }
                entity.PlayerString = arenaTeammemberFrame.PlayerString;
                entity.SkillString = arenaTeammemberFrame.SkillString;
                MemcachedFactory.ArenaSolutionClient.Set(cacheKey, entity);
            }
            return entity;
        }
        #endregion

        #region GetManagerDetail
        public static ManagerDetailInfoEntity GetManagerDetail(Guid managerId, string siteId = "")
        {
            var manager = GetManager(managerId, true, false, siteId);
            if (manager == null)
                return null;

            var managerDetail = new ManagerDetailInfoEntity();

            managerDetail.Name = manager.Name;
            managerDetail.Logo = manager.Logo;
            managerDetail.Level = manager.Level;
            managerDetail.Score = manager.Score;
            managerDetail.VipLevel = manager.VipLevel;
            managerDetail.Kpi = manager.Kpi;
            managerDetail.SolutionInfo = GetSolutionInfo(managerId);
            managerDetail.SolutionTalents = GetTalents(manager.Idx, siteId);
            return managerDetail;
        }

        
        #endregion

        #region GetFightinfo

        public static Match_FightManagerinfo GetFightinfo(DicNpcEntity npcEntity, DTOBuffMemberView buffView = null, bool isNpc = false)
        {
            if (null == buffView)
                buffView = NpcDataHelper.GetMemberView(npcEntity);
            return GetFightinfo(npcEntity.Idx, npcEntity.Name, npcEntity.FormationId, npcEntity.Level, npcEntity.Logo.ToString(),
                                npcEntity.CoachId
                                , buffView, "", isNpc);
        }

        public static Match_FightManagerinfo GetFightinfo(Guid managerId, bool isBot, DTOBuffMemberView buffView = null, string siteId = "")
        {
            var manager = GetManager(managerId, false, false, siteId);
            if (manager == null)
                return null;
            if (buffView == null)
                return null;
            manager.Kpi = buffView.Kpi;
            var solution = GetSolution(manager.Idx, siteId);
            if (solution == null)
                return null;
            string name = isBot ? _botName : manager.Name;
            // int coachId = GetCoachId(manager.Idx, siteId);
            return GetFightinfo(manager.Idx, name, solution.FormationId, manager.Level, manager.Logo, 0, buffView, siteId);
        }


        static Match_FightManagerinfo GetFightinfo(Guid managerId, string name, int formationId, int level, string logo, int coachId, DTOBuffMemberView buffView, string siteId = "", bool isNpc = false)
        {
            if (buffView == null)
                return null;

            var managerinfo = new Match_FightManagerinfo();
            managerinfo.FormationId = formationId;
            managerinfo.Level = level;
            managerinfo.Logo = logo;
            managerinfo.ManagerId = managerId;
            managerinfo.Name = name;
            managerinfo.CoachId = coachId;
            managerinfo.Teammembers = GetSolutionTeammembers(managerId, buffView, true, true, siteId, isNpc);
            managerinfo.Kpi = buffView.Kpi;
            managerinfo.KpiReady = buffView.KpiReady;

            return managerinfo;
        }
        #endregion


        #region GetFightinfoArena

        public static Match_FightManagerinfo GetFightinfoArena( ArenaTeammemberFrame arenaFrame , bool isBot, DTOBuffMemberView buffView = null, string siteId = "")
        {
            if (arenaFrame == null)
                return null;
            var manager = GetManager(arenaFrame.ManagerId, false, false, siteId);
            if (manager == null)
                return null;
            if (buffView == null)
                return null;
            arenaFrame.Kpi = buffView.Kpi;
            var solution = GetArenaSolution(arenaFrame);
            if (solution == null)
                return null;
            string name = isBot ? _botName : manager.Name;
            // int coachId = GetCoachId(manager.Idx, siteId);
            return GetFightinfoArena(arenaFrame, name, solution.FormationId, manager.Level, manager.Logo, 0, buffView, siteId);
        }


        static Match_FightManagerinfo GetFightinfoArena(ArenaTeammemberFrame arenaFrame, string name, int formationId, int level, string logo, int coachId, DTOBuffMemberView buffView, string siteId = "", bool isNpc = false)
        {
            if (buffView == null || arenaFrame ==null)
                return null;

            var managerinfo = new Match_FightManagerinfo();
            managerinfo.FormationId = formationId;
            managerinfo.Level = level;
            managerinfo.Logo = logo;
            managerinfo.ManagerId = arenaFrame.ManagerId;
            managerinfo.Name = name;
            managerinfo.CoachId = coachId;
            managerinfo.Teammembers = GetArenaSolutionTeammembers(arenaFrame, buffView, true, true, siteId, isNpc);
            managerinfo.Kpi = buffView.Kpi;
            managerinfo.KpiReady = buffView.KpiReady;

            return managerinfo;
        }
        #endregion


        #region GetSolutionTeammembers

        public static List<NBSolutionTeammember> GetSolutionTeammembers(Guid managerId, bool onlyMain = false, bool withHire = true, string siteId = "")
        {
            var buffView = BuffDataCore.Instance().GetMembers(managerId, true, siteId);
            if (buffView == null)
                return null;
            return GetSolutionTeammembers(managerId, buffView, onlyMain, withHire, siteId);
        }

        static List<NBSolutionTeammember> GetSolutionTeammembers(Guid managerId, DTOBuffMemberView buffView, bool onlyMain = false, bool withHire = true, string siteId = "", bool isNpc = false)
        {
            List<NBSolutionTeammember> list = new List<NBSolutionTeammember>();
            if (isNpc)
            {
                foreach (var buffMember in buffView.BuffMembers.Values)
                {
                    if (!onlyMain || buffMember.IsMain)
                    {
                        var entity = new NBSolutionTeammember();
                        entity.Idx = buffMember.Tid;
                        entity.Kpi = buffMember.Kpi;
                        entity.Level = buffMember.Level;
                        entity.PlayerId = buffMember.Pid;
                        entity.Position = buffMember.PPos;
                        entity.Strength = buffMember.Strength;
                        entity.IsMain = buffMember.IsMain;

                        list.Add(entity);
                    }
                }
            }
            else
            {
                var dicMembers = GetDicRawMembers4View(managerId, buffView, siteId);
                TeammemberEntity member = null;
                foreach (var buffMember in buffView.BuffMembers.Values)
                {
                    if (!onlyMain || buffMember.IsMain)
                    {
                        if (dicMembers.TryGetValue(buffMember.Tid, out member))
                        {
                            if (!withHire && member.IsHirePlayer)
                                continue;
                        }
                        var entity = new NBSolutionTeammember();
                        entity.Idx = buffMember.Tid;
                        entity.Kpi = buffMember.Kpi;
                        entity.Level = buffMember.Level;
                        entity.PlayerId = buffMember.Pid;
                        entity.Position = buffMember.PPos;
                        entity.Strength = buffMember.Strength;
                        entity.IsMain = buffMember.IsMain;
                        if (null != member)
                        {
                            entity.IsCopyed = member.IsCopyed;
                            entity.IsInherited = member.IsInherited;
                            entity.ArousalLv = member.ArousalLv;
                        }
                        list.Add(entity);
                    }
                }
            }
            return list;
        }
        #endregion

        #region GetSolutionInfo
        public static NBSolutionInfo GetSolutionInfo(Guid managerId)
        {
            var solution = GetSolution(managerId);
            if (solution == null)
                return null;
            var manager = GetManager(managerId);
            if (manager == null)
                return null;

            var solutionInfo = new NBSolutionInfo();
            solutionInfo.FormationId = solution.FormationId;
            solutionInfo.PlayerString = solution.PlayerString;
            solutionInfo.TeammemberMax = manager.TeammemberMax;
            solutionInfo.VeteranCount = solution.VeteranCount;
            int veteranCount = NbManagerextraMgr.GetById(managerId).VeteranNumber;
            solutionInfo.MaxVeteranCount = veteranCount < _maxVeteranCount ? _maxVeteranCount : veteranCount;
            solutionInfo.ClothId = BuffPoolCore.Instance().GetManagerClothId(managerId, null);
            solutionInfo.Teammembers = GetSolutionTeammembers(managerId);
            return solutionInfo;
        }
        #endregion

        #region GetArenaSolutionInfo
        public static NBSolutionInfo GetArenaSolutionInfo(ArenaTeammemberFrame arenaTeammemberFrame)
        {
            var solution = GetArenaSolution(arenaTeammemberFrame);
            if (solution == null)
                return null;
            //var manager = GetManager(managerId);
            //if (manager == null)
            //    return null;

            var solutionInfo = new NBSolutionInfo();
            solutionInfo.FormationId = solution.FormationId;
            solutionInfo.PlayerString = solution.PlayerString;
            //solutionInfo.ClothId = BuffPoolCore.Instance().GetManagerClothId(managerId, null);
            //if (!arenaTeammemberFrame.PlayerList.Exists(r => r == 0))
                solutionInfo.Teammembers = GetArenaSolutionTeammembers(arenaTeammemberFrame);
            //else

            return solutionInfo;
        }
        #endregion


        #region GetArenaSolutionTeammembers

        public static List<NBSolutionTeammember> GetArenaSolutionTeammembers(ArenaTeammemberFrame arenaTeammemberFrame, bool onlyMain = false, bool withHire = true, string siteId = "")
        {
            var buffView = ArenaBuffDataCore.Instance().GetMembers(arenaTeammemberFrame.ManagerId, arenaTeammemberFrame, true, arenaTeammemberFrame.ZoneName);
            if (buffView == null)
                return null;
            return GetArenaSolutionTeammembers(arenaTeammemberFrame, buffView, onlyMain, withHire, arenaTeammemberFrame.ZoneName);
        }

        static List<NBSolutionTeammember> GetArenaSolutionTeammembers(ArenaTeammemberFrame arenaTeammemberFrame, DTOBuffMemberView buffView, bool onlyMain = false, bool withHire = true, string siteId = "", bool isNpc = false)
        {
            List<NBSolutionTeammember> list = new List<NBSolutionTeammember>();
            if (isNpc)
            {
                foreach (var buffMember in buffView.BuffMembers.Values)
                {
                    if (!onlyMain || buffMember.IsMain)
                    {
                        var entity = new NBSolutionTeammember();
                        entity.Idx = buffMember.Tid;
                        entity.Kpi = buffMember.Kpi;
                        entity.Level = buffMember.Level;
                        entity.PlayerId = buffMember.Pid;
                        entity.Position = buffMember.PPos;
                        entity.Strength = buffMember.Strength;
                        entity.IsMain = buffMember.IsMain;

                        list.Add(entity);
                    }
                }
            }
            else
            {
                if (buffView == null || buffView.BuffMembers == null)
                    return list;
                var dicMembers = GetDicRawMembers4ViewArena(arenaTeammemberFrame.ManagerId, arenaTeammemberFrame, buffView, siteId);
                TeammemberEntity member = null;
                foreach (var buffMember in buffView.BuffMembers.Values)
                {
                    if (!onlyMain || buffMember.IsMain)
                    {
                        if (dicMembers.TryGetValue(buffMember.Tid, out member))
                        {
                            if (!withHire && member.IsHirePlayer)
                                continue;
                        }
                        var entity = new NBSolutionTeammember();
                        entity.Idx = buffMember.Tid;
                        entity.Kpi = buffMember.Kpi;
                        entity.Level = buffMember.Level;
                        entity.PlayerId = buffMember.Pid;
                        entity.Position = buffMember.PPos;
                        entity.Strength = buffMember.Strength;
                        entity.IsMain = buffMember.IsMain;
                        if (null != member)
                        {
                            entity.IsCopyed = member.IsCopyed;
                            entity.IsInherited = member.IsInherited;
                            entity.ArousalLv = member.ArousalLv;
                        }
                        list.Add(entity);
                    }
                }
                arenaTeammemberFrame.Kpi = buffView.Kpi;
            }
            return list;
        }
        #endregion

        public static Dictionary<Guid, TeammemberEntity> GetDicRawMembers4ViewArena(Guid managerId,ArenaTeammemberFrame arenaTeammemberFrame, DTOBuffMemberView buffView, string siteId = "")
        {
            Dictionary<Guid, TeammemberEntity> dicMembers = buffView.RawMembers;
            if (null == dicMembers || dicMembers.Count == 0)
            {
                var members = GetRawMembersArena(managerId,arenaTeammemberFrame, true, siteId);
                if (null == members)
                    return null;
                dicMembers = members.ToDictionary(i => i.Idx, i => i);
            }
            return dicMembers;
        }

        public static void CalKpi(DTOBuffMemberView view)
        {
            
        }

        #region encapsulation
        static void FillTourLeague(NbManagerEntity manager)
        {

        }

        private static void BuildTeammemberProperty(TeammemberEntity entity)
        {
            if (entity == null)
                return;

        }

        static void BuildTeammemberProperty(TeammemberEntity entity, DTOBuffMemberView buffView)
        {
           
        }

        //public static int GetCoachId(Guid managerId, string siteId = "")
        //{
        //    var coach = CoachMainMgr.C_CoachGetCoachID(managerId, siteId);
        //    if (coach != null)
        //        return coach.CoachID;
        //    else
        //    {
        //        return 0;
        //    }
        //}

        static string GetTalents(Guid managerId, string siteId = "")
        {
            string val = string.Empty;
            var use = ManagerskillUseMgr.GetById(managerId, siteId);
            if (null != use)
                val = use.Talents;
            return val;
        }
        #endregion
    }
}
