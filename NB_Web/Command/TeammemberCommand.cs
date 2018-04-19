using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.UAFacade;
using Games.NBall.WebClient.Weibo;
using Games.NBall.WebServerFacade;

namespace Games.NBall.NB_Web.Command
{
    public class TeammemberCommand : BaseCommand
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
                case "getsolution":
                    GetSolution();
                    break;
                case "getteammember":
                    GetTeammember();
                    break;
                case "fireteammember":
                    FireTeammember();
                    break;
                case "setequip":
                    SetEquip();
                    break;
                case "removeequip":
                    RemoveEquip();
                    break;
                case "getformationlist":
                    GetFormationList();
                    break;
                case "sf":
                    SetFormation();
                    break;
                case "gti":
                    GetTrainInfo();
                    break;
                case "transcard":
                    ReplacePlayer();
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly TeammemberClient reader = new TeammemberClient();
        //private readonly TrainThreadClient trainReader = new TrainThreadClient();

        void GetTrainInfo()
        {
            Guid teammemberId = GetParamGuid("ti");
            if (!CheckParam(teammemberId)) return;
           // var response = trainReader.GetTrainInfo(UserAccount.ManagerId, teammemberId);
           // OutputHelper.Output(response);
        }

        void SetFormation()
        {
            var formationId = GetParamInt("f");
            if (!CheckParam(formationId)) return;
            var response = reader.SetFormation(UserAccount.ManagerId, formationId);
            OutputHelper.Output(response);
        }

        void ReplacePlayer()
        {
            var teammemberId = GetParamGuid("tid");
            var byTeammemberId = GetParamGuid("bytid");
            var response = reader.ReplacePlayer(UserAccount.ManagerId, teammemberId, byTeammemberId);
            OutputHelper.Output(response);
        }

        void GetSolution()
        {
            var response = reader.SolutionAndTeammemberResponse(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void GetTeammember()
        {
            Guid teammemberId = GetParamGuid("teammemberId");
            if (!CheckParam(teammemberId)) return;
            var response = reader.GetTeammemberResponse(UserAccount.ManagerId, teammemberId);
            OutputHelper.Output(response);
        }

        void FireTeammember()
        {
            var teammemberId = GetParamGuid("teammemberId");
            var response = reader.FireTeamMember(UserAccount.ManagerId, teammemberId);
            OutputHelper.Output(response);
        }

        void SetEquip()
        {
            var teammemberId = GetParamGuid("teammemberId");
            var itemId = GetParamGuid("itemId");
            var response = reader.SetEquip(UserAccount.ManagerId, teammemberId, itemId);
            OutputHelper.Output(response);
        }

        void RemoveEquip()
        {
            var teammemberId = GetParamGuid("teammemberId");
            var removeType = GetParamInt("removetype");
            switch (removeType)
            {
                case 1://装备
                    var equipment = reader.RemoveEquipment(UserAccount.ManagerId, teammemberId);
                    OutputHelper.Output(equipment);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        /// <summary>
        /// 获取阵型列表
        /// </summary>
        /// <returns></returns>
        void GetFormationList()
        {
            var response = reader.GetFormationList(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }


    }
}