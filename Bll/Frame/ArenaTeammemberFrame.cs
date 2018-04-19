using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;
using log4net.Appender;

namespace Games.NBall.Bll.Frame
{
    /// <summary>
    /// 竞技场逻辑封装
    /// </summary>
    public class ArenaTeammemberFrame
    {
        #region .ctor
        
        private string _zoneId="";
        private bool isInsert = false;
        /// <summary>
        /// 数据库对象
        /// </summary>
        public ArenaTeammemberEntity _arenaTeammemberEntity;
        /// <summary>
        /// 阵型对象
        /// </summary>
        private ArenaTeammeberFrame _arenaTeammemberFrame;

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="arenaType"></param>
        /// <param name="zoneName"></param>
        public ArenaTeammemberFrame(Guid managerId,EnumArenaType arenaType,string zoneName ="")
        {
            var teammpmberInfo = ArenaTeammemberMgr.GetByManagerId(managerId, (int) arenaType,zoneName);
            _zoneId = zoneName;
            if (teammpmberInfo == null)
            {
                teammpmberInfo = new ArenaTeammemberEntity();
                teammpmberInfo.ManagerId = managerId;
                teammpmberInfo.ArenaType = (int) arenaType;
                teammpmberInfo.SkillString = ",,,,,,";
                teammpmberInfo.TeammemberString = new byte[0];
                teammpmberInfo.RowTime = DateTime.Now;
                isInsert = true;
            }
            _arenaTeammemberEntity = teammpmberInfo;
            Analyse();
            ArenaType = arenaType;
            ManagerId = managerId;
            ZoneName = zoneName;
        }

     
        #endregion

        #region 属性

        /// <summary>
        /// 经理ID
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 综合实力
        /// </summary>
        public int Kpi { get; set; }

        /// <summary>
        /// 区ID
        /// </summary>
        public string ZoneName { get; set; }

        /// <summary>
        /// 竞技场类型
        /// </summary>
        public EnumArenaType ArenaType { get; set; }

        /// <summary>
        /// 球员列表
        /// </summary>
        public Dictionary<Guid,ArenaTeammember> TeammebmerDic { get; set; }

        /// <summary>
        /// 阵型ID
        /// </summary>
        public int SolutionId { get; set; }

        /// <summary>
        /// 球员串
        /// </summary>
        public string PlayerString { get; set; }

        private List<int> _playerList;

        /// <summary>
        /// 球员ID列表
        /// </summary>
        public List<int> PlayerList
        {
            get
            {
                if (_playerList == null)
                {
                    _playerList = new List<int>();
                    if (PlayerString.Length > 0)
                    {
                        var p = PlayerString.Split(',');
                        foreach (var s in p)
                        {
                            var playerId = ConvertHelper.ConvertToInt(s);
                            _playerList.Add(playerId);
                        }
                    }
                }
                return _playerList;
            }
            set { value = _playerList; }
        }

        public void SetPlayerList()
        {
            _playerList = new List<int>();
            if (PlayerString.Length > 0)
            {
                var p = PlayerString.Split(',');
                foreach (var s in p)
                {
                    var playerId = ConvertHelper.ConvertToInt(s);
                    _playerList.Add(playerId);
                }
            }
        }

        /// <summary>
        /// 技能串
        /// </summary>
        public string SkillString { get; set; }

        private List<string> _skillList;

        /// <summary>
        /// 技能列表
        /// </summary>
        public List<string> SkillList
        {
            get
            {
                if (_skillList == null)
                {
                    _skillList = new List<string>();
                    if (SkillString.Length > 0)
                    {
                        var p = SkillString.Split(',');
                        foreach (var s in p)
                        {
                            _skillList.Add(s);
                        }
                    }
                }
                return _skillList;
            }
            set { value = _skillList; }
        }

        #endregion

        /// <summary>
        /// 设置球员串
        /// </summary>
        /// <returns></returns>
        public string SetPlayerString()
        {
            PlayerString = "";
            foreach (var i in PlayerList)
            {
                PlayerString += i + ",";
            }
            PlayerString = PlayerString.Substring(0, PlayerString.Length - 1);
            return PlayerString;
        }

        /// <summary>
        /// 替换阵型
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <param name="byTeammemberId"></param>
        /// <param name="playerId"></param>
        /// <param name="itemProperty"></param>
        public void ExchangePlayer(Guid teammemberId,Guid byTeammemberId,int playerId,PlayerCardProperty itemProperty)
        {
            if (this.TeammebmerDic.ContainsKey(byTeammemberId))
                this.TeammebmerDic.Remove(byTeammemberId);
            if (!this.TeammebmerDic.ContainsKey(teammemberId))
                this.TeammebmerDic.Add(teammemberId,
                    new ArenaTeammember() { ItemId = teammemberId, PlayerId = playerId, UsePlayer = itemProperty });
        }

        /// <summary>
        /// 上阵
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <param name="playerId"></param>
        /// <param name="itemProperty"></param>
        public void UpFormation(Guid teammemberId, int playerId, PlayerCardProperty itemProperty)
        {
            if (!this.TeammebmerDic.ContainsKey(teammemberId))
                this.TeammebmerDic.Add(teammemberId,
                    new ArenaTeammember() { ItemId = teammemberId, PlayerId = playerId, UsePlayer = itemProperty });
        }

        /// <summary>
        /// 获取阵型球员
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public ArenaTeammember GetTeammember(Guid teammemberId)
        {
            if (this.TeammebmerDic.ContainsKey(teammemberId))
                return this.TeammebmerDic[teammemberId];
            return null;
        }

        /// <summary>
        /// 获取球员列表
        /// </summary>
        /// <returns></returns>
        public List<TeammemberEntity> GetTeammebmerList()
        {
            var result = new List<TeammemberEntity>();
            if (this.TeammebmerDic != null)
            {
                foreach (var item in TeammebmerDic.Values)
                {
                    TeammemberEntity entity = new TeammemberEntity(this.ManagerId, item.PlayerId, item.UsePlayer.Level,
                        item.UsePlayer.Strength);
                    entity.PlayerCard = new PlayerCardUsedEntity() {ItemId = item.ItemId, Property = item.UsePlayer};
                    entity.Equipment = item.UsePlayer.Equipment;
                    entity.Idx = item.ItemId;
                    result.Add(entity);
                }
                return result;
            }
            return null;
        }


        #region 保存

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool Save(DbTransaction trans = null)
        {
            _arenaTeammemberFrame.Kpi = this.Kpi;
            _arenaTeammemberFrame.PlayerString = this.PlayerString;
            _arenaTeammemberFrame.SolutionId = this.SolutionId;
            _arenaTeammemberFrame.TeammemberList = this.TeammebmerDic;
            var teammemberString = GenerateString();
            _arenaTeammemberEntity.TeammemberString = teammemberString;
            _arenaTeammemberEntity.UpdateTime = DateTime.Now;
            _arenaTeammemberEntity.SkillString = this.SkillString;
            if (isInsert)
            {
                if (!ArenaTeammemberMgr.Insert(_arenaTeammemberEntity, trans, _zoneId))
                    return false;
            }
            else if (!ArenaTeammemberMgr.Update(_arenaTeammemberEntity, trans, _zoneId))
                return false;
            return true;
        }

        #endregion

        #region 解析

        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>成功返回true</returns>
        private void Analyse()
        {
            _arenaTeammemberFrame =
                SerializationHelper.FromByte<ArenaTeammeberFrame>(_arenaTeammemberEntity.TeammemberString);
            if (_arenaTeammemberFrame == null)
            {
                _arenaTeammemberFrame = new ArenaTeammeberFrame();
                _arenaTeammemberFrame.SolutionId = 1;
                _arenaTeammemberFrame.PlayerString = "0,0,0,0,0,0,0";
                _arenaTeammemberFrame.TeammemberList = new Dictionary<Guid, ArenaTeammember>();
            }
            if (_arenaTeammemberFrame.SolutionId == 0)
                _arenaTeammemberFrame.SolutionId = 1;
            if(_arenaTeammemberFrame.PlayerString.Length==0)
                _arenaTeammemberFrame.PlayerString = "0,0,0,0,0,0,0";
            if (_arenaTeammemberFrame.TeammemberList == null)
                _arenaTeammemberFrame.TeammemberList = new Dictionary<Guid, ArenaTeammember>();
            if (string.IsNullOrEmpty(_arenaTeammemberFrame.PlayerString))
                _arenaTeammemberFrame.PlayerString = "0,0,0,0,0,0,0";
            if (string.IsNullOrEmpty(_arenaTeammemberEntity.SkillString))
                _arenaTeammemberEntity.SkillString = ",,,,,,";
            TeammebmerDic = _arenaTeammemberFrame.TeammemberList;
            SolutionId = _arenaTeammemberFrame.SolutionId;
            PlayerString = _arenaTeammemberFrame.PlayerString;
            SkillString = _arenaTeammemberEntity.SkillString;
            Kpi = _arenaTeammemberFrame.Kpi;
        }

        #endregion

        #region 系列化

        /// <summary>
        /// 系列化
        /// </summary>
        private byte[] GenerateString()
        {
            if (_arenaTeammemberFrame == null)
                return null;
            return SerializationHelper.ToByte(_arenaTeammemberFrame);
        }
        #endregion
    }

}
