

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class LadderHookProvider
    {
        #region  GetList

        /// <summary>
        /// GetList
        /// </summary>
        /// <returns>LadderHookEntity列表</returns>
        /// <remarks>2016/5/4 15:40:47</remarks>
        public List<LadderHookEntity> GetList()
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_LadderHook_GetList");

            List<LadderHookEntity> list = new List<LadderHookEntity>();
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                while (reader.Read())
                {
                    var obj = LoadSingleRow(reader);
                    obj.Score = (System.Int32)reader["Score"];
                    list.Add(obj);
                }
            }

            return list;
        }

        #endregion	
	}
}
