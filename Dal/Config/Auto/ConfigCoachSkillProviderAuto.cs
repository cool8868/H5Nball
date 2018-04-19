

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
    
    public partial class ConfigCoachskillProvider
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
		/// 将IDataReader的当前记录读取到ConfigCoachskillEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigCoachskillEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigCoachskillEntity();
			
            obj.CoachId = (System.Int32) reader["CoachId"];
            obj.SkillName = (System.String) reader["SkillName"];
            obj.TriggerCondition = (System.String) reader["TriggerCondition"];
            obj.CD = (System.Int32) reader["CD"];
            obj.TimeOfDuration = (System.String) reader["TimeOfDuration"];
            obj.TriggerProbability = (System.Int32) reader["TriggerProbability"];
            obj.Description = (System.String) reader["Description"];
            obj.PlusDescription = (System.String) reader["PlusDescription"];
            obj.Base0 = (System.Decimal) reader["Base0"];
            obj.Base1 = (System.Decimal) reader["Base1"];
            obj.Plus0 = (System.Decimal) reader["Plus0"];
            obj.Plus1 = (System.Decimal) reader["Plus1"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigCoachskillEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigCoachskillEntity>();
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
        public ConfigCoachskillProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigCoachskillProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="coachId">coachId</param>
        /// <returns>ConfigCoachskillEntity</returns>
        /// <remarks>2017/2/22 17:28:20</remarks>
        public ConfigCoachskillEntity GetById( System.Int32 coachId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCoachskill_GetById");
            
			database.AddInParameter(commandWrapper, "@CoachId", DbType.Int32, coachId);

            
            ConfigCoachskillEntity obj=null;
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
        /// <returns>ConfigCoachskillEntity列表</returns>
        /// <remarks>2017/2/22 17:28:20</remarks>
        public List<ConfigCoachskillEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCoachskill_GetAll");
            

            
            List<ConfigCoachskillEntity> list = null;
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
		/// <param name="coachId">coachId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2017/2/22 17:28:20</remarks>
        public bool Delete ( System.Int32 coachId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCoachskill_Delete");
            
			database.AddInParameter(commandWrapper, "@CoachId", DbType.Int32, coachId);

            
            
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
        /// <remarks>2017/2/22 17:28:20</remarks>
        public bool Insert(ConfigCoachskillEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/22 17:28:20</remarks>
        public bool Insert(ConfigCoachskillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCoachskill_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@CoachId", DbType.Int32, entity.CoachId);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@TriggerCondition", DbType.AnsiString, entity.TriggerCondition);
			database.AddInParameter(commandWrapper, "@CD", DbType.Int32, entity.CD);
			database.AddInParameter(commandWrapper, "@TimeOfDuration", DbType.AnsiString, entity.TimeOfDuration);
			database.AddInParameter(commandWrapper, "@TriggerProbability", DbType.Int32, entity.TriggerProbability);
			database.AddInParameter(commandWrapper, "@Description", DbType.AnsiString, entity.Description);
			database.AddInParameter(commandWrapper, "@PlusDescription", DbType.AnsiString, entity.PlusDescription);
			database.AddInParameter(commandWrapper, "@Base0", DbType.Decimal, entity.Base0);
			database.AddInParameter(commandWrapper, "@Base1", DbType.Decimal, entity.Base1);
			database.AddInParameter(commandWrapper, "@Plus0", DbType.Decimal, entity.Plus0);
			database.AddInParameter(commandWrapper, "@Plus1", DbType.Decimal, entity.Plus1);

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
        /// <remarks>2017/2/22 17:28:20</remarks>
        public bool Update(ConfigCoachskillEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/22 17:28:20</remarks>
        public bool Update(ConfigCoachskillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCoachskill_Update");
            
			database.AddInParameter(commandWrapper, "@CoachId", DbType.Int32, entity.CoachId);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@TriggerCondition", DbType.AnsiString, entity.TriggerCondition);
			database.AddInParameter(commandWrapper, "@CD", DbType.Int32, entity.CD);
			database.AddInParameter(commandWrapper, "@TimeOfDuration", DbType.AnsiString, entity.TimeOfDuration);
			database.AddInParameter(commandWrapper, "@TriggerProbability", DbType.Int32, entity.TriggerProbability);
			database.AddInParameter(commandWrapper, "@Description", DbType.AnsiString, entity.Description);
			database.AddInParameter(commandWrapper, "@PlusDescription", DbType.AnsiString, entity.PlusDescription);
			database.AddInParameter(commandWrapper, "@Base0", DbType.Decimal, entity.Base0);
			database.AddInParameter(commandWrapper, "@Base1", DbType.Decimal, entity.Base1);
			database.AddInParameter(commandWrapper, "@Plus0", DbType.Decimal, entity.Plus0);
			database.AddInParameter(commandWrapper, "@Plus1", DbType.Decimal, entity.Plus1);

            
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
