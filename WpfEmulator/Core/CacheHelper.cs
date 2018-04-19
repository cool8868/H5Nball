using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Core
{
    /// <summary>
    /// 负责获取静态数据
    /// </summary>
    public class CacheHelper
    {
        #region .ctor
        private static CacheHelper _instance = null;
        private static object _lockObj = new object();

        private CacheHelper()
        {
            InitCache();
        }
        #endregion

        #region Facade
        public static CacheHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                            _instance = new CacheHelper();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// module 下拉列表
        /// </summary>
        public List<KeyValueComboBoxItem> ModuleCombo { get; private set; }
        /// <summary>
        /// action下拉字典
        /// module->action下拉列表
        /// </summary>
        public Dictionary<string, List<KeyValueComboBoxItem>> ActionComboDic { get; private set; }

        /// <summary>
        /// module字典
        /// modulename->entity
        /// </summary>
        public Dictionary<string, RequestConfigModuleEntity> ModuleDic { get; private set; }

        /// <summary>
        /// action字典
        /// modulename->actionname->entity
        /// </summary>
        public Dictionary<string, Dictionary<string, RequestConfigActionEntity>> ActionDic { get; private set; }

        /// <summary>
        /// request api 字典
        /// modulename -> action name->parameter->entity
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, RequestConfigParameterEntity>>> RequestApiDic { get; private set; }

        /// <summary>
        /// 枚举消息字典
        /// </summary>
        public Dictionary<int, string> MessageCodeDic { get; private set; }
        /// <summary>
        /// 球员位置字典
        /// </summary>
        public Dictionary<int, string> MessagePositionDic { get; private set; }
        /// <summary>
        /// 球员属性字典
        /// </summary>
        public Dictionary<int, string> MessagePropertyDic { get; private set; }
        /// <summary>
        /// 物品类型字典
        /// </summary>
        public Dictionary<int, string> MessageItemTypeDic { get; private set; }

        public Dictionary<int, string> MessagePlayerCardLevelDic { get; private set; }

        public Dictionary<int, string> MessageEquipmentQuarityDic { get; private set; }

        public Dictionary<int, string> MessageBallsoulColorDic { get; private set; }

        public Dictionary<int, string> MessageTrainStateDic { get; private set; }
        public Dictionary<int, string> MessageCurrencyTypeDic { get; private set; }
        public Dictionary<int, string> MessageWinTypeDic { get; private set; }
        /// <summary>
        /// 阵型配置
        /// </summary>
        public FormationConfigEntity FormationConfig { get; private set; }
        /// <summary>
        /// 描述字典
        /// </summary>
        public DescriptionConfigEntity DescriptionConfig { get; private set; }


        public Dictionary<string, DicManagertalenttipsEntity> Talent
        {
            get;
            set;
        }
        /// <summary>
        /// 低级意志
        /// </summary>
        public Dictionary<string, DicManagerwilltipsEntity> Will
        {
            get;
            set;
        }

        public RequestConfigActionEntity GetAction(string module, string action)
        {
            if (ActionDic.ContainsKey(module))
            {
                if (ActionDic[module].ContainsKey(action))
                    return ActionDic[module][action];
            }
            return null;
        }

        public RequestConfigModuleEntity GetModule(string module)
        {
            if (ModuleDic.ContainsKey(module))
            {
                return ModuleDic[module];
            }
            return null;
        }

        public void RefreshFormation(FormationConfigEntity config)
        {
            FormationConfig = config;
        }

        public void RefreshMessage(MessageConfigEntity messageConfig)
        {
            MessageCodeDic = messageConfig.MessageCode.ToDictionary(d => d.Code, d => d.Description);
            //MessagePositionDic = messageConfig.EnumPosition.ToDictionary(d => d.Code, d => d.Description);
            //MessagePropertyDic = messageConfig.EnumProperty.ToDictionary(d => d.Code, d => d.Description);
            //MessageItemTypeDic = messageConfig.EnumItemType.ToDictionary(d => d.Code, d => d.Description);
            //MessagePlayerCardLevelDic = messageConfig.EnumPlayerCardLevel.ToDictionary(d => d.Code, d => d.Description);
            //MessageBallsoulColorDic = messageConfig.EnumBallsoulColor.ToDictionary(d => d.Code, d => d.Description);
            //MessageEquipmentQuarityDic = messageConfig.EnumEquipmentQuarity.ToDictionary(d => d.Code, d => d.Description);
            //MessageTrainStateDic = messageConfig.EnumTrainState.ToDictionary(d => d.Code, d => d.Description);
            //MessageCurrencyTypeDic = messageConfig.EnumCurrencyType.ToDictionary(d => d.Code, d => d.Description);
            //MessageWinTypeDic = messageConfig.EnumWinType.ToDictionary(d => d.Code, d => d.Description);
        }
        #endregion

        #region encapsulation

        private void InitCache()
        {
            #region init request
            var requestConfig = EmulatorHelper.LoadXml<RequestConfigEntity>(EmulatorHelper.ApiConfigFileName);
            RequestApiDic = new Dictionary<string, Dictionary<string, Dictionary<string, RequestConfigParameterEntity>>>();
            ModuleCombo = new List<KeyValueComboBoxItem>();
            ActionComboDic = new Dictionary<string, List<KeyValueComboBoxItem>>();
            ActionDic = new Dictionary<string, Dictionary<string, RequestConfigActionEntity>>();
            ModuleDic = new Dictionary<string, RequestConfigModuleEntity>();

            foreach (var module in requestConfig.Modules)
            {
                ModuleDic.Add(module.Name, module);
                RequestApiDic.Add(module.Name, new Dictionary<string, Dictionary<string, RequestConfigParameterEntity>>());
                ModuleCombo.Add(new KeyValueComboBoxItem(module.Name, BuildContent(module.Name, module.Description)));
                ActionDic.Add(module.Name, new Dictionary<string, RequestConfigActionEntity>());
                ActionComboDic.Add(module.Name, new List<KeyValueComboBoxItem>());
                foreach (var action in module.Actions)
                {
                    ActionDic[module.Name].Add(action.Name, action);
                    RequestApiDic[module.Name].Add(action.Name, new Dictionary<string, RequestConfigParameterEntity>());

                    ActionComboDic[module.Name].Add(new KeyValueComboBoxItem(action.Name, BuildContent(action.Name, action.Description)));
                    foreach (var parameter in action.Parameters)
                    {
                        RequestApiDic[module.Name][action.Name].Add(parameter.Name, parameter);
                    }
                }
            }
            #endregion

            #region init Message config
            var messageConfig = EmulatorHelper.LoadConfig<MessageConfigEntity>(EmulatorHelper.MessageFileName);
            if (messageConfig == null)
            {
                messageConfig = DataExport.ExportMesseageConfig();
                EmulatorHelper.SaveConfig<MessageConfigEntity>(messageConfig, EmulatorHelper.MessageFileName);
            }

            RefreshMessage(messageConfig);
            #endregion

            #region init Formation config
            FormationConfig = EmulatorHelper.LoadConfig<FormationConfigEntity>(EmulatorHelper.FormationFileName);
            if (FormationConfig == null)
            {
            }

            #endregion

            #region init Description config
            DescriptionConfig = EmulatorHelper.LoadConfig<DescriptionConfigEntity>(EmulatorHelper.DescriptionDicFileName);
            if (DescriptionConfig == null)
            {
            }
            #endregion

        }

        string BuildContent(string name, string description)
        {
            return string.Format("{0}<{1}>", description, name);
        }
        #endregion
    }
}
