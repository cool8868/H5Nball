using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Games.NBall.Common;
using Games.NBall.Entity.Response;

namespace Games.NBall.Bll.Frame
{
    public class FrameUtil
    {
        #region CastInt
        public static Dictionary<int, int> CastIntDic(string inStr, params char[] splitChars)
        {
            if (string.IsNullOrEmpty(inStr))
                return new Dictionary<int, int>(0);
            var splits = inStr.Split(splitChars);
            int key, val;
            var dic = new Dictionary<int, int>(splits.Length / 2 + 1);
            for (int i = 0; i < splits.Length; i += 2)
            {
                if (i + 1 >= splits.Length)
                    break;
                int.TryParse(splits[i], out key);
                int.TryParse(splits[i + 1], out val);
                dic[key] = val;
            }
            return dic;
        }
        public static List<int> CastIntList(string inStr, params char[] splitChars)
        {
            var obj = CastIntArray(inStr, splitChars);
            if (null == obj)
                return new List<int>(0);
            else
                return obj.ToList();
        }
        public static int[] CastIntArray(string inStr, params char[] splitChars)
        {
            if (string.IsNullOrEmpty(inStr))
                return new int[0];
            var splits = inStr.Split(splitChars);
            return Array.ConvertAll(splits, s =>
            {
                int val;
                int.TryParse(s, out val);
                return val;
            });
        }
        #endregion

        #region Char32Id
        public static string GenChar22Id()
        {
            return GenChar32Id(Guid.NewGuid()).Substring(0, 22);
        }
        public static bool CheckChar22Id(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 22)
                return false;
            DateTime dt;
            return FromDateTime10Str(id.Substring(0, 10), out dt);
        }
        public static string GenChar32Id()
        {
            return GenChar32Id(Guid.NewGuid());
        }
        public static string GenChar32Id(Guid guid)
        {
            string timeStr = ToDateTime10Str(DateTime.Now);
            string guidStr = EngCharEncoding.FromGuid(guid, 22);
            return string.Concat(timeStr, guidStr);
        }
        public static bool CheckChar32Id(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 32)
                return false;
            DateTime dt;
            return FromDateTime10Str(id.Substring(0, 10), out dt);
        }
        public static string ToDateTime10Str(DateTime dt)
        {
            DateTime dtBase = FrameConfig.BaseTime;
            int month = 12 * (dt.Year - dtBase.Year) + dt.Month;
            month = Math.Min(Math.Max(0, month), 99);
            return string.Concat(month.ToString("00"), dt.ToString("ddHHmmss"));
        }
        public static bool FromDateTime10Str(string str, out DateTime dt)
        {
            dt = DateTime.Now;
            if (string.IsNullOrEmpty(str) || str.Length != 10)
                return false;
            int month = 0;
            if (!int.TryParse(str.Substring(0, 2), out month))
                return false;
            month = month > 0 ? month - 1 : 0;
            DateTime dtBase = FrameConfig.BaseTime;
            string dateStr = dtBase.AddMonths(month).ToString("yyyyMM") + str.Substring(2, 8);
            return DateTime.TryParseExact(dateStr, "yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dt);
        }
        #endregion
    }

    #region RandomPicker
    public class RandomPickIndex
    {
        public RandomPickIndex(int idx) : this(idx, 0, 0) { }
        public RandomPickIndex(int idx, int idx2) : this(idx, idx2, 0) { }
        public RandomPickIndex(int idx, int idx2, int idx3)
        {
            this.Index = idx;
            this.Index2 = idx2;
            this.Index3 = idx3;
        }
        public int Index;
        public int Index2;
        public int Index3;
    }
    public class RandomPicker
    {
        #region .ctor
        public RandomPicker(int idxNum, List<int> points)
            : this(idxNum, null == points ? new int[0] : points.ToArray())
        {
        }
        public RandomPicker(int idxNum, params int[] points)
        {
            if (idxNum <= 0 || idxNum > 3)
                return;
            if (null == points || points.Length == 0)
                return;
            MaxSeed = 0;
            switch (idxNum)
            {
                case 1:
                    for (int i = 0; i < points.Length / 2; i++)
                    {
                        if ((2 * i + 1) >= points.Length)
                            break;
                        MaxSeed += points[2 * i + 1];
                        Rates[MaxSeed] = new RandomPickIndex(points[2 * i]);
                    }
                    break;
                case 2:
                    for (int i = 0; i < points.Length / 3; i++)
                    {
                        if ((3 * i + 2) >= points.Length)
                            break;
                        MaxSeed += points[3 * i + 2];
                        Rates[MaxSeed] = new RandomPickIndex(points[3 * i], points[3 * i + 1]);
                    }
                    break;
                case 3:
                    for (int i = 0; i < points.Length / 4; i++)
                    {
                        if ((4 * i + 3) >= points.Length)
                            break;
                        MaxSeed += points[4 * i + 3];
                        Rates[MaxSeed] = new RandomPickIndex(points[4 * i], points[4 * i + 1], points[4 * i + 2]);
                    }
                    break;
            }
        }
        #endregion

        #region Fields
        readonly SortedDictionary<int, RandomPickIndex> Rates = new SortedDictionary<int, RandomPickIndex>();
        public readonly int MaxSeed = 0;
        static readonly RandomPickIndex DEFAULTPickIndex = new RandomPickIndex(0);
        #endregion

        #region PickRandom
        public RandomPickIndex PickRandom()
        {
            return PickRandom(0, MaxSeed);
        }
        public RandomPickIndex PickRadomFirst(int mid)
        {
            return PickRandom(0, mid);
        }
        public RandomPickIndex PickRadomLast(int mid)
        {
            return PickRandom(mid, MaxSeed);
        }
        public RandomPickIndex PickRandom(int min, int max)
        {
            if (MaxSeed <= 0)
                return DEFAULTPickIndex;
            if (min > max)
            {
                int tmp = min;
                min = max;
                max = tmp;
            }
            int rnd = RandomInt(min, max);
            foreach (var kvp in Rates)
            {
                if (rnd <= kvp.Key)
                    return kvp.Value;
            }
            return DEFAULTPickIndex;
        }
        #endregion

        public static Random GetRandom()
        {
            return new Random(Guid.NewGuid().GetHashCode());
        }
        public static int RandomInt(int min, int max, Random rand = null)
        {
            if (null == rand)
                rand = GetRandom();
            return rand.Next(min, max + 1);
        }
        public static int[] RandomSort(int min, int max, Random rand = null)
        {
            if (min == max)
                return new int[] { min };
            if (null == rand)
                rand = GetRandom();
            if (min > max)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }
            int len = max - min + 1;
            byte[] img = new byte[len];
            int[] dst = new int[len];
            for (int i = 0; i < len; ++i)
            {
                dst[i] = min + i;
            }
            rand.NextBytes(img);
            Array.Sort(img, dst);
            img = null;
            return dst;
        }

    }
    #endregion

    #region EngCharEncoding
    public class EngCharEncoding
    {
        const string BASEChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        static readonly ulong BASENum = (ulong)BASEChars.Length;

        public static string FromNumber(ulong num, int len)
        {
            string str = string.Empty;
            int idx = 0;
            while (num > 0)
            {
                idx = (int)(num % BASENum);
                str = BASEChars[idx] + str;
                num = num / BASENum;
            }
            if (str.Length > len)
                str = str.Substring(str.Length - len);
            else
                str = str.PadLeft(len, '0');
            return str;
        }
        public static ulong ToNumber(string str)
        {
            ulong num = 0;
            int len = str.Length;
            int idx = 0;
            for (int i = 0; i < len; i++)
            {
                idx = BASEChars.IndexOf(str[len - i - 1]);
                if (idx < 0)
                    continue;
                num += (ulong)Math.Pow(BASENum, i) * (ulong)idx;
            }
            return num;
        }
        public static string FromGuid(Guid uid, int len)
        {
            string s = uid.ToString().Replace("-", "").ToUpper();
            string s1 = s.Substring(0, 16);
            string s2 = s.Substring(16);
            ulong num1 = UInt64.Parse(s1, System.Globalization.NumberStyles.HexNumber);
            ulong num2 = UInt64.Parse(s2, System.Globalization.NumberStyles.HexNumber);
            len /= 2;
            return string.Concat(FromNumber(num1, len), FromNumber(num2, len));
        }
        public static Guid ToGuid(string str)
        {
            if (string.IsNullOrEmpty(str))
                return Guid.Empty;
            int len = str.Length / 2;
            string s1 = str.Substring(0, len);
            string s2 = str.Substring(len);
            ulong num1 = ToNumber(s1);
            ulong num2 = ToNumber(s2);
            string uidStr = string.Concat(num1.ToString("X").PadLeft(16, '0'), num2.ToString("X").PadLeft(16, '0'));
            return new Guid(uidStr);
        }
    }
    #endregion

    #region FlatTextFormatter
    public class FlatTextFormatter
    {
        #region Config
        public const char SPLITSect = '^';
        public const char SPLITUnit = ',';
        #endregion

        #region Facade
        public static string ItemToText<T>(T item, string[] units, bool withPrefix)
            where T : IFlatSplit
        {
            return ItemToText<T>(item, units, withPrefix, SPLITSect, SPLITUnit);
        }
        public static string ItemToText<T>(T item, string[] units, bool withPrefix, char splitSect, char splitUnit)
            where T : IFlatSplit
        {
            if (null == item)
                return string.Empty;
            if (null == units)
                units = item.GetFlatSplit();
            if (!item.ToFlatSplit(units))
                return string.Empty;
            return string.Concat(withPrefix ? splitSect.ToString() : string.Empty, string.Join(splitUnit.ToString(), units));
        }
        public static T ItemFromText<T>(string text)
          where T : IFlatSplit, new()
        {
            return ItemFromText<T>(text, SPLITUnit);
        }
        public static T ItemFromText<T>(string text, char splitUnit)
            where T : IFlatSplit, new()
        {
            if (string.IsNullOrEmpty(text))
                return default(T);
            var units = text.Split(splitUnit);
            var item = new T();
            if (!item.FromFlatSplit(units))
                return default(T);
            return item;
        }
        public static string ListToText<T>(IEnumerable<T> list)
            where T : IFlatSplit
        {
            return ListToText<T>(list, SPLITSect, SPLITUnit);
        }
        public static string ListToText<T>(IEnumerable<T> list, char splitSect, char splitUnit, string[] units = null)
            where T : IFlatSplit
        {
            if (null == list)
                return string.Empty;
            var sb = new StringBuilder();
            foreach (var item in list)
            {
                if (null == item)
                    continue;
                if (null == units)
                    units = item.GetFlatSplit();
                sb.Append(ItemToText<T>(item, units, true, splitSect, splitUnit));
            }
            var str = sb.ToString();
            sb.Clear();
            return str;
        }
        public static List<T> ListFromText<T>(string text)
            where T : IFlatSplit, new()
        {
            return ListFromText<T>(text, SPLITSect, SPLITUnit);
        }
        public static List<T> ListFromText<T>(string text, char splitSect, char splitUnit)
            where T : IFlatSplit, new()
        {
            if (string.IsNullOrEmpty(text))
                return new List<T>(0);
            T item = default(T);
            var sects = text.Trim(splitSect).Split(splitSect);
            var list = new List<T>(sects.Length);
            foreach (var sect in sects)
            {
                item = ItemFromText<T>(sect, splitUnit);
                if (null == item)
                    continue;
                list.Add(item);
            }
            sects = null;
            return list;
        }
        #endregion
    }
    #endregion
}
