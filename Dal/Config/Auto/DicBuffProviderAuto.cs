

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
    
    public partial class DicBuffProvider
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
		/// 将IDataReader的当前记录读取到DicBuffEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicBuffEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicBuffEntity();
			
            obj.BuffId = (System.Int32) reader["BuffId"];
            obj.BuffName = (System.String) reader["BuffName"];
            obj.BuffType = (System.Int32) reader["BuffType"];
            obj.BaseFlag = (System.Boolean) reader["BaseFlag"];
            obj.BaseBuffMap = (System.String) reader["BaseBuffMap"];
            obj.Icon = (System.String) reader["Icon"];
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
        public List<DicBuffEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicBuffEntity>();
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
        public DicBuffProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicBuffProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="buffId">buffId</param>
        /// <returns>DicBuffEntity</returns>
        /// <remarks>2015/10/19 10:49:19</remarks>
        public DicBuffEntity GetById( System.Int32 buffId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicBuff_GetById");
            
			database.AddInParameter(commandWrapper, "@BuffId", DbType.Int32, buffId);

            
            DicBuffEntity obj=null;
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
        /// <returns>DicBuffEntity列表</returns>
        /// <remarks>2015/10/19 10:49:19</remarks>
        public List<DicBuffEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicBuff_GetAll");
            

            
            List<DicBuffEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
		/// <summary>
        /// GetAllForCache
        /// </summary>
        /// <returns>DicBuffEntity列表</returns>
        /// <remarks>2015/10/19 10:49:19</remarks>
        public List<DicBuffEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicBuff_GetAllForCache");
            

            
            List<DicBuffEntity> list = null;
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
		/// <param name="buffId">buffId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 10:49:19</remarks>
        public bool Delete ( System.Int32 buffId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicBuff_Delete");
            
			database.AddInParameter(commandWrapper, "@BuffId", DbType.Int32, buffId);

            
            
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
        /// <remarks>2015/10/19 10:49:19</remarks>
        public bool Insert(DicBuffEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:49:19</remarks>
        public bool Insert(DicBuffEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicBuff_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@BuffId", DbType.Int32, entity.BuffId);
			database.AddInParameter(commandWrapper, "@BuffName", DbType.String, entity.BuffName);
			database.AddInParameter(commandWrapper, "@BuffType", DbType.Int32, entity.BuffType);
			database.AddInParameter(commandWrapper, "@BaseFlag", DbType.Boolean, entity.BaseFlag);
			database.AddInParameter(commandWrapper, "@BaseBuffMap", DbType.AnsiString, entity.BaseBuffMap);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
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
            
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:49:19</remarks>
        public bool Update(DicBuffEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:49:19</remarks>
        public bool Update(DicBuffEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicBuff_Update");
            
			database.AddInParameter(commandWrapper, "@BuffId", DbType.Int32, entity.BuffId);
			database.AddInParameter(commandWrapper, "@BuffName", DbType.String, entity.BuffName);
			database.AddInParameter(commandWrapper, "@BuffType", DbType.Int32, entity.BuffType);
			database.AddInParameter(commandWrapper, "@BaseFlag", DbType.Boolean, entity.BaseFlag);
			database.AddInParameter(commandWrapper, "@BaseBuffMap", DbType.AnsiString, entity.BaseBuffMap);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
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

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

