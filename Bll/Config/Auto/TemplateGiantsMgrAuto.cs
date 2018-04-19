
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
    /// TemplateGiants管理类
    /// </summary>
    public static partial class TemplateGiantsMgr
    {
        
		#region  GetById
		
        public static TemplateGiantsEntity GetById( System.Int32 markId, System.Int32 round,string zoneId="")
        {
            var provider = new TemplateGiantsProvider(zoneId);
            return provider.GetById( markId, round);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TemplateGiantsEntity> GetAll(string zoneId="")
        {
            var provider = new TemplateGiantsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 markId, System.Int32 round,DbTransaction trans=null,string zoneId="")
        {
            TemplateGiantsProvider provider = new TemplateGiantsProvider(zoneId);

            return provider.Delete( markId, round,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TemplateGiantsEntity templateGiantsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TemplateGiantsProvider(zoneId);
            return provider.Insert(templateGiantsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TemplateGiantsEntity templateGiantsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TemplateGiantsProvider(zoneId);
            return provider.Update(templateGiantsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

