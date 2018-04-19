

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
    
    public partial class DicGiftpackprizeProvider
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
		/// 将IDataReader的当前记录读取到DicGiftpackprizeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicGiftpackprizeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicGiftpackprizeEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.PackId = (System.Int32) reader["PackId"];
            obj.PrizeType = (System.Int32) reader["PrizeType"];
            obj.SubType = (System.Int32) reader["SubType"];
            obj.ThirdType = (System.Int32) reader["ThirdType"];
            obj.MinPower = (System.Int32) reader["MinPower"];
            obj.MaxPower = (System.Int32) reader["MaxPower"];
            obj.Count = (System.Int32) reader["Count"];
            obj.Strength1 = (System.Int32) reader["Strength1"];
            obj.Strength2 = (System.Int32) reader["Strength2"];
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
        public List<DicGiftpackprizeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicGiftpackprizeEntity>();
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
        public DicGiftpackprizeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicGiftpackprizeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicGiftpackprizeEntity</returns>
        /// <remarks>2016/2/16 16:49:27</remarks>
        public DicGiftpackprizeEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicGiftpackprize_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DicGiftpackprizeEntity obj=null;
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
        /// <returns>DicGiftpackprizeEntity列表</returns>
        /// <remarks>2016/2/16 16:49:27</remarks>
        public List<DicGiftpackprizeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicGiftpackprize_GetAll");
            

            
            List<DicGiftpackprizeEntity> list = null;
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
        /// <remarks>2016/2/16 16:49:27</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicGiftpackprize_Delete");
            
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
        /// <remarks>2016/2/16 16:49:27</remarks>
        public bool Insert(DicGiftpackprizeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/2/16 16:49:27</remarks>
        public bool Insert(DicGiftpackprizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicGiftpackprize_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@PackId", DbType.Int32, entity.PackId);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, entity.PrizeType);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@ThirdType", DbType.Int32, entity.ThirdType);
			database.AddInParameter(commandWrapper, "@MinPower", DbType.Int32, entity.MinPower);
			database.AddInParameter(commandWrapper, "@MaxPower", DbType.Int32, entity.MaxPower);
			database.AddInParameter(commandWrapper, "@Count", DbType.Int32, entity.Count);
			database.AddInParameter(commandWrapper, "@Strength1", DbType.Int32, entity.Strength1);
			database.AddInParameter(commandWrapper, "@Strength2", DbType.Int32, entity.Strength2);
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
        /// <remarks>2016/2/16 16:49:27</remarks>
        public bool Update(DicGiftpackprizeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/2/16 16:49:27</remarks>
        public bool Update(DicGiftpackprizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicGiftpackprize_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@PackId", DbType.Int32, entity.PackId);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, entity.PrizeType);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@ThirdType", DbType.Int32, entity.ThirdType);
			database.AddInParameter(commandWrapper, "@MinPower", DbType.Int32, entity.MinPower);
			database.AddInParameter(commandWrapper, "@MaxPower", DbType.Int32, entity.MaxPower);
			database.AddInParameter(commandWrapper, "@Count", DbType.Int32, entity.Count);
			database.AddInParameter(commandWrapper, "@Strength1", DbType.Int32, entity.Strength1);
			database.AddInParameter(commandWrapper, "@Strength2", DbType.Int32, entity.Strength2);
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

