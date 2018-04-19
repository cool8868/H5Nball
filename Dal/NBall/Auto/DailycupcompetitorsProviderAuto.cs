

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
    
    public partial class DailycupCompetitorsProvider
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
		/// 将IDataReader的当前记录读取到DailycupCompetitorsEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DailycupCompetitorsEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DailycupCompetitorsEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.DailyCupId = (System.Int32) reader["DailyCupId"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ManagerName = (System.String) reader["ManagerName"];
            obj.Logo = (System.String) reader["Logo"];
            obj.MaxRound = (System.Int32) reader["MaxRound"];
            obj.WinCount = (System.Int32) reader["WinCount"];
            obj.Rank = (System.Int32) reader["Rank"];
            obj.PrizeScore = (System.Int32) reader["PrizeScore"];
            obj.PrizeSophisticate = (System.Int32) reader["PrizeSophisticate"];
            obj.PrizeCoin = (System.Int32) reader["PrizeCoin"];
            obj.PrizeItems = (System.String) reader["PrizeItems"];
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
        public List<DailycupCompetitorsEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DailycupCompetitorsEntity>();
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
        public DailycupCompetitorsProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DailycupCompetitorsProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DailycupCompetitorsEntity</returns>
        /// <remarks>2016/1/14 15:50:51</remarks>
        public DailycupCompetitorsEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailycupCompetitors_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DailycupCompetitorsEntity obj=null;
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
        /// <returns>DailycupCompetitorsEntity列表</returns>
        /// <remarks>2016/1/14 15:50:51</remarks>
        public List<DailycupCompetitorsEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailycupCompetitors_GetAll");
            

            
            List<DailycupCompetitorsEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByDailycupId
		
		/// <summary>
        /// GetByDailycupId
        /// </summary>
        /// <returns>DailycupCompetitorsEntity列表</returns>
        /// <remarks>2016/1/14 15:50:51</remarks>
        public List<DailycupCompetitorsEntity> GetByDailycupId( System.Int32 dailyCupId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCupCompetitors_GetByDailycupId");
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, dailyCupId);

            
            List<DailycupCompetitorsEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetForFight
		
		/// <summary>
        /// GetForFight
        /// </summary>
        /// <returns>DailycupCompetitorsEntity列表</returns>
        /// <remarks>2016/1/14 15:50:51</remarks>
        public List<DailycupCompetitorsEntity> GetForFight( System.Int32 dailyCupId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCupCompetitors_GetForFight");
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, dailyCupId);

            
            List<DailycupCompetitorsEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Attend
		
		/// <summary>
        /// Attend
        /// </summary>
		/// <param name="dailyCupId">dailyCupId</param>
		/// <param name="managerId">managerId</param>
		/// <param name="attendRepeatCode">attendRepeatCode</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/14 15:50:51</remarks>
        public bool Attend ( System.Int32 dailyCupId, System.Guid managerId, System.Int32 attendRepeatCode,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_Attend");
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, dailyCupId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@AttendRepeatCode", DbType.Int32, attendRepeatCode);
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
		
		#region  ExistsByManager
		
		/// <summary>
        /// ExistsByManager
        /// </summary>
		/// <param name="dailyCupId">dailyCupId</param>
		/// <param name="managerId">managerId</param>
		/// <param name="isExists">isExists</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/14 15:50:51</remarks>
        public bool ExistsByManager ( System.Int32 dailyCupId, System.Guid managerId,ref  System.Boolean isExists,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCupCompetitors_ExistsByManager");
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, dailyCupId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@IsExists", DbType.Boolean, ParameterDirection.InputOutput,"",DataRowVersion.Current,isExists);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            isExists=(System.Boolean)database.GetParameterValue(commandWrapper, "@IsExists");
            
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
        /// <remarks>2016/1/14 15:50:51</remarks>
        public bool Insert(DailycupCompetitorsEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailycupCompetitors_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, entity.DailyCupId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, entity.Logo);
			database.AddInParameter(commandWrapper, "@MaxRound", DbType.Int32, entity.MaxRound);
			database.AddInParameter(commandWrapper, "@WinCount", DbType.Int32, entity.WinCount);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@PrizeScore", DbType.Int32, entity.PrizeScore);
			database.AddInParameter(commandWrapper, "@PrizeSophisticate", DbType.Int32, entity.PrizeSophisticate);
			database.AddInParameter(commandWrapper, "@PrizeCoin", DbType.Int32, entity.PrizeCoin);
			database.AddInParameter(commandWrapper, "@PrizeItems", DbType.AnsiString, entity.PrizeItems);
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
        /// <remarks>2016/1/14 15:50:51</remarks>
        public bool Update(DailycupCompetitorsEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailycupCompetitors_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, entity.DailyCupId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, entity.Logo);
			database.AddInParameter(commandWrapper, "@MaxRound", DbType.Int32, entity.MaxRound);
			database.AddInParameter(commandWrapper, "@WinCount", DbType.Int32, entity.WinCount);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@PrizeScore", DbType.Int32, entity.PrizeScore);
			database.AddInParameter(commandWrapper, "@PrizeSophisticate", DbType.Int32, entity.PrizeSophisticate);
			database.AddInParameter(commandWrapper, "@PrizeCoin", DbType.Int32, entity.PrizeCoin);
			database.AddInParameter(commandWrapper, "@PrizeItems", DbType.AnsiString, entity.PrizeItems);
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

