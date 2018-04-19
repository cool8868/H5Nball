using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Shadow
{
    public class ShadowTeammemberEntity : BaseShadowEntity
    {
        /// <summary>
        /// TeammemberId
        /// </summary>
        public Guid TeammemberId { get; set; }

        /// <summary>
        /// PlayerId
        /// </summary>
        public int PlayerId { get; set; }
    }
}
