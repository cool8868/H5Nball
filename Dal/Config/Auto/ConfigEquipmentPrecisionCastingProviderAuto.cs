

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
    
    public partial class ConfigEquipmentprecisioncastingProvider
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
		/// 将IDataReader的当前记录读取到ConfigEquipmentprecisioncastingEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigEquipmentprecisioncastingEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigEquipmentprecisioncastingEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.EquipmentQuality = (System.Int32) reader["EquipmentQuality"];
            obj.PropertyQuality = (System.Int32) reader["PropertyQuality"];
            obj.PropertyType = (System.Int32) reader["PropertyType"];
            obj.RateMin = (System.Int32) reader["RateMin"];
            obj.RateMax = (System.Int32) reader["RateMax"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigEquipmentprecisioncastingEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigEquipmentprecisioncastingEntity>();
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
        public ConfigEquipmentprecisioncastingProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigEquipmentprecisioncastingProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigEquipmentprecisioncastingEntity</returns>
        /// <remarks>2015/10/19 10:44:32</remarks>
        public ConfigEquipmentprecisioncastingEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentprecisioncasting_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigEquipmentprecisioncastingEntity obj=null;
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
        /// <returns>ConfigEquipmentprecisioncastingEntity列表</returns>
        /// <remarks>2015/10/19 10:44:32</remarks>
        public List<ConfigEquipmentprecisioncastingEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentprecisioncasting_GetAll");
            

            
            List<ConfigEquipmentprecisioncastingEntity> list = null;
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
        /// <remarks>2015/10/19 10:44:32</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentprecisioncasting_Delete");
            
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
		/// <param name="equipmentQuality">equipmentQuality</param>
		/// <param name="propertyQuality">propertyQuality</param>
		/// <param name="propertyType">propertyType</param>
		/// <param name="rateMin">rateMin</param>
		/// <param name="rateMax">rateMax</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 10:44:32</remarks>
        public bool Update ( System.Int32 idx, System.Int32 equipmentQuality, System.Int32 propertyQuality, System.Int32 propertyType, System.Int32 rateMin, System.Int32 rateMax,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentprecisioncasting_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@EquipmentQuality", DbType.Int32, equipmentQuality);
			database.AddInParameter(commandWrapper, "@PropertyQuality", DbType.Int32, propertyQuality);
			database.AddInParameter(commandWrapper, "@PropertyType", DbType.Int32, propertyType);
			database.AddInParameter(commandWrapper, "@RateMin", DbType.Int32, rateMin);
			database.AddInParameter(commandWrapper, "@RateMax", DbType.Int32, rateMax);

            
            
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
        /// <remarks>2015/10/19 10:44:32</remarks>
        public bool Insert(ConfigEquipmentprecisioncastingEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:44:32</remarks>
        public bool Insert(ConfigEquipmentprecisioncastingEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigEquipmentprecisioncasting_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@EquipmentQuality", DbType.Int32, entity.EquipmentQuality);
			database.AddInParameter(commandWrapper, "@PropertyQuality", DbType.Int32, entity.PropertyQuality);
			database.AddInParameter(commandWrapper, "@PropertyType", DbType.Int32, entity.PropertyType);
			database.AddInParameter(commandWrapper, "@RateMin", DbType.Int32, entity.RateMin);
			database.AddInParameter(commandWrapper, "@RateMax", DbType.Int32, entity.RateMax);

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
        /// <remarks>2015/10/19 10:44:32</remarks>
        public bool Update(ConfigEquipmentprecisioncastingEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:44:32</remarks>
        public bool Update(ConfigEquipmentprecisioncastingEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigEquipmentprecisioncasting_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@EquipmentQuality", DbType.Int32, entity.EquipmentQuality);
			database.AddInParameter(commandWrapper, "@PropertyQuality", DbType.Int32, entity.PropertyQuality);
			database.AddInParameter(commandWrapper, "@PropertyType", DbType.Int32, entity.PropertyType);
			database.AddInParameter(commandWrapper, "@RateMin", DbType.Int32, entity.RateMin);
			database.AddInParameter(commandWrapper, "@RateMax", DbType.Int32, entity.RateMax);

            
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

