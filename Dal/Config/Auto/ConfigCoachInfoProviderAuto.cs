

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
    
    public partial class ConfigCoachinfoProvider
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
		/// 将IDataReader的当前记录读取到ConfigCoachinfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigCoachinfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigCoachinfoEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Name = (System.String) reader["Name"];
            obj.Offensive = (System.Int32) reader["Offensive"];
            obj.Organizational = (System.Int32) reader["Organizational"];
            obj.Defense = (System.Int32) reader["Defense"];
            obj.BodyAttr = (System.Int32) reader["BodyAttr"];
            obj.Goalkeeping = (System.Int32) reader["Goalkeeping"];
            obj.IsSkill = (System.Boolean) reader["IsSkill"];
            obj.SkillId = (System.Int32) reader["SkillId"];
            obj.DebrisCode = (System.Int32) reader["DebrisCode"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigCoachinfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigCoachinfoEntity>();
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
        public ConfigCoachinfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigCoachinfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigCoachinfoEntity</returns>
        /// <remarks>2017/2/22 17:28:08</remarks>
        public ConfigCoachinfoEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCoachinfo_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigCoachinfoEntity obj=null;
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
        /// <returns>ConfigCoachinfoEntity列表</returns>
        /// <remarks>2017/2/22 17:28:08</remarks>
        public List<ConfigCoachinfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCoachinfo_GetAll");
            

            
            List<ConfigCoachinfoEntity> list = null;
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
        /// <remarks>2017/2/22 17:28:08</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCoachinfo_Delete");
            
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
        /// <remarks>2017/2/22 17:28:08</remarks>
        public bool Insert(ConfigCoachinfoEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/22 17:28:08</remarks>
        public bool Insert(ConfigCoachinfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCoachinfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Offensive", DbType.Int32, entity.Offensive);
			database.AddInParameter(commandWrapper, "@Organizational", DbType.Int32, entity.Organizational);
			database.AddInParameter(commandWrapper, "@Defense", DbType.Int32, entity.Defense);
			database.AddInParameter(commandWrapper, "@BodyAttr", DbType.Int32, entity.BodyAttr);
			database.AddInParameter(commandWrapper, "@Goalkeeping", DbType.Int32, entity.Goalkeeping);
			database.AddInParameter(commandWrapper, "@IsSkill", DbType.Boolean, entity.IsSkill);
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@DebrisCode", DbType.Int32, entity.DebrisCode);

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
        /// <remarks>2017/2/22 17:28:08</remarks>
        public bool Update(ConfigCoachinfoEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/22 17:28:08</remarks>
        public bool Update(ConfigCoachinfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCoachinfo_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Offensive", DbType.Int32, entity.Offensive);
			database.AddInParameter(commandWrapper, "@Organizational", DbType.Int32, entity.Organizational);
			database.AddInParameter(commandWrapper, "@Defense", DbType.Int32, entity.Defense);
			database.AddInParameter(commandWrapper, "@BodyAttr", DbType.Int32, entity.BodyAttr);
			database.AddInParameter(commandWrapper, "@Goalkeeping", DbType.Int32, entity.Goalkeeping);
			database.AddInParameter(commandWrapper, "@IsSkill", DbType.Boolean, entity.IsSkill);
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@DebrisCode", DbType.Int32, entity.DebrisCode);

            
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
