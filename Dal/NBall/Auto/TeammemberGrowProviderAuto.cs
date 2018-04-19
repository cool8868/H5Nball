

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
    
    public partial class TeammemberGrowProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Main;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到TeammemberGrowEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TeammemberGrowEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TeammemberGrowEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.GrowLevel = (System.Int32) reader["GrowLevel"];
            obj.GrowNum = (System.Int32) reader["GrowNum"];
            obj.DayGrowCount = (System.Int32) reader["DayGrowCount"];
            obj.DayFastGrowCount = (System.Int32) reader["DayFastGrowCount"];
            obj.DayFreeFastGrowCount = (System.Int32) reader["DayFreeFastGrowCount"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
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
        public List<TeammemberGrowEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TeammemberGrowEntity>();
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
        public TeammemberGrowProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TeammemberGrowProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>TeammemberGrowEntity</returns>
        /// <remarks>2015/10/18 15:50:38</remarks>
        public TeammemberGrowEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TeammemberGrow_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            TeammemberGrowEntity obj=null;
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
		
		#region  GetByManager
		
		/// <summary>
        /// GetByManager
        /// </summary>
        /// <returns>TeammemberGrowEntity列表</returns>
        /// <remarks>2015/10/18 15:50:38</remarks>
        public List<TeammemberGrowEntity> GetByManager( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TeammemberGrow_GetByManager");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<TeammemberGrowEntity> list = null;
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
        /// <remarks>2015/10/18 15:50:38</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TeammemberGrow_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
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
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 15:50:38</remarks>
        public bool Insert(TeammemberGrowEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TeammemberGrow_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@GrowLevel", DbType.Int32, entity.GrowLevel);
			database.AddInParameter(commandWrapper, "@GrowNum", DbType.Int32, entity.GrowNum);
			database.AddInParameter(commandWrapper, "@DayGrowCount", DbType.Int32, entity.DayGrowCount);
			database.AddInParameter(commandWrapper, "@DayFastGrowCount", DbType.Int32, entity.DayFastGrowCount);
			database.AddInParameter(commandWrapper, "@DayFreeFastGrowCount", DbType.Int32, entity.DayFreeFastGrowCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 15:50:38</remarks>
        public bool Update(TeammemberGrowEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TeammemberGrow_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@GrowLevel", DbType.Int32, entity.GrowLevel);
			database.AddInParameter(commandWrapper, "@GrowNum", DbType.Int32, entity.GrowNum);
			database.AddInParameter(commandWrapper, "@DayGrowCount", DbType.Int32, entity.DayGrowCount);
			database.AddInParameter(commandWrapper, "@DayFastGrowCount", DbType.Int32, entity.DayFastGrowCount);
			database.AddInParameter(commandWrapper, "@DayFreeFastGrowCount", DbType.Int32, entity.DayFreeFastGrowCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
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

            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

