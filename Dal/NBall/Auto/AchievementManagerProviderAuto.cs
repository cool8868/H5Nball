

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
    
    public partial class AchievementManagerProvider
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
		/// 将IDataReader的当前记录读取到AchievementManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public AchievementManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new AchievementManagerEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.PurpleCardCount = (System.Int32) reader["PurpleCardCount"];
            obj.OrangeCardCount = (System.Int32) reader["OrangeCardCount"];
            obj.SilverCardCount = (System.Int32) reader["SilverCardCount"];
            obj.GoldCardCount = (System.Int32) reader["GoldCardCount"];
            obj.MaxLadderGoals = (System.Int32) reader["MaxLadderGoals"];
            obj.MaxPkMatchGoals = (System.Int32) reader["MaxPkMatchGoals"];
            obj.DayPkMatchGoals = (System.Int32) reader["DayPkMatchGoals"];
            obj.DayPkMatchDate = (System.DateTime) reader["DayPkMatchDate"];
            obj.MaxDayPkMatchGoals = (System.Int32) reader["MaxDayPkMatchGoals"];
            obj.MaxLadderWin = (System.Int32) reader["MaxLadderWin"];
            obj.LadderWin = (System.Int32) reader["LadderWin"];
            obj.LadderSeason = (System.Int32) reader["LadderSeason"];
            obj.FriendWinComb = (System.Int32) reader["FriendWinComb"];
            obj.MaxFriendWinComb = (System.Int32) reader["MaxFriendWinComb"];
            obj.MaxDailyCupRank = (System.Int32) reader["MaxDailyCupRank"];
            obj.Level5CardCount = (System.Int32) reader["Level5CardCount"];
            obj.Level10CardCount = (System.Int32) reader["Level10CardCount"];
            obj.Level20CardCount = (System.Int32) reader["Level20CardCount"];
            obj.Level30CardCount = (System.Int32) reader["Level30CardCount"];
            obj.LeagueScore1 = (System.Int32) reader["LeagueScore1"];
            obj.LeagueScore2 = (System.Int32) reader["LeagueScore2"];
            obj.LeagueScore3 = (System.Int32) reader["LeagueScore3"];
            obj.LeagueScore4 = (System.Int32) reader["LeagueScore4"];
            obj.LeagueScore5 = (System.Int32) reader["LeagueScore5"];
            obj.LeagueScore6 = (System.Int32) reader["LeagueScore6"];
            obj.LeagueScore7 = (System.Int32) reader["LeagueScore7"];
            obj.LeagueScore8 = (System.Int32) reader["LeagueScore8"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<AchievementManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<AchievementManagerEntity>();
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
        public AchievementManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public AchievementManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>AchievementManagerEntity</returns>
        /// <remarks>2016/3/10 10:32:31</remarks>
        public AchievementManagerEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_AchievementManager_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            AchievementManagerEntity obj=null;
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
        /// <returns>AchievementManagerEntity列表</returns>
        /// <remarks>2016/3/10 10:32:31</remarks>
        public List<AchievementManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AchievementManager_GetAll");
            

            
            List<AchievementManagerEntity> list = null;
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
        /// <remarks>2016/3/10 10:32:31</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AchievementManager_Delete");
            
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
        /// <remarks>2016/3/10 10:32:31</remarks>
        public bool Insert(AchievementManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AchievementManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@PurpleCardCount", DbType.Int32, entity.PurpleCardCount);
			database.AddInParameter(commandWrapper, "@OrangeCardCount", DbType.Int32, entity.OrangeCardCount);
			database.AddInParameter(commandWrapper, "@SilverCardCount", DbType.Int32, entity.SilverCardCount);
			database.AddInParameter(commandWrapper, "@GoldCardCount", DbType.Int32, entity.GoldCardCount);
			database.AddInParameter(commandWrapper, "@MaxLadderGoals", DbType.Int32, entity.MaxLadderGoals);
			database.AddInParameter(commandWrapper, "@MaxPkMatchGoals", DbType.Int32, entity.MaxPkMatchGoals);
			database.AddInParameter(commandWrapper, "@DayPkMatchGoals", DbType.Int32, entity.DayPkMatchGoals);
			database.AddInParameter(commandWrapper, "@DayPkMatchDate", DbType.DateTime, entity.DayPkMatchDate);
			database.AddInParameter(commandWrapper, "@MaxDayPkMatchGoals", DbType.Int32, entity.MaxDayPkMatchGoals);
			database.AddInParameter(commandWrapper, "@MaxLadderWin", DbType.Int32, entity.MaxLadderWin);
			database.AddInParameter(commandWrapper, "@LadderWin", DbType.Int32, entity.LadderWin);
			database.AddInParameter(commandWrapper, "@LadderSeason", DbType.Int32, entity.LadderSeason);
			database.AddInParameter(commandWrapper, "@FriendWinComb", DbType.Int32, entity.FriendWinComb);
			database.AddInParameter(commandWrapper, "@MaxFriendWinComb", DbType.Int32, entity.MaxFriendWinComb);
			database.AddInParameter(commandWrapper, "@MaxDailyCupRank", DbType.Int32, entity.MaxDailyCupRank);
			database.AddInParameter(commandWrapper, "@Level5CardCount", DbType.Int32, entity.Level5CardCount);
			database.AddInParameter(commandWrapper, "@Level10CardCount", DbType.Int32, entity.Level10CardCount);
			database.AddInParameter(commandWrapper, "@Level20CardCount", DbType.Int32, entity.Level20CardCount);
			database.AddInParameter(commandWrapper, "@Level30CardCount", DbType.Int32, entity.Level30CardCount);
			database.AddInParameter(commandWrapper, "@LeagueScore1", DbType.Int32, entity.LeagueScore1);
			database.AddInParameter(commandWrapper, "@LeagueScore2", DbType.Int32, entity.LeagueScore2);
			database.AddInParameter(commandWrapper, "@LeagueScore3", DbType.Int32, entity.LeagueScore3);
			database.AddInParameter(commandWrapper, "@LeagueScore4", DbType.Int32, entity.LeagueScore4);
			database.AddInParameter(commandWrapper, "@LeagueScore5", DbType.Int32, entity.LeagueScore5);
			database.AddInParameter(commandWrapper, "@LeagueScore6", DbType.Int32, entity.LeagueScore6);
			database.AddInParameter(commandWrapper, "@LeagueScore7", DbType.Int32, entity.LeagueScore7);
			database.AddInParameter(commandWrapper, "@LeagueScore8", DbType.Int32, entity.LeagueScore8);
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
        /// <remarks>2016/3/10 10:32:31</remarks>
        public bool Update(AchievementManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AchievementManager_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@PurpleCardCount", DbType.Int32, entity.PurpleCardCount);
			database.AddInParameter(commandWrapper, "@OrangeCardCount", DbType.Int32, entity.OrangeCardCount);
			database.AddInParameter(commandWrapper, "@SilverCardCount", DbType.Int32, entity.SilverCardCount);
			database.AddInParameter(commandWrapper, "@GoldCardCount", DbType.Int32, entity.GoldCardCount);
			database.AddInParameter(commandWrapper, "@MaxLadderGoals", DbType.Int32, entity.MaxLadderGoals);
			database.AddInParameter(commandWrapper, "@MaxPkMatchGoals", DbType.Int32, entity.MaxPkMatchGoals);
			database.AddInParameter(commandWrapper, "@DayPkMatchGoals", DbType.Int32, entity.DayPkMatchGoals);
			database.AddInParameter(commandWrapper, "@DayPkMatchDate", DbType.DateTime, entity.DayPkMatchDate);
			database.AddInParameter(commandWrapper, "@MaxDayPkMatchGoals", DbType.Int32, entity.MaxDayPkMatchGoals);
			database.AddInParameter(commandWrapper, "@MaxLadderWin", DbType.Int32, entity.MaxLadderWin);
			database.AddInParameter(commandWrapper, "@LadderWin", DbType.Int32, entity.LadderWin);
			database.AddInParameter(commandWrapper, "@LadderSeason", DbType.Int32, entity.LadderSeason);
			database.AddInParameter(commandWrapper, "@FriendWinComb", DbType.Int32, entity.FriendWinComb);
			database.AddInParameter(commandWrapper, "@MaxFriendWinComb", DbType.Int32, entity.MaxFriendWinComb);
			database.AddInParameter(commandWrapper, "@MaxDailyCupRank", DbType.Int32, entity.MaxDailyCupRank);
			database.AddInParameter(commandWrapper, "@Level5CardCount", DbType.Int32, entity.Level5CardCount);
			database.AddInParameter(commandWrapper, "@Level10CardCount", DbType.Int32, entity.Level10CardCount);
			database.AddInParameter(commandWrapper, "@Level20CardCount", DbType.Int32, entity.Level20CardCount);
			database.AddInParameter(commandWrapper, "@Level30CardCount", DbType.Int32, entity.Level30CardCount);
			database.AddInParameter(commandWrapper, "@LeagueScore1", DbType.Int32, entity.LeagueScore1);
			database.AddInParameter(commandWrapper, "@LeagueScore2", DbType.Int32, entity.LeagueScore2);
			database.AddInParameter(commandWrapper, "@LeagueScore3", DbType.Int32, entity.LeagueScore3);
			database.AddInParameter(commandWrapper, "@LeagueScore4", DbType.Int32, entity.LeagueScore4);
			database.AddInParameter(commandWrapper, "@LeagueScore5", DbType.Int32, entity.LeagueScore5);
			database.AddInParameter(commandWrapper, "@LeagueScore6", DbType.Int32, entity.LeagueScore6);
			database.AddInParameter(commandWrapper, "@LeagueScore7", DbType.Int32, entity.LeagueScore7);
			database.AddInParameter(commandWrapper, "@LeagueScore8", DbType.Int32, entity.LeagueScore8);

            
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

