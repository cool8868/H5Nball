using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Item
{
   /// <summary>
   /// 背包输出类
   /// </summary>
    [Serializable]
    [DataContract]
    public class GetPackageResponse :BaseResponse<GetPackage>
    {
    }

    [Serializable]
    [DataContract]
    public class GetPackage
    {
        /// <summary>
        /// 背包数据
        /// </summary>
        [DataMember]
        public byte[] ItemString { get; set; }

        /// <summary>
        /// 背包大小
        /// </summary>
        [DataMember]
        public int PackageSize { get; set; }
    }
}
