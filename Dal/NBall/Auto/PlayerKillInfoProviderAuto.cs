

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
    
    public partial class PlayerkillInfoProvider
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
		/// 将IDataReader的当前记录读取到PlayerkillInfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public PlayerkillInfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new PlayerkillInfoEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.RemainTimes = (System.Int32) reader["RemainTimes"];
            obj.RemainByTimes = (System.Int32) reader["RemainByTimes"];
            obj.BuyTimes = (System.Int32) reader["BuyTimes"];
            obj.DayWinTimes = (System.Int32) reader["DayWinTimes"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.Win = (System.Int32) reader["Win"];
            obj.Lose = (System.Int32) reader["Lose"];
            obj.Draw = (System.Int32) reader["Draw"];
            obj.LotteryMatchId = (System.Guid) reader["LotteryMatchId"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.OpponentInfo = (System.Byte[]) reader["OpponentInfo"];
            obj.OpponentRefreshTime = (System.DateTime) reader["OpponentRefreshTime"];
            obj.DayPoint = (System.Int32) reader["DayPoint"];
            obj.SpecialItemNumber = (System.Int32) reader["SpecialItemNumber"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<PlayerkillInfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<PlayerkillInfoEntity>();
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
        public PlayerkillInfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public PlayerkillInfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>PlayerkillInfoEntity</returns>
        /// <remarks>2016/9/6 14:33:26</remarks>
        public PlayerkillInfoEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PlayerkillInfo_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            PlayerkillInfoEntity obj=null;
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
        /// <returns>PlayerkillInfoEntity列表</returns>
        /// <remarks>2016/9/6 14:33:26</remarks>
        public List<PlayerkillInfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PlayerkillInfo_GetAll");
            

            
            List<PlayerkillInfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  ResetByTimes
		
		/// <summary>
        /// ResetByTimes
        /// </summary>
		/// <param name="configTimes">configTimes</param>
		/// <param name="configByTimes">configByTimes</param>
		/// <param name="updateDate">updateDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/6 14:33:26</remarks>
        public bool ResetByTimes ( System.Int32 configTimes, System.Int32 configByTimes, System.DateTime updateDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PlayerKill_ResetByTimes");
            
			database.AddInParameter(commandWrapper, "@ConfigTimes", DbType.Int32, configTimes);
			database.AddInParameter(commandWrapper, "@ConfigByTimes", DbType.Int32, configByTimes);
			database.AddInParameter(commandWrapper, "@UpdateDate", DbType.DateTime, updateDate);

            
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
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/6 14:33:26</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PlayerkillInfo_Delete");
            
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
		
		#region  SaveFightResult
		
		/// <summary>
        /// SaveFightResult
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="logo">logo</param>
		/// <param name="awayId">awayId</param>
		/// <param name="lotteryMatchId">lotteryMatchId</param>
		/// <param name="win">win</param>
		/// <param name="lose">lose</param>
		/// <param name="draw">draw</param>
		/// <param name="curTime">curTime</param>
		/// <param name="matchId">matchId</param>
		/// <param name="homeName">homeName</param>
		/// <param name="awayName">awayName</param>
		/// <param name="homeScore">homeScore</param>
		/// <param name="awayScore">awayScore</param>
		/// <param name="prizeExp">prizeExp</param>
		/// <param name="prizeCoin">prizeCoin</param>
		/// <param name="prizeItemCode">prizeItemCode</param>
		/// <param name="prizeItemString">prizeItemString</param>
		/// <param name="isRevenge">isRevenge</param>
		/// <param name="revengeRecordId">revengeRecordId</param>
		/// <param name="prizeItemCount">prizeItemCount</param>
		/// <param name="outRevengeRecordId">outRevengeRecordId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/6 14:33:26</remarks>
        public bool SaveFightResult ( System.Guid managerId, System.String logo, System.Guid awayId, System.Guid lotteryMatchId, System.Int32 win, System.Int32 lose, System.Int32 draw, System.DateTime curTime, System.Guid matchId, System.String homeName, System.String awayName, System.Int32 homeScore, System.Int32 awayScore, System.Int32 prizeExp, System.Int32 prizeCoin, System.Int32 prizeItemCode, System.String prizeItemString, System.Boolean isRevenge, System.Int64 revengeRecordId, System.Int32 prizeItemCount,ref  System.Int64 outRevengeRecordId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PlayerKill_SaveFightResult");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, logo);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, awayId);
			database.AddInParameter(commandWrapper, "@LotteryMatchId", DbType.Guid, lotteryMatchId);
			database.AddInParameter(commandWrapper, "@Win", DbType.Int32, win);
			database.AddInParameter(commandWrapper, "@Lose", DbType.Int32, lose);
			database.AddInParameter(commandWrapper, "@Draw", DbType.Int32, draw);
			database.AddInParameter(commandWrapper, "@CurTime", DbType.DateTime, curTime);
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, matchId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.String, homeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.String, awayName);
			database.AddInParameter(commandWrapper, "@HomeScore", DbType.Int32, homeScore);
			database.AddInParameter(commandWrapper, "@AwayScore", DbType.Int32, awayScore);
			database.AddInParameter(commandWrapper, "@PrizeExp", DbType.Int32, prizeExp);
			database.AddInParameter(commandWrapper, "@PrizeCoin", DbType.Int32, prizeCoin);
			database.AddInParameter(commandWrapper, "@PrizeItemCode", DbType.Int32, prizeItemCode);
			database.AddInParameter(commandWrapper, "@PrizeItemString", DbType.AnsiString, prizeItemString);
			database.AddInParameter(commandWrapper, "@IsRevenge", DbType.Boolean, isRevenge);
			database.AddInParameter(commandWrapper, "@RevengeRecordId", DbType.Int64, revengeRecordId);
			database.AddInParameter(commandWrapper, "@PrizeItemCount", DbType.Int32, prizeItemCount);
			database.AddParameter(commandWrapper, "@OutRevengeRecordId", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,outRevengeRecordId);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            outRevengeRecordId=(System.Int64)database.GetParameterValue(commandWrapper, "@OutRevengeRecordId");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  LotterySave
		
		/// <summary>
        /// LotterySave
        /// </summary>
		/// <param name="matchId">matchId</param>
		/// <param name="managerId">managerId</param>
		/// <param name="lotteryRepeatCode">lotteryRepeatCode</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/6 14:33:26</remarks>
        public bool LotterySave ( System.Guid matchId, System.Guid managerId, System.Int32 lotteryRepeatCode,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PlayerKill_LotterySave");
            
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, matchId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@LotteryRepeatCode", DbType.Int32, lotteryRepeatCode);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetMatchTimes
		
		/// <summary>
        /// GetMatchTimes
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="matchCount">matchCount</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/6 14:33:26</remarks>
        public bool GetMatchTimes ( System.Guid managerId,ref  System.Int32 matchCount,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PlayerKill_GetMatchTimes");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@MatchCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,matchCount);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            matchCount=(System.Int32)database.GetParameterValue(commandWrapper, "@MatchCount");
            
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
        /// <remarks>2016/9/6 14:33:26</remarks>
        public bool Insert(PlayerkillInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PlayerkillInfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@RemainTimes", DbType.Int32, entity.RemainTimes);
			database.AddInParameter(commandWrapper, "@RemainByTimes", DbType.Int32, entity.RemainByTimes);
			database.AddInParameter(commandWrapper, "@BuyTimes", DbType.Int32, entity.BuyTimes);
			database.AddInParameter(commandWrapper, "@DayWinTimes", DbType.Int32, entity.DayWinTimes);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@Win", DbType.Int32, entity.Win);
			database.AddInParameter(commandWrapper, "@Lose", DbType.Int32, entity.Lose);
			database.AddInParameter(commandWrapper, "@Draw", DbType.Int32, entity.Draw);
			database.AddInParameter(commandWrapper, "@LotteryMatchId", DbType.Guid, entity.LotteryMatchId);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@OpponentInfo", DbType.Binary, entity.OpponentInfo);
			database.AddInParameter(commandWrapper, "@OpponentRefreshTime", DbType.DateTime, entity.OpponentRefreshTime);
			database.AddInParameter(commandWrapper, "@DayPoint", DbType.Int32, entity.DayPoint);
			database.AddInParameter(commandWrapper, "@SpecialItemNumber", DbType.Int32, entity.SpecialItemNumber);
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
        /// <remarks>2016/9/6 14:33:26</remarks>
        public bool Update(PlayerkillInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PlayerkillInfo_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@RemainTimes", DbType.Int32, entity.RemainTimes);
			database.AddInParameter(commandWrapper, "@RemainByTimes", DbType.Int32, entity.RemainByTimes);
			database.AddInParameter(commandWrapper, "@BuyTimes", DbType.Int32, entity.BuyTimes);
			database.AddInParameter(commandWrapper, "@DayWinTimes", DbType.Int32, entity.DayWinTimes);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@Win", DbType.Int32, entity.Win);
			database.AddInParameter(commandWrapper, "@Lose", DbType.Int32, entity.Lose);
			database.AddInParameter(commandWrapper, "@Draw", DbType.Int32, entity.Draw);
			database.AddInParameter(commandWrapper, "@LotteryMatchId", DbType.Guid, entity.LotteryMatchId);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@OpponentInfo", DbType.Binary, entity.OpponentInfo);
			database.AddInParameter(commandWrapper, "@OpponentRefreshTime", DbType.DateTime, entity.OpponentRefreshTime);
			database.AddInParameter(commandWrapper, "@DayPoint", DbType.Int32, entity.DayPoint);
			database.AddInParameter(commandWrapper, "@SpecialItemNumber", DbType.Int32, entity.SpecialItemNumber);

            
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
