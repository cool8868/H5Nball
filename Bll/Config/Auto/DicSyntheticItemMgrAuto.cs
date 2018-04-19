
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
    /// DicSyntheticitem管理类
    /// </summary>
    public static partial class DicSyntheticitemMgr
    {
        
		#region  GetById
		
        public static DicSyntheticitemEntity GetById( System.Int32 itemCode,string zoneId="")
        {
            var provider = new DicSyntheticitemProvider(zoneId);
            return provider.GetById( itemCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicSyntheticitemEntity> GetAll(string zoneId="")
        {
            var provider = new DicSyntheticitemProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 itemCode,DbTransaction trans=null,string zoneId="")
        {
            DicSyntheticitemProvider provider = new DicSyntheticitemProvider(zoneId);

            return provider.Delete( itemCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicSyntheticitemEntity dicSyntheticitemEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSyntheticitemProvider(zoneId);
            return provider.Insert(dicSyntheticitemEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicSyntheticitemEntity dicSyntheticitemEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSyntheticitemProvider(zoneId);
            return provider.Update(dicSyntheticitemEntity,trans);
        }
		
		#endregion	
		
		
	}
}

