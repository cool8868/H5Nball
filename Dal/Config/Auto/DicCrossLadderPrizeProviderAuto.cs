

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
    
    public partial class DicCrossladderprizeProvider
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
		/// 将IDataReader的当前记录读取到DicCrossladderprizeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicCrossladderprizeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicCrossladderprizeEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.MinRank = (System.Int32) reader["MinRank"];
            obj.MaxRank = (System.Int32) reader["MaxRank"];
            obj.SubType = (System.Int32) reader["SubType"];
            obj.PrizeType = (System.Int32) reader["PrizeType"];
            obj.Title = (System.String) reader["Title"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicCrossladderprizeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicCrossladderprizeEntity>();
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
        public DicCrossladderprizeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicCrossladderprizeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicCrossladderprizeEntity</returns>
        /// <remarks>2016-08-15 17:16:31</remarks>
        public DicCrossladderprizeEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicCrossladderprize_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DicCrossladderprizeEntity obj=null;
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
        /// <returns>DicCrossladderprizeEntity列表</returns>
        /// <remarks>2016-08-15 17:16:31</remarks>
        public List<DicCrossladderprizeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicCrossladderprize_GetAll");
            

            
            List<DicCrossladderprizeEntity> list = null;
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
        /// <remarks>2016-08-15 17:16:31</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicCrossladderprize_Delete");
            
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
        /// <remarks>2016-08-15 17:16:31</remarks>
        public bool Insert(DicCrossladderprizeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 17:16:31</remarks>
        public bool Insert(DicCrossladderprizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicCrossladderprize_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@MinRank", DbType.Int32, entity.MinRank);
			database.AddInParameter(commandWrapper, "@MaxRank", DbType.Int32, entity.MaxRank);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, entity.PrizeType);
			database.AddInParameter(commandWrapper, "@Title", DbType.String, entity.Title);

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
        /// <remarks>2016-08-15 17:16:31</remarks>
        public bool Update(DicCrossladderprizeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-15 17:16:31</remarks>
        public bool Update(DicCrossladderprizeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicCrossladderprize_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@MinRank", DbType.Int32, entity.MinRank);
			database.AddInParameter(commandWrapper, "@MaxRank", DbType.Int32, entity.MaxRank);
			database.AddInParameter(commandWrapper, "@SubType", DbType.Int32, entity.SubType);
			database.AddInParameter(commandWrapper, "@PrizeType", DbType.Int32, entity.PrizeType);
			database.AddInParameter(commandWrapper, "@Title", DbType.String, entity.Title);

            
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
