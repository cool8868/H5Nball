
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
    /// PenaltykickSeason管理类
    /// </summary>
    public static partial class PenaltykickSeasonMgr
    {
        
		#region  GetById
		
        public static PenaltykickSeasonEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new PenaltykickSeasonProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetSeason
		
        public static PenaltykickSeasonEntity GetSeason(string zoneId="")
        {
            var provider = new PenaltykickSeasonProvider(zoneId);
            return provider.GetSeason();
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<PenaltykickSeasonEntity> GetAll(string zoneId="")
        {
            var provider = new PenaltykickSeasonProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            PenaltykickSeasonProvider provider = new PenaltykickSeasonProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(PenaltykickSeasonEntity penaltykickSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PenaltykickSeasonProvider(zoneId);
            return provider.Insert(penaltykickSeasonEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(PenaltykickSeasonEntity penaltykickSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PenaltykickSeasonProvider(zoneId);
            return provider.Update(penaltykickSeasonEntity,trans);
        }
		
		#endregion	
		
		
	}
}
