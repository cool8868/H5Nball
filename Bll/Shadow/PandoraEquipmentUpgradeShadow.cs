using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class PandoraEquipmentUpgradeShadow : IShadow
    {
        public PandoraEquipmentUpgradeShadow(Guid managerId, Guid itemId, int curLevel, int resultLevle, string properties, int costCoin, Guid transactionId)
        {
            Shadow = new ShadowPandoraEquipmentUpgradeEntity();
            Shadow.TransactionId = transactionId;
            Shadow.ManagerId = managerId;
            Shadow.ItemId = itemId;
            Shadow.CurLevel = curLevel;
            Shadow.ResultLevel = resultLevle;
            Shadow.Properties = properties;
            Shadow.CostCoin = costCoin;
        }

        public ShadowPandoraEquipmentUpgradeEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraEquipmentUpgrade(Shadow);
        }
    }

    public class PandoraEquipmentSellShadow : IShadow
    {
        public PandoraEquipmentSellShadow(Guid managerId, Guid itemId, int itemCode, int coin,Guid transactionId)
        {
            Shadow = new ShadowPandoraEquipmentSellEntity();
            Shadow.TransactionId = transactionId;
            Shadow.ManagerId = managerId;
            Shadow.ItemId = itemId;
            Shadow.ItemCode = itemCode;
            Shadow.Coin = coin;
        }

        public ShadowPandoraEquipmentSellEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraEquipmentSell(Shadow);
        }
    }
}
