using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class MatchDataCommand : BaseCommand
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
                case "fightinfo":
                    FightInfo();
                    break;
                case "ladderfightinfo":
                    LadderFightInfo();
                    break;
                case "leaguefightinfo":
                    LeagueFightInfo();
                    break;
                case "getprocess":
                    GetProcess();
                    break;
                case "crosspreview":
                    FightInfo();
                    break;
                case "afight":
                    ArenaFightInfo();
                    break;
                case "hitdown":
                    CheckReward();
                    break;
                case "hitup":
                    CommitReward();
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly MatchDataClient reader = new MatchDataClient();
        private readonly CrossDataClient reader1 = new CrossDataClient();
       

        void FightInfo()
        {
            var awayId = GetParamGuid("awayId");
            string siteId = GetParam("siteid");
            int ownSide = GetParamInt("side");
            var awayIsbot = GetParamBool("awayisbot");
            if (awayId == Guid.Empty)
            {
                OutputHelper.OutputParameterError();
                return;
            }
            if (string.IsNullOrEmpty(siteId))
                OutputHelper.Output(reader.GetFightInfo(UserAccount.ManagerId, awayId));
        }

        void LadderFightInfo()
        {
            var matchId = GetParamGuid("matchId");
            if (matchId==Guid.Empty)
            {
                OutputHelper.OutputParameterError();
                return;
            }
            var response = reader.GetLadderFightInfo(matchId,UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void LeagueFightInfo()
        {
            var matchId = GetParamGuid("matchId");
            if (matchId == Guid.Empty)
            {
                OutputHelper.OutputParameterError();
                return;
            }
            var response = reader.GetLeagueFightInfo(matchId, UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        private void ArenaFightInfo()
        {
            var matchId = GetParamGuid("matchId");
            if (matchId == Guid.Empty)
            {
                OutputHelper.OutputParameterError();
                return;
            }
            var response = reader1.GetArenaFightInfo(matchId);
            OutputHelper.Output(response);
        }

        void GetProcess()
        {
            int matchType = GetParamInt("matchType");
            Guid matchId = GetParamGuid("matchId");
            if (matchId==Guid.Empty || matchType <= 0)
            {
                OutputHelper.Output((int)MessageCode.NbParameterError);
                return;
            }
            MatchProcessResponse response = null;
            if (matchType == (int) EnumMatchType.Arena || matchType == (int) EnumMatchType.CrossCrowd)
            {
                response = reader1.GetMatchProcess(matchId, matchType);
            }
            else
                response = reader.GetMatchProcess(matchId, matchType);
            
            if (response.Code != (int) MessageCode.Success)
            {
                OutputHelper.Output(response.Code);
            }
            else if( response.Data==null)
            {
                OutputHelper.Output((int)MessageCode.MatchMiss);
            }
            else
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);  
                HttpContext.Current.Response.BinaryWrite(response.Data);
            }
        }

        void MatchTest()
        {
            var response = reader.MatchTest(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void CheckReward()
        {
            int matchType = GetParamInt("matchType");
            Guid matchId = GetParamGuid("matchId");
            if (!CheckMatchParam(matchType,matchId))
            {
                OutputHelper.Output((int)MessageCode.NbParameterError);
                return;
            }
            var response = reader.CheackReward(UserAccount.ManagerId, matchType,matchId);
            OutputHelper.Output(response);
        }

        void CommitReward()
        {
            int matchType = GetParamInt("matchType");
            Guid matchId = GetParamGuid("matchId");
            string mask = GetParam("mask");
            string sig = GetParam("sig");
            if (!CheckMatchParam(matchType, matchId) || string.IsNullOrEmpty(mask))
            {
                OutputHelper.Output((int)MessageCode.NbParameterError);
                return;
            }
            var response = reader.CommitReward(UserAccount.ManagerId, matchType, matchId, mask, sig);
            OutputHelper.Output(response);
        }

        bool CheckMatchParam(int matchType, Guid matchId)
        {
            return matchType > 0 && matchId != Guid.Empty;
        }
    }
}