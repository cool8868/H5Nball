

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
    
    public partial class TxYellowvipProvider
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
		/// 将IDataReader的当前记录读取到TxYellowvipEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TxYellowvipEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TxYellowvipEntity();
			
            obj.Account = (System.String) reader["Account"];
            obj.IsYellowVip = (System.Boolean) reader["IsYellowVip"];
            obj.IsYellowYearVip = (System.Boolean) reader["IsYellowYearVip"];
            obj.IsYellowHighVip = (System.Boolean) reader["IsYellowHighVip"];
            obj.YellowVipLevel = (System.Int32) reader["YellowVipLevel"];
            obj.OpeningTimes = (System.Int32) reader["OpeningTimes"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.ExtraData = (System.String) reader["ExtraData"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<TxYellowvipEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TxYellowvipEntity>();
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
        public TxYellowvipProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TxYellowvipProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="account">account</param>
        /// <returns>TxYellowvipEntity</returns>
        /// <remarks>2016/6/22 14:33:54</remarks>
        public TxYellowvipEntity GetById( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TxYellowvip_GetById");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            TxYellowvipEntity obj=null;
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
        /// <returns>TxYellowvipEntity列表</returns>
        /// <remarks>2016/6/22 14:33:54</remarks>
        public List<TxYellowvipEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TxYellowvip_GetAll");
            

            
            List<TxYellowvipEntity> list = null;
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
		/// <param name="account">account</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/22 14:33:54</remarks>
        public bool Delete ( System.String account,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TxYellowvip_Delete");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
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
		
		#region  Add
		
		/// <summary>
        /// Add
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="isYellowVip">isYellowVip</param>
		/// <param name="isYellowYearVip">isYellowYearVip</param>
		/// <param name="isYellowHighVip">isYellowHighVip</param>
		/// <param name="yellowVipLevel">yellowVipLevel</param>
		/// <param name="extraData">extraData</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/22 14:33:54</remarks>
        public bool Add ( System.String account, System.Boolean isYellowVip, System.Boolean isYellowYearVip, System.Boolean isYellowHighVip, System.Int32 yellowVipLevel, System.String extraData,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TxYellowvip_Add");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@IsYellowVip", DbType.Boolean, isYellowVip);
			database.AddInParameter(commandWrapper, "@IsYellowYearVip", DbType.Boolean, isYellowYearVip);
			database.AddInParameter(commandWrapper, "@IsYellowHighVip", DbType.Boolean, isYellowHighVip);
			database.AddInParameter(commandWrapper, "@YellowVipLevel", DbType.Int32, yellowVipLevel);
			database.AddInParameter(commandWrapper, "@ExtraData", DbType.AnsiString, extraData);

            
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
        /// <remarks>2016/6/22 14:33:54</remarks>
        public bool Insert(TxYellowvipEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TxYellowvip_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@IsYellowVip", DbType.Boolean, entity.IsYellowVip);
			database.AddInParameter(commandWrapper, "@IsYellowYearVip", DbType.Boolean, entity.IsYellowYearVip);
			database.AddInParameter(commandWrapper, "@IsYellowHighVip", DbType.Boolean, entity.IsYellowHighVip);
			database.AddInParameter(commandWrapper, "@YellowVipLevel", DbType.Int32, entity.YellowVipLevel);
			database.AddInParameter(commandWrapper, "@OpeningTimes", DbType.Int32, entity.OpeningTimes);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@ExtraData", DbType.AnsiString, entity.ExtraData);

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
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/22 14:33:54</remarks>
        public bool Update(TxYellowvipEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TxYellowvip_Update");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@IsYellowVip", DbType.Boolean, entity.IsYellowVip);
			database.AddInParameter(commandWrapper, "@IsYellowYearVip", DbType.Boolean, entity.IsYellowYearVip);
			database.AddInParameter(commandWrapper, "@IsYellowHighVip", DbType.Boolean, entity.IsYellowHighVip);
			database.AddInParameter(commandWrapper, "@YellowVipLevel", DbType.Int32, entity.YellowVipLevel);
			database.AddInParameter(commandWrapper, "@OpeningTimes", DbType.Int32, entity.OpeningTimes);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@ExtraData", DbType.AnsiString, entity.ExtraData);

            
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
