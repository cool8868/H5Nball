

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
    
    public partial class DicManagerwillparttipsProvider
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
		/// 将IDataReader的当前记录读取到DicManagerwillparttipsEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicManagerwillparttipsEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicManagerwillparttipsEntity();
			
            obj.Id = (System.Int32) reader["Id"];
            obj.SkillId = (System.Int32) reader["SkillId"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.ItemCode = (System.Int32) reader["ItemCode"];
            obj.Pid = (System.Int32) reader["Pid"];
            obj.PName = (System.String) reader["PName"];
            obj.PNickName = (System.String) reader["PNickName"];
            obj.PColor = (System.Int32) reader["PColor"];
            obj.PColorMemo = (System.String) reader["PColorMemo"];
            obj.ReqStrength = (System.Int32) reader["ReqStrength"];
            obj.BuffMemo = (System.String) reader["BuffMemo"];
            obj.BuffArg = (System.Decimal) reader["BuffArg"];
            obj.BuffArg2 = (System.Decimal) reader["BuffArg2"];
            obj.Icon = (System.String) reader["Icon"];
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
        public List<DicManagerwillparttipsEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicManagerwillparttipsEntity>();
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
        public DicManagerwillparttipsProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicManagerwillparttipsProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="id">id</param>
        /// <returns>DicManagerwillparttipsEntity</returns>
        /// <remarks>2015/10/19 16:24:34</remarks>
        public DicManagerwillparttipsEntity GetById( System.Int32 id)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicManagerwillparttips_GetById");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, id);

            
            DicManagerwillparttipsEntity obj=null;
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
        /// <returns>DicManagerwillparttipsEntity列表</returns>
        /// <remarks>2015/10/19 16:24:34</remarks>
        public List<DicManagerwillparttipsEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicManagerwillparttips_GetAll");
            

            
            List<DicManagerwillparttipsEntity> list = null;
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
		/// <param name="id">id</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 16:24:34</remarks>
        public bool Delete ( System.Int32 id,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicManagerwillparttips_Delete");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, id);

            
            
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
        /// <remarks>2015/10/19 16:24:34</remarks>
        public bool Insert(DicManagerwillparttipsEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 16:24:34</remarks>
        public bool Insert(DicManagerwillparttipsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicManagerwillparttips_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
			database.AddInParameter(commandWrapper, "@Pid", DbType.Int32, entity.Pid);
			database.AddInParameter(commandWrapper, "@PName", DbType.String, entity.PName);
			database.AddInParameter(commandWrapper, "@PNickName", DbType.String, entity.PNickName);
			database.AddInParameter(commandWrapper, "@PColor", DbType.Int32, entity.PColor);
			database.AddInParameter(commandWrapper, "@PColorMemo", DbType.String, entity.PColorMemo);
			database.AddInParameter(commandWrapper, "@ReqStrength", DbType.Int32, entity.ReqStrength);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.Currency, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@BuffArg2", DbType.Currency, entity.BuffArg2);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
			database.AddInParameter(commandWrapper, "@Memo", DbType.String, entity.Memo);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Id", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Id);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Id=(System.Int32)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2015/10/19 16:24:34</remarks>
        public bool Update(DicManagerwillparttipsEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 16:24:34</remarks>
        public bool Update(DicManagerwillparttipsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicManagerwillparttips_Update");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, entity.Id);
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
			database.AddInParameter(commandWrapper, "@Pid", DbType.Int32, entity.Pid);
			database.AddInParameter(commandWrapper, "@PName", DbType.String, entity.PName);
			database.AddInParameter(commandWrapper, "@PNickName", DbType.String, entity.PNickName);
			database.AddInParameter(commandWrapper, "@PColor", DbType.Int32, entity.PColor);
			database.AddInParameter(commandWrapper, "@PColorMemo", DbType.String, entity.PColorMemo);
			database.AddInParameter(commandWrapper, "@ReqStrength", DbType.Int32, entity.ReqStrength);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@BuffArg", DbType.Currency, entity.BuffArg);
			database.AddInParameter(commandWrapper, "@BuffArg2", DbType.Currency, entity.BuffArg2);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
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

            entity.Id=(System.Int32)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

