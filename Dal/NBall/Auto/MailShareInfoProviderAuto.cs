

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
    
    public partial class MailshareInfoProvider
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
		/// 将IDataReader的当前记录读取到MailshareInfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public MailshareInfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new MailshareInfoEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Account = (System.String) reader["Account"];
            obj.MailType = (System.Int32) reader["MailType"];
            obj.ContentString = (System.String) reader["ContentString"];
            obj.Attachment = (System.Byte[]) reader["Attachment"];
            obj.HasAttach = (System.Boolean) reader["HasAttach"];
            obj.IsRead = (System.Boolean) reader["IsRead"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.ExpiredTime = (System.DateTime) reader["ExpiredTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<MailshareInfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<MailshareInfoEntity>();
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
        public MailshareInfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public MailshareInfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>MailshareInfoEntity</returns>
        /// <remarks>2015/10/19 14:31:58</remarks>
        public MailshareInfoEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_MailshareInfo_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            MailshareInfoEntity obj=null;
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
        /// <returns>MailshareInfoEntity列表</returns>
        /// <remarks>2015/10/19 14:31:58</remarks>
        public List<MailshareInfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_MailshareInfo_GetAll");
            

            
            List<MailshareInfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByPage
		
		/// <summary>
        /// GetByPage
        /// </summary>
        /// <returns>MailshareInfoEntity列表</returns>
        /// <remarks>2015/10/19 14:31:58</remarks>
        public List<MailshareInfoEntity> GetByPage( System.String account, System.Int32 pageIndex, System.Int32 pageSize,ref  System.Int32 totalCount)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MailShare_GetByPage");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@PageIndex", DbType.Int32, pageIndex);
			database.AddInParameter(commandWrapper, "@PageSize", DbType.Int32, pageSize);
			database.AddParameter(commandWrapper, "@TotalCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,totalCount);

            
            List<MailshareInfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                totalCount=(System.Int32)database.GetParameterValue(commandWrapper, "@TotalCount");
                
            return list;
        }
		
		#endregion		  
		
		#region  GetForAttachmentBatch
		
		/// <summary>
        /// GetForAttachmentBatch
        /// </summary>
        /// <returns>MailshareInfoEntity列表</returns>
        /// <remarks>2015/10/19 14:31:58</remarks>
        public List<MailshareInfoEntity> GetForAttachmentBatch( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MailShare_GetForAttachmentBatch");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            List<MailshareInfoEntity> list = null;
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
		/// <param name="recordIds">recordIds</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 14:31:58</remarks>
        public bool Delete ( System.String account, System.String recordIds,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MailShare_Delete");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@RecordIds", DbType.AnsiString, recordIds);
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
		
		#region  Read
		
		/// <summary>
        /// Read
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="recordId">recordId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 14:31:58</remarks>
        public bool Read ( System.String account, System.Int32 recordId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MailShare_Read");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@RecordId", DbType.Int32, recordId);

            
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
        /// <remarks>2015/10/19 14:31:58</remarks>
        public bool Insert(MailshareInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_MailshareInfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@MailType", DbType.Int32, entity.MailType);
			database.AddInParameter(commandWrapper, "@ContentString", DbType.String, entity.ContentString);
			database.AddInParameter(commandWrapper, "@Attachment", DbType.Binary, entity.Attachment);
			database.AddInParameter(commandWrapper, "@HasAttach", DbType.Boolean, entity.HasAttach);
			database.AddInParameter(commandWrapper, "@IsRead", DbType.Boolean, entity.IsRead);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@ExpiredTime", DbType.DateTime, entity.ExpiredTime);
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
        /// <remarks>2015/10/19 14:31:58</remarks>
        public bool Update(MailshareInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_MailshareInfo_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@MailType", DbType.Int32, entity.MailType);
			database.AddInParameter(commandWrapper, "@ContentString", DbType.String, entity.ContentString);
			database.AddInParameter(commandWrapper, "@Attachment", DbType.Binary, entity.Attachment);
			database.AddInParameter(commandWrapper, "@HasAttach", DbType.Boolean, entity.HasAttach);
			database.AddInParameter(commandWrapper, "@IsRead", DbType.Boolean, entity.IsRead);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@ExpiredTime", DbType.DateTime, entity.ExpiredTime);

            
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

