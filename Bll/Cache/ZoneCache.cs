using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class ZoneCache : BaseSingleton
    {
        #region encapsulation

        /// <summary>
        /// 根据平台获取所有区
        /// </summary>
        private Dictionary<string, List<AllZoneinfoEntity>> _platFormZoneDic;

        /// <summary>
        /// 所有区 
        /// </summary>
        private List<AllZoneinfoEntity> _allZoneList;
        /// <summary>
        /// 独服列表
        /// </summary>
        private List<AllZoneinfoEntity> _aloneZoneList;
        /// <summary>
        /// 混服列表
        /// </summary>
        private List<AllZoneinfoEntity> _mixtureZoneList;
        /// <summary>
        /// 群黑和9G列表
        /// </summary>
        private List<AllZoneinfoEntity> _qunheiAnd9GList;
        /// <summary>
        /// 玩吧区列表
        /// </summary>
        private List<AllZoneinfoEntity> _wanBaZoneList;
        /// <summary>
        /// 玩吧区列表
        /// </summary>
        private List<AllZoneinfoEntity> _wanBaZoneListIos;

        /// <summary>
        /// Egret区
        /// </summary>
        private List<AllZoneinfoEntity> _egretZoneList;

        /// <summary>
        /// Egret区
        /// </summary>
        private List<AllZoneinfoEntity> _qunheiZoneList;
        /// <summary>
        /// 小熊区
        /// </summary>
        private List<AllZoneinfoEntity> _bearZoneList;

        private Dictionary<string, AllZoneinfoEntity> _zoneDic;
        public ZoneCache(int p)
            : base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            _zoneDic = new Dictionary<string, AllZoneinfoEntity>();
            _platFormZoneDic = new Dictionary<string, List<AllZoneinfoEntity>>();
            _qunheiZoneList = new List<AllZoneinfoEntity>();
            _aloneZoneList = new List<AllZoneinfoEntity>();
            _mixtureZoneList = new List<AllZoneinfoEntity>();
            _wanBaZoneList = new List<AllZoneinfoEntity>();
            _wanBaZoneListIos = new List<AllZoneinfoEntity>();
            _qunheiAnd9GList = new List<AllZoneinfoEntity>();
            _egretZoneList=new List<AllZoneinfoEntity>();
            _bearZoneList = new List<AllZoneinfoEntity>();
            _allZoneList = AllZoneinfoMgr.GetAll();
            foreach (var item in _allZoneList)
            {
                if (!_platFormZoneDic.ContainsKey(item.PlatformCode))
                    _platFormZoneDic.Add(item.PlatformCode, new List<AllZoneinfoEntity>());
                _platFormZoneDic[item.PlatformCode].Add(item);
                int platzoneId = ConvertHelper.ConvertToInt(item.PlatformZoneName);

                if (!_zoneDic.ContainsKey(item.ZoneName))
                    _zoneDic.Add(item.ZoneName, item);
                if (ShareUtil.IsTx)
                {
                    if (platzoneId > 1000 && platzoneId < 10000)
                        _wanBaZoneList.Add(item);
                    else if (platzoneId > 10000)
                        _wanBaZoneListIos.Add(item);
                    continue;
                }
                if (item.PlatformCode == "h5_egret")
                {
                    _egretZoneList.Add(item);
                }else if (item.PlatformCode == "h5_qunhei")
                {
                    _qunheiZoneList.Add(item);
                }
                //if (platzoneId == 1001)
                //{
                //    _egretZoneList.Add(item);
                //}
                //else
                    if (platzoneId==1003)
                {
                    _bearZoneList.Add(item);
                }
                else if (platzoneId == 1)
                {
                    _mixtureZoneList.Add(item);
                    _aloneZoneList.Add(item);
                    _qunheiAnd9GList.Add(item);
                }
                else if (platzoneId < 1000)
                {
                    if (platzoneId == 2)
                        _qunheiAnd9GList.Add(item);
                    _aloneZoneList.Add(item);
                }
                else
                {
                    if (platzoneId != 10002)
                        _qunheiAnd9GList.Add(item);
                    _mixtureZoneList.Add(item);
                }
            }
        }
        #endregion

        #region Facade

        public static ZoneCache Instance
        {
            get { return SingletonFactory<ZoneCache>.SInstance; }

        }

        /// <summary>
        /// 根据平台获取所有区
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public List<AllZoneinfoEntity> GetZoneListByPlatForm(string platform)
        {
            if (_platFormZoneDic.ContainsKey(platform))
                return _platFormZoneDic[platform];
            return new List<AllZoneinfoEntity>();
        }

        /// <summary>
        /// 获取所有区
        /// </summary>
        /// <returns></returns>
        public List<AllZoneinfoEntity> GetAllZone()
        {
            return _allZoneList;
        }

        /// <summary>
        /// 获取所有独服
        /// </summary>
        /// <returns></returns>
        public List<AllZoneinfoEntity> GetAllAloneZone()
        {
            return _aloneZoneList;
        }

        /// <summary>
        /// 获取所有混服
        /// </summary>
        /// <returns></returns>
        public List<AllZoneinfoEntity> GetAllMixtureZone()
        {
            return _mixtureZoneList;
        }

        /// <summary>
        /// 获取群黑和9G区列表
        /// </summary>
        /// <returns></returns>
        public List<AllZoneinfoEntity> GetQunheiAnd9GZone() {
            return _qunheiAnd9GList;
        }

        /// <summary>
        /// 获取玩吧区列表
        /// </summary>
        /// <returns></returns>
        public List<AllZoneinfoEntity> GetWanBaZone(string longType)
        {
            if (longType == "2")
                return _wanBaZoneListIos;
            return _wanBaZoneList;
        }

        /// <summary>
        /// 获取egret区列表
        /// </summary>
        /// <returns></returns>
        public List<AllZoneinfoEntity> GetEgterZone()
        {
            return _egretZoneList;
        }
        /// <summary>
        /// 获取小熊区列表
        /// </summary>
        /// <returns></returns>
        public List<AllZoneinfoEntity> GetBearZone()
        {
            return _bearZoneList;
        }

        public List<AllZoneinfoEntity> GetQunHeiZone()
        {
            return _qunheiZoneList;
        }

        /// <summary>
        /// 根据zoneName 获取区信息
        /// </summary>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public AllZoneinfoEntity GetZoneInfo(string zoneName)
        {
            if (_zoneDic.ContainsKey(zoneName))
                return _zoneDic[zoneName];
            return null;
        }

        #endregion
    }
}
