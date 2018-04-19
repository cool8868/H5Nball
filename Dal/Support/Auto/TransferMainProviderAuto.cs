

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
    
    public partial class TransferMainProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Support;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到TransferMainEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TransferMainEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TransferMainEntity();
			
            obj.TransferId = (System.Guid) reader["TransferId"];
            obj.DomainId = (System.Int32) reader["DomainId"];
            obj.ItemCode = (System.Int32) reader["ItemCode"];
            obj.ItemName = (System.String) reader["ItemName"];
            obj.ItemProp = (System.Byte[]) reader["ItemProp"];
            obj.SellName = (System.String) reader["SellName"];
            obj.SellId = (System.Guid) reader["SellId"];
            obj.SellZoneName = (System.String) reader["SellZoneName"];
            obj.Price = (System.Int32) reader["Price"];
            obj.DealEndName = (System.String) reader["DealEndName"];
            obj.DealEndZoneName = (System.String) reader["DealEndZoneName"];
            obj.DealEndPrice = (System.Int32) reader["DealEndPrice"];
            obj.DealEndId = (System.Guid) reader["DealEndId"];
            obj.TransferStartTime = (System.DateTime) reader["TransferStartTime"];
            obj.TransferDuration = (System.DateTime) reader["TransferDuration"];
            obj.Status = (System.Int32) reader["Status"];
            obj.Poundage = (System.Int32) reader["Poundage"];
            obj.GetPrice = (System.Int32) reader["GetPrice"];
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
        public List<TransferMainEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TransferMainEntity>();
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
        public TransferMainProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TransferMainProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="transferId">transferId</param>
        /// <returns>TransferMainEntity</returns>
        /// <remarks>2016/10/26 15:35:21</remarks>
        public TransferMainEntity GetById( System.Guid transferId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TransferMain_GetById");
            
			database.AddInParameter(commandWrapper, "@TransferId", DbType.Guid, transferId);

            
            TransferMainEntity obj=null;
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
        /// <returns>TransferMainEntity列表</returns>
        /// <remarks>2016/10/26 15:35:21</remarks>
        public List<TransferMainEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TransferMain_GetAll");
            

            
            List<TransferMainEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetTransferList
		
		/// <summary>
        /// GetTransferList
        /// </summary>
        /// <returns>TransferMainEntity列表</returns>
        /// <remarks>2016/10/26 15:35:21</remarks>
        public List<TransferMainEntity> GetTransferList( System.Int32 domainId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TransferMain_GetTransferList");
            
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);

            
            List<TransferMainEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetTransferNumber
		
		/// <summary>
        /// GetTransferNumber
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>Int32</returns>
        /// <remarks>2016/10/26 15:35:21</remarks>
        public Int32 GetTransferNumber ( System.Guid managerId)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TransferMain_GetTransferNumber");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="transferId">transferId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/10/26 15:35:21</remarks>
        public bool Delete ( System.Guid transferId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TransferMain_Delete");
            
			database.AddInParameter(commandWrapper, "@TransferId", DbType.Guid, transferId);

            
            
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
        /// <remarks>2016/10/26 15:35:21</remarks>
        public bool Insert(TransferMainEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/10/26 15:35:21</remarks>
        public bool Insert(TransferMainEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TransferMain_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
			database.AddInParameter(commandWrapper, "@ItemName", DbType.AnsiString, entity.ItemName);
			database.AddInParameter(commandWrapper, "@ItemProp", DbType.Binary, entity.ItemProp);
			database.AddInParameter(commandWrapper, "@SellName", DbType.AnsiString, entity.SellName);
			database.AddInParameter(commandWrapper, "@SellId", DbType.Guid, entity.SellId);
			database.AddInParameter(commandWrapper, "@SellZoneName", DbType.AnsiString, entity.SellZoneName);
			database.AddInParameter(commandWrapper, "@Price", DbType.Int32, entity.Price);
			database.AddInParameter(commandWrapper, "@DealEndName", DbType.AnsiString, entity.DealEndName);
			database.AddInParameter(commandWrapper, "@DealEndZoneName", DbType.AnsiString, entity.DealEndZoneName);
			database.AddInParameter(commandWrapper, "@DealEndPrice", DbType.Int32, entity.DealEndPrice);
			database.AddInParameter(commandWrapper, "@DealEndId", DbType.Guid, entity.DealEndId);
			database.AddInParameter(commandWrapper, "@TransferStartTime", DbType.DateTime, entity.TransferStartTime);
			database.AddInParameter(commandWrapper, "@TransferDuration", DbType.DateTime, entity.TransferDuration);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Poundage", DbType.Int32, entity.Poundage);
			database.AddInParameter(commandWrapper, "@GetPrice", DbType.Int32, entity.GetPrice);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@TransferId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.TransferId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.TransferId=(System.Guid)database.GetParameterValue(commandWrapper, "@TransferId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/10/26 15:35:21</remarks>
        public bool Update(TransferMainEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/10/26 15:35:21</remarks>
        public bool Update(TransferMainEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TransferMain_Update");
            
			database.AddInParameter(commandWrapper, "@TransferId", DbType.Guid, entity.TransferId);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
			database.AddInParameter(commandWrapper, "@ItemName", DbType.AnsiString, entity.ItemName);
			database.AddInParameter(commandWrapper, "@ItemProp", DbType.Binary, entity.ItemProp);
			database.AddInParameter(commandWrapper, "@SellName", DbType.AnsiString, entity.SellName);
			database.AddInParameter(commandWrapper, "@SellId", DbType.Guid, entity.SellId);
			database.AddInParameter(commandWrapper, "@SellZoneName", DbType.AnsiString, entity.SellZoneName);
			database.AddInParameter(commandWrapper, "@Price", DbType.Int32, entity.Price);
			database.AddInParameter(commandWrapper, "@DealEndName", DbType.AnsiString, entity.DealEndName);
			database.AddInParameter(commandWrapper, "@DealEndZoneName", DbType.AnsiString, entity.DealEndZoneName);
			database.AddInParameter(commandWrapper, "@DealEndPrice", DbType.Int32, entity.DealEndPrice);
			database.AddInParameter(commandWrapper, "@DealEndId", DbType.Guid, entity.DealEndId);
			database.AddInParameter(commandWrapper, "@TransferStartTime", DbType.DateTime, entity.TransferStartTime);
			database.AddInParameter(commandWrapper, "@TransferDuration", DbType.DateTime, entity.TransferDuration);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Poundage", DbType.Int32, entity.Poundage);
			database.AddInParameter(commandWrapper, "@GetPrice", DbType.Int32, entity.GetPrice);
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

            entity.TransferId=(System.Guid)database.GetParameterValue(commandWrapper, "@TransferId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
