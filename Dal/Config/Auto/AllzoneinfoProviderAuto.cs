

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
    
    public partial class AllZoneinfoProvider
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
		/// 将IDataReader的当前记录读取到AllZoneinfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public AllZoneinfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new AllZoneinfoEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.PlatformCode = (System.String) reader["PlatformCode"];
            obj.ZoneName = (System.String) reader["ZoneName"];
            obj.PlatformZoneName = (System.String) reader["PlatformZoneName"];
            obj.Name = (System.String) reader["Name"];
            obj.ApiUrl = (System.String) reader["ApiUrl"];
            obj.ChargeUrl = (System.String) reader["ChargeUrl"];
            obj.WebServerUrl = (System.String) reader["WebServerUrl"];
            obj.IsDebug = (System.Int32) reader["IsDebug"];
            obj.IsOpenCharge = (System.Int32) reader["IsOpenCharge"];
            obj.ChatIp = (System.String) reader["ChatIp"];
            obj.ChatPort = (System.Int32) reader["ChatPort"];
            obj.ClientVersion = (System.String) reader["ClientVersion"];
            obj.Cdn = (System.String) reader["Cdn"];
            obj.CrossName = (System.String) reader["CrossName"];
            obj.OpenIndulge = (System.Boolean) reader["OpenIndulge"];
            obj.TxFooter = (System.String) reader["TxFooter"];
            obj.GameName = (System.String) reader["GameName"];
            obj.States = (System.Int32) reader["States"];
            obj.Maintain = (System.String) reader["Maintain"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<AllZoneinfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<AllZoneinfoEntity>();
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
        public AllZoneinfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public AllZoneinfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>AllZoneinfoEntity</returns>
        /// <remarks>2016/6/1 19:20:29</remarks>
        public AllZoneinfoEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllZoneinfo_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            AllZoneinfoEntity obj=null;
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
        /// <returns>AllZoneinfoEntity列表</returns>
        /// <remarks>2016/6/1 19:20:29</remarks>
        public List<AllZoneinfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllZoneinfo_GetAll");
            

            
            List<AllZoneinfoEntity> list = null;
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
        /// <remarks>2016/6/1 19:20:29</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllZoneinfo_Delete");
            
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
        /// <remarks>2016/6/1 19:20:29</remarks>
        public bool Insert(AllZoneinfoEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/1 19:20:29</remarks>
        public bool Insert(AllZoneinfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllZoneinfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@PlatformCode", DbType.AnsiString, entity.PlatformCode);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.AnsiString, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@PlatformZoneName", DbType.AnsiString, entity.PlatformZoneName);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@ApiUrl", DbType.AnsiString, entity.ApiUrl);
			database.AddInParameter(commandWrapper, "@ChargeUrl", DbType.AnsiString, entity.ChargeUrl);
			database.AddInParameter(commandWrapper, "@WebServerUrl", DbType.AnsiString, entity.WebServerUrl);
			database.AddInParameter(commandWrapper, "@IsDebug", DbType.Int32, entity.IsDebug);
			database.AddInParameter(commandWrapper, "@IsOpenCharge", DbType.Int32, entity.IsOpenCharge);
			database.AddInParameter(commandWrapper, "@ChatIp", DbType.AnsiString, entity.ChatIp);
			database.AddInParameter(commandWrapper, "@ChatPort", DbType.Int32, entity.ChatPort);
			database.AddInParameter(commandWrapper, "@ClientVersion", DbType.AnsiString, entity.ClientVersion);
			database.AddInParameter(commandWrapper, "@Cdn", DbType.AnsiString, entity.Cdn);
			database.AddInParameter(commandWrapper, "@CrossName", DbType.String, entity.CrossName);
			database.AddInParameter(commandWrapper, "@OpenIndulge", DbType.Boolean, entity.OpenIndulge);
			database.AddInParameter(commandWrapper, "@TxFooter", DbType.String, entity.TxFooter);
			database.AddInParameter(commandWrapper, "@GameName", DbType.String, entity.GameName);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
			database.AddInParameter(commandWrapper, "@Maintain", DbType.AnsiString, entity.Maintain);

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
        /// <remarks>2016/6/1 19:20:29</remarks>
        public bool Update(AllZoneinfoEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/1 19:20:29</remarks>
        public bool Update(AllZoneinfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllZoneinfo_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@PlatformCode", DbType.AnsiString, entity.PlatformCode);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.AnsiString, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@PlatformZoneName", DbType.AnsiString, entity.PlatformZoneName);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@ApiUrl", DbType.AnsiString, entity.ApiUrl);
			database.AddInParameter(commandWrapper, "@ChargeUrl", DbType.AnsiString, entity.ChargeUrl);
			database.AddInParameter(commandWrapper, "@WebServerUrl", DbType.AnsiString, entity.WebServerUrl);
			database.AddInParameter(commandWrapper, "@IsDebug", DbType.Int32, entity.IsDebug);
			database.AddInParameter(commandWrapper, "@IsOpenCharge", DbType.Int32, entity.IsOpenCharge);
			database.AddInParameter(commandWrapper, "@ChatIp", DbType.AnsiString, entity.ChatIp);
			database.AddInParameter(commandWrapper, "@ChatPort", DbType.Int32, entity.ChatPort);
			database.AddInParameter(commandWrapper, "@ClientVersion", DbType.AnsiString, entity.ClientVersion);
			database.AddInParameter(commandWrapper, "@Cdn", DbType.AnsiString, entity.Cdn);
			database.AddInParameter(commandWrapper, "@CrossName", DbType.String, entity.CrossName);
			database.AddInParameter(commandWrapper, "@OpenIndulge", DbType.Boolean, entity.OpenIndulge);
			database.AddInParameter(commandWrapper, "@TxFooter", DbType.String, entity.TxFooter);
			database.AddInParameter(commandWrapper, "@GameName", DbType.String, entity.GameName);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
			database.AddInParameter(commandWrapper, "@Maintain", DbType.AnsiString, entity.Maintain);

            
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

