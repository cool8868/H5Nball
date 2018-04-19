

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
    
    public partial class ArenaManagerrecordProvider
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
		/// 将IDataReader的当前记录读取到ArenaManagerrecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ArenaManagerrecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ArenaManagerrecordEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ManagerName = (System.String) reader["ManagerName"];
            obj.SiteId = (System.String) reader["SiteId"];
            obj.ZoneName = (System.String) reader["ZoneName"];
            obj.Integral = (System.Int32) reader["Integral"];
            obj.DanGrading = (System.Int32) reader["DanGrading"];
            obj.ArenaType = (System.Int32) reader["ArenaType"];
            obj.SeasonId = (System.Int32) reader["SeasonId"];
            obj.Rank = (System.Int32) reader["Rank"];
            obj.IsPrize = (System.Boolean) reader["IsPrize"];
            obj.PrizeId = (System.Int32) reader["PrizeId"];
            obj.PrizeTime = (System.DateTime) reader["PrizeTime"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.DomainId = (System.Int32) reader["DomainId"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ArenaManagerrecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ArenaManagerrecordEntity>();
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
        public ArenaManagerrecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ArenaManagerrecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ArenaManagerrecordEntity</returns>
        /// <remarks>2016/9/1 13:46:00</remarks>
        public ArenaManagerrecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaManagerrecord_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ArenaManagerrecordEntity obj=null;
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
		
		#region  GetChampion
		
		/// <summary>
        /// GetChampion
        /// </summary>
		/// <param name="seasonId">seasonId</param>
		/// <param name="domainId">domainId</param>
        /// <returns>ArenaManagerrecordEntity</returns>
        /// <remarks>2016/9/1 13:46:00</remarks>
        public ArenaManagerrecordEntity GetChampion( System.Int32 seasonId, System.Int32 domainId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaManagerRecord_GetChampion");
            
			database.AddInParameter(commandWrapper, "@SeasonId", DbType.Int32, seasonId);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);

            
            ArenaManagerrecordEntity obj=null;
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
        /// <returns>ArenaManagerrecordEntity列表</returns>
        /// <remarks>2016/9/1 13:46:00</remarks>
        public List<ArenaManagerrecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaManagerrecord_GetAll");
            

            
            List<ArenaManagerrecordEntity> list = null;
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
        /// <returns>ArenaManagerrecordEntity列表</returns>
        /// <remarks>2016/9/1 13:46:00</remarks>
        public List<ArenaManagerrecordEntity> GetNotPrize()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaManagerInfo_GetNotPrize");
            

            
            List<ArenaManagerrecordEntity> list = null;
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
        /// <remarks>2016/9/1 13:46:00</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaManagerrecord_Delete");
            
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
		
		#region Insert
		
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/9/1 13:46:00</remarks>
        public bool Insert(ArenaManagerrecordEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/9/1 13:46:00</remarks>
        public bool Insert(ArenaManagerrecordEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ArenaManagerrecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.AnsiString, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.AnsiString, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@Integral", DbType.Int32, entity.Integral);
			database.AddInParameter(commandWrapper, "@DanGrading", DbType.Int32, entity.DanGrading);
			database.AddInParameter(commandWrapper, "@ArenaType", DbType.Int32, entity.ArenaType);
			database.AddInParameter(commandWrapper, "@SeasonId", DbType.Int32, entity.SeasonId);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@IsPrize", DbType.Boolean, entity.IsPrize);
			database.AddInParameter(commandWrapper, "@PrizeId", DbType.Int32, entity.PrizeId);
			database.AddInParameter(commandWrapper, "@PrizeTime", DbType.DateTime, entity.PrizeTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
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
        /// <remarks>2016/9/1 13:46:00</remarks>
        public bool Update(ArenaManagerrecordEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/9/1 13:46:00</remarks>
        public bool Update(ArenaManagerrecordEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ArenaManagerrecord_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.AnsiString, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.AnsiString, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@Integral", DbType.Int32, entity.Integral);
			database.AddInParameter(commandWrapper, "@DanGrading", DbType.Int32, entity.DanGrading);
			database.AddInParameter(commandWrapper, "@ArenaType", DbType.Int32, entity.ArenaType);
			database.AddInParameter(commandWrapper, "@SeasonId", DbType.Int32, entity.SeasonId);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@IsPrize", DbType.Boolean, entity.IsPrize);
			database.AddInParameter(commandWrapper, "@PrizeId", DbType.Int32, entity.PrizeId);
			database.AddInParameter(commandWrapper, "@PrizeTime", DbType.DateTime, entity.PrizeTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);

            
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
