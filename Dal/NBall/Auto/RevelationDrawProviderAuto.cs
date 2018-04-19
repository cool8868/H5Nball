

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
    
    public partial class RevelationDrawProvider
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
		/// 将IDataReader的当前记录读取到RevelationDrawEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public RevelationDrawEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new RevelationDrawEntity();
			
            obj.DrawId = (System.Guid) reader["DrawId"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.MarkId = (System.Int32) reader["MarkId"];
            obj.Schedule = (System.Int32) reader["Schedule"];
            obj.AllItemString = (System.String) reader["AllItemString"];
            obj.PrizeItemString = (System.String) reader["PrizeItemString"];
            obj.OpenNumber = (System.Int32) reader["OpenNumber"];
            obj.Status = (System.Int32) reader["Status"];
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
        public List<RevelationDrawEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<RevelationDrawEntity>();
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
        public RevelationDrawProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public RevelationDrawProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="drawId">drawId</param>
        /// <returns>RevelationDrawEntity</returns>
        /// <remarks>2017/1/12 18:55:15</remarks>
        public RevelationDrawEntity GetById( System.Guid drawId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationDraw_GetById");
            
			database.AddInParameter(commandWrapper, "@DrawId", DbType.Guid, drawId);

            
            RevelationDrawEntity obj=null;
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
        /// <returns>RevelationDrawEntity列表</returns>
        /// <remarks>2017/1/12 18:55:15</remarks>
        public List<RevelationDrawEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationDraw_GetAll");
            

            
            List<RevelationDrawEntity> list = null;
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
		/// <param name="drawId">drawId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2017/1/12 18:55:15</remarks>
        public bool Delete ( System.Guid drawId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationDraw_Delete");
            
			database.AddInParameter(commandWrapper, "@DrawId", DbType.Guid, drawId);

            
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
        /// <remarks>2017/1/12 18:55:15</remarks>
        public bool Insert(RevelationDrawEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationDraw_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@AllItemString", DbType.AnsiString, entity.AllItemString);
			database.AddInParameter(commandWrapper, "@PrizeItemString", DbType.AnsiString, entity.PrizeItemString);
			database.AddInParameter(commandWrapper, "@OpenNumber", DbType.Int32, entity.OpenNumber);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@DrawId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.DrawId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.DrawId=(System.Guid)database.GetParameterValue(commandWrapper, "@DrawId");
            
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
        /// <remarks>2017/1/12 18:55:15</remarks>
        public bool Update(RevelationDrawEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationDraw_Update");
            
			database.AddInParameter(commandWrapper, "@DrawId", DbType.Guid, entity.DrawId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@AllItemString", DbType.AnsiString, entity.AllItemString);
			database.AddInParameter(commandWrapper, "@PrizeItemString", DbType.AnsiString, entity.PrizeItemString);
			database.AddInParameter(commandWrapper, "@OpenNumber", DbType.Int32, entity.OpenNumber);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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

            entity.DrawId=(System.Guid)database.GetParameterValue(commandWrapper, "@DrawId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
