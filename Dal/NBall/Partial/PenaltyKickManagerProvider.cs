

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Games.NBall.Entity.Response.Ad;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class PenaltykickManagerProvider
    {

        #region LoadSingleRow
        /// <summary>
        /// 将IDataReader的当前记录读取到PenaltykickManagerEntity 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public PenaltyKickRankEntity LoadSingleRow1(IDataReader reader)
        {
            var obj = new PenaltyKickRankEntity();

            obj.ManagerId = (System.Guid)reader["ManagerId"];
            obj.Name = (System.String)reader["Name"];
            obj.TotalScore = (System.Int32)reader["TotalScore"];
            obj.Rank = (System.Int32)reader["Rank"];
            obj.ScoreChangeTime = (System.DateTime)reader["ScoreChangeTime"];

            return obj;
        }
        #endregion

        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<PenaltyKickRankEntity> LoadRows1(IDataReader reader)
        {
            var clt = new List<PenaltyKickRankEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow1(reader));
            }
            return clt;
        }
        #endregion
        

        #region  GetRank

        /// <summary>
        /// GetRank
        /// </summary>
        /// <returns>PenaltykickManagerEntity列表</returns>
        /// <remarks>2016/9/13 20:03:50</remarks>
        public List<PenaltyKickRankEntity> GetRank()
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_PenaltyKick_GetRank");



            List<PenaltyKickRankEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows1(reader);
            }

            return list;
        }

        #endregion		  
		
	}
}
