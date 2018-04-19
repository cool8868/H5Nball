using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Response;
using MsEntLibWrapper.Data;

namespace Games.NBall.Bll.Frame
{
    public class BuffPoolCore
    {
        #region Config
        const string MEMPrefixBuffPool = "BFP_";
        const int MEMTickSecondsBuffPool = 600;
        const int MEMFlushDueSeconds = 120;
        static readonly DateTime DATEInfi = new DateTime(2020, 1, 1);
        #endregion

        #region Cache
        static IBuffSync s_buffSync;
        static MemCacheClient s_memBuffPool;
        #endregion

        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile BuffPoolCore s_instnce = null;
        public readonly bool InitFlag = false;
        public static BuffPoolCore Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new BuffPoolCore();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        private BuffPoolCore()
        {
            try
            {
                s_buffSync = BuffSyncThreadProvider.Instance();
                s_memBuffPool = new MemCacheClient(MEMPrefixBuffPool, MEMTickSecondsBuffPool);
                this.InitFlag = true;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "BuffPoolCore:Init");
                this.InitFlag = false;
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// 获取经理显示Buff
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <returns></returns>
        public RootResponse<DTOManagerBuffView> GetManagerShowBuffs(Guid managerId)
        {
            var list = GetManagerShowBuffList(managerId);
            if (list == null)
                return ResponseHelper.CreateRoot<DTOManagerBuffView>(null);

            var data = new DTOManagerBuffView();
            data.BuffList = list;
            return ResponseHelper.CreateRoot<DTOManagerBuffView>(data);
        }

        public List<DTOBuffValue> GetManagerShowBuffList(Guid managerId)
        {
            bool syncFlag = true;
            var values = GetBuffValues(managerId, EnumBuffUnitType.ManagerShow, true, syncFlag, BuffCache.BUFFCodes4ManagerShow);
            if (null == values || values.Length == 0)
                return null;
            var list = new List<DTOBuffValue>(values.Length);
            foreach (var item in values)
            {
                if (null != item.SrcList)
                    list.Add(item);
            }
            DicBuffEntity buffCfg = null;
            foreach (var item in list)
            {
                if (BuffCache.Instance().TryGetBuff(out buffCfg, item.BuffId))
                {
                    item.BuffName = buffCfg.BuffName;
                    item.Icon = buffCfg.Icon;
                    item.BuffMemo = string.Format(buffCfg.Memo, item.Point, item.Percent);
                }
                foreach (var srcItem in item.SrcList)
                {
                    srcItem.SkillName = BuffCache.Instance().GetSkillName(srcItem.SkillCode, srcItem.SkillLevel);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取单一Buff
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="buffCode">Buff枚举</param>
        /// <param name="syncFlag">缓存标记</param>
        /// <returns></returns>
        public DTOBuffValue GetBuffValue(Guid managerId, EnumBuffCode buffCode, bool syncFlag = true)
        {
            var buffCodes = new EnumBuffCode[] { buffCode };
            var values = GetBuffValues(managerId, EnumBuffUnitType.None, false, syncFlag, buffCodes);
            if (null != values && values.Length > 0)
                return values[0];
            return null;
        }

        /// <summary>
        /// 获取单一Buff
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="buffCode">Buff枚举</param>
        /// <param name="fillSource">是否填充源</param>
        /// <param name="syncFlag">缓存标记</param>
        /// <returns></returns>
        public DTOBuffValue GetBuffValue(Guid managerId, EnumBuffCode buffCode,bool fillSource, bool syncFlag)
        {
            var buffCodes = new EnumBuffCode[] { buffCode };
            var values = GetBuffValues(managerId, EnumBuffUnitType.None, fillSource, syncFlag, buffCodes);
            if (null != values && values.Length > 0)
                return values[0];
            return null;
        }

        /// <summary>
        /// 获取多个Buff
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="unitType">Buff细分类型</param>
        /// <param name="fillSource">是否填充源</param>
        /// <param name="syncFlag">缓存标记</param>
        /// <param name="buffCodes">Buff枚举</param>
        /// <returns></returns>
        public DTOBuffValue[] GetBuffValues(Guid managerId, EnumBuffUnitType unitType = EnumBuffUnitType.None, bool fillSource = false, bool syncFlag = true, params EnumBuffCode[] buffCodes)
        {
            if (null == buffCodes || buffCodes.Length == 0)
                return null;
            int cnt = buffCodes.Length;
            var arr = new DTOBuffValue[cnt];
            int buffId = 0;
            DTOBuffValue obj = null;
            Dictionary<int, DTOBuffValue> dic = null;
            bool multiFlag = cnt > 1;
            if (multiFlag)
            {
                dic = new Dictionary<int, DTOBuffValue>(cnt);
                for (int i = 0; i < buffCodes.Length; ++i)
                {
                    buffId = (int)buffCodes[i];
                    if (!dic.TryGetValue(buffId, out obj))
                    {
                        obj = new DTOBuffValue(buffId);
                        dic[buffId] = obj;
                    }
                    arr[i] = obj;
                }
            }
            else
            {
                buffId = (int)buffCodes[0];
                obj = new DTOBuffValue(buffId);
                arr[0] = obj;
            }
            var pools = GetBuffSource(managerId, unitType, syncFlag);
            if (null == pools || pools.Count == 0)
                return arr;
            int[] buffIds = null;
            DateTime dtNow = DateTime.Now;
            foreach (var item in pools)
            {
                if (item.ExpiryTime <= dtNow)
                    continue;
                buffIds = BuffCache.Instance().GetBaseBuffArray(item.BuffMap);
                if (null == buffIds)
                    continue;
                foreach (int val in buffIds)
                {
                    if (!multiFlag && val != buffId
                        || multiFlag && !dic.TryGetValue(val, out obj))
                        continue;
                    obj.Point += (double)item.BuffVal;
                    obj.Percent += (double)item.BuffPer;
                    if (fillSource)
                    {
                        if (null == obj.SrcList)
                            obj.SrcList = new List<DTOBuffSource>();
                        obj.SrcList.Add(new DTOBuffSource()
                        {
                            Idx = item.Id,
                            SkillCode = item.SkillCode,
                            SkillLevel = item.SkillLevel,
                            Point = (double)item.BuffVal,
                            Percent = (double)item.BuffPer,
                            ExpiryTime = ShareUtil.GetTimeTick(item.ExpiryTime),
                        });
                    }
                    if (!multiFlag)
                        break;
                }
            }
            if (multiFlag)
                dic.Clear();
            return arr;
        }
        public int GetManagerClothId(Guid managerId, DTOBuffPoolView rawPools = null)
        {
            var buffSrc = GetBuffSource(managerId, "M100", true, rawPools);
            if (null == buffSrc || buffSrc.Count == 0)
                return 0;
            return buffSrc[0].SkillLevel;
        }
        /// <summary>
        /// 获取源Buff
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="skillCode">技能Code</param>
        /// <param name="syncFlag">缓存标记</param>
        /// <param name="rawPools"></param>
        /// <returns></returns>
        public List<NbManagerbuffpoolEntity> GetBuffSource(Guid managerId, string skillCode, bool syncFlag = true, DTOBuffPoolView rawPools = null)
        {
            if (null == rawPools)
                rawPools = GetRawPools(managerId, "", syncFlag);
            if (null == rawPools || null == rawPools.BuffPools)
                return null;
            DateTime dtNow = DateTime.Now;
            return rawPools.BuffPools.FindAll(i => i.SkillCode == skillCode && i.ExpiryTime > dtNow);
        }
        /// <summary>
        /// 获取源Buff
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="unitType">Buff细分类型</param>
        /// <param name="syncFlag">缓存标记</param>
        /// <returns></returns>
        public List<NbManagerbuffpoolEntity> GetBuffSource(Guid managerId, EnumBuffUnitType unitType = EnumBuffUnitType.None, bool syncFlag = true, DTOBuffPoolView rawPools = null)
        {
            if (null == rawPools)
                rawPools = GetRawPools(managerId, "", syncFlag);
            if (null == rawPools || null == rawPools.BuffPools)
                return null;
            if (unitType == EnumBuffUnitType.None)
                return rawPools.BuffPools;
            DateTime dtNow = DateTime.Now;
            return rawPools.BuffPools.FindAll(i => (i.BuffUnitType & (int)unitType) > 0 && (i.ExpiryTime > dtNow || (i.ExpiryTime == DATEInfi && i.RemainTimes != 0)));
        }
        /// <summary>
        /// 获取原始Buff
        /// </summary>
        /// <param name="managerId">经理id</param>
        /// <param name="syncFlag">同步缓存标记</param>
        /// <returns></returns>
        public DTOBuffPoolView GetRawPools(Guid managerId, string siteId = "", bool syncFlag = true)
        {
            if (FrameConfig.SWAPBuffDisableCrossCache && !string.IsNullOrEmpty(siteId))
                syncFlag = false;
            var data = s_memBuffPool.Get<DTOBuffPoolView>(managerId.ToString());
            if (null == data)
                data = ReqRawPools(managerId, siteId, syncFlag);
            else
            {
                if (syncFlag && !IfMemValid(data.CreateTime))
                    s_buffSync.SyncBuffPools(managerId, siteId);
            }
            return data;
        }
        bool IfMemValid(DateTime createTime)
        {
            var dateNow = DateTime.Now;
            return dateNow.Subtract(createTime).TotalSeconds < MEMFlushDueSeconds;
        }
        #endregion

        #region Set
        public MessageCode AddPools(TransactionManager tranMgr, Guid managerId, string srcId, string skillCode, int skillLevel = 0)
        {
            return AddPools(ref tranMgr, managerId, srcId, skillCode, skillLevel);
        }
        public MessageCode AddPools(ref TransactionManager tranMgr, Guid managerId, string srcId, string skillCode, int skillLevel = 0)
        {
            try
            {
                int managerHash = ShareUtil.GetTableMod(managerId);
                var incPools = BuffCache.Instance().GetPoolIncBuffList(skillCode, skillLevel);
                var flows = BuffCache.Instance().GetFirmBuffList(skillCode, skillLevel);
                bool syncFlow = (null != flows && flows.Count > 0);
                if (null == incPools || incPools.Count == 0)
                {
                    if (null != tranMgr && tranMgr.IsOpen)
                        tranMgr.Commit();
                    if (syncFlow)
                        s_buffSync.SetBuffMembers(managerId);
                    return MessageCode.Success;
                }
                string[] excSkills;
                int[] excBuffs;
                GetExcSkillBuffs(out excSkills, out excBuffs, skillCode, skillLevel);
                if (null == tranMgr)
                    tranMgr = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault());
                if (!tranMgr.IsOpen)
                    tranMgr.BeginTransaction();
                if (excSkills[0] != string.Empty)
                {
                    NbManagerbuffpoolMgr.ExcludeMulti(managerId, managerHash,
                        excSkills[0], excBuffs[0], excSkills[1], excBuffs[1], excSkills[2], excBuffs[2], excSkills[3], excBuffs[3], excSkills[4], excBuffs[4],
                        tranMgr.TransactionObject);
                }
                foreach (var cfg in incPools)
                {
                    NbManagerbuffpoolMgr.Include(managerId, managerHash, cfg.SkillCode, cfg.SkillLevel, cfg.BuffSrcType, srcId,
                        (int)cfg.AsBuffUnitType, cfg.LiveFlag, cfg.BuffNo,
                        cfg.DstDir, cfg.DstMode, cfg.DstKey, cfg.BuffMap, cfg.BuffVal, cfg.BuffPer,
                        cfg.ExpiryMinutes, cfg.LimitTimes, cfg.TotalTimes, cfg.RepeatBuffFlag, cfg.RepeatTimeFlag, cfg.RepeatTimesFlag,
                        tranMgr.TransactionObject);
                }
                s_buffSync.SyncBuffPools(managerId);
                if (syncFlow)
                    s_buffSync.SetBuffMembers(managerId);
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                if (null != tranMgr)
                    tranMgr.Rollback();
                LogHelper.Insert(ex, "BuffPoolCore:AddPools");
                return MessageCode.Exception;
            }
        }
        /// <summary>
        /// 添加技能Buff，自动提交事务
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="srcId">来源Id</param>
        /// <param name="skillCode">技能Code</param>
        /// <param name="skillLevel">技能等级</param>
        /// <param name="tranMgr">事务对象</param>
        /// <returns></returns>
        public bool PostAddPools(Guid managerId, string srcId, string skillCode, int skillLevel = 0, TransactionManager tranMgr = null)
        {
            try
            {
                int managerHash = ShareUtil.GetTableMod(managerId);
                var incPools = BuffCache.Instance().GetPoolIncBuffList(skillCode, skillLevel);
                var flows = BuffCache.Instance().GetFirmBuffList(skillCode, skillLevel);
                bool syncFlow = (null != flows && flows.Count > 0);
                if (null == incPools || incPools.Count == 0)
                {
                    if (null != tranMgr && tranMgr.IsOpen)
                        tranMgr.Commit();
                    if (syncFlow)
                        s_buffSync.SetBuffMembers(managerId);
                    return true;
                }
                string[] excSkills;
                int[] excBuffs;
                GetExcSkillBuffs(out excSkills, out excBuffs, skillCode, skillLevel);
                if (null == tranMgr)
                    tranMgr = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault());
                using (tranMgr)
                {
                    if (!tranMgr.IsOpen)
                        tranMgr.BeginTransaction();
                    if (excSkills[0] != string.Empty)
                    {
                        NbManagerbuffpoolMgr.ExcludeMulti(managerId, managerHash,
                            excSkills[0], excBuffs[0], excSkills[1], excBuffs[1], excSkills[2], excBuffs[2], excSkills[3], excBuffs[3], excSkills[4], excBuffs[4],
                            tranMgr.TransactionObject);
                    }
                    foreach (var cfg in incPools)
                    {
                        NbManagerbuffpoolMgr.Include(managerId, managerHash, cfg.SkillCode, cfg.SkillLevel, cfg.BuffSrcType, srcId,
                            (int)cfg.AsBuffUnitType, cfg.LiveFlag, cfg.BuffNo,
                            cfg.DstDir, cfg.DstMode, cfg.DstKey, cfg.BuffMap, cfg.BuffVal, cfg.BuffPer,
                            cfg.ExpiryMinutes, cfg.LimitTimes, cfg.TotalTimes, cfg.RepeatBuffFlag, cfg.RepeatTimeFlag, cfg.RepeatTimesFlag,
                            tranMgr.TransactionObject);
                    }
                    tranMgr.Commit();
                }
                s_buffSync.SyncBuffPools(managerId);
                if (syncFlow)
                    s_buffSync.SetBuffMembers(managerId);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "BuffPoolCore:PostAddPools");
                throw ex;
            }
        }
        public void ClearMemPools(Guid managerId)
        {
            s_memBuffPool.Delete(managerId);
        }
        void GetExcSkillBuffs(out string[] excSkills, out int[] excBuffs, string skillCode, int skillLevel)
        {
            const int exLen = 5;
            excSkills = new string[exLen];
            excBuffs = new int[exLen];
            var excPools = BuffCache.Instance().GetPoolExcBuffList(skillCode, skillLevel);
            for (int i = 0; i < exLen; i++)
            {
                if (i < excPools.Count)
                {
                    excSkills[i] = excPools[i].SkillCode;
                    excBuffs[i] = excPools[i].BuffNo;
                }
                else
                {
                    excSkills[i] = string.Empty;
                    excBuffs[i] = -1;
                }
            }
        }
        /// <summary>
        /// 移除Buff，自动提交事务
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="srcType">来源类型</param>
        /// <param name="skillCode">技能Code</param>
        /// <param name="srcId">来源Id</param>
        /// <param name="tranMgr">事务对象</param>
        /// <returns></returns>
        public bool RemovePools(Guid managerId, EnumSkillSrcType srcType, string skillCode = "", string srcId = "", TransactionManager tranMgr = null)
        {
            try
            {
                int managerHash = ShareUtil.GetTableMod(managerId);
                if (null == tranMgr)
                    tranMgr = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault());
                using (tranMgr)
                {
                    if (!tranMgr.IsOpen)
                        tranMgr.BeginTransaction();
                    NbManagerbuffpoolMgr.Exclude(managerId, managerHash, (int)srcType, srcId, skillCode, tranMgr.TransactionObject);
                    tranMgr.Commit();
                }
                s_buffSync.SyncBuffPools(managerId);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "BuffPoolCore:AddBuffPools");
                throw ex;
            }
        }
        #endregion

        #region Internal
        internal DTOBuffPoolView ReqRawPools(Guid managerId, string siteId = "", bool syncFlag = true)
        {
            int managerHash = ShareUtil.GetTableMod(managerId);
            byte[] verDb = null;
            var data = CreateBuffView();
            var pools = NbManagerbuffpoolMgr.GetByMid(managerId, managerHash, siteId);
            var teamPools = CheckTeamBuffPools(managerId, siteId);
            if (null != teamPools)
                pools.AddRange(teamPools);
            NbManagerbuffpoolMgr.GetVersionByMid(managerId, managerHash, ref verDb, null, siteId);
            ulong verNo = 0;
            if (null != verDb)
                verNo = BitConverter.ToUInt64(verDb, 0);
            DateTime dtNow = DateTime.Now;
            DateTime dtSync = DATEInfi;
            List<string> liveSkills = null;
            foreach (var item in pools)
            {
                if (dtNow < item.ExpiryTime && item.ExpiryTime < dtSync)
                    dtSync = item.ExpiryTime;
                if (item.LiveFlag != 2)
                    continue;
                if (null == liveSkills)
                    liveSkills = new List<string>();
                liveSkills.Add(BuffCache.Instance().CastSkillKey(item.SkillCode, item.SkillLevel));
            }
            data.BuffPools = pools;
            data.LiveSkills = liveSkills;
            data.SyncTime = dtSync;
            data.SyncVersion = verNo;
            if (syncFlag)
                s_memBuffPool.Set(managerId.ToString(), data);
            return data;
        }

        #endregion

        #region BuffView
        DTOBuffPoolView CreateBuffView()
        {
            var data = new DTOBuffPoolView();
            data.CreateTime = DateTime.Now;
            return data;
        }
        #endregion

        #region TeamClubBuff
        static readonly string SKILLTeamBuff = "SP001";
        public List<NbManagerbuffpoolEntity> CheckTeamBuffPools(Guid mid, string siteId = "")
        {
            var form = MemcachedFactory.SolutionClient.Get<NbSolutionEntity>(mid);
            if (null == form)
                form = NbSolutionMgr.GetById(mid, siteId);
            if (null == form)
                return null;
            var pids = FrameUtil.CastIntList(form.PlayerString, ',');
            string club = string.Empty;
            string nation = string.Empty;
            DicPlayerEntity cfg = null;
            foreach (int pid in pids)
            {
                cfg = PlayersdicCache.Instance.GetPlayer(pid);
                if (null == cfg)
                    continue;
                if (club == string.Empty)
                    club = cfg.Club;
                if (nation == string.Empty)
                    nation = cfg.Nationality;
                if (null != club && cfg.Club != club)
                    club = null;
                if (null != nation && cfg.Nationality != nation)
                    nation = null;
            }
            if (null != club || null != nation)
                return BuffCache.Instance().GenManagerPoolList(SKILLTeamBuff);
            return null;
        }
        #endregion

    }
}
