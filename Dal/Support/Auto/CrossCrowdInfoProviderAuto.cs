

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
    
    public partial class CrosscrowdInfoProvider
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
		/// 将IDataReader的当前记录读取到CrosscrowdInfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public CrosscrowdInfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new CrosscrowdInfoEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.StartTime = (System.DateTime) reader["StartTime"];
            obj.EndTime = (System.DateTime) reader["EndTime"];
            obj.DomainId = (System.Int32) reader["DomainId"];
            obj.PlayerCount = (System.Int32) reader["PlayerCount"];
            obj.PairCount = (System.Int32) reader["PairCount"];
            obj.PrizePoint = (System.Int32) reader["PrizePoint"];
            obj.IsSendKillPrize = (System.Boolean) reader["IsSendKillPrize"];
            obj.IsSendRankPrize = (System.Boolean) reader["IsSendRankPrize"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.PrizeLegendCount = (System.Int32) reader["PrizeLegendCount"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<CrosscrowdInfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<CrosscrowdInfoEntity>();
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
        public CrosscrowdInfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public CrosscrowdInfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>CrosscrowdInfoEntity</returns>
        /// <remarks>2016-08-15 11:12:27</remarks>
        public CrosscrowdInfoEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrosscrowdInfo_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            CrosscrowdInfoEntity obj=null;
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
		
		#region  GetCurrent
		
		/// <summary>
        /// GetCurrent
        /// </summary>
		/// <param name="domainId">domainId</param>
        /// <returns>CrosscrowdInfoEntity</returns>
        /// <remarks>2016-08-15 11:12:27</remarks>
        public CrosscrowdInfoEntity GetCurrent( System.Int32 domainId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Crowd_GetCurrent");
            
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);

            
            CrosscrowdInfoEntity obj=null;
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
        /// <returns>CrosscrowdInfoEntity列表</returns>
        /// <remarks>2016-08-15 11:12:27</remarks>
        public List<CrosscrowdInfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrosscrowdInfo_GetAll");
            

            
            List<CrosscrowdInfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  C_CrossCrowdNotSendPrize
		
		/// <summary>
        /// C_CrossCrowdNotSendPrize
        /// </summary>
        /// <returns>CrosscrowdInfoEntity列表</returns>
        /// <remarks>2016-08-15 11:12:28</remarks>
        public List<CrosscrowdInfoEntity> C_CrossCrowdNotSendPrize()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossCrowdNotSendPrize");
            

            
            List<CrosscrowdInfoEntity> list = null;
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
        /// <remarks>2016-08-15 11:12:28</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrosscrowdInfo_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            
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
		
		#region  SaveRankPrizeStatus
		
		/// <summary>
        /// SaveRankPrizeStatus
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="status">status</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-08-15 11:12:28</remarks>
        public bool SaveRankPrizeStatus ( System.Int32 idx, System.Int32 status,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Crowd_SaveRankPrizeStatus");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
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
        /// <remarks>2016-08-15 11:12:28</remarks>
        public bool Insert(CrosscrowdInfoEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:12:28</remarks>
        public bool Insert(CrosscrowdInfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrosscrowdInfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@PlayerCount", DbType.Int32, entity.PlayerCount);
			database.AddInParameter(commandWrapper, "@PairCount", DbType.Int32, entity.PairCount);
			database.AddInParameter(commandWrapper, "@PrizePoint", DbType.Int32, entity.PrizePoint);
			database.AddInParameter(commandWrapper, "@IsSendKillPrize", DbType.Boolean, entity.IsSendKillPrize);
			database.AddInParameter(commandWrapper, "@IsSendRankPrize", DbType.Boolean, entity.IsSendRankPrize);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@PrizeLegendCount", DbType.Int32, entity.PrizeLegendCount);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:12:28</remarks>
        public bool Update(CrosscrowdInfoEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:12:28</remarks>
        public bool Update(CrosscrowdInfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrosscrowdInfo_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@PlayerCount", DbType.Int32, entity.PlayerCount);
			database.AddInParameter(commandWrapper, "@PairCount", DbType.Int32, entity.PairCount);
			database.AddInParameter(commandWrapper, "@PrizePoint", DbType.Int32, entity.PrizePoint);
			database.AddInParameter(commandWrapper, "@IsSendKillPrize", DbType.Boolean, entity.IsSendKillPrize);
			database.AddInParameter(commandWrapper, "@IsSendRankPrize", DbType.Boolean, entity.IsSendRankPrize);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@PrizeLegendCount", DbType.Int32, entity.PrizeLegendCount);

            
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
