using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Games.NBall.UAFacade.UABll
{
    public interface UASoGouInterface
    {
        /// <summary>
        /// 查询在线人数
        /// </summary>
        void SelectPeople();
        /// <summary>
        /// 创建删除角色
        /// </summary>
        void CreateManager();
    }
}
