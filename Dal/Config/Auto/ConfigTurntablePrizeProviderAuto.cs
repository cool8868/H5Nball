

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
    
    public partial class ConfigTurntableprizeProvider
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
		/// 将IDataReader的当前记录读取到ConfigTurntableprizeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigTurntableprizeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigTurntableprizeEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.TurntableId = (System.Int32) reader["TurntableId"];
            obj.TurntableType = (System.Int32) reader["TurntableType"];
            obj.PrizeType = (System.Int32) reader["PrizeType"];
            obj.SubType = (System.Int32) reader["SubType"];
            obj.ItemCount = (System.Int32) reader["ItemCount"];
            obj.SpecialString = (System.String) reader["SpecialString"];
            obj.InitialRate = (System.Int32) reader["InitialRate"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigTurntableprizeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigTurntableprizeEntity>();
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
        public ConfigTurntableprizeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigTurntableprizeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigTurntableprizeEntity</returns>
        /// <remarks>2016/7/12 19:42:45</remarks>
        public ConfigTurntableprizeEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigTurntableprize_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigTurntableprizeEntity obj=null;
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
        /// <returns>ConfigTurntableprizeEntity列表</returns>
        /// <remarks>2016/7/12 19:42:45</remarks>
        public List<ConfigTurntableprizeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigTurntableprize_GetAll");
            

            
            List<ConfigTurntableprizeEntity> list = null;
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
        /// <remarks>2016/7/12 19:42:45</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigTurntableprize_Delete");
            
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
        /// <remarks>2016/7/12 19:42:45</remarks>
        public bool Insert(ConfigTurntableprizeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/7/12 19:42:45</remarks>
        public bool Insert(ConfigTurntableprizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigTurntableprize_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@TurntableId", DbType.Int32, entity.TurntableId);
			database.AddInParameter(commandWrapper, "@TurntableType", DbType.Int32, entity.TurntableType);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, entity.PrizeType);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@ItemCount", DbType.Int32, entity.ItemCount);
			database.AddInParameter(commandWrapper, "@SpecialString", DbType.AnsiString, entity.SpecialString);
			database.AddInParameter(commandWrapper, "@InitialRate", DbType.Int32, entity.InitialRate);

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
        /// <remarks>2016/7/12 19:42:45</remarks>
        public bool Update(ConfigTurntableprizeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/7/12 19:42:45</remarks>
        public bool Update(ConfigTurntableprizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigTurntableprize_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@TurntableId", DbType.Int32, entity.TurntableId);
			database.AddInParameter(commandWrapper, "@TurntableType", DbType.Int32, entity.TurntableType);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, entity.PrizeType);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@ItemCount", DbType.Int32, entity.ItemCount);
			database.AddInParameter(commandWrapper, "@SpecialString", DbType.AnsiString, entity.SpecialString);
			database.AddInParameter(commandWrapper, "@InitialRate", DbType.Int32, entity.InitialRate);

            
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
