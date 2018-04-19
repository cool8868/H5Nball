using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class PlayerKillOpponentResponse : BaseResponse<PlayerKillOpponentListEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class PlayerKillOpponentListEntity
    {
        [DataMember]
        public List<PlayerKillOpponentEntity> Opponents { get; set; }

        /// <summary>
        /// 对手刷新时间
        /// </summary>
        [DataMember]
        public long OpponentRefreshTimeTick { get; set; }
    }

    [Serializable]
    [DataContract]
    [ProtoContract]
    public class PlayerKillOpponentEntity
    {
        [DataMember]
        [ProtoMember(1)]
        public Guid ManagerId { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public string Name { get; set; }

        [DataMember]
        [ProtoMember(3)]
        public string Logo { get; set; }

        [DataMember]
        [ProtoMember(4)]
        public int Level { get; set; }

        [DataMember]
        [ProtoMember(5)]
        public int Kpi { get; set; }

        [DataMember]
        [ProtoMember(6)]
        public int Win { get; set; }

        [DataMember]
        [ProtoMember(7)]
        public int Lose { get; set; }

        [DataMember]
        [ProtoMember(8)]
        public int Draw { get; set; }

        [DataMember]
        [ProtoMember(9)]
        public int RemainByTimes { get; set; }
        /// <summary>
        /// 阵型id
        /// </summary>
        [DataMember]
        [ProtoMember(10)]
        public int FormationId { get; set; }

        [DataMember]
        [ProtoMember(11)]
        public bool HasWin { get; set; }

        public PlayerKillOpponentEntity Clone()
        {
            var entity = new PlayerKillOpponentEntity();
            entity.ManagerId = this.ManagerId;
            entity.Name = this.Name;
            entity.Logo = this.Logo;
            entity.Level = this.Level;
            entity.Kpi = this.Kpi;
            entity.Win = this.Win;
            entity.Lose = this.Lose;
            entity.Draw = this.Draw;
            entity.FormationId = this.FormationId;
         
            return entity;
        }
    }
}
