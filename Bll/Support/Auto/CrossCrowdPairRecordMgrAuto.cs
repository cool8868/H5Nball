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
    /// CrosscrowdPairrecord管理类
    /// </summary>
    public static partial class CrosscrowdPairrecordMgr
    {
        
		#region  GetById
		
        public static CrosscrowdPairrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrosscrowdPairrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrosscrowdPairrecordEntity> GetAll(string zoneId="")
        {
            var provider = new CrosscrowdPairrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrosscrowdPairrecordProvider provider = new CrosscrowdPairrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrosscrowdPairrecordEntity crosscrowdPairrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdPairrecordProvider(zoneId);
            return provider.Insert(crosscrowdPairrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrosscrowdPairrecordEntity crosscrowdPairrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdPairrecordProvider(zoneId);
            return provider.Update(crosscrowdPairrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
