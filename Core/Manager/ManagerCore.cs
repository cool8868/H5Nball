using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Games.Dpm.Core.Activity;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Activity;
using Games.NBall.Core.FriendShip;
using Games.NBall.Core.Item;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Online;
using Games.NBall.Core.SkillCard;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.A8csdk;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Manager;
using Games.NBall.ServiceEngine;
using Games.NBall.WebClient.Util;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Manager
{
    /// <summary>
    /// 经理相关对外主逻辑
    /// </summary>
    public partial class ManagerCore : BaseDomain
    {
        private readonly int _registerstamina;
        private readonly int _registerpackageSize;
        private readonly int _registerLadderScore;
        private readonly int _registerSubstitute;
        private readonly int _registerFormationId;
        private readonly int _registerTrainSeat;
        private readonly int _resumeStaminaTimeConfig;
        private readonly int _resumeStaminaAccelerateVip;
        private readonly int _resumeStaminaCount;
        private readonly int _registerSendCoin;
        private readonly int _registerSendPoint;
        private readonly int _scoutingFellNumber;

        private readonly Dictionary<int, int> _staminaGiftTimeDic;
        #region .ctor

        public ManagerCore(int p)
        {
            _registerstamina = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterStamina);
            _registerpackageSize = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterPackageSize);
            _registerLadderScore = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterLadderScore);
            _registerSubstitute = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterSubstitute);
            _registerFormationId = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterFormationId);
            _registerTrainSeat = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterTrainSeat);
            _resumeStaminaCount = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ResumeStaminaCount);
            _resumeStaminaTimeConfig =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ResumeStaminaTimeConfig);
            _resumeStaminaAccelerateVip =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ResumeStaminaAccelerateVip);
            _registerSendCoin =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterSendCoin);
            _registerSendPoint =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterSendPoint);
            _scoutingFellNumber = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ScoutingFellNumber);
            var staminaGiftTimes = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.StaminaGiftTimes);
            _staminaGiftTimeDic = FrameUtil.CastIntDic(staminaGiftTimes, ',');
        }

        #endregion

        public int ResumeStaminaCount
        {
            get { return _resumeStaminaCount; }
        }

        public int ResumeStaminaTimeConfig
        {
            get { return _resumeStaminaTimeConfig; }
        }

        public int GetResumeStaminaTimeConfig(int vipLevel)
        {
            if (vipLevel >= _resumeStaminaAccelerateVip)
                return _resumeStaminaTimeConfig/2;
            return _resumeStaminaTimeConfig;
        }

        public int ResumeStaminaAccelerateVip
        {
            get { return _resumeStaminaAccelerateVip; }
        }

        #region Facade

        public static ManagerCore Instance
        {
            get { return SingletonFactory<ManagerCore>.SInstance; }
        }

        #region GetRegisterSolution

        public TemplateRegisterResponse GetRegisterSolution()
        {
            var entity = CacheFactory.TemplateCache.GetRandom();
            entity.FormationId = _registerFormationId;
            var response = ResponseHelper.CreateSuccess<TemplateRegisterResponse>();
            response.Data = entity;
            return response;
        }

        #endregion

        #region CreateManager

        /// <summary>
        /// 预创建角色
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="name"></param>
        /// <param name="returnCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CreateManager(Guid managerId, string name, ref int returnCode, ref string errorMessage)
        {
            var itemVersion = SystemConstants.CurItemVersion;

            int teammemberMax = SystemConstants.TeammemberCount + _registerSubstitute;

            return NbManagerMgr.Create(managerId, ShareUtil.GetTableMod(managerId), name, _registerstamina,
                _registerpackageSize, itemVersion, teammemberMax, _registerTrainSeat, _registerLadderScore
                , (int)MessageCode.RegisterNameRepeat, (int)MessageCode.RegisterExistsManager, ref errorMessage,
                ref returnCode);
        }

        /// <summary>
        /// 预创建角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public NBManagerCreateResponse CreateManager(string name)
        {
            //check name 
            int exists = -1;
            NbManagerMgr.NameExists(name, ref exists);
            if (exists != 0)
            {
                return ResponseHelper.Create<NBManagerCreateResponse>(MessageCode.RegisterNameRepeat);
            }

            Guid managerId = ShareUtil.GenerateComb();

            int returnCode = -2;
            string errorMessage = "";
            CreateManager(managerId, name, ref returnCode, ref errorMessage);


            if (returnCode == 0)
            {
                var response = ResponseHelper.CreateSuccess<NBManagerCreateResponse>();
                response.Data = managerId;
                return response;
            }
            else
            {
                return ResponseHelper.HandleTransactionError<NBManagerCreateResponse>("CreateManager", returnCode,
                    errorMessage);
            }
        }


        #endregion

        #region RegisterManager

        /// <summary>
        /// 注册角色
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <param name="area"></param>
        /// <param name="logo"></param>
        /// <param name="isBot"></param>
        /// <param name="templateId"></param>
        /// <param name="playerString"></param>
        /// <param name="registerFormationId"></param>
        /// <param name="mod"></param>
        /// <param name="managerId"></param>
        /// <param name="returnCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool RegisterManager(string account, string name, string logo, bool isBot,
            int templateId, string playerString, int registerFormationId, int kpi,
            ref Guid managerId, ref int returnCode, ref string errorMessage)
        {
            return NbManagerMgr.Register(account, name, logo, templateId, playerString, registerFormationId, kpi,
                (int)MessageCode.RegisterNameRepeat,
                (int)MessageCode.RegisterExistsManager, (int)MessageCode.RegisterFail, isBot,
                ref managerId, ref returnCode, ref errorMessage);
        }

        /// <summary>
        /// 注册角色
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <param name="logo"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public NBManagerCreateResponse RegisterManager(string account, string name, string logo, int templateId,
            string userIp)
        {
            return RegisterManager(account, name, logo, templateId, false, userIp);
        }

        /// <summary>
        /// 注册角色
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <param name="area"></param>
        /// <param name="logo"></param>
        /// <param name="templateId"></param>
        /// <param name="isBot"></param>
        /// <returns></returns>
        public NBManagerCreateResponse RegisterManager(string account, string name, string logo, int templateId,
            bool isBot, string userIp)
        {
            var user = NbUserMgr.GetById(account);
            if (user == null)
            {
                return ResponseHelper.Create<NBManagerCreateResponse>(MessageCode.LoginNoUser);
            }
            //check name 
            //var messageCode = ManagerUtil.CheckUserName(name, logo);
            //if (messageCode != MessageCode.Success)
            //{
            //    return ResponseHelper.Create<NBManagerCreateResponse>(messageCode);
            //}
            var template = CacheFactory.TemplateCache.GetEntity(templateId);
            if (template == null)
                return ResponseHelper.InvalidParameter<NBManagerCreateResponse>();
            int returnCode = -2;
            Guid managerId = Guid.Empty;
            string errorMessage = "";
            RegisterManager(account, name, logo, isBot, template.Idx, template.SolutionString, _registerFormationId,
                template.Kpi, ref managerId, ref returnCode, ref errorMessage);
            if (returnCode != 0)
            {
                return ResponseHelper.Create<NBManagerCreateResponse>(returnCode);
            }
            var teammembers = MatchDataHelper.GetTeammembers(managerId);
            //注册完角色后将生成初始化
            //球员列表
            var playerIdList = FrameUtil.CastIntList(template.SolutionString, ',');
            ItemPackageFrame package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.TeammemberInit);
            foreach (var playerId in playerIdList)
            {
                var item = ItemsdicCache.Instance.GetItemByPlayerId(playerId);
                var teammemberId = teammembers.Find(t => t.PlayerId == playerId);
                //主力球员卡
                package.AddPlayerCard(item.ItemCode, teammemberId.Idx);
            }
            if (ShareUtil.IsTx)
                package.AddItem(310180);
            if (package.Save())
                package.Shadow.Save();
            if (ShareUtil.ZoneName == "a8s1")//A8内测区 发放封测充值的点卷
            {
                SendPackagingPrize(managerId,account);
            }

            if (returnCode == 0)
            {
                if (!isBot)
                {
                    SetOnlineData(managerId, userIp, account);
                    //if (_registerSendCoin > 0)
                    //{
                    //    AdminCore.Instance.AddCoin(managerId, _registerSendCoin);
                    //}
                    //if (_registerSendPoint > 0)
                    //{
                    //    AdminCore.Instance.Charge(managerId, account, (int)EnumChargeSourceType.BetaSend,
                    //        _registerSendPoint);
                    //}
                    if (_registerSendPoint > 0)
                    {
                        PayCore.Instance.AddBonus(managerId, _registerSendPoint, EnumChargeSourceType.BetaSend,
                            ShareUtil.GenerateComb().ToString());
                    }
                    ActivityExThread.Instance.TememberColect(managerId, (int)EnumPlayerCardLevel.Orange,
                     template.OrangeCount);
                }
                var response = ResponseHelper.CreateSuccess<NBManagerCreateResponse>();
                response.Data = managerId;
                return response;
            }
            else
            {
                return ResponseHelper.HandleTransactionError<NBManagerCreateResponse>("RegisterManager", returnCode,
                    errorMessage);
            }
        }

        /// <summary>
        /// 发送封测奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCode SendPackagingPrize(Guid managerId,string account)
        {
            try
            {
                List<ConfigMallgiftbagEntity> prizeList = new List<ConfigMallgiftbagEntity>();
                var s0manager = NbManagerMgr.GetByAccount(account,"a8s0");
                if (s0manager == null || s0manager.Count ==0)
                    return MessageCode.Success;
                var managerChargeList = ManagerChargenumberMgr.GetManagerIdList(s0manager[0].Idx, "a8s0");
                int bonus = 0;
                DateTime date = DateTime.Now;
                foreach (var item in managerChargeList)
                {
                    var mallItem = CacheFactory.MallCache.GetMallEntityWithoutPoint(item.MallCode);
                    if (mallItem == null)
                        continue;
                    if (mallItem.MallType == (int) EnumMallType.QP)
                    {
                        if (mallItem.EffectType == (int) EnumMallEffectType.MonthCard)
                        {
                            var monthcardEntity = ManagerMonthcardMgr.GetById(managerId);
                            if (monthcardEntity == null)
                            {
                                monthcardEntity = new ManagerMonthcardEntity(managerId, 1, date, date.AddDays(30),
                                    date.AddDays(-1),
                                    date, date);
                                ManagerMonthcardMgr.Insert(monthcardEntity);
                            }
                        }
                        bonus += (mallItem.CurrencyCount*item.BuyNumber*2);
                    }
                    else if (mallItem.MallType == (int) EnumMallType.Mystery)
                    {
                        var list = CacheFactory.MallCache.GetMallGiftBagPrize(mallItem.MallCode);
                        prizeList.AddRange(list);
                    }
                }
                if (bonus > 0 || prizeList.Count > 0)
                {
                    var mail = new MailBuilder(managerId, EnumMailType.PackagingRebate, bonus, prizeList);
                    mail.Save();
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SendPackagingPrize", managerId + "__" + ex);
            }
            return MessageCode.Success;
        }

        public MessageCodeResponse UpdateName(Guid managerId, string name)
        {
            var manager = GetManager(managerId);
            if (manager == null)
                return ResponseHelper.Create<MessageCodeResponse>((int) MessageCode.NbParameterError);
            if (manager.Idx.ToString().ToLower() != manager.Name.ToLower())
                return CardUpdateName(manager, name);
            var messageCode = ManagerUtil.CheckUserName(name, "1");
            if (messageCode != MessageCode.Success)
                return ResponseHelper.Create<MessageCodeResponse>((int)messageCode);
            manager.Name = name;
            if (!NbManagerMgr.Update(manager))
                return ResponseHelper.Create<MessageCodeResponse>((int) MessageCode.NbUpdateFail);
            ManagerCore.Instance.DeleteCache(managerId);
            return ResponseHelper.Create<MessageCodeResponse>((int) MessageCode.Success);
        }

        public MessageCodeResponse CardUpdateName(NbManagerEntity manager, string name)
        {
            if (manager.Name == name)
                return ResponseHelper.Create<MessageCodeResponse>((int)MessageCode.NbUpdateFail);
            var package = ItemCore.Instance.GetPackage(manager.Idx, EnumTransactionType.UpdateLogo);
            if (package == null)
                return ResponseHelper.Create<MessageCodeResponse>((int) MessageCode.NbNoPackage);
            //获取更名卡数量
            var itemCount = package.GetItemNumber(310180);
            if (itemCount <= 0)
                return ResponseHelper.Create<MessageCodeResponse>((int)MessageCode.UpdateNameCardNot);
            var messageCode = package.Delete(310180, 1);
            if (messageCode != MessageCode.Success)
                return ResponseHelper.Create<MessageCodeResponse>((int)messageCode);

            manager.Name = name;
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                messageCode = MessageCode.NbUpdateFail;
                do
                {
                    if (!package.Save(transactionManager.TransactionObject))
                        break;
                    if (!NbManagerMgr.Update(manager, transactionManager.TransactionObject))
                        break;
                    messageCode = MessageCode.Success;
                } while (false);
                if (messageCode == MessageCode.Success)
                {
                    transactionManager.Commit();
                    package.Shadow.Save();
                    DeleteCache(manager.Idx);
                    CrossladderManagerEntity ladder = CrossladderManagerMgr.GetById(manager.Idx);
                    CrosscrowdManagerEntity crowd = CrosscrowdManagerMgr.GetById(manager.Idx);
                    ArenaManagerinfoEntity arena = ArenaManagerinfoMgr.GetById(manager.Idx);
                    if (ladder != null)
                    {
                        ladder.Name = name;
                        CrossladderManagerMgr.Update(ladder);
                    }
                    if (crowd != null)
                    {
                        crowd.Name = name;
                        CrosscrowdManagerMgr.Update(crowd);
                    }
                    if (arena != null)
                    {
                        arena.ManagerName = name;
                        ArenaManagerinfoMgr.Update(arena);
                    }
                }
                else
                {
                    transactionManager.Rollback();
                }
            }
            return ResponseHelper.Create<MessageCodeResponse>((int)messageCode);
        }

        /// <summary>
        /// 获取用户创建角色信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="siteId"></param>
        /// <param name="kgext"></param>
        /// <returns></returns>
        public GetRegisterManagerResponse GetRegisterManager(Guid managerId, string siteId, string kgext)
        {
            GetRegisterManagerResponse response = new GetRegisterManagerResponse();
            try
            {
                var manger = GetManager(managerId);
                if (manger == null)
                    return ResponseHelper.Create<GetRegisterManagerResponse>((int)MessageCode.MissManager);
                response.Data = new RegisterManagerInfo();
                response.Data.RoleId = managerId.ToString();
                response.Data.RoleName = manger.Name;
                response.Data.ServerId = siteId;
                response.Data.UId = manger.Account;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取用户创建角色信息", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #region GetUserByAccount

        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="userIp"></param>
        /// <returns></returns>
        public NbUserResponse GetUserByAccount(string account, string userIp, int status = 0)
        {
            var user = NbUserMgr.GetByAccount(account, userIp, DateTime.Today.AddDays(-1), DateTime.Today, status);
            if (user == null)
            {
                return ResponseHelper.Exception<NbUserResponse>();
            }
            else
            {
                var response = ResponseHelper.CreateSuccess<NbUserResponse>();
                response.Data = user;
                return response;
            }
        }

        #endregion

        #region GetKpi

        /// <summary>
        /// 获取经理综合实力
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public int GetKpi(Guid managerId)
        {
            var manager = GetManager(managerId, true);
            if (manager == null)
                return 0;
            return manager.Kpi;
        }

        #endregion

        #region GetManagerInfo

        public string GetName(Guid managerId)
        {
            var manager = GetManager(managerId);
            if (manager != null)
                return manager.Name;
            else
            {
                return "";
            }
        }



        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="containKpi">是否包含综合实力</param>
        /// <returns></returns>
        public NbManagerEntity GetManager(Guid managerId, bool containKpi = false, string siteId = "")
        {
            return MatchDataHelper.GetManager(managerId, containKpi, false, siteId);
        }
        public NbManagerEntity GetManager(Guid managerId, string siteId)
        {
            return MatchDataHelper.GetManager(managerId, false, false, siteId);
        }
        public NbManagerextraEntity GetManagerExtra(Guid managerId)
        {
            return InnerGetManagerExtra(managerId);
        }

        public NbManagerEntity GetManager(string account, string siteId = "")
        {
            var managerList = NbManagerMgr.GetByAccount(account, siteId);
            if (managerList == null || managerList.Count <= 0)
            {
                return null;
            }
            return managerList[0];
        }

        public NbManagerEntity GetManagerByName(string name)
        {
            
            return NbManagerMgr.GetByName(name);
        }

        /// <summary>
        /// 通过绑定码，把其他账号的角色复制到本账号的角色
        /// </summary>
        /// <param name="bindCode">绑定码</param>
        /// <param name="account">账号</param>
        /// <param name="name">经理名</param>
        /// <param name="managerId">经理ID</param>
        /// <param name="mod">Mod</param>
        /// <returns>是否复制成功</returns>
        public MessageCodeResponse BindAccount(Guid bindCode, string account, string name, Guid managerId)
        {
            int returnCode = 0;
            int mod = ShareUtil.GetTableMod(managerId);
            NbManagerMgr.BindAccount(account, name, managerId.ToString(), mod.ToString(), bindCode, ref returnCode);
            //清缓存
            if (returnCode == 0)
            {
                DeleteCache(managerId);
                ManagerUtil.DeleteOpenFunctionCache(managerId);

                MemcachedFactory.TeammembersClient.Delete(managerId);
                MemcachedFactory.SolutionClient.Delete(managerId);
            }
            return ResponseHelper.Create<MessageCodeResponse>(returnCode);
        }

        /// <summary>
        /// 删除角色--合区使用
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="bindcode">绑定码</param>
        /// <returns>是否删除成功</returns>
        public MessageCodeResponse DeleteRole(string account, Guid bindcode)
        {
            NbManagerMgr.DeleteRole(account, bindcode);

            return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
        }

        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public NBManagerListResponse GetManagerInfoByAccount(string account, string userIp, bool isTx)
        {
            var managerList = NbManagerMgr.GetByAccount(account);
            if (managerList == null || managerList.Count <= 0)
            {
                var response = RegisterManager(account, "", "1", 1, false, userIp);
                if (response.Code != (int) MessageCode.Success)
                    return ResponseHelper.Create<NBManagerListResponse>(response.Code);
                managerList = NbManagerMgr.GetByAccount(account);
            }
            List<AMovedatatoserverecordEntity> mdsrList = AMovedatatoserverecordMgr.GetDataByAccount(account);

            if (mdsrList == null || mdsrList.Count == 0)
            {
                var response = ResponseHelper.CreateSuccess<NBManagerListResponse>();
                response.Data = new NBManagerListEntity();
                if (managerList[0].Status == 100)
                {
                    response.Data.CharacterList.Add(new NBCharacterEntity() { ManagerId = managerList[0].Idx, Name = managerList[0].Name, Level = managerList[0].Level, IsNeedChangeName = true, OldZoneName = string.Empty, BindCode = String.Empty });
                    try
                    {
                        response.Data.ManagerInfo.Manager.IsYellowVip = CSDKinterface.Instance.IsTxVip(response.Data.ManagerInfo.Manager.Account);

                    }
                    catch (Exception ex)
                    {
                        
                    }
                    return response;
                }
                var manager = MatchDataHelper.GetManager(managerList[0].Idx, true, true);
                var code = SetOnlineData(manager.Idx, userIp, account);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<NBManagerListResponse>(code);
                DeleteCache(manager.Idx);
                ManagerUtil.DeleteOpenFunctionCache(manager.Idx);

                MemcachedFactory.TeammembersClient.Delete(manager.Idx);
                MemcachedFactory.SolutionClient.Delete(manager.Idx);

                response.Data.ManagerInfo = BuildManagerInfoData(manager);
                try
                {
                    response.Data.ManagerInfo.Manager.IsYellowVip = CSDKinterface.Instance.IsTxVip(response.Data.ManagerInfo.Manager.Account);

                }
                catch (Exception ex)
                {

                }
                return response;
            }
            else
            {
                var response = ResponseHelper.CreateSuccess<NBManagerListResponse>();

                response.Data = new NBManagerListEntity();
                response.Data.NeedSelect = true;
                response.Data.CharacterList = new List<NBCharacterEntity>(mdsrList.Count + 1);
                if (managerList[0].Status != 99)
                {
                    if (managerList[0].Status == 100)
                    {
                        response.Data.CharacterList.Add(new NBCharacterEntity() { ManagerId = managerList[0].Idx, Name = managerList[0].Name, Level = managerList[0].Level, IsNeedChangeName = true, OldZoneName = string.Empty, BindCode = String.Empty });
                    }
                    else
                    {
                        response.Data.CharacterList.Add(new NBCharacterEntity() { ManagerId = managerList[0].Idx, Name = managerList[0].Name, Level = managerList[0].Level, IsNeedChangeName = false, OldZoneName = string.Empty, BindCode = String.Empty });
                    }
                }

                foreach (var entity in mdsrList)
                {
                    response.Data.CharacterList.Add(new NBCharacterEntity() { ManagerId = entity.ManagerId, Name = entity.Name, Level = entity.Level, IsNeedChangeName = false, OldZoneName = entity.OldZoneName, BindCode = entity.BindCode.ToString() });
                    try
                    {
                        response.Data.ManagerInfo.Manager.IsYellowVip = CSDKinterface.Instance.IsTxVip(response.Data.ManagerInfo.Manager.Account);

                    }
                    catch (Exception ex)
                    {

                    }
                }
                if (response.Data.CharacterList.Count < 1)
                {
                    response = null;
                    return ResponseHelper.Create<NBManagerListResponse>(MessageCode.LoginOnlineLock);
                }
                return response;
            }
        }



        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public NBManagerInfoResponse SelectManager(string account, Guid managerId, string userIp, bool isTx)
        {
            var manager = MatchDataHelper.GetManager(managerId, true, true);
            if (manager == null)
                return ResponseHelper.Create<NBManagerInfoResponse>(MessageCode.NbParameterError);

            var code = SetOnlineData(manager.Idx, userIp, account);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<NBManagerInfoResponse>(code);
            DeleteCache(manager.Idx);
            ManagerUtil.DeleteOpenFunctionCache(manager.Idx);

            MemcachedFactory.TeammembersClient.Delete(manager.Idx);
            MemcachedFactory.SolutionClient.Delete(manager.Idx);
            var guild = GuildListMgr.GetGuildByManager(manager.Idx);
            return BuildManagerInfoResponse(manager, guild);
        }

        private MessageCode SetOnlineData(Guid managerId, string userIp, string account)
        {
            try
            {
                var onlineEntity = OnlineInfoMgr.GetById(managerId);
                if (onlineEntity == null)
                {
                    onlineEntity = new OnlineInfoEntity(managerId, DateTime.Now);
                    onlineEntity.LoginIp = userIp;
                    if (!OnlineInfoMgr.Insert(onlineEntity))
                    {
                        return MessageCode.NbUpdateFail;
                    }
                }
                else
                {
                    OnlineInfoMgr.Login(managerId, userIp, DateTime.Today);
                    if (CacheFactory.FunctionAppCache.CheckOpenIndulge(ShareUtil.ZoneName))
                    {
                        var compareTime = DateTime.Now.AddHours(-5);
                        if (onlineEntity.TodayMinutes > 0 && onlineEntity.ActiveTime < compareTime)
                        {
                            var userReg = NbUserregMgr.GetById(account);
                            if (userReg != null)
                            {
                                userReg.RecordDate = DateTime.Today;
                                userReg.LastOnlineMinutes = onlineEntity.TodayMinutes;
                                NbUserregMgr.Update(userReg);
                            }
                        }
                    }
                    ShadowMgr.SaveOnlineHistory(onlineEntity);
                }

                //if (onlineEntity.TodayMinutes <= 0)
                //    ActivityExThread.Instance.Login(managerId);

                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SetOnlineData", ex);
                return MessageCode.Exception;
            }

        }

        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public NBManagerInfoResponse GetManagerInfo(Guid managerId, bool isTx)
        {
            var manager = MatchDataHelper.GetManager(managerId, true, true);
            if (manager == null)
                return ResponseHelper.Create<NBManagerInfoResponse>(MessageCode.NbParameterError);
            else
            {
                manager.IsYellowVip = CSDKinterface.Instance.IsTxVip(manager);
                return BuildManagerInfoResponse(manager);
            }
        }

        #endregion

        #region GetManagerDetail

        public ManagerDetailInfoResponse GetManagerDetailInfo(Guid managerId, string siteId = "")
        {
            var detail = MatchDataHelper.GetManagerDetail(managerId, siteId);
            if (detail == null)
                return ResponseHelper.InvalidParameter<ManagerDetailInfoResponse>();
            var guild = GuildListMgr.GetGuildByManager(managerId, siteId);
            var response = ResponseHelper.CreateSuccess<ManagerDetailInfoResponse>();
            response.Data = detail;
            response.Data.ArenaRank = 0;
            response.Data.LadderRank = LadderCore.Instance.GetLadderRank(managerId);
            response.Data.GuildName = null == guild ? "" : guild.GuildName;
            response.Data.Score = detail.Score;
            response.Data.Honors = NbManagerhonorMgr.GetByManagerTop(managerId, siteId);
            response.Data.Matchstats = NbMatchstatMgr.GetByManager(managerId, siteId);
            return response;
        }

        #endregion

        #region GetManagerHonorList

        public ManagerHonorListResponse GetManagerHonorList(Guid managerId)
        {
            var response = ResponseHelper.CreateSuccess<ManagerHonorListResponse>();
            response.Data = new ManagerHonorListEntity();
            response.Data.Honors = NbManagerhonorMgr.GetByManager(managerId);
            return response;
        }

        #endregion

        #region Stamina

        public ManagerStaminaResponse ResumeStamina(Guid managerId)
        {
            var response = ResponseHelper.CreateSuccess<ManagerStaminaResponse>();
            response.Data = new ManagerStaminaEntity();

            var extra = InnerGetManagerExtra(managerId);
            var manager = GetManager(managerId);
            ManagerUtil.CalCurrentStamina(extra, manager.Level,manager.VipLevel);
            if (!NbManagerextraMgr.Update(extra))
            {
                return ResponseHelper.Create<ManagerStaminaResponse>(MessageCode.NbUpdateFail);
            }
            response.Data.ResumeStaminaTimeTick =
                ShareUtil.GetTimeTick(extra.ResumeStaminaTime.AddSeconds(GetResumeStaminaTimeConfig(manager.VipLevel)));
            response.Data.Stamina = extra.Stamina;
            response.Data.StaminaMax = extra.StaminaMax;
            return response;
        }

        public ManagerStaminaResponse GiftStamina(Guid managerId)
        {
            var curHour = DateTime.Now.Hour;
            var extra = InnerGetManagerExtra(managerId);
            var manager = GetManager(managerId);
            ManagerUtil.CalCurrentStamina(extra, manager.Level,manager.VipLevel);
            if (extra.Stamina >= extra.StaminaMax)
            {
                return ResponseHelper.Create<ManagerStaminaResponse>(MessageCode.MallStaminaOver);
            }
            if (!_staminaGiftTimeDic.ContainsKey(curHour))
            {
                return ResponseHelper.Create<ManagerStaminaResponse>(MessageCode.MallStaminaGiftNoTime);
            }
            var giftStatus = _staminaGiftTimeDic[curHour];
            if (extra.StaminaGiftStatus == giftStatus)
                return ResponseHelper.Create<ManagerStaminaResponse>(MessageCode.NbPrizeRepeat);
            extra.Stamina += 50;
            if (extra.Stamina > extra.StaminaMax)
                extra.Stamina = extra.StaminaMax;
            extra.StaminaGiftStatus = giftStatus;
            if (!NbManagerextraMgr.Update(extra))
            {
                return ResponseHelper.Create<ManagerStaminaResponse>(MessageCode.NbUpdateFail);
            }
            var response = ResponseHelper.CreateSuccess<ManagerStaminaResponse>();
            response.Data = new ManagerStaminaEntity();
            response.Data.ResumeStaminaTimeTick =
                ShareUtil.GetTimeTick(extra.ResumeStaminaTime.AddSeconds(GetResumeStaminaTimeConfig(manager.VipLevel)));
            response.Data.Stamina = extra.Stamina;
            response.Data.StaminaMax = extra.StaminaMax;
            return response;
        }

        /// <summary>
        /// Checks the stamina.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <param name="costStamina">costStamina</param>
        /// <returns></returns>
        public bool CheckStamina(Guid managerId, int costStamina, int level,int vipLevel)
        {
            try
            {
                var managerExtra = InnerGetManagerExtra(managerId);
                return CheckStamina(managerExtra, costStamina, level, vipLevel);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CheckStamina", ex.Message, ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Checks the stamina.
        /// </summary>
        /// <param name="managerExtra">The manager property.</param>
        /// <param name="costStamina">costStamina</param>
        /// <returns></returns>
        public bool CheckStamina(NbManagerextraEntity managerExtra, int costStamina, int level,int vipLevel)
        {
            try
            {
                if (managerExtra == null)
                    return false;
                if (costStamina < 0)
                {
                    return false;
                }

                ManagerUtil.CalCurrentStamina(managerExtra, level,vipLevel);
                if (managerExtra.Stamina < costStamina)
                    return false;
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CheckStamina", ex.Message, ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 扣除行动力，不保存
        /// </summary>
        /// <param name="managerExtra"></param>
        /// <param name="costStamina"></param>
        /// <returns></returns>
        public MessageCode SubStamina(NbManagerextraEntity managerExtra, int costStamina, int level,int vipLevel)
        {
            try
            {
                if (managerExtra == null)
                    return MessageCode.NbParameterError;
                if (costStamina < 0)
                {
                    return MessageCode.NbParameterError;
                }
                ManagerUtil.CalCurrentStamina(managerExtra, level,vipLevel);
                int newStamina = managerExtra.Stamina - costStamina;
                if (newStamina < 0)
                    return MessageCode.NbStaminaShortage;
                managerExtra.Stamina = newStamina;
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SubStamina", ex.Message, ex.StackTrace);
                return MessageCode.Exception;
            }
        }

        #endregion

        #region VipData

        public VipDataResponse GetVipData(Guid managerId)
        {
            var manager = GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<VipDataResponse>();
           // var payUser = PayUserMgr.GetById(manager.Account);
            var vipManager = VipManagerMgr.GetById(managerId);

            var response = ResponseHelper.CreateSuccess<VipDataResponse>();
            response.Data = new VipDataEntity();
            response.Data.VipLevel = manager.VipLevel;
            response.Data.Point = vipManager == null ? 0 : vipManager.VipExp;
            response.Data.LevelUpPoint = CacheFactory.VipdicCache.GetVipLevelupPoint(manager.VipLevel);
            //if (payUser != null)
            //{
            //    response.Data.Point = vipManager.VipExp;// payUser.ChargePoint + vipManager.VipExp;
            //}
            //if (manager.VipLevel > 0)
            //{
            //    var prevLevelPoint = CacheFactory.VipdicCache.GetVipLevelupPoint(manager.VipLevel - 1);
            //    if (prevLevelPoint > 0 && response.Data.Point < prevLevelPoint)
            //    {
            //        var gmPoint = 0;
            //        PayUserMgr.GetGmChargePoint(manager.Account, ref gmPoint);
            //        gmPoint += vipManager.VipExp;
            //        if (gmPoint >= prevLevelPoint)
            //        {
            //            response.Data.Point = gmPoint;
            //        }
            //    }
            //}
            //response.Data.LevelUpPoint = CacheFactory.VipdicCache.GetVipLevelupPoint(manager.VipLevel);
            //if (response.Data.Point >= response.Data.LevelUpPoint)
            //{
            //    var newlevel = CacheFactory.VipdicCache.GetVipLevel(response.Data.Point);
            //    if (newlevel > manager.VipLevel)
            //    {
            //        manager.VipLevel = newlevel;
            //        response.Data.VipLevel = newlevel;
            //        response.Data.LevelUpPoint = CacheFactory.VipdicCache.GetVipLevelupPoint(newlevel);
            //        ManagerUtil.SaveManagerData(manager);
            //        DeleteCache(manager.Idx);
            //    }
            //}
            return response;
        }

        #endregion

        #region coin

        public void UpdateCoinAfter(NbManagerEntity manager)
        {
            if (manager == null)
                return;
            if (manager.AddCoin >0)
            {
               
                ShadowMgr.SaveCoinCharge(manager.Idx, manager.AddCoin, 0, false, -1, manager.CoinSourceType,
                    manager.CoinOrderId);
            }
            else if (manager.AddCoin < 0)
            {
               
                ShadowMgr.SaveCoinConsume(manager.Idx, manager.AddCoin, manager.CoinSourceType, manager.CoinOrderId);
            }
        }

        public MessageCode AddCoin(NbManagerEntity manager, int coin, EnumCoinChargeSourceType coinSourceType,
            string coinOrderId, DbTransaction trans = null)
        {
            return AddCoin(manager, coin, (int)coinSourceType, coinOrderId, trans);
        }
        /// <summary>
        /// 加游戏币
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="coin"></param>
        /// <param name="coinSourceType"></param>
        /// <param name="coinOrderId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public MessageCode AddCoin(NbManagerEntity manager, int coin, int coinSourceType,
            string coinOrderId, DbTransaction trans = null)
        {
            if (coin <= 0 || manager == null)
                return MessageCode.NbParameterError;
            int curCoin = manager.Coin;
            if (NbManagerMgr.AddCoin(manager.Idx, coin, ref curCoin, trans))
            {
                manager.AddCoin += coin;
                manager.CoinSourceType = coinSourceType;
                manager.CoinOrderId = coinOrderId;
                manager.Coin = curCoin;
                DeleteCache(manager.Idx);
                
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbUpdateFail;
            }
        }

        public MessageCode AddCoinV2(out int totalCoin, NbManagerEntity manager, int coin, int coinSourceType,
           string coinOrderId, DbTransaction trans = null)
        {
            totalCoin = -1;
            if (coin <= 0 || manager == null)
                return MessageCode.NbParameterError;
            int curCoin = manager.Coin;
            if (NbManagerMgr.AddCoin(manager.Idx, coin, ref curCoin, trans))
            {
                manager.AddCoin += coin;
                manager.CoinSourceType = coinSourceType;
                manager.CoinOrderId = coinOrderId;
                manager.Coin = curCoin;
                //DeleteCache(manager.Idx);
                totalCoin = curCoin;
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbUpdateFail;
            }
        }

        public MessageCode ClearPackage(Guid managerId, DbTransaction trans = null)
        {
            if (NbManagerMgr.ClearPackage(managerId))
            {
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbUpdateFail;
            }
        }

        public MessageCode AddCoinAndSendMessage(Guid managerId, int coin,
            EnumCoinChargeSourceType coinSourceType, string coinOrderId)
        {
            var manager = GetManager(managerId);
            return AddCoinAndSendMessage(manager, coin, coinSourceType, coinOrderId);
        }

        public MessageCode AddCoinAndSendMessage(NbManagerEntity manager, int coin,
            EnumCoinChargeSourceType coinSourceType, string coinOrderId)
        {
            var code = AddCoin(manager, coin, coinSourceType, coinOrderId);
            if (code == MessageCode.Success)
            {
               // ChatHelper.SendUpdateManagerInfoPop(manager, true, -1);
            }
            return code;
        }

        /// <summary>
        /// 加金币，积分，并保存日志
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="score"></param>
        /// <param name="coin"></param>
        /// <param name="coinSourceType"></param>
        /// <param name="coinOrderId"></param>
        /// <returns></returns>
        public MessageCode AddCoinAndScoreWithShadow(Guid managerId, int score, int coin,
            EnumCoinChargeSourceType coinSourceType, string coinOrderId)
        {
            if (coin <= 0 && score <= 0)
                return MessageCode.NbParameterError;
            var manager = GetManager(managerId);
            if (coin <= 0 || manager == null)
                return MessageCode.NbParameterError;
            if (NbManagerMgr.AddCoinAndScore(manager.Idx, coin, score))
            {
                if (coin > 0)
                {
                    ShadowMgr.SaveCoinCharge(managerId, coin, 0, false, -1, coinSourceType, coinOrderId);
                }
                DeleteCache(manager.Idx);
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbUpdateFail;
            }
        }

        /// <summary>
        /// 扣游戏币
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="coin"></param>
        /// <param name="coinSourceType"></param>
        /// <param name="coinOrderId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public MessageCode CostCoin(NbManagerEntity manager, int coin, EnumCoinConsumeSourceType coinSourceType,
            string coinOrderId, DbTransaction trans = null)
        {
            if (coin <= 0 || manager == null)
                return MessageCode.NbParameterError;
            int returnCode = -2;
            int curCoin = manager.Coin;
            NbManagerMgr.CostCoin(manager.Idx, coin, ref curCoin, ref returnCode, trans);
            if (returnCode == 0)
            {
                manager.AddCoin += (coin * -1);
                manager.CoinSourceType = (int)coinSourceType;
                manager.CoinOrderId = coinOrderId;
                manager.Coin = curCoin;
                DeleteCache(manager.Idx);
                UpdateCoinAfter(manager);
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbCoinShortage;
            }
        }

        /// <summary>
        /// 跨服扣游戏币
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="siteId"></param>
        /// <param name="coin"></param>
        /// <param name="coinSourceType"></param>
        /// <param name="coinOrderId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public MessageCode CrossCostCoin(NbManagerEntity manager, string siteId, int coin, EnumCoinConsumeSourceType coinSourceType,
            string coinOrderId, DbTransaction trans = null)
        {
            manager = NbManagerMgr.GetById(manager.Idx, siteId);
            if (coin <= 0 || manager == null)
                return MessageCode.NbParameterError;
            int returnCode = -2;
            int curCoin = manager.Coin;
            NbManagerMgr.CostCoin(manager.Idx, coin, ref curCoin, ref returnCode, trans, siteId);
            if (returnCode == 0)
            {
                manager.AddCoin += (coin * -1);
                manager.CoinSourceType = (int)coinSourceType;
                manager.CoinOrderId = coinOrderId;
                manager.Coin = curCoin;
                DeleteCache(manager.Idx);
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbCoinShortage;
            }
        }

        #endregion

        #region Score

        /// <summary>
        /// 加世界杯积分
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public MessageCode AddScore(Guid managerId, int score, DbTransaction trans = null)
        {
            if (score <= 0)
                return MessageCode.NbParameterError;
            if (NbManagerMgr.AddScore(managerId, score, trans))
            {
                DeleteCache(managerId);
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbUpdateFail;
            }
        }

        #endregion

        #region Reiki

        /// <summary>
        /// 增加/扣除灵气
        /// </summary>
        /// <param name="manager">经理</param>
        /// <param name="reiki">灵气值</param>
        /// <param name="actionType">1增加，-1扣除</param>
        /// <returns></returns>
        public MessageCode UpdateReiki(NbManagerEntity manager, int reiki, EnumActionType actionType)
        {
            reiki = Math.Abs(reiki);
            int resultReiki = manager.Reiki;
            int returnCode = -2;
            if (actionType == EnumActionType.Add)
            {
                NbManagerMgr.AddReiki(manager.Idx, reiki, ref resultReiki);
                returnCode = 0;
            }
            else if (actionType == EnumActionType.Minus)
            {
                NbManagerMgr.CostReiki(manager.Idx, reiki, ref resultReiki, ref returnCode);
            }
            else
            {
                return MessageCode.NbParameterError;
            }
            manager.Reiki = resultReiki;
            DeleteCache(manager.Idx);
            return (MessageCode)returnCode;
        }

        #endregion

        #region FriendShipPoint
        /// <summary>
        /// 更新友情点
        /// </summary>
        /// <param name="manager">经理</param>
        /// <param name="friendShipPoint">友情点</param>
        /// <param name="actionType">1增加，-1扣除</param>
        /// <returns></returns>
        public MessageCode UpdateFriendShipPoint(NbManagerEntity manager, int friendShipPoint, EnumActionType actionType, DbTransaction trans = null)
        {
            friendShipPoint = Math.Abs(friendShipPoint);
            int resultFriendShipPoint = manager.FriendShipPoint;
            int returnCode = -2;
            if (actionType == EnumActionType.Add)
            {
                NbManagerMgr.AddFriendShipPoint(manager.Idx, friendShipPoint, ref resultFriendShipPoint, trans);
                returnCode = 0;
            }
            else if (actionType == EnumActionType.Minus)
            {
                NbManagerMgr.CostFriendShipPoint(manager.Idx, friendShipPoint, ref resultFriendShipPoint, ref returnCode, trans);
            }
            else
            {
                return MessageCode.NbParameterError;
            }
            manager.FriendShipPoint = resultFriendShipPoint;
            DeleteCache(manager.Idx);
            return (MessageCode)returnCode;
        }


        #endregion

        #region Sophisticate

        /// <summary>
        /// 增加/扣除阅历
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="sophisticate"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public MessageCode UpdateSophisticate(NbManagerEntity manager, int sophisticate, EnumActionType actionType)
        {
            sophisticate = Math.Abs(sophisticate);
            int resultSophisticate = manager.Sophisticate;
            int returnCode = -2;
            if (actionType == EnumActionType.Add)
            {
                NbManagerMgr.AddSophisticate(manager.Idx, sophisticate, ref resultSophisticate);
                returnCode = 0;
            }
            else if (actionType == EnumActionType.Minus)
            {
                NbManagerMgr.CostSophisticate(manager.Idx, sophisticate,
                    (int)MessageCode.TeammemberSophisticateShortage, ref resultSophisticate, ref returnCode);
            }
            else
            {
                return MessageCode.NbParameterError;
            }
            manager.Sophisticate = resultSophisticate;
            DeleteCache(manager.Idx);
            return (MessageCode)returnCode;
        }

        #endregion

        #region GetManagerIdByName

        public string GetManagerIdByName(string name)
        {
            try
            {
                var manager = NbManagerMgr.GetByName(name);
                if (manager == null)
                    return "";
                return manager.Idx.ToString();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetManagerIdByName", ex);
                return "";
            }
        }

        #endregion

        #region DeleteCache

        public bool DeleteCache(Guid managerId)
        {
            try
            {
                return MemcachedFactory.ManagerClient.Delete(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ManagerCore DeleteCache", ex);
                return false;
            }
        }

        #endregion

        #region GetFunctionList

        public FunctionListResponse GetFunctionList(Guid managerId)
        {
            var managerExtra = GetManagerExtra(managerId);
            var func = managerExtra.FunctionList;
            MemcachedFactory.OpenFunctionClient.Set(managerId, func);
            var response = ResponseHelper.CreateSuccess<FunctionListResponse>();
            response.Data = new FunctionListEntity();
            response.Data.FunctionList = func;
            return response;
        }

        #endregion

        #region UpdateLogo

        public MessageCodeResponse UpdateLogo(Guid managerId, string logo)
        {
            if (string.IsNullOrEmpty(logo))
                return ResponseHelper.InvalidParameter<MessageCodeResponse>();
            //var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.UpdateLogo);
            //var item = package.GetItem(itemId);
            //if (item == null)
            //    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.ItemNotExists);
            //var mallCache = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(item.ItemCode);
            //if (mallCache == null || mallCache.EffectType != (int)EnumMallEffectType.UpdateLogo)
            //    return ResponseHelper.InvalidParameter<MessageCodeResponse>();
            //package.Delete(item);
            //package.Save();
            NbManagerMgr.UpdateLogo(managerId, logo);
            DeleteCache(managerId);
            //package.Shadow.Save();
            return ResponseHelper.CreateSuccess<MessageCodeResponse>();
        }

        #endregion
        /// <summary>
        /// a8用户数据采集"{dataType,sessionId,gameNum,channelAlias,channelId,deviceId,model,release,uid,uname,serverId,serverName,roleId,roleName,roleLevel,ext,sdkVersion}";
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public string ManagerAction(string openId,string actionType)
        {
            var str = "";
            return "";
        }

        public UpdateIndulgeResponse UpdateIndulgeInfo(string account, Guid managerId, string name, string certId, DateTime birthday)
        {
            var userReg = NbUserregMgr.GetById(account);
            if (userReg != null)
            {
                userReg.Cert = certId;
                userReg.Birthday = birthday;
                NbUserregMgr.Update(userReg);
            }
            else
            {
                userReg = new NbUserregEntity(account, "", name, certId, birthday, DateTime.Now, 0, 0, DateTime.Now);
                NbUserregMgr.Insert(userReg);
            }
            var response = ResponseHelper.CreateSuccess<UpdateIndulgeResponse>();
            response.Data = new UpdateIndulgeEntity();
            response.Data.OnlineMinutes = OnlineCore.Instance.GetIndulgeMinutes(account, managerId);
            return response;
        }
        #endregion

        #region UpdateName
        public MessageCodeResponse UpdateName(System.Guid managerId, System.String oldName, System.String newName)
        {
            int returnCode = 0;
            NbManagerMgr.UpdateName(managerId, oldName, newName, ref returnCode);
            var response = ResponseHelper.Create<MessageCodeResponse>(returnCode);
            return response;
        }

        #endregion

        #region encapsulation

        private NbManagerextraEntity InnerGetManagerExtra(Guid managerId, string account = null)
        {
            var managerExtra = NbManagerextraMgr.GetById(managerId);
            //是否需要更新
            bool isUpdate = false;
            if (managerExtra.RecordDate != DateTime.Today)
            {
                managerExtra.HelpTrainCount = 0;
                managerExtra.ByHelpTrainCount = 0;
                managerExtra.RecordDate = DateTime.Today;
                managerExtra.OpenBoxCount = 0;
                managerExtra.StaminaGiftStatus = 0;
                managerExtra.ResetPotentialNumber = 2;
                isUpdate = true;
                ActivityExThread.Instance.Login(managerId);
                var leagueManagerInfo = LaegueManagerinfoMgr.GetById(managerId);
                if(leagueManagerInfo != null)
                {
                    if (leagueManagerInfo.DailyWinUpdateTime.Date != DateTime.Now.Date)
                    {
                        leagueManagerInfo.DailyWinCount = 0;
                        LaegueManagerinfoMgr.Update(leagueManagerInfo);
                    }
                }
            }
            if (managerExtra.ScoutingUpdate <= DateTime.Now && managerExtra.Scouting < _scoutingFellNumber)
            {
                managerExtra.Scouting = _scoutingFellNumber;
                isUpdate = true;
            }
            if (managerExtra.CoinScoutingUpdate <= DateTime.Now && managerExtra.CoinScouting < _scoutingFellNumber)
            {
                managerExtra.CoinScouting = _scoutingFellNumber;
                isUpdate = true;
            }
            if (managerExtra.FriendScoutingUpdate <= DateTime.Now && managerExtra.FriendScouting < _scoutingFellNumber)
            {
                managerExtra.FriendScouting = _scoutingFellNumber;
                isUpdate = true;
            }
            if (isUpdate)
                NbManagerextraMgr.Update(managerExtra);
            return managerExtra;
        }

        private NBManagerInfoResponse BuildManagerInfoResponse(NbManagerEntity manager, GuildListEntity guild = null)
        {
            var response = ResponseHelper.CreateSuccess<NBManagerInfoResponse>();
            response.Data = BuildManagerInfoData(manager, guild);
            return response;
        }

        private NBManagerInfoData BuildManagerInfoData(NbManagerEntity manager, GuildListEntity guild = null)
        {
            var responseData = new NBManagerInfoData();
            responseData.Manager = manager;
            var managerExtra = InnerGetManagerExtra(manager.Idx, manager.Account);
            ManagerUtil.CalCurrentStamina(managerExtra, manager.Level,manager.VipLevel);
            managerExtra.ResumeStaminaTimeTick =
                ShareUtil.GetTimeTick(managerExtra.ResumeStaminaTime.AddSeconds(GetResumeStaminaTimeConfig(manager.VipLevel)));

            //responseData.CoachId = MatchDataHelper.GetCoachId(manager.Idx);
            responseData.Point = PayCore.Instance.GetPoint(manager.Account);
            responseData.BindPoint = PayCore.Instance.GetBindPoint(manager.Account);
            responseData.ServerTime = ShareUtil.GetTimeTick(DateTime.Now);
            responseData.OnlineMinutes = OnlineCore.Instance.GetIndulgeMinutes(manager.Account, manager.Idx);
            manager.LevelupExp = CacheFactory.ManagerDataCache.GetExp(manager.Level);
            if (manager.LevelupExp > 0 && manager.EXP >= manager.LevelupExp)
            {
                try
                {
                    manager.Level++;
                    manager.EXP -= manager.LevelupExp;
                    manager.IsLevelup = true;
                    if (ManagerUtil.SaveManagerData(manager))
                    {
                        ManagerUtil.SaveManagerAfter(manager, true);
                    }
                    manager.LevelupExp = CacheFactory.ManagerDataCache.GetExp(manager.Level);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("BuildManagerInfoResponse level up", ex);
                }
            }
            bool needUpExtra = false;
            if (manager.Level >= 9)
            {
                var func = managerExtra.FunctionList;
                var func2 = CacheFactory.ManagerDataCache.GetFunctionList(manager.Level);
                if (func2 != null && func != func2.FunctionList)
                {
                    managerExtra.FunctionList = func2.FunctionList;
                    ManagerUtil.DeleteOpenFunctionCache(manager.Idx);
                    needUpExtra = true;
                }
            }

            if (manager.Level >= 60)
            {
                if (managerExtra.LevelGiftExpired3 == ShareUtil.BaseTime)
                {
                    managerExtra.LevelGiftExpired3 = DateTime.Now.AddHours(72);
                    managerExtra.LevelGiftStep = 3;
                    needUpExtra = true;
                }
                if (managerExtra.LevelGiftExpired2 == ShareUtil.BaseTime)
                {
                    managerExtra.LevelGiftExpired2 = DateTime.Now.AddHours(72);
                    managerExtra.LevelGiftStep = 3;
                    needUpExtra = true;
                }
                if (managerExtra.LevelGiftExpired == ShareUtil.BaseTime)
                {
                    managerExtra.LevelGiftExpired = DateTime.Now.AddHours(72);
                    managerExtra.LevelGiftStep = 3;
                    needUpExtra = true;
                }
                NbManagerextraMgr.Update(managerExtra);
            }
            else if (manager.Level >= 30)
            {
                if (managerExtra.LevelGiftExpired2 == ShareUtil.BaseTime)
                {
                    managerExtra.LevelGiftExpired2 = DateTime.Now.AddHours(72);
                    managerExtra.LevelGiftStep = 2;
                    needUpExtra = true;
                }
                if (managerExtra.LevelGiftExpired == ShareUtil.BaseTime)
                {
                    managerExtra.LevelGiftExpired = DateTime.Now.AddHours(72);
                    managerExtra.LevelGiftStep = 2;
                    needUpExtra = true;
                }
            }
            else if (manager.Level >= 10)
            {
                if (managerExtra.LevelGiftExpired == ShareUtil.BaseTime)
                {
                    managerExtra.LevelGiftExpired = DateTime.Now.AddHours(72);
                    managerExtra.LevelGiftStep = 2;
                    needUpExtra = true;
                }
            }

            //if (needUpExtra)
                NbManagerextraMgr.Update(managerExtra);
            responseData.ManagerExtra = managerExtra;
            return responseData;
        }

        #endregion

        /// <summary>
        /// 获取经理的所有功能次数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerAllFunctionNumberResponse GetManagerAllFunctionNumber(Guid managerId)
        {
            ManagerAllFunctionNumberResponse response = new ManagerAllFunctionNumberResponse();
            response.Data = new ManagerAllFunctionNumber();
            try
            {
                var manager = GetManager(managerId);
                
                //友谊赛可获得钻石和已经获得多少钻石
                var pointConfig = CacheFactory.PlayerKillCache.GetPointConfig(manager.VipLevel);
                response.Data.PkMaxPoint = pointConfig.TotalPoint;
                var pkManager = PlayerKillCore.Instance.InnerGetInfo(managerId);
                response.Data.PkHavePoint = pkManager.DayPoint;
                //杯赛可竞猜次数和已经竞猜次数
                int dailycupmaxCount = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.DailycupGambleCount);
                response.Data.DailycupMaxNumber = dailycupmaxCount;

                DailycupInfoEntity dailycup = DailycupInfoMgr.GetByDate(DateTime.Today);

                if (dailycup != null)
                {
                    int dailycupHaveCount = DailycupGambleMgr.GambleNumber(dailycup.Idx, managerId);
                    response.Data.DailycupHaveNumber = dailycupHaveCount;
                }

                var managerextra =  GetManagerExtra(managerId);
                if (managerextra != null)
                {
                    if (managerextra.CoinScouting > 0 || managerextra.Scouting > 0 || managerextra.FriendScouting > 0)
                        response.Data.IsFreeScouting = true;
                }

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取经理的所有功能剩余次数", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }
       /// <summary>
       /// 玩家信息
       /// </summary>
       /// <param name="openId"></param>
       /// <param name="serverNo"></param>
       /// <returns></returns>
        public NbManagerEntity IsRegist(String openId, String serverNo)
        {
            string str = "";
            var manager = ManagerCore.Instance.GetManager(openId);
           
            // str = "" + manager.Idx + "," + manager.Account + "," + manager.Level;
           if (manager != null)
           {

               var entity = GetManagerInfo(manager.Idx, false);
               if (entity != null && entity.Data != null)
                   manager.Kpi = entity.Data.ManagerExtra.Kpi;

               return manager;

           }
            return null;
        }
         public NbManagerEntity IsRegistByName(string name,string serverid)
        {
           
            var manager = new NbManagerEntity();
            manager=ManagerCore.Instance.GetManagerByName(name);
             if (manager != null)
             {
                 var entity = GetManagerInfo(manager.Idx, false);
                 if (entity != null && entity.Data != null)
                     manager.Kpi = entity.Data.ManagerExtra.Kpi;
             }
             return manager;
            return null;
        }
       

    }
}
