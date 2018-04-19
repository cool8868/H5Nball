using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.League;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Player;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Activity
{
    public partial class ActivityExThread
    {
        public static TemplateActivityexdetailEntity _emptyDetail = new TemplateActivityexdetailEntity();

        #region Facade

        public void Charge(Guid managerId, string account, int point)
        {
            int requireId = (int) EnumActivityExRequire.Charge;
            doHandler(requireId, managerId, point, EnumActivityExDataPlusType.Plus);

            var requireId2 = (int) EnumActivityExRequire.ChargeReturnPoint;
            doHandler(requireId2, managerId, point, EnumActivityExDataPlusType.Plus);

            var requireId3 = (int) EnumActivityExRequire.ChargeCrossRank;
            doHandler(requireId3, managerId, point, EnumActivityExDataPlusType.Plus);

            var requireId4 = (int) EnumActivityExRequire.ChargeDailyCrossRank;
            doHandler(requireId4, managerId, point, EnumActivityExDataPlusType.Plus);

            var requireId5 = (int) EnumActivityExRequire.SingleCharge;
            doHandler(requireId5, managerId, point, EnumActivityExDataPlusType.Total);

        }

        /// <summary>
        /// 充值返点。 购买的礼包也算
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="point"></param>
        public void Charge(Guid managerId, int point)
        {
            int requireId = (int) EnumActivityExRequire.ChargeCount;
            doHandlerCount(requireId, managerId, point, EnumActivityExDataPlusType.Plus);

            int result = 0;
            int itemrate = 0;
            GetEffectValue(EnumActivityExEffectType.Charge, 0, 0, ref result, ref itemrate);
            if (result > 0)
            {
                point = (point*result)/100;
                var mail = new MailBuilder(managerId, "充值返利50%");
                mail.AddAttachment(EnumCurrencyType.Point, point);
                mail.Save();
            }
        }

        /// <summary>
        /// 领取消耗奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="account"></param>
        /// <param name="point"></param>
        public void Consume(Guid managerId, string account, int point)
        {
            int requireId = (int) EnumActivityExRequire.Consume;
            doHandler(requireId, managerId, point, EnumActivityExDataPlusType.Plus);
        }

        public void Ladder(Guid managerId, int score, int winType)
        {
            int requireId = (int) EnumActivityExRequire.LadderScore;
            doHandler(requireId, managerId, score, EnumActivityExDataPlusType.Total);

            requireId = (int) EnumActivityExRequire.Ladder;
            doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="managerId"></param>
        public void Login(Guid managerId)
        {
            int requireId = (int) EnumActivityExRequire.Login;
            doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
        }

        /// <summary>
        /// 天梯每日获胜奖励
        /// </summary>
        /// <param name="managerId"></param>
        public void LadderDayPrize(Guid managerId)
        {
            int requireId = (int) EnumActivityExRequire.LadderDayPrzie;
            doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
        }



        public void TememberColect(Guid managerId, int cardLevel, int count = 1)
        {
            if (cardLevel == 3)
            {
                int requireId = (int) EnumActivityExRequire.TeammemberColect;
                doHandler(requireId, managerId, count, EnumActivityExDataPlusType.Total);
            }
        }

        public void PlayerCardSynthesis(Guid managerId, int cardLevel)
        {
            if (cardLevel <= 2 || cardLevel == 7)
            {
                int requireId = (int) EnumActivityExRequire.PlayerSysthesis;
                doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);

                requireId = (int) EnumActivityExRequire.PlayerSysthesisRank;
                doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
            }
        }

        public void Scouting(Guid managerId, int scoutingId, int orangeCount, int scoutingCount)
        {
            int requireId = (int) EnumActivityExRequire.ScoutingOrange;
            if (orangeCount > 0)
            {
                doHandler(requireId, managerId, orangeCount, EnumActivityExDataPlusType.Plus);
            }
            if (scoutingId == 1)
            {
                requireId = (int) EnumActivityExRequire.ScoutingCoinRefresh;
            }
            else
            {
                requireId = (int) EnumActivityExRequire.ScoutingPointRefresh;
            }
            doHandler(requireId, managerId, scoutingCount, EnumActivityExDataPlusType.Plus);
        }

        /// <summary>
        /// 球探半价
        /// </summary>
        /// <param name="price"></param>
        public void ScoutingHalfPrice(ref int price)
        {
            if (IsActivity(EnumActivityExEffectType.ScoutingHalfPrice, 0, 0))
            {
                price = price/2;
            }
        }

        /// <summary>
        /// 根据卡等级参加活动
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="cardLevel"></param>
        public void ScoutingCardLevel(Guid managerId, string cardLevel)
        {
            if (cardLevel == "A" || cardLevel == "A+" || cardLevel == "S" || cardLevel == "S+")
            {
                var requireId = (int) EnumActivityExRequire.ScoutingACard;
                doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
            }
            if (cardLevel == "A+" || cardLevel == "S" || cardLevel == "S+")
            {
                var requireId = (int) EnumActivityExRequire.ScoutingACardAdd;
                doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
            }
        }

        public void ScoutingLottery(Guid managerId, int scoutingId, int scoutingCount = 1)
        {
            int requireId = 0;
            if (scoutingId == 8)
            {
                requireId = (int) EnumActivityExRequire.ScoutingCoinRefresh;
            }
            else
            {
                requireId = (int) EnumActivityExRequire.ScoutingPointRefresh;
            }
            doHandler(requireId, managerId, scoutingCount, EnumActivityExDataPlusType.Plus);
        }

        public void Arena(Guid managerId, EnumWinType winType)
        {
            int requireId = (int) EnumActivityExRequire.ArenaWin;
            if (winType == EnumWinType.Win)
            {
                doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
            }
        }

        public void ManagerLevelup(Guid managerId, int level)
        {
            int requireId = (int) EnumActivityExRequire.ManagerLevel;
            doHandler(requireId, managerId, level, EnumActivityExDataPlusType.Total);

            requireId = (int) EnumActivityExRequire.ManagerLevelRank;
            doHandler(requireId, managerId, level, EnumActivityExDataPlusType.Limit);
        }

        /// <summary>
        /// 强9送合同页
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        public void Strengthening(Guid managerId, ref int itemCode)
        {
            int requireId = (int) EnumActivityExRequire.strengthening;
            doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);

            int result = 0;
            GetEffectValue(EnumActivityExEffectType.Strengthening, 0, 0, ref result);
            if (result != 0)
            {
                DicItemEntity item = CacheFactory.ItemsdicCache.LotteryTheContract(1, 105, 109);
                if (item != null)
                    itemCode = item.ItemCode;
            }
        }



        /// <summary>
        /// 强9送欧冠合同
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <param name="cardLevel"></param>
        public void StrengtheningEuro(Guid managerId, ref List<int> itemCode, int cardLevel)
        {
            int requireId = (int) EnumActivityExRequire.strengthningEuro;
            if (cardLevel == (int) EnumPlayerCardLevel.Gold || cardLevel >= (int) EnumPlayerCardLevel.BlackGold)
                doHandler(requireId, managerId, 2, EnumActivityExDataPlusType.Plus);
            else
                doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);

            int result = 0;
            GetEffectValue(EnumActivityExEffectType.StrengtheningEuro, 0, 0, ref result);
            if (result != 0)
            {
                DicItemEntity item1 = CacheFactory.ItemsdicCache.LotteryTheContract(9, 99, 110);
                if (item1 != null)
                    itemCode.Add(item1.ItemCode);
                if (cardLevel == (int) EnumPlayerCardLevel.Gold || cardLevel >= (int) EnumPlayerCardLevel.BlackGold)
                {
                    DicItemEntity item2 = CacheFactory.ItemsdicCache.LotteryTheContract(9, 99, 110);
                    if (item2 != null)
                        itemCode.Add(item2.ItemCode);
                }
            }
        }

        /// <summary>
        /// 强9返卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        public void Strengthen9ReturnCard(Guid managerId, int itemCode)
        {
            int requireId = (int) EnumActivityExRequire.Strengthen9ReturnCard;
            doHandlerItem(requireId, managerId, itemCode, EnumActivityExDataPlusType.Total);
        }

        public void WorldChallenge(Guid managerId, int markNumber)
        {
            int requireId = (int) EnumActivityExRequire.WorldChallengeMaxMark;
            doHandler(requireId, managerId, markNumber, EnumActivityExDataPlusType.Total);

            requireId = (int) EnumActivityExRequire.WordChallengeMarkRank;
            doHandler(requireId, managerId, markNumber, EnumActivityExDataPlusType.Total);
        }

        public void Kpi(Guid managerId, int kpi)
        {
            int requireId = (int) EnumActivityExRequire.Kpi;
            doHandler(requireId, managerId, kpi, EnumActivityExDataPlusType.Total);

            requireId = (int) EnumActivityExRequire.KpiRank;
            doHandler(requireId, managerId, kpi, EnumActivityExDataPlusType.Total);
        }

        /// <summary>
        /// 点球大战跨服累计排行
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="score">积分</param>
        public void TopScore(Guid managerId, int score)
        {
            int requireId = (int) EnumActivityExRequire.TopScoreCrossRank;
            doHandler(requireId, managerId, score, EnumActivityExDataPlusType.Plus);
        }

        /// <summary>
        /// 装备洗练锁定
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="point">点券</param>
        public void EqLockConsume(Guid managerId, int point)
        {
            int requireId = (int) EnumActivityExRequire.EquipmentLockProperty;
            doHandler(requireId, managerId, point, EnumActivityExDataPlusType.Plus);
        }

        /// <summary>
        /// 欧洲之星
        /// </summary>
        public void EuropeTheStars(Guid managerId, EnumActivityExRequire type)
        {
            int requireId = (int) type;
            doHandlerCount(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
        }

        /// <summary>
        /// 球员卡合成 橙卡和元老
        /// </summary>
        /// <param name="managerId"></param>
        public void SynthesisPlayer(Guid managerId)
        {
            int requireId = (int) EnumActivityExRequire.SynthesisPlayer;
            doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
        }

        /// <summary>
        /// 巨星集会
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <param name="mail"></param>
        public void ScoutingDebris(Guid managerId, int itemCode, ref MailBuilder mail)
        {
            if (IsActivity(EnumActivityExEffectType.ScoutingDebris, 0, 0) ||
                IsActivity(EnumActivityExEffectType.ScoutingDebris1, 0, 0))
            {
                var item = CacheFactory.ItemsdicCache.GetItem(itemCode);
                if (item != null && item.ItemType == (int) EnumItemType.MallItem)
                {
                    if (item.MallEffectType == (int) EnumMallEffectType.TheContract)
                    {
                        //var playerId = CacheFactory.MallCache.GetTheContractPlayerId(item.ItemCode % 300000);
                        //if (playerId == 0)
                        //    return;
                        var player = CacheFactory.PlayersdicCache.GetPlayer(item.ImageId);
                        if (player != null)
                        {
                            var kpiLevel = player.KpiLevel.Trim().ToLower();
                            if (kpiLevel == "s" || kpiLevel == "s+")
                            {
                                if (mail == null)
                                    mail = new MailBuilder(managerId, "巨星集会");
                                if (GetActivityId(EnumActivityExEffectType.ScoutingDebris1, 0, 0) == 22) //送超级碎片宝箱
                                    mail.AddAttachment(1, 310171, false, 1);
                                else //送相同的球员碎片
                                    mail.AddAttachment(1, itemCode, false, 1);
                                int requireId = (int) EnumActivityExRequire.ScoutingCardS;
                                doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
                            }
                        }
                    }
                    else if (item.MallEffectType == (int) EnumMallEffectType.CoachDebris)
                    {
                        if (mail == null)
                            mail = new MailBuilder(managerId, "巨星集会");
                        if (GetActivityId(EnumActivityExEffectType.ScoutingDebris1, 0, 0) == 22) //送超级碎片宝箱
                            mail.AddAttachment(1, 310171, false, 1);
                        else //送相同的球员碎片
                            mail.AddAttachment(1, itemCode, false, 1);
                        int requireId = (int)EnumActivityExRequire.ScoutingCardS;
                        doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
                    }
                }
            }
            var item1 = CacheFactory.ItemsdicCache.GetItem(itemCode);
            if (item1 == null)
                return;
            if (item1.ItemType == (int) EnumItemType.PlayerCard ||
                (item1.ItemType == (int) EnumItemType.MallItem &&
                 item1.MallEffectType == (int) EnumMallEffectType.TheContract))
            {
                var player1 = CacheFactory.PlayersdicCache.GetPlayer(item1.ImageId);
                if (player1 != null)
                {
                    var kpiLevel = player1.KpiLevel.Trim().ToLower();
                    if (kpiLevel == "s" || kpiLevel == "s+")
                    {
                        var requireId1 = (int) EnumActivityExRequire.ScoutingCardOrDebris;
                        doHandler(requireId1, managerId, 1, EnumActivityExDataPlusType.Plus);
                    }
                }
            }
        }

        /// <summary>
        /// 合卡达人
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="playerId"></param>
        /// <param name="mail"></param>
        public void SynthesisTarento(Guid managerId, int playerId, ref MailBuilder mail)
        {
            if (IsActivity(EnumActivityExEffectType.SynthesisTarento, 0, 0))
            {
                var player = CacheFactory.PlayersdicCache.GetPlayer(playerId);
                if (player != null)
                {
                    var kpiLevel = player.KpiLevel.Trim().ToLower();
                    if (kpiLevel == "s" || kpiLevel == "s+")
                    {
                        int itemCode = 0;
                        int rate = 0;
                        GetEffectValue(EnumActivityExEffectType.SynthesisTarento, 0, 0, ref itemCode, ref rate);
                        if (itemCode > 0)
                        {
                            if (mail == null)
                                mail = new MailBuilder(managerId, "合成达人");
                            mail.AddAttachment(1, itemCode, false, 1);
                        }
                         int requireId = (int)EnumActivityExRequire.SynthesisCardS;
                         doHandler(requireId, managerId, 1, EnumActivityExDataPlusType.Plus);
                    }
                }
            }
        }

        /// <summary>
        /// 商城买一送一
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="mail"></param>
        public void MallBuyOneGetOne(Guid managerId, int itemCode, int itemCount, ref MailBuilder mail)
        {
            if (IsActivity(EnumActivityExEffectType.MallBuyOneGetOne, 0, 0))
            {
                if (mail == null)
                    mail = new MailBuilder(managerId, " 商城买一送一");
                mail.AddAttachment(itemCount, itemCode, false, 1);
            }
        }

        /// <summary>
        /// 暑期礼包
        /// </summary>
        /// <param name="scoutingType"></param>
        /// <returns></returns>
        public int SummerGiftBag(int scoutingType)
        {
            int itemcode = 0;
            int rate = 0;
            GetEffectValue(EnumActivityExEffectType.ScoutingGiftBag, scoutingType, 0, ref itemcode, ref rate);
            if (rate > 0)
            {
                if (RandomHelper.CheckPercentage(rate))
                    return itemcode;
            }
            return 0;
        }

        /// <summary>
        /// 抽元老碎片
        /// </summary>
        /// <param name="scoutingType"></param>
        /// <returns></returns>
        public int ScoutingDebris1(int scoutingType)
        {
            if (scoutingType != 2)//钻石抽卡才能抽中
                return 0;
            int itemcode = 0;
            int rate = 0;
            GetEffectValue(EnumActivityExEffectType.ScoutingDebris1, 0, 0, ref itemcode, ref rate);
            if (rate > 0)
            {
                if (RandomHelper.CheckPercentagePow(rate))
                    return itemcode;
            }
            return 0;
        }

        /// <summary>
        /// 中秋活动
        /// </summary>
        /// <param name="scoutingType"></param>
        /// <param name="specialItemNumber"></param>
        /// <returns></returns>
        public int MidAutumnActivity(int scoutingType, int specialItemNumber)
        {
            EnumActivityExEffectType type = EnumActivityExEffectType.CoinScouting;
            switch (scoutingType)
            {
                case 1:
                    type = EnumActivityExEffectType.CoinScouting;
                    break;
                case 2:
                    type = EnumActivityExEffectType.PointScouting;
                    break;
                case 3:
                    type = EnumActivityExEffectType.FriendPointScouting;
                    break;
                case 4:
                    type = EnumActivityExEffectType.FriendMatchScouting;
                    break;
                case 99:
                    type = EnumActivityExEffectType.GoldBarScouting;
                    break;
            }
            int itemcode = 0;
            int rate = 0;
            int count = 0;
            GetEffectValue(type, 0, 0, ref itemcode, ref rate,ref count);
            if (count > 0 && specialItemNumber >= count)
                return 0;
            if (rate > 0)
            {
                if (RandomHelper.CheckPercentage(rate))
                    return itemcode;
            }
            return 0;
        }

        /// <summary>
        /// 强化返百搭
        /// </summary>
        /// <returns></returns>
        public bool StrengthenReturnCard()
        {
            if (IsActivity(EnumActivityExEffectType.StrengthenReturnCard, 0, 0))
                return true;
            return false;
        }

        /// <summary>
        /// 升星双倍经验
        /// </summary>
        /// <returns></returns>
        public bool UpgradeTheStarDlouble()
        {
            if (IsActivity(EnumActivityExEffectType.UpgradeTheStarDlouble, 0, 0))
                return true;
            return false;
        }

        /// <summary>
        /// 潜力免费锁定
        /// </summary>
        /// <returns></returns>
        public bool PotentialLock()
        {
            if (IsActivity(EnumActivityExEffectType.PotentialLock, 0, 0))
                return true;
            return false;
        }

        /// <summary>
        /// 重置潜力半价
        /// </summary>
        /// <param name="point"></param>
        public void PotentialHalf(ref int point)
        {
            int result = 0;
            int itemrate = 0;
            GetEffectValue(EnumActivityExEffectType.PotentialHalf, 0, 0, ref result, ref itemrate);
            if (result > 0)
            {
                point = (point*result)/100;
            }
        }

        /// <summary>
        /// 装备合一送一
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        public void EquipmentSynthesis(Guid managerId, int itemCode)
        {
            if (IsActivity(EnumActivityExEffectType.EquipmentSynthesis, 0, 0))
            {
                var mail = new MailBuilder(managerId, "装备合一送一");
                mail.AddAttachment(1, itemCode, false, 0);
                mail.Save();
            }
        }

        /// <summary>
        /// 欧洲杯狂欢
        /// </summary>
        /// <param name="type">1友谊赛，2杯赛，3生涯，4装备合成，5友谊赛钻石掉落，6充值，7强化</param>
        /// <param name="values"></param>
        public bool EuropeCarnival(int type, ref int values)
        {
            if (IsActivity(EnumActivityExEffectType.EuropeCarnival, 0, 0) || IsActivity(EnumActivityExEffectType.Olympic, 0, 0))
            {
                DateTime date = DateTime.Now;
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        if (type == 1)
                            values = values + values/2;
                        break;
                    case DayOfWeek.Tuesday:
                        if (type == 2)
                            values = values*2;
                        break;
                    case DayOfWeek.Wednesday:
                        if (type == 3)
                            values = values*3;
                        break;
                    case DayOfWeek.Thursday:
                        if (type == 4)
                            values = values + values/2;
                        break;
                    case DayOfWeek.Friday:
                        if (type == 5)
                            values = values*3;
                        break;
                    case DayOfWeek.Saturday:
                        if (type == 6)
                            values = values/5;
                        break;
                    case DayOfWeek.Sunday:
                        if (type == 7)
                            values = values + ((values*3)/10);
                        break;
                }
                return true;
            }
            return false;
        }

        #region GetEffectValue
        public void GetEffectValue(EnumActivityExEffectType effectType, int condition, int conditionSub, ref int curValue)
        {
            try
            {
                var rate = GetEffectValue(effectType, condition, conditionSub);
                if (rate != null)
                {
                    curValue = rate.EffectValue + curValue * (100 + rate.EffectRate) / 100;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler-GetEffectValue", ex);
            }
            
        }

        public void GetEffectValue(EnumActivityExEffectType effectType, int condition, int conditionSub, ref int itemCode, ref int itemrate)
        {
            try
            {
                var rate = GetEffectValue(effectType, condition, conditionSub);
                if (rate != null)
                {
                    itemrate = rate.EffectRate;
                    itemCode = rate.EffectValue;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler-GetEffectValue", ex);
            }

        }

        public int GetActivityId(EnumActivityExEffectType effectType, int condition, int conditionSub)
        {
            try
            {
                var rate = GetEffectValue(effectType, condition, conditionSub);
                if (rate != null)
                {
                    return rate.ExcitingId;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler-GetEffectValue", ex);
            }
            return 0;
        }

        public void GetEffectValue(EnumActivityExEffectType effectType, int condition, int conditionSub, ref int itemCode, ref int itemrate,ref int count)
        {
            try
            {
                var rate = GetEffectValue(effectType, condition, conditionSub);
                if (rate != null)
                {
                    itemrate = rate.EffectRate;
                    itemCode = rate.EffectValue;
                    count = rate.Count;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler-GetEffectValue", ex);
            }

        }


        public void GetEffectValue(EnumActivityExEffectType effectType, int condition, int conditionSub, ref double curValue)
        {
            try
            {
                var rate = GetEffectValue(effectType, condition, conditionSub);
                if (rate != null)
                {
                    curValue = rate.EffectValue + curValue * (100 + rate.EffectRate) / 100;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler-GetEffectValue2", ex);
            }

        }

        public TemplateActivityexdetailEntity GetEffectValue(EnumActivityExEffectType effectType, int condition, int conditionSub)
        {
            return GetEffectValueServer(effectType, condition, conditionSub);
        }
        #endregion
        #endregion

        #region ServiceFacade
        public TemplateActivityexdetailEntity GetEffectValueServer(EnumActivityExEffectType effectType, int condition, int conditionSub)
        {
            try
            {
                if (_effectDic.ContainsKey((int)effectType))
                {
                    var list = _effectDic[(int)effectType];
                    DateTime curTime = DateTime.Now;
                    foreach (var entity in list)
                    {
                        if (entity.StartTime <= curTime && entity.EndTime >= curTime)
                        {
                            if ((entity.Condition == 0 || entity.Condition == condition)
                                && (entity.ConditionSub == 0 || entity.ConditionSub == conditionSub))
                                return entity;
                        }
                    }
                }
                return _emptyDetail;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler GetEffectValue",ex);
                return _emptyDetail;
            }
        }

        public bool IsActivity(EnumActivityExEffectType effectType, int condition, int conditionSub)
        {
            try
            {
                if (_effectDic.ContainsKey((int)effectType))
                {
                    var list = _effectDic[(int)effectType];
                    DateTime curTime = DateTime.Now;
                    foreach (var entity in list)
                    {
                        if (entity.StartTime <= curTime && entity.EndTime >= curTime)
                        {
                            if ((entity.Condition == 0 || entity.Condition == condition)
                                && (entity.ConditionSub == 0 || entity.ConditionSub == conditionSub))
                                return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler GetEffectValue", ex);
            }
            return false;
        }

        void doHandler(int requireId, Guid managerId, int exData, EnumActivityExDataPlusType exDataPlusType)
        {
            if (_activityClient != null)
            {
                try
                {
                    _activityClient.ActivityHandler(requireId, managerId, exData, exDataPlusType);

                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
                ActivityHandler(requireId, managerId, exData, exDataPlusType);
            }
        }

        void doHandlerCount(int requireId, Guid managerId, int exData, EnumActivityExDataPlusType exDataPlusType)
        {
            ActivityHandlerCount(requireId, managerId, exData, exDataPlusType);
        }

        void doHandlerItem(int requireId, Guid managerId, int itemCode, EnumActivityExDataPlusType exDataPlusType)
        {
            ActivityHandlerItem(requireId, managerId, itemCode, exDataPlusType);
        }

        public void ActivityHandlerCount(int requireId, Guid managerId, int exData, EnumActivityExDataPlusType exDataPlusType)
        {
            if (_activityGroupRequireDic.ContainsKey(requireId))
            {
                var groups = _activityGroupRequireDic[requireId];
                if (groups != null)
                {
                    DateTime curTime = DateTime.Now;
                    foreach (var group in groups)
                    {
                        if (group.StartTime <= curTime && group.EndTime >= curTime)
                        {
                            doGroupCount(group, managerId, exData, exDataPlusType, curTime.Date);
                        }
                    }
                }
            }
        }

        public void ActivityHandlerItem(int requireId, Guid managerId, int itemCode, EnumActivityExDataPlusType exDataPlusType)
        {
            if (_activityGroupRequireDic.ContainsKey(requireId))
            {
                var groups = _activityGroupRequireDic[requireId];
                if (groups != null)
                {
                    DateTime curTime = DateTime.Now;
                    foreach (var group in groups)
                    {
                        if (group.StartTime <= curTime && group.EndTime >= curTime)
                        {
                            doGroupItem(group, managerId, itemCode, exDataPlusType, curTime.Date);
                        }
                    }
                }
            }
        }

        public void ActivityHandler(int requireId, Guid managerId, int exData, EnumActivityExDataPlusType exDataPlusType)
        {
            if (_activityGroupRequireDic.ContainsKey(requireId))
            {
                var groups = _activityGroupRequireDic[requireId];
                if (groups != null)
                {
                    DateTime curTime = DateTime.Now;
                    foreach (var group in groups)
                    {
                        if (group.StartTime <= curTime && group.EndTime >= curTime)
                        {
                            if (group.IsRank)
                            {
                                if (requireId == (int) EnumActivityExRequire.TopScoreCrossRank
                                    || requireId == (int)EnumActivityExRequire.ChargeCrossRank
                                    || requireId == (int)EnumActivityExRequire.ChargeDailyCrossRank
                                    )//跨服
                                    doCrossRank(group, managerId, exData, exDataPlusType);
                                else
                                    doRank(group, managerId, exData, exDataPlusType);
                            }
                            else
                            {
                                doGroup(group, managerId, exData, exDataPlusType,curTime.Date);
                            }
                        }
                    }
                }
            }
        }


        void doGroupCount(TemplateActivityexgroupEntity group, Guid managerId, int exData, EnumActivityExDataPlusType exDataPlusType, DateTime recordDate)
        {
            try
            {
                var exRecord = ActivityexCountrecordMgr.GetByManagerId(managerId, group.ZoneActivityId, group.GroupId);
                if (exRecord == null)
                {
                    exRecord = BuildExCountRecord(group, managerId);
                }
                if (group.ExRequireId == (int)EnumActivityExRequire.ScoutingEurope || group.ExRequireId == (int)EnumActivityExRequire.ScoutingHugeEurope)
                {
                    if (exDataPlusType == (int)EnumActivityExDataPlusType.Total)
                    {
                        if (exRecord.ExData < exData)
                            exRecord.ExData = exData;
                        if (exRecord.CurData < exData)
                            exRecord.CurData = exData;
                    }
                    else
                    {
                        exRecord.CurData += exData;
                        exRecord.ExData += exData;
                    }
                    if (exRecord.AlreadySendCount - exRecord.ExData > 0)
                        exRecord.Status = 1;
                    if (exRecord.Idx == 0)
                    {
                        ActivityexCountrecordMgr.Insert(exRecord);
                    }
                    else
                    {
                        exRecord.UpdateTime = DateTime.Now;
                        ActivityexCountrecordMgr.Update(exRecord);
                    }
                }else if (group.ExRequireId == (int) EnumActivityExRequire.ChargeCount)
                {
                    if (exDataPlusType == (int)EnumActivityExDataPlusType.Total)
                    {
                        if (exRecord.ExData < exData)
                            exRecord.ExData = exData;
                        if (exRecord.CurData < exData)
                            exRecord.CurData = exData;
                    }
                    else
                    {
                        exRecord.CurData += exData;
                        exRecord.ExData += exData;
                    }
                    if (exRecord.AlreadySendCount - exRecord.ExData >= group.RankCount)
                        exRecord.Status = 1;
                    if (exRecord.Idx == 0)
                    {
                        ActivityexCountrecordMgr.Insert(exRecord);
                    }
                    else
                    {
                        exRecord.UpdateTime = DateTime.Now;
                        ActivityexCountrecordMgr.Update(exRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler doGroup", ex);
            }
        }

        void doGroupItem(TemplateActivityexgroupEntity group, Guid managerId, int itemCode, EnumActivityExDataPlusType exDataPlusType, DateTime recordDate)
        {
            try
            {
                //强9返卡
                if (group.ExRequireId == (int) EnumActivityExRequire.Strengthen9ReturnCard)
                {
                    var exRecord = ActivityexItemrecordMgr.GetByManagerExcitingId(managerId, group.ZoneActivityId,
                        group.GroupId);
                    if (exRecord == null)
                        exRecord = BuildExItemRecord(group, managerId);
                    exRecord.ItemString += "3," + itemCode + ",1|";
                    if (exRecord.Idx == 0)
                    {
                        ActivityexItemrecordMgr.Insert(exRecord);
                    }
                    else
                    {
                        exRecord.UpdateTime = DateTime.Now;
                        ActivityexItemrecordMgr.Update(exRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler doGroup", ex);
            }
        }

        private void doGroup(TemplateActivityexgroupEntity group, Guid managerId, int exData,
            EnumActivityExDataPlusType exDataPlusType, DateTime recordDate)
        {
            try
            {
                var exRecord = ActivityexRecordMgr.GetByManagerExcitingId(managerId, group.ZoneActivityId,
                    group.GroupId);
                if (exRecord == null)
                {
                    exRecord = BuildExRecord(group, managerId);
                }
                else
                {
                    HandlerDailyExData(group, exRecord, recordDate, false);
                }
                if (exRecord.Status == 0 || group.ExRequireId == (int) EnumActivityExRequire.ChargeReturnPoint
                    || group.ExRequireId == (int) EnumActivityExRequire.EquipmentLockProperty //装备洗练锁定返消耗
                    || group.ExcitingId == 80 //双十一返消耗50%
                    )
                {

                    if (exDataPlusType == (int) EnumActivityExDataPlusType.Total)
                    {
                        if (exRecord.ExData < exData)
                            exRecord.ExData = exData;
                        if (exRecord.CurData < exData)
                            exRecord.CurData = exData;
                    }
                    else
                    {
                        exRecord.CurData += exData;
                        exRecord.ExData += exData;
                    }
                    CalExStep(group, exRecord);

                    if (exRecord.Idx == 0)
                    {
                        ActivityexRecordMgr.Insert(exRecord);
                    }
                    else
                    {
                        exRecord.UpdateTime = DateTime.Now;
                        ActivityexRecordMgr.Update(exRecord);
                    }
                    if (group.ExRequireId == (int) EnumActivityExRequire.SynthesisCardS)
                    {
                        try
                        {
                            if (exRecord.ExData > 0 && group.RankCount > 0 && (exRecord.ExData%group.RankCount) == 0)
                            {
                                var mail = new MailBuilder(managerId, "合成达人");
                                mail.AddAttachment(1, group.RankCondition, false, 1);
                                mail.Save();
                            }
                        }
                        catch
                        {
                        }
                    }
                    else if (group.ExRequireId == (int) EnumActivityExRequire.ScoutingCardS)
                    {
                        try
                        {
                            if (exRecord.ExData > 0 && group.RankCount > 0 && (exRecord.ExData%group.RankCount) == 0)
                            {
                                var mail = new MailBuilder(managerId, "巨星集会");
                                mail.AddAttachment(1, group.RankCondition, false, 1);

                                mail.AddAttachment(5, 310176, false, 1);
                                mail.Save();
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler doGroup", ex);
            }
        }

        private void doRank(TemplateActivityexgroupEntity group, Guid managerId, int exData,
                             EnumActivityExDataPlusType exDataPlusType)
        {
            try
            {
                var rankKey = BuildExRankKey(group);
                var exRank = ActivityexRankMgr.GetByManagerRankKey(managerId, rankKey);
                if (exRank == null)
                {
                    var manager = ManagerCore.Instance.GetManager(managerId);
                    exRank = new ActivityexRankEntity(){ExData = 0,ManagerId = managerId,Name = manager==null?"":manager.Name,RankKey = rankKey,RowTime = DateTime.Now,Status = 0
                    ,UpdateTime = DateTime.Now};

                }
                
                if (exDataPlusType == EnumActivityExDataPlusType.Total)
                {
                    if (exRank.ExData < exData)
                        exRank.ExData = exData;
                }
                else if (exDataPlusType == EnumActivityExDataPlusType.Limit)
                {
                    if (exRank.ExData >= group.RankCondition)
                    {
                        return;
                    }
                    else
                    {
                        exRank.ExData = exData;
                    }
                }
                else
                {
                    exRank.ExData += exData;
                }
                if (exRank.Idx == 0)
                {
                    ActivityexRankMgr.Insert(exRank);
                }
                else
                {
                    exRank.UpdateTime = DateTime.Now;
                    ActivityexRankMgr.Update(exRank);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler doRank", ex);
            }
        }

        /// <summary>
        /// 精彩活动跨服排行
        /// </summary>
        /// <param name="group"></param>
        /// <param name="managerId"></param>
        /// <param name="exData"></param>
        /// <param name="exDataPlusType"></param>
        private void doCrossRank(TemplateActivityexgroupEntity group, Guid managerId, int exData,
                             EnumActivityExDataPlusType exDataPlusType)
        {
            try
            {
                //获取domainId
                var domainId = 0;
                //CrossSiteCache.Instance().TryGetDomainId(ShareUtil.ZoneName, out domainId);

                var rankKey = BuildExRankKey(group);
                var exRank = ActivityexCrossrankMgr.GetByManagerCrossRankKey(managerId, rankKey);
                if (exRank == null)
                {
                    var manager = ManagerCore.Instance.GetManager(managerId);
                    exRank = new ActivityexCrossrankEntity()
                    {
                        ExData = 0,
                        DomainId = domainId,
                        SiteId = ShareUtil.ZoneName,
                        ManagerId = managerId,
                        Name = manager == null ? "" : ShareUtil.ZoneName + "." + manager.Name,
                        RankKey = rankKey,
                        RowTime = DateTime.Now,
                        Status = 0,
                        UpdateTime = DateTime.Now
                    };
                }

                if (exDataPlusType == EnumActivityExDataPlusType.Total)
                {
                    if (exRank.ExData < exData)
                        exRank.ExData = exData;
                }
                else if (exDataPlusType == EnumActivityExDataPlusType.Limit)
                {
                    if (exRank.ExData >= group.RankCondition)
                    {
                        return;
                    }
                    else
                    {
                        exRank.ExData = exData;
                    }
                }
                else
                {
                    exRank.ExData += exData;
                }
                if (exRank.Idx == 0)
                {
                    ActivityexCrossrankMgr.Insert(exRank);
                }
                else
                {
                    exRank.UpdateTime = DateTime.Now;
                    ActivityexCrossrankMgr.Update(exRank);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExHandler doRank", ex);
            }
        }

        public static void CalExStep(TemplateActivityexgroupEntity group,ActivityexRecordEntity exRecord)
        {
            int step = 0;
            bool isMax = false;
            for (int i = group.Details.Count; i > 0; i--)
            {
                var detail = group.Details[i - 1];
                if (exRecord.ExData >= detail.Count)
                {
                    if (group.ExRequireId==(int)EnumActivityExRequire.ChargeReturnPoint || i==group.Details.Count)
                        isMax = true;
                    step = detail.ExStep;
                    break;
                }
            }
            exRecord.ExStep = step;
            if (isMax)
            {
               var messcode = ActivityExThread.Instance.CheckVip(exRecord);
                if (messcode == MessageCode.Success)
                    exRecord.Status = 1;
            }
        }
        
        #endregion

        #region Consume

        public void SettlementConsume(int activityId, Guid managerId)
        {
            var groupList = _activityGroupDic[activityId];
            if (groupList != null && groupList.Count > 0)
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                var point = -1;
                int getState = 0;
                foreach (var group in groupList)
                {
                    if (group.ExRequireId == (int)EnumActivityExRequire.Consume)
                    {
                        if (group.StatisticCycle == (int)EnumActivityExStatisticCycle.Daily)
                        {
                            if (point < 0 && getState != 1)
                            {
                                PayConsumehistoryMgr.GetPointForActivity(manager.Account, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1),
                                                                         ref point);
                                getState = 1;
                            }
                        }
                        else
                        {
                            if (point < 0 && getState != 2)
                            {
                                PayConsumehistoryMgr.GetPointForActivity(manager.Account, group.StartTime, group.EndTime,
                                                                         ref point);
                                getState = 2;
                            }
                        }
                    }
                    //洗练锁定属性消耗
                    if (group.ExRequireId == (int)EnumActivityExRequire.EquipmentLockProperty)
                    {
                        PayConsumehistoryMgr.GetEqLockPointForActivity(managerId, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1),
                                                                         ref point);
                    }

                    if (group.IsRank)
                        doRank(group,managerId,point < 0 ? 0 : point,EnumActivityExDataPlusType.Total);
                    else
                        doGroup(group, managerId, point < 0 ? 0 : point, EnumActivityExDataPlusType.Total, DateTime.Today);
                }
            }
        }

        void SettlementConsume(TemplateActivityexgroupEntity group, bool isDaily, DateTime recordDate)
        {
            if (group.ExRequireId == (int)EnumActivityExRequire.Consume)
            {
                recordDate = recordDate.AddDays(-1);
                var startTime = group.StartTime;
                var endTime = group.EndTime;
                if (isDaily)
                {
                    startTime = recordDate;
                    endTime = recordDate.AddDays(1).AddSeconds(-1);
                }
                var managerList = PayConsumehistoryMgr.GetPointList(startTime, endTime);
                if (managerList != null && managerList.Count > 0)
                {
                    foreach (var entity in managerList)
                    {
                        var activity = ActivityexRecordMgr.GetByManagerExcitingId(entity.ManagerId, group.ZoneActivityId,
                                                                                  group.Idx);
                        if (SettlementConsumeCheck(activity, isDaily, recordDate))
                        {
                            if (group.IsRank)
                                doRank(group, entity.ManagerId, entity.Point, EnumActivityExDataPlusType.Total);
                            else
                                doGroup(group, entity.ManagerId, entity.Point, EnumActivityExDataPlusType.Total,
                                    recordDate);
                        }
                        //双十一返消耗50%
                        if (group.ExcitingId == 80)
                            doGroup(group, entity.ManagerId, entity.Point, EnumActivityExDataPlusType.Total,
                                                               recordDate);

                    }
                }
            }
            //洗练锁定属性消耗
            if (group.ExRequireId == (int) EnumActivityExRequire.EquipmentLockProperty)
            {
                recordDate = recordDate.AddDays(-1);
                var startTime = group.StartTime;
                var endTime = group.EndTime;
                if (isDaily)
                {
                    startTime = recordDate;
                    endTime = recordDate.AddDays(1).AddSeconds(-1);
                }
                var managerList = PayConsumehistoryMgr.GetEqLockPointList(startTime, endTime);
                if (managerList != null && managerList.Count > 0)
                {
                    foreach (var entity in managerList)
                    {
                        //var activity = ActivityexRecordMgr.GetByManagerExcitingId(entity.ManagerId, group.ZoneActivityId,
                        //                                                            group.Idx);
                        //if (SettlementConsumeCheck(activity, isDaily, recordDate))
                        //{

                        //需要统计所有消耗，包括超过对应档次需求的部分
                        doGroup(group, entity.ManagerId, entity.Point, EnumActivityExDataPlusType.Total,
                                recordDate);
                        //}
                    }
                }
            }

        }

        bool SettlementConsumeCheck(ActivityexRecordEntity activity, bool isDaily, DateTime recordDate)
        {
            if (activity == null)
            {
                return true;
            }
            if (isDaily)
            {
                if (activity.RecordDate != recordDate)
                    return true;
                if (activity.Status == 0)
                    return true;
            }
            else
            {
                if (activity.Status == 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region encapsulation
        static ActivityexRecordEntity BuildExRecord(TemplateActivityexgroupEntity group, Guid managerId)
        {
            var exRecord = new ActivityexRecordEntity();
            exRecord.ExData = 0;
            exRecord.ExStep = 0;
            exRecord.ExcitingId = group.ExcitingId;
            exRecord.GroupId = group.GroupId;
            exRecord.ManagerId = managerId;
            exRecord.RecordDate = DateTime.Today;
            exRecord.RowTime = DateTime.Now;
            exRecord.Status = 0;
            exRecord.UpdateTime = DateTime.Now;
            exRecord.ZoneActivityId = group.ZoneActivityId;
            exRecord.NeedSync = true;
            return exRecord;
        }

        static ActivityexItemrecordEntity BuildExItemRecord(TemplateActivityexgroupEntity group, Guid managerId)
        {
            var exRecord = new ActivityexItemrecordEntity();
            exRecord.ExcitingId = group.ExcitingId;
            exRecord.GroupId = group.GroupId;
            exRecord.ManagerId = managerId;
            exRecord.RecordDate = DateTime.Today;
            exRecord.RowTime = DateTime.Now;
            exRecord.Status = 0;
            exRecord.UpdateTime = DateTime.Now;
            exRecord.ZoneActivityId = group.ZoneActivityId;
            exRecord.ItemString = "";
            return exRecord;
        }

        static ActivityexCountrecordEntity BuildExCountRecord(TemplateActivityexgroupEntity group, Guid managerId)
        {
            var exRecord = new ActivityexCountrecordEntity();
            exRecord.ExData = 0;
            exRecord.CurData = 0;
            exRecord.ExStep = 1;
            exRecord.ExcitingId = group.ExcitingId;
            exRecord.GroupId = group.GroupId;
            exRecord.ManagerId = managerId;
            exRecord.RecordDate = DateTime.Today;
            exRecord.RowTime = DateTime.Now;
            exRecord.Status = 0;
            exRecord.UpdateTime = DateTime.Now;
            exRecord.ZoneActivityId = group.ZoneActivityId;
            exRecord.NeedSync = true;
            exRecord.AlreadySendCount = 0;
            return exRecord;
        }
        #endregion 
    }
}
