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
    public class CrossArenaCommand : BaseCommand
    {
        public void Dispatch(string action)
        {
            //校验，如有接口不需校验，需加在下面
            if (true)
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
                case "gar":
                    var gar = reader.GetArenaResponse(UserAccount.ManagerId,ShareUtil.ZoneName);
                    OutputHelper.Output(gar);
                    break;
                case "go":
                    var go = reader.GetOpponent(UserAccount.ManagerId, ShareUtil.ZoneName);
                    OutputHelper.Output(go);
                    break;
                case "bsp":
                    var bsp = reader.BuyStaminaPara(UserAccount.ManagerId, ShareUtil.ZoneName);
                    OutputHelper.Output(bsp);
                    break;
                case "bs":
                    var bs = reader.BuyStamina(UserAccount.ManagerId, ShareUtil.ZoneName);
                    OutputHelper.Output(bs);
                    break;
                case "fh":
                    var opponentId = GetParamGuid("oid");
                    var fh = reader.Fight(UserAccount.ManagerId, opponentId, ShareUtil.ZoneName);
                    OutputHelper.Output(fh);
                    break;
                case "gs":
                    var gs = reader.GetStamina(UserAccount.ManagerId, ShareUtil.ZoneName);
                    OutputHelper.Output(gs);
                    break;
                case "ga":
                    var managerId = GetParamGuid("mid");
                    var zoneName = GetParam("zn");
                    var ga = reader.SolutionAndTeammemberResponse(managerId, zoneName);
                    OutputHelper.Output(ga);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }
        }




        private readonly CrossDataClient reader = new CrossDataClient();
    }
}