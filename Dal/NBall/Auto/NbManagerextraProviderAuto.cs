

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
    
    public partial class NbManagerextraProvider
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
		/// 将IDataReader的当前记录读取到NbManagerextraEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbManagerextraEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbManagerextraEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Stamina = (System.Int32) reader["Stamina"];
            obj.StaminaMax = (System.Int32) reader["StaminaMax"];
            obj.ResumeStaminaTime = (System.DateTime) reader["ResumeStaminaTime"];
            obj.HelpTrainCount = (System.Int32) reader["HelpTrainCount"];
            obj.ByHelpTrainCount = (System.Int32) reader["ByHelpTrainCount"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.Kpi = (System.Int32) reader["Kpi"];
            obj.FunctionList = (System.String) reader["FunctionList"];
            obj.GuideBuffRecord = (System.String) reader["GuideBuffRecord"];
            obj.HasGuidePrize = (System.Boolean) reader["HasGuidePrize"];
            obj.GuidePrizeExpired = (System.DateTime) reader["GuidePrizeExpired"];
            obj.PayFirstFlag = (System.Boolean) reader["PayFirstFlag"];
            obj.PayContinuDate = (System.DateTime) reader["PayContinuDate"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.Vigor = (System.Int32) reader["Vigor"];
            obj.LevelGiftExpired = (System.DateTime) reader["LevelGiftExpired"];
            obj.GuidePrizeCount = (System.Int32) reader["GuidePrizeCount"];
            obj.GuidePrizeLastDate = (System.DateTime) reader["GuidePrizeLastDate"];
            obj.Scouting = (System.Int32) reader["Scouting"];
            obj.ScoutingUpdate = (System.DateTime) reader["ScoutingUpdate"];
            obj.LevelGiftStep = (System.Int32) reader["LevelGiftStep"];
            obj.LevelGiftExpired2 = (System.DateTime) reader["LevelGiftExpired2"];
            obj.LevelGiftExpired3 = (System.DateTime) reader["LevelGiftExpired3"];
            obj.Active = (System.Int32) reader["Active"];
            obj.ScoutingPointFirst = (System.Boolean) reader["ScoutingPointFirst"];
            obj.FriendInviteCount = (System.Int32) reader["FriendInviteCount"];
            obj.VeteranNumber = (System.Int32) reader["VeteranNumber"];
            obj.StaminaGiftStatus = (System.Int32) reader["StaminaGiftStatus"];
            obj.GuideItemCode = (System.Int32) reader["GuideItemCode"];
            obj.IsGuideLottery = (System.Boolean) reader["IsGuideLottery"];
            obj.LeagueScore = (System.Int32) reader["LeagueScore"];
            obj.CoinScouting = (System.Int32) reader["CoinScouting"];
            obj.CoinScoutingUpdate = (System.DateTime) reader["CoinScoutingUpdate"];
            obj.FriendScouting = (System.Int32) reader["FriendScouting"];
            obj.FriendScoutingUpdate = (System.DateTime) reader["FriendScoutingUpdate"];
            obj.OpenBoxCount = (System.Int32) reader["OpenBoxCount"];
            obj.SkillPoint = (System.Int32) reader["SkillPoint"];
            obj.SkillType = (System.Int32) reader["SkillType"];
            obj.ResetPotentialNumber = (System.Int32) reader["ResetPotentialNumber"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<NbManagerextraEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbManagerextraEntity>();
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
        public NbManagerextraProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbManagerextraProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>NbManagerextraEntity</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public NbManagerextraEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagerextra_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            NbManagerextraEntity obj=null;
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
        /// <returns>NbManagerextraEntity列表</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public List<NbManagerextraEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagerextra_GetAll");
            

            
            List<NbManagerextraEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetTODayLoginManager
		
		/// <summary>
        /// GetTODayLoginManager
        /// </summary>
        /// <returns>NbManagerextraEntity列表</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public List<NbManagerextraEntity> GetTODayLoginManager()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_GetTODayLoginManager");
            

            
            List<NbManagerextraEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetInviteFriendCount
		
		/// <summary>
        /// GetInviteFriendCount
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>Int32</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public Int32 GetInviteFriendCount ( System.Guid managerId)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NBManagerExtra_GetInviteFriendCount");
            
			database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  UpdateGuidePrize
		
		/// <summary>
        /// UpdateGuidePrize
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="hasGuidePrize">hasGuidePrize</param>
		/// <param name="guidePrizeExpired">guidePrizeExpired</param>
		/// <param name="guidePrizeCount">guidePrizeCount</param>
		/// <param name="guidePrizeLastDate">guidePrizeLastDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool UpdateGuidePrize ( System.Guid managerId, System.Boolean hasGuidePrize, System.DateTime guidePrizeExpired, System.Int32 guidePrizeCount, System.DateTime guidePrizeLastDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_UpdateGuidePrize");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@HasGuidePrize", DbType.Boolean, hasGuidePrize);
			database.AddInParameter(commandWrapper, "@GuidePrizeExpired", DbType.DateTime, guidePrizeExpired);
			database.AddInParameter(commandWrapper, "@GuidePrizeCount", DbType.Int32, guidePrizeCount);
			database.AddInParameter(commandWrapper, "@GuidePrizeLastDate", DbType.DateTime, guidePrizeLastDate);

            
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
		
		#region  UpdateGuideScouting
		
		/// <summary>
        /// UpdateGuideScouting
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool UpdateGuideScouting ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_UpdateGuideScouting");
            
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
		
		#region  UpdateScoutingPointFirst
		
		/// <summary>
        /// UpdateScoutingPointFirst
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool UpdateScoutingPointFirst ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_UpdateScoutingPointFirst");
            
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
		
		#region  UpdateKpi
		
		/// <summary>
        /// UpdateKpi
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="kpi">kpi</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool UpdateKpi ( System.Guid managerId, System.Int32 kpi,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_UpdateKpi");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, kpi);

            
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
		
		#region  SaveHelpCount
		
		/// <summary>
        /// SaveHelpCount
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="helpCount">helpCount</param>
		/// <param name="byHelpCount">byHelpCount</param>
		/// <param name="recordDate">recordDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool SaveHelpCount ( System.Guid managerId, System.Int32 helpCount, System.Int32 byHelpCount, System.DateTime recordDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_SaveHelpCount");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@HelpCount", DbType.Int32, helpCount);
			database.AddInParameter(commandWrapper, "@ByHelpCount", DbType.Int32, byHelpCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);

            
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
		
		#region  SaveByHelpCount
		
		/// <summary>
        /// SaveByHelpCount
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="byHelpCount">byHelpCount</param>
		/// <param name="recordDate">recordDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool SaveByHelpCount ( System.Guid managerId, System.Int32 byHelpCount, System.DateTime recordDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_SaveByHelpCount");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ByHelpCount", DbType.Int32, byHelpCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);

            
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
		
		#region  UpdatePayFirst
		
		/// <summary>
        /// UpdatePayFirst
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="payFirstFlag">payFirstFlag</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool UpdatePayFirst ( System.Guid managerId, System.Boolean payFirstFlag,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_UpdatePayFirst");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@PayFirstFlag", DbType.Boolean, payFirstFlag);

            
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
		
		#region  UpdateLevelGift
		
		/// <summary>
        /// UpdateLevelGift
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="expired">expired</param>
		/// <param name="step">step</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool UpdateLevelGift ( System.Guid managerId, System.DateTime expired, System.Int32 step,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_UpdateLevelGift");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Expired", DbType.DateTime, expired);
			database.AddInParameter(commandWrapper, "@Step", DbType.Int32, step);

            
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
		
		#region  UpdateScouting
		
		/// <summary>
        /// UpdateScouting
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="date">date</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool UpdateScouting ( System.Guid managerId, System.DateTime date,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_UpdateScouting");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Date", DbType.DateTime, date);

            
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
		
		#region  UpdateCoinScouting
		
		/// <summary>
        /// UpdateCoinScouting
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="date">date</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool UpdateCoinScouting ( System.Guid managerId, System.DateTime date,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_UpdateCoinScouting");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Date", DbType.DateTime, date);

            
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
		
		#region  UpdateFriendScouting
		
		/// <summary>
        /// UpdateFriendScouting
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="date">date</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool UpdateFriendScouting ( System.Guid managerId, System.DateTime date,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_UpdateFriendScouting");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Date", DbType.DateTime, date);

            
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
		
		#region  AddActive
		
		/// <summary>
        /// AddActive
        /// </summary>
		/// <param name="managerID">managerID</param>
		/// <param name="number">number</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool AddActive ( System.Guid managerID, System.Int32 number,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_AddActive");
            
			database.AddInParameter(commandWrapper, "@ManagerID", DbType.Guid, managerID);
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
		
		#region  AddLeagueScore
		
		/// <summary>
        /// AddLeagueScore
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="score">score</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool AddLeagueScore ( System.Guid managerId, System.Int32 score,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NBManagerExtra_AddLeagueScore");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@score", DbType.Int32, score);

            
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
		
		#region  AddSkillPoint
		
		/// <summary>
        /// AddSkillPoint
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="skillPoint">skillPoint</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool AddSkillPoint ( System.Guid managerId, System.Int32 skillPoint,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NBManagerExtra_AddSkillPoint");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SkillPoint", DbType.Int32, skillPoint);

            
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
		
		#region  ToDeductSkillPoint
		
		/// <summary>
        /// ToDeductSkillPoint
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="skillPoint">skillPoint</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool ToDeductSkillPoint ( System.Guid managerId, System.Int32 skillPoint,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ManagerExtra_ToDeductSkillPoint");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SkillPoint", DbType.Int32, skillPoint);

            
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
		
		#region  SetSkillType
		
		/// <summary>
        /// SetSkillType
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="skillType">skillType</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool SetSkillType ( System.Guid managerId, System.Int32 skillType,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NBManagerExtra_SetSkillType");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SkillType", DbType.Int32, skillType);

            
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
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool Insert(NbManagerextraEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagerextra_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Stamina", DbType.Int32, entity.Stamina);
			database.AddInParameter(commandWrapper, "@StaminaMax", DbType.Int32, entity.StaminaMax);
			database.AddInParameter(commandWrapper, "@ResumeStaminaTime", DbType.DateTime, entity.ResumeStaminaTime);
			database.AddInParameter(commandWrapper, "@HelpTrainCount", DbType.Int32, entity.HelpTrainCount);
			database.AddInParameter(commandWrapper, "@ByHelpTrainCount", DbType.Int32, entity.ByHelpTrainCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, entity.Kpi);
			database.AddInParameter(commandWrapper, "@FunctionList", DbType.AnsiString, entity.FunctionList);
			database.AddInParameter(commandWrapper, "@GuideBuffRecord", DbType.AnsiString, entity.GuideBuffRecord);
			database.AddInParameter(commandWrapper, "@HasGuidePrize", DbType.Boolean, entity.HasGuidePrize);
			database.AddInParameter(commandWrapper, "@GuidePrizeExpired", DbType.DateTime, entity.GuidePrizeExpired);
			database.AddInParameter(commandWrapper, "@PayFirstFlag", DbType.Boolean, entity.PayFirstFlag);
			database.AddInParameter(commandWrapper, "@PayContinuDate", DbType.DateTime, entity.PayContinuDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@Vigor", DbType.Int32, entity.Vigor);
			database.AddInParameter(commandWrapper, "@LevelGiftExpired", DbType.DateTime, entity.LevelGiftExpired);
			database.AddInParameter(commandWrapper, "@GuidePrizeCount", DbType.Int32, entity.GuidePrizeCount);
			database.AddInParameter(commandWrapper, "@GuidePrizeLastDate", DbType.DateTime, entity.GuidePrizeLastDate);
			database.AddInParameter(commandWrapper, "@Scouting", DbType.Int32, entity.Scouting);
			database.AddInParameter(commandWrapper, "@ScoutingUpdate", DbType.DateTime, entity.ScoutingUpdate);
			database.AddInParameter(commandWrapper, "@LevelGiftStep", DbType.Int32, entity.LevelGiftStep);
			database.AddInParameter(commandWrapper, "@LevelGiftExpired2", DbType.DateTime, entity.LevelGiftExpired2);
			database.AddInParameter(commandWrapper, "@LevelGiftExpired3", DbType.DateTime, entity.LevelGiftExpired3);
			database.AddInParameter(commandWrapper, "@Active", DbType.Int32, entity.Active);
			database.AddInParameter(commandWrapper, "@ScoutingPointFirst", DbType.Boolean, entity.ScoutingPointFirst);
			database.AddInParameter(commandWrapper, "@FriendInviteCount", DbType.Int32, entity.FriendInviteCount);
			database.AddInParameter(commandWrapper, "@VeteranNumber", DbType.Int32, entity.VeteranNumber);
			database.AddInParameter(commandWrapper, "@StaminaGiftStatus", DbType.Int32, entity.StaminaGiftStatus);
			database.AddInParameter(commandWrapper, "@GuideItemCode", DbType.Int32, entity.GuideItemCode);
			database.AddInParameter(commandWrapper, "@IsGuideLottery", DbType.Boolean, entity.IsGuideLottery);
			database.AddInParameter(commandWrapper, "@LeagueScore", DbType.Int32, entity.LeagueScore);
			database.AddInParameter(commandWrapper, "@CoinScouting", DbType.Int32, entity.CoinScouting);
			database.AddInParameter(commandWrapper, "@CoinScoutingUpdate", DbType.DateTime, entity.CoinScoutingUpdate);
			database.AddInParameter(commandWrapper, "@FriendScouting", DbType.Int32, entity.FriendScouting);
			database.AddInParameter(commandWrapper, "@FriendScoutingUpdate", DbType.DateTime, entity.FriendScoutingUpdate);
			database.AddInParameter(commandWrapper, "@OpenBoxCount", DbType.Int32, entity.OpenBoxCount);
			database.AddInParameter(commandWrapper, "@SkillPoint", DbType.Int32, entity.SkillPoint);
			database.AddInParameter(commandWrapper, "@SkillType", DbType.Int32, entity.SkillType);
			database.AddInParameter(commandWrapper, "@ResetPotentialNumber", DbType.Int32, entity.ResetPotentialNumber);
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
        /// <remarks>2016/5/26 20:52:55</remarks>
        public bool Update(NbManagerextraEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagerextra_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Stamina", DbType.Int32, entity.Stamina);
			database.AddInParameter(commandWrapper, "@StaminaMax", DbType.Int32, entity.StaminaMax);
			database.AddInParameter(commandWrapper, "@ResumeStaminaTime", DbType.DateTime, entity.ResumeStaminaTime);
			database.AddInParameter(commandWrapper, "@HelpTrainCount", DbType.Int32, entity.HelpTrainCount);
			database.AddInParameter(commandWrapper, "@ByHelpTrainCount", DbType.Int32, entity.ByHelpTrainCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, entity.Kpi);
			database.AddInParameter(commandWrapper, "@FunctionList", DbType.AnsiString, entity.FunctionList);
			database.AddInParameter(commandWrapper, "@GuideBuffRecord", DbType.AnsiString, entity.GuideBuffRecord);
			database.AddInParameter(commandWrapper, "@HasGuidePrize", DbType.Boolean, entity.HasGuidePrize);
			database.AddInParameter(commandWrapper, "@GuidePrizeExpired", DbType.DateTime, entity.GuidePrizeExpired);
			database.AddInParameter(commandWrapper, "@PayFirstFlag", DbType.Boolean, entity.PayFirstFlag);
			database.AddInParameter(commandWrapper, "@PayContinuDate", DbType.DateTime, entity.PayContinuDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@Vigor", DbType.Int32, entity.Vigor);
			database.AddInParameter(commandWrapper, "@LevelGiftExpired", DbType.DateTime, entity.LevelGiftExpired);
			database.AddInParameter(commandWrapper, "@GuidePrizeCount", DbType.Int32, entity.GuidePrizeCount);
			database.AddInParameter(commandWrapper, "@GuidePrizeLastDate", DbType.DateTime, entity.GuidePrizeLastDate);
			database.AddInParameter(commandWrapper, "@Scouting", DbType.Int32, entity.Scouting);
			database.AddInParameter(commandWrapper, "@ScoutingUpdate", DbType.DateTime, entity.ScoutingUpdate);
			database.AddInParameter(commandWrapper, "@LevelGiftStep", DbType.Int32, entity.LevelGiftStep);
			database.AddInParameter(commandWrapper, "@LevelGiftExpired2", DbType.DateTime, entity.LevelGiftExpired2);
			database.AddInParameter(commandWrapper, "@LevelGiftExpired3", DbType.DateTime, entity.LevelGiftExpired3);
			database.AddInParameter(commandWrapper, "@Active", DbType.Int32, entity.Active);
			database.AddInParameter(commandWrapper, "@ScoutingPointFirst", DbType.Boolean, entity.ScoutingPointFirst);
			database.AddInParameter(commandWrapper, "@FriendInviteCount", DbType.Int32, entity.FriendInviteCount);
			database.AddInParameter(commandWrapper, "@VeteranNumber", DbType.Int32, entity.VeteranNumber);
			database.AddInParameter(commandWrapper, "@StaminaGiftStatus", DbType.Int32, entity.StaminaGiftStatus);
			database.AddInParameter(commandWrapper, "@GuideItemCode", DbType.Int32, entity.GuideItemCode);
			database.AddInParameter(commandWrapper, "@IsGuideLottery", DbType.Boolean, entity.IsGuideLottery);
			database.AddInParameter(commandWrapper, "@LeagueScore", DbType.Int32, entity.LeagueScore);
			database.AddInParameter(commandWrapper, "@CoinScouting", DbType.Int32, entity.CoinScouting);
			database.AddInParameter(commandWrapper, "@CoinScoutingUpdate", DbType.DateTime, entity.CoinScoutingUpdate);
			database.AddInParameter(commandWrapper, "@FriendScouting", DbType.Int32, entity.FriendScouting);
			database.AddInParameter(commandWrapper, "@FriendScoutingUpdate", DbType.DateTime, entity.FriendScoutingUpdate);
			database.AddInParameter(commandWrapper, "@OpenBoxCount", DbType.Int32, entity.OpenBoxCount);
			database.AddInParameter(commandWrapper, "@SkillPoint", DbType.Int32, entity.SkillPoint);
			database.AddInParameter(commandWrapper, "@SkillType", DbType.Int32, entity.SkillType);
			database.AddInParameter(commandWrapper, "@ResetPotentialNumber", DbType.Int32, entity.ResetPotentialNumber);

            
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
