

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class CrosscrowdInfoProvider
    {
        #region  GetForSendRankPrize
        public List<CrosscrowdSendRankPrizeEntity> GetForSendRankPrize(int crowdId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Crowd_GetForSendRankPrize");
            database.AddInParameter(commandWrapper, "@CrowId", DbType.Int32, crowdId);
            List<CrosscrowdSendRankPrizeEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<CrosscrowdSendRankPrizeEntity>();
                while (reader.Read())
                {
                    var obj = new CrosscrowdSendRankPrizeEntity();

                    obj.Idx = (System.Int32)reader["Idx"];
                    obj.ManagerId = (System.Guid)reader["ManagerId"];
                    obj.Rank = (System.Int32)reader["Rank"];
                    obj.Score = (System.Int32)reader["Score"];
                    obj.Status = (System.Int32)reader["Status"];
                    obj.SiteId = (System.String)reader["SiteId"];
                    list.Add(obj);
                }
            }
            return list;
        }

        #endregion

        #region  GetMaxKiller
        public CrosscrowdSendRankPrizeEntity GetMaxKiller(int crowdId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Crowd_GetMaxKiller");
            database.AddInParameter(commandWrapper, "@CrowId", DbType.Int32, crowdId);

            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                if (reader.Read())
                {
                    var obj = new CrosscrowdSendRankPrizeEntity();

                    obj.Idx = (System.Int32)reader["Idx"];
                    obj.ManagerId = (System.Guid)reader["ManagerId"];
                    obj.Rank = (System.Int32)reader["Rank"];
                    obj.Score = (System.Int32)reader["Score"];
                    obj.Status = (System.Int32)reader["Status"];
                    obj.SiteId = (System.String)reader["SiteId"];
                    return obj;
                }
            }
            return null;
        }

        #endregion

        #region  GetForSendKillPrize
        public List<CrosscrowdSendKillPrizeEntity> GetForSendKillPrize(int crowdId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Crowd_GetForSendKillPrize");
            database.AddInParameter(commandWrapper, "@CrowId", DbType.Int32, crowdId);
            List<CrosscrowdSendKillPrizeEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<CrosscrowdSendKillPrizeEntity>();
                while (reader.Read())
                {
                    var obj = new CrosscrowdSendKillPrizeEntity();

                    obj.Idx = (System.Guid)reader["Idx"];
                    obj.HomeId = (System.Guid)reader["HomeId"];
                    obj.AwayId = (System.Guid)reader["AwayId"];
                    obj.HomeName = (System.String)reader["HomeName"];
                    obj.AwayName = (System.String)reader["AwayName"];
                    obj.HomeSiteId = (System.String)reader["HomeSiteId"];
                    obj.AwaySiteId = (System.String)reader["AwaySiteId"];
                    obj.HomeMorale = (System.Int32)reader["HomeMorale"];
                    obj.AwayMorale = (System.Int32)reader["AwayMorale"];
                    obj.Status = (System.Int32)reader["Status"];
                    list.Add(obj);
                }
            }
            return list;
        }

        #endregion	
	}
}
