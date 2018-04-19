using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Friend;
using Games.NBall.Entity.Response.Match;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IFriendService
    {
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="name"></param>
        /// <param name="hasTask"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse AddFriend(Guid managerId, string name, bool hasTask);

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse AddBlack(Guid managerId, string name);

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="byManagerId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse AddBlack2(Guid managerId, Guid byManagerId);

        /// <summary>
        /// 获取我的好友列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [OperationContract]
        MyFriendsResponse GetMyFriends(Guid managerId, int pageIndex, int pageSize);
        
        /// <summary>
        /// 获取被邀请列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        FriendsAddInfoResponse GetFriendRequestList(Guid managerId);

        /// <summary>
        /// 忽略好友邀请
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse IgnoreAddFriend(Guid managerId, Guid friendId);

        /// <summary>
        /// 获取黑名单列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [OperationContract]
        MyBlacksResponse GetMyBlacks(Guid managerId, int pageIndex, int pageSize);

        /// <summary>
        /// 友谊赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        [OperationContract]
        MatchCreateResponse Fight(Guid managerId, Guid awayId);
        /// <summary>
        /// 获取友谊赛结果
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        FriendMatchResponse GetMatchResponse(Guid matchId);

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [OperationContract]
        MyFriendsResponse DeleteFriend(Guid managerId, int recordId, int pageIndex, int pageSize);
        /// <summary>
        /// 删除黑名单
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [OperationContract]
        MyBlacksResponse DeleteBlack(Guid managerId, int recordId, int pageIndex, int pageSize);

        /// <summary>
        /// 好友邀请可获得奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        PriendInvitePrizeListResponse GetFriendInvitePrizeList(string account, Guid managerId);

        /// <summary>
        /// 领取好友邀请奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <param name="inviteCount"></param>
        /// <returns></returns>
        [OperationContract]
        InvitePrizeResponse InvitePrize(string account, Guid managerId, int inviteCount);

        /// <summary>
        /// 获取好友邀请成长奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        InviteFriendGrowUpPrizeResponse GetInviteFriendGrowUpPrize(string account, Guid managerId);

        /// <summary>
        /// 领取好友成长奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        GrowUpPrizeResponse GrowUpPrize(string account, Guid managerId);
    }
}
