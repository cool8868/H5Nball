

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
    
    public partial class CrosscrowdMatchProvider
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
		/// 将IDataReader的当前记录读取到CrosscrowdMatchEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public CrosscrowdMatchEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new CrosscrowdMatchEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.CrossCrowdId = (System.Int32) reader["CrossCrowdId"];
            obj.PairIndex = (System.Int32) reader["PairIndex"];
            obj.HomeSiteId = (System.String) reader["HomeSiteId"];
            obj.AwaySiteId = (System.String) reader["AwaySiteId"];
            obj.HomeId = (System.Guid) reader["HomeId"];
            obj.AwayId = (System.Guid) reader["AwayId"];
            obj.HomeName = (System.String) reader["HomeName"];
            obj.AwayName = (System.String) reader["AwayName"];
            obj.HomeScore = (System.Int32) reader["HomeScore"];
            obj.AwayScore = (System.Int32) reader["AwayScore"];
            obj.HomePrizeCoin = (System.Int32) reader["HomePrizeCoin"];
            obj.HomePrizeHonor = (System.Int32) reader["HomePrizeHonor"];
            obj.HomeMorale = (System.Int32) reader["HomeMorale"];
            obj.HomeCostMorale = (System.Int32) reader["HomeCostMorale"];
            obj.HomePrizeScore = (System.Int32) reader["HomePrizeScore"];
            obj.AwayPrizeCoin = (System.Int32) reader["AwayPrizeCoin"];
            obj.AwayPrizeHonor = (System.Int32) reader["AwayPrizeHonor"];
            obj.AwayMorale = (System.Int32) reader["AwayMorale"];
            obj.AwayCostMorale = (System.Int32) reader["AwayCostMorale"];
            obj.AwayPrizeScore = (System.Int32) reader["AwayPrizeScore"];
            obj.IsKill = (System.Boolean) reader["IsKill"];
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
        public List<CrosscrowdMatchEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<CrosscrowdMatchEntity>();
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
        public CrosscrowdMatchProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public CrosscrowdMatchProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>CrosscrowdMatchEntity</returns>
        /// <remarks>2016-08-15 16:49:36</remarks>
        public CrosscrowdMatchEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrosscrowdMatch_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            CrosscrowdMatchEntity obj=null;
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
        /// <returns>CrosscrowdMatchEntity列表</returns>
        /// <remarks>2016-08-15 16:49:36</remarks>
        public List<CrosscrowdMatchEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrosscrowdMatch_GetAll");
            

            
            List<CrosscrowdMatchEntity> list = null;
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
        /// <remarks>2016-08-15 16:49:36</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrosscrowdMatch_Delete");
            
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
		
		#region  SaveKillPrizeStatus
		
		/// <summary>
        /// SaveKillPrizeStatus
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="status">status</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-08-15 16:49:36</remarks>
        public bool SaveKillPrizeStatus ( System.Guid idx, System.Int32 status,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Crowd_SaveKillPrizeStatus");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);

            
            
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
        /// <remarks>2016-08-15 16:49:36</remarks>
        public bool Insert(CrosscrowdMatchEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 16:49:36</remarks>
        public bool Insert(CrosscrowdMatchEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrosscrowdMatch_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@CrossCrowdId", DbType.Int32, entity.CrossCrowdId);
			database.AddInParameter(commandWrapper, "@PairIndex", DbType.Int32, entity.PairIndex);
			database.AddInParameter(commandWrapper, "@HomeSiteId", DbType.AnsiString, entity.HomeSiteId);
			database.AddInParameter(commandWrapper, "@AwaySiteId", DbType.AnsiString, entity.AwaySiteId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
			database.AddInParameter(commandWrapper, "@HomePrizeCoin", DbType.Int32, entity.HomePrizeCoin);
			database.AddInParameter(commandWrapper, "@HomePrizeHonor", DbType.Int32, entity.HomePrizeHonor);
			database.AddInParameter(commandWrapper, "@HomeMorale", DbType.Int32, entity.HomeMorale);
			database.AddInParameter(commandWrapper, "@HomeCostMorale", DbType.Int32, entity.HomeCostMorale);
			database.AddInParameter(commandWrapper, "@HomePrizeScore", DbType.Int32, entity.HomePrizeScore);
			database.AddInParameter(commandWrapper, "@AwayPrizeCoin", DbType.Int32, entity.AwayPrizeCoin);
			database.AddInParameter(commandWrapper, "@AwayPrizeHonor", DbType.Int32, entity.AwayPrizeHonor);
			database.AddInParameter(commandWrapper, "@AwayMorale", DbType.Int32, entity.AwayMorale);
			database.AddInParameter(commandWrapper, "@AwayCostMorale", DbType.Int32, entity.AwayCostMorale);
			database.AddInParameter(commandWrapper, "@AwayPrizeScore", DbType.Int32, entity.AwayPrizeScore);
			database.AddInParameter(commandWrapper, "@IsKill", DbType.Boolean, entity.IsKill);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016-08-15 16:49:36</remarks>
        public bool Update(CrosscrowdMatchEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 16:49:36</remarks>
        public bool Update(CrosscrowdMatchEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrosscrowdMatch_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@CrossCrowdId", DbType.Int32, entity.CrossCrowdId);
			database.AddInParameter(commandWrapper, "@PairIndex", DbType.Int32, entity.PairIndex);
			database.AddInParameter(commandWrapper, "@HomeSiteId", DbType.AnsiString, entity.HomeSiteId);
			database.AddInParameter(commandWrapper, "@AwaySiteId", DbType.AnsiString, entity.AwaySiteId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
			database.AddInParameter(commandWrapper, "@HomePrizeCoin", DbType.Int32, entity.HomePrizeCoin);
			database.AddInParameter(commandWrapper, "@HomePrizeHonor", DbType.Int32, entity.HomePrizeHonor);
			database.AddInParameter(commandWrapper, "@HomeMorale", DbType.Int32, entity.HomeMorale);
			database.AddInParameter(commandWrapper, "@HomeCostMorale", DbType.Int32, entity.HomeCostMorale);
			database.AddInParameter(commandWrapper, "@HomePrizeScore", DbType.Int32, entity.HomePrizeScore);
			database.AddInParameter(commandWrapper, "@AwayPrizeCoin", DbType.Int32, entity.AwayPrizeCoin);
			database.AddInParameter(commandWrapper, "@AwayPrizeHonor", DbType.Int32, entity.AwayPrizeHonor);
			database.AddInParameter(commandWrapper, "@AwayMorale", DbType.Int32, entity.AwayMorale);
			database.AddInParameter(commandWrapper, "@AwayCostMorale", DbType.Int32, entity.AwayCostMorale);
			database.AddInParameter(commandWrapper, "@AwayPrizeScore", DbType.Int32, entity.AwayPrizeScore);
			database.AddInParameter(commandWrapper, "@IsKill", DbType.Boolean, entity.IsKill);
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

            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
