using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.AdminWeb.AdminEntity;
using Games.NBall.AdminWeb.UserControls;

namespace Games.NBall.AdminWeb.Account
{
    public partial class AccountPage : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        private bool _isSet;
        private SelectControl[] _selectControls;
        private AccountSelectControl.ClearDelegate _onclear;
        private AccountSelectControl.SelectedDelegate _onselected;
        public void SetMaster(string title, AccountSelectControl.SelectedDelegate onselected,
                              AccountSelectControl.ClearDelegate onclear, params SelectControl[] selectControls)
        {
            AccountSelectControl1.OnSelect = SelectData;
            AccountSelectControl1.OnClear = ClearData;
            AccountSelectControl1.OnLoadZone = SetSelectControlZone;
            PageTitle = title;
            _selectControls = selectControls;
            _onclear = onclear;
            _onselected = onselected;
        }

        public void SelectData()
        {
            SetSelectControlData();

            if (_onclear != null)
                _onclear();
            if (_onselected != null)
            {
                var accountData = GetAccount();
                if (accountData != null)
                {
                    _onselected();
                }
                else
                {
                    ShowMessage("请先选择经理");
                }
            }
        }

        void ClearData()
        {
            if (_selectControls != null)
            {
                if (_selectControls != null)
                {
                    foreach (var selectControl in _selectControls)
                    {
                        foreach (var whereParam in selectControl.WhereList)
                        {
                            if (whereParam.IsDisable)
                            {
                                whereParam.DefaultValue = "clear";
                            }
                        }
                    }
                }
            }
            if (_onclear != null)
                _onclear();
        }

        public void SetSelectControlData()
        {
            if (_isSet)
                return;
            var accountData = GetAccount();
            if (accountData == null)
            {
                ShowMessage("请先选择经理.");
                return;
            }

            SetManager(accountData.ManagerId.ToString(),accountData.Account);
            SetZone(accountData.ZoneId);
        }

        public void SetSelectControlZone()
        {
            SetZone(AccountSelectControl1.ZoneId);
        }

        public void SetManager(string managerId,string account,bool isClear=false)
        {
            if(_isSet)
                return;
            if (_selectControls != null)
            {
                foreach (var selectControl in _selectControls)
                {
                    foreach (var whereParam in selectControl.WhereList)
                    {
                        if (whereParam.FieldNameN.ToLower().Contains("managerid"))
                        {
                            whereParam.FieldValue = managerId;
                            if (isClear)
                            {
                                whereParam.DefaultValue = "clear";
                            }
                        }
                        else if (whereParam.FieldNameN.ToLower().Contains("account"))
                        {
                            whereParam.FieldValue = account;
                            if (isClear)
                            {
                                whereParam.DefaultValue = "clear";
                            }
                        }
                    }
                }
            }
            _isSet = false;
        }

        void SetZone(string zoneId)
        {
            if (_selectControls != null)
            {
                foreach (var selectControl in _selectControls)
                {
                    selectControl.ZoneId = zoneId;
                }
            }
        }

        public AccountData GetAccount()
        {
            return AccountSelectControl1.GetAccount();
        }

        public void ShowMessage(string message)
        {
            ltlMessage.Text = message;
        }

        protected string PageTitle { get; set; }

        public string ZoneId { get { return AccountSelectControl1.ZoneId; } }
    }
}