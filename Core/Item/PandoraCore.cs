using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Core.Teammember;
using Games.NBall.Core.Turntable;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Exceptions;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response.Item;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Item
{
    public class PandoraCore
    {
        readonly int _pandoraStrengthMax;
        readonly int _pandoraDecomposeCritPlus;
        readonly int _pandoraFusogenMallcode;
        private readonly int _goldFormulaRate;
        private readonly int _goldFormulaLib;
        private readonly int _equipmentSynthesisMinEquipment;
        private readonly int __strengthPlayerItemCode;//强化球员卡需要物品CODE
        private readonly int _playerTheStarExp = 20;//球员觉醒每张卡增加的经验
        private readonly int _potentialResetPoint;
        private readonly int _potentialLock1;
        private readonly int _potentialLock2;

        #region .ctor

        public PandoraCore(int p)
        {
            _pandoraStrengthMax = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PandoraStrengthMax);
            _pandoraDecomposeCritPlus = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PandoraDecomposeCritPlus);
            _pandoraFusogenMallcode = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PandoraFusogenMallcode);
            _equipmentSynthesisMinEquipment = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.EquipmentSynthesisMinEquipment);
            _goldFormulaRate = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.GoldFormulaRate);
            _goldFormulaLib = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.GoldFormulaLib);
            __strengthPlayerItemCode = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.StrengthPlayerItemCode); 
            _potentialResetPoint = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PotentialResetPoint, 30);
            _potentialLock1 = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PotentialLock1, 60);
            _potentialLock2 = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PotentialLock2, 100);
        }
        #endregion

        #region Facade

        public int PandoraDecomposeCritPlus { get { return _pandoraDecomposeCritPlus; } }

        public int FusogenMallcode { get { return _pandoraFusogenMallcode; } }

        public static PandoraCore Instance
        {
            get { return SingletonFactory<PandoraCore>.SInstance; }
        }

        #endregion 

        #region 强化球员卡

        /// <summary>
        /// 强化球员卡
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="itemId1">物品ID1</param>
        /// <param name="itemId2">物品ID2</param>
        /// <param name="isProtect">是否保护</param>
        /// <param name="protectId">保护膜物品ID</param>
        /// <param name="hasTask">是否有任务</param>
        /// <param name="guideTaskRecordId">引导任务id</param>
        /// <returns></returns>
        public StrengthResponse Strengthen(Guid managerId, Guid itemId1, Guid itemId2, bool isProtect,Guid protectId,bool hasTask=false,
            int guideTaskRecordId=0)
        {
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CardStrength);
            if (package == null)
                return ResponseHelper.InvalidParameter<StrengthResponse>();
            NbManagerextraEntity extra = null;
            string guideBuff = "";
            #region check
            var item1 = package.GetItem(itemId1);
            var item2 = package.GetItem(itemId2);
            int protectCode = 0;
            ItemInfoEntity protectItem = null;
            
            if (isProtect)
            {
                protectItem = package.GetItem(protectId);
                if (protectItem == null)
                {
                    return ResponseHelper.Create<StrengthResponse>(MessageCode.ProtectItemNot);
                }
                protectCode = protectItem.ItemCode;
            }
            //强化参数

            var param = StrengthenCheck(item1, item2, isProtect, protectCode,ref guideBuff, out extra);

            if (param.Code != ShareUtil.SuccessCode)
            {
                return ResponseHelper.Create<StrengthResponse>(param.Code);
            }
            var strengthParam = param.Data;
            if (strengthParam.IsProtect || !isProtect)
            {
                strengthParam.CostPoint = 0;
            }
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<StrengthResponse>();
            if (strengthParam.CostCoin > 0)
            {
                if (manager.Coin < strengthParam.CostCoin)
                {
                    return ResponseHelper.Create<StrengthResponse>(MessageCode.NbCoinShortage);
                }
            }
            #endregion

            if (strengthParam.IsProtect)
                isProtect = true;
            int strength1 = item1.GetStrength();
            int strength2 = item2.GetStrength();

            MessageCode code = MessageCode.NbUpdateFail;
            int resultType = strengthParam.FailType;
            Guid resultItemId = Guid.Empty;
            int resultItemCode = 0;
            int resultStrength = 0;
            //if (strengthParam.ResultStrength > 7)
            //{
            //    strengthParam.RealRate = strengthParam.RealRate * 0.7;
            //    if (strengthParam.RealRate < 1)
            //        strengthParam.RealRate = 1;
            //}
            ItemInfoEntity resultItem = null;

            //如果有强9送卡活动
            if (ActivityExThread.Instance.IsActivity(EnumActivityExEffectType.Strengthen9ReturnCard, 0, 0))
            {
                //原强化成功率低于50%的，成功率后台降低20%
                if (strengthParam.RealRate < 50)
                    strengthParam.RealRate = strengthParam.RealRate - (strengthParam.RealRate*20) / 100;
            }
            if (RandomHelper.CheckPercentage(strengthParam.RealRate))
            {
                ItemInfoEntity roleItem = null;
                if (item1.ItemType == (int)EnumItemType.MallItem)
                {
                    resultItem = item2;
                    roleItem = item1;
                }
                else
                {
                    resultItem = item1;
                    roleItem = item2;
                }
                if (RandomHelper.CheckPercentage(strengthParam.UpgradeRate))
                {
                    if (strengthParam.ResultStrength < 9)
                        strengthParam.ResultStrength ++;
                }
                code = package.Delete(roleItem);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<StrengthResponse>(code);
                item1.UpdateStrength(strengthParam.ResultStrength);
                code = package.Update(item1);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<StrengthResponse>(code);
                resultType = (int)EnumPandoraResultType.Success;
                resultItemId = resultItem.ItemId;
                resultItemCode = resultItem.ItemCode;
                resultStrength = strengthParam.ResultStrength;
            }
            else
            {
                if (strengthParam.FailType == (int)EnumPandoraResultType.Downgrade)
                {
                    code = StrengthenItemDowngrade(item1, package);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<StrengthResponse>(code);
                    code = StrengthenItemDowngrade(item2, package);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<StrengthResponse>(code);
                    strengthParam.ResultStrength -= 2;
                }
                else
                {
                    if (item1.ItemType == (int) EnumItemType.MallItem)
                    {
                        code = package.Delete(item1);
                        if (code != MessageCode.Success)
                            return ResponseHelper.Create<StrengthResponse>(code);
                    }
                    else if (item2.ItemType == (int) EnumItemType.MallItem)
                    {
                        code = package.Delete(item2);
                        if (code != MessageCode.Success)
                            return ResponseHelper.Create<StrengthResponse>(code);
                    }
                    strengthParam.ResultStrength--;
                }
            }
            if (protectItem != null)
            {
                code = package.Delete(protectItem);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<StrengthResponse>(code);
            }

            package.Shadow.AddShadow(item1.ItemId, item1.ItemCode, strength1, item2.ItemId, item2.ItemCode, strength2, isProtect, strengthParam.CostCoin, strengthParam.CostPoint, resultType, resultItemId, resultItemCode, resultStrength
                ,  Guid.Empty , 0, strengthParam.Rate);
            if (extra != null)
            {
                extra.GuideBuffRecord = extra.GuideBuffRecord + guideBuff;
            }

            var isMain = false;
            ArenaTeammemberFrame arenaFrame = null;
            TeammemberEntity teammember = null;
            //主力球员卡强化
            var cardProperty = item1.ItemProperty as PlayerCardProperty;
            item1.IsDeal = false;
            if (cardProperty != null && cardProperty.IsMain)
            {
                if (cardProperty.MainType == 0)
                {
                    isMain = true;
                    //获取阵形
                    teammember = TeammemberCore.Instance.GetTeammember(managerId, cardProperty.TeammemberId);
                    if (teammember == null)
                    {
                        arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType) cardProperty.MainType);
                        if (arenaFrame.TeammebmerDic != null &&
                            arenaFrame.TeammebmerDic.ContainsKey(cardProperty.TeammemberId))
                        {
                            var arenaTeammember = arenaFrame.GetTeammember(cardProperty.TeammemberId);
                            arenaTeammember.UsePlayer = cardProperty;
                        }
                        else
                            arenaFrame = null;
                    }
                    else
                    {
                        var usingPlayerCard = new PlayerCardUsedEntity(item1);
                        teammember.UsedPlayerCard = SerializationHelper.ToByte(usingPlayerCard);
                    }
                }
                else
                {
                    arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType)cardProperty.MainType);
                    if (arenaFrame.TeammebmerDic != null &&
                        arenaFrame.TeammebmerDic.ContainsKey(cardProperty.TeammemberId))
                    {
                        var arenaTeammember = arenaFrame.GetTeammember(cardProperty.TeammemberId);
                        arenaTeammember.UsePlayer = cardProperty;
                    }
                    else
                        arenaFrame = null;
                }
                code = MallCore.Instance.Pandora(managerId, package, manager, null, extra, strengthParam.CostCoin, strengthParam.CostPoint, false, teammember);
            }
            else
                code = MallCore.Instance.Pandora(managerId, package, manager, null, extra, strengthParam.CostCoin, strengthParam.CostPoint);

            if (arenaFrame != null)
                arenaFrame.Save();
            if (code != MessageCode.Success)
                return ResponseHelper.Create<StrengthResponse>(code);
            else
            {
                if (cardProperty.MainType == 0)
                    MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, true);
                else
                    MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId,arenaFrame.ArenaType);
                var managerkpi = ManagerCore.Instance.GetManager(managerId, true);
                var response = ResponseHelper.CreateSuccess<StrengthResponse>();

                if (resultType != (int) EnumPandoraResultType.Success)//强化返百搭
                {
                    if (ActivityExThread.Instance.StrengthenReturnCard())
                    {
                        var player = CacheFactory.PlayersdicCache.GetPlayer(item1.ItemCode % 100000);
                        if (player != null)
                        {
                            switch (player.KpiLevel)
                            {
                                case "A":
                                case "A+":
                                case "S":
                                case "S+":
                                    var mail = new MailBuilder(managerId, "强化返百搭");
                                    mail.AddAttachment(1, 310101, false, 1);
                                    mail.Save();
                                    break;
                            }
                        }
                    }
                }
                if (resultItem != null && cardProperty.Strength == 9)
                    ActivityExThread.Instance.Strengthen9ReturnCard(managerId, resultItem.ItemCode);

                response.Data = new StrengthEntity();
                if (managerkpi != null)
                    response.Data.Kpi = managerkpi.Kpi;
                else
                    response.Data.Kpi = -1;
                response.Data.ResultStrength = strengthParam.ResultStrength;
                response.Data.FailType = resultType;
                response.Data.Coin = manager.Coin;
                response.Data.Package = ItemCore.Instance.BuildPackageData(package);
                response.Data.Point = -1;
                if (strengthParam.CostCoin > 0)
                {
                    ShadowMgr.SaveCoinConsume(managerId, strengthParam.CostCoin, EnumCoinConsumeSourceType.Strength,
                                              package.Shadow.TransactionId.ToString());
                }
                response.Data.PopMsg = TaskHandler.Instance.PandoraStrength(managerId, strengthParam.CardLevel, strengthParam.ResultStrength);

                return response;
            }
        }

        /// <summary>
        /// 强化参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <returns></returns>
        public StrengthParamResponse StrengthenParam(Guid managerId, Guid itemId1, Guid itemId2)
        {
            var package = ItemCore.Instance.GetPackageWithoutShadow(managerId);
            if (package == null)
                return ResponseHelper.InvalidParameter<StrengthParamResponse>();
            var item1 = package.GetItem(itemId1);
            var item2 = package.GetItem(itemId2);
            NbManagerextraEntity extra = null;
            string guideBuff = "";
            return StrengthenCheck(item1, item2, false,0, ref guideBuff, out extra);
        }

        #endregion

        #region 分解球员卡

        public DecomposeResponse Decompose(Guid managerId, string itemIds,bool hasTask=false)
        {
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CardDecompound);
            int coin = 0;
            string itemCodes = "";
            string equipmentItemList ="";
            #region check
            if (package == null)
                return ResponseHelper.InvalidParameter<DecomposeResponse>();

            var ss = itemIds.Split(',');
            int itemCount = 0;
            bool isCrit = false;
            //暴击
            var manager = ManagerCore.Instance.GetManager(managerId);
            int curCoin = 0;
            List<int> cardLevelList = new List<int>();
            foreach (var s in ss)
            {
                Guid itemId = Guid.Empty;
                Guid.TryParse(s, out itemId);
                if (itemId == Guid.Empty)
                    return ResponseHelper.InvalidParameter<DecomposeResponse>();
                var item = package.GetItem(itemId);
                if (item == null)
                    return ResponseHelper.InvalidParameter<DecomposeResponse>();
                //主力球员卡不可分解
                var cardProperty = item.ItemProperty as PlayerCardProperty;
                if (cardProperty != null && cardProperty.IsMain)
                    return ResponseHelper.Create<DecomposeResponse>(MessageCode.TeammemberCantDecompose);
                //检查球员卡训练状态
                package.CheckPlayerTrain();
                if (cardProperty != null && cardProperty.IsTrain)
                    return ResponseHelper.Create<DecomposeResponse>(MessageCode.TeammemberTraining);

                var itemCache = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item.ItemCode);
                if (itemCache == null)
                    return ResponseHelper.InvalidParameter<DecomposeResponse>();
                var config = CacheFactory.PandoraCache.GetDecomposeConfig(itemCache.CardLevel);
                if (config == null)
                    return ResponseHelper.Create<DecomposeResponse>(MessageCode.ItemDecomposeNoConfig);
                cardLevelList.Add(config.CardLevel);
                curCoin = config.Coin;
                int equipmentItem = -1;
                if (config.CritiRate > 0)
                {
                    if (RandomHelper.CheckPercentage(config.CritiRate)) //满足暴击概率
                    {
                        curCoin = curCoin * _pandoraDecomposeCritPlus / 100;
                        isCrit = true;
                    }
                    else//没有暴击则按概率获得装备或金币
                    {
                        if (RandomHelper.CheckPercentage(config.EquipmentRate)) //满足获得装备概率
                        {
                            var equipmentLottery = LotteryCache.Instance.Lottery(EnumLotteryType.DailyCup, config.CardLevel);
                            equipmentItem = equipmentLottery.PrizeItemCode;
                            equipmentItemList += equipmentLottery.PrizeItemCode + ",";
                            curCoin = 0;
                        }
                    }
                }

                var result = package.Delete(item);
                if (result != MessageCode.Success)
                    return ResponseHelper.Create<DecomposeResponse>(result);
                if (equipmentItem != -1)
                {
                    result = package.AddItem(equipmentItem);
                     if (result != MessageCode.Success)
                         return ResponseHelper.Create<DecomposeResponse>(result);
                }

                itemCodes = item.ItemCode + ",";
                coin += curCoin;
                itemCount++;
            }
            if (itemCount == 0)
                return ResponseHelper.InvalidParameter<DecomposeResponse>();
            #endregion

            itemCodes = itemCodes.TrimEnd(',');
            equipmentItemList = equipmentItemList.TrimEnd(',');

            package.Shadow.AddShadow(itemIds, itemCodes, 5, isCrit, coin, equipmentItemList);
            var code = SaveDecompose(package, manager, coin);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<DecomposeResponse>(code);
            }
            else
            {
                package.Shadow.Save();
                var response = ResponseHelper.CreateSuccess<DecomposeResponse>();
                response.Data = new DecomposeEntity
                {
                    IsCrit = isCrit,
                    Coin = coin,
                    EquipmentId = equipmentItemList,
                    ManagerCoin = manager.Coin,
                    Package = ItemCore.Instance.BuildPackageData(package)
                };
                foreach (var item in cardLevelList)
                { 
                    //奥运金牌掉落
                    if (response.Data.OlympicTheGoldMedalId == 0)
                    {
                        switch (item)
                        {
                            case (int)EnumPlayerCardLevel.Purple:
                                response.Data.OlympicTheGoldMedalId = OlympicCore.Instance.GetOlympicTheGoldMedal(
                                    managerId, EnumOlympicGeyType.FriendMatch);
                                break;
                            case (int)EnumPlayerCardLevel.BlackGold:
                            case (int)EnumPlayerCardLevel.Euro:
                            case (int)EnumPlayerCardLevel.Gold:
                            case (int)EnumPlayerCardLevel.Orange:
                            case (int)EnumPlayerCardLevel.Silver:
                                response.Data.OlympicTheGoldMedalId = OlympicCore.Instance.GetOlympicTheGoldMedal(
                                    managerId, EnumOlympicGeyType.FriendMatch);
                                break;
                        }
                    }
                }
                //if (hasTask)
                //{
                    response.Data.PopMsg = TaskHandler.Instance.PandoraDecompose(managerId, itemCount);
                //}
                return response;
            }
        }
        #endregion

        #region 合成球员卡
        /// <summary>
        /// 合成球员卡
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
        public SynthesisResponse Synthesis(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4, Guid itemId5, bool isProtect, Guid protectId)
        {
            if (!CheckSysnthesisItems(itemId1, itemId2, itemId3, itemId4, itemId5))
                return ResponseHelper.InvalidParameter<SynthesisResponse>();

            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CardSynthesize);

            #region check
            if (package == null)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();
            var item1 = package.GetItem(itemId1);
            var item2 = package.GetItem(itemId2);
            var item3 = package.GetItem(itemId3);
            var item4 = package.GetItem(itemId4);
            var item5 = package.GetItem(itemId5);
            if (item1 == null || item2 == null || item3 == null || item4 == null || item5 == null
                || item1.ItemType != (int)EnumItemType.PlayerCard
                || item2.ItemType != (int)EnumItemType.PlayerCard
                || item3.ItemType != (int)EnumItemType.PlayerCard
                || item4.ItemType != (int)EnumItemType.PlayerCard
                || item5.ItemType != (int)EnumItemType.PlayerCard)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();

            var itemCache1 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item1.ItemCode);
            var itemCache2 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item2.ItemCode);
            var itemCache3 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item3.ItemCode);
            var itemCache4 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item4.ItemCode);
            var itemCache5 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item5.ItemCode);
            if (itemCache1 == null || itemCache2 == null || itemCache3 == null || itemCache4 == null || itemCache5 == null)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();
            ItemInfoEntity protectItem = null;
            int cardLevel = itemCache1.CardLevel;
            if (isProtect)
            {
                protectItem = package.GetItem(protectId);
                if (protectItem == null)
                {
                    return ResponseHelper.InvalidParameter<SynthesisResponse>();
                }
                var mallDic = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(protectItem.ItemCode);
                if (mallDic == null || mallDic.EffectType != (int)EnumMallEffectType.ProtectSynthesis)
                {
                    return ResponseHelper.InvalidParameter<SynthesisResponse>();
                }
            }

            if (itemCache1.CardLevel != itemCache2.CardLevel
                || itemCache1.CardLevel != itemCache3.CardLevel
                || itemCache1.CardLevel != itemCache4.CardLevel
                || itemCache1.CardLevel != itemCache5.CardLevel)
                return ResponseHelper.Create<SynthesisResponse>(MessageCode.ItemSynthesisDiffCardlevel);
            
            NbManagerEntity manager = ManagerCore.Instance.GetManager(managerId);
            //===============5张相同橙卡，判断是否可以合成黑金（比如5张橙卡梅西，可以合成黑金梅西）=========================
            if (itemCache1.Idx == itemCache2.Idx
                && itemCache1.Idx == itemCache3.Idx
                && itemCache1.Idx == itemCache4.Idx
                && itemCache1.Idx == itemCache5.Idx)
            {
                int darkPlayerId = CacheFactory.PlayersdicCache.GetLinkPlayer(itemCache1.Idx);
                if (darkPlayerId > 0 && darkPlayerId < 90000 && itemCache1.CardLevel < 9)
                {
                    int darkIdToItemId = CacheFactory.ItemsdicCache.GetItemByType(darkPlayerId, EnumItemType.PlayerCard).ItemCode;
                    return doSynthesisDark(managerId, manager.Name, EnumSynthesisType.PlayerCardSynthesis, darkIdToItemId, package, 0, item1, item2, item3, item4, item5);
                }
            }
            //==============================================================================================================

            var config = CacheFactory.PandoraCache.GetSynthesisConfig(cardLevel, isProtect);
            if (config == null)
                return ResponseHelper.Create<SynthesisResponse>(MessageCode.ItemSynthesisNoConfig);
            int costCoin = config.Coin;
            int costPoint = 0;
            if (costCoin > 0)
            {
                if (manager.Coin < costCoin)
                {
                    return ResponseHelper.Create<SynthesisResponse>(MessageCode.NbCoinShortage);
                }
            }

            #endregion
            var rate = config.Rate / 100.00;
            var cardLib = config.CardLibrary;
           
            var vipRate = GetVipAddSynthesisRate(manager);
            int newItemCode = 0;
            var totalRate = CalRatePlus(rate, vipRate);
            if (RandomHelper.CheckPercentage(totalRate))
            {
                newItemCode = CacheFactory.LotteryCache.LotteryByLib(cardLib);
            }
            var response = doSynthesis(EnumSynthesisType.PlayerCardSynthesis, totalRate, isProtect, config.ProtectCode, newItemCode, package, costCoin, item1, item2, item3, item4, item5, protectItem, null);
            return response;
        }
        /// <summary>
        /// 黑金卡合成
        /// </summary>
        private SynthesisResponse doSynthesisDark(Guid mid, string name, EnumSynthesisType synthesisType,
                                              int newItemCode, ItemPackageFrame package, int costCoin,
                                              ItemInfoEntity item1, ItemInfoEntity item2, ItemInfoEntity item3,
                                              ItemInfoEntity item4, ItemInfoEntity item5)
        {
            var response = doSynthesis(EnumSynthesisType.PlayerCardSynthesis, 100, false, 0, newItemCode, package, costCoin, item1, item2, item3, item4, item5, null, null);
            if (response.Code == ShareUtil.SuccessCode)
            {
                int newCardLevel = 0;
                if (newItemCode > 0)
                {
                    var newItemDic = CacheFactory.ItemsdicCache.GetItem(newItemCode);
                    if (newItemDic != null)
                    {
                        newCardLevel = newItemDic.PlayerCardLevel;
                    }
                }
            }
            return response;
        }

        public SynthesisParamResponse SynthesisParam(Guid managerId, int cardLevel)
        {
            var config = CacheFactory.PandoraCache.GetSynthesisConfig(cardLevel);
            if (config == null)
                return ResponseHelper.Create<SynthesisParamResponse>(MessageCode.ItemSynthesisNoConfig);
            var vipRate = GetVipAddSynthesisRate(managerId);
            var rate = config.Rate / 100.00;
            var totalRate = CalRatePlus(rate, vipRate);
            var costPoint = CacheFactory.MallCache.GetCostPoint(config.ProtectCode, DateTime.Now);
            var response = ResponseHelper.CreateSuccess<SynthesisParamResponse>();
            response.Data = new SynthesisParamEntity();
            response.Data.CostPoint = costPoint;
            response.Data.CostCoin = config.Coin;
            response.Data.Rate = totalRate;
            return response;
        }


        public SynthesisParamResponse SynthesisParamNew(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4, Guid itemId5, bool isGoldFormula)
        {

            if (!CheckSysnthesisItems(itemId1, itemId2, itemId3, itemId4, itemId5))
                return ResponseHelper.InvalidParameter<SynthesisParamResponse>();
            
                int showRate = 0;
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CardSynthesize);
            var usedPlayerList = new List<DicPlayerEntity>(5);//合成使用的5个球员信息
            #region check
            if (package == null)
                return ResponseHelper.InvalidParameter<SynthesisParamResponse>();
            var item1 = package.GetItem(itemId1);
            var item2 = package.GetItem(itemId2);
            var item3 = package.GetItem(itemId3);
            var item4 = package.GetItem(itemId4);
            var item5 = package.GetItem(itemId5);
            if (item1 == null || item2 == null || item3 == null || item4 == null || item5 == null
                || item1.ItemType != (int)EnumItemType.PlayerCard
                || item2.ItemType != (int)EnumItemType.PlayerCard
                || item3.ItemType != (int)EnumItemType.PlayerCard
                || item4.ItemType != (int)EnumItemType.PlayerCard
                || item5.ItemType != (int)EnumItemType.PlayerCard)
                return ResponseHelper.InvalidParameter<SynthesisParamResponse>();

            var itemProperty1 = item1.ItemProperty as PlayerCardProperty;
            var itemProperty2 = item2.ItemProperty as PlayerCardProperty;
            var itemProperty3 = item3.ItemProperty as PlayerCardProperty;
            var itemProperty4 = item4.ItemProperty as PlayerCardProperty;
            var itemProperty5 = item5.ItemProperty as PlayerCardProperty;
            //检查球员卡训练状态
            package.CheckPlayerTrain();
            if (itemProperty1 == null || itemProperty2 == null || itemProperty3 == null || itemProperty4 == null || itemProperty5 == null
                || itemProperty1.IsMain || itemProperty1.IsTrain
                || itemProperty2.IsMain || itemProperty2.IsTrain
                || itemProperty3.IsMain || itemProperty3.IsTrain
                || itemProperty4.IsMain || itemProperty4.IsTrain
                || itemProperty5.IsMain || itemProperty5.IsTrain)
                return ResponseHelper.Create<SynthesisParamResponse>(MessageCode.MainPlayerCannotSynthesis);
            

            var itemCache1 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item1.ItemCode);
            var itemCache2 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item2.ItemCode);
            var itemCache3 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item3.ItemCode);
            var itemCache4 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item4.ItemCode);
            var itemCache5 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item5.ItemCode);
            if (itemCache1 == null || itemCache2 == null || itemCache3 == null || itemCache4 == null || itemCache5 == null)
                return ResponseHelper.InvalidParameter<SynthesisParamResponse>();

            bool isEuro = false;
            //如果有欧冠卡则只能全为欧冠卡才能合成
            if (itemCache1.CardLevel == (int)EnumPlayerCardLevel.Euro
                || itemCache2.CardLevel == (int)EnumPlayerCardLevel.Euro
                || itemCache3.CardLevel == (int)EnumPlayerCardLevel.Euro
                || itemCache4.CardLevel == (int)EnumPlayerCardLevel.Euro
                || itemCache5.CardLevel == (int)EnumPlayerCardLevel.Euro)
            {
                isEuro = true;

                if (!(itemCache1.CardLevel == (int)EnumPlayerCardLevel.Euro
                      && itemCache2.CardLevel == (int)EnumPlayerCardLevel.Euro
                      && itemCache3.CardLevel == (int)EnumPlayerCardLevel.Euro
                      && itemCache4.CardLevel == (int)EnumPlayerCardLevel.Euro
                      && itemCache5.CardLevel == (int)EnumPlayerCardLevel.Euro))
                    return ResponseHelper.Create<SynthesisParamResponse>(MessageCode.EuroCardOnly);
            }
            //暂不开放传奇和元老卡
            if (itemCache1.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache1.CardLevel == (int)EnumPlayerCardLevel.Silver
                || itemCache2.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache2.CardLevel == (int)EnumPlayerCardLevel.Silver
                || itemCache3.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache3.CardLevel == (int)EnumPlayerCardLevel.Silver
                || itemCache4.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache4.CardLevel == (int)EnumPlayerCardLevel.Silver
                || itemCache5.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache5.CardLevel == (int)EnumPlayerCardLevel.Silver)
                return ResponseHelper.Create<SynthesisParamResponse>(MessageCode.SilverCardCannotSynthesis);

            #endregion
            //放入的球员列表
            usedPlayerList.Add(itemCache1);
            usedPlayerList.Add(itemCache2);
            usedPlayerList.Add(itemCache3);
            usedPlayerList.Add(itemCache4);
            usedPlayerList.Add(itemCache5);

            //计算Kpi
            double allKpi = 0;
            double minKpi = 0;
            double maxKpi = 0;

            double playersMinKpi = usedPlayerList[0].Capacity;
            foreach (var playerEntity in usedPlayerList)
            {
                allKpi += playerEntity.Capacity;
                if (playerEntity.Capacity < playersMinKpi)
                    playersMinKpi = playerEntity.Capacity;
            }
            minKpi = allKpi / 4.5 - 15;
            maxKpi = allKpi / 4.5 -5;

            var coin = 0;
            var point = 0;
            var totalRate = 0;
            if (isGoldFormula)//合成金卡
                totalRate = _goldFormulaRate;
            CacheFactory.PandoraCache.GetSynthesisParam(usedPlayerList, out coin, out point);//获取消耗点数

            List<int> playCardList = CacheFactory.PlayersdicCache.GetSynthesisResult(minKpi, maxKpi, isEuro);//根据KPI抽取
            if (playCardList.Count > 0)
            {
                bool notAllInSource = false;
                foreach (var card in playCardList)
                {
                    if (card != item1.ItemCode && card != item2.ItemCode && card != item3.ItemCode &&
                        card != item4.ItemCode && card != item5.ItemCode)
                    {
                        notAllInSource = true;
                        break;
                    }
                }
                if (notAllInSource) //如果不是所有的都是合成使用的卡，则去除合成用卡
                {
                    if (playCardList.Contains(item1.ItemCode))
                        playCardList.Remove(item1.ItemCode);
                    if (playCardList.Contains(item2.ItemCode))
                        playCardList.Remove(item2.ItemCode);
                    if (playCardList.Contains(item3.ItemCode))
                        playCardList.Remove(item3.ItemCode);
                    if (playCardList.Contains(item4.ItemCode))
                        playCardList.Remove(item4.ItemCode);
                    if (playCardList.Contains(item5.ItemCode))
                        playCardList.Remove(item5.ItemCode);
                }
                //移除所有比最小KPI还小的卡
                if (totalRate != _goldFormulaRate)
                {
                    int sumNumber = playCardList.Count;
                    int exclude = 0;
                    foreach (var playerCard in playCardList)
                    {
                        var player = CacheFactory.ItemsdicCache.GetPlayerByItemCode(playerCard);
                        if (player.Capacity >= playersMinKpi) //低于使用的卡中kpi最低的则失败
                            exclude++;
                    }
                    if (exclude > 0)
                        showRate = exclude * 100/sumNumber;
                }

            }
            var response = ResponseHelper.CreateSuccess<SynthesisParamResponse>();
            response.Data = new SynthesisParamEntity
            {
                CostPoint = point,
                CostCoin = coin,
                Rate = totalRate,
                PlayCardList = playCardList,
                ShowRate =showRate >0 ? showRate :totalRate,
                ShowRandomGoldCard = isGoldFormula
            };
            return response;
        }

        //球员卡合成（新）
        public SynthesisResponse SynthesisNew(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4, Guid itemId5, bool isProtect, Guid luckyId, Guid goldFormulaId, bool hasTask)
        {
            if (!CheckSysnthesisItems(itemId1, itemId2, itemId3, itemId4, itemId5))
                return ResponseHelper.InvalidParameter<SynthesisResponse>();

            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CardSynthesize);
            var usedPlayerList = new List<DicPlayerEntity>(5);
            #region check
            if (package == null)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();
            var item1 = package.GetItem(itemId1);
            var item2 = package.GetItem(itemId2);
            var item3 = package.GetItem(itemId3);
            var item4 = package.GetItem(itemId4);
            var item5 = package.GetItem(itemId5);
            if (item1 == null || item2 == null || item3 == null || item4 == null || item5 == null
                || item1.ItemType != (int)EnumItemType.PlayerCard
                || item2.ItemType != (int)EnumItemType.PlayerCard
                || item3.ItemType != (int)EnumItemType.PlayerCard
                || item4.ItemType != (int)EnumItemType.PlayerCard
                || item5.ItemType != (int)EnumItemType.PlayerCard)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();

            var itemProperty1 = item1.ItemProperty as PlayerCardProperty;
            var itemProperty2 = item2.ItemProperty as PlayerCardProperty;
            var itemProperty3 = item3.ItemProperty as PlayerCardProperty;
            var itemProperty4 = item4.ItemProperty as PlayerCardProperty;
            var itemProperty5 = item5.ItemProperty as PlayerCardProperty;

            if (itemProperty1 == null || itemProperty2 == null || itemProperty3 == null || itemProperty4 == null ||
                itemProperty5 == null
                || itemProperty1.IsMain
                || itemProperty2.IsMain
                || itemProperty3.IsMain
                || itemProperty4.IsMain
                || itemProperty5.IsMain)
                return ResponseHelper.Create<SynthesisResponse>(MessageCode.MainPlayerCannotSynthesis);

            var itemCache1 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item1.ItemCode);
            var itemCache2 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item2.ItemCode);
            var itemCache3 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item3.ItemCode);
            var itemCache4 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item4.ItemCode);
            var itemCache5 = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item5.ItemCode);
            if (itemCache1 == null || itemCache2 == null || itemCache3 == null || itemCache4 == null || itemCache5 == null)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();
            bool isEuro = false;
            //如果有欧冠卡则只能全为欧冠卡才能合成
            if (itemCache1.CardLevel == (int)EnumPlayerCardLevel.Euro
                || itemCache2.CardLevel == (int)EnumPlayerCardLevel.Euro
                || itemCache3.CardLevel == (int)EnumPlayerCardLevel.Euro
                || itemCache4.CardLevel == (int)EnumPlayerCardLevel.Euro
                || itemCache5.CardLevel == (int)EnumPlayerCardLevel.Euro)
            {
                isEuro = true;
                if (!(itemCache1.CardLevel == (int)EnumPlayerCardLevel.Euro
                      && itemCache2.CardLevel == (int)EnumPlayerCardLevel.Euro
                      && itemCache3.CardLevel == (int)EnumPlayerCardLevel.Euro
                      && itemCache4.CardLevel == (int)EnumPlayerCardLevel.Euro
                      && itemCache5.CardLevel == (int)EnumPlayerCardLevel.Euro))
                    return ResponseHelper.Create<SynthesisResponse>(MessageCode.EuroCardOnly);
            }
            //暂不开放传奇和元老卡
            if (itemCache1.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache1.CardLevel == (int)EnumPlayerCardLevel.Silver
                || itemCache2.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache2.CardLevel == (int)EnumPlayerCardLevel.Silver
                || itemCache3.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache3.CardLevel == (int)EnumPlayerCardLevel.Silver
                || itemCache4.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache4.CardLevel == (int)EnumPlayerCardLevel.Silver
                || itemCache5.CardLevel == (int)EnumPlayerCardLevel.Gold || itemCache5.CardLevel == (int)EnumPlayerCardLevel.Silver)
                return ResponseHelper.Create<SynthesisResponse>(MessageCode.SilverCardCannotSynthesis);

            ItemInfoEntity goldFormulaItem = null;
            if (goldFormulaId != Guid.Empty)
            {
                goldFormulaItem = package.GetItem(goldFormulaId);
                if (goldFormulaItem == null)
                {
                    return ResponseHelper.InvalidParameter<SynthesisResponse>();
                }
                var mallDic = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(goldFormulaItem.ItemCode);
                if (mallDic == null || mallDic.EffectType != (int)EnumMallEffectType.GoldFormula)
                {
                    return ResponseHelper.InvalidParameter<SynthesisResponse>();
                }
            }
            //放入的球员卡列表
            usedPlayerList.Add(itemCache1);
            usedPlayerList.Add(itemCache2);
            usedPlayerList.Add(itemCache3);
            usedPlayerList.Add(itemCache4);
            usedPlayerList.Add(itemCache5);
            NbManagerEntity manager = ManagerCore.Instance.GetManager(managerId);

            int costCoin = 0;
            int costPoint = 0;
            CacheFactory.PandoraCache.GetSynthesisParam(usedPlayerList, out costCoin, out costPoint);
            costPoint = 0;//合成保护不消耗钻石

            ItemInfoEntity protectItem = null;

            if (isProtect)
            {
                protectItem = package.GetItem(luckyId);
                if (protectItem == null)
                    return ResponseHelper.Create<SynthesisResponse>(MessageCode.NoProtectCard);
                if(protectItem.ItemCode!=310105)
                    return ResponseHelper.Create<SynthesisResponse>(MessageCode.NotProtectCard);
            }
            

            if (costCoin > 0)
            {
                if (manager.Coin < costCoin)
                {
                    return ResponseHelper.Create<SynthesisResponse>(MessageCode.NbCoinShortage);
                }
            }
            PayUserEntity payUser = null;
            if (costPoint > 0)
            {
                payUser = PayCore.Instance.GetPayUser(managerId);
                if (payUser.TotalPoint < costPoint)
                {
                    return ResponseHelper.Create<SynthesisResponse>(MessageCode.NbPointShortage);
                }
            }

            #endregion

            var rate = 0;
            var cardLib = 0;
            if (goldFormulaItem != null)
            {
                rate = _goldFormulaRate;
                cardLib = _goldFormulaLib;
            }

            var vipRate = GetVipAddSynthesisRate(manager);
            int newItemCode = 0;
            var totalRate = CalRatePlus(rate, vipRate);
            //BuffPlusHelper.SynthesisRateLow50(ref totalRate);
            if (RandomHelper.CheckPercentage(totalRate))//合成金卡
            {
                newItemCode = CacheFactory.LotteryCache.LotteryByLib(cardLib);
            }
            else//按KPI规则合成球员卡
            {
                double allKpi = 0;
                double minKpi = 0;
                double maxKpi = 0;

                double playersMinKpi = usedPlayerList[0].Capacity;
                foreach (var playerEntity in usedPlayerList)
                {
                    allKpi += playerEntity.Capacity;
                    if (playerEntity.Capacity < playersMinKpi)
                        playersMinKpi = playerEntity.Capacity;
                }
                minKpi = allKpi / 4.5 - 15;
                maxKpi = allKpi / 4.5 - 5;

                List<int> playCardList = CacheFactory.PlayersdicCache.GetSynthesisResult(minKpi, maxKpi, isEuro);//根据KPI抽取
                if (playCardList.Count > 0)
                {
                    bool notAllInSource = false;
                    foreach (var card in playCardList)
                    {
                        if (card != item1.ItemCode && card != item2.ItemCode && card != item3.ItemCode &&
                            card != item4.ItemCode && card != item5.ItemCode)
                        {
                            notAllInSource = true;
                            break;
                        }
                    }
                    if (notAllInSource)//如果不是所有的都是合成使用的卡，则去除合成用卡
                    {
                        if (playCardList.Contains(item1.ItemCode))
                            playCardList.Remove(item1.ItemCode);
                        if (playCardList.Contains(item2.ItemCode))
                            playCardList.Remove(item2.ItemCode);
                        if (playCardList.Contains(item3.ItemCode))
                            playCardList.Remove(item3.ItemCode);
                        if (playCardList.Contains(item4.ItemCode))
                            playCardList.Remove(item4.ItemCode);
                        if (playCardList.Contains(item5.ItemCode))
                            playCardList.Remove(item5.ItemCode);
                    }

                    var index = RandomHelper.GetInt32WithoutMax(0, playCardList.Count);
                    newItemCode = playCardList[index];
                    var player = CacheFactory.ItemsdicCache.GetPlayerByItemCode(newItemCode);
                    if (player.Capacity < playersMinKpi)//低于使用的卡中kpi最低的则失败
                        newItemCode = 0;
                }
            }
            var mallCode = 51910;//新版球员卡合成消耗
            var response = doSynthesis(EnumSynthesisType.PlayerCardSynthesis, totalRate, isProtect, mallCode, newItemCode, package, costCoin, item1, item2, item3, item4, item5, protectItem, goldFormulaItem, null, costPoint);
            if (response.Code == ShareUtil.SuccessCode)
            {
                int newCardLevel = 0;
                if (newItemCode > 0)
                {

                    var newItemDic = CacheFactory.ItemsdicCache.GetItem(newItemCode);
                    if (newItemDic != null)
                    {
                        newCardLevel = newItemDic.PlayerCardLevel;
                        if (newItemDic.PlayerCardLevel == (int)EnumPlayerCardLevel.Gold
                            || newItemDic.PlayerCardLevel == (int)EnumPlayerCardLevel.Orange)
                        {
                            //ChatHelper.SendBannerSynthesis(managerId, manager.Name, newItemDic);
                        }
                        //ActivityExThread.Instance.PlayerCardSynthesis(managerId, newCardLevel);
                    }
                }
                if (hasTask)
                {
                    //response.Data.PopMsg = TaskHandler.Instance.PandoraSynthesisPlayer(managerId, newCardLevel);
                }
            }
            return response;
        }
        

        #endregion

        #region 球员升星

        /// <summary>
        /// 球员升星参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public UpgradeTheStarParamResponse PlayerUpgradeTheStarParam(Guid managerId, Guid itemId)
        {
            UpgradeTheStarParamResponse response = new UpgradeTheStarParamResponse();
            response.Data = new UpgradeTheStarParam();
            try
            {
                NbManagerEntity manager = null;
                ItemPackageFrame package = null;
                List<ItemInfoEntity> itemList = null;
                ItemInfoEntity deleteItem = null;
                ItemInfoEntity item = null;
                ConfigPlayerthestarEntity theStarConfig = null;
                int isPromptTheStart = 0;
                bool isTrain = false;
                var messageCode = UpgradeTheStarCheck(managerId, itemId, ref manager, ref package, ref itemList, ref theStarConfig,ref item,ref deleteItem,ref isTrain,ref isPromptTheStart,true);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<UpgradeTheStarParamResponse>((int) messageCode);
                response.Data.IsPromptStrengthen = 0;
                if (deleteItem != null && deleteItem.GetStrength() >1)
                    response.Data.IsPromptStrengthen = deleteItem.GetStrength();
                response.Data.IsPromptTheStart = isPromptTheStart;

                var itemProperty = item.ItemProperty as PlayerCardProperty;
                if (itemProperty == null)
                    return ResponseHelper.Create<UpgradeTheStarParamResponse>((int)MessageCode.NbParameterError);

              
                int addExp = 0;
                if (isPromptTheStart > 0)
                {
                    var deletePropery = deleteItem.ItemProperty as PlayerCardProperty;
                    if (deletePropery != null && deletePropery.TheStar > 0)
                    {
                        switch (deletePropery.TheStar)
                        {
                            case 1:
                                addExp += _playerTheStarExp*1;
                                break;
                            case 2:
                                addExp += _playerTheStarExp*3;
                                break;
                            case 3:
                                addExp += _playerTheStarExp*6;
                                break;
                            case 4:
                                addExp += _playerTheStarExp*10;
                                break;
                            case 5:
                                addExp += _playerTheStarExp*15;
                                break;
                        }
                        addExp += deletePropery.TheStarExp;
                    }
                }
                int forIndex = 0;
                forIndex = addExp / _playerTheStarExp;
                bool isUpgrade = false;
                int costCoin = 0;
                for (int i = 1; i <= forIndex; i++)
                {
                    itemProperty.TheStarExp += _playerTheStarExp;
                    PlayerStarExpCalculate(ref theStarConfig, ref itemProperty, ref isUpgrade);
                    costCoin += theStarConfig.Coin;
                    if (itemProperty.TheStar == 5)
                        break;
                }

                int exp = _playerTheStarExp;
                //有活动
                if (ActivityExThread.Instance.UpgradeTheStarDlouble())
                    exp = _playerTheStarExp * 2;
                itemProperty.TheStarExp += exp;
                PlayerStarExpCalculate(ref theStarConfig, ref itemProperty, ref isUpgrade);
                costCoin += theStarConfig.Coin;
                response.Data.CostCoin = costCoin;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("球员升星参数", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 球员升星验证
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="manager"></param>
        /// <param name="package"></param>
        /// <param name="resultList"></param>
        /// <param name="theStarConfig"></param>
        /// <param name="item"></param>
        /// <param name="deleteItem"></param>
        /// <param name="isTrain"></param>
        /// <param name="isPromptTheStart"></param>
        /// <param name="isParam"></param>
        /// <returns></returns>
        MessageCode UpgradeTheStarCheck(Guid managerId, Guid itemId, ref NbManagerEntity manager, ref ItemPackageFrame package, ref List<ItemInfoEntity> resultList, ref ConfigPlayerthestarEntity theStarConfig,ref ItemInfoEntity item, ref ItemInfoEntity deleteItem,ref bool isTrain,ref int isPromptTheStart,bool isParam =false)
        {
            //获取经理信息
            manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return MessageCode.AdMissManager;
            //获取背包
            package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.UpgradeTheStar);
            if (package == null)
               return MessageCode.NbNoPackage;
            //获取物品
            item = package.GetItem(itemId);
            if (item == null)
                return MessageCode.ItemNotExists;
            if (item.ItemType != (int)EnumItemType.PlayerCard)
                return MessageCode.ItemNotUpgradeTheStar;
            //检查球员卡训练状态
            package.CheckPlayerTrain();
            //获取属性
            var property = item.ItemProperty as PlayerCardProperty;
            if (property == null)
                return MessageCode.NbParameterError;
            //获取配置
            theStarConfig = CacheFactory.PlayersdicCache.UpgradeTheStarCoin(property.TheStar + 1);
            if (theStarConfig == null)
                return MessageCode.MaxTheStar;
            if (!isParam)
            {
                if (theStarConfig.Coin > manager.Coin)
                    return MessageCode.NbCoinShortage;
            }
            var itemList = package.GetListByItemCode(item.ItemCode);
            //移除自己
            if (!itemList.Remove(item))
                return MessageCode.NbParameterError;
            resultList = new List<ItemInfoEntity>();
            int minStrength = 0;
            int minLevel = 0;
            int minTheStart = 0;
            int minTheStartExp = 0;
            bool isOne = true;
            deleteItem = null;

            foreach (var itemInfo in itemList)
            {
                var itemproperty = itemInfo.ItemProperty as PlayerCardProperty;
                if (itemproperty == null)
                    continue;
                if (itemInfo.ItemId == item.ItemId)
                    continue;
                if (itemproperty.IsMain)
                    continue;
                resultList.Add(itemInfo);
                if (isOne || itemproperty.TheStar <= minTheStart) 
                {
                    isOne = false;
                    if (itemproperty.TheStar == minTheStart) 
                    {
                        if (itemproperty.TheStarExp > minTheStartExp)
                            continue;
                        minTheStartExp = itemproperty.TheStarExp;
                        if (minStrength == 0 || itemproperty.Strength <= minStrength)
                        {
                            deleteItem = itemInfo;
                            if (itemproperty.IsTrain)
                                isTrain = true;
                            else
                                isTrain = false;
                            if (itemproperty.TheStar > 0)
                                isPromptTheStart = itemproperty.TheStar;
                            else
                                isPromptTheStart = 0;
                            if (itemproperty.Strength == minStrength)
                            {
                                if (minLevel == 0 || itemproperty.Level < minLevel)
                                {
                                    minLevel = itemproperty.Level;
                                    deleteItem = itemInfo; 
                                    if (itemproperty.IsTrain)
                                        isTrain = true;
                                    else
                                        isTrain = false;
                                    if (itemproperty.TheStar > 0)
                                        isPromptTheStart = itemproperty.TheStar;
                                    else
                                        isPromptTheStart = 0;
                                }
                                else
                                    continue;
                            }
                            minStrength = itemproperty.Strength;
                        }
                    }
                    minTheStart = itemproperty.TheStar;
                    deleteItem = itemInfo;
                     if (itemproperty.IsTrain)
                         isTrain = true;
                     else
                         isTrain = false;
                    if (itemproperty.TheStar > 0)
                        isPromptTheStart = itemproperty.TheStar;
                    else
                        isPromptTheStart = 0;
                }
            }
            if (!isParam)
            {
                if (resultList.Count < theStarConfig.PlayerCard || deleteItem == null)
                    return MessageCode.PlayerInsufficient;
            }

            return MessageCode.Success;
        }

        /// <summary>
        /// 球员升星
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public UpgradeTheStarResponse PlayerUpgradeTheStar(Guid managerId, Guid itemId)
        {
            UpgradeTheStarResponse response = new UpgradeTheStarResponse();
            response.Data = new UpgradeTheStar();
            try
            {
                NbManagerEntity manager = null;
                ItemPackageFrame package = null;
                List<ItemInfoEntity> itemList = null;
                ConfigPlayerthestarEntity theStarConfig = null;
                ItemInfoEntity deleteItem = null;
                bool isTrain = false;
                ItemInfoEntity item = null;
                int isPromptTheStart = 0;
                var messageCode = UpgradeTheStarCheck(managerId, itemId, ref manager, ref package, ref itemList, ref theStarConfig, ref item, ref deleteItem, ref isTrain, ref isPromptTheStart);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<UpgradeTheStarResponse>((int)messageCode);
                int costCoin = theStarConfig.Coin;
                Guid trainId = Guid.Empty;
                var property = deleteItem.ItemProperty as PlayerCardProperty;
                if (property != null)
                {
                    if (property.IsTrain)
                    {
                        trainId = property.TeammemberId;
                        property.IsTrain = false;
                        deleteItem.ItemProperty = property;
                    }
                    if (property.Equipment != null)
                    {
                        //装备返还背包
                        var result = package.AddUsedItem(property.Equipment);
                        if (result != MessageCode.Success)
                        {
                            return ResponseHelper.Create<UpgradeTheStarResponse>(result);
                        }
                    }
                }
                item.IsDeal = false;
                var itemProperty = item.ItemProperty as PlayerCardProperty;
                if(itemProperty ==null)
                    return ResponseHelper.Create<UpgradeTheStarResponse>((int)MessageCode.NbParameterError);

                int addExp = 0;
                if (isPromptTheStart > 0)
                {
                    var deletePropery = deleteItem.ItemProperty as PlayerCardProperty;
                    if (deletePropery != null && deletePropery.TheStar > 0)
                    {
                        switch (deletePropery.TheStar)
                        {
                            case 1:
                                addExp += _playerTheStarExp * 1;
                                break;
                            case 2:
                                addExp += _playerTheStarExp * 3;
                                break;
                            case 3:
                                addExp += _playerTheStarExp * 6;
                                break;
                            case 4:
                                addExp += _playerTheStarExp * 10;
                                break;
                            case 5:
                                addExp += _playerTheStarExp * 15;
                                break;
                        }
                        addExp += deletePropery.TheStarExp;
                    }
                }
                //已有经验不翻倍
                int forIndex = 0;
                forIndex = addExp / _playerTheStarExp;
                bool isUpgrade = false;
                costCoin = 0;
                for (int i = 1; i <= forIndex; i++)
                {
                    itemProperty.TheStarExp += _playerTheStarExp;
                    PlayerStarExpCalculate(ref theStarConfig, ref itemProperty, ref isUpgrade);
                    costCoin += theStarConfig.Coin;
                    if (itemProperty.TheStar == 5)
                        break;
                }

                int exp = _playerTheStarExp;
                //有活动
                if (ActivityExThread.Instance.UpgradeTheStarDlouble())
                    exp = _playerTheStarExp * 2;
                itemProperty.TheStarExp += exp;
                PlayerStarExpCalculate(ref theStarConfig, ref itemProperty, ref isUpgrade);
                costCoin += theStarConfig.Coin;
                if (isUpgrade)
                {
                    if (itemProperty.Potential == null)
                        itemProperty.Potential = new List<Potential>();
                    if (itemProperty.Potential.Count < theStarConfig.PotentialCount)
                    {
                        int potentialNumber = theStarConfig.PotentialCount - itemProperty.Potential.Count;
                        for (int i = 0; i < potentialNumber; i++)
                        {
                            var player = CacheFactory.PlayersdicCache.GetPlayer(item.ItemCode%100000);
                            if (player == null)
                                return ResponseHelper.Create<UpgradeTheStarResponse>((int) MessageCode.NbParameterError);
                            var potential = CacheFactory.PlayersdicCache.GetRandomPotential(player.PositionDesc,
                                itemProperty.Potential, 1);
                            if (potential == null)
                                return ResponseHelper.Create<UpgradeTheStarResponse>((int) MessageCode.NbParameterError);
                            itemProperty.Potential.Add( CacheFactory.PlayersdicCache.GetPotentialValue(potential));
                        }
                    }
                }
                item.ItemProperty = itemProperty;
                TeammemberEntity teammember = null;

                ArenaTeammemberFrame arenaFrame = null;
                if (itemProperty.IsMain)
                {
                    if (itemProperty.MainType == 0)
                    {
                        teammember = TeammemberCore.Instance.GetTeammember(managerId, itemProperty.TeammemberId);
                        if (teammember != null)
                        {
                            var usingPlayerCard = new PlayerCardUsedEntity(item);
                            teammember.UsedPlayerCard = SerializationHelper.ToByte(usingPlayerCard);
                        }
                    }
                    else
                    {
                        arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType)itemProperty.MainType);
                        if (arenaFrame.TeammebmerDic != null &&
                            arenaFrame.TeammebmerDic.ContainsKey(itemProperty.TeammemberId))
                        {
                            var arenaTeammember = arenaFrame.GetTeammember(itemProperty.TeammemberId);
                            arenaTeammember.UsePlayer = itemProperty;
                        }
                        else
                            arenaFrame = null;
                    }
                }
                package.Delete(deleteItem);
                package.Update(item);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    MessageCode messCode = MessageCode.NbUpdateFail;
                    do
                    {
                        var code = ManagerCore.Instance.CostCoin(manager, costCoin,
                            EnumCoinConsumeSourceType.PlayerTheStar, ShareUtil.GenerateComb().ToString(),
                            transactionManager.TransactionObject);
                        if (code != MessageCode.Success)
                        {
                            messCode = code;
                            break;
                        } 
                        if (isTrain)
                        {
                            if (!TeammemberTrainMgr.Delete(trainId, transactionManager.TransactionObject))
                                break;
                            PlayerTrain.Instance.RemovetrainDic(trainId, managerId);
                        }
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (arenaFrame != null)
                        {
                            if (!arenaFrame.Save(transactionManager.TransactionObject))
                                break;
                        }
                        messCode = MessageCode.Success;
                    } while (false);
                    if (messCode != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<UpgradeTheStarResponse>((int)messCode);
                    }
                    transactionManager.Commit();
                    package.Shadow.Save();
                    if (teammember != null)
                    {
                            int returnCode = 0;
                            string errorMessage = "";
                            TeammemberMgr.SetUsePlayerCard(teammember.Idx, teammember.ManagerId, manager.Mod,
                           teammember.UsedPlayerCard, ref returnCode, ref errorMessage);
                            if (!string.IsNullOrEmpty(errorMessage))
                            {
                                SystemlogMgr.Error("SaveSetPlayerCard", errorMessage);
                            }
                        if (returnCode == ShareUtil.SuccessCode && isUpgrade)
                        {
                            MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, true);
                            var buff = BuffDataCore.Instance().RebuildMembers(managerId);
                            if (buff != null && buff.Kpi > 0)
                                response.Data.Kpi = buff.Kpi;
                            else
                                response.Data.Kpi = -1;
                        }
                    }
                    if (arenaFrame != null)
                    {
                        MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId,arenaFrame.ArenaType);
                    }
                    response.Data.UpdateItem = new List<ItemInfoEntity>();
                    response.Data.UpdateItem.Add(item);
                    response.Data.DeleteItem = new List<Guid>();
                    response.Data.DeleteItem.Add(deleteItem.ItemId);
                    response.Data.Coin = manager.Coin;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("球员升星", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 球员卡觉醒经验算法
        /// </summary>
        /// <param name="theStarConfig"></param>
        /// <param name="itemProperty"></param>
        /// <param name="isUpgrade"></param>
        private void PlayerStarExpCalculate(ref ConfigPlayerthestarEntity theStarConfig,
            ref PlayerCardProperty itemProperty, ref bool isUpgrade)
        {
            bool isWhile = true;
            int index = 0;
            do
            {
                theStarConfig = CacheFactory.PlayersdicCache.UpgradeTheStarCoin(itemProperty.TheStar + 1);
                if (itemProperty.TheStarExp >= theStarConfig.Exp)
                {
                    itemProperty.TheStarExp = itemProperty.TheStarExp - theStarConfig.Exp;
                    itemProperty.TheStar++;
                    isUpgrade = true;
                    //获取下一级的
                    var NexttheStarConfig = CacheFactory.PlayersdicCache.UpgradeTheStarCoin(itemProperty.TheStar + 1);
                    if (NexttheStarConfig == null || itemProperty.TheStarExp < NexttheStarConfig.Exp)
                        isWhile = false;
                    else
                        theStarConfig = NexttheStarConfig;
                }
                else
                    isWhile = false;
                index++;
            } while (isWhile && index < 6);
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
            ResetPotentialParamResponse response = new ResetPotentialParamResponse();
            response.Data = new ResetPotentialParam();
            try
            {
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.PotentialReset);
                if (package == null)
                    return ResponseHelper.Create<ResetPotentialParamResponse>((int)MessageCode.NbParameterError);
                var item = package.GetItem(itemId);
                if (item == null)
                    return ResponseHelper.Create<ResetPotentialParamResponse>((int)MessageCode.ItemNotExists);
                var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
                if (managerExtra == null)
                    return ResponseHelper.Create<ResetPotentialParamResponse>((int)MessageCode.NbParameterError);
                  var property = item.ItemProperty as PlayerCardProperty;
                if (property == null)
                    return ResponseHelper.Create<ResetPotentialParamResponse>((int)MessageCode.NbParameterError);
                var lockList = new List<int>() { 0, 0, 0 };
                if (lockString.Length > 0)
                {
                    var lockSplit = lockString.Split(',');
                    lockList = new List<int>();
                    foreach (var s in lockSplit)
                    {
                        lockList.Add(ConvertHelper.ConvertToInt(s));
                    }
                }
                int lockNumber = 0;
                int costPoint = 0;
                response.Data.FreeResetNumber = managerExtra.ResetPotentialNumber;
                var messageCode = CheckResetPotential(property, lockList, managerExtra, ref costPoint, ref lockNumber,true);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<ResetPotentialParamResponse>((int)messageCode);
                response.Data.CostPoint = costPoint;
                response.Data.LockNumber = lockNumber;

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("重置潜力参数", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 验证重置潜力
        /// </summary>
        /// <param name="property"></param>
        /// <param name="lockList"></param>
        /// <param name="managerExtra"></param>
        /// <param name="costPoint"></param>
        /// <param name="lockNumber"></param>
        /// <returns></returns>
        public MessageCode CheckResetPotential(PlayerCardProperty property, List<int> lockList, NbManagerextraEntity managerExtra,ref int costPoint,ref int lockNumber,bool isParam =false )
        {
            costPoint = 0;
            if (property == null)
                return MessageCode.PotentialNot;
            if (property.Potential == null || property.Potential.Count <= 0)
                return MessageCode.PotentialNot;
            if (managerExtra.ResetPotentialNumber <= 0)
            {
                if (!isParam)
                {
                    var mypoint = PayCore.Instance.GetPoint(managerExtra.ManagerId);
                    if (mypoint < _potentialResetPoint)
                        return MessageCode.NbPointShortage;
                }
                costPoint += _potentialResetPoint;
            }
            else
                managerExtra.ResetPotentialNumber--;
            lockNumber = 0;
            for (int i = 0; i < lockList.Count; i++)
            {
                if (lockList[i] == 1)
                {
                    lockNumber++;
                    if (property.Potential.Count <= i)
                        return MessageCode.PotentialNot;
                    
                }
            }
            if (ActivityExThread.Instance.PotentialLock())
                lockNumber --;
            if (lockNumber < 0)
                lockNumber = 0;
            if (lockNumber == 1)
                costPoint += _potentialLock1;
            else if (lockNumber == 2)
                costPoint += _potentialLock2;
            else if (lockNumber > 2)
                return MessageCode.NbParameterError;
            ActivityExThread.Instance.PotentialHalf(ref costPoint);
            return MessageCode.Success;
        }

        /// <summary>
        /// 重置潜力
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="lockString">0,0,0</param>
        /// <returns></returns>
        public ResetPotentialResponse ResetPotential(Guid managerId,Guid itemId,string lockString)
        {
            ResetPotentialResponse response = new ResetPotentialResponse();
            response.Data = new ResetPotential();
            try
            {
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.PotentialReset);
                if (package == null)
                    return ResponseHelper.Create<ResetPotentialResponse>((int) MessageCode.NbParameterError);
                var item = package.GetItem(itemId);
                if (item == null)
                    return ResponseHelper.Create<ResetPotentialResponse>((int) MessageCode.ItemNotExists);
                var manager = ManagerCore.Instance.GetManager(managerId);
                var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
                if (manager == null || managerExtra == null)
                    return ResponseHelper.Create<ResetPotentialResponse>((int) MessageCode.NbParameterError);
                var property = item.ItemProperty as PlayerCardProperty;
                if (property == null)
                    return ResponseHelper.Create<ResetPotentialResponse>((int) MessageCode.NbParameterError);

                var lockList = new List<int>() {0, 0, 0};
                if (lockString.Length > 0)
                {
                    var lockSplit = lockString.Split(',');
                    lockList = new List<int>();
                    foreach (var s in lockSplit)
                    {
                        lockList.Add(ConvertHelper.ConvertToInt(s));
                    }
                }
                int lockNumber = 0;
                int costPoint = 0;
                var messageCode = CheckResetPotential(property, lockList, managerExtra, ref costPoint, ref lockNumber);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<ResetPotentialResponse>((int) messageCode);
                var player = CacheFactory.PlayersdicCache.GetPlayer(item.ItemCode%100000);
                if (player == null)
                    return ResponseHelper.Create<ResetPotentialResponse>((int) MessageCode.NbParameterError);

                for (int i = 0; i < property.Potential.Count; i++)
                {
                    if (lockList[i] != 1)
                    {
                        //获取潜力等级
                        var potentialLevel = CacheFactory.PlayersdicCache.GetPotentialLevel(player.KpiLevel);
                        if (potentialLevel == 0)
                            return ResponseHelper.Create<ResetPotentialResponse>((int) MessageCode.NbParameterError);
                        //获取一个潜力
                        var potential = CacheFactory.PlayersdicCache.GetRandomPotential(player.PositionDesc, property.Potential,
                            potentialLevel);
                        property.Potential[i] = CacheFactory.PlayersdicCache.GetPotentialValue(potential);
                    }
                }
                item.ItemProperty = property;
                package.Update(item);
                TeammemberEntity teammember = null;
                ArenaTeammemberFrame arenaFrame = null;
                if (property.IsMain)
                {
                    if (property.MainType == 0)
                    {
                        teammember = TeammemberCore.Instance.GetTeammember(managerId, property.TeammemberId);
                        if (teammember != null)
                        {
                            var usingPlayerCard = new PlayerCardUsedEntity(item);
                            teammember.UsedPlayerCard = SerializationHelper.ToByte(usingPlayerCard);
                        }
                    }
                    else
                    {
                        arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType)property.MainType);
                        if (arenaFrame.TeammebmerDic != null &&
                            arenaFrame.TeammebmerDic.ContainsKey(property.TeammemberId))
                        {
                            var arenaTeammember = arenaFrame.GetTeammember(property.TeammemberId);
                            arenaTeammember.UsePlayer = property;
                        }
                        else
                            arenaFrame = null;
                    }
                }
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    MessageCode messCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (costPoint > 0)
                        {
                            var code = PayCore.Instance.GambleConsume(managerId, costPoint, ShareUtil.GenerateComb(),
                                EnumConsumeSourceType.ResetPotential, transactionManager.TransactionObject);
                            if (code != MessageCode.Success)
                            {
                                messCode = code;
                                break;
                            }
                        }
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (!NbManagerextraMgr.Update(managerExtra, transactionManager.TransactionObject))
                            break;
                        if (arenaFrame != null)
                        {
                            if (!arenaFrame.Save(transactionManager.TransactionObject))
                                break;
                        }
                        messCode = MessageCode.Success;
                    } while (false);
                    if (messCode != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<ResetPotentialResponse>((int)messCode);
                    }
                    transactionManager.Commit();
                    response.Data.UpdateItem = item;
                    if (teammember != null)
                    {
                        int returnCode = 0;
                        string errorMessage = "";
                        TeammemberMgr.SetUsePlayerCard(teammember.Idx, teammember.ManagerId, manager.Mod,
                            teammember.UsedPlayerCard, ref returnCode, ref errorMessage);
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            SystemlogMgr.Error("SaveSetPlayerCard", errorMessage);
                        }
                        if (returnCode == ShareUtil.SuccessCode)
                        {
                            MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, true);
                            var buff = BuffDataCore.Instance().RebuildMembers(managerId);
                            if (buff != null && buff.Kpi > 0)
                                response.Data.Kpi = buff.Kpi;
                            else
                                response.Data.Kpi = -1;
                        }
                    }
                    if (arenaFrame != null)
                    {
                        MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId,arenaFrame.ArenaType);
                    }
                    if (costPoint == 0)
                        response.Data.Point = -1;
                    else
                        response.Data.Point = PayCore.Instance.GetPoint(managerId);
                   
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("重置潜力", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #region 合同页合成

        public SynthesisParamResponse TheContractSyntheticParam(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4,
            Guid itemId5)
        {
             Dictionary<Guid, int> itemCountDic = new Dictionary<Guid, int>();
            BuildTheContractItemCountDic(itemCountDic, itemId1);
            BuildTheContractItemCountDic(itemCountDic, itemId2);
            BuildTheContractItemCountDic(itemCountDic, itemId3);
            BuildTheContractItemCountDic(itemCountDic, itemId4);
            BuildTheContractItemCountDic(itemCountDic, itemId5);
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TheContractSynthesis);

            #region check
            if (package == null)
                return ResponseHelper.InvalidParameter<SynthesisParamResponse>();
            MessageCode code = MessageCode.NbUpdateFail;
            //对比的ItemCode
            int itemCode = 0;
            var contrast = package.GetItem(itemId1);
            if (contrast == null)
                return ResponseHelper.InvalidParameter<SynthesisParamResponse>();
            foreach (var dic in itemCountDic)
            {
                var item = package.GetItem(dic.Key);
                if (item == null || item.ItemType != (int)EnumItemType.MallItem)
                    return ResponseHelper.InvalidParameter<SynthesisParamResponse>();
                if (itemCode == 0)
                {
                    itemCode = item.ItemCode;
                    var itemCache = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                    if (itemCache == null || itemCache.MallEffectType != (int)EnumMallEffectType.TheContract)
                        return ResponseHelper.InvalidParameter<SynthesisParamResponse>();
                }
                else if (item.ItemCode != contrast.ItemCode || item.ItemCode != itemCode)
                {
                    return ResponseHelper.InvalidParameter<SynthesisParamResponse>();
                }
                if (item.ItemCount < dic.Value)
                {
                    return ResponseHelper.InvalidParameter<SynthesisParamResponse>();
                }
            }
            //获得合成物品
            int newItemCode = CacheFactory.ItemsdicCache.GetTheContractItemCode(contrast.ItemCode);
            var newItemInfo = CacheFactory.ItemsdicCache.GetItem(newItemCode);
            var costCoin = 0;
            if (newItemInfo != null && newItemInfo.ItemType == (int)EnumItemType.PlayerCard)
            {
                if (newItemInfo.PlayerCardLevel == (int)EnumPlayerCardLevel.Gold)
                    costCoin = 10000;
                else if (newItemInfo.PlayerCardLevel == (int)EnumPlayerCardLevel.Silver)
                    costCoin = 5000;
                else if (newItemInfo.PlayerCardLevel == (int)EnumPlayerCardLevel.Orange)
                    costCoin = 2000;
                else
                {
                    return ResponseHelper.InvalidParameter<SynthesisParamResponse>();
                }
            }
            else if (newItemInfo != null && newItemInfo.ItemType == (int)EnumItemType.Equipment)
            {
                costCoin = 5000;
            }
            #endregion
            var response = ResponseHelper.CreateSuccess<SynthesisParamResponse>();
            response.Data = new SynthesisParamEntity();
            response.Data.CostPoint = 0;
            response.Data.CostCoin = costCoin;
            response.Data.Rate = 1;
            return response;

        }


        public SynthesisResponse TheContractSynthetic(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3,
            Guid itemId4, Guid itemId5)
        {
            Dictionary<Guid, int> itemCountDic = new Dictionary<Guid, int>();
            BuildTheContractItemCountDic(itemCountDic, itemId1);
            BuildTheContractItemCountDic(itemCountDic, itemId2);
            BuildTheContractItemCountDic(itemCountDic, itemId3);
            BuildTheContractItemCountDic(itemCountDic, itemId4);
            BuildTheContractItemCountDic(itemCountDic, itemId5);

            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TheContractSynthesis);

            #region check

            if (package == null)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();
            int itemCode = 0;
            bool isBinding = false;
            MessageCode code = MessageCode.NbUpdateFail;
            //对比的ItemCode
            var contrast = package.GetItem(itemId1);
            if (contrast == null)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();
            int newItemCode = 0;
            bool isDeal = true;
            if (PandoraCache.Instance.HasSyntheticItem(contrast.ItemCode))
            {
                foreach (var dic in itemCountDic)
                {
                    var item = package.GetItem(dic.Key);
                    if (item == null || item.ItemType != (int)EnumItemType.MallItem)
                        return ResponseHelper.InvalidParameter<SynthesisResponse>();
                    if (item.IsBinding)
                        isBinding = true;
                    if (!item.IsDeal)
                        isDeal = false;
                    code = package.Delete(dic.Key, dic.Value);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<SynthesisResponse>(code);
                }

                newItemCode = PandoraCache.Instance.GetSyntheticItem(contrast.ItemCode);
            }
            else
            {
                foreach (var dic in itemCountDic)
                {
                    var item = package.GetItem(dic.Key);
                    if (item == null || item.ItemType != (int)EnumItemType.MallItem)
                        return ResponseHelper.InvalidParameter<SynthesisResponse>();
                    if (itemCode == 0)
                    {
                        itemCode = item.ItemCode;
                        var itemCache = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                        if (itemCache == null || itemCache.MallEffectType != (int)EnumMallEffectType.TheContract)
                            return ResponseHelper.InvalidParameter<SynthesisResponse>();
                    }
                    else if (item.ItemCode != contrast.ItemCode || item.ItemCode != itemCode)
                    {
                        return ResponseHelper.InvalidParameter<SynthesisResponse>();
                    }
                    if (item.ItemCount < dic.Value)
                    {
                        return ResponseHelper.InvalidParameter<SynthesisResponse>();
                    }
                    if (item.IsBinding)
                        isBinding = true;
                    if (!item.IsDeal)
                        isDeal = false;
                    code = package.Delete(dic.Key, dic.Value);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<SynthesisResponse>(code);
                }
                //获得合成物品
                newItemCode = CacheFactory.ItemsdicCache.GetTheContractItemCode(contrast.ItemCode);
            }

            var newItemInfo = CacheFactory.ItemsdicCache.GetItem(newItemCode);
            var costCoin = 0;
            if (newItemInfo != null && newItemInfo.ItemType == (int)EnumItemType.PlayerCard)
            {
                if (newItemInfo.PlayerCardLevel == (int)EnumPlayerCardLevel.Gold)
                    costCoin = 10000;
                else if (newItemInfo.PlayerCardLevel == (int)EnumPlayerCardLevel.Silver)
                    costCoin = 5000;
                else if (newItemInfo.PlayerCardLevel == (int)EnumPlayerCardLevel.Orange)
                    costCoin = 2000;
                else
                {
                    return ResponseHelper.InvalidParameter<SynthesisResponse>();
                }
            }
            else if (newItemInfo != null && newItemInfo.ItemType == (int)EnumItemType.Equipment)
            {
                costCoin = 5000;
            }


            #endregion

            NbManagerEntity manager = ManagerCore.Instance.GetManager(managerId);
            PayUserEntity payUser = null;

            code = package.AddItem(newItemCode, isBinding,isDeal);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<SynthesisResponse>(code);
            var resultItemId = package.LastAddItemId;

            package.Shadow.Shadows.Add(new PandoraSynthesisShadow((int)EnumSynthesisType.TheContractSynthesis, itemId1,
                itemCode, itemId2, itemCode, itemId3,
                itemCode, itemId4, itemCode, itemId5, itemCode, Guid.Empty,
                0, false, costCoin, 0, (int)EnumPandoraResultType.Success,
                resultItemId, newItemCode, package.Shadow.TransactionId, Guid.Empty,
                0, Guid.Empty,
                0, 100));
            var result = MallCore.Instance.Pandora(package, manager, payUser, null, costCoin, 0, 0);
            if (result != MessageCode.Success)
                return ResponseHelper.Create<SynthesisResponse>(result);
            else
            {

                var response = ResponseHelper.CreateSuccess<SynthesisResponse>();
                response.Data = new SynthesisEntity();
                response.Data.ItemCode = newItemCode;
                response.Data.Package = ItemCore.Instance.BuildPackageData(package);
                response.Data.Coin = manager.Coin;
                response.Data.Point = -1;
                if (response.Data.Package != null && response.Data.Package.Items != null)
                {
                    var item = CacheFactory.ItemsdicCache.GetItem(newItemCode);
                    if (item != null && item.ItemType == (int)EnumItemType.Equipment)
                    {
                        if (ActivityExThread.Instance.IsActivity(Entity.Enums.Activity.EnumActivityExEffectType.EquipmentDebris, 0, 0))
                        {
                            if (item.ThirdType == (int)EnumEquipmentQuality.Epic)
                            {
                                var mail = new MailBuilder(managerId, "合成史诗装备送欧罗巴专属套装碎片");
                                var prizeCode = ActivityExThread.Instance.GetRandomDebris();
                                mail.AddAttachment(1, prizeCode, false, 1);
                                if (prizeCode > 0)
                                    mail.Save();
                            }
                        }
                       
                        if (item.ThirdType == (int)EnumEquipmentQuality.Epic)
                            ActivityExThread.Instance.EquipmentSynthesis(managerId, newItemCode);
                    }
                    else if (item != null && item.ItemType == (int)EnumItemType.PlayerCard)
                    {
                        if (item.SubType <= (int)EnumPlayerCardLevel.Orange)
                        {
                            ActivityExThread.Instance.SynthesisPlayer(managerId);
                        }
                        MailBuilder mail = null;
                        ActivityExThread.Instance.SynthesisTarento(managerId, item.LinkId, ref mail);
                        if (mail != null)
                            mail.Save();
                    }

                }
                return response;
            }
        }

        void BuildTheContractItemCountDic(Dictionary<Guid, int> itemCountDic, Guid itemId)
        {
            if (itemCountDic.ContainsKey(itemId))
                itemCountDic[itemId]++;
            else
            {
                itemCountDic.Add(itemId, 1);
            }
        }


        #endregion

        #region 合成装备
        /// <summary>
        /// 合成装备
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <returns></returns>
        public EquipmentSynthesisParamResponse EquipmentSynthesisParam(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4, Guid itemId5)
        {
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CardSynthesize);
            #region check
            if (package == null)
                return ResponseHelper.InvalidParameter<EquipmentSynthesisParamResponse>();
            var item1 = package.GetItem(itemId1);
            var item2 = package.GetItem(itemId2);
            var item3 = package.GetItem(itemId3);
            var item4 = package.GetItem(itemId4);
            var item5 = package.GetItem(itemId5);
            if (item1 == null || item2 == null || item3 == null || item4 == null || item5 == null)
                return ResponseHelper.InvalidParameter<EquipmentSynthesisParamResponse>();

            var itemCache1 = CacheFactory.ItemsdicCache.GetItem(item1.ItemCode);
            var itemCache2 = CacheFactory.ItemsdicCache.GetItem(item2.ItemCode);
            var itemCache3 = CacheFactory.ItemsdicCache.GetItem(item3.ItemCode);
            var itemCache4 = CacheFactory.ItemsdicCache.GetItem(item4.ItemCode);
            var itemCache5 = CacheFactory.ItemsdicCache.GetItem(item5.ItemCode);
            if (itemCache1 == null || itemCache2 == null || itemCache3 == null || itemCache4 == null || itemCache5 == null)
                return ResponseHelper.InvalidParameter<EquipmentSynthesisParamResponse>();

            //检查合成百搭卡
            Dictionary<Guid, int> wildcardCountDic = new Dictionary<Guid, int>();
            List<ItemInfoEntity> equipmentList = new List<ItemInfoEntity>();
            List<int> qualityList=new List<int>();
            Dictionary<Guid, ItemInfoEntity> mallList = new Dictionary<Guid, ItemInfoEntity>();

            int quality = 5;

            var code = CheckSynthesisEquipment(item1, itemCache1, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<EquipmentSynthesisParamResponse>(code);
            code = CheckSynthesisEquipment(item2, itemCache2, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<EquipmentSynthesisParamResponse>(code);
            code = CheckSynthesisEquipment(item3, itemCache3, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<EquipmentSynthesisParamResponse>(code);
            code = CheckSynthesisEquipment(item4, itemCache4, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<EquipmentSynthesisParamResponse>(code);
            code = CheckSynthesisEquipment(item5, itemCache5, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<EquipmentSynthesisParamResponse>(code);

            //史诗品质不可合成
            if (quality== 1)
                return ResponseHelper.InvalidParameter<EquipmentSynthesisParamResponse>();
            if (qualityList.Count < 4)
                qualityList.Add(quality);
            if (qualityList.Count < 5)
                qualityList.Add(quality);
            #endregion

            var maxQuality = quality - 1;
            var coin = 0;
            var rate = 0;
            CacheFactory.PandoraCache.GetEquipmentSynthesisParam(qualityList, maxQuality, out coin, out rate);//获取消耗点数
            var orgRate = rate / 100.00;//原始成功率
            var vipRate = GetVipAddSynthesisRate(managerId);//vip加成
            var totalRate = CalRatePlus(orgRate, vipRate);
            //欧洲杯狂欢
            var totalrate = (int)totalRate;
            ActivityExThread.Instance.EuropeCarnival(4, ref totalrate);
            var response = ResponseHelper.CreateSuccess<EquipmentSynthesisParamResponse>();
            response.Data = new EquipmentSynthesisParamEntity();
            response.Data.CostCoin = coin;
            response.Data.Rate = totalrate;
            response.Data.ItemString = "";
            response.Data.CostPoint = 0;
            return response;
        }


        public SynthesisResponse EquipmentSynthesis(Guid managerId, Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4, Guid itemId5, bool isProtect, Guid protectId)
        {
           var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CardSynthesize);
            #region check
            if (package == null)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();
            var item1 = package.GetItem(itemId1);
            var item2 = package.GetItem(itemId2);
            var item3 = package.GetItem(itemId3);
            var item4 = package.GetItem(itemId4);
            var item5 = package.GetItem(itemId5);
            if (item1 == null || item2 == null || item3 == null || item4 == null || item5 == null)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();

            var itemCache1 = CacheFactory.ItemsdicCache.GetItem(item1.ItemCode);
            var itemCache2 = CacheFactory.ItemsdicCache.GetItem(item2.ItemCode);
            var itemCache3 = CacheFactory.ItemsdicCache.GetItem(item3.ItemCode);
            var itemCache4 = CacheFactory.ItemsdicCache.GetItem(item4.ItemCode);
            var itemCache5 = CacheFactory.ItemsdicCache.GetItem(item5.ItemCode);
            if (itemCache1 == null || itemCache2 == null || itemCache3 == null || itemCache4 == null || itemCache5 == null)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();

            //检查合成百搭卡
             Dictionary<Guid, int> wildcardCountDic = new Dictionary<Guid, int>();
            List<ItemInfoEntity> equipmentList = new List<ItemInfoEntity>();
            List<int> qualityList=new List<int>();
            Dictionary<Guid, ItemInfoEntity> mallList = new Dictionary<Guid, ItemInfoEntity>();

            int quality = 5;

            var code = CheckSynthesisEquipment(item1, itemCache1, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<SynthesisResponse>(code);
            code = CheckSynthesisEquipment(item2, itemCache2, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<SynthesisResponse>(code);
            code = CheckSynthesisEquipment(item3, itemCache3, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<SynthesisResponse>(code);
            code = CheckSynthesisEquipment(item4, itemCache4, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<SynthesisResponse>(code);
            code = CheckSynthesisEquipment(item5, itemCache5, ref quality, ref wildcardCountDic, ref equipmentList, ref qualityList, ref mallList);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<SynthesisResponse>(code);

            //史诗品质不可合成
            if (quality== 1)
                return ResponseHelper.InvalidParameter<SynthesisResponse>();

            if (qualityList.Count < 4)
                qualityList.Add(quality);
            if (qualityList.Count < 5)
                qualityList.Add(quality);
            ItemInfoEntity protectItem = null;
            if (protectId != Guid.Empty)
            {
                protectItem = package.GetItem(protectId);
                if (protectItem == null)
                {
                    return ResponseHelper.InvalidParameter<SynthesisResponse>();
                }
                isProtect = true;
            }
            else
            {
                isProtect = false;
            }

            #endregion

            var maxQuality = quality - 1;
            var coin = 0;
            var rate = 0;
            CacheFactory.PandoraCache.GetEquipmentSynthesisParam(qualityList, maxQuality, out coin, out rate);//获取消耗点数
            var orgRate = rate / 100.00;//原始成功率
            var vipRate = GetVipAddSynthesisRate(managerId);//Vip加成
            var totalRate = CalRatePlus(orgRate, vipRate);
            //欧洲杯狂欢
            var totalrate = (int)totalRate;
            ActivityExThread.Instance.EuropeCarnival(4, ref totalrate);
            int newItemCode = 0;
            //成功
            if (RandomHelper.CheckPercentage(totalrate))
            {
                var newEquipment = CacheFactory.ItemsdicCache.RandomEquipment(maxQuality);
                newItemCode = newEquipment.ItemCode;
            }
            var response = doSynthesis(EnumSynthesisType.EquipmentSynthesis, totalrate, isProtect, 0, newItemCode, package, coin, equipmentList, mallList, wildcardCountDic, protectItem, null, null);
            return response;
        }

        MessageCode CheckSynthesisEquipment(ItemInfoEntity item, DicItemEntity itemCache, ref int quality, ref Dictionary<Guid, int> wildcardCountDic, ref List<ItemInfoEntity> equipmentList,ref List<int> qualityList, ref Dictionary<Guid, ItemInfoEntity> mallList )
        {
            if (itemCache.ItemType == (int)EnumItemType.Equipment)
            {
                if (equipmentList.Contains(item))
                    return MessageCode.NbParameterError;

                if (itemCache.EquipmentQuality == 1)
                    return MessageCode.ItemEquipmentSynthesisQuality1;

                if (quality > itemCache.EquipmentQuality)
                    quality = itemCache.EquipmentQuality;
                qualityList.Add(itemCache.EquipmentQuality);
                equipmentList.Add(item);
            }
            else if (itemCache.ItemType == (int)EnumItemType.MallItem)
            {
                if (itemCache.MallEffectType != (int)EnumMallEffectType.SynthesisJokerCard)
                    return MessageCode.ItemEquipmentSynthesisNoConfig;

                var allWildcardCount = 0;
                foreach (var wildcardCount in wildcardCountDic.Values)
                {
                    allWildcardCount += wildcardCount;
                }

                if (allWildcardCount >= 3)
                    return MessageCode.ItemSynthesisWildcardCoutOver;

                //if (quality > itemCache.MallQuality)
                //    quality = itemCache.MallQuality;
                //qualityList.Add(itemCache.MallQuality);

                if (wildcardCountDic.ContainsKey(item.ItemId))
                {
                    wildcardCountDic[item.ItemId]++;
                }
                else
                {
                    mallList.Add(item.ItemId, item);
                    wildcardCountDic.Add(item.ItemId, 1);
                }
            }
            else
            {
                return MessageCode.ItemSynthesisNoConfig;
            }
            return MessageCode.Success;
        }
        #endregion

        #region 装备出售

        public EquipmentSellResponse EquipmentSell(Guid managerId, Guid itemId)
        {
            try
            {
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.EquipmentSell);
                #region check
                if (package == null)
                    return ResponseHelper.InvalidParameter<EquipmentSellResponse>();
                var manager = ManagerCore.Instance.GetManager(managerId);
                var item = package.GetItem(itemId);
                if (item == null)
                    return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.ItemNotExists);
                if (item.ItemType != (int) EnumItemType.Equipment)
                    return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.ItemNotSell);
                var result = package.Delete(item);
                if (result != MessageCode.Success)
                    return ResponseHelper.Create<EquipmentSellResponse>(result);
                #endregion
                var itemInfo = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                if (itemInfo == null)
                    return ResponseHelper.InvalidParameter<EquipmentSellResponse>();
                int coin = CacheFactory.PandoraCache.GetEquipmentSellCoin(itemInfo.ThirdType);
                if (coin == 0)
                    return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.ItemNotSell);
                package.Shadow.AddShadow(managerId, itemId, item.ItemCode, coin);
                var code = SaveEquipmentSell(manager, coin, package);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<EquipmentSellResponse>(code);
                package.Shadow.Save();
                var response = ResponseHelper.CreateSuccess<EquipmentSellResponse>();
                response.Data = new EquipmentSellEntity();
                response.Data.Coin = coin;
                response.Data.ManagerCoin = manager.Coin;
                response.Data.Package = ItemCore.Instance.BuildPackageData(package);
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("装备出售", ex);
                return ResponseHelper.InvalidParameter<EquipmentSellResponse>();
            }
        }

        /// <summary>
        /// 道具出售
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public EquipmentSellResponse PrpoSell(Guid managerId, string items)
        {
            try
            {
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.EquipmentSell);

                if (package == null)
                    return ResponseHelper.InvalidParameter<EquipmentSellResponse>();
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return ResponseHelper.InvalidParameter<EquipmentSellResponse>();
                if (items.Length <= 0)
                    return ResponseHelper.Create<EquipmentSellResponse>((MessageCode.NotSellPrpo));
                var itemIds = items.Split(',');
                if(itemIds.Length>10)
                    return ResponseHelper.Create<EquipmentSellResponse>((MessageCode.MaxThePrpo));
                int coin = 0;
                foreach (var itemId in itemIds)
                {
                    Guid id = Guid.Empty;
                    Guid.TryParse(itemId, out id);
                    if (id == Guid.Empty)
                        return ResponseHelper.InvalidParameter<EquipmentSellResponse>();
                    var item = package.GetItem(id);
                    if (item == null)
                        return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.ItemNotExists);
                    var itemconfig = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                    if (itemconfig == null)
                        return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.ItemNotExists);
                    ConfigPrposellEntity prpoConfig = null;
                    int level = 1;
                    if (itemconfig.ItemType == (int) EnumItemType.PlayerCard)
                    {
                        var player = CacheFactory.PlayersdicCache.GetPlayer(itemconfig.LinkId);
                        //主力球员卡不可分解
                        var cardProperty = item.ItemProperty as PlayerCardProperty;
                        if (cardProperty != null && cardProperty.Equipment != null)
                            return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.PlayerHaveEquipment);
                        if (cardProperty != null && cardProperty.IsMain)
                            return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.TeammemberCantSell);
                        if (cardProperty != null && cardProperty.IsTrain)
                            return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.TeammemberTrainSell);
                        prpoConfig = CacheFactory.ItemsdicCache.GetPrpoSell(itemconfig.ItemType, player.CardLevel);
                        if (cardProperty != null)
                            level = cardProperty.Strength;
                        if (prpoConfig == null)
                            return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.ItemsNotSell);
                    }
                    else if (itemconfig.ItemType == (int) EnumItemType.Equipment)
                    {
                        var equipment = CacheFactory.EquipmentCache.GetEquipment(itemconfig.LinkId);
                        prpoConfig = CacheFactory.ItemsdicCache.GetPrpoSell(itemconfig.ItemType, equipment.Quality);
                        if (prpoConfig == null)
                            return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.ItemsNotSell);
                    }
                    else if (itemconfig.ItemType == (int)EnumItemType.MallItem)
                    {
                        var mallitem = CacheFactory.MallCache.GetMallEntityWithoutPoint(itemconfig.LinkId);
                        prpoConfig = CacheFactory.ItemsdicCache.GetPrpoSell(itemconfig.ItemType, mallitem.Quality);
                        if (prpoConfig == null)
                            return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.ItemsNotSell);
                    }
                    if (prpoConfig == null)
                        return ResponseHelper.Create<EquipmentSellResponse>(MessageCode.ItemsNotSell);
                    var count = item.ItemCount;
                    coin = (coin + prpoConfig.Coin * count) * level;
                    var result = package.Delete(item, count);

                    if (result != MessageCode.Success)
                        return ResponseHelper.Create<EquipmentSellResponse>(result);
                }

                var code = SaveEquipmentSell(manager, coin, package);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<EquipmentSellResponse>(code);
                package.Shadow.Save();
                var response = ResponseHelper.CreateSuccess<EquipmentSellResponse>();
                response.Data = new EquipmentSellEntity();
                response.Data.Coin = coin;
                response.Data.ManagerCoin = manager.Coin;
                response.Data.Package = ItemCore.Instance.BuildPackageData(package);
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("道具出售", ex);
                return ResponseHelper.InvalidParameter<EquipmentSellResponse>();
            }
        }

        #endregion

        /// <summary>
        /// 强化球员卡参数
        /// </summary>
        /// <param name="item1">物品ID</param>
        /// <param name="item2">物品ID</param>
        /// <param name="isProtectCheck">是否保护</param>
        /// <param name="protectCode">保护物品CODE</param>
        /// <param name="guideBuff"></param>
        /// <param name="extra"></param>
        /// <returns></returns>
        StrengthParamResponse StrengthenCheck(ItemInfoEntity item1, ItemInfoEntity item2, bool isProtectCheck, int protectCode, ref string guideBuff, out NbManagerextraEntity extra)
        {
            bool isProtect = isProtectCheck;
            extra = null;
            if (isProtect)
            {
                var proteceItem = CacheFactory.ItemsdicCache.GetItem(protectCode);
                if (proteceItem == null || proteceItem.MallEffectType != (int) EnumMallEffectType.ProteceItem)
                    return ResponseHelper.Create<StrengthParamResponse>(MessageCode.ProtectItemNot);
            }
            if (item1 == null
                || item2 == null
                || item1.Equals(item2) //同一张卡不能强化
                || item1.GetStrength() > _pandoraStrengthMax //可强化的卡的最高等级为8
                || item2.GetStrength() > _pandoraStrengthMax
                || item1.Status == (int)EnumItemStatus.Locked //球员卡为锁定状态
                || item2.Status == (int)EnumItemStatus.Locked
                )
            {
                return ResponseHelper.Create<StrengthParamResponse>(MessageCode.ItemStrengthInvalid);
            }
            if (item2.ItemType == (int) EnumItemType.PlayerCard)
            {
                var playerCardProperty = item2.ItemProperty as PlayerCardProperty;
                if (playerCardProperty != null && playerCardProperty.IsMain)
                    return ResponseHelper.Create<StrengthParamResponse>(MessageCode.ItemStrengthInvalid);
            }

            int strength1 = item1.GetStrength();
            int strength2 = item2.GetStrength();
            int protectStrength = 0;
            ConfigStrengthEntity strengthConfig = null;

            ItemInfoEntity resultItem = null;
            int luckyRate = 0;//幸运符增加概率
            var code = MessageCode.Success;
            //获取强化配置
            if (item1.ItemType == (int)EnumItemType.MallItem)
            {
                code = StrengthenWildCardCheck(item1, item2, out protectStrength, out strengthConfig, ref luckyRate);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<StrengthParamResponse>(code);
                resultItem = item2;
            }
            else if (item2.ItemType == (int)EnumItemType.MallItem)
            {
                code = StrengthenWildCardCheck(item2, item1, out protectStrength, out strengthConfig, ref luckyRate);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<StrengthParamResponse>(code);
                resultItem = item1;
            }
            else
            {
                //两个球员卡
                if (item1.ItemType != (int)EnumItemType.PlayerCard
                    || item2.ItemType != (int)EnumItemType.PlayerCard
                    || item1.ItemCode != item2.ItemCode)
                    return ResponseHelper.Create<StrengthParamResponse>(MessageCode.ItemStrengthInvalid);
                var playerCache = CacheFactory.ItemsdicCache.GetPlayerByItemCode(item1.ItemCode);
                if (playerCache == null)
                    return ResponseHelper.Create<StrengthParamResponse>(MessageCode.ItemStrengthInvalid);
                var playerCache2= CacheFactory.ItemsdicCache.GetPlayerByItemCode(item2.ItemCode);
                if (playerCache2 == null)
                    return ResponseHelper.Create<StrengthParamResponse>(MessageCode.ItemStrengthInvalid);
                var property = item2.ItemProperty as PlayerCardProperty;
                if(property!=null && property.IsTrain)
                    return ResponseHelper.Create<StrengthParamResponse>(MessageCode.TrainPlayerNoStrength);
                if (property != null && property.IsMain)
                    return ResponseHelper.Create<StrengthParamResponse>(MessageCode.MainPlayerNoStrength);
                strengthConfig = CacheFactory.PandoraCache.GetStrengthConfig(playerCache.CardLevel, strength1, strength2);
                if (strengthConfig == null)
                    return ResponseHelper.Create<StrengthParamResponse>(MessageCode.ItemStrengthNoConfig);
                resultItem = item1;
            }
            if (item1.IsBinding || item2.IsBinding)
                resultItem.IsBinding = true;

            var response = ResponseHelper.CreateSuccess<StrengthParamResponse>();
            response.Data = new StrengthParamEntity();
            if (strengthConfig.isProtect)
                isProtect = true;
            response.Data.CostPoint = 0;
            response.Data.CostCoin = strengthConfig.Coin;
            response.Data.ResultStrength = strengthConfig.Result;
            response.Data.CardLevel = strengthConfig.CardLevel;

            response.Data.LuckyRate = luckyRate;
            var sRate = Addlucky(strengthConfig.ShowRate / 100.00, response.Data.LuckyRate);
           // var sRate = CalRatePlus(strengthConfig.ShowRate / 100.00, response.Data.LuckyRate);
            //欧洲杯狂欢
            if (ActivityExThread.Instance.IsActivity(Entity.Enums.Activity.EnumActivityExEffectType.EuropeCarnival, 0, 0))
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    int rate = (int) sRate;
                    ActivityExThread.Instance.EuropeCarnival(7, ref rate);
                    sRate = rate;
                }
            }
            response.Data.Rate = sRate;
            if (sRate >= 95)
                response.Data.RealRate = sRate;
            else
            {
                double rRate = 0;
                if( response.Data.ResultStrength >5)
                    rRate = CalRatePlus(strengthConfig.Rate / 100.00, response.Data.LuckyRate); 
                else
                    rRate = Addlucky(strengthConfig.Rate / 100.00, response.Data.LuckyRate);

                if (ActivityExThread.Instance.IsActivity(Entity.Enums.Activity.EnumActivityExEffectType.EuropeCarnival, 0, 0))
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                    {
                        int rate = (int)rRate;
                        ActivityExThread.Instance.EuropeCarnival(7, ref rate);
                        rRate = rate;
                    }
                }

                response.Data.RealRate = rRate;
            }
            if (isProtect)
            {
                var mallDic = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(protectCode);
                if (mallDic != null && mallDic.EffectType == (int)EnumMallEffectType.ProteceItem)
                {
                    response.Data.UpgradeRate = mallDic.EffectValue;
                }
            }
            response.Data.ProtectFailType = (int)CalFailType(strengthConfig.Result, true);
            response.Data.IsProtect = strengthConfig.isDragon || strengthConfig.isProtect;
            if (strengthConfig.isDragon)
                response.Data.FailType = (int)EnumPandoraResultType.NoDowngrade;
            else
            {
                response.Data.FailType = (int)CalFailType(strengthConfig.Result, isProtect);
            }

            return response;
        }

        /// <summary>
        /// 加幸运符概率
        /// </summary>
        /// <param name="luckycharmCode"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        private MessageCode CalLuckyRate(int luckycharmCode, ref int rate)
        {
            if (luckycharmCode > 0)
            {
                var mallDic = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(luckycharmCode);
                if (mallDic != null && mallDic.EffectType == (int)EnumMallEffectType.Luckycharm)
                {
                    rate = mallDic.EffectValue;
                    return MessageCode.Success;
                }
                else
                {
                    return MessageCode.ItemLuckyParameterError;
                }
            }
            return MessageCode.Success;
        }

        MessageCode StrengthenWildCardCheck(ItemInfoEntity wildCard, ItemInfoEntity resultItem, out int protectStrength, out ConfigStrengthEntity strengthConfig,ref int rate)
        {
            protectStrength = 0;
            strengthConfig = null;

            if (resultItem.ItemType != (int)EnumItemType.PlayerCard)
                return MessageCode.ItemStrengthInvalid;
            var playerCache = CacheFactory.ItemsdicCache.GetPlayerByItemCode(resultItem.ItemCode);
            if (playerCache == null)
                return MessageCode.ItemStrengthInvalid;

            var mallCache = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(wildCard.ItemCode);
            if (mallCache == null)
                return MessageCode.ItemStrengthInvalid;
            strengthConfig = CacheFactory.PandoraCache.GetStrengthConfig(playerCache.CardLevel, resultItem.GetStrength(), 1);
            if (strengthConfig == null)
                return MessageCode.ItemStrengthNoConfig;
            if (mallCache.EffectType != (int)EnumMallEffectType.PandoraJokerCard&&
                mallCache.EffectType != (int)EnumMallEffectType.PandoraJokerLowCard&&
                mallCache.EffectType != (int)EnumMallEffectType.PandoraExtremeCard)
                return MessageCode.ItemStrengthInvalid;

            //次级百搭卡只能和紫色，蓝色、绿色搭配
            if (mallCache.EffectType == (int)EnumMallEffectType.PandoraJokerLowCard)
            {
                if (playerCache.CardLevel != (int)EnumPlayerCardLevel.Purple &&
                    playerCache.CardLevel != (int)EnumPlayerCardLevel.Blue &&
                    playerCache.CardLevel != (int)EnumPlayerCardLevel.Green
                    )
                    return MessageCode.ItemStrengthInvalid;
            }
            if (mallCache.EffectValue > 0)
            {
                rate = mallCache.EffectValue;
                if (mallCache.EffectType == (int) EnumMallEffectType.ProteceItem)
                    strengthConfig.isProtect = true;
            }
            return MessageCode.Success;
        }

        double CalRatePlus(double rate, int plus)
        {
            return (plus + 100) * rate / 100.00;
        }

        /// <summary>
        /// 幸运符加概率
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="luckyPlus"></param>
        /// <returns></returns>
        private double Addlucky(double rate, int luckyPlus)
        {
            return rate + luckyPlus;
        }

        /// <summary>
        /// 根据VIP等级获取合成成功率
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        int GetVipAddSynthesisRate(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            return GetVipAddSynthesisRate(manager);
        }
        /// <summary>
        /// 根据VIP等级获取合成成功率
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        int GetVipAddSynthesisRate(NbManagerEntity manager)
        {
            return CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.AddSynthesisRate);
        }

        /// <summary>
        /// 分解暴击
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public int GetCritRate(NbManagerEntity manager)
        {
            double vipRate = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.DecomposeCritRate);
            var buffCrit = BuffPoolCore.Instance().GetBuffValue(manager.Idx, EnumBuffCode.DestroyCardCritRate, true, false);
            if (buffCrit != null)
            {
                vipRate += buffCrit.Percent * 100;
            }
            return (int)vipRate;
        }

        /// <summary>
        /// 计算强化失败类型
        /// </summary>
        /// <param name="result"></param>
        /// <param name="isProtect"></param>
        /// <returns></returns>
        EnumPandoraResultType CalFailType(int result, bool isProtect)
        {
            //if (result <= 2)
            //    return EnumPandoraResultType.None;
            if (isProtect || result<= 5)
                return EnumPandoraResultType.NoDowngrade;
            return EnumPandoraResultType.Downgrade;
        }

        /// <summary>
        /// 删除百搭卡
        /// </summary>
        /// <param name="item"></param>
        /// <param name="package"></param>
        /// <param name="wildcardProtect"></param>
        /// <returns></returns>
        private MessageCode StrengthenItemDelete(ItemInfoEntity item, ItemPackageFrame package, bool wildcardProtect = false)
        {
            if (wildcardProtect)
            {
                if (item.ItemType == (int)EnumItemType.MallItem)
                {
                    var mallDic = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(item.ItemCode);
                    if (mallDic != null && mallDic.EffectType != (int)EnumMallEffectType.WildDragonCard)
                    {
                        return MessageCode.Success;
                    }

                }
            }
            return package.Delete(item);
        }

        /// <summary>
        /// 更新强化等级
        /// </summary>
        /// <param name="item"></param>
        /// <param name="package"></param>
        /// <param name="wildcardProtect"></param>
        /// <returns></returns>
        MessageCode StrengthenItemDowngrade(ItemInfoEntity item, ItemPackageFrame package, bool wildcardProtect = false)
        {
            var strength = item.GetStrength();
            if (strength > 1)
            {
                item.UpdateStrength(strength - 1);
                return package.Update(item);
            }
            else
            {
                if (wildcardProtect)
                {
                    if (item.ItemType == (int)EnumItemType.MallItem)
                    {
                        var mallDic = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(item.ItemCode);
                        if (mallDic != null && mallDic.EffectType != (int)EnumMallEffectType.WildDragonCard)
                        {
                            return MessageCode.Success;
                        }

                    }
                }
                return package.Delete(item);
            }
        }

        /// <summary>
        /// 保存球员卡分解
        /// </summary>
        /// <param name="package"></param>
        /// <param name="manager"></param>
        /// <param name="coin"></param>
        /// <param name="equipmentItemList"></param>
        /// <returns></returns>
        MessageCode SaveDecompose(ItemPackageFrame package, NbManagerEntity manager, int coin)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveDecompose(transactionManager.TransactionObject, package, manager, coin);
                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return messageCode;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveDecompose", ex);
                return MessageCode.Exception;
            }
        }

        /// <summary>
        /// 保存球员卡分解
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="package"></param>
        /// <param name="manager"></param>
        /// <param name="reiki"></param>
        /// <returns></returns>
        private MessageCode Tran_SaveDecompose(DbTransaction transaction, ItemPackageFrame package,
            NbManagerEntity manager, int coin)
        {
            if (package == null || manager == null)
                return MessageCode.NbUpdateFail;

            if (coin != 0)
            {
                var code = ManagerCore.Instance.AddCoin(manager, coin, EnumCoinChargeSourceType.PlayerCardDecompose,
                    ShareUtil.CreateSequential().ToString());
                if (code != MessageCode.Success)
                    return code;
            }

            if (!package.Save(transaction))
            {
                return MessageCode.NbUpdateFail;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 保存球员卡分解
        /// </summary>
        /// <param name="package"></param>
        /// <param name="manager"></param>
        /// <param name="coin"></param>
        /// <returns></returns>
        MessageCode SaveEquipmentSell(NbManagerEntity manager,int coin,ItemPackageFrame package)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    MessageCode message = MessageCode.NbUpdateFail;
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                       message = ManagerCore.Instance.AddCoin(manager, coin, EnumCoinChargeSourceType.EquipmentSell,
       ShareUtil.GenerateComb().ToString(),transactionManager.TransactionObject);
                        if (message != MessageCode.Success)
                            break;
                    } while (false);
                    if (message == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return message;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveEquipmentSell", ex);
                return MessageCode.Exception;
            }
        }

        /// <summary>
        /// 是否是一个物品
        /// </summary>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <returns></returns>
        bool CheckSysnthesisItems(Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4, Guid itemId5)
        {
            if (itemId1 == itemId2 || itemId1 == itemId3 || itemId1 == itemId4 || itemId1 == itemId5
                || itemId2 == itemId3 || itemId2 == itemId4 || itemId2 == itemId5
                || itemId3 == itemId4 || itemId3 == itemId5
                || itemId4 == itemId5
                )
                return false;
            else
            {
                return true;
            }
        }

        private SynthesisResponse doSynthesis(EnumSynthesisType synthesisType, double rate, bool isProtect, int protectCode,
                                              int newItemCode, ItemPackageFrame package, int costCoin,
                                              ItemInfoEntity item1, ItemInfoEntity item2, ItemInfoEntity item3,
                                              ItemInfoEntity item4, ItemInfoEntity item5, ItemInfoEntity luckyItem, ItemInfoEntity goldFormulaItem,
                                              ItemInfoEntity suitdrawingItem = null, int costPoint = 0)
        {
            var itemList = new List<ItemInfoEntity>(5);
            itemList.Add(item1);
            itemList.Add(item2);
            itemList.Add(item3);
            itemList.Add(item4);
            itemList.Add(item5);
            return doSynthesis(synthesisType, rate, isProtect, protectCode, newItemCode, package, costCoin, itemList, null,
                               null, luckyItem, goldFormulaItem, suitdrawingItem, costPoint);
        }

        SynthesisResponse doSynthesis(EnumSynthesisType synthesisType, double rate, bool isProtect, int protectCode, int newItemCode, ItemPackageFrame package, int costCoin,
            List<ItemInfoEntity> itemList, Dictionary<Guid, ItemInfoEntity> mallList, Dictionary<Guid, int> mallCountDic, ItemInfoEntity protectItem, ItemInfoEntity goldFormulaItem,
            ItemInfoEntity suitdrawingItem = null, int currectCostPoint = 0)
        {
            Guid managerId = package.ManagerId;
            EnumPandoraResultType resultType = EnumPandoraResultType.Success;
            int costPoint = 0;
            if (isProtect)
            {
                //costPoint = CacheFactory.MallCache.GetCostPoint(protectCode, DateTime.Now);
                //if (protectCode == 51910)//新球员卡合成
                //    costPoint = currectCostPoint;
                //BuffPlusHelper.SynthesisProtectDiscount(ref costPoint);
            }

            NbManagerEntity manager = null;
            if (costCoin > 0)
            {
                manager = ManagerCore.Instance.GetManager(managerId);
                if (manager.Coin < costCoin)
                {
                    return ResponseHelper.Create<SynthesisResponse>(MessageCode.NbCoinShortage);
                }
            }
            PayUserEntity payUser = null;
            if (costPoint > 0)
            {
                payUser = PayCore.Instance.GetPayUser(managerId);
                if (payUser.TotalPoint < costPoint)
                {
                    return ResponseHelper.Create<SynthesisResponse>(MessageCode.NbPointShortage);
                }
            }

            MessageCode code = MessageCode.NbUpdateFail;
            Guid resultItemId = Guid.Empty;
            //成功
            if (newItemCode > 0)
            {
                bool isBinding = itemList.Exists(d => d.IsBinding);
                if (!isBinding && mallList != null)
                {
                    foreach (var entity in mallList.Values)
                    {
                        if (entity.IsBinding)
                            isBinding = true;
                    }
                }
                if (!isBinding && suitdrawingItem != null && suitdrawingItem.IsBinding)
                    isBinding = true;
                code = DeleteSynthesisItem(package, itemList, mallList, mallCountDic, protectItem, goldFormulaItem, suitdrawingItem);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<SynthesisResponse>(code);

                code = package.AddItem(newItemCode, isBinding,false);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<SynthesisResponse>(code);
                resultItemId = package.LastAddItemId;
            }
            else
            {
                if (!isProtect)
                {
                    resultType = EnumPandoraResultType.Disappear;
                    code = DeleteSynthesisItem(package, itemList, mallList, mallCountDic, protectItem, goldFormulaItem, suitdrawingItem);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<SynthesisResponse>(code);
                }
                else
                {

                    code = DeleteSynthesisItem(package, null, mallList, mallCountDic, protectItem, goldFormulaItem);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<SynthesisResponse>(code);
                    resultType = EnumPandoraResultType.None;
                }
            }

            package.Shadow.AddShadow((int)synthesisType, itemList, mallList, mallCountDic,
                suitdrawingItem == null ? Guid.Empty : suitdrawingItem.ItemId, suitdrawingItem == null ? 0 : suitdrawingItem.ItemCode, isProtect, costCoin, costPoint, (int)resultType, resultItemId, newItemCode
                , protectItem == null ? Guid.Empty : protectItem.ItemId, protectItem == null ? 0 : protectItem.ItemCode
                 , goldFormulaItem == null ? Guid.Empty : goldFormulaItem.ItemId, goldFormulaItem == null ? 0 : goldFormulaItem.ItemCode, rate);
            var result = MallCore.Instance.Pandora(package, manager, payUser, null, costCoin, costPoint, protectCode);
            if (result != MessageCode.Success)
                return ResponseHelper.Create<SynthesisResponse>(result);
            else
            {
                var response = ResponseHelper.CreateSuccess<SynthesisResponse>();
                response.Data = new SynthesisEntity();
                response.Data.ItemCode = newItemCode;
                response.Data.Package = ItemCore.Instance.BuildPackageData(package);
                if (synthesisType == EnumSynthesisType.EquipmentSynthesis)
                {
                    var item = CacheFactory.ItemsdicCache.GetItem(newItemCode);
                    if (item != null && item.ItemType == (int) EnumItemType.Equipment)
                    {
                        if (ActivityExThread.Instance.IsActivity(Entity.Enums.Activity.EnumActivityExEffectType.EquipmentDebris, 0, 0))
                        {
                            if (item.ThirdType == (int)EnumEquipmentQuality.Epic)
                            {
                                var mail = new MailBuilder(managerId, "合成史诗装备送欧罗巴专属套装碎片");
                                var prizeCode = ActivityExThread.Instance.GetRandomDebris();
                                mail.AddAttachment(1, prizeCode, false, 1);
                                if (prizeCode > 0)
                                    mail.Save();
                            }
                        }
                        if (item.ThirdType == (int) EnumEquipmentQuality.Epic)
                            ActivityExThread.Instance.EquipmentSynthesis(managerId, newItemCode);
                        if (newItemCode > 0)
                            response.Data.EquipmentProperty = package.LastAddItem.ItemProperty as EquipmentProperty;
                    }
                }
                else if (synthesisType == EnumSynthesisType.PlayerCardSynthesis)
                {
                    var item = CacheFactory.ItemsdicCache.GetItem(newItemCode);
                    if (item != null && item.ItemType == (int) EnumItemType.PlayerCard)
                    {
                        if (item.SubType <= (int) EnumPlayerCardLevel.Orange)
                        {
                            ActivityExThread.Instance.SynthesisPlayer(managerId);
                        }
                        MailBuilder mail = null;
                        ActivityExThread.Instance.SynthesisTarento(managerId, item.LinkId, ref mail);
                        if (mail != null)
                            mail.Save();
                    }
                }
                if (manager != null)
                {
                    response.Data.Coin = manager.Coin;
                }
                else
                {
                    response.Data.Coin = -1;
                }

                if (payUser != null)
                {
                    response.Data.Point = payUser.TotalPoint - costPoint;
                }
                else
                {
                    response.Data.Point = -1;
                }
                if (costCoin > 0)
                {
                    switch (synthesisType)
                    {
                        case EnumSynthesisType.BallsoulSynthesis:
                            ShadowMgr.SaveCoinConsume(managerId, costCoin, EnumCoinConsumeSourceType.BallsoulSynthesis,
                                                package.Shadow.TransactionId.ToString());
                            break;
                        case EnumSynthesisType.EquipmentSynthesis:
                            ShadowMgr.SaveCoinConsume(managerId, costCoin, EnumCoinConsumeSourceType.EquipmentSynthesis,
                                            package.Shadow.TransactionId.ToString());
                            break;
                        case EnumSynthesisType.PlayerCardSynthesis:
                            ShadowMgr.SaveCoinConsume(managerId, costCoin, EnumCoinConsumeSourceType.PlayerCardSynthesis,
                                            package.Shadow.TransactionId.ToString());
                            break;
                        case EnumSynthesisType.WashstoneSynthesis:
                            ShadowMgr.SaveCoinConsume(managerId, costCoin, EnumCoinConsumeSourceType.WashstoneSynthesis,
                                            package.Shadow.TransactionId.ToString());
                            break;
                    }
                }
                return response;
            }
        }

        MessageCode DeleteSynthesisItem(ItemPackageFrame package, List<ItemInfoEntity> itemList, Dictionary<Guid, ItemInfoEntity> mallList, Dictionary<Guid, int> mallCountDic, ItemInfoEntity luckyItem, ItemInfoEntity goldFormulaItem, ItemInfoEntity suitdrawingItem = null)
        {
            var code = MessageCode.Success;
            if (itemList != null && itemList.Count > 0)
            {
                foreach (var item in itemList)
                {
                    code = package.Delete(item);
                    if (code != MessageCode.Success)
                        return code;
                }
            }
            if (mallList != null && mallList.Count > 0)
            {
                foreach (var item in mallList.Values)
                {
                    var count = mallCountDic[item.ItemId];
                    code = package.Delete(item, count);
                    if (code != MessageCode.Success)
                        return code;
                }
            }
            if (suitdrawingItem != null)
            {
                code = package.Delete(suitdrawingItem);
                if (code != MessageCode.Success)
                    return code;
            }
            if (luckyItem != null)
            {
                code = package.Delete(luckyItem);
                if (code != MessageCode.Success)
                    return code;
            }
            if (goldFormulaItem != null)
            {
                code = package.Delete(goldFormulaItem);
                if (code != MessageCode.Success)
                    return code;
            }
            return MessageCode.Success;
        }

    }
}
