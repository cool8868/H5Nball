using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Activity
{
    [Serializable]
    [DataContract]
    public class ActivityExRecordListResponse : BaseResponse<ActivityExRecordListEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class ActivityExRecordListEntity
    {
        [DataMember]
        public List<ActivityExRecordGroupEntity> Groups { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ActivityExRecordGroupEntity
    {
       
        [DataMember]
        public int GroupId { get; set; }
        [DataMember]
        public int ExData { get; set; }
        [DataMember]
        public ActivityexRecordEntity ExRecord { get; set; }

        [DataMember]
        public List<ActivityexRankEntity> Ranks { get; set; }

    }
}
