using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Revelation
{
    /// <summary>
    ///球星启示录挂机输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationOutboardMotorResponse : BaseResponse<RevelationOutboardMotor>
    {
    }

    [Serializable]
    [DataContract]
    public class RevelationOutboardMotor
    {
    }
}
