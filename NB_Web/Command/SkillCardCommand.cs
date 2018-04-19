using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class SkillCardCommand : BaseCommand
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
                case "upskill":
                    var cid = GetParam("cid");
                    if (!CheckCid(cid))
                        return;
                    OutputHelper.Output(client.UseSkillExp(UserAccount.ManagerId, cid));
                    return;
                case "setinfo":
                    OutputHelper.Output(client.GetSkillSetInfo(UserAccount.ManagerId));
                    return;
                case "set":
                    var cids = GetParam("cids");
                    if (!CheckCids(cids))
                        return;
                    OutputHelper.Output(client.SkillSet(UserAccount.ManagerId, cids, HasTask));
                    return;
                case "savenew":
                    var skills = GetParam("skills");
                    OutputHelper.Output(client.SaveNewSkills(UserAccount.ManagerId, skills));
                    return;
                case "getnew":
                    OutputHelper.Output(client.GetNewSkills(UserAccount.ManagerId));
                    return;

                default:
                    OutputHelper.OutputBadRequest();
                    return;
            }
        }
        readonly SkillCardClient client = new SkillCardClient();

        bool CheckCid(string cid)
        {
            if (!string.IsNullOrEmpty(cid) && cid.Length != 22)
                return false;
            return true;
        }
        bool CheckNpcid(int npcid)
        {
            if (npcid < 0 || npcid > 5)
                return false;
            return true;
        }
        bool CheckCids(string cids)
        {
            if (string.IsNullOrEmpty(cids) && cids.Length >400)
                return false;
            return true;
        }
    }
}