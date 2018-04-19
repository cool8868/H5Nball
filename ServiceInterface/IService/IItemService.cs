using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;

using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Item;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IItemService
    {
        /// <summary>
        /// 获取背包信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        ItemPackageResponse GetPackage(Guid managerId);

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
        [OperationContract]
        MessageCode GMAddItem(string zoneId, Guid managerId, int itemCode, int itemCount, int strength,
            bool isBinding, int slotColorCount = 0);
        

        /// <summary>
        /// 整理背包
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        ItemPackageDataResponse Arrange(Guid managerId);

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [OperationContract]
        ItemPackageDataResponse DeleteItem(Guid managerId, Guid itemId, int count);

        /// <summary>
        /// 拆分物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="splitCount"></param>
        /// <returns></returns>
        [OperationContract]
        ItemPackageDataResponse SplitItem(Guid managerId, Guid itemId, int splitCount);

        /// <summary>
        /// 强化球员卡参数
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="itemId1">物品ID</param>
        /// <param name="itemId2">物品ID</param>
        /// <returns></returns>
        [OperationContract]
        StrengthParamResponse StrengthenParam(Guid managerId, Guid itemId1, Guid itemId2);

        /// <summary>
        /// 强化球员卡
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="itemId1">物品ID1</param>
        /// <param name="itemId2">物品ID2</param>
        /// <param name="isProtect">是否保护</param>
        /// <param name="protectId">保护膜物品ID</param>
        /// <returns></returns>
        [OperationContract]
        StrengthResponse Strengthen(Guid managerId, Guid itemId1, Guid itemId2, bool isProtect, Guid protectId);

        /// <summary>
        /// 分解球员卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemIds"></param>
        /// <returns></returns>
        [OperationContract]
        DecomposeResponse Decompose(Guid managerId, string itemIds);

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
        [OperationContract]
        SynthesisResponse SynthesisNew(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5, bool isProtect, Guid luckyId, Guid goldFormulaId, bool hasTask);

        /// <summary>
        /// 球员卡合成参数(新)
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <param name="isGoldFormula"></param>
        /// <returns></returns>
        [OperationContract]
        SynthesisParamResponse SynthesisParamNew(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3,
            Guid itemId4, Guid itemId5, bool isGoldFormula);


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
        [OperationContract]
        SynthesisParamResponse TheContractSyntheticParam(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5);

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
        [OperationContract]
        SynthesisResponse TheContractSynthetic(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5);

        /// <summary>
        ///  合成装备参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <returns></returns>
        [OperationContract]
        EquipmentSynthesisParamResponse EquipmentSynthesisParam(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
           Guid itemId5);

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
        [OperationContract]
        SynthesisResponse EquipmentSynthesis(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5, bool isProtect, Guid protectId);

        /// <summary>
        /// 装备出售
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [OperationContract]
        EquipmentSellResponse EquipmentSell(Guid managerId, Guid itemId);

        /// <summary>
        /// 道具出售
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        [OperationContract]
        EquipmentSellResponse PrpoSell(Guid managerId, string items);

        /// <summary>
        /// 兑换码礼包兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeId"></param>
        /// <returns></returns>
        [OperationContract]
        ExchangeResponse Exchange(Guid managerId, string exchangeId, string pf);
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
        [OperationContract]
        MessageCode Charge(string account, int sourceType, int cash, int point, int bonus, string orderId, string eqid);
        /// <summary>
        /// gm加点券
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coin"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCode AddCoin(Guid managerId, int coin);

        /// <summary>
        /// 球员升星参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [OperationContract]
        UpgradeTheStarParamResponse PlayerUpgradeTheStarParam(Guid managerId, Guid itemId);

        /// <summary>
        /// 球员升星
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [OperationContract]
        UpgradeTheStarResponse PlayerUpgradeTheStar(Guid managerId, Guid itemId);

        /// <summary>
        /// 重置潜力参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="lockString"></param>
        /// <returns></returns>
        [OperationContract]
        ResetPotentialParamResponse ResetPotentialParam(Guid managerId, Guid itemId, string lockString);

        /// <summary>
        /// 重置潜力
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="lockString"></param>
        /// <returns></returns>
        [OperationContract]
        ResetPotentialResponse ResetPotential(Guid managerId, Guid itemId, string lockString);
    }
}
