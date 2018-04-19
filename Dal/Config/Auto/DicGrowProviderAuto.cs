

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
    
    public partial class DicGrowProvider
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
		/// 将IDataReader的当前记录读取到DicGrowEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicGrowEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicGrowEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Name = (System.String) reader["Name"];
            obj.Stage = (System.Int32) reader["Stage"];
            obj.Reiki = (System.Int32) reader["Reiki"];
            obj.FastReiki = (System.Int32) reader["FastReiki"];
            obj.GrowNum = (System.Int32) reader["GrowNum"];
            obj.BreakRate = (System.Int32) reader["BreakRate"];
            obj.PlusPercent = (System.Int32) reader["PlusPercent"];
            obj.PropertyMax = (System.Int32) reader["PropertyMax"];
            obj.GrowTip = (System.String) reader["GrowTip"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicGrowEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicGrowEntity>();
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
        public DicGrowProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicGrowProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicGrowEntity</returns>
        /// <remarks>2015/10/23 14:51:37</remarks>
        public DicGrowEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicGrow_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DicGrowEntity obj=null;
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
        /// <returns>DicGrowEntity列表</returns>
        /// <remarks>2015/10/23 14:51:37</remarks>
        public List<DicGrowEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicGrow_GetAll");
            

            
            List<DicGrowEntity> list = null;
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
        /// <returns>DicGrowEntity列表</returns>
        /// <remarks>2015/10/23 14:51:37</remarks>
        public List<DicGrowEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicGrow_GetAllForCache");
            

            
            List<DicGrowEntity> list = null;
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
        /// <remarks>2015/10/23 14:51:37</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicGrow_Delete");
            
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
        /// <remarks>2015/10/23 14:51:37</remarks>
        public bool Insert(DicGrowEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/23 14:51:37</remarks>
        public bool Insert(DicGrowEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicGrow_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Stage", DbType.Int32, entity.Stage);
			database.AddInParameter(commandWrapper, "@Reiki", DbType.Int32, entity.Reiki);
			database.AddInParameter(commandWrapper, "@FastReiki", DbType.Int32, entity.FastReiki);
			database.AddInParameter(commandWrapper, "@GrowNum", DbType.Int32, entity.GrowNum);
			database.AddInParameter(commandWrapper, "@BreakRate", DbType.Int32, entity.BreakRate);
			database.AddInParameter(commandWrapper, "@PlusPercent", DbType.Int32, entity.PlusPercent);
			database.AddInParameter(commandWrapper, "@PropertyMax", DbType.Int32, entity.PropertyMax);
			database.AddInParameter(commandWrapper, "@GrowTip", DbType.String, entity.GrowTip);

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
        /// <remarks>2015/10/23 14:51:37</remarks>
        public bool Update(DicGrowEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/23 14:51:37</remarks>
        public bool Update(DicGrowEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicGrow_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Stage", DbType.Int32, entity.Stage);
			database.AddInParameter(commandWrapper, "@Reiki", DbType.Int32, entity.Reiki);
			database.AddInParameter(commandWrapper, "@FastReiki", DbType.Int32, entity.FastReiki);
			database.AddInParameter(commandWrapper, "@GrowNum", DbType.Int32, entity.GrowNum);
			database.AddInParameter(commandWrapper, "@BreakRate", DbType.Int32, entity.BreakRate);
			database.AddInParameter(commandWrapper, "@PlusPercent", DbType.Int32, entity.PlusPercent);
			database.AddInParameter(commandWrapper, "@PropertyMax", DbType.Int32, entity.PropertyMax);
			database.AddInParameter(commandWrapper, "@GrowTip", DbType.String, entity.GrowTip);

            
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

