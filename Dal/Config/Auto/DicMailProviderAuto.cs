

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
    
    public partial class DicMailProvider
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
		/// 将IDataReader的当前记录读取到DicMailEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicMailEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicMailEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Category = (System.Int32) reader["Category"];
            obj.SourceType = (System.Int32) reader["SourceType"];
            obj.Title = (System.String) reader["Title"];
            obj.ContentTemplate = (System.String) reader["ContentTemplate"];
            obj.ContentParam = (System.String) reader["ContentParam"];
            obj.HasAttach = (System.Boolean) reader["HasAttach"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicMailEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicMailEntity>();
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
        public DicMailProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicMailProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicMailEntity</returns>
        /// <remarks>2016/2/23 15:26:55</remarks>
        public DicMailEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicMail_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DicMailEntity obj=null;
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
        /// <returns>DicMailEntity列表</returns>
        /// <remarks>2016/2/23 15:26:55</remarks>
        public List<DicMailEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicMail_GetAll");
            

            
            List<DicMailEntity> list = null;
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
        /// <returns>DicMailEntity列表</returns>
        /// <remarks>2016/2/23 15:26:55</remarks>
        public List<DicMailEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicMail_GetAllForCache");
            

            
            List<DicMailEntity> list = null;
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
        /// <remarks>2016/2/23 15:26:55</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicMail_Delete");
            
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
        /// <remarks>2016/2/23 15:26:55</remarks>
        public bool Insert(DicMailEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/2/23 15:26:55</remarks>
        public bool Insert(DicMailEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicMail_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Category", DbType.Int32, entity.Category);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, entity.SourceType);
			database.AddInParameter(commandWrapper, "@Title", DbType.String, entity.Title);
			database.AddInParameter(commandWrapper, "@ContentTemplate", DbType.String, entity.ContentTemplate);
			database.AddInParameter(commandWrapper, "@ContentParam", DbType.AnsiString, entity.ContentParam);
			database.AddInParameter(commandWrapper, "@HasAttach", DbType.Boolean, entity.HasAttach);

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
        /// <remarks>2016/2/23 15:26:55</remarks>
        public bool Update(DicMailEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/2/23 15:26:55</remarks>
        public bool Update(DicMailEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicMail_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Category", DbType.Int32, entity.Category);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, entity.SourceType);
			database.AddInParameter(commandWrapper, "@Title", DbType.String, entity.Title);
			database.AddInParameter(commandWrapper, "@ContentTemplate", DbType.String, entity.ContentTemplate);
			database.AddInParameter(commandWrapper, "@ContentParam", DbType.AnsiString, entity.ContentParam);
			database.AddInParameter(commandWrapper, "@HasAttach", DbType.Boolean, entity.HasAttach);

            
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

