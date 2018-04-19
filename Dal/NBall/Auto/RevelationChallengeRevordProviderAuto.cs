

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
    
    public partial class RevelationChallengerevordProvider
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
		/// 将IDataReader的当前记录读取到RevelationChallengerevordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public RevelationChallengerevordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new RevelationChallengerevordEntity();
			
            obj.GameId = (System.Guid) reader["GameId"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Mark = (System.Int32) reader["Mark"];
            obj.Schedule = (System.Int32) reader["Schedule"];
            obj.Goals = (System.Int32) reader["Goals"];
            obj.ToConcede = (System.Int32) reader["ToConcede"];
            obj.GameDate = (System.DateTime) reader["GameDate"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<RevelationChallengerevordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<RevelationChallengerevordEntity>();
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
        public RevelationChallengerevordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public RevelationChallengerevordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="gameId">gameId</param>
        /// <returns>RevelationChallengerevordEntity</returns>
        /// <remarks>2014/11/21 15:12:58</remarks>
        public RevelationChallengerevordEntity GetById( System.Guid gameId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationChallengerevord_GetById");
            
			database.AddInParameter(commandWrapper, "@GameId", DbType.Guid, gameId);

            
            RevelationChallengerevordEntity obj=null;
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
        /// <returns>RevelationChallengerevordEntity列表</returns>
        /// <remarks>2014/11/21 15:12:58</remarks>
        public List<RevelationChallengerevordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationChallengerevord_GetAll");
            

            
            List<RevelationChallengerevordEntity> list = null;
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
		/// <param name="gameId">gameId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/11/21 15:12:58</remarks>
        public bool Delete ( System.Guid gameId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationChallengerevord_Delete");
            
			database.AddInParameter(commandWrapper, "@GameId", DbType.Guid, gameId);

            
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
        /// <remarks>2014/11/21 15:12:58</remarks>
        public bool Insert(RevelationChallengerevordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationChallengerevord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, entity.Goals);
			database.AddInParameter(commandWrapper, "@ToConcede", DbType.Int32, entity.ToConcede);
			database.AddInParameter(commandWrapper, "@GameDate", DbType.DateTime, entity.GameDate);
			database.AddParameter(commandWrapper, "@GameId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.GameId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.GameId=(System.Guid)database.GetParameterValue(commandWrapper, "@GameId");
            
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
        /// <remarks>2014/11/21 15:12:58</remarks>
        public bool Update(RevelationChallengerevordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationChallengerevord_Update");
            
			database.AddInParameter(commandWrapper, "@GameId", DbType.Guid, entity.GameId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, entity.Goals);
			database.AddInParameter(commandWrapper, "@ToConcede", DbType.Int32, entity.ToConcede);
			database.AddInParameter(commandWrapper, "@GameDate", DbType.DateTime, entity.GameDate);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.GameId=(System.Guid)database.GetParameterValue(commandWrapper, "@GameId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

