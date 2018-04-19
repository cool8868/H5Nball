using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Response;

namespace Games.NBall.Core.League
{
    /// <summary>
    /// 联赛级别枚举
    /// 联赛级别0业余联赛，1乙级联赛，2甲级联赛，3超级联赛
    /// </summary>
    public enum EnumLeagueLevel
    {
        /// <summary>
        /// 俄超
        /// </summary>
        Russian = 0,
        /// <summary>
        /// 葡超
        /// </summary>
        Portugal = 1,
        /// <summary>
        /// 荷甲
        /// </summary>
        Dutch = 2,
        /// <summary>
        /// 法甲
        /// </summary>
        France = 3,
        /// <summary>
        /// 意甲
        /// </summary>
        Italy = 4,
        /// <summary>
        /// 德甲
        /// </summary>
        Germany = 5,
        /// <summary>
        /// 英超
        /// </summary>
        England = 6,
        /// <summary>
        /// 西甲
        /// </summary>
        Spain = 7
    }

    /// <summary>
    /// 各级别联赛等级要求
    /// </summary>
    public enum EunmLeagueOpenLevel
    {
        /// <summary>
        /// 俄超
        /// </summary>
        Russian = 1,
        /// <summary>
        /// 葡超
        /// </summary>
        Portugal = 10,
        /// <summary>
        /// 荷甲
        /// </summary>
        Dutch = 20,
        /// <summary>
        /// 法甲
        /// </summary>
        France = 30,
        /// <summary>
        /// 意甲
        /// </summary>
        Italy = 40,
        /// <summary>
        /// 德甲
        /// </summary>
        Germany = 50,
        /// <summary>
        /// 英超
        /// </summary>
        England = 60,
        /// <summary>
        /// 西甲
        /// </summary>
        Spain = 80
    }

    public enum EnumLeaguePrize
    {
        /// <summary>
        /// 经验
        /// </summary>
        Exp=1,
        /// <summary>
        /// 金币
        /// </summary>
        Coin=2,
        /// <summary>
        /// 积分
        /// </summary>
        Score=3,
        /// <summary>
        /// 道具
        /// </summary>
        Item=4,
        /// <summary>
        /// 钻石
        /// </summary>
        Point=5
    }


    /// <summary>
    /// 联赛状态枚举
    /// 0未开始，1开始接受报名，但人数不够，2人数足够，3分组开始，4分组结束，5业余联赛新增加了分组开始，6业余联赛新增分组结束，7比赛开打，8比赛全部结束，9比赛颁奖开始，10比赛颁奖结束，11开始开奖（竞猜开奖），12完成开奖，13升降级开始,14升降级结束,15联赛完成
    /// </summary>
    public enum EnumLeagueStatus
    {
        /// <summary>
        /// 刚初始化，一切为0
        /// </summary>
        Init,
        /// <summary>
        /// 开始接受报名，但人数不够--1
        /// </summary>
        StartJoin,
        /// <summary>
        /// 人数足够--2
        /// </summary>
        EndJoin,
        /// <summary>
        /// 分组开始--3
        /// </summary>
        StartGroup,
        /// <summary>
        /// 分组结束--4
        /// </summary>
        EndGroup,
       /// <summary>
        /// 比赛开打--5
        /// </summary>
        StartMatch,
        /// <summary>
        /// 比赛全部结束--6
        /// </summary>
        EndMatch,

        /// <summary>
        /// 某些状态下，需要保持，比如打完比赛，需要Hold住一段时间--7
        /// </summary>
        Holding,
        /// <summary>
        /// 开始发放单场奖励--8
        /// </summary>
        StartSingleReward,
        /// <summary>
        /// 结束发放单场奖励--9
        /// </summary>
        EndSingleReward,

        /// <summary>
        /// 比赛颁奖开始--10
        /// </summary>
        StartReward,
        /// <summary>
        /// 比赛颁奖结束--11
        /// </summary>
        EndReward,
        /// <summary>
        /// 开始开奖（竞猜开奖）--14
        /// </summary>
        StartBet,
        /// <summary>
        /// 完成开奖--15
        /// </summary>
        EndBet,
        /// <summary>
        /// 升降级开始--16
        /// </summary>
        StartLevelChange,
        /// <summary>
        /// 升降级结束--17
        /// </summary>
        EndLevelChange,
       

        /// <summary>
        /// 联赛完成--18
        /// </summary>
        Complete
    }

    public enum EnumLeagueMatchDataStatus
    {
        /// <summary>
        /// 为开始比赛
        /// </summary>
        NoStartMatch,
        /// <summary>
        /// 已经打过比赛了
        /// </summary>
        Matched,
        /// <summary>
        ///包含以上两张状况
        /// </summary>
        ALL
    }

    /// <summary>
    /// 联赛奖励类型枚举
    /// </summary>
    public enum EnumLeagueRewardType
    {
        /// <summary>
        /// 金币
        /// </summary>
        Coin,
        /// <summary>
        /// 冠军杯积分
        /// </summary>
        WorldScore,
        /// <summary>
        /// 点券
        /// </summary>
        Points
    }
    /// <summary>
    /// 比赛结果枚举
    /// </summary>
    public enum EnumMatchResult
    {
        /// <summary>
        /// 胜
        /// </summary>
        Win,
        /// <summary>
        /// 平
        /// </summary>
        Draw,
        /// <summary>
        /// 负
        /// </summary>
        Lose
    }

    /// <summary>
    /// 联赛的系统常量
    /// </summary>
    public class LeagueConst
    {
        //超级联赛人数
        public const int SuperCount = 10;
        //甲级联赛人数
        public const int GradeACount = 20;
        //乙级联赛人数
        public const int GradeBCount = 40;
        //业余联赛人数
        public const int AmateurCount = 16;
        //每个小组的人数
        public const int GroupCount = 10;

        //随机数的最大值
        public const int RandomMax = 10000;

        //联赛一共有多少轮次
        public const int RoundMax = 18;
        //每轮联赛有多少场比赛
        public const int MatchCountPerRound = 5;

        //超级联赛比赛数量
        public const int SuperMatchNums = 90;
        //甲级联赛比赛数量
        public const int GradeAMatchNums = 180;
        //乙级联赛比赛数量
        public const int GradeBMatchNums = 360;

        //球员数据库表的前缀
        public const string TeamMemberTablePrefix = "Teammember_";

        //各级别联赛分组数量
        public const int SuperGroupCount = 1;
        public const int GradeAGroupCount = 2;
        public const int GradeBGroupCount = 4;
        public const int AmateurGroupCount = 1;
        //联赛最终排名前几位的记录成就数据
        public const int AchievementRange = 5;
        //比赛开始前多少分钟关闭竞猜
        public const int BetStopMins = -15;
        //联赛竞猜最低赔率，低于这个赔率，不扣除税费
        public const double BetMinRate = 1.05;
        //联赛冠军奖励发奖，各级别对应的subtype
        public const int SuperSubType = 1;
        public const int GradeASubType = 2;
        public const int GradeBSubType = 3;

        /// <summary>
        /// 以毫秒为单位，1分钟,调试为10秒
        /// </summary>
        public const double TriggerInterval = 6000;
        /// <summary>
        /// 延迟开奖的分钟数
        /// </summary>
        public const int DelayOpenBetMinutes = 10;

        public static readonly Guid GambleNpcId = new Guid("00000000-0000-0000-0000-000000000002");

        /// <summary>
        /// NPC每场比赛，每个结果的押注额度
        /// </summary>
        public const int NpcGambleCount = 50;

        /// <summary>
        /// 让球计算规则数组
        /// </summary>
        public static readonly int[,] ArraySubGoal = new int[,] { {0,200,0} , {201,400,1},{401,600,2},{601,1000,3},{1001,1000000,4} };
    }

    /// <summary>
    /// 联赛的一些字典数据表
    /// </summary>
    public class LeagueConfig
    {
        private static LeagueConfig instance;
        ////当前轮次
        //private static int currentRound = 1;
        //当前联赛实体，避免多次查询数据库
        private static LeagueInfoEntity superLeague = null;
        private static LeagueInfoEntity gradeALeague = null;
        private static LeagueInfoEntity gradeBLeague = null;
        private static LeagueInfoEntity amateurLeague = null;
        
        private LeagueConfig() 
        {
            
        }
        public static LeagueConfig Instance
　　    { 
　　        get 
　　        {
                if (instance == null)
                {
                    instance = new LeagueConfig();
                }
　　            return instance; 
　　        } 
　　    }
        
    }
}
