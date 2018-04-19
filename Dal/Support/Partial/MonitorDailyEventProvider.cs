

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class MonitorDailyeventProvider
    {
        public List<MonitorDailyeventEntity> GetListForShow(System.Int32 zoneId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_MonitorEvent_GetListForShow");

            database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);


            List<MonitorDailyeventEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                var clt = new List<MonitorDailyeventEntity>();
                while (reader.Read())
                {
                    clt.Add(LoadSingleRow(reader));
                }
                while (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        clt.Add(LoadSingleRow(reader));
                    }

                }
                return clt;
            }

            return list;
        }
	}
}
