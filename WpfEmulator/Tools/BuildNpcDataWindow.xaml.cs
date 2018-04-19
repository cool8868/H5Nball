using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Helpers;

namespace Games.NBall.WpfEmulator.Tools
{
    /// <summary>
    /// BuildNpcDataWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BuildNpcDataWindow : Window
    {
        public BuildNpcDataWindow()
        {
            InitializeComponent();
        }

        #region 全局变量

        /// <summary>
        /// 阵型
        /// </summary>
        List<DicFormationEntity> _formationList;
        /// <summary>
        /// 球员
        /// </summary>
        private List<DicPlayerEntity> _playerList;
        /// <summary>
        /// 装备
        /// </summary>
        private List<DicEquipmentEntity> _equipmentList;
        ///// <summary>
        ///// 技能
        ///// </summary>
        //private List<DicSkillcardEntity> _skillList;
        /// <summary>
        /// 球员意志
        /// </summary>
        private Dictionary<string, List<int>> _willPlayerDic;
        /// <summary>
        /// 意志
        /// </summary>
        private Dictionary<string, int> _willStrengthDic;

        /// <summary>
        /// 球员字典  -位置
        /// </summary>
        private Dictionary<string, List<int>> _playerDic;

        private Dictionary<int,List<int>> _suitDic;
        private Dictionary<int, List<int>> _positionPropertyDic;
        private Dictionary<int, List<int>> _positionSkillDic;
        private Dictionary<int, DicFormationdetailEntity> _formationdetail;

        #endregion

        private string _connection;
        private bool _init = false;
        void BuildCache()
        {
            if(_init)
                return;

            _connection = ConfigurationManager.ConnectionStrings["Games.NBall.Dal.Properties.Settings.NB_ConfigConnectionString"].ConnectionString;
            _formationList = CacheFactory.FormationCache.GetFormationList();
            _playerList = DicPlayerMgr.GetAllForCache();
            _equipmentList = DicEquipmentMgr.GetAllForCache();
            var suitList = DicEquipmentsuitMgr.GetAllForCache();
           // _skillList = DicSkillcardMgr.GetAll();

            var willList = DicManagerwillMgr.GetAll();
            _willPlayerDic=new Dictionary<string, List<int>>();
            _willStrengthDic=new Dictionary<string, int>();
            foreach (var entity in willList)
            {
                if (entity.DriveFlag == (int) EnumSkillDriveType.Active)
                {
                    var ss = entity.PartMap.TrimEnd(',').Split(',');
                    _willStrengthDic.Add(entity.SkillCode,0);
                    _willPlayerDic.Add(entity.SkillCode,new List<int>());
                    foreach (var s in ss)
                    {
                        var pp = s.Split('+');
                        var pid = Convert.ToInt32(pp[0]);
                        var stren = Convert.ToInt32(pp[1]);
                        if (_willStrengthDic[entity.SkillCode] < stren)
                            _willStrengthDic[entity.SkillCode] = stren;
                        _willPlayerDic[entity.SkillCode].Add(pid);
                    }
                }
            }

            _suitDic = new Dictionary<int, List<int>>();
            foreach (var entity in suitList)
            {
                if(!_suitDic.ContainsKey(entity.SuitType))
                    _suitDic.Add(entity.SuitType,new List<int>());
                _suitDic[entity.SuitType].Add(entity.Idx);
            }
            //_positionPropertyDic = new Dictionary<int, List<int>>();
            //_positionPropertyDic.Add((int)EnumPosition.Forward,new List<int>(){(int)EnumProperty.Speed,(int)EnumProperty.Shoot,(int)EnumProperty.FreeKick});
            //_positionPropertyDic.Add((int)EnumPosition.Midfielder, new List<int>() { (int)EnumProperty.Dribble, (int)EnumProperty.Pass, (int)EnumProperty.Mentality });
            //_positionPropertyDic.Add((int)EnumPosition.Fullback, new List<int>() { (int)EnumProperty.Aggression, (int)EnumProperty.Disturb, (int)EnumProperty.Interception });
            //_positionPropertyDic.Add((int)EnumPosition.Goalkeeper, new List<int>() { (int)EnumProperty.HandControl, (int)EnumProperty.Response, (int)EnumProperty.Positioning });

            _positionSkillDic = new Dictionary<int, List<int>>();
            _positionSkillDic.Add((int)EnumPosition.Forward, new List<int>() { (int)EnumSKillActType.Shoot });
            _positionSkillDic.Add((int)EnumPosition.Midfielder, new List<int>() { (int)EnumSKillActType.Through, (int)EnumSKillActType.Organize});
            _positionSkillDic.Add((int)EnumPosition.Fullback, new List<int>() { (int)EnumSKillActType.Defence});
            _positionSkillDic.Add((int)EnumPosition.Goalkeeper, new List<int>() { (int)EnumSKillActType.GoalKeep});
            
            _init = true;


            var allPlayer = DicPlayerMgr.GetAllForCache();
            _playerDic = new Dictionary<string, List<int>>();
            foreach (var item in allPlayer)
            {
                if (item.CardLevel == 6 || item.CardLevel == 5)
                    continue;
                if (!_playerDic.ContainsKey(item.PositionDesc))
                    _playerDic.Add(item.PositionDesc, new List<int>());
                _playerDic[item.PositionDesc].Add(item.Idx);
            }
        }

        private int m_totalCount;
        private int m_curCount;
        private int m_npcCount=2;
        void ReBuildArenaNpc()
        {
            //var templates = TemplateWorldchallengeMgr.GetAll();
            //m_totalCount = templates.Count * m_npcCount;
            ProgressBar1.Maximum = 300;
            ProgressBar1.Value = -1;
            lblProcess.Content = "初始化缓存...";
            doRebuildArenaNpc(CreateCallback);

            
        }

        private Thread _thread;

        public delegate void CreateDelegate();
        void CreateCallback()
        {
            ProgressBar1.Value =m_curCount;
            lblProcess.Content = string.Format("进度：{0}/{1}", m_curCount, m_totalCount);
        }

        //private TemplateWorldchallengeEntity m_template;


        private int m_normalEquipCount;
        private int m_skillCount;
        private Dictionary<int, int> m_playerDic;
        private Dictionary<int, List<int>> m_equipmentDic;

        //void BuildNpc()
        //{
        //    DicNpcEntity entity = BuildBasicData();
        //    BuildNpcData(entity);
        //    DicNpcMgr.Insert(entity);
        //    ConfigWorldchallengenpclinkMgr.Insert(new ConfigWorldchallengenpclinkEntity(0, m_template.Idx, entity.Idx));
        //}

        //DicNpcEntity BuildBasicData()
        //{
        //    var formationId = _formationList[RandomHelper.GetInt32WithoutMax(0, _formationList.Count)].Idx;
        //    _formationdetail = CacheFactory.FormationCache.GetFormationDetail(formationId);
        //    DicNpcEntity entity = new DicNpcEntity();
        //    entity.Idx = ShareUtil.GenerateComb();
        //    entity.Type = 3;
        //    entity.Name = m_template.Name;
        //    entity.Logo = 1;
        //    entity.FormationId = formationId;
        //    entity.FormationLevel = m_template.FormationLevel;
        //    entity.TeammemberLevel = m_template.TeammemberLevel;
        //    entity.PlayerCardStrength = m_template.Strength;
        //    entity.CoachId = 0;
        //    entity.DoTalent = GetDoTalent();//主动天赋
        //    entity.ManagerSkill = GetNodoManagerSkill();//被动天赋和意志
        //    int comb = 0;
        //    entity.DoWill = GetDoWill(ref comb);//主动意志
        //    entity.CombLevel = m_template.CombLevel;
        //    entity.Buff = m_template.Buff;
        //    entity.PropertyPoint = m_template.TeammemberLevel / 3;

        //    m_playerDic = new Dictionary<int, int>();
        //    int suitType = m_template.SuitType;
        //    m_normalEquipCount = m_template.EquipCount;
        //    m_skillCount = m_template.SkillCount;
        //    m_skillDic = new Dictionary<string, int>();

        //    m_equipmentDic = new Dictionary<int, List<int>>();
        //    if (suitType > 0 && suitType < 4)
        //    {
        //        var suitId = _suitDic[suitType][RandomHelper.GetInt32WithoutMax(0, _suitDic[suitType].Count)];
        //        var equipmentList = _equipmentList.FindAll(d => d.SuitId == suitId && d.Quality == m_template.EquipQuality);
        //        foreach (var p in equipmentList)
        //        {
        //            m_equipmentDic.Add(p.Idx, new List<int>() { p.PropertyType1, p.PropertyType2 });
        //        }
        //        m_normalEquipCount -= m_equipmentDic.Count;
        //    }
        //    return entity;
        //}

      //  #region 创建NPC数据

        //void BuildNpcData(DicNpcEntity entity)
        //{
        //    entity.TP1 = GetPlayer(1);
        //    entity.TE1 = GetEquipment(1);
        //    entity.TS1 = GetSkill(1);

        //    entity.TP2 = GetPlayer(2);
        //    entity.TE2 = GetEquipment(2);
        //    entity.TS2 = GetSkill(2);
        //    entity.TP3 = GetPlayer(3);
        //    entity.TE3 = GetEquipment(3);
        //    entity.TS3 = GetSkill(3);
        //    entity.TP4 = GetPlayer(4);
        //    entity.TE4 = GetEquipment(4);
        //    entity.TS4 = GetSkill(4);
        //    entity.TP5 = GetPlayer(5);
        //    entity.TE5 = GetEquipment(5);
        //    entity.TS5 = GetSkill(5);
        //    entity.TP6 = GetPlayer(6);
        //    entity.TE6 = GetEquipment(6);
        //    entity.TS6 = GetSkill(6);
        //    entity.TP7 = GetPlayer(7);
        //    entity.TE7 = GetEquipment(7);
        //    entity.TS7 = GetSkill(7);
        //}

        //#endregion

        //#region 球员

        //int GetPlayer(int index)
        //{
        //    int position = _formationdetail[index - 1].Position;
        //    var players =
        //        _playerList.FindAll(
        //            d => d.Kpi >= m_template.MinKpi && d.Kpi <= m_template.MaxKpi && d.Position == position && !m_playerDic.ContainsKey(d.Idx));
        //    var player = players[RandomHelper.GetInt32WithoutMax(0, players.Count)];
        //    m_playerDic.Add(player.Idx, 1);
        //    return player.Idx;
        //}

        //#endregion

        //#region 装备

        //int GetEquipment(int index)
        //{
        //    int position = GetPosition(index);
        //    var propertys = _positionPropertyDic[position];
        //    int equipId = 0;
        //    foreach (var equip in m_equipmentDic)
        //    {
        //        foreach (var p in equip.Value)
        //        {
        //            if (propertys.Contains(p))
        //            {
        //                equipId = equip.Key;
        //            }
        //        }
        //    }
        //    if (equipId > 0)
        //        m_equipmentDic.Remove(equipId);
        //    else
        //    {
        //        //if (m_normalEquipCount > 0)
        //        //{
        //        //    var equipments = _equipmentList.FindAll(d =>d.Quality==m_template.EquipQuality && d.SuitType == (int)EnumSuitType.Normal && propertys.Contains(d.PropertyType1));
        //        //    if (equipments.Count>0)
        //        //    {
        //        //        equipId = equipments[RandomHelper.GetInt32WithoutMax(0,equipments.Count)].Idx;
        //        //    }
        //        //    m_normalEquipCount--;
        //        //}
        //    }
        //    return equipId;
        //}

        //#endregion

        //#region 技能

        //private Dictionary<string, int> m_skillDic; 
        //string GetSkill(int index)
        //{
        //    if (m_skillCount > 0)
        //    {
        //        //int position = GetPosition(index);
        //        //int act = GetRandom(_positionSkillDic[position]);
        //        //var skills =
        //        //    _skillList.FindAll(
        //        //        d =>
        //        //        d.ActType == act && d.SkillClass >= m_template.MinSkillClass &&
        //        //        d.SkillClass <= m_template.MaxSkillClass && d.SkillLevel == m_template.SkillLevel && !m_skillDic.ContainsKey(d.SkillCode));
        //        //if (skills.Count > 0)
        //        //{
        //        //    var skill = GetRandom(skills);
        //        //    if (skill != null)
        //        //    {
        //        //        m_skillDic.Add(skill.SkillCode, 0);
        //        //        return skill.SkillCode;
        //        //    }
        //        //}
        //    }
        //    return "";
        //}

        //#endregion

        #region 意志

        string GetDoWill(ref int comb)
        {
            int willType = 0;
            List<string> willList=new List<string>();
            switch (willType)
            {
                case 2:
                    willList = new List<string>() {"W113", "W111", "W106", "W107", "W109"};
                    break;
                case 3:
                    willList = new List<string>() { "W108", "W112", "W114", "W115", "W117" };
                    break;
                case 4:
                    willList = new List<string>() { "W101", "W102", "W104", "W103"};
                    break;
            }
            
            return string.Join(",",willList);
        }

        #endregion

        #region 天赋

        string GetNodoManagerSkill()
        {
            int willType = 0;
            string s = "";
            switch (willType)
            {
                case 1:
                    s = "W001,W002,W003,W004,W005,W006,W007,W008,W009,W010";
                    break;
                case 2:
                    s = "W001,W002,W003,W004,W005,W006,W007,W008,W009,W010,W011,W012,W013,W014,W015,W016,W017,W018,W019,W020";
                    break;
                case 3:
                    s = "W001,W002,W003,W004,W005,W006,W007,W008,W009,W010,W011,W012,W013,W014,W015,W016,W017,W018,W019,W020,W021,W022,W023,W024,W025,W026,W027,W028,W029,W030";
                    break;
                case 4:
                    s = "W001,W002,W003,W004,W005,W006,W007,W008,W009,W010,W011,W012,W013,W014,W015,W016,W017,W018,W019,W020,W021,W022,W023,W024,W025,W026,W027,W028,W029,W030,W031,W032,W033,W034,W035,W036,W037,W038,W039,W040,W041,W042,W043,W044,W045";
                    break;
            }
            if (!string.IsNullOrEmpty(s))
                s = s + ",";
            //if (m_template.TalentLevel >=10)
            //{
            //    s += "T001,T002,T003,";
            //}
            //if (m_template.TalentLevel >= 20)
            //{
            //    s += "T007,T008,T009,";
            //}
            //if (m_template.TalentLevel >= 40)
            //{
            //    s += "T013,T014,T015,";
            //}
            //if (m_template.TalentLevel >= 65)
            //{
            //    s += "T019,T020,T021,";
            //}
            return s.TrimEnd(',');
        }

        string GetDoTalent()
        {
            List<string> talents=new List<string>();
            //if (m_template.TalentLevel >= 80)
            //{
            //    talents=new List<string>(){"T022","T023","T024"};
            //}
            //else if (m_template.TalentLevel >= 50)
            //{
            //    talents = new List<string>() { "T016", "T017", "T018" };
            //}
            //else if (m_template.TalentLevel >= 30)
            //{
            //    talents = new List<string>() { "T010", "T011", "T012" };
            //}
            //else if (m_template.TalentLevel >= 15)
            //{
            //    talents = new List<string>() { "T004", "T005", "T006" };
            //}
            string s = "";
            if (talents.Count > 0)
            {
                int a = RandomHelper.GetInt32WithoutMax(0, 3);
                talents.RemoveAt(a);
                s = string.Join(",", talents);
            }
            return s;
        }

        #endregion

        private T GetRandom<T>(List<T> list)
        {
            if(list==null || list.Count==0)
                throw new Exception("list is empty.");
            return list[RandomHelper.GetInt32WithoutMax(0, list.Count)];
        }

        int GetPosition(int index)
        {
            return _formationdetail[index - 1].Position;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //ReBuildChallengeNpc();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ReBuildRevelationNpc();
        }

        /// <summary>
        /// 注册球星启示录npc
        /// </summary>
        private void ReBuildRevelationNpc()
        {
            BuildCache();
            var allnpcTemp = ConfigRevelationnpctempMgr.GetAll();
            SqlHelper.ExecuteNonQuery(_connection, CommandType.Text, "Delete From Dic_Npc Where Type=3;Truncate table Config_RevelationNpcLink");
            int index = 0;
            foreach (var item in allnpcTemp)
            {
                index ++;
                DicNpcEntity entity = new DicNpcEntity();
                entity.Idx = ShareUtil.GenerateComb();
                entity.Type = 3;
                entity.Name = item.OpponentTeamName;
                entity.Logo = 1;
                entity.FormationId = item.FormationID;
                entity.FormationLevel = item.PlayerLevel;
                entity.TeammemberLevel = item.PlayerLevel;
                entity.PlayerCardStrength = item.PlayerCardStrength;
                entity.CoachId = 0;
                entity.DoTalent = GetDoTalent();//主动天赋
                entity.ManagerSkill = GetNodoManagerSkill();//被动天赋和意志
                int comb = 0;
                entity.DoWill = GetDoWill(ref comb);//主动意志
                entity.CombLevel = item.PlayerLevel;
                entity.Buff = item.Buff;

                entity.TP1 = item.P1;
                entity.TP2 = item.P2;
                entity.TP3 = item.P3;
                entity.TP4 = item.P4;
                entity.TP5 = item.P5;
                entity.TP6 = item.P6;
                entity.TP7 = item.P7;

                entity.TE1 = 211071;
                entity.TE2 = 211076;
                entity.TE3 = 211072;
                entity.TE4 = 211073;
                entity.TE5 = 211074;
                entity.TE6 = 211075;
                entity.TE7 = 211077;

                entity.TS1 = "A031_40";
                entity.TS2 = "A033_40";
                entity.TS3 = "A027_40";
                entity.TS4 = "A034_40";
                entity.TS5 = "A030_40";
                entity.TS6 = "A032_40";
                entity.TS7 = "A028_40";
                DicNpcMgr.Insert(entity);
                ConfigRevelationnpclinkMgr.Insert(new ConfigRevelationnpclinkEntity(index, item.MarkId, item.Schedule,
                    entity.Idx));
                 m_curCount++;
                    ProgressBar1.Value = m_curCount;
                    lblProcess.Content = string.Format("进度：{0}/{1}", m_curCount, m_totalCount);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ReBuildArenaNpc();
        }


        void doRebuildArenaNpc(CreateDelegate createDelegate)
        {
            try
            {
                BuildCache();
                SqlHelper.ExecuteNonQuery(_connection, CommandType.Text, "Delete From Dic_Npc Where Type=2;Truncate table Config_ArenaNpcLink");
                for (int j = 1 ; j < 3; j++)
                {
                    int playerLevel = 0;
                    int strengthenLevel = 0;
                    int eqLevel = 0;
                    for (int i = 1; i < 101; i++)
                    {
                        //球员等级
                        playerLevel = i;
                        if (playerLevel > 80)
                            playerLevel = 80;
                        //强化等级
                        strengthenLevel = i / 10 + 1;
                        if (strengthenLevel > 9)
                            strengthenLevel = 9;
                        //装备等级
                        if (i < 10)
                            eqLevel = 5;
                        else if (i < 20)
                            eqLevel = 4;
                        else if (i < 30)
                            eqLevel = 3;
                        else if (i < 40)
                            eqLevel = 2;
                        else if (i < 50)
                            eqLevel = 1;
                        var entity = BuildBasicData(playerLevel, strengthenLevel, eqLevel);
                        DicNpcMgr.Insert(entity);
                        ConfigArenanpclinkMgr.Insert(new ConfigArenanpclinkEntity(entity.Idx, i, j, 0,0));
                        m_curCount++;
                        ProgressBar1.Value = m_curCount;
                        lblProcess.Content = string.Format("进度：{0}/{1}", m_curCount, m_totalCount);
                    }
                }
                


            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                m_curCount = -100;
                lblProcess.Content = string.Format("进度：{0}/{1}", m_curCount, m_totalCount);
            }
        }


        private int GetOnePlayer(string positionDesc)
        {
            if (_playerDic.ContainsKey(positionDesc))
            {
                var list = _playerDic[positionDesc];
                return list[RandomHelper.GetInt32(0, list.Count - 1)];
            }
            return 0;
        }

        DicNpcEntity BuildBasicData(int playerLevel, int strengthenLevel, int eqLevel)
        {  
            //阵型
            int formationId = RandomHelper.GetInt32(1, 17);
            //获取阵型
            var formationList = CacheFactory.FormationCache.GetFormationDetail(formationId);

            DicNpcEntity entity = new DicNpcEntity();
            entity.Idx = ShareUtil.GenerateComb();
            entity.Type = 2;

            entity.Name = GetName();
            entity.Logo = 1;
            entity.FormationId = formationId;
            entity.FormationLevel = playerLevel;
            entity.TeammemberLevel = playerLevel;
            entity.PlayerCardStrength = strengthenLevel;
            entity.CoachId = 0;
            entity.DoTalent = GetDoTalent();//主动天赋
            entity.ManagerSkill = GetNodoManagerSkill();//被动天赋和意志
            int comb = 0;
            entity.DoWill = GetDoWill(ref comb);//主动意志
            entity.CombLevel = playerLevel;
            entity.Buff = 100 + playerLevel;

            entity.TP1 = GetPlayer(1, formationList.Values.ToList());
            entity.TP2 = GetPlayer(2, formationList.Values.ToList());
            entity.TP3 = GetPlayer(3, formationList.Values.ToList());
            entity.TP4 = GetPlayer(4, formationList.Values.ToList());
            entity.TP5 = GetPlayer(5, formationList.Values.ToList());
            entity.TP6 = GetPlayer(6, formationList.Values.ToList());
            entity.TP7 = GetPlayer(7, formationList.Values.ToList());

            entity.TE1 = GetEquipment(eqLevel);
            entity.TE2 = GetEquipment(eqLevel);
            entity.TE3 = GetEquipment(eqLevel);
            entity.TE4 = GetEquipment(eqLevel);
            entity.TE5 = GetEquipment(eqLevel);
            entity.TE6 = GetEquipment(eqLevel);
            entity.TE7 = GetEquipment(eqLevel);
            entity.TS1 = "";
            entity.TS2 = "";
            entity.TS3 = "";
            entity.TS4 = "";
            entity.TS5 = "";
            entity.TS6 = "";
            entity.TS7 = "";
            return entity;
        }

        private int GetPlayer(int index,List<DicFormationdetailEntity> formationdetail)
        {
            var detail = formationdetail.Find(r => r.SpecificPoint + 1 == index);
            var playerId = GetOnePlayer(detail.SpecificPointDesc);
            return playerId;
        }

        private int GetEquipment(int eqLevel)
        {
            var equipment = CacheFactory.EquipmentCache.RandomEquipment(eqLevel);
            if (equipment == null)
                return 0;
            return equipment.Idx;
        }
        public static string[] nameString = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private string GetName()
        {
            return nameString[RandomHelper.GetInt32(0, 25)] + nameString[RandomHelper.GetInt32(0, 25)] + CacheFactory.PlayersdicCache.GetRandomPlayerName();
        }
    }
}
