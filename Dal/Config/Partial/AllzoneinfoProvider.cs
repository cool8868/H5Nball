

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class AllZoneinfoProvider
    {
        public List<AllZoneinfoEntity> GetAllForFactory()
        {
            var database = new SqlDatabase(Properties.Settings.Default.NB_ConfigConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllZoneinfo_GetAll");



            List<AllZoneinfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }

            return list;
        }

        #region  GetByPlatform

        /// <summary>
        /// GetByPlatform
        /// </summary>
        /// <returns>AllZoneinfoEntity列表</returns>
        /// <remarks>2014/7/10 16:47:07</remarks>
        public List<AllZoneinfoEntity> GetByPlatform(System.String platformCode)
        {
            var database = new SqlDatabase(Properties.Settings.Default.NB_ConfigConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_AllZone_GetByPlatform");

            database.AddInParameter(commandWrapper, "@PlatformCode", DbType.AnsiString, platformCode);


            List<AllZoneinfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }

            return list;
        }

        #endregion		  
	}
}

