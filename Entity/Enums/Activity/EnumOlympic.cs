using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Activity
{
    public enum EnumOlympic
    {
        /// <summary>
        /// 足球金牌
        /// </summary>
        Football = 1,
        /// <summary>
        /// 篮球金牌
        /// </summary>
        Basketball = 2,
        /// <summary>
        /// 排球金牌
        /// </summary>
        Volleyball = 3,
        /// <summary>
        /// 游泳金牌
        /// </summary>
        Swimming = 4,
        /// <summary>
        /// 体操金牌
        /// </summary>
        Gymnastics = 5,
        /// <summary>
        /// 射击金牌
        /// </summary>
        Shooting = 6,
        /// <summary>
        /// 田径金牌
        /// </summary>
        TrackAndField = 7,
        /// <summary>
        /// 举重金牌
        /// </summary>
        WeightLifting = 8,
        /// <summary>
        /// 乒乓球金牌
        /// </summary>
        TableTennis = 9,
        /// <summary>
        /// 羽毛球金牌
        /// </summary>
        Badminton = 10,
        /// <summary>
        /// 赛艇金牌
        /// </summary>
        Rowing = 11,
        /// <summary>
        /// 柔道金牌
        /// </summary>
        Judo = 12
    }

    /// <summary>
    /// 奥运金牌掉落类型
    /// </summary>
    public enum EnumOlympicGeyType
    {
        //1= 金币球探 2=钻石球探 3=友情的球探 4=友谊赛 5=紫卡分解 6=橙卡元老分解
        /// <summary>
        /// 金币抽卡
        /// </summary>
        ScoutingCoin = 1,
        /// <summary>
        /// 钻石抽卡
        /// </summary>
        ScoutingPoint = 2,
        /// <summary>
        /// 友情点抽卡
        /// </summary>
        ScoutingFriend = 3,
        /// <summary>
        /// 友谊赛
        /// </summary>
        FriendMatch = 4,
        /// <summary>
        /// 分解紫卡
        /// </summary>
        DecomposePurple = 5,
        /// <summary>
        /// 分解橙卡
        /// </summary>
        DecomposePurpleOrange = 6,
    }
}
