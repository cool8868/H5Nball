

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
    
    public partial class DicPlayercarddecProvider
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
		/// 将IDataReader的当前记录读取到DicPlayercarddecEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicPlayercarddecEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicPlayercarddecEntity();
			
            obj.ItemCode = (System.Int32) reader["ItemCode"];
            obj.Reiki = (System.Int32) reader["Reiki"];
            obj.SoulRange = (System.Int32) reader["SoulRange"];
            obj.Soul = (System.Int32) reader["Soul"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicPlayercarddecEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicPlayercarddecEntity>();
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
        public DicPlayercarddecProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicPlayercarddecProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="itemCode">itemCode</param>
        /// <returns>DicPlayercarddecEntity</returns>
        /// <remarks>2015/10/19 10:52:46</remarks>
        public DicPlayercarddecEntity GetById( System.Int32 itemCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicPlayercarddec_GetById");
            
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, itemCode);

            
            DicPlayercarddecEntity obj=null;
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
        /// <returns>DicPlayercarddecEntity列表</returns>
        /// <remarks>2015/10/19 10:52:46</remarks>
        public List<DicPlayercarddecEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicPlayercarddec_GetAll");
            

            
            List<DicPlayercarddecEntity> list = null;
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
		/// <param name="itemCode">itemCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 10:52:46</remarks>
        public bool Delete ( System.Int32 itemCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicPlayercarddec_Delete");
            
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, itemCode);

            
            
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
        /// <remarks>2015/10/19 10:52:46</remarks>
        public bool Insert(DicPlayercarddecEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:52:46</remarks>
        public bool Insert(DicPlayercarddecEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicPlayercarddec_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
			database.AddInParameter(commandWrapper, "@Reiki", DbType.Int32, entity.Reiki);
			database.AddInParameter(commandWrapper, "@SoulRange", DbType.Int32, entity.SoulRange);
			database.AddInParameter(commandWrapper, "@Soul", DbType.Int32, entity.Soul);

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
        /// <remarks>2015/10/19 10:52:46</remarks>
        public bool Update(DicPlayercarddecEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 10:52:46</remarks>
        public bool Update(DicPlayercarddecEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicPlayercarddec_Update");
            
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
			database.AddInParameter(commandWrapper, "@Reiki", DbType.Int32, entity.Reiki);
			database.AddInParameter(commandWrapper, "@SoulRange", DbType.Int32, entity.SoulRange);
			database.AddInParameter(commandWrapper, "@Soul", DbType.Int32, entity.Soul);

            
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

