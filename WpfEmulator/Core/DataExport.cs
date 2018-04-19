using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Match;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.WpfEmulator.Entity;
using Newtonsoft.Json;

namespace Games.NBall.WpfEmulator.Core
{
    public class DataExport
    {
        /// <summary>
        /// 获取请求示例
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static string GetRequestEg(string typeName)
        {
            return "";
        }

        #region ExportEnumData
        public static List<MessageEnumEntity> ExportEnumData(Type type)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XmlNodeList xmlNodeListByXpath = XMLHelper.GetXmlNodeListByXpath(AppDomain.CurrentDomain.BaseDirectory + "Games.NBall.Entity.XML", "doc/members/member");
            if ((xmlNodeListByXpath != null) && (xmlNodeListByXpath.Count > 0))
            {
                foreach (XmlNode node in xmlNodeListByXpath)
                {
                    string key = node.Attributes["name"].Value;

                    if (key.StartsWith("F:" + type.FullName))
                    {
                        key = key.Replace("F:" + type.FullName + ".", "");

                        XmlNode node2 = null;
                        string str3 = "";
                        if (node.ChildNodes.Count > 0)
                        {
                            node2 = node.ChildNodes[0];
                            if ((node2 != null) && (node2.ChildNodes.Count > 0))
                            {
                                node2 = node2.ChildNodes[0];
                            }
                        }
                        if ((node2 != null) && (node2.Value != null))
                        {
                            str3 = node2.Value.Replace("\r\n", "").Replace(" ", "");
                        }
                        LogHelper.Insert("desc:" + str3, LogType.Info);
                        dictionary.Add(key, str3);
                    }
                }
            }
            List<MessageEnumEntity> list2 = new List<MessageEnumEntity>();
            foreach (FieldInfo info in type.GetFields())
            {
                if (info.FieldType == type)
                {
                    MessageEnumEntity item = new MessageEnumEntity();
                    item.Name = info.Name;

                    if (dictionary.ContainsKey(item.Name))
                    {
                        item.Description = dictionary[item.Name];
                    }
                    else
                    {
                        item.Description = "";
                    }
                    item.Code = (int)(Enum.Parse(type, item.Name));
                    list2.Add(item);
                }
            }
            return list2;
        }
        #endregion

        #region ExportMesseageConfig
        public static MessageConfigEntity ExportMesseageConfig()
        {
            var entity = new MessageConfigEntity();
            entity.MessageCode = ExportEnumData(typeof(MessageCode));
            entity.EnumPosition = ExportEnumData(typeof(EnumPosition));
            entity.EnumProperty = ExportEnumData(typeof(EnumProperty));
            entity.EnumItemType = ExportEnumData(typeof(EnumItemType));
            entity.EnumPlayerCardLevel = ExportEnumData(typeof(EnumPlayerCardLevel));
            entity.EnumAttachmentType = ExportEnumData(typeof(EnumAttachmentType));
            entity.EnumCurrencyType = ExportEnumData(typeof(EnumCurrencyType));
            entity.EnumPandoraResultType = ExportEnumData(typeof(EnumPandoraResultType));
            entity.EnumWinType = ExportEnumData(typeof(EnumWinType));
            return entity;
        }
        #endregion

        #region ExportRequestConfig

        public static RequestConfigEntity ExportRequestConfig(bool isDebug)
        {
            var requestConfig = EmulatorHelper.LoadXml<RequestConfigEntity>(EmulatorHelper.ApiConfigFileName);
            Assembly assembly = Assembly.Load("Games.NBall.Entity");
            var summaryDic = BuildSummaryDic(assembly);

            Dictionary<string, string> responseNameDic = new Dictionary<string, string>();
            foreach (var exportedType in assembly.ExportedTypes)
            {
                string key = "";
                if (exportedType.GenericTypeArguments.Length == 1)
                    key = exportedType.Name + "[" + exportedType.GenericTypeArguments[0].Name + "]";

                else
                    key = exportedType.Name;
                if (!responseNameDic.ContainsKey(key))
                    responseNameDic.Add(key, exportedType.FullName);
            }
            if (isDebug)
            {
                string fullName = string.Empty;
                foreach (var module in requestConfig.Modules)
                {
                    if (module.Actions != null)
                    {
                        foreach (var action in module.Actions)
                        {
                            if (!string.IsNullOrEmpty(action.Response))
                            {
                                if (GetFullName(action.Response, out fullName, responseNameDic))
                                {
                                    Type type = assembly.GetType(fullName);
                                    dynamic t = Activator.CreateInstance(type);
                                    ActivatorCreateType(type, t);
                                    JsonConvert.EntitySummaryDic = null;
                                    action.Eg = JsonConvert.SerializeObject(t);
                                    JsonConvert.EntitySummaryDic = summaryDic;
                                    action.EgMemo = JsonConvert.SerializeObject(t);
                                    JsonConvert.EntitySummaryDic = null;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var module in requestConfig.Modules)
                {
                    module.Description = "";
                    module.Status = 0;
                    if (module.Actions != null)
                    {
                        foreach (var action in module.Actions)
                        {
                            action.Memo = "";
                            action.Response = "";
                            action.Eg = "";
                            action.EgMemo = "";
                            action.Description = "";
                            if (action.Parameters != null)
                            {
                                foreach (var parameter in action.Parameters)
                                {
                                    parameter.Description = "";
                                }
                            }
                        }
                    }
                }
            }
            return requestConfig;
        }
        static bool GetFullName(string cfgName, out string fullName, Dictionary<string, string> dicNames)
        {
            fullName = cfgName;
            if (!cfgName.EndsWith("]"))
                return dicNames.TryGetValue(cfgName, out fullName); ;
            var spilts = cfgName.TrimEnd(']').Split('[');
            if (spilts.Length != 2)
                return false;
            string a, b;
            if (!dicNames.TryGetValue(spilts[0], out a) || !dicNames.TryGetValue(spilts[1], out b))
                return false;
            fullName = string.Concat(a, "[", b, "]");
            return true;
        }

        public static Dictionary<string, Dictionary<string, string>> BuildSummaryDic(Assembly assembly, string remarkPath = "Games.NBall.Entity.XML")
        {
            Dictionary<string, Dictionary<string, string>> summaryDic = new Dictionary<string, Dictionary<string, string>>();
            foreach (var exportedType in assembly.ExportedTypes)
            {
                summaryDic.Add(exportedType.FullName, new Dictionary<string, string>());
            }

            XmlNodeList xmlNodeListByXpath = XMLHelper.GetXmlNodeListByXpath(AppDomain.CurrentDomain.BaseDirectory + remarkPath, "doc/members/member");
            if ((xmlNodeListByXpath != null) && (xmlNodeListByXpath.Count > 0))
            {
                foreach (XmlNode node in xmlNodeListByXpath)
                {
                    string key = node.Attributes["name"].Value;

                    if (key.StartsWith("P:"))
                    {
                        var index = key.LastIndexOf('.');
                        string className = key.Substring(0, index).Replace("P:", "");
                        var propertyName = key.Substring(index + 1);
                        if (summaryDic.ContainsKey(className))
                        {
                            if (!summaryDic[className].ContainsKey(propertyName))
                            {
                                XmlNode node2 = null;
                                string str3 = "";
                                if (node.ChildNodes.Count > 0)
                                {
                                    node2 = node.ChildNodes[0];
                                    if ((node2 != null) && (node2.ChildNodes.Count > 0))
                                    {
                                        node2 = node2.ChildNodes[0];
                                    }
                                }
                                if ((node2 != null) && (node2.Value != null))
                                {
                                    str3 = node2.Value.Replace("\r\n", "").Replace(" ", "");
                                }
                                if (!string.IsNullOrEmpty(str3))
                                {
                                    summaryDic[className].Add(propertyName, str3);
                                }
                            }
                        }
                    }
                }
            }
            return summaryDic;
        }

        /// <summary>
        /// 动态创建对象，递归
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        public static void ActivatorCreateType(Type type, object obj)
        {
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (!propertyInfo.PropertyType.FullName.Contains("System"))
                {
                    var o = Activator.CreateInstance(propertyInfo.PropertyType);
                    ActivatorCreateType(propertyInfo.PropertyType, o);
                    obj.GetType().GetProperty(propertyInfo.Name).SetValue(obj, o);
                }
                else
                {
                    if (propertyInfo.PropertyType.Name == "String")
                    {
                        obj.GetType().GetProperty(propertyInfo.Name).SetValue(obj, propertyInfo.Name.ToLower());
                    }
                    else if (propertyInfo.PropertyType.Name == "DateTime")
                    {
                        obj.GetType().GetProperty(propertyInfo.Name).SetValue(obj, new DateTime(2000, 1, 1));
                    }
                    else if (propertyInfo.PropertyType.Name.Contains("List"))
                    {
                        Type generic = typeof(List<>);
                        generic = generic.MakeGenericType(propertyInfo.PropertyType.GenericTypeArguments);
                        var list = (IList)Activator.CreateInstance(generic);
                        var gt = propertyInfo.PropertyType.GenericTypeArguments[0];
                        object o = null;
                        if (gt == typeof(string))
                            o = propertyInfo.Name.ToLower();
                        else
                        {
                            o = Activator.CreateInstance(gt);
                            ActivatorCreateType(propertyInfo.PropertyType.GenericTypeArguments[0], o);
                        }
                        list.Add(o);
                        obj.GetType().GetProperty(propertyInfo.Name).SetValue(obj, list);
                    }
                }
            }
        }

        //static void GetServiceMethods()
        //{
        //    Assembly assemblyService = Assembly.Load("Games.NBall.ServiceContract");
        //    RequestConfigEntity requestConfigEntity = new RequestConfigEntity();
        //    Type[] mytypes = assemblyService.GetTypes();
        //    requestConfigEntity.Modules = new List<RequestConfigModuleEntity>();
        //    foreach (var type in mytypes)
        //    {
        //        if (type.Namespace.IsNullOrWhiteSpace()
        //            || type.Name.Contains("<>"))
        //            continue;
        //        if (type.Namespace == "Games.NBall.ServiceContract.Service")
        //        {
        //            var module = new RequestConfigModuleEntity();
        //            module.Name = type.Name;
        //            module.Actions = new List<RequestConfigActionEntity>();
        //            foreach (var method in type.GetMethods())
        //            {
        //                if (method.Name != "ToString"
        //                    && method.Name != "Equals"
        //                    && method.Name != "GetHashCode"
        //                    && method.Name != "GetType"
        //                    && method.Name != ".ctor")
        //                {
        //                    var action = new RequestConfigActionEntity();
        //                    action.Name = method.Name;
        //                    action.Parameters = new List<RequestConfigParameterEntity>();
        //                    foreach (var p in method.GetParameters())
        //                    {
        //                        action.Parameters.Add(new RequestConfigParameterEntity() { Name = p.Name, Description = p.Name });
        //                    }
        //                    module.Actions.Add(action);
        //                }
        //            }
        //            requestConfigEntity.Modules.Add(module);
        //        }
        //    }
        //}
        #endregion

        #region ExportFormationConfig
        public static FormationConfigEntity ExportFormationConfig()
        {
            var formationConfig = new FormationConfigEntity();
            var list = DicFormationMgr.GetAll();
            var list2 = DicFormationdetailMgr.GetAll();
            formationConfig.FormationList = new List<FormationEntity>();
            foreach (var entity in list)
            {
                var formationEntity = new FormationEntity();
                formationEntity.Idx = entity.Idx;
                formationEntity.Formation = entity.Formation;
                formationEntity.Name = entity.Name;
                formationEntity.BuffPerLevel = entity.BuffPerLevel;
                formationEntity.Description = entity.Description;
                formationEntity.DetailList = new List<FormationdetailEntity>();
                var details = list2.FindAll(d => d.FormationId == entity.Idx);
                foreach (var detail in details)
                {
                    formationEntity.DetailList.Add(new FormationdetailEntity() { Position = detail.Position, Coordinate = detail.Coordinate, SpecificPointDesc = detail.SpecificPointDesc });
                }
                formationConfig.FormationList.Add(formationEntity);
            }
            return formationConfig;
        }
        #endregion
        #region ExportItemTips
        public static ItemTipsEntity ExportItemTips()
        {
            var itemList = DicItemMgr.GetAll();
            var playerList = DicPlayerMgr.GetAll();
            var equipmentList = DicEquipmentMgr.GetAll();
            var mallList = DicMallitemMgr.GetAll().FindAll(d => d.MallType != 5);
            var ballList = DicBallsoulMgr.GetAll();
            var willList = DicManagerwillMgr.GetAll();
            var starskillList = DicStarskillsMgr.GetAll();
            ItemTipsEntity itemTips = new ItemTipsEntity();
            itemTips.PlayerCard = new List<PlayerCardDescriptionEntity>(playerList.Count);
            itemTips.Equipment = new List<EquipmentDescriptionEntity>(equipmentList.Count);
            itemTips.Ballsoul = new List<BallsoulDescriptionEntity>(0);
            itemTips.MallItem = new List<MallItemDescriptionEntity>(mallList.Count);
            foreach (var itemEntity in itemList)
            {
                switch (itemEntity.ItemType)
                {
                    case (int)EnumItemType.PlayerCard:
                        var playerd = BuildPlayerCardDescription(itemEntity,
                                                                 playerList.Find(d => d.Idx == itemEntity.LinkId));
                        var star = starskillList.FindAll(d => d.PlayerId == playerd.PlayerId);
                        if (star.Count > 0)
                        {
                            playerd.Starskill = "";
                            playerd.StarskillCode = "";
                            foreach (var entity in star)
                            {
                                playerd.Starskill += entity.Name + ",";
                                playerd.StarskillCode += entity.SkillCode + ",";
                            }
                            playerd.Starskill = playerd.Starskill.TrimEnd(',');
                            playerd.StarskillCode = playerd.StarskillCode.TrimEnd(',');
                        }


                        var wills =
                            willList.FindAll(d => d.DriveFlag == 1 && d.PartMap.Contains(playerd.PlayerId.ToString()));
                        if (wills != null && wills.Count > 0)
                        {
                            foreach (var entity in wills)
                            {
                                playerd.CombSkill = entity.SkillName + ",";
                            }
                            playerd.CombSkill = playerd.CombSkill.TrimEnd(',');
                        }
                        itemTips.PlayerCard.Add(playerd);
                        break;
                    case (int)EnumItemType.Equipment:
                        var equipd = BuildEquipmentDescription(itemEntity,
                                                                 equipmentList.Find(d => d.Idx == itemEntity.LinkId));
                        itemTips.Equipment.Add(equipd);
                        break;
                   
                    case (int)EnumItemType.MallItem:
                        var malld = BuildMallDescription(itemEntity, mallList.Find(d => d.MallCode == itemEntity.LinkId));
                        itemTips.MallItem.Add(malld);
                        break;
                }
            }
            return itemTips;
        }

        static PlayerCardDescriptionEntity BuildPlayerCardDescription( DicItemEntity itemEntity, DicPlayerEntity playerEntity)
        {

            var entity = new PlayerCardDescriptionEntity();
            entity.ItemCode = itemEntity.ItemCode;
            entity.ItemType = itemEntity.ItemType;
            entity.Name = itemEntity.ItemName;
            entity.ImageId = itemEntity.ImageId;
            entity.CardLevel = playerEntity.CardLevel;
            entity.Attack = Math.Round((playerEntity.Speed + playerEntity.Shoot + playerEntity.FreeKick) / 3, 2);
            entity.Body = Math.Round((playerEntity.Balance + playerEntity.Physique + playerEntity.Bounce) / 3, 2);
            entity.Defense = Math.Round((playerEntity.Aggression + playerEntity.Disturb + playerEntity.Interception) / 3, 2);
            entity.Organize = Math.Round((playerEntity.Dribble + playerEntity.Pass + playerEntity.Mentality) / 3, 2);
            entity.Goalkeep = Math.Round((playerEntity.Response + playerEntity.Positioning + playerEntity.HandControl) / 3, 2);
            entity.CombSkill = "";
            entity.Kpi = playerEntity.Kpi;
            entity.KpiLevel = playerEntity.KpiLevel;
            entity.Capacity = playerEntity.Capacity;
            entity.LeagueLevel = playerEntity.LeagueLevel;
            entity.PlayerId = playerEntity.Idx;
            entity.Club = playerEntity.Club;
            entity.Birthday = playerEntity.Birthday;
            entity.Description = playerEntity.Description;
            entity.Nationality = playerEntity.Nationality;
            entity.Stature = playerEntity.Stature.ToString("f2");
            entity.Weight = playerEntity.Weight.ToString("f2");
            entity.AllPosition = playerEntity.AllPosition.Trim();
            entity.Specific = playerEntity.Specific;
            entity.Position = playerEntity.Position;
            entity.Speed = playerEntity.Speed;
            entity.Shoot = playerEntity.Shoot;
            entity.FreeKick = playerEntity.FreeKick;
            entity.Balance = playerEntity.Balance;
            entity.Physique = playerEntity.Physique;
            entity.Bounce = playerEntity.Bounce;
            entity.Aggression = playerEntity.Aggression;
            entity.Disturb = playerEntity.Disturb;
            entity.Interception = playerEntity.Interception;
            entity.Dribble = playerEntity.Dribble;
            entity.Pass = playerEntity.Pass;
            entity.Mentality = playerEntity.Mentality;
            entity.Response = playerEntity.Response;
            entity.Positioning = playerEntity.Positioning;
            entity.HandControl = playerEntity.HandControl;
            entity.Acceleration = playerEntity.Acceleration;
            entity.Power = playerEntity.Power;
            entity.EnName = playerEntity.NameEn;
            return entity;
        }

        static EquipmentDescriptionEntity BuildEquipmentDescription(DicItemEntity itemEntity, DicEquipmentEntity equipmentEntity)
        {
            var entity = new EquipmentDescriptionEntity();
            entity.ItemCode = itemEntity.ItemCode;
            entity.ItemType = itemEntity.ItemType;
            entity.Name = itemEntity.ItemName;
            entity.ImageId = itemEntity.ImageId;
            entity.Quality = equipmentEntity.Quality;
            entity.SuitId = equipmentEntity.SuitId;
            entity.SuitType = equipmentEntity.SuitType;
            entity.Idx = equipmentEntity.Idx;
            entity.Property1 = equipmentEntity.PropertyType1;
            entity.Property2 = equipmentEntity.PropertyType2;

            if (equipmentEntity.SuitType == 4)
            {
                #region 处理散装前缀
                string s = equipmentEntity.Name.Substring(0, 2);
                switch (s)
                {
                    case "迅捷":
                        entity.Prefix = 1;
                        break;
                    case "灵感":
                        entity.Prefix = 2;
                        break;
                    case "精准":
                        entity.Prefix = 3;
                        break;
                    case "专制":
                        entity.Prefix = 4;
                        break;
                    case "活力":
                        entity.Prefix = 5;
                        break;
                    case "旋风":
                        entity.Prefix = 6;
                        break;
                    case "狂热":
                        entity.Prefix = 7;
                        break;
                    case "尘暴":
                        entity.Prefix = 8;
                        break;
                    case "犀利":
                        entity.Prefix = 9;
                        break;
                    case "驭风":
                        entity.Prefix = 10;
                        break;
                    case "弧光":
                        entity.Prefix = 11;
                        break;
                    case "灵动":
                        entity.Prefix = 12;
                        break;
                    case "反射":
                        entity.Prefix = 13;
                        break;
                    case "空间":
                        entity.Prefix = 14;
                        break;
                    case "触感":
                        entity.Prefix = 15;
                        break;

                }

                #endregion

                #region 处理散装后缀

                var ss = equipmentEntity.Name.Substring(2);
                switch (ss)
                {
                    case "战靴":
                        entity.ImageId = 1;
                        break;
                    case "护目镜":
                        entity.ImageId = 2;
                        break;
                    case "护踝":
                        entity.ImageId = 3;
                        break;
                    case "护臂":
                        entity.ImageId = 4;
                        break;
                    case "护腿板":
                        entity.ImageId = 5;
                        break;
                    case "耳环":
                        entity.ImageId = 6;
                        break;
                    case "头带":
                        entity.ImageId = 7;
                        break;
                    case "护腕":
                        entity.ImageId = 8;
                        break;
                    case "护膝":
                        entity.ImageId = 9;
                        break;
                    case "护袜":
                        entity.ImageId = 10;
                        break;
                    case "手镯":
                        entity.ImageId = 11;
                        break;
                    case "戒指":
                        entity.ImageId = 12;
                        break;
                    case "指贴":
                        entity.ImageId = 13;
                        break;
                    case "球帽":
                        entity.ImageId = 14;
                        break;
                    case "手套":
                        entity.ImageId = 15;
                        break;
                }

                #endregion
            }
            return entity;
        }

        static MallItemDescriptionEntity BuildMallDescription(DicItemEntity itemEntity, DicMallitemEntity mallEntity)
        {
            var entity = new MallItemDescriptionEntity();
            entity.ItemCode = itemEntity.ItemCode;
            entity.ItemType = itemEntity.ItemType;
            entity.Name = itemEntity.ItemName;
            entity.ImageId = itemEntity.ImageId;
            entity.Idx = mallEntity.MallCode;
            entity.ItemIntro = mallEntity.ItemIntro;
            entity.ItemTip = mallEntity.ItemTip;
            entity.UseMsg = mallEntity.UseMsg;
            entity.UseNote = mallEntity.UseNote;
            entity.Quality = mallEntity.Quality;
            entity.UseLevel = mallEntity.UseLevel;
            entity.ImageId = itemEntity.ImageId;
            entity.ShowUse = mallEntity.ShowUse ? 1 : 0;
            entity.ShowBatch = mallEntity.ShowBatch ? 1 : 0;
            return entity;
        }

        static BallsoulDescriptionEntity BuildBallsoulDescription(DicItemEntity itemEntity, DicBallsoulEntity ballsoulEntity)
        {
            var entity = new BallsoulDescriptionEntity();
            entity.ItemCode = itemEntity.ItemCode;
            entity.ItemType = itemEntity.ItemType;
            entity.Name = itemEntity.ItemName;
            entity.ImageId = itemEntity.ImageId;
            entity.Idx = ballsoulEntity.Idx;
            entity.Color = ballsoulEntity.Color;
            entity.Level = ballsoulEntity.Level;
            entity.Description = ballsoulEntity.Description;
            entity.Type = ballsoulEntity.Type;
            return entity;
        }
        #endregion
        #region ExportSkillTips
        public static SkillTipsEntity ExportSkillTips(bool includeStarSkillLevel=false)
        {
            var tips = new SkillTipsEntity();
            tips.SkillCard = DicSkillcardtipsMgr.GetAll();
            tips.StarSkill = DicStarskilltipsMgr.GetAll();
            tips.ManagerTalent = DicManagertalenttipsMgr.GetAll();
            tips.ClubSkills = DicClubskillMgr.GetAll();
            var wills = DicManagerwilltipsMgr.GetAll();
            var willParts = DicManagerwillparttipsMgr.GetAll();
            var dicWill = new Dictionary<string, DicManagerwilltipsEntity>(wills.Count);
            tips.LowWill = new List<DicManagerwilltipsEntity>();
            tips.HighWill = new List<DicManagerwilltipsEntity>();
            var strengthList = ConfigStrengthMgr.GetAll();
            var _configStrengthDic = new Dictionary<int, ConfigStrengthEntity>(strengthList.Count);

            foreach (var entity in strengthList)
            {
                var key = BuildStrengthKey(entity.CardLevel, entity.Source, entity.Target);
                if (!_configStrengthDic.ContainsKey(key))
                    _configStrengthDic.Add(key, entity);
            }
            tips.PlayerStrengthDic = _configStrengthDic;

            var allSkillConfig = ConfigSkillupgradeMgr.GetAll();
            var _SkillUpgradeDic = new Dictionary<int, ConfigSkillupgradeEntity>(allSkillConfig.Count);
            foreach (var item in allSkillConfig)
            {
                var key = GetKey(item.SkillLevel, item.Quality);
                if (!_SkillUpgradeDic.ContainsKey(key))
                    _SkillUpgradeDic.Add(key, item);
                else
                    _SkillUpgradeDic[key] = item;
            }
            tips.SkillUpGradeDic = _SkillUpgradeDic;

            var allprpo = ConfigPrposellMgr.GetAll();

            tips.PrpoSelllist = allprpo;

            var allSkill = DicSkillstreeMgr.GetAll();
            var allSkillDesc = DicSkillstreedesdicMgr.GetAll();
            var skillList = new List<ManagerSkillTree>();
            foreach (var skill in allSkill)
            {
                var desc = allSkillDesc.FindAll(r => r.SkillCode == skill.SkillCode);
                foreach (var d in desc)
                {
                    var sk = new ManagerSkillTree();
                    sk.CoditionPoint = skill.ConditionPoint;
                    sk.Condition = skill.Condition;
                    sk.Description = d.Description;
                    sk.ManagerLevel = skill.RequireManagerLevel;
                    sk.ManagerType = skill.ManagerType;
                    sk.MaxPoint = skill.MaxPoint;
                    sk.SkillCode = skill.SkillCode;
                    sk.SkillLevel = d.SkillLevel;
                    sk.SkillName = skill.SkillName;
                    sk.Opener = skill.Opener;
                    skillList.Add(sk);
                }
            }
            tips.ManagerSkillTree = skillList;
            #region CombLink
            //var combLinks = DicPlayerlinkMgr.GetAll();
            var cdic = new SortedDictionary<int, int>();
            int cno = 0;
            int cnt = 0;
            //foreach (var item in combLinks)
            //{
            //    if (!cdic.TryGetValue(item.Idx, out cno))
            //    {
            //        if (!cdic.TryGetValue(item.LinkId, out cno))
            //            cno = ++cnt;
            //    }
            //    cdic[item.Idx] = cno;
            //    cdic[item.LinkId] = cno;
            //}
            var dicComb = new Dictionary<int, string>();
            var list = new List<int>();
            for (int i = 0; i <= cnt; i++)
            {
                cno = 0;
                list.Clear();
                foreach (var kvp in cdic)
                {
                    if (kvp.Value != i)
                        continue;
                    if (cno == 0)
                        cno = kvp.Key;
                    else
                        list.Add(kvp.Key);
                }
                if (list.Count > 0)
                    dicComb[cno] = string.Join(",", list.ToArray());
            }
            #endregion

            foreach (var item in wills)
            {
                if (item.WillRank == 1)
                    tips.LowWill.Add(item);
                else
                    tips.HighWill.Add(item);
                dicWill[item.SkillCode] = item;
            }
            DicManagerwilltipsEntity will = null;
            foreach (var g in willParts.GroupBy(i => i.SkillCode))
            {
                if (!dicWill.TryGetValue(g.Key, out will))
                    continue;
                will.PartList = g.ToList();
            }
            CombTipsEntity comb = null;
            tips.Combs = new List<CombTipsEntity>();
            foreach (var item in dicWill.Values)
            {
                if (item.WillRank == 1)
                    continue;
                comb = new CombTipsEntity(item);
                comb.PartList = new List<CombPartTipsEntity>();
                foreach (var part in item.PartList)
                {
                    if (dicComb.ContainsKey(part.Pid))
                        part.LinkPid = dicComb[part.Pid];
                    else
                        part.LinkPid = string.Empty;
                    comb.PartList.Add(new CombPartTipsEntity(part));
                }
                tips.Combs.Add(comb);
            }
            //tips.StarArousalSkills = DicStararousalskillsMgr.GetAll();

            tips.Potential = new Dictionary<int, List<ConfigPotentialEntity>>();
            var allPotential = ConfigPotentialMgr.GetAll();
            foreach (var item in allPotential)
            {
                if (!tips.Potential.ContainsKey(item.PotentialId))
                    tips.Potential.Add(item.PotentialId, new List<ConfigPotentialEntity>());
                tips.Potential[item.PotentialId].Add(item);
            }
            if (includeStarSkillLevel)
                tips.StarSkillLevels = DicStarskillleveltipsMgr.GetAll();
            return tips;
        }

        static int BuildStrengthKey(int cardLevel, int source, int target)
        {
            return cardLevel * 10000 + source * 100 + target;
        }

        private static int GetKey(int skillLevel, int quality)
        {
            return quality * 10000 + skillLevel;
        }

        #endregion

        #region ExportDescriptionDic
        public static DescriptionConfigEntity ExportDescriptionDic()
        {
            var config = new DescriptionConfigEntity();
            var list = DicGrowMgr.GetAll();
            config.GrowDic = new List<LDescriptionEntity>(list.Count);
            foreach (var dicGrowEntity in list)
            {
                config.GrowDic.Add(new LDescriptionEntity() { Description = dicGrowEntity.Name, Idx = dicGrowEntity.Idx, GrowTip = dicGrowEntity.GrowTip });
            }
            var suitList = DicEquipmentsuitMgr.GetAll();
            config.EquipmentSuit = new List<EquipmentsuitEntity>(suitList.Count);
            foreach (var entity in suitList)
            {
                config.EquipmentSuit.Add(new EquipmentsuitEntity() { Idx = entity.Idx, Memo1 = entity.Memo1, Memo2 = entity.Memo2, Memo3 = entity.Memo3, Name = entity.Name, SuitType = entity.SuitType });
            }
            var appsettings = ConfigAppsettingMgr.GetAllForCache();

            config.WChallengeNameTemplate = GetAppSettingName("WChallengeStageNameTemplate", appsettings);
            config.HighBallsoulName = GetAppSettingName("HighBallsoulName", appsettings);
            config.NormalBallsoulName = GetAppSettingName("NormalBallsoulName", appsettings);

            var funcs = ConfigFunctionopenMgr.GetAll();
            config.Functionopens = new List<WpfConfigFunctionopenEntity>();
            foreach (var entity in funcs)
            {
                if (entity.FunctionId > 0)
                {
                    config.Functionopens.Add(new WpfConfigFunctionopenEntity() { Idx = entity.FunctionId, LockMemo = entity.LockMemo, Name = entity.Name, ManagerLevel = entity.ManagerLevel });
                }
            }
            config.BuffTips = new List<BuffTipsEntity>();
            foreach (var item in DicBuffMgr.GetAll())
            {
                if (item.BuffType == 2)
                    config.BuffTips.Add(new BuffTipsEntity(item));
            }
            config.BuffSrcTips = new List<BuffSrcTipsEntity>();
            foreach (var item in DicSkillMgr.GetAll())
            {
                if (item.BuffSrcType == 1)
                    config.BuffSrcTips.Add(new BuffSrcTipsEntity(item));
            }
          
            config.PlayerCardStrengthPlus = GetAppSettingName("PlayerCardStrengthPlus", appsettings);
            config.Equipmentplus = ConfigEquipmentplusMgr.GetAll();
            config.Mails = DicMailMgr.GetAll();

            var ladderExchanges = DicLadderexchangeMgr.GetAll();
            config.LadderExchanges=new List<LadderExchangeEntity>();
            foreach (var entity in ladderExchanges)
            {
                var exchange = new LadderExchangeEntity()
                {
                    Idx = entity.Type,
                    CostHonor = entity.CostHonor,
                    LadderCoin = entity.LadderCoin
                };
                config.LadderExchanges.Add(exchange);
                
            }

            var leagueExchanges = DicLeagueexchangeMgr.GetAll();
            config.LeagueExchanges = new List<LeagueExchangeEntity>();
            foreach (var entity in leagueExchanges)
            {
                var exchange = new LeagueExchangeEntity()
                {
                    Idx = entity.Idx,
                    CostScore = entity.CostScore
                };
                config.LeagueExchanges.Add(exchange);
            }

            var taskList = ConfigTaskMgr.GetAll();
            var taskList2 = ConfigTaskrequireMgr.GetAll();
            var giftItems = DicGiftpackprizeMgr.GetAll();

            foreach (var entity in taskList2)
            {
                var task = taskList.Find(d => d.Idx == entity.TaskId);
                if (!string.IsNullOrEmpty(task.RequireFuncs))
                    task.RequireFuncs += ",";
                task.RequireFuncs += entity.RequireType;
            }

            config.Tasks = new List<TaskDescriptionEntity>(taskList.Count);
            foreach (var entity in taskList)
            {
                config.Tasks.Add(new TaskDescriptionEntity()
                {
                    Idx = entity.Idx,
                    Name = entity.Name,
                    Level = entity.ManagerLevel,
                    Description = entity.Tip,
                    PrizeCoin = entity.PrizeCoin,
                    PrizeExp = entity.PrizeExp,
                    PrizeItemCode = entity.PrizeItemCode,
                    PrizeItems = GetGiftItems(giftItems,entity.PrizeItemCode),
                    RequireFuncs = entity.RequireFuncs,
                    TaskType = entity.TaskType,
                    Times = entity.Times,
                    NpcIdx = entity.NpcIdx
                });
            }

            config.Turntable = new List<ConfigTurntableprizeEntity>();
            var allturntable = ConfigTurntableprizeMgr.GetAll();
            config.Turntable = allturntable;

            return config;
        }

        static string GetGiftItems(List<DicGiftpackprizeEntity> giftItems, int giftPackId)
        {
            string items = "";
            var gifts = giftItems.FindAll(g => g.PackId == giftPackId);
            foreach (var item in gifts)
            {
                items += item.PrizeType + "," + item.SubType + "," + item.Count + "|";
            }
            items = items.TrimEnd('|');
            return items;

        }

        #endregion

        #region ExportDescriptionDic1
        public static DescriptionConfigEntity1 ExportDescriptionDic1()
        {
            var config = new DescriptionConfigEntity1();
            var allLeagueNpc = ConfigLeaguemarkMgr.GetAll();
            config.LeagueNpc = new Dictionary<int, List<LeagueNpc>>();
            foreach (var item in allLeagueNpc)
            {
                LeagueNpc entity = new LeagueNpc();
                if (!config.LeagueNpc.ContainsKey(item.LeagueId))
                    config.LeagueNpc.Add(item.LeagueId, new List<LeagueNpc>());
                entity.LeagueId = item.LeagueId;
                entity.Logo = item.TeamId+"";
                entity.Name = item.TeamName;
                entity.TeamId = item.TeamId;
                config.LeagueNpc[item.LeagueId].Add(entity);
            }

            config.LeagueStar = new Dictionary<int, List<ConfigLeaguestarEntity>>();
            var allStarPrize = ConfigLeaguestarMgr.GetAll();
            foreach (var item in allStarPrize)
            {
                if (!config.LeagueStar.ContainsKey(item.LeagueId))
                    config.LeagueStar.Add(item.LeagueId, new List<ConfigLeaguestarEntity>());
                config.LeagueStar[item.LeagueId].Add(item);
            }

            config.LeagueFightMap = new Dictionary<int, List<ConfigLeaguefightmapEntity>>();
            var allFightMap = ConfigLeaguefightmapMgr.GetAll();
            foreach (var item in allFightMap)
            {

                if (!config.LeagueFightMap.ContainsKey(item.TemplateId))
                    config.LeagueFightMap.Add(item.TemplateId, new List<ConfigLeaguefightmapEntity>());
                config.LeagueFightMap[item.TemplateId].Add(item);
            }
            config.RevelationList = new List<RevelationMarkEntity>();
            var allRevelation = ConfigRevelationMgr.GetAll();
            foreach (var item in allRevelation)
            {
                RevelationMarkEntity entity = new RevelationMarkEntity();
                entity.Describe = item.Describe;
                entity.FirstPassItem = item.FirstPassItem;
                entity.Formation = item.Formation;
                entity.MarkId = item.MarkId;
                entity.MarkPlayer = item.MarkPlayer;
                entity.OpponentFormation = item.OpponentFormation;
                entity.OpponentTeamName = item.OpponentTeamName;
                entity.PassPrizeItems = item.PassPrizeItem;
                entity.Schedule = item.Schedule;
                entity.Story = item.Story;
                entity.TeamName = item.TeamName;
                config.RevelationList.Add(entity);
            }

            var allCoach = ConfigCoachinfoMgr.GetAll();
            var allCoachSkill = ConfigCoachskillMgr.GetAll();
            var allupgrade = ConfigCoachupgradeMgr.GetAll();
            var allStar = ConfigCoachstarMgr.GetAll();
            List<CoachInfoEntity> coachinfoList = new List<CoachInfoEntity>();
            foreach (var item in allCoach)
            {
                var coachSkill = allCoachSkill.Find(r => r.CoachId == item.Idx);
                CoachInfoEntity info = new CoachInfoEntity();
                info.Base0 = coachSkill.Base0.ToString();
                info.Base1 = coachSkill.Base1.ToString();
                info.BodyAttr = item.BodyAttr;
                info.Cd = coachSkill.CD.ToString();
                info.CoachId = item.Idx;
                info.DebrisCode = item.DebrisCode;
                info.Defense = item.Defense;
                info.Description = coachSkill.Description;
                info.Goalkeeping = item.Goalkeeping;
                info.IsSkill = item.IsSkill;
                info.Name = item.Name;
                info.Offensive = item.Offensive;
                info.Organizational = item.Organizational;
                info.Plus0 = coachSkill.Plus0.ToString();
                info.Plus1 = coachSkill.Plus1.ToString();
                info.PlusDescription = coachSkill.PlusDescription;
                info.SkillId = item.SkillId;
                info.SkillName = coachSkill.SkillName;
                info.TimeOfDuration = coachSkill.TimeOfDuration;
                info.TriggerCondition = coachSkill.TriggerCondition;
                info.TriggerProbability = coachSkill.TriggerProbability;
                coachinfoList.Add(info);
            }
            config.CoachInfo = coachinfoList;

            List<CoachUpgradeEntity> coachUpgradeList = new List<CoachUpgradeEntity>();
            foreach (var item in allupgrade)
            {
                CoachUpgradeEntity entity = new CoachUpgradeEntity();
                entity.Level = item.Level;
                entity.UpgradeExp = item.UpgradeExp;
                entity.UpgradeSkillCoin = item.UpgradeSkillCoin;
                coachUpgradeList.Add(entity);
            }
            config.CoachUpgrade = coachUpgradeList;

            List<CoachStarEntity> coachStarList = new List<CoachStarEntity>();
            foreach (var item in allStar)
            {
                CoachStarEntity entity = new CoachStarEntity();
                entity.CoachId = item.CoachId;
                entity.ConsumeDebris = item.ConsumeDebris;
                var info = allCoach.Find(r => r.Idx == item.CoachId);
                entity.CosumeDebrisCode = info.DebrisCode;
                entity.MaxSkillLevel = item.MaxLevel;
                entity.StarLevel = item.StarLevel;
                coachStarList.Add(entity);
            }
            config.CoachStar = coachStarList;

            return config;
        }
        #endregion

        #region ExportDescriptionDic1
        //public static DescriptionConfigPlayerKpiEntity ExportPlaryKpi()
        //{
        //    var config = new DescriptionConfigPlayerKpiEntity();
        //    var player = DicPlayerMgr.GetAll();

        //    var result = player.FindAll(r => r.Kpi >= 80 && r.Kpi <= 108);
        //    List<PlayerKpiEntity> list = new List<PlayerKpiEntity>();
        //    foreach (var item in result)
        //    {
        //        PlayerKpiEntity entity = new PlayerKpiEntity();
        //        entity.PlayerId = item.Idx;
        //        entity.Name = item.Name;
        //        entity.Kpi = item.Kpi;
        //        list.Add(entity);
        //    }
        //    config.Player = list.OrderByDescending(r => r.Kpi).ToList();
        //    return config;
        //}
        #endregion

        #region ExportNameLibrary
        public static NameLibraryEntity ExportNameLibrary()
        {
            var tips = new NameLibraryEntity();
            tips.Prefix = DicNameprefixMgr.GetAll();
            tips.Suffix = DicNamesuffixMgr.GetAll();
            tips.PlayerNames = TemplateRegisterMgr.GetPlayerNameList();

            return tips;
        }
        #endregion

        #region NpcConfig
        //public static NpcDicEntity ExportNpcConfig()
        //{
        //    var config = new NpcDicEntity();
        //    var list = DicNpcMgr.GetAll();
        //    config.NpcList =new List<Match_FightManagerinfo>(list.Count);
        //    foreach (var entity in list)
        //    {
        //        config.NpcList.Add(MatchDataHelper.GetFightinfo(entity));
        //    }
        //    return config;
        //}
        #endregion

        static string GetAppSettingName(string key,List<ConfigAppsettingEntity> appsettings )
        {
            var aaa = appsettings.Find(d => d.Key == key);
            if (aaa != null)
                return aaa.Value;
            else
            {
                return "";
            }
        }

        static string GetNodeDescription(XmlNode node)
        {
            string str3 = "";
            if (node.ChildNodes.Count > 0)
            {
                var node2 = node.ChildNodes[0];
                if ((node2 != null) && (node2.ChildNodes.Count > 0))
                {
                    node2 = node2.ChildNodes[0];
                }
                if ((node2 != null) && (node2.Value != null))
                {
                    str3 = node2.Value.Replace("\r\n", "").Replace(" ", "");
                }
            }
            return str3;
        }
    }
}
