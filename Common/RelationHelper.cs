using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NBall.Common;


namespace Games.NBall.Common
{
    public class RelationHelper
    {
        /// <summary>
        /// 格式0,0~50,100&0,0~50,100
        /// 开始时间,结束时间~最小概率,最大概率&开始时间,结束时间~最小概率,最大概率
        /// </summary>
        /// <param name="strConfig"></param>
        /// <param name="lotteryBaseTime"></param>
        /// <returns></returns>
        public static List<RandomSingleInTime> BuildSingleRandomInTimeList(string strConfig, DateTime lotteryBaseTime)
        {
            List<RandomSingleInTime> randomSingleInTimes = new List<RandomSingleInTime>();
            string[] commandValues = strConfig.Split('&');
            foreach (var commandValue in commandValues)
            {
                var randomSingleInTime = BuildSingleRandomInTime(commandValue, lotteryBaseTime);
                randomSingleInTimes.Add(randomSingleInTime);
            }
            commandValues = null;
            return randomSingleInTimes;
        }

        /// <summary>
        /// 格式0,0~50,100
        /// 开始时间,结束时间~最小概率,最大概率
        /// </summary>
        /// <param name="strConfig"></param>
        /// <param name="lotteryBaseTime"></param>
        /// <returns></returns>
        public static RandomSingleInTime BuildSingleRandomInTime(string strConfig, DateTime lotteryBaseTime)
        {
            RandomSingleInTime randomSingleInTime = new RandomSingleInTime();
            string[] rateInTimes = strConfig.Split('~');
            string[] times = rateInTimes[0].Split(',');
            string[] rates = rateInTimes[1].Split(',');

            randomSingleInTime.StartTime = lotteryBaseTime.AddMinutes(Convert.ToInt32(times[0]));
            randomSingleInTime.EndTime = lotteryBaseTime.AddMinutes(Convert.ToInt32(times[1]));

            randomSingleInTime.Begin = Convert.ToInt32(rates[0]);
            randomSingleInTime.End = Convert.ToInt32(rates[1]);
            
            rateInTimes = null;
            times = null;
            rates = null;
            return randomSingleInTime;
        }


        /// <summary>
        /// 概率配置格式 1,100|2,50
        /// </summary>
        /// <param name="strConfig"></param>
        /// <returns></returns>
        public static RandomRelation BuildRelation(string strConfig)
        {
            if (string.IsNullOrEmpty(strConfig))
                return null;
            string[] rates = strConfig.Split('|');
            List<RandomRate> rateList = new List<RandomRate>();
            int maxSeed = 0;
            foreach (string s in rates)
            {
                rateList.Add(BuildRate(ref maxSeed, s));
            }
            rates = null;
            return new RandomRelation(maxSeed + 1, rateList);
        }

        /// <summary>
        /// 概率配置格式 1,100|2,50
        /// </summary>
        /// <param name="strConfig"></param>
        /// <returns></returns>
        public static RandomRelation BuildRelationString(string strConfig)
        {
            if (string.IsNullOrEmpty(strConfig))
                return null;
            string[] rates = strConfig.Split('|');
            List<RandomRate> rateList = new List<RandomRate>();
            int maxSeed = 0;
            foreach (string s in rates)
            {
                rateList.Add(BuildRateString(ref maxSeed, s));
            }
            rates = null;
            return new RandomRelation(maxSeed + 1, rateList);
        }

        public static RandomRate BuildRate(ref int maxSeed, string strRate)
        {
            string[] rateInfo = strRate.Split(',');

            int linkId = ConvertHelper.ConvertToInt(rateInfo[0]);
            int rateBegin = maxSeed + 1;
            maxSeed = maxSeed + Convert.ToInt32(rateInfo[1]);

            return new RandomRate(linkId, rateBegin, maxSeed);
        }

        public static RandomRate BuildRateString(ref int maxSeed, string strRate)
        {
            string[] rateInfo = strRate.Split(',');

            string linkString = rateInfo[0];
            int rateBegin = maxSeed + 1;
            maxSeed = maxSeed + Convert.ToInt32(rateInfo[1]);

            return new RandomRate(linkString, rateBegin, maxSeed);
        }
    }
}
