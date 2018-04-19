using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Cache;
using Games.NBall.Common;

namespace Games.NBall.Bll.Share
{
    public class CacheFactory
    {

        /// <summary>
        /// 物品字典缓存
        /// </summary>
        public static ItemsdicCache ItemsdicCache
        {
            get { return Cache.ItemsdicCache.Instance; }
        }

        /// <summary>
        /// 潘多拉缓存
        /// </summary>
        public static PandoraCache PandoraCache { get { return Cache.PandoraCache.Instance; } }

        /// <summary>
        /// 球员成长缓存
        /// </summary>
        public static TeammemberCache TeammemberCache { get { return Cache.TeammemberCache.Instance; } }

        /// <summary>
        /// 装备字典缓存
        /// </summary>
        public static EquipmentCache EquipmentCache
        {
            get { return Cache.EquipmentCache.Instance; }
        }
        /// <summary>
        /// 配置缓存
        /// </summary>
        public static AppsettingCache AppsettingCache { get { return Cache.AppsettingCache.Instance; } }
        /// <summary>
        /// 注册时的模板缓存
        /// </summary>
        public static TemplateCache TemplateCache { get { return Cache.TemplateCache.Instance; } }

        /// <summary>
        /// 经理数据缓存
        /// </summary>
        public static ManagerDataCache ManagerDataCache { get { return Cache.ManagerDataCache.Instance; } }

        /// <summary>
        /// Npc缓存
        /// </summary>
        public static NpcdicCache NpcdicCache { get { return Cache.NpcdicCache.Instance; } }

        /// <summary>
        /// 阵型缓存
        /// </summary>
        public static FormationCache FormationCache { get { return Cache.FormationCache.Instance; } }
        /// <summary>
        /// Vip权限缓存
        /// </summary>
        public static VipDicCache VipdicCache { get { return Cache.VipDicCache.Instance; } }
        /// <summary>
        /// 抽奖缓存
        /// </summary>
        public static LotteryCache LotteryCache { get { return Cache.LotteryCache.Instance; } }
        /// <summary>
        /// 球探缓存
        /// </summary>
        public static ScoutingCache ScoutingCache { get { return Cache.ScoutingCache.Instance; } }
        /// <summary>
        /// 友谊赛缓存
        /// </summary>
        public static PlayerKillCache PlayerKillCache { get { return Cache.PlayerKillCache.Instance; } }

        /// <summary>
        /// 阵形缓存
        /// </summary>
        public static FunctionAppCache FunctionAppCache { get { return Cache.FunctionAppCache.Instance; } }

        /// <summary>
        /// 球员缓存
        /// </summary>
        public static PlayersdicCache PlayersdicCache
        {
            get { return Cache.PlayersdicCache.Instance; }
        }

        public static ServicetionSectionCache ServicetionSectionCache
        {
            get { return Cache.ServicetionSectionCache.Instance; }
        }
        /// <summary>
        /// 商城物品
        /// </summary>
        public static MallCache MallCache { get { return Cache.MallCache.Instance; } }

        /// <summary>
        /// 联赛缓存
        /// </summary>
        public static LeagueCache LeagueCache { get { return Cache.LeagueCache.Instance; } }

        /// <summary>
        /// 天梯赛配置缓存
        /// </summary>
        public static LadderCache LadderCache { get { return Cache.LadderCache.Instance; } }

        /// <summary>
        /// 天梯赛季缓存
        /// </summary>
        public static SeasonCache SeasonCache { get { return Cache.SeasonCache.Instance; } }

        /// <summary>
        /// 杯赛奖励配置缓存
        /// </summary>
        public static DailycupCache DailycupCache { get { return Cache.DailycupCache.Instance; } }


        /// <summary>
        /// 好友缓存类
        /// </summary>
        public static FriendCache FriendCache { get { return Cache.FriendCache.Instance; } }

        /// <summary>
        /// 任务配置缓存
        /// </summary>
        public static TaskConfigCache TaskConfigCache { get { return Cache.TaskConfigCache.Instance; } }

        /// <summary>
        /// 签到缓存
        /// </summary>
        public static DayAttendCache DayAttendCache { get { return Cache.DayAttendCache.Instance; } }

        /// <summary>
        /// 活动配置缓存
        /// </summary>
        public static ActivityCache ActivityCache { get { return Cache.ActivityCache.Instance; } }

        /// <summary>
        /// 区配置
        /// </summary>
        public static ZoneCache ZoneCache {
            get { return Cache.ZoneCache.Instance; }
        }
        /// <summary>
        /// 转盘缓存
        /// </summary>
        public static TurntableCache TurntableCache { get { return Cache.TurntableCache.Instance; } }

        /// <summary>
        /// 竞技场缓存
        /// </summary>
        public static ArenaCache ArenaCache{
            get { return Cache.ArenaCache.Instance; }
        }

        public static CrowdCache CrowdCache { get { return Cache.CrowdCache.Instance; } }
        public static CrossLadderCache CrossLadderCache { get { return Cache.CrossLadderCache.Instance; } }

        /// <summary>
        /// 点球缓存
        /// </summary>
        public static AdCache AdCache{
            get { return Cache.AdCache.Instance; }
        }

        /// <summary>
        /// 球星启示录配置缓存
        /// </summary>
        public static RevelationCache RevelationCache
        {
            get { return Cache.RevelationCache.Instance; }
        }

        /// <summary>
        /// 球星启示录配置缓存
        /// </summary>
        public static CoachCache CoachCache
        {
            get { return Cache.CoachCache.Instance; }
        }
    }
}
