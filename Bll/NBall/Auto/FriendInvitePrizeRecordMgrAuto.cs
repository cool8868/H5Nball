
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
    /// FriendinvitePrizerecord管理类
    /// </summary>
    public static partial class FriendinvitePrizerecordMgr
    {
        
		#region  GetById
		
        public static FriendinvitePrizerecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new FriendinvitePrizerecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<FriendinvitePrizerecordEntity> GetAll(string zoneId="")
        {
            var provider = new FriendinvitePrizerecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  InvitePrizeIsGet
		
        public static List<FriendinvitePrizerecordEntity> InvitePrizeIsGet( System.String account,string zoneId="")
        {
            var provider = new FriendinvitePrizerecordProvider(zoneId);
            return provider.InvitePrizeIsGet( account);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            FriendinvitePrizerecordProvider provider = new FriendinvitePrizerecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(FriendinvitePrizerecordEntity friendinvitePrizerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendinvitePrizerecordProvider(zoneId);
            return provider.Insert(friendinvitePrizerecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(FriendinvitePrizerecordEntity friendinvitePrizerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendinvitePrizerecordProvider(zoneId);
            return provider.Update(friendinvitePrizerecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

