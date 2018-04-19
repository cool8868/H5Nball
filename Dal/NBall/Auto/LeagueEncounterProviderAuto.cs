

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
    
    public partial class LeagueEncounterProvider
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
		/// 将IDataReader的当前记录读取到LeagueEncounterEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LeagueEncounterEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LeagueEncounterEntity();
			
            obj.MatchId = (System.Guid) reader["MatchId"];
            obj.LeagueRecordId = (System.Guid) reader["LeagueRecordId"];
            obj.HomeName = (System.String) reader["HomeName"];
            obj.AwayName = (System.String) reader["AwayName"];
            obj.WheelNumber = (System.Int32) reader["WheelNumber"];
            obj.HomeId = (System.Guid) reader["HomeId"];
            obj.AwayId = (System.Guid) reader["AwayId"];
            obj.HomeIsNpc = (System.Boolean) reader["HomeIsNpc"];
            obj.AwayIsNpc = (System.Boolean) reader["AwayIsNpc"];
            obj.IsMatch = (System.Boolean) reader["IsMatch"];
            obj.ReMatched = (System.Boolean) reader["ReMatched"];
            obj.Confirmed = (System.Boolean) reader["Confirmed"];
            obj.HomeGoals = (System.Int32) reader["HomeGoals"];
            obj.AwayGoals = (System.Int32) reader["AwayGoals"];
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
        public List<LeagueEncounterEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LeagueEncounterEntity>();
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
        public LeagueEncounterProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LeagueEncounterProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="matchId">matchId</param>
        /// <returns>LeagueEncounterEntity</returns>
        /// <remarks>2016-06-03 19:54:14</remarks>
        public LeagueEncounterEntity GetById( System.Guid matchId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueEncounter_GetById");
            
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, matchId);

            
            LeagueEncounterEntity obj=null;
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
        /// <returns>LeagueEncounterEntity列表</returns>
        /// <remarks>2016-06-03 19:54:14</remarks>
        public List<LeagueEncounterEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueEncounter_GetAll");
            

            
            List<LeagueEncounterEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetLeaguePair
		
		/// <summary>
        /// GetLeaguePair
        /// </summary>
        /// <returns>LeagueEncounterEntity列表</returns>
        /// <remarks>2016-06-03 19:54:14</remarks>
        public List<LeagueEncounterEntity> GetLeaguePair( System.Guid managerId, System.Guid leagueRecordId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_LeagueEncounter_GetLeaguePair");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, leagueRecordId);

            
            List<LeagueEncounterEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetWheelMatchs
		
		/// <summary>
        /// GetWheelMatchs
        /// </summary>
        /// <returns>LeagueEncounterEntity列表</returns>
        /// <remarks>2016-06-03 19:54:14</remarks>
        public List<LeagueEncounterEntity> GetWheelMatchs( System.Guid leagueRecordId, System.Int32 wheel)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_LeagueEncounter_GetWheelMatchs");
            
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, leagueRecordId);
			database.AddInParameter(commandWrapper, "@Wheel", DbType.Int32, wheel);

            
            List<LeagueEncounterEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetMatchsByHomeAwayIds
		
		/// <summary>
        /// GetMatchsByHomeAwayIds
        /// </summary>
        /// <returns>LeagueEncounterEntity列表</returns>
        /// <remarks>2016-06-03 19:54:14</remarks>
        public List<LeagueEncounterEntity> GetMatchsByHomeAwayIds( System.Guid leagueRecordId, System.Guid managerId, System.Guid npcId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_LeagueEncounter_GetMatchsByHomeAwayIds");
            
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, leagueRecordId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, npcId);

            
            List<LeagueEncounterEntity> list = null;
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
		/// <param name="matchId">matchId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-06-03 19:54:14</remarks>
        public bool Delete ( System.Guid matchId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueEncounter_Delete");
            
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, matchId);

            
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
		
		#region  GenerateFightdic
		
		/// <summary>
        /// GenerateFightdic
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="leagueId">leagueId</param>
		/// <param name="leagueRecordId">leagueRecordId</param>
		/// <param name="templateId">templateId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-06-03 19:54:14</remarks>
        public bool GenerateFightdic ( System.Guid managerId, System.Int32 leagueId, System.Guid leagueRecordId, System.Int32 templateId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_League_GenerateFightdic");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@LeagueId", DbType.Int32, leagueId);
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, leagueRecordId);
			database.AddInParameter(commandWrapper, "@TemplateId", DbType.Int32, templateId);

            
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
        /// <remarks>2016-06-03 19:54:14</remarks>
        public bool Insert(LeagueEncounterEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueEncounter_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, entity.LeagueRecordId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.AnsiString, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.AnsiString, entity.AwayName);
			database.AddInParameter(commandWrapper, "@WheelNumber", DbType.Int32, entity.WheelNumber);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeIsNpc", DbType.Boolean, entity.HomeIsNpc);
			database.AddInParameter(commandWrapper, "@AwayIsNpc", DbType.Boolean, entity.AwayIsNpc);
			database.AddInParameter(commandWrapper, "@IsMatch", DbType.Boolean, entity.IsMatch);
			database.AddInParameter(commandWrapper, "@ReMatched", DbType.Boolean, entity.ReMatched);
			database.AddInParameter(commandWrapper, "@Confirmed", DbType.Boolean, entity.Confirmed);
			database.AddInParameter(commandWrapper, "@HomeGoals", DbType.Int32, entity.HomeGoals);
			database.AddInParameter(commandWrapper, "@AwayGoals", DbType.Int32, entity.AwayGoals);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@MatchId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.MatchId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.MatchId=(System.Guid)database.GetParameterValue(commandWrapper, "@MatchId");
            
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
        /// <remarks>2016-06-03 19:54:14</remarks>
        public bool Update(LeagueEncounterEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueEncounter_Update");
            
			database.AddInParameter(commandWrapper, "@MatchId", DbType.Guid, entity.MatchId);
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, entity.LeagueRecordId);
			database.AddInParameter(commandWrapper, "@HomeName", DbType.AnsiString, entity.HomeName);
			database.AddInParameter(commandWrapper, "@AwayName", DbType.AnsiString, entity.AwayName);
			database.AddInParameter(commandWrapper, "@WheelNumber", DbType.Int32, entity.WheelNumber);
			database.AddInParameter(commandWrapper, "@HomeId", DbType.Guid, entity.HomeId);
			database.AddInParameter(commandWrapper, "@AwayId", DbType.Guid, entity.AwayId);
			database.AddInParameter(commandWrapper, "@HomeIsNpc", DbType.Boolean, entity.HomeIsNpc);
			database.AddInParameter(commandWrapper, "@AwayIsNpc", DbType.Boolean, entity.AwayIsNpc);
			database.AddInParameter(commandWrapper, "@IsMatch", DbType.Boolean, entity.IsMatch);
			database.AddInParameter(commandWrapper, "@ReMatched", DbType.Boolean, entity.ReMatched);
			database.AddInParameter(commandWrapper, "@Confirmed", DbType.Boolean, entity.Confirmed);
			database.AddInParameter(commandWrapper, "@HomeGoals", DbType.Int32, entity.HomeGoals);
			database.AddInParameter(commandWrapper, "@AwayGoals", DbType.Int32, entity.AwayGoals);
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

            entity.MatchId=(System.Guid)database.GetParameterValue(commandWrapper, "@MatchId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
