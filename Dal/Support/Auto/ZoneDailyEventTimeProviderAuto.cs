

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
    
    public partial class ZoneDailyeventtimeProvider
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
		/// 将IDataReader的当前记录读取到ZoneDailyeventtimeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ZoneDailyeventtimeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ZoneDailyeventtimeEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ZoneId = (System.Int32) reader["ZoneId"];
            obj.EventType = (System.Int32) reader["EventType"];
            obj.OpenHour = (System.Int32) reader["OpenHour"];
            obj.OpenMinute = (System.Int32) reader["OpenMinute"];
            obj.StartHour = (System.Int32) reader["StartHour"];
            obj.StartMinute = (System.Int32) reader["StartMinute"];
            obj.EndHour = (System.Int32) reader["EndHour"];
            obj.EndMinute = (System.Int32) reader["EndMinute"];
            obj.StartDay = (System.Int32) reader["StartDay"];
            obj.EndDay = (System.Int32) reader["EndDay"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ZoneDailyeventtimeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ZoneDailyeventtimeEntity>();
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
        public ZoneDailyeventtimeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ZoneDailyeventtimeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ZoneDailyeventtimeEntity</returns>
        /// <remarks>2016-08-16 10:45:22</remarks>
        public ZoneDailyeventtimeEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ZoneDailyeventtime_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ZoneDailyeventtimeEntity obj=null;
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
        /// <returns>ZoneDailyeventtimeEntity列表</returns>
        /// <remarks>2016-08-16 10:45:22</remarks>
        public List<ZoneDailyeventtimeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ZoneDailyeventtime_GetAll");
            

            
            List<ZoneDailyeventtimeEntity> list = null;
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
        /// <returns>ZoneDailyeventtimeEntity列表</returns>
        /// <remarks>2016-08-16 10:45:22</remarks>
        public List<ZoneDailyeventtimeEntity> GetByZone( System.Int32 zoneId, System.DateTime curDate)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyEventTime_GetByZone");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@CurDate", DbType.DateTime, curDate);

            
            List<ZoneDailyeventtimeEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByZoneAndEvent
		
		/// <summary>
        /// GetByZoneAndEvent
        /// </summary>
        /// <returns>ZoneDailyeventtimeEntity列表</returns>
        /// <remarks>2016-08-16 10:45:22</remarks>
        public List<ZoneDailyeventtimeEntity> GetByZoneAndEvent( System.Int32 zoneId, System.Int32 eventType, System.DateTime curDate)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyEventTime_GetByZoneAndEvent");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@EventType", DbType.Int32, eventType);
			database.AddInParameter(commandWrapper, "@CurDate", DbType.DateTime, curDate);

            
            List<ZoneDailyeventtimeEntity> list = null;
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
        /// <remarks>2016-08-16 10:45:23</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ZoneDailyeventtime_Delete");
            
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
        /// <remarks>2016-08-16 10:45:23</remarks>
        public bool Insert(ZoneDailyeventtimeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-16 10:45:23</remarks>
        public bool Insert(ZoneDailyeventtimeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ZoneDailyeventtime_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@EventType", DbType.Int32, entity.EventType);
			database.AddInParameter(commandWrapper, "@OpenHour", DbType.Int32, entity.OpenHour);
			database.AddInParameter(commandWrapper, "@OpenMinute", DbType.Int32, entity.OpenMinute);
			database.AddInParameter(commandWrapper, "@StartHour", DbType.Int32, entity.StartHour);
			database.AddInParameter(commandWrapper, "@StartMinute", DbType.Int32, entity.StartMinute);
			database.AddInParameter(commandWrapper, "@EndHour", DbType.Int32, entity.EndHour);
			database.AddInParameter(commandWrapper, "@EndMinute", DbType.Int32, entity.EndMinute);
			database.AddInParameter(commandWrapper, "@StartDay", DbType.Int32, entity.StartDay);
			database.AddInParameter(commandWrapper, "@EndDay", DbType.Int32, entity.EndDay);

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
        /// <remarks>2016-08-16 10:45:23</remarks>
        public bool Update(ZoneDailyeventtimeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-16 10:45:23</remarks>
        public bool Update(ZoneDailyeventtimeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ZoneDailyeventtime_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@EventType", DbType.Int32, entity.EventType);
			database.AddInParameter(commandWrapper, "@OpenHour", DbType.Int32, entity.OpenHour);
			database.AddInParameter(commandWrapper, "@OpenMinute", DbType.Int32, entity.OpenMinute);
			database.AddInParameter(commandWrapper, "@StartHour", DbType.Int32, entity.StartHour);
			database.AddInParameter(commandWrapper, "@StartMinute", DbType.Int32, entity.StartMinute);
			database.AddInParameter(commandWrapper, "@EndHour", DbType.Int32, entity.EndHour);
			database.AddInParameter(commandWrapper, "@EndMinute", DbType.Int32, entity.EndMinute);
			database.AddInParameter(commandWrapper, "@StartDay", DbType.Int32, entity.StartDay);
			database.AddInParameter(commandWrapper, "@EndDay", DbType.Int32, entity.EndDay);

            
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
