using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class NbMatchstatEntity
	{
        public double WinRate
        {
            get
            {
                if (Win == 0)
                    return 0;
                else
                {
                    return Win * 1.00 / TotalCount;
                }
            }
        }

        public int TotalCount
        {
            get { return Win + Draw + Lose; }
        }
	}
	
	
    public partial class NbMatchstatResponse
    {

    }
}

