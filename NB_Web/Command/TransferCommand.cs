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
    public class TransferCommand : BaseCommand
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
                case "ai":
                   var itemId = GetParamGuid("ii");
                    var price = GetParamInt("p");
                    var ai = reader.AuctionItem(UserAccount.ManagerId, ShareUtil.ZoneName, itemId, price, 0);
                    OutputHelper.Output(ai);
                    break;
                case "gml":
                    var gml = reader.GetMyAuctionList(UserAccount.ManagerId, ShareUtil.ZoneName);
                    OutputHelper.Output(gml);
                    break;
                case "gtl":
                    var rankRule = GetParamInt("r");
                    var itemName = GetParam("in");
                    var pageSize = GetParamInt("ps");
                    var pageIndex = GetParamInt("pi");
                    var gtl = reader.GetTransferList(UserAccount.ManagerId,rankRule, itemName, ShareUtil.ZoneName,pageSize,pageIndex);
                    OutputHelper.Output(gtl);
                    break;
                case "gi":
                    var transferId = GetParamGuid("ti");
                    var gi = reader.GetTransferInfo(UserAccount.ManagerId, transferId, ShareUtil.ZoneName);
                    OutputHelper.Output(gi);
                    break;
                case "a":
                    var transferId1 = GetParamGuid("ti");
                    var a = reader.Auction(transferId1, UserAccount.ManagerId, ShareUtil.ZoneName);
                    OutputHelper.Output(a);
                    break;
                case "so":
                    var t2 = GetParamGuid("ti");
                    var so = reader.SoldOut(UserAccount.ManagerId, t2, ShareUtil.ZoneName);
                    OutputHelper.Output(so);
                    break;
                case "ga":
                    var ga = reader.GetActivityInfo(UserAccount.ManagerId, ShareUtil.ZoneName);
                    OutputHelper.Output(ga);
                    break;
                case "pr":
                    var pr = reader.Prize(UserAccount.ManagerId, ShareUtil.ZoneName);
                    OutputHelper.Output(pr);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }

        }
        private readonly CrossDataClient reader = new CrossDataClient();

      
    }
}