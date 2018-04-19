

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
    
    public partial class ManagerskillUseProvider
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
		/// 将IDataReader的当前记录读取到ManagerskillUseEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ManagerskillUseEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ManagerskillUseEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.SyncFlag = (System.Int32) reader["SyncFlag"];
            obj.PlayerSkills = (System.String) reader["PlayerSkills"];
            obj.ManagerSkills = (System.String) reader["ManagerSkills"];
            obj.CoachSkill = (System.String) reader["CoachSkill"];
            obj.Talents = (System.String) reader["Talents"];
            obj.Wills = (System.String) reader["Wills"];
            obj.Combs = (System.String) reader["Combs"];
            obj.Suits = (System.String) reader["Suits"];
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
        public List<ManagerskillUseEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ManagerskillUseEntity>();
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
        public ManagerskillUseProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ManagerskillUseProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>ManagerskillUseEntity</returns>
        /// <remarks>2015/10/19 17:26:54</remarks>
        public ManagerskillUseEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerskillUse_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            ManagerskillUseEntity obj=null;
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
        /// <returns>ManagerskillUseEntity列表</returns>
        /// <remarks>2015/10/19 17:26:54</remarks>
        public List<ManagerskillUseEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerskillUse_GetAll");
            

            
            List<ManagerskillUseEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  HitTalent
		
		/// <summary>
        /// HitTalent
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="syncTalentPoint">syncTalentPoint</param>
		/// <param name="maxTalentPoint">maxTalentPoint</param>
		/// <param name="todoFlag">todoFlag</param>
		/// <param name="talents">talents</param>
		/// <param name="libRowVersion">libRowVersion</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:26:54</remarks>
        public bool HitTalent ( System.Guid managerId, System.Int32 syncTalentPoint, System.Int32 maxTalentPoint, System.Boolean todoFlag, System.String talents, System.Byte[] libRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerSkill_HitTalent");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SyncTalentPoint", DbType.Int32, syncTalentPoint);
			database.AddInParameter(commandWrapper, "@MaxTalentPoint", DbType.Int32, maxTalentPoint);
			database.AddInParameter(commandWrapper, "@TodoFlag", DbType.Boolean, todoFlag);
			database.AddInParameter(commandWrapper, "@Talents", DbType.AnsiString, talents);
			database.AddInParameter(commandWrapper, "@LibRowVersion", DbType.Binary, libRowVersion);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SetTalent
		
		/// <summary>
        /// SetTalent
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="talents">talents</param>
		/// <param name="useRowVersion">useRowVersion</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:26:54</remarks>
        public bool SetTalent ( System.Guid managerId, System.String talents, System.Byte[] useRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerSkill_SetTalent");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Talents", DbType.AnsiString, talents);
			database.AddInParameter(commandWrapper, "@UseRowVersion", DbType.Binary, useRowVersion);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ResetTalent
		
		/// <summary>
        /// ResetTalent
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="managerHash">managerHash</param>
		/// <param name="account">account</param>
		/// <param name="costGold">costGold</param>
		/// <param name="costGoldItemNo">costGoldItemNo</param>
		/// <param name="costGoldOrderId">costGoldOrderId</param>
		/// <param name="costCoin">costCoin</param>
		/// <param name="costRowVersion">costRowVersion</param>
		/// <param name="poolBuffType">poolBuffType</param>
		/// <param name="useRowVersion">useRowVersion</param>
		/// <param name="libRowVersion">libRowVersion</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:26:54</remarks>
        public bool ResetTalent ( System.Guid managerId, System.Int32 managerHash, System.String account, System.Int32 costGold, System.Int32 costGoldItemNo, System.Guid costGoldOrderId, System.Int32 costCoin, System.Byte[] costRowVersion, System.Int32 poolBuffType, System.Byte[] useRowVersion, System.Byte[] libRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerSkill_ResetTalent");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@CostGold", DbType.Int32, costGold);
			database.AddInParameter(commandWrapper, "@CostGoldItemNo", DbType.Int32, costGoldItemNo);
			database.AddInParameter(commandWrapper, "@CostGoldOrderId", DbType.Guid, costGoldOrderId);
			database.AddInParameter(commandWrapper, "@CostCoin", DbType.Int32, costCoin);
			database.AddInParameter(commandWrapper, "@CostRowVersion", DbType.Binary, costRowVersion);
			database.AddInParameter(commandWrapper, "@PoolBuffType", DbType.Int32, poolBuffType);
			database.AddInParameter(commandWrapper, "@UseRowVersion", DbType.Binary, useRowVersion);
			database.AddInParameter(commandWrapper, "@LibRowVersion", DbType.Binary, libRowVersion);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  PutWill
		
		/// <summary>
        /// PutWill
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="willCode">willCode</param>
		/// <param name="putMap">putMap</param>
		/// <param name="enableFlag">enableFlag</param>
		/// <param name="srcRowVersion">srcRowVersion</param>
		/// <param name="maxWillNumber">maxWillNumber</param>
		/// <param name="todoFlag">todoFlag</param>
		/// <param name="useWills">useWills</param>
		/// <param name="libWills">libWills</param>
		/// <param name="libRowVersion">libRowVersion</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:26:54</remarks>
        public bool PutWill ( System.Guid managerId, System.String willCode, System.String putMap, System.Int32 enableFlag, System.Byte[] srcRowVersion, System.Int32 maxWillNumber, System.Boolean todoFlag, System.String useWills, System.String libWills, System.Byte[] libRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerSkill_PutWill");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@WillCode", DbType.AnsiString, willCode);
			database.AddInParameter(commandWrapper, "@PutMap", DbType.AnsiString, putMap);
			database.AddInParameter(commandWrapper, "@EnableFlag", DbType.Int32, enableFlag);
			database.AddInParameter(commandWrapper, "@SrcRowVersion", DbType.Binary, srcRowVersion);
			database.AddInParameter(commandWrapper, "@MaxWillNumber", DbType.Int32, maxWillNumber);
			database.AddInParameter(commandWrapper, "@TodoFlag", DbType.Boolean, todoFlag);
			database.AddInParameter(commandWrapper, "@UseWills", DbType.AnsiString, useWills);
			database.AddInParameter(commandWrapper, "@LibWills", DbType.AnsiString, libWills);
			database.AddInParameter(commandWrapper, "@LibRowVersion", DbType.Binary, libRowVersion);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SetWill
		
		/// <summary>
        /// SetWill
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="wills">wills</param>
		/// <param name="useRowVersion">useRowVersion</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:26:54</remarks>
        public bool SetWill ( System.Guid managerId, System.String wills, System.Byte[] useRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerSkill_SetWill");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Wills", DbType.AnsiString, wills);
			database.AddInParameter(commandWrapper, "@UseRowVersion", DbType.Binary, useRowVersion);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
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
        /// <remarks>2015/10/19 17:26:54</remarks>
        public bool Insert(ManagerskillUseEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ManagerskillUse_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SyncFlag", DbType.Int32, entity.SyncFlag);
			database.AddInParameter(commandWrapper, "@PlayerSkills", DbType.AnsiString, entity.PlayerSkills);
			database.AddInParameter(commandWrapper, "@ManagerSkills", DbType.AnsiString, entity.ManagerSkills);
			database.AddInParameter(commandWrapper, "@CoachSkill", DbType.AnsiString, entity.CoachSkill);
			database.AddInParameter(commandWrapper, "@Talents", DbType.AnsiString, entity.Talents);
			database.AddInParameter(commandWrapper, "@Wills", DbType.AnsiString, entity.Wills);
			database.AddInParameter(commandWrapper, "@Combs", DbType.AnsiString, entity.Combs);
			database.AddInParameter(commandWrapper, "@Suits", DbType.AnsiString, entity.Suits);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@ManagerId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.ManagerId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
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
        /// <remarks>2015/10/19 17:26:54</remarks>
        public bool Update(ManagerskillUseEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ManagerskillUse_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@SyncFlag", DbType.Int32, entity.SyncFlag);
			database.AddInParameter(commandWrapper, "@PlayerSkills", DbType.AnsiString, entity.PlayerSkills);
			database.AddInParameter(commandWrapper, "@ManagerSkills", DbType.AnsiString, entity.ManagerSkills);
			database.AddInParameter(commandWrapper, "@CoachSkill", DbType.AnsiString, entity.CoachSkill);
			database.AddInParameter(commandWrapper, "@Talents", DbType.AnsiString, entity.Talents);
			database.AddInParameter(commandWrapper, "@Wills", DbType.AnsiString, entity.Wills);
			database.AddInParameter(commandWrapper, "@Combs", DbType.AnsiString, entity.Combs);
			database.AddInParameter(commandWrapper, "@Suits", DbType.AnsiString, entity.Suits);
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

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

