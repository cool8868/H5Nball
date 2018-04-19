using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Item;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface ITeammemberService
    {
        /// <summary>
        /// 获取整容和球员信息
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns></returns>
        [OperationContract]
        NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId);

        /// <summary>
        /// 获取球员信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        [OperationContract]
        TeammemberResponse GetTeammemberResponse(Guid managerId, Guid teammemberId);

        /// <summary>
        /// 替换球员
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <param name="byTeammemberId"></param>
        /// <returns></returns>
        [OperationContract]
        NBSolutionInfoResponse ReplacePlayer(Guid managerId, Guid teammemberId, Guid byTeammemberId);

        /// <summary>
        /// 解雇球员
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse FireTeamMember(Guid managerId, Guid teammemberId);

        /// <summary>
        /// 设置装备
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [OperationContract]
        TeammemberResponse SetEquip(Guid managerId, Guid teammemberId, Guid itemId);

        /// <summary>
        /// 移除装备
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        [OperationContract]
        TeammemberResponse RemoveEquipment(Guid managerId, Guid teammemberId);

        /// <summary>
        /// 获取阵型
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        FormationListResponse GetFormationList(Guid managerId);

        /// <summary>
        /// 设置阵型
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="formationId"></param>
        /// <returns></returns>
        [OperationContract]
        SetFormationResponse SetFormation(Guid managerId, int formationId);
    }
}
