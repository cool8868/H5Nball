using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class ServicetionSectionCache
    {
        private Dictionary<string, int> _serviceDic;
        #region .ctor
        private ServicetionSectionCache()
        {
            InitCache();
            _initFlag = true;
        }
        #endregion

        #region Facade

        #region Instance
        static readonly object _lockObj = new object();
        static volatile ServicetionSectionCache _instance = null;
        public readonly bool _initFlag = false;
        public static ServicetionSectionCache Instance
        {
            get
            {
                if (null == _instance || !_instance._initFlag)
                {
                    lock (_lockObj)
                    {
                        if (null == _instance || !_instance._initFlag)
                        {
                            _instance = new ServicetionSectionCache();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion



        public bool HasTaskService()
        {
            return _serviceDic.ContainsKey("Games.NBall.ServiceContract.Service.TaskService");
        }

        public bool HasActivityService()
        {
            return _serviceDic.ContainsKey("Games.NBall.ServiceContract.Service.ActivityService");
        }
        public bool HasOnlineService()
        {
            return _serviceDic.ContainsKey("Games.NBall.ServiceContract.Service.OnlineService");
        }
        public bool HasService(string name)
        {
            return _serviceDic.ContainsKey(name);
        }

        #endregion

        #region encapsulation
        void InitCache()
        {
            ServicesSection serviesSection = ConfigurationManager.GetSection("system.serviceModel/services") as ServicesSection;
            _serviceDic = new Dictionary<string, int>();
            if (serviesSection != null)
            {
                for (int i = 0; i < serviesSection.Services.Count; i++)
                {
                    _serviceDic.Add(serviesSection.Services[i].Name, 1);
                }
            }
        }
        #endregion
    }
}
