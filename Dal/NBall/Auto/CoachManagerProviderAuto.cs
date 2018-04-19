

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
    
    public partial class CoachManagerProvider
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
		/// 将IDataReader的当前记录读取到CoachManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public CoachManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new CoachManagerEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.HaveExp = (System.Int32) reader["HaveExp"];
            obj.CoachString = (System.Byte[]) reader["CoachString"];
            obj.EnableCoachId = (System.Int32) reader["EnableCoachId"];
            obj.EnableCoachLevel = (System.Int32) reader["EnableCoachLevel"];
            obj.EnableCoachStar = (System.Int32) reader["EnableCoachStar"];
            obj.EnableCoachSkillLevel = (System.Int32) reader["EnableCoachSkillLevel"];
            obj.Offensive = (System.Decimal) reader["Offensive"];
            obj.Organizational = (System.Decimal) reader["Organizational"];
            obj.Defense = (System.Decimal) reader["Defense"];
            obj.BodyAttr = (System.Decimal) reader["BodyAttr"];
            obj.Goalkeeping = (System.Decimal) reader["Goalkeeping"];
            obj.Status = (System.Int32) reader["Status"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
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
        public List<CoachManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<CoachManagerEntity>();
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
        public CoachManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public CoachManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>CoachManagerEntity</returns>
        /// <remarks>2017/3/3 15:56:41</remarks>
        public CoachManagerEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CoachManager_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            CoachManagerEntity obj=null;
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
        /// <returns>CoachManagerEntity列表</returns>
        /// <remarks>2017/3/3 15:56:41</remarks>
        public List<CoachManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CoachManager_GetAll");
            

            
            List<CoachManagerEntity> list = null;
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
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2017/3/3 15:56:41</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CoachManager_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
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
		
		#region  AddExp
		
		/// <summary>
        /// AddExp
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="number">number</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2017/3/3 15:56:41</remarks>
        public bool AddExp ( System.Guid managerId, System.Int32 number,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Coach_AddExp");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Number", DbType.Int32, number);

            
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
		
		#region  CostExpRecord
		
		/// <summary>
        /// CostExpRecord
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="number">number</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2017/3/3 15:56:41</remarks>
        public bool CostExpRecord ( System.Guid managerId, System.Int32 number,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Coach_CostExpRecord");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Number", DbType.Int32, number);

            
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
        /// <remarks>2017/3/3 15:56:41</remarks>
        public bool Insert(CoachManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CoachManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@HaveExp", DbType.Int32, entity.HaveExp);
			database.AddInParameter(commandWrapper, "@CoachString", DbType.Binary, entity.CoachString);
			database.AddInParameter(commandWrapper, "@EnableCoachId", DbType.Int32, entity.EnableCoachId);
			database.AddInParameter(commandWrapper, "@EnableCoachLevel", DbType.Int32, entity.EnableCoachLevel);
			database.AddInParameter(commandWrapper, "@EnableCoachStar", DbType.Int32, entity.EnableCoachStar);
			database.AddInParameter(commandWrapper, "@EnableCoachSkillLevel", DbType.Int32, entity.EnableCoachSkillLevel);
			database.AddInParameter(commandWrapper, "@Offensive", DbType.Decimal, entity.Offensive);
			database.AddInParameter(commandWrapper, "@Organizational", DbType.Decimal, entity.Organizational);
			database.AddInParameter(commandWrapper, "@Defense", DbType.Decimal, entity.Defense);
			database.AddInParameter(commandWrapper, "@BodyAttr", DbType.Decimal, entity.BodyAttr);
			database.AddInParameter(commandWrapper, "@Goalkeeping", DbType.Decimal, entity.Goalkeeping);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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
        /// <remarks>2017/3/3 15:56:41</remarks>
        public bool Update(CoachManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CoachManager_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@HaveExp", DbType.Int32, entity.HaveExp);
			database.AddInParameter(commandWrapper, "@CoachString", DbType.Binary, entity.CoachString);
			database.AddInParameter(commandWrapper, "@EnableCoachId", DbType.Int32, entity.EnableCoachId);
			database.AddInParameter(commandWrapper, "@EnableCoachLevel", DbType.Int32, entity.EnableCoachLevel);
			database.AddInParameter(commandWrapper, "@EnableCoachStar", DbType.Int32, entity.EnableCoachStar);
			database.AddInParameter(commandWrapper, "@EnableCoachSkillLevel", DbType.Int32, entity.EnableCoachSkillLevel);
			database.AddInParameter(commandWrapper, "@Offensive", DbType.Decimal, entity.Offensive);
			database.AddInParameter(commandWrapper, "@Organizational", DbType.Decimal, entity.Organizational);
			database.AddInParameter(commandWrapper, "@Defense", DbType.Decimal, entity.Defense);
			database.AddInParameter(commandWrapper, "@BodyAttr", DbType.Decimal, entity.BodyAttr);
			database.AddInParameter(commandWrapper, "@Goalkeeping", DbType.Decimal, entity.Goalkeeping);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
