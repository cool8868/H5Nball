
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
    /// TemplateRegister管理类
    /// </summary>
    public static partial class TemplateRegisterMgr
    {
        
		#region  GetById
		
        public static TemplateRegisterEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new TemplateRegisterProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TemplateRegisterEntity> GetAll(string zoneId="")
        {
            var provider = new TemplateRegisterProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            TemplateRegisterProvider provider = new TemplateRegisterProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Add
		
        public static bool Add ( System.String solutionString, System.Int32 kpi,DbTransaction trans=null,string zoneId="")
        {
            TemplateRegisterProvider provider = new TemplateRegisterProvider(zoneId);

            return provider.Add( solutionString, kpi,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TemplateRegisterEntity templateRegisterEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TemplateRegisterProvider(zoneId);
            return provider.Insert(templateRegisterEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TemplateRegisterEntity templateRegisterEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TemplateRegisterProvider(zoneId);
            return provider.Update(templateRegisterEntity,trans);
        }
		
		#endregion	
		
		
	}
}

