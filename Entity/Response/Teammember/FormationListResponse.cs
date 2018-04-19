using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 阵型列表Response
    /// </summary>
    [DataContract]
    [Serializable]
    public class FormationListResponse : BaseResponse<FormationList>
    {
    }

    /// <summary>
    /// 阵型列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class FormationList
    {
        /// <summary>
        /// 当前使用阵型
        /// </summary>
        [DataMember]
        public int CurrentFormationId { get; set; }


        /// <summary>
        /// 阵型列表
        /// </summary>
        [DataMember]
        public List<FormationOutEntity> Formations { get; set; }
        /// <summary>
        /// Kpi
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }

    }

    [Serializable]
    [DataContract]
    public class FormationOutEntity
    {
        public FormationOutEntity()
        {

        }

        public FormationOutEntity(int idx, int level, int sophisticate)
        {
            Idx = idx;
        }

        /// <summary>
        /// 阵型id
        /// </summary>
        [DataMember]
        public int Idx { get; set; }

    }
}
