

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Games.NBall.Entity.Response;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class PlayerkillInfoProvider
    { 
        #region  GetOpponent

        public List<PlayerKillOpponentEntity> GetOpponents(int minKpi, int maxKpi, int configByTimes)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_PlayerKill_GetOpponent");
            database.AddInParameter(commandWrapper, "@MinKpi", DbType.Int32, minKpi);
            database.AddInParameter(commandWrapper, "@MaxKpi", DbType.Int32, maxKpi);
            database.AddInParameter(commandWrapper, "@ConfigByTimes", DbType.Int32, configByTimes);


            List<PlayerKillOpponentEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<PlayerKillOpponentEntity>();
                while (reader.Read())
                {
                    var obj = new PlayerKillOpponentEntity();

                    obj.ManagerId = (System.Guid)reader["ManagerId"];
                    obj.Kpi = Convert.ToInt32(reader["Kpi"]);
                    obj.Name = (System.String)reader["Name"];
                    obj.Logo = (System.String)(reader["Logo"]);
                    obj.Level = Convert.ToInt32(reader["Level"]);
                    obj.Win = Convert.ToInt32(reader["Win"]);
                    obj.Lose = Convert.ToInt32(reader["Lose"]);
                    obj.Draw = Convert.ToInt32(reader["Draw"]);
                    obj.RemainByTimes = Convert.ToInt32(reader["RemainByTimes"]);
                    obj.FormationId = Convert.ToInt32(reader["FormationId"]);
                    list.Add(obj);
                }
            }

            return list;
        }

        public PlayerKillOpponentEntity GetOpponentByName(string name, int configByTimes)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_PlayerKill_GetOpponentByName");
            database.AddInParameter(commandWrapper, "@Name", DbType.String, name);
            database.AddInParameter(commandWrapper, "@ConfigByTimes", DbType.Int32, configByTimes);

            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                if (reader.Read())
                {
                    var obj = new PlayerKillOpponentEntity();

                    obj.ManagerId = (System.Guid)reader["ManagerId"];
                    obj.Kpi = (System.Int32)reader["Kpi"];
                    obj.Name = (System.String)reader["Name"];
                    obj.Logo = (System.String)(reader["Logo"]);
                    obj.Level = (System.Int32)reader["Level"];
                    obj.Win = (System.Int32)reader["Win"];
                    obj.Lose = (System.Int32)reader["Lose"];
                    obj.Draw = (System.Int32)reader["Draw"];
                    obj.RemainByTimes = (System.Int32)reader["RemainByTimes"];
                    return obj;
                }
            }

            return null;
        }
        #endregion		  
	}
}

