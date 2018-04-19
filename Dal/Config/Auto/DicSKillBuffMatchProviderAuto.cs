

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
    
    public partial class DicSkillbuffmatchProvider
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
		/// 将IDataReader的当前记录读取到DicSkillbuffmatchEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicSkillbuffmatchEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicSkillbuffmatchEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Type = (System.Int32) reader["Type"];
            obj.LinkId = (System.String) reader["LinkId"];
            obj.LinkType = (System.String) reader["LinkType"];
            obj.BuffEngineId = (System.String) reader["BuffEngineId"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicSkillbuffmatchEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicSkillbuffmatchEntity>();
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
        public DicSkillbuffmatchProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicSkillbuffmatchProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicSkillbuffmatchEntity</returns>
        /// <remarks>2015/10/19 11:11:42</remarks>
        public DicSkillbuffmatchEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillbuffmatch_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DicSkillbuffmatchEntity obj=null;
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
        /// <returns>DicSkillbuffmatchEntity列表</returns>
        /// <remarks>2015/10/19 11:11:42</remarks>
        public List<DicSkillbuffmatchEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillbuffmatch_GetAll");
            

            
            List<DicSkillbuffmatchEntity> list = null;
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
        /// <returns>DicSkillbuffmatchEntity列表</returns>
        /// <remarks>2015/10/19 11:11:42</remarks>
        public List<DicSkillbuffmatchEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_SKillBuffMatch_GetAllForCache");
            

            
            List<DicSkillbuffmatchEntity> list = null;
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
        /// <remarks>2015/10/19 11:11:43</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicSkillbuffmatch_Delete");
            
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
        /// <remarks>2015/10/19 11:11:43</remarks>
        public bool Insert(DicSkillbuffmatchEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 11:11:43</remarks>
        public bool Insert(DicSkillbuffmatchEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkillbuffmatch_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@LinkId", DbType.AnsiString, entity.LinkId);
			database.AddInParameter(commandWrapper, "@LinkType", DbType.AnsiString, entity.LinkType);
			database.AddInParameter(commandWrapper, "@BuffEngineId", DbType.AnsiString, entity.BuffEngineId);

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
        /// <remarks>2015/10/19 11:11:43</remarks>
        public bool Update(DicSkillbuffmatchEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 11:11:43</remarks>
        public bool Update(DicSkillbuffmatchEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicSkillbuffmatch_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@LinkId", DbType.AnsiString, entity.LinkId);
			database.AddInParameter(commandWrapper, "@LinkType", DbType.AnsiString, entity.LinkType);
			database.AddInParameter(commandWrapper, "@BuffEngineId", DbType.AnsiString, entity.BuffEngineId);

            
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

