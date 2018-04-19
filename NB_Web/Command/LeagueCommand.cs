using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class LeagueCommand : BaseCommand
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
                case "gl":
                    GetLeagueLock();
                    break;
                case "gli":
                    GetLeagueInfo();
                    break;
                case "sl":
                    StarLeague();
                    break;
                case "rl":
                    ResetLeague();
                    break;
                case "fm":
                    FightMatch();
                    break;
                case "gapl":
                    GetAllPrizeInfo();
                    break;
                case "grp":
                    GetRankPrize();
                    break;
                case "gr":
                    GetRank();
                    break;
                case "lmg":
                    GetLeagueMallInfo();
                    break;
                case "lme":
                    Exchange();
                    break;
                case "lmr":
                    RefreshExchange();
                    break;
                case "gwcpi":
                    GetWincountPrizeInfo();
                    break;
                case "gwcp":
                    GetWincountPrize();
                    break;
                case "glf":
                    GetLeagueFightMap();
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly LeagueClient reader = new LeagueClient();
        
        void GetLeagueLock()
        {
            var response = reader.GetLeagueLock(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void GetLeagueInfo()
        {
            int leagueId = GetParamInt("l");
            var response = reader.GetLeagueInfo(UserAccount.ManagerId, leagueId);
            OutputHelper.Output(response);
        }

        void StarLeague()
        {
            int leagueId = GetParamInt("l");
            var response = reader.StarLeague(UserAccount.ManagerId, leagueId);
            OutputHelper.Output(response);
        }

        void ResetLeague()
        {
            int leagueId = GetParamInt("l");
            var response = reader.ResetLeague(UserAccount.ManagerId, leagueId);
            OutputHelper.Output(response);
        }

        void FightMatch()
        {
            int leagueId = GetParamInt("m");
            var response = reader.FightMatch(UserAccount.ManagerId, leagueId);
            OutputHelper.Output(response);
        }

        void GetAllPrizeInfo()
        {
            var response = reader.GetAllPrizeInfo(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void GetRankPrize()
        {
            Guid leagueRecordId = GetParamGuid("lr");
            var response = reader.GetRankPrize(UserAccount.ManagerId, leagueRecordId);
            OutputHelper.Output(response);
        }

        void GetRank()
        {
            int leagueId = GetParamInt("l");
            var response = reader.GetRank(UserAccount.ManagerId, leagueId);
            OutputHelper.Output(response);

        }

        void GetLeagueMallInfo()
        {
            var response = reader.GetLeagueMallInfo(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void Exchange()
        {
            string exkey = GetParam("k");
            var response = reader.Exchange(UserAccount.ManagerId, exkey);
            OutputHelper.Output(response);
        }

        void RefreshExchange()
        {
            var response = reader.RefreshExchange(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void GetWincountPrizeInfo()
        {
            int leagueId = GetParamInt("l");
            var response = reader.GetWincountPrizeInfo(UserAccount.ManagerId,leagueId);
            OutputHelper.Output(response);
        }

        void GetWincountPrize()
        {
            int leagueId = GetParamInt("l");
            int countType = GetParamInt("t");
            var response = reader.GetWincountPrize(UserAccount.ManagerId, leagueId,countType);
            OutputHelper.Output(response);
        }

        private void GetLeagueFightMap()
        {
            int leagueId = GetParamInt("l");
            int round = GetParamInt("r");
            var response = reader.GetLeagueFigMap(UserAccount.ManagerId, leagueId, round);
            OutputHelper.Output(response);
        }
    }
}