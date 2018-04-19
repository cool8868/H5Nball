using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;

namespace Games.NBall.Bll.Frame
{
    public class ManagerUtil
    {
        public static ManagerSkillUseWrap GetSkillUseWrap(Guid managerId, string siteId = "")
        {
            var use = ManagerskillUseMgr.GetById(managerId, siteId);
            if (null == use)
            {
                use = new ManagerskillUseEntity()
                {
                    ManagerId = managerId,
                    SyncFlag = 1,
                    PlayerSkills = string.Empty,
                    ManagerSkills = string.Empty,
                    Talents = string.Empty,
                    Wills = string.Empty,
                    RowVersion = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 1 }
                };
            }
            return new ManagerSkillUseWrap(use);
        }

        /// <summary>
        /// 获取已经学习到的天赋和意志
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public static ManagerSkillLibWrap GetSkillLibWrap(Guid managerId, string siteId = "")
        {
            var lib = ManagerskillLibMgr.GetById(managerId, siteId);
            if (null == lib)
            {
                lib = new ManagerskillLibEntity()
                {
                    ManagerId = managerId,
                    SyncTalentPoint = 0,
                    MaxTalentPoint = 0,
                    NodoTalents = string.Empty,
                    TodoTalents = string.Empty,
                    NodoWills = string.Empty,
                    TodoWills = string.Empty,
                };
            }
            lib.MaxWillNumber = FrameConfig.MAXWillNumber;
            return new ManagerSkillLibWrap(lib);
        }
       
    }

   
}
