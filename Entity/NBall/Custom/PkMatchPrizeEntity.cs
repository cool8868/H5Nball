using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.NBall.Custom
{
    public class PkMatchPrizeEntity
    {
        public PkMatchPrizeEntity()
        {

        }

        public PkMatchPrizeEntity(string prizes)
        {
            var ss = prizes.Split(',');
            Exp = Convert.ToInt32(ss[1]);
            Coin = Convert.ToInt32(ss[0]);
        }

        public int Exp { get; set; }

        public int Coin { get; set; }

        public PkMatchPrizeEntity Clone()
        {
            var entity = new PkMatchPrizeEntity();
            entity.Coin = this.Coin;
            entity.Exp = this.Exp;
            return entity;
        }
    }

    public class PKChallengeTimesEntity
    {
        public PKChallengeTimesEntity()
        {

        }

        public PKChallengeTimesEntity(string prizes)
        {
            var ss = prizes.Split(':');
            var s = ss[0].Split(',');
            SLevel = Convert.ToInt32(s[0]);
            ELevel = Convert.ToInt32(s[1]);
            Number = Convert.ToInt32(ss[1]);
        }

        public int SLevel { get; set; }

        public int ELevel { get; set; }

        public int Number { get; set; }

        public PKChallengeTimesEntity Clone()
        {
            var entity = new PKChallengeTimesEntity();
            entity.SLevel = this.SLevel;
            entity.ELevel = this.ELevel;
            entity.Number = this.Number;
            return entity;
        }
    }
}
