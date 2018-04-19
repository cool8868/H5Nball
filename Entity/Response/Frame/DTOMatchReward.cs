using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class DTOMatchRewardState
    {
       
        [DataMember]
        [ProtoMember(1)]
        public Guid MatchId
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(2)]
        public int GetTimes
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(3)]
        public int SetTimes
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(4)]
        public int Coin
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(5)]
        public int Point
        {
            get;
            set;
        }
    }

    [DataContract]
    [Serializable]
    public class DTOAssetInfo
    {
        public DTOAssetInfo()
        {
            Coin = -1;
            Point = -1;
        }
        public DTOAssetInfo(int coin, int point)
        {
            Coin = coin;
            Point = point;
        }
        [DataMember]
        public int Coin
        {
            get;
            set;
        }

        [DataMember]
        public int Point
        {
            get;
            set;
        }
      
    }
}
