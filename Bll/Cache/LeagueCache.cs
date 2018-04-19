using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class LeagueCache : BaseSingleton
    {
        #region encapsulation

        private Dictionary<int, List<ConfigLeaguemarkEntity>> _markDic;
        private Dictionary<int, List<int>> _npcDic;
        private Dictionary<int, Guid> _npcGuidDic;

        private Dictionary<string, List<ConfigLeagueprizeEntity>> _leaguePrizeDic;

        private Dictionary<int, ConfigLeagueEntity> _leagueDic;
        /// <summary>
        /// type->list
        /// </summary>
        private Dictionary<int, List<string>> _exchangeTypeDic;
        /// <summary>
        /// itemCode,entity
        /// </summary>
        private Dictionary<string, DicLeagueexchangeEntity> _exchangeDic;

        private Dictionary<int, List<int>> _leagueWincounDic; 

        private Dictionary<string, ConfigLeaguewincountprizeEntity> _leagueWincountPrizeDic;

        /// <summary>
        /// 联赛对阵
        /// </summary>
        //private Dictionary<int, List<int>> _leagueFightMapDic;
        private Dictionary<int, List<ConfigLeaguefightmapEntity>> _leagueFightMapDic;
        private Dictionary<int, List<ConfigLeaguefightmapEntity>> _leagueFightMapTeamplateDic;
        /// <summary>
        /// 联赛星星配置
        /// </summary>
        public Dictionary<int, List<ConfigLeaguestarEntity>> _leagueStarDic;
        /// <summary>
        /// 联赛比分模版
        /// </summary>
        public Dictionary<int, List<ConfigLeaguegoalsEntity>> _leagueGoalsMap;

        /// <summary>
        /// leagueid->teamcount
        /// </summary>
        private Dictionary<int, int> _leagueTeamCount; 
        public LeagueCache(int p)
            : base(p)
        {
            InitCache();
        }

        private void InitCache()
        {
            _markDic = new Dictionary<int, List<ConfigLeaguemarkEntity>>();
            _npcDic = new Dictionary<int, List<int>>();
            _npcGuidDic = new Dictionary<int, Guid>();
            _leaguePrizeDic = new Dictionary<string, List<ConfigLeagueprizeEntity>>();
            _leagueDic=new Dictionary<int, ConfigLeagueEntity>();
            var allMark = ConfigLeaguemarkMgr.GetAll();
            _leagueTeamCount = new Dictionary<int, int>();
            _leagueStarDic = new Dictionary<int, List<ConfigLeaguestarEntity>>();
            foreach (var item in allMark)
            {
                if (!_markDic.ContainsKey(item.LeagueId))
                    _markDic.Add(item.LeagueId, new List<ConfigLeaguemarkEntity>());
                _markDic[item.LeagueId].Add(item);
                if (!_npcDic.ContainsKey(item.LeagueId))
                    _npcDic.Add(item.LeagueId, new List<int>());
                _npcDic[item.LeagueId].Add(item.TeamId);

                var key = NpcKey(item.LeagueId,item.TeamId);
                if (!_npcGuidDic.ContainsKey(key))
                    _npcGuidDic.Add(key, item.Idx);
                else
                    _npcGuidDic[key] = item.Idx;
            }
            foreach (var dicItem in _markDic)
            {
                _leagueTeamCount.Add(dicItem.Key,dicItem.Value.Count);
            }

            var allPrize = ConfigLeagueprizeMgr.GetAll();
            foreach (var prize in allPrize)
            {
                string key = BuildPrizeKey(prize.LeagueID, prize.ResultType);
                if(!_leaguePrizeDic.ContainsKey(key))
                    _leaguePrizeDic.Add(key,new List<ConfigLeagueprizeEntity>());
                _leaguePrizeDic[key].Add(prize);
            }

            var leagueConfig = ConfigLeagueMgr.GetAll();
            _leagueDic = leagueConfig.ToDictionary(d => d.LeagueID, d => d);

            var list = DicLeagueexchangeMgr.GetAll();
            _exchangeTypeDic = new Dictionary<int, List<string>>();
            _exchangeDic = new Dictionary<string, DicLeagueexchangeEntity>();
            foreach (var entity in list)
            {
                if (!_exchangeTypeDic.ContainsKey(entity.Type))
                    _exchangeTypeDic.Add(entity.Type, new List<string>());

                var exkeylist = BuildExchangeKey(entity);
                _exchangeTypeDic[entity.Type].AddRange(exkeylist);
                foreach (var exkey in exkeylist)
                {
                    _exchangeDic.Add(exkey, entity);
                }
            }

            _leagueWincountPrizeDic=new Dictionary<string, ConfigLeaguewincountprizeEntity>();
            _leagueWincounDic = new Dictionary<int, List<int>>();
            var listWinCount = ConfigLeaguewincountprizeMgr.GetAll();
            foreach (var winEntity in listWinCount)
            {
                if (!_leagueWincounDic.ContainsKey(winEntity.LeagueId))
                {
                    _leagueWincounDic.Add(winEntity.LeagueId,new List<int>());
                }
                _leagueWincounDic[winEntity.LeagueId].Add(winEntity.WinCount);

                var key = BuildWincountPrizeKey(winEntity.LeagueId, winEntity.WinCount);
                if(!_leagueWincountPrizeDic.ContainsKey(key))
                    _leagueWincountPrizeDic.Add(key,winEntity);
                else
                    _leagueWincountPrizeDic[key] = winEntity;
            }
            //_leagueFightMapDic= ConfigLeaguefightmapMgr.GetAllForCache();
            _leagueFightMapDic = new Dictionary<int, List<ConfigLeaguefightmapEntity>>();
            _leagueFightMapTeamplateDic = new Dictionary<int, List<ConfigLeaguefightmapEntity>>();
            var allfightMap = ConfigLeaguefightmapMgr.GetAll();
            foreach (var item in allfightMap)
            {
                if (!_leagueFightMapDic.ContainsKey(item.TeamCount))
                    _leagueFightMapDic.Add(item.TeamCount, new List<ConfigLeaguefightmapEntity>());
                _leagueFightMapDic[item.TeamCount].Add(item);
                var key = TemplateKey(item.TemplateId, item.RoundIndex);
                if (!_leagueFightMapTeamplateDic.ContainsKey(key))
                    _leagueFightMapTeamplateDic.Add(key, new List<ConfigLeaguefightmapEntity>());
                _leagueFightMapTeamplateDic[key].Add(item);
            }

            var allStar = ConfigLeaguestarMgr.GetAll();
            foreach (var item in allStar)
            {
                if (!_leagueStarDic.ContainsKey(item.LeagueId))
                    _leagueStarDic.Add(item.LeagueId, new List<ConfigLeaguestarEntity>());
                _leagueStarDic[item.LeagueId].Add(item);
            }

            var allGoalsMap = ConfigLeaguegoalsMgr.GetAll();
            _leagueGoalsMap = new Dictionary<int, List<ConfigLeaguegoalsEntity>>();
            foreach (var item in allGoalsMap)
            {
                if (!_leagueGoalsMap.ContainsKey(item.TemplateId))
                    _leagueGoalsMap.Add(item.TemplateId, new List<ConfigLeaguegoalsEntity>());
                _leagueGoalsMap[item.TemplateId].Add(item);
            }
        }

        public static LeagueCache Instance
        {
            get { return SingletonFactory<LeagueCache>.SInstance; }
        }

        private string BuildPrizeKey(int leagueId,int resultType)
        {
            return leagueId + "-" + resultType;
        }

        public string BuildWincountPrizeKey(int leagueId, int winCount)
        {
            return leagueId + "_" + winCount;
        }

        public ConfigLeaguewincountprizeEntity GetLeagueWincountPrize(int leagueId, int winCount)
        {
            var key = BuildWincountPrizeKey(leagueId, winCount);
            if (_leagueWincountPrizeDic.ContainsKey(key))
                return _leagueWincountPrizeDic[key];
            return null;
        }

        /// <summary>
        /// 获取联赛球队
        /// </summary>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public List<ConfigLeaguemarkEntity> GetLeagueTeam(int leagueId)
        {
            if (_markDic.ContainsKey(leagueId))
                return _markDic[leagueId];
            return null;
        }

        /// <summary>
        /// 获取联赛球队ID
        /// </summary>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public List<int> GetLeagueTeamId(int leagueId)
        {
            if (_npcDic.ContainsKey(leagueId))
                return _npcDic[leagueId];
            return new List<int>();
        }

        /// <summary>
        /// 根据联赛ID和球队ID 获取NPC经理ID
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public Guid GetLeagueNpcGuid(int leagueId, int teamId)
        {
            var key = NpcKey(leagueId, teamId);
            if (_npcGuidDic.ContainsKey(key))
                return _npcGuidDic[key];
            return Guid.Empty;
        }


        /// <summary>
        /// 获取联赛奖励
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="winType"></param>
        /// <returns></returns>
        public List<ConfigLeagueprizeEntity> GetLeaguePrize(int leagueId, int winType)
        {
            string key = BuildPrizeKey(leagueId, winType);
            if (_leaguePrizeDic.ContainsKey(key))
                return _leaguePrizeDic[key];
            return null;
        }

        /// <summary>
        /// 获取各联赛开启等级
        /// </summary>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public int GetLeagueOpenLevel(int leagueId)
        {
            if (_leagueDic.ContainsKey(leagueId))
                return _leagueDic[leagueId].Level;
            return -1;
        }

        /// <summary>
        /// 获取球队编号
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public int GetNpcTeamId(int leagueId,string teamName)
        {
            if (_markDic.ContainsKey(leagueId))
            {
                foreach (var team in _markDic[leagueId])
                {
                    if (team.TeamName == teamName)
                        return team.TeamId;
                }
            }
            return -1;
        }

        List<string> BuildExchangeKey(DicLeagueexchangeEntity entity)
        {
            var list = new List<string>();
            switch (entity.ItemType)
            {
                case 0://指定物品
                    list.Add(entity.Idx + "," + entity.ItemCode);
                    break;
                case 1://从卡库中随机抽取
                    var itemlist = LotteryCache.Instance.GetAllItemsByLib(entity.ItemCode);
                    foreach (var item in itemlist)
                    {
                        var key = entity.Idx + "," + item;
                        if (!list.Contains(key))
                            list.Add(key);
                    }
                    break;
                default:
                    break;
            }
            return list;
        }

        string GetExchangeId(int type)
        {
            var list = _exchangeTypeDic[type];
            return list[RandomHelper.GetInt32WithoutMax(0, list.Count)];
        }

        string GetEquipmentExchangeId(List<int> types,int count)
        {
            string returnStr = "";
            var allList = new List<string>();
            foreach (var type in types)
            {
                var list = _exchangeTypeDic[type];
                if(list!=null&&list.Count>0)
                    allList.AddRange(_exchangeTypeDic[type]);
            }

            for (int i = 0; i < count; i++)
            {
                int index = RandomHelper.GetInt32WithoutMax(0, allList.Count);
                returnStr += allList[index] + "|";
                allList.RemoveAt(index);
            }
            return returnStr;
        }


        private string GetEquipmentPropertys(string equipmentItems,out string itemcodes)
        {
            var propertys = "";
            itemcodes = "";
            var equipmentList = equipmentItems.Split('|');
            foreach (var equipmentItem in equipmentList)
            {
                var equipmentItemCode = equipmentItem.Split(',');
                if(equipmentItemCode.Length<2)
                    continue;

                var itemcode = Convert.ToInt32(equipmentItemCode[1]);
                var iteminfo = ItemsdicCache.Instance.GetItem(itemcode);
                var equipmentProperty = EquipmentCache.Instance.RandomEquipmentProperty(iteminfo.LinkId);
                var bytes = SerializationHelper.ToByte(equipmentProperty);

                itemcodes += itemcode + "|";
                propertys += ShareUtil.ByteArrayToHexStr(bytes) +"|";
            }
            itemcodes = itemcodes.TrimEnd('|');
            return propertys.TrimEnd('|');
        }
        



        public List<EquipmentProperty> AnalysisProperties(string equipmentProperties)
        {
            var propertyList = new List<EquipmentProperty>();
            var equipmentPropertiesHexList = equipmentProperties.Split('|');
            foreach (var equipmentProperty in equipmentPropertiesHexList)
            {
               if(string.IsNullOrEmpty(equipmentProperties))
                   continue;

               var propertyBytes = ShareUtil.HexStrToByteArray(equipmentProperty);
                var property = SerializationHelper.FromByte<EquipmentProperty>(propertyBytes);
                if(property!=null)
                    propertyList.Add(property);
            }
            return propertyList;
        }



        /// <summary>
        /// 获取商城兑换列表
        /// </summary>
        /// <param name="withHighEquip">是否可以兑换高级装备</param>
        /// <param name="equipmentItemcode">装备</param>
        /// <param name="equipmentProperties">装备属性</param>
        /// <param name="isReplace">是否刷新碎片</param>
        /// <returns></returns>
        public string GetExchanges(bool withHighEquip, out string equipmentItemcode, out string equipmentProperties, bool isReplace, string codeString)
        {
            string newExchanges = "";
            equipmentProperties = "";
            equipmentItemcode = "";
            for (int i = 1; i <= 8; i++)
            {
               
                var exchange = GetExchangeId(i);
                var itemcode = exchange.Split(',')[1];
                //if (isReplace && itemcode.IndexOf("39") == 0)
                //    continue;
                while (newExchanges.Contains(itemcode))
                {
                    exchange = GetExchangeId(i);
                    itemcode = exchange.Split(',')[1];
                }
                newExchanges = newExchanges + exchange + "|";
            }
            //if (isReplace && codeString.Length > 0)
            //    newExchanges += codeString+"|";
            //装备抽取2件
            var eqList = new List<int>() {9, 10};
            if(withHighEquip)
                eqList.Add(11);
            var equipmentItems= GetEquipmentExchangeId(eqList, 2);
            newExchanges += equipmentItems;
            equipmentProperties = GetEquipmentPropertys(equipmentItems, out equipmentItemcode);
            return newExchanges.TrimEnd('|');
        }

        public DicLeagueexchangeEntity GetExchangeEntity(string exchange)
        {
            if (!_exchangeDic.ContainsKey(exchange))
                return null;
            return _exchangeDic[exchange];
        }

        /// <summary>
        /// 根据人数获取模版ID
        /// </summary>
        /// <param name="teamCount"></param>
        /// <returns></returns>
        public int GetTemplateId(int teamCount)
        {
            if (_leagueFightMapDic.ContainsKey(teamCount))
            {
                var index = RandomHelper.GetInt32WithoutMax(0, _leagueFightMapDic[teamCount].Count);
                return _leagueFightMapDic[teamCount][index].TemplateId;
            }
            return 0;
        }

        public int GetTeamCount(int leagueId)
        {
            if (_leagueTeamCount.ContainsKey(leagueId))
                return _leagueTeamCount[leagueId];
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据对阵ID和回合数获取对阵详情
        /// </summary>
        /// <param name="teamplateId"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<ConfigLeaguefightmapEntity> GetFightMap(int teamplateId, int round)
        {
            var key = TemplateKey(teamplateId, round);
            if (_leagueFightMapTeamplateDic.ContainsKey(key))
                return _leagueFightMapTeamplateDic[key];
            return new List<ConfigLeaguefightmapEntity>();
        }

        /// <summary>
        /// 获取npc随机比分
        /// </summary>
        /// <param name="leagueId"></param>
        /// <param name="npcId"></param>
        /// <returns></returns>
        public int GetGoalsMap(int leagueId, int npcId)
        {
            if(_leagueGoalsMap.ContainsKey(leagueId))
            {
                var list = _leagueGoalsMap[leagueId];
                if(list.Exists(r=>r.NpcId ==npcId))
                {
                    var entity = list.Find(r => r.NpcId == npcId);
                    if (entity != null)
                    {
                        return RandomHelper.GetInt32(entity.MinGoals, entity.MaxGoals);
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取联赛星星配置
        /// </summary>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public List<ConfigLeaguestarEntity> GetLeagueStar(int leagueId)
        {
            if (_leagueStarDic.ContainsKey(leagueId))
                return _leagueStarDic[leagueId];
            return new List<ConfigLeaguestarEntity>();
        }

        public int TemplateKey(int teamplateId, int round)
        {
            return teamplateId * 10000 + round;
        }

        public int NpcKey(int leagueId, int teamId)
        {
            return leagueId*10000 + teamId;
        }

        #endregion
    }
}
