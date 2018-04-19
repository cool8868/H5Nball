
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
    /// TurntableLuckyrecord管理类
    /// </summary>
    public static partial class TurntableLuckyrecordMgr
    {
        
		#region  GetById
		
        public static TurntableLuckyrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new TurntableLuckyrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TurntableLuckyrecordEntity> GetAll(string zoneId="")
        {
            var provider = new TurntableLuckyrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            TurntableLuckyrecordProvider provider = new TurntableLuckyrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TurntableLuckyrecordEntity turntableLuckyrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TurntableLuckyrecordProvider(zoneId);
            return provider.Insert(turntableLuckyrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TurntableLuckyrecordEntity turntableLuckyrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TurntableLuckyrecordProvider(zoneId);
            return provider.Update(turntableLuckyrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
