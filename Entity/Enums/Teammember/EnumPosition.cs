using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    public enum EnumPosition
    {
        /// <summary>
        /// 守门员
        /// </summary>
        Goalkeeper = 0,

        /// <summary>
        /// 后卫
        /// </summary>
        Fullback = 1,

        /// <summary>
        /// 中场
        /// </summary>
        Midfielder = 2,

        /// <summary>
        /// 前锋
        /// </summary>
        Forward = 3
    }

    public enum EnumTrainState
    {
        /// <summary>
        /// 初始
        /// </summary>
        None,
        /// <summary>
        /// 训练中
        /// </summary>
        Train
    }

}
