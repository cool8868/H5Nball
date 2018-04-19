using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using MsEntLibWrapper.Data;

namespace Games.NBall.Bll.Frame
{
    public class BuffDataCore
    {
        #region Config
        const string MEMPrefixBuffMember = "BFM_";
        const int MEMTickSecondsBuffMember = 1200;
        const int CNTPlayersOn = 11;
        #endregion

        #region Cache
        static IBuffSync s_buffSync;
        static MemCacheClient s_memBuffMember;
        #endregion

        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile BuffDataCore s_instnce = null;
        public readonly bool InitFlag = false;
        public static BuffDataCore Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new BuffDataCore();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        private BuffDataCore()
        {
            try
            {
                s_buffSync = BuffSyncThreadProvider.Instance();
                if (ShareUtil.IsCross)
                {
                    s_memBuffMember = new MemCacheClient(MEMPrefixBuffMember, 30);
                }
                else
                {
                    s_memBuffMember = new MemCacheClient(MEMPrefixBuffMember, MEMTickSecondsBuffMember);
                }


                this.InitFlag = true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("BuffDataCore:Init", ex);
                this.InitFlag = false;
            }
        }
        #endregion

        #region Get
        public void GetMembers(out DTOBuffMemberView homeData, out DTOBuffMemberView awayData, string homeSiteId, Guid homeId, bool isHomeNpc, string awaySiteId, Guid awayId, bool isAwayNpc, bool syncHomeFlag = true, bool syncAwayFlag = false)
        {
            var buffPack = new DTOBuffPack();
            if (isHomeNpc)
            {
                syncHomeFlag = false;
                homeData = CloneBuffView(NpcdicCache.Instance.GetBuffView(homeId), true);
            }
            else
            {
                homeData = GetMembersCore(buffPack, true, homeId, syncHomeFlag, homeSiteId);
            }
            if (isAwayNpc)
            {
                syncAwayFlag = false;
                awayData = CloneBuffView(NpcdicCache.Instance.GetBuffView(awayId), true);
            }
            else
            {
                awayData = GetMembersCore(buffPack, false, awayId, syncAwayFlag, awaySiteId);
            }
            string antiTalentSkill;
            if (ManagerSkillCache.Instance().TryGetAntiTanlent(out antiTalentSkill, homeData.TalentType,awayData.TalentType))
            {
                if(null==homeData.ReadySkillList)
                    homeData.ReadySkillList=new List<string>(2);
                homeData.ReadySkillList.Add(antiTalentSkill);
            }
            if (ManagerSkillCache.Instance().TryGetAntiTanlent(out antiTalentSkill, awayData.TalentType, homeData.TalentType))
            {
                if (null == awayData.ReadySkillList)
                    awayData.ReadySkillList = new List<string>(2);
                awayData.ReadySkillList.Add(antiTalentSkill);
            }
            int cntHome = 0;
            int cntAway = 0;
            if (null != homeData.ReadySkillList)
                cntHome = homeData.ReadySkillList.Count;
            if (null != awayData.ReadySkillList)
                cntAway = awayData.ReadySkillList.Count;
            if (cntHome == 0 && cntAway == 0)
                return;
            if (isHomeNpc)
                FillBuffPack(buffPack, true, homeData.BuffPlayers);
            else
                FillBuffPack(buffPack, true, homeId, null, homeSiteId);
            if (isAwayNpc)
                FillBuffPack(buffPack, false, awayData.BuffPlayers);
            else
                FillBuffPack(buffPack, false, awayId, null, awaySiteId);
            BuffFlowFacade.ProcManagerBuff(buffPack, true, homeData.ReadySkillList, true);
            BuffFlowFacade.ProcManagerBuff(buffPack, false, awayData.ReadySkillList, true);
            FillBuffView(homeData, buffPack, true, true, 2);
            FillBuffView(awayData, buffPack, false, true, 2);
        }
        /// <summary>
        /// 获取球员Buff
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="syncFlag">同步缓存标记</param>
        /// <returns></returns>
        public DTOBuffMemberView GetMembers(Guid managerId, bool syncFlag = true, string siteId = "")
        {
            const bool homeFlag = true;
            var buffPack = new DTOBuffPack();
            return GetMembersCore(buffPack, homeFlag, managerId, syncFlag, siteId);
        }

        public DTOBuffMemberView RebuildMembers(Guid managerId)
        {
            const bool homeFlag = true;
            var buffPack = new DTOBuffPack();
            var use = ManagerUtil.GetSkillUseWrap(managerId);
            var poolView = BuffPoolCore.Instance().GetRawPools(managerId, "", true);

            FillBuffPack(buffPack, homeFlag, managerId);
            TeammemberDataHelper.FillTeammemberData(buffPack, homeFlag);
            use.OnPids = buffPack.GetOnPids(homeFlag).Keys.ToArray();
            BuffUtil.GetManagerSkillList(managerId, use);
            BuffCache.Instance().FillRankedSkillList(buffPack.GetSBM(homeFlag), use.ManagerSkills);
            BuffUtil.FillLiveSkillList(use, poolView.LiveSkills);
            var data = CreateBuffView(managerId, use, buffPack.GetRawMembers(homeFlag));
            data.PoolSyncTime = poolView.SyncTime;
            data.PoolSyncVersion = poolView.SyncVersion;
            FillBuffView(data, buffPack, homeFlag, false, 0);
            BuffFlowFacade.ProcManagerBuff(buffPack, homeFlag, data.FirmSkillList, false);
            BuffFlowFacade.ProcPlayerBuff(buffPack, homeFlag, data.BuffMembers.Values, false);
            BuffFlowFacade.ProcManagerBuff(buffPack, homeFlag, poolView);
            FillBuffView(data, buffPack, homeFlag, true, 1);
            data.TalentType = BuffUtil.GetTalentType(managerId);
            NbManagerextraMgr.UpdateKpi(managerId, data.Kpi);
            data.SyncIdleFlag = false;
            s_memBuffMember.Set(managerId.ToString(), data);
            return data;
        }

        DTOBuffMemberView GetMembersCore(DTOBuffPack buffPack, bool homeFlag, Guid managerId, bool syncFlag = true, string siteId = "")
        {
            if (FrameConfig.SWAPBuffDisableCrossCache && !string.IsNullOrEmpty(siteId))
                syncFlag = false;
            ManagerSkillUseWrap use;
            DTOBuffPoolView poolView;
            var data = GetMembers4Mem(out use, out poolView, managerId, siteId);
            do
            {
                if (null != data)
                    break;
                if (string.IsNullOrEmpty(siteId))
                    data = GetMembers4Db(managerId, use);
                if (null == data)
                {
                    FillBuffPack(buffPack, homeFlag, managerId, null, siteId);
                    TeammemberDataHelper.FillTeammemberData(buffPack, homeFlag, siteId);
                    use.OnPids = buffPack.GetOnPids(homeFlag).Keys.ToArray();
                    BuffUtil.GetManagerSkillList(managerId, use, siteId);
                    BuffCache.Instance().FillRankedSkillList(buffPack.GetSBM(homeFlag), use.ManagerSkills);
                    BuffUtil.FillLiveSkillList(use, poolView.LiveSkills);
                    data = CreateBuffView(managerId, use, buffPack.GetRawMembers(homeFlag));
                    data.PoolSyncTime = poolView.SyncTime;
                    data.PoolSyncVersion = poolView.SyncVersion;
                    FillBuffView(data, buffPack, homeFlag, false, 0);
                    BuffFlowFacade.ProcManagerBuff(buffPack, homeFlag, data.FirmSkillList, false);
                    BuffFlowFacade.ProcPlayerBuff(buffPack, homeFlag, data.BuffMembers.Values, false);
                    BuffFlowFacade.ProcManagerBuff(buffPack, homeFlag, poolView);
                    FillBuffView(data, buffPack, homeFlag, true, 1);
                }
                data.TalentType = BuffUtil.GetTalentType(managerId, siteId);
                if (syncFlag)
                    s_buffSync.SyncBuffMembers(managerId, CloneBuffView(data), siteId);
            }
            while (false);
            data.KpiReady = data.Kpi;
            data.ClothId = BuffPoolCore.Instance().GetManagerClothId(managerId, poolView);
            return data;
        }
        DTOBuffMemberView GetMembers4Mem(out ManagerSkillUseWrap use, out DTOBuffPoolView poolView, Guid managerId, string siteId)
        {
            use = ManagerUtil.GetSkillUseWrap(managerId, siteId);
            poolView = BuffPoolCore.Instance().GetRawPools(managerId, siteId, true);
            bool syncFlag = true;
            if (FrameConfig.SWAPBuffDisableCrossCache && !string.IsNullOrEmpty(siteId))
                syncFlag = false;
            ulong verNo = use.VersionNo;
            DateTime dtNow = DateTime.Now;
            if (verNo == 0 || dtNow >= poolView.SyncTime)
            {
                if (syncFlag)
                    s_buffSync.SyncBuffPools(managerId, siteId);
                return null;
            }
            var data = s_memBuffMember.Get<DTOBuffMemberView>(managerId.ToString());
            if (null != data
                && data.UseSyncVersion == verNo
                && data.PoolSyncTime == poolView.SyncTime
                && data.PoolSyncVersion == poolView.SyncVersion)
                return data;
            return null;
        }
        DTOBuffMemberView GetMembers4Db(Guid managerId, ManagerSkillUseWrap use)
        {
            if (!FrameConfig.SWAPBuffDataReadDb
                || null == use || use.Raw.SyncFlag != 0)
                return null;
            var rawMembers = BuffUtil.GetRawMembers(managerId, true);
            var members = NbManagerbuffmemberMgr.GetByMid(managerId, ShareUtil.GetTableMod(managerId));
            var extra = NbManagerextraMgr.GetById(managerId);
            var dic = new Dictionary<Guid, NbManagerbuffmemberEntity>(members.Count);
            DicPlayerEntity cfg = null;
            foreach (var item in members)
            {
                cfg = PlayersdicCache.Instance.GetPlayer(Math.Abs(item.Pid));
                if (null == cfg)
                    continue;
                FillBuffMemberProp(item, cfg);
                item.ReadySkillList = item.ReadySkills.Split(',').ToList();
                item.LiveSkillList = item.LiveSkills.Split(',').ToList();
            }
            var data = CreateBuffView(managerId, use, rawMembers.ToDictionary(i => i.Idx, i => i));
            data.Kpi = extra.Kpi;
            data.SyncIdleFlag = false;
            data.BuffMembers = dic;
            return data;
        }
        #endregion

        #region Set
        /// <summary>
        /// 请求同步球员Buff
        /// </summary>
        /// <param name="managerId">经理Id</param>
        public void SetMembers(Guid managerId, bool memFlag)
        {
            if (memFlag)
                s_memBuffMember.Delete(managerId.ToString());
            else
                s_buffSync.SetBuffMembers(managerId);
        }
        #endregion

        #region Internal
        internal bool SetMembersCore(Guid managerId)
        {
            try
            {
                int errorCode = 0;
                NbManagerbuffmemberMgr.SyncSend(managerId, null, ref errorCode);
                if (errorCode != 0)
                    throw new Exception(string.Format("Manager[{0}] FailCode[{1}]", managerId, errorCode));
                return true;
            }
            catch (Exception ex)
            {
                s_memBuffMember.Delete(managerId.ToString());
                SystemlogMgr.Error("BuffDataCore:SyncSend", ex);
                return false;
            }
        }
        internal bool SyncMembersCore(Guid managerId, DTOBuffMemberView buffData, string siteId = "")
        {
            try
            {
                if (null == buffData)
                    return false;
                if (string.IsNullOrEmpty(siteId))
                {
                    int managerHash = ShareUtil.GetTableMod(managerId);
                    //var idles = GetIdleMembers(managerId, buffData);
                    string mSkills = FrameConvert.SkillListToText(buffData.ManagerSkills);
                    int errorCode = 0;
                    byte[] rowVer = BitConverter.GetBytes(buffData.UseSyncVersion);
                    NbManagerbuffmemberMgr.SyncBatch(managerId, buffData.Kpi, string.Empty, mSkills, rowVer, ref rowVer, ref errorCode);
                    if (errorCode != 0)
                        return false;
                    buffData.UseSyncVersion = BitConverter.ToUInt64(rowVer, 0);
                }
                buffData.SyncIdleFlag = false;
                s_memBuffMember.Set(managerId.ToString(), buffData);
                return true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error(string.Format("BuffDataCore:Sync Manager[{0}]", managerId), ex);
                return false;
            }
        }
        Guid[] GetIdleMembers(Guid managerId, DTOBuffMemberView buffData)
        {
            if (!buffData.SyncIdleFlag)
                return null;
            var members = NbManagerbuffmemberMgr.GetByMid(managerId, ShareUtil.GetTableMod(managerId));
            Guid tid = Guid.Empty;
            var dic = buffData.BuffMembers;
            const int cnt = 5;
            int i = 0;
            var ary = new Guid[cnt];
            foreach (var item in members)
            {
                if (i >= cnt)
                    break;
                tid = item.Tid;
                if (!dic.ContainsKey(tid))
                    ary[i++] = tid;
            }
            for (; i < cnt; ++i)
            {
                ary[i] = Guid.Empty;
            }
            return ary;
        }
        #endregion

        #region BuffView
        DTOBuffMemberView CloneBuffView(DTOBuffMemberView src, bool withPack = false)
        {
            var obj = SerializationHelper.FromByte<DTOBuffMemberView>(SerializationHelper.ToByte(src));
            if (withPack && null != src.BuffPlayers)
            {
                obj.BuffPlayers = new Dictionary<Guid, DTOBuffPlayer>(src.BuffPlayers.Count);
                foreach (var kvp in src.BuffPlayers)
                {
                    obj.BuffPlayers[kvp.Key] = kvp.Value.Clone();
                }
            }
            return obj;
        }
        DTOBuffMemberView CreateBuffView(Guid managerId, ManagerSkillUseWrap use, Dictionary<Guid, TeammemberEntity> rawMembers)
        {
            var mSkills = use.ManagerSkills;
            var obj = new DTOBuffMemberView();
            obj.CreateTime = DateTime.Now;
            obj.SyncIdleFlag = true;
            obj.UseSyncVersion = use.VersionNo;
            obj.ReadySkillList = mSkills[0];
            obj.LiveSkillList = mSkills[1];
            obj.FirmSkillList = mSkills[2];
            obj.SubSkills = use.SetTalents;
            obj.RawMembers = rawMembers;
            return obj;
        }
        void FillBuffView(DTOBuffMemberView buffView, DTOBuffPack buffPack, bool homeFlag, bool fillProp = true, int fillKpi = 0)
        {
            var solution = buffPack.GetSolution(homeFlag);
            FillBuffView(buffView, buffPack.GetBuffPlayers(homeFlag), fillProp, fillKpi,solution.FormationId);
        }
        public void FillBuffView(DTOBuffMemberView buffView, Dictionary<Guid, DTOBuffPlayer> buffPlayers, bool fillProp = true, int fillKpi = 0,int formationId = 0)
        {
            if (null == buffPlayers)
                return;
            var dic = buffView.BuffMembers;
            if (null == dic)
            {
                dic = new Dictionary<Guid, NbManagerbuffmemberEntity>(buffPlayers.Count);
                buffView.BuffMembers = dic;
            }
            Guid tid = Guid.Empty;
            NbManagerbuffmemberEntity member = null;
            double kpi = 0;
            int index = 0;
            foreach (var kvp in buffPlayers)
            {
                tid = kvp.Key;
                if (!dic.TryGetValue(tid, out member))
                {
                    member = CreateBuffMember(tid, kvp.Value);
                    FillBuffMemberSkill(buffView, member, kvp.Value);
                    dic[tid] = member;
                }
                if (fillProp)
                    FillBuffMemberProp(member, kvp.Value);
                if (fillKpi > 0)
                    kpi += member.AsKpi(index, formationId);
                index ++;
            }
            if (fillKpi == 1)
                buffView.Kpi = buffView.KpiReady = Convert.ToInt32(kpi);
            else if (fillKpi > 1)
                buffView.KpiReady = Convert.ToInt32(kpi);
        }
        NbManagerbuffmemberEntity CreateBuffMember(Guid tid, DTOBuffPlayer player)
        {
            if (null == player)
                return null;
            var member = new NbManagerbuffmemberEntity();
            member.Tid = tid;
            member.Pid = player.AsPid;
            member.PPos = player.Pos;
            member.PPosOn = player.PosOn;
            member.Level = player.Level;
            member.Strength = player.Strength;
            member.IsMain = player.OnFlag;
            member.ShowOrder = player.ShowOrder;
            var props = player.Props;
            member.SpeedConst = props[0].Orig;
            member.ShootConst = props[1].Orig;
            member.FreeKickConst = props[2].Orig;
            member.BalanceConst = props[3].Orig;
            member.PhysiqueConst = props[4].Orig;
            member.PowerConst = props[5].Orig;
            member.AggressionConst = props[6].Orig;
            member.DisturbConst = props[7].Orig;
            member.InterceptionConst = props[8].Orig;
            member.DribbleConst = props[9].Orig;
            member.PassConst = props[10].Orig;
            member.MentalityConst = props[11].Orig;
            member.ResponseConst = props[12].Orig;
            member.PositioningConst = props[13].Orig;
            member.HandControlConst = props[14].Orig;
            member.AccelerationConst = props[15].Orig;
            member.BounceConst = props[16].Orig;
            return member;
        }
        void FillBuffMemberSkill(DTOBuffMemberView view, NbManagerbuffmemberEntity member, DTOBuffPlayer player)
        {
            member.MatchPropList = player.MatchPropList;
            member.MatchBoostList = player.MatchBoostList;
            if (null == view.ReadySkillList)
                view.ReadySkillList = new List<string>();
            member.LiveSkillList = new List<string>();
            member.FirmSkillList = new List<string>();
            if (null != player.SBMList)
            {
                var rankSkills = BuffCache.Instance().GetRankedSkillList(player.SBMList);
                view.ReadySkillList.AddRange(rankSkills[0]);
                member.LiveSkillList.AddRange(rankSkills[1]);
                member.FirmSkillList.AddRange(rankSkills[2]);
            }
            var skill = player.ActionSkill;
            if (!string.IsNullOrEmpty(skill))
                member.LiveSkillList.Add(skill);
            skill = player.StarPlusSkill;
            if (!string.IsNullOrEmpty(skill))
                member.LiveSkillList.Add(skill);
            skill = player.StarSkill;
            if (!string.IsNullOrEmpty(skill))
                member.LiveSkillList.AddRange(player.AsStarSkill);
        }
        void FillBuffMemberProp(NbManagerbuffmemberEntity member, DicPlayerEntity cfg)
        {
            if (null == member || null == cfg)
                return;
            member.SpeedConst = cfg.Speed;
            member.ShootConst = cfg.Shoot;
            member.FreeKickConst = cfg.FreeKick;
            member.BalanceConst = cfg.Balance;
            member.PhysiqueConst = cfg.Physique;
            member.PowerConst = cfg.Power;
            member.BounceConst = cfg.Bounce;
            member.AggressionConst = cfg.Aggression;
            member.DisturbConst = cfg.Disturb;
            member.InterceptionConst = cfg.Interception;
            member.DribbleConst = cfg.Dribble;
            member.PassConst = cfg.Pass;
            member.MentalityConst = cfg.Mentality;
            member.ResponseConst = cfg.Response;
            member.PositioningConst = cfg.Positioning;
            member.HandControlConst = cfg.HandControl;
            member.AccelerationConst = cfg.Acceleration;
        }
        void FillBuffMemberProp(NbManagerbuffmemberEntity member, DTOBuffPlayer player)
        {
            var props = player.Props;
            member.SpeedCount += props[0].BuffValue;
            member.ShootCount += props[1].BuffValue;
            member.FreeKickCount += props[2].BuffValue;
            member.BalanceCount += props[3].BuffValue;
            member.PhysiqueCount += props[4].BuffValue;
            member.PowerConst += props[5].BuffValue;
            member.AggressionCount += props[6].BuffValue;
            member.DisturbCount += props[7].BuffValue;
            member.InterceptionCount += props[8].BuffValue;
            member.DribbleCount += props[9].BuffValue;
            member.PassCount += props[10].BuffValue;
            member.MentalityCount += props[11].BuffValue;
            member.ResponseCount += props[12].BuffValue;
            member.PositioningCount += props[13].BuffValue;
            member.HandControlCount += props[14].BuffValue;
            member.AccelerationCount += props[15].BuffValue;
            member.BounceCount += props[16].BuffValue;
            player.ClearBuff();
        }
        #endregion

        #region BuffPack
        void FillBuffPack(DTOBuffPack buffPack, bool homeFlag, Dictionary<Guid, DTOBuffPlayer> buffPlayers)
        {
            if (null == buffPlayers)
                return;
            buffPack.SetBuffPlayers(homeFlag, buffPlayers);
            buffPack.SetOnBuffPlayers(homeFlag, buffPlayers.Values.ToList());
            //buffPack.SetOnPids(homeFlag, buffPlayers.ToDictionary(i => i.Value.Pid, i => i.Key));
        }
        void FillBuffPack(DTOBuffPack buffPack, bool homeFlag, Guid managerId, List<TeammemberEntity> rawMembers = null, string siteId = "")
        {
            FillBuffPackRaw(buffPack, homeFlag, managerId, rawMembers, siteId);
            var dicAll = buffPack.GetBuffPlayers(homeFlag);
            var lstOn = buffPack.GetOnBuffPlayers(homeFlag);
            if (null != dicAll && null != lstOn)
                return;
            var members = buffPack.GetRawMembers(homeFlag);
            var pids = buffPack.GetOnPids(homeFlag);
            var form = buffPack.GetSolution(homeFlag);
            dicAll = new Dictionary<Guid, DTOBuffPlayer>(members.Count);
            lstOn = new List<DTOBuffPlayer>(12);
            int pid = 0;
            Guid tid = Guid.Empty;
            DicPlayerEntity cfg = null;
            DTOBuffPlayer buffP = null;

            int showOrder = 1;
            foreach (var kvp in form.PlayerDic)
            {
                pid = kvp.Key;
                cfg = PlayersdicCache.Instance.GetPlayer(pid);
                if (null == cfg || !pids.TryGetValue(pid, out tid))
                    throw new Exception(string.Format("BuffDataCore:Manager[{0}] Miss Player[{1}] With Config Or Member", managerId, pid));
                buffP = CreateBuffPlayer(cfg, showOrder++);
                buffP.Pos = buffP.PosOn = kvp.Value.Position;
                buffP.ActionSkill = kvp.Value.SkillCode;
                dicAll[tid] = buffP;
                lstOn.Add(buffP);
            }
            foreach (var kvp in members)
            {
                pid = kvp.Value.PlayerId;
                tid = kvp.Key;
                if (dicAll.ContainsKey(tid))
                    continue;
                cfg = PlayersdicCache.Instance.GetPlayer(pid);
                if (null == cfg)
                    continue;
                buffP = CreateBuffPlayer(cfg, showOrder++, -cfg.Idx);
                dicAll[tid] = buffP;
            }
            buffPack.SetBuffPlayers(homeFlag, dicAll);
            buffPack.SetOnBuffPlayers(homeFlag, lstOn);
        }
        void FillBuffPackRaw(DTOBuffPack buffPack, bool homeFlag, Guid managerId, List<TeammemberEntity> rawMembers = null, string siteId = "")
        {
            bool memFlag = string.IsNullOrEmpty(siteId);
            buffPack.SetMid(homeFlag, managerId);
            bool topFlag = null == rawMembers;
            var members = buffPack.GetRawMembers(homeFlag);
            var pids = buffPack.GetOnPids(homeFlag);
            var form = buffPack.GetSolution(homeFlag);
            if (null == members || null != rawMembers)
            {
                if (null == rawMembers)
                {
                    rawMembers = BuffUtil.GetRawMembers(managerId, homeFlag, siteId);
                    if (null == rawMembers || rawMembers.Count == 0)
                        throw new Exception(string.Format("BuffDataCore:Manager[{0}] site[{1}] Miss RawMembers", managerId, siteId));
                }
                members = rawMembers.ToDictionary(i => i.Idx, i => i);
                buffPack.SetRawMembers(homeFlag, members);
            }
            if (null == pids || null == form)
            {
                form = BuffUtil.GetSolution(managerId, siteId);
                var pDic = new Dictionary<int, Guid>(members.Count);
                foreach (var entity in members.Values)
                {
                    pDic.Add(entity.PlayerId, entity.Idx);
                }
                int pid = 0;
                Guid tid = Guid.Empty;
                if (form == null || form.PlayerDic == null)
                    return;
                pids = new Dictionary<int, Guid>(form.PlayerDic.Count);
                foreach (var kvp in form.PlayerDic)
                {
                    pid = kvp.Key;
                    if (!pDic.TryGetValue(pid, out tid))
                        break;
                    pids[pid] = tid;
                }
                pDic.Clear();
                if (pids.Count != CNTPlayersOn && topFlag)
                {
                    if (memFlag)
                        MemcachedFactory.SolutionClient.Delete(managerId);
                    buffPack.SetSolution(homeFlag, null);
                    FillBuffPackRaw(buffPack, homeFlag, managerId, BuffUtil.GetRawMembers(managerId, homeFlag, siteId), siteId);
                    return;
                }
                buffPack.SetSolution(homeFlag, form);
                buffPack.SetOnPids(homeFlag, pids);
            }
        }

        DTOBuffPlayer CreateBuffPlayer(DicPlayerEntity cfg, int showOrder, int pid = 0)
        {
            if (null == cfg)
                return null;
            var rawProps = cfg.GetRawProps();
            var obj = new DTOBuffPlayer();
            obj.Pid = pid == 0 ? cfg.Idx : pid;
            obj.Pos = cfg.Position;
            obj.Clr = cfg.CardLevel;
            obj.Club = cfg.Club;
            obj.Nationality = cfg.Nationality;
            obj.Props = new DTOBuffProp[rawProps.Length];
            obj.PosOn = -1;
            obj.ShowOrder = showOrder;
            for (int i = 0; i < rawProps.Length; ++i)
            {
                obj.Props[i] = new DTOBuffProp { Orig = rawProps[i] };
            }
            rawProps = null;
            return obj;
        }
        public DTOBuffPlayer BuildBuffPlayerForGuide(int playerId, int strength)
        {
            var cfg = PlayersdicCache.Instance.GetPlayer(playerId);
            var buffP = CreateBuffPlayer(cfg, 0);
            buffP.Strength = strength;
            buffP.StarSkill = CacheFactory.PlayersdicCache.GetStarSkill(playerId, 10);
            Dictionary<int, List<int>> suitDic = new Dictionary<int, List<int>>();
            Dictionary<int, int> suitTypeDic = new Dictionary<int, int>();
            TeammemberDataHelper.FillEquipData(buffP, null, ref suitDic, ref suitTypeDic);
            return buffP;
        }
        #endregion


    }
}
