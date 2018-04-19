using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;
using Games.NBall.Core.FriendShip;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceEngine;

namespace Games.NBall.Core
{
    class OnlineConfig
    {
        #region Connection
        public static readonly int COMMANDTimeout = 120;
        public static readonly int COMMANDBatchSize = 3000;
        public static readonly int PROCBatchSize = 5000;
        #endregion

        #region Timer
        public static int TIMEWaitLock = 3000;
        public static int INITCacheSize = 5000;
        public static int INITUpdateAtMinute = 0;
        public static int INITUpdateDueMinutes = 1;//5
        public static int INITResetAtMinute = 0;//120
        public static int INITResetDueMinutes = 24 * 60 - 1;//24*60
        public static int TIMERBuffDueSeconds = 30;
        public static int TIMERGuildDueMinutes = 60;
        public static int RETRYDueMilliseconds = 1000;
        public static int TIMEOffLineMinutes = 3;
        #endregion

    }
}
