using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Config.Custom;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class MallUseItemResponse : BaseResponse<MallUseItemEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class MallUseItemEntity
    {
        /// <summary>
        /// 效果类型:1，体力；2，灵气；3，金币;4,世界挑战赛体能;5,buff；6，物品；7，技能 8,点卷;9,绑定点卷 100 奥运金牌
        /// </summary>
        [DataMember]
        public int EffectType { get; set; }
        /// <summary>
        /// 效果值
        /// </summary>
        [DataMember]
        public string EffectValue { get; set; }
        /// <summary>
        /// 加效果后的值，用于更新
        /// </summary>
        [DataMember]
        public int CurValue { get; set; }

        /// <summary>
        /// 背包数据
        /// </summary>
        [DataMember]
        public ItemPackageData Package { get; set; }
    }
}
