

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
    
    public partial class ConfigSkillcardaskrankProvider
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
		/// 将IDataReader的当前记录读取到ConfigSkillcardaskrankEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigSkillcardaskrankEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigSkillcardaskrankEntity();
			
            obj.NpcId = (System.Int32) reader["NpcId"];
            obj.NpcName = (System.String) reader["NpcName"];
            obj.AskRank = (System.Int32) reader["AskRank"];
            obj.OpenCostGoldItemNo = (System.Int32) reader["OpenCostGoldItemNo"];
            obj.OpenCostType = (System.Int32) reader["OpenCostType"];
            obj.OpenCostValue = (System.Int32) reader["OpenCostValue"];
            obj.CostGoldItemNo = (System.Int32) reader["CostGoldItemNo"];
            obj.CostType = (System.Int32) reader["CostType"];
            obj.CostValue = (System.Int32) reader["CostValue"];
            obj.SuccRate = (System.Decimal) reader["SuccRate"];
            obj.SkillRateMap = (System.String) reader["SkillRateMap"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigSkillcardaskrankEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigSkillcardaskrankEntity>();
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
        public ConfigSkillcardaskrankProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigSkillcardaskrankProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="npcId">npcId</param>
        /// <returns>ConfigSkillcardaskrankEntity</returns>
        /// <remarks>2015/10/19 15:47:49</remarks>
        public ConfigSkillcardaskrankEntity GetById( System.Int32 npcId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSkillcardaskrank_GetById");
            
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Int32, npcId);

            
            ConfigSkillcardaskrankEntity obj=null;
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
        /// <returns>ConfigSkillcardaskrankEntity列表</returns>
        /// <remarks>2015/10/19 15:47:49</remarks>
        public List<ConfigSkillcardaskrankEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSkillcardaskrank_GetAll");
            

            
            List<ConfigSkillcardaskrankEntity> list = null;
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
        /// <remarks>2015/10/19 15:47:49</remarks>
        public bool Delete ( System.Int32 npcId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigSkillcardaskrank_Delete");
            
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Int32, npcId);

            
            
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
        /// <remarks>2015/10/19 15:47:49</remarks>
        public bool Insert(ConfigSkillcardaskrankEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:47:49</remarks>
        public bool Insert(ConfigSkillcardaskrankEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigSkillcardaskrank_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Int32, entity.NpcId);
			database.AddInParameter(commandWrapper, "@NpcName", DbType.String, entity.NpcName);
			database.AddInParameter(commandWrapper, "@AskRank", DbType.Int32, entity.AskRank);
			database.AddInParameter(commandWrapper, "@OpenCostGoldItemNo", DbType.Int32, entity.OpenCostGoldItemNo);
			database.AddInParameter(commandWrapper, "@OpenCostType", DbType.Int32, entity.OpenCostType);
			database.AddInParameter(commandWrapper, "@OpenCostValue", DbType.Int32, entity.OpenCostValue);
			database.AddInParameter(commandWrapper, "@CostGoldItemNo", DbType.Int32, entity.CostGoldItemNo);
			database.AddInParameter(commandWrapper, "@CostType", DbType.Int32, entity.CostType);
			database.AddInParameter(commandWrapper, "@CostValue", DbType.Int32, entity.CostValue);
			database.AddInParameter(commandWrapper, "@SuccRate", DbType.Currency, entity.SuccRate);
			database.AddInParameter(commandWrapper, "@SkillRateMap", DbType.AnsiString, entity.SkillRateMap);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);

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
        /// <remarks>2015/10/19 15:47:50</remarks>
        public bool Update(ConfigSkillcardaskrankEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:47:50</remarks>
        public bool Update(ConfigSkillcardaskrankEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigSkillcardaskrank_Update");
            
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Int32, entity.NpcId);
			database.AddInParameter(commandWrapper, "@NpcName", DbType.String, entity.NpcName);
			database.AddInParameter(commandWrapper, "@AskRank", DbType.Int32, entity.AskRank);
			database.AddInParameter(commandWrapper, "@OpenCostGoldItemNo", DbType.Int32, entity.OpenCostGoldItemNo);
			database.AddInParameter(commandWrapper, "@OpenCostType", DbType.Int32, entity.OpenCostType);
			database.AddInParameter(commandWrapper, "@OpenCostValue", DbType.Int32, entity.OpenCostValue);
			database.AddInParameter(commandWrapper, "@CostGoldItemNo", DbType.Int32, entity.CostGoldItemNo);
			database.AddInParameter(commandWrapper, "@CostType", DbType.Int32, entity.CostType);
			database.AddInParameter(commandWrapper, "@CostValue", DbType.Int32, entity.CostValue);
			database.AddInParameter(commandWrapper, "@SuccRate", DbType.Currency, entity.SuccRate);
			database.AddInParameter(commandWrapper, "@SkillRateMap", DbType.AnsiString, entity.SkillRateMap);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);

            
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

