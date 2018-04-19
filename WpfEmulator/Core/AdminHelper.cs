using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceContract.Client;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.WpfEmulator.Core
{
    public class AdminHelper
    {
        private readonly AdminClient adminClient = new AdminClient();
        public static MessageCode AddItem(int itemCode,int itemCount,int strength)
        {
            return AdminCore.Instance.AddItems(ApiTestCore.CurManager.Idx, itemCode, itemCount, strength, false);
            return MessageCode.Success;
        }

        public static MessageCode AddCoin(int coin)
        {
            return AdminCore.Instance.AddCoin(ApiTestCore.CurManager.Idx, coin);
        }

        public static MessageCode AddSophisticate(int coin)
        {
            return AdminCore.Instance.AddSophisticate(ApiTestCore.CurManager.Idx, coin);
        }

        public static MessageCode AddReiki(int coin)
        {
            return AdminCore.Instance.AddReiki(ApiTestCore.CurManager.Idx, coin);
        }

        public static MessageCode ChargeTest(int point,DateTime curTime)
        {
            return PayCore.Instance.ChargeTest(ApiTestCore.CurManager.Account,(int)EnumChargeSourceType.Emulator, point / 10, point, 0, Guid.NewGuid().ToString(), curTime);
            //AdminCore.Instance.Charge(ApiTestCore.CurManager.Idx, ApiTestCore.CurManager.Account, point);
        }
        public MessageCode Charge(int point, DateTime curTime)
        {
            //return adminClient.Charge(ApiTestCore.CurManager.Account, (int)EnumChargeSourceType.Emulator, point / 10, point, 0, Guid.NewGuid().ToString());
            return MessageCode.Success;

        }
        public static NBManagerInfoResponse Levelup(Guid managerId)
        {
            return AdminCore.Instance.Levelup(managerId);
        }

        public static MessageCode ClearPackage(Guid managerId)
        {
            return AdminCore.Instance.ClearPackage(managerId);
        }
    }
}
