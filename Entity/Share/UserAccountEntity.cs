
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NBall.Entity.Share
{
    [Serializable]
    public class UserAccountEntity
    {
        #region 私有成员
        private string _account = string.Empty;
        private Guid _managerId = Guid.Empty;
        private string _name = string.Empty;
        private int _area = 1;
        private string _certId = string.Empty;

        #endregion

        #region 构造器
        public UserAccountEntity()
        {
            
        }

        public UserAccountEntity(string account, Guid managerId, string name, int area, string paltformCode,string sessionId,string kgext="")
        {
            this.Account = account;
            this.ManagerId = managerId;
            this.Name = name;
            this.Area = area;
            this.PlatformCode = paltformCode;
            this.SessionId = sessionId;
        }
        #endregion

        public string PlatformCode { get; set; }

        /// <summary>
        /// Account
        /// </summary>
        public string Account
        {
            get { return this._account; }
            set { this._account = value; }
        }

        private string kgext = string.Empty;
        public string Kgext {
            get { return this.kgext; }
            set { this.kgext = value; }
        }

        public string SessionId { get; set; }
        /// <summary>
        /// ManagerId
        /// </summary>
        public Guid ManagerId
        {
            get { return this._managerId; }
            set { this._managerId = value; }
        }


        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <summary>
        /// Area
        /// </summary>
        public int Area
        {
            get { return this._area; }
            set { this._area = value; }
        }


        /// <summary>
        /// CertId
        /// </summary>
        public string CertId
        {
            get { return this._certId; }
            set { this._certId = value; }
        }

        public string ExtraData { get; set; }

        /// <summary>
        /// 由证件号码取得的出生日期
        /// <remarks>证件号码由pass9平台提供</remarks>
        /// </summary>
        public  DateTime BirthdayOfCert
        {
            get
            {
                var defaultDate = new DateTime(1980, 10, 5);
                var birthday = new StringBuilder();
                if (String.IsNullOrEmpty(CertId) || CertId.Trim().Length == 0)
                {
                    return defaultDate;
                }
                if (CertId.Trim().Length == 15)
                {
                    birthday = birthday.Append("19").Append(CertId.Substring(6, 2)).Append("-").Append(CertId.Substring(8, 2)).Append("-").Append(CertId.Substring(10, 2));
                }
                else if (CertId.Trim().Length == 18)
                {
                    birthday = birthday.Append(CertId.Substring(6, 4)).Append("-").Append(CertId.Substring(10, 2)).Append("-").Append(CertId.Substring(12, 2));
                }
                else
                {
                    return defaultDate;
                }
                var result = defaultDate;
                if (DateTime.TryParse(birthday.ToString(), out result))
                {
                    return result;
                }
                else
                {
                    return defaultDate;
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>
        /// </remarks>
        public override string ToString()
        {
            return String.Format("{0}&{1}&{2}&{3}&{4}&{5}&{6}", Account, ManagerId, Name, Area, PlatformCode,SessionId,ExtraData);
        }


    }
}
