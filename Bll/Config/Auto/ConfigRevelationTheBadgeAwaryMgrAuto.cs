﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    /// <summary>
    /// ConfigRevelationthebadgeawary管理类
    /// </summary>
    public static partial class ConfigRevelationthebadgeawaryMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationthebadgeawaryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigRevelationthebadgeawaryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationthebadgeawaryEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationthebadgeawaryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationthebadgeawaryProvider provider = new ConfigRevelationthebadgeawaryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationthebadgeawaryEntity configRevelationthebadgeawaryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationthebadgeawaryProvider(zoneId);
            return provider.Insert(configRevelationthebadgeawaryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationthebadgeawaryEntity configRevelationthebadgeawaryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationthebadgeawaryProvider(zoneId);
            return provider.Update(configRevelationthebadgeawaryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

