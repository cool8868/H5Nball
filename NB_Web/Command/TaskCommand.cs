using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class TaskCommand : BaseCommand
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
                case "gtl":
                    var response = reader.GetTaskListResponse(UserAccount.ManagerId);
                    OutputHelper.Output(response);
                    break;
                case "st":
                    var recordId = GetParamInt("ri");
                    if(!CheckParam(recordId))
                        return;
                    var response2 = reader.SubmitTask(UserAccount.ManagerId,recordId);
                    OutputHelper.Output(response2);
                    break;
                case "sdt":
                    var taskId = GetParamInt("t");
                    if (!CheckParam(taskId))
                        return;
                    var response5 = reader.SubmitDailyTask(UserAccount.ManagerId, taskId);
                    OutputHelper.Output(response5);
                    break;
                case "rgp":
                    var rgp = reader.ReceiveGuidePrize(UserAccount.ManagerId);
                    OutputHelper.Output(rgp);
                    break;
                case "ctl":
                    var ctl = reader.GetTaskCompleteListResponse(UserAccount.ManagerId);
                    OutputHelper.Output(ctl);
                    break;

                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly TaskClient reader = new TaskClient();
    }
}