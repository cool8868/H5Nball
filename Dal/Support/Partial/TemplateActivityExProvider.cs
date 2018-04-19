

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class TemplateActivityexProvider
    {
        #region  GetByZoneId

        public List<TemplateActivityexEntity> GetByZoneId(int zoneId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityEx_GetByZone");
            database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);


            List<TemplateActivityexEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<TemplateActivityexEntity>();
                while (reader.Read())
                {
                    var obj = new TemplateActivityexEntity();

                    obj.Idx = (System.Int32)reader["Idx"];
                    obj.Name = (System.String)reader["Name"];
                    obj.ImageId = (System.Int32)reader["ImageId"];
                    obj.Status = (System.Int32)reader["Status"];
                    obj.RowTime = (System.DateTime)reader["RowTime"];
                    obj.UpdateTime = (System.DateTime)reader["UpdateTime"];
                    obj.StartTime = (System.DateTime)reader["StartTime"];
                    obj.EndTime = (System.DateTime)reader["EndTime"];
                    obj.CloseTime = (System.DateTime)reader["CloseTime"];
                    obj.ZoneActivityId = (System.Int32)reader["ZoneActivityId"];
                    list.Add(obj);
                }
            }

            return list;
        }

        #endregion	
	}
}

