using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.WebClient.Data;

namespace Games.NBall.WebClient.Weibo.Data
{
    public class WyxPayBackInfo : BaseWebArgs
    {
        #region Names
        public const string COLOrderId = "order_id";
        public const string COLAppKey = "appkey";
        public const string COLSrvKey = "srvkey";
        public const string COLUid = "order_uid";
        public const string COLAmount = "amount";
        public const string COLSign = "sign";
        #endregion

        #region Values
        public string OrderId
        {
            get { return GetValue(COLOrderId); }
            set { SetValue(COLOrderId, value); }
        }
        public string AppKey
        {
            get { return GetValue(COLAppKey); }
            set { SetValue(COLAppKey, value); }
        }
        public int ServerId
        {
            get { return GetInt32(COLSrvKey); }
            set { SetValue(COLSrvKey, value.ToString()); }
        }
        public string Uid
        {
            get { return GetValue(COLUid); }
            set { SetValue(COLUid, value); }
        }
        public int Amount
        {
            get { return GetInt32(COLAmount); }
            set { SetValue(COLAmount, value.ToString()); }
        }
        public string Sign
        {
            get { return GetValue(COLSign); }
            set { SetValue(COLSign, value); }
        }
        #endregion

        public override bool ValidateValue()
        {
            return !string.IsNullOrEmpty(this.OrderId)
              && !string.IsNullOrEmpty(this.Sign)
              && Amount > 0;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}", OrderId, AppKey, ServerId, Uid, Amount, Sign);
        }
    }
}
