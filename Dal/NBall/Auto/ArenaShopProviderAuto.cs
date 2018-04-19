

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
    
    public partial class ArenaShopProvider
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
		/// 将IDataReader的当前记录读取到ArenaShopEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ArenaShopEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ArenaShopEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ItemString = (System.String) reader["ItemString"];
            obj.ExChangeRecord = (System.String) reader["ExChangeRecord"];
            obj.RefreshTime = (System.DateTime) reader["RefreshTime"];
            obj.RefreshNumber = (System.Int32) reader["RefreshNumber"];
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
        public List<ArenaShopEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ArenaShopEntity>();
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
        public ArenaShopProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ArenaShopProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>ArenaShopEntity</returns>
        /// <remarks>2016/8/22 13:37:09</remarks>
        public ArenaShopEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaShop_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            ArenaShopEntity obj=null;
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
        /// <returns>ArenaShopEntity列表</returns>
        /// <remarks>2016/8/22 13:37:09</remarks>
        public List<ArenaShopEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaShop_GetAll");
            

            
            List<ArenaShopEntity> list = null;
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
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/8/22 13:37:09</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaShop_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
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
        /// <remarks>2016/8/22 13:37:09</remarks>
        public bool Insert(ArenaShopEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ArenaShop_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ItemString", DbType.AnsiString, entity.ItemString);
			database.AddInParameter(commandWrapper, "@ExChangeRecord", DbType.AnsiString, entity.ExChangeRecord);
			database.AddInParameter(commandWrapper, "@RefreshTime", DbType.DateTime, entity.RefreshTime);
			database.AddInParameter(commandWrapper, "@RefreshNumber", DbType.Int32, entity.RefreshNumber);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@ManagerId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.ManagerId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
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
        /// <remarks>2016/8/22 13:37:09</remarks>
        public bool Update(ArenaShopEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ArenaShop_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.AnsiString, entity.ItemString);
			database.AddInParameter(commandWrapper, "@ExChangeRecord", DbType.AnsiString, entity.ExChangeRecord);
			database.AddInParameter(commandWrapper, "@RefreshTime", DbType.DateTime, entity.RefreshTime);
			database.AddInParameter(commandWrapper, "@RefreshNumber", DbType.Int32, entity.RefreshNumber);
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

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
