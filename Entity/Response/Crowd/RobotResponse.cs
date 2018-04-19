using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Crowd
{
    public class RobotResponse : BaseResponse<RobotEntity>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class RobotEntity
    {
        public bool CrossCrowd { get; set; }

    }
}
