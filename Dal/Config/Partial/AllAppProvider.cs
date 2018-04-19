

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class AllAppProvider
    { /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>AllAppEntity列表</returns>
        /// <remarks>2014/3/21 15:59:49</remarks>
        public List<AllAppEntity> GetAllForFactory()
        {
            var database = new SqlDatabase(Properties.Settings.Default.NB_ConfigConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllApp_GetAll");



            List<AllAppEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }

            return list;
        }
	}
}

