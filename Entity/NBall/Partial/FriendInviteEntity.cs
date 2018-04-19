using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class FriendinviteEntity
    {  
        /// <summary>
        /// 经理名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 经理等级
        /// </summary>
        public int NLevel { get; set; }
	}
	
	
    public partial class FriendinviteResponse
    {

    }
}

