using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Weibo.Data
{
    public class WyxUserInfo
    {
        string _uid = string.Empty;
        string _uname = string.Empty;
        string _logo = string.Empty;
        string _vipFlag = string.Empty;

        public string Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        public string Uname
        {
            get { return _uname; }
            set { _uname = value; }
        }
        public string Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }
        public string VipFlag
        {
            get { return _vipFlag; }
            set { _vipFlag = value; }
        }

        public string UserData
        {
            get
            {
                return _logo;
                //return string.Format("{0}|{1}", _logo, _vipFlag);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}", Uid, Uname, Logo, VipFlag);
        }
    }
}
