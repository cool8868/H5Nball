

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
    
    public partial class DicManagerwillProvider
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
		/// 将IDataReader的当前记录读取到DicManagerwillEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicManagerwillEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicManagerwillEntity();
			
            obj.SkillId = (System.Int32) reader["SkillId"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillName = (System.String) reader["SkillName"];
            obj.WillRank = (System.Int32) reader["WillRank"];
            obj.DriveFlag = (System.Int32) reader["DriveFlag"];
            obj.PartMap = (System.String) reader["PartMap"];
            obj.CombSkillCode = (System.String) reader["CombSkillCode"];
            obj.MaxCombLevel = (System.Int32) reader["MaxCombLevel"];
            obj.BuffMemo = (System.String) reader["BuffMemo"];
            obj.BuffArg = (System.Decimal) reader["BuffArg"];
            obj.BuffArg2 = (System.Decimal) reader["BuffArg2"];
            obj.SortNo = (System.Int32) reader["SortNo"];
            obj.DenyFlag = (System.Boolean) reader["DenyFlag"];
            obj.Icon = (System.String) reader["Icon"];
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
        public List<DicManagerwillEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicManagerwillEntity>();
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
        public DicManagerwillProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicManagerwillProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="skillCode">skillCode</param>
        /// <returns>DicManagerwillEntity</returns>
        /// <remarks>2015/10/19 16:24:09</remarks>
        public DicManagerwillEntity GetById( System.String skillCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicManagerwill_GetById");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);

            
            DicManagerwillEntity obj=null;
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
        /// <returns>DicManagerwillEntity列表</returns>
        /// <remarks>2015/10/19 16:24:09</remarks>
        public List<DicManagerwillEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicManagerwill_GetAll");
            

            
            List<DicManagerwillEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
		/// <summary>
        /// GetAllForCache
        /// </summary>
        /// <returns>DicManagerwillEntity列表</returns>
        /// <remarks>2015/10/19 16:24:09</remarks>
        public List<DicManagerwillEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicManagerWill_GetAllForCache");
            

            
            List<DicManagerwillEntity> list = null;
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
        /// <remarks>2015/10/19 16:24:10</remarks>
        public bool Delete ( System.String skillCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicManagerwill_Delete");
            
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
        /// <remarks>2015/10/19 16:24:10</remarks>
        public bool Insert(DicManagerwillEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 16:24:10</remarks>
        public bool Insert(DicManagerwillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicManagerwill_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@WillRank", DbType.Int32, entity.WillRank);
			database.AddInParameter(commandWrapper, "@DriveFlag", DbType.Int32, entity.DriveFlag);
			database.AddInParameter(commandWrapper, "@PartMap", DbType.AnsiString, entity.PartMap);
			database.AddInParameter(commandWrapper, "@CombSkillCode", DbType.AnsiString, entity.CombSkillCode);
			database.AddInParameter(commandWrapper, "@MaxCombLevel", DbType.Int32, entity.MaxCombLevel);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.Currency, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@BuffArg2", DbType.Currency, entity.BuffArg2);
			database.AddInParameter(commandWrapper, "@SortNo", DbType.Int32, entity.SortNo);
			database.AddInParameter(commandWrapper, "@DenyFlag", DbType.Boolean, entity.DenyFlag);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
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
        /// <remarks>2015/10/19 16:24:10</remarks>
        public bool Update(DicManagerwillEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 16:24:10</remarks>
        public bool Update(DicManagerwillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicManagerwill_Update");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@WillRank", DbType.Int32, entity.WillRank);
			database.AddInParameter(commandWrapper, "@DriveFlag", DbType.Int32, entity.DriveFlag);
			database.AddInParameter(commandWrapper, "@PartMap", DbType.AnsiString, entity.PartMap);
			database.AddInParameter(commandWrapper, "@CombSkillCode", DbType.AnsiString, entity.CombSkillCode);
			database.AddInParameter(commandWrapper, "@MaxCombLevel", DbType.Int32, entity.MaxCombLevel);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.Currency, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@BuffArg2", DbType.Currency, entity.BuffArg2);
			database.AddInParameter(commandWrapper, "@SortNo", DbType.Int32, entity.SortNo);
			database.AddInParameter(commandWrapper, "@DenyFlag", DbType.Boolean, entity.DenyFlag);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
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

