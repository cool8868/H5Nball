

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
    
    public partial class ConfigScheduleProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Config;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到ConfigScheduleEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigScheduleEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigScheduleEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Name = (System.String) reader["Name"];
            obj.Category = (System.Int32) reader["Category"];
            obj.ActionType = (System.Int32) reader["ActionType"];
            obj.TimeType = (System.Int32) reader["TimeType"];
            obj.Setting = (System.String) reader["Setting"];
            obj.Times = (System.Int32) reader["Times"];
            obj.RetryTimes = (System.Int32) reader["RetryTimes"];
            obj.Parameters = (System.String) reader["Parameters"];
            obj.RunDelay = (System.Int32) reader["RunDelay"];
            obj.Description = (System.String) reader["Description"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigScheduleEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigScheduleEntity>();
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
        public ConfigScheduleProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigScheduleProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigScheduleEntity</returns>
        /// <remarks>2015/10/19 10:48:08</remarks>
        public ConfigScheduleEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSchedule_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigScheduleEntity obj=null;
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
        /// <returns>ConfigScheduleEntity列表</returns>
        /// <remarks>2015/10/19 10:48:08</remarks>
        public List<ConfigScheduleEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSchedule_GetAll");
            

            
            List<ConfigScheduleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
		/// <summary>
        /// GetAllForCache
        /// </summary>
        /// <returns>ConfigScheduleEntity列表</returns>
        /// <remarks>2015/10/19 10:48:08</remarks>
        public List<ConfigScheduleEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ConfigSchedule_GetAllForCache");
            

            
            List<ConfigScheduleEntity> list = null;
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
        /// <remarks>2015/10/19 10:48:08</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSchedule_Delete");
            
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
        /// <remarks>2015/10/19 10:48:08</remarks>
        public bool Insert(ConfigScheduleEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:48:08</remarks>
        public bool Insert(ConfigScheduleEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigSchedule_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.AnsiString, entity.Name);
			database.AddInParameter(commandWrapper, "@Category", DbType.Int32, entity.Category);
			database.AddInParameter(commandWrapper, "@ActionType", DbType.Int32, entity.ActionType);
			database.AddInParameter(commandWrapper, "@TimeType", DbType.Int32, entity.TimeType);
			database.AddInParameter(commandWrapper, "@Setting", DbType.AnsiString, entity.Setting);
			database.AddInParameter(commandWrapper, "@Times", DbType.Int32, entity.Times);
			database.AddInParameter(commandWrapper, "@RetryTimes", DbType.Int32, entity.RetryTimes);
			database.AddInParameter(commandWrapper, "@Parameters", DbType.AnsiString, entity.Parameters);
			database.AddInParameter(commandWrapper, "@RunDelay", DbType.Int32, entity.RunDelay);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:48:08</remarks>
        public bool Update(ConfigScheduleEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:48:08</remarks>
        public bool Update(ConfigScheduleEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigSchedule_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.AnsiString, entity.Name);
			database.AddInParameter(commandWrapper, "@Category", DbType.Int32, entity.Category);
			database.AddInParameter(commandWrapper, "@ActionType", DbType.Int32, entity.ActionType);
			database.AddInParameter(commandWrapper, "@TimeType", DbType.Int32, entity.TimeType);
			database.AddInParameter(commandWrapper, "@Setting", DbType.AnsiString, entity.Setting);
			database.AddInParameter(commandWrapper, "@Times", DbType.Int32, entity.Times);
			database.AddInParameter(commandWrapper, "@RetryTimes", DbType.Int32, entity.RetryTimes);
			database.AddInParameter(commandWrapper, "@Parameters", DbType.AnsiString, entity.Parameters);
			database.AddInParameter(commandWrapper, "@RunDelay", DbType.Int32, entity.RunDelay);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

