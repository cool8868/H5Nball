

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
    
    public partial class LadderInfoProvider
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
		/// 将IDataReader的当前记录读取到LadderInfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LadderInfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LadderInfoEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.AvgWaitTime = (System.Int32) reader["AvgWaitTime"];
            obj.PlayerNumber = (System.Int32) reader["PlayerNumber"];
            obj.Groups = (System.Int32) reader["Groups"];
            obj.Season = (System.Int32) reader["Season"];
            obj.StartTime = (System.DateTime) reader["StartTime"];
            obj.GroupingTime = (System.DateTime) reader["GroupingTime"];
            obj.CountdownTime = (System.DateTime) reader["CountdownTime"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<LadderInfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LadderInfoEntity>();
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
        public LadderInfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LadderInfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>LadderInfoEntity</returns>
        /// <remarks>2016/1/11 15:05:16</remarks>
        public LadderInfoEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LadderInfo_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            LadderInfoEntity obj=null;
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
        /// <returns>LadderInfoEntity列表</returns>
        /// <remarks>2016/1/11 15:05:16</remarks>
        public List<LadderInfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LadderInfo_GetAll");
            

            
            List<LadderInfoEntity> list = null;
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
        /// <remarks>2016/1/11 15:05:16</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LadderInfo_Delete");
            
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
		
		#region  ScoreToHonor
		
		/// <summary>
        /// ScoreToHonor
        /// </summary>
		/// <param name="curDate">curDate</param>
		/// <param name="curSeasonId">curSeasonId</param>
		/// <param name="isNewSeason">isNewSeason</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/11 15:05:16</remarks>
        public bool ScoreToHonor ( System.DateTime curDate, System.Int32 curSeasonId, System.Int32 isNewSeason,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("J_Ladder_ScoreToHonor");
            
			database.AddInParameter(commandWrapper, "@CurDate", DbType.DateTime, curDate);
			database.AddInParameter(commandWrapper, "@CurSeasonId", DbType.Int32, curSeasonId);
			database.AddInParameter(commandWrapper, "@IsNewSeason", DbType.Int32, isNewSeason);

            
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
		
		#region  ScoreToHonorMergeZone
		
		/// <summary>
        /// ScoreToHonorMergeZone
        /// </summary>
		/// <param name="curDate">curDate</param>
		/// <param name="curSeasonId">curSeasonId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/11 15:05:16</remarks>
        public bool ScoreToHonorMergeZone ( System.DateTime curDate, System.Int32 curSeasonId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("J_Ladder_ScoreToHonorMergeZone");
            
			database.AddInParameter(commandWrapper, "@CurDate", DbType.DateTime, curDate);
			database.AddInParameter(commandWrapper, "@CurSeasonId", DbType.Int32, curSeasonId);

            
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
        /// <remarks>2016/1/11 15:05:16</remarks>
        public bool Insert(LadderInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LadderInfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@AvgWaitTime", DbType.Int32, entity.AvgWaitTime);
			database.AddInParameter(commandWrapper, "@PlayerNumber", DbType.Int32, entity.PlayerNumber);
			database.AddInParameter(commandWrapper, "@Groups", DbType.Int32, entity.Groups);
			database.AddInParameter(commandWrapper, "@Season", DbType.Int32, entity.Season);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@GroupingTime", DbType.DateTime, entity.GroupingTime);
			database.AddInParameter(commandWrapper, "@CountdownTime", DbType.DateTime, entity.CountdownTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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
        /// <remarks>2016/1/11 15:05:16</remarks>
        public bool Update(LadderInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LadderInfo_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@AvgWaitTime", DbType.Int32, entity.AvgWaitTime);
			database.AddInParameter(commandWrapper, "@PlayerNumber", DbType.Int32, entity.PlayerNumber);
			database.AddInParameter(commandWrapper, "@Groups", DbType.Int32, entity.Groups);
			database.AddInParameter(commandWrapper, "@Season", DbType.Int32, entity.Season);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@GroupingTime", DbType.DateTime, entity.GroupingTime);
			database.AddInParameter(commandWrapper, "@CountdownTime", DbType.DateTime, entity.CountdownTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);

            
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

