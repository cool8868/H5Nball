
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
    /// TemplateActivityex管理类
    /// </summary>
    public static partial class TemplateActivityexMgr
    {
        
		#region  GetById
		
        public static TemplateActivityexEntity GetById( System.Int32 idx)
        {
            var provider = new TemplateActivityexProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TemplateActivityexEntity> GetAll()
        {
            var provider = new TemplateActivityexProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            TemplateActivityexProvider provider = new TemplateActivityexProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TemplateActivityexEntity templateActivityexEntity,DbTransaction trans=null)
        {
            var provider = new TemplateActivityexProvider();
            return provider.Insert(templateActivityexEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TemplateActivityexEntity templateActivityexEntity,DbTransaction trans=null)
        {
            var provider = new TemplateActivityexProvider();
            return provider.Update(templateActivityexEntity,trans);
        }
		
		#endregion	
		
		
	}
}

