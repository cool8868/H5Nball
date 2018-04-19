using System;
using System.Runtime.Serialization;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Entity
{    

	public partial class ConfigBuffpoolEntity
	{
        public int[] BaseBuffList
        {
            get;
            set;
        }
        public int[] PropIndexList
        {
            get;
            set;
        }
        public EnumBuffUnitType AsBuffUnitType
        {
            get;
            set;
        }
	}
	
	
    public partial class ConfigBuffpoolResponse
    {

    }
}

