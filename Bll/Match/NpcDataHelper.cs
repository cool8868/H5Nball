using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Exceptions;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;

namespace Games.NBall.Bll.Match
{
    public class NpcDataHelper
    {
        private static readonly int PLAERCOUNT = SystemConstants.TeammemberCount;

        public static DTOBuffMemberView GetMemberView(DicNpcEntity npcEntity)
        {
            if (npcEntity.Type == 1 || npcEntity.Type == 2 || npcEntity.Type == 3 || npcEntity.Type == 4 || npcEntity.Type == 5 || npcEntity.Type == 6)//球星启示录计算真实kpi
            {
                npcEntity.KpiBuff = npcEntity.Buff;
                npcEntity.Buff = 100;// npcEntity.Buff;
            }
            else
            {
                npcEntity.KpiBuff = 100;
            }
            var data = new DTOBuffMemberView();
            //TODO: CombLevel

            var managerSBMList = new List<string>();
            //套装字典 套装id->数量
            var suitDic = new Dictionary<int, List<int>>();
            //套装id->套装类型
            var suitTypeDic = new Dictionary<int, int>();
            var formationDetails = CacheFactory.FormationCache.GetFormationDetail(npcEntity.FormationId);
            int i = 0;

            var buffPlayers = new Dictionary<Guid, DTOBuffPlayer>(PLAERCOUNT);
            buffPlayers.Add(Guid.NewGuid(), BuildPlayer(npcEntity, npcEntity.TP1, npcEntity.TE1, npcEntity.TS1, formationDetails[i++].Position, npcEntity.KpiBuff, ref suitDic, ref suitTypeDic));
            buffPlayers.Add(Guid.NewGuid(), BuildPlayer(npcEntity, npcEntity.TP2, npcEntity.TE2, npcEntity.TS2, formationDetails[i++].Position, npcEntity.KpiBuff, ref suitDic, ref suitTypeDic));
            buffPlayers.Add(Guid.NewGuid(), BuildPlayer(npcEntity, npcEntity.TP3, npcEntity.TE3, npcEntity.TS3, formationDetails[i++].Position, npcEntity.KpiBuff, ref suitDic, ref suitTypeDic));
            buffPlayers.Add(Guid.NewGuid(), BuildPlayer(npcEntity, npcEntity.TP4, npcEntity.TE4, npcEntity.TS4, formationDetails[i++].Position, npcEntity.KpiBuff, ref suitDic, ref suitTypeDic));
            buffPlayers.Add(Guid.NewGuid(), BuildPlayer(npcEntity, npcEntity.TP5, npcEntity.TE5, npcEntity.TS5, formationDetails[i++].Position, npcEntity.KpiBuff, ref suitDic, ref suitTypeDic));
            buffPlayers.Add(Guid.NewGuid(), BuildPlayer(npcEntity, npcEntity.TP6, npcEntity.TE6, npcEntity.TS6, formationDetails[i++].Position, npcEntity.KpiBuff, ref suitDic, ref suitTypeDic));
            buffPlayers.Add(Guid.NewGuid(), BuildPlayer(npcEntity, npcEntity.TP7, npcEntity.TE7, npcEntity.TS7, formationDetails[i++].Position, npcEntity.KpiBuff, ref suitDic, ref suitTypeDic));
           //套装
            TeammemberDataHelper.FillSuitData(suitDic, suitTypeDic, ref managerSBMList);
            //阵型加成
            TeammemberDataHelper.FillFormationData(npcEntity.FormationId, npcEntity.FormationLevel, ref managerSBMList);
            //TODO:球员组合
            BuffDataCore.Instance().FillBuffView(data, buffPlayers, true, 1, npcEntity.FormationId);
            data.BuffPlayers = buffPlayers;
            return data;
        }

        static DTOBuffPlayer BuildPlayer(DicNpcEntity npcEntity, int tp, int te, string ts, int position, int buffScale, ref Dictionary<int, List<int>> suitDic,
                                         ref Dictionary<int, int> suitTypeDic)
        {
            return BuildPlayer(npcEntity.TeammemberLevel, npcEntity.PropertyPoint, npcEntity.PlayerCardStrength, tp, te, ts,
                               position, buffScale, ref suitDic, ref suitTypeDic);
        }

        static DTOBuffPlayer BuildPlayer(int level, int propertyPoint, int strength, int playerId, int equipId, string skill, int position, int buffScale, ref Dictionary<int, List<int>> suitDic, ref Dictionary<int, int> suitTypeDic)
        {
            DicPlayerEntity cfg = MatchDataUtil.GetDicPlayer(Guid.Empty, playerId);
            var rawProps = cfg.GetRawProps();
            var obj = new DTOBuffPlayer();
            obj.Pid = cfg.Idx;
            obj.Pos = position;
            obj.Clr = cfg.CardLevel;
            obj.Props = new DTOBuffProp[rawProps.Length];

            for (int i = 0; i < rawProps.Length; ++i)
            {
                obj.Props[i] = new DTOBuffProp { Orig = rawProps[i] };
                obj.Props[i].Percent = (buffScale - 100) / 100.00;
            }
            if (propertyPoint > 0)
            {
                switch (position)
                {
                    case (int)EnumPosition.Forward:
                        obj.Props[0].Point += propertyPoint;
                        obj.Props[1].Point += propertyPoint;
                        obj.Props[2].Point += propertyPoint;
                        obj.Props[3].Point += propertyPoint;
                        break;
                    case (int)EnumPosition.Midfielder:
                        obj.Props[10].Point += propertyPoint;
                        obj.Props[11].Point += propertyPoint;
                        obj.Props[12].Point += propertyPoint;
                        obj.Props[3].Point += propertyPoint;
                        break;
                    case (int)EnumPosition.Fullback:
                        obj.Props[7].Point += propertyPoint;
                        obj.Props[8].Point += propertyPoint;
                        obj.Props[9].Point += propertyPoint;
                        break;
                    case (int)EnumPosition.Goalkeeper:
                        obj.Props[13].Point += propertyPoint;
                        obj.Props[14].Point += propertyPoint;
                        obj.Props[15].Point += propertyPoint;
                        break;
                }
            }
            rawProps = null;
            obj.Level = level;
            obj.Strength = strength;
            obj.SBMList = new List<string>();
            obj.ActionSkill = skill;
            //obj.StarSkill = CacheFactory.PlayersdicCache.GetStarSkill(obj.AsPid, obj.Strength);
            EquipmentUsedEntity equipment = null;
            if (equipId > 0)
            {
                equipment = new EquipmentUsedEntity();
                equipment.Property = CacheFactory.EquipmentCache.RandomEquipmentPropertyForNpc(equipId);
            }
            //装备和副卡
            //TeammemberDataHelper.FillEquipData(obj, equipment, null, null, ref suitDic, ref suitTypeDic);

            return obj;
        }
    }
}
