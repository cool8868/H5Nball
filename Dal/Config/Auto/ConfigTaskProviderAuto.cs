

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
    
    public partial class ConfigTaskProvider
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
		/// 将IDataReader的当前记录读取到ConfigTaskEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigTaskEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigTaskEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Name = (System.String) reader["Name"];
            obj.TaskType = (System.Int32) reader["TaskType"];
            obj.ManagerLevel = (System.Int32) reader["ManagerLevel"];
            obj.ParentId = (System.Int32) reader["ParentId"];
            obj.Times = (System.Int32) reader["Times"];
            obj.PrizeExp = (System.Int32) reader["PrizeExp"];
            obj.PrizeCoin = (System.Int32) reader["PrizeCoin"];
            obj.PrizeItemCode = (System.Int32) reader["PrizeItemCode"];
            obj.OpenFunc = (System.Int32) reader["OpenFunc"];
            obj.GuideBuff = (System.String) reader["GuideBuff"];
            obj.UniqueConstraint = (System.Boolean) reader["UniqueConstraint"];
            obj.Description = (System.String) reader["Description"];
            obj.Tip = (System.String) reader["Tip"];
            obj.NpcIdx = (System.Int32) reader["NpcIdx"];
            obj.RecordPeriod = (System.Int32) reader["RecordPeriod"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigTaskEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigTaskEntity>();
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
        public ConfigTaskProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigTaskProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigTaskEntity</returns>
        /// <remarks>2016/5/18 12:26:18</remarks>
        public ConfigTaskEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigTask_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigTaskEntity obj=null;
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
        /// <returns>ConfigTaskEntity列表</returns>
        /// <remarks>2016/5/18 12:26:18</remarks>
        public List<ConfigTaskEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigTask_GetAll");
            

            
            List<ConfigTaskEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
		/// <summary>
        /// GetAllForCache
        /// </summary>
        /// <returns>ConfigTaskEntity列表</returns>
        /// <remarks>2016/5/18 12:26:18</remarks>
        public List<ConfigTaskEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ConfigTask_GetAllForCache");
            

            
            List<ConfigTaskEntity> list = null;
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
        /// <remarks>2016/5/18 12:26:18</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigTask_Delete");
            
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
        /// <remarks>2016/5/18 12:26:18</remarks>
        public bool Insert(ConfigTaskEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/18 12:26:18</remarks>
        public bool Insert(ConfigTaskEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigTask_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@TaskType", DbType.Int32, entity.TaskType);
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, entity.ManagerLevel);
			database.AddInParameter(commandWrapper, "@ParentId", DbType.Int32, entity.ParentId);
			database.AddInParameter(commandWrapper, "@Times", DbType.Int32, entity.Times);
			database.AddInParameter(commandWrapper, "@PrizeExp", DbType.Int32, entity.PrizeExp);
			database.AddInParameter(commandWrapper, "@PrizeCoin", DbType.Int32, entity.PrizeCoin);
			database.AddInParameter(commandWrapper, "@PrizeItemCode", DbType.Int32, entity.PrizeItemCode);
			database.AddInParameter(commandWrapper, "@OpenFunc", DbType.Int32, entity.OpenFunc);
			database.AddInParameter(commandWrapper, "@GuideBuff", DbType.AnsiString, entity.GuideBuff);
			database.AddInParameter(commandWrapper, "@UniqueConstraint", DbType.Boolean, entity.UniqueConstraint);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);
			database.AddInParameter(commandWrapper, "@Tip", DbType.String, entity.Tip);
			database.AddInParameter(commandWrapper, "@NpcIdx", DbType.Int32, entity.NpcIdx);
			database.AddInParameter(commandWrapper, "@RecordPeriod", DbType.Int32, entity.RecordPeriod);

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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/5/18 12:26:18</remarks>
        public bool Update(ConfigTaskEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/18 12:26:18</remarks>
        public bool Update(ConfigTaskEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigTask_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@TaskType", DbType.Int32, entity.TaskType);
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, entity.ManagerLevel);
			database.AddInParameter(commandWrapper, "@ParentId", DbType.Int32, entity.ParentId);
			database.AddInParameter(commandWrapper, "@Times", DbType.Int32, entity.Times);
			database.AddInParameter(commandWrapper, "@PrizeExp", DbType.Int32, entity.PrizeExp);
			database.AddInParameter(commandWrapper, "@PrizeCoin", DbType.Int32, entity.PrizeCoin);
			database.AddInParameter(commandWrapper, "@PrizeItemCode", DbType.Int32, entity.PrizeItemCode);
			database.AddInParameter(commandWrapper, "@OpenFunc", DbType.Int32, entity.OpenFunc);
			database.AddInParameter(commandWrapper, "@GuideBuff", DbType.AnsiString, entity.GuideBuff);
			database.AddInParameter(commandWrapper, "@UniqueConstraint", DbType.Boolean, entity.UniqueConstraint);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);
			database.AddInParameter(commandWrapper, "@Tip", DbType.String, entity.Tip);
			database.AddInParameter(commandWrapper, "@NpcIdx", DbType.Int32, entity.NpcIdx);
			database.AddInParameter(commandWrapper, "@RecordPeriod", DbType.Int32, entity.RecordPeriod);

            
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

