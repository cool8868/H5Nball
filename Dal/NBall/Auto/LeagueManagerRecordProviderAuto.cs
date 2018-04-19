

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
    
    public partial class LeagueManagerrecordProvider
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
		/// 将IDataReader的当前记录读取到LeagueManagerrecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LeagueManagerrecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LeagueManagerrecordEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.LaegueId = (System.Int32) reader["LaegueId"];
            obj.LeagueRecordId = (System.Guid) reader["LeagueRecordId"];
            obj.LastPrizeLeagueRecordId = (System.Guid) reader["LastPrizeLeagueRecordId"];
            obj.SendFirstPassPrize = (System.Boolean) reader["SendFirstPassPrize"];
            obj.MatchId = (System.Guid) reader["MatchId"];
            obj.MaxWheelNumber = (System.Int32) reader["MaxWheelNumber"];
            obj.Score = (System.Int32) reader["Score"];
            obj.IsLock = (System.Boolean) reader["IsLock"];
            obj.IsStart = (System.Boolean) reader["IsStart"];
            obj.IsPass = (System.Boolean) reader["IsPass"];
            obj.PassNumber = (System.Int32) reader["PassNumber"];
            obj.MatchNumber = (System.Int32) reader["MatchNumber"];
            obj.WinNumber = (System.Int32) reader["WinNumber"];
            obj.FlatNumber = (System.Int32) reader["FlatNumber"];
            obj.LoseNumber = (System.Int32) reader["LoseNumber"];
            obj.GoalsNumber = (System.Int32) reader["GoalsNumber"];
            obj.FumbleNumber = (System.Int32) reader["FumbleNumber"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.FightDicId = (System.Int32) reader["FightDicId"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<LeagueManagerrecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LeagueManagerrecordEntity>();
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
        public LeagueManagerrecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LeagueManagerrecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>LeagueManagerrecordEntity</returns>
        /// <remarks>2016/6/16 12:22:59</remarks>
        public LeagueManagerrecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueManagerrecord_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            LeagueManagerrecordEntity obj=null;
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
		
		#region  GetManagerMarkInfo
		
		/// <summary>
        /// GetManagerMarkInfo
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="leagueId">leagueId</param>
        /// <returns>LeagueManagerrecordEntity</returns>
        /// <remarks>2016/6/16 12:22:59</remarks>
        public LeagueManagerrecordEntity GetManagerMarkInfo( System.Guid managerId, System.Int32 leagueId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_League_GetManagerMarkInfo");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@LeagueId", DbType.Int32, leagueId);

            
            LeagueManagerrecordEntity obj=null;
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
        /// <returns>LeagueManagerrecordEntity列表</returns>
        /// <remarks>2016/6/16 12:22:59</remarks>
        public List<LeagueManagerrecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueManagerrecord_GetAll");
            

            
            List<LeagueManagerrecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetManagerAllMark
		
		/// <summary>
        /// GetManagerAllMark
        /// </summary>
        /// <returns>LeagueManagerrecordEntity列表</returns>
        /// <remarks>2016/6/16 12:22:59</remarks>
        public List<LeagueManagerrecordEntity> GetManagerAllMark( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_League_GetManagerAllMark");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<LeagueManagerrecordEntity> list = null;
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
        /// <remarks>2016/6/16 12:22:59</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueManagerrecord_Delete");
            
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
        /// <remarks>2016/6/16 12:22:59</remarks>
        public bool Insert(LeagueManagerrecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueManagerrecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@LaegueId", DbType.Int32, entity.LaegueId);
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, entity.LeagueRecordId);
			database.AddInParameter(commandWrapper, "@LastPrizeLeagueRecordId", DbType.Guid, entity.LastPrizeLeagueRecordId);
			database.AddInParameter(commandWrapper, "@SendFirstPassPrize", DbType.Boolean, entity.SendFirstPassPrize);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, entity.MatchId);
			database.AddInParameter(commandWrapper, "@MaxWheelNumber", DbType.Int32, entity.MaxWheelNumber);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@IsLock", DbType.Boolean, entity.IsLock);
			database.AddInParameter(commandWrapper, "@IsStart", DbType.Boolean, entity.IsStart);
			database.AddInParameter(commandWrapper, "@IsPass", DbType.Boolean, entity.IsPass);
			database.AddInParameter(commandWrapper, "@PassNumber", DbType.Int32, entity.PassNumber);
			database.AddInParameter(commandWrapper, "@MatchNumber", DbType.Int32, entity.MatchNumber);
			database.AddInParameter(commandWrapper, "@WinNumber", DbType.Int32, entity.WinNumber);
			database.AddInParameter(commandWrapper, "@FlatNumber", DbType.Int32, entity.FlatNumber);
			database.AddInParameter(commandWrapper, "@LoseNumber", DbType.Int32, entity.LoseNumber);
			database.AddInParameter(commandWrapper, "@GoalsNumber", DbType.Int32, entity.GoalsNumber);
			database.AddInParameter(commandWrapper, "@FumbleNumber", DbType.Int32, entity.FumbleNumber);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@FightDicId", DbType.Int32, entity.FightDicId);
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
        /// <remarks>2016/6/16 12:22:59</remarks>
        public bool Update(LeagueManagerrecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueManagerrecord_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@LaegueId", DbType.Int32, entity.LaegueId);
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, entity.LeagueRecordId);
			database.AddInParameter(commandWrapper, "@LastPrizeLeagueRecordId", DbType.Guid, entity.LastPrizeLeagueRecordId);
			database.AddInParameter(commandWrapper, "@SendFirstPassPrize", DbType.Boolean, entity.SendFirstPassPrize);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, entity.MatchId);
			database.AddInParameter(commandWrapper, "@MaxWheelNumber", DbType.Int32, entity.MaxWheelNumber);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@IsLock", DbType.Boolean, entity.IsLock);
			database.AddInParameter(commandWrapper, "@IsStart", DbType.Boolean, entity.IsStart);
			database.AddInParameter(commandWrapper, "@IsPass", DbType.Boolean, entity.IsPass);
			database.AddInParameter(commandWrapper, "@PassNumber", DbType.Int32, entity.PassNumber);
			database.AddInParameter(commandWrapper, "@MatchNumber", DbType.Int32, entity.MatchNumber);
			database.AddInParameter(commandWrapper, "@WinNumber", DbType.Int32, entity.WinNumber);
			database.AddInParameter(commandWrapper, "@FlatNumber", DbType.Int32, entity.FlatNumber);
			database.AddInParameter(commandWrapper, "@LoseNumber", DbType.Int32, entity.LoseNumber);
			database.AddInParameter(commandWrapper, "@GoalsNumber", DbType.Int32, entity.GoalsNumber);
			database.AddInParameter(commandWrapper, "@FumbleNumber", DbType.Int32, entity.FumbleNumber);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@FightDicId", DbType.Int32, entity.FightDicId);

            
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

