

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
    
    public partial class TurntableManagerProvider
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
		/// 将IDataReader的当前记录读取到TurntableManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TurntableManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TurntableManagerEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.LuckyCoin = (System.Int32) reader["LuckyCoin"];
            obj.GiveLuckyCoin = (System.Int32) reader["GiveLuckyCoin"];
            obj.DayProduceLuckyCoin = (System.Int32) reader["DayProduceLuckyCoin"];
            obj.TurntableType = (System.Int32) reader["TurntableType"];
            obj.DetailsString = (System.Byte[]) reader["DetailsString"];
            obj.ResetNumber = (System.String) reader["ResetNumber"];
            obj.RefreshDate = (System.DateTime) reader["RefreshDate"];
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
        public List<TurntableManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TurntableManagerEntity>();
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
        public TurntableManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TurntableManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>TurntableManagerEntity</returns>
        /// <remarks>2016/7/29 15:49:15</remarks>
        public TurntableManagerEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TurntableManager_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            TurntableManagerEntity obj=null;
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
        /// <returns>TurntableManagerEntity列表</returns>
        /// <remarks>2016/7/29 15:49:15</remarks>
        public List<TurntableManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TurntableManager_GetAll");
            

            
            List<TurntableManagerEntity> list = null;
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
        /// <remarks>2016/7/29 15:49:15</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TurntableManager_Delete");
            
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
		
		#region  AddLuckyCoin
		
		/// <summary>
        /// AddLuckyCoin
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="addLuckyCoinNumber">addLuckyCoinNumber</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/29 15:49:15</remarks>
        public bool AddLuckyCoin ( System.Guid managerId, System.Int32 addLuckyCoinNumber,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TurntableManager_AddLuckyCoin");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@AddLuckyCoinNumber", DbType.Int32, addLuckyCoinNumber);

            
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
		
		#region  AddSystemProduceLuckyCoin
		
		/// <summary>
        /// AddSystemProduceLuckyCoin
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="isAddSuccess">isAddSuccess</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/29 15:49:15</remarks>
        public bool AddSystemProduceLuckyCoin ( System.Guid managerId,ref  System.Boolean isAddSuccess,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TurntableManger_AddSystemProduceLuckyCoin");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@IsAddSuccess", DbType.Boolean, ParameterDirection.InputOutput,"",DataRowVersion.Current,isAddSuccess);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            isAddSuccess=(System.Boolean)database.GetParameterValue(commandWrapper, "@IsAddSuccess");
            
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
        /// <remarks>2016/7/29 15:49:15</remarks>
        public bool Insert(TurntableManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TurntableManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@LuckyCoin", DbType.Int32, entity.LuckyCoin);
			database.AddInParameter(commandWrapper, "@GiveLuckyCoin", DbType.Int32, entity.GiveLuckyCoin);
			database.AddInParameter(commandWrapper, "@DayProduceLuckyCoin", DbType.Int32, entity.DayProduceLuckyCoin);
			database.AddInParameter(commandWrapper, "@TurntableType", DbType.Int32, entity.TurntableType);
			database.AddInParameter(commandWrapper, "@DetailsString", DbType.Binary, entity.DetailsString);
			database.AddInParameter(commandWrapper, "@ResetNumber", DbType.AnsiString, entity.ResetNumber);
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.Date, entity.RefreshDate);
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
        /// <remarks>2016/7/29 15:49:15</remarks>
        public bool Update(TurntableManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TurntableManager_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@LuckyCoin", DbType.Int32, entity.LuckyCoin);
			database.AddInParameter(commandWrapper, "@GiveLuckyCoin", DbType.Int32, entity.GiveLuckyCoin);
			database.AddInParameter(commandWrapper, "@DayProduceLuckyCoin", DbType.Int32, entity.DayProduceLuckyCoin);
			database.AddInParameter(commandWrapper, "@TurntableType", DbType.Int32, entity.TurntableType);
			database.AddInParameter(commandWrapper, "@DetailsString", DbType.Binary, entity.DetailsString);
			database.AddInParameter(commandWrapper, "@ResetNumber", DbType.AnsiString, entity.ResetNumber);
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.Date, entity.RefreshDate);
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
