using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.Dpm.Core.Activity;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Online;
using Games.NBall.Core.Vip;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Manager;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class ManagerService : IManagerService
    {
        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="userIp"></param>
        /// <returns></returns>
        public NbUserResponse GetUserByAccount(string account, string userIp)
        {
            return ResponseHelper.TryCatch<NbUserResponse>(() => ManagerCore.Instance.GetUserByAccount(account, userIp));
        }

        /// <summary>
        /// 通过绑定码，把其他账号的角色复制到本账号的角色
        /// </summary>
        /// <param name="bindCode">绑定码</param>
        /// <param name="account">账号</param>
        /// <param name="name">经理名</param>
        /// <param name="managerId">经理ID</param>        
        /// <returns>是否复制成功</returns>
        public MessageCodeResponse BindAccount(Guid bindCode, string account, string name, Guid managerId)
        {
            return ResponseHelper.TryCatch<MessageCodeResponse>(() => ManagerCore.Instance.BindAccount(bindCode, account, name, managerId));
        }
        /// <summary>
        /// 删除角色--合区使用
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="bindcode">绑定码</param>
        /// <returns>是否删除成功</returns>
        public MessageCodeResponse DeleteRole(string account, Guid bindcode)
        {
            return ResponseHelper.TryCatch<MessageCodeResponse>(() => ManagerCore.Instance.DeleteRole(account, bindcode));
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <param name="area"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        public NBManagerCreateResponse CreateManager(string account, string name, string logo, int templateId, string userIp)
        {
            return ResponseHelper.TryCatch<NBManagerCreateResponse>(() => ManagerCore.Instance.RegisterManager(account, name, logo, templateId, userIp));
        }

        /// <summary>
        /// 设置名字
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public MessageCodeResponse UpdateName(Guid managerId, string name)
        {
            return ResponseHelper.TryCatch<MessageCodeResponse>(() => ManagerCore.Instance.UpdateName(managerId, name));
        }

        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public NBManagerInfoResponse GetManagerInfo(Guid managerId, bool isTx)
        {
            return ResponseHelper.TryCatch<NBManagerInfoResponse>(() => ManagerCore.Instance.GetManagerInfo(managerId, isTx));
        }

        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public NBManagerListResponse GetManagerInfoByAccount(string account, string userIp, bool isTx)
        {
            return ResponseHelper.TryCatch<NBManagerListResponse>(() => ManagerCore.Instance.GetManagerInfoByAccount(account, userIp, isTx));
        }

        /// <summary>
        /// 获取阵容
        /// </summary>
        /// <returns></returns>
        public TemplateRegisterResponse GetRegisterSolution()
        {
            return ResponseHelper.TryCatch<TemplateRegisterResponse>(() => ManagerCore.Instance.GetRegisterSolution());
        }

        /// <summary>
        /// 通过名字获取id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetManagerIdByName(string name)
        {
            return ManagerCore.Instance.GetManagerIdByName(name);
        }

        /// <summary>
        /// 恢复体力
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerStaminaResponse ResumeStamina(Guid managerId)
        {
            return ResponseHelper.TryCatch<ManagerStaminaResponse>(() => ManagerCore.Instance.ResumeStamina(managerId));
        }

        /// <summary>
        /// 获取经理详细信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerDetailInfoResponse GetManagerDetailInfo(Guid managerId, string siteId)
        {
            return ResponseHelper.TryCatch<ManagerDetailInfoResponse>(() => ManagerCore.Instance.GetManagerDetailInfo(managerId, siteId));
        }
        /// <summary>
        /// 获取经理荣誉列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerHonorListResponse GetManagerHonorList(Guid managerId)
        {
            return ResponseHelper.TryCatch<ManagerHonorListResponse>(() => ManagerCore.Instance.GetManagerHonorList(managerId));
        }

        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public FunctionListResponse GetFunctionList(Guid managerId)
        {
            return ResponseHelper.TryCatch<FunctionListResponse>(() => ManagerCore.Instance.GetFunctionList(managerId));
        }

        /// <summary>
        /// 获取vip数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public VipDataResponse GetVipData(Guid managerId)
        {
            return ResponseHelper.TryCatch<VipDataResponse>(() => ManagerCore.Instance.GetVipData(managerId));
        }

        /// <summary>
        ///签到vip经验
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public VipDataResponse DailyAttendVip(Guid managerId)
        {
            return ResponseHelper.TryCatch<VipDataResponse>(() => VipCore.Instance.DailyAttendVip(managerId));
        }


        /// <summary>
        /// 修改logo
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        public MessageCodeResponse UpdateLogo(Guid managerId, string logo)
        {
            return ResponseHelper.TryCatch<MessageCodeResponse>(() => ManagerCore.Instance.UpdateLogo(managerId, logo));
        }

        //public string ResetCache(int cacheType)
        //{
        //    return CacheManager.Instance.ResetCache(cacheType);
        //}

        public UpdateIndulgeResponse UpdateIndulgeInfo(string account, Guid managerId, string name, string certId, DateTime birthday)
        {
            return ResponseHelper.TryCatch<UpdateIndulgeResponse>(() => ManagerCore.Instance.UpdateIndulgeInfo(account, managerId, name, certId, birthday));
        }

        public ManagerStaminaResponse GiftStamina(Guid managerId)
        {
            return ResponseHelper.TryCatch<ManagerStaminaResponse>(() => ManagerCore.Instance.GiftStamina(managerId));
        }

        public NBManagerInfoResponse SelectManager(string account, Guid managerId, string userIp, bool isTx)
        {
            return ResponseHelper.TryCatch<NBManagerInfoResponse>(() => ManagerCore.Instance.SelectManager(account, managerId, userIp, isTx));
        }

        /// <summary>
        /// 获取经理的所有功能次数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerAllFunctionNumberResponse GetManagerAllFunctionNumber(Guid managerId)
        {
            return ResponseHelper.TryCatch<ManagerAllFunctionNumberResponse>(() => ManagerCore.Instance.GetManagerAllFunctionNumber(managerId));
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
            return ResponseHelper.TryCatch<GetRegisterManagerResponse>(() => ManagerCore.Instance.GetRegisterManager(managerId, siteId, kgext));
        }

        /// <summary>
        /// 获取签到信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public DailyAttendanceInfoResponse GetDailyAttendanceInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch<DailyAttendanceInfoResponse>(() => DailyAttendance.Instance.GetDailyAttendanceInfo(managerId));
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public DailyAttendancePrizeResponse AttendancePrize(Guid managerId)
        {
            return ResponseHelper.TryCatch<DailyAttendancePrizeResponse>(() => DailyAttendance.Instance.AttendancePrize(managerId));
        }
        /// <summary>
        /// 是否注册
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="serverNo"></param>
        /// <returns></returns>
        public NbManagerEntity IsRegist(String openId, string serverNo)
        {
            try
            {
                return ManagerCore.Instance.IsRegist(openId, serverNo);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            
        }

        /// <summary>
        /// 查询经理信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="serverNo"></param>
        /// <returns></returns>

        public NbManagerEntity IsRegistByName(string name, string serverNo)
        {
            try
            {
                return ManagerCore.Instance.IsRegistByName(name, serverNo);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 批量获取经理信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<NbManagerEntity> IsRegistByNameList(string data)
        {
            try
            {
                return CSDKinterface.Instance.IsRegistByNameList(data);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        ///  分享礼包
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int SendItemByShare(string name,int type)
        {
            try
            {
                return CSDKinterface.Instance.SendItemByShare(name,type);
            }
            catch (Exception)
            {
                return -1;
            }
        }


        /// <summary>
        /// 获取公告
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>

        public GetPlatformAnnouncementResponse GetPlatformAnnouncement(string platform="")
        {
            return ResponseHelper.TryCatch<GetPlatformAnnouncementResponse>(() => AnnouncementCore.Instance.GetPlatformAnnouncement(platform));
        }

        /// <summary>
        /// 增加公告
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="isTop"></param>
        /// <param name="title"></param>
        /// <param name="contentString"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="trans"></param>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public bool SetPlatformAnnouncement(string platform, bool isTop, string title, string contentString, DateTime startTime, DateTime endTime, System.Data.Common.DbTransaction trans = null, string zoneId = "")
        {
            try
            {
                return AnnouncementCore.Instance.SetPlatformAnnouncement(platform, isTop, title, contentString, startTime, endTime, trans, zoneId);

            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 启用公告
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="isTop"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="trans"></param>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public bool RanablePlatformAnnouncement(int idx, bool isTop, DateTime startTime, DateTime endTime, System.Data.Common.DbTransaction trans = null, string zoneId = "")
        {
            try
            {
                return AnnouncementCore.Instance.RanablePlatformAnnouncement(idx, isTop, startTime, endTime, trans, zoneId);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 关闭公告
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="trans"></param>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public bool ClosePlatformAnnouncement(int idx, System.Data.Common.DbTransaction trans = null, string zoneId = "")
        {
            try
            {
                return AnnouncementCore.Instance.ClosePlatformAnnouncement(idx, trans, zoneId);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 关闭公告
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="trans"></param>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public bool DeleteAnnouncement(int idx, System.Data.Common.DbTransaction trans = null, string zoneId = "")
        {
            try
            {
                return AnnouncementCore.Instance.DeleteAnnouncement(idx, trans, zoneId);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取玩家是否玩吧达人
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public bool IsTxVip(Guid managerId)
        {
            return CSDKinterface.Instance.IsTxVip(managerId);
        }

        /// <summary>
        /// 分享游戏任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse ShareTask(Guid managerId)
        {
            return CSDKinterface.Instance.ShareTask(managerId);
        }
    }
}
