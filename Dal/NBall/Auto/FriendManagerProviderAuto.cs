

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
    
    public partial class FriendManagerProvider
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
		/// 将IDataReader的当前记录读取到FriendManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public FriendManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new FriendManagerEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.FriendId = (System.Guid) reader["FriendId"];
            obj.Intimacy = (System.Int32) reader["Intimacy"];
            obj.DayIntimacy = (System.Int32) reader["DayIntimacy"];
            obj.HelpTrainCount = (System.Int32) reader["HelpTrainCount"];
            obj.DayHelpTrainCount = (System.Int32) reader["DayHelpTrainCount"];
            obj.DayOpenBoxCount = (System.Int32) reader["DayOpenBoxCount"];
            obj.MatchCount = (System.Int32) reader["MatchCount"];
            obj.DayMatchCount = (System.Int32) reader["DayMatchCount"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
            obj.OpenBoxTime = (System.DateTime) reader["OpenBoxTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<FriendManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<FriendManagerEntity>();
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
        public FriendManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public FriendManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>FriendManagerEntity</returns>
        /// <remarks>2016/6/14 19:35:20</remarks>
        public FriendManagerEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_FriendManager_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            FriendManagerEntity obj=null;
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
		
		#region  GetOne
		
		/// <summary>
        /// GetOne
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="friendId">friendId</param>
        /// <returns>FriendManagerEntity</returns>
        /// <remarks>2016/6/14 19:35:21</remarks>
        public FriendManagerEntity GetOne( System.Guid managerId, System.Guid friendId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Friend_GetOne");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@FriendId", DbType.Guid, friendId);

            
            FriendManagerEntity obj=null;
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
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/14 19:35:21</remarks>
        public bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_FriendManager_Delete");
            
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
		
		#region  AddBlack
		
		/// <summary>
        /// AddBlack
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="blackId">blackId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/14 19:35:21</remarks>
        public bool AddBlack ( System.Guid managerId, System.Guid blackId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Friend_AddBlack");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@BlackId", DbType.Guid, blackId);

            
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
		
		#region  AddFriend
		
		/// <summary>
        /// AddFriend
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="friendId">friendId</param>
		/// <param name="maxCount">maxCount</param>
		/// <param name="maxMessageCode">maxMessageCode</param>
		/// <param name="existsMessageCode">existsMessageCode</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/14 19:35:21</remarks>
        public bool AddFriend ( System.Guid managerId, System.Guid friendId, System.Int32 maxCount, System.Int32 maxMessageCode, System.Int32 existsMessageCode,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Friend_AddFriend");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@FriendId", DbType.Guid, friendId);
			database.AddInParameter(commandWrapper, "@MaxCount", DbType.Int32, maxCount);
			database.AddInParameter(commandWrapper, "@MaxMessageCode", DbType.Int32, maxMessageCode);
			database.AddInParameter(commandWrapper, "@ExistsMessageCode", DbType.Int32, existsMessageCode);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetTotal
		
		/// <summary>
        /// GetTotal
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="totalRecord">totalRecord</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/14 19:35:21</remarks>
        public bool GetTotal ( System.Guid managerId,ref  System.Int32 totalRecord,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Friend_GetTotal");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@TotalRecord", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,totalRecord);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            totalRecord=(System.Int32)database.GetParameterValue(commandWrapper, "@TotalRecord");
            
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
        /// <remarks>2016/6/14 19:35:21</remarks>
        public bool Insert(FriendManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_FriendManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@FriendId", DbType.Guid, entity.FriendId);
			database.AddInParameter(commandWrapper, "@Intimacy", DbType.Int32, entity.Intimacy);
			database.AddInParameter(commandWrapper, "@DayIntimacy", DbType.Int32, entity.DayIntimacy);
			database.AddInParameter(commandWrapper, "@HelpTrainCount", DbType.Int32, entity.HelpTrainCount);
			database.AddInParameter(commandWrapper, "@DayHelpTrainCount", DbType.Int32, entity.DayHelpTrainCount);
			database.AddInParameter(commandWrapper, "@DayOpenBoxCount", DbType.Int32, entity.DayOpenBoxCount);
			database.AddInParameter(commandWrapper, "@MatchCount", DbType.Int32, entity.MatchCount);
			database.AddInParameter(commandWrapper, "@DayMatchCount", DbType.Int32, entity.DayMatchCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@OpenBoxTime", DbType.DateTime, entity.OpenBoxTime);
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
        /// <remarks>2016/6/14 19:35:21</remarks>
        public bool Update(FriendManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_FriendManager_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@FriendId", DbType.Guid, entity.FriendId);
			database.AddInParameter(commandWrapper, "@Intimacy", DbType.Int32, entity.Intimacy);
			database.AddInParameter(commandWrapper, "@DayIntimacy", DbType.Int32, entity.DayIntimacy);
			database.AddInParameter(commandWrapper, "@HelpTrainCount", DbType.Int32, entity.HelpTrainCount);
			database.AddInParameter(commandWrapper, "@DayHelpTrainCount", DbType.Int32, entity.DayHelpTrainCount);
			database.AddInParameter(commandWrapper, "@DayOpenBoxCount", DbType.Int32, entity.DayOpenBoxCount);
			database.AddInParameter(commandWrapper, "@MatchCount", DbType.Int32, entity.MatchCount);
			database.AddInParameter(commandWrapper, "@DayMatchCount", DbType.Int32, entity.DayMatchCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);
			database.AddInParameter(commandWrapper, "@OpenBoxTime", DbType.DateTime, entity.OpenBoxTime);

            
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

