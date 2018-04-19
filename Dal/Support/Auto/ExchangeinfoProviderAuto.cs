

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
    
    public partial class ExchangeInfoProvider
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
		/// 将IDataReader的当前记录读取到ExchangeInfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ExchangeInfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ExchangeInfoEntity();
			
            obj.Idx = (System.String) reader["Idx"];
            obj.ExchangeType = (System.Int32) reader["ExchangeType"];
            obj.ZoneName = (System.Int32) reader["ZoneName"];
            obj.Account = (System.String) reader["Account"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.AtZoneId = (System.Int32) reader["AtZoneId"];
            obj.PackId = (System.Int32) reader["PackId"];
            obj.BatchId = (System.Int32) reader["BatchId"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
            obj.PlatformCode = (System.String) reader["PlatformCode"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ExchangeInfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ExchangeInfoEntity>();
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
        public ExchangeInfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ExchangeInfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ExchangeInfoEntity</returns>
        /// <remarks>2016/6/6 19:10:26</remarks>
        public ExchangeInfoEntity GetById( System.String idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ExchangeInfo_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.AnsiString, idx);

            
            ExchangeInfoEntity obj=null;
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
        /// <returns>ExchangeInfoEntity列表</returns>
        /// <remarks>2016/6/6 19:10:26</remarks>
        public List<ExchangeInfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ExchangeInfo_GetAll");
            

            
            List<ExchangeInfoEntity> list = null;
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
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/6 19:10:26</remarks>
        public bool Delete ( System.String idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ExchangeInfo_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.AnsiString, idx);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);

            
            
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
		
		#region  Save
		
		/// <summary>
        /// Save
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="platformCode">platformCode</param>
		/// <param name="account">account</param>
		/// <param name="managerId">managerId</param>
		/// <param name="zoneName">zoneName</param>
		/// <param name="packId">packId</param>
		/// <param name="rowVersion">rowVersion</param>
		/// <param name="exchangeBatchLimitCode">exchangeBatchLimitCode</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/6 19:10:26</remarks>
        public bool Save ( System.String idx, System.String platformCode, System.String account, System.Guid managerId, System.Int32 zoneName, System.Int32 packId, System.Byte[] rowVersion, System.Int32 exchangeBatchLimitCode,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ExchangeInfo_Save");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.AnsiString, idx);
			database.AddInParameter(commandWrapper, "@PlatformCode", DbType.AnsiString, platformCode);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.Int32, zoneName);
			database.AddInParameter(commandWrapper, "@PackId", DbType.Int32, packId);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);
			database.AddInParameter(commandWrapper, "@ExchangeBatchLimitCode", DbType.Int32, exchangeBatchLimitCode);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }

            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
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
        /// <remarks>2016/6/6 19:10:26</remarks>
        public bool Insert(ExchangeInfoEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/6 19:10:26</remarks>
        public bool Insert(ExchangeInfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ExchangeInfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.AnsiString, entity.Idx);
			database.AddInParameter(commandWrapper, "@ExchangeType", DbType.Int32, entity.ExchangeType);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.Int32, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@AtZoneId", DbType.Int32, entity.AtZoneId);
			database.AddInParameter(commandWrapper, "@PackId", DbType.Int32, entity.PackId);
			database.AddInParameter(commandWrapper, "@BatchId", DbType.Int32, entity.BatchId);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);
			database.AddInParameter(commandWrapper, "@PlatformCode", DbType.AnsiString, entity.PlatformCode);

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
        /// <remarks>2016/6/6 19:10:26</remarks>
        public bool Update(ExchangeInfoEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/6 19:10:26</remarks>
        public bool Update(ExchangeInfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ExchangeInfo_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.AnsiString, entity.Idx);
			database.AddInParameter(commandWrapper, "@ExchangeType", DbType.Int32, entity.ExchangeType);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.Int32, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@AtZoneId", DbType.Int32, entity.AtZoneId);
			database.AddInParameter(commandWrapper, "@PackId", DbType.Int32, entity.PackId);
			database.AddInParameter(commandWrapper, "@BatchId", DbType.Int32, entity.BatchId);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);
			database.AddInParameter(commandWrapper, "@PlatformCode", DbType.AnsiString, entity.PlatformCode);

            
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
