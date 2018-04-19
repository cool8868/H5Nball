using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Information;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Task;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Friend;
using Games.NBall.Entity.Response.Match;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Friend
{
    public class FriendCore
    {
        private readonly int _friendMaxCount;
        private readonly int _friendDayIntimacyMax;
        private readonly int _friendHelpIntimacyCount;
        private readonly int _friendMatchIntimacyCount;
        private readonly int _friendMatchWaitTime;
        private readonly int _friendDayMatchMax;
        #region .ctor
        public FriendCore(int p)
        {
            _friendMaxCount = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.FriendMaxCount);
            _friendDayIntimacyMax = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.FriendDayIntimacyMax);
            _friendHelpIntimacyCount = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.FriendHelpIntimacyCount);
            _friendMatchIntimacyCount = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.FriendMatchIntimacyCount);
            _friendMatchWaitTime = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.FriendMatchWaitTime);
            _friendDayMatchMax = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.FriendDayMatchMax);
        }
        #endregion

        #region Instance
        public static FriendCore Instance
        {
            get { return SingletonFactory<FriendCore>.SInstance; }
        }
        #endregion

        #region Facade

        public MessageCodeResponse AddFriend(Guid managerId, string name, bool hasTask)
        {
            var byManager = ManagerCore.Instance.GetManagerByName(name);
            if (byManager == null)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.FriendNotExistsName);
            if(managerId==byManager.Idx)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.FriendNotSelf);
            var manager = ManagerCore.Instance.GetManager(managerId);
            var friend = FriendManagerMgr.GetOne(managerId, byManager.Idx);
            if (friend != null)
            {
                if (friend.Status == 0)
                {
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.FriendHasExists);
                }
                if (friend.Status == 2)
                {
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.FriendIsByBlack);
                }
            }
            int returnCode = 0;
            FriendManagerMgr.AddFriend(managerId, byManager.Idx, _friendMaxCount, (int) MessageCode.FriendCountOver,
                                       (int) MessageCode.FriendHasExists, ref returnCode);
            if (returnCode == 1) //自己已在对方好友列表
            {
                returnCode = 0;
            }
            else
            {
                InformationHelper.SendAddFriendPop(byManager.Idx, manager.Name);
            }

            if (returnCode == 0)
            {
                var response = ResponseHelper.CreateSuccess<MessageCodeResponse>();
                response.Data = new MessageDataEntity();
                if (hasTask)
                {
                    //response.Data.PopMsg = TaskHandler.Instance.FriendAdd(managerId);
                }
                return response;
            }
            else
            {
                return ResponseHelper.Create<MessageCodeResponse>(returnCode);
            }
        }

        /// <summary>
        /// 获取被邀请好友列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public FriendsAddInfoResponse GetFriendRequestList(Guid managerId)
        {
            var response = ResponseHelper.Create<FriendsAddInfoResponse>(MessageCode.Success);
            response.Data = new FriendsAddInfo { FriendAddList = FriendManagerMgr.GetFriendAddList(managerId) };
            return response;
        }

        /// <summary>
        /// 是否有好友邀请
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public bool HasFriendRequest(Guid managerId)
        {
            var list = FriendManagerMgr.GetFriendAddList(managerId);
            if (list == null)
                return false;
            if (list.Count > 0)
                return true;
            return false;
        }


        /// <summary>
        /// 忽略添加好友 
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        public MessageCodeResponse IgnoreAddFriend(Guid managerId, Guid friendId)
        {
            int returnCode = 0;
            FriendManagerMgr.IgnoreAddFriend(managerId, friendId, ref returnCode);
            if (returnCode == -1)//已经是好友了
                returnCode = 0;
            return ResponseHelper.Create<MessageCodeResponse>(returnCode);
        }




        public MessageCodeResponse AddBlack(Guid managerId, string name)
        {
            var byManager = ManagerCore.Instance.GetManagerByName(name);
            return AddBlack(managerId, byManager);
        }

        public MessageCodeResponse AddBlack(Guid managerId, Guid byManagerId)
        {
            var byManager = ManagerCore.Instance.GetManager(byManagerId);
            return AddBlack(managerId, byManager);
        }

        public MyFriendsResponse DeleteFriend(Guid managerId, int recordId, int pageIndex, int pageSize)
        {
            var friend = FriendManagerMgr.GetById(recordId);
            if (friend == null || friend.ManagerId != managerId || friend.Status!=0)
                return ResponseHelper.InvalidParameter<MyFriendsResponse>();
            if (FriendManagerMgr.Delete(friend.Idx, friend.RowVersion))
            {
                int returnCode = 0;
                FriendManagerMgr.IgnoreAddFriend(managerId, friend.FriendId, ref returnCode);

                return GetMyFriends(managerId, pageIndex, pageSize);
            }
            else
            {
                return ResponseHelper.Create<MyFriendsResponse>(MessageCode.NbUpdateFail);
            }
        }

        public MyBlacksResponse DeleteBlack(Guid managerId, int recordId, int pageIndex, int pageSize)
        {
            var friend = FriendManagerMgr.GetById(recordId);
            if (friend == null || friend.ManagerId != managerId || friend.Status != 1)
                return ResponseHelper.InvalidParameter<MyBlacksResponse>();
            var byFriend = FriendManagerMgr.GetOne(friend.FriendId, managerId);
            if (byFriend != null && byFriend.Status == 1)
            {
                friend.Status = 2;
                FriendManagerMgr.Update(friend);
            }
            else
            {
                FriendManagerMgr.Delete(friend.Idx, friend.RowVersion);
                if (byFriend != null && byFriend.Status == 2)
                {
                    FriendManagerMgr.Delete(byFriend.Idx, byFriend.RowVersion);
                }
            }
            return GetMyBlacks(managerId, pageIndex, pageSize);
        }

        public MyFriendsResponse GetMyFriends(Guid managerId,int pageIndex,int pageSize)
        {
            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            var manager = ManagerCore.Instance.GetManager(managerId);
            int maxHelpCount = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel,
                                                                       EnumVipEffect.TrainHelpFriendCount);
            bool canHelp = managerExtra.HelpTrainCount < maxHelpCount;
            int totalCount = 0;
            var response = ResponseHelper.CreateSuccess<MyFriendsResponse>();
            response.Data = new MyFriendsData();
            response.Data.Friends= FriendManagerMgr.GetMyFriends(managerId,pageIndex,pageSize,ref totalCount);
            response.Data.TotalCount = totalCount;
            response.Data.TotalPage = ShareUtil.CalPageCount(totalCount, pageSize);
            response.Data.DayHelpTrainCount = maxHelpCount - managerExtra.HelpTrainCount;
            DateTime curDate = DateTime.Today;
            foreach (var entity in response.Data.Friends)
            {
                if (canHelp)
                {
                    CalMatchTimes(entity, curDate);
                }
                entity.IsTrain = PlayerTrain.Instance.GetIsHaveTrain(entity.FriendId);
            }
            return response;
        }

        public MyBlacksResponse GetMyBlacks(Guid managerId, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            var response = ResponseHelper.CreateSuccess<MyBlacksResponse>();
            response.Data = new MyBlacksData();
            response.Data.Blaks = FriendManagerMgr.GetMyBlacks(managerId, pageIndex, pageSize, ref totalCount);
            response.Data.TotalCount = totalCount;
            response.Data.TotalPage = ShareUtil.CalPageCount(totalCount, pageSize);
            return response;
        }

        public FriendManagerEntity GetFriendById(int recordId)
        {
            var entity = FriendManagerMgr.GetById(recordId);
            if (entity != null
                && entity.RecordDate != DateTime.Today)
            {
                entity.DayHelpTrainCount = 0;
                entity.DayIntimacy = 0;
                entity.DayMatchCount = 0;
               
                entity.RecordDate = DateTime.Today;
            }
            if (entity != null && DateTime.Now >= entity.OpenBoxTime.AddHours(24))
            {
                entity.DayOpenBoxCount = 0;
            }
            return entity;
        }

        public MatchCreateResponse Fight(Guid managerId, Guid awayId)
        {
            var lastTime = MemcachedFactory.FriendMutexClient.Get<DateTime>(managerId);
            if (lastTime > DateTime.Now)
            {
                return ResponseHelper.Create<MatchCreateResponse>(MessageCode.FriendMatchWait);
            }

            //好友比赛每天每个好友只能挑战3次， 只有第一次有奖励
            var friend = FriendManagerMgr.GetOne(managerId, awayId);
            if (friend.DayMatchCount >= 3)//每日三次比赛
                return ResponseHelper.Create<MatchCreateResponse>(MessageCode.FriendMatchOver);

            var matchId = ShareUtil.GenerateComb();
            var code = MatchCore.CreateMatchFriendAsyn(matchId,managerId,awayId, friend, MatchCallback);
            if(code!=MessageCode.Success)
                return ResponseHelper.Create<MatchCreateResponse>(code);
            MemcachedFactory.FriendMutexClient.Set(managerId, DateTime.Now.AddSeconds(_friendMatchWaitTime));

            return ResponseHelper.MatchCreateResponse(matchId);
        }

        #region MatchCallback

        public MessageCode MatchCallback(BaseMatchData matchData)
        {
            var fmatchData = (FriendMatchData)matchData;
            if (fmatchData == null || fmatchData.ErrorCode != (int)MessageCode.Success)
                return MessageCode.MatchCreateFail;
            var friendRecord = fmatchData.FriendRecord;
            bool isFriend = friendRecord != null;
            var coin = 0;
            int intimacy = 0;
            if (isFriend)
            {
                var oldIntimacy = friendRecord.Intimacy;
                AddFriendMatchIntimacy(friendRecord);
                intimacy = friendRecord.Intimacy - oldIntimacy;
                if (friendRecord.DayMatchCount == 1)
                {
                    //第一次比赛有奖励
                    if (fmatchData.Home.Score > fmatchData.Away.Score)
                        coin = 30;
                    else if (fmatchData.Home.Score == fmatchData.Away.Score)
                        coin = 20;
                    else
                        coin = 10;
                }
            }
            var match = new FriendMatchEntity();
            match.Idx = fmatchData.MatchId;
            match.HomeId = fmatchData.Home.ManagerId;
            match.HomeName = fmatchData.Home.Name;
            match.HomeScore = fmatchData.Home.Score;
            match.AwayId = fmatchData.Away.ManagerId;
            match.AwayName = fmatchData.Away.Name;
            match.AwayScore = fmatchData.Away.Score;
            match.Intimacy = intimacy;
            match.IsFriend = isFriend;
            match.RowTime = DateTime.Now;
            match.Status = 0;

            MatchCore.SaveMatchStat(match.HomeId, EnumMatchType.Friend, match.HomeScore, match.AwayScore, match.HomeScore); 
            var winType = EnumWinType.Win;
            if (match.HomeScore == match.AwayScore)
                winType = EnumWinType.Draw;
            else if (match.HomeScore < match.AwayScore)
                winType = EnumWinType.Lose;

            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                var trans = transactionManager.TransactionObject;
                var messageCode = MessageCode.NbUpdateFail;
                do
                {
                    if (isFriend)
                    {
                        if (!FriendManagerMgr.Update(friendRecord, trans))
                            break;
                    }
                    if (!FriendMatchMgr.Insert(match, trans))
                        break;
                    //记录成就相关数据
                   var mess = AchievementTaskCore.Instance.UpdateFriendMatchComb(match.HomeId, winType,trans);
                    if (mess != MessageCode.Success)
                        break;

                    if (coin > 0)
                    {
                        //友谊赛金币奖励
                        var manager = ManagerCore.Instance.GetManager(fmatchData.Home.ManagerId);
                        if (manager != null)
                        {
                            mess = ManagerCore.Instance.AddCoin(manager, coin, EnumCoinChargeSourceType.FriendMatch,
                                ShareUtil.GenerateComb().ToString(), trans);
                            if (mess != MessageCode.Success)
                                break;
                        }
                    }
                    messageCode = MessageCode.Success;
                } while (false); 
                if (messageCode == ShareUtil.SuccessCode)
                {
                    transactionManager.Commit();
                }
                else
                {
                    transactionManager.Rollback();
                }
            }
            return MessageCode.Success;
        }
        #endregion

        #region GetMatchResponse
        public FriendMatchResponse GetMatchResponse(Guid matchId)
        {
            var match = FriendMatchMgr.GetById(matchId);
            if (match == null)
                return ResponseHelper.InvalidParameter<FriendMatchResponse>();
            var response = ResponseHelper.CreateSuccess<FriendMatchResponse>();
            response.Data = match;
            return response;
        }
        #endregion

        #region AddIntimacy
        public void AddHelpTrainIntimacy(FriendManagerEntity entity)
        {
            AddIntimacy(entity,_friendHelpIntimacyCount);
            entity.DayHelpTrainCount++;
        }

        public void AddFriendMatchIntimacy(FriendManagerEntity entity)
        {
            AddIntimacy(entity,_friendMatchIntimacyCount);
            entity.DayMatchCount++;
        }
        #endregion

        /// <summary>
        /// 好友邀请可获得奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PriendInvitePrizeListResponse GetFriendInvitePrizeList(string account,Guid managerId)
        {
            PriendInvitePrizeListResponse response = new PriendInvitePrizeListResponse();
            try
            {
                response.Data = FriendInvitePrizeList(account, managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取好友邀请奖励集合", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        public PriendInvitePrizeList FriendInvitePrizeList(string account, Guid managerId)
        {
            PriendInvitePrizeList response = new PriendInvitePrizeList();
            response.ItemList = new List<ItemInfo>();
            //获取成功邀请人数
            var count = NbManagerextraMgr.GetInviteFriendCount(managerId);
            response.InviteCount = count;
            var result = CacheFactory.FriendCache.GetAllPrize();
            ConfigFriendinviteprizeEntity[] dic = new ConfigFriendinviteprizeEntity[result.Count];
            result.CopyTo(dic);
            if (dic != null && dic.Length != 0)
            {
                response.IsInviteAccomplish = false;
                //获取领取奖励记录
                var record = FriendinvitePrizerecordMgr.InvitePrizeIsGet(account);
                List<ConfigFriendinviteprizeEntity> list = new List<ConfigFriendinviteprizeEntity>();
               
                //移除领取过的奖励 
                foreach (var item in record)
                {
                    if (ConvertHelper.ConvertToInt(item.PrizeInfo) == 0)
                        continue;
                    foreach (var item1 in dic)
                    {
                        if (item1.SucceedCount == ConvertHelper.ConvertToInt(item.PrizeInfo))
                            continue;
                        list.Add(item1);
                    }
                    if (list.Count != 0)
                    {
                        dic = list.ToArray();
                        list = new List<ConfigFriendinviteprizeEntity>();
                    }
                }
                
                
                //留下最低挡的奖励
                int idx = dic.Min(r => r.SucceedCount);
                foreach (var item in dic)
                {
                    if (item.SucceedCount == idx)
                        list.Add(item);
                }
                response.NextInviteCount = list[0].SucceedCount - count < 0 ? 0 : list[0].SucceedCount - count;
                response.PrizeId = list[0].SucceedCount;
                foreach (var item in list)
                {
                    ItemInfo info = new ItemInfo();
                    info.Count = item.Count;
                    info.ItemCode = item.ItemCode;
                    info.PrizeType = item.PrizeType;
                    info.IsBinding = item.IsBinding;
                    response.ItemList.Add(info);
                }
            }
            else
                response.IsInviteAccomplish = true;
            return response;
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
            InvitePrizeResponse response = new InvitePrizeResponse();
            response.Data = new InvitePrize();
            response.Data.ItemList = new List<ItemInfo>();
            try
            {
                NbManagerEntity manager = null;

                var entity = FriendInvitePrizeList(account, managerId);
                if(entity.IsInviteAccomplish || entity.NextInviteCount > 0)
                    return ResponseHelper.Create<InvitePrizeResponse>((int)MessageCode.ActiveNotPrize);
                
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.FriendInvite);
                if (package == null)
                    return ResponseHelper.Create<InvitePrizeResponse>((int)MessageCode.NbNoPackage);
                MessageCode messCode = MessageCode.NbParameterError;
                FriendinvitePrizerecordEntity record = new FriendinvitePrizerecordEntity();
                record.Account = account;
                record.UpdateTime = DateTime.Now;
                record.PrizeType = 0;
                record.PrizeInfo = entity.PrizeId.ToString();
                int coin = 0;
                foreach (var item in entity.ItemList)
                {
                    switch (item.PrizeType)
                    {
                        case 1:
                            coin = item.Count;
                            record.PrizeString += "1," + item.Count+"|";
                            break;
                        case 2:
                            messCode = package.AddItems(item.ItemCode, item.Count, 1, item.IsBinding,false);
                            if(messCode != MessageCode.Success)
                                return ResponseHelper.Create<InvitePrizeResponse>((int)messCode);
                            record.PrizeString += item.ItemCode +"," + item.Count + "|";
                            break;
                        case 3:
                            var itemcode = CacheFactory.LotteryCache.LotteryByLib(item.ItemCode);
                            messCode = package.AddItems(itemcode, item.Count, 1, item.IsBinding,false);
                            if (messCode != MessageCode.Success)
                                return ResponseHelper.Create<InvitePrizeResponse>((int)messCode);
                            record.PrizeString += itemcode + "," + item.Count + "|";
                            break;
                    }
                    response.Data.ItemList.Add(item);
                }
                record.PrizeString = record.PrizeString.Substring(0, record.PrizeString.Length - 1);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    messCode = SaveInvitePrize(manager, record, package, transactionManager.TransactionObject, coin, account);
                    if (messCode != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<InvitePrizeResponse>((int) messCode);
                    }
                    transactionManager.Commit();
                    if (manager != null)
                        ManagerUtil.SaveManagerAfter(manager);
                    response.Data.BindPoint = PayCore.Instance.GetBindPoint(account);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("领取好友邀请奖励", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        public MessageCode SaveInvitePrize(NbManagerEntity manager, FriendinvitePrizerecordEntity record, ItemPackageFrame package, DbTransaction trans, int coin, string account)
        {
            if (manager != null)
            {
                if (!ManagerUtil.SaveManagerData(manager, null, trans))
                    return MessageCode.NbUpdateFailPackage;
            }
            if (coin > 0)
            {
                var mess = ManagerCore.Instance.AddCoin(manager,coin, EnumCoinChargeSourceType.FriendInvite, Guid.NewGuid().ToString(), trans);
                if (mess != MessageCode.Success)
                    return mess;
            }
            if(!FriendinvitePrizerecordMgr.Insert(record,trans))
                return MessageCode.NbUpdateFailPackage;
            if(!package.Save(trans))
                return MessageCode.NbUpdateFailPackage;
            package.Shadow.Save();
            return MessageCode.Success;
        }

        /// <summary>
        /// 获取好友邀请成长奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public InviteFriendGrowUpPrizeResponse GetInviteFriendGrowUpPrize(string account, Guid managerId)
        {
            InviteFriendGrowUpPrizeResponse response = new InviteFriendGrowUpPrizeResponse();
            response.Data = new InviteFriendGrowUpPrize();
            response.Data.ManagerList = new List<FriendManagerInfo>();
            try
            {
                var list = GetFrindInviteList(account);
                if (list == null)
                    return response;
                var mayPrize = list.Sum(r => r.MayPrize);
                response.Data.IsHavePrize = mayPrize > 0;
                response.Data.NotPoint = mayPrize;
                response.Data.AlreadyPoint = list.Sum(r => r.AlreadyPrize);

                foreach (var item in list)
                {
                    FriendManagerInfo info = new FriendManagerInfo();
                    info.HavePoint = item.AlreadyPrize;
                    info.Level = item.NLevel;
                    info.Name = item.Name;
                    response.Data.ManagerList.Add(info);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取好友邀请成长奖励", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 领取好友成长奖励
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GrowUpPrizeResponse GrowUpPrize(string account, Guid managerId)
        {
            GrowUpPrizeResponse response = new GrowUpPrizeResponse();
            response.Data = new GrowUpPrize();
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return ResponseHelper.Create<GrowUpPrizeResponse>((int) MessageCode.AdMissManager);
                var list = GetFrindInviteList(account);
                if (list == null)
                    return ResponseHelper.Create<GrowUpPrizeResponse>((int)MessageCode.ActiveNotPrize);
                FriendinvitePrizerecordEntity record = new FriendinvitePrizerecordEntity();
                int mayPrize = 0;
                foreach (var item in list)
                {
                    record.PrizeInfo += item.ByAccount+",";
                    mayPrize += item.MayPrize;
                }
                record.PrizeInfo = record.PrizeInfo.Substring(0, record.PrizeInfo.Length - 1);
                if(mayPrize <= 0)
                    return ResponseHelper.Create<GrowUpPrizeResponse>((int)MessageCode.ActiveNotPrize);
                record.Account = account;
                record.UpdateTime = DateTime.Now;
                record.PrizeType = 1;
                record.PrizeString = mayPrize.ToString();
               
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messCode = PayCore.Instance.AddBonus(managerId, mayPrize, EnumChargeSourceType.FriendInvite,
                        ShareUtil.CreateSequential().ToString(),transactionManager.TransactionObject);
                    if (messCode != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<GrowUpPrizeResponse>((int) messCode);
                    }
                    if (!FriendinviteMgr.SavePrize(account, transactionManager.TransactionObject))
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<GrowUpPrizeResponse>((int)MessageCode.NbParameterError);
                    }
                    if(!FriendinvitePrizerecordMgr.Insert(record,transactionManager.TransactionObject))
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<GrowUpPrizeResponse>((int)MessageCode.NbParameterError);
                    }
                    transactionManager.Commit();
                    response.Data.Point = mayPrize;
                    response.Data.SumPoint = PayCore.Instance.GetPoint(managerId);;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("领取好友邀请奖励", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        public List<FriendinviteEntity> GetFrindInviteList(string account)
        {
            var list = FriendinviteMgr.InviteManagerList(account);

            Dictionary<string, FriendinviteEntity> resultList = new Dictionary<string, FriendinviteEntity>();

            foreach (var item in list)
            {
                if (resultList.ContainsKey(item.ByAccount))
                {
                    if (resultList[item.ByAccount].NLevel > item.NLevel)
                        resultList[item.ByAccount] = item;
                }
                else
                    resultList.Add(item.ByAccount, item);
            }
            if (resultList.Count > 0)
                return resultList.Values.ToList();
            return null;
        }

        /// <summary>
        /// 统计可领取的点卷
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCode CalculatePoint(string account, Guid managerId)
        {
            var list = FriendinviteMgr.InviteManagerList(account);
            foreach (var item in list)
            {
                var charge = PayCore.Instance.GetPayUser(item.ByAccount);
                if (charge == null)
                    continue;
                try
                {
                    //总可得到点卷
                    var sumPoint = charge.ChargePoint*
                                   CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.FriendinvitePoint)/100;
                    //未领取点卷
                    item.MayPrize= sumPoint - item.AlreadyPrize < 0 ? 0 : sumPoint - item.AlreadyPrize;
                    FriendinviteMgr.Update(item);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("好友邀请计算点卷出错", ex);
                    return MessageCode.NbParameterError;
                }
            }
            return MessageCode.Success;
        }

        #endregion

        #region encapsulation
        void AddIntimacy(FriendManagerEntity entity, int intimacy)
        {
            if (entity.DayIntimacy < _friendDayIntimacyMax)
            {
                entity.Intimacy += intimacy;
                entity.DayIntimacy += intimacy;
            }
        }

        bool CheckByHelp(MyFriendsEntity entity,DateTime curDate)
        {
            if (entity.RecordDate != curDate)
                return true;
            int maxByHelpCount = CacheFactory.VipdicCache.GetEffectValue(entity.VipLevel,
                                                                       EnumVipEffect.TrainHelpAcceptCount);
            return entity.ByHelpTrainCount < maxByHelpCount;
        }

        void CalMatchTimes(MyFriendsEntity entity, DateTime curDate)
        {
            if (entity.FRecordDate != curDate)
            {
                entity.DayMatchCount = 0;
                entity.ByHelpTrainCount = 0;
            }

            if (entity.RecordDate != curDate)
            {
                entity.DayCanMatchCount = _friendDayMatchMax;
                entity.ByHelpTrainCount = 0;
            }
            else if (entity.DayMatchCount < _friendDayMatchMax)
            {
                entity.DayCanMatchCount = _friendDayMatchMax - entity.DayMatchCount;
            }
            else
            {
                entity.DayCanMatchCount = 0;
            }
            entity.RecordDate = curDate;
            entity.FRecordDate = curDate;
        }

        MessageCodeResponse AddBlack(Guid managerId, NbManagerEntity byManager)
        {
            if (byManager == null)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.FriendNotExistsName);
            }
            if (managerId == byManager.Idx)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.FriendNotSelf);
            var friend = FriendManagerMgr.GetOne(managerId, byManager.Idx);
            if (friend != null && friend.Status == 1)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.FriendBlackExists);
            }
            FriendManagerMgr.AddBlack(managerId, byManager.Idx);
            return ResponseHelper.CreateSuccess<MessageCodeResponse>();
        }
        #endregion

    }
}
