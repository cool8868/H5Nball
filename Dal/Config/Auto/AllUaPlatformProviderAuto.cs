

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
    
    public partial class AllUaplatformProvider
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
		/// 将IDataReader的当前记录读取到AllUaplatformEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public AllUaplatformEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new AllUaplatformEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.FactoryCode = (System.String) reader["FactoryCode"];
            obj.PlatformCode = (System.String) reader["PlatformCode"];
            obj.ExchangeRate = (System.Int32) reader["ExchangeRate"];
            obj.CashRate = (System.Int32) reader["CashRate"];
            obj.ChargeUrl = (System.String) reader["ChargeUrl"];
            obj.PlatformUrl = (System.String) reader["PlatformUrl"];
            obj.LoginKey = (System.String) reader["LoginKey"];
            obj.ChargeKey = (System.String) reader["ChargeKey"];
            obj.ClientVersion = (System.String) reader["ClientVersion"];
            obj.UserActionUrl = (System.String) reader["UserActionUrl"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<AllUaplatformEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<AllUaplatformEntity>();
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
        public AllUaplatformProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public AllUaplatformProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>AllUaplatformEntity</returns>
        /// <remarks>2016/5/30 13:18:06</remarks>
        public AllUaplatformEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllUaplatform_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            AllUaplatformEntity obj=null;
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
        /// <returns>AllUaplatformEntity列表</returns>
        /// <remarks>2016/5/30 13:18:06</remarks>
        public List<AllUaplatformEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllUaplatform_GetAll");
            

            
            List<AllUaplatformEntity> list = null;
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
        /// <remarks>2016/5/30 13:18:06</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllUaplatform_Delete");
            
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
        /// <remarks>2016/5/30 13:18:06</remarks>
        public bool Insert(AllUaplatformEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/30 13:18:06</remarks>
        public bool Insert(AllUaplatformEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllUaplatform_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@FactoryCode", DbType.AnsiString, entity.FactoryCode);
			database.AddInParameter(commandWrapper, "@PlatformCode", DbType.AnsiString, entity.PlatformCode);
			database.AddInParameter(commandWrapper, "@ExchangeRate", DbType.Int32, entity.ExchangeRate);
			database.AddInParameter(commandWrapper, "@CashRate", DbType.Int32, entity.CashRate);
			database.AddInParameter(commandWrapper, "@ChargeUrl", DbType.AnsiString, entity.ChargeUrl);
			database.AddInParameter(commandWrapper, "@PlatformUrl", DbType.AnsiString, entity.PlatformUrl);
			database.AddInParameter(commandWrapper, "@LoginKey", DbType.AnsiString, entity.LoginKey);
			database.AddInParameter(commandWrapper, "@ChargeKey", DbType.AnsiString, entity.ChargeKey);
			database.AddInParameter(commandWrapper, "@ClientVersion", DbType.AnsiString, entity.ClientVersion);
			database.AddInParameter(commandWrapper, "@UserActionUrl", DbType.AnsiString, entity.UserActionUrl);

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
        /// <remarks>2016/5/30 13:18:06</remarks>
        public bool Update(AllUaplatformEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/30 13:18:06</remarks>
        public bool Update(AllUaplatformEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllUaplatform_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@FactoryCode", DbType.AnsiString, entity.FactoryCode);
			database.AddInParameter(commandWrapper, "@PlatformCode", DbType.AnsiString, entity.PlatformCode);
			database.AddInParameter(commandWrapper, "@ExchangeRate", DbType.Int32, entity.ExchangeRate);
			database.AddInParameter(commandWrapper, "@CashRate", DbType.Int32, entity.CashRate);
			database.AddInParameter(commandWrapper, "@ChargeUrl", DbType.AnsiString, entity.ChargeUrl);
			database.AddInParameter(commandWrapper, "@PlatformUrl", DbType.AnsiString, entity.PlatformUrl);
			database.AddInParameter(commandWrapper, "@LoginKey", DbType.AnsiString, entity.LoginKey);
			database.AddInParameter(commandWrapper, "@ChargeKey", DbType.AnsiString, entity.ChargeKey);
			database.AddInParameter(commandWrapper, "@ClientVersion", DbType.AnsiString, entity.ClientVersion);
			database.AddInParameter(commandWrapper, "@UserActionUrl", DbType.AnsiString, entity.UserActionUrl);

            
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
