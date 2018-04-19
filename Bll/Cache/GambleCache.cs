using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;

namespace Games.NBall.Bll.Cache
{
    public class GambleCache
    {
        public GambleCache(int p)
        {
            InitCache();

        }

        public static GambleCache Instance
        {
            get { return SingletonFactory<GambleCache>.SInstance; }
        }


        private Dictionary<string, ConfigGambleiconEntity> _gambleIconDictionary;

        private void InitCache()
        {
            var listGambleIcon = ConfigGambleiconMgr.GetAll();
            _gambleIconDictionary = new Dictionary<string, ConfigGambleiconEntity>();

            foreach (var inner in listGambleIcon)
            {
                if (_gambleIconDictionary.ContainsKey(inner.Name))
                {
                    _gambleIconDictionary[inner.Name] = inner;

                }
                _gambleIconDictionary.Add(inner.Name,inner);
             
            }
        }

        public int GetIcon(string name)
        {
            return _gambleIconDictionary[name].Idx;
        }

        public List<ConfigGambleiconEntity> GetAllIcon()
        {
            try
            {
                return _gambleIconDictionary.Values.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
