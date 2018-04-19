

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class GambleHostProvider
    {
        #region LoadSingleRow
        /// <summary>
        /// 将IDataReader的当前记录读取到GambleHostEntity 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public GambleHostEntity LoadSingleRow2(IDataReader reader)
        {
            var obj = new GambleHostEntity();

            obj.Idx = (System.Int32)reader["Idx"];
            obj.ManagerId = (System.Guid)reader["ManagerId"];
            obj.ManagerName = (System.String)reader["ManagerName"];
            obj.TitleId = (System.Guid)reader["TitleId"];
            obj.HostMoney = (System.Int32)reader["HostMoney"];
            obj.TotalMoney = (System.Int32)reader["TotalMoney"];
            obj.AttendPeopleCount = (System.Int32)reader["AttendPeopleCount"];
            obj.Status = (System.Int32)reader["Status"];
            obj.RowTime = (System.DateTime)reader["RowTime"];
            obj.RowVersion = (System.Byte[])reader["RowVersion"];
            obj.Title = (System.String)reader["Title"];

            DateTime startTime = (System.DateTime)reader["StartTime"];
            DateTime BaseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            obj.StartedTime = Convert.ToInt64(startTime.Subtract(BaseTime).TotalMilliseconds);
            DateTime stopTime = (System.DateTime)reader["StopTime"];
            obj.StopedTime = Convert.ToInt64(stopTime.Subtract(BaseTime).TotalMilliseconds);
            return obj;
        }
        /// <summary>
        /// 将IDataReader的当前记录读取到GambleHostEntity 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public GambleHostEntity LoadSingleRow3(IDataReader reader)
        {
            var obj = new GambleHostEntity();

            obj.Idx = (System.Int32)reader["Idx"];
            obj.ManagerId = (System.Guid)reader["ManagerId"];
            obj.ManagerName = (System.String)reader["ManagerName"];
            obj.TitleId = (System.Guid)reader["TitleId"];
            obj.HostMoney = (System.Int32)reader["HostMoney"];
            obj.TotalMoney = (System.Int32)reader["TotalMoney"];
            obj.AttendPeopleCount = (System.Int32)reader["AttendPeopleCount"];
            obj.Status = (System.Int32)reader["Status"];
            obj.RowTime = (System.DateTime)reader["RowTime"];
            obj.RowVersion = (System.Byte[])reader["RowVersion"];
            obj.Title = (System.String)reader["Title"];
            obj.RightOption = (System.String)reader["RightOption"];

            DateTime startTime = (System.DateTime)reader["StartTime"];
            DateTime BaseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            obj.StartedTime = Convert.ToInt64(startTime.Subtract(BaseTime).TotalMilliseconds);
            DateTime stopTime = (System.DateTime)reader["StopTime"];
            obj.StopedTime = Convert.ToInt64(stopTime.Subtract(BaseTime).TotalMilliseconds);
            return obj;
        }
        #endregion

        #region  GetById

        /// <summary>
        /// GetStartList
        /// </summary>
        /// <returns>GambleHostEntity</returns>
        /// <remarks>2014/6/16 1:48:57</remarks>
        public List<GambleHostEntity> GetStartList()
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHost_GetStartList");


            List<GambleHostEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows2(reader);
            }

            return list;
        }

        #endregion
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<GambleHostEntity> LoadRows2(IDataReader reader)
        {
            var clt = new List<GambleHostEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow2(reader));
            }
            return clt;
        }
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<GambleHostEntity> LoadRows3(IDataReader reader)
        {
            var clt = new List<GambleHostEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow3(reader));
            }
            return clt;
        }
        #endregion

        #region  InsertOnce

        /// <summary>
        /// InsertOnce
        /// </summary>
        /// <param name="managerId">managerId</param>
        /// <param name="managerName">managerName</param>
        /// <param name="titleId">titleId</param>
        /// <param name="totalMoney">totalMoney</param>
        /// <param name="status">status</param>
        /// <param name="rowTime">rowTime</param>
        /// <param name="idx">idx</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/6/18 0:59:27</remarks>
        public bool InsertOnce2(GambleHostEntity e, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHost_InsertOnce");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, e.ManagerId);
            database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, e.ManagerName);
            database.AddInParameter(commandWrapper, "@TitleId", DbType.Guid, e.TitleId);
            database.AddInParameter(commandWrapper, "@HostMoney", DbType.Int32, e.HostMoney);
            database.AddInParameter(commandWrapper, "@TotalMoney", DbType.Int32, e.TotalMoney);
            //database.AddInParameter(commandWrapper, "@Status", DbType.Int32, e.Status);
            database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, e.RowTime);
            database.AddParameter(commandWrapper, "@Idx", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, e.Idx);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            e.Idx = (System.Int32)database.GetParameterValue(commandWrapper, "@Idx");

            return Convert.ToBoolean(e.Idx);
        }

        #endregion

        #region  GetByManagerIdTop10

        /// <summary>
        /// GetByManagerIdTop10
        /// </summary>
        /// <returns>GambleHostEntity列表</returns>
        /// <remarks>2014/6/18 0:59:23</remarks>
        public List<GambleHostEntity> GetByManagerIdTop10(System.Guid managerId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHost_GetByManagerIdTop10");

            database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);


            List<GambleHostEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows3(reader);
            }

            return list;
        }

        #endregion		
	}
}
