using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;

namespace Games.NBall.Bll.Match
{
    public class TeammemberPropertyBuff
    {
        public TeammemberPropertyBuff(DicPlayerEntity player, int strength)
            : this(player, strength, 100)
        {
        }

        public TeammemberPropertyBuff(DicPlayerEntity player, int strength, int buff)
        {
            this.PlayerId = player.Idx;
            this.Original = new double[]
                {
                    player.Speed                    ,player.Shoot                    ,player.FreeKick                    ,player.Balance                    ,player.Physique                    ,player.Power                    ,player.Aggression                    ,player.Disturb                    ,player.Interception                    ,player.Dribble                    ,player.Pass                    ,player.Mentality                    ,player.Response                    ,player.Positioning                    ,player.HandControl                    ,player.Acceleration                    ,player.Bounce
                };

            this.Percent = new double[this.Original.Length];
            this.Point = new double[this.Original.Length];
            this.Trains = new double[this.Original.Length];
            if (strength > 0)
                Strength = 1 + (strength - 1) * 0.35;
            else
            {
                Strength = 1;
            }

            if (buff > 0)
            {
                BuffScale = buff / 100.00;
            }
            else
            {
                BuffScale = 1;
            }
        }

        /// <summary>
        /// 血战削弱球员能力
        /// </summary>
        public double BuffScale { get; set; }

        public int PlayerId { get; set; }

        /// <summary>
        /// 基础值
        /// </summary>
        public double[] Original { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        public double[] Percent { get; set; }

        /// <summary>
        /// 绝对值
        /// </summary>
        public double[] Point { get; set; }

        /// <summary>
        /// 培养值
        /// </summary>
        public double[] Trains { get; set; }

        /// <summary>
        /// 强化等级
        /// </summary>
        private double Strength { get; set; }

        public double this[int i]
        {
            get
            {
                if (i > 16 || i < 1)
                    return 0;
                i = i - 1;
                return (Original[i] * Percent[i] + Point[i] + (Original[i] + Trains[i]) * Strength) * BuffScale;
            }
        }
    }
}
