using System;
using System.Runtime.Serialization;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Entity
{    

	public partial class ConfigBuffengineEntity
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
	
	
    public partial class ConfigBuffengineResponse
    {

    }
}

