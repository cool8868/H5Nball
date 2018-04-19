using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;

using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{

    /// <summary>
    /// 球魂的属性
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class BallSoulProperty:ItemProperty
    {

        public override ItemProperty Clone()
        {
            return new BallSoulProperty();
        }
    }
}
