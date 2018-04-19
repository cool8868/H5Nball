using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Bll.Cache
{
    public class PlayerCardDecDicCache
    {
        /// <summary>
        /// DicGrowEntity>>entity
        /// </summary>
        static Dictionary<int, DicPlayercarddecEntity> _playerCardDecDic;

        public PlayerCardDecDicCache(int p)
        {
            InitCache();
        }

        void InitCache()
        {
            LogHelper.Insert("playerCarddec cache init start", LogType.Info);

            var list = DicPlayercarddecMgr.GetAll();
            _playerCardDecDic = list.ToDictionary(d => d.ItemCode, d => d);

            LogHelper.Insert("playerCarddec cache init end", LogType.Info);
        }

        public static PlayerCardDecDicCache Instance
        {
            get { return SingletonFactory<PlayerCardDecDicCache>.SInstance; }
        }

        public DicPlayercarddecEntity GetPlayerCardDec(int quality)
        {
            return _playerCardDecDic[quality];
        }
    }
}
