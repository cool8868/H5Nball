
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
    /// DicCrossladderexchange管理类
    /// </summary>
    public static partial class DicCrossladderexchangeMgr
    {
        
		#region  GetById
		
        public static DicCrossladderexchangeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicCrossladderexchangeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicCrossladderexchangeEntity> GetAll(string zoneId="")
        {
            var provider = new DicCrossladderexchangeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicCrossladderexchangeProvider provider = new DicCrossladderexchangeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicCrossladderexchangeEntity dicCrossladderexchangeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicCrossladderexchangeProvider(zoneId);
            return provider.Insert(dicCrossladderexchangeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicCrossladderexchangeEntity dicCrossladderexchangeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicCrossladderexchangeProvider(zoneId);
            return provider.Update(dicCrossladderexchangeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
