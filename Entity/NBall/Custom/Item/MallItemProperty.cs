using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace Games.NBall.Entity.NBall.Custom
{
    /// <summary>
    /// 商城道具属性
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class MallItemProperty:ItemProperty
    {
        public override ItemProperty Clone()
        {
            return new MallItemProperty();
        }
    }
}
