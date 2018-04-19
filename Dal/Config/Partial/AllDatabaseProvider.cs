

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class AllDatabaseProvider
    {
        public AllDatabaseProvider(int a, int b)
        {

        }
        /// <summary>
        /// GetAllForFactory
        /// </summary>
        /// <returns>ConfigDatabaseEntity列表</returns>
        /// <remarks>2013/9/19 7:25:34</remarks>
        public List<AllDatabaseEntity> GetAllForFactory()
        {
            var database = new SqlDatabase(Properties.Settings.Default.NB_ConfigConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllDatabase_GetAll");

            List<AllDatabaseEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }

            return list;
        }
	}
}

