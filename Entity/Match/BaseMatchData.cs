using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Entity.Match
{
    /// <summary>
    /// 玩家报名信息，包含区和连接信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class BaseMatchData
    {
        public BaseMatchData(int matchType, Guid matchId)
        {
            this.MatchType = matchType;
            this.MatchId = matchId;
            this.RowTime = DateTime.Now;
        }

        public BaseMatchData(EnumMatchType matchType, Guid matchId, Guid homeId, Guid awayId)
            :this((int)matchType,matchId,homeId,awayId)
        {
        }

        public BaseMatchData(int matchType, Guid matchId, Guid homeId, Guid awayId)
            :this(matchType,matchId)
        {
            this.Home = new MatchManagerInfo(homeId);
            this.Away = new MatchManagerInfo(awayId);
        }

        public BaseMatchData(EnumMatchType matchType, Guid matchId, MatchManagerInfo home, MatchManagerInfo away)
            :this((int)matchType,matchId,home,away)
        {
        }

        public BaseMatchData(int matchType, Guid matchId, MatchManagerInfo home, MatchManagerInfo away)
            : this(matchType, matchId)
        {
            this.Home = home;
            this.Away = away;
        }

        /// <summary>
        /// 比赛类型
        /// </summary>
        public int MatchType { get; set; }

        /// <summary>
        /// 比赛id
        /// </summary>
        public Guid MatchId { get; set; }

        /// <summary>
        /// Gets or sets the away score.
        /// </summary>
        /// <value>The away score.</value>
        public MatchManagerInfo Away
        { get; set; }

        /// <summary>
        /// Gets or sets the home score.
        /// </summary>
        /// <value>The home score.</value>
        public MatchManagerInfo Home
        { get; set; }
        
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime RowTime { get; set; }

        /// <summary>
        /// 是否有任务
        /// </summary>
        public bool HasTask { get; set; }

        /// <summary>
        /// 是否不能平局
        /// </summary>
        public bool NoDraw { get; set; }
        /// <summary>
        /// 是否新手引导
        /// </summary>
        public bool IsGuide { get; set; }
    }
}
