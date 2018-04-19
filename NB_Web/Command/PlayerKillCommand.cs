using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class PlayerKillCommand:BaseCommand
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
                case "gi":
                    var gi = reader.GetInfo(UserAccount.ManagerId);
                    OutputHelper.Output(gi);
                    break;
                case "go":
                    var oname = GetParam("oname");
                    var go = reader.GetOpponents(UserAccount.ManagerId, oname);
                    OutputHelper.Output(go);
                    break;
                case "fi":
                    var awayId = GetParamGuid("ai");
                    if (!CheckParam(awayId))
                        return;
                    var fi = reader.Fight(UserAccount.ManagerId, awayId, HasTask);
                    OutputHelper.Output(fi);
                    break;
                case "gr":
                    var matchId = GetParamGuid("mi");
                    if (!CheckParam(matchId))
                        return;
                    var gr = reader.GetMatchResult(UserAccount.ManagerId, matchId);
                    OutputHelper.Output(gr);
                    break;
                case "lr":
                    var matchId1 = GetParamGuid("mi");
                    if (!CheckParam(matchId1))
                        return;
                    var lr = reader.Lottery(UserAccount.ManagerId, matchId1);
                    OutputHelper.Output(lr);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }
        }

        private readonly PlayerKillClient reader = new PlayerKillClient();
    }
}