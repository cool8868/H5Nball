using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
//using Games.NBall.Entity.Response.Frame;
using Games.NBall.Entity.Response.SkillCard;
using Games.NBall.ServiceEngine;
using Games.NBall.ServiceContract.IService;


namespace Games.NBall.ServiceContract.Client
{
    public class SkillCardClient : ISkillCardService
    {
        static ISkillCardService proxy = ServiceProxy<ISkillCardService>.Create("NetTcp_ISkillCardService");

        public RootResponse<DTOSkillSetView> UseSkillExp(Guid mid, string cid)
        {
            return proxy.UseSkillExp(mid, cid);
        }

        public RootResponse<DTOSkillSetView> GetSkillSetInfo(Guid mid)
        {
            return proxy.GetSkillSetInfo(mid);
        }

        public RootResponse<DTOSkillSetView> SkillSet(Guid mid, string cids, bool hasTask)
        {
            return proxy.SkillSet(mid, cids,hasTask);
        }

        public ManagerskillNewResponse SaveNewSkills(Guid managerId, string skills)
        {
            return  proxy.SaveNewSkills(managerId, skills);
        }

        public ManagerskillNewResponse GetNewSkills(Guid managerId)
        {
            return  proxy.GetNewSkills(managerId);
        }
    }
}
