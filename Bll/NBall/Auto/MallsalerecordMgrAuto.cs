
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
    /// MallSalerecord管理类
    /// </summary>
    public static partial class MallSalerecordMgr
    {
        
		#region  GetById
		
        public static MallSalerecordEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new MallSalerecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<MallSalerecordEntity> GetAll(string zoneId="")
        {
            var provider = new MallSalerecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            MallSalerecordProvider provider = new MallSalerecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(MallSalerecordEntity mallSalerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MallSalerecordProvider(zoneId);
            return provider.Insert(mallSalerecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(MallSalerecordEntity mallSalerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MallSalerecordProvider(zoneId);
            return provider.Update(mallSalerecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

