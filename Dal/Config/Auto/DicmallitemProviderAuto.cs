

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
    
    public partial class DicMallitemProvider
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
		/// 将IDataReader的当前记录读取到DicMallitemEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicMallitemEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicMallitemEntity();
			
            obj.MallCode = (System.Int32) reader["MallCode"];
            obj.Name = (System.String) reader["Name"];
            obj.MallType = (System.Int32) reader["MallType"];
            obj.Quality = (System.Int32) reader["Quality"];
            obj.ShowOrder = (System.Int32) reader["ShowOrder"];
            obj.ImageId = (System.Int32) reader["ImageId"];
            obj.ItemIntro = (System.String) reader["ItemIntro"];
            obj.ItemTip = (System.String) reader["ItemTip"];
            obj.UseNote = (System.String) reader["UseNote"];
            obj.UseMsg = (System.String) reader["UseMsg"];
            obj.UseLevel = (System.Int32) reader["UseLevel"];
            obj.ShowUse = (System.Boolean) reader["ShowUse"];
            obj.CurrencyType = (System.Int32) reader["CurrencyType"];
            obj.CurrencyCount = (System.Int32) reader["CurrencyCount"];
            obj.CurrencyDiscount = (System.String) reader["CurrencyDiscount"];
            obj.EffectType = (System.Int32) reader["EffectType"];
            obj.EffectValue = (System.Int32) reader["EffectValue"];
            obj.ShowFlag = (System.Boolean) reader["ShowFlag"];
            obj.HotFlag = (System.Boolean) reader["HotFlag"];
            obj.PackageFlag = (System.Boolean) reader["PackageFlag"];
            obj.ShowBatch = (System.Boolean) reader["ShowBatch"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicMallitemEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicMallitemEntity>();
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
        public DicMallitemProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicMallitemProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="mallCode">mallCode</param>
        /// <returns>DicMallitemEntity</returns>
        /// <remarks>2015/10/19 15:57:49</remarks>
        public DicMallitemEntity GetById( System.Int32 mallCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicMallitem_GetById");
            
			database.AddInParameter(commandWrapper, "@MallCode", DbType.Int32, mallCode);

            
            DicMallitemEntity obj=null;
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
        /// <returns>DicMallitemEntity列表</returns>
        /// <remarks>2015/10/19 15:57:49</remarks>
        public List<DicMallitemEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicMallitem_GetAll");
            

            
            List<DicMallitemEntity> list = null;
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
        /// <returns>DicMallitemEntity列表</returns>
        /// <remarks>2015/10/19 15:57:49</remarks>
        public List<DicMallitemEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicMallItem_GetAllForCache");
            

            
            List<DicMallitemEntity> list = null;
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
		/// <param name="mallCode">mallCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 15:57:49</remarks>
        public bool Delete ( System.Int32 mallCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicMallitem_Delete");
            
			database.AddInParameter(commandWrapper, "@MallCode", DbType.Int32, mallCode);

            
            
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
        /// <remarks>2015/10/19 15:57:49</remarks>
        public bool Insert(DicMallitemEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:57:49</remarks>
        public bool Insert(DicMallitemEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicMallitem_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@MallCode", DbType.Int32, entity.MallCode);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@MallType", DbType.Int32, entity.MallType);
			database.AddInParameter(commandWrapper, "@Quality", DbType.Int32, entity.Quality);
			database.AddInParameter(commandWrapper, "@ShowOrder", DbType.Int32, entity.ShowOrder);
			database.AddInParameter(commandWrapper, "@ImageId", DbType.Int32, entity.ImageId);
			database.AddInParameter(commandWrapper, "@ItemIntro", DbType.String, entity.ItemIntro);
			database.AddInParameter(commandWrapper, "@ItemTip", DbType.String, entity.ItemTip);
			database.AddInParameter(commandWrapper, "@UseNote", DbType.String, entity.UseNote);
			database.AddInParameter(commandWrapper, "@UseMsg", DbType.String, entity.UseMsg);
			database.AddInParameter(commandWrapper, "@UseLevel", DbType.Int32, entity.UseLevel);
			database.AddInParameter(commandWrapper, "@ShowUse", DbType.Boolean, entity.ShowUse);
			database.AddInParameter(commandWrapper, "@CurrencyType", DbType.Int32, entity.CurrencyType);
			database.AddInParameter(commandWrapper, "@CurrencyCount", DbType.Int32, entity.CurrencyCount);
			database.AddInParameter(commandWrapper, "@CurrencyDiscount", DbType.AnsiString, entity.CurrencyDiscount);
			database.AddInParameter(commandWrapper, "@EffectType", DbType.Int32, entity.EffectType);
			database.AddInParameter(commandWrapper, "@EffectValue", DbType.Int32, entity.EffectValue);
			database.AddInParameter(commandWrapper, "@ShowFlag", DbType.Boolean, entity.ShowFlag);
			database.AddInParameter(commandWrapper, "@HotFlag", DbType.Boolean, entity.HotFlag);
			database.AddInParameter(commandWrapper, "@PackageFlag", DbType.Boolean, entity.PackageFlag);
			database.AddInParameter(commandWrapper, "@ShowBatch", DbType.Boolean, entity.ShowBatch);

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
        /// <remarks>2015/10/19 15:57:49</remarks>
        public bool Update(DicMallitemEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 15:57:49</remarks>
        public bool Update(DicMallitemEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicMallitem_Update");
            
			database.AddInParameter(commandWrapper, "@MallCode", DbType.Int32, entity.MallCode);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@MallType", DbType.Int32, entity.MallType);
			database.AddInParameter(commandWrapper, "@Quality", DbType.Int32, entity.Quality);
			database.AddInParameter(commandWrapper, "@ShowOrder", DbType.Int32, entity.ShowOrder);
			database.AddInParameter(commandWrapper, "@ImageId", DbType.Int32, entity.ImageId);
			database.AddInParameter(commandWrapper, "@ItemIntro", DbType.String, entity.ItemIntro);
			database.AddInParameter(commandWrapper, "@ItemTip", DbType.String, entity.ItemTip);
			database.AddInParameter(commandWrapper, "@UseNote", DbType.String, entity.UseNote);
			database.AddInParameter(commandWrapper, "@UseMsg", DbType.String, entity.UseMsg);
			database.AddInParameter(commandWrapper, "@UseLevel", DbType.Int32, entity.UseLevel);
			database.AddInParameter(commandWrapper, "@ShowUse", DbType.Boolean, entity.ShowUse);
			database.AddInParameter(commandWrapper, "@CurrencyType", DbType.Int32, entity.CurrencyType);
			database.AddInParameter(commandWrapper, "@CurrencyCount", DbType.Int32, entity.CurrencyCount);
			database.AddInParameter(commandWrapper, "@CurrencyDiscount", DbType.AnsiString, entity.CurrencyDiscount);
			database.AddInParameter(commandWrapper, "@EffectType", DbType.Int32, entity.EffectType);
			database.AddInParameter(commandWrapper, "@EffectValue", DbType.Int32, entity.EffectValue);
			database.AddInParameter(commandWrapper, "@ShowFlag", DbType.Boolean, entity.ShowFlag);
			database.AddInParameter(commandWrapper, "@HotFlag", DbType.Boolean, entity.HotFlag);
			database.AddInParameter(commandWrapper, "@PackageFlag", DbType.Boolean, entity.PackageFlag);
			database.AddInParameter(commandWrapper, "@ShowBatch", DbType.Boolean, entity.ShowBatch);

            
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

