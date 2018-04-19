
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
    /// TemplateRegisterplayer管理类
    /// </summary>
    public static partial class TemplateRegisterplayerMgr
    {
        
		#region  GetById
		
        public static TemplateRegisterplayerEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new TemplateRegisterplayerProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TemplateRegisterplayerEntity> GetAll(string zoneId="")
        {
            var provider = new TemplateRegisterplayerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            TemplateRegisterplayerProvider provider = new TemplateRegisterplayerProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TemplateRegisterplayerEntity templateRegisterplayerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TemplateRegisterplayerProvider(zoneId);
            return provider.Insert(templateRegisterplayerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TemplateRegisterplayerEntity templateRegisterplayerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TemplateRegisterplayerProvider(zoneId);
            return provider.Update(templateRegisterplayerEntity,trans);
        }
		
		#endregion	
		
		
	}
}

