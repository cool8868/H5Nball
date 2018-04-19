using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class DicItemEntity
	{
        #region Equipment
        public int EquipmentSuitType { get { return SubType; } }
        public int EquipmentQuality { get { return ThirdType; } }
        public int EquipmentSuitId { get { return FourthType; } }
        #endregion

        #region Player
        public int PlayerCardLevel { get { return SubType; } }
        public int PlayerLeagueLevel { get { return ThirdType; } }
        public int PlayerKpi { get { return FourthType; } }
        #endregion

        #region Mall
        public int MallMallType { get { return SubType; } }
        public int MallQuality { get { return ThirdType; } }
        public int MallEffectType { get { return FourthType; } }
        #endregion

        #region Ballsoul
        public int BallsoulColor { get { return SubType; } }
        public int BallsoulLevel { get { return ThirdType; } }
        public int BallsoulType { get { return FourthType; } }
        #endregion
	}
	
	
    public partial class DicItemResponse
    {

    }
}

