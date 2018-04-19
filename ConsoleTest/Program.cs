using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.Response;
using Games.NBall.Bll.Frame;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start.");
            Guid mid = new Guid("5F8BEB96-6110-4C92-AD35-A60E013147B1");
            var data = BuffDataCore.Instance().GetMembers(mid);
            data.Kpi.GetHashCode();
            ScoutingLoopTest();
            Console.WriteLine("end.");
            Console.ReadLine();
        }


        public static void ScoutingLoopTest()
        {
            for (int id = 1; id < 4; id++)
            {
                for (int i = 0; i < 50; i++)
                {
                    WriteLog2("id:{0},i:{1}...", id, i * 1000);
                    for (int j = 0; j < 1000; j++)
                    {
                        doLoop(id);
                    }
                    //doLoop(3,5,3,false);
                }

            }
        }

        static void doLoop(int scoutingId)
        {
            for (int count = 1; count < 6; count++)
            {
                doLoop(scoutingId, count);
            }
            /*
             //十连抽
            for (int count = 10; count < 11; count++)
            {
                doLoop(scoutingId, count);
            }
            */
        }

        static void doLoop(int scoutingId, int count)
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

        static void doLoop(int scoutingId, int count, int limitedOrangeCount, bool isFree)
        {
            WriteLog("");
            WriteLog("{0},{1},{2},{3}", scoutingId, count, limitedOrangeCount, isFree);
            ScoutingLottery(scoutingId, count, limitedOrangeCount, isFree);
        }

        static LotteryEntity ScoutingLottery(int scoutingId, int count, int limitedOrangeCount, bool isFree)
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
                    while (loopCount < 2000 && card.PlayerKpi > 87)
                    {
                        loopCount++;
                        lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                        card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                    }
                    WriteLog("while 1,loopCount:{0}", loopCount);
                    if (loopCount > 200)
                    {
                        WriteLog2("{0},{1},{2},{3},while 1,loopCount:{4}", scoutingId, count, limitedOrangeCount, isFree, loopCount);
                    }
                }
                //友情点抽卡十次必得80-84橙卡
                if (scoutingType == 3)
                {
                    loopCount = 0;
                    WriteLog("while 2,start");
                    while (loopCount < 2000 && !(card.PlayerKpi >= 80 && card.PlayerKpi <= 84))
                    {
                        loopCount++;
                        lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                        card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                    }
                    WriteLog("while 2,loopCount:{0}", loopCount);
                    if (loopCount > 200)
                    {
                        WriteLog2("{0},{1},{2},{3},while 2,loopCount:{4}", scoutingId, count, limitedOrangeCount, isFree, loopCount);
                    }
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
                    while (card.MallEffectType != (int)EnumMallEffectType.TheContract
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
                        if (loopCount == 2000)
                            break;
                    }
                    WriteLog("while 3,loopCount:{0}", loopCount);
                    if (loopCount > 200)
                    {
                        WriteLog2("{0},{1},{2},{3},while 3,loopCount:{4}", scoutingId, count, limitedOrangeCount, isFree, loopCount);
                    }
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
                        if (loopCount == 2000)
                            break;
                    }
                    WriteLog("while 4,loopCount:{0}", loopCount);
                    if (loopCount > 200)
                    {
                        WriteLog2("{0},{1},{2},{3},while 4,loopCount:{4}", scoutingId, count, limitedOrangeCount, isFree, loopCount);
                    }
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
                        if (loopCount == 2000)
                            break;
                    }
                    WriteLog("while 5,loopCount:{0}", loopCount);
                    if (loopCount > 200)
                    {
                        WriteLog2("{0},{1},{2},{3},while 5,loopCount:{4}", scoutingId, count, limitedOrangeCount, isFree, loopCount);
                    }
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

        static void WriteLog2(string format, params object[] arg)
        {
            var msg = string.Format(format, arg);
            Console.WriteLine(msg);
            LogHelper.Insert(msg, LogType.Error);
        }

        static void WriteLog(string format, params object[] arg)
        {
            return;
            var msg = string.Format(format, arg);
            //Console.WriteLine(msg);
            LogHelper.Insert(msg, LogType.Info);
        }

        static bool IsContract(DicItemEntity card)
        {
            if (card.ItemType == (int)EnumItemType.MallItem &&
                card.MallEffectType == (int)EnumMallEffectType.TheContract)
            {
                return true;
            }
            return false;
        }
    }
}
