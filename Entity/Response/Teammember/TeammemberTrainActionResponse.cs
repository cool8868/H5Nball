using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Teammember
{
    /// <summary>
    /// 球员训练动作响应
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberTrainActionResponse : BaseResponse<TeammemberTrainActionEntity>
    {
    }

    /// <summary>
    /// 球员训练动作
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberTrainActionEntity
    {
        ///<summary>
        ///训练状态：0,初始;1,正在训练;2,恢复体力
        ///</summary>
        [DataMember]
        public int TrainState { get; set; }

        ///<summary>
        ///训练剩余时间/恢复体力剩余时间，单位秒
        /// 这个是绝对时间，不是相对于基准时间的刻度
        ///</summary>
        [DataMember]
        public long TrainTick { get; set; }

        ///<summary>
        ///球员训练等级
        ///</summary>
        [DataMember]
        public int Level { get; set; }

        ///<summary>
        ///球员训练经验
        ///</summary>
        [DataMember]
        public int EXP { get; set; }

        /// <summary>
        /// 球员升级经验
        /// </summary>
        [DataMember]
        public int LevelupExp { get; set; }

        /// <summary>
        /// 球员训练体力
        /// </summary>
        [DataMember]
        public int TrainStamina { get; set; }

        /// <summary>
        /// 当前点券数量，加速训练时用
        /// </summary>
        [DataMember]
        public int CurPoint { get; set; }

        /// <summary>
        /// 加速训练check
        /// </summary>
        [DataMember]
        public MallCheckExtraEntity CheckQuickenResult { get; set; }

        /// <summary>
        /// 帮助训练-增加亲密度
        /// </summary>
        [DataMember]
        public int AddIntimacy { get; set; }

        /// <summary>
        /// 帮助训练-当前亲密度
        /// </summary>
        [DataMember]
        public int CurIntimacy { get; set; }
        /// <summary>
        /// 帮助训练-增加游戏币
        /// </summary>
        [DataMember]
        public int AddCoin { get; set; }

        /// <summary>
        /// 帮助训练-当前游戏币
        /// </summary>
        [DataMember]
        public int CurCoin { get; set; }

        /// <summary>
        /// 帮助训练-增加的友情点
        /// </summary>
        [DataMember]
        public int AddFriendShipPoint { get; set; }

        /// <summary>
        /// 帮助训练-当前友情点
        /// </summary>
        [DataMember]
        public int CurFriendShipPoint { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }



    }
}
