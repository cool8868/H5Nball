using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Common;

namespace Games.NBall.AdminWeb.AdminEntity
{
    public class AccountData
    {
        public string ZoneId { get; set; }
        
        public Guid ManagerId { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public int Mod { get; set; }
    }
}