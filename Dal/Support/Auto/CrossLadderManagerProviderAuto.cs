

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
    
    public partial class CrossladderManagerProvider
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
		/// 将IDataReader的当前记录读取到CrossladderManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public CrossladderManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new CrossladderManagerEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.DomainId = (System.Int32) reader["DomainId"];
            obj.Name = (System.String) reader["Name"];
            obj.Logo = (System.String) reader["Logo"];
            obj.Kpi = (System.Int32) reader["Kpi"];
            obj.SiteId = (System.String) reader["SiteId"];
            obj.SiteName = (System.String) reader["SiteName"];
            obj.Score = (System.Int32) reader["Score"];
            obj.NewlyScore = (System.Int32) reader["NewlyScore"];
            obj.NewlyHonor = (System.Int32) reader["NewlyHonor"];
            obj.Honor = (System.Int32) reader["Honor"];
            obj.NewlyLadderCoin = (System.Int32) reader["NewlyLadderCoin"];
            obj.LadderCoin = (System.Int32) reader["LadderCoin"];
            obj.MaxScore = (System.Int32) reader["MaxScore"];
            obj.MatchTime = (System.Int32) reader["MatchTime"];
            obj.LastExchageTime = (System.DateTime) reader["LastExchageTime"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
            obj.DailyMaxScore = (System.Int32) reader["DailyMaxScore"];
            obj.DailyMaxAddScore = (System.Int32) reader["DailyMaxAddScore"];
            obj.Stamina = (System.Int32) reader["Stamina"];
            obj.StaminaBuy = (System.Int32) reader["StaminaBuy"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<CrossladderManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<CrossladderManagerEntity>();
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
        public CrossladderManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public CrossladderManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>CrossladderManagerEntity</returns>
        /// <remarks>2016-09-13 17:03:58</remarks>
        public CrossladderManagerEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossladderManager_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            CrossladderManagerEntity obj=null;
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
        /// <returns>CrossladderManagerEntity列表</returns>
        /// <remarks>2016-09-13 17:03:58</remarks>
        public List<CrossladderManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossladderManager_GetAll");
            

            
            List<CrossladderManagerEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetBot
		
		/// <summary>
        /// GetBot
        /// </summary>
        /// <returns>CrossladderManagerEntity列表</returns>
        /// <remarks>2016-09-13 17:03:58</remarks>
        public List<CrossladderManagerEntity> GetBot( System.Int32 botNumber, System.Int32 minScore, System.Int32 maxScore)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossLadder_GetBot");
            
			database.AddInParameter(commandWrapper, "@BotNumber", DbType.Int32, botNumber);
			database.AddInParameter(commandWrapper, "@MinScore", DbType.Int32, minScore);
			database.AddInParameter(commandWrapper, "@MaxScore", DbType.Int32, maxScore);

            
            List<CrossladderManagerEntity> list = null;
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
		/// <param name="managerId">managerId</param>
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-09-13 17:03:58</remarks>
        public bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossladderManager_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);

            
            
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
		
		#region  GetOldHonor
		
		/// <summary>
        /// GetOldHonor
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="honor">honor</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-09-13 17:03:58</remarks>
        public bool GetOldHonor ( System.Guid managerId,ref  System.Int32 honor,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossLadder_GetOldHonor");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@Honor", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,honor);

            
            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }

            honor=(System.Int32)database.GetParameterValue(commandWrapper, "@Honor");
            
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
        /// <remarks>2016-09-13 17:03:58</remarks>
        public bool Insert(CrossladderManagerEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-09-13 17:03:58</remarks>
        public bool Insert(CrossladderManagerEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrossladderManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, entity.Logo);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, entity.Kpi);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@SiteName", DbType.String, entity.SiteName);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@NewlyScore", DbType.Int32, entity.NewlyScore);
			database.AddInParameter(commandWrapper, "@NewlyHonor", DbType.Int32, entity.NewlyHonor);
			database.AddInParameter(commandWrapper, "@Honor", DbType.Int32, entity.Honor);
			database.AddInParameter(commandWrapper, "@NewlyLadderCoin", DbType.Int32, entity.NewlyLadderCoin);
			database.AddInParameter(commandWrapper, "@LadderCoin", DbType.Int32, entity.LadderCoin);
			database.AddInParameter(commandWrapper, "@MaxScore", DbType.Int32, entity.MaxScore);
			database.AddInParameter(commandWrapper, "@MatchTime", DbType.Int32, entity.MatchTime);
			database.AddInParameter(commandWrapper, "@LastExchageTime", DbType.DateTime, entity.LastExchageTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@DailyMaxScore", DbType.Int32, entity.DailyMaxScore);
			database.AddInParameter(commandWrapper, "@DailyMaxAddScore", DbType.Int32, entity.DailyMaxAddScore);
			database.AddInParameter(commandWrapper, "@Stamina", DbType.Int32, entity.Stamina);
			database.AddInParameter(commandWrapper, "@StaminaBuy", DbType.Int32, entity.StaminaBuy);
			database.AddParameter(commandWrapper, "@ManagerId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.ManagerId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016-09-13 17:03:59</remarks>
        public bool Update(CrossladderManagerEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-09-13 17:03:59</remarks>
        public bool Update(CrossladderManagerEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrossladderManager_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, entity.Logo);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, entity.Kpi);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@SiteName", DbType.String, entity.SiteName);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@NewlyScore", DbType.Int32, entity.NewlyScore);
			database.AddInParameter(commandWrapper, "@NewlyHonor", DbType.Int32, entity.NewlyHonor);
			database.AddInParameter(commandWrapper, "@Honor", DbType.Int32, entity.Honor);
			database.AddInParameter(commandWrapper, "@NewlyLadderCoin", DbType.Int32, entity.NewlyLadderCoin);
			database.AddInParameter(commandWrapper, "@LadderCoin", DbType.Int32, entity.LadderCoin);
			database.AddInParameter(commandWrapper, "@MaxScore", DbType.Int32, entity.MaxScore);
			database.AddInParameter(commandWrapper, "@MatchTime", DbType.Int32, entity.MatchTime);
			database.AddInParameter(commandWrapper, "@LastExchageTime", DbType.DateTime, entity.LastExchageTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);
			database.AddInParameter(commandWrapper, "@DailyMaxScore", DbType.Int32, entity.DailyMaxScore);
			database.AddInParameter(commandWrapper, "@DailyMaxAddScore", DbType.Int32, entity.DailyMaxAddScore);
			database.AddInParameter(commandWrapper, "@Stamina", DbType.Int32, entity.Stamina);
			database.AddInParameter(commandWrapper, "@StaminaBuy", DbType.Int32, entity.StaminaBuy);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
