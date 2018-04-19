using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.UAFacade;
using Games.NBall.WebClient.Weibo;
using Games.NBall.WebServerFacade;

namespace Games.NBall.NB_Web.Command
{
    public class ManagerSkillCommand : BaseCommand
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
            Guid mid = UserAccount.ManagerId;
            switch (action)
            {
                case "tlist":
                    OutputHelper.Output(client.GetTalentList(mid));
                    return;
                case "thit":
                    string tid = GetParam("tid");
                    if (!CheckSkillCode(tid))
                        return;
                    OutputHelper.Output(client.HitTalent(mid, tid, HasTask));
                    return;
                case "tset":
                    string tids = GetParam("tids");
                    if (!CheckSkillCodes(tids))
                        return;
                    OutputHelper.Output(client.SetTalent(mid, tids));
                    return;
                case "treset":
                    OutputHelper.Output(client.ResetTalent(mid));
                    return;
                case "wlist":
                    OutputHelper.Output(client.GetWillList(mid));
                    return;
                case "wput":
                    string wid = GetParam("wid");
                    Guid cid = GetParamGuid("cid");
                    if (!CheckSkillCode(wid) || cid == Guid.Empty)
                        return;
                    OutputHelper.Output(client.PutWill(mid, wid, cid, HasTask));
                    return;
                case "wset":
                    wid = GetParam("wid");
                    int flag = GetParamInt("flag");
                    if (!CheckSkillCodes(wid))
                        return;
                    OutputHelper.Output(client.SetWill(mid, wid, flag > 0));
                    return;
                case "stt":
                    int type = GetParamInt("type");
                    OutputHelper.Output(client.SetManagerTreeSkillType(mid, type));
                    return;

                case "gt":
                    OutputHelper.Output(client.GetManagerTree(mid));
                    return;
                case "ams":
                    var skillCode = GetParam("sc");
                    OutputHelper.Output(client.AssignManagerSkill(mid, skillCode));
                    return;
                case "rs":
                    OutputHelper.Output(client.ResetSkillPoint(mid));
                    return;
                case "stodo":
                    var tds = GetParam("tids");
                    OutputHelper.Output(client.SetSkillTree(mid, tds));
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }
        }

        readonly ManagerSkillClient client = new ManagerSkillClient();

        bool CheckSkillCode(string skillCode)
        {
            if (string.IsNullOrEmpty(skillCode) || skillCode.Length > 10)
                return false;
            return true;
        }
        bool CheckSkillCodes(string skillCode)
        {
            if (string.IsNullOrEmpty(skillCode) || skillCode.Length > 30)
                return false;
            return true;
        }
    }
}