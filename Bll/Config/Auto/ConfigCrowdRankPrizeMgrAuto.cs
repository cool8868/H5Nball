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
    /// ConfigCrowdrankprize管理类
    /// </summary>
    public static partial class ConfigCrowdrankprizeMgr
    {
        
		#region  GetById
		
        public static ConfigCrowdrankprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigCrowdrankprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigCrowdrankprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigCrowdrankprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigCrowdrankprizeProvider provider = new ConfigCrowdrankprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigCrowdrankprizeEntity configCrowdrankprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrowdrankprizeProvider(zoneId);
            return provider.Insert(configCrowdrankprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigCrowdrankprizeEntity configCrowdrankprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrowdrankprizeProvider(zoneId);
            return provider.Update(configCrowdrankprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
