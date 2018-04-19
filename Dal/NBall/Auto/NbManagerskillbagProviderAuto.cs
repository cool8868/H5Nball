

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
    
    public partial class NbManagerskillbagProvider
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
		/// 将IDataReader的当前记录读取到NbManagerskillbagEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbManagerskillbagEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbManagerskillbagEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.SetSkills = (System.String) reader["SetSkills"];
            obj.SetMap = (System.String) reader["SetMap"];
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
        public List<NbManagerskillbagEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbManagerskillbagEntity>();
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
        public NbManagerskillbagProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbManagerskillbagProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>NbManagerskillbagEntity</returns>
        /// <remarks>2016/1/21 18:10:01</remarks>
        public NbManagerskillbagEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagerskillbag_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            NbManagerskillbagEntity obj=null;
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
        /// <returns>NbManagerskillbagEntity列表</returns>
        /// <remarks>2016/1/21 18:10:01</remarks>
        public List<NbManagerskillbagEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagerskillbag_GetAll");
            

            
            List<NbManagerskillbagEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Add
		
		/// <summary>
        /// Add
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="onItemMap">onItemMap</param>
		/// <param name="bagRowVersion">bagRowVersion</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/21 18:10:01</remarks>
        public bool Add ( System.Guid managerId, System.String onItemMap, System.Byte[] bagRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_SkillCard_Add");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@OnItemMap", DbType.AnsiString, onItemMap);
			database.AddInParameter(commandWrapper, "@BagRowVersion", DbType.Binary, bagRowVersion);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Set
		
		/// <summary>
        /// Set
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="setSkills">setSkills</param>
		/// <param name="formRowVersion">formRowVersion</param>
		/// <param name="bagSetMap">bagSetMap</param>
		/// <param name="bagRowVersion">bagRowVersion</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/21 18:10:01</remarks>
        public bool Set ( System.Guid managerId, System.String setSkills, System.Byte[] formRowVersion, System.String bagSetMap, System.Byte[] bagRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_SkillCard_Set");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SetSkills", DbType.AnsiString, setSkills);
			database.AddInParameter(commandWrapper, "@FormRowVersion", DbType.Binary, formRowVersion);
			database.AddInParameter(commandWrapper, "@BagSetMap", DbType.AnsiString, bagSetMap);
			database.AddInParameter(commandWrapper, "@BagRowVersion", DbType.Binary, bagRowVersion);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  MixUpTran
		
		/// <summary>
        /// MixUpTran
        /// </summary>
		/// <param name="tranFlag">tranFlag</param>
		/// <param name="managerId">managerId</param>
		/// <param name="setSkills">setSkills</param>
		/// <param name="setMap">setMap</param>
		/// <param name="itemMap">itemMap</param>
		/// <param name="bagRowVersion">bagRowVersion</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/21 18:10:01</remarks>
        public bool MixUpTran ( System.Boolean tranFlag, System.Guid managerId, System.String setSkills, System.String setMap, System.String itemMap, System.Byte[] bagRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_SkillCard_MixUpTran");
            
			database.AddInParameter(commandWrapper, "@TranFlag", DbType.Boolean, tranFlag);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SetSkills", DbType.AnsiString, setSkills);
			database.AddInParameter(commandWrapper, "@SetMap", DbType.AnsiString, setMap);
			database.AddInParameter(commandWrapper, "@ItemMap", DbType.AnsiString, itemMap);
			database.AddInParameter(commandWrapper, "@BagRowVersion", DbType.Binary, bagRowVersion);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
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
        /// <remarks>2016/1/21 18:10:01</remarks>
        public bool Insert(NbManagerskillbagEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagerskillbag_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SetSkills", DbType.AnsiString, entity.SetSkills);
			database.AddInParameter(commandWrapper, "@SetMap", DbType.AnsiString, entity.SetMap);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@ManagerId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.ManagerId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
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
        /// <remarks>2016/1/21 18:10:01</remarks>
        public bool Update(NbManagerskillbagEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagerskillbag_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@SetSkills", DbType.AnsiString, entity.SetSkills);
			database.AddInParameter(commandWrapper, "@SetMap", DbType.AnsiString, entity.SetMap);
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

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

