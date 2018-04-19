

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class GambleDetailProvider
    {
        #region LoadSingleRow
        /// <summary>
        /// 将IDataReader的当前记录读取到GambleDetailEntity 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public GambleDetailEntity LoadSingleRow2(IDataReader reader)
        {
            var obj = new GambleDetailEntity();

            obj.Idx = (System.Int32)reader["Idx"];
            obj.ManagerId = (System.Guid)reader["ManagerId"];
            obj.ManagerName = (System.String)reader["ManagerName"];
            obj.HostOptionId = (System.Int32)reader["HostOptionId"];
            obj.GambleMoney = (System.Int32)reader["GambleMoney"];
            obj.ResultMoney = (System.Int32)reader["ResultMoney"];
            obj.Status = (System.Int32)reader["Status"];
            obj.RowTime = (System.DateTime)reader["RowTime"];
            obj.RowVersion = (System.Byte[])reader["RowVersion"];
            obj.WinRate = (System.Decimal)reader["WinRate"];
            obj.OptionContent = (System.String)reader["OptionContent"];
            obj.Title = (System.String)reader["Title"];
            obj.RightOption = (System.String)reader["RightOption"];
            return obj;
        }
        #endregion
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<GambleDetailEntity> LoadRows2(IDataReader reader)
        {
            var clt = new List<GambleDetailEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow2(reader));
            }
            return clt;
        }
        #endregion
        #region  GetByManagerIdTop10

        /// <summary>
        /// GetByManagerIdTop10
        /// </summary>
        /// <returns>GambleDetailEntity列表</returns>
        /// <remarks>2014/6/16 23:43:08</remarks>
        public List<GambleDetailEntity> GetByManagerIdTop10(System.Guid managerId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_GamBleDetail_GetByManagerIdTop10");

            database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);


            List<GambleDetailEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows2(reader);
            }

            return list;
        }

        #endregion	
	}
}
