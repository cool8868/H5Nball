using System;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class LeagueEncounterEntity
    {
        
        ///<summary>
        ///NPC队伍ICON编号
        ///</summary>
        [DataMember]
        [ProtoMember(17)]
        public System.Int32 IconId { get; set; }

        ///<summary>
        ///主队胜场数
        ///</summary>
        [DataMember]
        [ProtoMember(18)]
        public System.Int32 HomeWin { get; set; }

        ///<summary>
        ///主队平场数
        ///</summary>
        [DataMember]
        [ProtoMember(19)]
        public System.Int32 HomeFlat { get; set; }

        ///<summary>
        ///主队负场数
        ///</summary>
        [DataMember]
        [ProtoMember(20)]
        public System.Int32 HomeLose { get; set; }

        ///<summary>
        ///客队胜场数
        ///</summary>
        [DataMember]
        [ProtoMember(21)]
        public System.Int32 AwayWin { get; set; }

        ///<summary>
        ///主队平场数
        ///</summary>
        [DataMember]
        [ProtoMember(22)]
        public System.Int32 AwayFlat { get; set; }

        ///<summary>
        ///主队负场数
        ///</summary>
        [DataMember]
        [ProtoMember(23)]
        public System.Int32 AwayLose { get; set; }

        ///<summary>
        ///主队Icon
        ///</summary>
        [DataMember]
        [ProtoMember(24)]
	    public System.Int32 HomeIconId { get; set; }

        ///<summary>
        ///客队Icon
        ///</summary>
        [DataMember]
        [ProtoMember(25)]
        public System.Int32 AwayIconId { get; set; }


    }
	
	
    public partial class LeagueEncounterResponse
    {

    }
}

