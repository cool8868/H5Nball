

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
    
    public partial class ConfigCoachupgradeProvider
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
		/// 将IDataReader的当前记录读取到ConfigCoachupgradeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigCoachupgradeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigCoachupgradeEntity();
			
            obj.Level = (System.Int32) reader["Level"];
            obj.UpgradeExp = (System.Int32) reader["UpgradeExp"];
            obj.UpgradeSumExp = (System.Int32) reader["UpgradeSumExp"];
            obj.UpgradeSkillCoin = (System.Int32) reader["UpgradeSkillCoin"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigCoachupgradeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigCoachupgradeEntity>();
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
        public ConfigCoachupgradeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigCoachupgradeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="level">level</param>
        /// <returns>ConfigCoachupgradeEntity</returns>
        /// <remarks>2017/2/22 17:28:37</remarks>
        public ConfigCoachupgradeEntity GetById( System.Int32 level)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCoachupgrade_GetById");
            
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);

            
            ConfigCoachupgradeEntity obj=null;
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
        /// <returns>ConfigCoachupgradeEntity列表</returns>
        /// <remarks>2017/2/22 17:28:37</remarks>
        public List<ConfigCoachupgradeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCoachupgrade_GetAll");
            

            
            List<ConfigCoachupgradeEntity> list = null;
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
		/// <param name="level">level</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2017/2/22 17:28:37</remarks>
        public bool Delete ( System.Int32 level,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCoachupgrade_Delete");
            
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);

            
            
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
        /// <remarks>2017/2/22 17:28:37</remarks>
        public bool Insert(ConfigCoachupgradeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/22 17:28:37</remarks>
        public bool Insert(ConfigCoachupgradeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCoachupgrade_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@UpgradeExp", DbType.Int32, entity.UpgradeExp);
			database.AddInParameter(commandWrapper, "@UpgradeSumExp", DbType.Int32, entity.UpgradeSumExp);
			database.AddInParameter(commandWrapper, "@UpgradeSkillCoin", DbType.Int32, entity.UpgradeSkillCoin);

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
        /// <remarks>2017/2/22 17:28:37</remarks>
        public bool Update(ConfigCoachupgradeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/22 17:28:37</remarks>
        public bool Update(ConfigCoachupgradeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCoachupgrade_Update");
            
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@UpgradeExp", DbType.Int32, entity.UpgradeExp);
			database.AddInParameter(commandWrapper, "@UpgradeSumExp", DbType.Int32, entity.UpgradeSumExp);
			database.AddInParameter(commandWrapper, "@UpgradeSkillCoin", DbType.Int32, entity.UpgradeSkillCoin);

            
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
