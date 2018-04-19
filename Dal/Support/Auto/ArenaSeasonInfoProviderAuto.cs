

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
    
    public partial class ArenaSeasoninfoProvider
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
		/// 将IDataReader的当前记录读取到ArenaSeasoninfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ArenaSeasoninfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ArenaSeasoninfoEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.PrepareTime = (System.DateTime) reader["PrepareTime"];
            obj.StartTime = (System.DateTime) reader["StartTime"];
            obj.EndTime = (System.DateTime) reader["EndTime"];
            obj.ArenaType = (System.Int32) reader["ArenaType"];
            obj.Status = (System.Int32) reader["Status"];
            obj.IsPrize = (System.Boolean) reader["IsPrize"];
            obj.PrizeTime = (System.DateTime) reader["PrizeTime"];
            obj.OnChampionId = (System.Guid) reader["OnChampionId"];
            obj.OnChampionName = (System.String) reader["OnChampionName"];
            obj.OnChampionZoneName = (System.String) reader["OnChampionZoneName"];
            obj.TheKingId = (System.Guid) reader["TheKingId"];
            obj.TheKingName = (System.String) reader["TheKingName"];
            obj.TheKingZoneName = (System.String) reader["TheKingZoneName"];
            obj.TheKingChampionNumber = (System.Int32) reader["TheKingChampionNumber"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.DomainId = (System.Int32) reader["DomainId"];
            obj.SeasonId = (System.Int32) reader["SeasonId"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ArenaSeasoninfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ArenaSeasoninfoEntity>();
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
        public ArenaSeasoninfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ArenaSeasoninfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ArenaSeasoninfoEntity</returns>
        /// <remarks>2016/9/1 18:45:31</remarks>
        public ArenaSeasoninfoEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaSeasoninfo_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ArenaSeasoninfoEntity obj=null;
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
		
		#region  GetSeasonInfo
		
		/// <summary>
        /// GetSeasonInfo
        /// </summary>
		/// <param name="seasonId">seasonId</param>
		/// <param name="domainId">domainId</param>
        /// <returns>ArenaSeasoninfoEntity</returns>
        /// <remarks>2016/9/1 18:45:31</remarks>
        public ArenaSeasoninfoEntity GetSeasonInfo( System.Int32 seasonId, System.Int32 domainId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaSeason_GetSeasonInfo");
            
			database.AddInParameter(commandWrapper, "@SeasonId", DbType.Int32, seasonId);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);

            
            ArenaSeasoninfoEntity obj=null;
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
        /// <returns>ArenaSeasoninfoEntity列表</returns>
        /// <remarks>2016/9/1 18:45:31</remarks>
        public List<ArenaSeasoninfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaSeasoninfo_GetAll");
            

            
            List<ArenaSeasoninfoEntity> list = null;
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
        /// <remarks>2016/9/1 18:45:31</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaSeasoninfo_Delete");
            
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
        /// <remarks>2016/9/1 18:45:31</remarks>
        public bool Insert(ArenaSeasoninfoEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/9/1 18:45:31</remarks>
        public bool Insert(ArenaSeasoninfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ArenaSeasoninfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@PrepareTime", DbType.DateTime, entity.PrepareTime);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
			database.AddInParameter(commandWrapper, "@ArenaType", DbType.Int32, entity.ArenaType);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@IsPrize", DbType.Boolean, entity.IsPrize);
			database.AddInParameter(commandWrapper, "@PrizeTime", DbType.DateTime, entity.PrizeTime);
			database.AddInParameter(commandWrapper, "@OnChampionId", DbType.Guid, entity.OnChampionId);
			database.AddInParameter(commandWrapper, "@OnChampionName", DbType.AnsiString, entity.OnChampionName);
			database.AddInParameter(commandWrapper, "@OnChampionZoneName", DbType.AnsiString, entity.OnChampionZoneName);
			database.AddInParameter(commandWrapper, "@TheKingId", DbType.Guid, entity.TheKingId);
			database.AddInParameter(commandWrapper, "@TheKingName", DbType.AnsiString, entity.TheKingName);
			database.AddInParameter(commandWrapper, "@TheKingZoneName", DbType.AnsiString, entity.TheKingZoneName);
			database.AddInParameter(commandWrapper, "@TheKingChampionNumber", DbType.Int32, entity.TheKingChampionNumber);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@SeasonId", DbType.Int32, entity.SeasonId);
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
        /// <remarks>2016/9/1 18:45:31</remarks>
        public bool Update(ArenaSeasoninfoEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/9/1 18:45:31</remarks>
        public bool Update(ArenaSeasoninfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ArenaSeasoninfo_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@PrepareTime", DbType.DateTime, entity.PrepareTime);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
			database.AddInParameter(commandWrapper, "@ArenaType", DbType.Int32, entity.ArenaType);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@IsPrize", DbType.Boolean, entity.IsPrize);
			database.AddInParameter(commandWrapper, "@PrizeTime", DbType.DateTime, entity.PrizeTime);
			database.AddInParameter(commandWrapper, "@OnChampionId", DbType.Guid, entity.OnChampionId);
			database.AddInParameter(commandWrapper, "@OnChampionName", DbType.AnsiString, entity.OnChampionName);
			database.AddInParameter(commandWrapper, "@OnChampionZoneName", DbType.AnsiString, entity.OnChampionZoneName);
			database.AddInParameter(commandWrapper, "@TheKingId", DbType.Guid, entity.TheKingId);
			database.AddInParameter(commandWrapper, "@TheKingName", DbType.AnsiString, entity.TheKingName);
			database.AddInParameter(commandWrapper, "@TheKingZoneName", DbType.AnsiString, entity.TheKingZoneName);
			database.AddInParameter(commandWrapper, "@TheKingChampionNumber", DbType.Int32, entity.TheKingChampionNumber);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@SeasonId", DbType.Int32, entity.SeasonId);

            
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
