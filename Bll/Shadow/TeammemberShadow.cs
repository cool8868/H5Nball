using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class TeammemberShadow : IShadow
    {
        public TeammemberShadow(TeammemberEntity entity, EnumOperationType operationType, Guid transactionId)
            : this(entity.Idx, entity.PlayerId, operationType, transactionId)
        {
        }

        public TeammemberShadow(Guid teammemberId, int playerId, EnumOperationType operationType, Guid transactionId)
        {
            Shadow = new ShadowTeammemberEntity();
            Shadow.TransactionId = transactionId;
            Shadow.OperationType = (int)operationType;
            Shadow.TeammemberId = teammemberId;
            Shadow.PlayerId = playerId;
        }

        public ShadowTeammemberEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SaveTeammember(Shadow);
        }

    }
}
