using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Command
{
    /// <summary>
    /// 球员相关
    /// </summary>
    public class WpfTeammemberCommand
    {
        public static string _moduleName = "Teammember";

        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <returns></returns>
        public static NBSolutionInfoResponse GetSolution()
        {
            return RequestHelper.Request<NBSolutionInfoResponse>(_moduleName,"getsolution");
        }

        /// <summary>
        /// 球员卡转换成球员
        /// </summary>
        /// <param name="cardId">卡牌guid</param>
        /// <returns></returns>
        public static MessageCodeResponse TransCardToTeammember(Guid cardId)
        {
            WpfRequestParameter parameter=new WpfRequestParameter();
            parameter.Add("itemid",cardId);
            parameter.AddHasTask(false);
            return RequestHelper.Request<MessageCodeResponse>(_moduleName,"transcard", parameter);
        }

        /// <summary>
        /// 获取球员信息
        /// </summary>
        /// <param name="teammemberId">球员guid</param>
        /// <returns></returns>
        public static TeammemberResponse GetTeammember(Guid teammemberId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("teammemberid", teammemberId);
            return RequestHelper.Request<TeammemberResponse>(_moduleName, "getteammember", parameter);
        }

        /// <summary>
        /// 获取阵容和球员信息
        /// </summary>
        /// <returns></returns>
        public static NBSolutionInfoResponse GetSolutionTeammember()
        {
            return RequestHelper.Request<NBSolutionInfoResponse>(_moduleName, "getsolutionteammember");
        }

        /// <summary>
        /// 保存阵容
        /// </summary>
        /// <param name="formationId">阵型id</param>
        /// <param name="playerstring">球员串,11个球员的pid，从守门员开始，以逗号分隔</param>
        /// <param name="skillstring">技能串，以逗号分隔</param>
        /// <returns></returns>
        public static MessageCodeResponse SaveSolution(int formationId, string playerstring)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("formationId",formationId);
            parameter.Add("playerstring",playerstring);
            parameter.AddHasTask(false);
            return RequestHelper.Request<MessageCodeResponse>(_moduleName, "savesolution", parameter);
        }

        /// <summary>
        /// 解雇球员
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public static MessageCodeResponse FireTeammember(Guid teammemberId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("teammemberId",teammemberId);
            return RequestHelper.Request<MessageCodeResponse>(_moduleName, "fireteammember", parameter);
        }

        /// <summary>
        /// 设置装备
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static TeammemberResponse SetEquip(Guid teammemberId, Guid itemId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("teammemberId", teammemberId);
            parameter.Add("itemId",itemId);
            parameter.AddHasTask(false);
            return RequestHelper.Request<TeammemberResponse>(_moduleName, "setequip", parameter);
        }

        /// <summary>
        /// 移除装备
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <param name="isEquipment"></param>
        /// <returns></returns>
        public static TeammemberResponse RemoveEquip(Guid teammemberId, bool isEquipment)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("teammemberId", teammemberId);
            parameter.Add("isEquipment", isEquipment?1:0);
            return RequestHelper.Request<TeammemberResponse>(_moduleName, "removeequip", parameter);
        }

        ///// <summary>
        ///// 获取阵型列表
        ///// </summary>
        ///// <returns></returns>
        //public static FormationListResponse GetFormationList()
        //{
        //    return RequestHelper.Request<FormationListResponse>(_moduleName, "getformationlist");
        //}

        ///// <summary>
        ///// 阵型升级
        ///// </summary>
        ///// <param name="formationId"></param>
        ///// <returns></returns>
        //public static FormationLevelupResponse FormationLevelup(int formationId)
        //{
        //    WpfRequestParameter parameter = new WpfRequestParameter();
        //    parameter.Add("formationId", formationId);
        //    parameter.AddHasTask(ApiTestCore.HasTask(EnumTaskRequireFunc.SolutionStrength));
        //    return RequestHelper.Request<FormationLevelupResponse>(_moduleName, "formationlevelup", parameter);
        //}

        /// <summary>
        /// 获取球员成长数据
        /// </summary>
        /// <param name="teammemberId">球员ID</param>
        /// <returns></returns>
        public static TeammemberGrowResponse GetTeammemberGrowInfo(Guid teammemberId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("teammemberId", teammemberId);
            return RequestHelper.Request<TeammemberGrowResponse>(_moduleName, "getteammembergrowinfo", parameter);
        }


        /// <summary>
        /// 球员成长
        /// </summary>
        /// <param name="teammemberId">球员ID</param>
        /// <param name="growType">成长类型：0普通成长，1快速成长，2免费快速成长</param>
        /// <returns></returns>
        public static TeammemberGrowResponse TeammemberGrow(Guid teammemberId, int growType)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("teammemberId", teammemberId);
            parameter.Add("growType", growType);
            parameter.AddHasTask(false);
            return RequestHelper.Request<TeammemberGrowResponse>(_moduleName, "teammembergrow", parameter);
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public static TeammemberListForGrowResponse GetTeammemberListForGrow()
        //{
        //    return RequestHelper.Request<TeammemberListForGrowResponse>(_moduleName, "getteammemberlistforgrow");
        //}

        ///// <summary>
        ///// 球员训练里的球员列表
        ///// </summary>
        ///// <returns></returns>
        //public static TeammemberTrainListResponse GetTeammemberListForTrain()
        //{
        //    return RequestHelper.Request<TeammemberTrainListResponse>(_moduleName, "getteammemberlistfortrain");
        //}

        ///// <summary>
        ///// 球员开始训练
        ///// </summary>
        ///// <param name="teammemberId"></param>
        ///// <returns></returns>
        //public static TeammemberTrainActionResponse StartTrain(Guid teammemberId)
        //{
        //    WpfRequestParameter parameter = new WpfRequestParameter();
        //    parameter.Add("teammemberId", teammemberId);
        //    parameter.AddHasTask(ApiTestCore.HasTask(EnumTaskRequireFunc.TeammemberTrain));
        //    return RequestHelper.Request<TeammemberTrainActionResponse>(_moduleName, "starttrain", parameter);
        //}

        ///// <summary>
        ///// 球员结束训练
        ///// </summary>
        ///// <param name="teammemberId"></param>
        ///// <returns></returns>
        //public static TeammemberTrainActionResponse StopTrain(Guid teammemberId)
        //{
        //    WpfRequestParameter parameter = new WpfRequestParameter();
        //    parameter.Add("teammemberId", teammemberId);
        //    return RequestHelper.Request<TeammemberTrainActionResponse>(_moduleName, "stoptrain", parameter);
        //}

        ///// <summary>
        ///// 加速训练check
        ///// </summary>
        ///// <param name="teammemberId"></param>
        ///// <returns></returns>
        //public static TeammemberTrainActionResponse CheckQuickenTrain(Guid teammemberId)
        //{
        //    WpfRequestParameter parameter = new WpfRequestParameter();
        //    parameter.Add("teammemberId", teammemberId);
        //    parameter.Add("gtri", ApiTestCore.GuideTaskRecordId(1010));
        //    return RequestHelper.Request<TeammemberTrainActionResponse>(_moduleName, "checkquickentrain", parameter);
        //}

        ///// <summary>
        ///// 加速训练
        ///// </summary>
        ///// <param name="teammemberId"></param>
        ///// <returns></returns>
        //public static TeammemberTrainActionResponse QuickenTrain(Guid teammemberId)
        //{
        //    WpfRequestParameter parameter = new WpfRequestParameter();
        //    parameter.Add("teammemberId", teammemberId);
        //    parameter.AddHasTask(ApiTestCore.HasTask(EnumTaskRequireFunc.QuickTrain));
        //    parameter.Add("gtri", ApiTestCore.GuideTaskRecordId(1010));
        //    return RequestHelper.Request<TeammemberTrainActionResponse>(_moduleName, "quickentrain", parameter);
        //}

        /// <summary>
        /// 分配球员属性
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public static TeammemberResponse AddProperty(Guid teammemberId, int propertyId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("teammemberId", teammemberId);
            parameter.Add("propertyId", propertyId);
            parameter.AddHasTask(false);
            return RequestHelper.Request<TeammemberResponse>(_moduleName, "addproperty", parameter);
        }
    }
}
