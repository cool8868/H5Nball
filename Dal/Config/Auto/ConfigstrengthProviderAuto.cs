

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
    
    public partial class ConfigStrengthProvider
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
		/// 将IDataReader的当前记录读取到ConfigStrengthEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigStrengthEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigStrengthEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.CardLevel = (System.Int32) reader["CardLevel"];
            obj.Source = (System.Int32) reader["Source"];
            obj.Target = (System.Int32) reader["Target"];
            obj.Result = (System.Int32) reader["Result"];
            obj.Rate = (System.Int32) reader["Rate"];
            obj.Coin = (System.Int32) reader["Coin"];
            obj.ProtectPoint = (System.Int32) reader["ProtectPoint"];
            obj.ShowRate = (System.Int32) reader["ShowRate"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigStrengthEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigStrengthEntity>();
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
        public ConfigStrengthProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigStrengthProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigStrengthEntity</returns>
        /// <remarks>2015/10/19 15:48:32</remarks>
        public ConfigStrengthEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigStrength_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigStrengthEntity obj=null;
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
        /// <returns>ConfigStrengthEntity列表</returns>
        /// <remarks>2015/10/19 15:48:32</remarks>
        public List<ConfigStrengthEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigStrength_GetAll");
            

            
            List<ConfigStrengthEntity> list = null;
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
        /// <remarks>2015/10/19 15:48:32</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigStrength_Delete");
            
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
        /// <remarks>2015/10/19 15:48:32</remarks>
        public bool Insert(ConfigStrengthEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:48:32</remarks>
        public bool Insert(ConfigStrengthEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigStrength_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@CardLevel", DbType.Int32, entity.CardLevel);
			database.AddInParameter(commandWrapper, "@Source", DbType.Int32, entity.Source);
			database.AddInParameter(commandWrapper, "@Target", DbType.Int32, entity.Target);
			database.AddInParameter(commandWrapper, "@Result", DbType.Int32, entity.Result);
			database.AddInParameter(commandWrapper, "@Rate", DbType.Int32, entity.Rate);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, entity.Coin);
			database.AddInParameter(commandWrapper, "@ProtectPoint", DbType.Int32, entity.ProtectPoint);
			database.AddInParameter(commandWrapper, "@ShowRate", DbType.Int32, entity.ShowRate);

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
        /// <remarks>2015/10/19 15:48:32</remarks>
        public bool Update(ConfigStrengthEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:48:32</remarks>
        public bool Update(ConfigStrengthEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigStrength_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@CardLevel", DbType.Int32, entity.CardLevel);
			database.AddInParameter(commandWrapper, "@Source", DbType.Int32, entity.Source);
			database.AddInParameter(commandWrapper, "@Target", DbType.Int32, entity.Target);
			database.AddInParameter(commandWrapper, "@Result", DbType.Int32, entity.Result);
			database.AddInParameter(commandWrapper, "@Rate", DbType.Int32, entity.Rate);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, entity.Coin);
			database.AddInParameter(commandWrapper, "@ProtectPoint", DbType.Int32, entity.ProtectPoint);
			database.AddInParameter(commandWrapper, "@ShowRate", DbType.Int32, entity.ShowRate);

            
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

