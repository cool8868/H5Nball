using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Exceptions;
using Games.NBall.Entity.NBall.Custom.Teammember;

namespace Games.NBall.Bll.Match
{
    public class MatchDataUtil
    {
        public static DicPlayerEntity GetDicPlayer(Guid teammemberId, int playerId)
        {
            DicPlayerEntity dicPlayer = CacheFactory.PlayersdicCache.GetPlayer(playerId);
            if (null == dicPlayer)
            {
                throw new TransferException(string.Format("can't find player,teammemberId:{0},playerId:{1}", teammemberId, playerId));
            }
            return dicPlayer;
        }

        public static SolutionPlayerEntity BuildSolutionPlayer(List<int> playerIds, Dictionary<int, DicFormationdetailEntity> formationdetails, int playerId, string skillCode)
        {
            SolutionPlayerEntity entity = new SolutionPlayerEntity();
            entity.SkillCode = skillCode;
            entity.Position = GetPosition(playerIds, formationdetails, playerId);
            return entity;
        }

        public static int GetPosition(List<int> playerIds, Dictionary<int, DicFormationdetailEntity> formationdetails, int playerId)
        {
            var index = playerIds.FindIndex(0, d => d == playerId);
            if (formationdetails.ContainsKey(index))
                return formationdetails[index].Position;//Scan 修改场上位置  原来是 .Point
            else
            {
                return -1;
            }
        }
    }
}
