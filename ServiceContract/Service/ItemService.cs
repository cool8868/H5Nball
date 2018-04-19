using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Item;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class ItemService : IItemService
    {
        public ItemPackageResponse GetPackage(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => ItemCore.Instance.GetPackageResponse(managerId));
        }
       /// <summary>
        /// 后台添加物品
       /// </summary>
       /// <param name="zoneId"></param>
       /// <param name="managerId"></param>
       /// <param name="itemCode"></param>
       /// <param name="itemCount"></param>
       /// <param name="strength"></param>
       /// <param name="isBinding"></param>
       /// <param name="slotColorCount"></param>
       /// <returns></returns>
        public MessageCode GMAddItem(string zoneId, Guid managerId, int itemCode, int itemCount, int strength,
            bool isBinding, int slotColorCount = 0)
        {
            return ResponseHelper.TryCatch(() => ItemCore.Instance.GMAddItem(zoneId, managerId, itemCode, itemCount, strength,isBinding, slotColorCount));
        }
        /// <summary>
        /// 整理背包
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ItemPackageDataResponse Arrange(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => ItemCore.Instance.Arrangepackage(managerId));
        }

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ItemPackageDataResponse DeleteItem(Guid managerId, Guid itemId, int count)
        {
            return ResponseHelper.TryCatch(() => ItemCore.Instance.DeleteItem(managerId, itemId, count));
        }

        /// <summary>
        /// 拆分物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="splitCount"></param>
        /// <returns></returns>
        public ItemPackageDataResponse SplitItem(Guid managerId, Guid itemId, int splitCount)
        {
            return ResponseHelper.TryCatch(() => ItemCore.Instance.SplitItem(managerId, itemId, splitCount));
        }

        #region 潘多拉

        /// <summary>
        /// 强化球员卡参数
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="itemId1">物品ID</param>
        /// <param name="itemId2">物品ID</param>
        /// <returns></returns>
        public StrengthParamResponse StrengthenParam(Guid managerId, Guid itemId1, Guid itemId2)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.StrengthenParam(managerId, itemId1, itemId2));
        }

        /// <summary>
        /// 强化球员卡
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="itemId1">物品ID1</param>
        /// <param name="itemId2">物品ID2</param>
        /// <param name="isProtect">是否保护</param>
        /// <param name="protectId">保护膜物品ID</param>
        /// <returns></returns>
        public StrengthResponse Strengthen(Guid managerId, Guid itemId1, Guid itemId2, bool isProtect, Guid protectId)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.Strengthen(managerId, itemId1, itemId2, isProtect,protectId));
        }

        /// <summary>
        /// 分解球员卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemIds"></param>
        /// <returns></returns>
        public DecomposeResponse Decompose(Guid managerId, string itemIds)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.Decompose(managerId, itemIds));
        }

        /// <summary>
        /// 球员卡合成(新)
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <param name="isProtect"></param>
        /// <param name="luckyId"></param>
        /// <param name="goldFormulaId"></param>
        /// <param name="hasTask"></param>
        /// <returns></returns>
        public SynthesisResponse SynthesisNew(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5, bool isProtect, Guid luckyId, Guid goldFormulaId, bool hasTask)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.SynthesisNew(managerId, itemId1, itemId2, itemId3, itemId4, itemId5, isProtect, luckyId, goldFormulaId, hasTask));
        }

        /// <summary>
        /// 球员卡合成参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <param name="isGoldFormula"></param>
        /// <returns></returns>
        public SynthesisParamResponse SynthesisParamNew(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3,
            Guid itemId4, Guid itemId5, bool isGoldFormula)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.SynthesisParamNew(managerId, itemId1, itemId2, itemId3, itemId4, itemId5, isGoldFormula));
        }

        /// <summary>
        /// 合同页合成参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <returns></returns>
        public SynthesisParamResponse TheContractSyntheticParam(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.TheContractSyntheticParam(managerId, itemId1, itemId2, itemId3, itemId4, itemId5));
        }

        /// <summary>
        /// 合同页合成
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <returns></returns>
        public SynthesisResponse TheContractSynthetic(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.TheContractSynthetic(managerId, itemId1, itemId2, itemId3, itemId4, itemId5));
        }

        /// <summary>
        /// 合成装备参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <returns></returns>
        public EquipmentSynthesisParamResponse EquipmentSynthesisParam(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.EquipmentSynthesisParam(managerId, itemId1, itemId2, itemId3, itemId4, itemId5));
        }

        /// <summary>
        /// 装备合成
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <param name="isProtect"></param>
        /// <param name="protectId"></param>
        /// <returns></returns>
        public SynthesisResponse EquipmentSynthesis(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5, bool isProtect, Guid protectId)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.EquipmentSynthesis(managerId, itemId1, itemId2, itemId3, itemId4, itemId5, isProtect, protectId));
        }

        /// <summary>
        /// 装备出售
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public EquipmentSellResponse EquipmentSell(Guid managerId, Guid itemId)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.EquipmentSell(managerId, itemId));
        }

        /// <summary>
        /// 道具出售
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public EquipmentSellResponse PrpoSell(Guid managerId, string items)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.PrpoSell(managerId, items));
        }

        #endregion
        /// <summary>
        /// 媒体礼包兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeId"></param>
        /// <returns></returns>
        public ExchangeResponse Exchange(Guid managerId, string exchangeId, string pf)
        {
            return ResponseHelper.TryCatch(() => ExchangeCore.Instance.Exchange(managerId, exchangeId,pf));
        }
        /// <summary>
        /// gm购买物品
        /// </summary>
        /// <param name="account"></param>
        /// <param name="sourceType"></param>
        /// <param name="cash"></param>
        /// <param name="point"></param>
        /// <param name="bonus"></param>
        /// <param name="orderId"></param>
        /// <param name="eqid"></param>
        /// <returns></returns>
        public MessageCode Charge(string account, int sourceType, int cash, int point, int bonus, string orderId, string eqid)
        {
            return PayCore.Instance.Charge(account, sourceType, cash, point, bonus, orderId);
        }
        /// <summary>
        /// gm加点券
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coin"></param>
        /// <returns></returns>
        public MessageCode AddCoin(Guid managerId, int coin)
        {
            return AdminCore.Instance.AddCoin(managerId, coin);
        }

        /// <summary>
        /// 球员升星参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public UpgradeTheStarParamResponse PlayerUpgradeTheStarParam(Guid managerId, Guid itemId)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.PlayerUpgradeTheStarParam(managerId, itemId));
        }

        /// <summary>
        /// 球员升星
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public UpgradeTheStarResponse PlayerUpgradeTheStar(Guid managerId, Guid itemId)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.PlayerUpgradeTheStar(managerId, itemId));
        }

        /// <summary>
        /// 重置潜力参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="lockString"></param>
        /// <returns></returns>
        public ResetPotentialParamResponse ResetPotentialParam(Guid managerId, Guid itemId, string lockString)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.ResetPotentialParam(managerId, itemId, lockString));
        }

        /// <summary>
        /// 重置潜力
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="lockString"></param>
        /// <returns></returns>
        public ResetPotentialResponse ResetPotential(Guid managerId, Guid itemId, string lockString)
        {
            return ResponseHelper.TryCatch(() => PandoraCore.Instance.ResetPotential(managerId, itemId, lockString));
        }
    }
}
