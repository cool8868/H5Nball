using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.League;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.League;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core
{
    /// <summary>
    /// 联赛逻辑处理
    /// </summary>
    public class LeagueCore
    {
        #region 初始化

        public LeagueCore(int p)
        {
           
        }

        #endregion

        #region 单例

        public static LeagueCore Instance
        {
            get { return SingletonFactory<LeagueCore>.SInstance; }
        }

        #endregion

        #region 联赛积分商城

        //获取联赛积分商城数据
        public LaegueManagerinfoResponse GetLeagueMallInfo(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<LaegueManagerinfoResponse>();

            var leagueManager = LaegueManagerinfoMgr.GetById(managerId);
            if (leagueManager == null)
                return ResponseHelper.InvalidParameter<LaegueManagerinfoResponse>();

            if (string.IsNullOrEmpty(leagueManager.ExchangeIds) || CheckExchangeRefresh(leagueManager.RefreshDate))
            {
                var equipmentProperties = "";
                var equipmentItemcode = "";
                bool isReplace = false;
                string codeString = "";
                if (!string.IsNullOrEmpty(leagueManager.ExchangeIds) && leagueManager.RefreshDate.Month != DateTime.Now.Month)
                {
                    isReplace = true;
                    var itemList = leagueManager.ExchangeIds.Split('|');
                    foreach (var item in itemList)
                    {
                        var itemcode = item.Split(',')[1];
                        if (itemcode.IndexOf("39") == 0)
                            codeString += item + "|";
                    }
                    if (codeString.Length > 0)
                        codeString = codeString.Substring(0, codeString.Length - 1);
                }
                leagueManager.ExchangeIds = CacheFactory.LeagueCache.GetExchanges(manager.Level >= 60, out equipmentItemcode, out equipmentProperties, isReplace, codeString);
                leagueManager.RefreshDate = DateTime.Now;
                leagueManager.RefreshTimes = 0;
                leagueManager.ExchangedIds = "";
                leagueManager.EquipmentProperties = equipmentProperties;
                leagueManager.EquipmentItems = equipmentItemcode;
                LaegueManagerinfoMgr.Update(leagueManager);
            }

            var response = ResponseHelper.CreateSuccess<LaegueManagerinfoResponse>();
            response.Data = leagueManager;
            response.Data.RefreshPoint =
               CacheFactory.MallCache.GetDirectPoint(EnumConsumeSourceType.RefreshLeagueExchange, leagueManager.RefreshTimes + 1);
            response.Data.AllEquipmentProperties =
                CacheFactory.LeagueCache.AnalysisProperties(leagueManager.EquipmentProperties);
            DateTime date = DateTime.Now;
            if (DateTime.Now.Hour >= 21)
                date = DateTime.Today.AddDays(1).AddHours(21);//每天21点刷新
            else
                date = DateTime.Today.AddHours(21);
            response.Data.NextRefreshTick = ShareUtil.GetTimeTick(date);
            return response;
        }

        bool CheckExchangeRefresh(DateTime refreshDate)
        {
            if (refreshDate.AddDays(1) < DateTime.Today.AddHours(21))
                return true;

            if (DateTime.Now >= DateTime.Today.AddHours(21) && refreshDate < DateTime.Today.AddHours(21))
                return true;
            return false;
        }


        public LaegueRefreshExchangeResponse RefreshExchange(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<LaegueRefreshExchangeResponse>();

            var entity = LaegueManagerinfoMgr.GetById(managerId);
            entity.RefreshTimes++;
            var mallDirect = new MallDirectFrame(managerId, EnumConsumeSourceType.RefreshLeagueExchange, entity.RefreshTimes);
            var checkCode = mallDirect.Check();
            if (checkCode != MessageCode.Success)
                return ResponseHelper.Create<LaegueRefreshExchangeResponse>(checkCode);
            var equipmentProperties = "";
            var equipmentItemcode = "";
            string codeString = "";

            var leagueManager = LaegueManagerinfoMgr.GetById(managerId);
            if (leagueManager == null)
                return ResponseHelper.InvalidParameter<LaegueRefreshExchangeResponse>();

            if (!string.IsNullOrEmpty(leagueManager.ExchangeIds))
            {
                var itemList = leagueManager.ExchangeIds.Split('|');
                foreach (var item in itemList)
                {
                    var itemcode = item.Split(',')[1];
                    if (itemcode.IndexOf("39") == 0)
                        codeString += item + "|";
                }
                if (codeString.Length > 0)
                    codeString = codeString.Substring(0, codeString.Length - 1);
            }

            entity.ExchangeIds = CacheFactory.LeagueCache.GetExchanges(manager.Level >= 60, out equipmentItemcode, out equipmentProperties, true, codeString);
            entity.ExchangedIds = "";
            entity.EquipmentProperties = equipmentProperties;
            entity.EquipmentItems = equipmentItemcode;
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                checkCode = mallDirect.Save(Guid.NewGuid().ToString(), transactionManager.TransactionObject);
                if (checkCode != MessageCode.Success)
                {
                    transactionManager.Rollback();
                    return ResponseHelper.Create<LaegueRefreshExchangeResponse>(checkCode);
                }
                if (!LaegueManagerinfoMgr.Update(entity, transactionManager.TransactionObject))
                {
                    transactionManager.Rollback();
                    return ResponseHelper.Create<LaegueRefreshExchangeResponse>(MessageCode.NbUpdateFail);
                }
                transactionManager.Commit();
            }
            var response = ResponseHelper.CreateSuccess<LaegueRefreshExchangeResponse>();
            DateTime date = DateTime.Now;
            if (DateTime.Now.Hour >= 21)
                date = DateTime.Today.AddDays(1).AddHours(21);//每天21点刷新
            else
                date = DateTime.Today.AddHours(21);
            response.Data = new LeagueRefreshExchangeEntity
            {
                ExchangeIds = entity.ExchangeIds,
                ManagerPoint = mallDirect.RemainPoint,
                SumScore=entity.SumScore,
                RefreshPoint =
                    CacheFactory.MallCache.GetDirectPoint(EnumConsumeSourceType.RefreshLeagueExchange,
                        entity.RefreshTimes + 1),
                AllEquipmentProperties = CacheFactory.LeagueCache.AnalysisProperties(entity.EquipmentProperties),
                NextRefreshTick = ShareUtil.GetTimeTick(date)
            };
            return response;
        }

        public LeagueExchangeResponse Exchange(Guid managerId, string exchangeKey)
        {
            var manager = LaegueManagerinfoMgr.GetById(managerId);
            if (manager == null || string.IsNullOrEmpty(manager.ExchangeIds)
                || !manager.ExchangeIds.Contains(exchangeKey))
                return ResponseHelper.InvalidParameter<LeagueExchangeResponse>();
            var exchangeCache = CacheFactory.LeagueCache.GetExchangeEntity(exchangeKey);
            if (exchangeCache == null)
                return ResponseHelper.InvalidParameter<LeagueExchangeResponse>();

            if (manager.ExchangedIds.Contains(exchangeKey))
                return ResponseHelper.Create<LeagueExchangeResponse>(MessageCode.LeagueExchangeTimesOver);

            if (manager.SumScore < exchangeCache.CostScore)
                return ResponseHelper.Create<LeagueExchangeResponse>(MessageCode.LeagueExchangeHonorShortage);
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.LeagueExchange);

            var itemcode = exchangeCache.ItemCode;
            var exchanagerItemCode = Convert.ToInt32(exchangeKey.Split(',')[1]);
            if (exchangeCache.ItemCode != exchanagerItemCode)
                itemcode = exchanagerItemCode;

            var itemInfo = CacheFactory.ItemsdicCache.GetItem(itemcode);
            var code = MessageCode.Success;
            if (itemInfo.ItemType == (int) EnumItemType.Equipment)
            {
                var allEquipmentProperties = CacheFactory.LeagueCache.AnalysisProperties(manager.EquipmentProperties);
                var property = GetEquipmentProperty(manager.EquipmentItems, allEquipmentProperties, itemcode);
                code = package.AddEquipment(itemcode, true,false, property);
            }
            else
            {
                code = package.AddItem(itemcode, true,false);
            }
            if (code != MessageCode.Success)
                return ResponseHelper.Create<LeagueExchangeResponse>(code);
            manager.SumScore = manager.SumScore - exchangeCache.CostScore;
            manager.UpdateTime = DateTime.Now;
            manager.ExchangedIds = manager.ExchangedIds + "|" + exchangeKey;

            var record = new LeagueExchangerecordEntity()
            {
                CostScore = exchangeCache.CostScore,
                ItemCode = itemcode,
                ManagerId = managerId,
                RowTime = DateTime.Now
            };
            code = SaveExchange(manager, package, record);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<LeagueExchangeResponse>(code);
            else
            {
                package.Shadow.Save();
                var response = ResponseHelper.CreateSuccess<LeagueExchangeResponse>();
                response.Data = new LeagueExchangeEntity { CurScore = manager.SumScore, ItemCode = itemcode };
                return response;
            }
        }
       

        EquipmentProperty GetEquipmentProperty(string itemcodes, List<EquipmentProperty> allEquipmentProperties,int curItemcode)
        {
            var itemList = itemcodes.Split('|');
            if (itemList.Length > 0)
            {
                if (itemList.Length != allEquipmentProperties.Count)
                    return null;

                for (int i = 0; i < itemList.Length; i++)
                {
                    var itemcode = Convert.ToInt32(itemList[i]);
                    if (itemcode == curItemcode)
                    {
                        return allEquipmentProperties[i];
                    }
                }
            }
            return null;
        }


        MessageCode SaveExchange(LaegueManagerinfoEntity leagueManager, ItemPackageFrame package, LeagueExchangerecordEntity leagueExchangerecord)
        {
            if (leagueManager == null || package == null || leagueExchangerecord == null)
            {
                return MessageCode.NbUpdateFail;
            }
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveExchange(transactionManager.TransactionObject, leagueManager, package, leagueExchangerecord);

                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return messageCode;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveExchange", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_SaveExchange(DbTransaction transaction, LaegueManagerinfoEntity leagueManager, ItemPackageFrame package, LeagueExchangerecordEntity leagueExchangerecord)
        {
            if (!LaegueManagerinfoMgr.Update(leagueManager, transaction))
                return MessageCode.NbUpdateFail;
            if (!package.Save(transaction))
                return MessageCode.NbUpdateFail;
            if (!LeagueExchangerecordMgr.Insert(leagueExchangerecord, transaction))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }


        #endregion

        #region 获取信息

        /// <summary>
        /// 获取所有联赛关卡锁住情况
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetLeagueLockResponse GetLeagueLock(Guid managerId)
        {
            try
            {
                var list = LeagueManagerrecordMgr.GetManagerAllMark(managerId);
                if (list == null)
                {
                    SystemlogMgr.Error("获取联赛信息", "经理联赛记录未找到ManagerId：" + managerId);
                    return ResponseHelper.InvalidParameter<GetLeagueLockResponse>();
                }
                var response = new GetLeagueLockResponse();
                response.Data = new LeagueLockEntity
                {
                    List = new List<bool>()
                };
                foreach (var item in list)
                {
                    response.Data.List.Add(item.IsLock);
                }
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("联赛获取所有联赛关卡锁住情况", ex);
                return ResponseHelper.InvalidParameter<GetLeagueLockResponse>();
            }
        }

        /// <summary>
        /// 获取联赛情况
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public GetLeagueInfoResponse GetLeagueInfoResponse(Guid managerId, int leagueId)
        {
            var response = new GetLeagueInfoResponse();
            response.Data = new LeagueInfoEntity();
            List<LeagueManagerrecordEntity> recordList = null;
            var lockList = new List<bool>();
            if (leagueId == 0)
            {
                recordList = LeagueManagerrecordMgr.GetManagerAllMark(managerId);
                foreach (var record in recordList)
                {
                    if (record.IsStart)
                    {
                        leagueId = record.LaegueId;
                        response.Data.IsHaveStartLeague = true;
                        break;
                    }
                    lockList.Add(record.IsLock);
                }
                if (!response.Data.IsHaveStartLeague)
                {
                    response.Data.IsHaveStartLeague = false;
                    response.Data.LockList = lockList;
                    return response;
                }
            }
            return GetLeagueInfo(managerId, leagueId, null);
        }

        /// <summary>
        /// 获取联赛情况
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <param name="currectLeague"></param>
        /// <returns></returns>
        public GetLeagueInfoResponse GetLeagueInfo(Guid managerId, int leagueId, LeagueManagerrecordEntity currectLeague)
        {
            try
            {
                if (currectLeague == null)
                    currectLeague = LeagueManagerrecordMgr.GetManagerMarkInfo(managerId, leagueId);
                if (currectLeague == null)
                {
                    SystemlogMgr.Error("获取联赛信息", "经理联赛记录未找到ManagerId：" + managerId);
                    return ResponseHelper.InvalidParameter<GetLeagueInfoResponse>();
                }
                var leagueRecord = LeagueRecordMgr.GetById(currectLeague.LeagueRecordId);
                //获取胜场
                var leagueWincountRecord = LeagueWincountrecordMgr.GetRecord(managerId, currectLeague.LaegueId);
                var winConfig = CacheFactory.LeagueCache.GetLeagueStar(leagueId);
                var winList = new List<LeagueWinCountInfo>();
                var prizeStatus = leagueWincountRecord.PrizeStep.Split(',');
                foreach (var item in winConfig)
                {
                    LeagueWinCountInfo entity = new LeagueWinCountInfo();
                    entity.PrizeLevel = item.PrizeLevel;
                    if (prizeStatus.Length >= item.PrizeLevel)
                        entity.PrizeStatus = ConvertHelper.ConvertToInt(prizeStatus[item.PrizeLevel - 1]);
                    winList.Add(entity);
                }
                //获取排名
                var leagueFightMap = new LeagueFightMapFrame(managerId);
                int myRank = 0;
                int myScore = 0;
                var rankList = leagueFightMap.GetRank(ref myRank,ref myScore);
                var response = ResponseHelper.CreateSuccess<GetLeagueInfoResponse>();
                response.Data = new LeagueInfoEntity();
                if (leagueRecord.Schedule > currectLeague.MaxWheelNumber)
                {
                    response.Data.IsHaveReturnMain = true;
                    leagueRecord.Schedule = currectLeague.MaxWheelNumber;
                }
                response.Data.LeagueInfo = currectLeague;
                response.Data.MyWinCount = leagueWincountRecord.MaxWinCount;
                response.Data.LeagueRecord = leagueRecord;
                response.Data.LeagueWincountRecord = winList;
                response.Data.MyRank = myRank;
                response.Data.RankList = rankList;
                response.Data.IsHaveStartLeague = true;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("联赛-获取联赛信息", ex);
                return ResponseHelper.InvalidParameter<GetLeagueInfoResponse>();
            }
        }

        #endregion
        
        #region 开启联赛
        /// <summary>
        /// 开启一个联赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public GetLeagueInfoResponse StarLeague(Guid managerId, int leagueId)
        {
            LeagueManagerrecordEntity leagueManagerRecord = null;
            var code = LeagueProcess.Instance.StartLeague(managerId, leagueId, ref leagueManagerRecord);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<GetLeagueInfoResponse>(code);
            return GetLeagueInfo(managerId, leagueId,leagueManagerRecord);
        }

        #endregion

        #region 重置联赛

        /// <summary>
        /// 重置联赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public MessageCodeResponse ResetLeague(Guid managerId, int leagueId)
        {
            var currectLeague = LeagueManagerrecordMgr.GetManagerMarkInfo(managerId, leagueId);
            if (currectLeague == null)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);

            var leagueWincountRecord = LeagueWincountrecordMgr.GetRecord(managerId, leagueId);
            if (leagueWincountRecord != null)
            {
                //获取所有奖励
                var winPrize = CacheFactory.LeagueCache.GetLeagueStar(leagueId);
                if (winPrize == null || winPrize.Count <= 0)
                    return ResponseHelper.InvalidParameter<MessageCodeResponse>();
                var prizeStatus = leagueWincountRecord.PrizeStep.Split(',');
                foreach (var item in prizeStatus)
                {
                    if(item == "1")
                        return ResponseHelper.Create<MessageCodeResponse>(MessageCode.LeaguePrizeNotGet);
                }
            }
            int price = 50;
            if (currectLeague.IsPass)
                price = 0;
            int point = PayCore.Instance.GetPoint(managerId);
            if (point < price) //重置联赛消耗点券数
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbPointShortage);
            var code = LeagueProcess.Instance.ResetLeague(managerId, leagueId, price);
            return ResponseHelper.Create<MessageCodeResponse>(code);
        }

        #endregion

        #region 打比赛
        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        public LeagueFightResultResponse FightMatch(Guid managerId, int leagueId)
        {
            var response = LeagueProcess.Instance.Fight(managerId, leagueId);
            return response;
        }

       
        #endregion

        #region 获取胜场奖励
        public LeagueWincountrecordResponse GetWincountPrizeInfo(Guid managerId, int leagueId)
        {
            var leagueWincountRecord = LeagueWincountrecordMgr.GetRecord(managerId, leagueId);
            if (leagueWincountRecord == null)
                return ResponseHelper.InvalidParameter<LeagueWincountrecordResponse>();
            var response = ResponseHelper.CreateSuccess<LeagueWincountrecordResponse>();
            response.Data = leagueWincountRecord;
            return response;
        }

        #endregion

        #region 领取胜场奖励

        public LeaguePrizeResponse GetWincountPrize(Guid managerId, int leagueId, int countType)
        {
            var response = LeagueProcess.Instance.GetWincountPrize(managerId, leagueId,countType);
            return response;
        }

        #endregion

        #region 获取冠军奖励列表
        /// <summary>
        /// 获取冠军奖励列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LeagueAllPrizeInfoResponse GetAllPrizeInfo(Guid managerId)
        {
            var response = LeagueProcess.Instance.GetAllPrizeInfo(managerId);
            return response;
        }

        #endregion

        #region 领取冠军奖励
        /// <summary>
        /// 领取冠军奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueRecordId"></param>
        public LeaguePrizeResponse GetRankPrize(Guid managerId, Guid leagueRecordId)
        {
            var response = LeagueProcess.Instance.GetRankPrize(managerId, leagueRecordId);
            return response;
        }
        #endregion

        #region 获取排名信息
        /// <summary>
        /// 获取排名信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public LeagueRankListResponse GetRank(Guid managerId, int leagueId)
        {
            try
            {
                var response = ResponseHelper.CreateSuccess<LeagueRankListResponse>();
                var leagueRecord = LeagueManagerrecordMgr.GetManagerMarkInfo(managerId, leagueId);
                if (leagueRecord == null)
                {
                    SystemlogMgr.Error("获取联赛信息", "经理联赛记录未找到ManagerId：" + managerId);
                    return ResponseHelper.InvalidParameter<LeagueRankListResponse>();
                }
                if (!leagueRecord.IsStart)
                    return ResponseHelper.Create<LeagueRankListResponse>(MessageCode.LeagueNotStart);
                //获取排名
                var leagueFightMap = new LeagueFightMapFrame(managerId);
                int myRank = 0;
                int myScore = 0;
                var rankList = leagueFightMap.GetRank(ref myRank, ref myScore);
                response.Data = new LeagueRank();
                response.Data.RankList = rankList;
                response.Data.MyRank = myRank;
                response.Data.MyScore = myScore;

                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取排名信息", ex);
                return ResponseHelper.Create<LeagueRankListResponse>(MessageCode.NbParameterError);
            }
        }

        #endregion

        /// <summary>
        /// 获取联赛某一轮对阵记录
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public LeagueGetFightMapRecordResponse GetLeagueFigMap(Guid managerId, int leagueId,int round)
        {
            LeagueGetFightMapRecordResponse response = new LeagueGetFightMapRecordResponse();
            response.Data = new LeagueGetFightMapRecord();
            try
            {
               var leagueRecord = LeagueManagerrecordMgr.GetManagerMarkInfo(managerId, leagueId);
               if (leagueRecord == null)
                {
                    SystemlogMgr.Error("获取联赛信息", "经理联赛记录未找到ManagerId：" + managerId);
                    return ResponseHelper.InvalidParameter<LeagueGetFightMapRecordResponse>();
                }
                if (!leagueRecord.IsStart)
                    return ResponseHelper.Create<LeagueGetFightMapRecordResponse>(MessageCode.LeagueNotStart);
                //获取对阵
                var leagueFightMap = new LeagueFightMapFrame(managerId);
                var fightMap = leagueFightMap.GetFightMap(round);
                response.Data.FightRecord = fightMap;
                response.Data.Round = round;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取联赛对阵记录", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }
    }
}
