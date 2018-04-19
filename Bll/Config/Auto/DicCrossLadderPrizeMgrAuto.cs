
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
    /// DicCrossladderprize管理类
    /// </summary>
    public static partial class DicCrossladderprizeMgr
    {
        
		#region  GetById
		
        public static DicCrossladderprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicCrossladderprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicCrossladderprizeEntity> GetAll(string zoneId="")
        {
            var provider = new DicCrossladderprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicCrossladderprizeProvider provider = new DicCrossladderprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicCrossladderprizeEntity dicCrossladderprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicCrossladderprizeProvider(zoneId);
            return provider.Insert(dicCrossladderprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicCrossladderprizeEntity dicCrossladderprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicCrossladderprizeProvider(zoneId);
            return provider.Update(dicCrossladderprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
