

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
    
    public partial class TemplateActivityexgroupProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Support;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到TemplateActivityexgroupEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TemplateActivityexgroupEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TemplateActivityexgroupEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ExcitingId = (System.Int32) reader["ExcitingId"];
            obj.GroupId = (System.Int32) reader["GroupId"];
            obj.ActivityExType = (System.Int32) reader["ActivityExType"];
            obj.ExRequireId = (System.Int32) reader["ExRequireId"];
            obj.StatisticCycle = (System.Int32) reader["StatisticCycle"];
            obj.IsRank = (System.Boolean) reader["IsRank"];
            obj.RankCondition = (System.Int32) reader["RankCondition"];
            obj.RankCount = (System.Int32) reader["RankCount"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<TemplateActivityexgroupEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TemplateActivityexgroupEntity>();
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
        public TemplateActivityexgroupProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TemplateActivityexgroupProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>TemplateActivityexgroupEntity</returns>
        /// <remarks>2016/3/11 14:15:06</remarks>
        public TemplateActivityexgroupEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TemplateActivityexgroup_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            TemplateActivityexgroupEntity obj=null;
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
        /// <returns>TemplateActivityexgroupEntity列表</returns>
        /// <remarks>2016/3/11 14:15:06</remarks>
        public List<TemplateActivityexgroupEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TemplateActivityexgroup_GetAll");
            

            
            List<TemplateActivityexgroupEntity> list = null;
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
        /// <remarks>2016/3/11 14:15:06</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TemplateActivityexgroup_Delete");
            
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
        /// <remarks>2016/3/11 14:15:06</remarks>
        public bool Insert(TemplateActivityexgroupEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/3/11 14:15:06</remarks>
        public bool Insert(TemplateActivityexgroupEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TemplateActivityexgroup_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ExcitingId", DbType.Int32, entity.ExcitingId);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, entity.GroupId);
			database.AddInParameter(commandWrapper, "@ActivityExType", DbType.Int32, entity.ActivityExType);
			database.AddInParameter(commandWrapper, "@ExRequireId", DbType.Int32, entity.ExRequireId);
			database.AddInParameter(commandWrapper, "@StatisticCycle", DbType.Int32, entity.StatisticCycle);
			database.AddInParameter(commandWrapper, "@IsRank", DbType.Boolean, entity.IsRank);
			database.AddInParameter(commandWrapper, "@RankCondition", DbType.Int32, entity.RankCondition);
			database.AddInParameter(commandWrapper, "@RankCount", DbType.Int32, entity.RankCount);

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
        /// <remarks>2016/3/11 14:15:06</remarks>
        public bool Update(TemplateActivityexgroupEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/3/11 14:15:06</remarks>
        public bool Update(TemplateActivityexgroupEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TemplateActivityexgroup_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ExcitingId", DbType.Int32, entity.ExcitingId);
			database.AddInParameter(commandWrapper, "@GroupId", DbType.Int32, entity.GroupId);
			database.AddInParameter(commandWrapper, "@ActivityExType", DbType.Int32, entity.ActivityExType);
			database.AddInParameter(commandWrapper, "@ExRequireId", DbType.Int32, entity.ExRequireId);
			database.AddInParameter(commandWrapper, "@StatisticCycle", DbType.Int32, entity.StatisticCycle);
			database.AddInParameter(commandWrapper, "@IsRank", DbType.Boolean, entity.IsRank);
			database.AddInParameter(commandWrapper, "@RankCondition", DbType.Int32, entity.RankCondition);
			database.AddInParameter(commandWrapper, "@RankCount", DbType.Int32, entity.RankCount);

            
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

