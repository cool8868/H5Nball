using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Config.Custom;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class MallCache : BaseSingleton
    {
        #region encapsulation

        Dictionary<int, DicMallItemDataEntity> _mallItemDic;
        private List<DicMallItemDataEntity> _showList;

        /// <summary>
        /// 商城礼包奖励配置
        /// </summary>
        private Dictionary<int, List<ConfigMallgiftbagEntity>> _giftBagDic;
        /// <summary>
        /// extraType>>usedCount>>mallCode
        /// </summary>
        private Dictionary<int, Dictionary<int, int>> _mallExtraLinkDic;

        private Dictionary<int, List<DicNewplayerpackEntity>> _newplayerpackDic;
        /// <summary>
        /// consumeSourceType>>usedCount>>ConfigMalldirectEntity
        /// </summary>
        private Dictionary<int, Dictionary<int, ConfigMalldirectEntity>> _mallDirectDic;
        /// <summary>
        /// 商品类型字典
        /// </summary>
        public Dictionary<int, List<DicMallitemEntity>> _mallTypeDic;

        public Dictionary<int, ConfigTxchargeidEntity> _txChargeIdDic;

        /// <summary>
        /// 教练碎片
        /// </summary>
        public Dictionary<int,DicMallitemEntity> _coachDebristDic;
        /// <summary>
        /// key =effvalues values=mallCode
        /// </summary>
        private Dictionary<int, int> _theContractdic;

        private List<DicMallItemDataEntity> _bindShowList;

        public MallCache(int p)
            : base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            LogHelper.Insert("Mall cache init start", LogType.Info);
            _coachDebristDic = new Dictionary<int, DicMallitemEntity>();
            var list = DicMallitemMgr.GetAllForCache();
            _mallItemDic = new Dictionary<int, DicMallItemDataEntity>(list.Count);
            _showList = new List<DicMallItemDataEntity>();
            _theContractdic = new Dictionary<int, int>();
            _mallTypeDic = new Dictionary<int, List<DicMallitemEntity>>();
            foreach (var entity in list)
            {
                var newEntity = new DicMallItemDataEntity();
                newEntity.RawCurrencyCount = entity.CurrencyCount;
                newEntity.EffectType = entity.EffectType;
                newEntity.EffectValue = entity.EffectValue;
                newEntity.UseLevel = entity.UseLevel;
                newEntity.CurrencyType = entity.CurrencyType;
                newEntity.HotFlag = entity.HotFlag;
                newEntity.ImageId = entity.ImageId;
                newEntity.MallCode = entity.MallCode;
                newEntity.MallType = entity.MallType;
                newEntity.CurrencyCount = entity.CurrencyCount;
                newEntity.PointInTimes = CalDiscount(entity);
                newEntity.ShowFlag = entity.ShowFlag;
                newEntity.ShowOrder = entity.ShowOrder;
                newEntity.PackageFlag = entity.PackageFlag;
                newEntity.Description = entity.ItemIntro;
                newEntity.Name = entity.Name;
                newEntity.Quality = entity.Quality;
                newEntity.ShowBatch = entity.ShowBatch;
                newEntity.CurrencyDiscount = entity.CurrencyDiscount;
                _mallItemDic.Add(entity.MallCode, newEntity);
                if (entity.ShowFlag)
                {
                    _showList.Add(newEntity);
                }
                if (!_mallTypeDic.ContainsKey(entity.MallType))
                    _mallTypeDic.Add(entity.MallType, new List<DicMallitemEntity>());
                _mallTypeDic[entity.MallType].Add(entity);

                if(entity.EffectType == (int)EnumMallEffectType.CoachDebris)
                    if (!_coachDebristDic.ContainsKey(entity.MallCode))
                        _coachDebristDic.Add(entity.MallCode , entity);
            }

            var list2 = ConfigMallextraMgr.GetAll();
            _mallExtraLinkDic = new Dictionary<int, Dictionary<int, int>>();
            foreach (var entity in list2)
            {
                if (!_mallExtraLinkDic.ContainsKey(entity.ExtraType))
                    _mallExtraLinkDic.Add(entity.ExtraType, new Dictionary<int, int>());
                _mallExtraLinkDic[entity.ExtraType].Add(entity.UsedCount, entity.MallCode);
            }

            var list3 = DicNewplayerpackMgr.GetAllForCache();
            _newplayerpackDic = new Dictionary<int, List<DicNewplayerpackEntity>>();
            foreach (var entity in list3)
            {
                if (!_newplayerpackDic.ContainsKey(entity.PackId))
                    _newplayerpackDic.Add(entity.PackId, new List<DicNewplayerpackEntity>());
                _newplayerpackDic[entity.PackId].Add(entity);
            }

            var list4 = ConfigMalldirectMgr.GetAll();
            _mallDirectDic = new Dictionary<int, Dictionary<int, ConfigMalldirectEntity>>();
            foreach (var entity in list4)
            {
                if (!_mallDirectDic.ContainsKey(entity.ConsumeSourceType))
                    _mallDirectDic.Add(entity.ConsumeSourceType, new Dictionary<int, ConfigMalldirectEntity>());
                _mallDirectDic[entity.ConsumeSourceType].Add(entity.UsedCount, entity);
            }

            _theContractdic = list.FindAll(r => r.EffectType == (int)EnumMallEffectType.TheContract).ToDictionary(r => r.EffectValue, r => r.MallCode);

            //绑定商城 价格为原商城的2倍
            _bindShowList = new List<DicMallItemDataEntity>();
            foreach (var entity in _showList)
            {
                if (entity.MallType != 6 && entity.MallType != 7 && entity.EffectType != 9)
                {
                    var newEntity = entity.Clone();
                    newEntity.CurrencyType = (int)EnumCurrencyType.BindPoint;
                    newEntity.CurrencyCount *= 2;
                    newEntity.RawCurrencyCount *= 2;
                    _bindShowList.Add(newEntity);
                }

            }

            _giftBagDic = new Dictionary<int, List<ConfigMallgiftbagEntity>>();
            var allgiftPrize = ConfigMallgiftbagMgr.GetAll();
            foreach (var item in allgiftPrize)
            {
                if (!_giftBagDic.ContainsKey(item.MallCode))
                    _giftBagDic.Add(item.MallCode, new List<ConfigMallgiftbagEntity>());
                _giftBagDic[item.MallCode].Add(item);
            }

            _txChargeIdDic = new Dictionary<int, ConfigTxchargeidEntity>();
            var alltxId = ConfigTxchargeidMgr.GetAll();
            foreach (var item in alltxId)
            {
                var key = Getkey(item.MallCode, item.ZoneType);
                if (!_txChargeIdDic.ContainsKey(key))
                    _txChargeIdDic.Add(key, item);
            }
            LogHelper.Insert("Mall cache init end", LogType.Info);
        }

        private int Getkey(int number1, int number2)
        {
            return number2*100000 + number1;
        }

        List<PointInTime> CalDiscount(DicMallitemEntity entity)
        {
            if (string.IsNullOrEmpty(entity.CurrencyDiscount))
            {
                return new List<PointInTime>(0);
            }
            else
            {
                //0,0~100&1000,2000~60
                string[] commandValues = entity.CurrencyDiscount.Split('&');
                var pointInTimes = new List<PointInTime>(commandValues.Length);
                foreach (var commandValue in commandValues)
                {
                    string[] rateInTimes = commandValue.Split('~');
                    string[] times = rateInTimes[0].Split(',');
                    int discount = Convert.ToInt32(rateInTimes[1]);
                    var pointEntity = new PointInTime();
                    pointEntity.Point = entity.CurrencyCount * discount / 100;
                    pointEntity.StartTime = ShareUtil.BaseTime.AddMinutes(Convert.ToInt32(times[0]));
                    pointEntity.EndTime = ShareUtil.BaseTime.AddMinutes(Convert.ToInt32(times[1]));
                    pointInTimes.Add(pointEntity);
                }
                return pointInTimes;
            }
        }

        int GetPoint(DicMallItemDataEntity entity, DateTime curTime)
        {
            var pointEntity = entity.PointInTimes.Find(d => d.StartTime <= curTime && curTime <= d.EndTime);
            if (pointEntity == null)
            {
                return entity.RawCurrencyCount;
            }
            else
            {
                return pointEntity.Point;
            }
        }

        DicMallItemDataEntity GetNewEntity(DicMallItemDataEntity entity, DateTime curTime)
        {
            var newEntity = entity.Clone();
            newEntity.CurrencyCount = GetPoint(entity, curTime);
            return newEntity;
        }
        #endregion

        #region Facade

        public static MallCache Instance
        {
            get { return SingletonFactory<MallCache>.SInstance; }
        }

        /// <summary>
        /// 获取教练碎片列表
        /// </summary>
        /// <returns></returns>
        public List<int> GetCoachDebristList()
        {
            var resultList = new List<int>();
            foreach (var entity in _coachDebristDic)
            {
                resultList.Add(entity.Key + 300000);
            }
            return resultList;
        }

        /// <summary>
        /// 获取腾讯充值ID
        /// </summary>
        /// <param name="mallCode"></param>
        /// <param name="zoneType"></param>
        /// <returns></returns>
        public int GetTxChargeId(int mallCode, int zoneType)
        {
            var key = Getkey(mallCode, zoneType);
            if (_txChargeIdDic.ContainsKey(key))
                return _txChargeIdDic[key].TxItemId;
            return 0;
        }

        public List<DicMallItemDataEntity> GetShowList()
        {
            var list = new List<DicMallItemDataEntity>(_showList.Count);
            DateTime curTime = DateTime.Now;
            foreach (var entity in _showList)
            {
                var newEntity = GetNewEntity(entity, curTime);
                newEntity.Description = "";
                list.Add(newEntity);
            }
            return list;
        }

        public List<DicMallItemDataEntity> GetBindShowList()
        {
            var list = new List<DicMallItemDataEntity>(_bindShowList.Count);
            DateTime curTime = DateTime.Now;
            foreach (var entity in _bindShowList)
            {
                var newEntity = GetNewEntity(entity, curTime);
                newEntity.Description = "";
                list.Add(newEntity);
            }
            return list;
        }

        public DicMallItemDataEntity GetMallEntity(int mallCode, DateTime curTime)
        {
            DicMallItemDataEntity entity = null;
            if (_mallItemDic.ContainsKey(mallCode))
            {
                entity = GetNewEntity(_mallItemDic[mallCode], curTime);
            }
            return entity;
        }

        public DicMallItemDataEntity GetMallEntityWithoutPoint(int mallCode)
        {
            DicMallItemDataEntity entity = null;
            if (_mallItemDic.ContainsKey(mallCode))
            {
                entity = _mallItemDic[mallCode];
            }
            return entity;
        }

        public int GetExtraMallCode(int extraType, int usedCount)
        {
            int mallCode = 0;
            if (_mallExtraLinkDic.ContainsKey(extraType))
            {
                if (_mallExtraLinkDic[extraType].ContainsKey(usedCount))
                {
                    mallCode = _mallExtraLinkDic[extraType][usedCount];
                }
                else if (_mallExtraLinkDic[extraType].ContainsKey(0))
                {
                    mallCode = _mallExtraLinkDic[extraType][0];
                }
            }
            return mallCode;
        }

        public int GetCostPoint(int mallCode, DateTime curTime)
        {
            var entity = GetMallEntity(mallCode, curTime);
            if (entity != null)
                return entity.CurrencyCount;
            return 0;
        }

        public List<DicNewplayerpackEntity> GetNewplayerpacklist(int packId)
        {
            List<DicNewplayerpackEntity> entity = null;
            _newplayerpackDic.TryGetValue(packId, out entity);
            return entity;
        }

        public ConfigMalldirectEntity GetDirectEntity(EnumConsumeSourceType consumeSourceType, int usedCount = 0)
        {
            return GetDirectEntity((int)consumeSourceType, usedCount);
        }

        public int GetDirectPoint(EnumConsumeSourceType consumeSourceType, int usedCount = 0)
        {
            var entity = GetDirectEntity((int)consumeSourceType, usedCount);
            if (entity != null)
                return entity.Point;
            return 0;
        }

        public ConfigMalldirectEntity GetDirectEntity(int consumeSourceType, int usedCount = 0)
        {
            //设usedCount=0，清除cd和复活都为默认值
            //usedCount = 0;
            if (_mallDirectDic.ContainsKey(consumeSourceType))
            {
                if (_mallDirectDic[consumeSourceType].ContainsKey(usedCount))
                {
                    return _mallDirectDic[consumeSourceType][usedCount];
                }
                else if (_mallDirectDic[consumeSourceType].ContainsKey(0))
                {
                    return _mallDirectDic[consumeSourceType][0];
                }
            }
            return null;
        }

        /// <summary>
        /// 根据商城ID获取EffectValue
        /// </summary>
        /// <param name="mallId"></param>
        /// <returns></returns>
        public int GetTheContractRewardCode(int mallId)
        {
            if (_mallItemDic.ContainsKey(mallId))
                return _mallItemDic[mallId].EffectValue;
            return 0;
        }

        /// <summary>
        /// 根据商城物品ID 获取球员playerId
        /// </summary>
        /// <param name="mallId"></param>
        /// <returns></returns>
        public int GetTheContractPlayerId(int mallId)
        {
            if (_mallItemDic.ContainsKey(mallId))
                return _mallItemDic[mallId].ImageId;
            return 0;
        }

        /// <summary>
        /// 是否是合同页
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool IsContract(DicItemEntity card)
        {
            if (card.ItemType == (int)EnumItemType.MallItem &&
                card.MallEffectType == (int)EnumMallEffectType.TheContract)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据商品类型活动物品列表
        /// </summary>
        /// <param name="mallType"></param>
        /// <returns></returns>
        public List<DicMallitemEntity> GetMallListByMallType(int mallType)
        {
            if (_mallTypeDic.ContainsKey(mallType))
                return _mallTypeDic[mallType];
            return new List<DicMallitemEntity>();
        }

        /// <summary>
        /// 获取礼包奖励
        /// </summary>
        /// <param name="mallCode"></param>
        /// <returns></returns>
        public List<ConfigMallgiftbagEntity> GetMallGiftBagPrize(int mallCode)
        {
            if (_giftBagDic.ContainsKey(mallCode))
                return _giftBagDic[mallCode];
            return new List<ConfigMallgiftbagEntity>();
        }

        #endregion
    }
}
