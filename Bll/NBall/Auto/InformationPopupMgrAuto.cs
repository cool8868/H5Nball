
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
    /// InformationPopup管理类
    /// </summary>
    public static partial class InformationPopupMgr
    {
        
		#region  GetById
		
        public static InformationPopupEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new InformationPopupProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<InformationPopupEntity> GetAll(string zoneId="")
        {
            var provider = new InformationPopupProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManagerTop10
		
        public static List<InformationPopupEntity> GetByManagerTop10( System.Guid managerId,string zoneId="")
        {
            var provider = new InformationPopupProvider(zoneId);
            return provider.GetByManagerTop10( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId, System.Int32 recordId,DbTransaction trans=null,string zoneId="")
        {
            InformationPopupProvider provider = new InformationPopupProvider(zoneId);

            return provider.Delete( managerId, recordId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(InformationPopupEntity informationPopupEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new InformationPopupProvider(zoneId);
            return provider.Insert(informationPopupEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(InformationPopupEntity informationPopupEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new InformationPopupProvider(zoneId);
            return provider.Update(informationPopupEntity,trans);
        }
		
		#endregion	
		
		
	}
}

