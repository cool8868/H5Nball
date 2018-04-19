using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Command
{
    public class WpfManagerCommand
    {
        public static string _moduleName = "Manager";

        public static NBManagerInfoResponse GetManager()
        {
            return RequestHelper.Request<NBManagerInfoResponse>(_moduleName, "managerinfo");
        }

        public static LoginResult Login(string name)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("username", name);
            return RequestHelper.Request<LoginResult>(_moduleName, "login", parameter);
        }

        public static NBManagerCreateResponse Register(string name, string area, string logo,int templateId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("name",name);
            parameter.Add("logo",logo);
            parameter.Add("areaId",area);
            parameter.Add("ti", templateId);
            return RequestHelper.Request<NBManagerCreateResponse>(_moduleName, "register", parameter);
        }

        public static MessageCodeResponse Heart()
        {
            return RequestHelper.Request<MessageCodeResponse>(_moduleName, "ho");
        }

        /// <summary>
        /// 更改经理名
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="oldName">老用户名</param>
        /// <param name="newName">新用户名</param>
        /// <returns>是否修改成功</returns>
        public static MessageCodeResponse UpdateName(System.Guid managerId, System.String oldName, System.String newName)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("g", managerId);
            parameter.Add("o", oldName);
            parameter.Add("n", newName);
            return RequestHelper.Request<MessageCodeResponse>(_moduleName, "un", parameter);
        }

        /// <summary>
        /// 删除角色--合区使用
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="bindcode">绑定码</param>
        /// <returns>是否删除成功</returns>        
        public static MessageCodeResponse DeleteRole(string account, Guid bindCode)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("b", bindCode);
            return RequestHelper.Request<MessageCodeResponse>(_moduleName, "ba", parameter);
        }

        /// <summary>
        /// 通过绑定码，把其他账号的角色复制到本账号的角色
        /// </summary>
        /// <param name="bindCode">绑定码</param>
        /// <param name="account">账号</param>
        /// <param name="name">经理名</param>
        /// <param name="managerId">经理ID</param>
        /// <param name="mod">Mod</param>
        /// <returns>是否复制成功</returns>
        public static MessageCodeResponse BindAccount(System.Guid bindCode)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("b", bindCode);           
            return RequestHelper.Request<MessageCodeResponse>(_moduleName, "ba", parameter);
        }
    }
}
