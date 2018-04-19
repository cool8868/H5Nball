using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Entity;

namespace Games.NBall.AdminWeb
{
    #region AdminMatchStatEntity
    public class AdminMatchStatEntity:NbMatchstatEntity
    {
        public AdminMatchStatEntity(NbMatchstatEntity entity)
        {
            this.Idx = entity.Idx;
            this.ManagerId = entity.ManagerId;
            this.MatchType = entity.MatchType;
            this.Win = entity.Win;
            this.Lose = entity.Lose;
            this.Draw = entity.Draw;
            this.UpdateTime = entity.UpdateTime;
        }
        public string MatchTypeV { get; set; }

        public string WinRateV { get { return string.Format("{0:f2}", WinRate*100); } }
    }
    #endregion

    #region dd
    public class AdminManagerhonorEntity : NbManagerhonorEntity
    {
        public AdminManagerhonorEntity(NbManagerhonorEntity entity)
        {
            this.Idx = entity.Idx;
            this.ManagerId = entity.ManagerId;
            this.MatchType = entity.MatchType;
            this.SubType = entity.SubType;
            this.PeriodId = entity.PeriodId;
            this.Rank = entity.Rank;
            this.Rowtime = entity.Rowtime;

        }
        public string MatchTypeV { get; set; }
    }
    #endregion
}