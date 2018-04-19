

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
    
    public partial class CsdkisregistProvider
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
		/// 将IDataReader的当前记录读取到CsdkisregistEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public CsdkisregistEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new CsdkisregistEntity();
			
            obj.idx = (System.Int32) reader["idx"];
            obj.ret = (System.String) reader["ret"];
            obj.msg = (System.String) reader["msg"];
            obj.roleId = (System.String) reader["roleId"];
            obj.roleName = (System.String) reader["roleName"];
            obj.roleLevel = (System.String) reader["roleLevel"];
            obj.serverNo = (System.String) reader["serverNo"];
            obj.serverId = (System.String) reader["serverId"];
            obj.serverName = (System.String) reader["serverName"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<CsdkisregistEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<CsdkisregistEntity>();
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
        public CsdkisregistProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public CsdkisregistProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>CsdkisregistEntity</returns>
        /// <remarks>2016/5/20 12:49:40</remarks>
        public CsdkisregistEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Csdkisregist_GetById");
            
			database.AddInParameter(commandWrapper, "@idx", DbType.Int32, idx);

            
            CsdkisregistEntity obj=null;
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
        /// <returns>CsdkisregistEntity列表</returns>
        /// <remarks>2016/5/20 12:49:40</remarks>
        public List<CsdkisregistEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Csdkisregist_GetAll");
            

            
            List<CsdkisregistEntity> list = null;
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
        /// <remarks>2016/5/20 12:49:40</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Csdkisregist_Delete");
            
			database.AddInParameter(commandWrapper, "@idx", DbType.Int32, idx);

            
            
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
        /// <remarks>2016/5/20 12:49:40</remarks>
        public bool Insert(CsdkisregistEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/20 12:49:40</remarks>
        public bool Insert(CsdkisregistEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Csdkisregist_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ret", DbType.AnsiString, entity.ret);
			database.AddInParameter(commandWrapper, "@msg", DbType.AnsiString, entity.msg);
			database.AddInParameter(commandWrapper, "@roleId", DbType.AnsiString, entity.roleId);
			database.AddInParameter(commandWrapper, "@roleName", DbType.AnsiString, entity.roleName);
			database.AddInParameter(commandWrapper, "@roleLevel", DbType.AnsiString, entity.roleLevel);
			database.AddInParameter(commandWrapper, "@serverNo", DbType.AnsiString, entity.serverNo);
			database.AddInParameter(commandWrapper, "@serverId", DbType.AnsiString, entity.serverId);
			database.AddInParameter(commandWrapper, "@serverName", DbType.AnsiString, entity.serverName);
			database.AddParameter(commandWrapper, "@idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.idx=(System.Int32)database.GetParameterValue(commandWrapper, "@idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/5/20 12:49:40</remarks>
        public bool Update(CsdkisregistEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/20 12:49:40</remarks>
        public bool Update(CsdkisregistEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Csdkisregist_Update");
            
			database.AddInParameter(commandWrapper, "@idx", DbType.Int32, entity.idx);
			database.AddInParameter(commandWrapper, "@ret", DbType.AnsiString, entity.ret);
			database.AddInParameter(commandWrapper, "@msg", DbType.AnsiString, entity.msg);
			database.AddInParameter(commandWrapper, "@roleId", DbType.AnsiString, entity.roleId);
			database.AddInParameter(commandWrapper, "@roleName", DbType.AnsiString, entity.roleName);
			database.AddInParameter(commandWrapper, "@roleLevel", DbType.AnsiString, entity.roleLevel);
			database.AddInParameter(commandWrapper, "@serverNo", DbType.AnsiString, entity.serverNo);
			database.AddInParameter(commandWrapper, "@serverId", DbType.AnsiString, entity.serverId);
			database.AddInParameter(commandWrapper, "@serverName", DbType.AnsiString, entity.serverName);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.idx=(System.Int32)database.GetParameterValue(commandWrapper, "@idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
