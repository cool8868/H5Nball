using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Schedule;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Core.Activity;
using Games.NBall.Core.CrossCrowd;
using Games.NBall.Core.CrossLadder;
using Games.NBall.Core.Item;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Online;
using Games.NBall.Core.Rank;
using Games.NBall.Core.Statistic;
using Games.NBall.Core.Turntable;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using NUnit.Framework;

namespace Games.NBall.NUnitTest
{
     [TestFixture]
    public class Test1
    {
         [Test]
         public void Testw1()
         {
            //sign + "|" + billingId + "|" + price + "|" + playerId + "|" + serverId + "|" + goodsId + "|" + orderId + "|" + payTime + "|" + goodsNumber
            var sign = "f5adb9b5eb1fd40854ea694724186e4c";
            var billingId = "42336448584246664645744563464E4D48513D3D";
            var playerId = "c9cccd5811332272216ee6e6cdf2bd91";
            var price = "1";
            var payTime = "1468405081";
            var serverId = "1";
            var goodsId = "70101";
            var goodsNumber = "1";
            var orderId = "80302016071318141411292487";
            var cryptKey = "3pN9Z25DPp1smngw6g84b";
            orderId = "42336448584246664645744664464A4D48773D3D";
            var id = "c9cccd5811332272216ee6e6cdf2bd91";
            var money = 1;
            var time = 1468463343;
            serverId = "1";
            goodsId = "70101";
            goodsNumber ="1";
            var ext = "80302016071414580708529147";
            sign = "6377fa0b69a0ef2da66bc1351a4f2d38";
            var r = "";
            string signParam =
                ShareUtil.GetMD5("ext=80302016071318141411292487goodsId=70101goodsNumber=1id=c9cccd5811332272216ee6e6cdf2bd91money=1orderId=42336448584246664645744563464E4D48513D3DserverId=1time=14684633433pN9Z25DPp1smngw6g84b");
            if (sign == signParam)
            {
                r = "09";

            }
            long time1 = ConvertHelper.ConvertToLong(payTime, 0);
            DateTime sourceTime = ShareUtil.GetTime(time1*1000);
            DateTime nowTime = DateTime.Now;
            //检查时间是否过期
             long _timeout30min = 1800000;
            if (sourceTime.AddSeconds(_timeout30min) < nowTime ||
                sourceTime.AddSeconds(-_timeout30min) > nowTime)
            {
                //记录详细的错误日志
                r= "{\"code\":-1,\"msg\":\"响应超时\"}";
            }
            r= ""+MallCore.Instance.BuyPointShipments(playerId, ext, orderId, money, 70101).Code;
            var rr = r;
             var rrr = rr;
         }
         [Test]
         public void Testt2()
         {
             var url = "http://open.ibeargame.com/SetProduct";
             var postDataStr = "";
            try
             {
                 HttpWebRequest request =
                     (HttpWebRequest)WebRequest.Create(url);
                 request.Method = "POST";
                 request.ContentType = "application/x-www-form-urlencoded";
                 request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                 Stream myRequestStream = request.GetRequestStream();
                 StreamWriter myStreamWriter = new StreamWriter(myRequestStream);
                 myStreamWriter.Write(postDataStr);
                 myStreamWriter.Close();

                 HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                 Stream myResponseStream = response.GetResponseStream();
                 StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                 var result = myStreamReader.ReadToEnd();
                 myStreamReader.Close();
                 myResponseStream.Close();
                 
                 var r = result;
             var rr = r;
             var rrr = rr;
             }
             catch (Exception ex)
             {
                 SystemlogMgr.Error("HttpPost请求", ex);
                 
             }
           
         }
         [Test]
         public void Testt3()
         {
             var r = ShareUtil.GetMD5("appId=16&data=[{\"id\":70101,\"name\":\"50钻石\",\"price\":500,\"desc\":\"50钻石\"},{\"id\":70102,\"name\":\"200钻石\",\"price\":2000,\"desc\":\"200钻石\"},{\"id\":70103,\"name\":\"1000钻石\",\"price\":10000,\"desc\":\"1000钻石\"},"+
"{\"id\":70104,\"name\":\"2000钻石\",\"price\":20000,\"desc\":\"2000钻石\"},{\"id\":70105,\"name\":\"5000钻石\",\"price\":50000,\"desc\":\"5000钻石\"},{\"id\":70001,\"name\":\"月卡\",\"price\":3000,\"desc\":\"月卡\"},{\"id\":70009,\"name\":\"新手礼包\",\"price\":100,\"desc\":\"新手礼包\"},"+
"{\"id\":70010,\"name\":\"超值礼包\",\"price\":1800,\"desc\":\"超值礼包\"},{\"id\":70011,\"name\":\"周末优惠礼包\",\"price\":5800,\"desc\":\"周末优惠礼包\"},{\"id\":70012,\"name\":\"强化礼包\",\"price\":6000,\"desc\":\"强化礼包\"},{\"id\":70013,\"name\":\"建队礼包\",\"price\":28800,\"desc\":\"建队礼包\"},{\"id\":70014,\"name\":\"限时礼包\",\"price\":19800,\"desc\":\"限时礼包\"}]e4cd0bd43bc73a811e99e30f077b29be");
             var rr = r;
             var rrr = rr;

         }
         [Test]
         public void Testt4()
         {
             //var entity = new LadderInfoEntity();
             //entity.FightList=new List<LadderManagerEntity>();
             //var list=new List<LadderManagerEntity>();
             //var e = new LadderManagerEntity();
             //e.Score = 1000;
             //list.Add(e);
             //entity.FightList = list;
             //LadderThread.Instance.BuildBot(entity);
             //var r = entity;
             //var rr = r;
             //var rrr = rr;

         }
         [Test]
         public void Testt5()
         {

             var r = MallCore.Instance.GetBuyPointInfo(new Guid("802631D7-8F8D-4950-BED7-A62600E5692B"));
             var rr = r;
             var rrr = rr;

         }
         [Test]
         public void Testt6()
         {
             //var r = MallCore.Instance.BuyPoint(new Guid("418A8F6F-7868-4A8D-A008-A62600E5BF1E"), 70016);
              var r = MallCore.Instance.BuyPointShipments("418A8F6F-7868-4A8D-A008-A62600E5BF1E", "80302016081016300893079085", "2016081003", 598, 70016);
             var rr = r;
             var rrr = rr;
         }
         [Test]
         public void Testt7()
         {
            // var r = CrossLadderCore.Instance.GetManagerInfo("a8s2", new Guid("E351355A-4C95-47D4-BAEA-A62600E5C782"));

             var r = CrossCrowdCore.Instance.GetManagerInfo("a8s2", new Guid("E351355A-4C95-47D4-BAEA-A62600E5C782"));
             var rr = r;
             var rrr = rr;

         }
         [Test]
         public void Testt8()
         {//"yyyy/MM/dd"
          //   var f = new DateTimeFormatInfo().GetFormat((Type)"yyyy/MM/dd");
            DateTime dt = Convert.ToDateTime("1985/12/31");
            DateTime dt2 = Convert.ToDateTime("1992/9/26");
            var r = 0;

             if (dt < dt2)
                 r = 1;
             var rr = dt2;
             var rrr = rr;

         }
         [Test]
         public void Testt9()
         {
             var r=MallCore.Instance.UseItem(new Guid("418a8f6f-7868-4a8d-a008-a62600e5bf1e"), new Guid("0a58229b-4108-4b77-bb39-a664013075fd"),1);
             var rr = r;
             var rrr = rr;

         }
         [Test]
         public void Testt10()
         {

             //var r = ScoutingCore.Instance.ScoutingLottery(new Guid("212DCEF2-3AAF-454D-A632-A62600E5BDFA"),2,false,10 );
             
             //var rr = r;
             //var rrr = rr;

             var list =NbManagerMgr.GetAll();
             var r = 0;
           
             foreach (var ent in list)
             {
                 var str = "";
                 str = ent.Account;
                 if (!string.IsNullOrEmpty(ent.Account) && !str.Contains("Bot"))
                 {
                     ItemCore.Instance.GMAddItem("", ent.Idx, 310166, 20, 1,false );
                 }
             }
             var rr = r;
             var rrr = rr;

         }
         [Test]
         public void Testt11()
         {
             var r = ActivityExThread.Instance.GetUserActivityEx(new Guid("418A8F6F-7868-4A8D-A008-A62600E5BF1E"), 46);
             var rr = r;
             var rrr = rr;
             //for (int i = 0; i<100; i++)
             //{
             
             //var truntable = new TurntableFrame(new Guid("95BC6B06-942C-4683-87FD-A62600E5CB0C"));
             //truntable.Save(true);
             //bool isAddSuccess = false;
             //TurntableManagerMgr.AddSystemProduceLuckyCoin(new Guid("95BC6B06-942C-4683-87FD-A62600E5CB0C"), ref isAddSuccess);
             //}
         }
    }
}
