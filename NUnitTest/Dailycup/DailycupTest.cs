using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Schedule;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.CrossLadder;
using Games.NBall.Core.Dailycup;
using Games.NBall.Core.FriendShip;
using Games.NBall.Core.League;
using Games.NBall.Entity;
using Newtonsoft.Json;
using NUnit.Framework;
using Games.NBall.Common;
using Games.NBall.Core.Gamble;
using Games.NBall.Core.Item;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Online;
using Games.NBall.Core.Statistic;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response.Auction;

namespace Games.NBall.NUnitTest.Manager
{
    [TestFixture]
    public class DailycupTest
    {

        [Test]
        public void ActivityPrizeTest()
        {
            var list = CacheFactory.ActivityCache.GetPrize(4, 4);
            TestUtil.WriteObj(list);
        }

        [Test]
        public void GetLadderInfoTest()
        {
            try
            {
                var list = CrossladderManagerMgr.GetDailyHonor();
                if (list != null)
                {
                    foreach (var entity in list)
                    {
                        LadderManagerMgr.AddDailyHonor(entity.ManagerId, entity.NewlyHonor, entity.NewlyLadderCoin, null, entity.SiteId);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SendDailyHonor", ex);
            }   
        }
        
        [Test]
        public void Dailycup()
        {

            var r = MallCore.Instance.BuyPoint(new Guid("72DC0369-2033-46D7-AD64-A62600E5C112"), 70101);
            var rr = r;
            var rrr = rr;

        }
        [Test]
        public void Testw7()
        {
            var r = StatisticThread.Instance.JobGetTodayKpi();

            var rr = r;
            var rrr = rr;
        }
         [Test]
        public void Testw6()
        {
            var r = MallCore.Instance.UseItem(new Guid("418A8F6F-7868-4A8D-A008-A62600E5BF1E"), new Guid( "2002e113-09cb-48bf-8f74-a63d010c85ad"), 1);
            
            var rr = r;
            var rrr = rr;
        }
        [Test]
        public void Testw5()
        {
            var r=ItemCore.Instance.GMAddItem("", new Guid("418A8F6F-7868-4A8D-A008-A62600E5BF1E"), 310144, 1, 1, false);
            
            var rr = r;
            var rrr = rr;
        }
        [Test]
        public void Testw4()
        {
            var r = MailCore.Instance.AttachmentReceive(new Guid("72DC0369-2033-46D7-AD64-A62600E5C112"), 1364);
            var rr = r;
            var rrr = rr;
        }

        [Test]
        public void Testw3()
        {
            var r = MailCore.Instance.GetMailData(new Guid("72DC0369-2033-46D7-AD64-A62600E5C112"));
            var rr = r;
            var rrr = rr;
        }

        [Test]
        public void Testw2()
        {
            var r = MallCore.Instance.BuyPointShipments("418A8F6F-7868-4A8D-A008-A62600E5BF1E", "80302016070814351240583240", "", 198, 70014);
            var rr = r;
            var rrr = rr;
        }

        [Test]
       public  void Testw1()
        {
             try
             {
                 var list = StatisticKpiMgr.GetbyDate(0, DateTime.Now.AddDays(-5), DateTime.Now);
                 var r = list;
                 var rr = r;
             }
             catch (Exception e)
             {

                 var r = e;
                 var ee = r;
             }
         
        }

        [Test]
        public void AddExchange()
        {
            var m = ExchangeCore.Instance.Exchange(new Guid("802631D7-8F8D-4950-BED7-A62600E5692B"), "666kkk",
                "h5_qunhei");
            var m2 = m;
            var m3 = m2;
        }

        [Test]
        public void SendPrize()
        {

            try
            {
                var package = ItemCore.Instance.GetPackage(new Guid("731DF1FF-B4ED-4932-989B-A60E01314832"), EnumTransactionType.AdminAddItem, AllZoneinfoMgr.GetById(1004).ZoneName);
                var result = package.AddItems(120006, 1, 1, false,false);
                if (result == MessageCode.Success)
                {
                    bool isSuccess = package.Save();
                    if (isSuccess)
                    {
                        package.Shadow.Save();
                    }
                }


            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Admin AddItems", ex);
            }



        }

        [Test]
        public void OpenGamble()
        {
            try
            {

            var time1 = DateTime.ParseExact("20160601", "yyyyMMdd", null);
            var time2 = DateTime.ParseExact("20160611", "yyyyMMdd", null);
            var starTime = Convert.ToDateTime(time1);
            var endTime = Convert.ToDateTime(time2);
            var list = StatisticKpiMgr.GetbyDate(1, starTime, endTime);
            foreach (var VARIABLE in list)
            {
                
            }
            }
            catch (Exception ex)
            {

            }
        }
        [Test]
        public  void SetExchange()
        {
            //兑换码礼包添加
            var entity = new ExchangeInfoEntity();
            entity.ExchangeType = 1;
            entity.ZoneName = 0;
            entity.Account = "";
            entity.ManagerId = Guid.Empty;
            entity.AtZoneId = 0;
            entity.PackId = 351;
            entity.BatchId = 17001;
            entity.Status = 0;
            var time = DateTime.Now;
            entity.UpdateTime = time;
            entity.RowTime = time;
            entity.PlatformCode = "h5_a8";
            string a = "abcdefghijklmnopqrstuvwxyz";
            var b = a.ToUpper() + "123456789";
            var strs = b.ToCharArray();
            var random = new Random();

            for (int j = 0; j < 10000; j++)
            {

                string exchangeId = "";
                for (int i = 0; i < 8; i++)
                {
                    int s = 0;
                    s = random.Next(35);
                    var c = strs[s];
                    exchangeId += c;
                }
                exchangeId += entity.PackId.ToString();
                entity.Idx = exchangeId;
                ExchangeInfoMgr.Insert(entity);
            }
        }
        [Test]
        public void Interface()
        {
            //var r=CSDKinterface.Instance.IsTxVip(new Guid("731DF1FF-B4ED-4932-989B-A60E01314832"));
            //var resp = r;
            
            var zonid=ShareUtil.ZoneId;
            var zonename = ShareUtil.ZoneName;
            var zhoneno = ShareUtil.ZoneNumber;
            var pfname = ShareUtil.Name;
            var pfcode = ShareUtil.PlatformCode;
            var pf = ShareUtil.PlatformZoneName;
            var r = zonid + zonename + zhoneno + pfname + pfcode + pf;
        }


    }
}
