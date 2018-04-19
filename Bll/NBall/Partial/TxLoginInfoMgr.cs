
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class TxLogininfoMgr
    {

        #region  InsertUpdate

        public static bool InsertUpdate(TxLogininfoEntity txLogininfoEntity, DbTransaction trans = null, string zoneId = "")
        {
            TxLogininfoProvider provider = new TxLogininfoProvider(zoneId);

            return provider.InsertUpdate(txLogininfoEntity, trans);

        }

        #endregion
        
	}
}
