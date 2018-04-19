

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
    
    public partial class ActivityexRecordProvider
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
		/// 将IDataReader的当前记录读取到ActivityexRecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ActivityexRecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ActivityexRecordEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ZoneActivityId = (System.Int32) reader["ZoneActivityId"];
            obj.ExcitingId = (System.Int32) reader["ExcitingId"];
            obj.GroupId = (System.Int32) reader["GroupId"];
            obj.CurData = (System.Int32) reader["CurData"];
            obj.ExData = (System.Int32) reader["ExData"];
            obj.ExStep = (System.Int32) reader["ExStep"];
            obj.ReceiveTimes = (System.Int32) reader["ReceiveTimes"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ActivityexRecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ActivityexRecordEntity>();
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
        public ActivityexRecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ActivityexRecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ActivityexRecordEntity</returns>
        /// <remarks>2016/5/3 15:01:39</remarks>
        public ActivityexRecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ActivityexRecord_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ActivityexRecordEntity obj=null;
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
		
		#region  GetByManagerExcitingId
		
		/// <summary>
        /// GetByManagerExcitingId
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="zoneActivityId">zoneActivityId</param>
		/// <param name="groupId">groupId</param>
        /// <returns>ActivityexRecordEntity</returns>
        /// <remarks>2016/5/3 15:01:39</remarks>
        public ActivityexRecordEntity GetByManagerExcitingId( System.Guid managerId, System.Int32 zoneActivityId, System.Int32 groupId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityEx_GetByManagerExcitingId");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ZoneActivityId", DbType.Int32, zoneActivityId);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, groupId);

            
            ActivityexRecordEntity obj=null;
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
        /// <returns>ActivityexRecordEntity列表</returns>
        /// <remarks>2016/5/3 15:01:39</remarks>
        public List<ActivityexRecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ActivityexRecord_GetAll");
            

            
            List<ActivityexRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetForSend
		
		/// <summary>
        /// GetForSend
        /// </summary>
        /// <returns>ActivityexRecordEntity列表</returns>
        /// <remarks>2016/5/3 15:01:39</remarks>
        public List<ActivityexRecordEntity> GetForSend( System.Int32 zoneActivityId, System.Int32 groupId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityEx_GetForSend");
            
			database.AddInParameter(commandWrapper, "@ZoneActivityId", DbType.Int32, zoneActivityId);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, groupId);

            
            List<ActivityexRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByActivityId
		
		/// <summary>
        /// GetByActivityId
        /// </summary>
        /// <returns>ActivityexRecordEntity列表</returns>
        /// <remarks>2016/5/3 15:01:39</remarks>
        public List<ActivityexRecordEntity> GetByActivityId( System.Guid managerId, System.Int32 zoneActivityId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityEx_GetByActivityId");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ZoneActivityId", DbType.Int32, zoneActivityId);

            
            List<ActivityexRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetManagerRank
		
		/// <summary>
        /// GetManagerRank
        /// </summary>
        /// <returns>ActivityexRecordEntity列表</returns>
        /// <remarks>2016/5/3 15:01:39</remarks>
        public List<ActivityexRecordEntity> GetManagerRank( System.Int32 zoneActivityId, System.Int32 excitingId, System.Int32 groupId, System.DateTime recordDate)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityEx_GetManagerRank");
            
			database.AddInParameter(commandWrapper, "@ZoneActivityId", DbType.Int32, zoneActivityId);
			database.AddInParameter(commandWrapper, "@ExcitingId", DbType.Int32, excitingId);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, groupId);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);

            
            List<ActivityexRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetCompleteByManager
		
		/// <summary>
        /// GetCompleteByManager
        /// </summary>
        /// <returns>ActivityexRecordEntity列表</returns>
        /// <remarks>2016/5/3 15:01:39</remarks>
        public List<ActivityexRecordEntity> GetCompleteByManager( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityEx_GetCompleteByManager");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<ActivityexRecordEntity> list = null;
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
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/3 15:01:39</remarks>
        public bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ActivityexRecord_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);

            
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
		
		#region  SavePrize
		
		/// <summary>
        /// SavePrize
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="exKey">exKey</param>
		/// <param name="curData">curData</param>
		/// <param name="exData">exData</param>
		/// <param name="exStep">exStep</param>
		/// <param name="receiveTimes">receiveTimes</param>
		/// <param name="recordDate">recordDate</param>
		/// <param name="status">status</param>
		/// <param name="updateTime">updateTime</param>
		/// <param name="rowVersion">rowVersion</param>
		/// <param name="saveHistory">saveHistory</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/3 15:01:39</remarks>
        public bool SavePrize ( System.Int32 idx, System.String exKey, System.Int32 curData, System.Int32 exData, System.Int32 exStep, System.Int32 receiveTimes, System.DateTime recordDate, System.Int32 status, System.DateTime updateTime, System.Byte[] rowVersion, System.Boolean saveHistory,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityEx_SavePrize");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@ExKey", DbType.AnsiString, exKey);
			database.AddInParameter(commandWrapper, "@CurData", DbType.Int32, curData);
			database.AddInParameter(commandWrapper, "@ExData", DbType.Int32, exData);
			database.AddInParameter(commandWrapper, "@ExStep", DbType.Int32, exStep);
			database.AddInParameter(commandWrapper, "@ReceiveTimes", DbType.Int32, receiveTimes);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, updateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);
			database.AddInParameter(commandWrapper, "@SaveHistory", DbType.Boolean, saveHistory);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
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
        /// <remarks>2016/5/3 15:01:39</remarks>
        public bool Insert(ActivityexRecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ActivityexRecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ZoneActivityId", DbType.Int32, entity.ZoneActivityId);
			database.AddInParameter(commandWrapper, "@ExcitingId", DbType.Int32, entity.ExcitingId);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, entity.GroupId);
			database.AddInParameter(commandWrapper, "@CurData", DbType.Int32, entity.CurData);
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
        /// <remarks>2016/5/3 15:01:39</remarks>
        public bool Update(ActivityexRecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ActivityexRecord_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ZoneActivityId", DbType.Int32, entity.ZoneActivityId);
			database.AddInParameter(commandWrapper, "@ExcitingId", DbType.Int32, entity.ExcitingId);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, entity.GroupId);
			database.AddInParameter(commandWrapper, "@CurData", DbType.Int32, entity.CurData);
			database.AddInParameter(commandWrapper, "@ExData", DbType.Int32, entity.ExData);
			database.AddInParameter(commandWrapper, "@ExStep", DbType.Int32, entity.ExStep);
			database.AddInParameter(commandWrapper, "@ReceiveTimes", DbType.Int32, entity.ReceiveTimes);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);

            
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

