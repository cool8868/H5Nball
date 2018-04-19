

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
    
    public partial class ConfigDailyeventtimeProvider
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
		/// 将IDataReader的当前记录读取到ConfigDailyeventtimeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigDailyeventtimeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigDailyeventtimeEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.EventType = (System.Int32) reader["EventType"];
            obj.OpenHour = (System.Int32) reader["OpenHour"];
            obj.OpenMinute = (System.Int32) reader["OpenMinute"];
            obj.StartHour = (System.Int32) reader["StartHour"];
            obj.StartMinute = (System.Int32) reader["StartMinute"];
            obj.EndHour = (System.Int32) reader["EndHour"];
            obj.EndMinute = (System.Int32) reader["EndMinute"];
            obj.Version = (System.Int32) reader["Version"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigDailyeventtimeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigDailyeventtimeEntity>();
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
        public ConfigDailyeventtimeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigDailyeventtimeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigDailyeventtimeEntity</returns>
        /// <remarks>2016/3/3 17:24:48</remarks>
        public ConfigDailyeventtimeEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigDailyeventtime_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigDailyeventtimeEntity obj=null;
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
        /// <returns>ConfigDailyeventtimeEntity列表</returns>
        /// <remarks>2016/3/3 17:24:48</remarks>
        public List<ConfigDailyeventtimeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigDailyeventtime_GetAll");
            

            
            List<ConfigDailyeventtimeEntity> list = null;
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
        /// <remarks>2016/3/3 17:24:48</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigDailyeventtime_Delete");
            
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
        /// <remarks>2016/3/3 17:24:48</remarks>
        public bool Insert(ConfigDailyeventtimeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/3/3 17:24:48</remarks>
        public bool Insert(ConfigDailyeventtimeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigDailyeventtime_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@EventType", DbType.Int32, entity.EventType);
			database.AddInParameter(commandWrapper, "@OpenHour", DbType.Int32, entity.OpenHour);
			database.AddInParameter(commandWrapper, "@OpenMinute", DbType.Int32, entity.OpenMinute);
			database.AddInParameter(commandWrapper, "@StartHour", DbType.Int32, entity.StartHour);
			database.AddInParameter(commandWrapper, "@StartMinute", DbType.Int32, entity.StartMinute);
			database.AddInParameter(commandWrapper, "@EndHour", DbType.Int32, entity.EndHour);
			database.AddInParameter(commandWrapper, "@EndMinute", DbType.Int32, entity.EndMinute);
			database.AddInParameter(commandWrapper, "@Version", DbType.Int32, entity.Version);

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
        /// <remarks>2016/3/3 17:24:48</remarks>
        public bool Update(ConfigDailyeventtimeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/3/3 17:24:48</remarks>
        public bool Update(ConfigDailyeventtimeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigDailyeventtime_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@EventType", DbType.Int32, entity.EventType);
			database.AddInParameter(commandWrapper, "@OpenHour", DbType.Int32, entity.OpenHour);
			database.AddInParameter(commandWrapper, "@OpenMinute", DbType.Int32, entity.OpenMinute);
			database.AddInParameter(commandWrapper, "@StartHour", DbType.Int32, entity.StartHour);
			database.AddInParameter(commandWrapper, "@StartMinute", DbType.Int32, entity.StartMinute);
			database.AddInParameter(commandWrapper, "@EndHour", DbType.Int32, entity.EndHour);
			database.AddInParameter(commandWrapper, "@EndMinute", DbType.Int32, entity.EndMinute);
			database.AddInParameter(commandWrapper, "@Version", DbType.Int32, entity.Version);

            
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

