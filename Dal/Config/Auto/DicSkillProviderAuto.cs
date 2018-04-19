

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
    
    public partial class DicSkillProvider
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
		/// 将IDataReader的当前记录读取到DicSkillEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicSkillEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicSkillEntity();
			
            obj.SkillId = (System.Int32) reader["SkillId"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.SkillName = (System.String) reader["SkillName"];
            obj.BuffSrcType = (System.Int32) reader["BuffSrcType"];
            obj.RefType = (System.String) reader["RefType"];
            obj.RefKey = (System.String) reader["RefKey"];
            obj.RefFlag = (System.String) reader["RefFlag"];
            obj.SkillType = (System.Int32) reader["SkillType"];
            obj.PoolFlag = (System.Int32) reader["PoolFlag"];
            obj.LiveFlag = (System.Int32) reader["LiveFlag"];
            obj.Memo = (System.String) reader["Memo"];
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
        public List<DicSkillEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicSkillEntity>();
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
        public DicSkillProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicSkillProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="skillCode">skillCode</param>
		/// <param name="skillLevel">skillLevel</param>
        /// <returns>DicSkillEntity</returns>
        /// <remarks>2015/10/19 11:11:29</remarks>
        public DicSkillEntity GetById( System.String skillCode, System.Int32 skillLevel)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkill_GetById");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, skillLevel);

            
            DicSkillEntity obj=null;
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
        /// <returns>DicSkillEntity列表</returns>
        /// <remarks>2015/10/19 11:11:29</remarks>
        public List<DicSkillEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkill_GetAll");
            

            
            List<DicSkillEntity> list = null;
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
		/// <param name="skillLevel">skillLevel</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 11:11:29</remarks>
        public bool Delete ( System.String skillCode, System.Int32 skillLevel,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkill_Delete");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, skillLevel);

            
            
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
        /// <remarks>2015/10/19 11:11:29</remarks>
        public bool Insert(DicSkillEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 11:11:29</remarks>
        public bool Insert(DicSkillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkill_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, entity.BuffSrcType);
			database.AddInParameter(commandWrapper, "@RefType", DbType.AnsiString, entity.RefType);
			database.AddInParameter(commandWrapper, "@RefKey", DbType.AnsiString, entity.RefKey);
			database.AddInParameter(commandWrapper, "@RefFlag", DbType.AnsiString, entity.RefFlag);
			database.AddInParameter(commandWrapper, "@SkillType", DbType.Int32, entity.SkillType);
			database.AddInParameter(commandWrapper, "@PoolFlag", DbType.Int32, entity.PoolFlag);
			database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, entity.LiveFlag);
			database.AddInParameter(commandWrapper, "@Memo", DbType.String, entity.Memo);
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
        /// <remarks>2015/10/19 11:11:29</remarks>
        public bool Update(DicSkillEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 11:11:29</remarks>
        public bool Update(DicSkillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkill_Update");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, entity.BuffSrcType);
			database.AddInParameter(commandWrapper, "@RefType", DbType.AnsiString, entity.RefType);
			database.AddInParameter(commandWrapper, "@RefKey", DbType.AnsiString, entity.RefKey);
			database.AddInParameter(commandWrapper, "@RefFlag", DbType.AnsiString, entity.RefFlag);
			database.AddInParameter(commandWrapper, "@SkillType", DbType.Int32, entity.SkillType);
			database.AddInParameter(commandWrapper, "@PoolFlag", DbType.Int32, entity.PoolFlag);
			database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, entity.LiveFlag);
			database.AddInParameter(commandWrapper, "@Memo", DbType.String, entity.Memo);
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

