

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
    
    public partial class ConfigLeaguemarkProvider
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
		/// 将IDataReader的当前记录读取到ConfigLeaguemarkEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigLeaguemarkEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigLeaguemarkEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.TeamId = (System.Int32) reader["TeamId"];
            obj.TeamName = (System.String) reader["TeamName"];
            obj.LeagueId = (System.Int32) reader["LeagueId"];
            obj.Buff = (System.Int32) reader["Buff"];
            obj.ManagerLevel = (System.Int32) reader["ManagerLevel"];
            obj.Formation = (System.Int32) reader["Formation"];
            obj.PlayerLevel = (System.Int32) reader["PlayerLevel"];
            obj.PlayerCardLevel = (System.Int32) reader["PlayerCardLevel"];
            obj.Coach = (System.Int32) reader["Coach"];
            obj.Talent = (System.String) reader["Talent"];
            obj.Player1Id = (System.Int32) reader["Player1Id"];
            obj.Equipment1Id = (System.Int32) reader["Equipment1Id"];
            obj.Skill1Id = (System.String) reader["Skill1Id"];
            obj.Player2Id = (System.Int32) reader["Player2Id"];
            obj.Equipment2Id = (System.Int32) reader["Equipment2Id"];
            obj.Skill2Id = (System.String) reader["Skill2Id"];
            obj.Player3Id = (System.Int32) reader["Player3Id"];
            obj.Equipment3Id = (System.Int32) reader["Equipment3Id"];
            obj.Skill3Id = (System.String) reader["Skill3Id"];
            obj.Player4Id = (System.Int32) reader["Player4Id"];
            obj.Equipment4Id = (System.Int32) reader["Equipment4Id"];
            obj.Skill4Id = (System.String) reader["Skill4Id"];
            obj.Player5Id = (System.Int32) reader["Player5Id"];
            obj.Equipment5Id = (System.Int32) reader["Equipment5Id"];
            obj.Skill5Id = (System.String) reader["Skill5Id"];
            obj.Player6Id = (System.Int32) reader["Player6Id"];
            obj.Equipment6Id = (System.Int32) reader["Equipment6Id"];
            obj.Skill6Id = (System.String) reader["Skill6Id"];
            obj.Player7Id = (System.Int32) reader["Player7Id"];
            obj.Equipment7Id = (System.Int32) reader["Equipment7Id"];
            obj.Skill7Id = (System.String) reader["Skill7Id"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigLeaguemarkEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigLeaguemarkEntity>();
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
        public ConfigLeaguemarkProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigLeaguemarkProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigLeaguemarkEntity</returns>
        /// <remarks>2016/1/18 14:06:18</remarks>
        public ConfigLeaguemarkEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLeaguemark_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            ConfigLeaguemarkEntity obj=null;
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
        /// <returns>ConfigLeaguemarkEntity列表</returns>
        /// <remarks>2016/1/18 14:06:18</remarks>
        public List<ConfigLeaguemarkEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLeaguemark_GetAll");
            

            
            List<ConfigLeaguemarkEntity> list = null;
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
        /// <remarks>2016/1/18 14:06:18</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLeaguemark_Delete");
            
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
		
		#region Insert
		
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/18 14:06:18</remarks>
        public bool Insert(ConfigLeaguemarkEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/18 14:06:18</remarks>
        public bool Insert(ConfigLeaguemarkEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigLeaguemark_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@TeamId", DbType.Int32, entity.TeamId);
			database.AddInParameter(commandWrapper, "@TeamName", DbType.String, entity.TeamName);
			database.AddInParameter(commandWrapper, "@LeagueId", DbType.Int32, entity.LeagueId);
			database.AddInParameter(commandWrapper, "@Buff", DbType.Int32, entity.Buff);
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, entity.ManagerLevel);
			database.AddInParameter(commandWrapper, "@Formation", DbType.Int32, entity.Formation);
			database.AddInParameter(commandWrapper, "@PlayerLevel", DbType.Int32, entity.PlayerLevel);
			database.AddInParameter(commandWrapper, "@PlayerCardLevel", DbType.Int32, entity.PlayerCardLevel);
			database.AddInParameter(commandWrapper, "@Coach", DbType.Int32, entity.Coach);
			database.AddInParameter(commandWrapper, "@Talent", DbType.AnsiString, entity.Talent);
			database.AddInParameter(commandWrapper, "@Player1Id", DbType.Int32, entity.Player1Id);
			database.AddInParameter(commandWrapper, "@Equipment1Id", DbType.Int32, entity.Equipment1Id);
			database.AddInParameter(commandWrapper, "@Skill1Id", DbType.AnsiString, entity.Skill1Id);
			database.AddInParameter(commandWrapper, "@Player2Id", DbType.Int32, entity.Player2Id);
			database.AddInParameter(commandWrapper, "@Equipment2Id", DbType.Int32, entity.Equipment2Id);
			database.AddInParameter(commandWrapper, "@Skill2Id", DbType.AnsiString, entity.Skill2Id);
			database.AddInParameter(commandWrapper, "@Player3Id", DbType.Int32, entity.Player3Id);
			database.AddInParameter(commandWrapper, "@Equipment3Id", DbType.Int32, entity.Equipment3Id);
			database.AddInParameter(commandWrapper, "@Skill3Id", DbType.AnsiString, entity.Skill3Id);
			database.AddInParameter(commandWrapper, "@Player4Id", DbType.Int32, entity.Player4Id);
			database.AddInParameter(commandWrapper, "@Equipment4Id", DbType.Int32, entity.Equipment4Id);
			database.AddInParameter(commandWrapper, "@Skill4Id", DbType.AnsiString, entity.Skill4Id);
			database.AddInParameter(commandWrapper, "@Player5Id", DbType.Int32, entity.Player5Id);
			database.AddInParameter(commandWrapper, "@Equipment5Id", DbType.Int32, entity.Equipment5Id);
			database.AddInParameter(commandWrapper, "@Skill5Id", DbType.AnsiString, entity.Skill5Id);
			database.AddInParameter(commandWrapper, "@Player6Id", DbType.Int32, entity.Player6Id);
			database.AddInParameter(commandWrapper, "@Equipment6Id", DbType.Int32, entity.Equipment6Id);
			database.AddInParameter(commandWrapper, "@Skill6Id", DbType.AnsiString, entity.Skill6Id);
			database.AddInParameter(commandWrapper, "@Player7Id", DbType.Int32, entity.Player7Id);
			database.AddInParameter(commandWrapper, "@Equipment7Id", DbType.Int32, entity.Equipment7Id);
			database.AddInParameter(commandWrapper, "@Skill7Id", DbType.AnsiString, entity.Skill7Id);
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
        /// <remarks>2016/1/18 14:06:18</remarks>
        public bool Update(ConfigLeaguemarkEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/18 14:06:18</remarks>
        public bool Update(ConfigLeaguemarkEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigLeaguemark_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@TeamId", DbType.Int32, entity.TeamId);
			database.AddInParameter(commandWrapper, "@TeamName", DbType.String, entity.TeamName);
			database.AddInParameter(commandWrapper, "@LeagueId", DbType.Int32, entity.LeagueId);
			database.AddInParameter(commandWrapper, "@Buff", DbType.Int32, entity.Buff);
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, entity.ManagerLevel);
			database.AddInParameter(commandWrapper, "@Formation", DbType.Int32, entity.Formation);
			database.AddInParameter(commandWrapper, "@PlayerLevel", DbType.Int32, entity.PlayerLevel);
			database.AddInParameter(commandWrapper, "@PlayerCardLevel", DbType.Int32, entity.PlayerCardLevel);
			database.AddInParameter(commandWrapper, "@Coach", DbType.Int32, entity.Coach);
			database.AddInParameter(commandWrapper, "@Talent", DbType.AnsiString, entity.Talent);
			database.AddInParameter(commandWrapper, "@Player1Id", DbType.Int32, entity.Player1Id);
			database.AddInParameter(commandWrapper, "@Equipment1Id", DbType.Int32, entity.Equipment1Id);
			database.AddInParameter(commandWrapper, "@Skill1Id", DbType.AnsiString, entity.Skill1Id);
			database.AddInParameter(commandWrapper, "@Player2Id", DbType.Int32, entity.Player2Id);
			database.AddInParameter(commandWrapper, "@Equipment2Id", DbType.Int32, entity.Equipment2Id);
			database.AddInParameter(commandWrapper, "@Skill2Id", DbType.AnsiString, entity.Skill2Id);
			database.AddInParameter(commandWrapper, "@Player3Id", DbType.Int32, entity.Player3Id);
			database.AddInParameter(commandWrapper, "@Equipment3Id", DbType.Int32, entity.Equipment3Id);
			database.AddInParameter(commandWrapper, "@Skill3Id", DbType.AnsiString, entity.Skill3Id);
			database.AddInParameter(commandWrapper, "@Player4Id", DbType.Int32, entity.Player4Id);
			database.AddInParameter(commandWrapper, "@Equipment4Id", DbType.Int32, entity.Equipment4Id);
			database.AddInParameter(commandWrapper, "@Skill4Id", DbType.AnsiString, entity.Skill4Id);
			database.AddInParameter(commandWrapper, "@Player5Id", DbType.Int32, entity.Player5Id);
			database.AddInParameter(commandWrapper, "@Equipment5Id", DbType.Int32, entity.Equipment5Id);
			database.AddInParameter(commandWrapper, "@Skill5Id", DbType.AnsiString, entity.Skill5Id);
			database.AddInParameter(commandWrapper, "@Player6Id", DbType.Int32, entity.Player6Id);
			database.AddInParameter(commandWrapper, "@Equipment6Id", DbType.Int32, entity.Equipment6Id);
			database.AddInParameter(commandWrapper, "@Skill6Id", DbType.AnsiString, entity.Skill6Id);
			database.AddInParameter(commandWrapper, "@Player7Id", DbType.Int32, entity.Player7Id);
			database.AddInParameter(commandWrapper, "@Equipment7Id", DbType.Int32, entity.Equipment7Id);
			database.AddInParameter(commandWrapper, "@Skill7Id", DbType.AnsiString, entity.Skill7Id);

            
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

