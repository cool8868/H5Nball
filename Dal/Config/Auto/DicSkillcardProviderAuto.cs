

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
    
    public partial class DicSkillcardProvider
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
		/// 将IDataReader的当前记录读取到DicSkillcardEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicSkillcardEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicSkillcardEntity();
			
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.ItemName = (System.String) reader["ItemName"];
            obj.ItemIcon = (System.String) reader["ItemIcon"];
            obj.SkillClass = (System.Int32) reader["SkillClass"];
            obj.SkillRoot = (System.String) reader["SkillRoot"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.GetLevel = (System.Int32) reader["GetLevel"];
            obj.ActType = (System.Int32) reader["ActType"];
            obj.MixExp = (System.Int32) reader["MixExp"];
            obj.MixDiscount = (System.Decimal) reader["MixDiscount"];
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
        public List<DicSkillcardEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicSkillcardEntity>();
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
        public DicSkillcardProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicSkillcardProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="skillCode">skillCode</param>
        /// <returns>DicSkillcardEntity</returns>
        /// <remarks>2016/1/21 16:27:56</remarks>
        public DicSkillcardEntity GetById( System.String skillCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillcard_GetById");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);

            
            DicSkillcardEntity obj=null;
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
        /// <returns>DicSkillcardEntity列表</returns>
        /// <remarks>2016/1/21 16:27:56</remarks>
        public List<DicSkillcardEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillcard_GetAll");
            

            
            List<DicSkillcardEntity> list = null;
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
        /// <returns>DicSkillcardEntity列表</returns>
        /// <remarks>2016/1/21 16:27:56</remarks>
        public List<DicSkillcardEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicSkillCard_GetAllForCache");
            

            
            List<DicSkillcardEntity> list = null;
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
        /// <remarks>2016/1/21 16:27:56</remarks>
        public bool Delete ( System.String skillCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillcard_Delete");
            
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
        /// <remarks>2016/1/21 16:27:56</remarks>
        public bool Insert(DicSkillcardEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/21 16:27:56</remarks>
        public bool Insert(DicSkillcardEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkillcard_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@ItemName", DbType.String, entity.ItemName);
			database.AddInParameter(commandWrapper, "@ItemIcon", DbType.AnsiString, entity.ItemIcon);
			database.AddInParameter(commandWrapper, "@SkillClass", DbType.Int32, entity.SkillClass);
			database.AddInParameter(commandWrapper, "@SkillRoot", DbType.AnsiString, entity.SkillRoot);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@GetLevel", DbType.Int32, entity.GetLevel);
			database.AddInParameter(commandWrapper, "@ActType", DbType.Int32, entity.ActType);
			database.AddInParameter(commandWrapper, "@MixExp", DbType.Int32, entity.MixExp);
			database.AddInParameter(commandWrapper, "@MixDiscount", DbType.Decimal, entity.MixDiscount);
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
        /// <remarks>2016/1/21 16:27:56</remarks>
        public bool Update(DicSkillcardEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/21 16:27:56</remarks>
        public bool Update(DicSkillcardEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkillcard_Update");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@ItemName", DbType.String, entity.ItemName);
			database.AddInParameter(commandWrapper, "@ItemIcon", DbType.AnsiString, entity.ItemIcon);
			database.AddInParameter(commandWrapper, "@SkillClass", DbType.Int32, entity.SkillClass);
			database.AddInParameter(commandWrapper, "@SkillRoot", DbType.AnsiString, entity.SkillRoot);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@GetLevel", DbType.Int32, entity.GetLevel);
			database.AddInParameter(commandWrapper, "@ActType", DbType.Int32, entity.ActType);
			database.AddInParameter(commandWrapper, "@MixExp", DbType.Int32, entity.MixExp);
			database.AddInParameter(commandWrapper, "@MixDiscount", DbType.Decimal, entity.MixDiscount);
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

