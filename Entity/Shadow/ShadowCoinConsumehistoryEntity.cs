using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Shadow
{
    public class ShadowCoinConsumehistoryEntity
    {
        #region Public Properties

        public long Idx { get; set; }

        ///<summary>
        ///经理id
        ///</summary>
        public System.Guid ManagerId { get; set; }

        ///<summary>
        ///消耗金币数
        ///</summary>
        public System.Int32 Coin { get; set; }

        ///<summary>
        ///消费来源类型
        ///</summary>
        public System.Int32 SourceType { get; set; }

        ///<summary>
        ///订单id
        ///</summary>
        public System.String SourceId { get; set; }

        ///<summary>
        ///RowTime
        ///</summary>
        public System.DateTime RowTime { get; set; }
        #endregion

    }
}
