using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class ArenaCache : BaseSingleton
    {
        public ArenaCache(int p)
            : base(p)
        {
            InitCache();
        }
        /// <summary>
        /// 段位配置
        /// </summary>
        private Dictionary<int,ConfigArenadangradingEntity> _dangradingDic;
        private List<ConfigArenadangradingEntity> _allDangrading;

        /// <summary>
        /// 竞技场上阵球员限制
        /// </summary>
        private Dictionary<int, ArenaUpFormationAskFor> _arenaUpFormationAskFor = null;

        /// <summary>
        /// NPC
        /// </summary>
        private Dictionary<int, List<ConfigArenanpclinkEntity>> _npcDic;
        /// <summary>
        /// 购买体力配置
        /// </summary>
        private Dictionary<int, int> _buyStaminaDic;
        /// <summary>
        /// 体力恢复
        /// </summary>
        private Dictionary<int, int> _staminaRestoreDic;
        /// <summary>
        /// 奖励字典
        /// </summary>
        private Dictionary<int, List<ConfigArenaprizeEntity>> _prizeDic;
        /// <summary>
        /// 竞技场商城物品
        /// </summary>
        private List<ConfigArenashopEntity> _shopList;
        /// <summary>
        /// 竞技场最大体力
        /// </summary>
        private int arenaMaxStamina;
        /// <summary>
        /// 竞技场刷新商店配置
        /// </summary>
        private Dictionary<int, int> _arenaRefreshShop;


        /// <summary>
        /// 区对应DomainId
        /// </summary>
        private Dictionary<string, EnumArenaDomainType> _siteDic;
        /// <summary>
        /// DomainId 对应区
        /// </summary>
        private Dictionary<EnumArenaDomainType, List<string>> _domainDic;

        private void InitCache()
        {
            LogHelper.Insert("ArenaCache cache init start", LogType.Info);

            _arenaUpFormationAskFor = new Dictionary<int, ArenaUpFormationAskFor>();
            var arenaUpFormationAskFor =
                CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.ArenaUpFormationPlayer);
            if (arenaUpFormationAskFor.Length > 0)
            {
                var s = arenaUpFormationAskFor.Split('|');
                foreach (var s1 in s)
                {
                    var s2 = s1.Split(',');
                    _arenaUpFormationAskFor.Add(ConvertHelper.ConvertToInt(s2[0]),
                        new ArenaUpFormationAskFor(ConvertHelper.ConvertToInt(s2[0]), ConvertHelper.ConvertToInt(s2[1]),
                            s2[2].ToString()));
                }
            }

            arenaMaxStamina = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ArenaMaxStamina, 20);

            _allDangrading = ConfigArenadangradingMgr.GetAll();
            _dangradingDic = _allDangrading.ToDictionary(r => r.Idx, r => r);

            _npcDic = new Dictionary<int,List<ConfigArenanpclinkEntity>>();
            var allNpc = ConfigArenanpclinkMgr.GetAll();
            var allNpcOpponent = ConfigArenanpcopponentMgr.GetAll();
            foreach (var item in allNpcOpponent)
            {
                var key = GetKey(item.Idx, item.Opponent);
                if (!_npcDic.ContainsKey(key))
                    _npcDic.Add(key, new List<ConfigArenanpclinkEntity>());
                var npcList = allNpc.FindAll(r => r.DanGrading == item.GroupId);
                _npcDic[key] = npcList;
            }
            //解析购买体力配置
            var buyStaminaConfig = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.ArenaBuyStaminaConfig);
            var configGroup = buyStaminaConfig.Split('|');
            _buyStaminaDic = new Dictionary<int, int>();
            foreach (var s in configGroup)
            {
                var ss = s.Split(',');
                var number = ss[0].Split('^');
                int point = ConvertHelper.ConvertToInt(ss[1]);
                int number1 = ConvertHelper.ConvertToInt(number[0]);
                int number2 = ConvertHelper.ConvertToInt(number[1]);
                for (int i = number1; i <= number2; i++)
                {
                    if (!_buyStaminaDic.ContainsKey(i))
                        _buyStaminaDic.Add(i, point);
                }

            }
            var arenaStaminaRestore = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.ArenaStaminaRestore);
            var restoreGroup = arenaStaminaRestore.Split('|');
            _staminaRestoreDic = new Dictionary<int, int>();
            foreach (var s in restoreGroup)
            {
                var value = s.Split(',');
                var vipLevel = ConvertHelper.ConvertToInt(value[0]);
                var times = ConvertHelper.ConvertToInt(value[1]);
                if (!_staminaRestoreDic.ContainsKey(vipLevel))
                    _staminaRestoreDic.Add(vipLevel, times);
            }

            //获取所有奖励
            _prizeDic = new Dictionary<int, List<ConfigArenaprizeEntity>>();
            var allPrize = ConfigArenaprizeMgr.GetAll();
            foreach (var item in allPrize)
            {
                for (int i = item.StartRank; i <= item.EndRank; i++)
                {
                    if (!_prizeDic.ContainsKey(i))
                        _prizeDic.Add(i, new List<ConfigArenaprizeEntity>());
                    _prizeDic[i].Add(item);
                }
            }

            _shopList = ConfigArenashopMgr.GetAll();

            //刷新商店配置
            _arenaRefreshShop = new Dictionary<int, int>();
            var arenaRefreshShop = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.ArenaRefreshShop);
            var shopConfig = arenaRefreshShop.Split('|');
            foreach (var item in shopConfig)
            {
                var points = item.Split(',');
                var number = ConvertHelper.ConvertToInt(points[0]);
                var point = ConvertHelper.ConvertToInt(points[1]);
                _arenaRefreshShop.Add(number, point);
            }


            var allZoneInfo = CacheFactory.ZoneCache.GetAllZone();
            _siteDic = new Dictionary<string, EnumArenaDomainType>();
            _domainDic = new Dictionary<EnumArenaDomainType, List<string>>();
            _domainDic.Add(EnumArenaDomainType.Small, new List<string>());
            _domainDic.Add(EnumArenaDomainType.Big, new List<string>());
            _domainDic.Add(EnumArenaDomainType.Alone, new List<string>());
            foreach (var item in allZoneInfo)
            {
                int platzoneId = ConvertHelper.ConvertToInt(item.PlatformZoneName);
                //1和2放到大混服
                if (platzoneId == 1 || platzoneId == 2)
                {
                    if (!_siteDic.ContainsKey(item.ZoneName))
                        _siteDic.Add(item.ZoneName, EnumArenaDomainType.Big);
                    _domainDic[EnumArenaDomainType.Big].Add(item.ZoneName);
                }
                else if (platzoneId < 1000)//小混服
                {
                    if (!_siteDic.ContainsKey(item.ZoneName))
                        _siteDic.Add(item.ZoneName, EnumArenaDomainType.Small);
                    _domainDic[EnumArenaDomainType.Small].Add(item.ZoneName);
                }
                else if (platzoneId > 10000) //大混服
                {
                    if (!_siteDic.ContainsKey(item.ZoneName))
                        _siteDic.Add(item.ZoneName, EnumArenaDomainType.Big);
                    _domainDic[EnumArenaDomainType.Big].Add(item.ZoneName);
                }
                else//独服
                {
                    if (!_siteDic.ContainsKey(item.ZoneName))
                        _siteDic.Add(item.ZoneName, EnumArenaDomainType.Alone);
                    _domainDic[EnumArenaDomainType.Alone].Add(item.ZoneName);
                }
            }

            LogHelper.Insert("ArenaCache cache init end", LogType.Info);
        }

        #region Facade

        public static ArenaCache Instance
        {
            get { return SingletonFactory<ArenaCache>.SInstance; }
        }


        public Dictionary<EnumArenaDomainType, List<string>> GetDomainDic()
        {
            return _domainDic;
        }


        /// <summary>
        /// 获取域
        /// </summary>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public EnumArenaDomainType? GetDomainId(string zoneName)
        {
            if (_siteDic.ContainsKey(zoneName))
                return _siteDic[zoneName];
            return null;
        }

        /// <summary>
        /// 获取域
        /// </summary>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public int GetDomainIdToInt(string zoneName)
        {
            if (_siteDic.ContainsKey(zoneName))
                return (int)_siteDic[zoneName];
            return 0;
        }


        /// <summary>
        /// 竞技场最大体力
        /// </summary>
        public int ArenaMaxStamina{
            get { return arenaMaxStamina; }
        }

        /// <summary>
        /// 获取所有段位
        /// </summary>
        public List<ConfigArenadangradingEntity> AllDangrading {
            get { return _allDangrading; }
        }

        /// <summary>
        /// 获取某一段位
        /// </summary>
        /// <param name="dangrading"></param>
        /// <returns></returns>
        public ConfigArenadangradingEntity GetDangrading(int dangrading)
        {
            if (!_dangradingDic.ContainsKey(dangrading))
                return null;
            return _dangradingDic[dangrading];
        }

        /// <summary>
        /// 根据排名获取奖励
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public List<ConfigArenaprizeEntity> GetPrize(int rank)
        {
            if (rank > 2500)
                rank = 2501;
            if (_prizeDic.ContainsKey(rank))
                return _prizeDic[rank];
            return new List<ConfigArenaprizeEntity>();
        }


        /// <summary>
        /// 球员卡是否可以上阵
        /// </summary>
        /// <param name="player"></param>
        /// <param name="arenaType"></param>
        /// <returns></returns>
        public bool IsPlayerMeetTheRequirements(DicPlayerEntity player, int arenaType)
        {
            try
            {
                if (!_arenaUpFormationAskFor.ContainsKey(arenaType))
                    return false;
                var info = _arenaUpFormationAskFor[arenaType];
                switch (info.AskForType)
                {
                    case 1: //身高
                        return player.Stature >= ConvertHelper.ConvertToInt(info.AskForValues);
                    case 2: //体重
                        return player.Weight >= ConvertHelper.ConvertToInt(info.AskForValues);
                    case 3: //年龄
                        return Convert.ToDateTime(player.Birthday) >= Convert.ToDateTime(info.AskForValues);
                    case 4: //年龄  排除元老
                        if (player.CardLevel == (int) EnumPlayerCardLevel.Gold ||
                            player.CardLevel == (int) EnumPlayerCardLevel.Silver)
                            return false;
                        return Convert.ToDateTime(player.Birthday) <= Convert.ToDateTime(info.AskForValues);
                    case 5: //卡等级
                        return info.AskForValues.IndexOf(player.KpiLevel) > -1;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public List<ConfigArenanpclinkEntity> GetNpc(int danGrading,int opponentIndex)
        {
            var key = GetKey(danGrading, opponentIndex);
            if (!_npcDic.ContainsKey(key))
                return null;
            return _npcDic[key];
        }
        /// <summary>
        /// 获取竞技场商城物品
        /// </summary>
        /// <returns></returns>
        public List<ConfigArenashopEntity> GetShopList()
        {
            return _shopList;
        }

        /// <summary>
        /// 获取购买体力消耗钻石
        /// </summary>
        /// <param name="buyNumber"></param>
        /// <returns></returns>
        public int GetBuyStaminaPoint(int buyNumber)
        {
            if (buyNumber > 40)
                buyNumber = 41;
            if (_buyStaminaDic.ContainsKey(buyNumber))
                return _buyStaminaDic[buyNumber];
            return -1;
        }

        /// <summary>
        /// 获取体力恢复配置  秒
        /// </summary>
        /// <param name="vipLevel"></param>
        /// <returns></returns>
        public int StaminaRestoreTime(int vipLevel)
        {
            if (vipLevel <= 0)
                vipLevel = 0;
            else if (vipLevel >= 10)
                vipLevel = 10;
            var time = _staminaRestoreDic[vipLevel];
            //*60  秒
            return time*60;
        }

        /// <summary>
        /// 获取刷新商店需要点卷
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int GetRefreshShopPoint(int number)
        {
            if (number > 6)
                number = 6;
            if (!_arenaRefreshShop.ContainsKey(number))
                return 100;
            return _arenaRefreshShop[number];
        }

        public int GetKey(int number1, int number2)
        {
            return number2*10000 + number1;
        }

        #endregion
    }

    internal class ArenaUpFormationAskFor
    {
        public int ArenaType { get; set; }
        public int AskForType { get; set; }
        public string AskForValues { get; set; }

        public ArenaUpFormationAskFor(int arenaType, int askForType, string askForValues)
        {
            this.ArenaType = arenaType;
            this.AskForType = askForType;
            this.AskForValues = askForValues;
        }
    }
}
