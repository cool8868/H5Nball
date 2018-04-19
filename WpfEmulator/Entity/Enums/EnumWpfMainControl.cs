using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WpfEmulator.Entity
{
    public enum EnumWpfMainControl
    {
        /// <summary>
        /// 发钱发卡
        /// </summary>
        SendMoney,
        /// <summary>
        /// Api调试
        /// </summary>
        ApiDebug,
        /// <summary>
        /// 背包
        /// </summary>
        Package,
        /// <summary>
        /// 巡回赛
        /// </summary>
        Tour,
        /// <summary>
        /// 精英巡回赛
        /// </summary>
        Elite,
        /// <summary>
        /// 天梯赛
        /// </summary>
        Ladder,
        /// <summary>
        /// 世界挑战赛record
        /// </summary>
        WCHRecord,
        /// <summary>
        /// 世界挑战赛info
        /// </summary>
        WCHInfo,
    }

    public enum ShowPanelType
    {
        Login = 1,

        Register = 2,

        Main = 3,
    }
}
