using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Dpm.Core.Activity;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Arena;
using Games.NBall.Core.Friend;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.SkillCard;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Information;
using Games.NBall.Entity.Response.SkillCard;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core
{
    public class ArenaCore
    {
        /// <summary>
        /// 跨服字典
        /// </summary>
        private Dictionary<EnumArenaDomainType, ArenaThread> _threadDic;

        public ArenaCore(int p)
        {
            _threadDic = new Dictionary<EnumArenaDomainType, ArenaThread>();
            var domainDic = CacheFactory.ArenaCache.GetDomainDic();
            foreach (var item in domainDic)
            {
                if (item.Value.Count == 0)
                    continue;
                _threadDic.Add(item.Key, new ArenaThread((int)item.Key));
            }
        }

        #region Instance

        public static ArenaCore Instance
        {
            get { return SingletonFactory<ArenaCore>.SInstance; }
        }


        #endregion

        /// <summary>
        /// 每天刷新
        /// </summary>
        /// <returns></returns>
        public MessageCode DayRefresh()
        {
            MessageCode messageResult = MessageCode.Success;
            foreach (var item in _threadDic)
            {
               var messageCode = item.Value.InitSeason();
                if (messageCode != MessageCode.Success)
                    messageResult = messageCode;
            }
            return messageResult;
        }

        /// <summary>
        /// 发奖
        /// </summary>
        /// <returns></returns>
        public MessageCode SendPrize()
        {
            //获取1000条未发奖的记录
            var prizeList = ArenaManagerrecordMgr.GetNotPrize();
            foreach (var item in prizeList)
            {
                if (item.IsPrize || item.PrizeId > 0)
                    continue;
                var prize = CacheFactory.ArenaCache.GetPrize(item.Rank);
                if (prize == null || prize.Count == 0)
                    continue;
                int arenaCoin = 0;
                var mail = new MailBuilder(item.ManagerId, EnumMailType.Arena, item.ArenaType, item.Rank, prize,
                    ref arenaCoin);

                item.IsPrize = true;
                item.PrizeId = item.Rank;
                item.PrizeTime = DateTime.Now;
                var messageCode = MessageCode.NbUpdateFail;
                using (
                    var transactionManager =
                        new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Support)))
                {
                    transactionManager.BeginTransaction();
                    do
                    {
                        if (!ArenaManagerinfoMgr.AddArenaCoin(item.ManagerId, arenaCoin,
                            transactionManager.TransactionObject))
                            break;
                        if (!ArenaManagerrecordMgr.Update(item, transactionManager.TransactionObject))
                            break;
                        messageCode = MessageCode.Success;
                    } while (false);
                    if (messageCode == MessageCode.Success)
                        transactionManager.Commit();
                    else
                    {
                        transactionManager.Rollback();
                        continue;
                    }
                }
                if (messageCode == MessageCode.Success)
                    mail.Save(item.SiteId);
            }
            
            //刷新对手和排名
            MessageCode messageResult = MessageCode.Success;
            foreach (var item in _threadDic)
            {
                var messageCode = item.Value.Refresh();
                if (messageCode != MessageCode.Success)
                    messageResult = messageCode;
            }
            return messageResult;
        }

        #region 获取竞技场信息

        /// <summary>
        /// 获取竞技场信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaGetInfoResponse GetArenaResponse(Guid managerId,string zoneName)
        {
            try
            {
                var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
                if (domainId == null || !_threadDic.ContainsKey((EnumArenaDomainType)domainId))
                    return ResponseHelper.Create<ArenaGetInfoResponse>(MessageCode.NbParameterError);
                return _threadDic[(EnumArenaDomainType)domainId].GetArenaResponse(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取竞技场信息", ex);
                return ResponseHelper.Create<ArenaGetInfoResponse>(MessageCode.NbParameterError);
            }
        }

        #endregion

        #region 对手

        /// <summary>
        /// 获取对手
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaGetOpponentResponse GetOpponent(Guid managerId,string zoneName)
        {
            try
            {
                var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
                if (domainId == null || !_threadDic.ContainsKey((EnumArenaDomainType)domainId))
                    return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.NbParameterError);
                return _threadDic[(EnumArenaDomainType)domainId].GetOpponent(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场获取对手", ex);
                return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.NbParameterError);
            }
        }

        #endregion

        #region 补充体力

        /// <summary>
        /// 获取体力
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaStaminaResponse GetStamina(Guid managerId,string zoneName)
        {
            try
            {
                var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
                if (domainId == null || !_threadDic.ContainsKey((EnumArenaDomainType)domainId))
                    return ResponseHelper.Create<ArenaStaminaResponse>(MessageCode.NbParameterError);
                return _threadDic[(EnumArenaDomainType)domainId].GetStamina(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场获取体力", ex);
                return ResponseHelper.Create<ArenaStaminaResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 购买通行证
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaBuyStaminaResponse BuyStamina(Guid managerId,string zoneName)
        {
            try
            {
                var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
                if (domainId == null || !_threadDic.ContainsKey((EnumArenaDomainType)domainId))
                    return ResponseHelper.Create<ArenaBuyStaminaResponse>(MessageCode.NbParameterError);
                return _threadDic[(EnumArenaDomainType)domainId].BuyStamina(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场补充体力", ex);
                return ResponseHelper.Create<ArenaBuyStaminaResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 购买通行证参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaBuyStaminaParaResponse BuyStaminaPara(Guid managerId,string zoneName)
        {
            try
            {
                var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
                if (domainId == null || !_threadDic.ContainsKey((EnumArenaDomainType)domainId))
                    return ResponseHelper.Create<ArenaBuyStaminaParaResponse>(MessageCode.NbParameterError);
                return _threadDic[(EnumArenaDomainType)domainId].BuyStaminaPara(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("购买通行证参数", ex);
                return ResponseHelper.Create<ArenaBuyStaminaParaResponse>(MessageCode.NbParameterError);
            }
        }

        #endregion

        #region 比赛

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="opponentId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaFightResponse Fight(Guid managerId, Guid opponentId,string zoneName)
        {
            try
            {
                var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
                if (domainId == null || !_threadDic.ContainsKey((EnumArenaDomainType)domainId))
                    return ResponseHelper.Create<ArenaFightResponse>(MessageCode.NbParameterError);
                return _threadDic[(EnumArenaDomainType)domainId].Fight(managerId, opponentId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("打比赛", ex);
                return ResponseHelper.Create<ArenaFightResponse>(MessageCode.NbParameterError);
            }
        }

        #endregion

        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId,string zoneName)
        {
            try
            {
                var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
                if (domainId == null || !_threadDic.ContainsKey((EnumArenaDomainType)domainId))
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbParameterError);
                return _threadDic[(EnumArenaDomainType)domainId].SolutionAndTeammemberResponse(managerId, zoneName);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场获取阵容", ex);
                return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 获取排名列表
        /// </summary>
        /// <returns></returns>
        public List<ArenaManagerinfoEntity> GetRankList(string zoneName)
        {
            var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
            if (domainId == null)
                return new List<ArenaManagerinfoEntity>();
            return _threadDic[(EnumArenaDomainType)domainId].GetRankList();
        }

        /// <summary>
        /// 获取我的排名详情
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaManagerinfoEntity GetRankInfo(Guid managerId, string zoneName)
        {
            var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
            if (domainId == null)
                return null;
            return _threadDic[(EnumArenaDomainType)domainId].GetRankInfo(managerId);
        }
    }
}
