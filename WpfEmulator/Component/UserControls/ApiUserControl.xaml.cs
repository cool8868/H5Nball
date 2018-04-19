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
using Games.NBall.Entity;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;
using ProtoBuf;

namespace Games.NBall.WpfEmulator.Component.UserControls
{
    /// <summary>
    /// ApiUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ApiUserControl : UserControl
    {
        private int _requestIndex = 1;
        public ApiUserControl()
        {
            InitializeComponent();
        }

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            var module = ComboBoxHelper.GetSelectValue(cmbModule);
            var action = ComboBoxHelper.GetSelectValue(cmbAction);
            var parameters = CacheHelper.Instance.RequestApiDic[module][action];
            WpfRequestParameter parameter = new WpfRequestParameter();
            if (parameters != null)
            {
                foreach (var child in gridParameter.Children)
                {
                    var type = child.GetType();
                    if (type.Name == "TextBox")
                    {
                        var box = child as TextBox;
                        string x = box.Name;
                        parameter.Add(x, box.Text);
                    }
                }
            }

            if (module == "MatchData" && action == "getprocess")
            {
                int code;
                var process = RequestHelper.GetProcess(module, action, parameter, out code);
                lstResponse.Items.Add("-----+++++ " + _requestIndex + " +++++-----");
                lstResponse.Items.Add("get process,code:" + code);
                lstResponse.Items.Add("");
            }
            else
            {
                var resp = RequestHelper.Request(module, action, parameter);
                lstResponse.Items.Add("-----+++++ " + _requestIndex + " +++++-----");
                lstResponse.Items.Add(resp);
                lstResponse.Items.Add("");
            }

            _requestIndex++;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            ComboBoxHelper.BindComboBox(cmbModule, CacheHelper.Instance.ModuleCombo);
        }

        private void cmbModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var module = ComboBoxHelper.GetSelectValue(cmbModule);

            var moduleEntity = CacheHelper.Instance.GetModule(module);
            lblModuleState.Content = GetStateStr(moduleEntity.Status);
            if (CacheHelper.Instance.ActionComboDic.ContainsKey(module))
            {
                ComboBoxHelper.BindComboBox(cmbAction, CacheHelper.Instance.ActionComboDic[module]);
                btnRequest.IsEnabled = true;
            }
            else
            {
                cmbAction.ItemsSource = null;
                btnRequest.IsEnabled = false;
            }
        }

        private void cmbAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var module = ComboBoxHelper.GetSelectValue(cmbModule);
            var action = ComboBoxHelper.GetSelectValue(cmbAction);
            if (string.IsNullOrEmpty(action))
                return;
            Dictionary<string, RequestConfigParameterEntity> parameters = null;
            if (CacheHelper.Instance.RequestApiDic.ContainsKey(module)
                && CacheHelper.Instance.RequestApiDic[module].ContainsKey(action))
                parameters = CacheHelper.Instance.RequestApiDic[module][action];

            var actionEntity = CacheHelper.Instance.GetAction(module, action);
            txtMemo.Text = actionEntity.Memo;
            lblActionState.Content = GetStateStr(actionEntity.Status);
            gridParameter.RowDefinitions.Clear();
            gridParameter.Children.Clear();
            if (parameters != null)
            {
                int index = 0;
                foreach (var entity in parameters)
                {
                    gridParameter.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                    Label label = new Label()
                    {
                        Content = entity.Value.Description + "：",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        ToolTip = entity.Value.Description,
                    };
                    label.SetValue(Grid.RowProperty, index);
                    label.SetValue(Grid.ColumnProperty, 0);
                    TextBox textBox = new TextBox()
                    {
                        Name = entity.Value.Name,
                        Text = entity.Value.Eg,
                    };
                    textBox.SetValue(Grid.RowProperty, index);
                    textBox.SetValue(Grid.ColumnProperty, 1);
                    gridParameter.Children.Add(label);
                    gridParameter.Children.Add(textBox);
                    index++;
                }
            }
        }

        string GetStateStr(int state)
        {
            switch (state)
            {
                case 1:
                    return "(开发中)";

                case 2:
                    return "(完成)";

                default:
                    return "(设计中)";

            }
        }
    }
}
