

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

    public partial class CrossladderMatchProvider
    {
        #region  GetMatchTop10

        /// <summary>
        /// GetFiveMatch
        /// </summary>
        /// <returns>LadderMatchEntity列表</returns>
        /// <remarks>2014/1/20 10:41:36</remarks>
        public List<LadderMatchMarqueeEntity> GetMatchTop10(int domainId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossLadder_GetMatchTop10");
            database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);

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
                    obj.HomeLogo = (System.String)reader["HomeLogo"];
                    obj.AwayLogo = (System.String)reader["AwayLogo"];
                    list.Add(obj);
                }
            }

            return list;
        }

        #endregion		
	}
}
