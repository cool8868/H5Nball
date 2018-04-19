

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
    
    public partial class ActivityexHistoryProvider
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
		/// 将IDataReader的当前记录读取到ActivityexHistoryEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ActivityexHistoryEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ActivityexHistoryEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ZoneActivityId = (System.Int32) reader["ZoneActivityId"];
            obj.ExcitingId = (System.Int32) reader["ExcitingId"];
            obj.GroupId = (System.Int32) reader["GroupId"];
            obj.ExData = (System.Int32) reader["ExData"];
            obj.ExStep = (System.Int32) reader["ExStep"];
            obj.ReceiveTimes = (System.Int32) reader["ReceiveTimes"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
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
        public List<ActivityexHistoryEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ActivityexHistoryEntity>();
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
        public ActivityexHistoryProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ActivityexHistoryProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ActivityexHistoryEntity</returns>
        /// <remarks>2016/3/11 14:31:44</remarks>
        public ActivityexHistoryEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ActivityexHistory_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ActivityexHistoryEntity obj=null;
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
        /// <returns>ActivityexHistoryEntity列表</returns>
        /// <remarks>2016/3/11 14:31:44</remarks>
        public List<ActivityexHistoryEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ActivityexHistory_GetAll");
            

            
            List<ActivityexHistoryEntity> list = null;
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
        /// <remarks>2016/3/11 14:31:44</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ActivityexHistory_Delete");
            
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
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/3/11 14:31:44</remarks>
        public bool Insert(ActivityexHistoryEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ActivityexHistory_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ZoneActivityId", DbType.Int32, entity.ZoneActivityId);
			database.AddInParameter(commandWrapper, "@ExcitingId", DbType.Int32, entity.ExcitingId);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, entity.GroupId);
			database.AddInParameter(commandWrapper, "@ExData", DbType.Int32, entity.ExData);
			database.AddInParameter(commandWrapper, "@ExStep", DbType.Int32, entity.ExStep);
			database.AddInParameter(commandWrapper, "@ReceiveTimes", DbType.Int32, entity.ReceiveTimes);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
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
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/3/11 14:31:44</remarks>
        public bool Update(ActivityexHistoryEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ActivityexHistory_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ZoneActivityId", DbType.Int32, entity.ZoneActivityId);
			database.AddInParameter(commandWrapper, "@ExcitingId", DbType.Int32, entity.ExcitingId);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, entity.GroupId);
			database.AddInParameter(commandWrapper, "@ExData", DbType.Int32, entity.ExData);
			database.AddInParameter(commandWrapper, "@ExStep", DbType.Int32, entity.ExStep);
			database.AddInParameter(commandWrapper, "@ReceiveTimes", DbType.Int32, entity.ReceiveTimes);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
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

