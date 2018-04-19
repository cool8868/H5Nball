
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;
using Games.NBall.Entity.Config.Custom;

namespace Games.NBall.Bll
{
    
    public partial class TemplateRegisterMgr
    {
        public static List<TemplatePlayerName> GetPlayerNameList()
        {
            var provider = new TemplateRegisterProvider();
            return provider.GetPlayerNameList();
        }
	}
}

