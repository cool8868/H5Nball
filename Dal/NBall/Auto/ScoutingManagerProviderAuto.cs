

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
    
    public partial class ScoutingManagerProvider
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
		/// 将IDataReader的当前记录读取到ScoutingManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ScoutingManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ScoutingManagerEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.CoinLotteryCount = (System.Int32) reader["CoinLotteryCount"];
            obj.CoinTenLotteryCount = (System.Int32) reader["CoinTenLotteryCount"];
            obj.PointLotteryCount = (System.Int32) reader["PointLotteryCount"];
            obj.PointTenLotteryCount = (System.Int32) reader["PointTenLotteryCount"];
            obj.FriendLotteryCount = (System.Int32) reader["FriendLotteryCount"];
            obj.FriendTenLotteryCount = (System.Int32) reader["FriendTenLotteryCount"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.SpecialItemCoin = (System.Int32) reader["SpecialItemCoin"];
            obj.SpecialItemPoint = (System.Int32) reader["SpecialItemPoint"];
            obj.SpecialItemFriend = (System.Int32) reader["SpecialItemFriend"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ScoutingManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ScoutingManagerEntity>();
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
        public ScoutingManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ScoutingManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>ScoutingManagerEntity</returns>
        /// <remarks>2016/9/6 14:33:46</remarks>
        public ScoutingManagerEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ScoutingManager_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            ScoutingManagerEntity obj=null;
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
        /// <returns>ScoutingManagerEntity列表</returns>
        /// <remarks>2016/9/6 14:33:46</remarks>
        public List<ScoutingManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ScoutingManager_GetAll");
            

            
            List<ScoutingManagerEntity> list = null;
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
        /// <remarks>2016/9/6 14:33:46</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ScoutingManager_Delete");
            
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
        /// <remarks>2016/9/6 14:33:46</remarks>
        public bool Insert(ScoutingManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ScoutingManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@CoinLotteryCount", DbType.Int32, entity.CoinLotteryCount);
			database.AddInParameter(commandWrapper, "@CoinTenLotteryCount", DbType.Int32, entity.CoinTenLotteryCount);
			database.AddInParameter(commandWrapper, "@PointLotteryCount", DbType.Int32, entity.PointLotteryCount);
			database.AddInParameter(commandWrapper, "@PointTenLotteryCount", DbType.Int32, entity.PointTenLotteryCount);
			database.AddInParameter(commandWrapper, "@FriendLotteryCount", DbType.Int32, entity.FriendLotteryCount);
			database.AddInParameter(commandWrapper, "@FriendTenLotteryCount", DbType.Int32, entity.FriendTenLotteryCount);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@SpecialItemCoin", DbType.Int32, entity.SpecialItemCoin);
			database.AddInParameter(commandWrapper, "@SpecialItemPoint", DbType.Int32, entity.SpecialItemPoint);
			database.AddInParameter(commandWrapper, "@SpecialItemFriend", DbType.Int32, entity.SpecialItemFriend);
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
        /// <remarks>2016/9/6 14:33:46</remarks>
        public bool Update(ScoutingManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ScoutingManager_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@CoinLotteryCount", DbType.Int32, entity.CoinLotteryCount);
			database.AddInParameter(commandWrapper, "@CoinTenLotteryCount", DbType.Int32, entity.CoinTenLotteryCount);
			database.AddInParameter(commandWrapper, "@PointLotteryCount", DbType.Int32, entity.PointLotteryCount);
			database.AddInParameter(commandWrapper, "@PointTenLotteryCount", DbType.Int32, entity.PointTenLotteryCount);
			database.AddInParameter(commandWrapper, "@FriendLotteryCount", DbType.Int32, entity.FriendLotteryCount);
			database.AddInParameter(commandWrapper, "@FriendTenLotteryCount", DbType.Int32, entity.FriendTenLotteryCount);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@SpecialItemCoin", DbType.Int32, entity.SpecialItemCoin);
			database.AddInParameter(commandWrapper, "@SpecialItemPoint", DbType.Int32, entity.SpecialItemPoint);
			database.AddInParameter(commandWrapper, "@SpecialItemFriend", DbType.Int32, entity.SpecialItemFriend);

            
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
