
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
    /// GambleTitle管理类
    /// </summary>
    public static partial class GambleTitleMgr
    {
        
		#region  GetByOptionRateId
		
        public static GambleTitleEntity GetByOptionRateId( System.Int32 optionRateId,string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetByOptionRateId( optionRateId);
        }
		
		#endregion		  
		
		#region  GetByIdAndStatus
		
        public static GambleTitleEntity GetByIdAndStatus( System.Guid idx, System.Int32 status,string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetByIdAndStatus( idx, status);
        }
		
		#endregion		  
		
		#region  GetById
		
        public static GambleTitleEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetStopedGambleTitles
		
        public static List<GambleTitleEntity> GetStopedGambleTitles(string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetStopedGambleTitles();            
        }
		
		#endregion		  
		
		#region  GetCanHostStartList
		
        public static List<GambleTitleEntity> GetCanHostStartList(string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetCanHostStartList();            
        }
		
		#endregion		  
		
		#region  GetStartList
		
        public static List<GambleTitleEntity> GetStartList(string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetStartList();            
        }
		
		#endregion		  
		
		#region  GetNeedOpenGambleTitles
		
        public static List<GambleTitleEntity> GetNeedOpenGambleTitles(string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetNeedOpenGambleTitles();            
        }
		
		#endregion		  
		
		#region  GetByStatus
		
        public static List<GambleTitleEntity> GetByStatus( System.Int32 status,string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetByStatus( status);            
        }
		
		#endregion		  
		
		#region  GetByDate
		
        public static List<GambleTitleEntity> GetByDate( System.DateTime date,string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetByDate( date);            
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GambleTitleEntity> GetAll(string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetFirstTime
		
		public static DateTime GetFirstTime (string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.GetFirstTime();
        }
		
		#endregion		  
		
		#region  AddCount
		
        public static bool AddCount ( System.Guid titleId,DbTransaction trans=null,string zoneId="")
        {
            GambleTitleProvider provider = new GambleTitleProvider(zoneId);

            return provider.AddCount( titleId,trans);
            
        }
		
		#endregion
        
		#region  Delete
		
        public static bool Delete ( System.Guid idx, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            GambleTitleProvider provider = new GambleTitleProvider(zoneId);

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GambleTitleEntity gambleTitleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.Insert(gambleTitleEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GambleTitleEntity gambleTitleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleTitleProvider(zoneId);
            return provider.Update(gambleTitleEntity,trans);
        }
		
		#endregion	
		
		
	}
}
