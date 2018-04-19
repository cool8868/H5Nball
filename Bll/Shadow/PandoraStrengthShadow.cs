using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class PandoraStrengthShadow : IShadow
    {
        public PandoraStrengthShadow(Guid itemId1, int itemCode1, int strength1, Guid itemId2, int itemCode2, int strength2, bool isProtect, int costCoin, int costPoint, int resultType, Guid resultItemId, int resultItemCode, int resultStrength, Guid transactionId
            , Guid luckyItemId, int luckyItemCode, double rate)
        {
            Shadow = new ShadowPandoraStrengthEntity();
            Shadow.TransactionId = transactionId;
            Shadow.ItemId1 = itemId1;
            Shadow.ItemCode1 = itemCode1;
            Shadow.Strength1 = strength1;
            Shadow.ItemId2 = itemId2;
            Shadow.ItemCode2 = itemCode2;
            Shadow.Strength2 = strength2;
            Shadow.IsProtect = isProtect;
            Shadow.CostCoin = costCoin;
            Shadow.CostPoint = costPoint;
            Shadow.ResultType = resultType;
            Shadow.ResultItemId = resultItemId;
            Shadow.ResultItemCode = resultItemCode;
            Shadow.ResultStrength = resultStrength;
            Shadow.LuckyItemId = luckyItemId;
            Shadow.LuckyItemCode = luckyItemCode;
            Shadow.Rate = Convert.ToDecimal(rate);
        }

        public PandoraStrengthShadow(Guid itemId1, int itemCode1, int strength1, bool isProtect, int costCoin, int costPoint, int resultType, Guid resultItemId, int resultItemCode, int resultStrength, Guid transactionId
           , Guid luckyItemId, int luckyItemCode, double rate)
        {
            Shadow = new ShadowPandoraStrengthEntity();
            Shadow.TransactionId = transactionId;
            Shadow.ItemId1 = itemId1;
            Shadow.ItemCode1 = itemCode1;
            Shadow.Strength1 = strength1;
            Shadow.IsProtect = isProtect;
            Shadow.CostCoin = costCoin;
            Shadow.CostPoint = costPoint;
            Shadow.ResultType = resultType;
            Shadow.ResultItemId = resultItemId;
            Shadow.ResultItemCode = resultItemCode;
            Shadow.ResultStrength = resultStrength;
            Shadow.LuckyItemId = luckyItemId;
            Shadow.LuckyItemCode = luckyItemCode;
            Shadow.Rate = Convert.ToDecimal(rate);
        }

        public ShadowPandoraStrengthEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraStrength(Shadow);
        }
    }
}
