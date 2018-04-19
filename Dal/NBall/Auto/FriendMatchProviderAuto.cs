

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
    
    public partial class FriendMatchProvider
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
		/// 将IDataReader的当前记录读取到FriendMatchEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public FriendMatchEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new FriendMatchEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.HomeId = (System.Guid) reader["HomeId"];
            obj.AwayId = (System.Guid) reader["AwayId"];
            obj.HomeName = (System.String) reader["HomeName"];
            obj.AwayName = (System.String) reader["AwayName"];
            obj.HomeScore = (System.Int32) reader["HomeScore"];
            obj.AwayScore = (System.Int32) reader["AwayScore"];
            obj.Intimacy = (System.Int32) reader["Intimacy"];
            obj.IsFriend = (System.Boolean) reader["IsFriend"];
            obj.Status = (System.Int32) reader["Status"];
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
        public List<FriendMatchEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<FriendMatchEntity>();
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
        public FriendMatchProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public FriendMatchProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>FriendMatchEntity</returns>
        /// <remarks>2016/1/26 13:35:38</remarks>
        public FriendMatchEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_FriendMatch_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            FriendMatchEntity obj=null;
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
        /// <returns>FriendMatchEntity列表</returns>
        /// <remarks>2016/1/26 13:35:38</remarks>
        public List<FriendMatchEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_FriendMatch_GetAll");
            

            
            List<FriendMatchEntity> list = null;
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
        /// <remarks>2016/1/26 13:35:38</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_FriendMatch_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
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
        /// <remarks>2016/1/26 13:35:38</remarks>
        public bool Insert(FriendMatchEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_FriendMatch_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
			database.AddInParameter(commandWrapper, "@Intimacy", DbType.Int32, entity.Intimacy);
			database.AddInParameter(commandWrapper, "@IsFriend", DbType.Boolean, entity.IsFriend);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
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
        /// <remarks>2016/1/26 13:35:38</remarks>
        public bool Update(FriendMatchEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_FriendMatch_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, entity.AwayName);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, entity.HomeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, entity.AwayScore);
			database.AddInParameter(commandWrapper, "@Intimacy", DbType.Int32, entity.Intimacy);
			database.AddInParameter(commandWrapper, "@IsFriend", DbType.Boolean, entity.IsFriend);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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

            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

