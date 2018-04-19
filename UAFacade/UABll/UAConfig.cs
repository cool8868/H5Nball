using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Games.NBall.UAFacade
{
    public class UAConfig
    {
        public List<Entity.AllUaplatformEntity> Platforms { get; set; } 
    }
}