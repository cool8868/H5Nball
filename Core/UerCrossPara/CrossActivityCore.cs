using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Arena;
using Games.NBall.Core.Item;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;

namespace Games.NBall.Core.UerCrossPara
{
    /// <summary>
    /// 跨服活动
    /// </summary>
    public class CrossActivityCore
    {
        /// <summary>
        /// 跨服字典
        /// </summary>
        private Dictionary<EnumArenaDomainType, CrossActivityThread> _threadDic;

        public CrossActivityCore(int p)
        {
            _threadDic = new Dictionary<EnumArenaDomainType, CrossActivityThread>();
            var domainDic = CacheFactory.ArenaCache.GetDomainDic();
            foreach (var item in domainDic)
            {
                if (item.Value.Count == 0)
                    continue;
                _threadDic.Add(item.Key, new CrossActivityThread((int)item.Key));
            }

        }

        #region Instance

        public static CrossActivityCore Instance
        {
            get { return SingletonFactory<CrossActivityCore>.SInstance; }
        }

        #endregion

        public MessageCode Refresh()
        {
            try
            {
                foreach (var entity in _threadDic.Values)
                {
                    entity.Refresh();
                }
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("感恩回馈自动刷新", ex);
                return MessageCode.NbParameterError;
            }
        }

        public CrossActivityInfoResponse GetActivityInfo(Guid managerId, string zoneName)
        {
            try
            {
                var transferThread = GetThread(zoneName);
                if (transferThread == null)
                    return ResponseHelper.Create<CrossActivityInfoResponse>(MessageCode.NbParameterError);
                return transferThread.GetActivityInfo(managerId, zoneName);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("感恩回馈获取信息", ex);
                return ResponseHelper.Create<CrossActivityInfoResponse>(MessageCode.NbParameterError);
            }
        }

        public CrossActivityPrizeResponse Prize(Guid managerId, string zoneName)
        {
            try
            {
                var transferThread = GetThread(zoneName);
                if (transferThread == null)
                    return ResponseHelper.Create<CrossActivityPrizeResponse>(MessageCode.NbParameterError);
                return transferThread.Prize(managerId, zoneName);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("感恩抽奖", ex);
                return ResponseHelper.Create<CrossActivityPrizeResponse>(MessageCode.NbParameterError);
            }
        }
     
        public CrossActivityThread GetThread(string zoneName)
        {
            var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
            if (domainId == null || !_threadDic.ContainsKey((EnumArenaDomainType)domainId))
                return null;
            return _threadDic[(EnumArenaDomainType)domainId];
        }

    }
}
