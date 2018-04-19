using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.ServiceContract.Client.Client;

namespace Games.NBall.NB_Web.Command
{
    public class RevelationCommand : BaseCommand
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
                case "getminfo":
                    var getmanager = reader.RevelationGetManagerId(UserAccount.ManagerId);
                    OutputHelper.Output(getmanager);
                    break;
                case "sm":
                    var mark =GetParamInt("mark");
                    if (!CheckParam(mark))
                        return;
                    var sm = reader.StartMark(UserAccount.ManagerId, mark);
                    OutputHelper.Output(sm);
                    break;
                case "gmi":
                    var gmi = reader.GetMarkInfo(UserAccount.ManagerId);
                    OutputHelper.Output(gmi);
                    break;
                case "rthegame":
                    var rthegame = reader.RevelationTheGame(UserAccount.ManagerId);
                    OutputHelper.Output(rthegame);
                    break;
                case "gdi":
                    var drawId = GetParamGuid("di");
                    var gdi = reader.GetDrawInfo(UserAccount.ManagerId, drawId);
                    OutputHelper.Output(gdi);
                    break;
                case "dr":
                    var drawId1 = GetParamGuid("di");
                    if (!CheckParam(drawId1))
                        return;
                    var dr = reader.DrawResult(UserAccount.ManagerId, drawId1);
                    OutputHelper.Output(dr);
                    break;
                case "addm":
                    var addm = reader.RevelationAddMorale(UserAccount.ManagerId);
                    OutputHelper.Output(addm);
                    break;
                case "rrm":
                    var rrm = reader.RevelationResetMark(UserAccount.ManagerId);
                    OutputHelper.Output(rrm);
                    break;
                case "rgsi":
                    var rgsi = reader.RevelationGetShopInfo(UserAccount.ManagerId);
                    OutputHelper.Output(rgsi);
                    break;
                case "rexi":
                    var idx = GetParamInt("idx");
                    var rexi = reader.RevelationPurchaseItems(UserAccount.ManagerId, idx);
                    OutputHelper.Output(rexi);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }
        }

        private readonly RevelationClient reader = new RevelationClient();
    }
}