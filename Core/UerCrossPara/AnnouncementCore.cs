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
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response;

namespace Games.NBall.Core
{
   public class AnnouncementCore
   {
       private Dictionary<string, AnnouncementEntity> _announcementDic;
       private AnnouncementEntity _TopAnnouncementDic;
       private Timer _timer1 = null;
       public AnnouncementCore(int p)
       {
           Init();
            _timer1 = new Timer();
            _timer1.Interval = 600000;
            _timer1.Elapsed += Timer_Elapsed;
            _timer1.Start();
       }

       #region Instance

       private void Init()
       {
           _announcementDic = new Dictionary<string, AnnouncementEntity>();
           _TopAnnouncementDic = null;
           var allAnnouncement = AnnouncementMgr.SelectAnnouncement("");
           foreach (var item in allAnnouncement)
           {
               if (item.IsTop)
               {
                   if (_TopAnnouncementDic == null)
                       _TopAnnouncementDic = item;
                   continue;
               }
               if (!_announcementDic.ContainsKey(item.Platform))
                   _announcementDic.Add(item.Platform, item);
               else
               {
                   if (_announcementDic[item.Platform].Idx < item.Idx)
                       _announcementDic[item.Platform] = item;
               }
           }
       }

       private void Timer_Elapsed(object sender, ElapsedEventArgs e)
       {
           _timer1.Stop();
           Init();
           _timer1.Start();
       }

       public static AnnouncementCore Instance
       {
           get { return SingletonFactory<AnnouncementCore>.SInstance; }
       }

       private AnnouncementEntity GetAnnouncement(string platform)
       {
           if (_announcementDic.ContainsKey(platform))
               return _announcementDic[platform];
           return null;
       }

       #endregion

       /// <summary>
       /// 获取公告
       /// </summary>
       /// <param name="platform"></param>
       /// <returns></returns>
       public GetPlatformAnnouncementResponse GetPlatformAnnouncement(string platform = "")
       {
           GetPlatformAnnouncementResponse response = new GetPlatformAnnouncementResponse();
           response.Data = new GetPlatformAnnouncement();
           try
           {
               if (platform == "")
                   platform = "all";
               var announcement = GetAnnouncement(platform);
               if (announcement == null)
                   announcement = GetAnnouncement("all");
               response.Data.AnnouncementList = new List<AnnouncementEntity>();
               if (announcement != null)
                   response.Data.AnnouncementList.Add(announcement);
               if (_TopAnnouncementDic != null)
                   response.Data.AnnouncementList.Add(_TopAnnouncementDic);

           }
           catch (Exception ex)
           {
               SystemlogMgr.Error("获取公告", ex);
               response.Code = (int) MessageCode.NbParameterError;
           }
           return response;
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
       public bool SetPlatformAnnouncement(string  platform, bool isTop,string title, string  contentString, DateTime startTime, DateTime endTime, DbTransaction trans = null, string zoneId = "")
       {
           try
           {
               return AnnouncementMgr.Release(platform, isTop, title, contentString, startTime, endTime,trans,zoneId);

           }
           catch (Exception ex)
           {
               SystemlogMgr.Error("获取公告", ex);
               return false;
           }
       }
       /// <summary>
       /// 启用公告
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
       public bool RanablePlatformAnnouncement(int idx, bool isTop, DateTime startTime, DateTime endTime, DbTransaction trans = null, string zoneId = "")
       {
           try
           {
               return AnnouncementMgr.Ranable(idx, isTop, startTime, endTime,trans,zoneId);

           }
           catch (Exception ex )
           {
               SystemlogMgr.Error("增加公告", ex);
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
       public bool ClosePlatformAnnouncement(int idx, DbTransaction trans = null, string zoneId = "")
       {
           try
           {
               return AnnouncementMgr.CloseAnnouncement(idx,trans,zoneId);

           }
           catch (Exception ex)
           {
               SystemlogMgr.Error("关闭公告", ex);
               return false;
           }
       }
       /// <summary>
       /// 删除公告
       /// </summary>
       /// <param name="idx"></param>
       /// <param name="trans"></param>
       /// <param name="zoneId"></param>
       /// <returns></returns>
       public bool DeleteAnnouncement(int idx, DbTransaction trans = null, string zoneId = "")
       {
           try
           {
               return AnnouncementMgr.Delete(idx, trans, zoneId);

           }
           catch (Exception ex)
           {
               SystemlogMgr.Error("删除公告", ex);
               return false;
           }
       }
   }
}
