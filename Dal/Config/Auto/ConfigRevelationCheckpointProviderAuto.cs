

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
    
    public partial class ConfigRevelationcheckpointProvider
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
		/// 将IDataReader的当前记录读取到ConfigRevelationcheckpointEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigRevelationcheckpointEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigRevelationcheckpointEntity();
			
            obj.Mark = (System.Int32) reader["Mark"];
            obj.SmallClearance = (System.Int32) reader["SmallClearance"];
            obj.CheckpointPlayers = (System.String) reader["CheckpointPlayers"];
            obj.Describe = (System.String) reader["Describe"];
            obj.TheStory = (System.String) reader["TheStory"];
            obj.Team = (System.String) reader["Team"];
            obj.AgainstTheTeam = (System.String) reader["AgainstTheTeam"];
            obj.Formation = (System.Int32) reader["Formation"];
            obj.TheGoalkeeperID = (System.Int32) reader["TheGoalkeeperID"];
            obj.TheGoalkeeperName = (System.String) reader["TheGoalkeeperName"];
            obj.PlayersID1 = (System.Int32) reader["PlayersID1"];
            obj.PlayersName1 = (System.String) reader["PlayersName1"];
            obj.PlayersID2 = (System.Int32) reader["PlayersID2"];
            obj.PlayersName2 = (System.String) reader["PlayersName2"];
            obj.PlayersID3 = (System.Int32) reader["PlayersID3"];
            obj.PlayersName3 = (System.String) reader["PlayersName3"];
            obj.PlayersID4 = (System.Int32) reader["PlayersID4"];
            obj.PlayersName4 = (System.String) reader["PlayersName4"];
            obj.PlayersID5 = (System.Int32) reader["PlayersID5"];
            obj.PlayersName5 = (System.String) reader["PlayersName5"];
            obj.PlayersID6 = (System.Int32) reader["PlayersID6"];
            obj.PlayersName6 = (System.String) reader["PlayersName6"];
            obj.PlayersID7 = (System.Int32) reader["PlayersID7"];
            obj.PlayersName7 = (System.String) reader["PlayersName7"];
            obj.PlayersID8 = (System.Int32) reader["PlayersID8"];
            obj.PlayersName8 = (System.String) reader["PlayersName8"];
            obj.PlayersID9 = (System.Int32) reader["PlayersID9"];
            obj.PlayersName9 = (System.String) reader["PlayersName9"];
            obj.PlayersID10 = (System.Int32) reader["PlayersID10"];
            obj.PlayersName10 = (System.String) reader["PlayersName10"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigRevelationcheckpointEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigRevelationcheckpointEntity>();
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
        public ConfigRevelationcheckpointProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigRevelationcheckpointProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="mark">mark</param>
		/// <param name="smallClearance">smallClearance</param>
        /// <returns>ConfigRevelationcheckpointEntity</returns>
        /// <remarks>2014/10/31 16:41:29</remarks>
        public ConfigRevelationcheckpointEntity GetById( System.Int32 mark, System.Int32 smallClearance)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationcheckpoint_GetById");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, mark);
			database.AddInParameter(commandWrapper, "@SmallClearance", DbType.Int32, smallClearance);

            
            ConfigRevelationcheckpointEntity obj=null;
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
        /// <returns>ConfigRevelationcheckpointEntity列表</returns>
        /// <remarks>2014/10/31 16:41:29</remarks>
        public List<ConfigRevelationcheckpointEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationcheckpoint_GetAll");
            

            
            List<ConfigRevelationcheckpointEntity> list = null;
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
		/// <param name="mark">mark</param>
		/// <param name="smallClearance">smallClearance</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/10/31 16:41:29</remarks>
        public bool Delete ( System.Int32 mark, System.Int32 smallClearance,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationcheckpoint_Delete");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, mark);
			database.AddInParameter(commandWrapper, "@SmallClearance", DbType.Int32, smallClearance);

            
            
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
        /// <remarks>2014/10/31 16:41:29</remarks>
        public bool Insert(ConfigRevelationcheckpointEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/10/31 16:41:29</remarks>
        public bool Insert(ConfigRevelationcheckpointEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelationcheckpoint_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@SmallClearance", DbType.Int32, entity.SmallClearance);
			database.AddInParameter(commandWrapper, "@CheckpointPlayers", DbType.String, entity.CheckpointPlayers);
			database.AddInParameter(commandWrapper, "@Describe", DbType.String, entity.Describe);
			database.AddInParameter(commandWrapper, "@TheStory", DbType.String, entity.TheStory);
			database.AddInParameter(commandWrapper, "@Team", DbType.String, entity.Team);
			database.AddInParameter(commandWrapper, "@AgainstTheTeam", DbType.String, entity.AgainstTheTeam);
			database.AddInParameter(commandWrapper, "@Formation", DbType.Int32, entity.Formation);
			database.AddInParameter(commandWrapper, "@TheGoalkeeperID", DbType.Int32, entity.TheGoalkeeperID);
			database.AddInParameter(commandWrapper, "@TheGoalkeeperName", DbType.String, entity.TheGoalkeeperName);
			database.AddInParameter(commandWrapper, "@PlayersID1", DbType.Int32, entity.PlayersID1);
			database.AddInParameter(commandWrapper, "@PlayersName1", DbType.String, entity.PlayersName1);
			database.AddInParameter(commandWrapper, "@PlayersID2", DbType.Int32, entity.PlayersID2);
			database.AddInParameter(commandWrapper, "@PlayersName2", DbType.String, entity.PlayersName2);
			database.AddInParameter(commandWrapper, "@PlayersID3", DbType.Int32, entity.PlayersID3);
			database.AddInParameter(commandWrapper, "@PlayersName3", DbType.String, entity.PlayersName3);
			database.AddInParameter(commandWrapper, "@PlayersID4", DbType.Int32, entity.PlayersID4);
			database.AddInParameter(commandWrapper, "@PlayersName4", DbType.String, entity.PlayersName4);
			database.AddInParameter(commandWrapper, "@PlayersID5", DbType.Int32, entity.PlayersID5);
			database.AddInParameter(commandWrapper, "@PlayersName5", DbType.String, entity.PlayersName5);
			database.AddInParameter(commandWrapper, "@PlayersID6", DbType.Int32, entity.PlayersID6);
			database.AddInParameter(commandWrapper, "@PlayersName6", DbType.String, entity.PlayersName6);
			database.AddInParameter(commandWrapper, "@PlayersID7", DbType.Int32, entity.PlayersID7);
			database.AddInParameter(commandWrapper, "@PlayersName7", DbType.String, entity.PlayersName7);
			database.AddInParameter(commandWrapper, "@PlayersID8", DbType.Int32, entity.PlayersID8);
			database.AddInParameter(commandWrapper, "@PlayersName8", DbType.String, entity.PlayersName8);
			database.AddInParameter(commandWrapper, "@PlayersID9", DbType.Int32, entity.PlayersID9);
			database.AddInParameter(commandWrapper, "@PlayersName9", DbType.String, entity.PlayersName9);
			database.AddInParameter(commandWrapper, "@PlayersID10", DbType.Int32, entity.PlayersID10);
			database.AddInParameter(commandWrapper, "@PlayersName10", DbType.String, entity.PlayersName10);

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
        /// <remarks>2014/10/31 16:41:29</remarks>
        public bool Update(ConfigRevelationcheckpointEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/10/31 16:41:29</remarks>
        public bool Update(ConfigRevelationcheckpointEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelationcheckpoint_Update");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@SmallClearance", DbType.Int32, entity.SmallClearance);
			database.AddInParameter(commandWrapper, "@CheckpointPlayers", DbType.String, entity.CheckpointPlayers);
			database.AddInParameter(commandWrapper, "@Describe", DbType.String, entity.Describe);
			database.AddInParameter(commandWrapper, "@TheStory", DbType.String, entity.TheStory);
			database.AddInParameter(commandWrapper, "@Team", DbType.String, entity.Team);
			database.AddInParameter(commandWrapper, "@AgainstTheTeam", DbType.String, entity.AgainstTheTeam);
			database.AddInParameter(commandWrapper, "@Formation", DbType.Int32, entity.Formation);
			database.AddInParameter(commandWrapper, "@TheGoalkeeperID", DbType.Int32, entity.TheGoalkeeperID);
			database.AddInParameter(commandWrapper, "@TheGoalkeeperName", DbType.String, entity.TheGoalkeeperName);
			database.AddInParameter(commandWrapper, "@PlayersID1", DbType.Int32, entity.PlayersID1);
			database.AddInParameter(commandWrapper, "@PlayersName1", DbType.String, entity.PlayersName1);
			database.AddInParameter(commandWrapper, "@PlayersID2", DbType.Int32, entity.PlayersID2);
			database.AddInParameter(commandWrapper, "@PlayersName2", DbType.String, entity.PlayersName2);
			database.AddInParameter(commandWrapper, "@PlayersID3", DbType.Int32, entity.PlayersID3);
			database.AddInParameter(commandWrapper, "@PlayersName3", DbType.String, entity.PlayersName3);
			database.AddInParameter(commandWrapper, "@PlayersID4", DbType.Int32, entity.PlayersID4);
			database.AddInParameter(commandWrapper, "@PlayersName4", DbType.String, entity.PlayersName4);
			database.AddInParameter(commandWrapper, "@PlayersID5", DbType.Int32, entity.PlayersID5);
			database.AddInParameter(commandWrapper, "@PlayersName5", DbType.String, entity.PlayersName5);
			database.AddInParameter(commandWrapper, "@PlayersID6", DbType.Int32, entity.PlayersID6);
			database.AddInParameter(commandWrapper, "@PlayersName6", DbType.String, entity.PlayersName6);
			database.AddInParameter(commandWrapper, "@PlayersID7", DbType.Int32, entity.PlayersID7);
			database.AddInParameter(commandWrapper, "@PlayersName7", DbType.String, entity.PlayersName7);
			database.AddInParameter(commandWrapper, "@PlayersID8", DbType.Int32, entity.PlayersID8);
			database.AddInParameter(commandWrapper, "@PlayersName8", DbType.String, entity.PlayersName8);
			database.AddInParameter(commandWrapper, "@PlayersID9", DbType.Int32, entity.PlayersID9);
			database.AddInParameter(commandWrapper, "@PlayersName9", DbType.String, entity.PlayersName9);
			database.AddInParameter(commandWrapper, "@PlayersID10", DbType.Int32, entity.PlayersID10);
			database.AddInParameter(commandWrapper, "@PlayersName10", DbType.String, entity.PlayersName10);

            
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

