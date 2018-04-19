using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class LadderCommand : BaseCommand
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
                case "getmanagerinfo":
                    GetManagerInfo();
                    break;
                case "attend":
                    Attend();
                    break;
                case "heart":
                    Heart();
                    break;
                case "leave":
                    Leave();
                    break;
                case "getmatchlist":
                    GetMatchList();
                    break;
                case "getmatch":
                    GetMatch();
                    break;
                case "e":
                    var exchangeKey = GetParam("k");
                    if(!CheckParam(exchangeKey))
                        return;
                    var e = reader.Exchange(UserAccount.ManagerId, exchangeKey);
                    OutputHelper.Output(e);
                    break;
                case "mq":
                    var mq = reader.GetMatchMarqueeResponse();
                    OutputHelper.Output(mq);
                    break;
                case "re":
                    var re = reader.RefreshExchange(UserAccount.ManagerId);
                    OutputHelper.Output(re);
                    break;
                case "r":
                    GetRankList();
                    break;
                case "hi":
                    var hi = reader.GetHookInfoResponse(UserAccount.ManagerId);
                    OutputHelper.Output(hi);
                    break;
                case "hst":
                    int maxTimes = GetParamInt("mt");
                    int minScore= GetParamInt("mins");
                    int maxScore= GetParamInt("maxs");
                    int winTimes = GetParamInt("wt");

                    var hst = reader.StartHook(UserAccount.ManagerId,maxTimes,minScore,maxScore,winTimes);
                    OutputHelper.Output(hst);
                    break;
                case "clearcd":
                     var clearck = reader.LadderClearCD(UserAccount.ManagerId);
                     OutputHelper.Output(clearck);
                    break;
                case "clarecdp":
                    var clearckp = reader.LadderClearCDPara(UserAccount.ManagerId);
                    OutputHelper.Output(clearckp);
                    break;
                case "hsp":
                    var hsp = reader.StopHook(UserAccount.ManagerId);
                    OutputHelper.Output(hsp);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly LadderClient reader = new LadderClient();

        void GetManagerInfo()
        {
            var response = reader.GetManagerInfo(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void Attend()
        {
            //检查时间
            //if (DateTime.Now >= ConvertToDateTime(3600) && DateTime.Now < ConvertToDateTime(28800))
            //{
            //    var response1= new MessageCodeResponse(){Code = (int)MessageCode.LadderClose};
            //    OutputHelper.Output(response1);
            //}
            //else
            //{
                bool isGuide = GetParamBool("ig");
                var response = reader.AttendLadder(UserAccount.ManagerId, HasTask, isGuide);
                OutputHelper.Output(response);
            //}
        }

        void Heart()
        {
            var response = reader.LadderHeart(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void Leave()
        {
            var response = reader.LeaveLadder(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void GetMatchList()
        {
            var response = reader.GetMatchList(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void GetMatch()
        {
            Guid matchId = GetParamGuid("matchId");
            if (matchId==Guid.Empty)
            {
                OutputHelper.OutputParameterError();
                return;
            }
            var response = reader.GetMatch(UserAccount.ManagerId,matchId);
            OutputHelper.Output(response);
        }

        private static DateTime ConvertToDateTime(int second)
        {
            return DateTime.Today.AddSeconds(second);
        }

        void GetRankList()
        {
            var rl = reader.GetLadderRank(UserAccount.ManagerId, (int)EnumRankType.LadderRank, PageIndex, PageSize);
            OutputHelper.Output(rl);
        }
    }
}