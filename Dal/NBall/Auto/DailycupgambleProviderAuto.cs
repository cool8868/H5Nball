

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
    
    public partial class DailycupGambleProvider
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
		/// 将IDataReader的当前记录读取到DailycupGambleEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DailycupGambleEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DailycupGambleEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.GamblePoint = (System.Int32) reader["GamblePoint"];
            obj.MatchId = (System.Guid) reader["MatchId"];
            obj.HomeName = (System.String) reader["HomeName"];
            obj.AwayName = (System.String) reader["AwayName"];
            obj.DailyCupId = (System.Int32) reader["DailyCupId"];
            obj.RoundLevel = (System.Int32) reader["RoundLevel"];
            obj.GambleResult = (System.Int32) reader["GambleResult"];
            obj.GambleManagerId = (System.Guid) reader["GambleManagerId"];
            obj.GambleManagerName = (System.String) reader["GambleManagerName"];
            obj.ResultPoint = (System.Int32) reader["ResultPoint"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DailycupGambleEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DailycupGambleEntity>();
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
        public DailycupGambleProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DailycupGambleProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DailycupGambleEntity</returns>
        /// <remarks>2016/5/9 19:15:26</remarks>
        public DailycupGambleEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailycupGamble_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DailycupGambleEntity obj=null;
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
        /// <returns>DailycupGambleEntity列表</returns>
        /// <remarks>2016/5/9 19:15:26</remarks>
        public List<DailycupGambleEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailycupGamble_GetAll");
            

            
            List<DailycupGambleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetMyGamebleData
		
		/// <summary>
        /// GetMyGamebleData
        /// </summary>
        /// <returns>DailycupGambleEntity列表</returns>
        /// <remarks>2016/5/9 19:15:26</remarks>
        public List<DailycupGambleEntity> GetMyGamebleData( System.Int32 dailyCupId, System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_GetMyGamebleData");
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, dailyCupId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<DailycupGambleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetGambleByMatchId
		
		/// <summary>
        /// GetGambleByMatchId
        /// </summary>
        /// <returns>DailycupGambleEntity列表</returns>
        /// <remarks>2016/5/9 19:15:26</remarks>
        public List<DailycupGambleEntity> GetGambleByMatchId( System.Guid matchId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_GetGambleByMatchId");
            
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, matchId);

            
            List<DailycupGambleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GambleCheck
		
		/// <summary>
        /// GambleCheck
        /// </summary>
		/// <param name="dailyCupId">dailyCupId</param>
		/// <param name="managerId">managerId</param>
		/// <param name="matchId">matchId</param>
        /// <returns>Int32</returns>
        /// <remarks>2016/5/9 19:15:26</remarks>
        public Int32 GambleCheck ( System.Int32 dailyCupId, System.Guid managerId, System.Guid matchId)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_GambleCheck");
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, dailyCupId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, matchId);

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  GambleNumber
		
		/// <summary>
        /// GambleNumber
        /// </summary>
		/// <param name="dailyCupId">dailyCupId</param>
		/// <param name="managerId">managerId</param>
        /// <returns>Int32</returns>
        /// <remarks>2016/5/9 19:15:26</remarks>
        public Int32 GambleNumber ( System.Int32 dailyCupId, System.Guid managerId)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_GambleNumber");
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, dailyCupId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  UnlockCardCheck
		
		/// <summary>
        /// UnlockCardCheck
        /// </summary>
		/// <param name="itemId">itemId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/9 19:15:26</remarks>
        public bool UnlockCardCheck ( System.Guid itemId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_UnlockCardCheck");
            
			database.AddInParameter(commandWrapper, "@ItemId", DbType.Guid, itemId);

            
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
		
		#region  Open
		
		/// <summary>
        /// Open
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="idx">idx</param>
		/// <param name="resultLevel">resultLevel</param>
		/// <param name="status">status</param>
		/// <param name="itemString">itemString</param>
		/// <param name="rowVersion">rowVersion</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/9 19:15:26</remarks>
        public bool Open ( System.Guid managerId, System.Int32 idx, System.Int32 resultLevel, System.Int32 status, System.Byte[] itemString, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCupGameble_Open");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@ResultLevel", DbType.Int32, resultLevel);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, itemString);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);
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
        /// <remarks>2016/5/9 19:15:26</remarks>
        public bool Insert(DailycupGambleEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailycupGamble_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@GamblePoint", DbType.Int32, entity.GamblePoint);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, entity.MatchId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, entity.DailyCupId);
			database.AddInParameter(commandWrapper, "@RoundLevel", DbType.Int32, entity.RoundLevel);
			database.AddInParameter(commandWrapper, "@GambleResult", DbType.Int32, entity.GambleResult);
			database.AddInParameter(commandWrapper, "@GambleManagerId", DbType.Guid, entity.GambleManagerId);
			database.AddInParameter(commandWrapper, "@GambleManagerName", DbType.String, entity.GambleManagerName);
			database.AddInParameter(commandWrapper, "@ResultPoint", DbType.Int32, entity.ResultPoint);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
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
        /// <remarks>2016/5/9 19:15:26</remarks>
        public bool Update(DailycupGambleEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailycupGamble_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@GamblePoint", DbType.Int32, entity.GamblePoint);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, entity.MatchId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, entity.DailyCupId);
			database.AddInParameter(commandWrapper, "@RoundLevel", DbType.Int32, entity.RoundLevel);
			database.AddInParameter(commandWrapper, "@GambleResult", DbType.Int32, entity.GambleResult);
			database.AddInParameter(commandWrapper, "@GambleManagerId", DbType.Guid, entity.GambleManagerId);
			database.AddInParameter(commandWrapper, "@GambleManagerName", DbType.String, entity.GambleManagerName);
			database.AddInParameter(commandWrapper, "@ResultPoint", DbType.Int32, entity.ResultPoint);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);

            
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

