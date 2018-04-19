

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
    
    public partial class AllCsdkProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Config;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到AllCsdkEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public AllCsdkEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new AllCsdkEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj._sign = (System.String) reader["_sign"];
            obj.orderId = (System.Int32) reader["orderId"];
            obj.gameOrderId = (System.String) reader["gameOrderId"];
            obj.price = (System.Int32) reader["price"];
            obj.channelAlias = (System.String) reader["channelAlias"];
            obj.playerId = (System.String) reader["playerId"];
            obj.serverId = (System.String) reader["serverId"];
            obj.goodsId = (System.Int32) reader["goodsId"];
            obj.payResult = (System.Int32) reader["payResult"];
            obj._state = (System.String) reader["_state"];
            obj.payTime = (System.DateTime) reader["payTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<AllCsdkEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<AllCsdkEntity>();
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
        public AllCsdkProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public AllCsdkProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>AllCsdkEntity</returns>
        /// <remarks>2016/5/19 14:22:36</remarks>
        public AllCsdkEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllCsdk_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            AllCsdkEntity obj=null;
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
        /// <returns>AllCsdkEntity列表</returns>
        /// <remarks>2016/5/19 14:22:36</remarks>
        public List<AllCsdkEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllCsdk_GetAll");
            

            
            List<AllCsdkEntity> list = null;
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
        /// <remarks>2016/5/19 14:22:36</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllCsdk_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            
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
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/19 14:22:36</remarks>
        public bool Insert(AllCsdkEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/19 14:22:36</remarks>
        public bool Insert(AllCsdkEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllCsdk_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@_sign", DbType.AnsiString, entity._sign);
			database.AddInParameter(commandWrapper, "@orderId", DbType.Int32, entity.orderId);
			database.AddInParameter(commandWrapper, "@gameOrderId", DbType.AnsiString, entity.gameOrderId);
			database.AddInParameter(commandWrapper, "@price", DbType.Int32, entity.price);
			database.AddInParameter(commandWrapper, "@channelAlias", DbType.AnsiString, entity.channelAlias);
			database.AddInParameter(commandWrapper, "@playerId", DbType.AnsiString, entity.playerId);
			database.AddInParameter(commandWrapper, "@serverId", DbType.AnsiString, entity.serverId);
			database.AddInParameter(commandWrapper, "@goodsId", DbType.Int32, entity.goodsId);
			database.AddInParameter(commandWrapper, "@payResult", DbType.Int32, entity.payResult);
			database.AddInParameter(commandWrapper, "@_state", DbType.AnsiString, entity._state);
			database.AddInParameter(commandWrapper, "@payTime", DbType.Date, entity.payTime);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/5/19 14:22:36</remarks>
        public bool Update(AllCsdkEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/19 14:22:36</remarks>
        public bool Update(AllCsdkEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllCsdk_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@_sign", DbType.AnsiString, entity._sign);
			database.AddInParameter(commandWrapper, "@orderId", DbType.Int32, entity.orderId);
			database.AddInParameter(commandWrapper, "@gameOrderId", DbType.AnsiString, entity.gameOrderId);
			database.AddInParameter(commandWrapper, "@price", DbType.Int32, entity.price);
			database.AddInParameter(commandWrapper, "@channelAlias", DbType.AnsiString, entity.channelAlias);
			database.AddInParameter(commandWrapper, "@playerId", DbType.AnsiString, entity.playerId);
			database.AddInParameter(commandWrapper, "@serverId", DbType.AnsiString, entity.serverId);
			database.AddInParameter(commandWrapper, "@goodsId", DbType.Int32, entity.goodsId);
			database.AddInParameter(commandWrapper, "@payResult", DbType.Int32, entity.payResult);
			database.AddInParameter(commandWrapper, "@_state", DbType.AnsiString, entity._state);
			database.AddInParameter(commandWrapper, "@payTime", DbType.Date, entity.payTime);

            
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
