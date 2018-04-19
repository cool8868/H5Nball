
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    /// <summary>
    /// GuildList管理类
    /// </summary>
    public static partial class GuildListMgr
    {
        
		#region  GetById
		
        public static GuildListEntity GetById( System.Int32 guildNo,string zoneId="")
        {
            var provider = new GuildListProvider(zoneId);
            return provider.GetById( guildNo);
        }
		
		#endregion		  
		
		#region  GetGuild
		
        public static GuildListEntity GetGuild( System.Guid guildId,string zoneId="")
        {
            var provider = new GuildListProvider(zoneId);
            return provider.GetGuild( guildId);
        }
		
		#endregion		  
		
		#region  GetGuildByName
		
        public static GuildListEntity GetGuildByName( System.String guildName,string zoneId="")
        {
            var provider = new GuildListProvider(zoneId);
            return provider.GetGuildByName( guildName);
        }
		
		#endregion		  
		
		#region  GetGuildByManager
		
        public static GuildListEntity GetGuildByManager( System.Guid managerId,string zoneId="")
        {
            var provider = new GuildListProvider(zoneId);
            return provider.GetGuildByManager( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GuildListEntity> GetAll(string zoneId="")
        {
            var provider = new GuildListProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  PageByRowNumber
		
        public static List<GuildListEntity> PageByRowNumber( System.String tableName, System.String selectFields, System.String whereStr, System.String orderFields, System.Int32 pageSize, System.Int32 pageNo, System.Boolean ifCount,ref  System.Int32 pageCount,ref  System.Int32 rowCount,string zoneId="")
        {
            var provider = new GuildListProvider(zoneId);
            return provider.PageByRowNumber( tableName, selectFields, whereStr, orderFields, pageSize, pageNo, ifCount,ref  pageCount,ref  rowCount);            
        }
		
		#endregion		  
		
		#region  Create
		
        public static bool Create ( System.Boolean tranFlag, System.Guid managerId, System.String account, System.Int32 costGold, System.Int32 costGoldItemNo, System.String costGoldOrderId, System.Int32 costCoin, System.Byte[] costRowVersion, System.Guid guildId, System.String guildName, System.String icon, System.String logo, System.String intro, System.String note, System.Int32 guildLevel, System.Int32 maxMembers, System.Int32 leadTrack, System.String managerName, System.Int32 authRank,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Create( tranFlag, managerId, account, costGold, costGoldItemNo, costGoldOrderId, costCoin, costRowVersion, guildId, guildName, icon, logo, intro, note, guildLevel, maxMembers, leadTrack, managerName, authRank,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Join
		
        public static bool Join ( System.Boolean tranFlag, System.Guid guildId, System.Int32 maxMembers, System.Guid managerId, System.String managerName, System.Int32 authRank,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Join( tranFlag, guildId, maxMembers, managerId, managerName, authRank,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Grant
		
        public static bool Grant ( System.Guid guildId, System.Guid leaderId, System.Int32 newLeaderRank, System.Guid memberId, System.String memberName, System.Int32 newMemberRank, System.Int32 leadTrack,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Grant( guildId, leaderId, newLeaderRank, memberId, memberName, newMemberRank, leadTrack,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Exit
		
        public static bool Exit ( System.Boolean tranFlag, System.Guid guildId, System.Guid managerId, System.Guid leaderId, System.Int32 authRank, System.Int32 exitTrack,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Exit( tranFlag, guildId, managerId, leaderId, authRank, exitTrack,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Drop
		
        public static bool Drop ( System.Boolean tranFlag, System.Guid guildId, System.Guid managerId, System.Int32 authRank, System.Int32 exitTrack,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Drop( tranFlag, guildId, managerId, authRank, exitTrack,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Option
		
        public static bool Option ( System.Guid guildId, System.Int32 opType, System.String opData,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Option( guildId, opType, opData,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Request
		
        public static bool Request ( System.Guid guildId, System.Int32 dstType, System.Guid dstId, System.String dstName, System.Int32 srcType, System.Guid srcId, System.String srcName, System.Int32 msgNo, System.Int32 msgType, System.Int32 msgState, System.String body, System.String link,ref  System.Int64 reqNo,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Request( guildId, dstType, dstId, dstName, srcType, srcId, srcName, msgNo, msgType, msgState, body, link,ref  reqNo,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Reply
		
        public static bool Reply ( System.Int64 reqNo, System.Guid dstId, System.Guid srcId, System.Int32 msgType,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Reply( reqNo, dstId, srcId, msgType,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Log
		
        public static bool Log ( System.Int32 msgSize, System.Int32 dstType, System.Guid dstId, System.String dstName, System.Int32 srcType, System.Guid srcId, System.String srcName, System.Int32 msgNo, System.Int32 msgType, System.Int32 msgState, System.String body, System.String link,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Log( msgSize, dstType, dstId, dstName, srcType, srcId, srcName, msgNo, msgType, msgState, body, link,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Act
		
        public static bool Act ( System.Boolean tranFlag, System.Guid managerId, System.String account, System.Int32 costGold, System.Int32 costGoldItemNo, System.String costGoldOrderId, System.Int32 costCoin, System.Byte[] costRowVersion, System.Int32 costActive, System.Int32 limitActTimes, System.Guid guildId, System.Int32 actType, System.String actKey, System.Int32 costType, System.Int32 costValue, System.Int32 gainType, System.String gainMap,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.Act( tranFlag, managerId, account, costGold, costGoldItemNo, costGoldOrderId, costCoin, costRowVersion, costActive, limitActTimes, guildId, actType, actKey, costType, costValue, gainType, gainMap,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  ShopBuy
		
        public static bool ShopBuy ( System.Boolean tranFlag, System.Guid managerId, System.String account, System.Int32 costGold, System.Int32 costGoldItemNo, System.String costGoldOrderId, System.Int32 costCoin, System.Byte[] costRowVersion, System.Guid guildId, System.Int32 prizeNo, System.Int32 costActive, System.Int32 costQty, System.Int32 gainType, System.String gainMap,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.ShopBuy( tranFlag, managerId, account, costGold, costGoldItemNo, costGoldOrderId, costCoin, costRowVersion, guildId, prizeNo, costActive, costQty, gainType, gainMap,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  UpdateActive
		
        public static bool UpdateActive ( System.Boolean tranFlag, System.Int32 limitActive, System.Guid guildId, System.Guid managerId, System.Int32 activeType, System.Int32 newActive, System.Int32 stepExp, System.Int32 nextGuildLevel, System.Int32 nextMaxMembers,ref  System.Int32 managerActive,ref  System.Int32 guildActive,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.UpdateActive( tranFlag, limitActive, guildId, managerId, activeType, newActive, stepExp, nextGuildLevel, nextMaxMembers,ref  managerActive,ref  guildActive,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  UpdateKpi
		
        public static bool UpdateKpi ( System.Guid guildId,ref  System.Int32 gKpi,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.UpdateKpi( guildId,ref  gKpi,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  SetViceManager
		
        public static bool SetViceManager ( System.Guid guildId, System.Int32 memberRank, System.Guid memberId, System.Guid memberId2, System.Guid memberId3, System.Int32 viceRank, System.Guid viceId, System.Guid viceId2, System.Guid viceId3,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.SetViceManager( guildId, memberRank, memberId, memberId2, memberId3, viceRank, viceId, viceId2, viceId3,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  DayFlush
		
        public static bool DayFlush ( System.String lastDayNo, System.DateTime lastDayTime,DbTransaction trans=null,string zoneId="")
        {
            GuildListProvider provider = new GuildListProvider(zoneId);

            return provider.DayFlush( lastDayNo, lastDayTime,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GuildListEntity guildListEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GuildListProvider(zoneId);
            return provider.Insert(guildListEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GuildListEntity guildListEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GuildListProvider(zoneId);
            return provider.Update(guildListEntity,trans);
        }
		
		#endregion	
		
		
	}
}
