
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
    /// CrosscrowdInfo管理类
    /// </summary>
    public static partial class CrosscrowdInfoMgr
    {
        
		#region  GetById
		
        public static CrosscrowdInfoEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrosscrowdInfoProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetCurrent
		
        public static CrosscrowdInfoEntity GetCurrent( System.Int32 domainId,string zoneId="")
        {
            var provider = new CrosscrowdInfoProvider(zoneId);
            return provider.GetCurrent( domainId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrosscrowdInfoEntity> GetAll(string zoneId="")
        {
            var provider = new CrosscrowdInfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  C_CrossCrowdNotSendPrize
		
        public static List<CrosscrowdInfoEntity> C_CrossCrowdNotSendPrize(string zoneId="")
        {
            var provider = new CrosscrowdInfoProvider(zoneId);
            return provider.C_CrossCrowdNotSendPrize();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrosscrowdInfoProvider provider = new CrosscrowdInfoProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  SaveRankPrizeStatus
		
        public static bool SaveRankPrizeStatus ( System.Int32 idx, System.Int32 status,DbTransaction trans=null,string zoneId="")
        {
            CrosscrowdInfoProvider provider = new CrosscrowdInfoProvider(zoneId);

            return provider.SaveRankPrizeStatus( idx, status,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrosscrowdInfoEntity crosscrowdInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdInfoProvider(zoneId);
            return provider.Insert(crosscrowdInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrosscrowdInfoEntity crosscrowdInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdInfoProvider(zoneId);
            return provider.Update(crosscrowdInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
