﻿

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
    
    public partial class ConfigPlayerlevelProvider
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
		/// 将IDataReader的当前记录读取到ConfigPlayerlevelEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigPlayerlevelEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigPlayerlevelEntity();
			
            obj.Level = (System.Int32) reader["Level"];
            obj.Exp = (System.Int32) reader["Exp"];
            obj.PropertyCount = (System.Int32) reader["PropertyCount"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigPlayerlevelEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigPlayerlevelEntity>();
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
        public ConfigPlayerlevelProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigPlayerlevelProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="level">level</param>
        /// <returns>ConfigPlayerlevelEntity</returns>
        /// <remarks>2015/10/19 10:47:48</remarks>
        public ConfigPlayerlevelEntity GetById( System.Int32 level)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigPlayerlevel_GetById");
            
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);

            
            ConfigPlayerlevelEntity obj=null;
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
        /// <returns>ConfigPlayerlevelEntity列表</returns>
        /// <remarks>2015/10/19 10:47:48</remarks>
        public List<ConfigPlayerlevelEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigPlayerlevel_GetAll");
            

            
            List<ConfigPlayerlevelEntity> list = null;
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
		/// <param name="level">level</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 10:47:48</remarks>
        public bool Delete ( System.Int32 level,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigPlayerlevel_Delete");
            
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);

            
            
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
        /// <remarks>2015/10/19 10:47:48</remarks>
        public bool Insert(ConfigPlayerlevelEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:47:48</remarks>
        public bool Insert(ConfigPlayerlevelEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigPlayerlevel_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@Exp", DbType.Int32, entity.Exp);
			database.AddInParameter(commandWrapper, "@PropertyCount", DbType.Int32, entity.PropertyCount);

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
        /// <remarks>2015/10/19 10:47:48</remarks>
        public bool Update(ConfigPlayerlevelEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:47:48</remarks>
        public bool Update(ConfigPlayerlevelEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigPlayerlevel_Update");
            
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@Exp", DbType.Int32, entity.Exp);
			database.AddInParameter(commandWrapper, "@PropertyCount", DbType.Int32, entity.PropertyCount);

            
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

