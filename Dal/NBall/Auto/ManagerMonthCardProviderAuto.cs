

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
    
    public partial class ManagerMonthcardProvider
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
		/// 将IDataReader的当前记录读取到ManagerMonthcardEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ManagerMonthcardEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ManagerMonthcardEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.BuyNumber = (System.Int32) reader["BuyNumber"];
            obj.BuyTime = (System.DateTime) reader["BuyTime"];
            obj.DueToTime = (System.DateTime) reader["DueToTime"];
            obj.PrizeDate = (System.DateTime) reader["PrizeDate"];
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
        public List<ManagerMonthcardEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ManagerMonthcardEntity>();
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
        public ManagerMonthcardProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ManagerMonthcardProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>ManagerMonthcardEntity</returns>
        /// <remarks>2016/5/29 17:51:27</remarks>
        public ManagerMonthcardEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerMonthcard_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            ManagerMonthcardEntity obj=null;
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
        /// <returns>ManagerMonthcardEntity列表</returns>
        /// <remarks>2016/5/29 17:51:27</remarks>
        public List<ManagerMonthcardEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerMonthcard_GetAll");
            

            
            List<ManagerMonthcardEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetPrizeList
		
		/// <summary>
        /// GetPrizeList
        /// </summary>
        /// <returns>ManagerMonthcardEntity列表</returns>
        /// <remarks>2016/5/29 17:51:27</remarks>
        public List<ManagerMonthcardEntity> GetPrizeList( System.DateTime dayDate)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerMonthCard_GetPrizeList");
            
			database.AddInParameter(commandWrapper, "@DayDate", DbType.Date, dayDate);

            
            List<ManagerMonthcardEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Insert
		
		/// <summary>
        /// Insert
        /// </summary>
		/// <param name="buyNumber">buyNumber</param>
		/// <param name="buyTime">buyTime</param>
		/// <param name="dueToTime">dueToTime</param>
		/// <param name="prizeDate">prizeDate</param>
		/// <param name="updateTime">updateTime</param>
		/// <param name="rowTime">rowTime</param>
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/29 17:51:29</remarks>
        public bool Insert ( System.Int32 buyNumber, System.DateTime buyTime, System.DateTime dueToTime, System.DateTime prizeDate, System.DateTime updateTime, System.DateTime rowTime,ref  System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerMonthcard_Insert");
            
			database.AddInParameter(commandWrapper, "@BuyNumber", DbType.Int32, buyNumber);
			database.AddInParameter(commandWrapper, "@BuyTime", DbType.DateTime, buyTime);
			database.AddInParameter(commandWrapper, "@DueToTime", DbType.DateTime, dueToTime);
			database.AddInParameter(commandWrapper, "@PrizeDate", DbType.Date, prizeDate);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, updateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, rowTime);
			database.AddParameter(commandWrapper, "@ManagerId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,managerId);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            managerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
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
        /// <remarks>2016/5/29 17:51:29</remarks>
        public bool Insert(ManagerMonthcardEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ManagerMonthcard_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@BuyNumber", DbType.Int32, entity.BuyNumber);
			database.AddInParameter(commandWrapper, "@BuyTime", DbType.DateTime, entity.BuyTime);
			database.AddInParameter(commandWrapper, "@DueToTime", DbType.DateTime, entity.DueToTime);
			database.AddInParameter(commandWrapper, "@PrizeDate", DbType.Date, entity.PrizeDate);
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
        /// <remarks>2016/5/29 17:51:29</remarks>
        public bool Update(ManagerMonthcardEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ManagerMonthcard_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@BuyNumber", DbType.Int32, entity.BuyNumber);
			database.AddInParameter(commandWrapper, "@BuyTime", DbType.DateTime, entity.BuyTime);
			database.AddInParameter(commandWrapper, "@DueToTime", DbType.DateTime, entity.DueToTime);
			database.AddInParameter(commandWrapper, "@PrizeDate", DbType.Date, entity.PrizeDate);
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

