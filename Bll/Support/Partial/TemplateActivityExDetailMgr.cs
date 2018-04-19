
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class TemplateActivityexdetailMgr
    {
        #region  GetByZone

        public static List<TemplateActivityexdetailEntity> GetByZone(System.Int32 zoneId)
        {
            var provider = new TemplateActivityexdetailProvider();
            return provider.GetByZone(zoneId);
        }

        #endregion	
	}
}

