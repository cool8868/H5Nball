using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;

namespace Games.NBall.Core.Ladder
{
    public class LadderMatchUtil
    {
        private static int _arenaProctiveScore = 0;
        private static LadderMatchUtil _instance = new LadderMatchUtil();
        #region .ctor
        private LadderMatchUtil()
        {
            _arenaProctiveScore = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderProctiveScore);
        }
        #endregion

        #region Facade

        #region Instance
        public static LadderMatchUtil Instance
        {
            get { return _instance; }
        }
        #endregion

        
        #endregion

        #region encapsulation


       
        #endregion
    }
}
