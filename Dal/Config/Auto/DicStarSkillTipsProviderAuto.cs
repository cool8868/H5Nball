

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
    
    public partial class DicStarskilltipsProvider
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
		/// 将IDataReader的当前记录读取到DicStarskilltipsEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicStarskilltipsEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicStarskilltipsEntity();
			
            obj.SkillId = (System.Int32) reader["SkillId"];
            obj.StarSkillCode = (System.String) reader["StarSkillCode"];
            obj.StarSkillName = (System.String) reader["StarSkillName"];
            obj.ActType = (System.Int32) reader["ActType"];
            obj.ActTypeMemo = (System.String) reader["ActTypeMemo"];
            obj.Pid = (System.Int32) reader["Pid"];
            obj.ReqStrength = (System.Int32) reader["ReqStrength"];
            obj.TriggerAction = (System.String) reader["TriggerAction"];
            obj.TriggerRate = (System.String) reader["TriggerRate"];
            obj.CD = (System.String) reader["CD"];
            obj.LastTime = (System.String) reader["LastTime"];
            obj.BuffMemo = (System.String) reader["BuffMemo"];
            obj.Icon = (System.String) reader["Icon"];
            obj.Memo = (System.String) reader["Memo"];
            obj.PlusSkillCode = (System.String) reader["PlusSkillCode"];
            obj.PlusSkillName = (System.String) reader["PlusSkillName"];
            obj.PlusSkillMemo = (System.String) reader["PlusSkillMemo"];
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
        public List<DicStarskilltipsEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicStarskilltipsEntity>();
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
        public DicStarskilltipsProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicStarskilltipsProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="skillId">skillId</param>
        /// <returns>DicStarskilltipsEntity</returns>
        /// <remarks>2016/4/11 17:28:43</remarks>
        public DicStarskilltipsEntity GetById( System.Int32 skillId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicStarskilltips_GetById");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, skillId);

            
            DicStarskilltipsEntity obj=null;
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
        /// <returns>DicStarskilltipsEntity列表</returns>
        /// <remarks>2016/4/11 17:28:43</remarks>
        public List<DicStarskilltipsEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicStarskilltips_GetAll");
            

            
            List<DicStarskilltipsEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region Insert
		
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/4/11 17:28:43</remarks>
        public bool Insert(DicStarskilltipsEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/4/11 17:28:43</remarks>
        public bool Insert(DicStarskilltipsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicStarskilltips_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@StarSkillCode", DbType.AnsiString, entity.StarSkillCode);
			database.AddInParameter(commandWrapper, "@StarSkillName", DbType.String, entity.StarSkillName);
			database.AddInParameter(commandWrapper, "@ActType", DbType.Int32, entity.ActType);
			database.AddInParameter(commandWrapper, "@ActTypeMemo", DbType.String, entity.ActTypeMemo);
			database.AddInParameter(commandWrapper, "@Pid", DbType.Int32, entity.Pid);
			database.AddInParameter(commandWrapper, "@ReqStrength", DbType.Int32, entity.ReqStrength);
			database.AddInParameter(commandWrapper, "@TriggerAction", DbType.String, entity.TriggerAction);
			database.AddInParameter(commandWrapper, "@TriggerRate", DbType.String, entity.TriggerRate);
			database.AddInParameter(commandWrapper, "@CD", DbType.String, entity.CD);
			database.AddInParameter(commandWrapper, "@LastTime", DbType.String, entity.LastTime);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
			database.AddInParameter(commandWrapper, "@Memo", DbType.String, entity.Memo);
			database.AddInParameter(commandWrapper, "@PlusSkillCode", DbType.AnsiString, entity.PlusSkillCode);
			database.AddInParameter(commandWrapper, "@PlusSkillName", DbType.String, entity.PlusSkillName);
			database.AddInParameter(commandWrapper, "@PlusSkillMemo", DbType.String, entity.PlusSkillMemo);
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
        /// <remarks>2016/4/11 17:28:43</remarks>
        public bool Update(DicStarskilltipsEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/4/11 17:28:43</remarks>
        public bool Update(DicStarskilltipsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicStarskilltips_Update");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@StarSkillCode", DbType.AnsiString, entity.StarSkillCode);
			database.AddInParameter(commandWrapper, "@StarSkillName", DbType.String, entity.StarSkillName);
			database.AddInParameter(commandWrapper, "@ActType", DbType.Int32, entity.ActType);
			database.AddInParameter(commandWrapper, "@ActTypeMemo", DbType.String, entity.ActTypeMemo);
			database.AddInParameter(commandWrapper, "@Pid", DbType.Int32, entity.Pid);
			database.AddInParameter(commandWrapper, "@ReqStrength", DbType.Int32, entity.ReqStrength);
			database.AddInParameter(commandWrapper, "@TriggerAction", DbType.String, entity.TriggerAction);
			database.AddInParameter(commandWrapper, "@TriggerRate", DbType.String, entity.TriggerRate);
			database.AddInParameter(commandWrapper, "@CD", DbType.String, entity.CD);
			database.AddInParameter(commandWrapper, "@LastTime", DbType.String, entity.LastTime);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
			database.AddInParameter(commandWrapper, "@Memo", DbType.String, entity.Memo);
			database.AddInParameter(commandWrapper, "@PlusSkillCode", DbType.AnsiString, entity.PlusSkillCode);
			database.AddInParameter(commandWrapper, "@PlusSkillName", DbType.String, entity.PlusSkillName);
			database.AddInParameter(commandWrapper, "@PlusSkillMemo", DbType.String, entity.PlusSkillMemo);
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

