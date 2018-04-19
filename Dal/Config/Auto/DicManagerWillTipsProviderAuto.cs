

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
    
    public partial class DicManagerwilltipsProvider
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
		/// 将IDataReader的当前记录读取到DicManagerwilltipsEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicManagerwilltipsEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicManagerwilltipsEntity();
			
            obj.SkillId = (System.Int32) reader["SkillId"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillName = (System.String) reader["SkillName"];
            obj.WillRank = (System.Int32) reader["WillRank"];
            obj.ActType = (System.String) reader["ActType"];
            obj.DriveFlag = (System.Int32) reader["DriveFlag"];
            obj.DriveFlagMemo = (System.String) reader["DriveFlagMemo"];
            obj.BuffMemo = (System.String) reader["BuffMemo"];
            obj.BuffArg = (System.Decimal) reader["BuffArg"];
            obj.BuffArg2 = (System.Decimal) reader["BuffArg2"];
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
        public List<DicManagerwilltipsEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicManagerwilltipsEntity>();
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
        public DicManagerwilltipsProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicManagerwilltipsProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="skillCode">skillCode</param>
        /// <returns>DicManagerwilltipsEntity</returns>
        /// <remarks>2015/10/19 16:24:59</remarks>
        public DicManagerwilltipsEntity GetById( System.String skillCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicManagerwilltips_GetById");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);

            
            DicManagerwilltipsEntity obj=null;
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
        /// <returns>DicManagerwilltipsEntity列表</returns>
        /// <remarks>2015/10/19 16:24:59</remarks>
        public List<DicManagerwilltipsEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicManagerwilltips_GetAll");
            

            
            List<DicManagerwilltipsEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region Insert
		
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 16:24:59</remarks>
        public bool Insert(DicManagerwilltipsEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 16:24:59</remarks>
        public bool Insert(DicManagerwilltipsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicManagerwilltips_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@WillRank", DbType.Int32, entity.WillRank);
			database.AddInParameter(commandWrapper, "@ActType", DbType.String, entity.ActType);
			database.AddInParameter(commandWrapper, "@DriveFlag", DbType.Int32, entity.DriveFlag);
			database.AddInParameter(commandWrapper, "@DriveFlagMemo", DbType.String, entity.DriveFlagMemo);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.Currency, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@BuffArg2", DbType.Currency, entity.BuffArg2);
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
        /// <remarks>2015/10/19 16:24:59</remarks>
        public bool Update(DicManagerwilltipsEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 16:24:59</remarks>
        public bool Update(DicManagerwilltipsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicManagerwilltips_Update");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@WillRank", DbType.Int32, entity.WillRank);
			database.AddInParameter(commandWrapper, "@ActType", DbType.String, entity.ActType);
			database.AddInParameter(commandWrapper, "@DriveFlag", DbType.Int32, entity.DriveFlag);
			database.AddInParameter(commandWrapper, "@DriveFlagMemo", DbType.String, entity.DriveFlagMemo);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.Currency, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@BuffArg2", DbType.Currency, entity.BuffArg2);
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

