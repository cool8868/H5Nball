

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Games.NBall.Entity.Response.Ladder;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class LadderMatchProvider
    { 
        #region  GetMatchTop10

        /// <summary>
        /// GetFiveMatch
        /// </summary>
        /// <returns>LadderMatchEntity列表</returns>
        /// <remarks>2014/1/20 10:41:36</remarks>
        public List<LadderMatchMarqueeEntity> GetMatchTop10()
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Ladder_GetMatchTop10");


            List<LadderMatchMarqueeEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<LadderMatchMarqueeEntity>();
                while (reader.Read())
                {
                    var obj = new LadderMatchMarqueeEntity();

                    obj.MatchId = (System.Guid)reader["Idx"];
                    obj.HomeName = (System.String)reader["HomeName"];
                    obj.AwayName = (System.String)reader["AwayName"];
                    obj.HomeScore = (System.Int32)reader["HomeScore"];
                    obj.AwayScore = (System.Int32)reader["AwayScore"];
                    list.Add(obj);
                }
            }

            return list;
        }

        #endregion		  
	}
}

