using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Command
{
    public class WpfMailCommand
    {
        public static string _moduleName = "MI";

        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static MailDataResponse GetMailData( int pageIndex)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("pi", pageIndex);
            return RequestHelper.Request<MailDataResponse>(_moduleName, "gm", parameter);
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="recordIds"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static MailDataResponse DeleteMail(string recordIds, int pageIndex)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("pi",pageIndex);
            parameter.Add("ri", recordIds);
            return RequestHelper.Request<MailDataResponse>(_moduleName, "dm", parameter);
        }

        /// <summary>
        /// 收取附件
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static MailAttachmentReceiveResponse AttachmentReceive(int recordId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("ri", recordId);
            return RequestHelper.Request<MailAttachmentReceiveResponse>(_moduleName, "ar", parameter);
        }

        /// <summary>
        /// 阅读
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static MessageCodeResponse Read(int recordId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("ri", recordId);
            return RequestHelper.Request<MessageCodeResponse>(_moduleName, "rd", parameter);
        }
    }
}
