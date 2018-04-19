using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Entity.Response;
using Newtonsoft.Json;
using ProtoBuf;

namespace Games.NBall.Entity
{

    public partial class NbManagerbuffmemberEntity
    {

        [DataMember]
        [ProtoMember(101)]
        [JsonIgnore]
        public List<string> ReadySkillList { get; set; }

        [DataMember]
        [ProtoMember(102)]
        [JsonIgnore]
        public List<string> LiveSkillList { get; set; }

        [JsonIgnore]
        public List<string> FirmSkillList { get; set; }

        #region Buff
        [DataMember]
        [ProtoMember(111)]
        public List<MatchPropInput> MatchPropList
        {
            get;
            set;
        }
        [DataMember]
        [ProtoMember(112)]
        public List<MatchBoostInput> MatchBoostList
        {
            get;
            set;
        }
        #endregion

        #region TotalValue
        public double TotalSpeed
        {
            get { return SpeedConst + SpeedCount; }
        }
        public double TotalShoot
        {
            get { return ShootConst + ShootCount; }
        }
        public double TotalFreeKick
        {
            get { return FreeKickConst + FreeKickCount; }
        }
        public double TotalBalance
        {
            get { return BalanceConst + BalanceCount; }
        }
        public double TotalPhysique
        {
            get { return PhysiqueConst + PhysiqueCount; }
        }
        public double TotalPower
        {
            get { return PowerConst + PowerCount; }
        }
        public double TotalBounce
        {
            get { return BounceConst + BounceCount; }
        }
        public double TotalAggression
        {
            get { return AggressionConst + AggressionCount; }
        }
        public double TotalDisturb
        {
            get { return DisturbConst + DisturbCount; }
        }
        public double TotalInterception
        {
            get { return InterceptionConst + InterceptionCount; }
        }
        public double TotalDribble
        {
            get { return DribbleConst + DribbleCount; }
        }
        public double TotalPass
        {
            get { return PassConst + PassCount; }
        }
        public double TotalMentality
        {
            get { return MentalityConst + MentalityCount; }
        }
        public double TotalResponse
        {
            get { return ResponseConst + ResponseCount; }
        }
        public double TotalPositioning
        {
            get { return PositioningConst + PositioningCount; }
        }
        public double TotalHandControl
        {
            get { return HandControlConst + HandControlCount; }
        }
        public double TotalAcceleration
        {
            get { return AccelerationConst + AccelerationCount; }
        }
        #endregion
    }
	
	
    public partial class NbManagerbuffmemberResponse
    {

    }
}

