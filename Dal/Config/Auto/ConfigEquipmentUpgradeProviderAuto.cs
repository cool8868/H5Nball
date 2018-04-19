

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
    
    public partial class ConfigEquipmentupgradeProvider
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
		/// 将IDataReader的当前记录读取到ConfigEquipmentupgradeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigEquipmentupgradeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigEquipmentupgradeEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.EquipQuality = (System.Int32) reader["EquipQuality"];
            obj.SourceLevel = (System.Int32) reader["SourceLevel"];
            obj.TargetLevel = (System.Int32) reader["TargetLevel"];
            obj.FailLevel = (System.Int32) reader["FailLevel"];
            obj.ProtectedLevel = (System.Int32) reader["ProtectedLevel"];
            obj.ProtectedConsume = (System.Int32) reader["ProtectedConsume"];
            obj.PropertyNum = (System.Int32) reader["PropertyNum"];
            obj.Rate = (System.Int32) reader["Rate"];
            obj.Coin = (System.Int32) reader["Coin"];
            obj.ItemCode1 = (System.Int32) reader["ItemCode1"];
            obj.ItemCount1 = (System.Int32) reader["ItemCount1"];
            obj.ItemCode2 = (System.Int32) reader["ItemCode2"];
            obj.ItemCount2 = (System.Int32) reader["ItemCount2"];
            obj.ItemCode3 = (System.Int32) reader["ItemCode3"];
            obj.ItemCount3 = (System.Int32) reader["ItemCount3"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigEquipmentupgradeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigEquipmentupgradeEntity>();
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
        public ConfigEquipmentupgradeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigEquipmentupgradeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigEquipmentupgradeEntity</returns>
        /// <remarks>2015/10/19 10:45:08</remarks>
        public ConfigEquipmentupgradeEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentupgrade_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigEquipmentupgradeEntity obj=null;
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
        /// <returns>ConfigEquipmentupgradeEntity列表</returns>
        /// <remarks>2015/10/19 10:45:08</remarks>
        public List<ConfigEquipmentupgradeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentupgrade_GetAll");
            

            
            List<ConfigEquipmentupgradeEntity> list = null;
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
        /// <remarks>2015/10/19 10:45:08</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentupgrade_Delete");
            
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
		
		#region  Update
		
		/// <summary>
        /// Update
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="equipQuality">equipQuality</param>
		/// <param name="sourceLevel">sourceLevel</param>
		/// <param name="targetLevel">targetLevel</param>
		/// <param name="failLevel">failLevel</param>
		/// <param name="protectedLevel">protectedLevel</param>
		/// <param name="protectedConsume">protectedConsume</param>
		/// <param name="propertyNum">propertyNum</param>
		/// <param name="rate">rate</param>
		/// <param name="coin">coin</param>
		/// <param name="itemCode1">itemCode1</param>
		/// <param name="itemCount1">itemCount1</param>
		/// <param name="itemCode2">itemCode2</param>
		/// <param name="itemCount2">itemCount2</param>
		/// <param name="itemCode3">itemCode3</param>
		/// <param name="itemCount3">itemCount3</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 10:45:08</remarks>
        public bool Update ( System.Int32 idx, System.Int32 equipQuality, System.Int32 sourceLevel, System.Int32 targetLevel, System.Int32 failLevel, System.Int32 protectedLevel, System.Int32 protectedConsume, System.Int32 propertyNum, System.Int32 rate, System.Int32 coin, System.Int32 itemCode1, System.Int32 itemCount1, System.Int32 itemCode2, System.Int32 itemCount2, System.Int32 itemCode3, System.Int32 itemCount3,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentupgrade_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@EquipQuality", DbType.Int32, equipQuality);
			database.AddInParameter(commandWrapper, "@SourceLevel", DbType.Int32, sourceLevel);
			database.AddInParameter(commandWrapper, "@TargetLevel", DbType.Int32, targetLevel);
			database.AddInParameter(commandWrapper, "@FailLevel", DbType.Int32, failLevel);
			database.AddInParameter(commandWrapper, "@ProtectedLevel", DbType.Int32, protectedLevel);
			database.AddInParameter(commandWrapper, "@ProtectedConsume", DbType.Int32, protectedConsume);
			database.AddInParameter(commandWrapper, "@PropertyNum", DbType.Int32, propertyNum);
			database.AddInParameter(commandWrapper, "@Rate", DbType.Int32, rate);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, coin);
			database.AddInParameter(commandWrapper, "@ItemCode1", DbType.Int32, itemCode1);
			database.AddInParameter(commandWrapper, "@ItemCount1", DbType.Int32, itemCount1);
			database.AddInParameter(commandWrapper, "@ItemCode2", DbType.Int32, itemCode2);
			database.AddInParameter(commandWrapper, "@ItemCount2", DbType.Int32, itemCount2);
			database.AddInParameter(commandWrapper, "@ItemCode3", DbType.Int32, itemCode3);
			database.AddInParameter(commandWrapper, "@ItemCount3", DbType.Int32, itemCount3);

            
            
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
        /// <remarks>2015/10/19 10:45:08</remarks>
        public bool Insert(ConfigEquipmentupgradeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:45:08</remarks>
        public bool Insert(ConfigEquipmentupgradeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigEquipmentupgrade_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@EquipQuality", DbType.Int32, entity.EquipQuality);
			database.AddInParameter(commandWrapper, "@SourceLevel", DbType.Int32, entity.SourceLevel);
			database.AddInParameter(commandWrapper, "@TargetLevel", DbType.Int32, entity.TargetLevel);
			database.AddInParameter(commandWrapper, "@FailLevel", DbType.Int32, entity.FailLevel);
			database.AddInParameter(commandWrapper, "@ProtectedLevel", DbType.Int32, entity.ProtectedLevel);
			database.AddInParameter(commandWrapper, "@ProtectedConsume", DbType.Int32, entity.ProtectedConsume);
			database.AddInParameter(commandWrapper, "@PropertyNum", DbType.Int32, entity.PropertyNum);
			database.AddInParameter(commandWrapper, "@Rate", DbType.Int32, entity.Rate);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, entity.Coin);
			database.AddInParameter(commandWrapper, "@ItemCode1", DbType.Int32, entity.ItemCode1);
			database.AddInParameter(commandWrapper, "@ItemCount1", DbType.Int32, entity.ItemCount1);
			database.AddInParameter(commandWrapper, "@ItemCode2", DbType.Int32, entity.ItemCode2);
			database.AddInParameter(commandWrapper, "@ItemCount2", DbType.Int32, entity.ItemCount2);
			database.AddInParameter(commandWrapper, "@ItemCode3", DbType.Int32, entity.ItemCode3);
			database.AddInParameter(commandWrapper, "@ItemCount3", DbType.Int32, entity.ItemCount3);

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
        /// <remarks>2015/10/19 10:45:08</remarks>
        public bool Update(ConfigEquipmentupgradeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:45:08</remarks>
        public bool Update(ConfigEquipmentupgradeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigEquipmentupgrade_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@EquipQuality", DbType.Int32, entity.EquipQuality);
			database.AddInParameter(commandWrapper, "@SourceLevel", DbType.Int32, entity.SourceLevel);
			database.AddInParameter(commandWrapper, "@TargetLevel", DbType.Int32, entity.TargetLevel);
			database.AddInParameter(commandWrapper, "@FailLevel", DbType.Int32, entity.FailLevel);
			database.AddInParameter(commandWrapper, "@ProtectedLevel", DbType.Int32, entity.ProtectedLevel);
			database.AddInParameter(commandWrapper, "@ProtectedConsume", DbType.Int32, entity.ProtectedConsume);
			database.AddInParameter(commandWrapper, "@PropertyNum", DbType.Int32, entity.PropertyNum);
			database.AddInParameter(commandWrapper, "@Rate", DbType.Int32, entity.Rate);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, entity.Coin);
			database.AddInParameter(commandWrapper, "@ItemCode1", DbType.Int32, entity.ItemCode1);
			database.AddInParameter(commandWrapper, "@ItemCount1", DbType.Int32, entity.ItemCount1);
			database.AddInParameter(commandWrapper, "@ItemCode2", DbType.Int32, entity.ItemCode2);
			database.AddInParameter(commandWrapper, "@ItemCount2", DbType.Int32, entity.ItemCount2);
			database.AddInParameter(commandWrapper, "@ItemCode3", DbType.Int32, entity.ItemCode3);
			database.AddInParameter(commandWrapper, "@ItemCount3", DbType.Int32, entity.ItemCount3);

            
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

