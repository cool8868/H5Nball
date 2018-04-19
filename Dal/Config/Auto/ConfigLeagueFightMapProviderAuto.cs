

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
    
    public partial class ConfigLeaguefightmapProvider
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
		/// 将IDataReader的当前记录读取到ConfigLeaguefightmapEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigLeaguefightmapEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigLeaguefightmapEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.TeamCount = (System.Int32) reader["TeamCount"];
            obj.TemplateId = (System.Int32) reader["TemplateId"];
            obj.RoundIndex = (System.Int32) reader["RoundIndex"];
            obj.GroupIndex = (System.Int32) reader["GroupIndex"];
            obj.Team1 = (System.Int32) reader["Team1"];
            obj.Team2 = (System.Int32) reader["Team2"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigLeaguefightmapEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigLeaguefightmapEntity>();
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
        public ConfigLeaguefightmapProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigLeaguefightmapProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigLeaguefightmapEntity</returns>
        /// <remarks>2016/6/3 17:51:56</remarks>
        public ConfigLeaguefightmapEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLeaguefightmap_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigLeaguefightmapEntity obj=null;
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
        /// <returns>ConfigLeaguefightmapEntity列表</returns>
        /// <remarks>2016/6/3 17:51:56</remarks>
        public List<ConfigLeaguefightmapEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLeaguefightmap_GetAll");
            

            
            List<ConfigLeaguefightmapEntity> list = null;
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
        /// <remarks>2016/6/3 17:51:56</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLeaguefightmap_Delete");
            
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
        /// <remarks>2016/6/3 17:51:56</remarks>
        public bool Insert(ConfigLeaguefightmapEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/3 17:51:56</remarks>
        public bool Insert(ConfigLeaguefightmapEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigLeaguefightmap_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@TeamCount", DbType.Int32, entity.TeamCount);
			database.AddInParameter(commandWrapper, "@TemplateId", DbType.Int32, entity.TemplateId);
			database.AddInParameter(commandWrapper, "@RoundIndex", DbType.Int32, entity.RoundIndex);
			database.AddInParameter(commandWrapper, "@GroupIndex", DbType.Int32, entity.GroupIndex);
			database.AddInParameter(commandWrapper, "@Team1", DbType.Int32, entity.Team1);
			database.AddInParameter(commandWrapper, "@Team2", DbType.Int32, entity.Team2);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/6/3 17:51:56</remarks>
        public bool Update(ConfigLeaguefightmapEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/3 17:51:56</remarks>
        public bool Update(ConfigLeaguefightmapEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigLeaguefightmap_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@TeamCount", DbType.Int32, entity.TeamCount);
			database.AddInParameter(commandWrapper, "@TemplateId", DbType.Int32, entity.TemplateId);
			database.AddInParameter(commandWrapper, "@RoundIndex", DbType.Int32, entity.RoundIndex);
			database.AddInParameter(commandWrapper, "@GroupIndex", DbType.Int32, entity.GroupIndex);
			database.AddInParameter(commandWrapper, "@Team1", DbType.Int32, entity.Team1);
			database.AddInParameter(commandWrapper, "@Team2", DbType.Int32, entity.Team2);

            
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

