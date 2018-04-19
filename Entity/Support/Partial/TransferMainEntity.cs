using System;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class TransferMainEntity
	{

        ///<summary>
        ///拍卖开始时间
        ///</summary>
        [DataMember]
        [ProtoMember(21)]
        public long TransferStartTimeTick { get; set; }

        ///<summary>
        ///持续时间 秒
        ///</summary>
        [DataMember]
        [ProtoMember(22)]
        public long TransferDurationTick { get; set; }

        /// <summary>
        /// 按经理ID取模 能不能搜索到该物品
        /// </summary>
        public int ModId { get; set; }
	}
	
	
    public partial class TransferMainResponse
    {

    }
}
