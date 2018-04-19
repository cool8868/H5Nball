

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
    
    public partial class NbManagerbuffpoolProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Main;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到NbManagerbuffpoolEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbManagerbuffpoolEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbManagerbuffpoolEntity();
			
            obj.Id = (System.Int64) reader["Id"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.BuffSrcType = (System.Int32) reader["BuffSrcType"];
            obj.BuffSrcId = (System.String) reader["BuffSrcId"];
            obj.BuffUnitType = (System.Int32) reader["BuffUnitType"];
            obj.LiveFlag = (System.Int32) reader["LiveFlag"];
            obj.BuffNo = (System.Int32) reader["BuffNo"];
            obj.DstDir = (System.Int32) reader["DstDir"];
            obj.DstMode = (System.Int32) reader["DstMode"];
            obj.DstKey = (System.String) reader["DstKey"];
            obj.BuffMap = (System.String) reader["BuffMap"];
            obj.BuffVal = (System.Decimal) reader["BuffVal"];
            obj.BuffPer = (System.Decimal) reader["BuffPer"];
            obj.ExpiryTime = (System.DateTime) reader["ExpiryTime"];
            obj.LimitTimes = (System.Int32) reader["LimitTimes"];
            obj.RemainTimes = (System.Int32) reader["RemainTimes"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<NbManagerbuffpoolEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbManagerbuffpoolEntity>();
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
        public NbManagerbuffpoolProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbManagerbuffpoolProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="id">id</param>
        /// <returns>NbManagerbuffpoolEntity</returns>
        /// <remarks>2016/2/23 17:06:02</remarks>
        public NbManagerbuffpoolEntity GetById( System.Int64 id)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagerbuffpool_GetById");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, id);

            
            NbManagerbuffpoolEntity obj=null;
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
        /// <returns>NbManagerbuffpoolEntity列表</returns>
        /// <remarks>2016/2/23 17:06:02</remarks>
        public List<NbManagerbuffpoolEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagerbuffpool_GetAll");
            

            
            List<NbManagerbuffpoolEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByMid
		
		/// <summary>
        /// GetByMid
        /// </summary>
        /// <returns>NbManagerbuffpoolEntity列表</returns>
        /// <remarks>2016/2/23 17:06:02</remarks>
        public List<NbManagerbuffpoolEntity> GetByMid( System.Guid managerId, System.Int32 managerHash)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_GetByMid");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);

            
            List<NbManagerbuffpoolEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Include
		
		/// <summary>
        /// Include
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="managerHash">managerHash</param>
		/// <param name="skillCode">skillCode</param>
		/// <param name="skillLevel">skillLevel</param>
		/// <param name="buffSrcType">buffSrcType</param>
		/// <param name="buffSrcId">buffSrcId</param>
		/// <param name="buffUnitType">buffUnitType</param>
		/// <param name="liveFlag">liveFlag</param>
		/// <param name="buffNo">buffNo</param>
		/// <param name="dstDir">dstDir</param>
		/// <param name="dstMode">dstMode</param>
		/// <param name="dstKey">dstKey</param>
		/// <param name="buffMap">buffMap</param>
		/// <param name="buffVal">buffVal</param>
		/// <param name="buffPer">buffPer</param>
		/// <param name="expiryMinutes">expiryMinutes</param>
		/// <param name="limitTimes">limitTimes</param>
		/// <param name="remainTimes">remainTimes</param>
		/// <param name="repeatBuffFlag">repeatBuffFlag</param>
		/// <param name="repeatTimeFlag">repeatTimeFlag</param>
		/// <param name="repeatTimesFlag">repeatTimesFlag</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/2/23 17:06:02</remarks>
        public bool Include ( System.Guid managerId, System.Int32 managerHash, System.String skillCode, System.Int32 skillLevel, System.Int32 buffSrcType, System.String buffSrcId, System.Int32 buffUnitType, System.Int32 liveFlag, System.Int32 buffNo, System.Int32 dstDir, System.Int32 dstMode, System.String dstKey, System.String buffMap, System.Decimal buffVal, System.Decimal buffPer, System.Int32 expiryMinutes, System.Int32 limitTimes, System.Int32 remainTimes, System.Boolean repeatBuffFlag, System.Boolean repeatTimeFlag, System.Boolean repeatTimesFlag,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_Include");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, skillLevel);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, buffSrcType);
			database.AddInParameter(commandWrapper, "@BuffSrcId", DbType.AnsiString, buffSrcId);
			database.AddInParameter(commandWrapper, "@BuffUnitType", DbType.Int32, buffUnitType);
			database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, liveFlag);
			database.AddInParameter(commandWrapper, "@BuffNo", DbType.Int32, buffNo);
			database.AddInParameter(commandWrapper, "@DstDir", DbType.Int32, dstDir);
			database.AddInParameter(commandWrapper, "@DstMode", DbType.Int32, dstMode);
			database.AddInParameter(commandWrapper, "@DstKey", DbType.AnsiString, dstKey);
			database.AddInParameter(commandWrapper, "@BuffMap", DbType.AnsiString, buffMap);
			database.AddInParameter(commandWrapper, "@BuffVal", DbType.Currency, buffVal);
			database.AddInParameter(commandWrapper, "@BuffPer", DbType.Currency, buffPer);
			database.AddInParameter(commandWrapper, "@ExpiryMinutes", DbType.Int32, expiryMinutes);
			database.AddInParameter(commandWrapper, "@LimitTimes", DbType.Int32, limitTimes);
			database.AddInParameter(commandWrapper, "@RemainTimes", DbType.Int32, remainTimes);
			database.AddInParameter(commandWrapper, "@RepeatBuffFlag", DbType.Boolean, repeatBuffFlag);
			database.AddInParameter(commandWrapper, "@RepeatTimeFlag", DbType.Boolean, repeatTimeFlag);
			database.AddInParameter(commandWrapper, "@RepeatTimesFlag", DbType.Boolean, repeatTimesFlag);

            
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
		
		#region  Exclude
		
		/// <summary>
        /// Exclude
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="managerHash">managerHash</param>
		/// <param name="buffSrcType">buffSrcType</param>
		/// <param name="buffSrcId">buffSrcId</param>
		/// <param name="skillCode">skillCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/2/23 17:06:02</remarks>
        public bool Exclude ( System.Guid managerId, System.Int32 managerHash, System.Int32 buffSrcType, System.String buffSrcId, System.String skillCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_Exclude");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, buffSrcType);
			database.AddInParameter(commandWrapper, "@BuffSrcId", DbType.AnsiString, buffSrcId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);

            
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
		
		#region  ExcludeMulti
		
		/// <summary>
        /// ExcludeMulti
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="managerHash">managerHash</param>
		/// <param name="skillCode">skillCode</param>
		/// <param name="buffNo">buffNo</param>
		/// <param name="skillCode2">skillCode2</param>
		/// <param name="buffNo2">buffNo2</param>
		/// <param name="skillCode3">skillCode3</param>
		/// <param name="buffNo3">buffNo3</param>
		/// <param name="skillCode4">skillCode4</param>
		/// <param name="buffNo4">buffNo4</param>
		/// <param name="skillCode5">skillCode5</param>
		/// <param name="buffNo5">buffNo5</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/2/23 17:06:02</remarks>
        public bool ExcludeMulti ( System.Guid managerId, System.Int32 managerHash, System.String skillCode, System.Int32 buffNo, System.String skillCode2, System.Int32 buffNo2, System.String skillCode3, System.Int32 buffNo3, System.String skillCode4, System.Int32 buffNo4, System.String skillCode5, System.Int32 buffNo5,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_ExcludeMulti");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);
			database.AddInParameter(commandWrapper, "@BuffNo", DbType.Int32, buffNo);
			database.AddInParameter(commandWrapper, "@SkillCode2", DbType.AnsiString, skillCode2);
			database.AddInParameter(commandWrapper, "@BuffNo2", DbType.Int32, buffNo2);
			database.AddInParameter(commandWrapper, "@SkillCode3", DbType.AnsiString, skillCode3);
			database.AddInParameter(commandWrapper, "@BuffNo3", DbType.Int32, buffNo3);
			database.AddInParameter(commandWrapper, "@SkillCode4", DbType.AnsiString, skillCode4);
			database.AddInParameter(commandWrapper, "@BuffNo4", DbType.Int32, buffNo4);
			database.AddInParameter(commandWrapper, "@SkillCode5", DbType.AnsiString, skillCode5);
			database.AddInParameter(commandWrapper, "@BuffNo5", DbType.Int32, buffNo5);

            
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
		
		#region  GetVersionByMid
		
		/// <summary>
        /// GetVersionByMid
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="managerHash">managerHash</param>
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/2/23 17:06:02</remarks>
        public bool GetVersionByMid ( System.Guid managerId, System.Int32 managerHash,ref  System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_GetVersionByMid");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
			database.AddParameter(commandWrapper, "@RowVersion", DbType.Binary, ParameterDirection.InputOutput,"",DataRowVersion.Current,rowVersion);

            commandWrapper.Parameters[2].Size = 8;
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            rowVersion=(System.Byte[])database.GetParameterValue(commandWrapper, "@RowVersion");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/2/23 17:06:02</remarks>
        public bool Insert(NbManagerbuffpoolEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagerbuffpool_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, entity.BuffSrcType);
			database.AddInParameter(commandWrapper, "@BuffSrcId", DbType.AnsiString, entity.BuffSrcId);
			database.AddInParameter(commandWrapper, "@BuffUnitType", DbType.Int32, entity.BuffUnitType);
			database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, entity.LiveFlag);
			database.AddInParameter(commandWrapper, "@BuffNo", DbType.Int32, entity.BuffNo);
			database.AddInParameter(commandWrapper, "@DstDir", DbType.Int32, entity.DstDir);
			database.AddInParameter(commandWrapper, "@DstMode", DbType.Int32, entity.DstMode);
			database.AddInParameter(commandWrapper, "@DstKey", DbType.AnsiString, entity.DstKey);
			database.AddInParameter(commandWrapper, "@BuffMap", DbType.AnsiString, entity.BuffMap);
			database.AddInParameter(commandWrapper, "@BuffVal", DbType.Currency, entity.BuffVal);
			database.AddInParameter(commandWrapper, "@BuffPer", DbType.Currency, entity.BuffPer);
			database.AddInParameter(commandWrapper, "@ExpiryTime", DbType.DateTime, entity.ExpiryTime);
			database.AddInParameter(commandWrapper, "@LimitTimes", DbType.Int32, entity.LimitTimes);
			database.AddInParameter(commandWrapper, "@RemainTimes", DbType.Int32, entity.RemainTimes);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Id", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Id);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Id=(System.Int64)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/2/23 17:06:02</remarks>
        public bool Update(NbManagerbuffpoolEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagerbuffpool_Update");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, entity.Id);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, entity.BuffSrcType);
			database.AddInParameter(commandWrapper, "@BuffSrcId", DbType.AnsiString, entity.BuffSrcId);
			database.AddInParameter(commandWrapper, "@BuffUnitType", DbType.Int32, entity.BuffUnitType);
			database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, entity.LiveFlag);
			database.AddInParameter(commandWrapper, "@BuffNo", DbType.Int32, entity.BuffNo);
			database.AddInParameter(commandWrapper, "@DstDir", DbType.Int32, entity.DstDir);
			database.AddInParameter(commandWrapper, "@DstMode", DbType.Int32, entity.DstMode);
			database.AddInParameter(commandWrapper, "@DstKey", DbType.AnsiString, entity.DstKey);
			database.AddInParameter(commandWrapper, "@BuffMap", DbType.AnsiString, entity.BuffMap);
			database.AddInParameter(commandWrapper, "@BuffVal", DbType.Currency, entity.BuffVal);
			database.AddInParameter(commandWrapper, "@BuffPer", DbType.Currency, entity.BuffPer);
			database.AddInParameter(commandWrapper, "@ExpiryTime", DbType.DateTime, entity.ExpiryTime);
			database.AddInParameter(commandWrapper, "@LimitTimes", DbType.Int32, entity.LimitTimes);
			database.AddInParameter(commandWrapper, "@RemainTimes", DbType.Int32, entity.RemainTimes);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Id=(System.Int64)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

