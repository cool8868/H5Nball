using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Command
{
    public class WpfMatchDataCommand
    {
        public static string _moduleName = "MatchData";

        public static Match_FightinfoResponse Fightinfo(Guid awayId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("AwayId",awayId);
            return RequestHelper.Request<Match_FightinfoResponse>(_moduleName, "fightinfo", parameter);
        }

        public static Match_FightinfoResponse LadderFightinfo(Guid matchId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("MatchId", matchId);
            return RequestHelper.Request<Match_FightinfoResponse>(_moduleName, "ladderfightinfo", parameter);
        }

        public static Match_FightinfoResponse CrowdFightinfo(Guid matchId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("MatchId", matchId);
            return RequestHelper.Request<Match_FightinfoResponse>(_moduleName, "cfi", parameter);
        }

        public static byte[] GetProcess(Guid matchId,int matchType,out int code)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("MatchId", matchId);
            parameter.Add("MatchType", matchType.ToString());

            return RequestHelper.GetProcess(_moduleName, "getprocess", parameter,out code);
        }

        public static MatchCreateResponse MatchTest()
        {
            return RequestHelper.Request<MatchCreateResponse>(_moduleName, "matchtest");
        }
    }
}
