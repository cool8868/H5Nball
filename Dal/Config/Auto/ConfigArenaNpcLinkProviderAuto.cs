

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
    
    public partial class ConfigArenanpclinkProvider
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
		/// 将IDataReader的当前记录读取到ConfigArenanpclinkEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigArenanpclinkEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigArenanpclinkEntity();
			
            obj.NpcId = (System.Guid) reader["NpcId"];
            obj.Idx = (System.Int32) reader["Idx"];
            obj.GroupId = (System.Int32) reader["GroupId"];
            obj.Kpi = (System.Int32) reader["Kpi"];
            obj.DanGrading = (System.Int32) reader["DanGrading"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigArenanpclinkEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigArenanpclinkEntity>();
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
        public ConfigArenanpclinkProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigArenanpclinkProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="npcId">npcId</param>
        /// <returns>ConfigArenanpclinkEntity</returns>
        /// <remarks>2016/8/15 14:43:38</remarks>
        public ConfigArenanpclinkEntity GetById( System.Guid npcId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigArenanpclink_GetById");
            
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, npcId);

            
            ConfigArenanpclinkEntity obj=null;
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
        /// <returns>ConfigArenanpclinkEntity列表</returns>
        /// <remarks>2016/8/15 14:43:38</remarks>
        public List<ConfigArenanpclinkEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigArenanpclink_GetAll");
            

            
            List<ConfigArenanpclinkEntity> list = null;
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
		/// <param name="npcId">npcId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/8/15 14:43:38</remarks>
        public bool Delete ( System.Guid npcId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigArenanpclink_Delete");
            
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, npcId);

            
            
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
        /// <remarks>2016/8/15 14:43:38</remarks>
        public bool Insert(ConfigArenanpclinkEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/8/15 14:43:38</remarks>
        public bool Insert(ConfigArenanpclinkEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigArenanpclink_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, entity.GroupId);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, entity.Kpi);
			database.AddInParameter(commandWrapper, "@DanGrading", DbType.Int32, entity.DanGrading);
			database.AddParameter(commandWrapper, "@NpcId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.NpcId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.NpcId=(System.Guid)database.GetParameterValue(commandWrapper, "@NpcId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/8/15 14:43:38</remarks>
        public bool Update(ConfigArenanpclinkEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/8/15 14:43:38</remarks>
        public bool Update(ConfigArenanpclinkEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigArenanpclink_Update");
            
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, entity.NpcId);
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, entity.GroupId);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, entity.Kpi);
			database.AddInParameter(commandWrapper, "@DanGrading", DbType.Int32, entity.DanGrading);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.NpcId=(System.Guid)database.GetParameterValue(commandWrapper, "@NpcId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
