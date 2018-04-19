
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class NbManagerMgr
    {

        public static NbManagerEntity GetWithKpi(System.Guid idx)
        {
            var provider = new NbManagerProvider();
            return provider.GetWithKpi(idx);
        }

        public static bool ClearPackage(Guid managerId)
        {
            var provider = new NbManagerProvider();
            return provider.ClearPackage(managerId);
        }

        public static bool AssetRecord(System.Boolean tranFlag, System.Guid managerId, System.Int32 assetType, System.Int32 tranType, System.String tranMap, ref  System.Int32 errorCode, DbTransaction trans = null)
        {
            var provider = new NbManagerProvider();
            return provider.AssetRecord(tranFlag, managerId, assetType, tranType, tranMap, ref errorCode, trans);

        }

	}
}

