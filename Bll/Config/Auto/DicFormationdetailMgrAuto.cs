
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
    /// DicFormationdetail管理类
    /// </summary>
    public static partial class DicFormationdetailMgr
    {
        
		#region  GetById
		
        public static DicFormationdetailEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicFormationdetailProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicFormationdetailEntity> GetAll(string zoneId="")
        {
            var provider = new DicFormationdetailProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicFormationdetailProvider provider = new DicFormationdetailProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicFormationdetailEntity dicFormationdetailEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicFormationdetailProvider(zoneId);
            return provider.Insert(dicFormationdetailEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicFormationdetailEntity dicFormationdetailEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicFormationdetailProvider(zoneId);
            return provider.Update(dicFormationdetailEntity,trans);
        }
		
		#endregion	
		
		
	}
}

