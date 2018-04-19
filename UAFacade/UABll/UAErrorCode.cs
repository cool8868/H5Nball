using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.NBall.UAFacade
{
    public class UAErrorCode
    {
        public const string ErrOK = "0";//成功
        public const string ErrPara = "1";
        public const string ErrCheckSign = "2";//签名错误
        public const string ErrPlatform = "3";//serverid错误
        public const string ErrRepeatOrder = "4";//重复领奖
        public const string ErrDataOP = "5";//参数错误
        public const string ErrTimeout = "6";//超时
        public const string ErrNoManager = "7";//用户信息错误
        public const string ErrNoUser = "8";//没有注册
        public const string ErrTxException = "9";
        public const string ErrOther = "99";//其他错误
        public const string ErrException = "10";

    }
}