

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
    
    public partial class TeammemberTrainProvider
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
		/// 将IDataReader的当前记录读取到TeammemberTrainEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TeammemberTrainEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TeammemberTrainEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.PlayerId = (System.Int32) reader["PlayerId"];
            obj.Level = (System.Int32) reader["Level"];
            obj.EXP = (System.Int32) reader["EXP"];
            obj.TrainStamina = (System.Int32) reader["TrainStamina"];
            obj.TrainState = (System.Int32) reader["TrainState"];
            obj.StartTime = (System.DateTime) reader["StartTime"];
            obj.SettleTime = (System.DateTime) reader["SettleTime"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.TrainType = (System.Int32) reader["TrainType"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<TeammemberTrainEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TeammemberTrainEntity>();
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
        public TeammemberTrainProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TeammemberTrainProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>TeammemberTrainEntity</returns>
        /// <remarks>2015/10/18 15:50:28</remarks>
        public TeammemberTrainEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TeammemberTrain_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            TeammemberTrainEntity obj=null;
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
		
		#region  GetForStart
		
		/// <summary>
        /// GetForStart
        /// </summary>
		/// <param name="teammemberId">teammemberId</param>
		/// <param name="trainStamina">trainStamina</param>
		/// <param name="mod">mod</param>
        /// <returns>TeammemberTrainEntity</returns>
        /// <remarks>2015/10/18 15:50:28</remarks>
        public TeammemberTrainEntity GetForStart( System.Guid teammemberId, System.Int32 trainStamina, System.Int32 mod)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TeammemberTrain_GetForStart");
            
			database.AddInParameter(commandWrapper, "@TeammemberId", DbType.Guid, teammemberId);
			database.AddInParameter(commandWrapper, "@TrainStamina", DbType.Int32, trainStamina);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            TeammemberTrainEntity obj=null;
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
        /// <returns>TeammemberTrainEntity列表</returns>
        /// <remarks>2015/10/18 15:50:28</remarks>
        public List<TeammemberTrainEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TeammemberTrain_GetAll");
            

            
            List<TeammemberTrainEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
		/// <summary>
        /// GetByManagerId
        /// </summary>
        /// <returns>TeammemberTrainEntity列表</returns>
        /// <remarks>2015/10/18 15:50:28</remarks>
        public List<TeammemberTrainEntity> GetByManagerId( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TeammemberTrain_GetByManagerId");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<TeammemberTrainEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetTrainList
		
		/// <summary>
        /// GetTrainList
        /// </summary>
        /// <returns>TeammemberTrainEntity列表</returns>
        /// <remarks>2015/10/18 15:50:28</remarks>
        public List<TeammemberTrainEntity> GetTrainList()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TeammemberTrain_GetTrainList");
            

            
            List<TeammemberTrainEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetRestList
		
		/// <summary>
        /// GetRestList
        /// </summary>
        /// <returns>TeammemberTrainEntity列表</returns>
        /// <remarks>2015/10/18 15:50:28</remarks>
        public List<TeammemberTrainEntity> GetRestList()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TeammemberTrain_GetRestList");
            

            
            List<TeammemberTrainEntity> list = null;
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
        /// <remarks>2015/10/18 15:50:29</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TeammemberTrain_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
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
		
		#region  Start
		
		/// <summary>
        /// Start
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="teammemberId">teammemberId</param>
		/// <param name="trainSeatOverCode">trainSeatOverCode</param>
		/// <param name="trainStamina">trainStamina</param>
		/// <param name="startTime">startTime</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 15:50:29</remarks>
        public bool Start ( System.Guid managerId, System.Guid teammemberId, System.Int32 trainSeatOverCode, System.Int32 trainStamina, System.DateTime startTime,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TeammemberTrain_Start");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@TeammemberId", DbType.Guid, teammemberId);
			database.AddInParameter(commandWrapper, "@TrainSeatOverCode", DbType.Int32, trainSeatOverCode);
			database.AddInParameter(commandWrapper, "@TrainStamina", DbType.Int32, trainStamina);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
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
		
		#region  UpdateData
		
		/// <summary>
        /// UpdateData
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="level">level</param>
		/// <param name="eXP">eXP</param>
		/// <param name="trainStamina">trainStamina</param>
		/// <param name="trainState">trainState</param>
		/// <param name="startTime">startTime</param>
		/// <param name="settleTime">settleTime</param>
		/// <param name="status">status</param>
		/// <param name="mod">mod</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 15:50:29</remarks>
        public bool UpdateData ( System.Guid idx, System.Int32 level, System.Int32 eXP, System.Int32 trainStamina, System.Int32 trainState, System.DateTime startTime, System.DateTime settleTime, System.Int32 status, System.Int32 mod,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TeammemberTrain_UpdateData");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);
			database.AddInParameter(commandWrapper, "@EXP", DbType.Int32, eXP);
			database.AddInParameter(commandWrapper, "@TrainStamina", DbType.Int32, trainStamina);
			database.AddInParameter(commandWrapper, "@TrainState", DbType.Int32, trainState);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@SettleTime", DbType.DateTime, settleTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
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
        /// <remarks>2015/10/18 15:50:29</remarks>
        public bool Insert(TeammemberTrainEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TeammemberTrain_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@PlayerId", DbType.Int32, entity.PlayerId);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@EXP", DbType.Int32, entity.EXP);
			database.AddInParameter(commandWrapper, "@TrainStamina", DbType.Int32, entity.TrainStamina);
			database.AddInParameter(commandWrapper, "@TrainState", DbType.Int32, entity.TrainState);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@SettleTime", DbType.DateTime, entity.SettleTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@TrainType", DbType.Int32, entity.TrainType);
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
        /// <remarks>2015/10/18 15:50:29</remarks>
        public bool Update(TeammemberTrainEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TeammemberTrain_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@PlayerId", DbType.Int32, entity.PlayerId);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@EXP", DbType.Int32, entity.EXP);
			database.AddInParameter(commandWrapper, "@TrainStamina", DbType.Int32, entity.TrainStamina);
			database.AddInParameter(commandWrapper, "@TrainState", DbType.Int32, entity.TrainState);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@SettleTime", DbType.DateTime, entity.SettleTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@TrainType", DbType.Int32, entity.TrainType);

            
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

