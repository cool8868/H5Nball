using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;


namespace Games.NBall.Bll.Frame
{
    public class BuffFlowFacade
    {
        #region Facade
        public static void ProcManagerBuff(DTOBuffPack buffPack, bool homeFlag, DTOBuffPoolView poolView = null)
        {
            Guid managerId = buffPack.GetMid(homeFlag);
            var pools = BuffPoolCore.Instance().GetBuffSource(managerId, EnumBuffUnitType.PlayerProp, true, poolView);
            ProcBuffPool(buffPack, homeFlag, Guid.Empty, pools);
        }
        public static void ProcManagerBuff(DTOBuffPack buffPack, bool homeFlag, List<string> skillList, bool liveFlag)
        {
            if (null == skillList || skillList.Count == 0)
                return;
            var flows = liveFlag ? BuffCache.Instance().GetReadyBuffList(skillList) : BuffCache.Instance().GetFirmBuffList(skillList);
            if (null == flows || flows.Count == 0)
                return;
            ProcBuffFlow(buffPack, homeFlag, Guid.Empty, flows);
        }
        public static void ProcPlayerBuff(DTOBuffPack buffPack, bool homeFlag, IEnumerable<NbManagerbuffmemberEntity> memberList, bool liveFlag)
        {
            List<string> skillList = null;
            Dictionary<string, List<ConfigBuffengineEntity>> flows = null;
            foreach (var item in memberList)
            {
                skillList = liveFlag ? item.ReadySkillList : item.FirmSkillList;
                if (null == skillList || skillList.Count == 0)
                    continue;
                flows = liveFlag ? BuffCache.Instance().GetReadyBuffList(skillList) : BuffCache.Instance().GetFirmBuffList(skillList);
                if (null == flows || flows.Count == 0)
                    continue;
                ProcBuffFlow(buffPack, homeFlag, item.Tid, flows);
            }
        }
        #endregion

        #region Native
        static void ProcBuffFlow(DTOBuffPack buffPack, bool homeFlag, Guid memberId, Dictionary<string, List<ConfigBuffengineEntity>> buffFlows)
        {
            if (null == buffPack || null == buffFlows || buffFlows.Count == 0)
                return;
            string skillKey = string.Empty;
            IBuffCheck chk = null;
            IBuffFlowProc proc = null;
            List<DTOBuffPlayer> buffDest = null;
            var dicDest = new Dictionary<string, List<DTOBuffPlayer>>();
            foreach (var kvp in buffFlows)
            {
                skillKey = kvp.Key;
                foreach (var flow in kvp.Value)
                {
                    chk = CreateBuffCheck(flow.CheckMode);
                    if (null != chk && !chk.CheckBuff(buffPack, homeFlag, flow.CheckKey))
                        continue;
                    proc = CreateBuffFlowProc(flow.CalcMode);
                    if (null == proc)
                        continue;
                    buffDest = GetBuffDest(dicDest, buffPack, homeFlag, memberId, flow.DstMode, flow.DstDir, flow.DstKey);
                    if (null == buffDest || buffDest.Count == 0)
                        continue;
                    proc.ProcBuffFlow(buffDest, buffPack, homeFlag, skillKey, flow);
                }
            }
            dicDest.Clear();
        }
        static void ProcBuffPool(DTOBuffPack buffPack, bool homeFlag, Guid memberId, List<NbManagerbuffpoolEntity> buffPools)
        {
            if (null == buffPack || null == buffPools || buffPools.Count == 0)
                return;
            var proc = CreateBuffPoolProc();
            if (null == proc)
                return;
            char split = BuffCache.SPLITValues;
            List<DTOBuffPlayer> buffDest = null;
            var dicDest = new Dictionary<string, List<DTOBuffPlayer>>();
            foreach (var pool in buffPools)
            {
                buffDest = GetBuffDest(dicDest, buffPack, homeFlag, memberId, pool.DstMode, pool.DstDir, pool.DstKey);
                if (null == buffDest || buffDest.Count == 0)
                    continue;
                proc.ProcBuffPool(buffDest, pool.SkillCode, FrameUtil.CastIntArray(pool.BuffMap, split), (double)pool.BuffVal, (double)pool.BuffPer);
            }
            dicDest.Clear();
        }
        static List<DTOBuffPlayer> GetBuffDest(Dictionary<string, List<DTOBuffPlayer>> dicDest, DTOBuffPack buffPack, bool homeFlag, Guid memberId, int dstMode, int dstDir, string dstKey)
        {
            string dstIdx = CastDestIdx(dstMode, dstDir, dstKey);
            List<DTOBuffPlayer> list;
            if (dicDest.TryGetValue(dstIdx, out list))
                return list;
            var dst = CreateBuffDest(dstMode);
            if (null == dst)
                return null;
            return dst.GetBuffDest(buffPack, homeFlag, memberId, dstDir, dstKey);
        }
        static IBuffCheck CreateBuffCheck(int checkMode)
        {
            return null;
        }
        static IBuffDest CreateBuffDest(int dstMode)
        {
            switch (dstMode)
            {
                case 0:
                    return BuffDestPid.Instance;
                case 1:
                    return BuffDestPos.Instance;
                case 2:
                    return BuffDestClr.Instance;
                case 3:
                    return BuffDestClub.Instance;
                case 4:
                    return BuffDestNationality.Instance;
                default:
                    return BuffDestPid.Instance;
            }
        }
        static IBuffFlowProc CreateBuffFlowProc(int procMode)
        {
            if (procMode == 1)
                return BuffVaryProcRoot.Instance;
            else if (procMode == 2)
                return BuffCoachProc.Instance;
            return BuffProcRoot.Instance;
        }
        static IBuffPoolProc CreateBuffPoolProc()
        {
            return BuffProcRoot.Instance;
        }
        static string CastDestIdx(int dstMode, int dstDir, string dstKey)
        {
            return string.Format("{0}@{1}_{2}", dstKey, dstMode, dstDir);
        }
        #endregion
    }

    #region IBuffFlow
    interface IBuffCheck
    {
        bool CheckBuff(DTOBuffPack buffPack, bool homeFlag, string checkKey);
    }
    interface IBuffDest
    {
        List<DTOBuffPlayer> GetBuffDest(DTOBuffPack buffPack, bool homeFlag, Guid memberId, int dstDir, string dstKey);
    }
    interface IBuffFlowProc
    {
        void ProcBuffFlow(List<DTOBuffPlayer> buffDest, DTOBuffPack buffPack, bool homeFlag, string skillKey, ConfigBuffengineEntity buffFlow);
    }
    interface IBuffPoolProc
    {
        void ProcBuffPool(List<DTOBuffPlayer> buffDest, string skillKey, int[] buffList, double buffVal, double buffPer);
    }
    #endregion

    #region BuffCheck
    class BuffCheckRoot : IBuffCheck
    {
        public static readonly BuffCheckRoot Instance = new BuffCheckRoot();

        public bool CheckBuff(DTOBuffPack buffPack, bool homeFlag, string checkKey)
        {
            return true;
        }
    }
    #endregion

    #region BuffDest
    abstract class BuffDestRoot : IBuffDest
    {
        public abstract List<DTOBuffPlayer> GetBuffDest(DTOBuffPack buffPack, bool homeFlag, Guid memberId, int dstDir, string dstKey);
        protected IEnumerable<DTOBuffPlayer> GetRootDest(DTOBuffPack buffPack, bool homeFlag, Guid memberId, int dstDir)
        {
            if (null == buffPack)
                return null;
            switch (dstDir)
            {
                case 0://己方全体
                    var players = buffPack.GetBuffPlayers(homeFlag);
                    if (null == players)
                        return null;
                    return players.Values;
                case 1://己方场上
                    var onPlayers = buffPack.GetOnBuffPlayers(homeFlag);
                    if (null == onPlayers)
                        return null;
                    return onPlayers;
                case 9://自己
                    players = buffPack.GetBuffPlayers(homeFlag);
                    DTOBuffPlayer player = null;
                    if (players.TryGetValue(memberId, out player))
                        return new DTOBuffPlayer[] { player };
                    return null;
                case 10://对方全体
                    players = buffPack.GetBuffPlayers(!homeFlag);
                    if (null == players)
                        return null;
                    return players.Values;
                case 11://对方场上
                    onPlayers = buffPack.GetOnBuffPlayers(!homeFlag);
                    if (null == onPlayers)
                        return null;
                    return onPlayers;
                default:
                    return null;
            }
        }
    }
    class BuffDestPid : BuffDestRoot
    {
        public static readonly BuffDestPid Instance = new BuffDestPid();
        public override List<DTOBuffPlayer> GetBuffDest(DTOBuffPack buffPack, bool homeFlag, Guid memberId, int dstDir, string dstKey)
        {
            var rootDest = GetRootDest(buffPack, homeFlag, memberId, dstDir);
            if (null == rootDest)
                return null;
            if (string.IsNullOrEmpty(dstKey))
                return rootDest.ToList();
            char split = BuffCache.SPLITValues;
            dstKey += split;
            return rootDest.Where(i => dstKey.IndexOf(i.AsPid.ToString() + split) >= 0).ToList();
        }
    }
    class BuffDestPos : BuffDestRoot
    {
        public static readonly BuffDestPos Instance = new BuffDestPos();
        public override List<DTOBuffPlayer> GetBuffDest(DTOBuffPack buffPack, bool homeFlag, Guid memberId, int dstDir, string dstKey)
        {
            var rootDest = GetRootDest(buffPack, homeFlag, memberId, dstDir);
            if (null == rootDest)
                return null;
            if (string.IsNullOrEmpty(dstKey))
                return rootDest.ToList();
            char split = BuffCache.SPLITValues;
            dstKey += split;
            return rootDest.Where(i => dstKey.IndexOf(i.Pos.ToString() + split) >= 0).ToList();
        }
    }
    class BuffDestClr : BuffDestRoot
    {
        public static readonly BuffDestClr Instance = new BuffDestClr();
        public override List<DTOBuffPlayer> GetBuffDest(DTOBuffPack buffPack, bool homeFlag, Guid memberId, int dstDir, string dstKey)
        {
            var rootDest = GetRootDest(buffPack, homeFlag, memberId, dstDir);
            if (null == rootDest)
                return null;
            if (string.IsNullOrEmpty(dstKey))
                return rootDest.ToList();
            char split = BuffCache.SPLITValues;
            dstKey += split;
            return rootDest.Where(i => dstKey.IndexOf(i.Clr.ToString() + split) >= 0).ToList();
        }
    }
    class BuffDestClub : BuffDestRoot
    {
        public static readonly BuffDestClub Instance = new BuffDestClub();
        public override List<DTOBuffPlayer> GetBuffDest(DTOBuffPack buffPack, bool homeFlag, Guid memberId, int dstDir, string dstKey)
        {
            var rootDest = GetRootDest(buffPack, homeFlag, memberId, dstDir);
            if (null == rootDest)
                return null;
            if (string.IsNullOrEmpty(dstKey))
                return rootDest.ToList();
            return rootDest.Where(i => string.Compare(i.Club, dstKey, true) == 0).ToList();
        }
    }
    class BuffDestNationality : BuffDestRoot
    {
        public static readonly BuffDestNationality Instance = new BuffDestNationality();
        public override List<DTOBuffPlayer> GetBuffDest(DTOBuffPack buffPack, bool homeFlag, Guid memberId, int dstDir, string dstKey)
        {
            var rootDest = GetRootDest(buffPack, homeFlag, memberId, dstDir);
            if (null == rootDest)
                return null;
            if (string.IsNullOrEmpty(dstKey))
                return rootDest.ToList();
            return rootDest.Where(i => string.Compare(i.Nationality, dstKey, true) == 0).ToList();
        }
    }
    #endregion

    #region BuffProc
    class BuffProcRoot : IBuffFlowProc, IBuffPoolProc
    {
        public static readonly BuffProcRoot Instance = new BuffProcRoot();

        public void ProcBuffFlow(List<DTOBuffPlayer> buffDest, DTOBuffPack buffPack, bool homeFlag, string skillKey, ConfigBuffengineEntity buffFlow)
        {
            ProcBuffPool(buffDest, skillKey, buffFlow.PropIndexList, (double)buffFlow.BuffVal, (double)buffFlow.BuffPer);
        }
        public void ProcBuffPool(List<DTOBuffPlayer> buffDest, string skillKey, int[] buffList, double buffVal, double buffPer)
        {
            if (null == buffList || buffList.Length == 0)
                return;
            foreach (var dst in buffDest)
            {
                foreach (int idx in buffList)
                {
                    if (idx >= 0 && idx < dst.Props.Length)
                    {
                        dst.Props[idx].Point += buffVal;
                        dst.Props[idx].Percent += buffPer;
                    }
                }
            }
        }
    }
    class BuffVaryProcRoot : IBuffFlowProc
    {
        public static readonly BuffVaryProcRoot Instance = new BuffVaryProcRoot();
        public void ProcBuffFlow(List<DTOBuffPlayer> buffDest, DTOBuffPack buffPack, bool homeFlag, string skillKey, ConfigBuffengineEntity buffFlow)
        {
            var buffList = buffFlow.PropIndexList;
            if (null == buffList || buffList.Length == 0)
                return;
            double buffVal = (double)buffFlow.BuffVal;
            double buffPer = (double)buffFlow.BuffPer;
            string args = skillKey.Substring(skillKey.LastIndexOf('.') + 1);
            int fact = 0;
            if (!int.TryParse(args, out fact) || fact <= 0)
                fact = 1;
            foreach (var dst in buffDest)
            {
                foreach (int idx in buffList)
                {
                    if (idx >= 0 && idx < dst.Props.Length)
                    {
                        dst.Props[idx].Point += buffVal * fact;
                        dst.Props[idx].Percent += buffPer * fact;
                    }
                }
            }
        }
    }
    class BuffCoachProc : IBuffFlowProc
    {
        public static readonly BuffCoachProc Instance = new BuffCoachProc();
        public void ProcBuffFlow(List<DTOBuffPlayer> buffDest, DTOBuffPack buffPack, bool homeFlag, string skillKey, ConfigBuffengineEntity buffFlow)
        {
            var buffList = buffFlow.PropIndexList;
            if (null == buffList || buffList.Length == 0)
                return;
            double buffVal = (double)buffFlow.BuffVal;
            double buffPer = (double)buffFlow.BuffPer;
            int skillStar = 0;
            int skillLv = 0;
            string[] args = skillKey.Split('.');
            if (args.Length >= 2 && !int.TryParse(args[1], out skillStar) || skillStar <= 0)
                skillStar = 0;
            if (args.Length >= 3 && !int.TryParse(args[2], out skillLv) || skillLv <= 0)
                skillLv = 1;
            foreach (var dst in buffDest)
            {
                foreach (int idx in buffList)
                {
                    if (idx >= 0 && idx < dst.Props.Length)
                    {
                        dst.Props[idx].Point += buffVal * (100 + skillLv - 1 + skillStar * 20) / 100d;
                        dst.Props[idx].Percent += buffPer * (100 + skillLv - 1 + skillStar * 20) / 100d;
                    }
                }
            }
        }
    }
    #endregion

}
