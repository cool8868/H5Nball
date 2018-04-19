
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class TemplateActivityexprizeMgr
    {
        #region  GetByZone

        public static List<TemplateActivityexprizeEntity> GetByZone(System.Int32 zoneId)
        {
            var provider = new TemplateActivityexprizeProvider();
            return provider.GetByZone(zoneId);
        }

        #endregion	
        #region  GetByZoneAll

        public static List<TemplateActivityexprizeEntity> GetByZoneAll()
        {
            var provider = new TemplateActivityexprizeProvider();
            return provider.GetByZoneAll();
        }

        #endregion	
        
	}
}

