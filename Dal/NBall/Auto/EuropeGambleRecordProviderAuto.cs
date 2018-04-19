

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
    
    public partial class EuropeGamblerecordProvider
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
		/// 将IDataReader的当前记录读取到EuropeGamblerecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public EuropeGamblerecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new EuropeGamblerecordEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.MatchId = (System.Int32) reader["MatchId"];
            obj.GambleType = (System.Int32) reader["GambleType"];
            obj.Point = (System.Int32) reader["Point"];
            obj.ReturnPoint = (System.Int32) reader["ReturnPoint"];
            obj.IsSendPrize = (System.Boolean) reader["IsSendPrize"];
            obj.IsGambleCorrect = (System.Boolean) reader["IsGambleCorrect"];
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
        public List<EuropeGamblerecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<EuropeGamblerecordEntity>();
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
        public EuropeGamblerecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public EuropeGamblerecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>EuropeGamblerecordEntity</returns>
        /// <remarks>2016/6/10 17:42:50</remarks>
        public EuropeGamblerecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_EuropeGamblerecord_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            EuropeGamblerecordEntity obj=null;
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
		
		#region  GambleRecord
		
		/// <summary>
        /// GambleRecord
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="matchId">matchId</param>
        /// <returns>EuropeGamblerecordEntity</returns>
        /// <remarks>2016/6/10 17:42:50</remarks>
        public EuropeGamblerecordEntity GambleRecord( System.Guid managerId, System.Int32 matchId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Europe_GambleRecord_GetGamble");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Int32, matchId);

            
            EuropeGamblerecordEntity obj=null;
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
        /// <returns>EuropeGamblerecordEntity列表</returns>
        /// <remarks>2016/6/10 17:42:50</remarks>
        public List<EuropeGamblerecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_EuropeGamblerecord_GetAll");
            

            
            List<EuropeGamblerecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GambleRecord
		
		/// <summary>
        /// GambleRecord
        /// </summary>
        /// <returns>EuropeGamblerecordEntity列表</returns>
        /// <remarks>2016/6/10 17:42:50</remarks>
        public List<EuropeGamblerecordEntity> GambleRecord( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Europe_GambleRecord_GetByManager");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<EuropeGamblerecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetNotPrize
		
		/// <summary>
        /// GetNotPrize
        /// </summary>
        /// <returns>EuropeGamblerecordEntity列表</returns>
        /// <remarks>2016/6/10 17:42:50</remarks>
        public List<EuropeGamblerecordEntity> GetNotPrize( System.Int32 matchId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_EuropeGambleRecord_GetNotPrize");
            
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Int32, matchId);

            
            List<EuropeGamblerecordEntity> list = null;
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
        /// <remarks>2016/6/10 17:42:50</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_EuropeGamblerecord_Delete");
            
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
        /// <remarks>2016/6/10 17:42:50</remarks>
        public bool Insert(EuropeGamblerecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_EuropeGamblerecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Int32, entity.MatchId);
			database.AddInParameter(commandWrapper, "@GambleType", DbType.Int32, entity.GambleType);
			database.AddInParameter(commandWrapper, "@Point", DbType.Int32, entity.Point);
			database.AddInParameter(commandWrapper, "@ReturnPoint", DbType.Int32, entity.ReturnPoint);
			database.AddInParameter(commandWrapper, "@IsSendPrize", DbType.Boolean, entity.IsSendPrize);
			database.AddInParameter(commandWrapper, "@IsGambleCorrect", DbType.Boolean, entity.IsGambleCorrect);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
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
        /// <remarks>2016/6/10 17:42:50</remarks>
        public bool Update(EuropeGamblerecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_EuropeGamblerecord_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Int32, entity.MatchId);
			database.AddInParameter(commandWrapper, "@GambleType", DbType.Int32, entity.GambleType);
			database.AddInParameter(commandWrapper, "@Point", DbType.Int32, entity.Point);
			database.AddInParameter(commandWrapper, "@ReturnPoint", DbType.Int32, entity.ReturnPoint);
			database.AddInParameter(commandWrapper, "@IsSendPrize", DbType.Boolean, entity.IsSendPrize);
			database.AddInParameter(commandWrapper, "@IsGambleCorrect", DbType.Boolean, entity.IsGambleCorrect);
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

            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

