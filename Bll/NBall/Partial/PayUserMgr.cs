
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;


namespace Games.NBall.Bll
{
    
    public partial class PayUserMgr
    {
        public static List<PayManagerEntity> GetPayList(string zoneId = "")
        {
            var provider = new PayUserProvider(zoneId);
            return provider.GetPayList();
        }
        #region  Charge

        public static bool ChargeTx(System.String account, System.Int32 sourceType, System.String billingId, System.Int32 gamePoint, System.Int32 chargePoint, System.Decimal cash, System.Int32 bonus,int mallCode, ref  System.Int32 result, DbTransaction trans = null, string zoneId = "")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.ChargeTx(account, sourceType, billingId, gamePoint, chargePoint, cash, bonus, mallCode, ref  result, trans);

        }

        #endregion

        #region  ChargeForBonusV2 for MatchReward

        public static bool ChargeForBonusV2(System.String account, System.Int32 sourceType, System.String billingId, System.Int32 bonus, ref  System.Int32 totalPoint, ref  System.Int32 result, DbTransaction trans = null, string zoneId = "")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.ChargeForBonusV2(account, sourceType, billingId, bonus, ref totalPoint, ref  result, trans);

        }

        #endregion
	}
}

