

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
    
    public partial class ConfigCrowdrankprizeProvider
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
		/// 将IDataReader的当前记录读取到ConfigCrowdrankprizeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigCrowdrankprizeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigCrowdrankprizeEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Category = (System.Int32) reader["Category"];
            obj.CategorySub = (System.Int32) reader["CategorySub"];
            obj.Type = (System.Int32) reader["Type"];
            obj.SubType = (System.Int32) reader["SubType"];
            obj.Rate = (System.Int32) reader["Rate"];
            obj.Min = (System.Int32) reader["Min"];
            obj.Max = (System.Int32) reader["Max"];
            obj.Strength = (System.Int32) reader["Strength"];
            obj.Count = (System.Int32) reader["Count"];
            obj.IsBinding = (System.Boolean) reader["IsBinding"];
            obj.Description = (System.String) reader["Description"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigCrowdrankprizeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigCrowdrankprizeEntity>();
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
        public ConfigCrowdrankprizeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigCrowdrankprizeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigCrowdrankprizeEntity</returns>
        /// <remarks>2016-08-15 16:43:15</remarks>
        public ConfigCrowdrankprizeEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCrowdrankprize_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigCrowdrankprizeEntity obj=null;
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
        /// <returns>ConfigCrowdrankprizeEntity列表</returns>
        /// <remarks>2016-08-15 16:43:15</remarks>
        public List<ConfigCrowdrankprizeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCrowdrankprize_GetAll");
            

            
            List<ConfigCrowdrankprizeEntity> list = null;
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
        /// <remarks>2016-08-15 16:43:15</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCrowdrankprize_Delete");
            
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
        /// <remarks>2016-08-15 16:43:15</remarks>
        public bool Insert(ConfigCrowdrankprizeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 16:43:15</remarks>
        public bool Insert(ConfigCrowdrankprizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCrowdrankprize_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Category", DbType.Int32, entity.Category);
			database.AddInParameter(commandWrapper, "@CategorySub", DbType.Int32, entity.CategorySub);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@Rate", DbType.Int32, entity.Rate);
			database.AddInParameter(commandWrapper, "@Min", DbType.Int32, entity.Min);
			database.AddInParameter(commandWrapper, "@Max", DbType.Int32, entity.Max);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@Count", DbType.Int32, entity.Count);
			database.AddInParameter(commandWrapper, "@IsBinding", DbType.Boolean, entity.IsBinding);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);

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
        /// <remarks>2016-08-15 16:43:15</remarks>
        public bool Update(ConfigCrowdrankprizeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 16:43:15</remarks>
        public bool Update(ConfigCrowdrankprizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCrowdrankprize_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Category", DbType.Int32, entity.Category);
			database.AddInParameter(commandWrapper, "@CategorySub", DbType.Int32, entity.CategorySub);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@Rate", DbType.Int32, entity.Rate);
			database.AddInParameter(commandWrapper, "@Min", DbType.Int32, entity.Min);
			database.AddInParameter(commandWrapper, "@Max", DbType.Int32, entity.Max);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@Count", DbType.Int32, entity.Count);
			database.AddInParameter(commandWrapper, "@IsBinding", DbType.Boolean, entity.IsBinding);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);

            
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
