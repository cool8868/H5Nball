using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Manager;
using Games.NBall.Core.ManagerSkill;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.ManagerSkill;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class ManagerSkillService : IManagerSkillService
    {
        public RootResponse<DTOTalentView> GetTalentList(Guid mid)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.GetTalentList(mid));
        }

        public RootResponse<DTOTalentView> HitTalent(Guid mid, string tid, bool hasTask)
        {
            if (!ManagerUtil.CheckFunction(mid, EnumOpenFunction.Talent))
                return ResponseHelper.InvalidFunction<RootResponse<DTOTalentView>>();
            return ResponseHelper.TryCatch(() => ManagerSkillRules.HitTalent(mid, tid, hasTask));
        }

        public RootResponse<DTOTalentView> SetTalent(Guid mid, string tids)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.SetTalent(mid, tids));
        }

        public RootResponse<DTOTalentView> ResetTalent(Guid mid)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.ResetTalent(mid));
        }

        public RootResponse<DTOWillView> GetWillList(Guid mid)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.GetWillList(mid));
        }

        public RootResponse<DTOWillItemView> PutWill(Guid mid, string wid, Guid cid, bool hasTask)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.PutWill(mid, wid, cid, hasTask));
        }

        public MessageCodeResponse SetWill(Guid mid, string wid, bool enableFlag)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.SetWill(mid, wid, enableFlag));
        }

        /// <summary>
        /// 设置天赋类型
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ManagerTreeResponse SetManagerTreeSkillType(Guid managerId, int type)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.SetManagerTreeSkillType(managerId, type));
        }

        /// <summary>
        /// 获取技能树
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerTreeResponse GetManagerTree(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.GetManagerTree(managerId));
        }

        /// <summary>
        /// 天赋加点
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="skillCode"></param>
        /// <returns></returns>
        public ManagerTreeResponse AssignManagerSkill(Guid managerId, string skillCode)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.AssignManagerSkill(managerId, skillCode));
        }

        /// <summary>
        /// 重置天赋
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerTreeResponse ResetSkillPoint(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.ResetSkillPoint(managerId));
        }

        /// <summary>
        /// 设置主动天赋
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="tids"></param>
        /// <returns></returns>
        public ManagerTreeResponse SetSkillTree(Guid mid, string tids)
        {
            return ResponseHelper.TryCatch(() => ManagerSkillRules.SetSkillTree(mid, tids));
        }

    }
}
