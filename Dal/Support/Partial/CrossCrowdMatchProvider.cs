

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class CrosscrowdMatchProvider
    {

        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/11/16 17:15:56</remarks>
        public bool Save(CrosscrowdMatchEntity entity, DateTime resurrectionTime, DateTime nextMatchTime, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.C_CrowdMatch_Save");


            database.AddInParameter(commandWrapper, "@CrowdId", DbType.Int32, entity.CrossCrowdId);
            database.AddInParameter(commandWrapper, "@PairIndex", DbType.Int32, entity.PairIndex);
            database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
            database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
            database.AddInParameter(commandWrapper, "@HomeKpi", DbType.Int32, entity.HomeKpi);
            database.AddInParameter(commandWrapper, "@AwayKpi", DbType.Int32, entity.AwayKpi);

            database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
            database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
            database.AddInParameter(commandWrapper, "@HomeSiteId", DbType.String, entity.HomeSiteId);
            database.AddInParameter(commandWrapper, "@AwaySiteId", DbType.String, entity.AwaySiteId);
            database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
            database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
            database.AddInParameter(commandWrapper, "@HomePrizeCoin", DbType.Int32, entity.HomePrizeCoin);
            database.AddInParameter(commandWrapper, "@HomePrizeHonor", DbType.Int32, entity.HomePrizeHonor);
            database.AddInParameter(commandWrapper, "@HomeCostMorale", DbType.Int32, entity.HomeCostMorale);
            database.AddInParameter(commandWrapper, "@HomePrizeScore", DbType.Int32, entity.HomePrizeScore);
            database.AddInParameter(commandWrapper, "@AwayPrizeCoin", DbType.Int32, entity.AwayPrizeCoin);
            database.AddInParameter(commandWrapper, "@AwayPrizeHonor", DbType.Int32, entity.AwayPrizeHonor);
            database.AddInParameter(commandWrapper, "@AwayCostMorale", DbType.Int32, entity.AwayCostMorale);
            database.AddInParameter(commandWrapper, "@AwayPrizeScore", DbType.Int32, entity.AwayPrizeScore);
            database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
            database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
            database.AddInParameter(commandWrapper, "@ResurrectionTime", DbType.DateTime, resurrectionTime);
            database.AddInParameter(commandWrapper, "@NextMatchTime", DbType.DateTime, nextMatchTime);

            database.AddParameter(commandWrapper, "@HomeMorale", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, entity.HomeMorale);
            database.AddParameter(commandWrapper, "@AwayMorale", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, entity.AwayMorale);
            database.AddParameter(commandWrapper, "@IsKill", DbType.Boolean, ParameterDirection.InputOutput, "", DataRowVersion.Current, entity.IsKill);

            database.AddParameter(commandWrapper, "@Idx", DbType.Guid, ParameterDirection.InputOutput, "", DataRowVersion.Current, entity.Idx);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx = (System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            entity.HomeMorale = (System.Int32)database.GetParameterValue(commandWrapper, "@HomeMorale");
            entity.AwayMorale = (System.Int32)database.GetParameterValue(commandWrapper, "@AwayMorale");
            entity.IsKill = (System.Boolean)database.GetParameterValue(commandWrapper, "@IsKill");
            return Convert.ToBoolean(results);
        }
	}
}
