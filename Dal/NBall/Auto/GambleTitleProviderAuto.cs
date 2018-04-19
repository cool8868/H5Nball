

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
    
    public partial class GambleTitleProvider
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
		/// 将IDataReader的当前记录读取到GambleTitleEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public GambleTitleEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new GambleTitleEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.Title = (System.String) reader["Title"];
            obj.IsOfficial = (System.Int32) reader["IsOfficial"];
            obj.ResultFlagId = (System.Guid) reader["ResultFlagId"];
            obj.StartTime = (System.DateTime) reader["StartTime"];
            obj.StopTime = (System.DateTime) reader["StopTime"];
            obj.OpenTime = (System.DateTime) reader["OpenTime"];
            obj.CurrentCount = (System.Int32) reader["CurrentCount"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
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
        public List<GambleTitleEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<GambleTitleEntity>();
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
        public GambleTitleProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public GambleTitleProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetByOptionRateId
		
		/// <summary>
        /// GetByOptionRateId
        /// </summary>
		/// <param name="optionRateId">optionRateId</param>
        /// <returns>GambleTitleEntity</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public GambleTitleEntity GetByOptionRateId( System.Int32 optionRateId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_GetByOptionRateId");
            
			database.AddInParameter(commandWrapper, "@optionRateId", DbType.Int32, optionRateId);

            
            GambleTitleEntity obj=null;
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
		
		#region  GetByIdAndStatus
		
		/// <summary>
        /// GetByIdAndStatus
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="status">status</param>
        /// <returns>GambleTitleEntity</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public GambleTitleEntity GetByIdAndStatus( System.Guid idx, System.Int32 status)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_GetByIdAndStatus");
            
			database.AddInParameter(commandWrapper, "@idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@status", DbType.Int32, status);

            
            GambleTitleEntity obj=null;
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
		
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>GambleTitleEntity</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public GambleTitleEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GambleTitle_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            GambleTitleEntity obj=null;
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
		
		#region  GetStopedGambleTitles
		
		/// <summary>
        /// GetStopedGambleTitles
        /// </summary>
        /// <returns>GambleTitleEntity列表</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public List<GambleTitleEntity> GetStopedGambleTitles()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_GetStopedGambleTitles");
            

            
            List<GambleTitleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetCanHostStartList
		
		/// <summary>
        /// GetCanHostStartList
        /// </summary>
        /// <returns>GambleTitleEntity列表</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public List<GambleTitleEntity> GetCanHostStartList()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_GetCanHostStartList");
            

            
            List<GambleTitleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetStartList
		
		/// <summary>
        /// GetStartList
        /// </summary>
        /// <returns>GambleTitleEntity列表</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public List<GambleTitleEntity> GetStartList()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_GetStartList");
            

            
            List<GambleTitleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetNeedOpenGambleTitles
		
		/// <summary>
        /// GetNeedOpenGambleTitles
        /// </summary>
        /// <returns>GambleTitleEntity列表</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public List<GambleTitleEntity> GetNeedOpenGambleTitles()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_GetNeedOpenGambleTitles");
            

            
            List<GambleTitleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByStatus
		
		/// <summary>
        /// GetByStatus
        /// </summary>
        /// <returns>GambleTitleEntity列表</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public List<GambleTitleEntity> GetByStatus( System.Int32 status)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_GetByStatus");
            
			database.AddInParameter(commandWrapper, "@status", DbType.Int32, status);

            
            List<GambleTitleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByDate
		
		/// <summary>
        /// GetByDate
        /// </summary>
        /// <returns>GambleTitleEntity列表</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public List<GambleTitleEntity> GetByDate( System.DateTime date)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_GetByDate");
            
			database.AddInParameter(commandWrapper, "@date", DbType.DateTime, date);

            
            List<GambleTitleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAll
		
		/// <summary>
        /// GetAll
        /// </summary>
        /// <returns>GambleTitleEntity列表</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public List<GambleTitleEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GambleTitle_GetAll");
            

            
            List<GambleTitleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetFirstTime
		
		/// <summary>
        /// GetFirstTime
        /// </summary>

        /// <returns>DateTime</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public DateTime GetFirstTime ()
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_GetFirstTime");
            

            
            
            DateTime rValue = (DateTime)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  AddCount
		
		/// <summary>
        /// AddCount
        /// </summary>
		/// <param name="titleId">titleId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public bool AddCount ( System.Guid titleId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleTitle_AddCount");
            
			database.AddInParameter(commandWrapper, "@titleId", DbType.Guid, titleId);

            
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
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public bool Delete ( System.Guid idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GambleTitle_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
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
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public bool Insert(GambleTitleEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GambleTitle_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Title", DbType.String, entity.Title);
			database.AddInParameter(commandWrapper, "@IsOfficial", DbType.Int32, entity.IsOfficial);
			database.AddInParameter(commandWrapper, "@ResultFlagId", DbType.Guid, entity.ResultFlagId);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@StopTime", DbType.DateTime, entity.StopTime);
			database.AddInParameter(commandWrapper, "@OpenTime", DbType.DateTime, entity.OpenTime);
			database.AddInParameter(commandWrapper, "@CurrentCount", DbType.Int32, entity.CurrentCount);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/7 18:38:46</remarks>
        public bool Update(GambleTitleEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GambleTitle_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@Title", DbType.String, entity.Title);
			database.AddInParameter(commandWrapper, "@IsOfficial", DbType.Int32, entity.IsOfficial);
			database.AddInParameter(commandWrapper, "@ResultFlagId", DbType.Guid, entity.ResultFlagId);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@StopTime", DbType.DateTime, entity.StopTime);
			database.AddInParameter(commandWrapper, "@OpenTime", DbType.DateTime, entity.OpenTime);
			database.AddInParameter(commandWrapper, "@CurrentCount", DbType.Int32, entity.CurrentCount);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
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

            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
