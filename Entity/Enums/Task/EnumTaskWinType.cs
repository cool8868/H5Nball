using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 任务获胜类型
    /// </summary>
    public enum EnumTaskWinType
    {
        /// <summary>
        /// 所有
        /// </summary>
        All=0,
        /// <summary>
        /// 获胜
        /// </summary>
        Win=1,
        /// <summary>
        /// 连胜
        /// </summary>
        WinningStreak=2,
        /// <summary>
        /// 三星获胜
        /// </summary>
        WinningAllStar = 3,
    }
}
