using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class DailycupCommand : BaseCommand
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
                case "getdailycupdata":
                    GetDailycupData();
                    break;
                case "attend":
                    Attend();
                    break;
                case "attendgambletask":
                    AttendGambleTask();
                    break;
                case "getmydailycupmatch":
                    GetMyDailycupMatch();
                    break;
                case "attendgamble":
                    AttendGamble();
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly DailycupClient reader = new DailycupClient();

        void GetDailycupData()
        {
            var dailycupId = GetParamInt("dailycupId");
            if (dailycupId<0)
            {
                OutputHelper.OutputParameterError();
                return;
            }
            var response = reader.GetDailycupData(UserAccount.ManagerId, dailycupId);
            OutputHelper.Output(response);
        }

        void Attend()
        {
            var response = reader.Attend(UserAccount.ManagerId,HasTask);
            OutputHelper.Output(response);
        }

        void GetMyDailycupMatch()
        {
            var response = reader.GetMyDailycupMatch(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void AttendGamble()
        {
            int gamblePoint = GetParamInt("gamblePoint");
            int gambleResult = GetParamInt("gambleresult");
            Guid matchId = GetParamGuid("matchid");
            if (!CheckParam(gamblePoint))
                return;
            if(!CheckParam(gambleResult))
                return;
            if(!CheckParam(matchId))
                return;
            var response = reader.AttendGamble(UserAccount.ManagerId, gamblePoint, gambleResult, matchId, HasTask);
            if (response.Code == (int) MessageCode.Success ||
                response.Code == (int) MessageCode.DailycupGamblePointLimit
                || response.Code == (int) MessageCode.DailycupGamebleCountLimit)
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);  
                HttpContext.Current.Response.Write(OutputHelper.GetJson(response));
            }
            else
            {
                OutputHelper.Output(response);
            }
            
        }

        void AttendGambleTask()
        {
            var response = reader.AttendGambleTask(UserAccount.ManagerId, HasTask);
            OutputHelper.Output(response);
        }

    }
}