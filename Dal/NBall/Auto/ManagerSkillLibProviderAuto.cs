

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
    
    public partial class ManagerskillLibProvider
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
		/// 将IDataReader的当前记录读取到ManagerskillLibEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ManagerskillLibEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ManagerskillLibEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.SyncTalentPoint = (System.Int32) reader["SyncTalentPoint"];
            obj.MaxTalentPoint = (System.Int32) reader["MaxTalentPoint"];
            obj.MaxWillNumber = (System.Int32) reader["MaxWillNumber"];
            obj.TodoTalents = (System.String) reader["TodoTalents"];
            obj.NodoTalents = (System.String) reader["NodoTalents"];
            obj.TodoWills = (System.String) reader["TodoWills"];
            obj.NodoWills = (System.String) reader["NodoWills"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
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
        public List<ManagerskillLibEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ManagerskillLibEntity>();
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
        public ManagerskillLibProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ManagerskillLibProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>ManagerskillLibEntity</returns>
        /// <remarks>2015/10/19 17:26:39</remarks>
        public ManagerskillLibEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerskillLib_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            ManagerskillLibEntity obj=null;
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
        /// <returns>ManagerskillLibEntity列表</returns>
        /// <remarks>2015/10/19 17:26:39</remarks>
        public List<ManagerskillLibEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerskillLib_GetAll");
            

            
            List<ManagerskillLibEntity> list = null;
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
        /// <remarks>2015/10/19 17:26:39</remarks>
        public bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerskillLib_Delete");
            
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
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 17:26:39</remarks>
        public bool Insert(ManagerskillLibEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ManagerskillLib_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SyncTalentPoint", DbType.Int32, entity.SyncTalentPoint);
			database.AddInParameter(commandWrapper, "@MaxTalentPoint", DbType.Int32, entity.MaxTalentPoint);
			database.AddInParameter(commandWrapper, "@MaxWillNumber", DbType.Int32, entity.MaxWillNumber);
			database.AddInParameter(commandWrapper, "@TodoTalents", DbType.AnsiString, entity.TodoTalents);
			database.AddInParameter(commandWrapper, "@NodoTalents", DbType.AnsiString, entity.NodoTalents);
			database.AddInParameter(commandWrapper, "@TodoWills", DbType.AnsiString, entity.TodoWills);
			database.AddInParameter(commandWrapper, "@NodoWills", DbType.AnsiString, entity.NodoWills);
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
        /// <remarks>2015/10/19 17:26:39</remarks>
        public bool Update(ManagerskillLibEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ManagerskillLib_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@SyncTalentPoint", DbType.Int32, entity.SyncTalentPoint);
			database.AddInParameter(commandWrapper, "@MaxTalentPoint", DbType.Int32, entity.MaxTalentPoint);
			database.AddInParameter(commandWrapper, "@MaxWillNumber", DbType.Int32, entity.MaxWillNumber);
			database.AddInParameter(commandWrapper, "@TodoTalents", DbType.AnsiString, entity.TodoTalents);
			database.AddInParameter(commandWrapper, "@NodoTalents", DbType.AnsiString, entity.NodoTalents);
			database.AddInParameter(commandWrapper, "@TodoWills", DbType.AnsiString, entity.TodoWills);
			database.AddInParameter(commandWrapper, "@NodoWills", DbType.AnsiString, entity.NodoWills);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
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

