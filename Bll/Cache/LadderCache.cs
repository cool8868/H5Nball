using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;

namespace Games.NBall.Bll.Cache
{
    public class LadderCache
    {
        /// <summary>
        /// type->list
        /// </summary>
        private Dictionary<int, List<string>> _exchangeTypeDic;
        /// <summary>
        /// itemCode,entity
        /// </summary>
        private Dictionary<string, DicLadderexchangeEntity> _exchangeDic; 
        private RandomRelation _relation;
        /// <summary>
        /// rank->list subType
        /// </summary>
        private Dictionary<int, List<int>> _prizeDic;
        /// <summary>
        /// rank->packId
        /// </summary>
        private Dictionary<int, int> _prizeNewDic;
        /// <summary>
        /// 7天一个赛季的排名奖励 rank->packId
        /// </summary>
        private Dictionary<int, int> _prizeSevenDic;

        /// <summary>
        /// 天梯每日胜场奖励
        /// </summary>
        private Dictionary<int, List<ConfigLadderdayprizeEntity>> _ladderDayPrzie;

        #region .ctor
        public LadderCache(int p)
        {
            InitCache();
        }
        #endregion

        #region Facade
        public static LadderCache Instance
        { get { return SingletonFactory<LadderCache>.SInstance; } }

        public string GetExchanges(out string equipmentItemcode, out string equipmentProperties)
        {
            string newExchanges = "";
            equipmentProperties = "";
            equipmentItemcode = "";
            var equipStr = "";
            foreach (var type in _exchangeTypeDic.Keys)
            {
                var exchange = GetExchangeId(type);
                var itemcode = exchange.Split(',')[1];
                while (newExchanges.Contains(itemcode))
                {
                    exchange = GetExchangeId(type);
                    itemcode = exchange.Split(',')[1];
                }
                if (exchange.StartsWith("5,") || exchange.StartsWith("6,"))
                    equipStr += exchange + "|";
                newExchanges = newExchanges + exchange + "|";
            }

            equipmentProperties = GetEquipmentPropertys(equipStr, out equipmentItemcode);
            return newExchanges.TrimEnd('|');
        }

        public DicLadderexchangeEntity GetExchangeEntity(string exchange)
        {
            if (!_exchangeDic.ContainsKey(exchange))
                return null;
            return _exchangeDic[exchange];
        }
       
        public int GetRankPrizeNew(int rank)
        {
            if (!_prizeNewDic.ContainsKey(rank))
                return 0;
            return _prizeNewDic[rank];
        }

        public int GetRankPrizeSevenNew(int rank)
        {
            if (!_prizeSevenDic.ContainsKey(rank))
                return 0;
            return _prizeSevenDic[rank];
        }
        #endregion

        #region encapsulation
        List<string> BuildExchangeKey(DicLadderexchangeEntity entity)
        {
            var list = new List<string>();
            switch (entity.ItemType)
            {
                case 0://指定物品
                    list.Add(entity.Type + "," + entity.ItemCode);
                    break;
                case 1://从卡库中随机抽取
                    var itemlist = LotteryCache.Instance.GetAllItemsByLib(entity.ItemCode);
                    foreach (var item in itemlist)
                    {
                        var key = entity.Type + "," + item ;
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

        private void InitCache()
        {
            var ladderExchangeRate = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.LadderExchangeRate);
            _ladderDayPrzie = new Dictionary<int, List<ConfigLadderdayprizeEntity>>();
            _relation = RelationHelper.BuildRelation(ladderExchangeRate);
            var list = DicLadderexchangeMgr.GetAll();
            _exchangeTypeDic = new Dictionary<int, List<string>>();
            _exchangeDic = new Dictionary<string, DicLadderexchangeEntity>();
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

            var list2 = DicLadderprizeMgr.GetAll();
            _prizeDic = new Dictionary<int, List<int>>(8);
            _prizeNewDic = new Dictionary<int, int>(7);
            _prizeSevenDic = new Dictionary<int, int>(7);
            foreach (var entity in list2)
            {
                for (int i = entity.MinRank; i <= entity.MaxRank; i++)
                {
                    if (entity.SubType < 100)
                    {
                        if (!_prizeDic.ContainsKey(i))
                            _prizeDic.Add(i, new List<int>(1));
                        _prizeDic[i].Add(entity.SubType);
                    }
                    else if (entity.SubType > 300)
                    {
                        if (!_prizeSevenDic.ContainsKey(i))
                            _prizeSevenDic.Add(i, entity.SubType);
                    }
                    else
                    {
                        if (!_prizeNewDic.ContainsKey(i))
                            _prizeNewDic.Add(i, entity.SubType);
                    }
                }
            }

            var listdayPrize = ConfigLadderdayprizeMgr.GetAll();
            foreach (var item in listdayPrize)
            {
                if (!_ladderDayPrzie.ContainsKey(item.WinNumber))
                    _ladderDayPrzie.Add(item.WinNumber, new List<ConfigLadderdayprizeEntity>());
                _ladderDayPrzie[item.WinNumber].Add(item);
            }
        }

        #endregion


        private string GetEquipmentPropertys(string equipmentItems, out string itemcodes)
        {
            var propertys = "";
            itemcodes = "";
            var equipmentList = equipmentItems.Split('|');
            foreach (var equipmentItem in equipmentList)
            {
                var equipmentItemCode = equipmentItem.Split(',');
                if (equipmentItemCode.Length < 2)
                    continue;

                var itemcode = Convert.ToInt32(equipmentItemCode[1]);
                var iteminfo = ItemsdicCache.Instance.GetItem(itemcode);
                var equipmentProperty = EquipmentCache.Instance.RandomEquipmentProperty(iteminfo.LinkId);
                var bytes = SerializationHelper.ToByte(equipmentProperty);

                itemcodes += itemcode + "|";
                propertys += ShareUtil.ByteArrayToHexStr(bytes) + "|";
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
                if (string.IsNullOrEmpty(equipmentProperties))
                    continue;

                var propertyBytes = ShareUtil.HexStrToByteArray(equipmentProperty);
                var property = SerializationHelper.FromByte<EquipmentProperty>(propertyBytes);
                if (property != null)
                    propertyList.Add(property);
            }
            return propertyList;
        }

        /// <summary>
        /// 根据胜场获取可领取的奖励
        /// </summary>
        /// <param name="winNumber"></param>
        /// <returns></returns>
        public List<ConfigLadderdayprizeEntity> GetWinPrize(int winNumber)
        {
            if (_ladderDayPrzie.ContainsKey(winNumber))
                return _ladderDayPrzie[winNumber];
            return new List<ConfigLadderdayprizeEntity>();
        }

    }
}
