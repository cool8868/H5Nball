using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class PandoraDecomposeShadow : IShadow
    {
        public PandoraDecomposeShadow(string itemIds, string itemCodes, int critRate, bool isCrit, int coin,string equipmentList, Guid transactionId)
        {
            Shadow = new ShadowPandoraDecomposeEntity();
            Shadow.TransactionId = transactionId;
            Shadow.ItemIds = itemIds;
            Shadow.ItemCodes = itemCodes;
            Shadow.CritRate = critRate;
            Shadow.IsCrit = isCrit;
            Shadow.Coin = coin;
            Shadow.EquipmentList = equipmentList;
        }

        public ShadowPandoraDecomposeEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraDecompose(Shadow);
        }
    }
}
