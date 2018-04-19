

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
    
    public partial class UserloginZoneProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Support;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到UserloginZoneEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public UserloginZoneEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new UserloginZoneEntity();
			
            obj.Account = (System.String) reader["Account"];
            obj.Platform = (System.String) reader["Platform"];
            obj.LastLoginTime = (System.DateTime) reader["LastLoginTime"];
            obj.LoginSties = (System.String) reader["LoginSties"];
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
        public List<UserloginZoneEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<UserloginZoneEntity>();
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
        public UserloginZoneProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public UserloginZoneProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="account">account</param>
        /// <returns>UserloginZoneEntity</returns>
        /// <remarks>2016/6/1 14:25:26</remarks>
        public UserloginZoneEntity GetById( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_UserloginZone_GetById");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            UserloginZoneEntity obj=null;
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
		
		#region  GetByAccountPlatform
		
		/// <summary>
        /// GetByAccountPlatform
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="platform">platform</param>
        /// <returns>UserloginZoneEntity</returns>
        /// <remarks>2016/6/1 14:25:26</remarks>
        public UserloginZoneEntity GetByAccountPlatform( System.String account, System.String platform)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_UserLoginZone_GetByAccountPlatform");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@Platform", DbType.AnsiString, platform);

            
            UserloginZoneEntity obj=null;
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
        /// <returns>UserloginZoneEntity列表</returns>
        /// <remarks>2016/6/1 14:25:26</remarks>
        public List<UserloginZoneEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_UserloginZone_GetAll");
            

            
            List<UserloginZoneEntity> list = null;
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
		/// <param name="account">account</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/1 14:25:26</remarks>
        public bool Delete ( System.String account,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_UserloginZone_Delete");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            
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
        /// <remarks>2016/6/1 14:25:26</remarks>
        public bool Insert(UserloginZoneEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/1 14:25:26</remarks>
        public bool Insert(UserloginZoneEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_UserloginZone_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Platform", DbType.AnsiString, entity.Platform);
			database.AddInParameter(commandWrapper, "@LastLoginTime", DbType.DateTime, entity.LastLoginTime);
			database.AddInParameter(commandWrapper, "@LoginSties", DbType.AnsiString, entity.LoginSties);
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
            
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/6/1 14:25:26</remarks>
        public bool Update(UserloginZoneEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/1 14:25:26</remarks>
        public bool Update(UserloginZoneEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_UserloginZone_Update");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Platform", DbType.AnsiString, entity.Platform);
			database.AddInParameter(commandWrapper, "@LastLoginTime", DbType.DateTime, entity.LastLoginTime);
			database.AddInParameter(commandWrapper, "@LoginSties", DbType.AnsiString, entity.LoginSties);
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

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

