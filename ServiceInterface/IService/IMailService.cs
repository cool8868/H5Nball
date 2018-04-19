using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Information;
using Games.NBall.Entity.Response.Item;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IMailService
    {
        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [OperationContract]
        MailDataResponse GetMailData(Guid managerId);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordIds"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [OperationContract]
        MailDataResponse DeleteMail(Guid managerId, string recordIds);

        /// <summary>
        /// 收取附件
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        [OperationContract]
        MailAttachmentReceiveResponse AttachmentReceive(Guid managerId, int recordId);

        /// <summary>
        /// 阅读
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse Read(Guid managerId, int recordId);

        /// <summary>
        /// 获取信息，用于进行标识显示
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns> 
        [OperationContract]
        InformationResponse GetInformation(Guid managerId);
    }
}
