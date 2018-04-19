

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
    
    public partial class FriendOpenboxrecordProvider
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
		/// 将IDataReader的当前记录读取到FriendOpenboxrecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public FriendOpenboxrecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new FriendOpenboxrecordEntity();
			
            obj.idx = (System.Int32) reader["idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.FriendId = (System.Guid) reader["FriendId"];
            obj.PrizeType = (System.Int32) reader["PrizeType"];
            obj.PrizeItem = (System.Int32) reader["PrizeItem"];
            obj.PrizeCount = (System.Int32) reader["PrizeCount"];
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
        public List<FriendOpenboxrecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<FriendOpenboxrecordEntity>();
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
        public FriendOpenboxrecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public FriendOpenboxrecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>FriendOpenboxrecordEntity</returns>
        /// <remarks>2016/3/23 18:18:11</remarks>
        public FriendOpenboxrecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_FriendOpenboxrecord_GetById");
            
			database.AddInParameter(commandWrapper, "@idx", DbType.Int32, idx);

            
            FriendOpenboxrecordEntity obj=null;
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
        /// <returns>FriendOpenboxrecordEntity列表</returns>
        /// <remarks>2016/3/23 18:18:11</remarks>
        public List<FriendOpenboxrecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_FriendOpenboxrecord_GetAll");
            

            
            List<FriendOpenboxrecordEntity> list = null;
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
        /// <remarks>2016/3/23 18:18:12</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_FriendOpenboxrecord_Delete");
            
			database.AddInParameter(commandWrapper, "@idx", DbType.Int32, idx);

            
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
		
		#region  GetCountByPrizeInfo
		
		/// <summary>
        /// GetCountByPrizeInfo
        /// </summary>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="prizeType">prizeType</param>
		/// <param name="prizeItem">prizeItem</param>
		/// <param name="prizeCount">prizeCount</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/3/23 18:18:12</remarks>
        public bool GetCountByPrizeInfo ( System.DateTime startTime, System.DateTime endTime, System.Int32 prizeType, System.Int32 prizeItem,ref  System.Int32 prizeCount,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_FriendOpenBoxRecord_GetCountByPrizeInfo");
            
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, prizeType);
			database.AddInParameter(commandWrapper, "@PrizeItem", DbType.Int32, prizeItem);
			database.AddParameter(commandWrapper, "@PrizeCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,prizeCount);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            prizeCount=(System.Int32)database.GetParameterValue(commandWrapper, "@PrizeCount");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetCountByPrizeType
		
		/// <summary>
        /// GetCountByPrizeType
        /// </summary>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="prizeType">prizeType</param>
		/// <param name="prizeCount">prizeCount</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/3/23 18:18:12</remarks>
        public bool GetCountByPrizeType ( System.DateTime startTime, System.DateTime endTime, System.Int32 prizeType,ref  System.Int32 prizeCount,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_FriendOpenBoxRecord_GetCountByPrizeType");
            
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, prizeType);
			database.AddParameter(commandWrapper, "@PrizeCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,prizeCount);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            prizeCount=(System.Int32)database.GetParameterValue(commandWrapper, "@PrizeCount");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetCountByManagerAndPrizeType
		
		/// <summary>
        /// GetCountByManagerAndPrizeType
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="prizeType">prizeType</param>
		/// <param name="prizeCount">prizeCount</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/3/23 18:18:12</remarks>
        public bool GetCountByManagerAndPrizeType ( System.Guid managerId, System.DateTime startTime, System.DateTime endTime, System.Int32 prizeType,ref  System.Int32 prizeCount,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_FriendOpenBoxRecord_GetCountByManagerAndPrizeType");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, prizeType);
			database.AddParameter(commandWrapper, "@PrizeCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,prizeCount);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            prizeCount=(System.Int32)database.GetParameterValue(commandWrapper, "@PrizeCount");
            
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
        /// <remarks>2016/3/23 18:18:12</remarks>
        public bool Insert(FriendOpenboxrecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_FriendOpenboxrecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@FriendId", DbType.Guid, entity.FriendId);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, entity.PrizeType);
			database.AddInParameter(commandWrapper, "@PrizeItem", DbType.Int32, entity.PrizeItem);
			database.AddInParameter(commandWrapper, "@PrizeCount", DbType.Int32, entity.PrizeCount);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.idx=(System.Int32)database.GetParameterValue(commandWrapper, "@idx");
            
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
        /// <remarks>2016/3/23 18:18:12</remarks>
        public bool Update(FriendOpenboxrecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_FriendOpenboxrecord_Update");
            
			database.AddInParameter(commandWrapper, "@idx", DbType.Int32, entity.idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@FriendId", DbType.Guid, entity.FriendId);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, entity.PrizeType);
			database.AddInParameter(commandWrapper, "@PrizeItem", DbType.Int32, entity.PrizeItem);
			database.AddInParameter(commandWrapper, "@PrizeCount", DbType.Int32, entity.PrizeCount);
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

            entity.idx=(System.Int32)database.GetParameterValue(commandWrapper, "@idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

