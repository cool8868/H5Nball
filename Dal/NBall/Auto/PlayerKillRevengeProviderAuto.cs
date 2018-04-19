

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
    
    public partial class PlayerkillRevengeProvider
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
		/// 将IDataReader的当前记录读取到PlayerkillRevengeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public PlayerkillRevengeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new PlayerkillRevengeEntity();
			
            obj.Idx = (System.Int64) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.HomeId = (System.Guid) reader["HomeId"];
            obj.HomeLogo = (System.String) reader["HomeLogo"];
            obj.HomeName = (System.String) reader["HomeName"];
            obj.HomeScore = (System.Int32) reader["HomeScore"];
            obj.AwayScore = (System.Int32) reader["AwayScore"];
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
        public List<PlayerkillRevengeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<PlayerkillRevengeEntity>();
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
        public PlayerkillRevengeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public PlayerkillRevengeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>PlayerkillRevengeEntity</returns>
        /// <remarks>2015/10/18 15:50:45</remarks>
        public PlayerkillRevengeEntity GetById( System.Int64 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PlayerkillRevenge_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, idx);

            
            PlayerkillRevengeEntity obj=null;
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
        /// <returns>PlayerkillRevengeEntity列表</returns>
        /// <remarks>2015/10/18 15:50:45</remarks>
        public List<PlayerkillRevengeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PlayerkillRevenge_GetAll");
            

            
            List<PlayerkillRevengeEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetRevenge
		
		/// <summary>
        /// GetRevenge
        /// </summary>
        /// <returns>PlayerkillRevengeEntity列表</returns>
        /// <remarks>2015/10/18 15:50:45</remarks>
        public List<PlayerkillRevengeEntity> GetRevenge( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PlayerKill_GetRevenge");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<PlayerkillRevengeEntity> list = null;
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
        /// <remarks>2015/10/18 15:50:45</remarks>
        public bool Delete ( System.Int64 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PlayerkillRevenge_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, idx);

            
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
        /// <remarks>2015/10/18 15:50:45</remarks>
        public bool Insert(PlayerkillRevengeEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PlayerkillRevenge_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@HomeLogo", DbType.AnsiString, entity.HomeLogo);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Int64)database.GetParameterValue(commandWrapper, "@Idx");
            
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
        /// <remarks>2015/10/18 15:50:45</remarks>
        public bool Update(PlayerkillRevengeEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PlayerkillRevenge_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@HomeLogo", DbType.AnsiString, entity.HomeLogo);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
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

            entity.Idx=(System.Int64)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

