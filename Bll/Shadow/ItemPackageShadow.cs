using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public class ItemPackageShadow : IShadow
    {
        public ItemPackageShadow(int packageSize, byte[] itemString, byte itemVersion, Guid transactionId)
        {
            Shadow = new ShadowItemPackageEntity();
            Shadow.TransactionId = transactionId;
            Shadow.OperationType = (int)EnumOperationType.Update;
            Shadow.PackageSize = packageSize;
            Shadow.ItemString = itemString;
            Shadow.ItemVersion = itemVersion;
        }

        public ShadowItemPackageEntity Shadow { get; set; }

        public void Save(ShadowProvider provider)
        {
            provider.SaveItemPackage(Shadow);
        }

    }
}
