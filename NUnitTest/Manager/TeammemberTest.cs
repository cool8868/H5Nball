using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.FriendShip;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using NUnit.Framework;
using Games.NBall.Common;

namespace Games.NBall.NUnitTest.Manager
{
    [TestFixture]
    public class TeammemberTest
    {
        [Test]
        public void Test1()
        {
            TeammemberCore.Instance.SolutionAndTeammemberResponse(new Guid("2BEC30D5-3A97-474E-8555-A539010AAAE6"));
        }
        [Test]
        public void Test2()
        {
            TeammemberCore.Instance.FireTeamMember(new Guid("7C6169BE-8A96-4D96-A50E-A539010AAB1B"), new Guid("6ae78133-557d-e511-afdd-9b9c85cadb32"));
        }

        [Test]
        public void Test3()
        {
            ItemCore.Instance.Test();
        }

        [Test]
        public void Test4()
        {
            TeammemberCore.Instance.ReplacePlayer(new Guid("D1BDE417-57DD-475A-8BCF-A539010AAAF8"),
                new Guid("9c18ddc6-b05e-4b96-9540-a53f011aa245"), new Guid("1AC35339-907C-E511-AFDD-9B9C85CADB32"));
        }

        [Test]
        public void Test5()
        {
            //获取球队列表
            var npcTeamList = CacheFactory.LeagueCache.GetLeagueTeam(1);
            int round = npcTeamList.Count * 2;//总回合数

            List<Guid> allGuidList = new List<Guid>(npcTeamList.Count + 1);//当前联赛的所有球队,包括玩家
            var managerId = new Guid("f94334e2-1c6d-41bf-a2e5-a57700b4b76a");
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return;

            allGuidList.Add(managerId);//将玩家信息加入需分组队列
            for (int i = 0; i < npcTeamList.Count; i++)//生成NPC guid
            {
                allGuidList.Add(ShareUtil.GenerateComb());
            }
            //生成所有配对索引组合
            var allTeamIndex = new List<int>();
            for (int i = 0; i < npcTeamList.Count + 1; i++)//所有需要分组的队伍Index
            {
                allTeamIndex.Add(i);
            }
            var allPairList = new List<KeyValuePair<int, int>>();
            DoubleComposition(allTeamIndex, ref allPairList);//计算出所有配对组合

            //生成所有轮配对
            var roundPair = new Dictionary<int, List<LeagueEncounterEntity>>();
            var allUsedPairList = new List<KeyValuePair<int, int>>();//所有轮中使用的键值对
            for (int i = 0; i < round; i++)
            {
                int roundId = i + 1;//轮数从1开始
                //本轮已使用的键值对
                var currectUsedPairList = new List<KeyValuePair<int, int>>();
                for (int j = 0; j < allGuidList.Count / 2; j++)
                {
                    var homeId = allGuidList[0];
                    var awayId = allGuidList[0];
                    var key = new KeyValuePair<int, int>();

                    //主队和客队不能相同 如果当前匹配在本组中已经有，或者在其他轮中已经存在则继续
                    while (homeId == awayId || allUsedPairList.Contains(key))
                    {
                        key = getNewKeyValue(allPairList, currectUsedPairList, allUsedPairList);
                        homeId = allGuidList[key.Key];
                        awayId = allGuidList[key.Value];

                        //当前匹配不在已有轮
                        while (homeId == awayId || allUsedPairList.Contains(key))
                        {
                            key = getNewKeyValue(allPairList, currectUsedPairList, allUsedPairList);
                            homeId = allGuidList[key.Key];
                            awayId = allGuidList[key.Value];
                        }
                    }
                    currectUsedPairList.Add(key);
                    allUsedPairList.Add(key);

                    var entity = new LeagueEncounterEntity();
                    entity.MatchId = Guid.NewGuid();
                    entity.HomeId = allGuidList[key.Key];
                    entity.AwayId = allGuidList[key.Value];
                    entity.WheelNumber = roundId;

                    entity.HomeIsNpc = homeId != managerId;
                    entity.AwayIsNpc = awayId != managerId;
                    entity.HomeName = entity.HomeIsNpc ? npcTeamList[key.Key - 1].TeamName : manager.Name;
                    entity.AwayName = entity.AwayIsNpc ? npcTeamList[key.Value - 1].TeamName : manager.Name;


                    //entity.LaegueRecordId = laegueRecordId;

                    if (!roundPair.ContainsKey(roundId))
                        roundPair.Add(roundId, new List<LeagueEncounterEntity>());
                    roundPair[roundId].Add(entity);
                }
            }
            var x = roundPair;

            
            //var x = result1List;
            //var y = result2List;


        }

        private KeyValuePair<int, int> getNewKeyValue(List<KeyValuePair<int, int>> allKeyValuePairs, List<KeyValuePair<int, int>> usedKeyValuePairs, List<KeyValuePair<int, int>> allUsedKeyValuePairs)
        {

            foreach (var allKeyValuePair in allKeyValuePairs)
            {
                if (usedKeyValuePairs.Contains(allKeyValuePair))
                    continue;
                if(allUsedKeyValuePairs.Contains(allKeyValuePair))
                    continue;
                var home = allKeyValuePair.Key;
                var away = allKeyValuePair.Value;
                bool used = false;
                foreach (var usedKeyValuePair in usedKeyValuePairs)
                {
                    if (usedKeyValuePair.Key == home || usedKeyValuePair.Value == home
                        || usedKeyValuePair.Key == away || usedKeyValuePair.Value == away)
                    {
                        used = true;
                        break;
                    }
                }
                if (!used)
                {
                    return allKeyValuePair;
                }

            }
            return new KeyValuePair<int, int>(0,0);
        }
      

        private bool isInDic(Dictionary<int, List<LeagueEncounterEntity>> roundPair, Guid homeId, Guid awayId)
        {
            bool isIn = false;
            foreach (var leagueList in roundPair.Values)
            {
                foreach (var leagueEncounterEntity in leagueList)
                {
                    if (leagueEncounterEntity.HomeId == homeId && leagueEncounterEntity.AwayId == awayId)
                    {
                        isIn = true;
                        break;
                    }
                }
            }
            return isIn;
        }


        static void DoubleComposition(List<int> arr, ref List<KeyValuePair<int, int>> resultList)
        {
            if (arr == null || arr.Count <= 1)
                return;

            int arrIndex = arr.First();
            arr.RemoveAt(0);
            List<KeyValuePair<int, int>> list = resultList;
            arr.ForEach(o =>
            {
                var keyValuePair = new KeyValuePair<int, int>(arrIndex, o);
                var keyValuePair1 = new KeyValuePair<int, int>(o, arrIndex);
                list.Add(keyValuePair);
                list.Add(keyValuePair1);
            });
            DoubleComposition(arr, ref resultList);
        }

    }
}
