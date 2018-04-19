using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response;

namespace Games.NBall.Core
{
   public class UserLoginCore
   {
       /// <summary>
       /// 单服的区列表
       /// </summary>
       private static string _aloneZoneList ="";
       /// <summary>
       /// 玩吧区
       /// </summary>
       private static string _h5_A8WanBa = "";
       /// <summary>
       /// 群黑和9G特殊处理
       /// </summary>
       private static string _qunheiAnd9G = "";
       /// <summary>
       /// 应用调试者
       /// </summary>
       private static string _H5_A8Debug = "";

       /// <summary>
       /// 白鹭
       /// </summary>
       private static string _H5_Egret = "";
       /// <summary>
       /// 小熊
       /// </summary>
       private static string _h5_Bear = "";
       public UserLoginCore(int p)
       {
           _aloneZoneList = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_A8AloneZoneList);
           _h5_A8WanBa = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_A8WanBa);
           _qunheiAnd9G = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_A8QunheiAnd9GList);
           _H5_A8Debug = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_A8Debug);
           _H5_Egret = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_Egret);
           _h5_Bear = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_Bear);

       }

       #region Instance

       public static UserLoginCore Instance
       {
           get { return SingletonFactory<UserLoginCore>.SInstance; }
       }

       #endregion

       /// <summary>
       /// 设置用户登录区信息
       /// </summary>
       /// <param name="account"></param>
       /// <param name="platform"></param>
       /// <param name="siteId"></param>
       /// <returns></returns>
       public MessageCode SetUserLogin(string account,string platform, string siteId)
       {
           try
           {
               DateTime date = DateTime.Now;
               var userRecord = UserloginZoneMgr.GetByAccountPlatform(account, platform);
               bool isInsert = false;
               if (userRecord == null)
               {
                   isInsert = true;
                   userRecord = new UserloginZoneEntity(account, platform, date, siteId, date);
               }
               else
               {
                   var sitesString = siteId;
                   if (userRecord.LoginSties.Length > 0)
                   {
                       var siteList = userRecord.LoginSties.Split(',');
                       foreach (var s in siteList)
                       {
                           if (s != siteId)
                               sitesString += "," + s;
                       }
                   }
                   userRecord.LastLoginTime = date;
                   userRecord.LoginSties = sitesString;
               }
               if (isInsert)
               {
                   if (!UserloginZoneMgr.Insert(userRecord))
                       return MessageCode.NbUpdateFail;
               }
               else
               {
                   if (!UserloginZoneMgr.Update(userRecord))
                       return MessageCode.NbUpdateFail;
               }
               return MessageCode.Success;
           }
           catch (Exception ex)
           {
               SystemlogMgr.Error("设置用户登录信息", ex);
               return MessageCode.NbParameterError;
           }
       }

       /// <summary>
       /// 获取用户登录信息
       /// </summary>
       /// <param name="account"></param>
       /// <param name="platform"></param>
       /// <returns></returns>
       public GetUserLoginRecordResponse GetUserLoginRecord(string account, string platform,string longType="")
       {
           GetUserLoginRecordResponse response = new GetUserLoginRecordResponse();
           response.Data = new Entity.Response.GetUserLoginRecord();
           try
           {
               var userRecord = UserloginZoneMgr.GetByAccountPlatform(account, platform);
               if (userRecord != null)
               {
                   response.Data.LoginRecord = userRecord.LoginSties;
               }

               List<AllZoneinfoEntity> zonelist = null;
               if (ShareUtil.IsTx)
                   zonelist = CacheFactory.ZoneCache.GetWanBaZone(longType);
               else if (_aloneZoneList.Contains(platform))
                   zonelist = CacheFactory.ZoneCache.GetAllAloneZone();
               else if (_qunheiAnd9G.Contains(platform))
                   zonelist = CacheFactory.ZoneCache.GetQunheiAnd9GZone();
               else if (_H5_Egret.Contains(platform))
                   zonelist = CacheFactory.ZoneCache.GetEgterZone();
               else if (_h5_Bear.Contains(platform))
                   zonelist = CacheFactory.ZoneCache.GetBearZone();
               else
                   zonelist = CacheFactory.ZoneCache.GetAllMixtureZone();

               if (ShareUtil.IsQunHei)
                   zonelist = CacheFactory.ZoneCache.GetQunHeiZone();
               List<GetAllZone> resultList = new List<GetAllZone>();

               foreach (var item in zonelist)
               {
                   if (item.States == 5)
                   {
                       if (!_H5_A8Debug.Contains(account))
                           continue;
                   }
                   if (_aloneZoneList.Contains(platform)) //暂时这样处理。后续去掉这限制
                   {
                       if (item.PlatformZoneName == "1" || item.PlatformZoneName == "2")
                       {
                           if (platform != "h5_1758")
                               continue;
                       }
                   }
                   GetAllZone entity = new GetAllZone();
                   entity.PlatForm = platform;
                   entity.ZoneId = item.PlatformZoneName;
                   entity.ZoneName = item.Name;
                   entity.Maintain = item.Maintain;
                   entity.Host = item.ApiUrl;
                   entity.ZoneStates = item.States;
                   resultList.Add(entity);
               }
               response.Data.ZoneList = resultList;
           }
           catch (Exception ex)
           {
               SystemlogMgr.Error("获取用户登录记录", ex);
               response.Code = (int) MessageCode.NbParameterError;
           }
           return response;
       }

       /// <summary>
       /// 获取平台的所有区信息
       /// </summary>
       /// <param name="platfrom"></param>
       /// <returns></returns>
       public GetAllZoneListResponse GetAllZoneInfo(string platfrom)
       {
           GetAllZoneListResponse response = new GetAllZoneListResponse();
           response.Data = new GetAllZoneList();
           try
           {
              var list = CacheFactory.ZoneCache.GetZoneListByPlatForm(platfrom);
               List<GetAllZone> resultList = new List<GetAllZone>();
               foreach (var item in list)
               {
                   GetAllZone entity = new GetAllZone();
                   entity.PlatForm = platfrom;
                   entity.ZoneId = item.PlatformZoneName;
                   entity.ZoneName = item.Name;
                   resultList.Add(entity);
               }
              // response.Data.ZoneList = resultList;
           }
           catch (Exception ex)
           {
               SystemlogMgr.Error("获取所有区", ex);
               response.Code = (int) MessageCode.NbParameterError;
           }
           return response;
       }
   }
}
