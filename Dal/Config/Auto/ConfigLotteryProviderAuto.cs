

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
    
    public partial class ConfigLotteryProvider
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
		/// 将IDataReader的当前记录读取到ConfigLotteryEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigLotteryEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigLotteryEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Name = (System.String) reader["Name"];
            obj.Type = (System.Int32) reader["Type"];
            obj.SubType = (System.Int32) reader["SubType"];
            obj.MinLevel = (System.Int32) reader["MinLevel"];
            obj.MaxLevel = (System.Int32) reader["MaxLevel"];
            obj.MinVip = (System.Int32) reader["MinVip"];
            obj.MaxVip = (System.Int32) reader["MaxVip"];
            obj.MinTime = (System.DateTime) reader["MinTime"];
            obj.MaxTime = (System.DateTime) reader["MaxTime"];
            obj.Strength = (System.Int32) reader["Strength"];
            obj.IsBinding = (System.Boolean) reader["IsBinding"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigLotteryEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigLotteryEntity>();
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
        public ConfigLotteryProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigLotteryProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigLotteryEntity</returns>
        /// <remarks>2015/10/19 10:46:20</remarks>
        public ConfigLotteryEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLottery_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigLotteryEntity obj=null;
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
        /// <returns>ConfigLotteryEntity列表</returns>
        /// <remarks>2015/10/19 10:46:20</remarks>
        public List<ConfigLotteryEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLottery_GetAll");
            

            
            List<ConfigLotteryEntity> list = null;
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
        /// <remarks>2015/10/19 10:46:21</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigLottery_Delete");
            
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
        /// <remarks>2015/10/19 10:46:21</remarks>
        public bool Insert(ConfigLotteryEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:46:21</remarks>
        public bool Insert(ConfigLotteryEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigLottery_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@MinLevel", DbType.Int32, entity.MinLevel);
			database.AddInParameter(commandWrapper, "@MaxLevel", DbType.Int32, entity.MaxLevel);
			database.AddInParameter(commandWrapper, "@MinVip", DbType.Int32, entity.MinVip);
			database.AddInParameter(commandWrapper, "@MaxVip", DbType.Int32, entity.MaxVip);
			database.AddInParameter(commandWrapper, "@MinTime", DbType.DateTime, entity.MinTime);
			database.AddInParameter(commandWrapper, "@MaxTime", DbType.DateTime, entity.MaxTime);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@IsBinding", DbType.Boolean, entity.IsBinding);

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
        /// <remarks>2015/10/19 10:46:21</remarks>
        public bool Update(ConfigLotteryEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:46:21</remarks>
        public bool Update(ConfigLotteryEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigLottery_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@MinLevel", DbType.Int32, entity.MinLevel);
			database.AddInParameter(commandWrapper, "@MaxLevel", DbType.Int32, entity.MaxLevel);
			database.AddInParameter(commandWrapper, "@MinVip", DbType.Int32, entity.MinVip);
			database.AddInParameter(commandWrapper, "@MaxVip", DbType.Int32, entity.MaxVip);
			database.AddInParameter(commandWrapper, "@MinTime", DbType.DateTime, entity.MinTime);
			database.AddInParameter(commandWrapper, "@MaxTime", DbType.DateTime, entity.MaxTime);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@IsBinding", DbType.Boolean, entity.IsBinding);

            
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

