

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
    
    public partial class DicClubskillProvider
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
		/// 将IDataReader的当前记录读取到DicClubskillEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicClubskillEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicClubskillEntity();
			
            obj.SkillId = (System.Int32) reader["SkillId"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.SkillName = (System.String) reader["SkillName"];
            obj.ClubType = (System.Int32) reader["ClubType"];
            obj.SkillKey = (System.String) reader["SkillKey"];
            obj.SkillValue = (System.Int32) reader["SkillValue"];
            obj.BuffMemo = (System.String) reader["BuffMemo"];
            obj.Icon = (System.String) reader["Icon"];
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
        public List<DicClubskillEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicClubskillEntity>();
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
        public DicClubskillProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicClubskillProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="skillId">skillId</param>
        /// <returns>DicClubskillEntity</returns>
        /// <remarks>2016/6/15 15:08:56</remarks>
        public DicClubskillEntity GetById( System.Int32 skillId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicClubskill_GetById");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, skillId);

            
            DicClubskillEntity obj=null;
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
        /// <returns>DicClubskillEntity列表</returns>
        /// <remarks>2016/6/15 15:08:56</remarks>
        public List<DicClubskillEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicClubskill_GetAll");
            

            
            List<DicClubskillEntity> list = null;
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
		/// <param name="skillId">skillId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/15 15:08:56</remarks>
        public bool Delete ( System.Int32 skillId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicClubskill_Delete");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, skillId);

            
            
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
        /// <remarks>2016/6/15 15:08:56</remarks>
        public bool Insert(DicClubskillEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/15 15:08:56</remarks>
        public bool Insert(DicClubskillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicClubskill_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@ClubType", DbType.Int32, entity.ClubType);
			database.AddInParameter(commandWrapper, "@SkillKey", DbType.String, entity.SkillKey);
			database.AddInParameter(commandWrapper, "@SkillValue", DbType.Int32, entity.SkillValue);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
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
        /// <remarks>2016/6/15 15:08:56</remarks>
        public bool Update(DicClubskillEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/15 15:08:56</remarks>
        public bool Update(DicClubskillEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicClubskill_Update");
            
			database.AddInParameter(commandWrapper, "@SkillId", DbType.Int32, entity.SkillId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@SkillName", DbType.String, entity.SkillName);
			database.AddInParameter(commandWrapper, "@ClubType", DbType.Int32, entity.ClubType);
			database.AddInParameter(commandWrapper, "@SkillKey", DbType.String, entity.SkillKey);
			database.AddInParameter(commandWrapper, "@SkillValue", DbType.Int32, entity.SkillValue);
			database.AddInParameter(commandWrapper, "@BuffMemo", DbType.String, entity.BuffMemo);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
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

