using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 邮件响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class MailDataResponse : BaseResponse<MailDataEntity>
    {

    }

    /// <summary>
    /// 邮件，分页
    /// </summary>
    [Serializable]
    [DataContract]
    public class MailDataEntity
    {
        /// <summary>
        /// 邮件列表
        /// </summary>
        [DataMember]
        public List<MailInfoEntity> Mails { get; set; }

        /// <summary>
        /// 共享邮件列表
        /// </summary>
        [DataMember]
        public List<MailshareInfoEntity> MailShares { get; set; }

        ///// <summary>
        ///// 当前页
        ///// </summary>
        //[DataMember]
        //public int CurPage { get; set; }
        ///// <summary>
        ///// 总页数
        ///// </summary>
        //[DataMember]
        //public int TotalPage { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// 附件收取响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class MailAttachmentReceiveResponse : BaseResponse<MailAttachmentReceiveEntity>
    {

    }

    /// <summary>
    /// 附件收取
    /// </summary>
    [Serializable]
    [DataContract]
    public class MailAttachmentReceiveEntity
    {
        /// <summary>
        /// 点券，-1不更新
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// 游戏币，-1不更新
        /// </summary>
        [DataMember]
        public int Coin { get; set; }

        /// <summary>
        /// 绑定点卷
        /// </summary>
        [DataMember]
        public int BPoint { get; set; }
    }

}

