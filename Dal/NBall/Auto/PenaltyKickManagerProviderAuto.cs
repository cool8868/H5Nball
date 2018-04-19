

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
    
    public partial class PenaltykickManagerProvider
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
		/// 将IDataReader的当前记录读取到PenaltykickManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public PenaltykickManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new PenaltykickManagerEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ShootNumber = (System.Int32) reader["ShootNumber"];
            obj.FreeNumber = (System.Int32) reader["FreeNumber"];
            obj.GameCurrency = (System.Int32) reader["GameCurrency"];
            obj.DayProduceLuckyCoin = (System.Int32) reader["DayProduceLuckyCoin"];
            obj.TotalScore = (System.Int32) reader["TotalScore"];
            obj.AvailableScore = (System.Int32) reader["AvailableScore"];
            obj.TotalGoals = (System.Int32) reader["TotalGoals"];
            obj.ShooterAttribute = (System.Int32) reader["ShooterAttribute"];
            obj.ShootLog = (System.String) reader["ShootLog"];
            obj.CombGoals = (System.Int32) reader["CombGoals"];
            obj.MaxCombGoals = (System.Int32) reader["MaxCombGoals"];
            obj.ExChangeString = (System.String) reader["ExChangeString"];
            obj.Status = (System.Int32) reader["Status"];
            obj.Rank = (System.Int32) reader["Rank"];
            obj.IsPrize = (System.Boolean) reader["IsPrize"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.RefreshDate = (System.DateTime) reader["RefreshDate"];
            obj.ScoreChangeTime = (System.DateTime) reader["ScoreChangeTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<PenaltykickManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<PenaltykickManagerEntity>();
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
        public PenaltykickManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public PenaltykickManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>PenaltykickManagerEntity</returns>
        /// <remarks>2016/9/28 13:30:55</remarks>
        public PenaltykickManagerEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PenaltykickManager_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            PenaltykickManagerEntity obj=null;
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
        /// <returns>PenaltykickManagerEntity列表</returns>
        /// <remarks>2016/9/28 13:30:55</remarks>
        public List<PenaltykickManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PenaltykickManager_GetAll");
            

            
            List<PenaltykickManagerEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetNotPrize
		
		/// <summary>
        /// GetNotPrize
        /// </summary>
        /// <returns>PenaltykickManagerEntity列表</returns>
        /// <remarks>2016/9/28 13:30:55</remarks>
        public List<PenaltykickManagerEntity> GetNotPrize( System.Int32 seasonId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PenaltyKickManager_GetNotPrize");
            
			database.AddInParameter(commandWrapper, "@SeasonId", DbType.Int32, seasonId);

            
            List<PenaltykickManagerEntity> list = null;
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
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/28 13:30:55</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PenaltykickManager_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
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
		
		#region  SetRank
		
		/// <summary>
        /// SetRank
        /// </summary>

        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/28 13:30:55</remarks>
        public bool SetRank (DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PenaltyKick_SetRank");
            

            
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
		
		#region  AddGameCurrency
		
		/// <summary>
        /// AddGameCurrency
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="number">number</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/28 13:30:55</remarks>
        public bool AddGameCurrency ( System.Guid managerId, System.Int32 number,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PenaltyKickManager_AddGameCurrency");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Number", DbType.Int32, number);

            
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
		
		#region  InsertRecord
		
		/// <summary>
        /// InsertRecord
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="number">number</param>
		/// <param name="isFree">isFree</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/28 13:30:55</remarks>
        public bool InsertRecord ( System.Guid managerId, System.Int32 number, System.Boolean isFree,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PenaltyKickRecord_InsertRecord");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Number", DbType.Int32, number);
			database.AddInParameter(commandWrapper, "@IsFree", DbType.Boolean, isFree);

            
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
		
		#region  AddSystemProduceGameCurrency
		
		/// <summary>
        /// AddSystemProduceGameCurrency
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="number">number</param>
		/// <param name="addSuccessNumber">addSuccessNumber</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/28 13:30:55</remarks>
        public bool AddSystemProduceGameCurrency ( System.Guid managerId, System.Int32 number,ref  System.Int32 addSuccessNumber,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PenaltyKickManager_AddSystemProduceGameCurrency");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Number", DbType.Int32, number);
			database.AddParameter(commandWrapper, "@AddSuccessNumber", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,addSuccessNumber);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            addSuccessNumber=(System.Int32)database.GetParameterValue(commandWrapper, "@AddSuccessNumber");
            
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
        /// <remarks>2016/9/28 13:30:55</remarks>
        public bool Insert(PenaltykickManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PenaltykickManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ShootNumber", DbType.Int32, entity.ShootNumber);
			database.AddInParameter(commandWrapper, "@FreeNumber", DbType.Int32, entity.FreeNumber);
			database.AddInParameter(commandWrapper, "@GameCurrency", DbType.Int32, entity.GameCurrency);
			database.AddInParameter(commandWrapper, "@DayProduceLuckyCoin", DbType.Int32, entity.DayProduceLuckyCoin);
			database.AddInParameter(commandWrapper, "@TotalScore", DbType.Int32, entity.TotalScore);
			database.AddInParameter(commandWrapper, "@AvailableScore", DbType.Int32, entity.AvailableScore);
			database.AddInParameter(commandWrapper, "@TotalGoals", DbType.Int32, entity.TotalGoals);
			database.AddInParameter(commandWrapper, "@ShooterAttribute", DbType.Int32, entity.ShooterAttribute);
			database.AddInParameter(commandWrapper, "@ShootLog", DbType.AnsiString, entity.ShootLog);
			database.AddInParameter(commandWrapper, "@CombGoals", DbType.Int32, entity.CombGoals);
			database.AddInParameter(commandWrapper, "@MaxCombGoals", DbType.Int32, entity.MaxCombGoals);
			database.AddInParameter(commandWrapper, "@ExChangeString", DbType.AnsiString, entity.ExChangeString);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@IsPrize", DbType.Boolean, entity.IsPrize);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.Date, entity.RefreshDate);
			database.AddInParameter(commandWrapper, "@ScoreChangeTime", DbType.DateTime, entity.ScoreChangeTime);
			database.AddParameter(commandWrapper, "@ManagerId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.ManagerId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
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
        /// <remarks>2016/9/28 13:30:55</remarks>
        public bool Update(PenaltykickManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PenaltykickManager_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ShootNumber", DbType.Int32, entity.ShootNumber);
			database.AddInParameter(commandWrapper, "@FreeNumber", DbType.Int32, entity.FreeNumber);
			database.AddInParameter(commandWrapper, "@GameCurrency", DbType.Int32, entity.GameCurrency);
			database.AddInParameter(commandWrapper, "@DayProduceLuckyCoin", DbType.Int32, entity.DayProduceLuckyCoin);
			database.AddInParameter(commandWrapper, "@TotalScore", DbType.Int32, entity.TotalScore);
			database.AddInParameter(commandWrapper, "@AvailableScore", DbType.Int32, entity.AvailableScore);
			database.AddInParameter(commandWrapper, "@TotalGoals", DbType.Int32, entity.TotalGoals);
			database.AddInParameter(commandWrapper, "@ShooterAttribute", DbType.Int32, entity.ShooterAttribute);
			database.AddInParameter(commandWrapper, "@ShootLog", DbType.AnsiString, entity.ShootLog);
			database.AddInParameter(commandWrapper, "@CombGoals", DbType.Int32, entity.CombGoals);
			database.AddInParameter(commandWrapper, "@MaxCombGoals", DbType.Int32, entity.MaxCombGoals);
			database.AddInParameter(commandWrapper, "@ExChangeString", DbType.AnsiString, entity.ExChangeString);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@IsPrize", DbType.Boolean, entity.IsPrize);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.Date, entity.RefreshDate);
			database.AddInParameter(commandWrapper, "@ScoreChangeTime", DbType.DateTime, entity.ScoreChangeTime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
