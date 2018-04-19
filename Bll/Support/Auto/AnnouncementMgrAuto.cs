
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
    /// Announcement管理类
    /// </summary>
    public static partial class AnnouncementMgr
    {
        
		#region  GetById
		
        public static AnnouncementEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new AnnouncementProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AnnouncementEntity> GetAll(string zoneId="")
        {
            var provider = new AnnouncementProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  SelectAnnouncement
		
        public static List<AnnouncementEntity> SelectAnnouncement( System.String platform,string zoneId="")
        {
            var provider = new AnnouncementProvider(zoneId);
            return provider.SelectAnnouncement( platform);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            AnnouncementProvider provider = new AnnouncementProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  CloseAnnouncement
		
        public static bool CloseAnnouncement ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            AnnouncementProvider provider = new AnnouncementProvider(zoneId);

            return provider.CloseAnnouncement( idx,trans);
            
        }
		
		#endregion
        
		#region  Ranable
		
        public static bool Ranable ( System.Int32 idx, System.Boolean isTop, System.DateTime startTime, System.DateTime endTime,DbTransaction trans=null,string zoneId="")
        {
            AnnouncementProvider provider = new AnnouncementProvider(zoneId);

            return provider.Ranable( idx, isTop, startTime, endTime,trans);
            
        }
		
		#endregion
        
		#region  Release
		
        public static bool Release ( System.String platform, System.Boolean isTop, System.String title, System.String contentString, System.DateTime startTime, System.DateTime endTime,DbTransaction trans=null,string zoneId="")
        {
            AnnouncementProvider provider = new AnnouncementProvider(zoneId);

            return provider.Release( platform, isTop, title, contentString, startTime, endTime,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AnnouncementEntity announcementEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AnnouncementProvider(zoneId);
            return provider.Insert(announcementEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AnnouncementEntity announcementEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AnnouncementProvider(zoneId);
            return provider.Update(announcementEntity,trans);
        }
		
		#endregion	
		
		
	}
}
