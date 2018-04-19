using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class PandoraSynthesisShadow : IShadow
    {
        public PandoraSynthesisShadow(int synthesisType, Guid itemId1, int itemCode1, Guid itemId2, int itemCode2,
            Guid itemId3, int itemCode3, Guid itemId4, int itemCode4, Guid itemId5, int itemCode5, Guid suitdrawingId, int suitdrawingItemCode,
            bool isProtect, int costCoin, int costPoint, int resultType, Guid resultItemId, int resultItemCode, Guid transactionId,
            Guid luckyItemId, int luckyItemCode, Guid goldformulaItemId, int goldformulaItemCode, double rate)
        {
            Shadow = new ShadowPandoraSynthesisEntity();
            Shadow.TransactionId = transactionId;
            Shadow.SynthesisType = synthesisType;
            Shadow.ItemId1 = itemId1;
            Shadow.ItemCode1 = itemCode1;
            Shadow.ItemId2 = itemId2;
            Shadow.ItemCode2 = itemCode2;
            Shadow.ItemId3 = itemId3;
            Shadow.ItemCode3 = itemCode3;
            Shadow.ItemId4 = itemId4;
            Shadow.ItemCode4 = itemCode4;
            Shadow.ItemId5 = itemId5;
            Shadow.ItemCode5 = itemCode5;
            Shadow.SuitdrawingId = suitdrawingId;
            Shadow.SuitdrawingItemCode = suitdrawingItemCode;
            Shadow.IsProtect = isProtect;
            Shadow.CostCoin = costCoin;
            Shadow.CostPoint = costPoint;
            Shadow.ResultType = resultType;
            Shadow.ResultItemId = resultItemId;
            Shadow.ResultItemCode = resultItemCode;
            Shadow.LuckyItemId = luckyItemId;
            Shadow.LuckyItemCode = luckyItemCode;
            Shadow.GoldFormulaItemId = goldformulaItemId;
            Shadow.GoldFormulaItemCode = goldformulaItemCode;
            Shadow.Rate = Convert.ToDecimal(rate);
        }

        public ShadowPandoraSynthesisEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraSynthesis(Shadow);
        }
    }
}
