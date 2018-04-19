using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class GetAllZoneListResponse : BaseResponse<GetAllZoneList>
    {
    }

    [Serializable]
    [DataContract]
    public class GetAllZoneList
    {
        
    }

    
}
