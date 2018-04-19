using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Match;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.NBall.Custom.Teammember;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Response;

namespace Games.NBall.Bll.Frame
{
    public static class BuffUtil
    {
        public static double[] GetRawProps(this DicPlayerEntity src)
        {
            return new double[]
            {
                src.Speed, src.Shoot, src.FreeKick,
                src.Balance, src.Physique, src.Power,
                src.Aggression, src.Disturb, src.Interception,
                src.Dribble, src.Pass, src.Mentality,
                src.Response, src.Positioning, src.HandControl,
                src.Acceleration, src.Bounce
            };
        }

        public static double[] GetTeammemberProps(TeammemberEntity src)
        {
            return new double[]
            {
                src.Speed, src.Shoot, src.FreeKick,
                src.Balance, src.Physique, src.Power,
                src.Aggression, src.Disturb, src.Interception,
                src.Dribble, src.Pass, src.Mentality,
                src.Response, src.Positioning, src.HandControl,
                src.Acceleration, src.Bounce
            };
        }

        public static List<string>[] GetManagerSkillList(Guid managerId, ManagerSkillUseWrap use, string siteId = "")
        {
            var lstAll = new List<string>();
            var lib = ManagerUtil.GetSkillLibWrap(managerId, siteId);
            FixTalents(use, lib, siteId);
            lstAll.AddRange(lib.LowWills.Keys);
            lstAll.AddRange(use.SetWills.Keys);
            lstAll.AddRange(lib.NodoTalents.Keys);
            if (!string.IsNullOrEmpty(use.Raw.CoachSkill))
                lstAll.Add(use.Raw.CoachSkill);
            //var combs = ManagerSkillCache.Instance().CheckCombs(use.SetWills.Keys, use.OnPids);
            //var combs = ManagerSkillCache.Instance().CheckCombs(use.OnPids);
            //if (null != combs)
            //    lstAll.AddRange(combs);
            var clubSkills = ManagerSkillCache.Instance().CheckClubSkills(use.OnPids);
            if (null != clubSkills)
                lstAll.AddRange(clubSkills);
            var rankSkills = BuffCache.Instance().GetRankedSkillList(lstAll);
            //if (null != combs)
            //    rankSkills[1].AddRange(combs);
            var skills = use.ManagerSkills;
            skills[0] = rankSkills[0];
            skills[1] = rankSkills[1];
            skills[2] = rankSkills[2];
            return skills;
        }
        static void FixTalents(ManagerSkillUseWrap use, ManagerSkillLibWrap lib, string siteId)
        {
            bool needFix = false;
            foreach (var item in lib.NodoTalents)
            {
                if (!item.Key.Contains('_'))
                {
                    needFix = true;
                    break;
                }
            }
            if (!needFix)
            {
                foreach (var item in lib.TodoTalents)
                {
                    if (!item.Key.Contains('_'))
                    {
                        needFix = true;
                        break;
                    }
                }
            }
            if (needFix)
            {
                var skills = NbManagertreeMgr.GetByManagerId(use.Raw.ManagerId, siteId);
                foreach (var item in skills)
                {
                    if (lib.NodoTalents.ContainsKey(item.SkillCode))
                    {
                        lib.NodoTalents.Remove(item.SkillCode);
                        lib.NodoTalents[item.SkillCode + "_" + item.Points] = 0;
                    }
                    if (lib.TodoTalents.ContainsKey(item.SkillCode))
                    {
                        lib.TodoTalents.Remove(item.SkillCode);
                        lib.TodoTalents[item.SkillCode + "_" + item.Points] = 0;
                    }
                }
            }
            for (int i = 0; i < use.SetTalents.Length; i++)
            {
                if (string.IsNullOrEmpty(use.SetTalents[i]))
                    continue;
                foreach (var todoSkill in lib.TodoTalents)
                {
                    if (todoSkill.Key.StartsWith(use.SetTalents[i], true, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        use.SetTalents[i] = todoSkill.Key;
                        break;
                    }
                }
            }
        }
        public static void FillLiveSkillList(ManagerSkillUseWrap use, List<string> liveSkills)
        {
            if (null == use || null == liveSkills || liveSkills.Count == 0)
                return;
            var skills = use.ManagerSkills;
            skills[1].AddRange(liveSkills);
        }

        public static List<string> GetPlayerSkillList(out List<ConfigBuffengineEntity> flows, Guid managerId,
            Guid memberId)
        {
            //TODO
            flows = null;
            return null;
        }

        public static List<TeammemberEntity> GetRawMembers(Guid managerId, bool homeFlag, string siteId = "")
        {
            bool syncFlag = homeFlag || string.IsNullOrEmpty(siteId);
            return MatchDataHelper.GetRawMembers(managerId, syncFlag, siteId);
        }

        public static int[] GetFormPids(Guid managerId)
        {
            var form = GetSolution(managerId);
            return GetFormPids(form);
        }

        public static int[] GetFormPids(NbSolutionEntity form)
        {
            string onStr = null == form ? string.Empty : form.PlayerString;
            return FrameUtil.CastIntArray(onStr, ',');
        }

        public static NbSolutionEntity GetSolution(Guid managerId, string siteId = "")
        {
            return MatchDataHelper.GetSolution(managerId, siteId);
        }
        public static int GetTalentType(Guid managerId, string siteId = "")
        {
            var extra = NbManagerextraMgr.GetById(managerId, siteId);
            if (null == extra)
                return 0;
            return extra.SkillType;
        }
        #region Kpi

        public static double AsKpi(this NbManagerbuffmemberEntity buffMember, int index, int formationId)
        {
            return AsKpiNew(buffMember, index, formationId);
            //double kpi = 0;
            //double memberKpi = 0;
            //switch (buffMember.PPos)
            //{
            //    case (int)EnumPosition.Goalkeeper:
            //        kpi = buffMember.TotalResponse + buffMember.TotalPositioning + buffMember.TotalHandControl;
            //        break;
            //    case (int)EnumPosition.Fullback:
            //        kpi = (buffMember.TotalBalance + buffMember.TotalPhysique + buffMember.TotalPower) * 0.4
            //            + (buffMember.TotalAggression + buffMember.TotalDisturb + buffMember.TotalInterception) * 0.6;
            //        break;
            //    case (int)EnumPosition.Midfielder:
            //        kpi = (buffMember.TotalDribble + buffMember.TotalPass + buffMember.TotalMentality) * 0.6
            //            + (buffMember.TotalSpeed + buffMember.TotalShoot + buffMember.TotalFreeKick) * 0.2
            //            + (buffMember.TotalBalance + buffMember.TotalPhysique + buffMember.TotalPower) * 0.2;
            //        break;
            //    case (int)EnumPosition.Forward:
            //        kpi = (buffMember.TotalSpeed + buffMember.TotalShoot + buffMember.TotalFreeKick) * 0.6
            //            + (buffMember.TotalBalance + buffMember.TotalPhysique + buffMember.TotalPower) * 0.2
            //            + (buffMember.TotalDribble + buffMember.TotalPass + buffMember.TotalMentality) * 0.2;
            //        break;
            //}
            ////var pPos = buffMember.IsMain ? buffMember.PPosOn : buffMember.PPos;
            //switch (buffMember.PPos)
            //{
            //    case (int)EnumPosition.Goalkeeper:
            //        memberKpi = (buffMember.TotalResponse + buffMember.TotalHandControl + buffMember.TotalPositioning) / 3;
            //        break;
            //    case (int)EnumPosition.Fullback:
            //        memberKpi = (buffMember.TotalAggression + buffMember.TotalDisturb + buffMember.TotalInterception) / 3;
            //        break;
            //    case (int)EnumPosition.Midfielder:
            //        memberKpi = (buffMember.TotalBalance + buffMember.TotalDribble + buffMember.TotalPass +
            //                  buffMember.TotalMentality) / 4;
            //        break;
            //    case (int)EnumPosition.Forward:
            //        memberKpi = (buffMember.TotalSpeed + buffMember.TotalShoot + buffMember.TotalDribble +
            //                  buffMember.TotalMentality) / 4;
            //        break;
            //}
            //buffMember.Kpi = Convert.ToInt32(memberKpi);
            //return buffMember.IsMain ? kpi : 0;
        }

        #endregion

        #region KpiNew

        public static double AsKpiNew(NbManagerbuffmemberEntity buffMember, int index, int formationId)
        {
            double kpi = 0;
            double memberKpi = 0;
            string playerPosition = "";

            var player = CacheFactory.PlayersdicCache.GetPlayer(buffMember.Pid);
            if (player == null)
                return 0;
            //计算位置适应
            if (formationId > 0)
            {
                try
                {
                    var formationList = CacheFactory.FormationCache.GetFormationDetail(formationId);
                    if (formationList != null && formationList.Count > index)
                    {
                        var formation = formationList[index];
                        playerPosition = formation.SpecificPointDesc.Trim();
                        if (player.PositionDesc.Length > 0)
                        {
                            if (player.AllPosition.IndexOf(playerPosition) < 0) //位置不适应
                            {
                                CheckBuff(buffMember, playerPosition.Trim(), player.PositionDesc.Trim());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("KPI", ex);
                }
            }
            switch (playerPosition)
            {
                case "GK":
                    kpi = buffMember.TotalResponse + buffMember.TotalPositioning + buffMember.TotalHandControl;
                    break;
                case "CB":
                case "LB":
                case "RB":
                    kpi = (buffMember.TotalBalance + buffMember.TotalPhysique + buffMember.TotalPower)*0.4
                          +
                          (buffMember.TotalAggression + buffMember.TotalDisturb + buffMember.TotalInterception)*0.6;
                    break;
                case "LM":
                case "RM":
                case "CDM":
                case "CAM":
                    kpi = (buffMember.TotalDribble + buffMember.TotalPass + buffMember.TotalMentality)*0.6
                          + (buffMember.TotalSpeed + buffMember.TotalShoot + buffMember.TotalFreeKick)*0.2
                          + (buffMember.TotalBalance + buffMember.TotalPhysique + buffMember.TotalPower)*0.2;
                    break;
                case "CF":
                    kpi = (buffMember.TotalSpeed + buffMember.TotalShoot + buffMember.TotalFreeKick)*0.6
                          + (buffMember.TotalBalance + buffMember.TotalPhysique + buffMember.TotalPower)*0.2
                          + (buffMember.TotalDribble + buffMember.TotalPass + buffMember.TotalMentality)*0.2;
                    break;
            }

            switch (player.PositionDesc)
            {
                case "CF":
                    memberKpi = (buffMember.TotalSpeed + buffMember.TotalShoot + buffMember.TotalBalance +
                                 buffMember.TotalDribble + buffMember.TotalMentality)/5;
                    break;
                case "CDM": //抢断，控球，传球，力量，侵略性
                    memberKpi = (buffMember.TotalInterception + buffMember.TotalDribble + buffMember.TotalPass +
                                 buffMember.TotalPower + buffMember.TotalAggression)/5;
                    break;
                case "CAM":
                    memberKpi = (buffMember.TotalShoot + buffMember.TotalBalance + buffMember.TotalDribble +
                                 buffMember.TotalPass)/4;
                    break;
                case "LM":
                case "RM":
                    memberKpi = (buffMember.TotalSpeed + buffMember.TotalBalance + buffMember.TotalDribble +
                                 buffMember.TotalPass + buffMember.TotalMentality)/5;
                    break;
                case "CB":
                    memberKpi = (buffMember.TotalPhysique + buffMember.TotalPower + buffMember.TotalAggression +
                                 buffMember.TotalDisturb + buffMember.TotalInterception)/5;
                    break;
                case "LB":
                case "RB": //控制，传球，干扰，抢断，侵略性
                    memberKpi = (buffMember.TotalBalance + buffMember.TotalPass + buffMember.TotalDisturb +
                                 buffMember.TotalInterception + buffMember.TotalAggression)/5;
                    break;
                case "GK":
                    memberKpi = (buffMember.TotalHandControl + buffMember.TotalPositioning +
                                 buffMember.TotalResponse)/3;
                    break;
            }

            buffMember.Kpi = Convert.ToInt32(memberKpi);
            return buffMember.IsMain ? kpi : 0;
        }

        /// <summary>
        /// 位置不适应 -BUFF
        /// </summary>
        /// <param name="buffMember">Buff列表</param>
        /// <param name="ballParkPoint">场上位置</param>
        /// <param name="playerposition">球员最佳位置</param>
        private static void CheckBuff(NbManagerbuffmemberEntity buffMember, string ballParkPoint, string playerposition)
        {
            try
            {
                var config = CacheFactory.FormationCache.GetFormationPoint(playerposition, ballParkPoint);
                if (config == null)
                    return;
                buffMember.AccelerationConst = buffMember.AccelerationConst*config.Buff/100;
                buffMember.AggressionConst = buffMember.AggressionConst*config.Buff/100;
                buffMember.BalanceConst = buffMember.BalanceConst*config.Buff/100;
                buffMember.BounceConst = buffMember.BounceConst*config.Buff/100;
                buffMember.DisturbConst = buffMember.DisturbConst*config.Buff/100;
                buffMember.DribbleConst = buffMember.DribbleConst*config.Buff/100;
                buffMember.FreeKickConst = buffMember.FreeKickConst*config.Buff/100;
                buffMember.HandControlConst = buffMember.HandControlConst*config.Buff/100;
                buffMember.InterceptionConst = buffMember.InterceptionConst*config.Buff/100;
                buffMember.MentalityConst = buffMember.MentalityConst*config.Buff/100;
                buffMember.PassConst = buffMember.PassConst*config.Buff/100;
                buffMember.PhysiqueConst = buffMember.PhysiqueConst*config.Buff/100;
                buffMember.PositioningConst = buffMember.PositioningConst*config.Buff/100;
                buffMember.PowerConst = buffMember.PowerConst*config.Buff/100;
                buffMember.ResponseConst = buffMember.ResponseConst*config.Buff/100;
                buffMember.ShootConst = buffMember.ShootConst*config.Buff/100;
                buffMember.SpeedConst = buffMember.SpeedConst*config.Buff/100;

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("位置不适应 -BUFF", ex.ToString(), ballParkPoint + "__" + playerposition);
            }

        }

        #endregion

        #region 竞技场


        public static NbSolutionEntity GetSolutionArena(ArenaTeammemberFrame arenaFrame)
        {
            return MatchDataHelper.GetArenaSolution(arenaFrame);
        }
        public static List<TeammemberEntity> GetRawMembers(Guid managerId, bool homeFlag, ArenaTeammemberFrame arenaFrame, string siteId = "")
        {
            bool syncFlag = homeFlag || string.IsNullOrEmpty(siteId);
            return MatchDataHelper.GetRawMembersArena(managerId, arenaFrame, syncFlag, siteId);
        }


        #endregion
    }
}
