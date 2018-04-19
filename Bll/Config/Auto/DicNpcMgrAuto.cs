
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
    /// DicNpc管理类
    /// </summary>
    public static partial class DicNpcMgr
    {
        
		#region  GetById
		
        public static DicNpcEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new DicNpcProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicNpcEntity> GetAll(string zoneId="")
        {
            var provider = new DicNpcProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCross
		
        public static List<DicNpcEntity> GetAllForCross(string zoneId="")
        {
            var provider = new DicNpcProvider(zoneId);
            return provider.GetAllForCross();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            DicNpcProvider provider = new DicNpcProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicNpcEntity dicNpcEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicNpcProvider(zoneId);
            return provider.Insert(dicNpcEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicNpcEntity dicNpcEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicNpcProvider(zoneId);
            return provider.Update(dicNpcEntity,trans);
        }
		
		#endregion	
		
		
	}
}

