
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
    /// NbSolution管理类
    /// </summary>
    public static partial class NbSolutionMgr
    {
        
		#region  GetById
		
        public static NbSolutionEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new NbSolutionProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbSolutionEntity> GetAll(string zoneId="")
        {
            var provider = new NbSolutionProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            NbSolutionProvider provider = new NbSolutionProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbSolutionEntity nbSolutionEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbSolutionProvider(zoneId);
            return provider.Insert(nbSolutionEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbSolutionEntity nbSolutionEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbSolutionProvider(zoneId);
            return provider.Update(nbSolutionEntity,trans);
        }
		
		#endregion	
		
		
	}
}

