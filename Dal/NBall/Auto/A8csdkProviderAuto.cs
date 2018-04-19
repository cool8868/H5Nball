

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
    
    public partial class A8csdkProvider
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
		/// 将IDataReader的当前记录读取到A8csdkEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public A8csdkEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new A8csdkEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.GameOrderId = (System.String) reader["GameOrderId"];
            obj.Price = (System.Int32) reader["Price"];
            obj.GoodsName = (System.String) reader["GoodsName"];
            obj.GoodsId = (System.String) reader["GoodsId"];
            obj.Title = (System.String) reader["Title"];
            obj.CsdkId = (System.String) reader["CsdkId"];
            obj.ChannelAlias = (System.String) reader["ChannelAlias"];
            obj.SubChannel = (System.String) reader["SubChannel"];
            obj.ServerId = (System.String) reader["ServerId"];
            obj.ServerName = (System.String) reader["ServerName"];
            obj.RoleId = (System.String) reader["RoleId"];
            obj.RoleName = (System.String) reader["RoleName"];
            obj.RoleLevel = (System.String) reader["RoleLevel"];
            obj.SessionId = (System.String) reader["SessionId"];
            obj.Model = (System.String) reader["Model"];
            obj.Release = (System.String) reader["Release"];
            obj.DeviceId = (System.String) reader["DeviceId"];
            obj.Ext = (System.String) reader["Ext"];
            obj.Uid = (System.String) reader["Uid"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<A8csdkEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<A8csdkEntity>();
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
        public A8csdkProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public A8csdkProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>A8csdkEntity</returns>
        /// <remarks>2016/5/23 14:32:34</remarks>
        public A8csdkEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_A8csdk_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            A8csdkEntity obj=null;
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
        /// <returns>A8csdkEntity列表</returns>
        /// <remarks>2016/5/23 14:32:34</remarks>
        public List<A8csdkEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_A8csdk_GetAll");
            

            
            List<A8csdkEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByGameOrderId
		
		/// <summary>
        /// GetByGameOrderId
        /// </summary>
        /// <returns>A8csdkEntity列表</returns>
        /// <remarks>2016/5/23 14:32:34</remarks>
        public List<A8csdkEntity> GetByGameOrderId( System.String gameOrderId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_A8csdk_GetByGameOrderId");
            
			database.AddInParameter(commandWrapper, "@GameOrderId", DbType.AnsiString, gameOrderId);

            
            List<A8csdkEntity> list = null;
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
        /// <remarks>2016/5/23 14:32:34</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_A8csdk_Delete");
            
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
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/23 14:32:34</remarks>
        public bool Insert(A8csdkEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_A8csdk_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@GameOrderId", DbType.AnsiString, entity.GameOrderId);
			database.AddInParameter(commandWrapper, "@Price", DbType.Int32, entity.Price);
			database.AddInParameter(commandWrapper, "@GoodsName", DbType.AnsiString, entity.GoodsName);
			database.AddInParameter(commandWrapper, "@GoodsId", DbType.AnsiString, entity.GoodsId);
			database.AddInParameter(commandWrapper, "@Title", DbType.AnsiString, entity.Title);
			database.AddInParameter(commandWrapper, "@CsdkId", DbType.AnsiString, entity.CsdkId);
			database.AddInParameter(commandWrapper, "@ChannelAlias", DbType.AnsiString, entity.ChannelAlias);
			database.AddInParameter(commandWrapper, "@SubChannel", DbType.AnsiString, entity.SubChannel);
			database.AddInParameter(commandWrapper, "@ServerId", DbType.AnsiString, entity.ServerId);
			database.AddInParameter(commandWrapper, "@ServerName", DbType.AnsiString, entity.ServerName);
			database.AddInParameter(commandWrapper, "@RoleId", DbType.AnsiString, entity.RoleId);
			database.AddInParameter(commandWrapper, "@RoleName", DbType.AnsiString, entity.RoleName);
			database.AddInParameter(commandWrapper, "@RoleLevel", DbType.AnsiString, entity.RoleLevel);
			database.AddInParameter(commandWrapper, "@SessionId", DbType.AnsiString, entity.SessionId);
			database.AddInParameter(commandWrapper, "@Model", DbType.AnsiString, entity.Model);
			database.AddInParameter(commandWrapper, "@Release", DbType.AnsiString, entity.Release);
			database.AddInParameter(commandWrapper, "@DeviceId", DbType.AnsiString, entity.DeviceId);
			database.AddInParameter(commandWrapper, "@Ext", DbType.AnsiString, entity.Ext);
			database.AddInParameter(commandWrapper, "@Uid", DbType.AnsiString, entity.Uid);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
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
        /// <remarks>2016/5/23 14:32:34</remarks>
        public bool Update(A8csdkEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_A8csdk_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@GameOrderId", DbType.AnsiString, entity.GameOrderId);
			database.AddInParameter(commandWrapper, "@Price", DbType.Int32, entity.Price);
			database.AddInParameter(commandWrapper, "@GoodsName", DbType.AnsiString, entity.GoodsName);
			database.AddInParameter(commandWrapper, "@GoodsId", DbType.AnsiString, entity.GoodsId);
			database.AddInParameter(commandWrapper, "@Title", DbType.AnsiString, entity.Title);
			database.AddInParameter(commandWrapper, "@CsdkId", DbType.AnsiString, entity.CsdkId);
			database.AddInParameter(commandWrapper, "@ChannelAlias", DbType.AnsiString, entity.ChannelAlias);
			database.AddInParameter(commandWrapper, "@SubChannel", DbType.AnsiString, entity.SubChannel);
			database.AddInParameter(commandWrapper, "@ServerId", DbType.AnsiString, entity.ServerId);
			database.AddInParameter(commandWrapper, "@ServerName", DbType.AnsiString, entity.ServerName);
			database.AddInParameter(commandWrapper, "@RoleId", DbType.AnsiString, entity.RoleId);
			database.AddInParameter(commandWrapper, "@RoleName", DbType.AnsiString, entity.RoleName);
			database.AddInParameter(commandWrapper, "@RoleLevel", DbType.AnsiString, entity.RoleLevel);
			database.AddInParameter(commandWrapper, "@SessionId", DbType.AnsiString, entity.SessionId);
			database.AddInParameter(commandWrapper, "@Model", DbType.AnsiString, entity.Model);
			database.AddInParameter(commandWrapper, "@Release", DbType.AnsiString, entity.Release);
			database.AddInParameter(commandWrapper, "@DeviceId", DbType.AnsiString, entity.DeviceId);
			database.AddInParameter(commandWrapper, "@Ext", DbType.AnsiString, entity.Ext);
			database.AddInParameter(commandWrapper, "@Uid", DbType.AnsiString, entity.Uid);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
