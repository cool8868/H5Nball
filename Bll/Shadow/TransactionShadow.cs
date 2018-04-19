using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class TransactionShadow
    {
        private static NBThreadPool _threadPool = new NBThreadPool(10);
        private string _zoneId = "";

        public Guid TransactionId { get { return Transaction.Idx; } }

        public ShadowTransactionEntity Transaction { get; set; }
        public List<IShadow> Shadows { get; set; }
        public List<ItemShadow> ItemShadows { get; set; }


        public TransactionShadow(Guid managerId, EnumTransactionType transactionType, string zoneId = "")
        {
            Transaction = new ShadowTransactionEntity();

            Transaction.Idx = ShareUtil.GenerateComb();
            Transaction.TransactionType = (int)transactionType;

            Transaction.ManagerId = managerId;
            Transaction.RowTime = DateTime.Now;
            Transaction.TerminalIP = ShareUtil.GetServerIp();
            Transaction.AppId = ShareUtil.AppId;
            Shadows = new List<IShadow>();
            ItemShadows = new List<ItemShadow>();
            _zoneId = zoneId;
        }

        #region item
        /// <summary>
        /// itemlist
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="operationType"></param>
        public void AddShadow(List<ItemInfoEntity> itemList, EnumOperationType operationType, int operationCount)
        {
            if (CacheFactory.AppsettingCache.NotShadowItem)
                return;
            if (itemList == null || itemList.Count <= 0)
                return;
            foreach (var itemInfoEntity in itemList)
            {
                AddShadow(itemInfoEntity, operationType, operationCount);
            }
        }

        /// <summary>
        /// item
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationType"></param>
        public void AddShadow(ItemInfoEntity entity, EnumOperationType operationType, int operationCount)
        {
            if (CacheFactory.AppsettingCache.NotShadowItem)
                return;
            var shadow = ItemShadows.Find(d => d.Shadow.ItemId == entity.ItemId);
            if (shadow != null)
            {
                if (shadow.Shadow.OperationType != (int)operationType)
                {
                    shadow.Shadow.OperationCount = operationCount;
                    shadow.Shadow.OperationType = (int)operationType;
                }
                else
                {
                    shadow.Shadow.OperationCount += operationCount;
                }
            }
            else
            {
                ItemShadows.Add(new ItemShadow(entity, operationType, TransactionId, operationCount));
            }
        }

        public void UpdateItemGrid(Guid itemId, int grid)
        {
            if (CacheFactory.AppsettingCache.NotShadowItem)
                return;
            var shadow = ItemShadows.Find(d => d.Shadow.ItemId == itemId);
            if (shadow != null)
            {
                shadow.Shadow.GridIndex = grid;
            }
        }
        #endregion

        #region pandora
        /// <summary>
        /// PandoraDecompose
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemIds"></param>
        /// <param name="itemCodes"></param>
        /// <param name="critRate"></param>
        /// <param name="isCrit"></param>
        /// <param name="coin"></param>
        /// <param name="equipmentList"></param>
        public void AddShadow(string itemIds, string itemCodes, int critRate, bool isCrit, int coin, string equipmentList)
        {
            Shadows.Add(new PandoraDecomposeShadow(itemIds, itemCodes, critRate, isCrit, coin, equipmentList, TransactionId));
        }
        /// <summary>
        /// PandoraEquipmentwash
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="itemCode"></param>
        /// <param name="lockPropertyId"></param>
        /// <param name="buyStone"></param>
        /// <param name="buyFusogen"></param>
        /// <param name="costPoint"></param>
        public void AddShadow(Guid itemId, int itemCode, int lockPropertyId, bool buyStone, bool buyFusogen, int costPoint)
        {
            Shadows.Add(new PandoraEquipmentwashShadow(itemId, itemCode, lockPropertyId, buyStone, buyFusogen, costPoint, TransactionId));
        }

        /// <summary>
        /// PandoraEquipmentSellShadow
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="itemCode"></param>
        /// <param name="coin"></param>
        public void AddShadow(Guid managerId,Guid itemId, int itemCode, int coin)
        {
            Shadows.Add(new PandoraEquipmentSellShadow(managerId, itemId, itemCode, coin, TransactionId));
        }

        /// <summary>
        /// PandoraEquipmentUpgradeShadow
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="curLevel"></param>
        /// <param name="resultLevel"></param>
        /// <param name="costCoin"></param>
        public void AddShadow(Guid managerId, Guid itemId, int curLevel, int resultLevel, string properties, int costCoin)
        {
            Shadows.Add(new PandoraEquipmentUpgradeShadow(managerId, itemId, curLevel, resultLevel, properties, costCoin, TransactionId));
        }

        /// <summary>
        /// PandoraEquipmentPrecisionCasting
        /// </summary>
        public void AddShadow(Guid managerId, Guid itemId, string lockCondition, string exProperties, string curProperties, int coin, int point)
        {
            Shadows.Add(new PandoraEquipmentPrecisionCastingShadow(managerId, itemId, lockCondition, exProperties,
                curProperties, coin, point, TransactionId));
        }

        /// <summary>
        /// PandoraArousal
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="sourceCard"></param>
        /// <param name="sourceCardLv"></param>
        /// <param name="curArousalLv"></param>
        /// <param name="tarArousalLv"></param>
        /// <param name="useCard"></param>
        /// <param name="useCardLevel"></param>
        /// <param name="isBingding"></param>
        public void AddShadow(Guid managerId, Guid sourceCard, int sourceCardLv, int curArousalLv, int tarArousalLv, Guid useCard,
            int useCardStrenth, bool isBingding)
        {
            Shadows.Add(new PandoraArousalShadow(managerId, sourceCard, sourceCardLv, curArousalLv, tarArousalLv, useCard, useCardStrenth,
                isBingding, TransactionId));
        }

        /// <summary>
        /// 勋章兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemIds"></param>
        /// <param name="itemCodes"></param>
        /// <param name="medalCount"></param>
        public void AddShadow(Guid managerId, int type, string itemIds, string itemCodes, int medalCount)
        {
            Shadows.Add(new PandoraMedalShadow(managerId, type, itemIds, itemCodes, medalCount, TransactionId));
        }

        /// <summary>
        /// PandoraMosaic
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="itemCode"></param>
        /// <param name="slotId"></param>
        /// <param name="ballsoulId"></param>
        /// <param name="ballsoulItemCode"></param>
        public void AddShadow(Guid itemId, int itemCode, int slotId, Guid ballsoulId, int ballsoulItemCode)
        {
            Shadows.Add(new PandoraMosaicShadow(itemId, itemCode, slotId, ballsoulId, ballsoulItemCode, TransactionId));
        }
        /// <summary>
        /// PandoraStrength
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemCode1"></param>
        /// <param name="strength1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemCode2"></param>
        /// <param name="strength2"></param>
        /// <param name="isProtect"></param>
        /// <param name="costCoin"></param>
        /// <param name="costPoint"></param>
        /// <param name="resultType"></param>
        /// <param name="resultItemId"></param>
        /// <param name="resultItemCode"></param>
        /// <param name="resultStrength"></param>
        public void AddShadow(Guid itemId1, int itemCode1, int strength1, Guid itemId2, int itemCode2, int strength2, bool isProtect, int costCoin, int costPoint, int resultType, Guid resultItemId, int resultItemCode, int resultStrength
            , Guid luckyItemId, int luckyItemCode, double rate)
        {
            Shadows.Add(new PandoraStrengthShadow(itemId1, itemCode1, strength1, itemId2, itemCode2, strength2, isProtect, costCoin, costPoint, resultType, resultItemId, resultItemCode, resultStrength, TransactionId, luckyItemId, luckyItemCode, rate));
        }
        /// <summary>
        /// PandoraStrength
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemCode1"></param>
        /// <param name="strength1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemCode2"></param>
        /// <param name="strength2"></param>
        /// <param name="isProtect"></param>
        /// <param name="costCoin"></param>
        /// <param name="costPoint"></param>
        /// <param name="resultType"></param>
        /// <param name="resultItemId"></param>
        /// <param name="resultItemCode"></param>
        /// <param name="resultStrength"></param>
        public void AddShadow(Guid itemId1, int itemCode1, int strength1, bool isProtect, int costCoin, int costPoint, int resultType, Guid resultItemId, int resultItemCode, int resultStrength
            , Guid luckyItemId, int luckyItemCode, double rate)
        {
            Shadows.Add(new PandoraStrengthShadow(itemId1, itemCode1, strength1, isProtect, costCoin, costPoint, resultType, resultItemId, resultItemCode, resultStrength, TransactionId, luckyItemId, luckyItemCode, rate));
        }

        /// <summary>
        /// PandoraSynthesis
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemCode1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemCode2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemCode3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemCode4"></param>
        /// <param name="itemId5"></param>
        /// <param name="itemCode5"></param>
        /// <param name="suitdrawingId"></param>
        /// <param name="suitdrawingItemCode"></param>
        /// <param name="isProtect"></param>
        /// <param name="costCoin"></param>
        /// <param name="costPoint"></param>
        /// <param name="resultType"></param>
        /// <param name="resultItemId"></param>
        /// <param name="resultItemCode"></param>
        public void AddShadow(int synthesisType, List<ItemInfoEntity> itemList, Dictionary<Guid, ItemInfoEntity> mallList, Dictionary<Guid, int> mallCountDic, Guid suitdrawingId, int suitdrawingItemCode,
    bool isProtect, int costCoin, int costPoint, int resultType, Guid resultItemId, int resultItemCode
    , Guid luckyItemId, int luckyItemCode, Guid goldformulaItemId, int goldformulaItemCode, double rate)
        {
            Guid itemId1 = Guid.Empty; int itemCode1 = 0; Guid itemId2 = Guid.Empty; int itemCode2 = 0;
            Guid itemId3 = Guid.Empty; int itemCode3 = 0; Guid itemId4 = Guid.Empty; int itemCode4 = 0; Guid itemId5 = Guid.Empty; int itemCode5 = 0;
            if (itemList.Count > 0)
            {
                itemId1 = itemList[0].ItemId;
                itemCode1 = itemList[0].ItemCode;
            }
            if (itemList.Count > 1)
            {
                itemId2 = itemList[1].ItemId;
                itemCode2 = itemList[1].ItemCode;
            }
            if (itemList.Count > 2)
            {
                itemId3 = itemList[2].ItemId;
                itemCode3 = itemList[2].ItemCode;
            }
            if (itemList.Count > 3)
            {
                itemId4 = itemList[3].ItemId;
                itemCode4 = itemList[3].ItemCode;
            }
            if (itemList.Count > 4)
            {
                itemId5 = itemList[4].ItemId;
                itemCode5 = itemList[4].ItemCode;
            }
            int mallCount = 0;
            if (mallList != null)
            {
                foreach (var entity in mallList.Values)
                {
                    int count = mallCountDic[entity.ItemId];
                    for (int i = 0; i < count; i++)
                    {
                        mallCount++;
                        switch (mallCount)
                        {
                            case 1:
                                itemId5 = entity.ItemId;
                                itemCode5 = entity.ItemCode;
                                break;
                            case 2:
                                itemId4 = entity.ItemId;
                                itemCode4 = entity.ItemCode;
                                break;
                            case 3:
                                itemId3 = entity.ItemId;
                                itemCode3 = entity.ItemCode;
                                break;
                            case 4:
                                itemId2 = entity.ItemId;
                                itemCode2 = entity.ItemCode;
                                break;
                            case 5:
                                itemId1 = entity.ItemId;
                                itemCode1 = entity.ItemCode;
                                break;
                        }
                    }
                }
            }


            Shadows.Add(new PandoraSynthesisShadow(synthesisType, itemId1, itemCode1, itemId2, itemCode2, itemId3,
                                                   itemCode3, itemId4, itemCode4, itemId5, itemCode5, suitdrawingId,
                                                   suitdrawingItemCode, isProtect, costCoin, costPoint, resultType,
                                                   resultItemId, resultItemCode, TransactionId, luckyItemId,
                                                   luckyItemCode, goldformulaItemId, goldformulaItemCode, rate));
        }
        #endregion

        #region teammember
        /// <summary>
        /// Teammember
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationType"></param>
        public void AddShadow(TeammemberEntity entity, EnumOperationType operationType)
        {
            Shadows.Add(new TeammemberShadow(entity, operationType, TransactionId));
        }
        /// <summary>
        /// Teammember
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <param name="playerId"></param>
        /// <param name="operationType"></param>
        public void AddShadow(Guid teammemberId, int playerId, EnumOperationType operationType)
        {
            Shadows.Add(new TeammemberShadow(teammemberId, playerId, operationType, TransactionId));
        }

        #endregion

        #region Package
        public void AddShadow(int packageSize, byte[] itemString, byte itemVersion)
        {
            Shadows.Add(new ItemPackageShadow(packageSize, itemString, itemVersion, TransactionId));
        }
        #endregion

        #region ConstellationPackage

        //public void AddShadow(int packageSize, byte[] itemString, byte itemVersion) 
        //{
        //    Shadow.Add(new ConstellationPackageShadow
        //}

        #endregion

        public void Save()
        {
            _threadPool.Add(() => doSave(this, _zoneId));
        }

        static void doSave(TransactionShadow transaction, string zoneId)
        {
            if (transaction.Shadows == null)
                return;
            try
            {
                ShadowProvider provider = new ShadowProvider(zoneId);
                provider.SaveTransaction(transaction.Transaction);
                foreach (var shadow in transaction.Shadows)
                {
                    shadow.Save(provider);
                }

                foreach (var itemShadow in transaction.ItemShadows)
                {
                    itemShadow.Save(provider);
                }
                

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("TransactionShadow doSave", ex);
            }
        }
    }
}
