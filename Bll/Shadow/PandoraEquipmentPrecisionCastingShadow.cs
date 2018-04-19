using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class PandoraEquipmentPrecisionCastingShadow : IShadow
    {
        public PandoraEquipmentPrecisionCastingShadow(Guid managerId, Guid itemId, string lockcondition, string exProperties, string curPropertiesint, int coin, int point, Guid transactionId)
        {
            Shadow = new ShadowPandoraEquipmentPrecisionCastingEntity();
            Shadow.ManagerId = managerId;
            Shadow.TransactionId = transactionId;
            Shadow.LockCondition = lockcondition;
            Shadow.ExProperties = exProperties;
            Shadow.CurProperties = curPropertiesint;
            Shadow.ItemId = itemId;
            Shadow.Coin = coin;
            Shadow.Point = point;
        }

        public ShadowPandoraEquipmentPrecisionCastingEntity Shadow { get; set; }


        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraEquipmentPrecisionCasting(Shadow);
        }
    }
}
