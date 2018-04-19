

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
    
    public partial class MatchprocessProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Process;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到MatchprocessEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public MatchprocessEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new MatchprocessEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.Process = (System.Byte[]) reader["Process"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<MatchprocessEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<MatchprocessEntity>();
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
        public MatchprocessProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public MatchprocessProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>MatchprocessEntity</returns>
        /// <remarks>2015/1/30 14:57:39</remarks>
        public MatchprocessEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Matchprocess_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            MatchprocessEntity obj=null;
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
		
		#region  GetByMatchId
		
		/// <summary>
        /// GetByMatchId
        /// </summary>
		/// <param name="dateChar">dateChar</param>
		/// <param name="matchType">matchType</param>
		/// <param name="matchId">matchId</param>
        /// <returns>MatchprocessEntity</returns>
        /// <remarks>2015/1/30 14:57:39</remarks>
        public MatchprocessEntity GetByMatchId( System.String dateChar, System.Int32 matchType, System.Guid matchId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MatchProcess_GetByMatchId");
            
			database.AddInParameter(commandWrapper, "@DateChar", DbType.AnsiStringFixedLength, dateChar);
			database.AddInParameter(commandWrapper, "@MatchType", DbType.Int32, matchType);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, matchId);

            
            MatchprocessEntity obj=null;
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
        /// <returns>MatchprocessEntity列表</returns>
        /// <remarks>2015/1/30 14:57:39</remarks>
        public List<MatchprocessEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Matchprocess_GetAll");
            

            
            List<MatchprocessEntity> list = null;
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
        /// <remarks>2015/1/30 14:57:39</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Matchprocess_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            
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
		
		#region  Job_CrossCreateProcessTable
		
		/// <summary>
        /// Job_CrossCreateProcessTable
        /// </summary>

        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/1/30 14:57:39</remarks>
        public bool Job_CrossCreateProcessTable (DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("Job_CrossCreateProcessTable");
            

            
            
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
		
		#region  Save
		
		/// <summary>
        /// Save
        /// </summary>
		/// <param name="matchType">matchType</param>
		/// <param name="process">process</param>
		/// <param name="rowTime">rowTime</param>
		/// <param name="dateChar">dateChar</param>
		/// <param name="matchId">matchId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/1/30 14:57:40</remarks>
        public bool Save ( System.Int32 matchType, System.Byte[] process, System.DateTime rowTime, System.String dateChar, System.Guid matchId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MatchProcess_Save");
            
			database.AddInParameter(commandWrapper, "@MatchType", DbType.Int32, matchType);
			database.AddInParameter(commandWrapper, "@Process", DbType.Binary, process);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, rowTime);
			database.AddInParameter(commandWrapper, "@DateChar", DbType.AnsiStringFixedLength, dateChar);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, matchId);

            
            
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
		
		#region  Job_CreateProcessTable
		
		/// <summary>
        /// Job_CreateProcessTable
        /// </summary>

        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/1/30 14:57:40</remarks>
        public bool Job_CreateProcessTable (DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("Job_CreateProcessTable");
            

            
            
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
        /// <remarks>2015/1/30 14:57:40</remarks>
        public bool Insert(MatchprocessEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/1/30 14:57:40</remarks>
        public bool Insert(MatchprocessEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Matchprocess_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Process", DbType.Binary, entity.Process);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2015/1/30 14:57:40</remarks>
        public bool Update(MatchprocessEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/1/30 14:57:40</remarks>
        public bool Update(MatchprocessEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Matchprocess_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@Process", DbType.Binary, entity.Process);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

