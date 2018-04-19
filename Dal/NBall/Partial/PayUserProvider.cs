

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class PayUserProvider
    {
        public List<PayManagerEntity> GetPayList()
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PayUser_GetPayList");

            List<PayManagerEntity> list = new List<PayManagerEntity>();
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                while (reader.Read())
                {
                    var obj = new PayManagerEntity();
                    obj.Name = Convert.ToString(reader["Name"]);
                    obj.TotalCash = Convert.ToDouble(reader["TotalCash"]);
                    list.Add(obj);
                }
            }
            return list;
        }

        #region  Charge

        /// <summary>
        /// Charge
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="sourceType">sourceType</param>
        /// <param name="billingId">billingId</param>
        /// <param name="gamePoint">gamePoint</param>
        /// <param name="chargePoint">chargePoint</param>
        /// <param name="cash">cash</param>
        /// <param name="bonus">bonus</param>
        /// <param name="mallCode">bonus</param>
        /// <param name="result">result</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool ChargeTx(System.String account, System.Int32 sourceType, System.String billingId, System.Int32 gamePoint, System.Int32 chargePoint, System.Decimal cash, System.Int32 bonus,int mallCode, ref  System.Int32 result, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ChargeTx");

            database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
            database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
            database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, billingId);
            database.AddInParameter(commandWrapper, "@GamePoint", DbType.Int32, gamePoint);
            database.AddInParameter(commandWrapper, "@ChargePoint", DbType.Int32, chargePoint);
            database.AddInParameter(commandWrapper, "@Cash", DbType.Decimal, cash);
            database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, bonus);
            database.AddInParameter(commandWrapper, "@MallCode", DbType.Int32, mallCode);
            database.AddParameter(commandWrapper, "@Result", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, result);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            result = (System.Int32)database.GetParameterValue(commandWrapper, "@Result");

            return Convert.ToBoolean(rValue);
        }

        #endregion		  
		

        #region  ChargeForBonusV2 for MatchReward

        /// <summary>
        /// ChargeForBonus
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="sourceType">sourceType</param>
        /// <param name="billingId">billingId</param>
        /// <param name="bonus">bonus</param>
        /// <param name="result">result</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool ChargeForBonusV2(System.String account, System.Int32 sourceType, System.String billingId, System.Int32 bonus, ref  System.Int32 totalPoint, ref  System.Int32 result, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ChargeForBonusV2");

            database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
            database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
            database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, billingId);
            database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, bonus);
            database.AddParameter(commandWrapper, "@TotalPoint", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, totalPoint);
            database.AddParameter(commandWrapper, "@Result", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, result);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            totalPoint = (System.Int32)database.GetParameterValue(commandWrapper, "@TotalPoint");
            result = (System.Int32)database.GetParameterValue(commandWrapper, "@Result");

            return Convert.ToBoolean(rValue);
        }

        #endregion		  
	}
}

