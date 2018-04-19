

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
    
    public partial class DicSkillcardtipsProvider
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
		/// 将IDataReader的当前记录读取到DicSkillcardtipsEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicSkillcardtipsEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicSkillcardtipsEntity();
			
            obj.SkillId = (System.Int32) reader["SkillId"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillName = (System.String) reader["SkillName"];
            obj.ActType = (System.Int32) reader["ActType"];
            obj.ActTypeMemo = (System.String) reader["ActTypeMemo"];
            obj.SkillClass = (System.Int32) reader["SkillClass"];
            obj.SkillClassMemo = (System.String) reader["SkillClassMemo"];
            obj.SkillRoot = (System.String) reader["SkillRoot"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.GetLevel = (System.Int32) reader["GetLevel"];
            obj.MaxExp = (System.Int32) reader["MaxExp"];
            obj.MixDiscount = (System.Decimal) reader["MixDiscount"];
            obj.TriggerAction = (System.String) reader["TriggerAction"];
            obj.TriggerRate = (System.String) reader["TriggerRate"];
            obj.CD = (System.String) reader["CD"];
            obj.LastTime = (System.String) reader["LastTime"];
            obj.BuffMemo = (System.String) reader["BuffMemo"];
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
        public List<DicSkillcardtipsEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicSkillcardtipsEntity>();
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
        public DicSkillcardtipsProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicSkillcardtipsProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="skillCode">skillCode</param>
        /// <returns>DicSkillcardtipsEntity</returns>
        /// <remarks>2016/1/21 15:03:42</remarks>
        public DicSkillcardtipsEntity GetById( System.String skillCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillcardtips_GetById");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);

            
            DicSkillcardtipsEntity obj=null;
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
        /// <returns>DicSkillcardtipsEntity列表</returns>
        /// <remarks>2016/1/21 15:03:42</remarks>
        public List<DicSkillcardtipsEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillcardtips_GetAll");
            

            
            List<DicSkillcardtipsEntity> list = null;
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
        /// <remarks>2016/1/21 15:03:42</remarks>
        public bool Insert(DicSkillcardtipsEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/21 15:03:42</remarks>
        public bool Insert(DicSkillcardtipsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkillcardtips_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@ActType", DbType.Int32, entity.ActType);
			database.AddInParameter(commandWrapper, "@ActTypeMemo", DbType.String, entity.ActTypeMemo);
			database.AddInParameter(commandWrapper, "@SkillClass", DbType.Int32, entity.SkillClass);
			database.AddInParameter(commandWrapper, "@SkillClassMemo", DbType.String, entity.SkillClassMemo);
			database.AddInParameter(commandWrapper, "@SkillRoot", DbType.AnsiString, entity.SkillRoot);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@GetLevel", DbType.Int32, entity.GetLevel);
			database.AddInParameter(commandWrapper, "@MaxExp", DbType.Int32, entity.MaxExp);
			database.AddInParameter(commandWrapper, "@MixDiscount", DbType.Decimal, entity.MixDiscount);
			database.AddInParameter(commandWrapper, "@TriggerAction", DbType.String, entity.TriggerAction);
			database.AddInParameter(commandWrapper, "@TriggerRate", DbType.String, entity.TriggerRate);
			database.AddInParameter(commandWrapper, "@CD", DbType.String, entity.CD);
			database.AddInParameter(commandWrapper, "@LastTime", DbType.String, entity.LastTime);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
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
        /// <remarks>2016/1/21 15:03:42</remarks>
        public bool Update(DicSkillcardtipsEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/21 15:03:42</remarks>
        public bool Update(DicSkillcardtipsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkillcardtips_Update");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@ActType", DbType.Int32, entity.ActType);
			database.AddInParameter(commandWrapper, "@ActTypeMemo", DbType.String, entity.ActTypeMemo);
			database.AddInParameter(commandWrapper, "@SkillClass", DbType.Int32, entity.SkillClass);
			database.AddInParameter(commandWrapper, "@SkillClassMemo", DbType.String, entity.SkillClassMemo);
			database.AddInParameter(commandWrapper, "@SkillRoot", DbType.AnsiString, entity.SkillRoot);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@GetLevel", DbType.Int32, entity.GetLevel);
			database.AddInParameter(commandWrapper, "@MaxExp", DbType.Int32, entity.MaxExp);
			database.AddInParameter(commandWrapper, "@MixDiscount", DbType.Decimal, entity.MixDiscount);
			database.AddInParameter(commandWrapper, "@TriggerAction", DbType.String, entity.TriggerAction);
			database.AddInParameter(commandWrapper, "@TriggerRate", DbType.String, entity.TriggerRate);
			database.AddInParameter(commandWrapper, "@CD", DbType.String, entity.CD);
			database.AddInParameter(commandWrapper, "@LastTime", DbType.String, entity.LastTime);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
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

