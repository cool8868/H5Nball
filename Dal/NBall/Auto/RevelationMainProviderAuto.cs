

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
    
    public partial class RevelationMainProvider
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
		/// 将IDataReader的当前记录读取到RevelationMainEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public RevelationMainEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new RevelationMainEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ChallengesNums = (System.Int32) reader["ChallengesNums"];
            obj.BuyTheNumber = (System.Int32) reader["BuyTheNumber"];
            obj.Courage = (System.Int32) reader["Courage"];
            obj.OnHookCD = (System.DateTime) reader["OnHookCD"];
            obj.FailCD = (System.DateTime) reader["FailCD"];
            obj.RefreshTime = (System.DateTime) reader["RefreshTime"];
            obj.States = (System.Int32) reader["States"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<RevelationMainEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<RevelationMainEntity>();
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
        public RevelationMainProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public RevelationMainProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>RevelationMainEntity</returns>
        /// <remarks>2014/10/16 17:23:13</remarks>
        public RevelationMainEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationMain_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            RevelationMainEntity obj=null;
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
        /// <returns>RevelationMainEntity列表</returns>
        /// <remarks>2014/10/16 17:23:13</remarks>
        public List<RevelationMainEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationMain_GetAll");
            

            
            List<RevelationMainEntity> list = null;
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
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/10/16 17:23:13</remarks>
        public bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationMain_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
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
		
		#region  C_RevelationEverDay
		
		/// <summary>
        /// C_RevelationEverDay
        /// </summary>
		/// <param name="managerid">managerid</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/10/16 17:23:13</remarks>
        public bool C_RevelationEverDay ( System.Guid managerid,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_RevelationEverDay");
            
			database.AddInParameter(commandWrapper, "@managerid", DbType.Guid, managerid);

            
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
        /// <remarks>2014/10/16 17:23:13</remarks>
        public bool Insert(RevelationMainEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationMain_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ChallengesNums", DbType.Int32, entity.ChallengesNums);
			database.AddInParameter(commandWrapper, "@BuyTheNumber", DbType.Int32, entity.BuyTheNumber);
			database.AddInParameter(commandWrapper, "@Courage", DbType.Int32, entity.Courage);
			database.AddInParameter(commandWrapper, "@OnHookCD", DbType.DateTime, entity.OnHookCD);
			database.AddInParameter(commandWrapper, "@FailCD", DbType.DateTime, entity.FailCD);
			database.AddInParameter(commandWrapper, "@RefreshTime", DbType.Date, entity.RefreshTime);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
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
        /// <remarks>2014/10/16 17:23:13</remarks>
        public bool Update(RevelationMainEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationMain_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ChallengesNums", DbType.Int32, entity.ChallengesNums);
			database.AddInParameter(commandWrapper, "@BuyTheNumber", DbType.Int32, entity.BuyTheNumber);
			database.AddInParameter(commandWrapper, "@Courage", DbType.Int32, entity.Courage);
			database.AddInParameter(commandWrapper, "@OnHookCD", DbType.DateTime, entity.OnHookCD);
			database.AddInParameter(commandWrapper, "@FailCD", DbType.DateTime, entity.FailCD);
			database.AddInParameter(commandWrapper, "@RefreshTime", DbType.Date, entity.RefreshTime);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);

            
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

