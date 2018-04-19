using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class MallSalerecordEntity
	{
        public string OrderId
        {
            get
            {
                return Idx.ToString() + MallCode;
            }
        }
	}
	
	
    public partial class MallSalerecordResponse
    {

    }
}

