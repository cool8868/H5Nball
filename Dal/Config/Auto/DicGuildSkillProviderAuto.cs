

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
    
    public partial class DicGuildskillProvider
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
		/// 将IDataReader的当前记录读取到DicGuildskillEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicGuildskillEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicGuildskillEntity();
			
            obj.SkillId = (System.Int32) reader["SkillId"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillName = (System.String) reader["SkillName"];
            obj.MaxLevel = (System.Int32) reader["MaxLevel"];
            obj.ReqGuildLevel = (System.Int32) reader["ReqGuildLevel"];
            obj.BuffMemo = (System.String) reader["BuffMemo"];
            obj.BaseValue = (System.Int32) reader["BaseValue"];
            obj.PlusValue = (System.Int32) reader["PlusValue"];
            obj.BuffLastMemo = (System.String) reader["BuffLastMemo"];
            obj.BuyCostActive = (System.Int32) reader["BuyCostActive"];
            obj.Icon = (System.String) reader["Icon"];
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
        public List<DicGuildskillEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicGuildskillEntity>();
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
        public DicGuildskillProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicGuildskillProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="skillCode">skillCode</param>
        /// <returns>DicGuildskillEntity</returns>
        /// <remarks>2015/10/19 15:50:19</remarks>
        public DicGuildskillEntity GetById( System.String skillCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicGuildskill_GetById");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);

            
            DicGuildskillEntity obj=null;
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
        /// <returns>DicGuildskillEntity列表</returns>
        /// <remarks>2015/10/19 15:50:19</remarks>
        public List<DicGuildskillEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicGuildskill_GetAll");
            

            
            List<DicGuildskillEntity> list = null;
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
		/// <param name="skillCode">skillCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 15:50:19</remarks>
        public bool Delete ( System.String skillCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicGuildskill_Delete");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);

            
            
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
        /// <remarks>2015/10/19 15:50:19</remarks>
        public bool Insert(DicGuildskillEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:50:19</remarks>
        public bool Insert(DicGuildskillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicGuildskill_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@MaxLevel", DbType.Int32, entity.MaxLevel);
			database.AddInParameter(commandWrapper, "@ReqGuildLevel", DbType.Int32, entity.ReqGuildLevel);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@BaseValue", DbType.Int32, entity.BaseValue);
			database.AddInParameter(commandWrapper, "@PlusValue", DbType.Int32, entity.PlusValue);
			database.AddInParameter(commandWrapper, "@BuffLastMemo", DbType.String, entity.BuffLastMemo);
			database.AddInParameter(commandWrapper, "@BuyCostActive", DbType.Int32, entity.BuyCostActive);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
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
        /// <remarks>2015/10/19 15:50:19</remarks>
        public bool Update(DicGuildskillEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:50:19</remarks>
        public bool Update(DicGuildskillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicGuildskill_Update");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@MaxLevel", DbType.Int32, entity.MaxLevel);
			database.AddInParameter(commandWrapper, "@ReqGuildLevel", DbType.Int32, entity.ReqGuildLevel);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@BaseValue", DbType.Int32, entity.BaseValue);
			database.AddInParameter(commandWrapper, "@PlusValue", DbType.Int32, entity.PlusValue);
			database.AddInParameter(commandWrapper, "@BuffLastMemo", DbType.String, entity.BuffLastMemo);
			database.AddInParameter(commandWrapper, "@BuyCostActive", DbType.Int32, entity.BuyCostActive);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
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

