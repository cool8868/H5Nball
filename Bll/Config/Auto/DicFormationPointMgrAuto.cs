
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
    /// DicFormationpoint管理类
    /// </summary>
    public static partial class DicFormationpointMgr
    {
        
		#region  GetById
		
        public static DicFormationpointEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicFormationpointProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicFormationpointEntity> GetAll(string zoneId="")
        {
            var provider = new DicFormationpointProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicFormationpointProvider provider = new DicFormationpointProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicFormationpointEntity dicFormationpointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicFormationpointProvider(zoneId);
            return provider.Insert(dicFormationpointEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicFormationpointEntity dicFormationpointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicFormationpointProvider(zoneId);
            return provider.Update(dicFormationpointEntity,trans);
        }
		
		#endregion	
		
		
	}
}

