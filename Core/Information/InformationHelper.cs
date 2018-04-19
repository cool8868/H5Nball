using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
//using Games.NBall.Entity.Enums.Mail;
using Games.NBall.Entity.Support.Custom;

namespace Games.NBall.Core.Information
{
    public class InformationHelper
    {
        #region Facade
        public static void SendAddFriendPop(Guid managerId,string byName)
        {
            InformationPopupEntity entity = new InformationPopupEntity();
            entity.ManagerId = managerId;
            entity.ContentString = string.Format("N,{0}",byName);
            entity.PopType = (int) EnumPopType.InformationPopAddFriend;
            entity.RowTime = DateTime.Now;
            entity.Status = 0;
            SendPopup(entity);
        }

        public static void SendGamblePrizePoolPop(Guid managerId, string matchName)
        {
            InformationPopupEntity entity = new InformationPopupEntity();
            entity.ManagerId = managerId;
            entity.ContentString = string.Format("M,{0}", matchName);
            entity.PopType = (int)EnumPopType.GamblePrizePool;
            entity.RowTime = DateTime.Now;
            entity.Status = 0;
            SendPopup(entity);
        }

        //public static List<InformationNoticeShowEntity> GetNoticeShowList()
        //{
        //    int zoneId = ShareUtil.ZoneId;
        //    var list = MemcachedFactory.NoticeClient.Get<List<InformationNoticeShowEntity>>(zoneId);
        //    if (list == null)
        //    {
        //        lock (_lockObj)
        //        {
        //            list = MemcachedFactory.NoticeClient.Get<List<InformationNoticeShowEntity>>(zoneId);
        //            if (list == null)
        //            {
        //                var list2 = InformationNoticeMgr.GetByZoneId(zoneId);
        //                if (list2 == null)
        //                {
        //                    list = new List<InformationNoticeShowEntity>(0);
        //                }
        //                else
        //                {
        //                    list = new List<InformationNoticeShowEntity>(list2.Count);
        //                    foreach (var entity in list2)
        //                    {
        //                        list.Add(new InformationNoticeShowEntity(){Idx = entity.Idx,ContentString = entity.ContentString,Frequency = entity.Frequency,
        //                        NoticeType = entity.NoticeType,
        //                        StartTimeTick = ShareUtil.GetTimeTick(entity.StartTime),
        //                        EndTimeTick = ShareUtil.GetTimeTick(entity.EndTime)});
        //                    }
        //                }
        //                MemcachedFactory.NoticeClient.Set(zoneId.ToString(), list);
        //            }
        //        }
        //    }
        //    return list;
        //}
        #endregion
        
        #region encapsulation
        static object _lockObj = new object();
        static readonly NBThreadPool _threadPool = new NBThreadPool(10);

        static void SendPopup(InformationPopupEntity entity)
        {
            _threadPool.Add(() => doSendPopup(entity));
        }

        static void doSendPopup(InformationPopupEntity entity)
        {
            try
            {
                InformationPopupMgr.Insert(entity);
                string content = string.Format("RF,{0}", entity.Idx);
                //ChatHelper.doSendPop(entity.ManagerId, EnumPopType.AddFriend, content);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("doSendPopup", ex);
            }
        }
        #endregion
    }
}
