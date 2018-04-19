using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Manager;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class ManagerClient
    {
        private static IManagerService proxy = ServiceProxy<IManagerService>.Create("NetTcp_IManagerService");

        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="userIp"></param>
        /// <returns></returns>
        public NbUserResponse GetUserByAccount(string account, string userIp)
        {
            return proxy.GetUserByAccount(account, userIp);
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
            return proxy.BindAccount(bindCode, account, name, managerId);
        }
        /// <summary>
        /// 删除角色--合区使用
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="bindcode">绑定码</param>
        /// <returns>是否删除成功</returns>
        public MessageCodeResponse DeleteRole(string account, Guid bindcode)
        {
            return proxy.DeleteRole(account, bindcode);
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
            return proxy.CreateManager(account, name, logo, templateId, userIp);
        }

        /// <summary>
        /// 设置名字
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public MessageCodeResponse UpdateName(Guid managerId, string name)
        {
            return proxy.UpdateName(managerId, name);
        }

        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public NBManagerInfoResponse GetManagerInfo(Guid managerId, bool isTx)
        {
            return proxy.GetManagerInfo(managerId, isTx);
        }

        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public NBManagerListResponse GetManagerInfoByAccount(string account, string userIp, bool isTx)
        {
            return proxy.GetManagerInfoByAccount(account, userIp, isTx);
        }

        /// <summary>
        /// 获取阵容
        /// </summary>
        /// <returns></returns>
        public TemplateRegisterResponse GetRegisterSolution()
        {
            return proxy.GetRegisterSolution();
        }

        /// <summary>
        /// 通过名字获取id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetManagerIdByName(string name)
        {
            return proxy.GetManagerIdByName(name);
        }

        /// <summary>
        /// 恢复体力
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerStaminaResponse ResumeStamina(Guid managerId)
        {
            return proxy.ResumeStamina(managerId);
        }

        /// <summary>
        /// 获取经理详细信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerDetailInfoResponse GetManagerDetailInfo(Guid managerId, string siteId)
        {
            return proxy.GetManagerDetailInfo(managerId, siteId);
        }
        /// <summary>
        /// 获取经理荣誉列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerHonorListResponse GetManagerHonorList(Guid managerId)
        {
            return proxy.GetManagerHonorList(managerId);
        }

        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public FunctionListResponse GetFunctionList(Guid managerId)
        {
            return proxy.GetFunctionList(managerId);
        }

        /// <summary>
        /// 获取vip数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public VipDataResponse GetVipData(Guid managerId)
        {
            return proxy.GetVipData(managerId);
        }

        /// <summary>
        /// 签到vip经验
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public VipDataResponse DailyAttendVip(Guid managerId)
        {
            return proxy.DailyAttendVip(managerId);
        }


        /// <summary>
        /// 修改logo
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        public MessageCodeResponse UpdateLogo(Guid managerId, string logo)
        {
            return proxy.UpdateLogo(managerId, logo);
        }
       
        public UpdateIndulgeResponse UpdateIndulgeInfo(string account, Guid managerId, string name, string certId, DateTime birthday)
        {
            return proxy.UpdateIndulgeInfo(account, managerId, name, certId, birthday);
        }

        public ManagerStaminaResponse GiftStamina(Guid managerId)
        {
            return proxy.GiftStamina(managerId);
        }

        public NBManagerInfoResponse SelectManager(string account, Guid managerId, string userIp, bool isTx)
        {
            return proxy.SelectManager(account, managerId, userIp, isTx);
        }


        /// <summary>
        /// 获取经理的所有功能次数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerAllFunctionNumberResponse GetManagerAllFunctionNumber(Guid managerId)
        {
            return proxy.GetManagerAllFunctionNumber(managerId);
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
            return proxy.GetRegisterManager(managerId, siteId, kgext);
        }


        /// <summary>
        /// 获取签到信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public DailyAttendanceInfoResponse GetDailyAttendanceInfo(Guid managerId)
        {
            return proxy.GetDailyAttendanceInfo(managerId);
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public DailyAttendancePrizeResponse AttendancePrize(Guid managerId)
        {
            return proxy.AttendancePrize(managerId);
        }
        /// <summary>
        /// 是否登录
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="serverNo"></param>
        /// <returns></returns>
        public NbManagerEntity IsRegist(String openId, string serverNo)
        {
            return proxy.IsRegist(openId, serverNo);
        }
        

        /// <summary>
        /// 查询经理信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="serverNo"></param>
        /// <returns></returns>
        public NbManagerEntity IsRegistByName(string name, string serverNo)
        {
            return proxy.IsRegistByName(name, serverNo);
        }
        /// <summary>
        /// 批量获取经理信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<NbManagerEntity> IsRegistByNameList(string data)
        {
            return proxy.IsRegistByNameList(data);
        }
        /// <summary>
        ///  分享礼包
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int SendItemByShare(string name, int type)
        {
            try
            {
                return proxy.SendItemByShare(name, type);
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

        public GetPlatformAnnouncementResponse GetPlatformAnnouncement(string platform = "")
        {
            return proxy.GetPlatformAnnouncement(platform);
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
                return proxy.SetPlatformAnnouncement(platform, isTop, title, contentString, startTime, endTime, trans, zoneId);

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
                return proxy.RanablePlatformAnnouncement(idx, isTop, startTime, endTime, trans, zoneId);

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
                return proxy.ClosePlatformAnnouncement(idx, trans, zoneId);
        }
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="trans"></param>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public bool DeleteAnnouncement(int idx, System.Data.Common.DbTransaction trans = null, string zoneId = "")
        {
            return proxy.DeleteAnnouncement(idx, trans, zoneId);
        }
        /// <summary>
        /// 获取玩家是否玩吧达人
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public bool IsTxVip(Guid managerId)
        {
            return proxy.IsTxVip(managerId);
        }

        /// <summary>
        /// 分享任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse ShareTask(Guid managerId)
        {
            return proxy.ShareTask(managerId);
        }
    }
}
