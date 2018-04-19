using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.AdminWeb.AdminEntity;

namespace Games.NBall.AdminWeb.GmTools
{
    public partial class TransferZone : System.Web.UI.Page
    {
        private AccountData _accountDataOld;
        private AccountData _accountDataNew;
        private string _zoneIdOld;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControl();
            }
        }

        void BindControl()
        {
            SZoneOld.DataSource = ConnectionFactory.Instance.GetZoneList(this.User.Identity.Name);
            SZoneOld.DataTextField = "Text";
            SZoneOld.DataValueField = "Value";
            SZoneOld.DataBind();
            if (!string.IsNullOrEmpty(_zoneIdOld))
                SZoneOld.SelectedValue = _zoneIdOld;
            else
            {
                SZoneOld.SelectedIndex = 0;
            }
            SZoneNew.DataSource = ConnectionFactory.Instance.GetZoneList(this.User.Identity.Name);
            SZoneNew.DataTextField = "Text";
            SZoneNew.DataValueField = "Value";
            SZoneNew.DataBind();
            if (!string.IsNullOrEmpty(_zoneIdNew))
                SZoneNew.SelectedValue = _zoneIdNew;
            else
            {
                SZoneNew.SelectedIndex = 0;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!CheckRoleName())
                return;
        }

        private bool CheckRoleName()
        {
            if (!GetManagerByName(txtOldRoleName.Text.Trim(), ZoneIdOld, true))
                return false;
            if (!GetManagerByName(txtNewRoleName.Text.Trim(), ZoneIdNew, true))
                return false;
            return true;
        }

        //private bool CopyData()
        //{
        //    int returnValue = 0 ;
        //    string returnMsg = string.Empty;
            
            
        //    NbManagerMgr.TransferZoneByAccount("", _accountDataOld.Account, _accountDataOld.Name, _accountDataOld.ManagerId.ToString(), _accountDataOld.Mod.ToString(), _accountDataNew.Account,
        //        _accountDataNew.Name, _accountDataNew.ManagerId.ToString(), _accountDataNew.Mod.ToString(),  ref returnValue,  ref returnMsg);
            






        //    return true;
        //}

        private bool GetManagerByName(string name, string zoneId, bool oldOrNew)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var manager = NbManagerMgr.GetByName(name, zoneId);
                if (manager != null)
                {
                    SetAccount(manager, zoneId,oldOrNew);
                    return true;
                }
            }
            return false;
        }
        void SetAccount(NbManagerEntity manager, string zoneId,bool oldOrNew)
        {
            if (manager == null)
            {
                ltlMessage.Text = "找不到经理信息";
                return;
            }
            if (oldOrNew)
            {
                if (_accountDataOld == null)
                    _accountDataOld = new AccountData();
                _accountDataOld.ZoneId = zoneId;
                _accountDataOld.ManagerId = manager.Idx;
                _accountDataOld.Name = manager.Name;
                _accountDataOld.Account = manager.Account;
                _accountDataOld.Mod = manager.Mod;
                //BindData();
                //Session[AccountKey] = _accountData;
            }
            else
            {
                if (_accountDataNew == null)
                    _accountDataNew = new AccountData();
                _accountDataNew.ZoneId = zoneId;
                _accountDataNew.ManagerId = manager.Idx;
                _accountDataNew.Name = manager.Name;
                _accountDataNew.Account = manager.Account;
                _accountDataNew.Mod = manager.Mod;
            }
        }

        public string ZoneIdOld
        {
            get
            {
                _zoneIdOld = SZoneOld.SelectedItem.Value;
                return _zoneIdOld;
            }
            set
            {
                SZoneOld.SelectedValue = value;
            }
        }
        private string _zoneIdNew;
        public string ZoneIdNew
        {
            get
            {
                _zoneIdNew = SZoneNew.SelectedItem.Value;
                return _zoneIdNew;
            }
            set
            {
                SZoneNew.SelectedValue = value;
            }
        }

        void ShowMessage(string msg)
        {
            ltlMessage.Text = msg;
        }
    }
}