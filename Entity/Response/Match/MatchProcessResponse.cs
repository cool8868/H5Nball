using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Match
{
    [Serializable]
    [DataContract]
    public class MatchProcessResponse : BaseResponse<byte[]>
    {
    }
}
