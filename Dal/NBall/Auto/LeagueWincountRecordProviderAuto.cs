

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
    
    public partial class LeagueWincountrecordProvider
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
		/// 将IDataReader的当前记录读取到LeagueWincountrecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LeagueWincountrecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LeagueWincountrecordEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.LeagueId = (System.Int32) reader["LeagueId"];
            obj.WinCount1 = (System.Int32) reader["WinCount1"];
            obj.WinCount1Status = (System.Int32) reader["WinCount1Status"];
            obj.WinCount2 = (System.Int32) reader["WinCount2"];
            obj.WinCount2Status = (System.Int32) reader["WinCount2Status"];
            obj.WinCount3 = (System.Int32) reader["WinCount3"];
            obj.WinCount3Status = (System.Int32) reader["WinCount3Status"];
            obj.MaxWinCount = (System.Int32) reader["MaxWinCount"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.PrizeDate = (System.DateTime) reader["PrizeDate"];
            obj.PrizeStep = (System.String) reader["PrizeStep"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<LeagueWincountrecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LeagueWincountrecordEntity>();
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
        public LeagueWincountrecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LeagueWincountrecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>LeagueWincountrecordEntity</returns>
        /// <remarks>2016/6/17 10:32:30</remarks>
        public LeagueWincountrecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueWincountrecord_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            LeagueWincountrecordEntity obj=null;
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
		
		#region  GetRecord
		
		/// <summary>
        /// GetRecord
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="leagueId">leagueId</param>
        /// <returns>LeagueWincountrecordEntity</returns>
        /// <remarks>2016/6/17 10:32:30</remarks>
        public LeagueWincountrecordEntity GetRecord( System.Guid managerId, System.Int32 leagueId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_LeagueWincountRecord_GetRecord");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@LeagueId", DbType.Int32, leagueId);

            
            LeagueWincountrecordEntity obj=null;
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
        /// <returns>LeagueWincountrecordEntity列表</returns>
        /// <remarks>2016/6/17 10:32:30</remarks>
        public List<LeagueWincountrecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueWincountrecord_GetAll");
            

            
            List<LeagueWincountrecordEntity> list = null;
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
        /// <remarks>2016/6/17 10:32:30</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueWincountrecord_Delete");
            
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
        /// <remarks>2016/6/17 10:32:30</remarks>
        public bool Insert(LeagueWincountrecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueWincountrecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@LeagueId", DbType.Int32, entity.LeagueId);
			database.AddInParameter(commandWrapper, "@WinCount1", DbType.Int32, entity.WinCount1);
			database.AddInParameter(commandWrapper, "@WinCount1Status", DbType.Int32, entity.WinCount1Status);
			database.AddInParameter(commandWrapper, "@WinCount2", DbType.Int32, entity.WinCount2);
			database.AddInParameter(commandWrapper, "@WinCount2Status", DbType.Int32, entity.WinCount2Status);
			database.AddInParameter(commandWrapper, "@WinCount3", DbType.Int32, entity.WinCount3);
			database.AddInParameter(commandWrapper, "@WinCount3Status", DbType.Int32, entity.WinCount3Status);
			database.AddInParameter(commandWrapper, "@MaxWinCount", DbType.Int32, entity.MaxWinCount);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@PrizeDate", DbType.Date, entity.PrizeDate);
			database.AddInParameter(commandWrapper, "@PrizeStep", DbType.AnsiString, entity.PrizeStep);
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
        /// <remarks>2016/6/17 10:32:30</remarks>
        public bool Update(LeagueWincountrecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueWincountrecord_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@LeagueId", DbType.Int32, entity.LeagueId);
			database.AddInParameter(commandWrapper, "@WinCount1", DbType.Int32, entity.WinCount1);
			database.AddInParameter(commandWrapper, "@WinCount1Status", DbType.Int32, entity.WinCount1Status);
			database.AddInParameter(commandWrapper, "@WinCount2", DbType.Int32, entity.WinCount2);
			database.AddInParameter(commandWrapper, "@WinCount2Status", DbType.Int32, entity.WinCount2Status);
			database.AddInParameter(commandWrapper, "@WinCount3", DbType.Int32, entity.WinCount3);
			database.AddInParameter(commandWrapper, "@WinCount3Status", DbType.Int32, entity.WinCount3Status);
			database.AddInParameter(commandWrapper, "@MaxWinCount", DbType.Int32, entity.MaxWinCount);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@PrizeDate", DbType.Date, entity.PrizeDate);
			database.AddInParameter(commandWrapper, "@PrizeStep", DbType.AnsiString, entity.PrizeStep);

            
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

