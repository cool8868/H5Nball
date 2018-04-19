

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
    
    public partial class ConfigRevelationvipProvider
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
		/// 将IDataReader的当前记录读取到ConfigRevelationvipEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigRevelationvipEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigRevelationvipEntity();
			
            obj.VipLevel = (System.Int32) reader["VipLevel"];
            obj.Challenges = (System.Int32) reader["Challenges"];
            obj.CDTime = (System.Int32) reader["CDTime"];
            obj.ItemIsBind = (System.Boolean) reader["ItemIsBind"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigRevelationvipEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigRevelationvipEntity>();
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
        public ConfigRevelationvipProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigRevelationvipProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="vipLevel">vipLevel</param>
        /// <returns>ConfigRevelationvipEntity</returns>
        /// <remarks>2014/10/21 18:01:47</remarks>
        public ConfigRevelationvipEntity GetById( System.Int32 vipLevel)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationvip_GetById");
            
			database.AddInParameter(commandWrapper, "@VipLevel", DbType.Int32, vipLevel);

            
            ConfigRevelationvipEntity obj=null;
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
        /// <returns>ConfigRevelationvipEntity列表</returns>
        /// <remarks>2014/10/21 18:01:47</remarks>
        public List<ConfigRevelationvipEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationvip_GetAll");
            

            
            List<ConfigRevelationvipEntity> list = null;
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
		/// <param name="vipLevel">vipLevel</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/10/21 18:01:47</remarks>
        public bool Delete ( System.Int32 vipLevel,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationvip_Delete");
            
			database.AddInParameter(commandWrapper, "@VipLevel", DbType.Int32, vipLevel);

            
            
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
        /// <remarks>2014/10/21 18:01:47</remarks>
        public bool Insert(ConfigRevelationvipEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/10/21 18:01:47</remarks>
        public bool Insert(ConfigRevelationvipEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelationvip_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@VipLevel", DbType.Int32, entity.VipLevel);
			database.AddInParameter(commandWrapper, "@Challenges", DbType.Int32, entity.Challenges);
			database.AddInParameter(commandWrapper, "@CDTime", DbType.Int32, entity.CDTime);
			database.AddInParameter(commandWrapper, "@ItemIsBind", DbType.Boolean, entity.ItemIsBind);

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
        /// <remarks>2014/10/21 18:01:47</remarks>
        public bool Update(ConfigRevelationvipEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/10/21 18:01:47</remarks>
        public bool Update(ConfigRevelationvipEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelationvip_Update");
            
			database.AddInParameter(commandWrapper, "@VipLevel", DbType.Int32, entity.VipLevel);
			database.AddInParameter(commandWrapper, "@Challenges", DbType.Int32, entity.Challenges);
			database.AddInParameter(commandWrapper, "@CDTime", DbType.Int32, entity.CDTime);
			database.AddInParameter(commandWrapper, "@ItemIsBind", DbType.Boolean, entity.ItemIsBind);

            
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

