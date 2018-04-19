using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
//using Games.NBall.Entity.Response.Frame;
using Games.NBall.Entity.Response.SkillCard;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.SkillCard;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class SkillCardService : ISkillCardService
    {
        public RootResponse<DTOSkillSetView> UseSkillExp(Guid mid, string cid)
        {
            return ResponseHelper.TryCatch(() => SkillCardRules.UseSkillExp(mid, cid));
        }

        public RootResponse<DTOSkillSetView> GetSkillSetInfo(Guid mid)
        {
            return ResponseHelper.TryCatch(() => SkillCardRules.GetSkillSetInfo(mid));
        }

        public RootResponse<DTOSkillSetView> SkillSet(Guid mid, string cids, bool hasTask)
        {
            return ResponseHelper.TryCatch(() => SkillCardRules.SkillSet(mid, cids,hasTask));
        }

        public ManagerskillNewResponse SaveNewSkills(Guid managerId, string skills)
        {
            return ResponseHelper.TryCatch(() => SkillCardRules.SaveNewSkills(managerId, skills));
        }

        public ManagerskillNewResponse GetNewSkills(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => SkillCardRules.GetNewSkills(managerId));
        }


    }
}
