

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
    
    public partial class ConfigEverydayactivitylegendProvider
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
		/// 将IDataReader的当前记录读取到ConfigEverydayactivitylegendEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigEverydayactivitylegendEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigEverydayactivitylegendEntity();
			
            obj.RefreshDate = (System.DateTime) reader["RefreshDate"];
            obj.LegendCode = (System.Int32) reader["LegendCode"];
            obj.LegendDebrisCode = (System.Int32) reader["LegendDebrisCode"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigEverydayactivitylegendEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigEverydayactivitylegendEntity>();
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
        public ConfigEverydayactivitylegendProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigEverydayactivitylegendProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="refreshDate">refreshDate</param>
        /// <returns>ConfigEverydayactivitylegendEntity</returns>
        /// <remarks>2016/11/7 14:45:32</remarks>
        public ConfigEverydayactivitylegendEntity GetById( System.DateTime refreshDate)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEverydayactivitylegend_GetById");
            
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.Date, refreshDate);

            
            ConfigEverydayactivitylegendEntity obj=null;
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
        /// <returns>ConfigEverydayactivitylegendEntity列表</returns>
        /// <remarks>2016/11/7 14:45:32</remarks>
        public List<ConfigEverydayactivitylegendEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEverydayactivitylegend_GetAll");
            

            
            List<ConfigEverydayactivitylegendEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetTop5
		
		/// <summary>
        /// GetTop5
        /// </summary>
        /// <returns>ConfigEverydayactivitylegendEntity列表</returns>
        /// <remarks>2016/11/7 14:45:32</remarks>
        public List<ConfigEverydayactivitylegendEntity> GetTop5( System.DateTime startDate, System.DateTime endDate)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ConfigEverydayActivityLegend_GetTop5");
            
			database.AddInParameter(commandWrapper, "@StartDate", DbType.Date, startDate);
			database.AddInParameter(commandWrapper, "@EndDate", DbType.Date, endDate);

            
            List<ConfigEverydayactivitylegendEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByTime
		
		/// <summary>
        /// GetByTime
        /// </summary>
		/// <param name="dateTime">dateTime</param>
        /// <returns>Int32</returns>
        /// <remarks>2016/11/7 14:45:33</remarks>
        public Int32 GetByTime ( System.DateTime dateTime)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ConfigEverydayActivityLegend_GetByTime");
            
			database.AddInParameter(commandWrapper, "@dateTime", DbType.Date, dateTime);

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="refreshDate">refreshDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/11/7 14:45:33</remarks>
        public bool Delete ( System.DateTime refreshDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEverydayactivitylegend_Delete");
            
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.Date, refreshDate);

            
            
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
        /// <remarks>2016/11/7 14:45:33</remarks>
        public bool Insert(ConfigEverydayactivitylegendEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/11/7 14:45:33</remarks>
        public bool Insert(ConfigEverydayactivitylegendEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigEverydayactivitylegend_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.Date, entity.RefreshDate);
			database.AddInParameter(commandWrapper, "@LegendCode", DbType.Int32, entity.LegendCode);
			database.AddInParameter(commandWrapper, "@LegendDebrisCode", DbType.Int32, entity.LegendDebrisCode);

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
        /// <remarks>2016/11/7 14:45:33</remarks>
        public bool Update(ConfigEverydayactivitylegendEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/11/7 14:45:33</remarks>
        public bool Update(ConfigEverydayactivitylegendEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigEverydayactivitylegend_Update");
            
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.Date, entity.RefreshDate);
			database.AddInParameter(commandWrapper, "@LegendCode", DbType.Int32, entity.LegendCode);
			database.AddInParameter(commandWrapper, "@LegendDebrisCode", DbType.Int32, entity.LegendDebrisCode);

            
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
