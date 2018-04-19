

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
    
    public partial class GuildListProvider
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
		/// 将IDataReader的当前记录读取到GuildListEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public GuildListEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new GuildListEntity();
			
            obj.GuildNo = (System.Int32) reader["GuildNo"];
            obj.GuildId = (System.Guid) reader["GuildId"];
            obj.GuildName = (System.String) reader["GuildName"];
            obj.Icon = (System.String) reader["Icon"];
            obj.Logo = (System.String) reader["Logo"];
            obj.Intro = (System.String) reader["Intro"];
            obj.Note = (System.String) reader["Note"];
            obj.GuildLevel = (System.Int32) reader["GuildLevel"];
            obj.GuildActive = (System.Int32) reader["GuildActive"];
            obj.GuildActiveCost = (System.Int32) reader["GuildActiveCost"];
            obj.CntMembers = (System.Int32) reader["CntMembers"];
            obj.MaxMembers = (System.Int32) reader["MaxMembers"];
            obj.CreatorId = (System.Guid) reader["CreatorId"];
            obj.CreatorName = (System.String) reader["CreatorName"];
            obj.CreateTime = (System.DateTime) reader["CreateTime"];
            obj.LeaderId = (System.Guid) reader["LeaderId"];
            obj.LeaderName = (System.String) reader["LeaderName"];
            obj.LeadTime = (System.DateTime) reader["LeadTime"];
            obj.LeadTrack = (System.Int32) reader["LeadTrack"];
            obj.GKpi = (System.Int32) reader["GKpi"];
            obj.GRank = (System.Int32) reader["GRank"];
            obj.LockFlag = (System.Int32) reader["LockFlag"];
            obj.LockTime = (System.DateTime) reader["LockTime"];
            obj.InvalidFlag = (System.Int32) reader["InvalidFlag"];
            obj.RowTimeUp = (System.DateTime) reader["RowTimeUp"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<GuildListEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<GuildListEntity>();
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
        public GuildListProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public GuildListProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="guildNo">guildNo</param>
        /// <returns>GuildListEntity</returns>
        /// <remarks>2016/6/7 16:20:07</remarks>
        public GuildListEntity GetById( System.Int32 guildNo)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GuildList_GetById");
            
			database.AddInParameter(commandWrapper, "@GuildNo", DbType.Int32, guildNo);

            
            GuildListEntity obj=null;
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
		
		#region  GetGuild
		
		/// <summary>
        /// GetGuild
        /// </summary>
		/// <param name="guildId">guildId</param>
        /// <returns>GuildListEntity</returns>
        /// <remarks>2016/6/7 16:20:07</remarks>
        public GuildListEntity GetGuild( System.Guid guildId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_GetGuild");
            
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);

            
            GuildListEntity obj=null;
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
		
		#region  GetGuildByName
		
		/// <summary>
        /// GetGuildByName
        /// </summary>
		/// <param name="guildName">guildName</param>
        /// <returns>GuildListEntity</returns>
        /// <remarks>2016/6/7 16:20:07</remarks>
        public GuildListEntity GetGuildByName( System.String guildName)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_GetGuildByName");
            
			database.AddInParameter(commandWrapper, "@GuildName", DbType.String, guildName);

            
            GuildListEntity obj=null;
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
		
		#region  GetGuildByManager
		
		/// <summary>
        /// GetGuildByManager
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>GuildListEntity</returns>
        /// <remarks>2016/6/7 16:20:07</remarks>
        public GuildListEntity GetGuildByManager( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_GetGuildByManager");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            GuildListEntity obj=null;
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
        /// <returns>GuildListEntity列表</returns>
        /// <remarks>2016/6/7 16:20:07</remarks>
        public List<GuildListEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GuildList_GetAll");
            

            
            List<GuildListEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  PageByRowNumber
		
		/// <summary>
        /// PageByRowNumber
        /// </summary>
        /// <returns>GuildListEntity列表</returns>
        /// <remarks>2016/6/7 16:20:07</remarks>
        public List<GuildListEntity> PageByRowNumber( System.String tableName, System.String selectFields, System.String whereStr, System.String orderFields, System.Int32 pageSize, System.Int32 pageNo, System.Boolean ifCount,ref  System.Int32 pageCount,ref  System.Int32 rowCount)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_PageByRowNumber");
            
			database.AddInParameter(commandWrapper, "@TableName", DbType.AnsiString, tableName);
			database.AddInParameter(commandWrapper, "@SelectFields", DbType.AnsiString, selectFields);
			database.AddInParameter(commandWrapper, "@WhereStr", DbType.AnsiString, whereStr);
			database.AddInParameter(commandWrapper, "@OrderFields", DbType.AnsiString, orderFields);
			database.AddInParameter(commandWrapper, "@PageSize", DbType.Int32, pageSize);
			database.AddInParameter(commandWrapper, "@PageNo", DbType.Int32, pageNo);
			database.AddInParameter(commandWrapper, "@IfCount", DbType.Boolean, ifCount);
			database.AddParameter(commandWrapper, "@PageCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,pageCount);
			database.AddParameter(commandWrapper, "@RowCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,rowCount);

            
            List<GuildListEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                pageCount=(System.Int32)database.GetParameterValue(commandWrapper, "@PageCount");
                rowCount=(System.Int32)database.GetParameterValue(commandWrapper, "@RowCount");
                
            return list;
        }
		
		#endregion		  
		
		#region  Create
		
		/// <summary>
        /// Create
        /// </summary>
		/// <param name="tranFlag">tranFlag</param>
		/// <param name="managerId">managerId</param>
		/// <param name="account">account</param>
		/// <param name="costGold">costGold</param>
		/// <param name="costGoldItemNo">costGoldItemNo</param>
		/// <param name="costGoldOrderId">costGoldOrderId</param>
		/// <param name="costCoin">costCoin</param>
		/// <param name="costRowVersion">costRowVersion</param>
		/// <param name="guildId">guildId</param>
		/// <param name="guildName">guildName</param>
		/// <param name="icon">icon</param>
		/// <param name="logo">logo</param>
		/// <param name="intro">intro</param>
		/// <param name="note">note</param>
		/// <param name="guildLevel">guildLevel</param>
		/// <param name="maxMembers">maxMembers</param>
		/// <param name="leadTrack">leadTrack</param>
		/// <param name="managerName">managerName</param>
		/// <param name="authRank">authRank</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:07</remarks>
        public bool Create ( System.Boolean tranFlag, System.Guid managerId, System.String account, System.Int32 costGold, System.Int32 costGoldItemNo, System.String costGoldOrderId, System.Int32 costCoin, System.Byte[] costRowVersion, System.Guid guildId, System.String guildName, System.String icon, System.String logo, System.String intro, System.String note, System.Int32 guildLevel, System.Int32 maxMembers, System.Int32 leadTrack, System.String managerName, System.Int32 authRank,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Create");
            
			database.AddInParameter(commandWrapper, "@TranFlag", DbType.Boolean, tranFlag);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@CostGold", DbType.Int32, costGold);
			database.AddInParameter(commandWrapper, "@CostGoldItemNo", DbType.Int32, costGoldItemNo);
			database.AddInParameter(commandWrapper, "@CostGoldOrderId", DbType.AnsiString, costGoldOrderId);
			database.AddInParameter(commandWrapper, "@CostCoin", DbType.Int32, costCoin);
			database.AddInParameter(commandWrapper, "@CostRowVersion", DbType.Binary, costRowVersion);
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@GuildName", DbType.String, guildName);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, icon);
			database.AddInParameter(commandWrapper, "@Logo", DbType.String, logo);
			database.AddInParameter(commandWrapper, "@Intro", DbType.String, intro);
			database.AddInParameter(commandWrapper, "@Note", DbType.String, note);
			database.AddInParameter(commandWrapper, "@GuildLevel", DbType.Int32, guildLevel);
			database.AddInParameter(commandWrapper, "@MaxMembers", DbType.Int32, maxMembers);
			database.AddInParameter(commandWrapper, "@LeadTrack", DbType.Int32, leadTrack);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, managerName);
			database.AddInParameter(commandWrapper, "@AuthRank", DbType.Int32, authRank);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Join
		
		/// <summary>
        /// Join
        /// </summary>
		/// <param name="tranFlag">tranFlag</param>
		/// <param name="guildId">guildId</param>
		/// <param name="maxMembers">maxMembers</param>
		/// <param name="managerId">managerId</param>
		/// <param name="managerName">managerName</param>
		/// <param name="authRank">authRank</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:07</remarks>
        public bool Join ( System.Boolean tranFlag, System.Guid guildId, System.Int32 maxMembers, System.Guid managerId, System.String managerName, System.Int32 authRank,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Join");
            
			database.AddInParameter(commandWrapper, "@TranFlag", DbType.Boolean, tranFlag);
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@MaxMembers", DbType.Int32, maxMembers);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, managerName);
			database.AddInParameter(commandWrapper, "@AuthRank", DbType.Int32, authRank);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Grant
		
		/// <summary>
        /// Grant
        /// </summary>
		/// <param name="guildId">guildId</param>
		/// <param name="leaderId">leaderId</param>
		/// <param name="newLeaderRank">newLeaderRank</param>
		/// <param name="memberId">memberId</param>
		/// <param name="memberName">memberName</param>
		/// <param name="newMemberRank">newMemberRank</param>
		/// <param name="leadTrack">leadTrack</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Grant ( System.Guid guildId, System.Guid leaderId, System.Int32 newLeaderRank, System.Guid memberId, System.String memberName, System.Int32 newMemberRank, System.Int32 leadTrack,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Grant");
            
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@LeaderId", DbType.Guid, leaderId);
			database.AddInParameter(commandWrapper, "@NewLeaderRank", DbType.Int32, newLeaderRank);
			database.AddInParameter(commandWrapper, "@MemberId", DbType.Guid, memberId);
			database.AddInParameter(commandWrapper, "@MemberName", DbType.String, memberName);
			database.AddInParameter(commandWrapper, "@NewMemberRank", DbType.Int32, newMemberRank);
			database.AddInParameter(commandWrapper, "@LeadTrack", DbType.Int32, leadTrack);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Exit
		
		/// <summary>
        /// Exit
        /// </summary>
		/// <param name="tranFlag">tranFlag</param>
		/// <param name="guildId">guildId</param>
		/// <param name="managerId">managerId</param>
		/// <param name="leaderId">leaderId</param>
		/// <param name="authRank">authRank</param>
		/// <param name="exitTrack">exitTrack</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Exit ( System.Boolean tranFlag, System.Guid guildId, System.Guid managerId, System.Guid leaderId, System.Int32 authRank, System.Int32 exitTrack,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Exit");
            
			database.AddInParameter(commandWrapper, "@TranFlag", DbType.Boolean, tranFlag);
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@LeaderId", DbType.Guid, leaderId);
			database.AddInParameter(commandWrapper, "@AuthRank", DbType.Int32, authRank);
			database.AddInParameter(commandWrapper, "@ExitTrack", DbType.Int32, exitTrack);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Drop
		
		/// <summary>
        /// Drop
        /// </summary>
		/// <param name="tranFlag">tranFlag</param>
		/// <param name="guildId">guildId</param>
		/// <param name="managerId">managerId</param>
		/// <param name="authRank">authRank</param>
		/// <param name="exitTrack">exitTrack</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Drop ( System.Boolean tranFlag, System.Guid guildId, System.Guid managerId, System.Int32 authRank, System.Int32 exitTrack,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Drop");
            
			database.AddInParameter(commandWrapper, "@TranFlag", DbType.Boolean, tranFlag);
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@AuthRank", DbType.Int32, authRank);
			database.AddInParameter(commandWrapper, "@ExitTrack", DbType.Int32, exitTrack);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Option
		
		/// <summary>
        /// Option
        /// </summary>
		/// <param name="guildId">guildId</param>
		/// <param name="opType">opType</param>
		/// <param name="opData">opData</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Option ( System.Guid guildId, System.Int32 opType, System.String opData,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Option");
            
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@OpType", DbType.Int32, opType);
			database.AddInParameter(commandWrapper, "@OpData", DbType.String, opData);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Request
		
		/// <summary>
        /// Request
        /// </summary>
		/// <param name="guildId">guildId</param>
		/// <param name="dstType">dstType</param>
		/// <param name="dstId">dstId</param>
		/// <param name="dstName">dstName</param>
		/// <param name="srcType">srcType</param>
		/// <param name="srcId">srcId</param>
		/// <param name="srcName">srcName</param>
		/// <param name="msgNo">msgNo</param>
		/// <param name="msgType">msgType</param>
		/// <param name="msgState">msgState</param>
		/// <param name="body">body</param>
		/// <param name="link">link</param>
		/// <param name="reqNo">reqNo</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Request ( System.Guid guildId, System.Int32 dstType, System.Guid dstId, System.String dstName, System.Int32 srcType, System.Guid srcId, System.String srcName, System.Int32 msgNo, System.Int32 msgType, System.Int32 msgState, System.String body, System.String link,ref  System.Int64 reqNo,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Request");
            
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@DstType", DbType.Int32, dstType);
			database.AddInParameter(commandWrapper, "@DstId", DbType.Guid, dstId);
			database.AddInParameter(commandWrapper, "@DstName", DbType.String, dstName);
			database.AddInParameter(commandWrapper, "@SrcType", DbType.Int32, srcType);
			database.AddInParameter(commandWrapper, "@SrcId", DbType.Guid, srcId);
			database.AddInParameter(commandWrapper, "@SrcName", DbType.String, srcName);
			database.AddInParameter(commandWrapper, "@MsgNo", DbType.Int32, msgNo);
			database.AddInParameter(commandWrapper, "@MsgType", DbType.Int32, msgType);
			database.AddInParameter(commandWrapper, "@MsgState", DbType.Int32, msgState);
			database.AddInParameter(commandWrapper, "@Body", DbType.String, body);
			database.AddInParameter(commandWrapper, "@Link", DbType.String, link);
			database.AddParameter(commandWrapper, "@ReqNo", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,reqNo);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            reqNo=(System.Int64)database.GetParameterValue(commandWrapper, "@ReqNo");
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Reply
		
		/// <summary>
        /// Reply
        /// </summary>
		/// <param name="reqNo">reqNo</param>
		/// <param name="dstId">dstId</param>
		/// <param name="srcId">srcId</param>
		/// <param name="msgType">msgType</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Reply ( System.Int64 reqNo, System.Guid dstId, System.Guid srcId, System.Int32 msgType,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Reply");
            
			database.AddInParameter(commandWrapper, "@ReqNo", DbType.Int64, reqNo);
			database.AddInParameter(commandWrapper, "@DstId", DbType.Guid, dstId);
			database.AddInParameter(commandWrapper, "@SrcId", DbType.Guid, srcId);
			database.AddInParameter(commandWrapper, "@MsgType", DbType.Int32, msgType);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Log
		
		/// <summary>
        /// Log
        /// </summary>
		/// <param name="msgSize">msgSize</param>
		/// <param name="dstType">dstType</param>
		/// <param name="dstId">dstId</param>
		/// <param name="dstName">dstName</param>
		/// <param name="srcType">srcType</param>
		/// <param name="srcId">srcId</param>
		/// <param name="srcName">srcName</param>
		/// <param name="msgNo">msgNo</param>
		/// <param name="msgType">msgType</param>
		/// <param name="msgState">msgState</param>
		/// <param name="body">body</param>
		/// <param name="link">link</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Log ( System.Int32 msgSize, System.Int32 dstType, System.Guid dstId, System.String dstName, System.Int32 srcType, System.Guid srcId, System.String srcName, System.Int32 msgNo, System.Int32 msgType, System.Int32 msgState, System.String body, System.String link,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Log");
            
			database.AddInParameter(commandWrapper, "@MsgSize", DbType.Int32, msgSize);
			database.AddInParameter(commandWrapper, "@DstType", DbType.Int32, dstType);
			database.AddInParameter(commandWrapper, "@DstId", DbType.Guid, dstId);
			database.AddInParameter(commandWrapper, "@DstName", DbType.String, dstName);
			database.AddInParameter(commandWrapper, "@SrcType", DbType.Int32, srcType);
			database.AddInParameter(commandWrapper, "@SrcId", DbType.Guid, srcId);
			database.AddInParameter(commandWrapper, "@SrcName", DbType.String, srcName);
			database.AddInParameter(commandWrapper, "@MsgNo", DbType.Int32, msgNo);
			database.AddInParameter(commandWrapper, "@MsgType", DbType.Int32, msgType);
			database.AddInParameter(commandWrapper, "@MsgState", DbType.Int32, msgState);
			database.AddInParameter(commandWrapper, "@Body", DbType.String, body);
			database.AddInParameter(commandWrapper, "@Link", DbType.String, link);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Act
		
		/// <summary>
        /// Act
        /// </summary>
		/// <param name="tranFlag">tranFlag</param>
		/// <param name="managerId">managerId</param>
		/// <param name="account">account</param>
		/// <param name="costGold">costGold</param>
		/// <param name="costGoldItemNo">costGoldItemNo</param>
		/// <param name="costGoldOrderId">costGoldOrderId</param>
		/// <param name="costCoin">costCoin</param>
		/// <param name="costRowVersion">costRowVersion</param>
		/// <param name="costActive">costActive</param>
		/// <param name="limitActTimes">limitActTimes</param>
		/// <param name="guildId">guildId</param>
		/// <param name="actType">actType</param>
		/// <param name="actKey">actKey</param>
		/// <param name="costType">costType</param>
		/// <param name="costValue">costValue</param>
		/// <param name="gainType">gainType</param>
		/// <param name="gainMap">gainMap</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Act ( System.Boolean tranFlag, System.Guid managerId, System.String account, System.Int32 costGold, System.Int32 costGoldItemNo, System.String costGoldOrderId, System.Int32 costCoin, System.Byte[] costRowVersion, System.Int32 costActive, System.Int32 limitActTimes, System.Guid guildId, System.Int32 actType, System.String actKey, System.Int32 costType, System.Int32 costValue, System.Int32 gainType, System.String gainMap,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_Act");
            
			database.AddInParameter(commandWrapper, "@TranFlag", DbType.Boolean, tranFlag);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@CostGold", DbType.Int32, costGold);
			database.AddInParameter(commandWrapper, "@CostGoldItemNo", DbType.Int32, costGoldItemNo);
			database.AddInParameter(commandWrapper, "@CostGoldOrderId", DbType.AnsiString, costGoldOrderId);
			database.AddInParameter(commandWrapper, "@CostCoin", DbType.Int32, costCoin);
			database.AddInParameter(commandWrapper, "@CostRowVersion", DbType.Binary, costRowVersion);
			database.AddInParameter(commandWrapper, "@CostActive", DbType.Int32, costActive);
			database.AddInParameter(commandWrapper, "@LimitActTimes", DbType.Int32, limitActTimes);
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@ActType", DbType.Int32, actType);
			database.AddInParameter(commandWrapper, "@ActKey", DbType.AnsiString, actKey);
			database.AddInParameter(commandWrapper, "@CostType", DbType.Int32, costType);
			database.AddInParameter(commandWrapper, "@CostValue", DbType.Int32, costValue);
			database.AddInParameter(commandWrapper, "@GainType", DbType.Int32, gainType);
			database.AddInParameter(commandWrapper, "@GainMap", DbType.AnsiString, gainMap);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ShopBuy
		
		/// <summary>
        /// ShopBuy
        /// </summary>
		/// <param name="tranFlag">tranFlag</param>
		/// <param name="managerId">managerId</param>
		/// <param name="account">account</param>
		/// <param name="costGold">costGold</param>
		/// <param name="costGoldItemNo">costGoldItemNo</param>
		/// <param name="costGoldOrderId">costGoldOrderId</param>
		/// <param name="costCoin">costCoin</param>
		/// <param name="costRowVersion">costRowVersion</param>
		/// <param name="guildId">guildId</param>
		/// <param name="prizeNo">prizeNo</param>
		/// <param name="costActive">costActive</param>
		/// <param name="costQty">costQty</param>
		/// <param name="gainType">gainType</param>
		/// <param name="gainMap">gainMap</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool ShopBuy ( System.Boolean tranFlag, System.Guid managerId, System.String account, System.Int32 costGold, System.Int32 costGoldItemNo, System.String costGoldOrderId, System.Int32 costCoin, System.Byte[] costRowVersion, System.Guid guildId, System.Int32 prizeNo, System.Int32 costActive, System.Int32 costQty, System.Int32 gainType, System.String gainMap,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_ShopBuy");
            
			database.AddInParameter(commandWrapper, "@TranFlag", DbType.Boolean, tranFlag);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@CostGold", DbType.Int32, costGold);
			database.AddInParameter(commandWrapper, "@CostGoldItemNo", DbType.Int32, costGoldItemNo);
			database.AddInParameter(commandWrapper, "@CostGoldOrderId", DbType.AnsiString, costGoldOrderId);
			database.AddInParameter(commandWrapper, "@CostCoin", DbType.Int32, costCoin);
			database.AddInParameter(commandWrapper, "@CostRowVersion", DbType.Binary, costRowVersion);
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@PrizeNo", DbType.Int32, prizeNo);
			database.AddInParameter(commandWrapper, "@CostActive", DbType.Int32, costActive);
			database.AddInParameter(commandWrapper, "@CostQty", DbType.Int32, costQty);
			database.AddInParameter(commandWrapper, "@GainType", DbType.Int32, gainType);
			database.AddInParameter(commandWrapper, "@GainMap", DbType.AnsiString, gainMap);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  UpdateActive
		
		/// <summary>
        /// UpdateActive
        /// </summary>
		/// <param name="tranFlag">tranFlag</param>
		/// <param name="limitActive">limitActive</param>
		/// <param name="guildId">guildId</param>
		/// <param name="managerId">managerId</param>
		/// <param name="activeType">activeType</param>
		/// <param name="newActive">newActive</param>
		/// <param name="stepExp">stepExp</param>
		/// <param name="nextGuildLevel">nextGuildLevel</param>
		/// <param name="nextMaxMembers">nextMaxMembers</param>
		/// <param name="managerActive">managerActive</param>
		/// <param name="guildActive">guildActive</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool UpdateActive ( System.Boolean tranFlag, System.Int32 limitActive, System.Guid guildId, System.Guid managerId, System.Int32 activeType, System.Int32 newActive, System.Int32 stepExp, System.Int32 nextGuildLevel, System.Int32 nextMaxMembers,ref  System.Int32 managerActive,ref  System.Int32 guildActive,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_UpdateActive");
            
			database.AddInParameter(commandWrapper, "@TranFlag", DbType.Boolean, tranFlag);
			database.AddInParameter(commandWrapper, "@LimitActive", DbType.Int32, limitActive);
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ActiveType", DbType.Int32, activeType);
			database.AddInParameter(commandWrapper, "@NewActive", DbType.Int32, newActive);
			database.AddInParameter(commandWrapper, "@StepExp", DbType.Int32, stepExp);
			database.AddInParameter(commandWrapper, "@NextGuildLevel", DbType.Int32, nextGuildLevel);
			database.AddInParameter(commandWrapper, "@NextMaxMembers", DbType.Int32, nextMaxMembers);
			database.AddParameter(commandWrapper, "@ManagerActive", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,managerActive);
			database.AddParameter(commandWrapper, "@GuildActive", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,guildActive);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            managerActive=(System.Int32)database.GetParameterValue(commandWrapper, "@ManagerActive");
            guildActive=(System.Int32)database.GetParameterValue(commandWrapper, "@GuildActive");
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  UpdateKpi
		
		/// <summary>
        /// UpdateKpi
        /// </summary>
		/// <param name="guildId">guildId</param>
		/// <param name="gKpi">gKpi</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool UpdateKpi ( System.Guid guildId,ref  System.Int32 gKpi,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_UpdateKpi");
            
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddParameter(commandWrapper, "@GKpi", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,gKpi);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            gKpi=(System.Int32)database.GetParameterValue(commandWrapper, "@GKpi");
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SetViceManager
		
		/// <summary>
        /// SetViceManager
        /// </summary>
		/// <param name="guildId">guildId</param>
		/// <param name="memberRank">memberRank</param>
		/// <param name="memberId">memberId</param>
		/// <param name="memberId2">memberId2</param>
		/// <param name="memberId3">memberId3</param>
		/// <param name="viceRank">viceRank</param>
		/// <param name="viceId">viceId</param>
		/// <param name="viceId2">viceId2</param>
		/// <param name="viceId3">viceId3</param>
		/// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool SetViceManager ( System.Guid guildId, System.Int32 memberRank, System.Guid memberId, System.Guid memberId2, System.Guid memberId3, System.Int32 viceRank, System.Guid viceId, System.Guid viceId2, System.Guid viceId3,ref  System.Int32 errorCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_SetViceManager");
            
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, guildId);
			database.AddInParameter(commandWrapper, "@MemberRank", DbType.Int32, memberRank);
			database.AddInParameter(commandWrapper, "@MemberId", DbType.Guid, memberId);
			database.AddInParameter(commandWrapper, "@MemberId2", DbType.Guid, memberId2);
			database.AddInParameter(commandWrapper, "@MemberId3", DbType.Guid, memberId3);
			database.AddInParameter(commandWrapper, "@ViceRank", DbType.Int32, viceRank);
			database.AddInParameter(commandWrapper, "@ViceId", DbType.Guid, viceId);
			database.AddInParameter(commandWrapper, "@ViceId2", DbType.Guid, viceId2);
			database.AddInParameter(commandWrapper, "@ViceId3", DbType.Guid, viceId3);
			database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,errorCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  DayFlush
		
		/// <summary>
        /// DayFlush
        /// </summary>
		/// <param name="lastDayNo">lastDayNo</param>
		/// <param name="lastDayTime">lastDayTime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool DayFlush ( System.String lastDayNo, System.DateTime lastDayTime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Guild_DayFlush");
            
			database.AddInParameter(commandWrapper, "@LastDayNo", DbType.AnsiString, lastDayNo);
			database.AddInParameter(commandWrapper, "@LastDayTime", DbType.DateTime, lastDayTime);

            
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
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Insert(GuildListEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GuildList_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, entity.GuildId);
			database.AddInParameter(commandWrapper, "@GuildName", DbType.String, entity.GuildName);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
			database.AddInParameter(commandWrapper, "@Logo", DbType.String, entity.Logo);
			database.AddInParameter(commandWrapper, "@Intro", DbType.String, entity.Intro);
			database.AddInParameter(commandWrapper, "@Note", DbType.String, entity.Note);
			database.AddInParameter(commandWrapper, "@GuildLevel", DbType.Int32, entity.GuildLevel);
			database.AddInParameter(commandWrapper, "@GuildActive", DbType.Int32, entity.GuildActive);
			database.AddInParameter(commandWrapper, "@GuildActiveCost", DbType.Int32, entity.GuildActiveCost);
			database.AddInParameter(commandWrapper, "@CntMembers", DbType.Int32, entity.CntMembers);
			database.AddInParameter(commandWrapper, "@MaxMembers", DbType.Int32, entity.MaxMembers);
			database.AddInParameter(commandWrapper, "@CreatorId", DbType.Guid, entity.CreatorId);
			database.AddInParameter(commandWrapper, "@CreatorName", DbType.String, entity.CreatorName);
			database.AddInParameter(commandWrapper, "@CreateTime", DbType.DateTime, entity.CreateTime);
			database.AddInParameter(commandWrapper, "@LeaderId", DbType.Guid, entity.LeaderId);
			database.AddInParameter(commandWrapper, "@LeaderName", DbType.String, entity.LeaderName);
			database.AddInParameter(commandWrapper, "@LeadTime", DbType.DateTime, entity.LeadTime);
			database.AddInParameter(commandWrapper, "@LeadTrack", DbType.Int32, entity.LeadTrack);
			database.AddInParameter(commandWrapper, "@GKpi", DbType.Int32, entity.GKpi);
			database.AddInParameter(commandWrapper, "@GRank", DbType.Int32, entity.GRank);
			database.AddInParameter(commandWrapper, "@LockFlag", DbType.Int32, entity.LockFlag);
			database.AddInParameter(commandWrapper, "@LockTime", DbType.DateTime, entity.LockTime);
			database.AddInParameter(commandWrapper, "@InvalidFlag", DbType.Int32, entity.InvalidFlag);
			database.AddInParameter(commandWrapper, "@RowTimeUp", DbType.DateTime, entity.RowTimeUp);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@GuildNo", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.GuildNo);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.GuildNo=(System.Int32)database.GetParameterValue(commandWrapper, "@GuildNo");
            
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
        /// <remarks>2016/6/7 16:20:08</remarks>
        public bool Update(GuildListEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GuildList_Update");
            
			database.AddInParameter(commandWrapper, "@GuildNo", DbType.Int32, entity.GuildNo);
			database.AddInParameter(commandWrapper, "@GuildId", DbType.Guid, entity.GuildId);
			database.AddInParameter(commandWrapper, "@GuildName", DbType.String, entity.GuildName);
			database.AddInParameter(commandWrapper, "@Icon", DbType.AnsiString, entity.Icon);
			database.AddInParameter(commandWrapper, "@Logo", DbType.String, entity.Logo);
			database.AddInParameter(commandWrapper, "@Intro", DbType.String, entity.Intro);
			database.AddInParameter(commandWrapper, "@Note", DbType.String, entity.Note);
			database.AddInParameter(commandWrapper, "@GuildLevel", DbType.Int32, entity.GuildLevel);
			database.AddInParameter(commandWrapper, "@GuildActive", DbType.Int32, entity.GuildActive);
			database.AddInParameter(commandWrapper, "@GuildActiveCost", DbType.Int32, entity.GuildActiveCost);
			database.AddInParameter(commandWrapper, "@CntMembers", DbType.Int32, entity.CntMembers);
			database.AddInParameter(commandWrapper, "@MaxMembers", DbType.Int32, entity.MaxMembers);
			database.AddInParameter(commandWrapper, "@CreatorId", DbType.Guid, entity.CreatorId);
			database.AddInParameter(commandWrapper, "@CreatorName", DbType.String, entity.CreatorName);
			database.AddInParameter(commandWrapper, "@CreateTime", DbType.DateTime, entity.CreateTime);
			database.AddInParameter(commandWrapper, "@LeaderId", DbType.Guid, entity.LeaderId);
			database.AddInParameter(commandWrapper, "@LeaderName", DbType.String, entity.LeaderName);
			database.AddInParameter(commandWrapper, "@LeadTime", DbType.DateTime, entity.LeadTime);
			database.AddInParameter(commandWrapper, "@LeadTrack", DbType.Int32, entity.LeadTrack);
			database.AddInParameter(commandWrapper, "@GKpi", DbType.Int32, entity.GKpi);
			database.AddInParameter(commandWrapper, "@GRank", DbType.Int32, entity.GRank);
			database.AddInParameter(commandWrapper, "@LockFlag", DbType.Int32, entity.LockFlag);
			database.AddInParameter(commandWrapper, "@LockTime", DbType.DateTime, entity.LockTime);
			database.AddInParameter(commandWrapper, "@InvalidFlag", DbType.Int32, entity.InvalidFlag);
			database.AddInParameter(commandWrapper, "@RowTimeUp", DbType.DateTime, entity.RowTimeUp);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.GuildNo=(System.Int32)database.GetParameterValue(commandWrapper, "@GuildNo");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
