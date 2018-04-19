using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
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
using Games.NBall.Entity.Response.SkillCard;
using MsEntLibWrapper.Data;
using Games.NBall.Core.Teammember;

namespace Games.NBall.Core
{
    public class ArenaTeammemberCore
    {

        public ArenaTeammemberCore(int p)
        {

        }

        #region Instance

        public static ArenaTeammemberCore Instance
        {
            get { return SingletonFactory<ArenaTeammemberCore>.SInstance; }
        }

        #endregion

        #region 下阵

        /// <summary>
        /// 竞技场场上球员下阵
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse ArenaGoOffStage(Guid managerId, int arenaType)
        {
            NBSolutionInfoResponse response = new NBSolutionInfoResponse();
            response.Data = new NBSolutionInfo();
            try
            {
                var season = ArenaSeasonMgr.GetSeason(DateTime.Now.Date);
                if (season == null)
                    return ResponseHelper.Create<NBSolutionInfoResponse>((int)MessageCode.NbParameterError);
                if(season.ArenaType == arenaType)
                    return ResponseHelper.Create<NBSolutionInfoResponse>((int)MessageCode.ArenaNotGoOffStage);
                var arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType) arenaType);
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ArenaGoOffStage);
                if (package == null)
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbParameterError);
                var goOffStageList = new List<Guid>();
                foreach (var item in arenaFrame.TeammebmerDic)
                {
                    //不加非空判断是为了维护数据一致  失败就直接报错 
                    var player = package.GetItem(item.Key);
                    var property = player.ItemProperty as PlayerCardProperty;
                    property.IsMain = false;
                    property.MainType = 0;
                    package.Update(player);
                    goOffStageList.Add(player.ItemId);
                }
                arenaFrame.TeammebmerDic = new Dictionary<Guid, ArenaTeammember>();
                arenaFrame.PlayerString = "0,0,0,0,0,0,0";
                arenaFrame.Kpi = 0;

                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (!arenaFrame.Save(transactionManager.TransactionObject))
                            break;
                        messCode = MessageCode.Success;

                    } while (false);
                    if (messCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        package.Shadow.Save();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<NBSolutionInfoResponse>((int)MessageCode.NbUpdateFail);
                    }
                }
                MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId,arenaFrame.ArenaType);
                var solution = MatchDataHelper.GetArenaSolutionInfo(arenaFrame);
                if (solution == null)
                    return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                UpdateArenaInfoStatus(managerId, arenaType);
                response.Data = solution;
                response.Data.Kpi = 0;
                response.Data.GoOffStageList = goOffStageList;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场场上球员下阵", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #region 获取阵型

        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId, int arenaType, string zoneName = "")
        {
            try
            {
                var arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType) arenaType, zoneName);
                var solution = MatchDataHelper.GetArenaSolutionInfo(arenaFrame);
                if (solution == null)
                    return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                var response = ResponseHelper.CreateSuccess<NBSolutionInfoResponse>();
                response.Data = solution;
                response.Data.Kpi = arenaFrame.Kpi;
                return response;
            }
            catch (Exception  ex)
            {
                SystemlogMgr.Error("竞技场获取阵容", ex);
                return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
            }
        }

        #endregion

        #region 上阵

        /// <summary>
        /// 上阵
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="byIndex"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse UpFormation(Guid managerId, int arenaType, int byIndex, Guid teammemberId)
        {
            try
            {
                var arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType) arenaType);
                if (arenaFrame.PlayerList.Count < byIndex || arenaFrame.PlayerList[byIndex - 1] > 0) //阵容满了，或者位置上已经有了球员
                    return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();

                var playerIdList = arenaFrame.PlayerList;
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TeammemberTrans);
                if (package == null)
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbNoPackage);
                var player = package.GetItem(teammemberId);
                if (player.ItemType != (int) EnumItemType.PlayerCard)
                {
                    return ResponseHelper.Exception<NBSolutionInfoResponse>();
                }
                player.IsDeal = false;
                var itemInfo = CacheFactory.ItemsdicCache.GetItem(player.ItemCode);
                var itemProperty = player.ItemProperty as PlayerCardProperty;
                if (itemProperty == null)
                    return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.NbParameterError);
                if (itemProperty.IsMain && itemProperty.MainType != arenaType)
                    return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.WillCardMain);
                var pid = itemInfo.LinkId;
                if (itemProperty.IsMain)
                {
                    arenaFrame.PlayerString = arenaFrame.PlayerString.Replace(pid.ToString(), "0");
                    arenaFrame.SetPlayerList();
                    arenaFrame.PlayerList[byIndex - 1] = pid;
                    arenaFrame.SetPlayerString();
                    if (!arenaFrame.Save())
                        return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.NbUpdateFail);
                    int kpi1 = MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId,
                   arenaFrame.ArenaType);
                    var solution = MatchDataHelper.GetArenaSolutionInfo(arenaFrame);
                    if (solution == null)
                        return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                    if (!playerIdList.Exists(r => r == 0))
                        UpdateArenaInfo(managerId, arenaType);
                    var response1 = ResponseHelper.CreateSuccess<NBSolutionInfoResponse>();
                    response1.Data = solution;
                    response1.Data.Kpi = kpi1;
                    return response1;
                }
                var playerConfig = CacheFactory.PlayersdicCache.GetPlayer(pid);
                if (!CacheFactory.ArenaCache.IsPlayerMeetTheRequirements(playerConfig, arenaType))
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.ArenaPlayerNotUpFormation);
                //检查是否已有相同pid的球员
                var linkList = CacheFactory.PlayersdicCache.GetLinkPlayerList(pid);
                if (linkList != null)
                {
                    foreach (var link in linkList)
                    {
                        if (playerIdList.Exists(d => d == link))
                        {
                            return
                                ResponseHelper.Create<NBSolutionInfoResponse>(
                                    MessageCode.TeammemberSolutionPlayerRepeat);
                        }
                    }
                }
                if (playerIdList.Exists(r => r == pid))
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.TeammemberSolutionPlayerRepeat);
                arenaFrame.PlayerList[byIndex - 1] = pid;
                arenaFrame.SetPlayerString();
                arenaFrame.UpFormation(teammemberId, pid, itemProperty);
                itemProperty.IsMain = true;
                itemProperty.MainType = arenaType;
                itemProperty.TeammemberId = teammemberId;
                player.ItemProperty = itemProperty;
                package.Update(player);

                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (!arenaFrame.Save(transactionManager.TransactionObject))
                            break;
                        messCode = MessageCode.Success;

                    } while (false);
                    if (messCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        package.Shadow.Save();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.NbUpdateFail);
                    }
                }
                int kpi = MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId,
                    arenaFrame.ArenaType);
                var solution1 = MatchDataHelper.GetArenaSolutionInfo(arenaFrame);
                if (solution1 == null)
                    return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                if (!playerIdList.Exists(r => r == 0))
                    UpdateArenaInfo(managerId, arenaType);
                var response = ResponseHelper.CreateSuccess<NBSolutionInfoResponse>();
                response.Data = solution1;
                response.Data.Kpi = kpi;
                return response;
            }
            catch (Exception  ex)
            {
                SystemlogMgr.Error("竞技场上阵", ex);
                return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
            }
        }

        #endregion

        #region 替换上场

        /// <summary>
        /// 替换上场
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="teammemberId"></param>
        /// <param name="byTeammemberId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse ReplacePlayer(Guid managerId, int arenaType, Guid teammemberId,
            Guid byTeammemberId)
        {
            try
            {
                ItemPackageFrame package = null;
                MessageCode messCode = MessageCode.Success;

                var arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType) arenaType);
                var byteammember = arenaFrame.GetTeammember(byTeammemberId);
                if (byteammember == null || !arenaFrame.PlayerList.Exists(r => r == byteammember.PlayerId))
                    return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.TeammemberNotMain);
                var teammember = arenaFrame.GetTeammember(teammemberId);
                var playerString = "";
                var playerIdList = arenaFrame.PlayerList;
                if (teammember != null)
                {
                    ExchangePlayer(playerIdList, teammember.PlayerId, byteammember.PlayerId, false, ref playerString);
                    arenaFrame.PlayerString = playerString;
                    arenaFrame.PlayerList = playerIdList;

                    if (!arenaFrame.Save())
                        return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.NbUpdateFail);

                    int kpi = MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId, arenaFrame.ArenaType);

                    var solution = MatchDataHelper.GetArenaSolutionInfo(arenaFrame);
                    if (solution == null)
                        return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                    if (!playerIdList.Exists(r => r == 0))
                        UpdateArenaInfo(managerId, arenaType);
                    var response1 = ResponseHelper.CreateSuccess<NBSolutionInfoResponse>();
                    response1.Data = solution;
                    response1.Data.Kpi = kpi;
                    return response1;
                    //换位置
                }
                //换替补
                package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TeammemberTrans);
                if (package == null)
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbNoPackage);
                var player = package.GetItem(teammemberId);
                if (player.ItemType != (int) EnumItemType.PlayerCard)
                {
                    return ResponseHelper.Exception<NBSolutionInfoResponse>();
                }
                var itemInfo = CacheFactory.ItemsdicCache.GetItem(player.ItemCode);
                var itemProperty = player.ItemProperty as PlayerCardProperty;
                if (itemProperty == null)
                    return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.NbParameterError);
                var pid = itemInfo.LinkId;

                var playerConfig = CacheFactory.PlayersdicCache.GetPlayer(pid);
                if (!CacheFactory.ArenaCache.IsPlayerMeetTheRequirements(playerConfig, arenaType))
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.ArenaPlayerNotUpFormation);

                //检查是否已有相同pid的球员
                var linkList = CacheFactory.PlayersdicCache.GetLinkPlayerList(pid);
                if (linkList != null)
                {
                    foreach (var link in linkList)
                    {
                        if (playerIdList.Exists(d => d == link))
                        {
                            return
                                ResponseHelper.Create<NBSolutionInfoResponse>(
                                    MessageCode.TeammemberSolutionPlayerRepeat);
                        }
                    }
                }
                if (playerIdList.Exists(r => r == pid))
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.TeammemberSolutionPlayerRepeat);

                messCode = package.ReplacePlayerCard(100000 + byteammember.PlayerId, false,
                    byteammember.UsePlayer.Strength,
                    byteammember.ItemId, null, player.ItemId, byteammember.UsePlayer.Level, arenaType);
                ExchangePlayer(playerIdList, pid, byteammember.PlayerId, true, ref playerString);
                arenaFrame.PlayerString = playerString;
                arenaFrame.PlayerList = playerIdList;
                arenaFrame.ExchangePlayer(teammemberId, byTeammemberId, pid, itemProperty);
                if (messCode != MessageCode.Success)
                {
                    return ResponseHelper.Create<NBSolutionInfoResponse>(messCode);
                }
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    messCode = MessageCode.NbUpdateFail;

                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (!arenaFrame.Save(transactionManager.TransactionObject))
                            break;
                        messCode = MessageCode.Success;

                    } while (false);
                    if (messCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        package.Shadow.Save();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.NbUpdateFail);
                    }
                }
                int kpi1 = MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId,arenaFrame.ArenaType);

                var solution1 = MatchDataHelper.GetArenaSolutionInfo(arenaFrame);
                if (solution1 == null)
                    return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                if (!playerIdList.Exists(r => r == 0))
                    UpdateArenaInfo(managerId, arenaType);
                var response = ResponseHelper.CreateSuccess<NBSolutionInfoResponse>();
                response.Data = solution1;
                response.Data.Kpi = kpi1;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("替换球员上场", ex);
                return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.NbParameterError);
            }
        }

        #endregion

        #region 设置阵型

        /// <summary>
        /// 设置阵型
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="formationId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SetSolution(Guid managerId, int arenaType, int formationId)
        {
            try
            {
                var arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType) arenaType);
                var formation = CacheFactory.FormationCache.GetFormation(formationId);
                if (formation == null)
                    return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                arenaFrame.SolutionId = formationId;

                if (!arenaFrame.Save())
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbUpdateFail);
                int kpi = arenaFrame.Kpi;
               // if (!arenaFrame.PlayerList.Exists(r => r == 0))
                    kpi = MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId,
                        arenaFrame.ArenaType);
                var solution = MatchDataHelper.GetArenaSolutionInfo(arenaFrame);
                if (solution == null)
                    return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                var response = ResponseHelper.CreateSuccess<NBSolutionInfoResponse>();
                response.Data = solution;
                response.Data.Kpi = kpi;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("设置阵型", ex);
                return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbParameterError);
            }
        }

        #endregion

        #region 装备操作

        /// <summary>
        /// Sets the teammember equip.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <param name="teammemberId">The teammemberId id.</param>
        /// <param name="itemId">The item id.</param>
        /// <param name="package"></param>
        /// <returns></returns>
        public TeammemberResponse SetEquip(Guid managerId, Guid teammemberId, Guid itemId, ItemPackageFrame package)
        {
            #region Check

            if (package == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var playerCardItem = package.GetPlayer(teammemberId);
            if (playerCardItem == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var cardProperty = playerCardItem.ItemProperty as PlayerCardProperty;
            if (cardProperty == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            ArenaTeammember teammember = null;
            var arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType) cardProperty.MainType);
            if (arenaFrame.TeammebmerDic != null)
            {
                if (arenaFrame.TeammebmerDic.ContainsKey(cardProperty.TeammemberId))
                    teammember = arenaFrame.TeammebmerDic[cardProperty.TeammemberId];
            }
            var item = package.GetItem(itemId);
            if (item == null)
                return ResponseHelper.Create<TeammemberResponse>(MessageCode.ItemNotExists);

            #endregion

            try
            {
                //删除要穿上的装备
                var code = package.Delete(item);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<TeammemberResponse>(code);
                var response = ResponseHelper.Create<TeammemberResponse>(MessageCode.Success);
                var itemDic = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                //装备
                if (itemDic.ItemType == (int) EnumItemType.Equipment)
                {
                    if (teammember != null)
                    {
                        if (teammember.UsePlayer.Equipment != null)
                        {
                            var result = package.AddUsedItem(teammember.UsePlayer.Equipment);
                            if (result != MessageCode.Success)
                            {
                                return ResponseHelper.Create<TeammemberResponse>(result);
                            }
                        }
                        //添加换下的装备
                        var newEquip = new EquipmentUsedEntity(item);
                        cardProperty.Equipment = newEquip;
                        code = package.Update(playerCardItem);
                        if (code != MessageCode.Success)
                            return ResponseHelper.Create<TeammemberResponse>(code);
                        //设置新装备
                        teammember.UsePlayer.Equipment = new EquipmentUsedEntity(item);
                    }
                }
                var messCode = MessageCode.NbUpdateFail;

                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;

                        if (!arenaFrame.Save(transactionManager.TransactionObject))
                            break;
                        messCode = MessageCode.Success;

                    } while (false);
                    if (messCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        package.Shadow.Save();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<TeammemberResponse>((int) MessageCode.NbUpdateFail);
                    }
                }
                if (messCode == MessageCode.Success)
                {
                    int kpi = MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(arenaFrame.ManagerId,arenaFrame.ArenaType);

                    if (response.Data == null)
                        response.Data = new TeammemberEntity();
                    response.Data.TotalKpi = kpi;
                    response.Data.Package = ItemCore.Instance.BuildPackageData(package);
                    response.Data.MainType = (int) arenaFrame.ArenaType;
                }
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SetEquip", ex);
                return ResponseHelper.Create<TeammemberResponse>(MessageCode.Exception);
            }
        }

        /// <summary>
        /// Removes the equip.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <param name="teammemberId">The teammember id.</param>
        /// <param name="package"></param>
        /// <returns></returns>
        public TeammemberResponse RemoveEquipment(Guid managerId, Guid teammemberId, ItemPackageFrame package)
        {

            #region Check

            if (package == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var playerCardItem = package.GetItem(teammemberId);//.GetPlayer(teammemberId);
            if (playerCardItem == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var cardProperty = playerCardItem.ItemProperty as PlayerCardProperty;
            if (cardProperty == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var equipEntity = cardProperty.Equipment;
            if (equipEntity == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            ArenaTeammember teammember = null;
            var arenaFrame = new ArenaTeammemberFrame(managerId, (EnumArenaType) cardProperty.MainType);
            if (arenaFrame.TeammebmerDic != null)
            {
                if (arenaFrame.TeammebmerDic.ContainsKey(cardProperty.TeammemberId))
                    teammember = arenaFrame.TeammebmerDic[cardProperty.TeammemberId];
            }

            #endregion

            if (package.IsFull) //背包已满
                return ResponseHelper.Create<TeammemberResponse>(MessageCode.ItemPackageFull);

            var result = package.AddUsedItem(equipEntity);
            if (result != MessageCode.Success)
                return ResponseHelper.Create<TeammemberResponse>(result);
            cardProperty.Equipment = null;
            var code = package.Update(playerCardItem);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<TeammemberResponse>(code);
            TeammemberResponse response = null;
            if (teammember != null)
            {
                teammember.UsePlayer.Equipment = null;
                response = new TeammemberResponse();
                response.Data = new TeammemberEntity();
            }
            else
            {
                response = ResponseHelper.Create<TeammemberResponse>(MessageCode.Success);
            }
            if (response.Code == ShareUtil.SuccessCode)
            {
                var messCode = MessageCode.NbUpdateFail;
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;

                        if (!arenaFrame.Save(transactionManager.TransactionObject))
                            break;
                        messCode = MessageCode.Success;

                    } while (false);
                    if (messCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        package.Shadow.Save();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<TeammemberResponse>((int) MessageCode.NbUpdateFail);
                    }
                }
                if (arenaFrame != null)
                {
                    int kpi = MatchDataCacheHelper.DeleteTeammemberAndSolutionCache(managerId, (EnumArenaType)cardProperty.MainType);
                    response.Data.TotalKpi = kpi;
                    response.Data.Package = ItemCore.Instance.BuildPackageData(package);
                    response.Data.MainType = (int) arenaFrame.ArenaType;
                }
                else
                {
                    response = new TeammemberResponse();
                    response.Data = new TeammemberEntity();
                    response.Data.Package = new ItemPackageData();
                    var packageItemsEntity = SerializationHelper.FromByte<ItemPackageItemsEntity>(package.NewItemString);
                    if (packageItemsEntity == null || packageItemsEntity.Items == null)
                        response.Data.Package.Items = new List<ItemInfoEntity>();
                    else
                        response.Data.Package.Items = packageItemsEntity.Items;
                    response.Data.Package.PackageSize = package.PackageSize;
                    response.Data.MainType = cardProperty.MainType;
                }
            }
            return response;
        }

        #endregion

        #region 换球员位置

        /// <summary>
        /// 调换球员位置
        /// </summary>
        /// <param name="playerIdList">球员串</param>
        /// <param name="pid">要换上的球员id</param>
        /// <param name="bypid">被换的球员ID</param>
        /// <param name="isSubstitution">是否是替补席换的</param>
        /// <param name="playerString">输出球员串</param>
        /// <returns></returns>
        public MessageCode ExchangePlayer(List<int> playerIdList, int pid, int bypid, bool isSubstitution,
            ref string playerString)
        {
            playerString = "";
            if (isSubstitution)
            {
                for (int i = 0; i < playerIdList.Count; i++)
                {
                    if (playerIdList[i] == bypid)
                        playerIdList[i] = pid;
                }
            }
            else
            {
                int p = -1;
                int byp = -1;
                for (int i = 0; i < playerIdList.Count; i++)
                {
                    if (playerIdList[i] == pid)
                        p = i;
                    else if (playerIdList[i] == bypid)
                        byp = i;
                }
                int team = playerIdList[p];
                playerIdList[p] = playerIdList[byp];
                playerIdList[byp] = team;
            }
            foreach (var item in playerIdList)
            {
                playerString += item + ",";
            }
            if (playerIdList.Count > 0)
                playerString = playerString.Substring(0, playerString.Length - 1);
            return MessageCode.Success;
        }

        #endregion

        #region 商城

        /// <summary>
        /// 获取竞技场商城
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetArenaShopResponse GetShopInfoResponse(Guid managerId)
        {
            GetArenaShopResponse response = new GetArenaShopResponse();
            response.Data = new ArenaShop();
            try
            {
                var info = GetShopInfo(managerId);
                if (info == null)
                    return ResponseHelper.Create<GetArenaShopResponse>(MessageCode.NbParameterError);
                response.Data.ItemString = info.ItemString;
                response.Data.RefreshTick = ShareUtil.GetTimeTick(info.RefreshTime);
                var arenaInfo = GetArenaInfo(managerId);
                if (arenaInfo != null)
                    response.Data.ArenaCoin = arenaInfo.ArenaCoin;
                response.Data.ExchangeString = info.ExChangeRecord;
                response.Data.NextRefreshPoint = CacheFactory.ArenaCache.GetRefreshShopPoint(info.RefreshNumber + 1);
                response.Data.Point = -1;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取竞技场商城", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 刷新竞技场商城
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetArenaShopResponse RefreshShop(Guid managerId)
        {
            GetArenaShopResponse response = new GetArenaShopResponse();
            response.Data = new ArenaShop();
            DateTime date = DateTime.Now;
            try
            {
                var info = GetShopInfo(managerId);
                if (info == null)
                    return ResponseHelper.Create<GetArenaShopResponse>(MessageCode.NbParameterError);
                var price = CacheFactory.ArenaCache.GetRefreshShopPoint(info.RefreshNumber + 1);
                var point = PayCore.Instance.GetPoint(managerId);
                if (point < price)
                    return ResponseHelper.Create<GetArenaShopResponse>(MessageCode.NbPointShortage);
                info.ItemString = RefreshShop();
                info.ExChangeRecord = "0,0,0,0,0,0,0,0,0,0";
                info.UpdateTime = date;
                info.RefreshNumber++;

                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    var messageCode = MessageCode.NbUpdateFail;
                    transactionManager.BeginTransaction();
                    do
                    {
                        messageCode = PayCore.Instance.GambleConsume(managerId, price, ShareUtil.GenerateComb(),
                            EnumConsumeSourceType.ArenaGambleResetShop, transactionManager.TransactionObject);
                        if (messageCode != MessageCode.Success)
                            break;
                        messageCode = MessageCode.NbUpdateFail;
                        if (!ArenaShopMgr.Update(info, transactionManager.TransactionObject))
                            break;

                        messageCode = MessageCode.Success;
                    } while (false);
                    if (messageCode == MessageCode.Success)
                        transactionManager.Commit();
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<GetArenaShopResponse>(messageCode);
                    }
                }
                response.Data.ItemString = info.ItemString;
                response.Data.RefreshTick = ShareUtil.GetTimeTick(info.RefreshTime);
                var arenaInfo = GetArenaInfo(managerId);
                if (arenaInfo != null)
                    response.Data.ArenaCoin = arenaInfo.ArenaCoin;
                response.Data.ExchangeString = info.ExChangeRecord;
                response.Data.NextRefreshPoint = CacheFactory.ArenaCache.GetRefreshShopPoint(info.RefreshNumber + 1);
                response.Data.Point = point - price;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("刷新竞技场商城", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        private ArenaShopEntity GetShopInfo(Guid managerId)
        {
            DateTime date = DateTime.Now;
            var shopInfo = ArenaShopMgr.GetById(managerId);
            if (shopInfo == null)
            {
                shopInfo = new ArenaShopEntity(managerId, "", "", date.Date.AddHours(21), 0, date, date);
                shopInfo.ItemString = RefreshShop();
                shopInfo.ExChangeRecord = "0,0,0,0,0,0,0,0,0,0";
                ArenaShopMgr.Insert(shopInfo);
            }
            CheckShop(shopInfo);
            return shopInfo;
        }

        /// <summary>
        /// 验证商城刷新
        /// </summary>
        /// <param name="shopInfo"></param>
        private void CheckShop(ArenaShopEntity shopInfo)
        {
            DateTime date = DateTime.Now;
            if (shopInfo == null)
                return;
            if (shopInfo.RefreshTime <= date)
            {
                shopInfo.ItemString = RefreshShop();
                shopInfo.RefreshTime = date.Date.AddHours(21);
                if (date.Hour >= 21)
                    shopInfo.RefreshTime = shopInfo.RefreshTime.AddDays(1);
                //shopInfo.RefreshTime = shopInfo.RefreshTime.AddDays(1);
                shopInfo.ExChangeRecord = "0,0,0,0,0,0,0,0,0,0";
                shopInfo.UpdateTime = date;
                shopInfo.RefreshNumber = 0;
                ArenaShopMgr.Update(shopInfo);
            }
        }

        /// <summary>
        /// 刷新商城
        /// </summary>
        /// <returns></returns>
        private string RefreshShop()
        {
            var shopList = CacheFactory.ArenaCache.GetShopList();
            var str = new StringBuilder();
            foreach (var item in shopList)
            {
                var itemcode = 0;
                switch (item.ItemType)
                {
                    case 3://指定物品
                        itemcode = item.ItemCode;
                        break;
                    case 4://卡库
                        itemcode = CacheFactory.LotteryCache.LotteryByLib(item.ItemCode);
                        break;
                    case 5://随机欧洲杯碎片
                        var itemList = new List<int>() { 395001, 395002, 395003, 395004, 395005, 395006, 395007 };
                        itemcode = itemList[RandomHelper.GetInt32(0, 6)];
                        break;
                }
                str.Append(itemcode + "," + item.ItemCount + "," + item.Price + "|");
            }
            var result = "";
            if (str.Length > 0)
                result = str.ToString().Substring(0, str.Length - 1);
            return result;
        }

        /// <summary>
        /// 兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exIndex"></param>
        /// <returns></returns>
        public ArenaExChangeResponse ExChange(Guid managerId, int exIndex)
        {
            ArenaExChangeResponse response = new ArenaExChangeResponse();
            response.Data = new ArenaExChange();
            try
            {
                var info = GetShopInfo(managerId);
                if (info == null)
                    return ResponseHelper.Create<ArenaExChangeResponse>(MessageCode.NbParameterError);
                var itemList = info.ItemString.Split('|');
                var exList = info.ExChangeRecord.Split(',');
                if (exList.Length - 1 < exIndex || itemList.Length - 1 < exIndex)
                    return ResponseHelper.Create<ArenaExChangeResponse>(MessageCode.NbParameterError);
                if (exList[exIndex] == "1")
                    return ResponseHelper.Create<ArenaExChangeResponse>(MessageCode.LadderExchangeTimesOver);
                var exConfig = itemList[exIndex].Split(',');
                var arenaInfo = GetArenaInfo(managerId);
                var price = ConvertHelper.ConvertToInt(exConfig[2]);
                if (arenaInfo == null || arenaInfo.ArenaCoin < price)
                    return ResponseHelper.Create<ArenaExChangeResponse>(MessageCode.ArenaCoinNot);
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ArenaShop);
                if (package == null)
                    return ResponseHelper.Create<ArenaExChangeResponse>(MessageCode.NbParameterError);
                var itemCode = ConvertHelper.ConvertToInt(exConfig[0]);
                var itemCount = ConvertHelper.ConvertToInt(exConfig[1]);
                var messageCode = package.AddItems(itemCode, itemCount, false,false);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<ArenaExChangeResponse>(messageCode);
                arenaInfo.ArenaCoin = arenaInfo.ArenaCoin - price;
                var record = new ArenaExchangerecordEntity(0, managerId, itemCode, price, itemCount, DateTime.Now);
                if (!ArenaManagerinfoMgr.Update(arenaInfo))
                    return ResponseHelper.Create<ArenaExChangeResponse>(MessageCode.NbUpdateFail);
                exList[exIndex] = "1";
                info.ExChangeRecord = string.Join(",", exList);
                info.UpdateTime = DateTime.Now;

                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    messageCode = MessageCode.NbUpdateFail;
                    transactionManager.BeginTransaction();
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (!ArenaShopMgr.Update(info, transactionManager.TransactionObject))
                            break;
                        if (!ArenaExchangerecordMgr.Insert(record))
                            break;
                        messageCode = MessageCode.Success;
                    } while (false);
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        package.Shadow.Save();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        //扣除的竞技币加上
                        arenaInfo.ArenaCoin = arenaInfo.ArenaCoin - price;
                        if (!ArenaManagerinfoMgr.Update(arenaInfo))
                        {
                            SystemlogMgr.Error("恢复竞技币失败", "managerId:" + managerId + ",number:" + price);
                            return ResponseHelper.Create<ArenaExChangeResponse>(messageCode);
                        }
                    }
                }
                response.Data.ArenaCoin = arenaInfo.ArenaCoin;
                response.Data.ExchangeString = info.ExChangeRecord;
                response.Data.ItemString = info.ItemString;
                response.Data.RefreshTick = ShareUtil.GetTimeTick(info.RefreshTime);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞技场兑换", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        public ArenaManagerinfoEntity GetArenaInfo(Guid managerId, string zoneName = "")
        {
            var info = ArenaManagerinfoMgr.GetById(managerId);
            return info;
        }

        #endregion

        public void UpdateArenaInfo(Guid managerId, int arenaType)
        {
            bool isInsert = false;
            var arenaInfo = ArenaManagerinfoMgr.GetById(managerId);
            if (arenaInfo == null)
            {
                int type = 1;
                var season = ArenaSeasonMgr.GetSeason(DateTime.Now.Date);
                if (season != null)
                    type = season.ArenaType;
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return;
                arenaInfo = new ArenaManagerinfoEntity();
                arenaInfo.ManagerId = managerId;
                arenaInfo.ManagerName = manager.Name;
                arenaInfo.SiteId = ShareUtil.ZoneName;
                arenaInfo.ZoneName = ShareUtil.CrossName;
                arenaInfo.ChampionNumber = 0;
                arenaInfo.Integral = 0;
                arenaInfo.DanGrading = 15;
                arenaInfo.ArenaCoin = 0;
                arenaInfo.BuyStaminaNumber = 0;
                arenaInfo.Rank = 0;
                arenaInfo.Status = 0;
                arenaInfo.ArenaType = type;
                arenaInfo.Stamina = CacheFactory.ArenaCache.ArenaMaxStamina;
                arenaInfo.MaxStamina = CacheFactory.ArenaCache.ArenaMaxStamina;
                arenaInfo.UpdateTime = DateTime.Now;
                arenaInfo.RowTime = DateTime.Now;
                arenaInfo.StaminaRestoreTime = DateTime.Now;
                arenaInfo.Opponent = new byte[0];
                arenaInfo.Logo = manager.Logo;
                var domainId = CacheFactory.ArenaCache.GetDomainId(ShareUtil.ZoneName);
                arenaInfo.DomainId = domainId != null ? (int) domainId : 0;
                isInsert = true;
                //体力和最大体力
            }
            switch (arenaType)
            {
                case 1:
                    if (arenaInfo.Teammember1Status)
                        return;
                    arenaInfo.Teammember1Status = true;
                    break;
                case 2:
                    if (arenaInfo.Teammember2Status)
                        return;
                    arenaInfo.Teammember2Status = true;
                    break;
                case 3:
                    if (arenaInfo.Teammember3Status)
                        return;
                    arenaInfo.Teammember3Status = true;
                    break;
                case 4:
                    if (arenaInfo.Teammember4Status)
                        return;
                    arenaInfo.Teammember4Status = true;
                    break;
                case 5:
                    if (arenaInfo.Teammember5Status)
                        return;
                    arenaInfo.Teammember5Status = true;
                    break;
            }
            if (isInsert)
                ArenaManagerinfoMgr.Insert(arenaInfo);
            else
                ArenaManagerinfoMgr.Update(arenaInfo);
        }

        public void UpdateArenaInfoStatus(Guid managerId, int arenaType)
        {
            var arenaInfo = ArenaManagerinfoMgr.GetById(managerId);
            if (arenaInfo == null)
                return;
            switch (arenaType)
            {
                case 1:
                    if (arenaInfo.Teammember1Status)
                        return;
                    arenaInfo.Teammember1Status = true;
                    break;
                case 2:
                    if (arenaInfo.Teammember2Status)
                        return;
                    arenaInfo.Teammember2Status = true;
                    break;
                case 3:
                    if (arenaInfo.Teammember3Status)
                        return;
                    arenaInfo.Teammember3Status = true;
                    break;
                case 4:
                    if (arenaInfo.Teammember4Status)
                        return;
                    arenaInfo.Teammember4Status = true;
                    break;
                case 5:
                    if (arenaInfo.Teammember5Status)
                        return;
                    arenaInfo.Teammember5Status = true;
                    break;
            }
            ArenaManagerinfoMgr.Update(arenaInfo);
        }
    }
}
