using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Dal;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class LotteryCache: BaseSingleton
    {
        #region encapsulation

        private int _scoutingKpiLimit=88;

        /// <summary>
        /// lotteryType*1000+subType->list(level,vip,time)
        /// </summary>
        Dictionary<int, List<ConfigLotteryEntity>> _lotteryDic;
        /// <summary>
        /// libraryId->list->itemCode
        /// </summary>
        Dictionary<int,List<int>> _cardLibrary;
        /// <summary>
        /// lotteryId->relation->lotterycardlibrary
        /// </summary>
        private Dictionary<int, RandomRelation> _lotteryRelationDic;
        /// <summary>
        /// equip suitId*100+quality->itemCode list
        /// 按套装id和品质，随机套装内物品
        /// </summary>
        private Dictionary<int, List<int>> _lotteryEquipmentSuitDic;
        /// <summary>
        /// suitId->itemCode list
        /// 按套装id，随机套装图纸
        /// </summary>
        private Dictionary<int, List<int>> _lotterySuitdrawingDic;
        /// <summary>
        /// suitType->suitId
        /// </summary>
        private Dictionary<int, List<int>> _lotterySuitTypeDic;
        private Dictionary<int, DicArenabagconfigEntity> _arenaBagConfigDic;
        private List<int> _scoutingBlackGolds;
       
        private int _scoutingTenOrangeCount;
        public LotteryCache(int p)
            :base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            LogHelper.Insert("Lottery cache init start", LogType.Info);
            _scoutingTenOrangeCount = 100;
                //CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ScoutingTenOrangeCount);
            var list = ConfigLotteryMgr.GetAll();
            var list2 = ConfigLotteryrelationMgr.GetAll();
            _lotteryDic = new Dictionary<int, List<ConfigLotteryEntity>>();
            _lotteryRelationDic = new Dictionary<int, RandomRelation>(list.Count);
            foreach (var entity in list)
            {
                var key = BuildLotteryKey(entity.Type, entity.SubType);
                if(!_lotteryDic.ContainsKey(key))
                    _lotteryDic.Add(key,new List<ConfigLotteryEntity>());
                if(entity.MinTime==ShareUtil.BaseTime)
                    _lotteryDic[key].Insert(0,entity);
                else
                {
                    _lotteryDic[key].Add(entity);
                }
                var relationlist = list2.FindAll(d => d.LotteryId == entity.Idx);
                _lotteryRelationDic.Add(entity.Idx,BuildRelation(relationlist));
            }
            
            var list3 = DicCardlibraryMgr.GetAll();
            var itemList = ItemsdicCache.Instance.GetItemAll();
            var playerList = CacheFactory.PlayersdicCache.GetAllPlayerEntities();
            var legendList = ItemsdicCache.Instance.GetLegendPlayerList();
            _arenaBagConfigDic=new Dictionary<int, DicArenabagconfigEntity>();
            var list4=DicArenabagconfigMgr.GetAll();
            foreach (var entity4 in list4)
            {
                if (!_arenaBagConfigDic.ContainsKey(entity4.Idx))
                {
                    _arenaBagConfigDic.Add(entity4.Idx,entity4);
                }
            }
            _cardLibrary = new Dictionary<int, List<int>>(list3.Count);
            foreach (var entity in list3)
            {
                if (entity.ItemType == 10000)//球员碎片
                {
                    _cardLibrary.Add(entity.Idx, new List<int>(1));
                    _cardLibrary[entity.Idx].Add(entity.SubType);
                }
                else if (entity.ItemType == 20000)//教练碎片
                {
                    var coachDebristList = CacheFactory.MallCache.GetCoachDebristList();
                    _cardLibrary.Add(entity.Idx, coachDebristList);
                }
                    //合同页
                else if (entity.ItemType == 1000)
                {
                    _cardLibrary.Add(entity.Idx,
                        CacheFactory.ItemsdicCache.LotteryTheContractList(entity.SubType, entity.MinPower,
                            entity.MaxPower));
                }
                else
                {
                    List<DicItemEntity> items = null;
                    List<DicPlayerEntity> listByPlayer = null;

                    if (entity.ItemType == 1 && entity.SubType == 100)
                    {
                        items = legendList.FindAll(d => (entity.ThirdType == 0 || d.ThirdType == entity.ThirdType)
                                                        &&
                                                        (entity.MinPower == 0 ||
                                                         (d.FourthType >= entity.MinPower &&
                                                          d.FourthType <= entity.MaxPower))
                            );
                    }
                    else if (entity.ItemType == 2 && entity.SubType > 100)
                    {
                        items = itemList.FindAll(d => (entity.ThirdType == 0 || d.ThirdType == entity.ThirdType)
                                                      && d.FourthType == entity.SubType
                            );
                    }
                        #region “天空之城”等特殊条件卡包（竞技场卡包）

                    else if (entity.Type == 1 && entity.ItemType == 1 && entity.SubType == 6)
                        //subType==6为竞技礼包类型    ThirdType为_arenaBagConfigDic礼包的idx
                    {
                        if (_arenaBagConfigDic.ContainsKey(entity.ThirdType))
                        {
                            items = SetArenaBagConfig(_arenaBagConfigDic[entity.ThirdType], itemList);
                        }

                    }
                        #endregion

                    else
                    {
                        items = itemList.FindAll(d => d.ItemType == entity.ItemType
                                                      && (entity.SubType == 0 || d.SubType == entity.SubType)
                                                      && (entity.ThirdType == 0 || d.ThirdType == entity.ThirdType)
                                                      &&
                                                      (entity.MinPower == 0 ||
                                                       (d.FourthType >= entity.MinPower &&
                                                        d.FourthType <= entity.MaxPower))
                            );

                    }

                    if (items.Count <= 0)
                    {
                        //LogHelper.Insert("no item in card library,library id:" + entity.Idx,LogType.Info);
                        throw new Exception("no item in card library,library id:" + entity.Idx);
                    }
                    _cardLibrary.Add(entity.Idx, new List<int>(items.Count));
                    //LogHelper.Insert("card library,library id:" + entity.Idx + ",item count:" + items.Count, LogType.Info);
                    foreach (var item in items)
                    {
                        _cardLibrary[entity.Idx].Add(item.ItemCode);
                    }
                }
            }
            var equipList = itemList.FindAll(d => d.ItemType == (int) EnumItemType.Equipment);
            _lotteryEquipmentSuitDic= new Dictionary<int, List<int>>();
            _lotterySuitTypeDic = new Dictionary<int, List<int>>();
            foreach (var entity in equipList)
            {
                var key = BuildEquipKey(entity.FourthType, entity.ThirdType);
                if (!_lotteryEquipmentSuitDic.ContainsKey(key))
                    _lotteryEquipmentSuitDic.Add(key, new List<int>());
                _lotteryEquipmentSuitDic[key].Add(entity.ItemCode);
                if (!_lotterySuitTypeDic.ContainsKey(entity.EquipmentSuitType))
                {
                    _lotterySuitTypeDic.Add(entity.EquipmentSuitType,new List<int>());
                }
                if (!_lotterySuitTypeDic[entity.EquipmentSuitType].Contains(entity.EquipmentSuitId))
                {
                    _lotterySuitTypeDic[entity.EquipmentSuitType].Add(entity.EquipmentSuitId);
                }
            }

         
            _scoutingBlackGolds=new List<int>();
            var s = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.ScoutingBlackGoldConfig);
            var ss = s.Split(',');
            foreach (var s1 in ss)
            {
                _scoutingBlackGolds.Add(Convert.ToInt32(s1));
            }

           
            LogHelper.Insert("Lottery cache init end", LogType.Info);
        }
        
        RandomRelation BuildRelation(List<ConfigLotteryrelationEntity> list)
        {
            List<RandomRate> rateList = new List<RandomRate>(list.Count);
            int maxSeed = 0;
            foreach (var entity in list)
            {
                rateList.Add(BuildRate(ref maxSeed,entity.Rate,entity.LibraryId));
            }
            return new RandomRelation(maxSeed + 1, rateList);
        }

        RandomRate BuildRate(ref int maxSeed, int rate,int linkId)
        {
            int rateBegin = maxSeed + 1;
            maxSeed = maxSeed + rate;
            return new RandomRate(linkId, rateBegin, maxSeed);
        }

        int BuildLotteryKey(int lotteryType, int subType)
        {
            return lotteryType*1000 + subType;
        }
        
        int BuildEquipKey(int suitId, int quality)
        {
            return suitId*100 + quality;
        }

        List<int> LotteryItem(ConfigLotteryEntity lotteryEntity, int cardCount, List<int> prizeEquipments)
        {
            var list = new List<int>(cardCount);
            for (int i = 0; i < cardCount; i++)
            {
                var item = LotteryItem(lotteryEntity, prizeEquipments);
                while (list.Exists(d => d == item)) //排除重复
                {
                    item = LotteryItem(lotteryEntity);
                }
                list.Add(item);
            }
            return list;
        }


        List<int> LotteryItem(ConfigLotteryEntity lotteryEntity, int cardCount, int orangeMax, int orangeLib, int lowLib, ref int orangeCount, int limitedOrangeCount,out List<int> limitedCardList )
        {
            var list = new List<int>(cardCount);
            orangeCount = 0;
            limitedCardList = new List<int>();
            var orangeIndex = RandomHelper.GetInt32WithoutMax(0, cardCount);
            for (int i = 0; i < cardCount; i++)
            {
                int itemCode = 0;
                if (i == orangeIndex && orangeCount == 0 && orangeLib > 0)
                {
                    itemCode = LotteryByLib(orangeLib);
                    orangeCount++;
                }
                else if (orangeCount >= orangeMax)
                {
                    itemCode = LotteryByLib(lowLib);
                }
                else
                {
                    itemCode = LotteryItem(lotteryEntity);
                    while (list.Exists(d => d == itemCode)) //排除重复
                    {
                        itemCode = LotteryItem(lotteryEntity);
                    }
                    var itemDic = CacheFactory.ItemsdicCache.GetItem(itemCode);
                    if (limitedOrangeCount >= 3)//已经有3个89以上了
                    {
                        while (list.Exists(d => d == itemCode) ||
                            (itemDic.ItemType == (int)EnumItemType.PlayerCard && itemDic.PlayerKpi > 89 && (itemDic.LinkId != 30001 || itemDic.LinkId != 30002)))
                        {
                            itemCode = LotteryItem(lotteryEntity);
                            itemDic = CacheFactory.ItemsdicCache.GetItem(itemCode);
                        }
                    }

                    if (itemDic.ItemType == (int)EnumItemType.PlayerCard && itemDic.PlayerKpi > 89 && (itemDic.LinkId != 30001 || itemDic.LinkId != 30002))
                    {
                        limitedCardList.Add(itemDic.ItemCode);
                        limitedOrangeCount++;
                    }

                    if (itemDic.ItemType == (int)EnumItemType.PlayerCard && itemDic.PlayerCardLevel == (int)EnumPlayerCardLevel.Orange)
                    {
                        orangeCount++;
                    }
                }
                list.Add(itemCode);
            }
            return list;
        }

        private List<DicItemEntity> SetArenaBagConfig(DicArenabagconfigEntity bagConfig,List<DicItemEntity> itemList)
        {
            List<DicItemEntity> items = new List<DicItemEntity>();
            List<DicPlayerEntity> bagList = DicPlayerMgr.GetAllForCache();
            try
            {
                #region 判断每个属性是否符合竞技场卡包配置，现在只能配置(所属赛区,球员位置,卡牌颜色等级,kpi等级,能力值,身高，体重，生日）

                if (!string.IsNullOrEmpty(bagConfig.Area))
                {
                    var str = bagConfig.Area;
                    string[] strs = str.Split('|');
                    if (strs.Length > 1)
                    {
                        List<int> dicS=new List<int>();
                        for (int i = 0; i < strs.Length; i++)
                        {
                            int s = ConvertHelper.ConvertToInt(strs[i]);
                            if (!dicS.Contains(s))
                            {
                                dicS.Add(s);
                            }
                        }
                        bagList.RemoveAll(b => (!dicS.Contains(b.Area)));
                        
                    }
                }
                if (!string.IsNullOrEmpty(bagConfig.PositionDesc))
                {
                    var str = bagConfig.PositionDesc;
                    string[] strs = str.Split('|');
                    if (strs.Length > 1)
                    {
                        List<string> dicS = new List<string>();
                        for (int i = 0; i < strs.Length; i++)
                        {
                          
                            if (!dicS.Contains(strs[i]))
                            {
                                dicS.Add(strs[i]);
                            }
                        }
                        bagList.RemoveAll(b => (!dicS.Contains(b.PositionDesc)));

                    }
                }

                if (!string.IsNullOrEmpty(bagConfig.CardLevel))
                {
                    var str = bagConfig.CardLevel;
                    string[] strs = str.Split('|');
                    if (strs.Length > 1)
                    {
                        List<int> dicS = new List<int>();
                        for (int i = 0; i < strs.Length; i++)
                        {
                            int s = ConvertHelper.ConvertToInt(strs[i]);
                            if (!dicS.Contains(s))
                            {
                                dicS.Add(s);
                            }
                        }
                        bagList.RemoveAll(b => (!dicS.Contains(b.CardLevel)));

                    }
                }
                if (!string.IsNullOrEmpty(bagConfig.KpiLevel))
                {
                    var str = bagConfig.KpiLevel;
                    string[] strs = str.Split('|');
                    if (strs.Length > 1)
                    {
                        List<string> dicS = new List<string>();
                        for (int i = 0; i < strs.Length; i++)
                        {

                            if (!dicS.Contains(strs[i]))
                            {
                                dicS.Add(strs[i]);
                            }
                        }
                        bagList.RemoveAll(b => (!dicS.Contains(b.KpiLevel)));

                    }
                }
                if (!string.IsNullOrEmpty(bagConfig.Capacity))
                {
                    var str = bagConfig.Capacity;
                    string[] strs = str.Split('|');
                    if (strs.Length == 2)
                    {
                        var min = ConvertHelper.ConvertToInt(strs[0]);
                        var max = ConvertHelper.ConvertToInt(strs[1]);
                        if (max == 0)
                            bagList.RemoveAll(b => (b.Capacity < min));
                        else
                        {
                            bagList.RemoveAll(b => (b.Capacity < min || b.Capacity > max));
                        }

                    }
                }
                if (!string.IsNullOrEmpty(bagConfig.Stature))
                {
                    var str = bagConfig.Stature;
                    string[] strs = str.Split('|');
                    if (strs.Length == 2)
                    {
                        var min = ConvertHelper.ConvertToInt(strs[0]);
                        var max = ConvertHelper.ConvertToInt(strs[1]);
                        if (max == 0)
                            bagList.RemoveAll(b => (b.Stature < min));
                        else
                        {
                            bagList.RemoveAll(b => (b.Stature < min || b.Stature > max));
                        }

                    }
                }
                if (!string.IsNullOrEmpty(bagConfig.Weight))
                {
                    var str = bagConfig.Weight;
                    string[] strs = str.Split('|');
                    if (strs.Length == 2)
                    {
                        var min = ConvertHelper.ConvertToInt(strs[0]);
                        var max = ConvertHelper.ConvertToInt(strs[1]);
                        if (max == 0)
                            bagList.RemoveAll(b => (b.Weight < min));
                        else
                        {
                            bagList.RemoveAll(b => (b.Weight < min || b.Weight > max));
                        }

                    }
                }
               // 限制生日
                if (!string.IsNullOrEmpty(bagConfig.Birthday))
                {
                    var i = 0;

                    try
                    {
                        var str = bagConfig.Birthday;

                        string[] strs = str.Split('|');

                        if (strs.Length == 2)
                        {

                            if (strs[1] == "0")
                            {

                                i = 1;
                                var min = ConvertHelper.StringToDateTime(strs[0]);
                                i = 2;
                                bagList.RemoveAll(b => (ConvertHelper.StringToDateTime(b.Birthday) < min));
                                i = 3;
                            }
                            else if (strs[0] == "0")
                            {

                                i = 4;
                                var max = ConvertHelper.StringToDateTime(strs[1]);
                                i = 5;
                                bagList.RemoveAll(b => (ConvertHelper.StringToDateTime(b.Birthday) > max));
                                i = 6;
                            }
                            else
                            {
                                var min = ConvertHelper.StringToDateTime(strs[0]);
                                var max = ConvertHelper.StringToDateTime(strs[1]);
                                bagList.RemoveAll(
                                    b => (ConvertHelper.StringToDateTime(b.Birthday) < min || ConvertHelper.StringToDateTime(b.Birthday) > max));
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        SystemlogMgr.Info("bagConfigBirthday", "*"+ e.Message + "*竞技场礼包*");

                    }
                }
                
                
                #endregion


                foreach (var entity in bagList)
                {
                    items .Add(itemList.Find(d => (d.LinkId == entity.Idx&&d.ItemType==1)));
                }
                
                return items;
            }
            catch (Exception)
            {
                return items;
            }
        }

      

        private void CompareBirthday(DicArenabagconfigEntity bagConfig,ref List<DicPlayerEntity> bagList)
        {
           
        }

        List<int> LotteryItemNew(ConfigLotteryEntity lotteryEntity, int cardCount, int orangeMax, int orangeLib, int lowLib, ref int orangeCount, ref int contractCount)
        {
            var list = new List<int>(cardCount);
            orangeCount = 0;
            contractCount = 0;
            //var orangeIndex = RandomHelper.GetInt32WithoutMax(0, cardCount);
            for (int i = 0; i < cardCount; i++)
            {
                int itemCode = 0;
                //if (i==orangeIndex && orangeCount==0 && orangeLib>0)
                //{
                //    itemCode = LotteryByLib(orangeLib);
                //    orangeCount++;
                //}
                //else
                if (orangeCount >= orangeMax || contractCount >= 1)
                {
                    itemCode = LotteryByLib(lowLib);
                }
                else
                {
                    itemCode = LotteryItem(lotteryEntity);
                    while (list.Exists(d => d == itemCode)) //排除重复
                    {
                        itemCode = LotteryItem(lotteryEntity);
                    }
                    var itemDic = CacheFactory.ItemsdicCache.GetItem(itemCode);
                    if (itemDic.ItemType == (int)EnumItemType.PlayerCard && itemDic.PlayerCardLevel == (int)EnumPlayerCardLevel.Orange)
                    {
                        orangeCount++;
                    }
                    //是否是合同页
                    if (itemDic.ItemType == (int) EnumItemType.MallItem && itemDic.FourthType == 56)
                    {
                        contractCount++;
                    }
                }
                list.Add(itemCode);
            }
            return list;
        }

        private int LotteryItem(ConfigLotteryEntity lotteryEntity)
        {
            return LotteryItem(lotteryEntity, null);
        }

        int LotteryItem(ConfigLotteryEntity lotteryEntity, List<int> prizeEquipments)
        {
            int itemCode = 0;
            if (_lotteryRelationDic.ContainsKey(lotteryEntity.Idx))
            {
                var relation = _lotteryRelationDic[lotteryEntity.Idx];
                var libraryId = relation.RandomLinkId;
                if (lotteryEntity.Type == 1 && prizeEquipments != null && prizeEquipments.Count > 0 && libraryId > 106 &&
                    libraryId < 110)
                {
                    itemCode = prizeEquipments[RandomHelper.GetInt32WithoutMax(0, prizeEquipments.Count)];
                    if (libraryId == 108)
                        itemCode = itemCode - 1000;
                    else if (libraryId == 109)
                        itemCode = itemCode - 2000;
                }
                else
                {
                    //特殊处理抽到87-88橙卡， 5%抽金靴卡
                    if (lotteryEntity.Idx == 10 && libraryId == 137 && RandomHelper.GetInt32(0, 100) <= 5)
                        itemCode = 180001;
                    else
                        itemCode = GetItemFromLib(libraryId);
                }
            }
            else if (lotteryEntity.Idx == 0)
            {
                return 0;
            }
            else
            {
                SystemlogMgr.Error("LotteryItem", "no relation,lottery id:" + lotteryEntity.Idx);
            }
            return itemCode;
        }

        ConfigLotteryEntity GetLotteryEntity(int lotteryType, int subType)
        {
            var key = BuildLotteryKey(lotteryType, subType);
            ConfigLotteryEntity lotteryEntity = null;
            if (_lotteryDic.ContainsKey(key))
            {
                var list = _lotteryDic[key];

                if (list.Count == 1)
                {
                    lotteryEntity = list[0];
                }
                else if (list.Count > 1)
                {
                    var curTime = DateTime.Now;
                    lotteryEntity = list.Find(d => d.MinTime <= curTime && d.MaxTime >= curTime);
                    if (lotteryEntity == null)
                        lotteryEntity = list[0];
                }
            }
            return lotteryEntity;
        }

        int GetItemFromLib(int libraryId)
        {
            int itemCode = 0;
            if (_cardLibrary.ContainsKey(libraryId))
            {
                var itemList = _cardLibrary[libraryId];
                var index = RandomHelper.GetInt32WithoutMax(0, itemList.Count);
                itemCode = itemList[index];
            }
            else
            {
                SystemlogMgr.Error("LotteryItem", "no card library,library id:" + libraryId);
            }
            return itemCode;
        }
        #endregion

        #region Facade

        public static LotteryCache Instance
        {
            get { return SingletonFactory<LotteryCache>.SInstance; }
        }

        public int RandomScoutingBlackGold()
        {
            var index = RandomHelper.GetInt32WithoutMax(0, _scoutingBlackGolds.Count);
            return _scoutingBlackGolds[index];
        }
        
        /// <summary>
        /// 根据libId获取所有对应的itemcode
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns></returns>
        public List<int> GetAllItemsByLib(int libraryId)
        {
            if (_cardLibrary.ContainsKey(libraryId))
            {
                return _cardLibrary[libraryId];
            }
            return null;
        }

        public LotteryEntity Lottery(EnumLotteryType lotteryType, int subType)
        {
            return Lottery((int)lotteryType, subType);
        }

        public LotteryEntity Lottery(int lotteryType, int subType)
        {
            return Lottery(lotteryType, subType, 1);
        }

        public LotteryEntity LotteryFive(EnumLotteryType lotteryType, int subType,List<int> prizeEquipments)
        {
            return Lottery((int)lotteryType, subType,5,prizeEquipments);
        }

        public LotteryEntity LotteryFive(EnumLotteryType lotteryType, int subType)
        {
            return Lottery((int)lotteryType, subType, 5, null);
        }

        public LotteryEntity LotteryFive(int lotteryType, int subType)
        {
            return Lottery(lotteryType, subType, 5);
        }

        public LotteryEntity Lottery(EnumLotteryType lotteryType, int subType, List<int> prizeEquipments)
        {
            return Lottery((int)lotteryType, subType, 1,prizeEquipments);
        }

        public LotteryEntity Lottery(EnumLotteryType lotteryType, int subType, int cardCount)
        {
            return Lottery((int)lotteryType, subType,cardCount);
        }

        public LotteryEntity ScoutingNew(int scoutingType, int orangeLib, int lowLib, out List<int> cardList, int giftCode = 0)
        {
            cardList = null;
            ConfigLotteryEntity configLotteryEntity = GetLotteryEntity((int)EnumLotteryType.Lottery, scoutingType);
            if (configLotteryEntity == null)
            {
                SystemlogMgr.Error("ScoutingNew", "no config lottery entity:" + ",subType:" + scoutingType);
                return null;
            }
            else
            {
                int cardCount = 5;
                int orangeCount = 0;
                int contractCount = 0;
                int scoutingorangeCount = _scoutingTenOrangeCount;
                cardList = LotteryItemNew(configLotteryEntity, cardCount, scoutingorangeCount, orangeLib, lowLib,
                    ref orangeCount, ref contractCount);
                
            }
            if (cardList != null && cardList.Count > 0)
            {
                var lotteryEntity = new LotteryEntity();
                lotteryEntity.ItemString = string.Join(",", cardList);
                lotteryEntity.Strength = configLotteryEntity.Strength;
                lotteryEntity.IsBinding = configLotteryEntity.IsBinding;
                return lotteryEntity;
            }
            return null;
        }

        public LotteryEntity ScoutingTen(int scoutingType, int orangeLib, int lowLib, out List<int> cardList, int limitedOrangeCount, out List<int> limitedCardList, int giftCode = 0)
        {
            //orangeLib = 0;
            cardList = null;
            limitedCardList = new List<int>();
            ConfigLotteryEntity configLotteryEntity = GetLotteryEntity((int)EnumLotteryType.Scouting, scoutingType);
            if (configLotteryEntity == null)
            {
                SystemlogMgr.Error("ScoutingTen", "no config lottery entity,scoutingType:" + scoutingType);
                return null;
            }
            else
            {
                int cardCount = 10;
                int orangeCount = 0;
                if (scoutingType == 1)
                {
                    cardList = LotteryItem(configLotteryEntity, cardCount,null);
                }
                else
                {
                    var scoutingorangeCount = _scoutingTenOrangeCount;
                    if (giftCode > 0)
                    {
                        var item = CacheFactory.ItemsdicCache.GetItem(giftCode);
                        if (item != null && item.ItemType == (int) EnumItemType.PlayerCard &&
                            item.PlayerCardLevel == (int) EnumPlayerCardLevel.Orange)
                        {
                            scoutingorangeCount --;
                        }
                    }

                    cardList = LotteryItem(configLotteryEntity, cardCount, scoutingorangeCount, orangeLib, lowLib, ref orangeCount, limitedOrangeCount, out limitedCardList);
                }
                if (cardList != null && cardList.Count > 0)
                {
                    var lotteryEntity = new LotteryEntity();
                    lotteryEntity.ItemString = string.Join(",", cardList);
                    lotteryEntity.Strength = configLotteryEntity.Strength;
                    lotteryEntity.IsBinding = configLotteryEntity.IsBinding;
                    return lotteryEntity;
                }
                return null;
            }
        }

        public LotteryEntity Lottery(int lotteryType, int subType, int cardCount)
        {
            return Lottery(lotteryType, subType, cardCount, null);
        }

        public LotteryEntity Lottery(int lotteryType, int subType, int cardCount, List<int> prizeEquipments)
        {
            ConfigLotteryEntity configLotteryEntity = GetLotteryEntity(lotteryType, subType);
            if (configLotteryEntity == null)
            {
                SystemlogMgr.Error("LotteryCache", "no config lottery entity,lotteryType:" + lotteryType + ",subType:" + subType);
                return null;
            }
            else
            {
                var list = LotteryItem(configLotteryEntity, cardCount,prizeEquipments);
                
                if (list != null && list.Count > 0)
                {
                    var lotteryEntity = new LotteryEntity();
                    var index = RandomHelper.GetInt32WithoutMax(0, list.Count);
                    lotteryEntity.PrizeItemCode = list[index];
                    lotteryEntity.ItemString = string.Join(",", list);
                    lotteryEntity.Strength = configLotteryEntity.Strength;
                    lotteryEntity.IsBinding = configLotteryEntity.IsBinding;
                    return lotteryEntity;
                }
                return null;
            }
        }

        public int LotteryByLib(int libraryId)
        {
            return GetItemFromLib(libraryId);
        }

        public List<int> LotteryEquipmentSuitListByType(int suitType, int quality)
        {
            int suitId = LotteryEquipmentSuitIdByType(suitType);
            if (suitId > 0)
            {
                var list= LotteryEquipmentSuitRange(suitId, quality);
                if (list.Count > 10)
                {
                    return new List<int>(0);
                }
                else
                {
                    return list;
                }
            }
            else
            {
                return new List<int>(0);
            }
        }

        public int LotteryEquipmentSuitIdByType(int suitType)
        {
            if (_lotterySuitTypeDic.ContainsKey(suitType))
            {
                var list = _lotterySuitTypeDic[suitType];
                return list[RandomHelper.GetInt32WithoutMax(0, list.Count)];
            }
            return 0;
        }

        public int LotteryEquipmentSuit(int suitId, int quality)
        {
            var key = BuildEquipKey(suitId, quality);
            int itemCode = 0;
            if (_lotteryEquipmentSuitDic.ContainsKey(key))
            {
                var itemList = _lotteryEquipmentSuitDic[key];
                var index = RandomHelper.GetInt32WithoutMax(0, itemList.Count);
                itemCode = itemList[index];
            }
            else
            {
                SystemlogMgr.Error("LotteryEquipmentSuit", "no equipment suit,suit id:" + suitId+",quality:"+quality);
            }
            return itemCode;
        }
        public List<int> LotteryEquipmentSuitRange(int suitId, int quality)
        {
            var key = BuildEquipKey(suitId, quality);
            List<int> list;
            if (!_lotteryEquipmentSuitDic.TryGetValue(key, out list))
                SystemlogMgr.Error("LotteryEquipmentSuit", "no equipment suit,suit id:" + suitId + ",quality:" + quality);
            return list;
        }
        public int LotterySuitDrawing(int suitId)
        {
            int itemCode = 0;
            if (_lotterySuitdrawingDic.ContainsKey(suitId))
            {
                var itemList = _lotterySuitdrawingDic[suitId];
                var index = RandomHelper.GetInt32WithoutMax(0, itemList.Count);
                itemCode = itemList[index];
            }
            else
            {
                SystemlogMgr.Error("LotterySuitDrawing", "no SuitDrawing ,suit id:" + suitId);
            }
            return itemCode;
        }
        #endregion
    }
}
