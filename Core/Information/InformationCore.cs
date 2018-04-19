using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Dpm.Core.Activity;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
//using Games.NBall.Core.Active;
//using Games.NBall.Core.Activity;
//using Games.NBall.Core.Giants;
using Games.NBall.Core.Friend;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.ManagerSkill;
using Games.NBall.Core.Task;
//using Games.NBall.Core.Tour;
//using Games.NBall.Core.WorldChallenge;
//using Games.NBall.Core.Guild;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Information;
using Games.NBall.Entity.Response;

namespace Games.NBall.Core.Information
{
    public class InformationCore
    {
        #region .ctor
        public InformationCore(int p)
        {
        }
        #endregion

        #region Facade
        public static InformationCore Instance
        {
            get { return SingletonFactory<InformationCore>.SInstance; }
        }

        public InformationPopupResponse GetPopup(Guid managerId, int recordId)
        {
            var entity = InformationPopupMgr.GetById(recordId);
            if (entity==null || entity.ManagerId != managerId)
                return ResponseHelper.InvalidParameter<InformationPopupResponse>();
            InformationPopupMgr.Delete(managerId, recordId);
            var response = ResponseHelper.CreateSuccess<InformationPopupResponse>();
            response.Data = entity;
            return response;
        }

        public MessageCodeResponse PopupRead(Guid managerId, int recordId)
        {
            InformationPopupMgr.Delete(managerId, recordId);
            var response = ResponseHelper.CreateSuccess<MessageCodeResponse>();
            return response;
        }

        public InformationResponse GetInformation(Guid managerId)
        {
            var response = ResponseHelper.CreateSuccess<InformationResponse>();
            response.Data = new InformationEntity();
            response.Data.TaskFinish = TaskCore.Instance.HasTaskComplete(managerId);
            //活动
            //response.Data.NewActivity = false;
            response.Data.NewFriend = FriendCore.Instance.HasFriendRequest(managerId);
            response.Data.MailUnRead = MailCore.Instance.HasUnReadMail(managerId);
            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            if (managerExtra != null)
            {
                response.Data.TalentUnUsed = managerExtra.SkillPoint > 0;
            }
            response.Data.ActivityComplete = new List<int>();

            var activityList = ActivityRecordMgr.GetCompleteByManager(managerId);
            foreach (var entity in activityList)
            {
                if (!response.Data.ActivityComplete.Contains(entity.ActivityId))
                {
                    response.Data.ActivityComplete.Add(entity.ActivityId);
                }

            }

            response.Data.ActivityExComplete = new List<int>();
            var activityExList = ActivityexRecordMgr.GetCompleteByManager(managerId);
            foreach (var entity in activityExList)
            {
                if (!response.Data.ActivityExComplete.Contains(entity.ExcitingId))
                {
                    response.Data.ActivityExComplete.Add(entity.ExcitingId);
                }
            }

            response.Data.IsHaveDailyAttendance = !DailyAttendance.Instance.GetIsAttendance(managerId);
            response.Data.IsHaveBuyPoint = false;
            if (MallCore.Instance._buyPointPoss.ContainsKey(managerId))
            {
                response.Data.IsHaveBuyPoint = true;
                bool b = false;
                MallCore.Instance._buyPointPoss.TryRemove(managerId, out b);
            }

            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager != null)
            {
                response.Data.ManagerLevel = manager.Level;
                response.Data.ManagerExp = manager.EXP;
            }
            return response;
        }

        //public InformationHookResponse GetHookResponse(Guid managerId)
        //{
        //    var response = ResponseHelper.CreateSuccess<InformationHookResponse>();
        //    response.Data = GetHookEntity(managerId);
        //    return response;
        //}

        //public InformationHookEntity GetHookEntity(Guid managerId)
        //{
        //    var entity = TourThread.Instance.GetInformationHookEntity(managerId);
        //    if (entity == null)
        //    {
        //        entity = WorldChallengeCore.Instance.GetInformationHookEntity(managerId);
        //        if (entity == null)
        //        {
        //           // entity = GiantsChallengeThread.Instance.GetInformationHookEntity(managerId);
        //            if (entity == null)
        //            {
        //                entity = new InformationHookEntity();
        //                entity.HookType = 0;
        //            }
        //        }
        //    }
        //    return entity;
        //}
        #endregion
    }
}
