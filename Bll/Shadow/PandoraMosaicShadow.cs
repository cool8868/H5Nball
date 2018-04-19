using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class PandoraMosaicShadow : IShadow
    {
        public PandoraMosaicShadow(Guid itemId, int itemCode, int slotId, Guid ballsoulId, int ballsoulItemCode, Guid transactionId)
        {
            Shadow = new ShadowPandoraMosaicEntity();
            Shadow.TransactionId = transactionId;
            Shadow.OperationType = (int)EnumOperationType.Update;
            Shadow.ItemId = itemId;
            Shadow.ItemCode = itemCode;
            Shadow.SlotId = slotId;
            Shadow.BallsoulId = ballsoulId;
            Shadow.BallsoulItemCode = ballsoulItemCode;
        }

        public ShadowPandoraMosaicEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraMosaic(Shadow);
        }

    }
}
