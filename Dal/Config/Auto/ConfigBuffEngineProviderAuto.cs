

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
    
    public partial class ConfigBuffengineProvider
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
		/// 将IDataReader的当前记录读取到ConfigBuffengineEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigBuffengineEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigBuffengineEntity();
			
            obj.Id = (System.Int32) reader["Id"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.BuffSrcType = (System.Int32) reader["BuffSrcType"];
            obj.BuffUnitType = (System.Int32) reader["BuffUnitType"];
            obj.LiveFlag = (System.Int32) reader["LiveFlag"];
            obj.CheckMode = (System.Int32) reader["CheckMode"];
            obj.CheckKey = (System.String) reader["CheckKey"];
            obj.CalcMode = (System.Int32) reader["CalcMode"];
            obj.SrcDir = (System.Int32) reader["SrcDir"];
            obj.SrcMode = (System.Int32) reader["SrcMode"];
            obj.SrcKey = (System.String) reader["SrcKey"];
            obj.DstDir = (System.Int32) reader["DstDir"];
            obj.DstMode = (System.Int32) reader["DstMode"];
            obj.DstKey = (System.String) reader["DstKey"];
            obj.BuffMap = (System.String) reader["BuffMap"];
            obj.BuffVal = (System.Decimal) reader["BuffVal"];
            obj.BuffPer = (System.Decimal) reader["BuffPer"];
            obj.BuffArg = (System.String) reader["BuffArg"];
            obj.Memo = (System.String) reader["Memo"];
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
        public List<ConfigBuffengineEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigBuffengineEntity>();
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
        public ConfigBuffengineProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigBuffengineProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="id">id</param>
        /// <returns>ConfigBuffengineEntity</returns>
        /// <remarks>2015/10/19 10:43:37</remarks>
        public ConfigBuffengineEntity GetById( System.Int32 id)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigBuffengine_GetById");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, id);

            
            ConfigBuffengineEntity obj=null;
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
        /// <returns>ConfigBuffengineEntity列表</returns>
        /// <remarks>2015/10/19 10:43:37</remarks>
        public List<ConfigBuffengineEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigBuffengine_GetAll");
            

            
            List<ConfigBuffengineEntity> list = null;
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
		/// <param name="id">id</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 10:43:37</remarks>
        public bool Delete ( System.Int32 id,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigBuffengine_Delete");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, id);

            
            
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
        /// <remarks>2015/10/19 10:43:37</remarks>
        public bool Insert(ConfigBuffengineEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:43:37</remarks>
        public bool Insert(ConfigBuffengineEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigBuffengine_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, entity.BuffSrcType);
			database.AddInParameter(commandWrapper, "@BuffUnitType", DbType.Int32, entity.BuffUnitType);
			database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, entity.LiveFlag);
			database.AddInParameter(commandWrapper, "@CheckMode", DbType.Int32, entity.CheckMode);
			database.AddInParameter(commandWrapper, "@CheckKey", DbType.AnsiString, entity.CheckKey);
			database.AddInParameter(commandWrapper, "@CalcMode", DbType.Int32, entity.CalcMode);
			database.AddInParameter(commandWrapper, "@SrcDir", DbType.Int32, entity.SrcDir);
			database.AddInParameter(commandWrapper, "@SrcMode", DbType.Int32, entity.SrcMode);
			database.AddInParameter(commandWrapper, "@SrcKey", DbType.AnsiString, entity.SrcKey);
			database.AddInParameter(commandWrapper, "@DstDir", DbType.Int32, entity.DstDir);
			database.AddInParameter(commandWrapper, "@DstMode", DbType.Int32, entity.DstMode);
			database.AddInParameter(commandWrapper, "@DstKey", DbType.AnsiString, entity.DstKey);
			database.AddInParameter(commandWrapper, "@BuffMap", DbType.AnsiString, entity.BuffMap);
			database.AddInParameter(commandWrapper, "@BuffVal", DbType.Currency, entity.BuffVal);
			database.AddInParameter(commandWrapper, "@BuffPer", DbType.Currency, entity.BuffPer);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.AnsiString, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@Memo", DbType.String, entity.Memo);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Id", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Id);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Id=(System.Int32)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:43:37</remarks>
        public bool Update(ConfigBuffengineEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:43:37</remarks>
        public bool Update(ConfigBuffengineEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigBuffengine_Update");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, entity.Id);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, entity.BuffSrcType);
			database.AddInParameter(commandWrapper, "@BuffUnitType", DbType.Int32, entity.BuffUnitType);
			database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, entity.LiveFlag);
			database.AddInParameter(commandWrapper, "@CheckMode", DbType.Int32, entity.CheckMode);
			database.AddInParameter(commandWrapper, "@CheckKey", DbType.AnsiString, entity.CheckKey);
			database.AddInParameter(commandWrapper, "@CalcMode", DbType.Int32, entity.CalcMode);
			database.AddInParameter(commandWrapper, "@SrcDir", DbType.Int32, entity.SrcDir);
			database.AddInParameter(commandWrapper, "@SrcMode", DbType.Int32, entity.SrcMode);
			database.AddInParameter(commandWrapper, "@SrcKey", DbType.AnsiString, entity.SrcKey);
			database.AddInParameter(commandWrapper, "@DstDir", DbType.Int32, entity.DstDir);
			database.AddInParameter(commandWrapper, "@DstMode", DbType.Int32, entity.DstMode);
			database.AddInParameter(commandWrapper, "@DstKey", DbType.AnsiString, entity.DstKey);
			database.AddInParameter(commandWrapper, "@BuffMap", DbType.AnsiString, entity.BuffMap);
			database.AddInParameter(commandWrapper, "@BuffVal", DbType.Currency, entity.BuffVal);
			database.AddInParameter(commandWrapper, "@BuffPer", DbType.Currency, entity.BuffPer);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.AnsiString, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@Memo", DbType.String, entity.Memo);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Id=(System.Int32)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

