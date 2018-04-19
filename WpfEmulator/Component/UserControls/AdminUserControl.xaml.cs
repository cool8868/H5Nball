using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.WpfEmulator.Core;

namespace Games.NBall.WpfEmulator.Component.UserControls
{
    /// <summary>
    /// AdminUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class AdminUserControl : UserControl
    {
        private bool _isLoad = false;
        private List<DicItemEntity> _itemList;
        public AdminUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (_isLoad)
                return;
            try
            {
                itemType = 1;
                _itemList = DicItemMgr.GetAll();
                BindItem();
            }
            catch (Exception ex)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("应用程序出现了异常，{0}\n", ex.Message);
                stringBuilder.AppendFormat("\n {0}", ex.StackTrace);
                MessageBox.Show(stringBuilder.ToString());
            }
            _isLoad = true;
        }

        #region Money
        private void btnMondy_Click(object sender, RoutedEventArgs e)
        {
            var coin = ConvertHelper.ConvertToInt(txtMoney.Text, 0);
            var code = AdminHelper.AddCoin(coin);
            if (code == MessageCode.Success)
            {
                ApiTestCore.MainWindow.RefreshManagerInfo();
                ShowMessage("加钱成功");
            }
            else
            {
                ShowMessage(EmulatorHelper.BuildErrorinfo("加钱失败", code));
            }
        }
        #endregion

        #region Sophisticate
        private void btnSophisticate_Click(object sender, RoutedEventArgs e)
        {
            var coin = ConvertHelper.ConvertToInt(txtSophisticate.Text, 0);
            var code = AdminHelper.AddSophisticate(coin);
            if (code == MessageCode.Success)
            {
                ApiTestCore.MainWindow.RefreshManagerInfo();
                ShowMessage("加阅历成功");
            }
            else
            {
                ShowMessage(EmulatorHelper.BuildErrorinfo("加阅历失败", code));
            }
        }
        #endregion

        #region Reiki
        private void btnReiki_Click(object sender, RoutedEventArgs e)
        {
            var coin = ConvertHelper.ConvertToInt(txtReiki.Text, 0);
            var code = AdminHelper.AddReiki(coin);
            if (code == MessageCode.Success)
            {
                ApiTestCore.MainWindow.RefreshManagerInfo();
                ShowMessage("加灵气成功");
            }
            else
            {
                ShowMessage(EmulatorHelper.BuildErrorinfo("加灵气失败", code));
            }
        }
        #endregion

        #region Item

        private int itemType;
        private int subType;


        private int _itemType;
        private int _subType;
        private int _thirdType;
        private void BindItem()
        {
            if (_itemType == itemType && _subType == subType)
                return;
            _itemType = itemType;
            _subType = subType;

              
        }

        private void btnItem_Click(object sender, RoutedEventArgs e)
        {

            int itemCode = ConvertHelper.ConvertToInt(txtResultItemName.Text, 0);
            if (itemCode <= 0)
            {
                ShowMessage("请先选择要添加的物品.");
                return;
            }
            int strength = ConvertHelper.ConvertToInt(txtItemStrength.Text, 1);
            int count = ConvertHelper.ConvertToInt(txtItemCount.Text, 1);
            var code = AdminHelper.AddItem(itemCode, count, strength);
            if (code == MessageCode.Success)
                ShowMessage("添加成功.");
            else
            {
                ShowMessage(EmulatorHelper.BuildErrorinfo("添加物品", code));
            }
        }

        private void btntime1_Click(object sender, RoutedEventArgs e)
        {
            long time1 = ConvertHelper.ConvertToLong(txttime1.Text, 0);
            if (time1 <= 0)
            {
                ShowMessage("请输入要对比的时间.");
                return;
            }
            txttime3.Text = ShareUtil.GetTime(time1).ToString("yyyy-MM-dd HH:mm:ss:fffff");
        }

        #endregion

        private int _index = 0;
        void ShowMessage(string msg)
        {
            _index++;
            lblMessage.Content = "(序列:" + _index + ")" + msg;
        }

        private void btnLevelup_Click(object sender, RoutedEventArgs e)
        {
            var response = AdminHelper.Levelup(ApiTestCore.CurManager.Idx);
            if (response.Code == (int)MessageCode.Success)
            {
                ApiTestCore.UpdateManagerData(response.Data);
                ShowMessage("升级成功");
            }
            else
            {
                ShowMessage(EmulatorHelper.BuildErrorinfo("升级", response.Code));
            }
        }

        private void btnClearPackage_Click(object sender, RoutedEventArgs e)
        {
            var code = AdminHelper.ClearPackage(ApiTestCore.CurManager.Idx);
            if (code == (int)MessageCode.Success)
            {
                ShowMessage("清理成功");
            }
            else
            {
                ShowMessage(EmulatorHelper.BuildErrorinfo("清理背包", code));
            }
        }

    }
}
