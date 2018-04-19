

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
    
    public partial class ConfigViplevelProvider
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
		/// 将IDataReader的当前记录读取到ConfigViplevelEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigViplevelEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigViplevelEntity();
			
            obj.EffectId = (System.Int32) reader["EffectId"];
            obj.Name = (System.String) reader["Name"];
            obj.Vip0 = (System.Int32) reader["Vip0"];
            obj.Vip1 = (System.Int32) reader["Vip1"];
            obj.Vip2 = (System.Int32) reader["Vip2"];
            obj.Vip3 = (System.Int32) reader["Vip3"];
            obj.Vip4 = (System.Int32) reader["Vip4"];
            obj.Vip5 = (System.Int32) reader["Vip5"];
            obj.Vip6 = (System.Int32) reader["Vip6"];
            obj.Vip7 = (System.Int32) reader["Vip7"];
            obj.Vip8 = (System.Int32) reader["Vip8"];
            obj.Vip9 = (System.Int32) reader["Vip9"];
            obj.Vip10 = (System.Int32) reader["Vip10"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigViplevelEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigViplevelEntity>();
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
        public ConfigViplevelProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigViplevelProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="effectId">effectId</param>
        /// <returns>ConfigViplevelEntity</returns>
        /// <remarks>2015/10/18 17:33:23</remarks>
        public ConfigViplevelEntity GetById( System.Int32 effectId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigViplevel_GetById");
            
			database.AddInParameter(commandWrapper, "@EffectId", DbType.Int32, effectId);

            
            ConfigViplevelEntity obj=null;
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
        /// <returns>ConfigViplevelEntity列表</returns>
        /// <remarks>2015/10/18 17:33:23</remarks>
        public List<ConfigViplevelEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigViplevel_GetAll");
            

            
            List<ConfigViplevelEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
		/// <summary>
        /// GetAllForCache
        /// </summary>
        /// <returns>ConfigViplevelEntity列表</returns>
        /// <remarks>2015/10/18 17:33:23</remarks>
        public List<ConfigViplevelEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ConfigVipLevel_GetAllForCache");
            

            
            List<ConfigViplevelEntity> list = null;
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
		/// <param name="effectId">effectId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 17:33:23</remarks>
        public bool Delete ( System.Int32 effectId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigViplevel_Delete");
            
			database.AddInParameter(commandWrapper, "@EffectId", DbType.Int32, effectId);

            
            
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
        /// <remarks>2015/10/18 17:33:23</remarks>
        public bool Insert(ConfigViplevelEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 17:33:23</remarks>
        public bool Insert(ConfigViplevelEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigViplevel_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@EffectId", DbType.Int32, entity.EffectId);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Vip0", DbType.Int32, entity.Vip0);
			database.AddInParameter(commandWrapper, "@Vip1", DbType.Int32, entity.Vip1);
			database.AddInParameter(commandWrapper, "@Vip2", DbType.Int32, entity.Vip2);
			database.AddInParameter(commandWrapper, "@Vip3", DbType.Int32, entity.Vip3);
			database.AddInParameter(commandWrapper, "@Vip4", DbType.Int32, entity.Vip4);
			database.AddInParameter(commandWrapper, "@Vip5", DbType.Int32, entity.Vip5);
			database.AddInParameter(commandWrapper, "@Vip6", DbType.Int32, entity.Vip6);
			database.AddInParameter(commandWrapper, "@Vip7", DbType.Int32, entity.Vip7);
			database.AddInParameter(commandWrapper, "@Vip8", DbType.Int32, entity.Vip8);
			database.AddInParameter(commandWrapper, "@Vip9", DbType.Int32, entity.Vip9);
			database.AddInParameter(commandWrapper, "@Vip10", DbType.Int32, entity.Vip10);

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
        /// <remarks>2015/10/18 17:33:23</remarks>
        public bool Update(ConfigViplevelEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 17:33:23</remarks>
        public bool Update(ConfigViplevelEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigViplevel_Update");
            
			database.AddInParameter(commandWrapper, "@EffectId", DbType.Int32, entity.EffectId);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Vip0", DbType.Int32, entity.Vip0);
			database.AddInParameter(commandWrapper, "@Vip1", DbType.Int32, entity.Vip1);
			database.AddInParameter(commandWrapper, "@Vip2", DbType.Int32, entity.Vip2);
			database.AddInParameter(commandWrapper, "@Vip3", DbType.Int32, entity.Vip3);
			database.AddInParameter(commandWrapper, "@Vip4", DbType.Int32, entity.Vip4);
			database.AddInParameter(commandWrapper, "@Vip5", DbType.Int32, entity.Vip5);
			database.AddInParameter(commandWrapper, "@Vip6", DbType.Int32, entity.Vip6);
			database.AddInParameter(commandWrapper, "@Vip7", DbType.Int32, entity.Vip7);
			database.AddInParameter(commandWrapper, "@Vip8", DbType.Int32, entity.Vip8);
			database.AddInParameter(commandWrapper, "@Vip9", DbType.Int32, entity.Vip9);
			database.AddInParameter(commandWrapper, "@Vip10", DbType.Int32, entity.Vip10);

            
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

