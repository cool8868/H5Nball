using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Shapes;
using Games.NBall.Common;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;
using Helpers;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Games.NBall.WpfEmulator.Tools
{
    /// <summary>
    /// DataImportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataImportWindow : Window
    {
        private OpenFileDialog _dialog = new OpenFileDialog();
        private DataTable _dataTable;
        private int _maxCount = 0;
        XSSFWorkbook _hssfworkbook;
        private bool _isLoad = false;
        private string _connectionString;

        public DataImportWindow()
        {
            InitializeComponent();
        }


        #region Event

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            if(_isLoad)
                return;
            _dialog.Filter = "配置文件(*.xls,*.xlsx,*.xlsm)|*.xls;*.xlsx;*.xlsm";
            _dialog.FileOk += OpenFileDialog_Ok;
            _dialog.Multiselect = false;
            _connectionString =
                ConfigurationManager.ConnectionStrings["Games.NBall.Dal.Properties.Settings.NB_ConfigConnectionString"]
                    .ConnectionString;

            var databases = SqlHelper.GetDatabases(_connectionString);
            //List<KeyValueComboBoxItem> list = new List<KeyValueComboBoxItem>(databases.Length);
            //list.Add(new KeyValueComboBoxItem("", "请选择数据库"));
            //for (int i = 0; i < databases.Length; i++)
            //{
            //    list.Add(new KeyValueComboBoxItem(databases[i], databases[i]));
            //}
            //cmbDatabase.ItemsSource = list;
            //cmbDatabase.SelectedIndex = 0;

            cmbTable.Items.Clear();
            cmbTable.Items.Add("请选择数据库");

            _isLoad = true;
        }

        private void btnFindFile_Click(object sender, RoutedEventArgs e)
        {
            _dialog.ShowDialog();
        }

        /// <summary>
        /// Invokes while the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileDialog_Ok(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(_dialog.FileName))
            {
                MessageBox.Show("请先选择文件");
            }

            try
            {
                txtFileName.Text = _dialog.FileName;
                using (FileStream file = new FileStream(this.txtFileName.Text, FileMode.Open, FileAccess.Read))
                {
                    _hssfworkbook = new XSSFWorkbook(file);
                }
                var sheetCount = _hssfworkbook.NumberOfSheets;
                //List<KeyValueComboBoxItem> list = new List<KeyValueComboBoxItem>(sheetCount);
                //list.Add(new KeyValueComboBoxItem(-1,"请选择Sheet"));
                //for (int i = 0; i < sheetCount; i++)
                //{
                //    list.Add(new KeyValueComboBoxItem(i,_hssfworkbook.GetSheetAt(i).SheetName));
                //}
                //cmbSheet.ItemsSource = list;
                //cmbSheet.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                LogHelper.Insert(ex);
                MessageBox.Show("打开文件失败！");
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            BatchImport();
        }

        private void CmbSheet_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoad)
            {
                int index = ComboBoxHelper.GetSelectValueInt(cmbSheet);
                if (index >= 0)
                {
                    ISheet sheet = _hssfworkbook.GetSheetAt(index);
                    BindData(sheet);
                }
            }
        }

        private void CmbDatabase_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                
        }

        private void CmbTable_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        private void BindData(ISheet sheet)
        {
            try
            {
                _dataTable = null;
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                while (rows.MoveNext())
                {
                    IRow row = (XSSFRow)rows.Current;
                    if (_dataTable == null)
                    {
                        _dataTable = new DataTable();
                        for (int i = 0; i < row.LastCellNum; i++)
                        {
                            _dataTable.Columns.Add(row.GetCell(i).StringCellValue);
                        }
                    }
                    else
                    {
                        DataRow dr = _dataTable.NewRow();

                        for (int i = 0; i < row.LastCellNum; i++)
                        {
                            ICell cell = row.GetCell(i);


                            if (cell == null)
                            {
                                dr[i] = null;
                            }
                            else
                            {
                                if (cell.CellType == CellType.Formula)
                                {
                                    dr[i] = cell.NumericCellValue.ToString("f2");
                                }
                                else
                                {
                                    dr[i] = cell.ToString();
                                }
                            }
                        }
                        _dataTable.Rows.Add(dr);
                    }

                }
                if (_dataTable == null)
                    this.datagrid1.ItemsSource = null;
                else
                {
                    this.datagrid1.ItemsSource = _dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BatchImport()
        {
            if (_dataTable == null)
            {
                MessageBox.Show("数据源为空.");
                return;
            }
        }

        const string DEFSTR = "";
        static T GetDefault<T>()
        {
            T t = default(T);
            //如果是字符串类型，则返回一个空字符串
            if (DEFSTR is T)
            {
                return (T)((object)DEFSTR);
            }
            return t;
        }

         private void bulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
         {
             progress1.Value = e.RowsCopied;
             lblProgress.Content = "进度 " + e.RowsCopied + "/" + _maxCount;
         }
    }
}
