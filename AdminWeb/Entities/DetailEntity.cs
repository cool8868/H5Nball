using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.NBall.AdminWeb.Entities
{
    public class DetailEntity
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 日活跃用户数
        /// </summary>
        public int ActiveUsers { get; set; }

        /// <summary>
        /// 新用户
        /// </summary>
        public int NewUsers { get; set; }

        /// <summary>
        /// 新经理
        /// </summary>
        public int NewManagers { get; set; }

        /// <summary>
        /// 收入
        /// </summary>
        public int InComes { get; set; }
    }
}