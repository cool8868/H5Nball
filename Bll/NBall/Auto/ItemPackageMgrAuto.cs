
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
    /// ItemPackage管理类
    /// </summary>
    public static partial class ItemPackageMgr
    {
        
		#region  GetById
		
        public static ItemPackageEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ItemPackageProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ItemPackageEntity> GetAll(string zoneId="")
        {
            var provider = new ItemPackageProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            ItemPackageProvider provider = new ItemPackageProvider(zoneId);

            return provider.Delete( managerId, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  Update
		
        public static bool Update ( System.Guid managerId, System.Byte[] itemString, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            ItemPackageProvider provider = new ItemPackageProvider(zoneId);

            return provider.Update( managerId, itemString, rowVersion,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  AddPlayer
		
        public static bool AddPlayer ( System.Guid teammemberId, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            ItemPackageProvider provider = new ItemPackageProvider(zoneId);

            return provider.AddPlayer( teammemberId, mod,trans);
            
        }
		
		#endregion
        
		#region  DeletePlayer
		
        public static bool DeletePlayer ( System.Int32 playerId, System.Guid managerId, System.Guid teammemberId, System.Int32 strengthenLevel, System.Byte[] usedPlayerCard, System.Byte[] usedEquipment, System.Int32 level, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            ItemPackageProvider provider = new ItemPackageProvider(zoneId);

            return provider.DeletePlayer( playerId, managerId, teammemberId, strengthenLevel, usedPlayerCard, usedEquipment, level, mod,trans);
            
        }
		
		#endregion
        
		#region  Delete5Player
		
        public static bool Delete5Player ( System.Guid teammemberId1, System.Guid teammemberId2, System.Guid teammemberId3, System.Guid teammemberId4, System.Guid teammemberId5, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            ItemPackageProvider provider = new ItemPackageProvider(zoneId);

            return provider.Delete5Player( teammemberId1, teammemberId2, teammemberId3, teammemberId4, teammemberId5, mod,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ItemPackageEntity itemPackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ItemPackageProvider(zoneId);
            return provider.Insert(itemPackageEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ItemPackageEntity itemPackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ItemPackageProvider(zoneId);
            return provider.Update(itemPackageEntity,trans);
        }
		
		#endregion	
		
		
	}
}

