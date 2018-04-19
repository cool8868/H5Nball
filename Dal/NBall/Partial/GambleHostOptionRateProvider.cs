

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class GambleHostoptionrateProvider
    {
        #region  GetByHostId

        /// <summary>
        /// GetByHostId
        /// </summary>
        /// <returns>GambleHostoptionrateEntity列表</returns>
        /// <remarks>2014/6/14 23:47:44</remarks>
        public List<GambleHostoptionrateEntity> GetByHostId2(System.Int32 hostId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHostOptionRate_GetByHostId");

            database.AddInParameter(commandWrapper, "@hostId", DbType.Int32, hostId);


            List<GambleHostoptionrateEntity> list = null;
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
        public List<GambleHostoptionrateEntity> LoadRows2(IDataReader reader)
        {
            var clt = new List<GambleHostoptionrateEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow2(reader));
            }
            return clt;
        }
        #endregion

        #region LoadSingleRow
        /// <summary>
        /// 将IDataReader的当前记录读取到GambleHostoptionrateEntity 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public GambleHostoptionrateEntity LoadSingleRow2(IDataReader reader)
        {
            var obj = new GambleHostoptionrateEntity();

            obj.Idx = (System.Int32)reader["Idx"];
            obj.HostId = (System.Int32)reader["HostId"];
            obj.OptionId = (System.Guid)reader["OptionId"];
            obj.WinRate = (System.Decimal)reader["WinRate"];
            obj.GambleMoney = (System.Int32)reader["GambleMoney"];
            obj.Status = (System.Int32)reader["Status"];
            obj.RowTime = (System.DateTime)reader["RowTime"];
            obj.RowVersion = (System.Byte[])reader["RowVersion"];
            obj.OptionContent = (System.String)reader["OptionContent"];
            return obj;
        }
        #endregion
	}
}
