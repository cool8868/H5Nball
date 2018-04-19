using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class ItemShadow : IShadow
    {
        public ItemShadow(ItemInfoEntity entity, EnumOperationType operationType, Guid transactionId, int operationCount)
        {
            Shadow = new ShadowItemEntity();
            Shadow.TransactionId = transactionId;
            Shadow.OperationType = (int)operationType;
            Shadow.ItemId = entity.ItemId;
            Shadow.ItemCode = entity.ItemCode;
            Shadow.ItemType = entity.ItemType;
            Shadow.ItemCount = entity.ItemCount;
            Shadow.IsBinding = entity.IsBinding;
            Shadow.ItemProperty = SerializationHelper.ToByte(entity.ItemProperty);
            Shadow.GridIndex = entity.GridIndex;
            Shadow.Status = entity.Status;
            Shadow.OperationCount = operationCount;
        }

        public ShadowItemEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SaveItem(Shadow);
        }

    }
}
