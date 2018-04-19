using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class PayUserEntity
	{
        /// <summary>
        /// 总点券数
        /// </summary>
        public int TotalPoint { get { return Point + Bonus; } }

        public int AddPoint { get; set; }

        public bool IsNew { get; set; }
	}
	
	
    public partial class PayUserResponse
    {

    }

    public class PayManagerEntity
    {
        public string Name { get; set; }
        public double TotalCash { get; set; }
    }
}

