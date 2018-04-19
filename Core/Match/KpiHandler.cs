using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Item;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.ServiceEngine;

namespace Games.NBall.Core
{
    public class KpiHandler
    {
        NBThreadPool _threadPool;
        #region .ctor
        public KpiHandler(int p)
        {
            this._threadPool = new NBThreadPool(5);
        }
        #endregion

        #region Facade

        public static KpiHandler Instance
        {
            get { return SingletonFactory<KpiHandler>.SInstance; }
        }

        public void RebuildKpi(Guid managerId, bool isSync)
        {
            if (isSync)
            {
                doRebuildKpi(managerId);
            }
            else
            {
                _threadPool.Add(() => doRebuildKpi(managerId));
            }
        }

        public int RebuildKpi(Guid managerId, int arenaType)
        {
            return doRebuildKpi(managerId, arenaType);
        }

        #endregion

        #region encapsulation
        void doRebuildKpi(Guid managerId)
        {
            try
            {
                var buffView = BuffDataCore.Instance().RebuildMembers(managerId);
                if (buffView != null)
                {
                   // ChatHelper.SendUpdateKpi(managerId, buffView.Kpi);
                    MatchDataHelper.UpdateManagerKpi(managerId, buffView.Kpi);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("doRebuildKpi", ex);
            }
        }

        int doRebuildKpi(Guid managerId, int arenaType)
        {
            try
            {
                var arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType)arenaType);
                var buffView = ArenaBuffDataCore.Instance().RebuildMembers(managerId, arenaFrame);
                if (buffView != null)
                {
                    if (buffView.Kpi > 0)
                    {
                        arenaFrame.Kpi = buffView.Kpi;
                        arenaFrame.Save();
                    }
                    return buffView.Kpi;
                }
                return -1;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("doRebuildKpi", ex);
                return -1;
            }
        }
        #endregion
    }
}
