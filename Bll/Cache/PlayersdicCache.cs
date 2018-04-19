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
    public class PlayersdicCache : BaseSingleton
    {

        Dictionary<int, DicPlayerEntity> _playersDic;
        private Dictionary<int, List<DicStarskillsEntity>> _starskillDic;
        private List<string> _starskillPlusList;
        private List<DicPlayerEntity> _playerEntities;
        /// <summary>
        /// pid->same player list
        /// </summary>
        private Dictionary<int, List<int>> _playerLinkDetailDic;
        /// <summary>
        /// 不能通过合成得到的元老卡
        /// </summary>
        private List<int> _notSynthesisPlayer;
        /// <summary>
        /// 球员卡星级配置
        /// </summary>
        private Dictionary<int,ConfigPlayerthestarEntity> _theStarDic;

        private List<int> allPotentialId;
        private Dictionary<int, ConfigPotentialEntity> _potentialDic;

        private Dictionary<string, List<ConfigPotentialcardEntity>> _potentialCardDic;
        private int _starskillplusCount;
        public PlayersdicCache(int p)
            : base(p)
        {
            InitCache();
        }

        #region encapsulation
        void InitCache()
        {
            var list = DicPlayerMgr.GetAllForCache();
            _playersDic = list.ToDictionary(d => d.Idx, d => d);
            _playerEntities = list;
            var list2 = DicStarskillsMgr.GetAllForCache();
            _starskillDic = new Dictionary<int, List<DicStarskillsEntity>>(list2.Count);
            _starskillPlusList = new List<string>(list2.Count);
            _starskillplusCount = 0;
            foreach (var entity in list2)
            {
                if (entity.IsValid)
                {
                    if (!_starskillDic.ContainsKey(entity.PlayerId))
                        _starskillDic[entity.PlayerId] = new List<DicStarskillsEntity>();
                    _starskillDic[entity.PlayerId].Add(entity);
                    _starskillPlusList.Add(entity.PlusCode);
                    _starskillplusCount++;
                }
            }

            _notSynthesisPlayer = new List<int>();
            var playerString = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.NotSynthesisPlayer);
            var pList = playerString.Split(',');
            foreach (var p in pList)
            {
                int pid = ConvertHelper.ConvertToInt(p, 0);
                if (pid != 0)
                    _notSynthesisPlayer.Add(pid);
            }
            _playerLinkDetailDic = new Dictionary<int, List<int>>();
            var list3 = DicPlayerlinkMgr.GetAll();
            foreach (var entity in list3)
            {
                if (!_playerLinkDetailDic.ContainsKey(entity.LinkId))
                {
                    _playerLinkDetailDic.Add(entity.LinkId, new List<int>());
                }
                _playerLinkDetailDic[entity.LinkId].Add(entity.Idx);
            }
            foreach (var entity in list3)
            {
                if (!_playerLinkDetailDic.ContainsKey(entity.Idx))
                {
                    _playerLinkDetailDic.Add(entity.Idx, new List<int>());
                }
                _playerLinkDetailDic[entity.Idx].Add(entity.LinkId);
                var links = _playerLinkDetailDic[entity.LinkId];
                foreach (var link in links)
                {
                    if (link != entity.Idx)
                    {
                        _playerLinkDetailDic[entity.Idx].Add(link);
                    }
                }
            }

            _theStarDic = new Dictionary<int, ConfigPlayerthestarEntity>();
            var allTheStar = ConfigPlayerthestarMgr.GetAll();
            _theStarDic = allTheStar.ToDictionary(r => r.Idx, r => r);

            var allPotential = ConfigPotentialMgr.GetAll();
            _potentialDic = new Dictionary<int, ConfigPotentialEntity>();
            allPotentialId = new List<int>();
            foreach (var item in allPotential)
            {
                if (!allPotentialId.Exists(r => r == item.PotentialId))
                    allPotentialId.Add(item.PotentialId);
               var key = GetKey(item.Level,item.PotentialId);
                if (!_potentialDic.ContainsKey(key))
                    _potentialDic.Add(key, item);
            }

            _potentialCardDic = new Dictionary<string, List<ConfigPotentialcardEntity>>();
            var allcard = ConfigPotentialcardMgr.GetAll();
            foreach (var item in allcard)
            {
                if (!_potentialCardDic.ContainsKey(item.CardLevel))
                    _potentialCardDic.Add(item.CardLevel, new List<ConfigPotentialcardEntity>());
                _potentialCardDic[item.CardLevel].Add(item);
            }
        }
        #endregion

        #region Facade
        public static PlayersdicCache Instance
        {
            get { return SingletonFactory<PlayersdicCache>.SInstance; }
        }

        public int GetKey(int number1, int number2)
        {
            return number1*10000 + number2;
        }

        public DicPlayerEntity GetPlayer(int playerId)
        {
            if (_playersDic.ContainsKey(playerId))
            {
                return _playersDic[playerId];
            }
            else
            {
                return null;
            }
        }

        public string GetRandomPlayerName()
        {
            var list = _playersDic.Values.ToList();
            return list[RandomHelper.GetInt32(0, list.Count - 1)].Name;
        }

        public int GetPlayerPosition(int playerId)
        {
            var player = GetPlayer(playerId);
            if (player != null)
                return player.Position;
            else
            {
                return 0;
            }
        }

        public string GetPlayerName(int playerId)
        {
            var player = GetPlayer(playerId);
            if (player != null)
                return player.Name;
            else
            {
                return "";
            }
        }

        //public bool CheckStarSkillPlus(int playerId, int strength, string plusCode)
        //{
        //    bool check = false;
        //    var entity = GetStarSkillEntity(playerId);
        //    if (entity != null && strength >= entity.Strength && entity.PlusCode==plusCode)
        //    {
        //        check= true;
        //    }
        //    return check;
        //}
        //public bool CheckStarSkillPlus(int playerId, int strength, string plusCode)
        //{
        //    var list = GetStarSkillEntity(playerId);
        //    if (null == list)
        //        return false;
        //    foreach (var item in list)
        //    {
        //        if (strength < item.Strength || item.PlusCode != plusCode)
        //            continue;
        //        return true;
        //    }
        //    return false;
        //}


        public string GetStarSkill(int playerId, int strength, int arousalLv = 0)
        {
            if (ShareUtil.IsAppRXYC)
                return GetStarSkill4AppRXYC(playerId, strength);
            string skill = "";
            var list = GetStarSkillEntity(playerId);
            if (null == list)
                return skill;
            foreach (var item in list)
            {
                if (strength < item.Strength)
                    continue;
                if (arousalLv == 0)
                    skill += item.SkillCode + ",";
                else
                    skill = item.SkillCode + "_" + arousalLv + ",";
            }
            skill = skill.TrimEnd(',');
            return skill;
        }

        string GetStarSkill4AppRXYC(int playerId, int strength)
        {
            strength = Math.Min(9, strength);
            string skill = "";
            var list = GetStarSkillEntity(playerId);
            if (null == list)
                return skill;
            foreach (var item in list)
            {
                skill += item.SkillCode + "_" + strength + ",";
            }
            skill = skill.TrimEnd(',');
            return skill;
        }

        List<DicStarskillsEntity> GetStarSkillEntity(int playerId)
        {
            List<DicStarskillsEntity> entity = null;
            _starskillDic.TryGetValue(playerId, out entity);
            return entity;
        }

        public string RandomStarSkillPlus()
        {
            if (_starskillPlusList.Count > 0)
            {
                int index = RandomHelper.GetInt32WithoutMax(0, _starskillPlusList.Count);
                return _starskillPlusList[index];
            }
            return "";
        }

        public List<int> GetLinkPlayerList(int playerId)
        {
            if (_playerLinkDetailDic.ContainsKey(playerId))
                return _playerLinkDetailDic[playerId];
            return null;
        }
        /// <summary>
        /// 根据橙卡来找对应的黑金卡
        /// </summary>
        /// <param name="playerId">橙卡ID</param>
        /// <returns>对应的黑金卡ID</returns>
        public int GetLinkPlayer(int playerId)
        {
            foreach (int k in _playerLinkDetailDic.Keys)
            {
                foreach (int pid in _playerLinkDetailDic[k])
                {
                    if (pid == playerId)
                        return k;
                }
            }
            return 0;
        }

        //List<DicStarskillsEntity> GetStarSkillEntity(int playerId)
        //{
        //    List<DicStarskillsEntity> entity = null;
        //    _starskillDic.TryGetValue(playerId, out entity);
        //    return entity;
        //}

        //public string GetStarSkill(int playerId, int strength, int arousalLv = 0)
        //{
        //    string skill = "";
        //    var list = GetStarSkillEntity(playerId);
        //    if (null == list)
        //        return skill;
        //    foreach (var item in list)
        //    {
        //        if (strength < item.Strength)
        //            continue;
        //        if (arousalLv == 0)
        //            skill += item.SkillCode + ",";
        //        else
        //            skill += item.SkillCode + "_" + arousalLv + ",";
        //    }
        //    skill = skill.TrimEnd(',');
        //    return skill;
        //}


        /// <summary>
        /// 根据合成KPI值范围确定可能合成的所有球员卡
        /// </summary>
        /// <param name="minKpi"></param>
        /// <param name="maxKpi"></param>
        /// <returns></returns>
        public List<int> GetSynthesisResult(double minKpi, double maxKpi, bool isEuro)
        {
            List<int> itemCodeList = new List<int>();
            foreach (var player in _playersDic.Values)
            {
                if (player.Capacity >= minKpi && player.Capacity <= maxKpi)
                {
                    if(player.KpiLevel=="N")//Npc球员
                        continue;

                    if (player.CardLevel == (int)EnumPlayerCardLevel.White || player.CardLevel ==(int)EnumPlayerCardLevel.BlackGold)
                        continue;

                    if (!isEuro)
                    {
                        if (player.CardLevel == (int)EnumPlayerCardLevel.Euro)
                            continue;
                    }
                    else
                    {
                        //欧冠卡合成不能合成梅西、C罗
                        if (player.Idx == 90001 || player.Idx == 90002)
                            continue;

                        if (player.CardLevel != (int)EnumPlayerCardLevel.Euro)
                            continue;
                    }
                    //去除不能通过合成得到的元老卡
                    if (_notSynthesisPlayer.Contains(player.Idx))
                        continue;
                    
                    var item = CacheFactory.ItemsdicCache.GetItemByPlayerId(player.Idx);
                    if (item != null)
                        itemCodeList.Add(item.ItemCode);
                }
            }
            return itemCodeList;
        }

        /// <summary>
        /// 升星配置
        /// </summary>
        /// <param name="theStar">需要升到的星级</param>
        /// <returns></returns>
        public ConfigPlayerthestarEntity UpgradeTheStarCoin(int theStar)
        {
            if (_theStarDic.ContainsKey(theStar))
                return _theStarDic[theStar];
            return null;
        }
        /// <summary>
        /// 返回所有球员
        /// </summary>
        /// <returns></returns>
        public List<DicPlayerEntity> GetAllPlayerEntities()
        {
            if (_playerEntities.Count>0)
            return _playerEntities;
            return null;
        }

        /// <summary>
        /// 随机获取一个潜能
        /// </summary>
        /// <param name="playerPosition"></param>
        /// <param name="potentialList"></param>
        /// <param name="potentialLevel"></param>
        /// <returns></returns>
        public ConfigPotentialEntity GetRandomPotential(string playerPosition, List<Potential> potentialList,
            int potentialLevel)
        {
            for (int i = 0; i < 20; i++)
            {
                var potentialId = allPotentialId[RandomHelper.GetInt32WithoutMax(0, allPotentialId.Count)];
                if (potentialId == 0)
                    continue;
                var key = GetKey(potentialLevel, potentialId);
                if (!_potentialDic.ContainsKey(key))
                    continue;
                var potential = _potentialDic[key];
                if (playerPosition == "GK" && potential.GKGetType == 2)
                    continue;
                else if (playerPosition != "GK" && potential.GKGetType == 3)
                    continue;
                else if (potentialList.Exists(r => r.Idx == potentialId))
                    continue;
                return potential;
            }
            return null;
        }

        /// <summary>
        /// 获取随机潜力等级
        /// </summary>
        /// <param name="playerPosition"></param>
        /// <returns></returns>
        public int GetPotentialLevel(string playerPosition)
        {
            if (!_potentialCardDic.ContainsKey(playerPosition))
                return 0;
            var list = _potentialCardDic[playerPosition];
            var rate = RandomHelper.GetInt32(0, 10000);
            foreach (var item in list)
            {
                if (rate <= item.Rate)
                {
                    return item.PotentialLevel;
                }
            }
            return 0;
        }
        /// <summary>
        /// 获取潜力值
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public Potential GetPotentialValue(ConfigPotentialEntity config)
        {
            Potential p = new Potential();
            p.Idx = config.PotentialId;
            p.Level = config.Level;
            if (config.BuffType == 2) //百分比
            {
                var buff = RandomHelper.GetInt32((int)(config.MinBuff * 100),
                    (int)(config.MaxBuff * 100));
                p.Buff = (decimal)buff / 100;
            }
            else
                p.Buff = RandomHelper.GetInt32((int)config.MinBuff, (int)config.MaxBuff);
            return p;
        }
        public ConfigPotentialEntity GetPotentialConfig(int potentialLevel, int potentialId)
        {
            var key = GetKey(potentialLevel, potentialId);
            ConfigPotentialEntity val;
            _potentialDic.TryGetValue(key, out val);
            return val;
        }
        #endregion
    }
}
