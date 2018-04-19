
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class TemplateActivityexMgr
    {
        public static List<TemplateActivityexEntity> GetByZoneId(int zoneId)
        {
            var provider = new TemplateActivityexProvider();
            return provider.GetByZoneId(zoneId);
        }
        
	}
}

