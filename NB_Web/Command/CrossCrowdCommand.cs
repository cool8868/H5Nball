using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class CrossCrowdCommand : BaseCommand
    {
        public void Dispatch(string action)
        {
            //校验，如有接口不需校验，需加在下面
            if (false)
            {
                if (ValidatorAccountOnly() == false)
                    return;
            }
            else
            {
                if (Validator() == false)
                    return;
            }
            switch (action)
            {
                case "gci":
                    var gci = reader.CrowdGetCrowdInfo(ShareUtil.ZoneName);
                    OutputHelper.Output(gci);
                    break;
                case "gmi":
                    var gmi = reader.CrowdGetManagerInfo(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(gmi);
                    break;
                case "at":
                    var at = reader.CrowdAttend(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(at);
                    break;
                case "lv":
                    var lv = reader.CrowdLeave(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(lv);
                    break;
                case "ht":
                    var ht = reader.CrowdHeart(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(ht);
                    break;
                case "gmh":
                    var matchId = GetParamGuid("mi");
                    if (!CheckParam(matchId))
                        return;
                    var gmh = reader.CrowdGetMatch(UserAccount.ManagerId, matchId);
                    OutputHelper.Output(gmh);
                    break;
                case "cl":
                    var cl = reader.CrowdClearCd(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(cl);
                    break;
                case "re":
                    var re = reader.CrowdResurrection(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(re);
                    break;
                case "stb":
                    StartRobot();
                    break;
                case "spb":
                    StopRobot();
                    break;
                case "rbi":
                    RobotInfo();
                    break;
                case "gcm":
                    var gcm = reader.CrowdBanner(ShareUtil.ZoneName);
                    OutputHelper.OutputMsg(gcm);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }

        }
        private readonly CrossDataClient reader = new CrossDataClient();

        void StartRobot()
        {
            int hookMatchType = GetParamInt("hookMatchType");
            if (!CheckParam(hookMatchType))
                return;
            var response = reader.StartRobot(ShareUtil.ZoneName, UserAccount.ManagerId, hookMatchType);
            OutputHelper.Output(response);
        }

        void StopRobot()
        {
            int hookMatchType = GetParamInt("hookMatchType");
            if (!CheckParam(hookMatchType))
                return;
            var response = reader.StopRobot(UserAccount.ManagerId, hookMatchType);
            OutputHelper.Output(response);
        }

        void RobotInfo()
        {
            var response = reader.RobotInfo(ShareUtil.ZoneName, UserAccount.ManagerId);
            OutputHelper.Output(response);
        }
    }
}