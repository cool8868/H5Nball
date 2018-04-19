using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.UAFacade;

namespace Games.NBall.NB_Web.Command
{
    public class RankCommand : BaseCommand
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
            //UAHelper.WriteLog("排行请求", "排行请求,Mid:" + UserAccount.ManagerId + " action:" + action);
            switch (action)
            {
                case "r":
                    var rankType = GetParamInt("rt");
                    if(!CheckParam(rankType))
                        return;
                    if (rankType == (int) EnumRankType.LadderRank
                        || rankType == (int)EnumRankType.CrowdRank)
                    {
                        var rl = reader2.GetLadderRank(UserAccount.ManagerId,rankType, PageIndex, PageSize);
                        OutputHelper.Output(rl);
                    }
                    else if (rankType == (int)EnumRankType.CrossCrowdRank
                        || rankType == (int)EnumRankType.ArenaRank
                        || rankType == (int)EnumRankType.CrossLadderRank
                        || rankType == (int)EnumRankType.CrossLadderDailyRank)
                    {
                        var r3 = reader3.CrowdGetRank(ShareUtil.ZoneName, UserAccount.ManagerId, rankType, PageIndex, PageSize);
                        OutputHelper.Output(r3);
                    }
                    else
                    {
                        var r = reader.GetRanking(UserAccount.ManagerId, rankType, PageIndex, PageSize);
                        OutputHelper.Output(r);
                    }
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }
        
        private readonly RankClient reader = new RankClient();
        private readonly LadderClient reader2 = new LadderClient();
        private readonly CrossDataClient reader3 = new CrossDataClient();
    }
}