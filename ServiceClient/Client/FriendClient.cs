using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Friend;
using Games.NBall.Entity.Response.Match;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class FriendClient
    {
        private static IFriendService proxy = ServiceProxy<IFriendService>.Create("NetTcp_IFriendService");
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="name"></param>
        /// <param name="hasTask"></param>
        /// <returns></returns>
        public MessageCodeResponse AddFriend(Guid managerId, string name, bool hasTask)
        {
            return proxy.AddFriend(managerId, name, hasTask);
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public MessageCodeResponse AddBlack(Guid managerId, string name)
        {
            return proxy.AddBlack(managerId, name);
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="byManagerId"></param>
        /// <returns></returns>
        public MessageCodeResponse AddBlack(Guid managerId, Guid byManagerId)
        {
            return proxy.AddBlack2(managerId, byManagerId);
        }

        /// <summary>
        /// 获取我的好友列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MyFriendsResponse GetMyFriends(Guid managerId, int pageIndex, int pageSize)
        {
            return proxy.GetMyFriends(managerId, pageIndex, pageSize);
        }

        /// <summary>
        ///  获取被邀请列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public FriendsAddInfoResponse GetFriendRequestList(Guid managerId)
        {
            return proxy.GetFriendRequestList(managerId);
        }

        /// <summary>
        /// 忽略好友邀请
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        public MessageCodeResponse IgnoreAddFriend(Guid managerId, Guid friendId)
        {
            return proxy.IgnoreAddFriend(managerId, friendId);
        }

        /// <summary>
        /// 获取黑名单列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MyBlacksResponse GetMyBlacks(Guid managerId, int pageIndex, int pageSize)
        {
            return proxy.GetMyBlacks(managerId, pageIndex, pageSize);
        }

        /// <summary>
        /// 友谊赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public MatchCreateResponse Fight(Guid managerId, Guid awayId)
        {
            return proxy.Fight(managerId, awayId);
        }

        /// <summary>
        /// 获取友谊赛结果
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public FriendMatchResponse GetMatchResponse(Guid matchId)
        {
            return proxy.GetMatchResponse(matchId);
        }

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MyFriendsResponse DeleteFriend(Guid managerId, int recordId, int pageIndex, int pageSize)
        {
            return proxy.DeleteFriend(managerId, recordId, pageIndex, pageSize);
        }
        /// <summary>
        /// 删除黑名单
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public MyBlacksResponse DeleteBlack(Guid managerId, int recordId, int pageIndex, int pageSize)
        {
            return proxy.DeleteBlack(managerId, recordId, pageIndex, pageSize);
        }

        /// <summary>
        /// 好友邀请可获得奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PriendInvitePrizeListResponse GetFriendInvitePrizeList(string account, Guid managerId)
        {
           return proxy.GetFriendInvitePrizeList(account, managerId);
        }

        /// <summary>
        /// 领取好友邀请奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <param name="inviteCount"></param>
        /// <returns></returns>
        public InvitePrizeResponse InvitePrize(string account, Guid managerId, int inviteCount)
        {
            return proxy.InvitePrize(account, managerId, inviteCount);
        }

        /// <summary>
        /// 获取好友邀请成长奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public InviteFriendGrowUpPrizeResponse GetInviteFriendGrowUpPrize(string account, Guid managerId)
        {
            return proxy.GetInviteFriendGrowUpPrize(account, managerId);
        }

        /// <summary>
        /// 领取好友成长奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GrowUpPrizeResponse GrowUpPrize(string account, Guid managerId)
        {
            return proxy.GrowUpPrize(account, managerId);
        }

    }
}
