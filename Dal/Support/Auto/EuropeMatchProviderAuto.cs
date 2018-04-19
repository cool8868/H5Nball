

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
    
    public partial class EuropeMatchProvider
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
		/// 将IDataReader的当前记录读取到EuropeMatchEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public EuropeMatchEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new EuropeMatchEntity();
			
            obj.MatchId = (System.Int32) reader["MatchId"];
            obj.HomeName = (System.String) reader["HomeName"];
            obj.AwayName = (System.String) reader["AwayName"];
            obj.MatchDate = (System.DateTime) reader["MatchDate"];
            obj.MatchTime = (System.DateTime) reader["MatchTime"];
            obj.HomeGoals = (System.Int32) reader["HomeGoals"];
            obj.AwayGoals = (System.Int32) reader["AwayGoals"];
            obj.ResultType = (System.Int32) reader["ResultType"];
            obj.States = (System.Int32) reader["States"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
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
        public List<EuropeMatchEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<EuropeMatchEntity>();
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
        public EuropeMatchProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public EuropeMatchProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="matchId">matchId</param>
        /// <returns>EuropeMatchEntity</returns>
        /// <remarks>2016/8/18 15:06:34</remarks>
        public EuropeMatchEntity GetById( System.Int32 matchId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_EuropeMatch_GetById");
            
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Int32, matchId);

            
            EuropeMatchEntity obj=null;
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
        /// <returns>EuropeMatchEntity列表</returns>
        /// <remarks>2016/8/18 15:06:34</remarks>
        public List<EuropeMatchEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_EuropeMatch_GetAll");
            

            
            List<EuropeMatchEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAllMatvch
		
		/// <summary>
        /// GetAllMatvch
        /// </summary>
        /// <returns>EuropeMatchEntity列表</returns>
        /// <remarks>2016/8/18 15:06:34</remarks>
        public List<EuropeMatchEntity> GetAllMatvch( System.DateTime startDate)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_EuropeMatch_GetAllMatvch");
            
			database.AddInParameter(commandWrapper, "@StartDate", DbType.Date, startDate);

            
            List<EuropeMatchEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetIsMatch
		
		/// <summary>
        /// GetIsMatch
        /// </summary>
		/// <param name="matchDate">matchDate</param>
        /// <returns>Int32</returns>
        /// <remarks>2016/8/18 15:06:34</remarks>
        public Int32 GetIsMatch ( System.DateTime matchDate)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_EuropeMatch_GetIsMatch");
            
			database.AddInParameter(commandWrapper, "@MatchDate", DbType.Date, matchDate);

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="matchId">matchId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/8/18 15:06:34</remarks>
        public bool Delete ( System.Int32 matchId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_EuropeMatch_Delete");
            
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Int32, matchId);

            
            
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
        /// <remarks>2016/8/18 15:06:34</remarks>
        public bool Insert(EuropeMatchEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/8/18 15:06:34</remarks>
        public bool Insert(EuropeMatchEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_EuropeMatch_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@HomeName", DbType.AnsiString, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.AnsiString, entity.AwayName);
			database.AddInParameter(commandWrapper, "@MatchDate", DbType.Date, entity.MatchDate);
			database.AddInParameter(commandWrapper, "@MatchTime", DbType.DateTime, entity.MatchTime);
			database.AddInParameter(commandWrapper, "@HomeGoals", DbType.Int32, entity.HomeGoals);
			database.AddInParameter(commandWrapper, "@AwayGoals", DbType.Int32, entity.AwayGoals);
			database.AddInParameter(commandWrapper, "@ResultType", DbType.Int32, entity.ResultType);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@MatchId", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.MatchId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.MatchId=(System.Int32)database.GetParameterValue(commandWrapper, "@MatchId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/8/18 15:06:34</remarks>
        public bool Update(EuropeMatchEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/8/18 15:06:34</remarks>
        public bool Update(EuropeMatchEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_EuropeMatch_Update");
            
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Int32, entity.MatchId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.AnsiString, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.AnsiString, entity.AwayName);
			database.AddInParameter(commandWrapper, "@MatchDate", DbType.Date, entity.MatchDate);
			database.AddInParameter(commandWrapper, "@MatchTime", DbType.DateTime, entity.MatchTime);
			database.AddInParameter(commandWrapper, "@HomeGoals", DbType.Int32, entity.HomeGoals);
			database.AddInParameter(commandWrapper, "@AwayGoals", DbType.Int32, entity.AwayGoals);
			database.AddInParameter(commandWrapper, "@ResultType", DbType.Int32, entity.ResultType);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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

            entity.MatchId=(System.Int32)database.GetParameterValue(commandWrapper, "@MatchId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
