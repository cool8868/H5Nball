

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
    
    public partial class ConfigRevelationProvider
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
		/// 将IDataReader的当前记录读取到ConfigRevelationEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigRevelationEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigRevelationEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.MarkId = (System.Int32) reader["MarkId"];
            obj.Schedule = (System.Int32) reader["Schedule"];
            obj.NpcId = (System.Guid) reader["NpcId"];
            obj.MarkPlayerId = (System.Int32) reader["MarkPlayerId"];
            obj.MarkPlayer = (System.String) reader["MarkPlayer"];
            obj.Describe = (System.String) reader["Describe"];
            obj.TeamName = (System.String) reader["TeamName"];
            obj.OpponentTeamName = (System.String) reader["OpponentTeamName"];
            obj.Formation = (System.String) reader["Formation"];
            obj.OpponentFormation = (System.String) reader["OpponentFormation"];
            obj.PassPrizeItem = (System.String) reader["PassPrizeItem"];
            obj.FirstPassItem = (System.String) reader["FirstPassItem"];
            obj.CourageNumber = (System.Int32) reader["CourageNumber"];
            obj.Story = (System.String) reader["Story"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigRevelationEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigRevelationEntity>();
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
        public ConfigRevelationProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigRevelationProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigRevelationEntity</returns>
        /// <remarks>2017/1/11 16:24:03</remarks>
        public ConfigRevelationEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelation_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigRevelationEntity obj=null;
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
        /// <returns>ConfigRevelationEntity列表</returns>
        /// <remarks>2017/1/11 16:24:03</remarks>
        public List<ConfigRevelationEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelation_GetAll");
            

            
            List<ConfigRevelationEntity> list = null;
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
        /// <remarks>2017/1/11 16:24:03</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelation_Delete");
            
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
        /// <remarks>2017/1/11 16:24:03</remarks>
        public bool Insert(ConfigRevelationEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/1/11 16:24:03</remarks>
        public bool Insert(ConfigRevelationEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelation_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, entity.NpcId);
			database.AddInParameter(commandWrapper, "@MarkPlayerId", DbType.Int32, entity.MarkPlayerId);
			database.AddInParameter(commandWrapper, "@MarkPlayer", DbType.AnsiString, entity.MarkPlayer);
			database.AddInParameter(commandWrapper, "@Describe", DbType.String, entity.Describe);
			database.AddInParameter(commandWrapper, "@TeamName", DbType.AnsiString, entity.TeamName);
			database.AddInParameter(commandWrapper, "@OpponentTeamName", DbType.AnsiString, entity.OpponentTeamName);
			database.AddInParameter(commandWrapper, "@Formation", DbType.AnsiString, entity.Formation);
			database.AddInParameter(commandWrapper, "@OpponentFormation", DbType.AnsiString, entity.OpponentFormation);
			database.AddInParameter(commandWrapper, "@PassPrizeItem", DbType.AnsiString, entity.PassPrizeItem);
			database.AddInParameter(commandWrapper, "@FirstPassItem", DbType.AnsiString, entity.FirstPassItem);
			database.AddInParameter(commandWrapper, "@CourageNumber", DbType.Int32, entity.CourageNumber);
			database.AddInParameter(commandWrapper, "@Story", DbType.String, entity.Story);

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
        /// <remarks>2017/1/11 16:24:03</remarks>
        public bool Update(ConfigRevelationEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/1/11 16:24:03</remarks>
        public bool Update(ConfigRevelationEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelation_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, entity.NpcId);
			database.AddInParameter(commandWrapper, "@MarkPlayerId", DbType.Int32, entity.MarkPlayerId);
			database.AddInParameter(commandWrapper, "@MarkPlayer", DbType.AnsiString, entity.MarkPlayer);
			database.AddInParameter(commandWrapper, "@Describe", DbType.String, entity.Describe);
			database.AddInParameter(commandWrapper, "@TeamName", DbType.AnsiString, entity.TeamName);
			database.AddInParameter(commandWrapper, "@OpponentTeamName", DbType.AnsiString, entity.OpponentTeamName);
			database.AddInParameter(commandWrapper, "@Formation", DbType.AnsiString, entity.Formation);
			database.AddInParameter(commandWrapper, "@OpponentFormation", DbType.AnsiString, entity.OpponentFormation);
			database.AddInParameter(commandWrapper, "@PassPrizeItem", DbType.AnsiString, entity.PassPrizeItem);
			database.AddInParameter(commandWrapper, "@FirstPassItem", DbType.AnsiString, entity.FirstPassItem);
			database.AddInParameter(commandWrapper, "@CourageNumber", DbType.Int32, entity.CourageNumber);
			database.AddInParameter(commandWrapper, "@Story", DbType.String, entity.Story);

            
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
