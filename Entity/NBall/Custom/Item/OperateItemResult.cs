using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Entity.NBall.Custom.Item
{
    /// <summary>
    /// 添加物品结果
    /// </summary>
    public class OperateItemResult
    {
        public OperateItemResult()
        {
        }

        public OperateItemResult(MessageCode code)
        {
            Code = code;
        }

        /// <summary>
        /// 结果code
        /// </summary>
        public MessageCode Code { get; set; }

        /// <summary>
        /// 添加的物品列表，可能为null
        /// </summary>
        public List<ItemInfoEntity> Items { get; set; }
    }
}
