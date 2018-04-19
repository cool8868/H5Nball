//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Games.NBall.Bll.Share;
//using Games.NBall.Common;
//using Games.NBall.Entity.Config.Custom;
//using Games.NBall.Entity.Enums;
//using Games.NBall.Entity.Enums;
//using Games.NBall.Entity.NBall.Custom;
//using Games.NBall.Entity.Response;
//using Games.NBall.Entity.Share;

//namespace Games.NBall.Bll.Cache
//{
//    public class MysteryShopCache
//    {
//        DateTime _expiredTime;
//        private DateTime _startTime;
//        private long _expiredTick;
//        private int _curIndex;
//        private DicMallItemDataEntity _itemData;
//        private List<MysteryshopEntity> _shopList;
//        #region .ctor
//        public MysteryShopCache(int p)
//        {
//            InitCache();
//            CacheManager.Instance.Register(EnumCacheType.MysteryShop, InitCache);
//        }

//        #endregion

//        #region Facade

//        public static MysteryShopCache Instance
//        {
//            get { return SingletonFactory<MysteryShopCache>.SInstance; }
//        }

//        public DicMallItemDataEntity GetItemData()
//        {
//            CheckData();
//            if (CheckTime())
//            {
//                return _itemData;
//            }
//            return null;
//        }

//        public long GetExpiredTick()
//        {
//            return _expiredTick;
//        }

//        public long GetStartTick()
//        {
//            return ShareUtil.GetTimeTick(_startTime);
//        }

//        public bool HasItem()
//        {
//            if (_expiredTick >= 0 && DateTime.Now < _expiredTime)
//            {
//                return true;
//            }
//            return false;
//        }
//        #endregion

//        #region encapsulation

//        bool InitCache()
//        {
//            _shopList = MysteryshopMgr.GetByStatus();
//            DateTime curTime = DateTime.Now;
//            if (_shopList != null && _shopList.Count > 0)
//            {
//                for (int i = 0; i < _shopList.Count; i++)
//                {
//                    if (BuildData(_shopList[i], i, curTime))
//                    {
//                        return true;
//                    }
//                }
//            }
//            _startTime = ShareUtil.BaseTime;
//            _expiredTime = ShareUtil.BaseTime;
//            _curIndex = -1;
//            _itemData = null;
//            _expiredTick = -1;
//            return true;
//        }

//        bool CheckTime()
//        {
//            DateTime curTime = DateTime.Now;
//            if (_startTime <= curTime && _expiredTime >= curTime)
//            {
//                return true;
//            }
//            return false;
//        }

//        static object _lockObj = new object();
//        void CheckData()
//        {
//            DateTime curTime = DateTime.Now;
//            if (_itemData == null)
//            {
//                if (_startTime != ShareUtil.BaseTime && _startTime <= curTime)
//                {
//                    lock (_lockObj)
//                    {
//                        if (_itemData == null && _startTime != ShareUtil.BaseTime && _startTime <= curTime)
//                        {
//                            doReBuildData(curTime);
//                        }
//                    }
//                }
//            }
//            else if (_expiredTime != ShareUtil.BaseTime && curTime >= _expiredTime)
//            {
//                lock (_lockObj)
//                {
//                    if (_expiredTime != ShareUtil.BaseTime && curTime >= _expiredTime)
//                    {
//                        doReBuildData(curTime);
//                    }
//                }
//            }
//        }
//        private ReaderWriterLock _rwl = new ReaderWriterLock();

//        void doReBuildData(DateTime curTime)
//        {
//            _rwl.AcquireReaderLock(Timeout.Infinite);
//            try
//            {
//                bool check = false;
//                int loopTime = 5;
//                while (!check && loopTime > 0)
//                {
//                    loopTime--;
//                    check = ReBuildData(curTime);
//                }
//            }
//            finally
//            {
//                _rwl.ReleaseReaderLock();
//            }

//        }

//        bool ReBuildData(DateTime curTime)
//        {
//            var index = _curIndex + 1;
//            if (_shopList != null && _shopList.Count > index)
//            {
//                var entity = _shopList[index];
//                return BuildData(entity, index, curTime);
//            }
//            else
//            {
//                _startTime = ShareUtil.BaseTime;
//                _expiredTime = ShareUtil.BaseTime;
//                _curIndex = index - 1;
//                _itemData = null;
//                _expiredTick = -1;
//                return true;
//            }
//        }

//        bool BuildData(MysteryshopEntity entity, int index, DateTime curTime)
//        {
//            if (curTime < entity.StartTime)
//            {
//                _startTime = entity.StartTime;
//                _expiredTime = ShareUtil.BaseTime;
//                _curIndex = index - 1;
//                _itemData = null;
//                _expiredTick = -1;
//                return true;
//            }
//            else if (curTime < entity.EndTime)
//            {
//                _itemData = MallCache.Instance.GetMallEntityWithoutPoint(entity.MallCode);
//                _itemData.CurrencyCount = entity.CurrencyCount;
//                _itemData.RawCurrencyCount = entity.RawCurrencyCount;
//                _itemData.MaxBuyCount = entity.MaxBuyCount;
//                _itemData.MysteryShopId = entity.Idx;
//                _startTime = entity.StartTime;
//                _expiredTime = entity.EndTime;
//                _expiredTick = ShareUtil.GetTimeTick(_expiredTime);
//                _curIndex = index;
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        #endregion

//        /// <summary>
//        /// 重新加载
//        /// </summary>
//        public void Load()
//        {
//            InitCache();
//            CacheManager.Instance.Register(EnumCacheType.MysteryShop, InitCache);
//        }
//    }
//}
