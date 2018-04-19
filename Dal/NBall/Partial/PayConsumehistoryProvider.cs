

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class PayConsumehistoryProvider
    {
        #region  GetPointForActivity

        public List<PayConsumeManagerEntity> GetPointList(System.DateTime startTime, System.DateTime endTime)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_PayConsume_GetPointList");

            database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
            database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);


            List<PayConsumeManagerEntity> list = new List<PayConsumeManagerEntity>();
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                while (reader.Read())
                {
                    var obj = new PayConsumeManagerEntity();
                    obj.ManagerId = (System.Guid)reader["Idx"];
                    obj.Point = (System.Int32)reader["Point"];
                    list.Add(obj);
                }
            }
            return list;
        }
        #endregion

        #region  GetPointForActivity

        public List<PayConsumeManagerEntity> GetEqLockPointList(System.DateTime startTime, System.DateTime endTime)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_PayConsume_GetEquipmentLockProperty");

            database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
            database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);


            List<PayConsumeManagerEntity> list = new List<PayConsumeManagerEntity>();
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                while (reader.Read())
                {
                    var obj = new PayConsumeManagerEntity();
                    obj.ManagerId = (System.Guid)reader["Idx"];
                    obj.Point = (System.Int32)reader["Point"];
                    list.Add(obj);
                }
            }
            return list;
        }
        #endregion


        public bool GetEqLockPointForActivity(System.Guid managerId, System.DateTime startTime, System.DateTime endTime, ref  System.Int32 point, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_PayConsume_GetEquipmentLockPropertyForActivity");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
            database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
            database.AddParameter(commandWrapper, "@Point", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, point);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            point = (System.Int32)database.GetParameterValue(commandWrapper, "@Point");

            return Convert.ToBoolean(rValue);
        }
	}
}

