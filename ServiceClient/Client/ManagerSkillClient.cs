using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.ManagerSkill;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class ManagerSkillClient : IManagerSkillService
    {
        private static IManagerSkillService proxy =
            ServiceProxy<IManagerSkillService>.Create("NetTcp_IManagerSkillService");

        public RootResponse<DTOTalentView> GetTalentList(Guid mid)
        {
            return proxy.GetTalentList(mid);
        }

        public RootResponse<DTOTalentView> HitTalent(Guid mid, string tid, bool hasTask)
        {
            return proxy.HitTalent(mid, tid, hasTask);
        }

        public RootResponse<DTOTalentView> SetTalent(Guid mid, string tids)
        {
            return proxy.SetTalent(mid, tids);
        }

        public RootResponse<DTOTalentView> ResetTalent(Guid mid)
        {
            return proxy.ResetTalent(mid);
        }

        public RootResponse<DTOWillView> GetWillList(Guid mid)
        {
            return proxy.GetWillList(mid);
        }

        public RootResponse<DTOWillItemView> PutWill(Guid mid, string wid, Guid cid, bool hasTask)
        {
            return proxy.PutWill(mid, wid, cid, hasTask);
        }

        public MessageCodeResponse SetWill(Guid mid, string wid, bool enableFlag)
        {
            return proxy.SetWill(mid, wid, enableFlag);
        }

        /// <summary>
        /// 设置天赋类型
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ManagerTreeResponse SetManagerTreeSkillType(Guid managerId, int type)
        {
            return proxy.SetManagerTreeSkillType(managerId, type);
        }

        /// <summary>
        /// 获取技能树
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerTreeResponse GetManagerTree(Guid managerId)
        {
            return proxy.GetManagerTree(managerId);
        }

        /// <summary>
        /// 天赋加点
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="skillCode"></param>
        /// <returns></returns>
        public ManagerTreeResponse AssignManagerSkill(Guid managerId, string skillCode)
        {
            return proxy.AssignManagerSkill(managerId, skillCode);
        }

        /// <summary>
        /// 重置天赋
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerTreeResponse ResetSkillPoint(Guid managerId)
        {
            return proxy.ResetSkillPoint(managerId);
        }

        /// <summary>
        /// 设置主动天赋 
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="tids"></param>
        /// <returns></returns>
        public ManagerTreeResponse SetSkillTree(Guid mid, string tids)
        {
            return proxy.SetSkillTree(mid, tids);
        }
    }
}
