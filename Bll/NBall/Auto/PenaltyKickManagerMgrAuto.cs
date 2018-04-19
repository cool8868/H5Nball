
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
    /// PenaltykickManager管理类
    /// </summary>
    public static partial class PenaltykickManagerMgr
    {
        
		#region  GetById
		
        public static PenaltykickManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new PenaltykickManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<PenaltykickManagerEntity> GetAll(string zoneId="")
        {
            var provider = new PenaltykickManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetNotPrize
		
        public static List<PenaltykickManagerEntity> GetNotPrize( System.Int32 seasonId,string zoneId="")
        {
            var provider = new PenaltykickManagerProvider(zoneId);
            return provider.GetNotPrize( seasonId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            PenaltykickManagerProvider provider = new PenaltykickManagerProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
		#region  SetRank
		
        public static bool SetRank (DbTransaction trans=null,string zoneId="")
        {
            PenaltykickManagerProvider provider = new PenaltykickManagerProvider(zoneId);

            return provider.SetRank(trans);
            
        }
		
		#endregion
        
		#region  AddGameCurrency
		
        public static bool AddGameCurrency ( System.Guid managerId, System.Int32 number,DbTransaction trans=null,string zoneId="")
        {
            PenaltykickManagerProvider provider = new PenaltykickManagerProvider(zoneId);

            return provider.AddGameCurrency( managerId, number,trans);
            
        }
		
		#endregion
        
		#region  InsertRecord
		
        public static bool InsertRecord ( System.Guid managerId, System.Int32 number, System.Boolean isFree,DbTransaction trans=null,string zoneId="")
        {
            PenaltykickManagerProvider provider = new PenaltykickManagerProvider(zoneId);

            return provider.InsertRecord( managerId, number, isFree,trans);
            
        }
		
		#endregion
        
		#region  AddSystemProduceGameCurrency
		
        public static bool AddSystemProduceGameCurrency ( System.Guid managerId, System.Int32 number,ref  System.Int32 addSuccessNumber,DbTransaction trans=null,string zoneId="")
        {
            PenaltykickManagerProvider provider = new PenaltykickManagerProvider(zoneId);

            return provider.AddSystemProduceGameCurrency( managerId, number,ref  addSuccessNumber,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(PenaltykickManagerEntity penaltykickManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PenaltykickManagerProvider(zoneId);
            return provider.Insert(penaltykickManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(PenaltykickManagerEntity penaltykickManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PenaltykickManagerProvider(zoneId);
            return provider.Update(penaltykickManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
