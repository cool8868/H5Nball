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
    
    public partial class ConfigSkilltreepointProvider
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
		/// 将IDataReader的当前记录读取到ConfigSkilltreepointEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigSkilltreepointEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigSkilltreepointEntity();
			
            obj.ManagerLevel = (System.Int32) reader["ManagerLevel"];
            obj.SumSkillPoint = (System.Int32) reader["SumSkillPoint"];
            obj.AddSkillPoint = (System.Int32) reader["AddSkillPoint"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigSkilltreepointEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigSkilltreepointEntity>();
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
        public ConfigSkilltreepointProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigSkilltreepointProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerLevel">managerLevel</param>
        /// <returns>ConfigSkilltreepointEntity</returns>
        /// <remarks>2016/5/26 20:35:15</remarks>
        public ConfigSkilltreepointEntity GetById( System.Int32 managerLevel)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSkilltreepoint_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, managerLevel);

            
            ConfigSkilltreepointEntity obj=null;
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
        /// <returns>ConfigSkilltreepointEntity列表</returns>
        /// <remarks>2016/5/26 20:35:15</remarks>
        public List<ConfigSkilltreepointEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSkilltreepoint_GetAll");
            

            
            List<ConfigSkilltreepointEntity> list = null;
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
		/// <param name="managerLevel">managerLevel</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:35:15</remarks>
        public bool Delete ( System.Int32 managerLevel,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSkilltreepoint_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, managerLevel);

            
            
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
        /// <remarks>2016/5/26 20:35:15</remarks>
        public bool Insert(ConfigSkilltreepointEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/26 20:35:15</remarks>
        public bool Insert(ConfigSkilltreepointEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigSkilltreepoint_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, entity.ManagerLevel);
			database.AddInParameter(commandWrapper, "@SumSkillPoint", DbType.Int32, entity.SumSkillPoint);
			database.AddInParameter(commandWrapper, "@AddSkillPoint", DbType.Int32, entity.AddSkillPoint);

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
        /// <remarks>2016/5/26 20:35:15</remarks>
        public bool Update(ConfigSkilltreepointEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/26 20:35:15</remarks>
        public bool Update(ConfigSkilltreepointEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigSkilltreepoint_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, entity.ManagerLevel);
			database.AddInParameter(commandWrapper, "@SumSkillPoint", DbType.Int32, entity.SumSkillPoint);
			database.AddInParameter(commandWrapper, "@AddSkillPoint", DbType.Int32, entity.AddSkillPoint);

            
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

