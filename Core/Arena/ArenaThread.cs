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

namespace Games.NBall.Core.Arena
{
    public class ArenaThread
    {
        /// <summary>
        /// 对手缓存
        /// </summary>
        private ConcurrentDictionary<int, List<ArenaManagerinfoEntity>> _OpponentDic;

        /// <summary>
        /// 排名
        /// </summary>
        private ConcurrentDictionary<Guid, ArenaManagerinfoEntity> _RankDic;
        /// <summary>
        /// 排名
        /// </summary>
        private List<ArenaManagerinfoEntity> _RankList;

        /// <summary>
        /// 当前赛季
        /// </summary>
        private ArenaSeasonEntity _season;

        /// <summary>
        /// 当前赛季
        /// </summary>
        public ArenaSeasonEntity Season
        {
            get { return _season; }
        }

        /// <summary>
        /// 当前竞技场类型
        /// </summary>
        public int ArenaType { get; set; }

        /// <summary>
        /// 赛季详情
        /// </summary>
        private ArenaSeasoninfoEntity _seasonInfo;

        /// <summary>
        /// 赛季详情
        /// </summary>
        public ArenaSeasoninfoEntity SeasonInfo
        {
            get { return _seasonInfo; }
        }

        /// <summary>
        /// 赛季是否开始
        /// </summary>
        public bool IsStart
        {
            get
            {
                if (_seasonInfo == null)
                    return false;
                return _seasonInfo.Status == 1;
            }
        }

        /// <summary>
        /// 是否结束
        /// </summary>
        public bool IsEnd
        {
            get
            {
                if (_seasonInfo == null)
                    return true;
                DateTime date = DateTime.Now;
                return _seasonInfo.EndTime < date;
            }
        }

        /// <summary>
        /// 当前域
        /// </summary>
        private int _domainId;

        public ArenaThread(int domainId)
        {
            _domainId = domainId;
            _OpponentDic = new ConcurrentDictionary<int, List<ArenaManagerinfoEntity>>();
            InitSeason();
            InitOpponent();
            InitRank();
        }


        #region 初始化排名

        private void InitRank()
        {
            var rankDic = new ConcurrentDictionary<Guid, ArenaManagerinfoEntity>();
            var rankList = new List<ArenaManagerinfoEntity>();
            for (int i = 1; i < 6; i++)//拉1万个排名
            {
                var list = ArenaManagerinfoMgr.GetRank(i,_domainId);
                foreach (var item in list)
                {
                    if (!rankDic.ContainsKey(item.ManagerId))
                        rankDic.TryAdd(item.ManagerId, item);
                    rankList.Add(item);
                }
            }
            _RankList = rankList;
            _RankDic = rankDic;
        }

        /// <summary>
        /// 设置排名
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="integral"></param>
        private void SetRank(Guid managerId, int integral)
        {
            ArenaManagerinfoEntity info = null;
            if (!_RankDic.ContainsKey(managerId))
                info = ArenaManagerinfoMgr.GetById(managerId);
            else
                _RankDic.TryGetValue(managerId, out info);
            if (info == null)
                return;
            if (info.Integral == integral && _RankDic.ContainsKey(managerId))
                return;
            info.Integral = integral;
            info.UpdateTime = DateTime.Now;
            if (_RankDic.ContainsKey(managerId))
                _RankDic[managerId] = info;
            else
                info.UpdateTime = DateTime.Now;
            _RankDic.TryAdd(managerId, info);
            var list = _RankDic.Values.OrderByDescending(r => r.Integral).ThenBy(r => r.UpdateTime).ToList();
            var rankDic = new ConcurrentDictionary<Guid, ArenaManagerinfoEntity>();
            int rank = 0;
            var rankList = new List<ArenaManagerinfoEntity>();
            foreach (var item in list)
            {
                rank++;
                item.Rank = rank;
                if (item.Rank > 10000)
                    item.Rank = 0;
                if (!rankDic.ContainsKey(item.ManagerId))
                    rankDic.TryAdd(item.ManagerId, item);
                if (item.Rank > 0)
                    rankList.Add(item);
            }
            _RankList = rankList;
            _RankDic = rankDic;

        }

        /// <summary>
        /// 获取排名列表
        /// </summary>
        /// <returns></returns>
        public List<ArenaManagerinfoEntity> GetRankList()
        {
            return _RankList;
        }

        /// <summary>
        /// 获取我的排名
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public int GetRank(Guid managerId)
        {
            if (!_RankDic.ContainsKey(managerId))
                return 0;
            ArenaManagerinfoEntity info = null;
            _RankDic.TryGetValue(managerId, out info);
            if (info == null)
                return 0;
            return info.Rank;
        }

        /// <summary>
        /// 获取我的排名详情
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ArenaManagerinfoEntity GetRankInfo(Guid managerId)
        {
            ArenaManagerinfoEntity info = null;
            if (!_RankDic.ContainsKey(managerId))
                info = ArenaManagerinfoMgr.GetById(managerId);
            _RankDic.TryGetValue(managerId, out info);
            if (info == null)
                return null;
            return info;
        }

        #endregion

        #region 初始化对手

        /// <summary>
        /// 初始化对手
        /// </summary>
        private void InitOpponent()
        {
            var allDangrading = CacheFactory.ArenaCache.AllDangrading;
            foreach (var item in allDangrading)
            {
                //获取某一段位的所有对手
                var list = ArenaManagerinfoMgr.RefreshOpponent(item.Idx,_domainId);
                if (list.Count > 0) //根据积分排序
                    list = list.OrderBy(r => r.Integral).ToList();
                var groupNumber = list.Count / 3; //计算每挡多少人
                for (int i = 1; i < 4; i++) //每个段位分挡    3个对手
                {
                    var opponentList = new List<ArenaManagerinfoEntity>();
                    //把每挡的数据加到集合里
                    for (int j = groupNumber * (i - 1); j < groupNumber * i; j++)
                    {
                        opponentList.Add(list[j]);
                    }
                    //每挡不足5人时增加npc
                    if (opponentList.Count < 5)
                    {
                        var npcList = CacheFactory.ArenaCache.GetNpc(item.Idx, i);
                        if (npcList != null && npcList.Count > 0)
                        {
                            foreach (var npc in npcList)
                            {
                                if (opponentList.Count > 6)
                                    break;
                                var entity = new ArenaManagerinfoEntity();
                                entity.DanGrading = item.Idx;
                                entity.IsNpc = true;
                                entity.ManagerId = npc.NpcId;
                                var npcInfo = CacheFactory.NpcdicCache.GetNpc(npc.NpcId);
                                if (npcInfo != null)
                                {
                                    entity.ManagerName = npcInfo.Name;
                                    entity.Logo = npcInfo.Logo.ToString();
                                    entity.Kpi = npc.Kpi;
                                }
                                entity.IsNpc = true;
                                entity.Status = 0;
                                opponentList.Add(entity);
                            }
                        }
                    }
                    //根据段位和对手序号获取KEY
                    var key = CacheFactory.ArenaCache.GetKey(item.Idx, i);
                    if (!_OpponentDic.ContainsKey(key))
                        _OpponentDic.TryAdd(key, new List<ArenaManagerinfoEntity>());
                    _OpponentDic[key] = opponentList;
                }
            }
        }

        #endregion

        #region 初始化赛季

        /// <summary>
        /// 初始化赛季
        /// </summary>
        /// <returns></returns>
        public MessageCode InitSeason()
        {
            try
            {
                DateTime date = DateTime.Now;
                _season = ArenaSeasonMgr.GetSeason(date.Date);
                if (_season == null)
                    return MessageCode.NbParameterError;
                _seasonInfo = ArenaSeasoninfoMgr.GetSeasonInfo(_season.SeasonId,_domainId);
                DateTime endTime = _season.EndTime.AddDays(1).AddSeconds(-1);
                this.ArenaType = _season.ArenaType;
                //新赛季
                if (_seasonInfo == null)
                {
                    if (_season.SeasonId == 1)
                    {
                        //第一个赛季
                        _seasonInfo = new ArenaSeasoninfoEntity(0, _season.PrepareTime, _season.StartTime,
                            endTime, _season.ArenaType, 0, false, date, new Guid(), "", "", new Guid(), "", "", 0,
                            date, date,_domainId,_season.SeasonId);
                    }
                    else
                    {
                        //上一赛季
                        var onSeasonInfo = ArenaSeasoninfoMgr.GetSeasonInfo(_season.SeasonId - 1,_domainId);

                        #region 初始化赛季

                        var messageCode = CalculateSeason(onSeasonInfo);
                        if (messageCode != MessageCode.Success)
                            return messageCode;

                        #endregion

                        //上届冠军
                        var onChampionId = new Guid();
                        var onChampionName = "";
                        var onChampionZoneName = "";

                        //王者之师
                        var theKingName = "";
                        var theKingZoneName = "";
                        var theKingId = new Guid();
                        var theKingChampionNumber = 0;
                        //获取上届冠军
                        var onChampion = ArenaManagerrecordMgr.GetChampion(_season.SeasonId - 1,_domainId);
                        if (onChampion != null)
                        {
                            onChampionId = onChampion.ManagerId;
                            onChampionName = onChampion.ManagerName;
                            onChampionZoneName = onChampion.ZoneName;
                            //冠军次数+1
                            ArenaManagerinfoMgr.SetChampion(onChampion.ManagerId);
                        }
                        //获取得到冠军次数最多的人
                        var maxChampion = ArenaManagerinfoMgr.GetChampionMax(_domainId);
                        if (maxChampion != null)
                        {
                            if (onSeasonInfo != null)
                            {
                                //上一届跟这一届是同一个人
                                if (onSeasonInfo.TheKingId == maxChampion.ManagerId)
                                {
                                    theKingId = maxChampion.ManagerId;
                                    theKingName = maxChampion.ZoneName + "." + maxChampion.ManagerName;
                                    theKingZoneName = maxChampion.ZoneName;
                                }
                                else //不是同一人
                                {
                                    //获取上一届王者之师用户信息
                                    var onTheKingInfo = ArenaManagerinfoMgr.GetById(onSeasonInfo.TheKingId);
                                    //先达到的为主
                                    if (onTheKingInfo != null &&
                                        onTheKingInfo.ChampionNumber >= maxChampion.ChampionNumber)
                                    {
                                        theKingId = onTheKingInfo.ManagerId;
                                        theKingName = onTheKingInfo.ZoneName + "." + onTheKingInfo.ManagerName;
                                        theKingZoneName = onTheKingInfo.ZoneName;
                                    }
                                    else
                                    {
                                        theKingId = maxChampion.ManagerId;
                                        theKingName = maxChampion.ZoneName + "." + maxChampion.ManagerName;
                                        theKingZoneName = maxChampion.ZoneName;
                                    }
                                }
                            }
                            else
                            {
                                theKingId = maxChampion.ManagerId;
                                theKingName = maxChampion.ZoneName + "." + maxChampion.ManagerName;
                                theKingZoneName = maxChampion.ZoneName;
                            }
                            theKingChampionNumber = maxChampion.ChampionNumber;
                        }
                        _seasonInfo = new ArenaSeasoninfoEntity(0, _season.PrepareTime, _season.StartTime,
                            endTime, _season.ArenaType, 0, false, date, onChampionId, onChampionName,
                            onChampionZoneName, theKingId, theKingName, theKingZoneName, theKingChampionNumber, date,
                            date,_domainId,_season.SeasonId);
                        //达到开始条件
                        if (_seasonInfo.StartTime.Date <= date.Date && _seasonInfo.Status == 0)
                        {
                            _seasonInfo.Status = 1;
                            ArenaManagerinfoMgr.ClearRecord(_seasonInfo.ArenaType, _domainId);
                        }
                    }

                    if (!ArenaSeasoninfoMgr.Insert(_seasonInfo))
                        return MessageCode.NbUpdateFail;
                    Refresh();
                }
                else
                {
                    //达到开始条件
                    if (_seasonInfo.StartTime.Date <= date.Date && _seasonInfo.Status == 0)
                    {
                        _seasonInfo.Status = 1;
                        ArenaManagerinfoMgr.ClearRecord(_seasonInfo.ArenaType, _domainId);
                        if (!ArenaSeasoninfoMgr.Update(_seasonInfo))
                            return MessageCode.NbUpdateFail;
                        Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场初始化赛季", ex);
                return MessageCode.NbParameterError;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 计算赛季
        /// </summary>
        /// <returns></returns>
        private MessageCode CalculateSeason(ArenaSeasoninfoEntity seasonInfo)
        {
            DateTime date = DateTime.Now;
            if (seasonInfo == null)
                return MessageCode.NbParameterError;
            //上一赛季还未结束
            if (seasonInfo.EndTime >= date.Date)
                return MessageCode.SeasonNotEnd;
            //计算排名  数据导入记录表
            if (!ArenaManagerinfoMgr.ImportRecord(seasonInfo.SeasonId, seasonInfo.ArenaType,_domainId))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }

        #endregion


        #region 获取竞技场信息

        public ArenaManagerinfoEntity GetArenaInfo(Guid managerId, string zoneName = "")
        {
            var info = ArenaManagerinfoMgr.GetById(managerId);
            return info;
        }

        /// <summary>
        /// 获取竞技场信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ArenaGetInfoResponse GetArenaResponse(Guid managerId)
        {
            ArenaGetInfoResponse response = new ArenaGetInfoResponse();
            response.Data = new ArenaGetInfo();
            try
            {
                int myChampionNumber = 0;
                var arenaInfo = GetArenaInfo(managerId);
                var result = new ArenaGetInfo();
                if (arenaInfo != null && IsStart)
                {
                    result.Integral = arenaInfo.Integral;
                    myChampionNumber = arenaInfo.ChampionNumber;
                    result.DanGrading = arenaInfo.DanGrading;
                    if (arenaInfo.DanGrading == 1)
                        result.IsMaxDanGrading = true;
                    else
                    {
                        var dangradingConfig = CacheFactory.ArenaCache.GetDangrading(arenaInfo.DanGrading);
                        result.UpIntegral = dangradingConfig.Integral - arenaInfo.Integral;
                    }
                    switch (arenaInfo.ArenaType)
                    {
                        case 1:
                            result.IsIntoMatch = arenaInfo.Teammember1Status;
                            break;
                        case 2:
                            result.IsIntoMatch = arenaInfo.Teammember2Status;
                            break;
                        case 3:
                            result.IsIntoMatch = arenaInfo.Teammember3Status;
                            break;
                        case 4:
                            result.IsIntoMatch = arenaInfo.Teammember4Status;
                            break;
                        case 5:
                            result.IsIntoMatch = arenaInfo.Teammember5Status;
                            break;
                    }
                }
                result.MyRank = GetRank(managerId);
                if (arenaInfo == null)
                    result.DanGrading = 15;
                result.ArenaType = _seasonInfo.ArenaType;
                result.EndTimeTick = ShareUtil.GetTimeTick(_seasonInfo.EndTime);
                result.StartTimeTick = ShareUtil.GetTimeTick(_seasonInfo.StartTime);
                result.MyChampionNumber = myChampionNumber;
                result.OnChampionName = _seasonInfo.OnChampionName;
                result.OnChampionZoneName = _seasonInfo.OnChampionZoneName;
                result.Status = _seasonInfo.Status;
                result.TheKingChampionNumber = _seasonInfo.TheKingChampionNumber;
                result.TheKingName = _seasonInfo.TheKingName;
                result.TheKingZoneName = _seasonInfo.TheKingZoneName;
                result.StaminaEntity = RestoreStamina(arenaInfo);
                response.Data = result;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取竞技场信息", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #region 对手

        /// <summary>
        /// 获取对手
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ArenaGetOpponentResponse GetOpponent(Guid managerId)
        {
            var response = new ArenaGetOpponentResponse();
            response.Data = new ArenaOpponent();
            try
            {
                if (!IsStart)
                    return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.SeasonNotStart);
                var info = GetArenaInfo(managerId);
                if (info == null)
                    return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.TeammemberNotNumber);
                switch (this.ArenaType)
                {
                    case 1:
                        if (!info.Teammember1Status)
                            return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.TeammemberNotNumber);
                        break;
                    case 2:
                        if (!info.Teammember2Status)
                            return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.TeammemberNotNumber);
                        break;
                    case 3:
                        if (!info.Teammember3Status)
                            return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.TeammemberNotNumber);
                        break;
                    case 4:
                        if (!info.Teammember4Status)
                            return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.TeammemberNotNumber);
                        break;
                    case 5:
                        if (!info.Teammember5Status)
                            return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.TeammemberNotNumber);
                        break;
                }
                info.OpponentList = AnalyseOpponent(info.Opponent);
                if (info.OpponentList == null || info.OpponentList.OpponentList.Count == 0)
                {
                    var opponentList = RefreshOpponent(managerId, info.DanGrading, null);
                    if (opponentList == null)
                        return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.NbParameterError);
                    info.OpponentList = opponentList;
                    info.Opponent = GenerateString(opponentList);
                    if (info.Opponent == null)
                        return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.NbParameterError);
                    response.Data.StaminaEntity = RestoreStamina(info);
                    if (!ArenaManagerinfoMgr.Update(info))
                        return ResponseHelper.Create<ArenaGetOpponentResponse>(MessageCode.NbUpdateFail);
                }
                response.Data = info.OpponentList;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场获取对手", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 刷新对手
        /// </summary>
        /// <param name="managerId">经理id</param>
        /// <param name="danGrading">段位</param>
        /// <param name="matchOpponent">打过的对手</param>
        /// <returns></returns>
        private ArenaOpponent RefreshOpponent(Guid managerId, int danGrading, List<Guid> matchOpponent)
        {
            var result = new ArenaOpponent();
            var opponentList = new List<OpponentEntity>();
            var resultList = new List<Guid>();
            for (int i = 1; i < 4; i++) //选3个对手
            {
                var key = CacheFactory.ArenaCache.GetKey(danGrading, i); //获取key
                if (!_OpponentDic.ContainsKey(key))
                    return null;
                var list = new List<ArenaManagerinfoEntity>();
                //循环添加。避免操作list影响对手池
                foreach (var item in _OpponentDic[key])
                {
                    list.Add(item);
                }
                int index = 0;
                while (index < 10 && list.Count > 0) //循环10次排除重复
                {
                    var listIndex = RandomHelper.GetInt32WithoutMax(0, list.Count);
                    var info = list[listIndex]; //随机一个对手
                    list.RemoveAt(listIndex); //移除随机过的项
                    if (managerId == info.ManagerId)//排除自己
                        continue;
                    if (!resultList.Exists(r => r == info.ManagerId)) //结果里面已经包含了该对手
                    {
                        if (matchOpponent != null && matchOpponent.Count < 5 &&
                            matchOpponent.Exists(r => r == info.ManagerId)) //打过比赛的对手里面已经包含了该对手
                        {
                            index++;
                            continue;
                        }
                        var entity = new OpponentEntity();
                        entity.OpponentManagerId = info.ManagerId;
                        entity.OpponentZoneName = info.SiteId;
                        if (info.IsNpc)
                        {
                            entity.OpponentKpi = info.Kpi;
                            entity.OpponentName = info.ManagerName;
                        }
                        else
                        {
                            entity.OpponentName = info.ZoneName + "." + info.ManagerName;
                            var arenaFrame = new ArenaTeammemberFrame(entity.OpponentManagerId,
                                (EnumArenaType)this.ArenaType,
                                entity.OpponentZoneName);
                            entity.OpponentKpi = arenaFrame.Kpi;
                        }

                        entity.IsNpc = info.IsNpc;
                        entity.OpponentLogo = info.Logo;
                        entity.OpponentIntegral = info.Integral;
                        entity.OpponentDanGrading = info.DanGrading;

                        resultList.Add(entity.OpponentManagerId);
                        opponentList.Add(entity);
                        break;
                    }
                    index++;
                }
                if (index >= 10 || list.Count == 0)
                    return null;
            }
            CheckOpponent(ref opponentList);
            result.OpponentList = opponentList;
            result.MatchOpponent = matchOpponent;
            return result;
        }

        public void CheckOpponent(ref List<OpponentEntity> opponentList)
        {
            if (opponentList == null)
                return;
            opponentList = opponentList.OrderBy(r => r.OpponentKpi).ToList();
            int index = 0;
            foreach (var item in opponentList)
            {
                index++;
                switch (index)
                {
                    case 1:
                        item.GetIntegral = 25;
                        break;
                    case 2:
                        item.GetIntegral = 50;
                        break;
                    case 3:
                        item.GetIntegral = 90;
                        break;
                }
            }
        }

        /// <summary>
        /// 解析对手串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private ArenaOpponent AnalyseOpponent(Byte[] str)
        {
            if (str == null || str.Length == 0)
                return null;
            var result = SerializationHelper.FromByte<ArenaOpponent>(str);
            return result;
        }

        private byte[] GenerateString(ArenaOpponent opponentList)
        {
            if (opponentList == null)
                return null;
            return SerializationHelper.ToByte(opponentList);
        }

        #endregion

        #region 补充体力

        /// <summary>
        /// 获取体力
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ArenaStaminaResponse GetStamina(Guid managerId)
        {
            var response = new ArenaStaminaResponse();
            response.Data = new ArenaStamina();
            try
            {
                var info = GetArenaInfo(managerId);
                if (info == null)
                {
                    response.Data.Stamina = CacheFactory.ArenaCache.ArenaMaxStamina;
                    response.Data.MaxStamina = CacheFactory.ArenaCache.ArenaMaxStamina;
                    return response;
                }
                response.Data = RestoreStamina(info, true);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场获取体力", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 购买通行证参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ArenaBuyStaminaResponse BuyStamina(Guid managerId)
        {
            var response = new ArenaBuyStaminaResponse();
            response.Data = new ArenaBuyStamina();
            try
            {
                //恢复体力
                var info = GetArenaInfo(managerId);
                if (info == null)
                    return ResponseHelper.Create<ArenaBuyStaminaResponse>(MessageCode.NbParameterError);
                RestoreStamina(info);
                if (info.Stamina >= CacheFactory.ArenaCache.ArenaMaxStamina)
                    return ResponseHelper.Create<ArenaBuyStaminaResponse>(MessageCode.StaminaHaveMax);
                var user = PayCore.Instance.GetPayUser(managerId, info.SiteId);
                if (user == null)
                    return ResponseHelper.Create<ArenaBuyStaminaResponse>(MessageCode.NbParameterError);
                //获取消耗多少点卷
                var consumPoint = CacheFactory.ArenaCache.GetBuyStaminaPoint(info.BuyStaminaNumber + 1);
                if (consumPoint == -1)
                    return ResponseHelper.Create<ArenaBuyStaminaResponse>(MessageCode.NbParameterError);
                if (user.Point + user.Bonus < consumPoint)
                    return ResponseHelper.Create<ArenaBuyStaminaResponse>(MessageCode.NbPointShortage);
                info.BuyStaminaNumber++;
                info.Stamina++;
                if (info.Stamina >= info.MaxStamina)
                    info.StaminaRestoreTime = DateTime.Now.Date.AddDays(1);
                if (!ArenaManagerinfoMgr.Update(info))
                    return ResponseHelper.Create<ArenaBuyStaminaResponse>(MessageCode.NbUpdateFail);
                //扣除点卷
                var messCode = PayCore.Instance.ConsumePointForGamble(user.Account, managerId,
                    (int)EnumConsumeSourceType.ArenaGamble,
                    ShareUtil.GenerateComb().ToString(), consumPoint, info.SiteId);
                if (messCode != MessageCode.Success)
                {
                    //扣除点卷失败  还原体力
                    info.BuyStaminaNumber--;
                    info.Stamina--;
                    ArenaManagerinfoMgr.Update(info);
                    return ResponseHelper.Create<ArenaBuyStaminaResponse>(MessageCode.NbUpdateFail);
                }
                response.Data.Point = (user.Point + user.Bonus) - consumPoint;
                response.Data.StaminaEntity = RestoreStamina(info, true);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场补充体力", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 购买通行证参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ArenaBuyStaminaParaResponse BuyStaminaPara(Guid managerId)
        {
            ArenaBuyStaminaParaResponse response = new ArenaBuyStaminaParaResponse();
            response.Data = new ArenaBuyStaminaPara();
            try
            {
                //恢复体力
                var info = GetArenaInfo(managerId);
                if (info == null)
                    return ResponseHelper.Create<ArenaBuyStaminaParaResponse>(MessageCode.NbParameterError);
                response.Data.StaminaEntity = RestoreStamina(info, true);
                if (info.Stamina >= CacheFactory.ArenaCache.ArenaMaxStamina)
                    return ResponseHelper.Create<ArenaBuyStaminaParaResponse>(MessageCode.StaminaHaveMax);
                //获取消耗多少点卷
                var consumPoint = CacheFactory.ArenaCache.GetBuyStaminaPoint(info.BuyStaminaNumber + 1);
                if (consumPoint == -1)
                    return ResponseHelper.Create<ArenaBuyStaminaParaResponse>(MessageCode.NbParameterError);
                response.Data.Point = consumPoint;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("购买通行证参数", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 体力恢复
        /// </summary>
        /// <param name="arenaManagerInfo"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        public ArenaStamina RestoreStamina(ArenaManagerinfoEntity arenaManagerInfo, bool isUpdate = false)
        {
            ArenaStamina response = new ArenaStamina();
            DateTime dateTime = DateTime.Now;
            try
            {
                int staminaStartStopHour = 1;
                int staminaEndStopHour = 8;
                //用于比较的恢复时间截止点
                DateTime restoreTime = dateTime;
                int hour = dateTime.Hour;
                response.IsRestoreStamina = true;
                //恢复时间截止1点
                if (hour >= staminaStartStopHour && hour < staminaEndStopHour)
                {
                    restoreTime = dateTime.Date.AddHours(staminaStartStopHour);
                    response.IsRestoreStamina = false;
                }
                if (arenaManagerInfo == null)
                {
                    response.Stamina = CacheFactory.ArenaCache.ArenaMaxStamina;
                    response.MaxStamina = CacheFactory.ArenaCache.ArenaMaxStamina;
                    return response;
                }
                var vipLevel = 0;
                var manager = ManagerCore.Instance.GetManager(arenaManagerInfo.ManagerId, arenaManagerInfo.SiteId);
                if (manager != null)
                    vipLevel = manager.VipLevel;
                //获取多少秒恢复1点体力
                var restoreTimes = CacheFactory.ArenaCache.StaminaRestoreTime(vipLevel);
                var presentStamina = arenaManagerInfo.Stamina;
                response.Stamina = presentStamina;
                response.MaxStamina = arenaManagerInfo.MaxStamina;
                response.Stamina = arenaManagerInfo.Stamina;
                response.RestoreTimes = restoreTimes;
                if (arenaManagerInfo.Stamina >= arenaManagerInfo.MaxStamina)
                    return response;

                //每天8点恢复所有体力
                if (hour >= staminaEndStopHour && arenaManagerInfo.StaminaRestoreTime < DateTime.Now.Date.AddHours(staminaEndStopHour))
                    arenaManagerInfo.StaminaRestoreTime = DateTime.Now.AddDays(-1);
                //上次恢复体力距离现在时间
                var lastTime = (int)restoreTime.Subtract(arenaManagerInfo.StaminaRestoreTime).TotalSeconds;
                if (lastTime <= 0)
                    return response;
                //恢复多少体力
                var stamina = lastTime / restoreTimes;
                if (stamina <= 0)
                {
                    response.NextRestoreStaminaTick =
                        ShareUtil.GetTimeTick(arenaManagerInfo.StaminaRestoreTime.AddSeconds(restoreTimes));
                    return response;
                }
                arenaManagerInfo.Stamina += stamina;
                if (arenaManagerInfo.Stamina > arenaManagerInfo.MaxStamina)
                    arenaManagerInfo.Stamina = arenaManagerInfo.MaxStamina;
                presentStamina = arenaManagerInfo.Stamina;
                arenaManagerInfo.StaminaRestoreTime = dateTime;
                if (arenaManagerInfo.Stamina < arenaManagerInfo.MaxStamina)
                {
                    //恢复体力后多出来的秒数
                    var surplusTime = lastTime % restoreTimes;
                    arenaManagerInfo.StaminaRestoreTime = dateTime.AddSeconds(-surplusTime);
                    //下次恢复时间
                    response.NextRestoreStaminaTick =
                        ShareUtil.GetTimeTick(arenaManagerInfo.StaminaRestoreTime.AddSeconds(restoreTimes));
                }
                else
                    response.NextRestoreStaminaTick = 0;
                response.Stamina = presentStamina;
                response.MaxStamina = arenaManagerInfo.MaxStamina;
                response.Stamina = arenaManagerInfo.Stamina;
                response.RestoreTimes = restoreTimes;
                if (isUpdate)
                    ArenaManagerinfoMgr.Update(arenaManagerInfo);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("体力恢复", ex);
            }
            return response;
        }

        #endregion

        #region 比赛

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="opponentId"></param>
        /// <returns></returns>
        public ArenaFightResponse Fight(Guid managerId, Guid opponentId)
        {
            ArenaFightResponse response = new ArenaFightResponse();
            response.Data = new ArenaFight();
            try
            {
                //还未开始
                if (!IsStart)
                    return ResponseHelper.Create<ArenaFightResponse>(MessageCode.SeasonNotStart);
                if (IsEnd)
                    return ResponseHelper.Create<ArenaFightResponse>(MessageCode.SeasonNotStart);
                DateTime date = DateTime.Now;
                var info = GetArenaInfo(managerId);
                //阵型未组建完成
                if (info == null)
                    return ResponseHelper.Create<ArenaFightResponse>(MessageCode.TeammemberNotNumber);
                RestoreStamina(info);
                //体力不足
                if (info.Stamina <= 0)
                    return ResponseHelper.Create<ArenaFightResponse>(MessageCode.StaminaInsufficient);
                var arenaInfo = new ArenaTeammemberFrame(managerId, (EnumArenaType)this.ArenaType, info.SiteId);
                //阵型人数<7
                if (arenaInfo.TeammebmerDic.Count < 7)
                    return ResponseHelper.Create<ArenaFightResponse>(MessageCode.TeammemberNotNumber);
                info.OpponentList = AnalyseOpponent(info.Opponent);
                //对手列表里找对手
                var opponent = info.OpponentList.OpponentList.Find(r => r.OpponentManagerId == opponentId);
                if (opponent == null)
                    return ResponseHelper.Create<ArenaFightResponse>(MessageCode.NbParameterError);
                if (!opponent.IsNpc)
                {
                    //对手信息
                    var opponentInfo = new ArenaTeammemberFrame(opponentId, (EnumArenaType)this.ArenaType,
                        opponent.OpponentZoneName);
                    //对手信息不完整  刷新
                    if (info.OpponentList == null || info.OpponentList.OpponentList.Count == 0 ||
                        opponentInfo.TeammebmerDic.Count < 7)
                    {
                        List<Guid> matcOpponet = null;
                        if (info.OpponentList != null)
                            matcOpponet = info.OpponentList.MatchOpponent;
                        var opponentList = RefreshOpponent(managerId, info.DanGrading, matcOpponet);
                        info.OpponentList = opponentList;
                        info.Opponent = GenerateString(info.OpponentList);
                        ArenaManagerinfoMgr.Update(info);
                        return ResponseHelper.Create<ArenaFightResponse>(MessageCode.NbParameterError);
                    }
                }

                var matchHome = new MatchManagerInfo(managerId, info.SiteId, info.ArenaType);
                MatchManagerInfo matchAway = null;
                if (!opponent.IsNpc)
                    matchAway = new MatchManagerInfo(opponent.OpponentManagerId, opponent.OpponentZoneName, info.ArenaType);
                else
                    matchAway = new MatchManagerInfo(opponent.OpponentManagerId, true, info.ArenaType);
                var matchId = ShareUtil.GenerateComb();
                var matchData = new BaseMatchData((int)EnumMatchType.Arena, matchId, matchHome, matchAway);
                matchData.ErrorCode = (int)MessageCode.MatchWait;
                MemcachedFactory.MatchClient.Set(matchData.MatchId, matchData);

                MatchCore.CreateMatch(matchData);
                if (matchData.ErrorCode != (int)MessageCode.Success)
                    return ResponseHelper.Create<ArenaFightResponse>(matchData.ErrorCode);

                //打比赛   自己为主队
                int homeGoals = matchData.Home.Score;
                int awayGoals = matchData.Away.Score;

                if (info.Stamina == info.MaxStamina)
                    info.StaminaRestoreTime = date;
                info.Stamina--;

                if (info.OpponentList.MatchOpponent == null)
                    info.OpponentList.MatchOpponent = new List<Guid>();
                info.OpponentList.MatchOpponent.Add(opponentId);
                //打完比赛重新刷新对手
                info.OpponentList = RefreshOpponent(managerId, info.DanGrading, info.OpponentList.MatchOpponent);
                info.Opponent = GenerateString(info.OpponentList);

                if (homeGoals > awayGoals) //胜利了获得对手的积分
                {
                    info.Integral += opponent.GetIntegral;
                    response.Data.Integral = opponent.GetIntegral;
                    info.UpdateTime = DateTime.Now;
                }
                //计算段位 
                CalculateDanGrading(ref info);
                info.Status = 1;
                response.Data.StaminEntity = RestoreStamina(info, false);
                if (!ArenaManagerinfoMgr.Update(info))
                    return ResponseHelper.Create<ArenaFightResponse>(MessageCode.NbUpdateFail);
                response.Data.OpponentList = info.OpponentList.OpponentList;
                response.Data.MyIntegral = info.Integral;
                response.Data.DanGrading = info.DanGrading;
                //更新排名
                SetRank(managerId, info.Integral);
                response.Data.MyRank = GetRank(managerId);
                response.Data.MatchId = matchId;
                if (info.DanGrading == 1)
                    response.Data.IsMaxDanGrading = true;
                else
                {
                    var dangradingConfig = CacheFactory.ArenaCache.GetDangrading(info.DanGrading);
                    if (dangradingConfig != null)
                        response.Data.UpIntegral = dangradingConfig.Integral - info.Integral;
                }

                MemcachedFactory.ArenaMatchClient.Set<BaseMatchData>(matchId, matchData);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("打比赛", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 计算段位
        /// </summary>
        /// <param name="arenaInfo"></param>
        /// <returns></returns>
        private void CalculateDanGrading(ref ArenaManagerinfoEntity arenaInfo)
        {
            if (arenaInfo == null)
                return;
            int index = 0;
            do
            {
                index++;
                if (arenaInfo.DanGrading == 1)
                    break;
                var dangradingConfig = CacheFactory.ArenaCache.GetDangrading(arenaInfo.DanGrading);
                if (arenaInfo.Integral < dangradingConfig.Integral)
                    break;
                arenaInfo.ArenaCoin += dangradingConfig.PrizeArenaCoin;
                arenaInfo.DanGrading = dangradingConfig.Idx - 1;
            } while (index < 5);
        }

        #endregion

        #region 获取阵型

        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId, string zoneName)
        {
            try
            {
                var arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType)this.ArenaType, zoneName);
                var solution = MatchDataHelper.GetArenaSolutionInfo(arenaFrame);
                if (solution == null)
                    return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                var response = ResponseHelper.CreateSuccess<NBSolutionInfoResponse>();
                response.Data = solution;
                response.Data.Kpi = arenaFrame.Kpi;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场获取阵容", ex);
                return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
            }
        }

        #endregion

        public MessageCode Refresh()
        {
            try
            {
                //设置竞技场排名
                ArenaManagerinfoMgr.SetRank(_domainId);

                //重新初始化对手
                InitOpponent();
                //初始化排名
                InitRank();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("刷新竞技场排名", ex);
                return MessageCode.NbParameterError;
            }
            return MessageCode.Success;
        }
    }
}
