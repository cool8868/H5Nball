

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
    
    public partial class ConfigLeagueProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Config;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到ConfigLeagueEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigLeagueEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigLeagueEntity();
			
            obj.LeagueID = (System.Int32) reader["LeagueID"];
            obj.LeagueName = (System.String) reader["LeagueName"];
            obj.Level = (System.Int32) reader["Level"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigLeagueEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigLeagueEntity>();
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
        public ConfigLeagueProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigLeagueProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="leagueID">leagueID</param>
        /// <returns>ConfigLeagueEntity</returns>
        /// <remarks>2016/4/5 13:35:21</remarks>
        public ConfigLeagueEntity GetById( System.Int32 leagueID)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLeague_GetById");
            
			database.AddInParameter(commandWrapper, "@LeagueID", DbType.Int32, leagueID);

            
            ConfigLeagueEntity obj=null;
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
        /// <returns>ConfigLeagueEntity列表</returns>
        /// <remarks>2016/4/5 13:35:21</remarks>
        public List<ConfigLeagueEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLeague_GetAll");
            

            
            List<ConfigLeagueEntity> list = null;
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
		/// <param name="leagueID">leagueID</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/4/5 13:35:21</remarks>
        public bool Delete ( System.Int32 leagueID,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLeague_Delete");
            
			database.AddInParameter(commandWrapper, "@LeagueID", DbType.Int32, leagueID);

            
            
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
        /// <remarks>2016/4/5 13:35:21</remarks>
        public bool Insert(ConfigLeagueEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/4/5 13:35:21</remarks>
        public bool Insert(ConfigLeagueEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigLeague_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@LeagueID", DbType.Int32, entity.LeagueID);
			database.AddInParameter(commandWrapper, "@LeagueName", DbType.String, entity.LeagueName);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/4/5 13:35:21</remarks>
        public bool Update(ConfigLeagueEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/4/5 13:35:21</remarks>
        public bool Update(ConfigLeagueEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigLeague_Update");
            
			database.AddInParameter(commandWrapper, "@LeagueID", DbType.Int32, entity.LeagueID);
			database.AddInParameter(commandWrapper, "@LeagueName", DbType.String, entity.LeagueName);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

