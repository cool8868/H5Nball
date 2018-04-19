using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Manager;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IManagerService
    {
        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="userIp"></param>
        /// <returns></returns>
        [OperationContract]
        NbUserResponse GetUserByAccount(string account, string userIp);

        /// <summary>
        /// 通过绑定码，把其他账号的角色复制到本账号的角色
        /// </summary>
        /// <param name="bindCode">绑定码</param>
        /// <param name="account">账号</param>
        /// <param name="name">经理名</param>
        /// <param name="managerId">经理ID</param>       
        /// <returns>是否复制成功</returns>
        [OperationContract]
        MessageCodeResponse BindAccount(Guid bindCode, string account, string name, Guid managerId);

        /// <summary>
        /// 删除角色--合区使用
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="bindcode">绑定码</param>
        /// <returns>是否删除成功</returns>
        [OperationContract]
        MessageCodeResponse DeleteRole(string account, Guid bindcode);

        /// <summary>
        /// 设置名字
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse UpdateName(Guid managerId, string name);

        /// <summary>
        /// 创建经理
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <param name="area"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        [OperationContract]
        NBManagerCreateResponse CreateManager(string account, string name, string logo, int templateId, string userIp);

        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        NBManagerInfoResponse GetManagerInfo(Guid managerId, bool isTx);

        /// <summary>
        /// 获取经理信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [OperationContract]
        NBManagerListResponse GetManagerInfoByAccount(string account, string userIp, bool isTx);

        /// <summary>
        /// 获取阵容
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        TemplateRegisterResponse GetRegisterSolution();

        /// <summary>
        /// 通过名字获取id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        string GetManagerIdByName(string name);

        /// <summary>
        /// 恢复体力
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerStaminaResponse ResumeStamina(Guid managerId);
        /// <summary>
        /// 获取经理详细信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerDetailInfoResponse GetManagerDetailInfo(Guid managerId, string siteId);
        /// <summary>
        /// 获取经理荣誉列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerHonorListResponse GetManagerHonorList(Guid managerId);
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        FunctionListResponse GetFunctionList(Guid managerId);
        /// <summary>
        /// 获取vip数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        VipDataResponse GetVipData(Guid managerId);

        /// <summary>
        ///签到vip经验
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        VipDataResponse DailyAttendVip(Guid managerId);


        /// <summary>
        /// 修改logo
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse UpdateLogo(Guid managerId, string logo);

        [OperationContract]
        UpdateIndulgeResponse UpdateIndulgeInfo(string account, Guid managerId, string name, string certId, DateTime birthday);

        [OperationContract]
        ManagerStaminaResponse GiftStamina(Guid managerId);
        [OperationContract]
        NBManagerInfoResponse SelectManager(string account, Guid managerId, string userIp, bool isTx);

        /// <summary>
        /// 获取经理的所有功能次数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerAllFunctionNumberResponse GetManagerAllFunctionNumber(Guid managerId);

        /// <summary>
        /// 获取用户创建角色信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="siteId"></param>
        /// <param name="kgext"></param>
        /// <returns></returns>
        [OperationContract]
        GetRegisterManagerResponse GetRegisterManager(Guid managerId, string siteId, string kgext);

        /// <summary>
        /// 获取签到信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        DailyAttendanceInfoResponse GetDailyAttendanceInfo(Guid managerId);

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        DailyAttendancePrizeResponse AttendancePrize(Guid managerId);
        /// <summary>
        /// 是否登录
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="serverNo"></param>
        /// <returns></returns>
        [OperationContract]
        NbManagerEntity IsRegist(String openId, string serverNo);


        /// <summary>
        /// 查询经理信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="serverNo"></param>
        /// <returns></returns>
        [OperationContract]
        NbManagerEntity IsRegistByName(string name, string serverNo);
        /// <summary>
        /// 批量获取经理信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        List<NbManagerEntity> IsRegistByNameList(string data);

        /// <summary>
        ///  分享礼包
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        int SendItemByShare(string name, int type);


        /// <summary>
        /// 获取公告
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        [OperationContract]
        GetPlatformAnnouncementResponse GetPlatformAnnouncement(string platform = "");

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
        [OperationContract]
        bool SetPlatformAnnouncement(string platform, bool isTop, string title, string contentString, DateTime startTime,
            DateTime endTime, DbTransaction trans = null, string zoneId = "");

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
        [OperationContract]
        bool RanablePlatformAnnouncement(int idx, bool isTop, DateTime startTime, DateTime endTime,
            DbTransaction trans = null, string zoneId = "");

        /// <summary>
        /// 关闭公告
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="trans"></param>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        [OperationContract]
        bool ClosePlatformAnnouncement(int idx, DbTransaction trans = null, string zoneId = "");
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="trans"></param>
        /// <param name="zoneId"></param>
        /// <returns></returns>

        [OperationContract]
        bool DeleteAnnouncement(int idx, DbTransaction trans = null, string zoneId = "");
        /// <summary>
        /// 是否玩吧达人
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsTxVip(Guid managerId);

        /// <summary>
        /// 分享游戏任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse ShareTask(Guid managerId);
    }
}
