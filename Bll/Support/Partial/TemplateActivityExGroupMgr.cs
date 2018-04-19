
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class TemplateActivityexgroupMgr
    {
        #region  GetByZone

        public static List<TemplateActivityexgroupEntity> GetByZone(System.Int32 zoneId)
        {
            var provider = new TemplateActivityexgroupProvider();
            return provider.GetByZone(zoneId);
        }

        #endregion		
        
	}
}

