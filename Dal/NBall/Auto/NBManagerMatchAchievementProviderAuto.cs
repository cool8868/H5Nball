

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
    
    public partial class NbManagermatchachievementProvider
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
		/// 将IDataReader的当前记录读取到NbManagermatchachievementEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbManagermatchachievementEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbManagermatchachievementEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.MatchType = (System.Int32) reader["MatchType"];
            obj.MatchTypeId = (System.Int32) reader["MatchTypeId"];
            obj.RankIndex = (System.Int32) reader["RankIndex"];
            obj.Status = (System.Int32) reader["Status"];
            obj.Rowtime = (System.DateTime) reader["Rowtime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<NbManagermatchachievementEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbManagermatchachievementEntity>();
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
        public NbManagermatchachievementProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbManagermatchachievementProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetByManagerIdAndTypeId
		
		/// <summary>
        /// GetByManagerIdAndTypeId
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="typeId">typeId</param>
        /// <returns>NbManagermatchachievementEntity</returns>
        /// <remarks>2015/10/19 17:31:11</remarks>
        public NbManagermatchachievementEntity GetByManagerIdAndTypeId( System.Guid managerId, System.Int32 typeId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NbManagerMatchAchievement_GetByManagerIdAndTypeId");
            
			database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@typeId", DbType.Int32, typeId);

            
            NbManagermatchachievementEntity obj=null;
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
		
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>NbManagermatchachievementEntity</returns>
        /// <remarks>2015/10/19 17:31:11</remarks>
        public NbManagermatchachievementEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagermatchachievement_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            NbManagermatchachievementEntity obj=null;
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
        /// <returns>NbManagermatchachievementEntity列表</returns>
        /// <remarks>2015/10/19 17:31:11</remarks>
        public List<NbManagermatchachievementEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagermatchachievement_GetAll");
            

            
            List<NbManagermatchachievementEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetCountByMatchTypeId
		
		/// <summary>
        /// GetCountByMatchTypeId
        /// </summary>
		/// <param name="matchTypeId">matchTypeId</param>
        /// <returns>Int32</returns>
        /// <remarks>2015/10/19 17:31:11</remarks>
        public Int32 GetCountByMatchTypeId ( System.Int32 matchTypeId)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NBManagerMatchAchievement_GetCountByMatchTypeId");
            
			database.AddInParameter(commandWrapper, "@matchTypeId", DbType.Int32, matchTypeId);

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  Record
		
		/// <summary>
        /// Record
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="matchType">matchType</param>
		/// <param name="matchTypeId">matchTypeId</param>
		/// <param name="rankIndex">rankIndex</param>
		/// <param name="status">status</param>
		/// <param name="rowtime">rowtime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:31:11</remarks>
        public bool Record ( System.Guid managerId, System.Int32 matchType, System.Int32 matchTypeId, System.Int32 rankIndex, System.Int32 status, System.DateTime rowtime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NbManagermatchachievement_Record");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@MatchType", DbType.Int32, matchType);
			database.AddInParameter(commandWrapper, "@MatchTypeId", DbType.Int32, matchTypeId);
			database.AddInParameter(commandWrapper, "@RankIndex", DbType.Int32, rankIndex);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);
			database.AddInParameter(commandWrapper, "@Rowtime", DbType.DateTime, rowtime);

            
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
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:31:11</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagermatchachievement_Delete");
            
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
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/19 17:31:11</remarks>
        public bool Insert(NbManagermatchachievementEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagermatchachievement_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MatchType", DbType.Int32, entity.MatchType);
			database.AddInParameter(commandWrapper, "@MatchTypeId", DbType.Int32, entity.MatchTypeId);
			database.AddInParameter(commandWrapper, "@RankIndex", DbType.Int32, entity.RankIndex);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Rowtime", DbType.DateTime, entity.Rowtime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
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
        /// <remarks>2015/10/19 17:31:11</remarks>
        public bool Update(NbManagermatchachievementEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagermatchachievement_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MatchType", DbType.Int32, entity.MatchType);
			database.AddInParameter(commandWrapper, "@MatchTypeId", DbType.Int32, entity.MatchTypeId);
			database.AddInParameter(commandWrapper, "@RankIndex", DbType.Int32, entity.RankIndex);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Rowtime", DbType.DateTime, entity.Rowtime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

