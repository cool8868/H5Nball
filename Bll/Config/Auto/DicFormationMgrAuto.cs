
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
    /// DicFormation管理类
    /// </summary>
    public static partial class DicFormationMgr
    {
        
		#region  GetById
		
        public static DicFormationEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicFormationProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicFormationEntity> GetAll(string zoneId="")
        {
            var provider = new DicFormationProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicFormationProvider provider = new DicFormationProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicFormationEntity dicFormationEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicFormationProvider(zoneId);
            return provider.Insert(dicFormationEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicFormationEntity dicFormationEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicFormationProvider(zoneId);
            return provider.Update(dicFormationEntity,trans);
        }
		
		#endregion	
		
		
	}
}

