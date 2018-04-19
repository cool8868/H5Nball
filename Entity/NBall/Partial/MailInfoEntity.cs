using System;
using System.Runtime.Serialization;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity
{    

	public partial class MailInfoEntity
	{
        /// <summary>
        /// 邮件发送时刻
        /// </summary>
        [DataMember]
        public long MailTick { get; set; }

        /// <summary>
        /// 邮件过期时刻
        /// </summary>
        [DataMember]
        public long MailExpiredTick { get; set; }

        /// <summary>
        /// 邮件附件
        /// </summary>
        [DataMember]
        public MailAttachmentEntity MailAttachment { get; set; }
	}
	
	
    public partial class MailInfoResponse
    {

    }
}

