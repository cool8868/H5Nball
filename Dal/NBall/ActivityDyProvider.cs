using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Games.NBall.Dal.NBall
{
    public class ActivityDyProvider
    {
        public List<ActivityDyUserEntity> GetManagerIdDyStrength(string zoneId = "")
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("B_Statistic_ActivityDouyuStrength");
            database.AddInParameter(commandWrapper, "@Zone", DbType.String, zoneId);

            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                List<ActivityDyUserEntity> list = new List<ActivityDyUserEntity>();
                while (reader.Read())
                {
                    var entity = new ActivityDyUserEntity();
                    entity.ZoneName = (System.String)reader["ZoneName"];
                    entity.ZoneId = (System.String) reader["ZoneId"];
                    entity.Account = (System.String) reader["Account"];
                    entity.ManagerId = (Guid)reader["ManagerId"];
                    entity.ExcitingId = Convert.ToInt32(reader["ExcitingId"]);
                    entity.Curdata = Convert.ToInt32(reader["Curdata"]);
                    entity.Status = Convert.ToInt32(reader["Status"]);
                    list.Add(entity);
                }
                return list;
            }
        }

        public List<ActivityDyUserEntity> GetDyLadderRank(string zoneId = "")
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("B_Statistic_ActivityDouyuLadderRank");
            database.AddInParameter(commandWrapper, "@Zone", DbType.String, zoneId);

            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                List<ActivityDyUserEntity> list = new List<ActivityDyUserEntity>();
                while (reader.Read())
                {
                    var entity = new ActivityDyUserEntity();
                    entity.ZoneName = (System.String)reader["ZoneName"];
                    entity.Account = (System.String)reader["Account"];
                    entity.ManagerId = (Guid)reader["ManagerId"];
                    entity.Curdata = Convert.ToInt32(reader["Curdata"]);
                    list.Add(entity);
                }
                return list;
            }
        }

        public List<ActivityDyUserEntity> GetDyPowerRank(string zoneId = "")
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("B_Statistic_ActivityDouyuPowerRank");
            database.AddInParameter(commandWrapper, "@Zone", DbType.String, zoneId);

            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                List<ActivityDyUserEntity> list = new List<ActivityDyUserEntity>();
                while (reader.Read())
                {
                    var entity = new ActivityDyUserEntity();
                    entity.ZoneName = (System.String)reader["ZoneName"];
                    entity.Account = (System.String)reader["Account"];
                    entity.ManagerId = (Guid)reader["ManagerId"];
                    entity.ExcitingId = Convert.ToInt32(reader["ExcitingId"]);
                    entity.Curdata = Convert.ToInt32(reader["Curdata"]);
                    list.Add(entity);
                }
                return list;
            }
        }

        protected string ConnectionString = "";
        private const EnumDbType DBTYPE = EnumDbType.Main;

        public ActivityDyProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }
        public ActivityDyProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName, DBTYPE);
        }
    }
    
}
