using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Coach;
using Games.NBall.Core.Dailycup;
using Games.NBall.Core.Friend;
using Games.NBall.Core.FriendShip;
using Games.NBall.Core.Gamble;
using Games.NBall.Core.Information;
using Games.NBall.Core.Item;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.League;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.ManagerSkill;
using Games.NBall.Core.Match;
using Games.NBall.Core.Rank;
using Games.NBall.Core.Revelation;
using Games.NBall.Core.SkillCard;
using Games.NBall.Core.Task;
using Games.NBall.Core.Teammember;
using Games.NBall.Core.Transfer;
using Games.NBall.Core.UerCrossPara;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response.Item;
using Games.NBall.Entity.Response.Match;
using Newtonsoft.Json;
using NUnit.Framework;
using Games.NBall.Common;
using Games.NBall.Core.Turntable;

namespace Games.NBall.NUnitTest.Manager
{
    [TestFixture]
    public class ManagerTest
    {
        [Test]
        public void GetManagerInfoTest()
        {
            //CacheFactory.LotteryCache.LotteryByLib(1);
            //var response = MatchDataCore.Instance.GetMatchProcess(new Guid("85C0C3FB-4D44-4121-A502-A6DC00CD621A"), 9);
            //var sss = response;

            //FileStream fs;
            //string filename = @"C:\Users\Winner\Desktop\111.bin";
         
            //  //否则创建文件
            //fs = new FileStream(filename, FileMode.Create);
            ////数据保存到磁盘中
            //BinaryWriter bw = new BinaryWriter(fs);
            //foreach (byte b in response)
            //{

            //    bw.Write(b);
            //}
            //bw.Flush();
            
            //bw.Close();
            //fs.Close();

            //var response = MallCore.Instance.BuyPoint(new Guid("31F0198F-B610-454C-8565-A60E01313CF6"), 70101);

           // MallCore.Instance.BuyPointShipments("sc", "001d524a-7aeb-4266-8c4b-a6cc005180bc", "001d524a-7aeb-4266-8c4b-a6cc005180bc", 1, 70009);
           // var s = ShareUtil.GetMD5("141910470009,1000217e863fc8ecc10dc17c4312bb734edd0").ToLower();
            // var ss = s;
            DateTime date = DateTime.Now;
            var managerId = new Guid("31F0198F-B610-454C-8565-A60E01313CF6");
            var coachFrame = CoachCore.Instance.GetFrame(managerId);
            var coachCode = coachFrame.CoachUpgarde(1);
           // CrossActivityCore.Instance.Prize(managerId, "a8s1");
            // ScoutingCore.Instance.ScoutingLottery(managerId, 99, false, 10, false);
            //RevelationNewCore.Instance.StartMark(managerId, 1);
           // RevelationNewCore.Instance.RevelationGetShopInfo(managerId);
        }

        [Test]
        public void GetPKInfoTest()
        {

            var response = MallCore.Instance.BuyPointShipments("31F0198F-B610-454C-8565-A60E01313CF6", "001d524a-7aeb-4266-8c4b-a6cc005180bd",
                "001d524a-7aeb-4266-8c4b-a6cc005180bd", 568, 70025);
        }

        [Test]
        public void GetTest()
        {

            var managerId = new Guid("00F55DD5-1D8E-4401-A3DB-A60E01316928");

            MallCore.Instance.BuyPointShipments("luobeng", "80302017011617542774596175", "80302017011617542774596175", 500,
                70105);
           


        }

        void HandleOrangeCard(Guid managerId, int itemCode)
        {
            var item = CacheFactory.ItemsdicCache.GetItem(itemCode);
            if (item != null && item.ItemType == (int)EnumItemType.PlayerCard)
            {
                if (item.PlayerCardLevel == (int)EnumPlayerCardLevel.Orange)
                {
                    
                }
                else if (item.PlayerCardLevel == (int)EnumPlayerCardLevel.BlackGold)
                {
                    
                }
                else if (item.PlayerCardLevel == (int)EnumPlayerCardLevel.Purple)
                {
                    
                }
                //欧洲之星
                if (ActivityExThread.Instance.IsActivity(EnumActivityExEffectType.EuropeTheStars, 0, 0))
                {
                    var player = CacheFactory.PlayersdicCache.GetPlayer(item.LinkId);
                    if (player != null)
                    {
                        if (player.KpiLevel.Trim() == "A")
                        {
                            ActivityExThread.Instance.EuropeTheStars(managerId, EnumActivityExRequire.ScoutingEurope);
                        }
                        else if (player.KpiLevel.Trim() == "A+" || player.KpiLevel.Trim() == "S")
                        {
                            ActivityExThread.Instance.EuropeTheStars(managerId, EnumActivityExRequire.ScoutingHugeEurope);
                        }
                    }
                }
            }
        }


        [Test]
        public void Test11()
        {
           DateTime date = DateTime.Now;

           for (int i = 1; i < 41; i++)
           {
               int bs = 2; //基础属性
               double up = 0.4;   //升级属性
               string sk = "A001";     //技能Code
               double buf = bs + (i * up );
               string name = "变向加速";     //技能名
               string buffmemo = "过人时增加速度和控球属性" + buf + "%";         //buff属性
               decimal mixDiscount = decimal.Parse("0.2000");
               int getLevel = 3;              //经理开放等级
               int actType = 2;            //技能类型
               string actTypeMemo = "过人";            //技能类型
               int skillClass = 1;             //技能品质
               string skillClassMemo = "绿";             //技能品质
               string Cd = "18分钟";              //技能CD
               string Memo = "带球者身边有风掠过";              //技能描述
               int skillId = 10100 + i;                //技能ID


               InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                   skillClassMemo, Cd, Memo, skillId, date, i);
           }

            for (int i = 1; i < 41; i++)
            {
                int bs = 1; //基础属性
                double up = 0.1;   //升级属性
                string sk = "A002";     //技能Code
                string name = "假射";     //技能名
                double buf = bs + (i * up);
                string buffmemo = "射门时" + buf + "%概率令门将倒地";         //buff属性
                decimal mixDiscount = decimal.Parse("0.2000");
                int getLevel = 3;              //经理开放等级
                int actType = 1;            //技能类型
                string actTypeMemo = "射门";            //技能类型
                int skillClass = 1;             //技能品质
                string skillClassMemo = "绿";             //技能品质
                string Cd = "18分钟";              //技能CD
                string Memo = "连续两次进行射门动作后，将球射出";              //技能描述
                int skillId = 10200 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 2; //基础属性
                double up = 0.6;   //升级属性
                string sk = "A003";     //技能Code
                string name = "破坏";     //技能名
                double buf = bs + (i * up);
                string buffmemo = "防守时有" + buf + "%概率将球立刻破坏出边线";         //buff属性
                decimal mixDiscount = decimal.Parse("0.2000");
                int getLevel = 5;              //经理开放等级
                int actType = 3;            //技能类型
                string actTypeMemo = "防守";            //技能类型
                int skillClass = 1;             //技能品质
                string skillClassMemo = "绿";             //技能品质
                string Cd = "25分钟";              //技能CD
                string Memo = "大脚破坏出边线，对方界外球";              //技能描述
                int skillId = 10300 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 3; //基础属性
                int bs1 = 2;
                double up = 0.3;   //升级属性
                double up1 = 0.2;
                string sk = "A004";     //技能Code
                string name = "推土机";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i* up1);
                string buffmemo = "过人时增加力量属性" + buf + "%，同时降低对方力量属性"+buf1+"%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.2500");
                int getLevel = 8;              //经理开放等级
                int actType = 2;            //技能类型
                string actTypeMemo = "过人";            //技能类型
                int skillClass = 2;             //技能品质
                string skillClassMemo = "蓝";             //技能品质
                string Cd = "25分钟";              //技能CD
                string Memo = "带球同时蓄力之后冲向防守球员将其撞倒";              //技能描述
                int skillId = 10400 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 2; //基础属性
                double up = 0.4;   //升级属性
                string sk = "A005";     //技能Code
                string name = "一脚出球";     //技能名
                double buf = bs + (i * up);
                string buffmemo = "传球" + buf + "%概率令防守球员静止";         //buff属性
                decimal mixDiscount = decimal.Parse("0.2500");
                int getLevel = 10;              //经理开放等级
                int actType = 4;            //技能类型
                string actTypeMemo = "组织";            //技能类型
                int skillClass = 2;             //技能品质
                string skillClassMemo = "蓝";             //技能品质
                string Cd = "25分钟";              //技能CD
                string Memo = "传球时，球的运行轨迹带有气流";              //技能描述
                int skillId = 10500 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 1;
                double up = 0.3;   //升级属性
                double up1 = 0.1;
                string sk = "A006";     //技能Code
                string name = "贴身防守";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i* up1);
                string buffmemo = "立即降低对方球员进攻属性" + buf + "%，然后每分钟减少进攻属性" + buf1 + "%，直至对方失去球权";         //buff属性
                decimal mixDiscount = decimal.Parse("0.2500");
                int getLevel = 12;              //经理开放等级
                int actType = 3;            //技能类型
                string actTypeMemo = "防守";            //技能类型
                int skillClass = 2;             //技能品质
                string skillClassMemo = "蓝";             //技能品质
                string Cd = "30分钟";              //技能CD
                string Memo = "拉人动画+debuff效果";              //技能描述
                int skillId = 10600 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 1;
                double up = 0.3;   //升级属性
                double up1 = 0.1;
                string sk = "A007";     //技能Code
                string name = "重炮";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "射门时" + buf + "%概率令门将扑球脱手";         //buff属性
                decimal mixDiscount = decimal.Parse("0.2500");
                int getLevel = 14;              //经理开放等级
                int actType = 1;            //技能类型
                string actTypeMemo = "射门";            //技能类型
                int skillClass = 2;             //技能品质
                string skillClassMemo = "蓝";             //技能品质
                string Cd = "30分钟";              //技能CD
                string Memo = "射门集气蓄力1秒，球被压扁";              //技能描述
                int skillId = 10700 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 4; //基础属性
                int bs1 = 1;
                double up = 0.4;   //升级属性
                double up1 = 0.1;
                string sk = "A008";     //技能Code
                string name = "人球分过";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "过人时" + buf + "%概率使对方困惑";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 15;              //经理开放等级
                int actType = 2;            //技能类型
                string actTypeMemo = "过人";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "30分钟";              //技能CD
                string Memo = "球从对手身旁穿过，进攻方快速绕到其身后";              //技能描述
                int skillId = 10800 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 4; //基础属性
                int bs1 = 1;
                double up = 0.4;   //升级属性
                double up1 = 0.1;
                string sk = "A009";     //技能Code
                string name = "拦截";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "防守时降低对方组织属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 16;              //经理开放等级
                int actType = 3;            //技能类型
                string actTypeMemo = "防守";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "30分钟";              //技能CD
                string Memo = "防守方伸出长脚";              //技能描述
                int skillId = 10900 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }
            for (int i = 1; i < 41; i++)
            {
                int bs = 3; //基础属性
                int bs1 = 1;
                double up = 0.3;   //升级属性
                double up1 = 0.1;
                string sk = "A010";     //技能Code
                string name = "快速移动";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "增加自身位置感属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.2500");
                int getLevel = 18;              //经理开放等级
                int actType = 5;            //技能类型
                string actTypeMemo = "守门";            //技能类型
                int skillClass = 2;             //技能品质
                string skillClassMemo = "蓝";             //技能品质
                string Cd = "25分钟";              //技能CD
                string Memo = "门将快速左右移动两步";              //技能描述
                int skillId = 11000 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 4; //基础属性
                int bs1 = 1;
                double up = 0.4;   //升级属性
                double up1 = 0.1;
                string sk = "A011";     //技能Code
                string name = "长传冲吊";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "强制向前传高球，增加接球者身体属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 20;              //经理开放等级
                int actType = 4;            //技能类型
                string actTypeMemo = "组织";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "28分钟";              //技能CD
                string Memo = "踢出弧线球之后球带有残影";              //技能描述
                int skillId = 11100 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }
            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 1;
                double up = 0.5;   //升级属性
                double up1 = 0.1;
                string sk = "A012";     //技能Code
                string name = "假动作过人";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "过人时降低对手防守属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 22;              //经理开放等级
                int actType = 2;            //技能类型
                string actTypeMemo = "过人";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "32分钟";              //技能CD
                string Memo = "边带球边俯身身体摇摆，盘带过防守球员";              //技能描述
                int skillId = 11200 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }
            for (int i = 1; i < 41; i++)
            {
                int bs = 4; //基础属性
                int bs1 = 1;
                double up = 0.4;   //升级属性
                double up1 = 0.1;
                string sk = "A013";     //技能Code
                string name = "挑射";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "射门时增加自身进攻属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 25;              //经理开放等级
                int actType = 1;            //技能类型
                string actTypeMemo = "射门";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "25分钟";              //技能CD
                string Memo = "守门员侧扑时射门球员将球挑过门将";              //技能描述
                int skillId = 11300 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 4; //基础属性
                int bs1 = 1;
                double up = 0.4;   //升级属性
                double up1 = 0.1;
                string sk = "A014";     //技能Code
                string name = "抢断";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "防守时降低对方进攻属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 26;              //经理开放等级
                int actType = 3;            //技能类型
                string actTypeMemo = "防守";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "15分钟";              //技能CD
                string Memo = "普通动画配以抢球者略有残影";              //技能描述
                int skillId = 11400 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 1;
                double up = 0.5;   //升级属性
                double up1 = 0.1;
                string sk = "A015";     //技能Code
                string name = "挑球过人";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "过人时增加进攻属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 28;              //经理开放等级
                int actType = 2;            //技能类型
                string actTypeMemo = "过人";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "16分钟";              //技能CD
                string Memo = "将球挑起后，身体360度转身绕过防守队员";              //技能描述
                int skillId = 11500 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 1;
                double up = 0.5;   //升级属性
                double up1 = 0.1;
                string sk = "A016";     //技能Code
                string name = "组织核心";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "传球时增加自身组织属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 30;              //经理开放等级
                int actType = 4;            //技能类型
                string actTypeMemo = "组织";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "16分钟";              //技能CD
                string Memo = "足球发光，并有3条光晕围绕球转";              //技能描述
                int skillId = 11600 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 4; //基础属性
                int bs1 = 4;
                double up = 0.4;   //升级属性
                double up1 = 0.4;
                string sk = "A017";     //技能Code
                string name = "屏障";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "防守时增加抢断属性" + buf + "%,对手" + buf1 + "%概率倒地";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 32;              //经理开放等级
                int actType = 3;            //技能类型
                string actTypeMemo = "防守";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "18分钟";              //技能CD
                string Memo = "带球队员冲向防守方，之后被身前的屏障弹回";              //技能描述
                int skillId = 11700 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 1;
                double up = 0.5;   //升级属性
                double up1 = 0.1;
                string sk = "A018";     //技能Code
                string name = "弧线射门";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "射门时使对方门将守门属性降低" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 35;              //经理开放等级
                int actType = 1;            //技能类型
                string actTypeMemo = "射门";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "30分钟";              //技能CD
                string Memo = "射出的球呈弧线飞行";              //技能描述
                int skillId = 11800 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 4; //基础属性
                int bs1 = 1;
                double up = 0.4;   //升级属性
                double up1 = 0.1;
                string sk = "A019";     //技能Code
                string name = "扑救";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "增加自身反应属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 36;              //经理开放等级
                int actType = 5;            //技能类型
                string actTypeMemo = "守门";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "16分钟";              //技能CD
                string Memo = "普通扑救Flash，扑救过程中守门员会变大";              //技能描述
                int skillId = 11900 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 6; //基础属性
                int bs1 = 1;
                double up = 0.6;   //升级属性
                double up1 = 0.1;
                string sk = "A020";     //技能Code
                string name = "踩单车";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "过人时" + buf + "%概率使对方倒地";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 38;              //经理开放等级
                int actType = 2;            //技能类型
                string actTypeMemo = "过人";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "18分钟";              //技能CD
                string Memo = "带球者双脚不停地围绕球画圈之后加速突破";              //技能描述
                int skillId = 12000 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 1;
                double up = 0.5;   //升级属性
                double up1 = 0.1;
                string sk = "A021";     //技能Code
                string name = "短传渗透";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "增加接球者组织属性" + buf + "%，强制传球";         //buff属性
                decimal mixDiscount = decimal.Parse("0.4000");
                int getLevel = 40;              //经理开放等级
                int actType = 4;            //技能类型
                string actTypeMemo = "组织";            //技能类型
                int skillClass = 3;             //技能品质
                string skillClassMemo = "紫";             //技能品质
                string Cd = "18分钟";              //技能CD
                string Memo = "传球时，脚下银光一闪";              //技能描述
                int skillId = 12100 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 5;
                double up = 0.5;   //升级属性
                double up1 = 0.5;
                string sk = "A022";     //技能Code
                string name = "剪刀脚";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "防守时增加侵略性" + buf + "%，使对方" + buf1 + "%概率致伤并暂时离场，自身一定概率得黄牌";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 42;              //经理开放等级
                int actType = 3;            //技能类型
                string actTypeMemo = "防守";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "20分钟";              //技能CD
                string Memo = "出现巨大的剪刀将对手夹在刀刃间，之后球员消失";              //技能描述
                int skillId = 12200 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }
            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 5;
                double up = 0.5;   //升级属性
                double up1 = 0.5;
                string sk = "A023";     //技能Code
                string name = "倒挂金钩";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "射门时增加射门属性" + buf + "%，同时对方门将" + buf1 + "%概率困惑";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 45;              //经理开放等级
                int actType = 1;            //技能类型
                string actTypeMemo = "射门";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "22分钟";              //技能CD
                string Memo = "起跳后不等球落地，直接倒钩、头球射门";              //技能描述
                int skillId = 12300 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 5;
                double up = 0.5;   //升级属性
                double up1 = 0.5;
                string sk = "A024";     //技能Code
                string name = "鱼跃";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "增加自身守门属性" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 46;              //经理开放等级
                int actType = 5;            //技能类型
                string actTypeMemo = "守门";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "18分钟";              //技能CD
                string Memo = "守门员变为一条鱼进行扑救";              //技能描述
                int skillId = 12400 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }
            for (int i = 1; i < 41; i++)
            {
                int bs = 6; //基础属性
                int bs1 = 5;
                double up = 0.6;   //升级属性
                double up1 = 0.5;
                string sk = "A025";     //技能Code
                string name = "精确制导";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "传球额外增加" + buf + "%成功率";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 48;              //经理开放等级
                int actType = 4;            //技能类型
                string actTypeMemo = "组织";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "22分钟";              //技能CD
                string Memo = "传球后，球变成导弹形状";              //技能描述
                int skillId = 12500 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 10; //基础属性
                int bs1 = 5;
                double up = 2;   //升级属性
                double up1 = 0.5;
                string sk = "A026";     //技能Code
                string name = "跳水";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                string buffmemo = "被断球时，对方" + buf + "%概率犯规，同时有" + buf1 + "%概率得黄牌";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 49;              //经理开放等级
                int actType = 2;            //技能类型
                string actTypeMemo = "过人";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "25分钟";              //技能CD
                string Memo = "带球者突然夸张的跳起后倒地";              //技能描述
                int skillId = 12600 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 5;
                int bs2 = 50;
                double up = 0.5;   //升级属性
                double up1 = 0.5;
                double up2 = -0.5;
                string sk = "A027";     //技能Code
                string name = "飞踹";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                double buf2 = bs2 + (i* up2);
                string buffmemo = "防守时增加防守属性" + buf + "%，使对方" + buf1 + "%概率致伤并退场，" + buf2 + "%的概率获得黄牌";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 51;              //经理开放等级
                int actType = 3;            //技能类型
                string actTypeMemo = "防守";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "28分钟";              //技能CD
                string Memo = "跳起飞踹对方";              //技能描述
                int skillId = 12700 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 3;
                int bs2 = 50;
                double up = 0.5;   //升级属性
                double up1 = 0.3;
                double up2 = -0.5;
                string sk = "A028";     //技能Code
                string name = "虎射";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                double buf2 = bs2 + (i * up2);
                string buffmemo = "射门时增加射门和力量属性" + buf + "%，同时对方门将" + buf1 + "%概率致伤暂时离场";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 53;              //经理开放等级
                int actType = 1;            //技能类型
                string actTypeMemo = "射门";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "28分钟";              //技能CD
                string Memo = "蓄力1.5秒，射门脚变成老虎形";              //技能描述
                int skillId = 12800 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 8;
                int bs2 = 50;
                double up = 0.5;   //升级属性
                double up1 = 0.8;
                double up2 = -0.5;
                string sk = "A029";     //技能Code
                string name = "盘带魔术师";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                double buf2 = bs2 + (i * up2);
                string buffmemo = "过人时增加进攻属性" + buf + "%，同时对方" + buf1 + "%概率静止";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 54;              //经理开放等级
                int actType = 2;            //技能类型
                string actTypeMemo = "过人";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "28分钟";              //技能CD
                string Memo = "全场黑色，聚光灯照在带球人身上，防守球员无法活动";              //技能描述
                int skillId = 12900 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 20; //基础属性
                int bs1 = 8;
                int bs2 = 50;
                double up = 2;   //升级属性
                double up1 = 0.8;
                double up2 = -0.5;
                string sk = "A030";     //技能Code
                string name = "空中挑传";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                double buf2 = bs2 + (i * up2);
                string buffmemo = "传球成功率增加" + buf + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 55;              //经理开放等级
                int actType = 4;            //技能类型
                string actTypeMemo = "组织";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "30分钟";              //技能CD
                string Memo = "球的飞行弧形为金色，并比普通长传的弧线更高";              //技能描述
                int skillId = 13000 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 2;
                int bs2 = 50;
                double up = 0.5;   //升级属性
                double up1 = 0.2;
                double up2 = -0.5;
                string sk = "A031";     //技能Code
                string name = "怒吼";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                double buf2 = bs2 + (i * up2);
                string buffmemo = "增加自身守门属性" + buf + "%，同时降低对方射门属性" + buf1 + "%";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 58;              //经理开放等级
                int actType = 5;            //技能类型
                string actTypeMemo = "守门";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "24分钟";              //技能CD
                string Memo = "守门员怒吼，并伴有轻微抖动";              //技能描述
                int skillId = 13100 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 2;
                int bs2 = 50;
                double up = 0.5;   //升级属性
                double up1 = 0.2;
                double up2 = -0.5;
                string sk = "A032";     //技能Code
                string name = "吊射";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                double buf2 = bs2 + (i * up2);
                string buffmemo = "射门时增加进攻属性" + buf + "%，无视地形";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 60;              //经理开放等级
                int actType = 1;            //技能类型
                string actTypeMemo = "射门";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "30分钟";              //技能CD
                string Memo = "蓄力1秒，踢球有气圈围绕效果。";              //技能描述
                int skillId = 13200 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }
            for (int i = 1; i < 41; i++)
            {
                int bs = 5; //基础属性
                int bs1 = 2;
                int bs2 = 7;
                double up = 0.5;   //升级属性
                double up1 = 0.2;
                double up2 = 0.7;
                string sk = "A033";     //技能Code
                string name = "后防领袖";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                double buf2 = bs2 + (i * up2);
                string buffmemo = "防守时增加自身防守属性" + buf + "%，同时降低对方全属性" + buf1 + "%，" + buf2 + "%概率迷惑";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 63;              //经理开放等级
                int actType = 3;            //技能类型
                string actTypeMemo = "防守";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "28分钟";              //技能CD
                string Memo = "自身体型变大并发出怒吼。";              //技能描述
                int skillId = 13300 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }
            for (int i = 1; i < 41; i++)
            {
                int bs = 20; //基础属性
                int bs1 = 2;
                int bs2 = 7;
                double up =2;   //升级属性
                double up1 = 0.2;
                double up2 = 0.7;
                string sk = "A034";     //技能Code
                string name = "致命传球";     //技能名
                double buf = bs + (i * up);
                double buf1 = bs1 + (i * up1);
                double buf2 = bs2 + (i * up2);
                string buffmemo = "与接球队员对位的防守方" + buf + "%静止且无法被免疫";         //buff属性
                decimal mixDiscount = decimal.Parse("0.8000");
                int getLevel = 65;              //经理开放等级
                int actType = 4;            //技能类型
                string actTypeMemo = "组织";            //技能类型
                int skillClass = 4;             //技能品质
                string skillClassMemo = "橙";             //技能品质
                string Cd = "35分钟";              //技能CD
                string Memo = "对位球员头上出现骷髅。";              //技能描述
                int skillId = 13400 + i;                //技能ID

                InsertIntoSill(sk, name, buffmemo, mixDiscount, getLevel, actType, actTypeMemo, skillClass,
                    skillClassMemo, Cd, Memo, skillId, date, i);
            }

        }

        private void InsertIntoSill(string sk,string name,string buffmemo,decimal mixDiscount,int getLevel,int actType,string actTypeMemo
            ,int skillClass,string skillClassMemo,string Cd,string Memo,int skillId,DateTime date,int i)
        {
        
            string triggerRate = "90%";
            string LastTime = "90%";
            DicSkillcardEntity entity = new DicSkillcardEntity(sk + "_" + i, name + "lv" + i, sk + "_" + i, skillClass, sk, i,
                getLevel, actType, 0, mixDiscount, buffmemo, date);
            DicSkillcardMgr.Insert(entity);

            DicSkillcardtipsEntity e = new DicSkillcardtipsEntity(skillId, sk + "_" + i, name, actType, actTypeMemo, skillClass, skillClassMemo, sk,
                i, getLevel, 0, mixDiscount, actTypeMemo, triggerRate, Cd, LastTime, buffmemo, sk + "_" + i, Memo, date);
            DicSkillcardtipsMgr.Insert(e);
        }

        #region 友谊赛

        [Test]
        public void Test1()
        {


        }

        
        private string GetEquipmentPropertys(string equipmentItems)
        {
            var equipmentPropertys = "";
            var equipmentList = equipmentItems.Split('|');
            foreach (var equipmentItem in equipmentList)
            {
                var itemcode = Convert.ToInt32(equipmentItem);
                var iteminfo = ItemsdicCache.Instance.GetItem(itemcode);
                var equipmentProperty = EquipmentCache.Instance.RandomEquipmentProperty(iteminfo.LinkId);
                var bytes = SerializationHelper.ToByte(equipmentProperty);
                var eqstr = Encoding.Default.GetString(bytes);
                equipmentPropertys += eqstr + "|";
            }
            return equipmentPropertys.TrimEnd();
        }

        [Test]
        public void Test2()
        {
            var allPlayer = DicPlayerMgr.GetAll();
            for (int i = 1; i < 501; i++)
            {
                var p1s = allPlayer.FindAll(r => r.Position == 0 && r.CardLevel == 5);//蓝门卫
                var p1 = p1s[RandomHelper.GetInt32WithoutMax(0, p1s.Count)];
                var p2s = allPlayer.FindAll(r => r.Position == 1);//后卫
                var p2ss = p2s.FindAll(r => r.CardLevel == 5);//蓝后卫
                var p2 = p2ss[RandomHelper.GetInt32WithoutMax(0, p2ss.Count)];
                var p2sss = p2s.FindAll(r => r.CardLevel == 6);//绿后卫
                var p3 = p2sss[RandomHelper.GetInt32WithoutMax(0, p2sss.Count)];
                p2sss.Remove(p3);
                var p4 = p2sss[RandomHelper.GetInt32WithoutMax(0, p2sss.Count)];
                var p3s = allPlayer.FindAll(r => r.Position == 2 && r.CardLevel == 6);//绿中场
                var p5 = p3s[RandomHelper.GetInt32WithoutMax(0, p3s.Count)];
               var p4s = allPlayer.FindAll(r => r.Position == 2 && r.CardLevel == 4 &&　(r.Capacity>= 70 && r.Capacity <= 75));//紫中场
                var p6 = p4s[RandomHelper.GetInt32WithoutMax(0, p4s.Count)];
                var p5s = allPlayer.FindAll(r => r.Position == 3 && r.CardLevel == 6);//绿前锋
                var p7 = p5s[RandomHelper.GetInt32WithoutMax(0, p5s.Count)];
                var kpi = p1.Kpi + p2.Kpi + p3.Kpi + p4.Kpi + p5.Kpi + p6.Kpi + p7.Kpi;
                var solutionstring = p1.Idx + "," + p2.Idx + "," + p3.Idx + "," + p4.Idx + "," + p5.Idx + "," + p6.Idx +
                                     "," + p7.Idx;
                TemplateRegisterMgr.Insert(new TemplateRegisterEntity(i, solutionstring, (int)kpi));
            }
        }

        [Test]
        public void Test3()
        {
            var allf = TemplateRegisterMgr.GetAll();
            foreach (var item in allf)
            {
                var player = item.SolutionString.Split(',');
                int index = 0;
                foreach (var s in player)
                {
                    int p = 0;
                    switch (index)
                    {
                        case 1:
                        case 2:
                        case 3:
                            p = 1;
                            break;
                        case 4:
                        case 5:
                            p = 2;
                            break;
                        case 6:
                            p = 3;
                            break;
                    }
                    TemplateRegisterplayerMgr.Insert(new TemplateRegisterplayerEntity(0, item.Idx,ConvertHelper.ConvertToInt(s), p));
                    index ++;
                }
            }
        }

        

        #endregion

        #region 联赛比分模版

        [Test]
        public void LeagueGoals()
        {

            int leagueId = 8;
            int npc = 19;
            int idx = 0;
            for (int i = 1; i <= leagueId; i++)
            {
                int min = i/2;
                int max = min + 4;
                switch (i)
                {
                    case 1:
                        min = 0;
                        max = 3;
                        break;
                    case 2:
                        min = 0;
                        max = 3;
                        break;
                    case 3:
                        min = 1;
                        max = 4;
                        break;
                    case 4:
                        min = 1;
                        max = 4;
                        break;
                    case 5:
                        min = 1;
                        max = 4;
                        break;
                    case 6:
                        min = 2;
                        max = 5;
                        break;
                    case 7:
                        min = 2;
                        max = 5;
                        break;
                    case 8:
                        min = 2;
                        max = 5;
                        break;
                }
                for (int j = 1; j <= npc; j++)
                {

                    idx++;
                    ConfigLeaguegoalsEntity entity = new ConfigLeaguegoalsEntity();
                    entity.Idx = idx;
                    entity.NpcId = j;
                    entity.TemplateId = i;
                    entity.MinGoals = min;
                    entity.MaxGoals = max;
                    if (j >= 6 && j < 12)
                    {
                        entity.MinGoals++;
                        entity.MaxGoals++;
                    }
                    else if (j >= 12 && j <= 16)
                    {
                        entity.MinGoals += 2;
                        entity.MaxGoals += 2;
                    }
                    else if (j >= 17)
                    {
                        entity.MinGoals += 3;
                        entity.MaxGoals += 3;
                    }
                    ConfigLeaguegoalsMgr.Insert(entity);
                }
            }
        }

        #endregion
    }
}
