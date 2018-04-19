using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.AdminWeb.Account;
using Games.NBall.AdminWeb.AdminEntity;
using Games.NBall.Bll;
using Games.NBall.Common;
using Games.NBall.Entity;

namespace Games.NBall.AdminWeb.UserControls
{
    public partial class AccountSelectControl : System.Web.UI.UserControl
    {
        private static readonly string AccountKey = "AD";
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoneControl1.OnLoadZone = OnLoadZone;
            if (!IsPostBack)
            {
                GetAccount();
                BindData();
            }
        }

        public AccountData GetAccount()
        {
            if (_accountData == null)
            {
                if (Session[AccountKey] != null)
                {
                    var data = Session[AccountKey] as AccountData;
                    if (data != null)
                    {
                        _accountData = data;
                    }
                }
            }
            return _accountData;
        }

        public string ZoneId
        {
            get { return ZoneControl1.ZoneId; }
        }

        void SetAccount(NbManagerEntity manager,string zoneId)
        {
            if (manager == null)
            {
                ltlMessage.Text = "manager is null.";
                return;
            }
            if (_accountData==null)
                _accountData=new AccountData();
            _accountData.ZoneId = zoneId;
            _accountData.ManagerId = manager.Idx;
            _accountData.Name = manager.Name;
            _accountData.Account = manager.Account;
            _accountData.Mod = manager.Mod;
            BindData();
            Session[AccountKey] = _accountData;
        }

        void BindData()
        {
            if (_accountData != null)
            {
                ZoneControl1.ZoneId = _accountData.ZoneId;
                txtAccount.Text = _accountData.Account;
                txtManagerId.Text = _accountData.ManagerId.ToString();
                txtManagerName.Text = _accountData.Name;
                if (OnSelect != null)
                    OnSelect();
            }
        }

        public delegate void SelectedDelegate();
        public SelectedDelegate OnSelect;

        public delegate void ClearDelegate();
        public ClearDelegate OnClear;

        public ZoneSelectControl.LoadZoneDelegate OnLoadZone;

        private AccountData _accountData;

        protected void btnAccountSearch_Click(object sender, EventArgs e)
        {
            var zoneId = ZoneControl1.ZoneId;
            if (string.IsNullOrEmpty(zoneId))
            {
                ltlMessage.Text = "请选择所属区.";
                return;
            }
            try
            {
                if (GetManagerByAccount(zoneId))
                    return;
                if (GetManagerByName(zoneId))
                    return;
                if (GetManagerByIdx(zoneId))
                    return;
                ltlMessage.Text = "查无此人，请检查输入.";
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ltlMessage.Text = ex.Message;
            }
        }

        protected void btnAccountClear_Click(object sender, EventArgs e)
        {
            _accountData = null;
            Session[AccountKey] = null;
            txtAccount.Text = "";
            txtManagerId.Text = "";
            txtManagerName.Text = "";
            if (OnClear != null)
                OnClear();
        }

        private bool GetManagerByAccount(string zoneId)
        {
            var account = txtAccount.Text.Trim();
            if (!string.IsNullOrEmpty(account))
            {
                var managers = NbManagerMgr.GetByAccount(account,zoneId);
                if (managers != null && managers.Count > 0)
                {
                    SetAccount(managers[0],zoneId);
                    return true;
                }
            }
            return false;
        }

        private bool GetManagerByName(string zoneId)
        {
            var name = txtManagerName.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                var manager = NbManagerMgr.GetByName(name, zoneId);
                if (manager != null)
                {
                    SetAccount(manager, zoneId);
                    return true;
                }
            }
            return false;
        }

        private bool GetManagerByIdx(string zoneId)
        {
            var idx = txtManagerId.Text.Trim();
            Guid managerId = Guid.Empty;
            Guid.TryParse(idx, out managerId);
            if (managerId!=Guid.Empty)
            {
                var manager = NbManagerMgr.GetById(managerId, zoneId);
                if (manager != null)
                {
                    SetAccount(manager, zoneId);
                    return true;
                }
            }
            return false;
        }
    }
}