

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
    
    public partial class StatisticOnlineProvider
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
		/// 将IDataReader的当前记录读取到StatisticOnlineEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public StatisticOnlineEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new StatisticOnlineEntity();
			
            obj.Idx = (System.Int64) reader["Idx"];
            obj.ZoneId = (System.Int32) reader["ZoneId"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.TotalMinutes = (System.Int64) reader["TotalMinutes"];
            obj.TotalValue = (System.Int32) reader["TotalValue"];
            obj.RecordCount = (System.Int32) reader["RecordCount"];
            obj.MinValue = (System.Int32) reader["MinValue"];
            obj.MinTime = (System.DateTime) reader["MinTime"];
            obj.MaxValue = (System.Int32) reader["MaxValue"];
            obj.MaxTime = (System.DateTime) reader["MaxTime"];
            obj.Hour0 = (System.Int32) reader["Hour0"];
            obj.Hour1 = (System.Int32) reader["Hour1"];
            obj.Hour2 = (System.Int32) reader["Hour2"];
            obj.Hour3 = (System.Int32) reader["Hour3"];
            obj.Hour4 = (System.Int32) reader["Hour4"];
            obj.Hour5 = (System.Int32) reader["Hour5"];
            obj.Hour6 = (System.Int32) reader["Hour6"];
            obj.Hour7 = (System.Int32) reader["Hour7"];
            obj.Hour8 = (System.Int32) reader["Hour8"];
            obj.Hour9 = (System.Int32) reader["Hour9"];
            obj.Hour10 = (System.Int32) reader["Hour10"];
            obj.Hour11 = (System.Int32) reader["Hour11"];
            obj.Hour12 = (System.Int32) reader["Hour12"];
            obj.Hour13 = (System.Int32) reader["Hour13"];
            obj.Hour14 = (System.Int32) reader["Hour14"];
            obj.Hour15 = (System.Int32) reader["Hour15"];
            obj.Hour16 = (System.Int32) reader["Hour16"];
            obj.Hour17 = (System.Int32) reader["Hour17"];
            obj.Hour18 = (System.Int32) reader["Hour18"];
            obj.Hour19 = (System.Int32) reader["Hour19"];
            obj.Hour20 = (System.Int32) reader["Hour20"];
            obj.Hour21 = (System.Int32) reader["Hour21"];
            obj.Hour22 = (System.Int32) reader["Hour22"];
            obj.Hour23 = (System.Int32) reader["Hour23"];
            obj.Count0 = (System.Int32) reader["Count0"];
            obj.Count1 = (System.Int32) reader["Count1"];
            obj.Count2 = (System.Int32) reader["Count2"];
            obj.Count3 = (System.Int32) reader["Count3"];
            obj.Count4 = (System.Int32) reader["Count4"];
            obj.Count5 = (System.Int32) reader["Count5"];
            obj.Count6 = (System.Int32) reader["Count6"];
            obj.Count7 = (System.Int32) reader["Count7"];
            obj.Count8 = (System.Int32) reader["Count8"];
            obj.Count9 = (System.Int32) reader["Count9"];
            obj.Count10 = (System.Int32) reader["Count10"];
            obj.Count11 = (System.Int32) reader["Count11"];
            obj.Count12 = (System.Int32) reader["Count12"];
            obj.Count13 = (System.Int32) reader["Count13"];
            obj.Count14 = (System.Int32) reader["Count14"];
            obj.Count15 = (System.Int32) reader["Count15"];
            obj.Count16 = (System.Int32) reader["Count16"];
            obj.Count17 = (System.Int32) reader["Count17"];
            obj.Count18 = (System.Int32) reader["Count18"];
            obj.Count19 = (System.Int32) reader["Count19"];
            obj.Count20 = (System.Int32) reader["Count20"];
            obj.Count21 = (System.Int32) reader["Count21"];
            obj.Count22 = (System.Int32) reader["Count22"];
            obj.Count23 = (System.Int32) reader["Count23"];
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
        public List<StatisticOnlineEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<StatisticOnlineEntity>();
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
        public StatisticOnlineProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public StatisticOnlineProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>StatisticOnlineEntity</returns>
        /// <remarks>2016/6/7 12:11:20</remarks>
        public StatisticOnlineEntity GetById( System.Int64 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_StatisticOnline_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, idx);

            
            StatisticOnlineEntity obj=null;
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
        /// <returns>StatisticOnlineEntity列表</returns>
        /// <remarks>2016/6/7 12:11:20</remarks>
        public List<StatisticOnlineEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_StatisticOnline_GetAll");
            

            
            List<StatisticOnlineEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetbyDate
		
		/// <summary>
        /// GetbyDate
        /// </summary>
        /// <returns>StatisticOnlineEntity列表</returns>
        /// <remarks>2016/6/7 12:11:20</remarks>
        public List<StatisticOnlineEntity> GetbyDate( System.Int32 zoneId, System.DateTime startTime, System.DateTime endTime)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticOnline_GetbyDate");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);

            
            List<StatisticOnlineEntity> list = null;
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
        /// <remarks>2016/6/7 12:11:21</remarks>
        public bool Delete ( System.Int64 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_StatisticOnline_Delete");
            
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
		
		#region  Create
		
		/// <summary>
        /// Create
        /// </summary>
		/// <param name="zoneId">zoneId</param>
		/// <param name="curDate">curDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 12:11:21</remarks>
        public bool Create ( System.Int32 zoneId, System.DateTime curDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticOnline_Create");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@CurDate", DbType.DateTime, curDate);

            
            
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
		
		#region  Update
		
		/// <summary>
        /// Update
        /// </summary>
		/// <param name="zoneId">zoneId</param>
		/// <param name="hour">hour</param>
		/// <param name="recordDate">recordDate</param>
		/// <param name="curTime">curTime</param>
		/// <param name="curValue">curValue</param>
		/// <param name="totalMinutes">totalMinutes</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 12:11:21</remarks>
        public bool Update ( System.Int32 zoneId, System.String hour, System.DateTime recordDate, System.DateTime curTime, System.Int32 curValue, System.Int64 totalMinutes,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticOnline_Update");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@Hour", DbType.AnsiString, hour);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);
			database.AddInParameter(commandWrapper, "@CurTime", DbType.DateTime, curTime);
			database.AddInParameter(commandWrapper, "@CurValue", DbType.Int32, curValue);
			database.AddInParameter(commandWrapper, "@TotalMinutes", DbType.Int64, totalMinutes);

            
            
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
        /// <remarks>2016/6/7 12:11:21</remarks>
        public bool Insert(StatisticOnlineEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/7 12:11:21</remarks>
        public bool Insert(StatisticOnlineEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_StatisticOnline_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@TotalMinutes", DbType.Int64, entity.TotalMinutes);
			database.AddInParameter(commandWrapper, "@TotalValue", DbType.Int32, entity.TotalValue);
			database.AddInParameter(commandWrapper, "@RecordCount", DbType.Int32, entity.RecordCount);
			database.AddInParameter(commandWrapper, "@MinValue", DbType.Int32, entity.MinValue);
			database.AddInParameter(commandWrapper, "@MinTime", DbType.DateTime, entity.MinTime);
			database.AddInParameter(commandWrapper, "@MaxValue", DbType.Int32, entity.MaxValue);
			database.AddInParameter(commandWrapper, "@MaxTime", DbType.DateTime, entity.MaxTime);
			database.AddInParameter(commandWrapper, "@Hour0", DbType.Int32, entity.Hour0);
			database.AddInParameter(commandWrapper, "@Hour1", DbType.Int32, entity.Hour1);
			database.AddInParameter(commandWrapper, "@Hour2", DbType.Int32, entity.Hour2);
			database.AddInParameter(commandWrapper, "@Hour3", DbType.Int32, entity.Hour3);
			database.AddInParameter(commandWrapper, "@Hour4", DbType.Int32, entity.Hour4);
			database.AddInParameter(commandWrapper, "@Hour5", DbType.Int32, entity.Hour5);
			database.AddInParameter(commandWrapper, "@Hour6", DbType.Int32, entity.Hour6);
			database.AddInParameter(commandWrapper, "@Hour7", DbType.Int32, entity.Hour7);
			database.AddInParameter(commandWrapper, "@Hour8", DbType.Int32, entity.Hour8);
			database.AddInParameter(commandWrapper, "@Hour9", DbType.Int32, entity.Hour9);
			database.AddInParameter(commandWrapper, "@Hour10", DbType.Int32, entity.Hour10);
			database.AddInParameter(commandWrapper, "@Hour11", DbType.Int32, entity.Hour11);
			database.AddInParameter(commandWrapper, "@Hour12", DbType.Int32, entity.Hour12);
			database.AddInParameter(commandWrapper, "@Hour13", DbType.Int32, entity.Hour13);
			database.AddInParameter(commandWrapper, "@Hour14", DbType.Int32, entity.Hour14);
			database.AddInParameter(commandWrapper, "@Hour15", DbType.Int32, entity.Hour15);
			database.AddInParameter(commandWrapper, "@Hour16", DbType.Int32, entity.Hour16);
			database.AddInParameter(commandWrapper, "@Hour17", DbType.Int32, entity.Hour17);
			database.AddInParameter(commandWrapper, "@Hour18", DbType.Int32, entity.Hour18);
			database.AddInParameter(commandWrapper, "@Hour19", DbType.Int32, entity.Hour19);
			database.AddInParameter(commandWrapper, "@Hour20", DbType.Int32, entity.Hour20);
			database.AddInParameter(commandWrapper, "@Hour21", DbType.Int32, entity.Hour21);
			database.AddInParameter(commandWrapper, "@Hour22", DbType.Int32, entity.Hour22);
			database.AddInParameter(commandWrapper, "@Hour23", DbType.Int32, entity.Hour23);
			database.AddInParameter(commandWrapper, "@Count0", DbType.Int32, entity.Count0);
			database.AddInParameter(commandWrapper, "@Count1", DbType.Int32, entity.Count1);
			database.AddInParameter(commandWrapper, "@Count2", DbType.Int32, entity.Count2);
			database.AddInParameter(commandWrapper, "@Count3", DbType.Int32, entity.Count3);
			database.AddInParameter(commandWrapper, "@Count4", DbType.Int32, entity.Count4);
			database.AddInParameter(commandWrapper, "@Count5", DbType.Int32, entity.Count5);
			database.AddInParameter(commandWrapper, "@Count6", DbType.Int32, entity.Count6);
			database.AddInParameter(commandWrapper, "@Count7", DbType.Int32, entity.Count7);
			database.AddInParameter(commandWrapper, "@Count8", DbType.Int32, entity.Count8);
			database.AddInParameter(commandWrapper, "@Count9", DbType.Int32, entity.Count9);
			database.AddInParameter(commandWrapper, "@Count10", DbType.Int32, entity.Count10);
			database.AddInParameter(commandWrapper, "@Count11", DbType.Int32, entity.Count11);
			database.AddInParameter(commandWrapper, "@Count12", DbType.Int32, entity.Count12);
			database.AddInParameter(commandWrapper, "@Count13", DbType.Int32, entity.Count13);
			database.AddInParameter(commandWrapper, "@Count14", DbType.Int32, entity.Count14);
			database.AddInParameter(commandWrapper, "@Count15", DbType.Int32, entity.Count15);
			database.AddInParameter(commandWrapper, "@Count16", DbType.Int32, entity.Count16);
			database.AddInParameter(commandWrapper, "@Count17", DbType.Int32, entity.Count17);
			database.AddInParameter(commandWrapper, "@Count18", DbType.Int32, entity.Count18);
			database.AddInParameter(commandWrapper, "@Count19", DbType.Int32, entity.Count19);
			database.AddInParameter(commandWrapper, "@Count20", DbType.Int32, entity.Count20);
			database.AddInParameter(commandWrapper, "@Count21", DbType.Int32, entity.Count21);
			database.AddInParameter(commandWrapper, "@Count22", DbType.Int32, entity.Count22);
			database.AddInParameter(commandWrapper, "@Count23", DbType.Int32, entity.Count23);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/6/7 12:11:21</remarks>
        public bool Update(StatisticOnlineEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/7 12:11:21</remarks>
        public bool Update(StatisticOnlineEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_StatisticOnline_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, entity.Idx);
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@TotalMinutes", DbType.Int64, entity.TotalMinutes);
			database.AddInParameter(commandWrapper, "@TotalValue", DbType.Int32, entity.TotalValue);
			database.AddInParameter(commandWrapper, "@RecordCount", DbType.Int32, entity.RecordCount);
			database.AddInParameter(commandWrapper, "@MinValue", DbType.Int32, entity.MinValue);
			database.AddInParameter(commandWrapper, "@MinTime", DbType.DateTime, entity.MinTime);
			database.AddInParameter(commandWrapper, "@MaxValue", DbType.Int32, entity.MaxValue);
			database.AddInParameter(commandWrapper, "@MaxTime", DbType.DateTime, entity.MaxTime);
			database.AddInParameter(commandWrapper, "@Hour0", DbType.Int32, entity.Hour0);
			database.AddInParameter(commandWrapper, "@Hour1", DbType.Int32, entity.Hour1);
			database.AddInParameter(commandWrapper, "@Hour2", DbType.Int32, entity.Hour2);
			database.AddInParameter(commandWrapper, "@Hour3", DbType.Int32, entity.Hour3);
			database.AddInParameter(commandWrapper, "@Hour4", DbType.Int32, entity.Hour4);
			database.AddInParameter(commandWrapper, "@Hour5", DbType.Int32, entity.Hour5);
			database.AddInParameter(commandWrapper, "@Hour6", DbType.Int32, entity.Hour6);
			database.AddInParameter(commandWrapper, "@Hour7", DbType.Int32, entity.Hour7);
			database.AddInParameter(commandWrapper, "@Hour8", DbType.Int32, entity.Hour8);
			database.AddInParameter(commandWrapper, "@Hour9", DbType.Int32, entity.Hour9);
			database.AddInParameter(commandWrapper, "@Hour10", DbType.Int32, entity.Hour10);
			database.AddInParameter(commandWrapper, "@Hour11", DbType.Int32, entity.Hour11);
			database.AddInParameter(commandWrapper, "@Hour12", DbType.Int32, entity.Hour12);
			database.AddInParameter(commandWrapper, "@Hour13", DbType.Int32, entity.Hour13);
			database.AddInParameter(commandWrapper, "@Hour14", DbType.Int32, entity.Hour14);
			database.AddInParameter(commandWrapper, "@Hour15", DbType.Int32, entity.Hour15);
			database.AddInParameter(commandWrapper, "@Hour16", DbType.Int32, entity.Hour16);
			database.AddInParameter(commandWrapper, "@Hour17", DbType.Int32, entity.Hour17);
			database.AddInParameter(commandWrapper, "@Hour18", DbType.Int32, entity.Hour18);
			database.AddInParameter(commandWrapper, "@Hour19", DbType.Int32, entity.Hour19);
			database.AddInParameter(commandWrapper, "@Hour20", DbType.Int32, entity.Hour20);
			database.AddInParameter(commandWrapper, "@Hour21", DbType.Int32, entity.Hour21);
			database.AddInParameter(commandWrapper, "@Hour22", DbType.Int32, entity.Hour22);
			database.AddInParameter(commandWrapper, "@Hour23", DbType.Int32, entity.Hour23);
			database.AddInParameter(commandWrapper, "@Count0", DbType.Int32, entity.Count0);
			database.AddInParameter(commandWrapper, "@Count1", DbType.Int32, entity.Count1);
			database.AddInParameter(commandWrapper, "@Count2", DbType.Int32, entity.Count2);
			database.AddInParameter(commandWrapper, "@Count3", DbType.Int32, entity.Count3);
			database.AddInParameter(commandWrapper, "@Count4", DbType.Int32, entity.Count4);
			database.AddInParameter(commandWrapper, "@Count5", DbType.Int32, entity.Count5);
			database.AddInParameter(commandWrapper, "@Count6", DbType.Int32, entity.Count6);
			database.AddInParameter(commandWrapper, "@Count7", DbType.Int32, entity.Count7);
			database.AddInParameter(commandWrapper, "@Count8", DbType.Int32, entity.Count8);
			database.AddInParameter(commandWrapper, "@Count9", DbType.Int32, entity.Count9);
			database.AddInParameter(commandWrapper, "@Count10", DbType.Int32, entity.Count10);
			database.AddInParameter(commandWrapper, "@Count11", DbType.Int32, entity.Count11);
			database.AddInParameter(commandWrapper, "@Count12", DbType.Int32, entity.Count12);
			database.AddInParameter(commandWrapper, "@Count13", DbType.Int32, entity.Count13);
			database.AddInParameter(commandWrapper, "@Count14", DbType.Int32, entity.Count14);
			database.AddInParameter(commandWrapper, "@Count15", DbType.Int32, entity.Count15);
			database.AddInParameter(commandWrapper, "@Count16", DbType.Int32, entity.Count16);
			database.AddInParameter(commandWrapper, "@Count17", DbType.Int32, entity.Count17);
			database.AddInParameter(commandWrapper, "@Count18", DbType.Int32, entity.Count18);
			database.AddInParameter(commandWrapper, "@Count19", DbType.Int32, entity.Count19);
			database.AddInParameter(commandWrapper, "@Count20", DbType.Int32, entity.Count20);
			database.AddInParameter(commandWrapper, "@Count21", DbType.Int32, entity.Count21);
			database.AddInParameter(commandWrapper, "@Count22", DbType.Int32, entity.Count22);
			database.AddInParameter(commandWrapper, "@Count23", DbType.Int32, entity.Count23);
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
