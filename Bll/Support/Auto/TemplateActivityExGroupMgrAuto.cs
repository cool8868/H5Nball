
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
    /// TemplateActivityexgroup管理类
    /// </summary>
    public static partial class TemplateActivityexgroupMgr
    {
        
		#region  GetById
		
        public static TemplateActivityexgroupEntity GetById( System.Int32 idx)
        {
            var provider = new TemplateActivityexgroupProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TemplateActivityexgroupEntity> GetAll()
        {
            var provider = new TemplateActivityexgroupProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            TemplateActivityexgroupProvider provider = new TemplateActivityexgroupProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TemplateActivityexgroupEntity templateActivityexgroupEntity,DbTransaction trans=null)
        {
            var provider = new TemplateActivityexgroupProvider();
            return provider.Insert(templateActivityexgroupEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TemplateActivityexgroupEntity templateActivityexgroupEntity,DbTransaction trans=null)
        {
            var provider = new TemplateActivityexgroupProvider();
            return provider.Update(templateActivityexgroupEntity,trans);
        }
		
		#endregion	
		
		
	}
}

