using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Teammember;
using Games.NBall.Entity.Share;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.UAFacade;
using Games.NBall.WebClient.Weibo;
using Games.NBall.WebServerFacade;

namespace Games.NBall.NB_Web.Command
{
    public class TrainCommand : BaseCommand
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
                case "getteammemberlistfortrain":
                    GetTeammemberListForTrain();
                    break;
                case "starttrain":
                    StartTrain();
                    break;
                case "stoptrain":
                    StopTrain();
                    break;
                case "ght":
                    GetHelpTrainList();
                    break;
                case "ht":
                    HelpTrain();
                    break;
                case "otb":
                    OpenTrainBox();
                    break;
                case "gti":
                    GetTrainInfo();
                    break;
                case "ust":
                    SpeedUpTrain();
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly TrainThreadClient reader = new TrainThreadClient();

        void GetTrainInfo()
        {
            Guid teammemberId = GetParamGuid("ti");
            if (!CheckParam(teammemberId)) return;
            var response = reader.GetTrainInfo(UserAccount.ManagerId, teammemberId);
            OutputHelper.Output(response);
        }

        void GetTeammemberListForTrain()
        {
            var response = reader.GetTeammemberListForTrain(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void GetHelpTrainList()
        {
            var friendRecordId = GetParamInt("f");
            if (!CheckParam(friendRecordId))
                return;
            var response = reader.GetHelpTrainList(UserAccount.ManagerId, friendRecordId);
            OutputHelper.Output(response);
        }

        void HelpTrain()
        {
            var friendRecordId = GetParamInt("f");
            if (!CheckParam(friendRecordId))
                return;
            var trainId = GetParamGuid("t");
            if (!CheckParam(trainId))
                return;
            var response = reader.HelpTrain(UserAccount.ManagerId, friendRecordId, trainId, HasTask);
            OutputTrain(response);
        }

        void OutputTrain(TeammemberTrainActionResponse response)
        {
            if (response.Code == (int)MessageCode.TeammemberTrainFinish || response.Code == (int)MessageCode.Success)
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);  
                HttpContext.Current.Response.Write(OutputHelper.GetJson(response));
            }
            else
            {
                OutputHelper.Output(response.Code);
            }
        }

        void OpenTrainBox()
        {
            var friendRecordId = GetParamInt("f");
            if (!CheckParam(friendRecordId))
                return;
            var response = reader.OpenBox(UserAccount.ManagerId, friendRecordId);
            OutputHelper.Output(response);
        }

        void StartTrain()
        {
            Guid teammemberId = GetParamGuid("ti");
            if (!CheckParam(teammemberId))
                return;
            var response = reader.StartTrain(UserAccount.ManagerId, teammemberId);
            OutputHelper.Output(response);
        }

        void StopTrain()
        {
            Guid teammemberId = GetParamGuid("ti");
            if (!CheckParam(teammemberId))
                return;
            var response = reader.StopTrain(UserAccount.ManagerId, teammemberId);
            OutputHelper.Output(response);
        }

        void SpeedUpTrain()
        {
            Guid teammemberId = GetParamGuid("ti");
            int type = GetParamInt("t");
            if (!CheckParam(teammemberId))
                return;
            var response = reader.SpeedUpTrain(UserAccount.ManagerId, teammemberId, type);
            OutputHelper.Output(response);
        }

    }
}