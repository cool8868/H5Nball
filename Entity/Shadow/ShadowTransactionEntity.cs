using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Shadow
{
    public class ShadowTransactionEntity
    {
        /// <summary>
        /// 事务id
        /// </summary>
        public Guid Idx { get; set; }
        /// <summary>
        /// 事务类型
        /// </summary>
        public int TransactionType { get; set; }
        /// <summary>
        /// 经理id
        /// </summary>
        public Guid ManagerId { get; set; }
        /// <summary>
        /// appname
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// app所属ip
        /// </summary>
        public string TerminalIP { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime RowTime { get; set; }
    }
}
