using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;

namespace Games.NBall.Bll.Schedule
{
    public class ScheduleConfig
    {
        private Dictionary<int, ConfigScheduleEntity> _configScheduleDic;

        #region .ctor
        public ScheduleConfig(int p)
        {
            Init();
        }
        #endregion

        #region Facade
        public static ScheduleConfig Instance
        {
            get { return SingletonFactory<ScheduleConfig>.SInstance; }
        }

        public ConfigScheduleEntity GetEntity(EnumSchedule enumSchedule)
        {
            return GetEntity((int)enumSchedule);
        }

        public ConfigScheduleEntity GetEntity(int idx)
        {
            if (_configScheduleDic.ContainsKey(idx))
                return _configScheduleDic[idx];
            return null;
        }
        #endregion

        #region encapsulation
        void Init()
        {
            var list = ConfigScheduleMgr.GetAllForCache();
            _configScheduleDic = list.ToDictionary(d => d.Idx, d => d);
        }
        #endregion
    }
}
