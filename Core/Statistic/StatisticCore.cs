using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;

namespace Games.NBall.Core.Statistic
{
    public class StatisticCore
    {
        #region .ctor
        public StatisticCore(int p)
        {

        }
        #endregion

        #region Facade
        public static StatisticCore Instance
        {
            get { return SingletonFactory<StatisticCore>.SInstance; }
        }
        #endregion
    }
}
