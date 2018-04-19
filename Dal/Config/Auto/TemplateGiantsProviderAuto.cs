

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
    
    public partial class TemplateGiantsProvider
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
		/// 将IDataReader的当前记录读取到TemplateGiantsEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TemplateGiantsEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TemplateGiantsEntity();
			
            obj.MarkId = (System.Int32) reader["MarkId"];
            obj.Round = (System.Int32) reader["Round"];
            obj.SPlay = (System.Int32) reader["SPlay"];
            obj.Eplay = (System.Int32) reader["Eplay"];
            obj.Strength = (System.Int32) reader["Strength"];
            obj.playLevel = (System.Int32) reader["playLevel"];
            obj.FormationLevel = (System.Int32) reader["FormationLevel"];
            obj.SkillCount = (System.Int32) reader["SkillCount"];
            obj.MinSkillClass = (System.Int32) reader["MinSkillClass"];
            obj.MaxSkillClass = (System.Int32) reader["MaxSkillClass"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.IsWill = (System.Boolean) reader["IsWill"];
            obj.EquipCount = (System.Int32) reader["EquipCount"];
            obj.EquipQuality = (System.Int32) reader["EquipQuality"];
            obj.SuitType = (System.Int32) reader["SuitType"];
            obj.TalentLevel = (System.Int32) reader["TalentLevel"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<TemplateGiantsEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TemplateGiantsEntity>();
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
        public TemplateGiantsProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TemplateGiantsProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="markId">markId</param>
		/// <param name="round">round</param>
        /// <returns>TemplateGiantsEntity</returns>
        /// <remarks>2015/10/18 15:56:36</remarks>
        public TemplateGiantsEntity GetById( System.Int32 markId, System.Int32 round)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TemplateGiants_GetById");
            
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, markId);
			database.AddInParameter(commandWrapper, "@Round", DbType.Int32, round);

            
            TemplateGiantsEntity obj=null;
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
        /// <returns>TemplateGiantsEntity列表</returns>
        /// <remarks>2015/10/18 15:56:36</remarks>
        public List<TemplateGiantsEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TemplateGiants_GetAll");
            

            
            List<TemplateGiantsEntity> list = null;
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
		/// <param name="markId">markId</param>
		/// <param name="round">round</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 15:56:36</remarks>
        public bool Delete ( System.Int32 markId, System.Int32 round,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TemplateGiants_Delete");
            
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, markId);
			database.AddInParameter(commandWrapper, "@Round", DbType.Int32, round);

            
            
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
        /// <remarks>2015/10/18 15:56:36</remarks>
        public bool Insert(TemplateGiantsEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 15:56:36</remarks>
        public bool Insert(TemplateGiantsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TemplateGiants_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Round", DbType.Int32, entity.Round);
			database.AddInParameter(commandWrapper, "@SPlay", DbType.Int32, entity.SPlay);
			database.AddInParameter(commandWrapper, "@Eplay", DbType.Int32, entity.Eplay);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@playLevel", DbType.Int32, entity.playLevel);
			database.AddInParameter(commandWrapper, "@FormationLevel", DbType.Int32, entity.FormationLevel);
			database.AddInParameter(commandWrapper, "@SkillCount", DbType.Int32, entity.SkillCount);
			database.AddInParameter(commandWrapper, "@MinSkillClass", DbType.Int32, entity.MinSkillClass);
			database.AddInParameter(commandWrapper, "@MaxSkillClass", DbType.Int32, entity.MaxSkillClass);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@IsWill", DbType.Boolean, entity.IsWill);
			database.AddInParameter(commandWrapper, "@EquipCount", DbType.Int32, entity.EquipCount);
			database.AddInParameter(commandWrapper, "@EquipQuality", DbType.Int32, entity.EquipQuality);
			database.AddInParameter(commandWrapper, "@SuitType", DbType.Int32, entity.SuitType);
			database.AddInParameter(commandWrapper, "@TalentLevel", DbType.Int32, entity.TalentLevel);

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
        /// <remarks>2015/10/18 15:56:36</remarks>
        public bool Update(TemplateGiantsEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 15:56:36</remarks>
        public bool Update(TemplateGiantsEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TemplateGiants_Update");
            
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Round", DbType.Int32, entity.Round);
			database.AddInParameter(commandWrapper, "@SPlay", DbType.Int32, entity.SPlay);
			database.AddInParameter(commandWrapper, "@Eplay", DbType.Int32, entity.Eplay);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@playLevel", DbType.Int32, entity.playLevel);
			database.AddInParameter(commandWrapper, "@FormationLevel", DbType.Int32, entity.FormationLevel);
			database.AddInParameter(commandWrapper, "@SkillCount", DbType.Int32, entity.SkillCount);
			database.AddInParameter(commandWrapper, "@MinSkillClass", DbType.Int32, entity.MinSkillClass);
			database.AddInParameter(commandWrapper, "@MaxSkillClass", DbType.Int32, entity.MaxSkillClass);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@IsWill", DbType.Boolean, entity.IsWill);
			database.AddInParameter(commandWrapper, "@EquipCount", DbType.Int32, entity.EquipCount);
			database.AddInParameter(commandWrapper, "@EquipQuality", DbType.Int32, entity.EquipQuality);
			database.AddInParameter(commandWrapper, "@SuitType", DbType.Int32, entity.SuitType);
			database.AddInParameter(commandWrapper, "@TalentLevel", DbType.Int32, entity.TalentLevel);

            
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

