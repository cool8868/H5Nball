

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class TxLogininfoProvider
    {

        #region  InsertUpdate

        /// <summary>
        /// InsertUpdate
        /// </summary>
        /// <param name="entity">openId</param>
        /// <param name="trans">openId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/12/19 16:54:28</remarks>
        public bool InsertUpdate(TxLogininfoEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_TxLoginInfo_InsertUpdate");

            database.AddInParameter(commandWrapper, "@OpenId", DbType.AnsiString, entity.OpenId);
            database.AddInParameter(commandWrapper, "@OpenKey", DbType.AnsiString, entity.OpenKey);
            database.AddInParameter(commandWrapper, "@Pf", DbType.AnsiString, entity.Pf);
            database.AddInParameter(commandWrapper, "@Format", DbType.AnsiString, entity.Format);
            database.AddInParameter(commandWrapper, "@Ext", DbType.AnsiString, entity.Ext);
            database.AddInParameter(commandWrapper, "@Ext1", DbType.AnsiString, entity.Ext1);
            database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
            database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }

            return Convert.ToBoolean(rValue);
        }

        #endregion		  
		
	}
}
