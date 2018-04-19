using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;

using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Config.Custom;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class ItemsdicCache : BaseSingleton
    {
        #region encapsulation
        private List<DicItemEntity> _itemList;

        private List<DicItemEntity> _itemListNoSystem; 
        /// <summary>
        /// itemcode>>entity
        /// </summary>
        Dictionary<int, DicItemEntity> _itemsDic;
        /// <summary>
        /// itemtype>>list
        /// </summary>
        Dictionary<int, List<DicItemEntity>> _itemTypesDic;
        /// <summary>
        /// itemtype>>linkid>>entity
        /// </summary>
        private Dictionary<int, Dictionary<int, DicItemEntity>> _itemTypeLinkDic; 
        /// <summary>
        /// itemtype>>lapovercount
        /// </summary>
        private Dictionary<int, int> _itemTypeLapoverDic;
        /// <summary>
        /// ballsoulType>>level>>itemCode
        /// </summary>
        private Dictionary<int, Dictionary<int, int>> _ballsoulLevelupDic;
        /// <summary>
        /// color*1000+level->list
        /// </summary>
        private Dictionary<int, List<int>> _ballsoulColorLevelDic; 
        /// <summary>
        /// 彩色球魂
        /// </summary>
        private List<int> _ballsoulMultiColorList;
        /// <summary>
        /// 其他颜色球魂
        /// </summary>
        private List<int> _ballsoulOtherColorList;
        /// <summary>
        /// 合同页 key effvalues values mallcode
        /// </summary>
        private List<DicItemEntity> _theContractList;
        /// <summary>
        /// 出售物品配置
        /// </summary>
        private Dictionary<int, ConfigPrposellEntity> _prpoSellDic;

        private int _ballsoulMultiColorCount;

        private int _ballsoulOtherColorCount;

        /// <summary>
        /// stoneItemCode>>nextleve itemCode
        /// </summary>
        private Dictionary<int, int> _washStoneLevelupDic;

        private Dictionary<int, int> _itemNewPackCodeDic;

        private List<DicItemEntity> _legendPlayerList;

        public ItemsdicCache(int p)
            :base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            _itemList = DicItemMgr.GetAllForCache();
            _itemsDic = _itemList.ToDictionary(d => d.ItemCode, d => d);
            _itemListNoSystem = new List<DicItemEntity>();
            _itemTypesDic = new Dictionary<int, List<DicItemEntity>>();
            _itemTypeLinkDic = new Dictionary<int, Dictionary<int, DicItemEntity>>();
            _ballsoulLevelupDic=new Dictionary<int, Dictionary<int, int>>();
            _ballsoulMultiColorList=new List<int>();
            _ballsoulOtherColorList=new List<int>();
            _prpoSellDic = new Dictionary<int, ConfigPrposellEntity>();
            _ballsoulColorLevelDic = new Dictionary<int, List<int>>();

            foreach (var dicItem in _itemList)
            {
                if(dicItem.ItemType!=(int)EnumItemType.PlayerCard)
                    _itemListNoSystem.Add(dicItem);
                else if(dicItem.PlayerCardLevel!=(int)EnumPlayerCardLevel.White)
                {
                    _itemListNoSystem.Add(dicItem);
                }
                if(!_itemTypesDic.ContainsKey(dicItem.ItemType))
                    _itemTypesDic.Add(dicItem.ItemType,new List<DicItemEntity>());
                _itemTypesDic[dicItem.ItemType].Add(dicItem);

                if(!_itemTypeLinkDic.ContainsKey(dicItem.ItemType))
                    _itemTypeLinkDic.Add(dicItem.ItemType,new Dictionary<int, DicItemEntity>());
                _itemTypeLinkDic[dicItem.ItemType].Add(dicItem.LinkId,dicItem);
            }
            _ballsoulMultiColorCount = _ballsoulMultiColorList.Count;
            _ballsoulOtherColorCount = _ballsoulOtherColorList.Count;

            var list2 = DicItemtypeMgr.GetAll();
            _itemTypeLapoverDic = list2.ToDictionary(d => d.Idx, d => d.LapoverCount);

            var equipmentWashStoneList = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.EquipmentWashStoneList);
            var ss = equipmentWashStoneList.Split(',');
            _washStoneLevelupDic=new Dictionary<int, int>(ss.Length-1);
            for (int i = 0; i < ss.Length-1; i++)
            {
                _washStoneLevelupDic.Add(ConvertHelper.ConvertToInt(ss[i]),ConvertHelper.ConvertToInt(ss[i+1]));
            }
            list2 = null;

            var itemNewPackCodes = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.ItemNewPackCodes);
            var ii = itemNewPackCodes.Split(',');
            _itemNewPackCodeDic=new Dictionary<int, int>(ii.Length);
            foreach (var s in ii)
            {
                _itemNewPackCodeDic.Add(ConvertHelper.ConvertToInt(s),0);
            }
            _legendPlayerList = _itemTypesDic[1].FindAll(d =>d.PlayerCardLevel == 1 || d.PlayerCardLevel == 7);

            _theContractList = DicItemMgr.GetContractItem();

            var list3 = ConfigPrposellMgr.GetAll();
            foreach (var item in list3)
            {
                var key = PrpoSellKey(item.ItemType, item.Quality);
                if (!_prpoSellDic.ContainsKey(key))
                    _prpoSellDic.Add(key, item);
                else
                    _prpoSellDic[key] = item;
            }
        }


        private int PrpoSellKey(int itemType, int quality)
        {
            return itemType * 10000 + quality;
        }

        int BallSoulColorLevelKey(int color,int level)
        {
            return color*1000 + level;
        }
        #endregion

        #region Facade
        public static ItemsdicCache Instance
        {
            get { return SingletonFactory<ItemsdicCache>.SInstance; }
        }

        public string GetItemName(int itemCode)
        {
            var item = GetItem(itemCode);
            if (item != null)
                return item.ItemName;
            else
            {
                return "";
            }
        }

        public DicItemEntity GetItem(int itemCode)
        {
            if (_itemsDic.ContainsKey(itemCode))
            {
                return _itemsDic[itemCode];
            }
            else
            {
                return null;
            }
        }
        


        public DicItemEntity GetItemByType(int linkId, EnumItemType itemType)
        {
            return GetItemByType(linkId, (int) itemType);
        }

        public ConfigPrposellEntity GetPrpoSell(int itemType, int quality)
        {
            var key = PrpoSellKey(itemType, quality);
            if (_prpoSellDic.ContainsKey(key))
                return _prpoSellDic[key];
            return null;
        }

        public DicItemEntity GetItemByType(int linkId, int itemType)
        {
            if (_itemTypeLinkDic.ContainsKey(itemType))
            {
                if (_itemTypeLinkDic[itemType].ContainsKey(linkId))
                    return _itemTypeLinkDic[itemType][linkId];
            }
            return null;
        }

        public DicItemEntity GetItemByPlayerId(int playerId)
        {
            var players = _itemTypesDic[(int)EnumItemType.PlayerCard];
            foreach (var entity in players)
            {
                if (entity.LinkId == playerId)
                {
                    //if (entity.PlayerCardLevel == (int)EnumPlayerCardLevel.Euro)
                    //{
                    //    var skill = PlayersdicCache.Instance.GetStarSkill(playerId, 9);
                    //    if (!string.IsNullOrEmpty(skill))
                    //        return entity;
                    //}
                    //else
                        return entity;

                }
            }
            return null;
        }

        public List<DicItemEntity> GetItemAll()
        {
            return _itemListNoSystem;
        }

        public List<DicItemEntity> GetLegendPlayerList()
        {
            return _legendPlayerList;
        }

        public DicItemEntity RandomItem(EnumItemType itemType)
        {
            int type = (int) itemType;
            if (_itemTypesDic.ContainsKey(type))
            {
                var list = _itemTypesDic[type];
                int index = RandomHelper.GetInt32WithoutMax(0, list.Count);
                return list[index];
            }
            else
            {
                return null;
            }
        }

        public int RandomBallsoulMulticolorForWCH()
        {
            return _ballsoulMultiColorList[RandomHelper.GetInt32WithoutMax(0, _ballsoulMultiColorCount)];
        }

        public int RandomBallsoulOthercolorForWCH()
        {
            return _ballsoulOtherColorList[RandomHelper.GetInt32WithoutMax(0, _ballsoulOtherColorCount)];
        }

        public int RandomBallsoul(int color, int level)
        {
            var key = BallSoulColorLevelKey(color, level);
            if (_ballsoulColorLevelDic.ContainsKey(key))
            {
                var list = _ballsoulColorLevelDic[key];
                return list[RandomHelper.GetInt32WithoutMax(0, list.Count)];
            }
            return 0;
        }


        /// <summary>
        /// 根据ItemCode 获取合成页合成得到的物品
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int GetTheContractItemCode(int itemCode)
        {
            if (_itemsDic.ContainsKey(itemCode))
            {
                var mallId = _itemsDic[itemCode].LinkId;
                return CacheFactory.MallCache.GetTheContractRewardCode(mallId);
            }
            return 0;
        }


        public Dictionary<int, int> GetLapoverDic()
        {
            return _itemTypeLapoverDic;
        }

        public int GetLapover(int itemType)
        {
            if (_itemTypeLapoverDic.ContainsKey(itemType))
                return _itemTypeLapoverDic[itemType];
            else
            {
                return 1;
            }
        }

        public DicPlayerEntity GetPlayerByItemCode(int itemCode)
        {
            var itemCache = GetItem(itemCode);
            if (itemCache == null || itemCache.ItemType!=(int)EnumItemType.PlayerCard)
                return null;
            return CacheFactory.PlayersdicCache.GetPlayer(itemCache.LinkId);
        }

        public DicEquipmentEntity GetEquipmentByItemCode(int itemCode)
        {
            var itemCache = GetItem(itemCode);
            if (itemCache == null || itemCache.ItemType != (int)EnumItemType.Equipment)
                return null;
            return CacheFactory.EquipmentCache.GetEquipment(itemCache.LinkId);
        }

        public int GetWashStoneNextCode(int itemCode)
        {
            if (_washStoneLevelupDic.ContainsKey(itemCode))
                return _washStoneLevelupDic[itemCode];
            return 0;
        }

        public DicMallItemDataEntity GetMallEntityWithoutPointByItemCode(int itemCode)
        {
            var itemCache = GetItem(itemCode);
            if (itemCache == null || itemCache.ItemType != (int)EnumItemType.MallItem)
                return null;
            return CacheFactory.MallCache.GetMallEntityWithoutPoint(itemCache.LinkId);
        }


        public int GetBallsoulNextCode(int ballsoulType, int nextLevel)
        {
            if (_ballsoulLevelupDic.ContainsKey(ballsoulType))
            {
                if (_ballsoulLevelupDic[ballsoulType].ContainsKey(nextLevel))
                    return _ballsoulLevelupDic[ballsoulType][nextLevel];
            }
            return 0;
        }

        public bool IsNewPlayerPack(int itemCode)
        {
            return _itemNewPackCodeDic.ContainsKey(itemCode);
        }

        public DicItemEntity RandomEquipment(int quality)
        {
            var equipments = _itemTypesDic[(int) EnumItemType.Equipment];
            var list = equipments.FindAll(d =>d.SubType==4 && d.ThirdType == quality);
            if(list.Count>0)
            {
                var index = RandomHelper.GetInt32WithoutMax(0, list.Count);
                return list[index];
            }
            else
            {
                return null;
            }
        }

        public DicItemEntity RandomPlayerCard(int cardlevel, int minpower, int maxpower)
        {
            List<DicItemEntity> list = null;
            if (cardlevel == 100)
            {
                list = _legendPlayerList;
            }
            else
            {
                var players = _itemTypesDic[(int)EnumItemType.PlayerCard];
                list = players.FindAll(d => d.PlayerCardLevel == cardlevel && d.FourthType >= minpower && d.FourthType <= maxpower);
            }
            if(list.Count>0)
            {
                var index = RandomHelper.GetInt32WithoutMax(0, list.Count);
                return list[index];
            }
            else
            {
                return null;
            }
        }

        
        public DicItemEntity LotteryTheContract(int cardlevel, int minpower, int maxpower)
        {
             List<DicItemEntity> list = null;
           
            if (cardlevel == 100)
            {
                list = _legendPlayerList;
            }
            else
            {
                list = _theContractList.FindAll(d => d.PlayerCardLevel == cardlevel && d.PlayerKpi >= minpower && d.PlayerKpi <= maxpower);
            }
            if(list.Count>0)
            {
                var index = RandomHelper.GetInt32WithoutMax(0, list.Count);
                return list[index];
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// 根据球员id获取合同页
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public DicItemEntity LotteryTheContract(int playerId)
        {
            List<DicItemEntity> list = null;

            list = _theContractList.FindAll(d => d.ImageId  == playerId);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 根据球员id获取合同页
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public int LotteryTheContractId(int playerId)
        {
            List<DicItemEntity> list = null;

            list = _theContractList.FindAll(d => d.ImageId == playerId);
            if (list.Count > 0)
            {
                return list[0].ItemCode;
            }
            else
            {
                return 0;
            }

        }

        public List<int> LotteryTheContractList(int cardlevel, int minpower, int maxpower)
        {
            List<int> list = null;

            if (cardlevel == 100)
            {
                list = new List<int>();
                foreach (var item in _legendPlayerList)
                {
                    list.Add(item.ItemCode);
                }
            }
            else if (cardlevel == 0)//不限等级
            {
                list = new List<int>();
                foreach (
                    var item in
                        _theContractList.FindAll(
                            d => d.ItemCode < 395000 && d.PlayerKpi >= minpower && d.PlayerKpi <= maxpower))
                {
                    list.Add(item.ItemCode);
                }
            }
            else
            {
                //itemcode >395000是装备碎片
                list = new List<int>();
                foreach (
                    var item in
                        _theContractList.FindAll(
                            d => d.ItemCode < 395000 && d.PlayerCardLevel == cardlevel && d.PlayerKpi >= minpower && d.PlayerKpi <= maxpower))
                {
                    if (item.ItemCode == 390262) //卡卡碎片不可以被抽到
                        continue;
                    list.Add(item.ItemCode);
                }
            }
            return list;
        }

        public DicItemEntity RandomPlayerCard(int minpower, int maxpower)
        {
            var players = _itemTypesDic[(int)EnumItemType.PlayerCard];
            var list = players.FindAll(d => d.FourthType >= minpower && d.FourthType <= maxpower);
            if (list.Count > 0)
            {
                var index = RandomHelper.GetInt32WithoutMax(0, list.Count);
                return list[index];
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
