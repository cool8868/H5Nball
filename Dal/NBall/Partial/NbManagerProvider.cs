

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class NbManagerProvider
    {
        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="idx">idx</param>
        /// <returns>NbManagerEntity</returns>
        /// <remarks>2014/4/18 6:30:11</remarks>
        public NbManagerEntity GetWithKpi(System.Guid idx)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_GetWithKpi");

            database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);


            NbManagerEntity obj = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                if (reader.Read())
                {
                    obj = LoadSingleRow(reader);
                    obj.Kpi = (System.Int32)reader["Kpi"];
                }
            }
            return obj;
        }

        public bool ClearPackage(Guid managerId, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ItemPackage_ClearPackage");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            int results = 0;
            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            return Convert.ToBoolean(results);
        }


        #region  AssetRecord

        /// <summary>
        /// AssetRecord
        /// </summary>
        /// <param name="tranFlag">tranFlag</param>
        /// <param name="managerId">managerId</param>
        /// <param name="assetType">assetType</param>
        /// <param name="tranType">tranType</param>
        /// <param name="tranMap">tranMap</param>
        /// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/10/19 16:29:39</remarks>
        public bool AssetRecord(System.Boolean tranFlag, System.Guid managerId, System.Int32 assetType, System.Int32 tranType, System.String tranMap, ref  System.Int32 errorCode, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_AssetRecord");

            database.AddInParameter(commandWrapper, "@TranFlag", DbType.Boolean, tranFlag);
            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@AssetType", DbType.Int32, assetType);
            database.AddInParameter(commandWrapper, "@TranType", DbType.Int32, tranType);
            database.AddInParameter(commandWrapper, "@TranMap", DbType.AnsiString, tranMap);
            database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, errorCode);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode = (System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");

            return Convert.ToBoolean(rValue);
        }

        #endregion		  
		
	}
}

