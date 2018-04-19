using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;

namespace Games.NBall.Bll.Cache
{
    public class DailycupCache
    {
        public DailycupCache(int p)
        {
            InitCache();
        }

        #region encapsulation
        /// <summary>
        /// taskid->entity
        /// </summary>
        private Dictionary<int, ConfigDailycupprizeEntity> _prizeDic;

        void InitCache()
        {
            var list = ConfigDailycupprizeMgr.GetAll();
            _prizeDic = list.ToDictionary(d => d.Rank, d => d);
        }
        #endregion

        #region Facade

        public static DailycupCache Instance
        {
            get { return SingletonFactory<DailycupCache>.SInstance; }
        }

        public ConfigDailycupprizeEntity GetEntity(int rank)
        {
            if (_prizeDic.ContainsKey(rank))
                return _prizeDic[rank];
            return null;
        }

        #endregion
    }
}
