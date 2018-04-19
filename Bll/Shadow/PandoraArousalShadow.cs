using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class PandoraArousalShadow : IShadow
    {
        public PandoraArousalShadow(Guid managerId, Guid sourceCard, int sourceCardLv, int curArousalLv, int tarArousalLv, Guid useCard,
            int useCardStrenth, bool isBingding, Guid transactionId)
        {
            Shadow = new ShadowPandoraArousalEntity();
            Shadow.TransactionId = transactionId;
            Shadow.ManagerId = managerId;
            Shadow.SourceCard = sourceCard;
            Shadow.SourceCardLv = sourceCardLv;
            Shadow.CurArousalLv = curArousalLv;
            Shadow.TarArousalLv = tarArousalLv;
            Shadow.UseCard = useCard;
            Shadow.UseCardStrenth = useCardStrenth;
            Shadow.IsBinding = isBingding;
        }

        public ShadowPandoraArousalEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraArousal(Shadow);
        }
    }
}
