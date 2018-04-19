

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
    
    public partial class AMovedatatoserverecordProvider
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
		/// 将IDataReader的当前记录读取到AMovedatatoserverecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public AMovedatatoserverecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new AMovedatatoserverecordEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.SourceDbFullName = (System.String) reader["SourceDbFullName"];
            obj.OldZoneName = (System.String) reader["OldZoneName"];
            obj.Account = (System.String) reader["Account"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Name = (System.String) reader["Name"];
            obj.Level = (System.Int32) reader["Level"];
            obj.Mod = (System.Int32) reader["Mod"];
            obj.TargetAccount = (System.String) reader["TargetAccount"];
            obj.NewName = (System.String) reader["NewName"];
            obj.Status = (System.Int32) reader["Status"];
            obj.ReturnValue = (System.Int32) reader["ReturnValue"];
            obj.ReturnMessage = (System.String) reader["ReturnMessage"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.BindCode = (System.Guid) reader["BindCode"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<AMovedatatoserverecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<AMovedatatoserverecordEntity>();
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
        public AMovedatatoserverecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public AMovedatatoserverecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>AMovedatatoserverecordEntity</returns>
        /// <remarks>2015/10/18 16:33:23</remarks>
        public AMovedatatoserverecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AMovedatatoserverecord_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            AMovedatatoserverecordEntity obj=null;
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
		
		#region  GetDataByAccount
		
		/// <summary>
        /// GetDataByAccount
        /// </summary>
        /// <returns>AMovedatatoserverecordEntity列表</returns>
        /// <remarks>2015/10/18 16:33:23</remarks>
        public List<AMovedatatoserverecordEntity> GetDataByAccount( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MoveDataToServeRecord_GetDataByAccount");
            
			database.AddInParameter(commandWrapper, "@account", DbType.AnsiString, account);

            
            List<AMovedatatoserverecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAll
		
		/// <summary>
        /// GetAll
        /// </summary>
        /// <returns>AMovedatatoserverecordEntity列表</returns>
        /// <remarks>2015/10/18 16:33:23</remarks>
        public List<AMovedatatoserverecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AMovedatatoserverecord_GetAll");
            

            
            List<AMovedatatoserverecordEntity> list = null;
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
        /// <remarks>2015/10/18 16:33:23</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AMovedatatoserverecord_Delete");
            
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
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 16:33:23</remarks>
        public bool Insert(AMovedatatoserverecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AMovedatatoserverecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SourceDbFullName", DbType.AnsiString, entity.SourceDbFullName);
			database.AddInParameter(commandWrapper, "@OldZoneName", DbType.String, entity.OldZoneName);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, entity.Mod);
			database.AddInParameter(commandWrapper, "@TargetAccount", DbType.AnsiString, entity.TargetAccount);
			database.AddInParameter(commandWrapper, "@NewName", DbType.String, entity.NewName);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@ReturnValue", DbType.Int32, entity.ReturnValue);
			database.AddInParameter(commandWrapper, "@ReturnMessage", DbType.AnsiString, entity.ReturnMessage);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@BindCode", DbType.Guid, entity.BindCode);
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
        /// <remarks>2015/10/18 16:33:23</remarks>
        public bool Update(AMovedatatoserverecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AMovedatatoserverecord_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@SourceDbFullName", DbType.AnsiString, entity.SourceDbFullName);
			database.AddInParameter(commandWrapper, "@OldZoneName", DbType.String, entity.OldZoneName);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, entity.Mod);
			database.AddInParameter(commandWrapper, "@TargetAccount", DbType.AnsiString, entity.TargetAccount);
			database.AddInParameter(commandWrapper, "@NewName", DbType.String, entity.NewName);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@ReturnValue", DbType.Int32, entity.ReturnValue);
			database.AddInParameter(commandWrapper, "@ReturnMessage", DbType.AnsiString, entity.ReturnMessage);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@BindCode", DbType.Guid, entity.BindCode);

            
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

