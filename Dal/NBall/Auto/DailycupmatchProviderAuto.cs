

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
    
    public partial class DailycupMatchProvider
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
		/// 将IDataReader的当前记录读取到DailycupMatchEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DailycupMatchEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DailycupMatchEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.DailyCupId = (System.Int32) reader["DailyCupId"];
            obj.HomeManager = (System.Guid) reader["HomeManager"];
            obj.AwayManager = (System.Guid) reader["AwayManager"];
            obj.HomeName = (System.String) reader["HomeName"];
            obj.AwayName = (System.String) reader["AwayName"];
            obj.HomeLogo = (System.String) reader["HomeLogo"];
            obj.AwayLogo = (System.String) reader["AwayLogo"];
            obj.HomeLevel = (System.Int32) reader["HomeLevel"];
            obj.AwayLevel = (System.Int32) reader["AwayLevel"];
            obj.HomePower = (System.Int32) reader["HomePower"];
            obj.AwayPower = (System.Int32) reader["AwayPower"];
            obj.HomeWorldScore = (System.Int32) reader["HomeWorldScore"];
            obj.AwayWorldScore = (System.Int32) reader["AwayWorldScore"];
            obj.HomeScore = (System.Int32) reader["HomeScore"];
            obj.AwayScore = (System.Int32) reader["AwayScore"];
            obj.Round = (System.Int32) reader["Round"];
            obj.ChipInCount = (System.Int32) reader["ChipInCount"];
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
        public List<DailycupMatchEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DailycupMatchEntity>();
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
        public DailycupMatchProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DailycupMatchProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DailycupMatchEntity</returns>
        /// <remarks>2016/1/13 10:31:12</remarks>
        public DailycupMatchEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailycupMatch_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            DailycupMatchEntity obj=null;
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
        /// <returns>DailycupMatchEntity列表</returns>
        /// <remarks>2016/1/13 10:31:12</remarks>
        public List<DailycupMatchEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailycupMatch_GetAll");
            

            
            List<DailycupMatchEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetMatchByManager
		
		/// <summary>
        /// GetMatchByManager
        /// </summary>
        /// <returns>DailycupMatchEntity列表</returns>
        /// <remarks>2016/1/13 10:31:12</remarks>
        public List<DailycupMatchEntity> GetMatchByManager( System.Int32 dailyCupId, System.Guid managerId, System.Int32 endRound)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_GetMatchByManager");
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, dailyCupId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@endRound", DbType.Int32, endRound);

            
            List<DailycupMatchEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetMatchByRound
		
		/// <summary>
        /// GetMatchByRound
        /// </summary>
        /// <returns>DailycupMatchEntity列表</returns>
        /// <remarks>2016/1/13 10:31:12</remarks>
        public List<DailycupMatchEntity> GetMatchByRound( System.Int32 dailyCupId, System.Int32 beginRound, System.Int32 endRound)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_GetMatchByRound");
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, dailyCupId);
			database.AddInParameter(commandWrapper, "@beginRound", DbType.Int32, beginRound);
			database.AddInParameter(commandWrapper, "@endRound", DbType.Int32, endRound);

            
            List<DailycupMatchEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  UpdateMatchChipInCount
		
		/// <summary>
        /// UpdateMatchChipInCount
        /// </summary>
		/// <param name="matchId">matchId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/13 10:31:13</remarks>
        public bool UpdateMatchChipInCount ( System.Guid matchId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyCup_UpdateMatchChipInCount");
            
			database.AddInParameter(commandWrapper, "@matchId", DbType.Guid, matchId);

            
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
        /// <remarks>2016/1/13 10:31:13</remarks>
        public bool Insert(DailycupMatchEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailycupMatch_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, entity.DailyCupId);
			database.AddInParameter(commandWrapper, "@HomeManager", DbType.Guid, entity.HomeManager);
			database.AddInParameter(commandWrapper, "@AwayManager", DbType.Guid, entity.AwayManager);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeLogo", DbType.AnsiString, entity.HomeLogo);
			database.AddInParameter(commandWrapper, "@AwayLogo", DbType.AnsiString, entity.AwayLogo);
			database.AddInParameter(commandWrapper, "@HomeLevel", DbType.Int32, entity.HomeLevel);
			database.AddInParameter(commandWrapper, "@AwayLevel", DbType.Int32, entity.AwayLevel);
			database.AddInParameter(commandWrapper, "@HomePower", DbType.Int32, entity.HomePower);
			database.AddInParameter(commandWrapper, "@AwayPower", DbType.Int32, entity.AwayPower);
			database.AddInParameter(commandWrapper, "@HomeWorldScore", DbType.Int32, entity.HomeWorldScore);
			database.AddInParameter(commandWrapper, "@AwayWorldScore", DbType.Int32, entity.AwayWorldScore);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
			database.AddInParameter(commandWrapper, "@Round", DbType.Int32, entity.Round);
			database.AddInParameter(commandWrapper, "@ChipInCount", DbType.Int32, entity.ChipInCount);
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
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/13 10:31:13</remarks>
        public bool Update(DailycupMatchEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailycupMatch_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@DailyCupId", DbType.Int32, entity.DailyCupId);
			database.AddInParameter(commandWrapper, "@HomeManager", DbType.Guid, entity.HomeManager);
			database.AddInParameter(commandWrapper, "@AwayManager", DbType.Guid, entity.AwayManager);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeLogo", DbType.AnsiString, entity.HomeLogo);
			database.AddInParameter(commandWrapper, "@AwayLogo", DbType.AnsiString, entity.AwayLogo);
			database.AddInParameter(commandWrapper, "@HomeLevel", DbType.Int32, entity.HomeLevel);
			database.AddInParameter(commandWrapper, "@AwayLevel", DbType.Int32, entity.AwayLevel);
			database.AddInParameter(commandWrapper, "@HomePower", DbType.Int32, entity.HomePower);
			database.AddInParameter(commandWrapper, "@AwayPower", DbType.Int32, entity.AwayPower);
			database.AddInParameter(commandWrapper, "@HomeWorldScore", DbType.Int32, entity.HomeWorldScore);
			database.AddInParameter(commandWrapper, "@AwayWorldScore", DbType.Int32, entity.AwayWorldScore);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
			database.AddInParameter(commandWrapper, "@Round", DbType.Int32, entity.Round);
			database.AddInParameter(commandWrapper, "@ChipInCount", DbType.Int32, entity.ChipInCount);
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

