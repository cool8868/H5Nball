

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
    
    public partial class GambleHostProvider
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
		/// 将IDataReader的当前记录读取到GambleHostEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public GambleHostEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new GambleHostEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ManagerName = (System.String) reader["ManagerName"];
            obj.TitleId = (System.Guid) reader["TitleId"];
            obj.HostMoney = (System.Int32) reader["HostMoney"];
            obj.TotalMoney = (System.Int32) reader["TotalMoney"];
            obj.AttendPeopleCount = (System.Int32) reader["AttendPeopleCount"];
            obj.HostWinMoney = (System.Int32) reader["HostWinMoney"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<GambleHostEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<GambleHostEntity>();
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
        public GambleHostProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public GambleHostProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetByManagerIdAndTitleId
		
		/// <summary>
        /// GetByManagerIdAndTitleId
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="titleId">titleId</param>
        /// <returns>GambleHostEntity</returns>
        /// <remarks>2016/6/7 15:33:38</remarks>
        public GambleHostEntity GetByManagerIdAndTitleId( System.Guid managerId, System.Guid titleId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHost_GetByManagerIdAndTitleId");
            
			database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@titleId", DbType.Guid, titleId);

            
            GambleHostEntity obj=null;
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
		
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>GambleHostEntity</returns>
        /// <remarks>2016/6/7 15:33:38</remarks>
        public GambleHostEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GambleHost_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            GambleHostEntity obj=null;
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
		
		#region  GetByTitleId
		
		/// <summary>
        /// GetByTitleId
        /// </summary>
        /// <returns>GambleHostEntity列表</returns>
        /// <remarks>2016/6/7 15:33:38</remarks>
        public List<GambleHostEntity> GetByTitleId( System.Guid titleId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHost_GetByTitleId");
            
			database.AddInParameter(commandWrapper, "@titleId", DbType.Guid, titleId);

            
            List<GambleHostEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
		/// <summary>
        /// GetByManagerId
        /// </summary>
        /// <returns>GambleHostEntity列表</returns>
        /// <remarks>2016/6/7 15:33:38</remarks>
        public List<GambleHostEntity> GetByManagerId( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHost_GetByManagerId");
            
			database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);

            
            List<GambleHostEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAll
		
		/// <summary>
        /// GetAll
        /// </summary>
        /// <returns>GambleHostEntity列表</returns>
        /// <remarks>2016/6/7 15:33:38</remarks>
        public List<GambleHostEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GambleHost_GetAll");
            

            
            List<GambleHostEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  AddAttendCount
		
		/// <summary>
        /// AddAttendCount
        /// </summary>
		/// <param name="hostId">hostId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 15:33:38</remarks>
        public bool AddAttendCount ( System.Int32 hostId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHost_AddAttendCount");
            
			database.AddInParameter(commandWrapper, "@hostId", DbType.Int32, hostId);

            
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
		
		#region  InsertOnce
		
		/// <summary>
        /// InsertOnce
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="managerName">managerName</param>
		/// <param name="titleId">titleId</param>
		/// <param name="hostMoney">hostMoney</param>
		/// <param name="totalMoney">totalMoney</param>
		/// <param name="rowTime">rowTime</param>
		/// <param name="idx">idx</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 15:33:38</remarks>
        public bool InsertOnce ( System.Guid managerId, System.String managerName, System.Guid titleId, System.Int32 hostMoney, System.Int32 totalMoney, System.DateTime rowTime,ref  System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHost_InsertOnce");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, managerName);
			database.AddInParameter(commandWrapper, "@TitleId", DbType.Guid, titleId);
			database.AddInParameter(commandWrapper, "@HostMoney", DbType.Int32, hostMoney);
			database.AddInParameter(commandWrapper, "@TotalMoney", DbType.Int32, totalMoney);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, rowTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,idx);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 15:33:38</remarks>
        public bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GambleHost_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);

            
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
        /// <remarks>2016/6/7 15:33:38</remarks>
        public bool Insert(GambleHostEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GambleHost_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@TitleId", DbType.Guid, entity.TitleId);
			database.AddInParameter(commandWrapper, "@HostMoney", DbType.Int32, entity.HostMoney);
			database.AddInParameter(commandWrapper, "@TotalMoney", DbType.Int32, entity.TotalMoney);
			database.AddInParameter(commandWrapper, "@AttendPeopleCount", DbType.Int32, entity.AttendPeopleCount);
			database.AddInParameter(commandWrapper, "@HostWinMoney", DbType.Int32, entity.HostWinMoney);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
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
        /// <remarks>2016/6/7 15:33:38</remarks>
        public bool Update(GambleHostEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GambleHost_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@TitleId", DbType.Guid, entity.TitleId);
			database.AddInParameter(commandWrapper, "@HostMoney", DbType.Int32, entity.HostMoney);
			database.AddInParameter(commandWrapper, "@TotalMoney", DbType.Int32, entity.TotalMoney);
			database.AddInParameter(commandWrapper, "@AttendPeopleCount", DbType.Int32, entity.AttendPeopleCount);
			database.AddInParameter(commandWrapper, "@HostWinMoney", DbType.Int32, entity.HostWinMoney);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
