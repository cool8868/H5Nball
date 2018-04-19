
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Activity;

namespace Games.NBall.Bll.Cache
{
    public class ActivityCache
    {
        private Dictionary<int, DicActivityEntity> _activityDic;
        private Dictionary<int, List<DicActivityprizeEntity>> _prizeDic;
        private Dictionary<int, ConfigDailyeventtimeEntity> _dailyEventDic;
        private Dictionary<int,List<DicActivityprizeEntity>> _prizeList;
        private Dictionary<int, List<ConfigInvestEntity>> _investDic;
        private Dictionary<int, List<ConfigVippackageEntity>> _vipPackageDic;
        private Dictionary<int, List<ConfigVippackageEntity>> _vipPackageIdDic;
        

        #region .ctor
        private ActivityCache()
        {
            InitCache();
            _initFlag = true;
        }
        #endregion

        #region Facade

        #region Instance
        static readonly object _lockObj = new object();
        static volatile ActivityCache _instance = null;
        public readonly bool _initFlag = false;
        public static ActivityCache Instance
        {
            get
            {
                if (null == _instance || !_instance._initFlag)
                {
                    lock (_lockObj)
                    {
                        if (null == _instance || !_instance._initFlag)
                        {
                            _instance = new ActivityCache();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion
        
        public DicActivityEntity GetActivity(int activityId)
        {
            if (_activityDic.ContainsKey(activityId))
                return _activityDic[activityId];
            else
            {
                return null;
            }
        }

        public List<DicActivityprizeEntity> GetPrize(int activityId, int activityStep)
        {
            var key = BuildPrizeKey(activityId, activityStep);
            if (_prizeDic.ContainsKey(key))
                return _prizeDic[key];
            return null;
        }

        public List<DicActivityprizeEntity> GetPrize(int activityId)
        {
            if (_prizeList.ContainsKey(activityId))
                return _prizeList[activityId];
            return null;
        }

        public ConfigDailyeventtimeEntity GetDailyevent(EnumDailyevent eventType)
        {
            return GetDailyevent((int) eventType);
        }

        public ConfigDailyeventtimeEntity GetDailyevent(int eventType)
        {
            if (_dailyEventDic.ContainsKey(eventType))
                return _dailyEventDic[eventType];
            else
            {
                return null;
            }
        }
        #endregion

        #region encapsulation

        private void InitCache()
        {
            //LogHelper.Insert("activity cache init start", LogType.Info);
            var list = DicActivityMgr.GetAllForCache();
            var list2 = DicActivitystepMgr.GetAllForCache();

            _activityDic = list.ToDictionary(d => d.Idx, d => d);

            int onlineActivity = (int) EnumActivityType.OnlinePrize;
            int intActivityCondition = 5;
            foreach (var entity in list2)
            {
                if (_activityDic[entity.ActivityId].StepDic == null)
                    _activityDic[entity.ActivityId].StepDic = new Dictionary<int, DicActivitystepEntity>();
                if (entity.ActivityId == onlineActivity)
                    entity.OnlineSeconds = ConvertHelper.ConvertToInt(entity.Condition)*60;

                entity.ConditionInt = ConvertHelper.ConvertToInt(entity.Condition);

                _activityDic[entity.ActivityId].StepDic.Add(entity.ActivityStep, entity);
            }
            var list3 = DicActivityprizeMgr.GetAll();
            _prizeDic = new Dictionary<int, List<DicActivityprizeEntity>>();
            _prizeList = new Dictionary<int, List<DicActivityprizeEntity>>();
            foreach (var entity in list3)
            {
                var key = BuildPrizeKey(entity.ActivityId, entity.ActivityStep);
                if (!_prizeDic.ContainsKey(key))
                    _prizeDic.Add(key, new List<DicActivityprizeEntity>());
                _prizeDic[key].Add(entity);
                if (!_prizeList.ContainsKey(entity.ActivityId))
                    _prizeList.Add(entity.ActivityId, new List<DicActivityprizeEntity>());
                _prizeList[entity.ActivityId].Add(entity);
            }

            var list4 = ConfigDailyeventtimeMgr.GetAll();
            _dailyEventDic = list4.ToDictionary(d => d.EventType, d => d);

            var investList = ConfigInvestMgr.GetAll();
            _investDic = new Dictionary<int, List<ConfigInvestEntity>>();
            foreach (var invest in investList)
            {
                if (!_investDic.ContainsKey(invest.Step))
                    _investDic.Add(invest.Step, new List<ConfigInvestEntity>());
                _investDic[invest.Step].Add(invest);
            }
            //vip礼包数据缓存
            var listVipPackage = ConfigVippackageMgr.GetAll();
            _vipPackageDic = new Dictionary<int, List<ConfigVippackageEntity>>();
            _vipPackageIdDic = new Dictionary<int, List<ConfigVippackageEntity>>();
            foreach (var inner in listVipPackage)
            {
                if (!_vipPackageDic.ContainsKey(inner.VipLevel))
                    _vipPackageDic.Add(inner.VipLevel,new List<ConfigVippackageEntity>());
                _vipPackageDic[inner.VipLevel].Add(inner);
                if (!_vipPackageIdDic.ContainsKey(inner.PackageId))
                    _vipPackageIdDic.Add(inner.PackageId, new List<ConfigVippackageEntity>());
                _vipPackageIdDic[inner.PackageId].Add(inner);
            }
            
                
           // LogHelper.Insert("activity cache init end", LogType.Info);
        }

        public List<ConfigVippackageEntity> GetVipPackagePrize(int managerVipLevel)
        {
            for (int i = managerVipLevel; i > 0 ; i--)
            {
                if (_vipPackageDic.ContainsKey(i))
                {
                    return _vipPackageDic[i];
                }
            }
             return new List<ConfigVippackageEntity>();
            
        }

        public List<ConfigVippackageEntity> GetVipPackagePrizeByPackageId(int packageId)
        {
            if (_vipPackageIdDic.ContainsKey(packageId))
                return _vipPackageIdDic[packageId];
            return new List<ConfigVippackageEntity>();
        }

        public List<ConfigInvestEntity> GetInvestEntityList(int step)
        {
            if (_investDic.ContainsKey(step))
                return _investDic[step];
            else
                return null;
        }

        /// <summary>
        /// 计算返还绑定点券
        /// </summary>
        /// <param name="stepStatus"></param>
        /// <returns></returns>
        public List<int> BuildRestitution(string stepStatus)
        {
            List<int> restitution = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            
            var ss = stepStatus.Split('|');
            for (int i = 0; i < ss.Length; i++)
            {
                var sp = ss[i].Split(',');
                for (int j = 0; j < sp.Length; j++)
                {
                    if (sp[j].ToInt32() == 1)
                    {
                        restitution[j] += _investDic[i + 1][j].Point * _investDic[i + 1][j].RestorePercent / 100;
                    }
                    else
                    {
                        restitution[j] += 0;
                    }
                }
            }
            return restitution;
        }

        int BuildPrizeKey(int activityId, int activityStep)
        {
            return activityId*1000 + activityStep;
        }
        #endregion

        
    }
}
