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
    /// DicLadderexchange管理类
    /// </summary>
    public static partial class DicLadderexchangeMgr
    {
        
		#region  GetById
		
        public static DicLadderexchangeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicLadderexchangeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicLadderexchangeEntity> GetAll(string zoneId="")
        {
            var provider = new DicLadderexchangeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicLadderexchangeProvider provider = new DicLadderexchangeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicLadderexchangeEntity dicLadderexchangeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicLadderexchangeProvider(zoneId);
            return provider.Insert(dicLadderexchangeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicLadderexchangeEntity dicLadderexchangeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicLadderexchangeProvider(zoneId);
            return provider.Update(dicLadderexchangeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
