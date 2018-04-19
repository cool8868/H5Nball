using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Bll.Cache
{
    /// <summary>
    /// 球星启示录缓存类
    /// </summary>
    public class RevelationCache
    {
        /// <summary>
        /// 默认士气
        /// </summary>
        public int _morale;
        /// <summary>
        /// 输球扣士气配置
        /// </summary>
        public Dictionary<int, int> SubtractMoraleDic;
        /// <summary>
        /// 最多扣多少士气
        /// </summary>
        public int _maxSubtractMorale;
        /// <summary>
        /// 增加士气价格 钻石
        /// </summary>
        public int _addMoralePrice;
        /// <summary>
        /// 增加翻牌次数价格 金条
        /// </summary>
        public int _addDrawsPrice;

        /// <summary>
        /// 抽卡数量
        /// </summary>
        public int DrawNumber = 3;

        /// <summary>
        /// 关卡配置
        /// </summary>
        private Dictionary<int, ConfigRevelationEntity> _markDic;

        public RevelationCache(int p)
        {
            InitCache();
        }

        #region encapsulation

        /// <summary>
        /// 勇气商城配置
        /// </summary>
        private List<ConfigRevelationshopEntity> _shopList;
        private Dictionary<int,ConfigRevelationshopEntity> _shopDic;

        /// <summary>
        /// 勇气商城刷新价格配置
        /// </summary>
        private Dictionary<int, int> _shopRefreshDic;

        /// <summary>
        /// 抽卡配置
        /// </summary>
        private List<ConfigRevelationdrawEntity> _drawList;

        /// <summary>
        /// 关卡NPC
        /// </summary>
        private Dictionary<int,ConfigRevelationnpclinkEntity> _npcList;

        void InitCache()
        {
            _morale = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RevelationMorale, 10);
            _addMoralePrice = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RevelationPlusMorale, 300);
            _addDrawsPrice = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RevelationDrawsPrice, 5);
            var subtractMorale = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.RevelationSubtractMorale);
            _maxSubtractMorale = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RevelationMaxSubtractMorale, 6);
            SubtractMoraleDic = new Dictionary<int, int>();
            if (subtractMorale.Length > 0)
            {
                var subtracts = subtractMorale.Split('|');
                foreach (var item in subtracts)
                {
                    var items = item.Split(',');
                    if (items.Length > 0)
                    {
                        SubtractMoraleDic.Add(ConvertHelper.ConvertToInt(items[0]), ConvertHelper.ConvertToInt(items[1]));
                    }
                }
            }

            _markDic = new Dictionary<int, ConfigRevelationEntity>();
            var allmarkInfo = ConfigRevelationMgr.GetAllFor();
            foreach (var item in allmarkInfo)
            {
                var key = GetKey(item.MarkId, item.Schedule);
                if (!_markDic.ContainsKey(key))
                    _markDic.Add(key, item);
            }
            _drawList = ConfigRevelationdrawMgr.GetAll();
            //勇气商城配置
            _shopList = ConfigRevelationshopMgr.GetAll();
            _shopDic = _shopList.ToDictionary(r => r.Idx, r => r);
            //获取所有NPC
            var npcList = ConfigRevelationnpclinkMgr.GetAll();
            _npcList = npcList.ToDictionary(d =>GetKey(d.SmallClearanceId,d.StageId), d => d);

            _shopRefreshDic = new Dictionary<int, int>();
            var shopRefresh = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.RevelationRefrestShopPrice);
            if (shopRefresh.Length > 0)
            {
                var shopPrizeList = shopRefresh.Split(',');
                for (int i = 0; i < shopPrizeList.Length; i++)
                {
                    var price = ConvertHelper.ConvertToInt(shopPrizeList[i]);
                    _shopRefreshDic.Add(i, price);
                }
            }
        }

        #endregion

        #region Facade

        public static RevelationCache Instance
        {
            get { return SingletonFactory<RevelationCache>.SInstance; }
        }

        /// <summary>
        /// 默认士气
        /// </summary>
        public int Morale{
            get { return _morale; }
        }
        /// <summary>
        /// 增加士气价格
        /// </summary>
        public int AddMoralePrice{
            get { return _addMoralePrice; }
        }
        /// <summary>
        /// 增加翻牌机会价格
        /// </summary>
        public int AddDrawsPrice{
            get { return _addDrawsPrice; }
        }

        int GetKey(int markId, int schedule)
        {
            return schedule*10000 + markId;
        }

        /// <summary>
        /// 获取刷新价格
        /// </summary>
        /// <param name="refreshNumber"></param>
        /// <returns></returns>
        public int GetShopRefreshPrice(int refreshNumber)
        {
            var maxKey = _shopRefreshDic.Keys.Max();
            if (refreshNumber > maxKey)
                refreshNumber = maxKey;
            if (_shopRefreshDic.ContainsKey(refreshNumber))
                return _shopRefreshDic[refreshNumber];
            return 0;
        }

        /// <summary>
        /// 获取关卡配置
        /// </summary>
        /// <param name="markId"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public ConfigRevelationEntity GetMarkInfo(int markId, int schedule)
        {
            var key = GetKey(markId, schedule);
            if (_markDic.ContainsKey(key))
                return _markDic[key];
            return null;
        }

        /// <summary>
        /// 下次解锁关卡
        /// </summary>
        /// <param name="markId">当前关卡</param>
        /// <returns></returns>
        public int NextLockMark(int markId)
        {
            var key = GetKey(markId + 1, 1);
            if (_markDic.ContainsKey(key))
                return _markDic[key].MarkId;
            return markId;
        }

        /// <summary>
        /// 是否通关
        /// </summary>
        /// <param name="markId"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public bool GetIsGeneral(int markId, int schedule)
        {
            var key = GetKey(markId, schedule + 1);
            if (_markDic.ContainsKey(key))
                return false;
            return true;
        }

        /// <summary>
        /// 抽3张卡
        /// </summary>
        /// <returns></returns>
        public List<ConfigRevelationdrawEntity> Draw()
        {
            if (_drawList.Count == 0)
                return new List<ConfigRevelationdrawEntity>();
            var resulst = new List<ConfigRevelationdrawEntity>();
            for(int i = 0;i<DrawNumber;i++)
            {
                var random = RandomHelper.GetInt32(0, 100);
                foreach (var item in _drawList)
                {
                    if (item.StartRate <= random && item.EndRate >= random)
                    {
                        resulst.Add(item);
                        break;
                    }
                }
            }
            return resulst;
        }

        /// <summary>
        /// 获取奖励详情
        /// </summary>
        /// <param name="drawIdx"></param>
        /// <returns></returns>
        public ConfigRevelationdrawEntity GetDrawPrizeInfo(int drawIdx)
        {
            return _drawList.Find(r => r.Idx == drawIdx);
        }

        /// <summary>
        /// 获取抽卡的串
        /// </summary>
        /// <returns></returns>
        public string GetDrawString()
        {
            var drawList = Draw();
            if (drawList.Count == 0)
                return "";
            var result = "";
            foreach (var item in drawList)
            {
                if (result.Length == 0)
                    result += item.Idx + ",0";
                else
                    result += "|" + item.Idx + ",0";
            }
            return result;
        }

        /// <summary>
        /// 扣士气
        /// </summary>
        /// <param name="toConcede"></param>
        /// <returns></returns>
        public int SubtractMorale(int toConcede)
        {
            toConcede = Math.Abs(toConcede);
            if (SubtractMoraleDic.ContainsKey(toConcede))
                return SubtractMoraleDic[toConcede];
            return _maxSubtractMorale;
        }

        /// <summary>
        /// 获取翻牌需要消耗多少金币
        /// </summary>
        /// <param name="drawNumber"></param>
        /// <param name="vipLevel"></param>
        /// <returns></returns>
        public int GetDrawGoldBar(int drawNumber,int vipLevel)
        {
            var maxCount = CacheFactory.VipdicCache.GetEffectValue(vipLevel, EnumVipEffect.RevelationDrawNumber);
            //免费
            if (drawNumber+1 <= maxCount)
                return 0;
            return (drawNumber+1 - maxCount)*_addDrawsPrice;
        }

        /// <summary>
        /// 获取商城串
        /// </summary>
        /// <returns></returns>
        public string GetShopString()
        {
            var result = "";
            foreach (var item in _shopList)
            {
                switch (item.ItemType)
                {
                    case 3://指定物品
                        result += item.Idx + "," + item.SubType + "," + item.ItemCount + "," + item.Price + "|";
                        break;
                    case 4://卡库
                        var itemCode = CacheFactory.LotteryCache.LotteryByLib(item.SubType);
                        result += item.Idx + "," + itemCode + "," + item.ItemCount + "," + item.Price + "|";
                        break;
                }
            }
            if (result.Length > 0)
                result = result.Substring(0, result.Length - 1);
            return result;
        }

        /// <summary>
        /// 获取勇气商城价格
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public int GetShopPrice(int idx)
        {
            if (_shopDic.ContainsKey(idx))
                return _shopDic[idx].Price;
            return 0;
        }

        #endregion
    }

    /// <summary>
    /// 球星启示录翻牌奖励类型
    /// </summary>
    public enum RevelationDrawPrizeType
    {
        /// <summary>
        /// 教练碎片
        /// </summary>
        CoachDebris =1,
        /// <summary>
        /// 士气
        /// </summary>
        Morale =2,
        /// <summary>
        /// 指定物品
        /// </summary>
        Item =3,
        /// <summary>
        /// 勇气值
        /// </summary>
        Courage = 13,

    }
}
