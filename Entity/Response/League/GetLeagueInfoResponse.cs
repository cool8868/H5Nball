using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取联赛信息输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class GetLeagueInfoResponse:BaseResponse<LeagueInfoEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class LeagueInfoEntity
    {
        /// <summary>
        /// 是否回到主界面
        /// </summary>
        [DataMember]
        public bool IsHaveReturnMain { get; set; }

        /// <summary>
        /// 是否有开启的联赛
        /// </summary>
        [DataMember]
        public bool IsHaveStartLeague { get; set; }

        /// <summary>
        /// 当前联赛信息 
        /// </summary>
        [DataMember]
        public LeagueManagerrecordEntity LeagueInfo { get; set; }

        /// <summary>
        /// 比赛记录
        /// </summary>
        [DataMember]
        public LeagueRecordEntity LeagueRecord { get; set; }

        /// <summary>
        /// 胜场奖励记录
        /// </summary>
        [DataMember]
        public List<LeagueWinCountInfo> LeagueWincountRecord { get; set; }

        /// <summary>
        /// 我的胜场
        /// </summary>
        [DataMember]
        public int MyWinCount { get; set; }

        /// <summary>
        /// 排名列表
        /// </summary>
        [DataMember]
        public List<LeagueRankRecord> RankList { get; set; }

        /// <summary>
        /// 我的排名
        /// </summary>
        [DataMember]
        public int MyRank { get; set; }

        /// <summary>
        /// 锁住情况 true=锁住了
        /// </summary>
        [DataMember]
        public List<bool> LockList { get; set; }

    }
    
    [Serializable]
    [DataContract]
    public class LeagueFightMap
    {
        /// <summary>
        /// 主队
        /// </summary>
        [DataMember]
        public int HomeId{get;set;}

        /// <summary>
        /// 客队ID
        /// </summary>
        [DataMember]
        public int AwayId{get;set;}

        /// <summary>
        /// 主队进球数
        /// </summary>
        [DataMember]
        public int HomeGoals { get; set; }

        /// <summary>
        /// 客队进球数
        /// </summary>
        [DataMember]
        public int AwayGoals { get; set; }
    }

    public class LeagueLeagueRank
    {
        /// <summary>
        /// 球队ID  自己=0
        /// </summary>
        [DataMember]
        public int TeamId { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        [DataMember]
        public int Rank { get; set; }
    }


    /// <summary>
    /// 获取联赛各轮配对信息输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class GetLeaguePairListResponse : BaseResponse<LeagueInfoPairEntity>
    {

    }

    [Serializable]
    [DataContract]
    public class LeagueInfoPairEntity
    {
        /// <summary>
        /// 配对列表
        /// </summary>
        [DataMember]
        public List<LeagueFightMap> PairList { get; set; }
    }

    [Serializable]
    [DataContract]
    public class LeagueWinCountInfo
    {
        /// <summary>
        /// 奖励等级
        /// </summary>
        [DataMember]
        public int PrizeLevel { get; set; }

        /// <summary>
        /// 奖励状态 1=可领取 2=已经领取
        /// </summary>
        [DataMember]
        public int PrizeStatus { get; set; }

    }

    /// <summary>
    /// 获取联赛某一轮对阵记录输出
    /// </summary>
    [Serializable]
    [DataContract]
    public class LeagueGetFightMapRecordResponse : BaseResponse<LeagueGetFightMapRecord>
    {

    }

    /// <summary>
    /// 获取联赛某一轮对阵记录输出
    /// </summary>
    [Serializable]
    [DataContract]
    public class LeagueGetFightMapRecord
    {
        /// <summary>
        /// 回合数
        /// </summary>
        [DataMember]
        public int Round { get; set; }

        /// <summary>
        /// 对阵记录
        /// </summary>
        [DataMember]
        public List<LeagueFight> FightRecord { get; set; }
    }
}
