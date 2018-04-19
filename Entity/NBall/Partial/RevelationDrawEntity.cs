using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class RevelationDrawEntity
	{
        /// <summary>
        /// 需要消耗多少钻石
        /// </summary>
        [DataMember]
        public int Price { get; set; }
	}
	
	
    public partial class RevelationDrawResponse
    {

    }
}
