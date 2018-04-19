

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class AllUaplatformProvider
    {
        #region  GetByFactory

        /// <summary>
        /// GetByFactory
        /// </summary>
        /// <returns>AllUaplatformEntity列表</returns>
        /// <remarks>2014/7/9 16:00:19</remarks>
        public List<AllUaplatformEntity> GetByFactory(System.String factoryCode)
        {
            var database = new SqlDatabase(Properties.Settings.Default.NB_ConfigConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_AllPlatform_GetByFactory");

            database.AddInParameter(commandWrapper, "@FactoryCode", DbType.String, factoryCode);
            List<AllUaplatformEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }

            return list;
        }

        #endregion

        #region  GetById

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="idx">idx</param>
        /// <returns>AllUaplatformEntity</returns>
        /// <remarks>2014/7/9 21:22:33</remarks>
        public AllUaplatformEntity GetByCode(string platformCode)
        {
            var database = new SqlDatabase(Properties.Settings.Default.NB_ConfigConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_AllPlatform_GetByCode");

            database.AddInParameter(commandWrapper, "@PlatformCode", DbType.AnsiString, platformCode);


            AllUaplatformEntity obj = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                if (reader.Read())
                {


                    obj = LoadSingleRow(reader);
                }
            }
            return obj;
        }

        #endregion		  
	}
}

