

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
    
    public partial class TxLogininfoProvider
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
		/// 将IDataReader的当前记录读取到TxLogininfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TxLogininfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TxLogininfoEntity();
			
            obj.OpenId = (System.String) reader["OpenId"];
            obj.OpenKey = (System.String) reader["OpenKey"];
            obj.Pf = (System.String) reader["Pf"];
            obj.Format = (System.String) reader["Format"];
            obj.Ext = (System.String) reader["Ext"];
            obj.Ext1 = (System.String) reader["Ext1"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
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
        public List<TxLogininfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TxLogininfoEntity>();
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
        public TxLogininfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TxLogininfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="openId">openId</param>
        /// <returns>TxLogininfoEntity</returns>
        /// <remarks>2016/12/19 16:54:28</remarks>
        public TxLogininfoEntity GetById( System.String openId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TxLogininfo_GetById");
            
			database.AddInParameter(commandWrapper, "@OpenId", DbType.AnsiString, openId);

            
            TxLogininfoEntity obj=null;
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
        /// <returns>TxLogininfoEntity列表</returns>
        /// <remarks>2016/12/19 16:54:28</remarks>
        public List<TxLogininfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TxLogininfo_GetAll");
            

            
            List<TxLogininfoEntity> list = null;
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
		/// <param name="openId">openId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/12/19 16:54:28</remarks>
        public bool Delete ( System.String openId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TxLogininfo_Delete");
            
			database.AddInParameter(commandWrapper, "@OpenId", DbType.AnsiString, openId);

            
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
        /// <remarks>2016/12/19 16:54:28</remarks>
        public bool Insert(TxLogininfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TxLogininfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@OpenId", DbType.AnsiString, entity.OpenId);
			database.AddInParameter(commandWrapper, "@OpenKey", DbType.AnsiString, entity.OpenKey);
			database.AddInParameter(commandWrapper, "@Pf", DbType.AnsiString, entity.Pf);
			database.AddInParameter(commandWrapper, "@Format", DbType.AnsiString, entity.Format);
			database.AddInParameter(commandWrapper, "@Ext", DbType.AnsiString, entity.Ext);
			database.AddInParameter(commandWrapper, "@Ext1", DbType.AnsiString, entity.Ext1);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/12/19 16:54:28</remarks>
        public bool Update(TxLogininfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TxLogininfo_Update");
            
			database.AddInParameter(commandWrapper, "@OpenId", DbType.AnsiString, entity.OpenId);
			database.AddInParameter(commandWrapper, "@OpenKey", DbType.AnsiString, entity.OpenKey);
			database.AddInParameter(commandWrapper, "@Pf", DbType.AnsiString, entity.Pf);
			database.AddInParameter(commandWrapper, "@Format", DbType.AnsiString, entity.Format);
			database.AddInParameter(commandWrapper, "@Ext", DbType.AnsiString, entity.Ext);
			database.AddInParameter(commandWrapper, "@Ext1", DbType.AnsiString, entity.Ext1);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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
