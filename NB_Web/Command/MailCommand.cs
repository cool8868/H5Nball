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
using Games.NBall.Entity;
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
    public class MailCommand : BaseCommand
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
                case "gm":
                    var response = reader.GetMailData(UserAccount.ManagerId);
                    OutputHelper.Output(response);
                    break;
                case "dm":
                    var recordId2 = GetParam("ri");
                    var response2 = reader.DeleteMail(UserAccount.ManagerId, recordId2);
                    OutputHelper.Output(response2);
                    break;
                case "ar":
                    var response3 = reader.AttachmentReceive(UserAccount.ManagerId, RecordId);
                    OutputHelper.Output(response3);
                    break;
                case "rd":
                    var response4 = reader.Read(UserAccount.ManagerId, RecordId);
                    OutputHelper.Output(response4);
                    break;

                case "info":
                    var response5 = reader.GetInformation(UserAccount.ManagerId);
                    OutputHelper.Output(response5);
                    break;

                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly MailClient reader = new MailClient();
    }
}