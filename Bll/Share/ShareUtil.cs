using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;

namespace Games.NBall.Bll.Share
{
    public class ShareUtil
    {
        public const int SuccessCode = (int) MessageCode.Success;
        public const int ExceptionCode = (int)MessageCode.Exception;
        public static readonly DateTime BaseTime=new DateTime(1970,1,1,0,0,0,0);

        #region GetServerIp
        /// <summary>
        /// Gets the server ip.
        /// </summary>
        /// <returns></returns>
        public static string GetServerIp()
        {
            try
            {
                string hostName = Dns.GetHostName(); //得到主机名
                IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
                for (int i = 0; i < ipEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (ipEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ipEntry.AddressList[i].ToString();
                    }
                }
                return "0.0.0.0";
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "0.0.0.0";
            }
        }
        #endregion

        #region CalWinType
        /// <summary>
        /// 计算比赛结果类型
        /// </summary>
        /// <param name="score"></param>
        /// <param name="awayScore"></param>
        /// <returns></returns>
        public static EnumWinType CalWinType(int score, int awayScore)
        {
            if (score > awayScore)
                return EnumWinType.Win;
            else if (score == awayScore)
                return EnumWinType.Draw;
            else
            {
                return EnumWinType.Lose;
            }
        }

        #endregion

        #region GenerateMatchId
        /// <summary>
        /// 生成32位唯一id
        /// </summary>
        /// <returns></returns>
        public static string GenerateMatchId()
        {
            Guid guid = Guid.NewGuid();

            byte[] buffer = guid.ToByteArray();

            var shortGuid = Convert.ToBase64String(buffer);
            shortGuid = shortGuid.Replace('+', 'Q');
            return string.Format("{0:yyyyMMdd}{1}", DateTime.Now, shortGuid);
        }
        #endregion

        #region BuildCacheKey

        //public static string BuildCacheKey(EnumMemcachedKey key, int id)
        //{
        //    return BuildCacheKey(key, id.ToString());
        //}

        //public static string BuildCacheKey(EnumMemcachedKey key, Guid id)
        //{
        //    return BuildCacheKey(key, id.ToString());
        //}

        //public static string BuildCacheKey(EnumMemcachedKey key, string id)
        //{
        //    return string.Format("{0}{1}", key.ToString(), id);
        //}

        #endregion

        #region UuidCreateSequential
        [System.Runtime.InteropServices.DllImport("Rpcrt4", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern long UuidCreateSequential(ref System.Guid ptrGuid);

        public static Guid CreateSequential()
        {
            Guid id = Guid.Empty;
            long num = UuidCreateSequential(ref id);
            if (0L != num)
            {
                return Guid.NewGuid();
            }
            return id;
        }
        #endregion

        #region GenerateComb
        /// <summary>
        /// Generate a new <see cref="Guid"/> using the comb algorithm.
        /// </summary>
        public static Guid GenerateComb()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }

        /// <summary>
        /// 从guid中返回日期串，for比赛日表
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string GetDateFromComb(Guid guid)
        {
            byte[] guidArray = guid.ToByteArray();
            byte[] dateArray = new byte[4];
            dateArray[0] = guidArray[11];
            dateArray[1] = guidArray[10];
            DateTime baseDate = new DateTime(1900, 1, 1);

            int i = BitConverter.ToInt32(dateArray, 0);
            var date = baseDate.AddDays(i);
            return date.ToString("yyyyMMdd");
        }

        //================================================================
        /// <summary>
        /// 从 SQL SERVER 返回的 GUID 中生成时间信息
        /// </summary>
        /// <param name="guid">包含时间信息的 COMB </param>
        /// <returns>时间</returns>
        public static DateTime GetDateTimeFromComb(System.Guid guid)
        {
            DateTime baseDate = new DateTime(1900, 1, 1);
            byte[] daysArray = new byte[4];
            byte[] msecsArray = new byte[4];
            byte[] guidArray = guid.ToByteArray();

            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);

            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Convert the bytes to ints 
            int days = BitConverter.ToInt32(daysArray, 0);
            int msecs = BitConverter.ToInt32(msecsArray, 0);

            DateTime date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.333333);

            return date;
        }
        #endregion

        #region GetTableMod
        /// <summary>
        /// 按经理id取模
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public static int GetTableMod(Guid managerId)
        {
            var code = managerId.GetHashCode();
            int mod= code%10;
            return Math.Abs(mod);
        }
        #endregion

        #region CheckBytesValidity
        /// <summary>
        /// 序列化前 检查二进制值是否有效
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool CheckBytes(byte[] param)
        {
            if (param != null && param.Length > 0)
                return true;
            return false;
        }
        #endregion

        #region GetTimeTick
        /// <summary>
        /// 获取时间刻度，相对于基准时间的毫秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetTimeTick(DateTime time)
        {
            return Convert.ToInt64(time.Subtract(BaseTime).TotalMilliseconds);
        }

        public static long GetTimeSeconds(DateTime time)
        {
            return Convert.ToInt64(time.Subtract(BaseTime).TotalSeconds);
        }

        /// <summary>
        /// 获取时间，相对于基准时间
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public static DateTime GetTime(long tick)
        {
            return BaseTime.AddMilliseconds(tick);
        }

        /// <summary>
        /// 获取时间，相对于基准时间
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public static DateTime GetTimeeconds(long tick)
        {
            return BaseTime.AddSeconds(tick);
        }

        /// <summary>
        /// 获取本周某一天的时间
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public static DateTime GetThisWeekDayDate(DayOfWeek week)
        {
            DateTime date = DateTime.Now.Date;
            DateTime mondayTime = DateTime.Now.Date;
            if (week == DayOfWeek.Sunday)//中国算法、 星期1才是每个星期的第一天
            {
                mondayTime = date.AddDays(1);
            }
            else
            {
                int i = date.DayOfWeek - DayOfWeek.Monday;
                if (i == -1) i = 6; // i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。 
                TimeSpan ts = new TimeSpan(i, 0, 0, 0);
                mondayTime = date.Subtract(ts); //周一的时间
            }
            int addDay = 0;
            switch (week)
            {
                case DayOfWeek.Monday:
                    addDay = 0;
                    break;
                case DayOfWeek.Tuesday:
                    addDay = 1;
                    break;
                case DayOfWeek.Wednesday:
                    addDay = 2;
                    break;
                case DayOfWeek.Thursday:
                    addDay = 3;
                    break;
                case DayOfWeek.Friday:
                    addDay = 4;
                    break;
                case DayOfWeek.Saturday:
                    addDay = 5;
                    break;
                case DayOfWeek.Sunday:
                    addDay = 6;
                    break;
            }
            return mondayTime.AddDays(addDay);
        }

        #endregion

        #region CalCharacterLength
        public static int CalCharacterLength(string character)
        {
            string chinese = @"[\u4E00-\u9FA5]";
            string letter = @"[a-zA-Z]";
            string number = @"^\d+$";
            //bool result = false;
            int count = 0;
            if (string.IsNullOrEmpty(character))
            {
                return 0;
            }
            char[] ch = character.ToCharArray();
            foreach (char c in ch)
            {
                if (Regex.IsMatch(c.ToString(), chinese))
                {
                    count = count + 2;
                }
                else if (Regex.IsMatch(c.ToString(), letter) || Regex.IsMatch(c.ToString(), number))
                {
                    count++;
                }
                else
                {
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region CalWaitTime
        public static int CalWaitTime(DateTime nextMatchTime, DateTime curTime)
        {
            if (nextMatchTime < curTime)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(nextMatchTime.Subtract(curTime).TotalSeconds);
            }
        }
        #endregion

        #region CalPageCount
        public static int CalPageCount(int totalRecord, int pageSize)
        {
            if (pageSize == 0)
                return 0;
            int pageCount = totalRecord/pageSize;
            if (totalRecord%pageSize != 0)
                pageCount++;
            return pageCount;
        }
        #endregion

        #region GetItemBinding
        public static bool GetItemBinding(EnumItemPrizeType prizeType, int vipLevel = 0)
        {
           if (prizeType == EnumItemPrizeType.PlayerKillPrize)
            {
                var effect = CacheFactory.VipdicCache.GetEffectValue(vipLevel, EnumVipEffect.PlayerKillItemBinding);
                if (effect == 1)
                    return false;
                else
                {
                    return true;
                }
            }
            else if (prizeType == EnumItemPrizeType.WorldChallengeDrop
                || prizeType == EnumItemPrizeType.LoginDailyPrize
                || prizeType == EnumItemPrizeType.OnlinePrize
                || prizeType == EnumItemPrizeType.NewPlayerPack
                || prizeType == EnumItemPrizeType.LadderExchange
                || prizeType == EnumItemPrizeType.TaskPrize
                || prizeType == EnumItemPrizeType.FirstChargePrize)
            {
                return true;
            }
            else if (prizeType == EnumItemPrizeType.LadderPrize
                || prizeType == EnumItemPrizeType.LeaguePrize)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region CalTimerInterval
        /// <summary>
        /// 返回与当前时间的秒数
        /// </summary>
        /// <param name="nextTime"></param>
        /// <returns></returns>
        public static double CalTimerInterval(DateTime nextTime)
        {
            return CalTimerInterval(nextTime, DateTime.Now);
        }

        public static double CalTimerInterval(DateTime nextTime, DateTime curTime)
        {
            TimeSpan ts = nextTime.Subtract(curTime);
            return ts.TotalMilliseconds;
        }

        public static int CalCountdown(DateTime nextTime)
        {
            return CalCountdown(nextTime, DateTime.Now);
        }

        /// <summary>
        /// 返回与当前时间的CD秒数
        /// </summary>
        /// <param name="nextTime"></param>
        /// <returns></returns>
        public static int CalCountdown(DateTime nextTime,DateTime curTime)
        {
            if (curTime < nextTime)
            {
                var s = CalTimerInterval(nextTime);
                return Convert.ToInt32(s);
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region GetCrossManagerName
        public static string GetCrossManagerNameByZoneId(string zoneId, string name)
        {
            var zoneName = CacheFactory.FunctionAppCache.GetCrossZoneName(zoneId);
            return string.Format("{0}.{1}", zoneName, name);
        }
        #endregion

        #region Md5
        /// <summary>
        /// Gets the M d5，默认返回小写
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string GetMD5(string input)
        {
            return GetMD5(input, "x2");
        }

        /// <summary>
        /// Gets the M d5.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="outFormat">输出格式，X2|x2.</param>
        /// <returns></returns>
        public static string GetMD5(string input, string outFormat)
        {
            MD5CryptoServiceProvider sha1 = new MD5CryptoServiceProvider();
            byte[] inputData = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] outputData = sha1.ComputeHash(inputData);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < outputData.Length; i++)
            {
                sb.Append(outputData[i].ToString(outFormat));
            }
            return sb.ToString();
        }

        public static string GetBlueVipAccount(string account)
        {
            return "BLUE|" + account;
        }

        public static string GetWanBaVipAccount(string account)
        {
            return "WANBA|" + account;
        }
        #endregion

        /// <summary>
        /// 将时间转成unix时间戳.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static double DateTime2UnixTimeStamp(DateTime time)
        {
            double result = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            result = (time - startTime).TotalSeconds;
            return result;
        }

        static FunctionAppCache FunctionAppCache { get { return FunctionAppCache.Instance; } }

        private static int _appId = -1;
        public static int AppId
        {
            get
            {
                if (_appId < 0)
                {
                    _appId = FunctionAppCache.GetAppId(ConfigurationManager.AppSettings["AppName"]);
                }
                return _appId;
            }
        }

        private static int _zoneId = -1;
        public static int ZoneId
        {
            get
            {
                if (_zoneId < 0)
                {
                    _zoneId = FunctionAppCache.GetZoneId(ConfigurationManager.AppSettings["ZoneName"]);
                }
                return _zoneId;
            }
        }

        private static int _zoneNumber = -1;
        public static int ZoneNumber
        {
            get
            {
                if (_zoneNumber < 0)
                {
                    _zoneNumber = ZoneId%1000;
                }
                return _zoneNumber;
            }
        }

        public static bool IsCross
        {
            get { return ZoneName.StartsWith("Cross"); }
        }

        public static string ZoneName
        {
            get
            {
                return ConfigurationManager.AppSettings["ZoneName"];
            }
        }

        private static string _platformCode = "";
        public static string PlatformCode
        {
            get
            {
                if (string.IsNullOrEmpty(_platformCode))
                {
                    var zone = FunctionAppCache.GetZone(ConfigurationManager.AppSettings["ZoneName"]);
                    if (zone != null)
                        _platformCode = zone.PlatformCode.ToLower();
                }
                return _platformCode;
            }
        }

        private static string _platformZoneName = "";
        public static string PlatformZoneName
        {
            get
            {
                if (string.IsNullOrEmpty(_platformZoneName))
                {
                    var zone = FunctionAppCache.GetZone(ConfigurationManager.AppSettings["ZoneName"]);
                    if (zone != null)
                        _platformZoneName = zone.PlatformZoneName.ToLower();
                }
                return _platformZoneName;
            }
        }

        private static string _name = "";
        public static string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    var zone = FunctionAppCache.GetZone(ConfigurationManager.AppSettings["ZoneName"]);
                    if (zone != null)
                        _name = zone.Name;
                }
                return _name;
            }
        }
        private static string _crossname = "";
        public static string CrossName
        {
            get
            {
                if (string.IsNullOrEmpty(_crossname))
                {
                    var zone = FunctionAppCache.GetZone(ConfigurationManager.AppSettings["ZoneName"]);
                    if (zone != null)
                        _crossname = zone.CrossName;
                }
                return _crossname;
            }
        }

        public static bool IsH5A8
        {
            get { return PlatformCode == "h5_a8"; }
        }

        public static bool IsTx
        {
            get { return PlatformCode == "h5_wb" || PlatformCode == "txh5_a8"; }
        }

        public static bool IsEgret
        {
            get { return PlatformCode == "h5_egret"; }
        }

        public static bool IsBear
        {
            get { return PlatformCode == "h5_bear"; }
        }

        public static bool IsQunHei
        {
            get { return PlatformCode == "h5_qunhei"; }
        }
        private static string _domainUrl = "*";
        public static string DomainUrl
        {
            get
            {
                if (_domainUrl == "*")
                {
                    var domainUrl = ConfigurationManager.AppSettings["DoaminUrl"];
                    if (domainUrl != null)
                        _domainUrl = domainUrl;
                }
                return _domainUrl;
            }
        }


        /// <summary>
        /// 十六进制字符串转换成字节数组 
        /// </summary>
        /// <param name="hexString">要转换的字符串</param>
        /// <returns></returns>
        public static byte[] HexStrToByteArray(string hexString)
        {
            if (hexString.StartsWith("0x"))
            {
                hexString = hexString.Remove(0, 2);
            }

            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                throw new ArgumentException("十六进制字符串长度不对");
            byte[] buffer = new byte[hexString.Length / 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 0x10);
            }
            return buffer;
        }

        /// <summary>
        /// 字节数组转换成十六进制字符串
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <returns></returns>
        public static string ByteArrayToHexStr(byte[] byteArray)
        {
            int capacity = byteArray.Length * 2;
            StringBuilder sb = new StringBuilder(capacity);

            if (byteArray != null)
            {
                for (int i = 0; i < byteArray.Length; i++)
                {
                    sb.Append(byteArray[i].ToString("X2"));
                }
            }
            return "0x" + sb;
        }

        static EnumAppCode? s_appCode = null;

        public static EnumAppCode AppCode
        {
            get
            {
                if (null != s_appCode)
                    return s_appCode.Value;
                string str = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.APPCode);
                EnumAppCode code = EnumAppCode.COMMON;
                if (!string.IsNullOrEmpty(str) && Enum.TryParse(str, true, out code))
                    s_appCode = code;
                else
                    s_appCode = EnumAppCode.COMMON;
                return s_appCode.Value;
            }
        }

        public static bool IsAppRXYC
        {
            get
            {
                return AppCode == EnumAppCode.RXYC;
            }
        }
    }
}
