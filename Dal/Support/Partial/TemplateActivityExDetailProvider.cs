

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class TemplateActivityexdetailProvider
    {
        #region  GetByZone

        /// <summary>
        /// GetByZone
        /// </summary>
        /// <returns>TemplateActivityexdetailEntity列表</returns>
        /// <remarks>2014/7/5 22:52:55</remarks>
        public List<TemplateActivityexdetailEntity> GetByZone(System.Int32 zoneId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityExDetail_GetByZone");

            database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);


            List<TemplateActivityexdetailEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<TemplateActivityexdetailEntity>();
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

