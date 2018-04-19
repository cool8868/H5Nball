

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class ConfigRevelationProvider
    {
        #region LoadSingleRow
        /// <summary>
        /// 将IDataReader的当前记录读取到ConfigRevelationEntity 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public ConfigRevelationEntity LoadSingleRow1(IDataReader reader)
        {
            var obj = new ConfigRevelationEntity();

            obj.Idx = (System.Int32)reader["Idx"];
            obj.MarkId = (System.Int32)reader["MarkId"];
            obj.Schedule = (System.Int32)reader["Schedule"];
            obj.NpcId = (System.Guid)reader["NpcId"];
            obj.MarkPlayerId = (System.Int32)reader["MarkPlayerId"];
            obj.PassPrizeItem = (System.String)reader["PassPrizeItem"];
            obj.FirstPassItem = (System.String)reader["FirstPassItem"];
            obj.CourageNumber = (System.Int32)reader["CourageNumber"];

            return obj;
        }
        #endregion

        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigRevelationEntity> LoadRows1(IDataReader reader)
        {
            var clt = new List<ConfigRevelationEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow1(reader));
            }
            return clt;
        }
        #endregion
        

        #region  GetAllFor

        /// <summary>
        /// GetAllFor
        /// </summary>
        /// <returns>ConfigRevelationEntity列表</returns>
        /// <remarks>2017/1/11 16:24:03</remarks>
        public List<ConfigRevelationEntity> GetAllFor()
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_ConfigRevelation_GetAllFor");



            List<ConfigRevelationEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows1(reader);
            }

            return list;
        }

        #endregion		  
		
	}
}
