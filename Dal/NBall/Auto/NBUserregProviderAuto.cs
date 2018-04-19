

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
    
    public partial class NbUserregProvider
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
		/// 将IDataReader的当前记录读取到NbUserregEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbUserregEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbUserregEntity();
			
            obj.Account = (System.String) reader["Account"];
            obj.Pwd = (System.String) reader["Pwd"];
            obj.Name = (System.String) reader["Name"];
            obj.Cert = (System.String) reader["Cert"];
            obj.Birthday = (System.DateTime) reader["Birthday"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.LastOnlineMinutes = (System.Int32) reader["LastOnlineMinutes"];
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
        public List<NbUserregEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbUserregEntity>();
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
        public NbUserregProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbUserregProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="account">account</param>
        /// <returns>NbUserregEntity</returns>
        /// <remarks>2016/6/17 10:29:20</remarks>
        public NbUserregEntity GetById( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbUserreg_GetById");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            NbUserregEntity obj=null;
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
        /// <returns>NbUserregEntity列表</returns>
        /// <remarks>2016/6/17 10:29:20</remarks>
        public List<NbUserregEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbUserreg_GetAll");
            

            
            List<NbUserregEntity> list = null;
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
        /// <remarks>2016/6/17 10:29:20</remarks>
        public bool Delete ( System.String account,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbUserreg_Delete");
            
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
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/17 10:29:20</remarks>
        public bool Insert(NbUserregEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbUserreg_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Pwd", DbType.AnsiString, entity.Pwd);
			database.AddInParameter(commandWrapper, "@Name", DbType.AnsiString, entity.Name);
			database.AddInParameter(commandWrapper, "@Cert", DbType.AnsiString, entity.Cert);
			database.AddInParameter(commandWrapper, "@Birthday", DbType.DateTime, entity.Birthday);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@LastOnlineMinutes", DbType.Int32, entity.LastOnlineMinutes);
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
        /// <remarks>2016/6/17 10:29:20</remarks>
        public bool Update(NbUserregEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbUserreg_Update");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Pwd", DbType.AnsiString, entity.Pwd);
			database.AddInParameter(commandWrapper, "@Name", DbType.AnsiString, entity.Name);
			database.AddInParameter(commandWrapper, "@Cert", DbType.AnsiString, entity.Cert);
			database.AddInParameter(commandWrapper, "@Birthday", DbType.DateTime, entity.Birthday);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@LastOnlineMinutes", DbType.Int32, entity.LastOnlineMinutes);
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

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
