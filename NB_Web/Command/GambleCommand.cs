using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response.Gamble;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class GambleCommand : BaseCommand
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
                case "gml":
                    //获取比赛列表
                    var m = GetParam("m");
                    var d = GetParam("d");
                    DateTime date = DateTime.Now;
                    try
                    {
                        date = DateTime.Parse(DateTime.Now.Year + "-" + m + "-" + d);
                    }
                    catch
                    {
                        OutputHelper.Output((int) MessageCode.NbParameterError);
                        break;
                    }
                    var isDefault = GetParamInt("id");
                    var response = reader.GetMatchList(UserAccount.ManagerId, date, isDefault);
                    OutputHelper.Output(response);
                    break;
                case "gm":
                    //竞猜一场比赛
                    var matchId = GetParamInt("mid");
                    var gambleType = GetParamInt("gt");
                    var pointType = GetParamInt("pt");

                    if (gambleType == 0 || pointType == 0)
                    {
                        OutputHelper.Output((int)MessageCode.NbParameterError);
                        break;
                    }
                    var gm = reader.GambleMatch(UserAccount.ManagerId, matchId, gambleType, pointType);
                     OutputHelper.Output(gm);
                    break;
                case "gmg":
                    //获取我的竞猜列表
                    var gmg = reader.GetMyGamble(UserAccount.ManagerId);
                    OutputHelper.Output(gmg);
                    break;
                case "gma":
                    //获取我的竞猜活动信息
                    var gma = reader.GetMyActivity(UserAccount.ManagerId);
                    OutputHelper.Output(gma);
                    break;
                case "dp":
                    //领取活动奖励
                    var step = GetParamInt("sp");
                    var dp = reader.DrawPrize(UserAccount.ManagerId, step);
                    OutputHelper.Output(dp);
                    break;
                case "md":
                    var md = reader.GetMatchTheCalendar();
                    OutputHelper.Output(md);
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly GambleClient reader = new GambleClient();
    }
}