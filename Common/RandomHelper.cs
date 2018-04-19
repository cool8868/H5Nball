
using System;
using System.Collections.Generic;

namespace Games.NBall.Common
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public static class RandomHelper
    {
        private static Random _random=new Random(Seed);
        private static readonly object _locker = new object();
        
        /// <summary>
        /// 随机数种子
        /// </summary>
        public static int Seed
        {
            get { return Guid.NewGuid().GetHashCode(); }
        }

        /// <summary>
        /// 获取随机数(包含min，但不包含max)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetInt32WithoutMax(int min, int max)
        {
            if (min == max) return min;
            if (min > max)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }

            lock (_locker)
            {
                return _random.Next(min, max);
            }
        }

        /// <summary>
        /// 获取随机数（包含min和max）
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static int GetInt32(int min, int max)
        {
            if (min == max) return min;
            if (min > max)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }

            lock (_locker)
            {
                return _random.Next(min, max + 1);
            }
        }

        /// <summary>
        /// 获取随机Boolean型
        /// </summary>
        /// <returns></returns>
        public static bool GetBoolean()
        {
            return GetInt32(0, 1) > 0;
        }

        /// <summary>
        /// 获取0 - 100随机值
        /// </summary>
        /// <returns></returns>
        public static int GetPercentage()
        {
            return GetInt32WithoutMax(0, 100);
        }

        public static bool CheckPercentagePow(int rate)
        {
            if (rate < 1)
                return false;
            if (rate >= 10000)
                return true;
            var random = GetPercentagePow();
            return random < rate;
        }

        public static bool CheckPercentage(int rate)
        {
            if (rate < 1)
                return false;
            if (rate >= 100)
                return true;
            var random = GetPercentage();
            return random < rate;
        }

        public static bool CheckPercentage(double rate)
        {
            if (rate < 1)
                return false;
            var random = GetPercentage();
            return random < rate;
        }

        /// <summary>
        /// 获取0 - 10000随机值
        /// </summary>
        /// <returns></returns>
        public static int GetPercentagePow()
        {
            return GetInt32WithoutMax(0, 10000);
        }

        /// <summary>
        /// 获取随机排列的数组
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="list">数组</param>
        /// <returns>随机排列后的数组</returns>
        public static List<T> GetRandomTestSortList<T>(List<T> list)
        {
            List<Int32> indexList = new List<int>(list.Count);
            for (Int32 i = 0; i < list.Count; i++)
            {
                indexList.Add(i);
            }

            List<Int32> randomIndex = new List<int>(list.Count);
            while (indexList.Count > 0)
            {
                var index = indexList[GetInt32(0, indexList.Count - 1)];
                randomIndex.Add(index);

                indexList.Remove(index);
            }

            List<T> newList = new List<T>(list.Count);
            foreach (Int32 index in randomIndex)
            {
                newList.Add(list[index]);
            }

            return newList;
        }
    }

    [Serializable()]
    public class RandomTest
    {
      //
      // Private Constants 
      //
      private const int MBIG =  Int32.MaxValue;
      private const int MSEED = 161803398;
      private const int MZ = 0;
    
      
      //
      // Member Variables
      //
      private int inext, inextp;
      private int[] SeedArray = new int[56];
    

    
      /**//// <include file='doc\RandomTest.uex' path='docs/doc[@for="RandomTest.RandomTest"]/*' />
      public RandomTest() 
        : this(Environment.TickCount) {
      }
    
      /**//// <include file='doc\RandomTest.uex' path='docs/doc[@for="RandomTest.RandomTest1"]/*' />
      public RandomTest(int Seed) {
        int ii;
        int mj, mk;
    
        //Initialize our Seed array.
        //This algorithm comes from Numerical Recipes in C (2nd Ed.)
        mj = MSEED - Math.Abs(Seed);
        SeedArray[55]=mj;
        mk=1;
        for (int i=1; i<55; i++) {  //Apparently the range [1..55] is special (Knuth) and so we're wasting the 0'th position.
          ii = (21*i)%55;
          SeedArray[ii]=mk;
          mk = mj - mk;
          if (mk<0) mk+=MBIG;
          mj=SeedArray[ii];
        }
        for (int k=1; k<5; k++) {
          for (int i=1; i<56; i++) {
        SeedArray[i] -= SeedArray[1+(i+30)%55];
        if (SeedArray[i]<0) SeedArray[i]+=MBIG;
          }
        }
        inext=0;
        inextp = 21;
        Seed = 1;
      }
    
      //
      // Package Private Methods
      //
    
      /**//*====================================Sample====================================
      **Action: Return a new random number [0..1) and reSeed the Seed array.
      **Returns: A double [0..1)
      **Arguments: None
      **Exceptions: None
      ==============================================================================*/
      /**//// <include file='doc\RandomTest.uex' path='docs/doc[@for="RandomTest.Sample"]/*' />
      protected virtual double Sample() {
          int retVal;
          int locINext = inext;
          int locINextp = inextp;

          if (++locINext >=56) locINext=1;
          if (++locINextp>= 56) locINextp = 1;
          
          retVal = SeedArray[locINext]-SeedArray[locINextp];
          
          if (retVal<0) retVal+=MBIG;
          
          SeedArray[locINext]=retVal;

          inext = locINext;
          inextp = locINextp;
                    
          //Including this division at the end gives us significantly improved
          //random number distribution.
          return (retVal*(1.0/MBIG));
      }
    
      //
      // Public Instance Methods
      // 
    
    
      /**//*=====================================Next=====================================
      **Returns: An int [0.._int4.MaxValue)
      **Arguments: None
      **Exceptions: None.
      ==============================================================================*/
      /**//// <include file='doc\RandomTest.uex' path='docs/doc[@for="RandomTest.Next"]/*' />
      public virtual int Next() {
        return (int)(Sample()*Int32.MaxValue);
      }
    
      /**//*=====================================Next=====================================
      **Returns: An int [minvalue..maxvalue)
      **Arguments: minValue -- the least legal value for the RandomTest number.
      **           maxValue -- the greatest legal return value.
      **Exceptions: None.
      ==============================================================================*/
      /**//// <include file='doc\RandomTest.uex' path='docs/doc[@for="RandomTest.Next1"]/*' />
      public virtual int Next(int minValue, int maxValue) {
          if (minValue>maxValue) {
              throw new ArgumentOutOfRangeException();
          }
          
          int range = (maxValue-minValue);
    
          //This is the case where we flipped around (e.g. MaxValue-MinValue);
          if (range<0) {
              long longRange = (long)maxValue-(long)minValue;
              return (int)(((long)(Sample()*((double)longRange)))+minValue);
          }
          
          return ((int)(Sample()*(range)))+minValue;
      }
    
    
      /**//*=====================================Next=====================================
      **Returns: An int [0..maxValue)
      **Arguments: maxValue -- the greatest legal return value.
      **Exceptions: None.
      ==============================================================================*/
      /**//// <include file='doc\RandomTest.uex' path='docs/doc[@for="RandomTest.Next2"]/*' />
      public virtual int Next(int maxValue) {
          if (maxValue<0) {
              throw new ArgumentOutOfRangeException();
          }
          return (int)(Sample()*maxValue);
      }
    
    
      /**//*=====================================Next=====================================
      **Returns: A double [0..1)
      **Arguments: None
      **Exceptions: None
      ==============================================================================*/
      /**//// <include file='doc\RandomTest.uex' path='docs/doc[@for="RandomTest.NextDouble"]/*' />
      public virtual double NextDouble() {
        return Sample();
      }
    
    
      /**//*==================================NextBytes===================================
      **Action:  Fills the byte array with random bytes [0..0x7f].  The entire array is filled.
      **Returns:Void
      **Arugments:  buffer -- the array to be filled.
      **Exceptions: None
      ==============================================================================*/
      /**//// <include file='doc\RandomTest.uex' path='docs/doc[@for="RandomTest.NextBytes"]/*' />
      public virtual void NextBytes(byte [] buffer){
        if (buffer==null) throw new ArgumentNullException("buffer");
        for (int i=0; i<buffer.Length; i++) {
          buffer[i]=(byte)(Sample()*(Byte.MaxValue+1)); 
        }
      }
    
    }
}
