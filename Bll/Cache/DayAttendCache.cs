using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;

namespace Games.NBall.Bll.Cache
{
    public class DayAttendCache
    {
        public DayAttendCache(int p)
        {
            InitCache();
        }

        #region encapsulation
        /// <summary>
        /// taskid->entity
        /// </summary>
        private Dictionary<int, ConfigDaysattendprizeEntity> _prizeDic;

        void InitCache()
        {
            var list = ConfigDaysattendprizeMgr.GetAll();
            _prizeDic = list.ToDictionary(d => d.Idx, d => d);
        }
        #endregion

        #region Facade

        public static DayAttendCache Instance
        {
            get { return SingletonFactory<DayAttendCache>.SInstance; }
        }

        public ConfigDaysattendprizeEntity GetPrizeEntity(int attendTimes)
        {
            if (_prizeDic.ContainsKey(attendTimes))
                return _prizeDic[attendTimes];
            return null;
        }

        #endregion
    }
}
