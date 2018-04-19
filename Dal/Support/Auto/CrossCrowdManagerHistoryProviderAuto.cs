

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
    
    public partial class CrosscrowdManagerhistoryProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Support;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到CrosscrowdManagerhistoryEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public CrosscrowdManagerhistoryEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new CrosscrowdManagerhistoryEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.SiteId = (System.String) reader["SiteId"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.CrossCrowdId = (System.Int32) reader["CrossCrowdId"];
            obj.Morale = (System.Int32) reader["Morale"];
            obj.Score = (System.Int32) reader["Score"];
            obj.ScoreUpdateTime = (System.DateTime) reader["ScoreUpdateTime"];
            obj.KillNumber = (System.Int32) reader["KillNumber"];
            obj.ByKillNumber = (System.Int32) reader["ByKillNumber"];
            obj.NextMatchTime = (System.DateTime) reader["NextMatchTime"];
            obj.ClearCdCount = (System.Int32) reader["ClearCdCount"];
            obj.ResurrectionTime = (System.DateTime) reader["ResurrectionTime"];
            obj.ResurrectionCount = (System.Int32) reader["ResurrectionCount"];
            obj.ResurrectionAuto = (System.Int32) reader["ResurrectionAuto"];
            obj.WinningCount = (System.Int32) reader["WinningCount"];
            obj.Rank = (System.Int32) reader["Rank"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<CrosscrowdManagerhistoryEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<CrosscrowdManagerhistoryEntity>();
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
        public CrosscrowdManagerhistoryProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public CrosscrowdManagerhistoryProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>CrosscrowdManagerhistoryEntity</returns>
        /// <remarks>2016-08-15 11:13:42</remarks>
        public CrosscrowdManagerhistoryEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrosscrowdManagerhistory_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            CrosscrowdManagerhistoryEntity obj=null;
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
        /// <returns>CrosscrowdManagerhistoryEntity列表</returns>
        /// <remarks>2016-08-15 11:13:42</remarks>
        public List<CrosscrowdManagerhistoryEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrosscrowdManagerhistory_GetAll");
            

            
            List<CrosscrowdManagerhistoryEntity> list = null;
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
        /// <remarks>2016-08-15 11:13:42</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrosscrowdManagerhistory_Delete");
            
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
		
		#region Insert
		
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:13:42</remarks>
        public bool Insert(CrosscrowdManagerhistoryEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:13:42</remarks>
        public bool Insert(CrosscrowdManagerhistoryEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrosscrowdManagerhistory_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@CrossCrowdId", DbType.Int32, entity.CrossCrowdId);
			database.AddInParameter(commandWrapper, "@Morale", DbType.Int32, entity.Morale);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@ScoreUpdateTime", DbType.DateTime, entity.ScoreUpdateTime);
			database.AddInParameter(commandWrapper, "@KillNumber", DbType.Int32, entity.KillNumber);
			database.AddInParameter(commandWrapper, "@ByKillNumber", DbType.Int32, entity.ByKillNumber);
			database.AddInParameter(commandWrapper, "@NextMatchTime", DbType.DateTime, entity.NextMatchTime);
			database.AddInParameter(commandWrapper, "@ClearCdCount", DbType.Int32, entity.ClearCdCount);
			database.AddInParameter(commandWrapper, "@ResurrectionTime", DbType.DateTime, entity.ResurrectionTime);
			database.AddInParameter(commandWrapper, "@ResurrectionCount", DbType.Int32, entity.ResurrectionCount);
			database.AddInParameter(commandWrapper, "@ResurrectionAuto", DbType.Int32, entity.ResurrectionAuto);
			database.AddInParameter(commandWrapper, "@WinningCount", DbType.Int32, entity.WinningCount);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:13:42</remarks>
        public bool Update(CrosscrowdManagerhistoryEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:13:42</remarks>
        public bool Update(CrosscrowdManagerhistoryEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrosscrowdManagerhistory_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@CrossCrowdId", DbType.Int32, entity.CrossCrowdId);
			database.AddInParameter(commandWrapper, "@Morale", DbType.Int32, entity.Morale);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@ScoreUpdateTime", DbType.DateTime, entity.ScoreUpdateTime);
			database.AddInParameter(commandWrapper, "@KillNumber", DbType.Int32, entity.KillNumber);
			database.AddInParameter(commandWrapper, "@ByKillNumber", DbType.Int32, entity.ByKillNumber);
			database.AddInParameter(commandWrapper, "@NextMatchTime", DbType.DateTime, entity.NextMatchTime);
			database.AddInParameter(commandWrapper, "@ClearCdCount", DbType.Int32, entity.ClearCdCount);
			database.AddInParameter(commandWrapper, "@ResurrectionTime", DbType.DateTime, entity.ResurrectionTime);
			database.AddInParameter(commandWrapper, "@ResurrectionCount", DbType.Int32, entity.ResurrectionCount);
			database.AddInParameter(commandWrapper, "@ResurrectionAuto", DbType.Int32, entity.ResurrectionAuto);
			database.AddInParameter(commandWrapper, "@WinningCount", DbType.Int32, entity.WinningCount);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);

            
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
