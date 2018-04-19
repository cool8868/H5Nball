

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
    
    public partial class LadderMatchProvider
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
		/// 将IDataReader的当前记录读取到LadderMatchEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LadderMatchEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LadderMatchEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.LadderId = (System.Guid) reader["LadderId"];
            obj.HomeId = (System.Guid) reader["HomeId"];
            obj.AwayId = (System.Guid) reader["AwayId"];
            obj.HomeName = (System.String) reader["HomeName"];
            obj.AwayName = (System.String) reader["AwayName"];
            obj.HomeLadderScore = (System.Int32) reader["HomeLadderScore"];
            obj.AwayLadderScore = (System.Int32) reader["AwayLadderScore"];
            obj.HomeScore = (System.Int32) reader["HomeScore"];
            obj.AwayScore = (System.Int32) reader["AwayScore"];
            obj.HomeIsBot = (System.Boolean) reader["HomeIsBot"];
            obj.AwayIsBot = (System.Boolean) reader["AwayIsBot"];
            obj.GroupIndex = (System.Int32) reader["GroupIndex"];
            obj.PrizeHomeScore = (System.Int32) reader["PrizeHomeScore"];
            obj.PrizeAwayScore = (System.Int32) reader["PrizeAwayScore"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.HomeCoin = (System.Int32) reader["HomeCoin"];
            obj.HomeExp = (System.Int32) reader["HomeExp"];
            obj.AwayCoin = (System.Int32) reader["AwayCoin"];
            obj.AwayExp = (System.Int32) reader["AwayExp"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<LadderMatchEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LadderMatchEntity>();
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
        public LadderMatchProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LadderMatchProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>LadderMatchEntity</returns>
        /// <remarks>2016/3/10 18:09:15</remarks>
        public LadderMatchEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LadderMatch_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            LadderMatchEntity obj=null;
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
        /// <returns>LadderMatchEntity列表</returns>
        /// <remarks>2016/3/10 18:09:15</remarks>
        public List<LadderMatchEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LadderMatch_GetAll");
            

            
            List<LadderMatchEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetFiveMatch
		
		/// <summary>
        /// GetFiveMatch
        /// </summary>
        /// <returns>LadderMatchEntity列表</returns>
        /// <remarks>2016/3/10 18:09:15</remarks>
        public List<LadderMatchEntity> GetFiveMatch( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Ladder_GetFiveMatch");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<LadderMatchEntity> list = null;
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
        /// <remarks>2016/3/10 18:09:16</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LadderMatch_Delete");
            
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
		
		#region  SaveMatch
		
		/// <summary>
        /// SaveMatch
        /// </summary>
		/// <param name="ladderId">ladderId</param>
		/// <param name="homeId">homeId</param>
		/// <param name="awayId">awayId</param>
		/// <param name="homeName">homeName</param>
		/// <param name="awayName">awayName</param>
		/// <param name="homeLadderScore">homeLadderScore</param>
		/// <param name="awayLadderScore">awayLadderScore</param>
		/// <param name="homeScore">homeScore</param>
		/// <param name="awayScore">awayScore</param>
		/// <param name="homeCoin">homeCoin</param>
		/// <param name="awayCoin">awayCoin</param>
		/// <param name="homeExp">homeExp</param>
		/// <param name="awayExp">awayExp</param>
		/// <param name="homeIsBot">homeIsBot</param>
		/// <param name="awayIsBot">awayIsBot</param>
		/// <param name="groupIndex">groupIndex</param>
		/// <param name="prizeHomeScore">prizeHomeScore</param>
		/// <param name="prizeAwayScore">prizeAwayScore</param>
		/// <param name="rowTime">rowTime</param>
		/// <param name="idx">idx</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/3/10 18:09:16</remarks>
        public bool SaveMatch ( System.Guid ladderId, System.Guid homeId, System.Guid awayId, System.String homeName, System.String awayName, System.Int32 homeLadderScore, System.Int32 awayLadderScore, System.Int32 homeScore, System.Int32 awayScore, System.Int32 homeCoin, System.Int32 awayCoin, System.Int32 homeExp, System.Int32 awayExp, System.Boolean homeIsBot, System.Boolean awayIsBot, System.Int32 groupIndex, System.Int32 prizeHomeScore, System.Int32 prizeAwayScore, System.DateTime rowTime, System.Guid idx,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Ladder_SaveMatch");
            
			database.AddInParameter(commandWrapper, "@LadderId", DbType.Guid, ladderId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, homeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, awayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.AnsiString, homeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.AnsiString, awayName);
			database.AddInParameter(commandWrapper, "@HomeLadderScore", DbType.Int32, homeLadderScore);
			database.AddInParameter(commandWrapper, "@AwayLadderScore", DbType.Int32, awayLadderScore);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, homeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, awayScore);
			database.AddInParameter(commandWrapper, "@HomeCoin", DbType.Int32, homeCoin);
			database.AddInParameter(commandWrapper, "@AwayCoin", DbType.Int32, awayCoin);
			database.AddInParameter(commandWrapper, "@HomeExp", DbType.Int32, homeExp);
			database.AddInParameter(commandWrapper, "@AwayExp", DbType.Int32, awayExp);
			database.AddInParameter(commandWrapper, "@HomeIsBot", DbType.Boolean, homeIsBot);
			database.AddInParameter(commandWrapper, "@AwayIsBot", DbType.Boolean, awayIsBot);
			database.AddInParameter(commandWrapper, "@GroupIndex", DbType.Int32, groupIndex);
			database.AddInParameter(commandWrapper, "@PrizeHomeScore", DbType.Int32, prizeHomeScore);
			database.AddInParameter(commandWrapper, "@PrizeAwayScore", DbType.Int32, prizeAwayScore);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, rowTime);
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
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
		
		#region  GetPrizeScoreByTime
		
		/// <summary>
        /// GetPrizeScoreByTime
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="beginTime">beginTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="score">score</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/3/10 18:09:16</remarks>
        public bool GetPrizeScoreByTime ( System.Guid managerId, System.DateTime beginTime, System.DateTime endTime,ref  System.Int32 score,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Ladder_GetPrizeScoreByTime");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@BeginTime", DbType.DateTime, beginTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddParameter(commandWrapper, "@Score", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,score);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            score=(System.Int32)database.GetParameterValue(commandWrapper, "@Score");
            
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
        /// <remarks>2016/3/10 18:09:16</remarks>
        public bool Insert(LadderMatchEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LadderMatch_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@LadderId", DbType.Guid, entity.LadderId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.AnsiString, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.AnsiString, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeLadderScore", DbType.Int32, entity.HomeLadderScore);
			database.AddInParameter(commandWrapper, "@AwayLadderScore", DbType.Int32, entity.AwayLadderScore);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
			database.AddInParameter(commandWrapper, "@HomeIsBot", DbType.Boolean, entity.HomeIsBot);
			database.AddInParameter(commandWrapper, "@AwayIsBot", DbType.Boolean, entity.AwayIsBot);
			database.AddInParameter(commandWrapper, "@GroupIndex", DbType.Int32, entity.GroupIndex);
			database.AddInParameter(commandWrapper, "@PrizeHomeScore", DbType.Int32, entity.PrizeHomeScore);
			database.AddInParameter(commandWrapper, "@PrizeAwayScore", DbType.Int32, entity.PrizeAwayScore);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@HomeCoin", DbType.Int32, entity.HomeCoin);
			database.AddInParameter(commandWrapper, "@HomeExp", DbType.Int32, entity.HomeExp);
			database.AddInParameter(commandWrapper, "@AwayCoin", DbType.Int32, entity.AwayCoin);
			database.AddInParameter(commandWrapper, "@AwayExp", DbType.Int32, entity.AwayExp);
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
        /// <remarks>2016/3/10 18:09:16</remarks>
        public bool Update(LadderMatchEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LadderMatch_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@LadderId", DbType.Guid, entity.LadderId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.AnsiString, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.AnsiString, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeLadderScore", DbType.Int32, entity.HomeLadderScore);
			database.AddInParameter(commandWrapper, "@AwayLadderScore", DbType.Int32, entity.AwayLadderScore);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
			database.AddInParameter(commandWrapper, "@HomeIsBot", DbType.Boolean, entity.HomeIsBot);
			database.AddInParameter(commandWrapper, "@AwayIsBot", DbType.Boolean, entity.AwayIsBot);
			database.AddInParameter(commandWrapper, "@GroupIndex", DbType.Int32, entity.GroupIndex);
			database.AddInParameter(commandWrapper, "@PrizeHomeScore", DbType.Int32, entity.PrizeHomeScore);
			database.AddInParameter(commandWrapper, "@PrizeAwayScore", DbType.Int32, entity.PrizeAwayScore);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@HomeCoin", DbType.Int32, entity.HomeCoin);
			database.AddInParameter(commandWrapper, "@HomeExp", DbType.Int32, entity.HomeExp);
			database.AddInParameter(commandWrapper, "@AwayCoin", DbType.Int32, entity.AwayCoin);
			database.AddInParameter(commandWrapper, "@AwayExp", DbType.Int32, entity.AwayExp);

            
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

