

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
    
    public partial class LaegueManagerinfoProvider
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
		/// 将IDataReader的当前记录读取到LaegueManagerinfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LaegueManagerinfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LaegueManagerinfoEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.SumScore = (System.Int32) reader["SumScore"];
            obj.ExchangeIds = (System.String) reader["ExchangeIds"];
            obj.ExchangedIds = (System.String) reader["ExchangedIds"];
            obj.EquipmentItems = (System.String) reader["EquipmentItems"];
            obj.EquipmentProperties = (System.String) reader["EquipmentProperties"];
            obj.RefreshDate = (System.DateTime) reader["RefreshDate"];
            obj.RefreshTimes = (System.Int32) reader["RefreshTimes"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.DailyWinCount = (System.Int32) reader["DailyWinCount"];
            obj.DailyWinUpdateTime = (System.DateTime) reader["DailyWinUpdateTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<LaegueManagerinfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LaegueManagerinfoEntity>();
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
        public LaegueManagerinfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LaegueManagerinfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>LaegueManagerinfoEntity</returns>
        /// <remarks>2016/6/16 10:32:23</remarks>
        public LaegueManagerinfoEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LaegueManagerinfo_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            LaegueManagerinfoEntity obj=null;
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
        /// <returns>LaegueManagerinfoEntity列表</returns>
        /// <remarks>2016/6/16 10:32:23</remarks>
        public List<LaegueManagerinfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LaegueManagerinfo_GetAll");
            

            
            List<LaegueManagerinfoEntity> list = null;
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
        /// <remarks>2016/6/16 10:32:23</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LaegueManagerinfo_Delete");
            
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
		
		#region  InsertManager
		
		/// <summary>
        /// InsertManager
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/16 10:32:23</remarks>
        public bool InsertManager ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_League_InsertManager");
            
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
        /// <remarks>2016/6/16 10:32:23</remarks>
        public bool Insert(LaegueManagerinfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LaegueManagerinfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SumScore", DbType.Int32, entity.SumScore);
			database.AddInParameter(commandWrapper, "@ExchangeIds", DbType.AnsiString, entity.ExchangeIds);
			database.AddInParameter(commandWrapper, "@ExchangedIds", DbType.AnsiString, entity.ExchangedIds);
			database.AddInParameter(commandWrapper, "@EquipmentItems", DbType.AnsiString, entity.EquipmentItems);
			database.AddInParameter(commandWrapper, "@EquipmentProperties", DbType.AnsiString, entity.EquipmentProperties);
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.DateTime, entity.RefreshDate);
			database.AddInParameter(commandWrapper, "@RefreshTimes", DbType.Int32, entity.RefreshTimes);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@DailyWinCount", DbType.Int32, entity.DailyWinCount);
			database.AddInParameter(commandWrapper, "@DailyWinUpdateTime", DbType.DateTime, entity.DailyWinUpdateTime);
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
        /// <remarks>2016/6/16 10:32:23</remarks>
        public bool Update(LaegueManagerinfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LaegueManagerinfo_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@SumScore", DbType.Int32, entity.SumScore);
			database.AddInParameter(commandWrapper, "@ExchangeIds", DbType.AnsiString, entity.ExchangeIds);
			database.AddInParameter(commandWrapper, "@ExchangedIds", DbType.AnsiString, entity.ExchangedIds);
			database.AddInParameter(commandWrapper, "@EquipmentItems", DbType.AnsiString, entity.EquipmentItems);
			database.AddInParameter(commandWrapper, "@EquipmentProperties", DbType.AnsiString, entity.EquipmentProperties);
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.DateTime, entity.RefreshDate);
			database.AddInParameter(commandWrapper, "@RefreshTimes", DbType.Int32, entity.RefreshTimes);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@DailyWinCount", DbType.Int32, entity.DailyWinCount);
			database.AddInParameter(commandWrapper, "@DailyWinUpdateTime", DbType.DateTime, entity.DailyWinUpdateTime);

            
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

