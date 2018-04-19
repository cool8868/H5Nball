using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Weibo.Data
{
    public struct DefineWyxUri
    {
        public const string PREFIXWyxUriV1 = "http://api.weibo.com/game/1/";

        #region 适应接口
        public const string CastUserShow = "wb_user_show";
        public const string CastRoleUpdate = "wb_user_upload";
        #endregion

        #region 用户信息相关接口
        /// <summary>
        /// 获取某用户的个人信息
        /// </summary>
        public const string User_show = "user/show";
        /// <summary>
        /// 获取当前用户的互粉好友信息
        /// </summary>
        public const string User_friends = "user/friends";
        /// <summary>
        /// 获取当前用户的互粉好友ID
        /// </summary>
        public const string User_friend_ids = "user/friend_ids";
        /// <summary>
        /// 获取当前用户安装了当前应用的互粉好友信息
        /// </summary>
        public const string User_app_friends = "user/app_friends";
        /// <summary>
        /// 获取当前用户安装了当前应用的互粉好友ID 返回所有结果(不分页)
        /// </summary>
        public const string User_app_friend_ids = "user/app_friend_ids";
        /// <summary>
        /// 判断两个用户是否为互粉关系 
        /// </summary>
        public const string User_are_friends = "user/are_friends";
        #endregion

        #region 用户信息相关接口
        /// <summary>
        /// 判断当前用户是否是本应用微博的粉丝
        /// </summary>
        public const string Application_is_fan = "application/is_fan";
        /// <summary>
        /// 判断是否是本应用的用户(是否安装了本应用)
        /// </summary>
        public const string Application_is_user = "application/is_user";
        /// <summary>
        /// 判断用户是否已经为本应用打分
        /// </summary>
        public const string Application_scored = "application/scored";
        /// <summary>
        /// 获取应用接口调用限额 
        /// </summary>
        public const string Application_rate_limit_status = "application/rate_limit_status";
        #endregion

        #region 通知相关接口
        /// <summary>
        ///  发送单条通知 
        /// </summary>
        public const string Notice_send = "Notice/send";
        #endregion

        #region 支付相关接口
        /// <summary>
        /// 获到支付token
        /// </summary>
        public const string Pay_get_token = "pay/get_token";
        /// <summary>
        /// 查询订单状态
        /// </summary> 
        public const string Pay_order_status = "pay/order_status";
        #endregion

        #region 成就相关接口
        /// <summary>
        /// 设置成就
        /// </summary>
        public const string Achievements_set = "achievements/set";
        /// <summary>
        /// 获得成就
        /// </summary>
        public const string Achievements_get = "achievements/get";
        #endregion

        #region 排行榜相关接口
        /// <summary>
        /// 设置排行榜(数值)
        /// </summary>
        public const string Leaderboards_set = "leaderboards/set";
        /// <summary>
        /// 获取好友排行榜
        /// </summary>
        public const string Leaderboards_get_friends = "leaderboards/get_friends";
        /// <summary>
        /// 排行计数累加
        /// </summary>
        public const string Leaderboards_increment = "leaderboards/increment";
        /// <summary>
        /// 获取总排行 
        /// </summary>
        public const string Leaderboards_get_total = "leaderboards/get_total";
        #endregion

        #region 创建角色上行接口
        public const string Ingame_RoleUpdate = "ingame/role_update";
        #endregion

    }
}
