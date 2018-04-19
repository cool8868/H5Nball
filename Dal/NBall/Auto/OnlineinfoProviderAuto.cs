

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
    
    public partial class OnlineInfoProvider
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
		/// 将IDataReader的当前记录读取到OnlineInfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public OnlineInfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new OnlineInfoEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ActiveFlag = (System.Boolean) reader["ActiveFlag"];
            obj.ResetFlag = (System.Boolean) reader["ResetFlag"];
            obj.LoginTime = (System.DateTime) reader["LoginTime"];
            obj.GuildInTime = (System.DateTime) reader["GuildInTime"];
            obj.ActiveTime = (System.DateTime) reader["ActiveTime"];
            obj.TotalOnlineMinutes = (System.Int64) reader["TotalOnlineMinutes"];
            obj.CntOnlineMinutes = (System.Int32) reader["CntOnlineMinutes"];
            obj.CurOnlineMinutes = (System.Int32) reader["CurOnlineMinutes"];
            obj.LoginIp = (System.String) reader["LoginIp"];
            obj.Status = (System.Int32) reader["Status"];
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
        public List<OnlineInfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<OnlineInfoEntity>();
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
        public OnlineInfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public OnlineInfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>OnlineInfoEntity</returns>
        /// <remarks>2015/10/18 16:53:07</remarks>
        public OnlineInfoEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_OnlineInfo_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            OnlineInfoEntity obj=null;
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
        /// <returns>OnlineInfoEntity列表</returns>
        /// <remarks>2015/10/18 16:53:07</remarks>
        public List<OnlineInfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_OnlineInfo_GetAll");
            

            
            List<OnlineInfoEntity> list = null;
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
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 16:53:08</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_OnlineInfo_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
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
		
		#region  Login
		
		/// <summary>
        /// Login
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="loginIp">loginIp</param>
		/// <param name="today">today</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 16:53:08</remarks>
        public bool Login ( System.Guid managerId, System.String loginIp, System.DateTime today,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Online_Login");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@LoginIp", DbType.AnsiString, loginIp);
			database.AddInParameter(commandWrapper, "@Today", DbType.DateTime, today);

            
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
		
		#region  GetByManagerId
		
		/// <summary>
        /// GetByManagerId
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="activeFlag">activeFlag</param>
		/// <param name="loginTime">loginTime</param>
		/// <param name="activeTime">activeTime</param>
		/// <param name="onlineMinutes">onlineMinutes</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 16:53:08</remarks>
        public bool GetByManagerId ( System.Guid managerId,ref  System.Boolean activeFlag,ref  System.DateTime loginTime,ref  System.DateTime activeTime,ref  System.Int32 onlineMinutes,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Online_GetByManagerId");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@ActiveFlag", DbType.Boolean, ParameterDirection.InputOutput,"",DataRowVersion.Current,activeFlag);
			database.AddParameter(commandWrapper, "@LoginTime", DbType.DateTime, ParameterDirection.InputOutput,"",DataRowVersion.Current,loginTime);
			database.AddParameter(commandWrapper, "@ActiveTime", DbType.DateTime, ParameterDirection.InputOutput,"",DataRowVersion.Current,activeTime);
			database.AddParameter(commandWrapper, "@OnlineMinutes", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,onlineMinutes);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            activeFlag=(System.Boolean)database.GetParameterValue(commandWrapper, "@ActiveFlag");
            loginTime=(System.DateTime)database.GetParameterValue(commandWrapper, "@LoginTime");
            activeTime=(System.DateTime)database.GetParameterValue(commandWrapper, "@ActiveTime");
            onlineMinutes=(System.Int32)database.GetParameterValue(commandWrapper, "@OnlineMinutes");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetOnlineMinutes
		
		/// <summary>
        /// GetOnlineMinutes
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="activeFlag">activeFlag</param>
		/// <param name="guildInTime">guildInTime</param>
		/// <param name="cntOnlineMinutes">cntOnlineMinutes</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 16:53:08</remarks>
        public bool GetOnlineMinutes ( System.Guid managerId,ref  System.Boolean activeFlag,ref  System.DateTime guildInTime,ref  System.Int32 cntOnlineMinutes,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Online_GetOnlineMinutes");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@ActiveFlag", DbType.Boolean, ParameterDirection.InputOutput,"",DataRowVersion.Current,activeFlag);
			database.AddParameter(commandWrapper, "@GuildInTime", DbType.DateTime, ParameterDirection.InputOutput,"",DataRowVersion.Current,guildInTime);
			database.AddParameter(commandWrapper, "@CntOnlineMinutes", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,cntOnlineMinutes);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            activeFlag=(System.Boolean)database.GetParameterValue(commandWrapper, "@ActiveFlag");
            guildInTime=(System.DateTime)database.GetParameterValue(commandWrapper, "@GuildInTime");
            cntOnlineMinutes=(System.Int32)database.GetParameterValue(commandWrapper, "@CntOnlineMinutes");
            
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
        /// <remarks>2015/10/18 16:53:08</remarks>
        public bool Insert(OnlineInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_OnlineInfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ActiveFlag", DbType.Boolean, entity.ActiveFlag);
			database.AddInParameter(commandWrapper, "@ResetFlag", DbType.Boolean, entity.ResetFlag);
			database.AddInParameter(commandWrapper, "@LoginTime", DbType.DateTime, entity.LoginTime);
			database.AddInParameter(commandWrapper, "@GuildInTime", DbType.DateTime, entity.GuildInTime);
			database.AddInParameter(commandWrapper, "@ActiveTime", DbType.DateTime, entity.ActiveTime);
			database.AddInParameter(commandWrapper, "@TotalOnlineMinutes", DbType.Int64, entity.TotalOnlineMinutes);
			database.AddInParameter(commandWrapper, "@CntOnlineMinutes", DbType.Int32, entity.CntOnlineMinutes);
			database.AddInParameter(commandWrapper, "@CurOnlineMinutes", DbType.Int32, entity.CurOnlineMinutes);
			database.AddInParameter(commandWrapper, "@LoginIp", DbType.AnsiString, entity.LoginIp);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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
        /// <remarks>2015/10/18 16:53:08</remarks>
        public bool Update(OnlineInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_OnlineInfo_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ActiveFlag", DbType.Boolean, entity.ActiveFlag);
			database.AddInParameter(commandWrapper, "@ResetFlag", DbType.Boolean, entity.ResetFlag);
			database.AddInParameter(commandWrapper, "@LoginTime", DbType.DateTime, entity.LoginTime);
			database.AddInParameter(commandWrapper, "@GuildInTime", DbType.DateTime, entity.GuildInTime);
			database.AddInParameter(commandWrapper, "@ActiveTime", DbType.DateTime, entity.ActiveTime);
			database.AddInParameter(commandWrapper, "@TotalOnlineMinutes", DbType.Int64, entity.TotalOnlineMinutes);
			database.AddInParameter(commandWrapper, "@CntOnlineMinutes", DbType.Int32, entity.CntOnlineMinutes);
			database.AddInParameter(commandWrapper, "@CurOnlineMinutes", DbType.Int32, entity.CurOnlineMinutes);
			database.AddInParameter(commandWrapper, "@LoginIp", DbType.AnsiString, entity.LoginIp);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

