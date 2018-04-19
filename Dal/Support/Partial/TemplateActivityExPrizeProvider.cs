

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class TemplateActivityexprizeProvider
    {
        #region  GetByZone

        /// <summary>
        /// GetByZone
        /// </summary>
        /// <returns>TemplateActivityexprizeEntity列表</returns>
        /// <remarks>2015/1/4 17:53:06</remarks>
        public List<TemplateActivityexprizeEntity> GetByZone(System.Int32 zoneId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityExPrize_GetByZone");

            database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);


            List<TemplateActivityexprizeEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<TemplateActivityexprizeEntity>();
                while (reader.Read())
                {
                    var obj = LoadSingleRow(reader);
                    obj.ZoneActivityId = (System.Int32)reader["ZoneActivityId"];
                    list.Add(obj);
                }
            }

            return list;
        }

        #endregion


        #region  GetByZoneAll

        /// <summary>
        /// GetByZone
        /// </summary>
        /// <returns>TemplateActivityexprizeEntity列表</returns>
        /// <remarks>2015/1/4 17:53:06</remarks>
        public List<TemplateActivityexprizeEntity> GetByZoneAll()
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityExPrize_GetByZoneAll");


            List<TemplateActivityexprizeEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<TemplateActivityexprizeEntity>();
                while (reader.Read())
                {
                    var obj = LoadSingleRow(reader);
                    obj.ZoneActivityId = (System.Int32)reader["ZoneActivityId"];
                    list.Add(obj);
                }
            }

            return list;
        }

        #endregion		
	}
}

