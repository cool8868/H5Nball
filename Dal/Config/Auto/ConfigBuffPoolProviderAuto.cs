

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
    
    public partial class ConfigBuffpoolProvider
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
		/// 将IDataReader的当前记录读取到ConfigBuffpoolEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigBuffpoolEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigBuffpoolEntity();
			
            obj.Id = (System.Int32) reader["Id"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.BuffSrcType = (System.Int32) reader["BuffSrcType"];
            obj.BuffUnitType = (System.Int32) reader["BuffUnitType"];
            obj.LiveFlag = (System.Int32) reader["LiveFlag"];
            obj.BuffNo = (System.Int32) reader["BuffNo"];
            obj.DstDir = (System.Int32) reader["DstDir"];
            obj.DstMode = (System.Int32) reader["DstMode"];
            obj.DstKey = (System.String) reader["DstKey"];
            obj.BuffMap = (System.String) reader["BuffMap"];
            obj.BuffVal = (System.Decimal) reader["BuffVal"];
            obj.BuffPer = (System.Decimal) reader["BuffPer"];
            obj.BuffArg = (System.String) reader["BuffArg"];
            obj.ExpiryMinutes = (System.Int32) reader["ExpiryMinutes"];
            obj.LimitTimes = (System.Int32) reader["LimitTimes"];
            obj.TotalTimes = (System.Int32) reader["TotalTimes"];
            obj.RepeatBuffFlag = (System.Boolean) reader["RepeatBuffFlag"];
            obj.RepeatTimeFlag = (System.Boolean) reader["RepeatTimeFlag"];
            obj.RepeatTimesFlag = (System.Boolean) reader["RepeatTimesFlag"];
            obj.CoverSkillCode = (System.String) reader["CoverSkillCode"];
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
        public List<ConfigBuffpoolEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigBuffpoolEntity>();
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
        public ConfigBuffpoolProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigBuffpoolProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="id">id</param>
        /// <returns>ConfigBuffpoolEntity</returns>
        /// <remarks>2016/2/23 17:11:07</remarks>
        public ConfigBuffpoolEntity GetById( System.Int32 id)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigBuffpool_GetById");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, id);

            
            ConfigBuffpoolEntity obj=null;
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
        /// <returns>ConfigBuffpoolEntity列表</returns>
        /// <remarks>2016/2/23 17:11:07</remarks>
        public List<ConfigBuffpoolEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigBuffpool_GetAll");
            

            
            List<ConfigBuffpoolEntity> list = null;
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
        /// <remarks>2016/2/23 17:11:07</remarks>
        public bool Delete ( System.Int32 id,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigBuffpool_Delete");
            
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
        /// <remarks>2016/2/23 17:11:07</remarks>
        public bool Insert(ConfigBuffpoolEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/2/23 17:11:07</remarks>
        public bool Insert(ConfigBuffpoolEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigBuffpool_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, entity.BuffSrcType);
			database.AddInParameter(commandWrapper, "@BuffUnitType", DbType.Int32, entity.BuffUnitType);
			database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, entity.LiveFlag);
			database.AddInParameter(commandWrapper, "@BuffNo", DbType.Int32, entity.BuffNo);
			database.AddInParameter(commandWrapper, "@DstDir", DbType.Int32, entity.DstDir);
			database.AddInParameter(commandWrapper, "@DstMode", DbType.Int32, entity.DstMode);
			database.AddInParameter(commandWrapper, "@DstKey", DbType.AnsiString, entity.DstKey);
			database.AddInParameter(commandWrapper, "@BuffMap", DbType.AnsiString, entity.BuffMap);
			database.AddInParameter(commandWrapper, "@BuffVal", DbType.Currency, entity.BuffVal);
			database.AddInParameter(commandWrapper, "@BuffPer", DbType.Currency, entity.BuffPer);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.AnsiString, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@ExpiryMinutes", DbType.Int32, entity.ExpiryMinutes);
			database.AddInParameter(commandWrapper, "@LimitTimes", DbType.Int32, entity.LimitTimes);
			database.AddInParameter(commandWrapper, "@TotalTimes", DbType.Int32, entity.TotalTimes);
			database.AddInParameter(commandWrapper, "@RepeatBuffFlag", DbType.Boolean, entity.RepeatBuffFlag);
			database.AddInParameter(commandWrapper, "@RepeatTimeFlag", DbType.Boolean, entity.RepeatTimeFlag);
			database.AddInParameter(commandWrapper, "@RepeatTimesFlag", DbType.Boolean, entity.RepeatTimesFlag);
			database.AddInParameter(commandWrapper, "@CoverSkillCode", DbType.AnsiString, entity.CoverSkillCode);
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
        /// <remarks>2016/2/23 17:11:07</remarks>
        public bool Update(ConfigBuffpoolEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/2/23 17:11:07</remarks>
        public bool Update(ConfigBuffpoolEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigBuffpool_Update");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, entity.Id);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, entity.BuffSrcType);
			database.AddInParameter(commandWrapper, "@BuffUnitType", DbType.Int32, entity.BuffUnitType);
			database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, entity.LiveFlag);
			database.AddInParameter(commandWrapper, "@BuffNo", DbType.Int32, entity.BuffNo);
			database.AddInParameter(commandWrapper, "@DstDir", DbType.Int32, entity.DstDir);
			database.AddInParameter(commandWrapper, "@DstMode", DbType.Int32, entity.DstMode);
			database.AddInParameter(commandWrapper, "@DstKey", DbType.AnsiString, entity.DstKey);
			database.AddInParameter(commandWrapper, "@BuffMap", DbType.AnsiString, entity.BuffMap);
			database.AddInParameter(commandWrapper, "@BuffVal", DbType.Currency, entity.BuffVal);
			database.AddInParameter(commandWrapper, "@BuffPer", DbType.Currency, entity.BuffPer);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.AnsiString, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@ExpiryMinutes", DbType.Int32, entity.ExpiryMinutes);
			database.AddInParameter(commandWrapper, "@LimitTimes", DbType.Int32, entity.LimitTimes);
			database.AddInParameter(commandWrapper, "@TotalTimes", DbType.Int32, entity.TotalTimes);
			database.AddInParameter(commandWrapper, "@RepeatBuffFlag", DbType.Boolean, entity.RepeatBuffFlag);
			database.AddInParameter(commandWrapper, "@RepeatTimeFlag", DbType.Boolean, entity.RepeatTimeFlag);
			database.AddInParameter(commandWrapper, "@RepeatTimesFlag", DbType.Boolean, entity.RepeatTimesFlag);
			database.AddInParameter(commandWrapper, "@CoverSkillCode", DbType.AnsiString, entity.CoverSkillCode);
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

