using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Shadow
{
    public class BaseShadowEntity
    {
        /// <summary>
        /// 记录id
        /// </summary>
        public long Idx { get; set; }

        /// <summary>
        /// 事务id
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int OperationType { get; set; }
    }
}
