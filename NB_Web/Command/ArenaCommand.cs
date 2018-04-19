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
    public class ArenaCommand : BaseCommand
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
                case "gs":
                    int arenaType = GetParamInt("at");
                    string zoneName = GetParam("zn");
                    var gs = reader.SolutionAndTeammemberResponse(UserAccount.ManagerId, arenaType, zoneName);
                    OutputHelper.Output(gs);
                    break;
                case "uf":
                    int arenaType1 = GetParamInt("at");
                    int buIndex = GetParamInt("bi");
                    Guid itemId = GetParamGuid("ii");
                    var uf = reader.UpFormation(UserAccount.ManagerId, arenaType1, buIndex, itemId);
                    OutputHelper.Output(uf);
                    break;
                case "rp":
                     int arenaType2 = GetParamInt("at");
                    Guid teammemberId = GetParamGuid("t1");
                    Guid byTeammemberId = GetParamGuid("t2");
                    var rp = reader.ReplacePlayer(UserAccount.ManagerId, arenaType2, teammemberId, byTeammemberId);
                    OutputHelper.Output(rp);
                    break;
                case "ss":
                    int arenaType3 = GetParamInt("at");
                    int formationId = GetParamInt("fid");
                    var ss = reader.SetSolution(UserAccount.ManagerId, arenaType3, formationId);
                    OutputHelper.Output(ss);
                    break;
                case "gsp":
                    var gsp = reader.GetShopInfoResponse(UserAccount.ManagerId);
                    OutputHelper.Output(gsp);
                    break;
                case "esp":
                    int exIndex = GetParamInt("id");
                    var esp = reader.ExChange(UserAccount.ManagerId, exIndex);
                    OutputHelper.Output(esp);
                    break;
                case "rsp":
                    var rsp = reader.RefreshShop(UserAccount.ManagerId);
                    OutputHelper.Output(rsp);
                    break;
                case "xz":
                    var xz = reader.ArenaGoOffStage(UserAccount.ManagerId, GetParamInt("at"));
                    OutputHelper.Output(xz);
                    break;
                    //点球
                case "pr":
                    var pr = reader.GetRank(UserAccount.ManagerId);
                    OutputHelper.Output(pr);
                    break;
                case "pgi":
                    var pgi = reader.GetInfo(UserAccount.ManagerId);
                    OutputHelper.Output(pgi);
                    break;
                case "join":
                    var join = reader.Join(UserAccount.ManagerId);
                    OutputHelper.Output(join);
                    break;
                case "shoot":
                    var shoot = reader.Shoot(UserAccount.ManagerId);
                    OutputHelper.Output(shoot);
                    break;
                case "pex":
                    var pex = reader.PenaltyKickExChange(UserAccount.ManagerId, GetParamInt("ic"));
                    OutputHelper.Output(pex);
                    break;
                case "prex":
                    var prex = reader.RefreshExChange(UserAccount.ManagerId);
                    OutputHelper.Output(prex);
                    break;
                case "ap":
                    var ap = reader.GetActivityInfo(UserAccount.ManagerId);
                    OutputHelper.Output(ap);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }
        }




        private readonly ArenaClient reader = new ArenaClient();
    }
}