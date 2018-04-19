using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web.Command
{
    public class FriendCommand : BaseCommand
    {
        public void Dispatch(string action)
        {
            if (action == "sc")
            {
                Statistic();
                return;
            }
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
                case "af":
                    string name = GetParam("n");
                    if(!CheckParam(name))
                        return;
                    var af = reader.AddFriend(UserAccount.ManagerId, name, HasTask);
                    OutputHelper.Output(af);
                    break;
                case "gf":
                    var gf = reader.GetMyFriends(UserAccount.ManagerId, PageIndex,PageSize);
                    OutputHelper.Output(gf);
                    break;
                case "gif":
                    var gif = reader.GetFriendRequestList(UserAccount.ManagerId);
                    OutputHelper.Output(gif);
                    break;
                case "iaf":
                    var friendId = GetParamGuid("f");
                    if (!CheckParam(friendId))
                        return;
                    var iaf = reader.IgnoreAddFriend(UserAccount.ManagerId, friendId);
                    OutputHelper.Output(iaf);
                    break;
                case "gb":
                    var gb = reader.GetMyBlacks(UserAccount.ManagerId, PageIndex, PageSize);
                    OutputHelper.Output(gb);
                    break;
                case "fi":
                    var awayId = GetParamGuid("a");
                    if (!CheckParam(awayId))
                        return;
                    var fi = reader.Fight(UserAccount.ManagerId, awayId);
                    OutputHelper.Output(fi);
                    break;
                case "fir":
                    var matchId = GetParamGuid("m");
                    if(!CheckParam(matchId))
                        return;
                    var fir = reader.GetMatchResponse(matchId);
                    OutputHelper.Output(fir);
                    break;
                case "df":
                    var recordId = GetParamInt("f");
                    if(!CheckParam(recordId))
                        return;
                    var df = reader.DeleteFriend(UserAccount.ManagerId, recordId, PageIndex, PageSize);
                    OutputHelper.Output(df);
                    break;
                case "gfi":
                    var gfi = reader.GetFriendInvitePrizeList(UserAccount.Account, UserAccount.ManagerId);
                    OutputHelper.Output(gfi);
                    break;
                case "sfi":
                    var inviteCount = GetParamInt("f");
                    var sfi = reader.InvitePrize(UserAccount.Account, UserAccount.ManagerId,inviteCount);
                    OutputHelper.Output(sfi);
                    break;
                case "cfi":
                    var cfi = reader.GetInviteFriendGrowUpPrize(UserAccount.Account, UserAccount.ManagerId);
                    OutputHelper.Output(cfi);
                    break;
                //case "scfi":
                //    var scfi = reader.GrowUpPrize(UserAccount.Account, UserAccount.ManagerId);
                //    OutputHelper.Output(scfi);
                //    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly FriendClient reader = new FriendClient();

        #region Statistic
        void Statistic()
        {
            try
            {
                var type = GetParamInt("type");
                Bll.StatisticClickMgr.Update(type, DateTime.Today);
                OutputHelper.Output(0);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                OutputHelper.OutputException();
            }
        }

        #endregion
    }
}