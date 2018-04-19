using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Mall;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Share;

namespace Games.NBall.Core.Mail
{
    public class MailBuilder
    {
        private MailInfoEntity _mail;

        #region .ctor
        public MailBuilder()
        {
            _mail = new MailInfoEntity();
        }
        /// <summary>
        /// 竞猜真实比赛，返回真实点券给玩家
        /// </summary>
        public MailBuilder(EnumMailType mailType, Guid managerId, string title,string option,EnumCurrencyType currencyType, int currency)
            : this()
        {
            string content = string.Format("T,{0}|O,{1}|C,{2}|AT,{3}", title,option,currency, (int)currencyType);
            AddAttachment(currencyType, currency);
            Builder(managerId, mailType, content);
        }
        /// <summary>
        /// 竞猜真实比赛，返回真实点券给庄家
        /// </summary>
        public MailBuilder(EnumMailType mailType, Guid managerId, string title, EnumCurrencyType currencyType, int currency, int currencyWin)
            : this()
        {
            string content = string.Format("T,{0}|C,{1}|AT,{2}|C,{3}|AT,{3}", title, currency, (int)currencyType, currencyWin, (int)currencyType);
            AddAttachment(currencyType, currency);
            Builder(managerId, mailType, content);
        }
        /// <summary>
        /// 拍卖成功
        /// </summary>
        public MailBuilder( Guid managerId, string itemName, EnumCurrencyType currencyType, int currency)
            : this()
        {
            //AI(拍卖的物品)|C(货币数量)|AT(货币类型)
            string content = string.Format("N,{0}|M,{1}", itemName, currency);
            AddAttachment(currencyType, currency);
            Builder(managerId, EnumMailType.TransferSuccess, content);
        }
        /// <summary>
        /// 竞拍成功
        /// </summary>
        public MailBuilder(Guid managerId, int itemCode,string itemName, int currency)
            : this()
        {
            //恭喜你，以{0}{1}成功拍得[Attachment],请查收附件。
            //C(货币数量)|AT(货币类型)
            string content = string.Format("M,{0}|N,{1}", currency, itemName);
            AddAttachment(1, itemCode, false, 1, true);
            Builder(managerId, EnumMailType.TransferBuySuccess, content);
        }
        /// <summary>
        /// 拍卖失败 和下架
        /// </summary>
        public MailBuilder(Guid managerId, int itemCode, string itemName,EnumMailType mailType)
            : this()
        {
            //Month(月)|Day(日)
            string content = string.Format("N,{0}", itemName);
            AddAttachment(1, itemCode, false, 1, true);
            Builder(managerId, mailType, content);
        }
       
        /// <summary>
        /// 租借球员装备追回
        /// </summary>
        /// <param name="mailType"></param>
        /// <param name="managerId"></param>
        /// <param name="itemInfo"></param>
        public MailBuilder(EnumMailType mailType, Guid managerId, ItemUsedEntity itemInfo) : this()
        {
            //你租借的球员已经到期，球员身上的装备或勋章等被如数追回，请查收附件
            string content = "";
            AddAttachment(itemInfo);
            Builder(managerId, mailType, content);
        }
        //成功租借球员
        public MailBuilder(Guid managerId, EnumMailType mailType, int itemCode, EnumCurrencyType currencyType, int currency)
            : this()
        {
            //恭喜您，以{0}{1}成功租借{itemcode},你可以在球员管理中安排他上场比赛了，租借有效期止明日18点30分。
            //C(货币数量)|AT(货币类型)|AI(拍卖的物品)
            string content = string.Format("C,{0}|AT,{1}|AI,{2}", currency, (int)currencyType, itemCode);
            Builder(managerId, mailType, content);
        }

        /// <summary>
        /// 月卡奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mailType"></param>
        /// <param name="point"></param>
        /// <param name="day"></param>
        public MailBuilder(Guid managerId, EnumMailType mailType, int point, int day)
            : this()
        {
            string content = string.Format("N,{0}|D,{1}", point, day);
            AddAttachment(EnumCurrencyType.Point, point);
            Builder(managerId, mailType, content);
        }

        /// <summary>
        /// 欧洲杯竞猜
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mailType"></param>
        /// <param name="point"></param>
        /// <param name="homeName"></param>
        /// <param name="awayName"></param>
        public MailBuilder(Guid managerId, EnumMailType mailType, int point,string homeName,string awayName)
            : this()
        {
            string content = string.Format("H,{0}|A,{1}|P,{2}", homeName, awayName, point);
            AddAttachment(EnumCurrencyType.Point, point);
            Builder(managerId, mailType, content);
        }

        /// <summary>
        /// 封测返点
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mailType"></param>
        /// <param name="point"></param>
        /// <param name="rebate"></param>
        /// <param name="isMonthCard"></param>
        public MailBuilder(Guid managerId, EnumMailType mailType, int rebate,List<ConfigMallgiftbagEntity> prizeList)
            : this()
        {
            string content = "";
            int coin = 0;
            if (prizeList != null)
            {
                foreach (var item in prizeList)
                {
                    switch (item.PrizeType)
                    {
                        case 1:
                            rebate += item.ItemCount;
                           // AddAttachment(EnumCurrencyType.Point, item.ItemCount);
                            break;
                        case 2:
                            AddAttachment(EnumCurrencyType.Coin, item.ItemCount);
                            break;
                        case 3:
                            AddAttachment(item.ItemCount, item.SubType, false, 1);
                            break;
                    }
                }
            }
            if (rebate > 0)
                AddAttachment(EnumCurrencyType.Point, rebate);
            Builder(managerId, mailType, content);
        }

        /// <summary>
        /// 月卡奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mailType"></param>
        /// <param name="point"></param>
        /// <param name="day"></param>
        public MailBuilder(Guid managerId, int point, EnumMailType mailType)
            : this()
        {
            string content = string.Format("N,{0}", point);
            AddAttachment(EnumCurrencyType.Point, point);
            Builder(managerId, mailType, content);
        }

        /// <summary>
        /// 月卡奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mailType"></param>
        /// <param name="itemName"></param>
        /// <param name="prizeList"></param>
        /// <param name="addLuckyCoin"></param>
        /// <param name="addgameCurrency"></param>
        /// <param name="point"></param>
        public MailBuilder(Guid managerId, string itemName,int point,List<ConfigMallgiftbagEntity> prizeList, EnumMailType mailType, int addLuckyCoin, int addgameCurrency)
            : this()
        {
            string content = "";
            if (mailType == EnumMailType.GiftBagSuccess || mailType == EnumMailType.Share)
            {
                content = string.Format("I,{0}", itemName);
                if (prizeList != null)
                {
                    foreach (var item in prizeList)
                    {
                        switch (item.PrizeType)
                        {
                            case 1:
                                AddAttachment(EnumCurrencyType.Point, item.ItemCount);
                                break;
                            case 2:
                                AddAttachment(EnumCurrencyType.Coin, item.ItemCount);
                                break;
                            case 3:
                                AddAttachment(item.ItemCount, item.SubType, false, 1);
                                break;
                            case 4:
                                var itemCode = CacheFactory.LotteryCache.LotteryByLib(item.SubType);
                                AddAttachment(1, itemCode, false, 1);
                                break;
                            case 10:
                                AddAttachment(EnumCurrencyType.GoldBar, item.ItemCount);
                                break;
                            case 11: //幸运币 转盘用
                                AddAttachment(EnumCurrencyType.LuckyCoin, item.ItemCount);
                                break;
                            case 12: //游戏币  点球用
                                AddAttachment(EnumCurrencyType.GameCoin, item.ItemCount);
                                break;
                        }
                    }
                }
            }
            else
            {
                if (prizeList != null)
                {
                    int goldBar = 0;
                    foreach (var item in prizeList)
                    {
                        switch (item.PrizeType)
                        {
                            case 10:
                                AddAttachment(EnumCurrencyType.GoldBar, item.ItemCount);
                                goldBar += item.ItemCount;
                                break;
                        }
                    }
                    content = string.Format("I,{0}|N,{1}|G,{2}", itemName, point, goldBar);
                }
            }
            if(addLuckyCoin>0)
                AddAttachment(EnumCurrencyType.LuckyCoin, addLuckyCoin);
            if (addgameCurrency > 0)
                AddAttachment(EnumCurrencyType.GameCoin, addgameCurrency);
            Builder(managerId, mailType, content);
        }

        /// <summary>
        /// 分享成功
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="point"></param>
        /// <param name="coin"></param>
        /// <param name="prizeItem"></param>
        /// <param name="mailType"></param>
        public MailBuilder(Guid managerId,int point,int coin,Dictionary<int,int> prizeItem, EnumMailType mailType)
            : this()
        {
            if (point > 0)
                AddAttachment(EnumCurrencyType.Point, point);
            if (coin > 0)
                AddAttachment(EnumCurrencyType.Coin, coin);
            foreach (var i in prizeItem)
            {
                if (i.Key > 0 && i.Value > 0)
                    AddAttachment(i.Value, i.Key, false, 1);
            }
            Builder(managerId, mailType, "");
        }

        /// <summary>
        /// 友谊背包满了
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemInfo"></param>
        /// <param name="awayName"></param>
        public MailBuilder(Guid managerId, int itemCode, bool isBinding, EquipmentProperty equipmentProperty, string awayName,int itemCount = 1)
            : this()
        {
            //H(主队比分)|A(客队比分)|N(对手名)|AM(比赛id)|AW(胜负类型)
            string content = string.Format("N,{0}",awayName);
            if (equipmentProperty != null)
            {
                AddAttachment(itemCount, itemCode, isBinding, equipmentProperty);
            }
            else
            {
                AddAttachment(itemCount, itemCode, isBinding, 1);
            }
            Builder(managerId, EnumMailType.TourPrize, content);
        }

        /// <summary>
        /// 竞技场排名奖励
        /// </summary>
        public MailBuilder(Guid managerId, EnumMailType mailType, int rank, LotteryEntity lotteryEntity)
            : this()
        {
            string content = string.Format("R,{0}", rank);
            AddAttachment(1, lotteryEntity.PrizeItemCode, lotteryEntity.IsBinding, lotteryEntity.Strength);
            Builder(managerId, mailType, content);
        }

        /// <summary>
        /// 竞技场排名奖励
        /// </summary>
        /// <param name="managerID">经理ID</param>
        /// <param name="ranking">排名</param>
        /// <param name="goldCOINS">金币</param>
        /// <param name="prestige">声望</param>
        /// <param name="list">物品</param>
        public MailBuilder(Guid managerID, int ranking, int goldCOINS, int prestige, Dictionary<int, LotteryEntity> list)
            : this() 
        {
            string content = string.Format("L,{0}|P,{1}",ranking,prestige);
            AddAttachment(EnumCurrencyType.Coin, goldCOINS);
            foreach (var item in list)
            {
                AddAttachment(item.Key, item.Value.PrizeItemCode, item.Value.IsBinding, item.Value.Strength);
            }
            Builder(managerID, EnumMailType.Arena, content);
        }

        /// <summary>
        /// 点球排名奖励
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="ranking">排名</param>
        public MailBuilder(int ranking,Guid managerId)
            : this()
        {
            string content = string.Format("L,{0}", ranking);
            Builder(managerId, EnumMailType.AdSpores, content);
        }

        /// <summary>
        /// 球星启示录过关奖励
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="prizeItemCode">物品编码</param>
        /// <param name="itemCount">物品数量</param>
        /// <param name="isBinding">是否绑定</param>
        /// <param name="strength">强化等级</param>
        public MailBuilder(Guid managerId, int prizeItemCode,int itemCount,bool isBinding,int strength)
            : this()
        {
            AddAttachment(itemCount, prizeItemCode, isBinding, strength);
            Builder(managerId, EnumMailType.RevelationAwary, "");
        }

        /// <summary>
        /// 球星启示录过关奖励
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="prizeItemCode">物品编码</param>
        /// <param name="itemCount">物品数量</param>
        /// <param name="isBinding">是否绑定</param>
        /// <param name="strength">强化等级</param>
        /// <param name="markName">强化等级</param>
        public MailBuilder(Guid managerId, int prizeItemCode, int itemCount, bool isBinding, int strength,string markName)
            : this()
        {

            string content = string.Format("N,{0}", markName);
            AddAttachment(itemCount, prizeItemCode, isBinding, strength);
            Builder(managerId, EnumMailType.RevelationPassAwary, content);
        }

        /// <summary>
        /// 星座补偿奖励
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="prizeItemCode">物品编码</param>
        /// <param name="isBinding">是否绑定</param>
        /// <param name="strength">强化等级</param>
        public MailBuilder(int prizeType, Guid managerId, int itemCode, int itenCount, bool isBinding)
            : this()
        {
            if (prizeType == 1)
                AddAttachment(EnumCurrencyType.Point, itenCount);
            else
               AddAttachment(itenCount,itemCode,isBinding,0);
            Builder(managerId, EnumMailType.Constellation, "");
        }

        /// <summary>
        /// 竞技场战败通知
        /// </summary>
        /// <param name="managerID">经理ID</param>
        /// <param name="theChallenger">挑战人</param>
        /// <param name="ranking">排名</param>
        public MailBuilder(Guid managerID, string theChallenger, int ranking) : this() 
        {
            string content = string.Format("N,{0}|M,{1}",theChallenger,ranking);
            Builder(managerID, EnumMailType.ArenaFailure, content);
        }

        /// <summary>
        /// 九宫格排名奖励
        /// </summary>
        /// <param name="managerID">经理ID</param>
        /// <param name="ranking">排名</param>
        /// <param name="prestige">声望</param>
        /// <param name="entity">物品</param>
        public MailBuilder(Guid managerID, int ranking,  int prestige, LotteryEntity entity)
            : this()
        {
            string content = string.Format("L,{0}|P,{1}", ranking, prestige);
            if (entity != null)
            {
                AddAttachment(1, entity.PrizeItemCode, entity.IsBinding, entity.Strength);
                Builder(managerID, EnumMailType.SudokuAwary, content);
            }else
                Builder(managerID, EnumMailType.SudokuAwaryNotItem, content);
            
        }

        /// <summary>
        /// 豪门试炼通关奖励
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="gateName">关卡名字</param>
        /// <param name="itemNumber">物品数量</param>
        /// <param name="entity">物品</param>
        public MailBuilder(LotteryEntity entity, Guid managerId, string gateName, int itemNumber)
            : this()
        {
            string content = string.Format("N,{0}", gateName);
            if (entity != null)
                AddAttachment(itemNumber, entity.PrizeItemCode, entity.IsBinding, entity.Strength);

            Builder(managerId, EnumMailType.GiantsAwary, content);
        }

        /// <summary>
        /// 活跃度未领取奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coin"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemNumber"></param>
        /// <param name="isBinding"></param>
        public MailBuilder(Guid managerId,int coin ,int itemCode, int itemNumber,bool isBinding)
            : this()
        {
            string content = string.Format("C,{0}", coin);
            AddAttachment(EnumCurrencyType.Coin, coin);
            AddAttachment(itemNumber, itemCode,isBinding,1);
            Builder(managerId, EnumMailType.Active, content);
        }



        /// <summary>
        /// 世界挑战赛排名奖励
        /// </summary>
        public MailBuilder(Guid managerId, DateTime recordDate, int rank, LotteryEntity lotteryEntity)
            : this()
        {
            string content = string.Format("D,{0:yyyy-MM-dd}|R,{1}", recordDate, rank);
            AddAttachment(1, lotteryEntity.PrizeItemCode, lotteryEntity.IsBinding, lotteryEntity.Strength);
            Builder(managerId, EnumMailType.WorldChalengePrize, content);
        }

        ///// <summary>
        ///// 天梯排名奖励
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="period"></param>
        ///// <param name="rank"></param>
        ///// <param name="lotteries"></param>
        //public MailBuilder(LadderManagerhistoryEntity managerhistory, List<LotteryEntity> lotteries)
        //    : this()
        //{
        //    //Period(第几届)|Rank(排名)
        //    string content = string.Format("P,{0}|R,{1}", managerhistory.Season, managerhistory.Rank);
        //    foreach (var lotteryEntity in lotteries)
        //    {
        //        AddAttachment(1, lotteryEntity.PrizeItemCode, lotteryEntity.IsBinding, lotteryEntity.Strength);
        //    }
        //    Builder(managerhistory.ManagerId, EnumMailType.LadderPrize, content);
        //}

        public MailBuilder(EnumMailType mailType,Guid managerId,int season,int rank,DateTime recordDate)
            : this()
        {
            string content = "";
            if (mailType == EnumMailType.CrossLadderDailyPrize)
            {
                content = string.Format("Y,{0}|M,{1}|D,{2}|R,{3}", recordDate.Year, recordDate.Month, recordDate.Day, rank);
            }
            else
            {
                content = string.Format("P,{0}|R,{1}", season, rank);
            }
            Builder(managerId, mailType, content);
        }

        /// <summary>
        /// 天梯排名奖励
        /// </summary>
        /// <param name="managerhistory"></param>
        public MailBuilder(LadderManagerhistoryEntity managerhistory)
            : this()
        {
            //Period(第几届)|Rank(排名)
            string content = string.Format("P,{0}|R,{1}", managerhistory.Season, managerhistory.Rank);
            Builder(managerhistory.ManagerId, EnumMailType.LadderPrize, content);
        }

        ///// <summary>
        ///// 球探抽卡
        ///// </summary>
        //public MailBuilder(ScoutingRecordEntity scoutingRecord)
        //    : this()
        //{
        //    string content = "";
        //    AddAttachment(1, scoutingRecord.ItemCode, scoutingRecord.IsBinding, scoutingRecord.Strength);
        //    Builder(scoutingRecord.ManagerId, EnumMailType.ScoutingLottery, content);
        //}

        /// <summary>
        ///  杯赛排名奖励
        /// </summary>
        /// <param name="competitors"></param>
        /// <param name="s1"></param>
        /// <param name="s1c"></param>
        /// <param name="s2"></param>
        /// <param name="s2c"></param>
        /// <param name="s3"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        public MailBuilder(DailycupCompetitorsEntity competitors, string s1, int s1c, string s2, int s2c, string s3, int c1, int c2)
            : this()
        {
            var prize3 = s3.Replace("，", "*").Replace("|", "、");
            //S1(报名奖励物品)|S1C(报名奖励物品数量)|W(获胜场次)|S2(获胜奖励物品)|S2C(获胜奖励物品数量)|C1(获胜金币)|R(名次)|O(冠军杯积分)|S3(排名奖励)|C2(排名金币)|AT(货币类型)
            string content = string.Format("S1,{0}|S1C,{1}|W,{2}|S2,{3}|S2C,{4}|C1,{5}|AR,{6}|O,{7}|S3,{8}|C2,{9}|AT,{10}",
                s1, s1c, competitors.WinCount, s2, s2c, c1, competitors.Rank, competitors.PrizeScore, prize3, c2, (int)EnumCurrencyType.Coin);
            if (competitors.PrizeCoin > 0)
                AddAttachment(EnumCurrencyType.Coin, competitors.PrizeCoin);

            var allPrize = competitors.PrizeItems.Split('|');
            var prizeList = new Dictionary<int, int>();
            foreach (var prize in allPrize)
            {
                if (!string.IsNullOrEmpty(prize))
                {
                    var prizeInfo = prize.Split(',');
                    var prizeCode = Convert.ToInt32(prizeInfo[0]);
                    var prizeCount = Convert.ToInt32(prizeInfo[1]);
                    if (prizeCount > 0)
                    {
                        if (prizeList.ContainsKey(prizeCode))
                            prizeList[prizeCode] += prizeCount;
                        else
                            prizeList.Add(prizeCode, prizeCount);
                    }
                }
            }
            foreach (var item in prizeList)
            {
                AddAttachment(item.Value, item.Key, false, 0);
            }

            Builder(competitors.ManagerId, EnumMailType.DailycupPrize, content);
        }

        /// <summary>
        /// 杯赛排名奖励
        /// </summary>
        /// <param name="competitors"></param>
        /// <param name="s1"></param>
        /// <param name="s1c"></param>
        /// <param name="s2"></param>
        /// <param name="s2c"></param>
        /// <param name="c1"></param>
        public MailBuilder(DailycupCompetitorsEntity competitors, string s1, int s1c, string s2, int s2c, int c1)
            : this()
        {
            //S1(报名奖励物品)|S1C(报名奖励物品数量)|W(获胜场次)|S2(获胜奖励物品)|S2C(获胜奖励物品数量)|C1(获胜金币)|AT(货币类型)
            string content = string.Format("S1,{0}|S1C,{1}|W,{2}|S2,{3}|S2C,{4}|C1,{5}|AT,{6}",
                s1, s1c, competitors.WinCount, s2, s2c, c1, (int)EnumCurrencyType.Coin);
            if (competitors.PrizeCoin > 0)
                AddAttachment(EnumCurrencyType.Coin, competitors.PrizeCoin);

            var allPrize = competitors.PrizeItems.Split('|');
            var prizeList = new Dictionary<int, int>();
            foreach (var prize in allPrize)
            {
                if (!string.IsNullOrEmpty(prize))
                {
                    var prizeInfo = prize.Split(',');
                    var prizeCode = Convert.ToInt32(prizeInfo[0]);
                    var prizeCount = Convert.ToInt32(prizeInfo[1]);

                    if (prizeCount > 0)
                    {
                        if (prizeList.ContainsKey(prizeCode))
                            prizeList[prizeCode] += prizeCount;
                        else
                            prizeList.Add(prizeCode, prizeCount);
                    }
                }
            }
            foreach (var item in prizeList)
            {
                AddAttachment(item.Value, item.Key, false, 0);
            }

            Builder(competitors.ManagerId, EnumMailType.DailycupPrizeBase, content);
        }
        /// <summary>
        /// 杯赛竞猜结果
        /// </summary>
        /// <param name="gambleEntity"></param>
        public MailBuilder(DailycupGambleEntity gambleEntity)
            : this()
        {
            //P(第几届)|AD(轮次)|N(选手名)|P(押注钻石数)|C(奖励钻石数)
            string content = string.Format("P,{0}|AD,{1}|N,{2}|GP,{3}|C,{4}", gambleEntity.DailyCupId,
                                           gambleEntity.RoundLevel, gambleEntity.GambleManagerName, gambleEntity.GamblePoint,
                                           gambleEntity.ResultPoint);
            if (gambleEntity.Status == (int) EnumGambleStatus.Success)
            {
                Builder(gambleEntity.ManagerId, EnumMailType.DailycupGambleSuccess, content);
                AddAttachment(EnumCurrencyType.Point, gambleEntity.ResultPoint);
            }
            else
            {
                Builder(gambleEntity.ManagerId, EnumMailType.DailycupGambleFail, content);
            }
        }


        /// <summary>
        /// 世界挑战赛关卡奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="stageId"></param>
        public MailBuilder(Guid managerId, int stageId)
            : this()
        {
            string content = string.Format("P,{0}", stageId);
            Builder(managerId, EnumMailType.WorldChallengeStagePrize, content);
        }
        
        /// <summary>
        /// 巡回赛挂机奖励
        /// </summary>
        /// <param name="mailType"></param>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <param name="isBinding"></param>
        public MailBuilder(EnumMailType mailType,Guid managerId, int itemCode, bool isBinding,int itemCount=1)
            : this()
        {
            Builder(managerId, mailType, "");
            if (itemCode > 0)
            {
                AddAttachment(itemCount, itemCode, isBinding, 1);
            }
        }

        /// <summary>
        /// 精彩活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="activityName"></param>
        public MailBuilder(Guid managerId,string activityName)
            : this()
        {
            Builder(managerId, EnumMailType.ActivityExPrize, string.Format("N,{0}",activityName));
        }

        /// <summary>
        /// 世界杯点球活动奖励
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="mailType"></param>
        /// <param name="items"></param>
        public MailBuilder(Guid mid, EnumMailType mailType, List<LotteryEntity> items)
            :this()
        {
            Builder(mid, mailType, "");
            foreach (var item in items)
            {
                AddAttachment(1, item.PrizeItemCode, item.IsBinding, item.Strength);
            }
        }

        public MailBuilder(string title, string content)
            : this()
        {
            string mailcontent = string.Format("{0}|{1}", title,content);
            Builder(Guid.NewGuid(),EnumMailType.AdminSend, mailcontent);
        }

        /// <summary>
        /// 精彩活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        public MailBuilder(Guid managerId, int itemCode,bool isBinding,EnumMailType type)
            : this()
        {
            Builder(managerId, type,"");
            AddAttachment(1, itemCode, isBinding, 1);
        }

        /// <summary>
        /// 竞技场排名
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="type"></param>
        /// <param name="arenaType"></param>
        /// <param name="rank"></param>
        /// <param name="prizeList"></param>
        /// <param name="arenaCoin"></param>
        public MailBuilder(Guid managerId, EnumMailType type, int arenaType, int rank,
            List<ConfigArenaprizeEntity> prizeList, ref int arenaCoin)
            : this()
        {
            string arenaName = "";
            switch (arenaType)
            {
                case 1:
                    arenaName = "天空之城";
                    break;
                case 2:
                    arenaName = "重力感应";
                    break;
                case 3:
                    arenaName = "青春风暴";
                    break;
                case 4:
                    arenaName = "老兵不死";
                    break;
                case 5:
                    arenaName = "巨星闪耀";
                    break;
            }
            foreach (var item in prizeList)
            {
                switch (item.PrizeType)
                {
                    case 1: //钻石
                        AddAttachment(EnumCurrencyType.Point, item.PrizeNumber);
                        break;
                    case 2: //金币
                        AddAttachment(EnumCurrencyType.Coin, item.PrizeNumber);
                        break;
                    case 3: //指定物品
                        AddAttachment(item.PrizeNumber, item.PrizeCode, false, 1);
                        break;
                    case 4: //竞技币
                        arenaCoin = item.PrizeNumber;
                        break;
                }
            }
            string mailcontent = string.Format("N,{0}|M,{1}|A,{2}", arenaName, rank, arenaCoin);
            Builder(managerId, type, mailcontent);
        }

        public MailBuilder(Guid managerId, EnumMailType mailType, int value)
            : this()
        {
            if (mailType == EnumMailType.ChargeReturnDouble)
            {
                Builder(managerId, mailType, string.Format("C,{0}|AT,{1}", value, (int)EnumCurrencyType.Point));
                AddAttachment(EnumCurrencyType.Point, value);
            }
            else if (mailType == EnumMailType.CrowdRank || mailType == EnumMailType.CrossCrowdRank)
            {
                Builder(managerId, mailType, string.Format("R,{0}", value));
            }
        }

        public MailBuilder(Guid managerId, EnumMailType mailType, string value)
            : this()
        {
            if (mailType == EnumMailType.CrowdKill || mailType == EnumMailType.CrossCrowdKill)
            {
                Builder(managerId, mailType, string.Format("N,{0}", value));
            }
        }
        public MailBuilder(Guid managerId, EnumMailType mailType)
            : this()
        {
            Builder(managerId, mailType, "");
        }
        #endregion

        public MailInfoEntity MailInfo
        {
            get
            {
                if (_mail != null)
                {
                    if (_mail.MailAttachment == null)
                    {
                        _mail.Attachment = new byte[0];
                        _mail.HasAttach = false;
                    }
                    else
                    {
                        _mail.Attachment = SerializationHelper.ToByte(_mail.MailAttachment);
                        _mail.HasAttach = true;
                    }
                }
                return _mail;
            }
        }

        public int RecordId
        {
            get
            {
                if (_mail != null)
                    return _mail.Idx;
                return 0;
            }
        }

        public bool Save(DbTransaction transaction = null)
        {
            return Save("", transaction);
        }

        public bool Save(string zoneId,DbTransaction transaction = null)
        {
            if (_mail == null)
                return true;
            if (_mail.MailAttachment == null)
            {
                _mail.Attachment = new byte[0];
                _mail.HasAttach = false;
            }
            else
            {
                _mail.Attachment = SerializationHelper.ToByte(_mail.MailAttachment);
                _mail.HasAttach = true;
            }
            _mail.ExpiredTime = MailCore.Instance.GetExpiredTime(_mail.HasAttach, _mail.RowTime);
            var result = MailInfoMgr.Insert(_mail, transaction, zoneId);
            return result;
        }

        public bool AddAttachment(EnumCurrencyType currencyType, int currency)
        {
            if (_mail.MailAttachment == null)
                _mail.MailAttachment = new MailAttachmentEntity();
            switch (currencyType)
            {
                case EnumCurrencyType.Coin:
                    _mail.MailAttachment.AddAttachment(EnumAttachmentType.Coin, currency);
                    break;
                case EnumCurrencyType.Point:
                    _mail.MailAttachment.AddAttachment(EnumAttachmentType.Point, currency);
                    break;
                case EnumCurrencyType.Prestige:
                    _mail.MailAttachment.AddAttachment(EnumAttachmentType.Prestige, currency);
                    break;
                case EnumCurrencyType.BindPoint:
                    _mail.MailAttachment.AddAttachment(EnumAttachmentType.BindPoint, currency);
                    break;
                case EnumCurrencyType.GoldBar:
                    _mail.MailAttachment.AddAttachment(EnumAttachmentType.GoldBar, currency);
                    break;
                case EnumCurrencyType.LuckyCoin:
                    _mail.MailAttachment.AddAttachment(EnumAttachmentType.LuckyCoin, currency);
                    break;
                case EnumCurrencyType.GameCoin:
                    _mail.MailAttachment.AddAttachment(EnumAttachmentType.GameCoin, currency);
                    break;
                default:
                    return false;

            }
            _mail.HasAttach = true;
            return true;
        }

        public bool AddAttachmentSophisticate(int count)
        {
            if (_mail.MailAttachment == null)
                _mail.MailAttachment = new MailAttachmentEntity();
            _mail.MailAttachment.AddAttachment(EnumAttachmentType.Sophisticate, count);
            _mail.HasAttach = true;
            return true;
        }

        public bool AddAttachmentPrestige(int count)
        {
            if (_mail.MailAttachment == null)
                _mail.MailAttachment = new MailAttachmentEntity();
            _mail.MailAttachment.AddAttachment(EnumAttachmentType.Prestige, count);
            _mail.HasAttach = true;
            return true;
        }

        public bool AddAttachment(int count, int itemCode, bool isBinding, int strength)
        {
            if (_mail.MailAttachment == null)
                _mail.MailAttachment = new MailAttachmentEntity();
            if (itemCode == 0)
                return false;
            _mail.MailAttachment.AddAttachment(count, itemCode, isBinding, strength);
            _mail.HasAttach = true;
            return true;
        }

        public bool AddAttachment(int count, int itemCode, bool isBinding, int strength,bool isDeal)
        {
            if (_mail.MailAttachment == null)
                _mail.MailAttachment = new MailAttachmentEntity();
            if (itemCode == 0)
                return false;
            _mail.MailAttachment.AddAttachment(count, itemCode, isBinding, strength, isDeal);
            _mail.HasAttach = true;
            return true;
        }

        public bool AddAttachment(int count, int itemCode, bool isBinding, EquipmentProperty property)
        {
            if (_mail.MailAttachment == null)
                _mail.MailAttachment = new MailAttachmentEntity();
            if (itemCode == 0)
                return false;
            _mail.MailAttachment.AddAttachmentEquipment(count, itemCode, isBinding, property);
            _mail.HasAttach = true;
            return true;
        }

        public bool AddAttachment(ItemUsedEntity itemInfo)
        {
            if (_mail.MailAttachment == null)
                _mail.MailAttachment = new MailAttachmentEntity();
            if (itemInfo == null)
                return false;
            var dicItem = CacheFactory.ItemsdicCache.GetItem(itemInfo.ItemCode);
            if (dicItem == null)
                return false;
            switch (dicItem.ItemType)
            {
                case (int)EnumItemType.PlayerCard:
                    _mail.MailAttachment.AddAttachmentUsedItem(EnumAttachmentType.UsedPlayerCard, 1, itemInfo);
                    break;
                case (int)EnumItemType.Equipment:
                    _mail.MailAttachment.AddAttachmentUsedItem(EnumAttachmentType.UsedEquipment, 1, itemInfo);
                    break;
                case (int)EnumItemType.MallItem:
                    _mail.MailAttachment.AddAttachmentUsedItem(EnumAttachmentType.UsedMallItem, 1, itemInfo);
                    break;
            }
            _mail.HasAttach = true;
            return true;
        }

        void Builder(Guid managerId, EnumMailType mailType, string contentString)
        {
            _mail.ManagerId = managerId;
            _mail.MailType = (int)mailType;
            _mail.ContentString = contentString;
            _mail.IsRead = false;
            _mail.Status = 0;
            _mail.RowTime = DateTime.Now;
            _mail.ExpiredTime = MailCore.Instance.GetExpiredTime(_mail.HasAttach, _mail.RowTime);
        }
    }
}
