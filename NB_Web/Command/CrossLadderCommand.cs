using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class CrossLadderCommand : BaseCommand
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
                case "gmi":
                    var gmi = reader.LadderGetManagerInfo(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(gmi);
                    break;
                case "at":
                    //检查时间
                    if (DateTime.Now >= ConvertToDateTime(3600) &&
                        DateTime.Now < ConvertToDateTime(28800))
                    {
                        var response1 = new MessageCodeResponse() { Code = (int)MessageCode.LadderClose };
                        OutputHelper.Output(response1);
                    }
                    else
                    {
                        var at = reader.LadderAttend(ShareUtil.ZoneName, UserAccount.ManagerId);
                        OutputHelper.Output(at);
                    }
                    break;
                case "ht":
                    var ht = reader.LadderHeart(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(ht);
                    break;
                case "lv":
                    var lv = reader.LadderLeave(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(lv);
                    break;
                case "getmatchlist":
                    var gml = reader.LadderGetMatchList(UserAccount.ManagerId);
                    OutputHelper.Output(gml);
                    break;
                case "getmatch":
                    var matchId = GetParamGuid("matchid");
                    if (!CheckParam(matchId))
                        return;
                    var gmh = reader.LadderGetMatch(UserAccount.ManagerId, matchId);
                    OutputHelper.Output(gmh);
                    break;
                case "e":
                    var exchangeKey = GetParamInt("ei");
                    if (!CheckParam(exchangeKey))
                        return;
                    var e = reader.LadderExchange(ShareUtil.ZoneName, UserAccount.ManagerId, exchangeKey);
                    OutputHelper.Output(e);
                    break;
                case "mq":
                    var mq = reader.LadderGetMatchMarqueeResponse(ShareUtil.ZoneName);
                    OutputHelper.Output(mq);
                    break;
                case "bm":
                    var bm = reader.LadderBuyStamina(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(bm);
                    break;
                case "hi":
                    var hi = reader.LadderGetHookInfoResponse(UserAccount.ManagerId);
                    OutputHelper.Output(hi);
                    break;
                case "hst":
                    int maxTimes = GetParamInt("mt");
                    int minScore = GetParamInt("mins");
                    int maxScore = GetParamInt("maxs");
                    int winTimes = GetParamInt("wt");

                    var hst = reader.LadderStartHook(ShareUtil.ZoneName, UserAccount.ManagerId, maxTimes, minScore, maxScore, winTimes);
                    OutputHelper.Output(hst);
                    break;
                case "hsp":
                    var hsp = reader.LadderStopHook(ShareUtil.ZoneName, UserAccount.ManagerId);
                    OutputHelper.Output(hsp);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }

        }
        private readonly CrossDataClient reader = new CrossDataClient();

        private static DateTime ConvertToDateTime(int second)
        {
            return DateTime.Today.AddSeconds(second);
        }
    }
}