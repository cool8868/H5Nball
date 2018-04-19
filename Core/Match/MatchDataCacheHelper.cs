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
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.ServiceEngine;

namespace Games.NBall.Core
{
    public class MatchDataCacheHelper
    {

        public static void DeleteTeamembersCache(Guid managerId, bool isSync)
        {
            MemcachedFactory.TeammembersClient.Delete(managerId);
            //BuffDataCore.Instance().SetMembers(managerId);
            KpiHandler.Instance.RebuildKpi(managerId, isSync);
        }


        public static void DeleteSolutionCache(Guid managerId, bool isSync)
        {
            MemcachedFactory.SolutionClient.Delete(managerId);
            MemcachedFactory.TeammembersClient.Delete(managerId);
            //BuffDataCore.Instance().SetMembers(managerId);
            BuffPoolCore.Instance().ClearMemPools(managerId);
            KpiHandler.Instance.RebuildKpi(managerId, isSync);
        }

        public static void DeleteTeammemberAndSolutionCache(Guid managerId, bool isSync)
        {
            MemcachedFactory.TeammembersClient.Delete(managerId);
            MemcachedFactory.SolutionClient.Delete(managerId);
            //BuffDataCore.Instance().SetMembers(managerId);
            KpiHandler.Instance.RebuildKpi(managerId, isSync);
        }

        public static int DeleteTeammemberAndSolutionCache(Guid managerId, EnumArenaType arenaType,bool isUpdateKpi=true)
        {
            try
            {
                MemcachedFactory.ArenaTeammembersClient.Delete(arenaType.ToString() + managerId.ToString());
                MemcachedFactory.ArenaSolutionClient.Delete(arenaType.ToString() + managerId.ToString());
                if (isUpdateKpi)
                    return KpiHandler.Instance.RebuildKpi(managerId, (int) arenaType);
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
