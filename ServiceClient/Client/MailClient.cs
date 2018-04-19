using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Information;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class MailClient
    {
        private static IMailService proxy = ServiceProxy<IMailService>.Create("NetTcp_IMailService");

        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public MailDataResponse GetMailData(Guid managerId)
        {
            return proxy.GetMailData(managerId);
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordIds"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public MailDataResponse DeleteMail(Guid managerId, string recordIds)
        {
            return proxy.DeleteMail(managerId, recordIds);
        }

        /// <summary>
        /// 收取附件
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public MailAttachmentReceiveResponse AttachmentReceive(Guid managerId, int recordId)
        {
            return proxy.AttachmentReceive(managerId, recordId);
        }

        /// <summary>
        /// 阅读
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public MessageCodeResponse Read(Guid managerId, int recordId)
        {
            return proxy.Read(managerId, recordId);
        }

        /// <summary>
        /// 获取信息，用于进行标识显示
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public InformationResponse GetInformation(Guid managerId)
        {
            return proxy.GetInformation(managerId);
        }

    }
}
