

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
    
    public partial class ConfigSkillcardlevelProvider
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
		/// 将IDataReader的当前记录读取到ConfigSkillcardlevelEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigSkillcardlevelEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigSkillcardlevelEntity();
			
            obj.RowId = (System.Int32) reader["RowId"];
            obj.SkillClass = (System.Int32) reader["SkillClass"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.MinExp = (System.Int32) reader["MinExp"];
            obj.MaxExp = (System.Int32) reader["MaxExp"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigSkillcardlevelEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigSkillcardlevelEntity>();
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
        public ConfigSkillcardlevelProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigSkillcardlevelProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="rowId">rowId</param>
        /// <returns>ConfigSkillcardlevelEntity</returns>
        /// <remarks>2015/10/19 15:48:05</remarks>
        public ConfigSkillcardlevelEntity GetById( System.Int32 rowId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSkillcardlevel_GetById");
            
			database.AddInParameter(commandWrapper, "@RowId", DbType.Int32, rowId);

            
            ConfigSkillcardlevelEntity obj=null;
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
        /// <returns>ConfigSkillcardlevelEntity列表</returns>
        /// <remarks>2015/10/19 15:48:05</remarks>
        public List<ConfigSkillcardlevelEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSkillcardlevel_GetAll");
            

            
            List<ConfigSkillcardlevelEntity> list = null;
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
		/// <param name="rowId">rowId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 15:48:05</remarks>
        public bool Delete ( System.Int32 rowId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSkillcardlevel_Delete");
            
			database.AddInParameter(commandWrapper, "@RowId", DbType.Int32, rowId);

            
            
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
        /// <remarks>2015/10/19 15:48:05</remarks>
        public bool Insert(ConfigSkillcardlevelEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:48:05</remarks>
        public bool Insert(ConfigSkillcardlevelEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigSkillcardlevel_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillClass", DbType.Int32, entity.SkillClass);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@MinExp", DbType.Int32, entity.MinExp);
			database.AddInParameter(commandWrapper, "@MaxExp", DbType.Int32, entity.MaxExp);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.Date, entity.RowTime);
			database.AddParameter(commandWrapper, "@RowId", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.RowId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.RowId=(System.Int32)database.GetParameterValue(commandWrapper, "@RowId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:48:05</remarks>
        public bool Update(ConfigSkillcardlevelEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:48:05</remarks>
        public bool Update(ConfigSkillcardlevelEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigSkillcardlevel_Update");
            
			database.AddInParameter(commandWrapper, "@RowId", DbType.Int32, entity.RowId);
			database.AddInParameter(commandWrapper, "@SkillClass", DbType.Int32, entity.SkillClass);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@MinExp", DbType.Int32, entity.MinExp);
			database.AddInParameter(commandWrapper, "@MaxExp", DbType.Int32, entity.MaxExp);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.Date, entity.RowTime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.RowId=(System.Int32)database.GetParameterValue(commandWrapper, "@RowId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

