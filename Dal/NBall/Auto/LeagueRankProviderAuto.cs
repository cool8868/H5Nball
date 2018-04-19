

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Dal
{
    
    public partial class LeagueRankProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Main;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到LeagueRankEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LeagueRankEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LeagueRankEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ManagerName = (System.String) reader["ManagerName"];
            obj.LeagueRecordId = (System.Guid) reader["LeagueRecordId"];
            obj.LeagueRank = (System.Int32) reader["LeagueRank"];
            obj.Score = (System.Int32) reader["Score"];
            obj.Goal = (System.Int32) reader["Goal"];
            obj.Lose = (System.Int32) reader["Lose"];
            obj.WinCount = (System.Int32) reader["WinCount"];
            obj.DrawCount = (System.Int32) reader["DrawCount"];
            obj.LostCount = (System.Int32) reader["LostCount"];
            obj.Status = (System.Int32) reader["Status"];
            obj.Rowtime = (System.DateTime) reader["Rowtime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<LeagueRankEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LeagueRankEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow(reader));
            }
            return clt;
        }
        #endregion
        
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public LeagueRankProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LeagueRankProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>LeagueRankEntity</returns>
        /// <remarks>2016/1/5 15:29:15</remarks>
        public LeagueRankEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueRank_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            LeagueRankEntity obj=null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                if(reader.Read())
                {
                    
            
                    obj = LoadSingleRow(reader);
                }
            }
            return obj;
        }
		
		#endregion		  
		
		#region  GetAll
		
		/// <summary>
        /// GetAll
        /// </summary>
        /// <returns>LeagueRankEntity列表</returns>
        /// <remarks>2016/1/5 15:29:15</remarks>
        public List<LeagueRankEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueRank_GetAll");
            

            
            List<LeagueRankEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByLeagueRecordId
		
		/// <summary>
        /// GetByLeagueRecordId
        /// </summary>
        /// <returns>LeagueRankEntity列表</returns>
        /// <remarks>2016/1/5 15:29:15</remarks>
        public List<LeagueRankEntity> GetByLeagueRecordId( System.Guid leagueRecordId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_LeagueRank_GetByLeagueRecordId");
            
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, leagueRecordId);

            
            List<LeagueRankEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/5 15:29:15</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueRank_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  UpdateRank
		
		/// <summary>
        /// UpdateRank
        /// </summary>
		/// <param name="leagueRecordId">leagueRecordId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/5 15:29:15</remarks>
        public bool UpdateRank ( System.Guid leagueRecordId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_LeagueRank_UpdateRank");
            
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, leagueRecordId);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/5 15:29:15</remarks>
        public bool Insert(LeagueRankEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueRank_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, entity.LeagueRecordId);
			database.AddInParameter(commandWrapper, "@LeagueRank", DbType.Int32, entity.LeagueRank);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@Goal", DbType.Int32, entity.Goal);
			database.AddInParameter(commandWrapper, "@Lose", DbType.Int32, entity.Lose);
			database.AddInParameter(commandWrapper, "@WinCount", DbType.Int32, entity.WinCount);
			database.AddInParameter(commandWrapper, "@DrawCount", DbType.Int32, entity.DrawCount);
			database.AddInParameter(commandWrapper, "@LostCount", DbType.Int32, entity.LostCount);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Rowtime", DbType.DateTime, entity.Rowtime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/5 15:29:15</remarks>
        public bool Update(LeagueRankEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueRank_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, entity.LeagueRecordId);
			database.AddInParameter(commandWrapper, "@LeagueRank", DbType.Int32, entity.LeagueRank);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@Goal", DbType.Int32, entity.Goal);
			database.AddInParameter(commandWrapper, "@Lose", DbType.Int32, entity.Lose);
			database.AddInParameter(commandWrapper, "@WinCount", DbType.Int32, entity.WinCount);
			database.AddInParameter(commandWrapper, "@DrawCount", DbType.Int32, entity.DrawCount);
			database.AddInParameter(commandWrapper, "@LostCount", DbType.Int32, entity.LostCount);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Rowtime", DbType.DateTime, entity.Rowtime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

