

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
    
    public partial class CrossladderMatchProvider
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
		/// 将IDataReader的当前记录读取到CrossladderMatchEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public CrossladderMatchEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new CrossladderMatchEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.DomainId = (System.Int32) reader["DomainId"];
            obj.LadderId = (System.Guid) reader["LadderId"];
            obj.HomeId = (System.Guid) reader["HomeId"];
            obj.AwayId = (System.Guid) reader["AwayId"];
            obj.HomeName = (System.String) reader["HomeName"];
            obj.AwayName = (System.String) reader["AwayName"];
            obj.HomeLogo = (System.String) reader["HomeLogo"];
            obj.AwayLogo = (System.String) reader["AwayLogo"];
            obj.HomeSiteId = (System.String) reader["HomeSiteId"];
            obj.AwaySiteId = (System.String) reader["AwaySiteId"];
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
        public List<CrossladderMatchEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<CrossladderMatchEntity>();
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
        public CrossladderMatchProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public CrossladderMatchProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>CrossladderMatchEntity</returns>
        /// <remarks>2016-08-15 11:23:04</remarks>
        public CrossladderMatchEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossladderMatch_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            CrossladderMatchEntity obj=null;
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
        /// <returns>CrossladderMatchEntity列表</returns>
        /// <remarks>2016-08-15 11:23:04</remarks>
        public List<CrossladderMatchEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossladderMatch_GetAll");
            

            
            List<CrossladderMatchEntity> list = null;
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
        /// <returns>CrossladderMatchEntity列表</returns>
        /// <remarks>2016-08-15 11:23:04</remarks>
        public List<CrossladderMatchEntity> GetFiveMatch( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossLadder_GetFiveMatch");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<CrossladderMatchEntity> list = null;
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
        /// <remarks>2016-08-15 11:23:04</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossladderMatch_Delete");
            
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
		/// <param name="domainId">domainId</param>
		/// <param name="ladderId">ladderId</param>
		/// <param name="homeId">homeId</param>
		/// <param name="awayId">awayId</param>
		/// <param name="homeName">homeName</param>
		/// <param name="awayName">awayName</param>
		/// <param name="homeLogo">homeLogo</param>
		/// <param name="awayLogo">awayLogo</param>
		/// <param name="homeSiteId">homeSiteId</param>
		/// <param name="awaySiteId">awaySiteId</param>
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
        /// <remarks>2016-08-15 11:23:04</remarks>
        public bool SaveMatch ( System.Int32 domainId, System.Guid ladderId, System.Guid homeId, System.Guid awayId, System.String homeName, System.String awayName, System.String homeLogo, System.String awayLogo, System.String homeSiteId, System.String awaySiteId, System.Int32 homeLadderScore, System.Int32 awayLadderScore, System.Int32 homeScore, System.Int32 awayScore, System.Int32 homeCoin, System.Int32 awayCoin, System.Int32 homeExp, System.Int32 awayExp, System.Boolean homeIsBot, System.Boolean awayIsBot, System.Int32 groupIndex, System.Int32 prizeHomeScore, System.Int32 prizeAwayScore, System.DateTime rowTime, System.Guid idx,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossLadder_SaveMatch");
            
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);
			database.AddInParameter(commandWrapper, "@LadderId", DbType.Guid, ladderId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, homeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, awayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, homeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, awayName);
			database.AddInParameter(commandWrapper, "@HomeLogo", DbType.String, homeLogo);
			database.AddInParameter(commandWrapper, "@AwayLogo", DbType.String, awayLogo);
			database.AddInParameter(commandWrapper, "@HomeSiteId", DbType.AnsiString, homeSiteId);
			database.AddInParameter(commandWrapper, "@AwaySiteId", DbType.AnsiString, awaySiteId);
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
		
		#region Insert
		
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:23:04</remarks>
        public bool Insert(CrossladderMatchEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:23:04</remarks>
        public bool Insert(CrossladderMatchEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrossladderMatch_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@LadderId", DbType.Guid, entity.LadderId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeLogo", DbType.AnsiString, entity.HomeLogo);
			database.AddInParameter(commandWrapper, "@AwayLogo", DbType.AnsiString, entity.AwayLogo);
			database.AddInParameter(commandWrapper, "@HomeSiteId", DbType.AnsiString, entity.HomeSiteId);
			database.AddInParameter(commandWrapper, "@AwaySiteId", DbType.AnsiString, entity.AwaySiteId);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:23:04</remarks>
        public bool Update(CrossladderMatchEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 11:23:04</remarks>
        public bool Update(CrossladderMatchEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrossladderMatch_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@LadderId", DbType.Guid, entity.LadderId);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeLogo", DbType.AnsiString, entity.HomeLogo);
			database.AddInParameter(commandWrapper, "@AwayLogo", DbType.AnsiString, entity.AwayLogo);
			database.AddInParameter(commandWrapper, "@HomeSiteId", DbType.AnsiString, entity.HomeSiteId);
			database.AddInParameter(commandWrapper, "@AwaySiteId", DbType.AnsiString, entity.AwaySiteId);
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
