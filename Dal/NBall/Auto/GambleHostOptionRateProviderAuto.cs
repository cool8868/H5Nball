

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
    
    public partial class GambleHostoptionrateProvider
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
		/// 将IDataReader的当前记录读取到GambleHostoptionrateEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public GambleHostoptionrateEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new GambleHostoptionrateEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.HostId = (System.Int32) reader["HostId"];
            obj.OptionId = (System.Guid) reader["OptionId"];
            obj.WinRate = (System.Decimal) reader["WinRate"];
            obj.GambleMoney = (System.Int32) reader["GambleMoney"];
            obj.AttendPeopleCount = (System.Int32) reader["AttendPeopleCount"];
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
        public List<GambleHostoptionrateEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<GambleHostoptionrateEntity>();
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
        public GambleHostoptionrateProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public GambleHostoptionrateProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>GambleHostoptionrateEntity</returns>
        /// <remarks>2016/6/7 17:03:12</remarks>
        public GambleHostoptionrateEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GambleHostoptionrate_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            GambleHostoptionrateEntity obj=null;
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
		
		#region  GetByHostId
		
		/// <summary>
        /// GetByHostId
        /// </summary>
        /// <returns>GambleHostoptionrateEntity列表</returns>
        /// <remarks>2016/6/7 17:03:12</remarks>
        public List<GambleHostoptionrateEntity> GetByHostId( System.Int32 hostId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_GambleHostOptionRate_GetByHostId");
            
			database.AddInParameter(commandWrapper, "@hostId", DbType.Int32, hostId);

            
            List<GambleHostoptionrateEntity> list = null;
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
        /// <returns>GambleHostoptionrateEntity列表</returns>
        /// <remarks>2016/6/7 17:03:12</remarks>
        public List<GambleHostoptionrateEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GambleHostoptionrate_GetAll");
            

            
            List<GambleHostoptionrateEntity> list = null;
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
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 17:03:12</remarks>
        public bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GambleHostoptionrate_Delete");
            
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
        /// <remarks>2016/6/7 17:03:12</remarks>
        public bool Insert(GambleHostoptionrateEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GambleHostoptionrate_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@HostId", DbType.Int32, entity.HostId);
			database.AddInParameter(commandWrapper, "@OptionId", DbType.Guid, entity.OptionId);
			database.AddInParameter(commandWrapper, "@WinRate", DbType.Decimal, entity.WinRate);
			database.AddInParameter(commandWrapper, "@GambleMoney", DbType.Int32, entity.GambleMoney);
			database.AddInParameter(commandWrapper, "@AttendPeopleCount", DbType.Int32, entity.AttendPeopleCount);
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
        /// <remarks>2016/6/7 17:03:12</remarks>
        public bool Update(GambleHostoptionrateEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GambleHostoptionrate_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@HostId", DbType.Int32, entity.HostId);
			database.AddInParameter(commandWrapper, "@OptionId", DbType.Guid, entity.OptionId);
			database.AddInParameter(commandWrapper, "@WinRate", DbType.Decimal, entity.WinRate);
			database.AddInParameter(commandWrapper, "@GambleMoney", DbType.Int32, entity.GambleMoney);
			database.AddInParameter(commandWrapper, "@AttendPeopleCount", DbType.Int32, entity.AttendPeopleCount);
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
