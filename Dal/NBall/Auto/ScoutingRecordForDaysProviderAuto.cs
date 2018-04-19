

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
    
    public partial class ScoutingRecordfordaysProvider
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
		/// 将IDataReader的当前记录读取到ScoutingRecordfordaysEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ScoutingRecordfordaysEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ScoutingRecordfordaysEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.CardItemCodeThen89 = (System.Int32) reader["CardItemCodeThen89"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.ScoutingType = (System.Int32) reader["ScoutingType"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ScoutingRecordfordaysEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ScoutingRecordfordaysEntity>();
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
        public ScoutingRecordfordaysProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ScoutingRecordfordaysProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ScoutingRecordfordaysEntity</returns>
        /// <remarks>2016/10/21 16:08:04</remarks>
        public ScoutingRecordfordaysEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ScoutingRecordfordays_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ScoutingRecordfordaysEntity obj=null;
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
        /// <returns>ScoutingRecordfordaysEntity列表</returns>
        /// <remarks>2016/10/21 16:08:04</remarks>
        public List<ScoutingRecordfordaysEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ScoutingRecordfordays_GetAll");
            

            
            List<ScoutingRecordfordaysEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetCountByTime
		
		/// <summary>
        /// GetCountByTime
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="scoutingType">scoutingType</param>
		/// <param name="count">count</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/10/21 16:08:04</remarks>
        public bool GetCountByTime ( System.Guid managerId, System.DateTime startTime, System.DateTime endTime, System.Int32 scoutingType,ref  System.Int32 count,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Scouting_GetCountByTime");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddInParameter(commandWrapper, "@ScoutingType", DbType.Int32, scoutingType);
			database.AddParameter(commandWrapper, "@Count", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,count);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            count=(System.Int32)database.GetParameterValue(commandWrapper, "@Count");
            
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
        /// <remarks>2016/10/21 16:08:04</remarks>
        public bool Insert(ScoutingRecordfordaysEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ScoutingRecordfordays_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@CardItemCodeThen89", DbType.Int32, entity.CardItemCodeThen89);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@ScoutingType", DbType.Int32, entity.ScoutingType);
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
        /// <remarks>2016/10/21 16:08:04</remarks>
        public bool Update(ScoutingRecordfordaysEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ScoutingRecordfordays_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@CardItemCodeThen89", DbType.Int32, entity.CardItemCodeThen89);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@ScoutingType", DbType.Int32, entity.ScoutingType);

            
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
