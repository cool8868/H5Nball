using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class TeammemberService : ITeammemberService
    {
        /// <summary>
        /// 获取整容和球员信息
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns></returns>
        public NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => TeammemberCore.Instance.SolutionAndTeammemberResponse(managerId));
        }

        /// <summary>
        /// 获取球员信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public TeammemberResponse GetTeammemberResponse(Guid managerId, Guid teammemberId)
        {
            return ResponseHelper.TryCatch(() => TeammemberCore.Instance.GetTeammemberResponse(managerId, teammemberId));
        }

        /// <summary>
        /// 替换球员
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <param name="byTeammemberId"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse ReplacePlayer(Guid managerId, Guid teammemberId, Guid byTeammemberId)
        {
            return ResponseHelper.TryCatch(() => TeammemberCore.Instance.ReplacePlayer(managerId, teammemberId, byTeammemberId));
        }

        /// <summary>
        /// 解雇球员
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public MessageCodeResponse FireTeamMember(Guid managerId, Guid teammemberId)
        {
            return ResponseHelper.TryCatch(() => TeammemberCore.Instance.FireTeamMember(managerId, teammemberId));
        }

        /// <summary>
        /// 设置装备
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public TeammemberResponse SetEquip(Guid managerId, Guid teammemberId, Guid itemId)
        {
            return ResponseHelper.TryCatch(() => TeammemberCore.Instance.SetEquip(managerId, teammemberId, itemId));
        }

        /// <summary>
        /// 移除装备
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public TeammemberResponse RemoveEquipment(Guid managerId, Guid teammemberId)
        {
            return ResponseHelper.TryCatch(() => TeammemberCore.Instance.RemoveEquipment(managerId, teammemberId));
        }

        /// <summary>
        /// 获取阵型列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public FormationListResponse GetFormationList(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => TeammemberCore.Instance.GetFormationList(managerId));
        }

        /// <summary>
        /// 设置阵型id
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="formationId"></param>
        /// <returns></returns>
        public SetFormationResponse SetFormation(Guid managerId, int formationId)
        {
            return
                ResponseHelper.TryCatch(
                    () => TeammemberCore.Instance.SetFormation(managerId, formationId));
        }

    }
}
