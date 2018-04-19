

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
    
    public partial class DicItemProvider
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
		/// 将IDataReader的当前记录读取到DicItemEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicItemEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicItemEntity();
			
            obj.ItemCode = (System.Int32) reader["ItemCode"];
            obj.ItemName = (System.String) reader["ItemName"];
            obj.ItemType = (System.Int32) reader["ItemType"];
            obj.SubType = (System.Int32) reader["SubType"];
            obj.ThirdType = (System.Int32) reader["ThirdType"];
            obj.FourthType = (System.Int32) reader["FourthType"];
            obj.ImageId = (System.Int32) reader["ImageId"];
            obj.LinkId = (System.Int32) reader["LinkId"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicItemEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicItemEntity>();
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
        public DicItemProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicItemProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="itemCode">itemCode</param>
        /// <returns>DicItemEntity</returns>
        /// <remarks>2015/10/19 10:51:44</remarks>
        public DicItemEntity GetById( System.Int32 itemCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicItem_GetById");
            
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, itemCode);

            
            DicItemEntity obj=null;
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
        /// <returns>DicItemEntity列表</returns>
        /// <remarks>2015/10/19 10:51:44</remarks>
        public List<DicItemEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicItem_GetAll");
            

            
            List<DicItemEntity> list = null;
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
        /// <returns>DicItemEntity列表</returns>
        /// <remarks>2015/10/19 10:51:44</remarks>
        public List<DicItemEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicItem_GetAllForCache");
            

            
            List<DicItemEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetContractItem
		
		/// <summary>
        /// GetContractItem
        /// </summary>
        /// <returns>DicItemEntity列表</returns>
        /// <remarks>2015/10/19 10:51:44</remarks>
        public List<DicItemEntity> GetContractItem()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicItem_GetContractItem");
            

            
            List<DicItemEntity> list = null;
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
		/// <param name="itemCode">itemCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 10:51:44</remarks>
        public bool Delete ( System.Int32 itemCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicItem_Delete");
            
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, itemCode);

            
            
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
        /// <remarks>2015/10/19 10:51:44</remarks>
        public bool Insert(DicItemEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:51:44</remarks>
        public bool Insert(DicItemEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicItem_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
			database.AddInParameter(commandWrapper, "@ItemName", DbType.String, entity.ItemName);
			database.AddInParameter(commandWrapper, "@ItemType", DbType.Int32, entity.ItemType);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@ThirdType", DbType.Int32, entity.ThirdType);
			database.AddInParameter(commandWrapper, "@FourthType", DbType.Int32, entity.FourthType);
			database.AddInParameter(commandWrapper, "@ImageId", DbType.Int32, entity.ImageId);
			database.AddInParameter(commandWrapper, "@LinkId", DbType.Int32, entity.LinkId);

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
        /// <remarks>2015/10/19 10:51:44</remarks>
        public bool Update(DicItemEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:51:44</remarks>
        public bool Update(DicItemEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicItem_Update");
            
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
			database.AddInParameter(commandWrapper, "@ItemName", DbType.String, entity.ItemName);
			database.AddInParameter(commandWrapper, "@ItemType", DbType.Int32, entity.ItemType);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@ThirdType", DbType.Int32, entity.ThirdType);
			database.AddInParameter(commandWrapper, "@FourthType", DbType.Int32, entity.FourthType);
			database.AddInParameter(commandWrapper, "@ImageId", DbType.Int32, entity.ImageId);
			database.AddInParameter(commandWrapper, "@LinkId", DbType.Int32, entity.LinkId);

            
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

