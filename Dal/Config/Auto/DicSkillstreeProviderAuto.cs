

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
    
    public partial class DicSkillstreeProvider
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
		/// 将IDataReader的当前记录读取到DicSkillstreeEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicSkillstreeEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicSkillstreeEntity();
			
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillName = (System.String) reader["SkillName"];
            obj.ManagerType = (System.Int32) reader["ManagerType"];
            obj.Series = (System.Int32) reader["Series"];
            obj.RequireManagerLevel = (System.Int32) reader["RequireManagerLevel"];
            obj.Description = (System.String) reader["Description"];
            obj.Condition = (System.String) reader["Condition"];
            obj.ConditionPoint = (System.Int32) reader["ConditionPoint"];
            obj.MaxPoint = (System.Int32) reader["MaxPoint"];
            obj.Opener = (System.Int32) reader["Opener"];
            obj.Status = (System.Int32) reader["Status"];
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
        public List<DicSkillstreeEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicSkillstreeEntity>();
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
        public DicSkillstreeProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicSkillstreeProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="skillCode">skillCode</param>
        /// <returns>DicSkillstreeEntity</returns>
        /// <remarks>2016/5/26 11:14:01</remarks>
        public DicSkillstreeEntity GetById( System.String skillCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillstree_GetById");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);

            
            DicSkillstreeEntity obj=null;
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
        /// <returns>DicSkillstreeEntity列表</returns>
        /// <remarks>2016/5/26 11:14:01</remarks>
        public List<DicSkillstreeEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillstree_GetAll");
            

            
            List<DicSkillstreeEntity> list = null;
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
        /// <remarks>2016/5/26 11:14:02</remarks>
        public bool Delete ( System.String skillCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillstree_Delete");
            
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
        /// <remarks>2016/5/26 11:14:02</remarks>
        public bool Insert(DicSkillstreeEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/26 11:14:02</remarks>
        public bool Insert(DicSkillstreeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkillstree_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.AnsiString, entity.SkillName);
			database.AddInParameter(commandWrapper, "@ManagerType", DbType.Int32, entity.ManagerType);
			database.AddInParameter(commandWrapper, "@Series", DbType.Int32, entity.Series);
			database.AddInParameter(commandWrapper, "@RequireManagerLevel", DbType.Int32, entity.RequireManagerLevel);
			database.AddInParameter(commandWrapper, "@Description", DbType.AnsiString, entity.Description);
			database.AddInParameter(commandWrapper, "@Condition", DbType.AnsiString, entity.Condition);
			database.AddInParameter(commandWrapper, "@ConditionPoint", DbType.Int32, entity.ConditionPoint);
			database.AddInParameter(commandWrapper, "@MaxPoint", DbType.Int32, entity.MaxPoint);
			database.AddInParameter(commandWrapper, "@Opener", DbType.Int32, entity.Opener);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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
        /// <remarks>2016/5/26 11:14:02</remarks>
        public bool Update(DicSkillstreeEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/26 11:14:02</remarks>
        public bool Update(DicSkillstreeEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkillstree_Update");
            
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.AnsiString, entity.SkillName);
			database.AddInParameter(commandWrapper, "@ManagerType", DbType.Int32, entity.ManagerType);
			database.AddInParameter(commandWrapper, "@Series", DbType.Int32, entity.Series);
			database.AddInParameter(commandWrapper, "@RequireManagerLevel", DbType.Int32, entity.RequireManagerLevel);
			database.AddInParameter(commandWrapper, "@Description", DbType.AnsiString, entity.Description);
			database.AddInParameter(commandWrapper, "@Condition", DbType.AnsiString, entity.Condition);
			database.AddInParameter(commandWrapper, "@ConditionPoint", DbType.Int32, entity.ConditionPoint);
			database.AddInParameter(commandWrapper, "@MaxPoint", DbType.Int32, entity.MaxPoint);
			database.AddInParameter(commandWrapper, "@Opener", DbType.Int32, entity.Opener);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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

