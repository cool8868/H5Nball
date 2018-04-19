using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Information;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Task;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Friend;
using Games.NBall.Entity.Response.Match;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Turntable
{
    public class OlympicCore
    {
        #region .ctor
        /// <summary>
        /// 活动开始时间
        /// </summary>
        private DateTime startTime;
        /// <summary>
        /// 活动结束时间
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// 奥运金牌获得概率
        /// </summary>
        private Dictionary<int, List<ConfigOlympicthegoldmedalEntity>> _theGoldMedalRate;
        /// <summary>
        /// 兑换奖励配置
        /// </summary>
        private Dictionary<int, List<ConfigOlympicexchangerizeEntity>> _exchangeDic;

        public OlympicCore(int p)
        {
            startTime = DateTime.Now;
            endTime = DateTime.Now;
            var activityTime = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.OlympicActivityTime);
            if (activityTime.Length > 0)
            {
                var timeList = activityTime.Split('|');
                var start = timeList[0].Split(',');
                var startYear = ConvertHelper.ConvertToInt(start[0]);
                var startMonth = ConvertHelper.ConvertToInt(start[1]);
                var startDay = ConvertHelper.ConvertToInt(start[2]);
                startTime = new DateTime(startYear, startMonth, startDay, 0, 0, 0);

                var end = timeList[1].Split(',');
                var endYear = ConvertHelper.ConvertToInt(end[0]);
                var endMonth = ConvertHelper.ConvertToInt(end[1]);
                var endDay = ConvertHelper.ConvertToInt(end[2]);
                endTime = new DateTime(endYear, endMonth, endDay, 23, 59, 59);
            }
            _exchangeDic = new Dictionary<int, List<ConfigOlympicexchangerizeEntity>>();
            _theGoldMedalRate = new Dictionary<int, List<ConfigOlympicthegoldmedalEntity>>();
            if (IsActivity)
            {
                var allRate = ConfigOlympicthegoldmedalMgr.GetAll();
                foreach (var item in allRate)
                {
                    if (!_theGoldMedalRate.ContainsKey(item.GetType))
                        _theGoldMedalRate.Add(item.GetType, new List<ConfigOlympicthegoldmedalEntity>());
                    _theGoldMedalRate[item.GetType].Add(item);
                }
                var allExchange = ConfigOlympicexchangerizeMgr.GetAll();
                foreach (var item in allExchange)
                {
                    if (!_exchangeDic.ContainsKey(item.ExchangeId))
                        _exchangeDic.Add(item.ExchangeId, new List<ConfigOlympicexchangerizeEntity>());
                    _exchangeDic[item.ExchangeId].Add(item);
                }
            }
        }

        #endregion

        #region Instance
        public static OlympicCore Instance
        {
            get { return SingletonFactory<OlympicCore>.SInstance; }
        }
        #endregion

        /// <summary>
        /// 是否有活动
        /// </summary>
        public bool IsActivity {
            get
            {
                DateTime date = DateTime.Now;
                if (date >= startTime && date <= endTime)
                    return true;
                return false;
            }
        }

        #region 逻辑相关

        /// <summary>
        /// 获取奥运金牌数量
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public OlympicGetInfoResponse GetManagerInfo(Guid managerId)
        {
            OlympicGetInfoResponse response = new OlympicGetInfoResponse();
            response.Data = new OlympicGetInfo();
            try
            {
                var info = GetInfo(managerId);
                response.Data.TheGoldMedalDic = info.TheGoldMedalDic;
                response.Data.StartTimeTick = ShareUtil.GetTimeTick(startTime);
                response.Data.EndTimeTick = ShareUtil.GetTimeTick(endTime);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取奥运金牌", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 兑换奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exChangerizeType"></param>
        /// <returns></returns>
        public OlympicExchangeResponse Exchange(Guid managerId, int exChangerizeType)
        {
            OlympicExchangeResponse response = new OlympicExchangeResponse();
            response.Data = new OlympicExchange();
            try
            {
                var info = GetInfo(managerId);
                var configList = GetExChangerize(exChangerizeType);
                if (configList.Count == 0)
                    return ResponseHelper.Create<OlympicExchangeResponse>((int) MessageCode.NbParameterError);
                foreach (var item in configList)
                {
                    if (!info.DeductTheGoldMedal(item.TheGoldMedalId, item.TheGoldMedalCount))
                        return ResponseHelper.Create<OlympicExchangeResponse>((int)MessageCode.OlympicTheGoldMedalCountNot);
                }
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.OlympocExChangerize);
                if (package == null || package.BlankCount <= 0)
                    return ResponseHelper.Create<OlympicExchangeResponse>((int) MessageCode.ItemPackageFull);
                var messageCode = package.AddItem(configList[0].PrizeItemCode);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<OlympicExchangeResponse>((int) messageCode);
                OlympicRecordEntity record = new OlympicRecordEntity(0, managerId, exChangerizeType,configList[0].PrizeItemCode, DateTime.Now);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Main)))
                {
                    transactionManager.BeginTransaction();
                    messageCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (!OlympicManagerMgr.Update(info, transactionManager.TransactionObject))
                            break;
                        if (!OlympicRecordMgr.Insert(record, transactionManager.TransactionObject))
                            break;
                        messageCode = MessageCode.Success;
                    } while (false); 
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        package.Shadow.Save();
                        response.Data.ItemCode = configList[0].PrizeItemCode;
                        response.Data.TheGoldMedalDic = info.TheGoldMedalDic;
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("奥运会兑换奖励", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 增加金牌
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="theGoldMedalId"></param>
        /// <param name="count"></param>
        public bool AddTheGoldMedal(Guid managerId,int theGoldMedalId, int count)
        {
            var info = GetInfo(managerId);
            if (!info.AddTheGoldMedal(theGoldMedalId, count))
                return false;
            OlympicManagerMgr.Update(info);
            return true;
        }

        /// <summary>
        /// 获取金牌信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public OlympicManagerEntity GetInfo(Guid managerId)
        {
            var info = OlympicManagerMgr.GetById(managerId);
            if (info == null)
            {
                info = new OlympicManagerEntity(managerId);
                OlympicManagerMgr.Insert(info);
            }
            return info;
        }

        #endregion
       

        #region 缓存相关

        /// <summary>
        ///随机得到一个奥运金牌
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="getType"></param>
        /// <returns></returns>
        public int GetOlympicTheGoldMedal(Guid managerId, EnumOlympicGeyType getType)
        {
            try
            {
                if (!IsActivity)
                    return 0;
                if (!_theGoldMedalRate.ContainsKey((int) getType))
                    return 0;
                foreach (var item in _theGoldMedalRate[(int) getType])
                {
                    if (RandomHelper.CheckPercentage(item.Rate))
                    {
                        if (AddTheGoldMedal(managerId, item.TheGoldMedalId, 1))
                            return item.TheGoldMedalId;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("奥运金牌", ex);
            }
            return 0;
        }

        /// <summary>
        /// 获取兑换奖励配置
        /// </summary>
        /// <param name="exchangeType"></param>
        /// <returns></returns>
        public List<ConfigOlympicexchangerizeEntity> GetExChangerize(int exchangeType)
        {
            if (_exchangeDic.ContainsKey(exchangeType))
                return _exchangeDic[exchangeType];
            return new List<ConfigOlympicexchangerizeEntity>();
        }

        #endregion
    }
}
