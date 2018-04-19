

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
    
    public partial class NbSolutionProvider
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
		/// 将IDataReader的当前记录读取到NbSolutionEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbSolutionEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbSolutionEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.FormationId = (System.Int32) reader["FormationId"];
            obj.PlayerString = (System.String) reader["PlayerString"];
            obj.SkillString = (System.String) reader["SkillString"];
            obj.FormationData = (System.Byte[]) reader["FormationData"];
            obj.VeteranCount = (System.Int32) reader["VeteranCount"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.OrangeCount = (System.Int32) reader["OrangeCount"];
            obj.CombCount = (System.Int32) reader["CombCount"];
            obj.HirePlayerString = (System.String) reader["HirePlayerString"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<NbSolutionEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbSolutionEntity>();
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
        public NbSolutionProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbSolutionProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>NbSolutionEntity</returns>
        /// <remarks>2015/10/18 17:21:42</remarks>
        public NbSolutionEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbSolution_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            NbSolutionEntity obj=null;
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
        /// <returns>NbSolutionEntity列表</returns>
        /// <remarks>2015/10/18 17:21:42</remarks>
        public List<NbSolutionEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbSolution_GetAll");
            

            
            List<NbSolutionEntity> list = null;
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
        /// <remarks>2015/10/18 17:21:42</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbSolution_Delete");
            
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
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 17:21:42</remarks>
        public bool Insert(NbSolutionEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbSolution_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@FormationId", DbType.Int32, entity.FormationId);
			database.AddInParameter(commandWrapper, "@PlayerString", DbType.AnsiStringFixedLength, entity.PlayerString);
			database.AddInParameter(commandWrapper, "@SkillString", DbType.AnsiString, entity.SkillString);
			database.AddInParameter(commandWrapper, "@FormationData", DbType.Binary, entity.FormationData);
			database.AddInParameter(commandWrapper, "@VeteranCount", DbType.Int32, entity.VeteranCount);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@OrangeCount", DbType.Int32, entity.OrangeCount);
			database.AddInParameter(commandWrapper, "@CombCount", DbType.Int32, entity.CombCount);
			database.AddInParameter(commandWrapper, "@HirePlayerString", DbType.AnsiStringFixedLength, entity.HirePlayerString);
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
        /// <remarks>2015/10/18 17:21:42</remarks>
        public bool Update(NbSolutionEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbSolution_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@FormationId", DbType.Int32, entity.FormationId);
			database.AddInParameter(commandWrapper, "@PlayerString", DbType.AnsiStringFixedLength, entity.PlayerString);
			database.AddInParameter(commandWrapper, "@SkillString", DbType.AnsiString, entity.SkillString);
			database.AddInParameter(commandWrapper, "@FormationData", DbType.Binary, entity.FormationData);
			database.AddInParameter(commandWrapper, "@VeteranCount", DbType.Int32, entity.VeteranCount);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@OrangeCount", DbType.Int32, entity.OrangeCount);
			database.AddInParameter(commandWrapper, "@CombCount", DbType.Int32, entity.CombCount);
			database.AddInParameter(commandWrapper, "@HirePlayerString", DbType.AnsiStringFixedLength, entity.HirePlayerString);

            
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

