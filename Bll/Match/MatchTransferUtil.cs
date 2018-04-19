using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Response;

namespace Games.NBall.Bll.Match
{
    public class MatchTransferUtil
    {
        static string _botName = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.BotName);

        #region BuildDTO

        public static MatchInput BuildTransferMatch(BaseMatchData matchData)
        {
            MatchInput match = new MatchInput();
            match.MatchId = matchData.MatchId;
            match.MatchType = matchData.MatchType;
            match.TranTime = 120;
            if (matchData.IsGuide)
            {
                matchData.Home.BuffScale = 400;
                // match.ForceType = EnumForceWinType.HomeWin;
            }
            else if (matchData.NoDraw)
            {
                match.ForceType = EnumForceWinType.NoDraw;
            }
            bool isHomeNpc = matchData.Home.IsNpc;
            bool isAwayNpc = matchData.Away.IsNpc;
            string homeSiteId = matchData.Home.ZoneName;
            string awaySiteId = matchData.Away.ZoneName;
            if (isHomeNpc || isAwayNpc)
            {
                if (matchData.MatchType == (int) EnumMatchType.Arena)
                {
                    ArenaTeammemberFrame homeFrame = null;
                    ArenaTeammemberFrame awayFrame = null;
                    if(!isHomeNpc)
                        homeFrame = new ArenaTeammemberFrame(matchData.Home.ManagerId,(EnumArenaType) matchData.Home.ArenaType, homeSiteId);
                    if(!isAwayNpc)
                        awayFrame = new ArenaTeammemberFrame(matchData.Away.ManagerId,(EnumArenaType) matchData.Away.ArenaType, awaySiteId);

                    match.HomeManager = BuildTransferManagerArena(matchData.Home, homeFrame, matchData.IsGuide, null, homeSiteId);
                    match.AwayManager = BuildTransferManagerArena(matchData.Away, awayFrame, false, null, awaySiteId);
                    
                }
                else
                {
                    match.HomeManager = BuildTransferManager(matchData.Home, matchData.IsGuide, null, homeSiteId);
                    match.AwayManager = BuildTransferManager(matchData.Away, false, null, awaySiteId);
                }
                return match;
            }
            DTOBuffMemberView homeView, awayView;
            if (matchData.MatchType == (int) EnumMatchType.Arena)
            {
                ArenaTeammemberFrame homeFrame = new ArenaTeammemberFrame(matchData.Home.ManagerId,(EnumArenaType) matchData.Home.ArenaType, homeSiteId);
                ArenaTeammemberFrame awayFrame = new ArenaTeammemberFrame(matchData.Away.ManagerId,(EnumArenaType) matchData.Away.ArenaType, awaySiteId);
                ArenaBuffDataCore.Instance().GetMembers(out homeView, out awayView, homeSiteId, matchData.Home.ManagerId, isHomeNpc, awaySiteId,
                        matchData.Away.ManagerId, homeFrame, awayFrame, isAwayNpc, true, false);
                match.HomeManager = BuildTransferManagerArena(matchData.Home, homeFrame, matchData.IsGuide, homeView, homeSiteId);
                match.AwayManager = BuildTransferManagerArena(matchData.Away, awayFrame, false, awayView, awaySiteId);
            }
            else
            {
                BuffDataCore.Instance().GetMembers(out homeView, out awayView,
                    homeSiteId, matchData.Home.ManagerId, isHomeNpc,
                    awaySiteId, matchData.Away.ManagerId, isAwayNpc, true, false); 
                match.HomeManager = BuildTransferManager(matchData.Home, matchData.IsGuide, homeView, homeSiteId);
                match.AwayManager = BuildTransferManager(matchData.Away, false, awayView, awaySiteId);
            }
            return match;
        }

        public static ManagerInput BuildTransferManager(MatchManagerInfo managerInfo, bool isGuide = false, DTOBuffMemberView buffView = null, string siteId = "")
        {
            try
            {
                if (managerInfo.IsNpc)
                    return GetTransferNpc(managerInfo);
                if (null == buffView)
                    buffView = BuffDataCore.Instance().GetMembers(managerInfo.ManagerId, true,siteId);
                return CreateTransferManager(managerInfo, buffView, isGuide, siteId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("BuildTransferManager", ex);
                throw;
            }

        }

        public static ManagerInput BuildTransferManagerArena(MatchManagerInfo managerInfo,ArenaTeammemberFrame arenaFrame, bool isGuide = false, DTOBuffMemberView buffView = null, string siteId = "")
        {
            try
            {
                if (managerInfo.IsNpc)
                    return GetTransferNpc(managerInfo);
                if (null == buffView)
                    buffView = ArenaBuffDataCore.Instance().GetMembers(managerInfo.ManagerId, arenaFrame,true, siteId);
                return CreateTransferManagerArena(managerInfo, buffView, isGuide, arenaFrame, siteId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("BuildTransferManager", ex);
                throw;
            }

        }
        #endregion

        #region Tools

        static ManagerInput GetTransferNpc(MatchManagerInfo managerInfo)
        {
            var data = CacheFactory.NpcdicCache.GetTransferData(managerInfo.ManagerId);
            managerInfo.Name = data.Name;
            return data;
        }

        static ManagerInput CreateTransferManager(MatchManagerInfo managerInfo, DTOBuffMemberView buffView, bool isGuide, string siteId = "")
        {
            if (null == buffView)
                return null;
            var manager = MatchDataHelper.GetManager(managerInfo.ManagerId, false, false, siteId);
            var solution = MatchDataHelper.GetSolution(managerInfo.ManagerId, siteId);
            string name = managerInfo.IsBot ? _botName : manager.Name;

            managerInfo.Name = name;
            var dstData = new ManagerInput();
            dstData.Mid = managerInfo.ManagerId;
            dstData.Name = name;
            dstData.Logo = manager.Logo.ToString();
            dstData.FormId = solution.FormationId;
            dstData.FormLv = solution.FormationLevel;
            dstData.ClothId = buffView.ClothId;
            dstData.Skills = buffView.LiveSkillList;
            dstData.SubSkills = buffView.SubSkills;
            if (null != buffView.MatchPropList)
            {
                dstData.PropList = new List<PropInput>();
                foreach (var item in buffView.MatchPropList)
                {
                    dstData.PropList.Add(new PropInput(item.Point, item.Percent, item.BuffId));
                }
            }
            if (null != buffView.MatchBoostList)
            {
                dstData.BoostList = new List<BoostInput>();
                foreach (var item in buffView.MatchBoostList)
                {
                    dstData.BoostList.Add(new BoostInput(item.BoostType, item.Point, item.Percent, item.BuffId));
                }
            }

            BuildManagerData(dstData, buffView, manager.VipLevel, managerInfo.BuffScale, isGuide);
            return dstData;
        }

        static ManagerInput CreateTransferManagerArena(MatchManagerInfo managerInfo, DTOBuffMemberView buffView, bool isGuide,ArenaTeammemberFrame arenaFrame, string siteId = "")
        {
            if (null == buffView)
                return null;
            var manager = MatchDataHelper.GetManager(managerInfo.ManagerId, false, false, siteId);
            var solution = MatchDataHelper.GetArenaSolution(arenaFrame);
            string name = managerInfo.IsBot ? _botName : manager.Name;

            managerInfo.Name = name;
            var dstData = new ManagerInput();
            dstData.Mid = managerInfo.ManagerId;
            dstData.Name = name;
            dstData.Logo = manager.Logo.ToString();
            dstData.FormId = solution.FormationId;
            dstData.FormLv = solution.FormationLevel;
            dstData.ClothId = buffView.ClothId;
            dstData.Skills = buffView.LiveSkillList;
            dstData.SubSkills = buffView.SubSkills;
            if (null != buffView.MatchPropList)
            {
                dstData.PropList = new List<PropInput>();
                foreach (var item in buffView.MatchPropList)
                {
                    dstData.PropList.Add(new PropInput(item.Point, item.Percent, item.BuffId));
                }
            }
            if (null != buffView.MatchBoostList)
            {
                dstData.BoostList = new List<BoostInput>();
                foreach (var item in buffView.MatchBoostList)
                {
                    dstData.BoostList.Add(new BoostInput(item.BoostType, item.Point, item.Percent, item.BuffId));
                }
            }

            BuildManagerData(dstData, buffView, manager.VipLevel, managerInfo.BuffScale, isGuide);
            return dstData;
        }

        public static void BuildManagerData(ManagerInput dstData, DTOBuffMemberView buffView, int vipLevel, int buffScale, bool isGuide = false)
        {
            if (null == dstData)
                return;
            dstData.Players = new List<PlayerInput>(11);

            var legendCount = 0;
            foreach (var buffMember in buffView.BuffMembers.Values)
            {
                if (buffMember.IsMain)
                {
                    var cachePlayer = MatchDataUtil.GetDicPlayer(buffMember.Tid, buffMember.Pid);
                    if (cachePlayer.CardLevel == 2 || cachePlayer.CardLevel == 1 || cachePlayer.CardLevel == 7 || cachePlayer.CardLevel == 8)
                        legendCount++;
                }
            }
            buffScale += CacheFactory.AppsettingCache.GetSolutionLegendAndVipPlus(legendCount, vipLevel);
            double buffPlus = buffScale / 100.00;
            foreach (var buffMember in buffView.BuffMembers.Values)
            {
                if (buffMember.IsMain)
                {
                    var cachePlayer = MatchDataUtil.GetDicPlayer(buffMember.Tid, buffMember.Pid);
                    PlayerInput transferPlayerEntity = new PlayerInput();
                    transferPlayerEntity.FamilyName = cachePlayer.Name;
                    transferPlayerEntity.Height = (int)cachePlayer.Stature;
                    transferPlayerEntity.Pid = buffMember.Pid;
                    transferPlayerEntity.Plus = (byte)buffMember.Strength;
                    transferPlayerEntity.Position = (byte)buffMember.PPos;
                    BuildTeammemberData(transferPlayerEntity, buffMember, buffPlus, isGuide);
                    dstData.Players.Add(transferPlayerEntity);
                }
            }
            if (isGuide)
            {
                var list = CacheFactory.NpcdicCache.GetGuidePlayers();
                int count = list.Count;
                int totalCount = dstData.Players.Count;
                for (int i = 1; i <= count; i++)
                {
                    PlayerInputClone(dstData.Players[totalCount - i], list[i - 1]);
                }
            }
        }

        static void BuildTeammemberData(PlayerInput dstData, NbManagerbuffmemberEntity srcTeammember, double buffScale, bool isGuide=false)
        {
            if (null == dstData || null == srcTeammember)
                return;
            dstData.Speed = srcTeammember.TotalSpeed*buffScale;
            dstData.Shooting = srcTeammember.TotalShoot * buffScale;
            dstData.FreeKick = srcTeammember.TotalFreeKick * buffScale;
            dstData.Balance = srcTeammember.TotalBalance * buffScale;
            dstData.Stamina = srcTeammember.TotalPhysique * buffScale;
            dstData.Strength = srcTeammember.TotalPower * buffScale;
            dstData.Aggression = srcTeammember.TotalAggression * buffScale;
            dstData.Disturb = srcTeammember.TotalDisturb * buffScale;
            dstData.Interception = srcTeammember.TotalInterception * buffScale;
            dstData.Dribble = srcTeammember.TotalDribble * buffScale;
            dstData.Passing = srcTeammember.TotalPass * buffScale;
            dstData.Mentality = srcTeammember.TotalMentality * buffScale;
            dstData.Reflexes = srcTeammember.TotalResponse * buffScale;
            dstData.Positioning = srcTeammember.TotalPositioning * buffScale;
            dstData.Handling = srcTeammember.TotalHandControl * buffScale;
            dstData.Acceleration = srcTeammember.TotalAcceleration * buffScale;
            dstData.Bounce = srcTeammember.TotalBounce * buffScale;
            dstData.Skills = srcTeammember.LiveSkillList;

            if (null != srcTeammember.MatchPropList)
            {
                dstData.PropList = new List<PropInput>();
                foreach (var item in srcTeammember.MatchPropList)
                {
                    dstData.PropList.Add(new PropInput(item.Point, item.Percent, item.BuffId));
                }
            }
            if (null != srcTeammember.MatchBoostList)
            {
                dstData.BoostList = new List<BoostInput>();
                foreach (var item in srcTeammember.MatchBoostList)
                {
                    dstData.BoostList.Add(new BoostInput(item.BoostType, item.Point, item.Percent, item.BuffId));
                }
            }
        }

        static void PlayerInputClone(PlayerInput dstData, PlayerInput player)
        {
            dstData.FamilyName = player.FamilyName;
            dstData.Plus = player.Plus;
            dstData.Pid = player.Pid;
            dstData.Height = player.Height;
            if (player.Skills != null)
            {
                dstData.Skills = new List<string>(player.Skills.Count);
                foreach (var skill in player.Skills)
                {
                    dstData.Skills.Add(skill);
                }
            }
            dstData.Speed = player.Speed;
            dstData.Shooting = player.Shooting;
            dstData.FreeKick = player.FreeKick;
            dstData.Balance = player.Balance;
            dstData.Stamina = player.Stamina;
            dstData.Strength = player.Strength;
            dstData.Aggression = player.Aggression;
            dstData.Disturb = player.Disturb;
            dstData.Interception = player.Interception;
            dstData.Dribble = player.Dribble;
            dstData.Passing = player.Passing;
            dstData.Mentality = player.Mentality;
            dstData.Reflexes = player.Reflexes;
            dstData.Positioning = player.Positioning;
            dstData.Handling = player.Handling;
            dstData.Acceleration = player.Acceleration;
            dstData.Bounce = player.Bounce;
        }

        public static ManagerInput BuildTransferNpc(DicNpcEntity npcEntity, DTOBuffMemberView buffView)
        {
            var dstData = new ManagerInput();
            dstData.Mid = npcEntity.Idx;
            dstData.Logo = npcEntity.Logo.ToString();
            dstData.Name = npcEntity.Name;
            dstData.FormId = npcEntity.FormationId;
            dstData.FormLv = npcEntity.FormationLevel;

            BuildManagerData(dstData, buffView, 0, npcEntity.Buff);
            return dstData;
        }

        public static PlayerInput BuildPlayerInputForGuide(int playerId, int strength, string skillCode)
        {
            var player = BuffDataCore.Instance().BuildBuffPlayerForGuide(playerId, strength);
            var cachePlayer = CacheFactory.PlayersdicCache.GetPlayer(playerId);
            PlayerInput dstData = new PlayerInput();
            dstData.FamilyName = cachePlayer.Name;
            dstData.Pid = playerId;
            dstData.Plus = (byte)strength;
            dstData.Skills = new List<string>(2);
            if (!String.IsNullOrEmpty(skillCode))
            {
                dstData.Skills.Add(skillCode);
            }
            if (!String.IsNullOrEmpty(player.StarSkill))
                dstData.Skills.Add(player.StarSkill);
            var props = player.Props;
            int i = 0;
            dstData.Speed = props[i++].TotalValue + 200;
            dstData.Shooting = props[i++].TotalValue + 200;
            dstData.FreeKick = props[i++].TotalValue + 200;
            dstData.Balance = props[i++].TotalValue + 200;
            dstData.Stamina = props[i++].TotalValue + 200;
            dstData.Strength = props[i++].TotalValue + 200;
            dstData.Aggression = props[i++].TotalValue + 200;
            dstData.Disturb = props[i++].TotalValue + 200;
            dstData.Interception = props[i++].TotalValue + 200;
            dstData.Dribble = props[i++].TotalValue + 200;
            dstData.Passing = props[i++].TotalValue + 200;
            dstData.Mentality = props[i++].TotalValue + 200;
            dstData.Reflexes = props[i++].TotalValue + 200;
            dstData.Positioning = props[i++].TotalValue + 200;
            dstData.Handling = props[i++].TotalValue + 200;
            dstData.Acceleration = props[i].TotalValue + 200;
            return dstData;
        }

        #endregion
    }
}
