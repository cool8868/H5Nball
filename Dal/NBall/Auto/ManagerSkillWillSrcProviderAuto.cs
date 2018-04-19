

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
    
    public partial class ManagerskillWillsrcProvider
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
		/// 将IDataReader的当前记录读取到ManagerskillWillsrcEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ManagerskillWillsrcEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ManagerskillWillsrcEntity();
			
            obj.Id = (System.Int64) reader["Id"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.SkillCode = (System.String) reader["SkillCode"];
            obj.PartMap = (System.String) reader["PartMap"];
            obj.EnableFlag = (System.Int32) reader["EnableFlag"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ManagerskillWillsrcEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ManagerskillWillsrcEntity>();
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
        public ManagerskillWillsrcProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ManagerskillWillsrcProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="id">id</param>
        /// <returns>ManagerskillWillsrcEntity</returns>
        /// <remarks>2015/10/19 17:27:14</remarks>
        public ManagerskillWillsrcEntity GetById( System.Int64 id)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerskillWillsrc_GetById");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, id);

            
            ManagerskillWillsrcEntity obj=null;
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
		
		#region  GetWillSrc
		
		/// <summary>
        /// GetWillSrc
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="willCode">willCode</param>
        /// <returns>ManagerskillWillsrcEntity</returns>
        /// <remarks>2015/10/19 17:27:14</remarks>
        public ManagerskillWillsrcEntity GetWillSrc( System.Guid managerId, System.String willCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerSkill_GetWillSrc");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@WillCode", DbType.AnsiString, willCode);

            
            ManagerskillWillsrcEntity obj=null;
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
        /// <returns>ManagerskillWillsrcEntity列表</returns>
        /// <remarks>2015/10/19 17:27:14</remarks>
        public List<ManagerskillWillsrcEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerskillWillsrc_GetAll");
            

            
            List<ManagerskillWillsrcEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetWillSrcList
		
		/// <summary>
        /// GetWillSrcList
        /// </summary>
        /// <returns>ManagerskillWillsrcEntity列表</returns>
        /// <remarks>2015/10/19 17:27:14</remarks>
        public List<ManagerskillWillsrcEntity> GetWillSrcList( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerSkill_GetWillSrcList");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<ManagerskillWillsrcEntity> list = null;
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
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:27:14</remarks>
        public bool Delete ( System.Int64 id, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ManagerskillWillsrc_Delete");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, id);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);

            
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
        /// <remarks>2015/10/19 17:27:14</remarks>
        public bool Insert(ManagerskillWillsrcEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ManagerskillWillsrc_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@PartMap", DbType.AnsiString, entity.PartMap);
			database.AddInParameter(commandWrapper, "@EnableFlag", DbType.Int32, entity.EnableFlag);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Id", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Id);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Id=(System.Int64)database.GetParameterValue(commandWrapper, "@Id");
            
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
        /// <remarks>2015/10/19 17:27:14</remarks>
        public bool Update(ManagerskillWillsrcEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ManagerskillWillsrc_Update");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, entity.Id);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, entity.SkillCode);
			database.AddInParameter(commandWrapper, "@PartMap", DbType.AnsiString, entity.PartMap);
			database.AddInParameter(commandWrapper, "@EnableFlag", DbType.Int32, entity.EnableFlag);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Id=(System.Int64)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

