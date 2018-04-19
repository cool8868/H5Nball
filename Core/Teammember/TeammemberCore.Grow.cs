using System.Data.Common;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Teammember
{
    public partial class TeammemberCore
    {
        #region Facade
        /// <summary>
        /// 球员成长里的球员列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TeammemberListForGrowResponse GetTeammemberListForGrow(Guid managerId)
        {
            var teammembers = MatchDataHelper.GetTeammembers(managerId);
            var grows = TeammemberGrowMgr.GetByManager(managerId);
            var response = ResponseHelper.CreateSuccess<TeammemberListForGrowResponse>();
            response.Data = new TeammemberListForGrowEntity();
            response.Data.Teammembers = new List<TeammemberForGrowEntity>(teammembers.Count);
            foreach (var entity in teammembers)
            {
                response.Data.Teammembers.Add(BuildTeammemberForGrow(entity, grows));
            }
            return response;
        }

        #region 获取球员成长数据
        /// <summary>
        /// 获取球员成长数据
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="teammemberId">球员ID</param>
        /// <returns></returns>
        public TeammemberGrowResponse GetTeammemberGrowInfo(Guid managerId, Guid teammemberId)
        {
            var response = ResponseHelper.CreateSuccess<TeammemberGrowResponse>();
            response.Data = GetTeammemberGrow(managerId,teammemberId);
            var manager = ManagerCore.Instance.GetManager(managerId);
            response.Data.ManagerReiki = manager.Reiki;
            return response;
        }

        public TeammemberGrowEntity GetTeammemberGrow(Guid managerId, Guid teammemberId)
        {
            TeammemberGrowEntity entity = TeammemberGrowMgr.GetById(teammemberId);
            if (entity == null || entity.ManagerId != managerId)
            {
                return null;
            }
            CalTeammemberGrowData(entity);
            return entity;
        }

        void CalTeammemberGrowData(TeammemberGrowEntity entity)
        {
            if (entity.RecordDate != DateTime.Now.Date)
            {
                entity.RecordDate = DateTime.Now.Date;
                entity.DayFastGrowCount = 0;
                entity.DayFreeFastGrowCount = 0;
                entity.DayGrowCount = 0;
            }
            else
            {
                CalFreeFastGrowData(entity);
            }
            //根据等级获取成长配置数据
            DicGrowEntity dicGrow = CacheFactory.TeammemberCache.GetGrow(entity.GrowLevel);
            if (dicGrow != null)
            {

                entity.BreakGrowNum = dicGrow.GrowNum;
                entity.BreakRate = dicGrow.BreakRate;
                entity.GrowCostReiki = dicGrow.Reiki;
                if (entity.FreeFastGrowCount < 1)
                    entity.FastGrowCostReiki = dicGrow.FastReiki;
                else
                {
                    entity.FastGrowCostReiki = 0;
                }
            }
        }

        void CalFreeFastGrowData(TeammemberGrowEntity entity)
        {
            if (entity.DayGrowCount >= _growCountToFast && entity.DayFreeFastGrowCount <= 0)
            {
                entity.FreeFastGrowCount = 1;
                entity.FastGrowCostReiki = 0;
            }
        }
        #endregion
     
        #endregion

        #region encapsulation

        TeammemberForGrowEntity BuildTeammemberForGrow(TeammemberEntity entity,List<TeammemberGrowEntity> grows)
        {
            var growEntity = grows.Find(d => d.Idx == entity.Idx);
            var grow = new TeammemberForGrowEntity();
            grow.TeammemberId = entity.Idx;
            grow.PlayerId = entity.PlayerId;
            grow.Kpi = entity.Kpi;
            grow.GrowLevel = growEntity==null?1:growEntity.GrowLevel;
            grow.Level = entity.Level;
            return grow;
        }
        #endregion


    }
}
