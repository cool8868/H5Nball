
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
    /// FriendOpenboxrecord管理类
    /// </summary>
    public static partial class FriendOpenboxrecordMgr
    {
        
		#region  GetById
		
        public static FriendOpenboxrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new FriendOpenboxrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<FriendOpenboxrecordEntity> GetAll(string zoneId="")
        {
            var provider = new FriendOpenboxrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            FriendOpenboxrecordProvider provider = new FriendOpenboxrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  GetCountByPrizeInfo
		
        public static bool GetCountByPrizeInfo ( System.DateTime startTime, System.DateTime endTime, System.Int32 prizeType, System.Int32 prizeItem,ref  System.Int32 prizeCount,DbTransaction trans=null,string zoneId="")
        {
            FriendOpenboxrecordProvider provider = new FriendOpenboxrecordProvider(zoneId);

            return provider.GetCountByPrizeInfo( startTime, endTime, prizeType, prizeItem,ref  prizeCount,trans);
            
        }
		
		#endregion
        
		#region  GetCountByPrizeType
		
        public static bool GetCountByPrizeType ( System.DateTime startTime, System.DateTime endTime, System.Int32 prizeType,ref  System.Int32 prizeCount,DbTransaction trans=null,string zoneId="")
        {
            FriendOpenboxrecordProvider provider = new FriendOpenboxrecordProvider(zoneId);

            return provider.GetCountByPrizeType( startTime, endTime, prizeType,ref  prizeCount,trans);
            
        }
		
		#endregion
        
		#region  GetCountByManagerAndPrizeType
		
        public static bool GetCountByManagerAndPrizeType ( System.Guid managerId, System.DateTime startTime, System.DateTime endTime, System.Int32 prizeType,ref  System.Int32 prizeCount,DbTransaction trans=null,string zoneId="")
        {
            FriendOpenboxrecordProvider provider = new FriendOpenboxrecordProvider(zoneId);

            return provider.GetCountByManagerAndPrizeType( managerId, startTime, endTime, prizeType,ref  prizeCount,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(FriendOpenboxrecordEntity friendOpenboxrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendOpenboxrecordProvider(zoneId);
            return provider.Insert(friendOpenboxrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(FriendOpenboxrecordEntity friendOpenboxrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendOpenboxrecordProvider(zoneId);
            return provider.Update(friendOpenboxrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

