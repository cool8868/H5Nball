using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class PandoraMedalShadow : IShadow
    {

        public PandoraMedalShadow(Guid managerId, int type, string itemIds, string itemCodes, int medalCount, Guid transactionId)
        {
            Shadow = new ShadowPandoraMedalEntity();
            Shadow.ManagerId = managerId;
            Shadow.Type = type;
            Shadow.ItemIds = itemIds;
            Shadow.ItemCodes = itemCodes;
            Shadow.MedalCount = medalCount;
            Shadow.TransactionId = transactionId;
        }

        public void Save(ShadowProvider provider)
        {
            provider.SavePandoraMedal(Shadow);
        }

        public ShadowPandoraMedalEntity Shadow { get; set; }
    }
}
