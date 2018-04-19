

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
    
    public partial class ConfigEquipmentplusProvider
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
		/// 将IDataReader的当前记录读取到ConfigEquipmentplusEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigEquipmentplusEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigEquipmentplusEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Quality = (System.Int32) reader["Quality"];
            obj.PlusValueMin = (System.Int32) reader["PlusValueMin"];
            obj.PlusValueMax = (System.Int32) reader["PlusValueMax"];
            obj.PlusRateMin = (System.Int32) reader["PlusRateMin"];
            obj.PlusRateMax = (System.Int32) reader["PlusRateMax"];
            obj.SlotMin = (System.Int32) reader["SlotMin"];
            obj.SlotMax = (System.Int32) reader["SlotMax"];
            obj.WashMallCode = (System.Int32) reader["WashMallCode"];
            obj.LockMallCode = (System.Int32) reader["LockMallCode"];
            obj.StarSkillPlusRate = (System.Int32) reader["StarSkillPlusRate"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigEquipmentplusEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigEquipmentplusEntity>();
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
        public ConfigEquipmentplusProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigEquipmentplusProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigEquipmentplusEntity</returns>
        /// <remarks>2015/10/19 10:44:18</remarks>
        public ConfigEquipmentplusEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentplus_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigEquipmentplusEntity obj=null;
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
        /// <returns>ConfigEquipmentplusEntity列表</returns>
        /// <remarks>2015/10/19 10:44:18</remarks>
        public List<ConfigEquipmentplusEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentplus_GetAll");
            

            
            List<ConfigEquipmentplusEntity> list = null;
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
        /// <remarks>2015/10/19 10:44:18</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigEquipmentplus_Delete");
            
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
        /// <remarks>2015/10/19 10:44:18</remarks>
        public bool Insert(ConfigEquipmentplusEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:44:18</remarks>
        public bool Insert(ConfigEquipmentplusEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigEquipmentplus_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Quality", DbType.Int32, entity.Quality);
			database.AddInParameter(commandWrapper, "@PlusValueMin", DbType.Int32, entity.PlusValueMin);
			database.AddInParameter(commandWrapper, "@PlusValueMax", DbType.Int32, entity.PlusValueMax);
			database.AddInParameter(commandWrapper, "@PlusRateMin", DbType.Int32, entity.PlusRateMin);
			database.AddInParameter(commandWrapper, "@PlusRateMax", DbType.Int32, entity.PlusRateMax);
			database.AddInParameter(commandWrapper, "@SlotMin", DbType.Int32, entity.SlotMin);
			database.AddInParameter(commandWrapper, "@SlotMax", DbType.Int32, entity.SlotMax);
			database.AddInParameter(commandWrapper, "@WashMallCode", DbType.Int32, entity.WashMallCode);
			database.AddInParameter(commandWrapper, "@LockMallCode", DbType.Int32, entity.LockMallCode);
			database.AddInParameter(commandWrapper, "@StarSkillPlusRate", DbType.Int32, entity.StarSkillPlusRate);

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
        /// <remarks>2015/10/19 10:44:18</remarks>
        public bool Update(ConfigEquipmentplusEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:44:18</remarks>
        public bool Update(ConfigEquipmentplusEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigEquipmentplus_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Quality", DbType.Int32, entity.Quality);
			database.AddInParameter(commandWrapper, "@PlusValueMin", DbType.Int32, entity.PlusValueMin);
			database.AddInParameter(commandWrapper, "@PlusValueMax", DbType.Int32, entity.PlusValueMax);
			database.AddInParameter(commandWrapper, "@PlusRateMin", DbType.Int32, entity.PlusRateMin);
			database.AddInParameter(commandWrapper, "@PlusRateMax", DbType.Int32, entity.PlusRateMax);
			database.AddInParameter(commandWrapper, "@SlotMin", DbType.Int32, entity.SlotMin);
			database.AddInParameter(commandWrapper, "@SlotMax", DbType.Int32, entity.SlotMax);
			database.AddInParameter(commandWrapper, "@WashMallCode", DbType.Int32, entity.WashMallCode);
			database.AddInParameter(commandWrapper, "@LockMallCode", DbType.Int32, entity.LockMallCode);
			database.AddInParameter(commandWrapper, "@StarSkillPlusRate", DbType.Int32, entity.StarSkillPlusRate);

            
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

