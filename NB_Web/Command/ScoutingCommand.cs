using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.ServiceContract.Client.Client;

namespace Games.NBall.NB_Web.Command
{
    public class ScoutingCommand : BaseCommand
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
                case "scoutinginfo":
                    OutputHelper.Output(reader.GetScoutingInfo(UserAccount.ManagerId));
                    break;
                case "lottery":
                    var scoutingId = GetParamInt("scoutingId");
                    var count = GetParamInt("c");
                    if (!CheckParam(scoutingId))
                    {
                        OutputHelper.OutputParameterError();
                    }
                    else
                    {
                        bool isAutoDec = GetParamBool("i");
                        OutputHelper.Output(reader.ScoutingLottery(UserAccount.ManagerId, scoutingId,HasTask,count,isAutoDec));
                    }
                    break;
                case "gt":
                    OutputHelper.Output(reader.GetManagerInfo(UserAccount.ManagerId));
                    break;
                case "ld":
                    OutputHelper.Output(reader.TurntableLuckDraw(UserAccount.ManagerId));
                    break;
                case "re":
                    OutputHelper.Output(reader.ResetTurntable(UserAccount.ManagerId));
                    break;


                case "getminfo":
                    var getmanager = reader.RevelationGetManagerId(UserAccount.ManagerId);
                    OutputHelper.Output(getmanager);
                    break;
                case "sm":
                    var mark = GetParamInt("mark");
                    if (!CheckParam(mark))
                        return;
                    var sm = reader.StartMark(UserAccount.ManagerId, mark);
                    OutputHelper.Output(sm);
                    break;
                case "gmi":
                    var gmi = reader.GetMarkInfo(UserAccount.ManagerId);
                    OutputHelper.Output(gmi);
                    break;
                case "rthegame":
                    var rthegame = reader.RevelationTheGame(UserAccount.ManagerId);
                    OutputHelper.Output(rthegame);
                    break;
                case "gdi":
                    var drawId = GetParamGuid("di");
                    var gdi = reader.GetDrawInfo(UserAccount.ManagerId, drawId);
                    OutputHelper.Output(gdi);
                    break;
                case "dr":
                    var drawId1 = GetParamGuid("di");
                    if (!CheckParam(drawId1))
                        return;
                    var dr = reader.DrawResult(UserAccount.ManagerId, drawId1);
                    OutputHelper.Output(dr);
                    break;
                case "addm":
                    var addm = reader.RevelationAddMorale(UserAccount.ManagerId);
                    OutputHelper.Output(addm);
                    break;
                case "rrm":
                    var rrm = reader.RevelationResetMark(UserAccount.ManagerId);
                    OutputHelper.Output(rrm);
                    break;
                case "rgsi":
                    var rgsi = reader.RevelationGetShopInfo(UserAccount.ManagerId);
                    OutputHelper.Output(rgsi);
                    break;
                case "rexi":
                    var idx = GetParamInt("idx");
                    var rexi = reader.RevelationPurchaseItems(UserAccount.ManagerId, idx);
                    OutputHelper.Output(rexi);
                    break;
                case "revref":
                    var revref = reader.RevelationShopRefresh(UserAccount.ManagerId);
                    OutputHelper.Output(revref);
                    break;
                case "gcol":
                    GetCoachList();
                    break;
                case"acoach":
                    ActivationCoach();
                    break;
                case "rcoach":
                    ReplaceCoach();
                    break;
                case "ucoach":
                    CoachUpgrade();
                    break;
                case "sucoach":
                    CoachSkillUpgrade();
                    break;
                case "stcoach":
                    CoachStarUpgrade();
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }
        private readonly ScoutingClient reader = new ScoutingClient();

        /// <summary>
        /// 获取教练列表
        /// </summary>
        void GetCoachList()
        {
            var response = reader.GetCoachList(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 激活教练
        /// </summary>
        void ActivationCoach()
        {
            var coachId = GetParamInt("cid");
            var response = reader.ActivationCoach(UserAccount.ManagerId, coachId);
            OutputHelper.Output(response);

        }

        /// <summary>
        /// 替换启用的教练
        /// </summary>
        void ReplaceCoach()
        {
            var coachId = GetParamInt("cid");
            var response = reader.ReplaceCoach(UserAccount.ManagerId, coachId);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 教练经验升级
        /// </summary>
        void CoachUpgrade()
        {
            var coachId = GetParamInt("cid");
            var response = reader.CoachUpgrade(UserAccount.ManagerId, coachId);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 教练技能升级
        /// </summary>
        void CoachSkillUpgrade()
        {
            var coachId = GetParamInt("cid");
            var response = reader.CoachSkillUpgrade(UserAccount.ManagerId, coachId);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 教练升星
        /// </summary>
        void CoachStarUpgrade()
        {
            var coachId = GetParamInt("cid");
            var response = reader.CoachStarUpgrade(UserAccount.ManagerId, coachId);
            OutputHelper.Output(response);
        }
    }
}