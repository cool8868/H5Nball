using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Activity;
using Games.NBall.Core.FriendShip;
using Games.NBall.Core.Item;
using Games.NBall.Core.League;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.Response.Scouting;
using NUnit.Framework;
using Games.NBall.Common;

namespace Games.NBall.NUnitTest.Manager
{
    [TestFixture]
    public class ScoutingTest
    {
        [Test]
        public void ActivityExPrizeReceiveTest()
        {
            Receive();
        }

        MessageCode Receive()
        {
            var exRecord = new ActivityexCountrecordEntity();
            exRecord.Idx = 1;
            exRecord.ZoneActivityId = 19;
            exRecord.ExcitingId = 32;
            exRecord.GroupId = 2;
            exRecord.ExData = 6000;
            exRecord.CurData = 6000;
            exRecord.ExStep = 1;
            exRecord.AlreadySendCount = 0;

            var detail = new TemplateActivityexdetailEntity(105, 32, 2, 1, 5000, 0, 0, 0, 1, 310161);
            int prizeItemcode = detail.EffectValue;
            int prizeItemCount = exRecord.ExData - exRecord.AlreadySendCount;
            int itemCount = 0;
            var group = new TemplateActivityexgroupEntity(103, 32, 2, 1, 108, 3, false, 0, 5000);
            if (group.ExRequireId == (int)EnumActivityExRequire.ChargeCount) //充值计数
            {
                itemCount = prizeItemCount / group.RankCount;
                exRecord.AlreadySendCount += (itemCount * group.RankCount);
                if (detail.EffectRate > 0)
                {
                    prizeItemCount = itemCount * detail.EffectRate;
                }
            }
            else
            {
                exRecord.AlreadySendCount += prizeItemCount;
            }
            MessageCode code = MessageCode.Success;
            ItemPackageFrame package = null;
            ScoutingGoldbarEntity goldBarManager = null;
            bool isInsert = false;
            if (prizeItemcode > 10000) //物品
            {
                package = ItemCore.Instance.GetPackage(new Guid("A995C3B5-4CA2-4348-A1B0-A60E013163A5"), EnumTransactionType.ActivityExPrize);
                if (package == null || package.PackageSize < prizeItemCount)
                    return MessageCode.ItemPackageFull;
                code = package.AddItems(prizeItemcode, prizeItemCount);
                if (code != MessageCode.Success)
                    return code;
            }
            return MessageCode.Success;
        }


        [Test]
        public void Scouting()
        {
            LeagueCore.Instance.StarLeague(new Guid("A7E6880B-F73A-4992-BB84-A60E013147E1"),1);
            //LotteryCache.Instance.LotteryByLib(113);

            //ScoutingCore.Instance.ScoutingLottery(new Guid("5A08D281-BF9A-40F4-BBC9-A537010C2C07"), 1, false, 10);

            //var leagueRecordId = new Guid("CCFFF0C1-E4D2-4D7B-B172-A58400C6657F");
            //var managerId = new Guid("F94334E2-1C6D-41BF-A2E5-A57700B4B76A");

            //var rankList = LeagueRankMgr.GetByLeagueRecordId(leagueRecordId);
            //LeagueProcess.Instance.CalculateRank(rankList, managerId, leagueRecordId, true);


            //for (int i = 0; i < 11; i++)
            //{
            //    ScoutingCore.Instance.ScoutingLottery(new Guid("31F0198F-B610-454C-8565-A60E01313CF6"), 1, false, 1, false);
            //}

        }

        [Test]
        public void ScoutingLoopTest()
        {
            for (int id = 1; id < 4; id++)
            {
                for (int i = 0; i < 500; i++)
                {
                    doLoop(id);
                }
                
            }
        }

        void doLoop(int scoutingId)
        {
            for (int count = 5; count < 6; count++)
            {
                doLoop(scoutingId, count);
            }

        }

        void doLoop(int scoutingId, int count)
        {
            for (int limit = 0; limit < 5; limit++)
            {
                doLoop(scoutingId, count, limit, false);
            }
            for (int limit = 0; limit < 5; limit++)
            {
                doLoop(scoutingId, count, limit, true);
            }
        }

        void doLoop(int scoutingId, int count, int limitedOrangeCount, bool isFree)
        {
            WriteLog("");
            WriteLog("{0},{1},{2},{3}", scoutingId, count, limitedOrangeCount, isFree);
            ScoutingLottery(scoutingId, count, limitedOrangeCount, isFree);
        }

        LotteryEntity ScoutingLottery(int scoutingId,int count, int limitedOrangeCount, bool isFree)
        {
            bool isTen = count == 10; //是否十连抽
            var configScouting = CacheFactory.ScoutingCache.GetEntity(scoutingId);
            if (configScouting == null)
                return null;
            var scoutingType = configScouting.Type;
            

            LotteryEntity lottery = null;
            List<int> cardList = null;
            List<int> limitedCardList = new List<int>();
            if (isTen)
            {
                if (!configScouting.HasTen)
                    return null;
                lottery = CacheFactory.LotteryCache.ScoutingTen(scoutingType, configScouting.OrangeLib,
                    configScouting.LowLib, out cardList, limitedOrangeCount, out limitedCardList);
            }
            else
            {
                lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
            }
            if (lottery == null)
                return null;
            int loopCount = 0;
            if (!isTen) //新手引导点券抽卡
            {
                var card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                if (isFree)
                {
                    scoutingType = 4; //免费抽卡卡库
                    loopCount = 0;
                    WriteLog("while 1,start");
                    //免费抽卡不能抽到87以上能力值的卡
                    while (loopCount<200 && card.PlayerKpi > 87)
                    {
                        loopCount++;
                        lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                        card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                    }
                    WriteLog("while 1,loopCount:{0}", loopCount);
                }
                //友情点抽卡十次必得80-84橙卡
                if (scoutingType == 3)
                {
                    loopCount = 0;
                    WriteLog("while 2,start");
                    while (loopCount < 200 && !(card.PlayerKpi >= 80 && card.PlayerKpi <= 84))
                    {
                        loopCount++;
                        lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                        card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                    }
                    WriteLog("while 2,loopCount:{0}", loopCount);
                }

                //金币抽卡十次必得80-84橙卡碎片
                if (scoutingType == 1)
                {
                    var playerItemcode = 0;
                    var playerItem = new DicItemEntity();
                    if (IsContract(card))
                    {
                        playerItemcode = CacheFactory.ItemsdicCache.GetTheContractItemCode(card.ItemCode);
                        playerItem = ItemsdicCache.Instance.GetItem(playerItemcode);
                    }
                    loopCount = 0;
                    WriteLog("while 3,start");
                    while (card.MallEffectType != (int) EnumMallEffectType.TheContract
                           ||
                           (IsContract(card) &&
                            !(playerItem.PlayerKpi >= 80 && playerItem.PlayerKpi <= 84)))
                    {
                        loopCount++;
                        lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                        card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);

                        if (IsContract(card))
                        {
                            playerItemcode = CacheFactory.ItemsdicCache.GetTheContractItemCode(card.ItemCode);
                            playerItem = ItemsdicCache.Instance.GetItem(playerItemcode);
                            if (playerItem == null)
                            {
                                WriteLog("playerItem is null,card.ItemCode={0},playerItemcode={1}", card.ItemCode, playerItemcode);
                            }
                        }
                        if (loopCount == 200)
                            break;
                    }
                    WriteLog("while 3,loopCount:{0}",loopCount);
                }

                if (configScouting.Type == 2 && scoutingType == 4)
                {
                    //第一次必得托雷斯
                    lottery.PrizeItemCode = 130153;
                    var itemstring = lottery.ItemString.Split(',');
                    itemstring[1] = lottery.PrizeItemCode.ToString();

                    lottery.ItemString = string.Join(",", itemstring);
                }
                //钻石抽卡十次必得84-87橙卡
                if (scoutingType == 2)
                {
                    loopCount = 0;
                    WriteLog("while 4,start");
                    while (!(card.PlayerKpi >= 84 && card.PlayerKpi <= 87))
                    {
                        loopCount++;
                        lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                        card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                        if(loopCount==200)
                            break;
                    }
                    WriteLog("while 4,loopCount:{0}", loopCount);
                }
                //C罗、梅西外每天只能出3张89以上的橙卡
                if (limitedOrangeCount >= 3)
                {
                    loopCount = 0;
                    WriteLog("while 5,start");
                    while (card.PlayerKpi >= 89 || card.LinkId == 30001 || card.LinkId == 30002)
                    {
                        loopCount++;
                        lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                        card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                        if (loopCount == 200)
                            break;
                    }
                    WriteLog("while 5,loopCount:{0}", loopCount);
                }
                else
                {
                    if (card.PlayerKpi >= 89 && (card.LinkId != 30001 || card.LinkId != 30002))
                    {
                        limitedCardList = new List<int>();
                        limitedCardList.Add(card.ItemCode);
                    }
                }
            }
            return lottery;
        }

        void WriteLog(string format,params object[] arg)
        {
            var msg = string.Format(format, arg);
            Console.WriteLine(msg);
            LogHelper.Insert(msg,LogType.Info);
        }

        bool IsContract(DicItemEntity card)
        {
            if (card.ItemType == (int) EnumItemType.MallItem &&
                card.MallEffectType == (int) EnumMallEffectType.TheContract)
            {
                return true;
            }
            return false;
        }
    }
}
