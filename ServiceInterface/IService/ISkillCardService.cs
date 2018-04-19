using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.SkillCard;


namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface ISkillCardService
    {
        [OperationContract]
        RootResponse<DTOSkillSetView> UseSkillExp(Guid mid, string cid);

        [OperationContract]
        RootResponse<DTOSkillSetView> GetSkillSetInfo(Guid mid);

        [OperationContract]
        RootResponse<DTOSkillSetView> SkillSet(Guid mid, string cids, bool hasTask);

        [OperationContract]
        ManagerskillNewResponse SaveNewSkills(Guid managerId, string skills);

        [OperationContract]
        ManagerskillNewResponse GetNewSkills(Guid managerId);
    }
}
