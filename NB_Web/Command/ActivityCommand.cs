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
    public class ActivityCommand : BaseCommand
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
                case "ga":
                    int activityId = GetParamInt("ai");
                    if (!CheckParam(activityId))
                        return;
                    var ga = reader.GetActivityInfo(UserAccount.ManagerId,activityId);
                    OutputHelper.Output(ga);
                    break;
                case "gr":
                    int activityId1 = GetParamInt("ai");
                    if (!CheckParam(activityId1))
                        return;
                    int activityStep = GetParamInt("as");
                    if (!CheckParam(activityStep))
                        return;
                    var gr = reader.PrizeReceive(UserAccount.ManagerId, activityId1,activityStep);
                    OutputHelper.Output(gr);
                    break;
                case "gae":
                    var gae = reader.GetActivityExList();
                    OutputHelper.Output(gae);
                    break;
                case "guae":
                    int activityId2 = GetParamInt("ai");
                    if (!CheckParam(activityId2))
                        return;
                    var guae = reader.GetUserActivityEx(UserAccount.ManagerId, activityId2);
                    OutputHelper.Output(guae);
                    break;
                case "gre":
                    var exRecordId = GetParamInt("ei");
                    var itemCode = GetParamInt("itemcode");
                    var iscount = GetParamInt("ic");
                    if (!CheckParam(exRecordId))
                        return;
                    var gre = reader.ExPrizeReceive(UserAccount.ManagerId, exRecordId, itemCode, iscount);
                    OutputHelper.Output(gre);
                    break;
                case"mp"://返回是否能够购买vip礼包和礼包参数
                    var mp = reader.HasVipPackage(UserAccount.ManagerId);
                    OutputHelper.Output(mp);
                    break;
                case "bmp":
                    var packageId = GetParamInt("pid");
                    var bmp = reader.BuyVipPackage(UserAccount.ManagerId, packageId);
                    OutputHelper.Output(bmp);
                    break;
                case "sg":
                    var shareType = GetParamInt("shareType");
                    if (shareType == 0)
                        shareType = 1;
                    var sharegame = reader.DoShare(UserAccount.ManagerId, shareType);
                    OutputHelper.Output(sharegame);
                    break;
                case "gsg":
                    var shareType1 = GetParamInt("shareType");
                    if (shareType1 == 0)
                        shareType1 = 1;
                    var response = reader.GetShareInfo(UserAccount.ManagerId, shareType1);
                    OutputHelper.Output(response);
                    break;
                case"st":
                    OutputHelper.Output(reader.ShareTask(UserAccount.ManagerId));
                    break;
                case "gon":
                    OutputHelper.Output(reader.GetManagerInfo(UserAccount.ManagerId));
                    break;
                case "oex":
                    var exchangeType = GetParamInt("et");
                    OutputHelper.Output(reader.Exchange(UserAccount.ManagerId, exchangeType));
                    break;
                //case "tr":
                //    var exRecordId2 = GetParamInt("ei");
                //    if(!CheckParam(exRecordId2))
                //        return;
                //    var playerId = GetParamInt("ti");
                //    var tr = reader.TeammemberReceive(UserAccount.ManagerId, exRecordId2, playerId);
                //    OutputHelper.Output(tr);
                //    break;
                //case "ii":
                //    InvestInfo();
                //    break;
                //case "id":
                //    InvestDeposit();
                //    break;
                //case"rbp":
                //    ReceiveBindPoint();
                //    break;
                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }
        }



        //void InvestInfo()
        //{
        //    var response = reader.InvestInfo(UserAccount.ManagerId);
        //    OutputHelper.Output(response);
        //}

        //void InvestDeposit()
        //{
        //    var step = GetParamInt("st");
        //    if (!CheckParam(step))
        //        return;
        //    var response = reader.InvestDeposit(UserAccount.ManagerId, step);
        //    OutputHelper.Output(response);
        //}

        //void ReceiveBindPoint()
        //{
        //    var index = GetParamInt("id");
        //    var response = reader.ReceiveBindPoint(UserAccount.ManagerId, index);
        //    OutputHelper.Output(response);
        //}

        private readonly ActivityClient reader = new ActivityClient();
    }
}