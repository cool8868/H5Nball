

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class TemplateActivityexgroupProvider
    {
        #region  GetByZone

        /// <summary>
        /// GetByZone
        /// </summary>
        /// <returns>TemplateActivityexgroupEntity列表</returns>
        /// <remarks>2014/7/7 8:51:25</remarks>
        public List<TemplateActivityexgroupEntity> GetByZone(System.Int32 zoneId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityExGroup_GetByZone");

            database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);


            List<TemplateActivityexgroupEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<TemplateActivityexgroupEntity>();
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

