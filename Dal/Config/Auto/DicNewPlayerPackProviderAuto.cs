

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
    
    public partial class DicNewplayerpackProvider
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
		/// 将IDataReader的当前记录读取到DicNewplayerpackEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicNewplayerpackEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicNewplayerpackEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.PackId = (System.Int32) reader["PackId"];
            obj.Type = (System.Int32) reader["Type"];
            obj.SubType = (System.Int32) reader["SubType"];
            obj.Strength = (System.Int32) reader["Strength"];
            obj.Count = (System.Int32) reader["Count"];
            obj.IsBinding = (System.Boolean) reader["IsBinding"];
            obj.Description = (System.String) reader["Description"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicNewplayerpackEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicNewplayerpackEntity>();
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
        public DicNewplayerpackProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicNewplayerpackProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicNewplayerpackEntity</returns>
        /// <remarks>2016/1/12 14:25:40</remarks>
        public DicNewplayerpackEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicNewplayerpack_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DicNewplayerpackEntity obj=null;
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
        /// <returns>DicNewplayerpackEntity列表</returns>
        /// <remarks>2016/1/12 14:25:40</remarks>
        public List<DicNewplayerpackEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicNewplayerpack_GetAll");
            

            
            List<DicNewplayerpackEntity> list = null;
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
        /// <returns>DicNewplayerpackEntity列表</returns>
        /// <remarks>2016/1/12 14:25:40</remarks>
        public List<DicNewplayerpackEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicNewPlayerPack_GetAllForCache");
            

            
            List<DicNewplayerpackEntity> list = null;
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
        /// <remarks>2016/1/12 14:25:40</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicNewplayerpack_Delete");
            
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
        /// <remarks>2016/1/12 14:25:40</remarks>
        public bool Insert(DicNewplayerpackEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/12 14:25:40</remarks>
        public bool Insert(DicNewplayerpackEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicNewplayerpack_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@PackId", DbType.Int32, entity.PackId);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@Count", DbType.Int32, entity.Count);
			database.AddInParameter(commandWrapper, "@IsBinding", DbType.Boolean, entity.IsBinding);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);

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
        /// <remarks>2016/1/12 14:25:40</remarks>
        public bool Update(DicNewplayerpackEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/12 14:25:40</remarks>
        public bool Update(DicNewplayerpackEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicNewplayerpack_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@PackId", DbType.Int32, entity.PackId);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@Count", DbType.Int32, entity.Count);
			database.AddInParameter(commandWrapper, "@IsBinding", DbType.Boolean, entity.IsBinding);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);

            
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

