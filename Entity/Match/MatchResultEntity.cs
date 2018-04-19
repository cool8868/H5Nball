using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Entity.Match
{
    public class MatchResultEntity
    {
        public MatchResultEntity()
        {}
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchResultEntity"/> class. 
        /// </summary>
        /// <param name="homeScore">The home score.</param>
        /// <param name="awayScore">The away score.</param>
        /// <param name="process">The process.</param>
        /// <param name="errorCode">The error code.</param>
        public MatchResultEntity(int homeScore,int awayScore,byte[] process,MessageCode errorCode)
        {
            this.AwayScore = awayScore;
            this.HomeScore = homeScore;
            this.Process = process;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchResultEntity"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public MatchResultEntity(MessageCode errorCode)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets or sets the away score.
        /// </summary>
        /// <value>The away score.</value>
        public int AwayScore
        { get; set; }

        /// <summary>
        /// Gets or sets the home score.
        /// </summary>
        /// <value>The home score.</value>
        public int HomeScore
        { get; set; }

        /// <summary>
        /// Gets or sets the process.
        /// </summary>
        /// <value>The process.</value>
        public byte[] Process
        { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public MessageCode ErrorCode
        { get; set; }
    }
}
