

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
    
    public partial class MonitorDailyeventProvider
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
		/// 将IDataReader的当前记录读取到MonitorDailyeventEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public MonitorDailyeventEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new MonitorDailyeventEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ZoneId = (System.Int32) reader["ZoneId"];
            obj.EventType = (System.Int32) reader["EventType"];
            obj.OpenTime = (System.DateTime) reader["OpenTime"];
            obj.StartTime = (System.DateTime) reader["StartTime"];
            obj.EndTime = (System.DateTime) reader["EndTime"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.Status = (System.Int32) reader["Status"];
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
        public List<MonitorDailyeventEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<MonitorDailyeventEntity>();
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
        public MonitorDailyeventProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public MonitorDailyeventProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>MonitorDailyeventEntity</returns>
        /// <remarks>2016-08-16 10:45:34</remarks>
        public MonitorDailyeventEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_MonitorDailyevent_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            MonitorDailyeventEntity obj=null;
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
		
		#region  GetByZoneEvent
		
		/// <summary>
        /// GetByZoneEvent
        /// </summary>
		/// <param name="zoneId">zoneId</param>
		/// <param name="eventType">eventType</param>
        /// <returns>MonitorDailyeventEntity</returns>
        /// <remarks>2016-08-16 10:45:35</remarks>
        public MonitorDailyeventEntity GetByZoneEvent( System.Int32 zoneId, System.Int32 eventType)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MonitorEvent_GetByZoneEvent");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@EventType", DbType.Int32, eventType);

            
            MonitorDailyeventEntity obj=null;
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
        /// <returns>MonitorDailyeventEntity列表</returns>
        /// <remarks>2016-08-16 10:45:35</remarks>
        public List<MonitorDailyeventEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_MonitorDailyevent_GetAll");
            

            
            List<MonitorDailyeventEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByZone
		
		/// <summary>
        /// GetByZone
        /// </summary>
        /// <returns>MonitorDailyeventEntity列表</returns>
        /// <remarks>2016-08-16 10:45:35</remarks>
        public List<MonitorDailyeventEntity> GetByZone( System.Int32 zoneId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MonitorEvent_GetByZone");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);

            
            List<MonitorDailyeventEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Save
		
		/// <summary>
        /// Save
        /// </summary>
		/// <param name="zoneId">zoneId</param>
		/// <param name="eventType">eventType</param>
		/// <param name="openTime">openTime</param>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="recordDate">recordDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-08-16 10:45:35</remarks>
        public bool Save ( System.Int32 zoneId, System.Int32 eventType, System.DateTime openTime, System.DateTime startTime, System.DateTime endTime, System.DateTime recordDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MonitorEvent_Save");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@EventType", DbType.Int32, eventType);
			database.AddInParameter(commandWrapper, "@OpenTime", DbType.DateTime, openTime);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);

            
            
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
        /// <remarks>2016-08-16 10:45:35</remarks>
        public bool Insert(MonitorDailyeventEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-16 10:45:35</remarks>
        public bool Insert(MonitorDailyeventEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_MonitorDailyevent_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@EventType", DbType.Int32, entity.EventType);
			database.AddInParameter(commandWrapper, "@OpenTime", DbType.DateTime, entity.OpenTime);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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
        /// <remarks>2016-08-16 10:45:35</remarks>
        public bool Update(MonitorDailyeventEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-16 10:45:35</remarks>
        public bool Update(MonitorDailyeventEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_MonitorDailyevent_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@EventType", DbType.Int32, entity.EventType);
			database.AddInParameter(commandWrapper, "@OpenTime", DbType.DateTime, entity.OpenTime);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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
