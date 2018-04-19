
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class FriendinviteMgr
    {

        #region  InviteManagerList

        public static List<FriendinviteEntity> InviteManagerList(System.String account, string zoneId = "")
        {
            var provider = new FriendinviteProvider(zoneId);
            return provider.InviteManagerList(account);
        }

        #endregion		 
	}
}

