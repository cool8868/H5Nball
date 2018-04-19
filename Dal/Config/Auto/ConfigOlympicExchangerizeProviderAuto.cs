

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
    
    public partial class ConfigOlympicexchangerizeProvider
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
		/// 将IDataReader的当前记录读取到ConfigOlympicexchangerizeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigOlympicexchangerizeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigOlympicexchangerizeEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ExchangeId = (System.Int32) reader["ExchangeId"];
            obj.PrizeItemCode = (System.Int32) reader["PrizeItemCode"];
            obj.ItemCount = (System.Int32) reader["ItemCount"];
            obj.TheGoldMedalId = (System.Int32) reader["TheGoldMedalId"];
            obj.TheGoldMedalCount = (System.Int32) reader["TheGoldMedalCount"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigOlympicexchangerizeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigOlympicexchangerizeEntity>();
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
        public ConfigOlympicexchangerizeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigOlympicexchangerizeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigOlympicexchangerizeEntity</returns>
        /// <remarks>2016/7/29 17:34:51</remarks>
        public ConfigOlympicexchangerizeEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigOlympicexchangerize_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigOlympicexchangerizeEntity obj=null;
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
        /// <returns>ConfigOlympicexchangerizeEntity列表</returns>
        /// <remarks>2016/7/29 17:34:51</remarks>
        public List<ConfigOlympicexchangerizeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigOlympicexchangerize_GetAll");
            

            
            List<ConfigOlympicexchangerizeEntity> list = null;
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
        /// <remarks>2016/7/29 17:34:52</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigOlympicexchangerize_Delete");
            
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
        /// <remarks>2016/7/29 17:34:52</remarks>
        public bool Insert(ConfigOlympicexchangerizeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/7/29 17:34:52</remarks>
        public bool Insert(ConfigOlympicexchangerizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigOlympicexchangerize_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ExchangeId", DbType.Int32, entity.ExchangeId);
			database.AddInParameter(commandWrapper, "@PrizeItemCode", DbType.Int32, entity.PrizeItemCode);
			database.AddInParameter(commandWrapper, "@ItemCount", DbType.Int32, entity.ItemCount);
			database.AddInParameter(commandWrapper, "@TheGoldMedalId", DbType.Int32, entity.TheGoldMedalId);
			database.AddInParameter(commandWrapper, "@TheGoldMedalCount", DbType.Int32, entity.TheGoldMedalCount);

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
        /// <remarks>2016/7/29 17:34:52</remarks>
        public bool Update(ConfigOlympicexchangerizeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/7/29 17:34:52</remarks>
        public bool Update(ConfigOlympicexchangerizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigOlympicexchangerize_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ExchangeId", DbType.Int32, entity.ExchangeId);
			database.AddInParameter(commandWrapper, "@PrizeItemCode", DbType.Int32, entity.PrizeItemCode);
			database.AddInParameter(commandWrapper, "@ItemCount", DbType.Int32, entity.ItemCount);
			database.AddInParameter(commandWrapper, "@TheGoldMedalId", DbType.Int32, entity.TheGoldMedalId);
			database.AddInParameter(commandWrapper, "@TheGoldMedalCount", DbType.Int32, entity.TheGoldMedalCount);

            
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
