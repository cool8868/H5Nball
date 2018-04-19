using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Dailycup
{
    /// <summary>
    /// 杯赛数据响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailycupFullDataResponse : BaseResponse<DailycupFullDataEntity>
    {
    }

    /// <summary>
    /// 杯赛数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailycupFullDataEntity
    {
        /// <summary>
        /// 杯赛id，上一届就id-1，下一届是+1，id要大于0
        /// </summary>
        [DataMember]
        public int DailycupId { get; set; }

        /// <summary>
        /// 是否有下一届，如果没有则下一届的按钮不能点亮
        /// </summary>
        [DataMember]
        public bool HasNext { get; set; }

        /// <summary>
        /// 当前进行到的回合类型：1,8强;2,4强;3,决赛
        /// </summary>
        [DataMember]
        public int RoundType { get; set; }

        /// <summary>
        /// 回合->比赛
        /// </summary>
        [DataMember]
        public List<DailycupMatchEntity> Matchs { get; set; }

        /// <summary>
        /// 最大竞猜次数
        /// </summary>
        [DataMember]
        public int GambleCountMax { get; set; }
        /// <summary>
        /// 参加状态,0-未参加；1-已参加
        /// </summary>
        [DataMember]
        public int AttendState { get; set; }
        /// <summary>
        /// 我的竞猜数据
        /// </summary>
        [DataMember]
        public List<DailycupGambleEntity> MyGambleData { get; set; } 

        /// <summary>
        /// 将一组比赛信息转成淘汰赛信息
        /// </summary>
        /// <param name="matchs">比赛信息按round和比赛id排序</param>
        /// <returns></returns>
        public List<List<DailycupMatchEntity>> ConvertToRounds(List<DailycupMatchEntity> matchs)
        {
            List<List<DailycupMatchEntity>> rounds = new List<List<DailycupMatchEntity>>();
            int currentRound = 0;
            rounds.Add(new List<DailycupMatchEntity>());
            for (int i = 0; i < matchs.Count; i++)
            {
                if (rounds[currentRound].Count <= 0)
                {
                    rounds[currentRound].Add(matchs[i]);
                }
                else if (rounds[currentRound][0].Round != matchs[i].Round) //轮数不同
                {
                    currentRound++;
                    rounds.Add(new List<DailycupMatchEntity>());//创建新一轮
                    rounds[currentRound].Add(matchs[i]);
                }
                else
                {
                    rounds[currentRound].Add(matchs[i]);
                }
            }
            return rounds;
        }
    }
}
