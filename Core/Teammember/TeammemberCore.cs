using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Bll.Cache;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.NBall.Custom.Teammember;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceEngine;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Teammember
{
    public partial class TeammemberCore
    {
        readonly int _growCountToFast;
        readonly int _growPoint;
        readonly int _growFailPercent;
        private readonly int _daliwanCode;
        private readonly int _maxVeteranCount;
        #region .ctor

        public TeammemberCore(int p)
        {
            _maxVeteranCount = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.MaxVeteranCount);
            _growCountToFast = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.GrowCountToFast);
            _growPoint = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.GrowPoint);
            _growFailPercent = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.GrowFailPercent);
            _daliwanCode = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.GrowDaliWanId);
        }
        #endregion

        #region Facade
        
        public static TeammemberCore Instance
        {
            get { return SingletonFactory<TeammemberCore>.SInstance; }
        }

        #region 获取阵容和球员信息

        /// <summary>
        /// 获取阵容和球员信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId)
        {
            var solution =  MatchDataHelper.GetSolutionInfo(managerId);
            if (solution == null)
                return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
            var response = ResponseHelper.CreateSuccess<NBSolutionInfoResponse>();

            response.Data = solution;
            var manager = ManagerCore.Instance.GetManager(managerId, true);
            if (manager != null)
                response.Data.Kpi = manager.Kpi;
            return response;
        }
        
        public TeammemberResponse GetTeammemberResponse(Guid managerId, Guid teammemberId)
        {
            return BuildTeammemberResponse(managerId,teammemberId);
        }

        public TeammemberEntity GetTeammember(Guid managerId, Guid teammemberId, bool withHire = false)
        {
            return MatchDataHelper.GetTeammember(managerId, teammemberId, withHire);
        }
        #endregion

        #region 替换球员上场

        /// <summary>
        /// 替换上场
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <param name="byTeammemberId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse ReplacePlayer(Guid managerId, Guid teammemberId, Guid byTeammemberId)
        {
            bool isChanagePlayer = false;
            try
            {
                ItemPackageFrame package = null;
                MessageCode messCode = MessageCode.Success;
                var soluti = MatchDataHelper.GetSolution(managerId);
                if (soluti == null)
                    return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                var byteammember = GetTeammember(managerId, byTeammemberId);
                if (byteammember == null || !soluti.PlayerDic.ContainsKey(byteammember.PlayerId))
                    return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.TeammemberNotMain);
                var teammember = GetTeammember(managerId, teammemberId);
                var playerString = "";
                var playerIdList = FrameUtil.CastIntList(soluti.PlayerString, ',');


                if (teammember != null)
                {
                    ExchangePlayer(playerIdList, teammember.PlayerId, byteammember.PlayerId, false, ref playerString);
                    soluti.PlayerString = playerString;
                    if (!NbSolutionMgr.Update(soluti))
                        return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.NbUpdateFail);
                    MemcachedFactory.SolutionClient.Delete(managerId);
                    KpiHandler.Instance.RebuildKpi(managerId, true);
                    var manager = MatchDataHelper.GetManager(managerId, true, true);
                    if (manager == null)
                        return ResponseHelper.Create<NBSolutionInfoResponse>((int) MessageCode.NbParameterError);

                    var response1 = SolutionAndTeammemberResponse(managerId);
                    return response1;
                    //换位置
                }
                else //换替补
                {
                    isChanagePlayer = true;
                    package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TeammemberTrans);
                    if (package == null)
                        return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbNoPackage);
                    var player = package.GetPlayer(teammemberId);
                    if (player.ItemType != (int) EnumItemType.PlayerCard)
                    {
                        return ResponseHelper.Exception<NBSolutionInfoResponse>();
                    }
                    player.IsDeal = false;
                    var itemInfo = CacheFactory.ItemsdicCache.GetItem(player.ItemCode);
                    //限制金卡
                    if (itemInfo != null && itemInfo.ItemType == (int) EnumItemType.PlayerCard &&
                        itemInfo.PlayerCardLevel == (int) EnumPlayerCardLevel.Gold)
                    {
                        var solution = MatchDataHelper.GetSolutionInfo(managerId);
                        if (solution == null)
                            return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();
                        if (solution.MaxVeteranCount <= solution.VeteranCount)
                            return
                                ResponseHelper.Create<NBSolutionInfoResponse>(
                                    (int) MessageCode.TeammemberVeteranCountOver);
                    }
                    var pid = player.ItemCode%100000;

                    //检查是否已有相同pid的球员
                    var linkList = CacheFactory.PlayersdicCache.GetLinkPlayerList(pid);
                    if (linkList != null)
                    {
                        foreach (var link in linkList)
                        {
                            if (playerIdList.Exists(d => d == link))
                            {
                                return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.TeammemberSolutionPlayerRepeat);
                            }
                        }
                    }
                    int teammemberCount = 0;
                    int returnCode = 0;
                    TeammemberMgr.GetForTransCheck(managerId, pid, ShareUtil.GetTableMod(managerId),
                        (int) MessageCode.TeammemberRepeat, ref teammemberCount, ref returnCode);
                    if (returnCode != (int) MessageCode.Success)
                    {
                        return ResponseHelper.Create<NBSolutionInfoResponse>(returnCode);
                    }
                    messCode = package.ReplacePlayerCard(100000 + byteammember.PlayerId, false,
                        byteammember.Strength,
                        byteammember.Idx, byteammember.Equipment, player.ItemId, byteammember.Level);
                    ExchangePlayer(playerIdList, pid, byteammember.PlayerId, true, ref playerString);
                    soluti.PlayerString = playerString;
                    if (messCode != MessageCode.Success)
                    {
                        return ResponseHelper.Create<NBSolutionInfoResponse>(messCode);
                    }

                }
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    messCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (package != null)
                        {
                            if (!package.SavePlayer(transactionManager.TransactionObject))
                                break;
                        }
                        if (!NbSolutionMgr.Update(soluti, transactionManager.TransactionObject))
                            break;
                        messCode = MessageCode.Success;

                    } while (false);
                    if (messCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        MemcachedFactory.TeammembersClient.Delete(managerId);
                        MemcachedFactory.SolutionClient.Delete(managerId);
                        package.Shadow.Save();
                        int orangeCount = 0;
                        string[] pIds = playerString.Split(',');
                        foreach (var pId in pIds)
                        {
                            int id = ConvertHelper.ConvertToInt(pId);
                            var player = CacheFactory.PlayersdicCache.GetPlayer(id);
                            if (player.CardLevel == (int) EnumPlayerCardLevel.Orange)
                            {
                                orangeCount++;
                            }
                        }
                        ActivityExThread.Instance.TememberColect(managerId, 3, orangeCount);
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<NBSolutionInfoResponse>(messCode);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("替换球员上场", ex);
            }
            KpiHandler.Instance.RebuildKpi(managerId, true);

            var response = SolutionAndTeammemberResponse(managerId);
            if (isChanagePlayer)
            {
                var pop = TaskHandler.Instance.SolutionChangePlayer(managerId);
                if (response.Data != null)
                {
                    response.Data.PopMsg = pop;
                }
            }

            return response;
        }

        /// <summary>
        /// 一键换人
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse AKeySubstitution(Guid managerId)
        {
            NBSolutionInfoResponse response = new NBSolutionInfoResponse();
            response.Data = new NBSolutionInfo();
            try
            {

            }
            catch (Exception ex)
            {
            }
            return response;
        }

        #endregion

        #region SaveSolution

        public NBSolutionInfoResponse SaveSolution(Guid managerId, int formationId, string playerString, bool hasTask)
        {
            var solution = MatchDataHelper.GetSolution(managerId);
            if (solution == null)
                return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();

            bool isChangePlayer = solution.PlayerString != playerString;
            string[] pIds = playerString.Split(',');
            if (pIds.Length != SystemConstants.TeammemberCount)
                return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.TeammemberInvalidCount);
            var formation = CacheFactory.FormationCache.GetFormation(formationId);
            if(formation==null)
                return ResponseHelper.InvalidParameter<NBSolutionInfoResponse>();

            var teammembers = MatchDataHelper.GetTeammembers(managerId, null, true);
            List<int> tempPids=new List<int>(pIds.Length);
            int veteranCount = 0;
            int orangeCount = 0;
            int combCount = 0;
            
            foreach (var pId in pIds)
            {
                int id = ConvertHelper.ConvertToInt(pId);
                if (!teammembers.Exists(d => d.PlayerId == id))
                {
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.TeammemberInvalidPlayer);
                }
                if (tempPids.Contains(id))
                {
                    return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.TeammemberSolutionPlayerRepeat);
                }
                tempPids.Add(id);
                var player = CacheFactory.PlayersdicCache.GetPlayer(id);
                if (player.CardLevel == (int) EnumPlayerCardLevel.Gold || player.CardLevel==(int)EnumPlayerCardLevel.Silver)
                {
                    veteranCount++;
                }
                else if (player.CardLevel == (int)EnumPlayerCardLevel.Orange || player.CardLevel == (int)EnumPlayerCardLevel.BlackGold)
                {
                    orangeCount++;
                }
            }
            foreach (var tempPid in tempPids)
            {
                var linkList = CacheFactory.PlayersdicCache.GetLinkPlayerList(tempPid);
                if (linkList != null)
                {
                    foreach (var link in linkList)
                    {
                        if (tempPids.Exists(d => d == link))
                        {
                            return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.TeammemberSolutionPlayerRepeat);
                        }
                    }
                }
            }
            int veteranNumber = _maxVeteranCount;
            var manager = NbManagerextraMgr.GetById(managerId);
            if(manager == null)
                return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbParameterError);
            if (manager.VeteranNumber > _maxVeteranCount)
                veteranNumber = manager.VeteranNumber;
            if (veteranCount > veteranNumber)
                return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.TeammemberVeteranCountOver);
            combCount = ManagerSkillCache.Instance().GetCombsNum(tempPids.ToArray());
            if (TeammemberMgr.SaveSolution(managerId, formationId, playerString, veteranCount,orangeCount,combCount))
            {
                DeleteSolutionCache(managerId,true);
                ActivityExThread.Instance.TememberColect(managerId, 3, orangeCount);
                var response = SolutionAndTeammemberResponse(managerId);
                if (hasTask && isChangePlayer)
                {
                    var pop = TaskHandler.Instance.SolutionChangePlayer(managerId);
                    if (response.Data != null)
                    {
                        response.Data.PopMsg = pop;
                    }
                }
                return response;
            }
            else
            {
                return ResponseHelper.Create<NBSolutionInfoResponse>(MessageCode.NbUpdateFail);
            }
        }

        public MessageCode BackSaveSolution(Guid managerId, NbSolutionEntity solution, int[] newPids, List<TeammemberEntity> members)
        {
            if (null == solution)
                return MessageCode.Success;
            if (newPids.Length != SystemConstants.TeammemberCount)
                return MessageCode.TeammemberInvalidCount;
            if (null == members)
                members = MatchDataHelper.GetTeammembers(managerId, null, true);
            List<int> tempPids = new List<int>(newPids.Length);
            int veteranCount = 0;
            int orangeCount = 0;
            int combCount = 0;
            foreach (var pId in newPids)
            {
                int id = ConvertHelper.ConvertToInt(pId);
                if (!members.Exists(d => d.PlayerId == id))
                {
                    return MessageCode.TeammemberInvalidPlayer;
                }
                if (tempPids.Contains(id))
                {
                    return MessageCode.TeammemberSolutionPlayerRepeat;
                }
                tempPids.Add(id);
                var player = CacheFactory.PlayersdicCache.GetPlayer(id);
                if (player.CardLevel == (int)EnumPlayerCardLevel.Gold || player.CardLevel == (int)EnumPlayerCardLevel.Silver)
                {
                    veteranCount++;
                }
                else if (player.CardLevel == (int)EnumPlayerCardLevel.Orange || player.CardLevel == (int)EnumPlayerCardLevel.BlackGold)
                {
                    orangeCount++;
                }
            }
            int veteranNumber = _maxVeteranCount;
            var manager = NbManagerextraMgr.GetById(managerId);
            if (manager == null)
                return MessageCode.NbParameterError;
            if (manager.VeteranNumber > _maxVeteranCount)
                veteranNumber = manager.VeteranNumber;
            if (veteranCount > veteranNumber)
                return MessageCode.TeammemberVeteranCountOver;
            combCount = ManagerSkillCache.Instance().GetCombsNum(tempPids.ToArray());
            string playerString = string.Join(",", newPids);
            if (!TeammemberMgr.SaveSolution(managerId, solution.FormationId, playerString, veteranCount, orangeCount, combCount))
                return MessageCode.NbUpdateFail;
            //DeleteSolutionCache(managerId, true);
            return MessageCode.Success;
        }
        #endregion

        #region 解雇球员
        /// <summary>
        /// 解雇球员
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public MessageCodeResponse FireTeamMember(Guid managerId, Guid teammemberId)
        {
            try
            {
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TeammemberTrans);
                if (package == null)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbNoPackage);
                var player = package.GetPlayer(teammemberId);
                if (player.ItemType != (int)EnumItemType.PlayerCard)
                    return ResponseHelper.Exception<MessageCodeResponse>();
                int  pid = player.ItemCode%100000;
             
                //检查球员是否已在场上
                var solution = MatchDataHelper.GetSolution(managerId);
                if (solution.PlayerDic.ContainsKey(pid))
                {
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.TeammemberIsMain);
                }
                var property = player.ItemProperty as PlayerCardProperty;
                if (property !=null && property.Equipment != null)
                {
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.TeammemberHasEquip);
                }
                var messcode = package.Delete(player.ItemId);
                if(messcode!= MessageCode.Success)
                    return ResponseHelper.Create<MessageCodeResponse>(messcode);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    if (!package.Save(transactionManager.TransactionObject))
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail);
                    }
                    transactionManager.Commit();
                    MemcachedFactory.TeammembersClient.Delete(managerId);
                     package.Shadow.Save();
                }
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("TeamMember-Fire",ex);
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Exception);
            }
        }
        #endregion

        #region 装备和附卡操作
        /// <summary>
        /// Sets the teammember equip.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <param name="teammemberId">The teammember id.</param>
        /// <param name="itemId">The item id.</param>
        /// <returns></returns>
        public TeammemberResponse SetEquip(Guid managerId, Guid teammemberId, Guid itemId)
        {
            #region Check
            //if (teammember == null
            //    || teammember.ManagerId != managerId)
            //    return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TeammemberSetEquip);
            if (package == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var playerCardItem = package.GetPlayer(teammemberId);
            if (playerCardItem == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var cardProperty = playerCardItem.ItemProperty as PlayerCardProperty;
            if (cardProperty == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            //在竞技场的阵型上
            if (cardProperty.IsMain && cardProperty.MainType > 0)
                return ArenaTeammemberCore.Instance.SetEquip(managerId, teammemberId, itemId,package);

            var item = package.GetItem(itemId);
            if (item == null)
                return ResponseHelper.Create<TeammemberResponse>(MessageCode.ItemNotExists);

            #endregion

            int mod = ShareUtil.GetTableMod(managerId);
            var teammember = GetTeammember(managerId, teammemberId);

            try
            {
                //删除要穿上的装备
                var code = package.Delete(item);
                if (code!=MessageCode.Success)
                    return ResponseHelper.Create<TeammemberResponse>(code);
                TeammemberResponse response = ResponseHelper.Create<TeammemberResponse>(MessageCode.Success);
                var itemDic = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                //装备
                if (itemDic.ItemType == (int)EnumItemType.Equipment)
                {
                    if (teammember != null)
                    {
                        if (teammember.Equipment != null)
                        {
                            var result = package.AddUsedItem(teammember.Equipment);
                            if (result != MessageCode.Success)
                            {
                                return ResponseHelper.Create<TeammemberResponse>(result);
                            }
                        }
                        var newEquip = new EquipmentUsedEntity(item);
                        cardProperty.Equipment = newEquip;
                        code = package.Update(playerCardItem);
                        if (code != MessageCode.Success)
                            return ResponseHelper.Create<TeammemberResponse>(code);

                        response = SetEquipment(teammember, package, item, mod);
                    }
                    else
                    {
                        if (cardProperty.Equipment != null)
                        {
                            var result = package.AddUsedItem(cardProperty.Equipment);
                            if (result != MessageCode.Success)
                            {
                                return ResponseHelper.Create<TeammemberResponse>(result);
                            }
                        }
                        var newEquip = new EquipmentUsedEntity(item);
                        cardProperty.Equipment = newEquip;
                        code = package.Update(playerCardItem);
                        if (code != MessageCode.Success)
                            return ResponseHelper.Create<TeammemberResponse>(code);
                        response = ResponseHelper.Create<TeammemberResponse>(MessageCode.Success);
                    }
                    
                }
                if (response.Code == ShareUtil.SuccessCode)
                {
                    if (teammember != null)
                    {
                        KpiHandler.Instance.RebuildKpi(managerId, true);
                        package.Shadow.AddShadow(teammember, EnumOperationType.Update);
                        package.Shadow.Save();
                    }
                    else
                    {
                        if (package.Save())
                            package.Shadow.Save();
                    }
                   

                    if (response.Data == null)
                        response.Data = new TeammemberEntity();
                    response.Data.TotalKpi = ManagerCore.Instance.GetKpi(managerId);
                    response.Data.Package = ItemCore.Instance.BuildPackageData(package);
                }
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SetEquip", ex);
                return ResponseHelper.Create<TeammemberResponse>(MessageCode.Exception);
            }
        }

        TeammemberResponse SetEquipment(TeammemberEntity teammember, ItemPackageFrame package, ItemInfoEntity item, int mod)
        {
            var newEquip = new EquipmentUsedEntity(item);
            var newEquipData = SerializationHelper.ToByte(newEquip);
            return SaveSetEquipment(teammember, package, newEquipData, mod);
        }

        TeammemberResponse SaveSetEquipment(TeammemberEntity teammember, ItemPackageFrame package,
                                                     byte[] newEquipData, int mod)
        {
            int returnCode = 0;
            string errorMessage = "";
            TeammemberMgr.SetEquipment(teammember.Idx, teammember.ManagerId, mod, newEquipData, package.NewItemString,
                                       package.RowVersion, ref returnCode, ref errorMessage);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                SystemlogMgr.Error("SaveSetEquipment", errorMessage);
            }
            if (returnCode == ShareUtil.SuccessCode)
            {
                teammember.UsedEquipment = newEquipData;
                return BuildTeammemberResponse(teammember.ManagerId,teammember.Idx,true);
            }
            else
            {
                return ResponseHelper.Create<TeammemberResponse>(returnCode);
            }
        }

        /// <summary>
        /// Removes the equip.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <param name="teammemberId">The teammember id.</param>
        /// <returns></returns>
        public TeammemberResponse RemoveEquipment(Guid managerId, Guid teammemberId)
        {
            #region Check

            //if (teammember == null
            //    || teammember.ManagerId != managerId)
            //    return ResponseHelper.InvalidParameter<TeammemberResponse>();
            //if (teammember.UsedEquipment == null)
            //    return ResponseHelper.InvalidParameter<TeammemberResponse>();


            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TeammemberRemoveEquip);
            if (package == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var playerCardItem = package.GetPlayer(teammemberId);
            if (playerCardItem == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var cardProperty = playerCardItem.ItemProperty as PlayerCardProperty;
            if (cardProperty == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            var equipEntity = cardProperty.Equipment;
            if (equipEntity == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();

            //在竞技场的阵型上
            if (cardProperty.IsMain && cardProperty.MainType > 0)
                return ArenaTeammemberCore.Instance.RemoveEquipment(managerId, teammemberId, package);

            #endregion

            int mod = ShareUtil.GetTableMod(managerId);
            var teammember = GetTeammember(managerId, teammemberId);

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
                teammember.UsedEquipment = new byte[0];
                response = SaveSetEquipment(teammember, package, teammember.UsedEquipment, mod);
            }
            else
            {
                response = ResponseHelper.Create<TeammemberResponse>(MessageCode.Success);
            }
            if (response.Code == ShareUtil.SuccessCode)
            {
                if (package.Save())
                    package.Shadow.Save();
                if (teammember != null)
                {
                    KpiHandler.Instance.RebuildKpi(managerId, false);
                    response.Data.TotalKpi = ManagerCore.Instance.GetKpi(managerId);
                    package.Shadow.AddShadow(teammember, EnumOperationType.Update);
                    package.Shadow.Save();
                    response.Data.Package = ItemCore.Instance.BuildPackageData(package);
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
                }
            }
            return response;
        }

        #endregion

        #region Formation
        public NbSolutionEntity GetSolution(Guid managerId)
        {
            return MatchDataHelper.GetSolution(managerId);
        }

        /// <summary>
        /// 获取阵型列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public FormationListResponse GetFormationList(Guid managerId)
        {
            var solution = GetSolution(managerId);
            var formationData = new FormationDataListEntity(solution.FormationData);
            var formationList = CacheFactory.FormationCache.GetFormationList();

            var response = ResponseHelper.CreateSuccess<FormationListResponse>();
            response.Data = new FormationList();
            response.Data.CurrentFormationId = solution.FormationId;
            response.Data.Formations = new List<FormationOutEntity>(formationList.Count);
            var manager = ManagerCore.Instance.GetManager(managerId, true);
            if(manager!=null)
                response.Data.Kpi = manager.Kpi;
            foreach (var entity in formationList)
            {
                int level = formationData.GetLevel(entity.Idx);
                int sophisticate = CacheFactory.FormationCache.GetSophisticate(level);
                var outEntity = new FormationOutEntity(entity.Idx, level, sophisticate);
                response.Data.Formations.Add(outEntity);
            }
            return response;
        }

        /// <summary>
        /// 设置阵型id
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="formationId"></param>
        /// <returns></returns>
        public SetFormationResponse SetFormation(Guid managerId, int formationId)
        {
            var response = new SetFormationResponse();
            response.Data = new Entity.SetFormation();
            var solution = GetSolution(managerId);
            solution.FormationId = formationId;
            if (NbSolutionMgr.Update(solution))
            {
                MatchDataCacheHelper.DeleteSolutionCache(managerId, true);
                response.Data.SolutionInfo = SolutionAndTeammemberResponse(managerId).Data;
                response.Data.Package = ItemCore.Instance.GetPackageResponse(managerId).Data;
            }
            else
            {
                return ResponseHelper.Create<SetFormationResponse>(MessageCode.NbUpdateFail);
            }
            return response;
        }

        #endregion

        #endregion

        #region encapsulation

        void DeleteTeamembersCache(Guid managerId,bool isSync)
        {
            MatchDataCacheHelper.DeleteTeamembersCache(managerId, isSync);
        }

        void DeleteSolutionCache(Guid managerId, bool isSync)
        {
            MatchDataCacheHelper.DeleteSolutionCache(managerId, isSync);
        }

        public TeammemberResponse BuildTeammemberResponse(Guid managerId, Guid teammemberId, bool deleteCache = false)
        {
            if (deleteCache)
                DeleteTeamembersCache(managerId, true);
            var teammember = GetTeammember(managerId, teammemberId, true);
            if (teammember == null)
                return ResponseHelper.InvalidParameter<TeammemberResponse>();
            var response = ResponseHelper.CreateSuccess<TeammemberResponse>();
            var dicPlayer = CacheFactory.PlayersdicCache.GetPlayer(teammember.PlayerId);
            if (dicPlayer != null)
            {
                teammember.BaseProperty = new TeammemberPropertyEntity(dicPlayer);
            }
            else
            {
                teammember.BaseProperty = new TeammemberPropertyEntity();
            }
            response.Data = teammember;
            return response;
        }

        /// <summary>
        /// 调换球员位置
        /// </summary>
        /// <param name="playerIdList">球员串</param>
        /// <param name="pid">要换上的球员id</param>
        /// <param name="bypid">被换的球员ID</param>
        /// <param name="isSubstitution">是否是替补席换的</param>
        /// <param name="playerString">输出球员串</param>
        /// <returns></returns>
        public MessageCode ExchangePlayer(List<int> playerIdList, int pid, int bypid, bool isSubstitution,ref string playerString)
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

    }
}
