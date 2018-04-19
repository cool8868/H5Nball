
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
    /// TemplateActivityexprize管理类
    /// </summary>
    public static partial class TemplateActivityexprizeMgr
    {
        
		#region  GetById
		
        public static TemplateActivityexprizeEntity GetById( System.Int32 idx)
        {
            var provider = new TemplateActivityexprizeProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TemplateActivityexprizeEntity> GetAll()
        {
            var provider = new TemplateActivityexprizeProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            TemplateActivityexprizeProvider provider = new TemplateActivityexprizeProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TemplateActivityexprizeEntity templateActivityexprizeEntity,DbTransaction trans=null)
        {
            var provider = new TemplateActivityexprizeProvider();
            return provider.Insert(templateActivityexprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TemplateActivityexprizeEntity templateActivityexprizeEntity,DbTransaction trans=null)
        {
            var provider = new TemplateActivityexprizeProvider();
            return provider.Update(templateActivityexprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

