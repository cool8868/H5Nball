

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
    
    public partial class DailycupInfoProvider
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
		/// 将IDataReader的当前记录读取到DailycupInfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DailycupInfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DailycupInfoEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Round = (System.Int32) reader["Round"];
            obj.OpenGambleRound = (System.Int32) reader["OpenGambleRound"];
            obj.AttendDate = (System.DateTime) reader["AttendDate"];
            obj.RunDate = (System.DateTime) reader["RunDate"];
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
        public List<DailycupInfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DailycupInfoEntity>();
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
        public DailycupInfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DailycupInfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DailycupInfoEntity</returns>
        /// <remarks>2016/1/13 10:35:41</remarks>
        public DailycupInfoEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailycupInfo_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DailycupInfoEntity obj=null;
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
		
		#region  GetByDate
		
		/// <summary>
        /// GetByDate
        /// </summary>
		/// <param name="rundate">rundate</param>
        /// <returns>DailycupInfoEntity</returns>
        /// <remarks>2016/1/13 10:35:41</remarks>
        public DailycupInfoEntity GetByDate( System.DateTime rundate)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_GetByDate");
            
			database.AddInParameter(commandWrapper, "@Rundate", DbType.DateTime, rundate);

            
            DailycupInfoEntity obj=null;
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
        /// <returns>DailycupInfoEntity列表</returns>
        /// <remarks>2016/1/13 10:35:41</remarks>
        public List<DailycupInfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailycupInfo_GetAll");
            

            
            List<DailycupInfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetListByTime
		
		/// <summary>
        /// GetListByTime
        /// </summary>
        /// <returns>DailycupInfoEntity列表</returns>
        /// <remarks>2016/1/13 10:35:41</remarks>
        public List<DailycupInfoEntity> GetListByTime( System.DateTime beginTime, System.DateTime endTime)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_GetListByTime");
            
			database.AddInParameter(commandWrapper, "@BeginTime", DbType.DateTime, beginTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);

            
            List<DailycupInfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Create
		
		/// <summary>
        /// Create
        /// </summary>

        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/13 10:35:41</remarks>
        public bool Create (DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("J_Dailycup_Create");
            

            
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
		
		#region  SendPrize
		
		/// <summary>
        /// SendPrize
        /// </summary>

        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/13 10:35:41</remarks>
        public bool SendPrize (DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("J_Dailycup_SendPrize");
            

            
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
        /// <remarks>2016/1/13 10:35:41</remarks>
        public bool Insert(DailycupInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailycupInfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Round", DbType.Int32, entity.Round);
			database.AddInParameter(commandWrapper, "@OpenGambleRound", DbType.Int32, entity.OpenGambleRound);
			database.AddInParameter(commandWrapper, "@AttendDate", DbType.DateTime, entity.AttendDate);
			database.AddInParameter(commandWrapper, "@RunDate", DbType.DateTime, entity.RunDate);
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
        /// <remarks>2016/1/13 10:35:41</remarks>
        public bool Update(DailycupInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailycupInfo_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Round", DbType.Int32, entity.Round);
			database.AddInParameter(commandWrapper, "@OpenGambleRound", DbType.Int32, entity.OpenGambleRound);
			database.AddInParameter(commandWrapper, "@AttendDate", DbType.DateTime, entity.AttendDate);
			database.AddInParameter(commandWrapper, "@RunDate", DbType.DateTime, entity.RunDate);
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

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

