
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
    /// TemplateActivityexdetail管理类
    /// </summary>
    public static partial class TemplateActivityexdetailMgr
    {
        
		#region  GetById
		
        public static TemplateActivityexdetailEntity GetById( System.Int32 idx)
        {
            var provider = new TemplateActivityexdetailProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TemplateActivityexdetailEntity> GetAll()
        {
            var provider = new TemplateActivityexdetailProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            TemplateActivityexdetailProvider provider = new TemplateActivityexdetailProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TemplateActivityexdetailEntity templateActivityexdetailEntity,DbTransaction trans=null)
        {
            var provider = new TemplateActivityexdetailProvider();
            return provider.Insert(templateActivityexdetailEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TemplateActivityexdetailEntity templateActivityexdetailEntity,DbTransaction trans=null)
        {
            var provider = new TemplateActivityexdetailProvider();
            return provider.Update(templateActivityexdetailEntity,trans);
        }
		
		#endregion	
		
		
	}
}

