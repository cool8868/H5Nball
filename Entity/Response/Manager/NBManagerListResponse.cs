using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取经理列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class NBManagerListResponse : BaseResponse<NBManagerListEntity>
    {
    }

    [DataContract]
    [Serializable]
    public class NBManagerListEntity
    {
        /// <summary>
        /// 是否需要选择角色
        /// </summary>
        [DataMember]
        public bool NeedSelect { get; set; }
        /// <summary>
        /// 不需要选择角色时，直接使用该对象
        /// </summary>
        [DataMember]
        public NBManagerInfoData ManagerInfo { get; set; }
        /// <summary>
        /// 需要选择角色时，使用这个列表显示选项
        /// </summary>
        [DataMember]
        public List<NBCharacterEntity> CharacterList { get; set; }
        [DataMember]
        public string Cookie { get; set; }
    }
    [DataContract]
    [Serializable]
    public class NBCharacterEntity
    {
        [DataMember]
        public Guid ManagerId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Level { get; set; }
        [DataMember]
        public bool IsNeedChangeName { get; set; }
        [DataMember]
        public string OldZoneName { get; set; }
        [DataMember]
        public string BindCode { get; set; }
    }
}
