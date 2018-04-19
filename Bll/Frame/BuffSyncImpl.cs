using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;

namespace Games.NBall.Bll.Frame
{
    public interface IBuffSync
    {
        void SetBuffMembers(Guid managerId);
        void SyncBuffMembers(Guid managerId, DTOBuffMemberView buffData, string siteId = "");
        void SyncBuffPools(Guid managerId, string siteId = "");
    }
    class BuffSyncThreadProvider : IBuffSync
    {
        #region Cache
        static NBThreadPool s_tpBuffMember;
        static NBThreadPool s_tpBuffPool;
        #endregion


        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile BuffSyncThreadProvider s_instnce = null;
        public readonly bool InitFlag = false;
        public static BuffSyncThreadProvider Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new BuffSyncThreadProvider();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        private BuffSyncThreadProvider()
        {
            s_tpBuffMember = new NBThreadPool(10);
            s_tpBuffPool = new NBThreadPool(10);
        }
        #endregion


        public void SetBuffMembers(Guid managerId)
        {
            s_tpBuffMember.Add(() => BuffDataCore.Instance().SetMembersCore(managerId));
        }

        public void SyncBuffMembers(Guid managerId, DTOBuffMemberView buffData, string siteId = "")
        {
            s_tpBuffMember.Add(() => BuffDataCore.Instance().SyncMembersCore(managerId, buffData, siteId));
        }

        public void SyncBuffPools(Guid managerId, string siteId = "")
        {
            s_tpBuffPool.Add(() => BuffPoolCore.Instance().ReqRawPools(managerId, siteId));
        }
    }

    public interface IArenaBuffSync
    {
        void SetBuffMembers(Guid managerId,EnumArenaType arenaType);
        void SyncBuffMembers(Guid managerId,EnumArenaType arenaType, DTOBuffMemberView buffData, string siteId = "");
        void SyncBuffPools(Guid managerId, string siteId = "");
    }
    class ArenaBuffSyncThreadProvider : IArenaBuffSync
    {
        #region Cache
        static NBThreadPool s_tpBuffMember;
        static NBThreadPool s_tpBuffPool;
        #endregion


        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile ArenaBuffSyncThreadProvider s_instnce = null;
        public readonly bool InitFlag = false;
        public static ArenaBuffSyncThreadProvider Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new ArenaBuffSyncThreadProvider();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        private ArenaBuffSyncThreadProvider()
        {
            s_tpBuffMember = new NBThreadPool(10);
            s_tpBuffPool = new NBThreadPool(10);
        }
        #endregion


        public void SetBuffMembers(Guid managerId,EnumArenaType arenaType)
        {
            s_tpBuffMember.Add(() => ArenaBuffDataCore.Instance().SetMembersCore(managerId, arenaType));
        }

        public void SyncBuffMembers(Guid managerId, EnumArenaType arenaType, DTOBuffMemberView buffData, string siteId = "")
        {
            s_tpBuffMember.Add(() => ArenaBuffDataCore.Instance().SyncMembersCore(managerId, arenaType, buffData, siteId));
        }

        public void SyncBuffPools(Guid managerId, string siteId = "")
        {
            s_tpBuffPool.Add(() => BuffPoolCore.Instance().ReqRawPools(managerId, siteId));
        }
    }
}
