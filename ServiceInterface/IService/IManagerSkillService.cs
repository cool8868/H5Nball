using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.ManagerSkill;
using Games.NBall.Entity.Response;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IManagerSkillService
    {
        [OperationContract]
        RootResponse<DTOTalentView> GetTalentList(Guid mid);

        [OperationContract]
        RootResponse<DTOTalentView> HitTalent(Guid mid, string tid, bool hasTask);

        [OperationContract]
        RootResponse<DTOTalentView> SetTalent(Guid mid, string tids);

        [OperationContract]
        RootResponse<DTOTalentView> ResetTalent(Guid mid);

        [OperationContract]
        RootResponse<DTOWillView> GetWillList(Guid mid);

        [OperationContract]
        RootResponse<DTOWillItemView> PutWill(Guid mid, string wid, Guid cid, bool hasTask);

        [OperationContract]
        MessageCodeResponse SetWill(Guid mid, string wid, bool enableFlag);

        /// <summary>
        /// 设置天赋类型
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerTreeResponse SetManagerTreeSkillType(Guid managerId, int type);

        /// <summary>
        /// 获取技能树
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerTreeResponse GetManagerTree(Guid managerId);

        /// <summary>
        /// 天赋加点
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="skillCode"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerTreeResponse AssignManagerSkill(Guid managerId, string skillCode);
        
        /// <summary>
        /// 重置天赋
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerTreeResponse ResetSkillPoint(Guid managerId);

        /// <summary>
        /// 设置主动天赋
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="tids"></param>
        /// <returns></returns>
        [OperationContract]
        ManagerTreeResponse SetSkillTree(Guid mid, string tids);

    }
}
