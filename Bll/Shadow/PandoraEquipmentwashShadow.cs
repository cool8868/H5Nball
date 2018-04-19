using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class PandoraEquipmentwashShadow : IShadow
    {
        public PandoraEquipmentwashShadow(Guid itemId, int itemCode, int lockPropertyId, bool buyStone, bool buyFusogen, int costPoint, Guid transactionId)
        {
            Shadow = new ShadowPandoraEquipmentwashEntity();
            Shadow.TransactionId = transactionId;
            Shadow.ItemId = itemId;
            Shadow.ItemCode = itemCode;
            Shadow.LockPropertyId = lockPropertyId;
            Shadow.BuyStone = buyStone;
            Shadow.BuyFusogen = buyFusogen;
            Shadow.CostPoint = costPoint;
        }

        public ShadowPandoraEquipmentwashEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraEquipmentwash(Shadow);
        }

    }
}
