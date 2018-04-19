using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Games.MyControl
{
    [DefaultProperty("Text"), ToolboxData("<{0}:EditControl runat=server></{0}:EditControl>"), ParseChildren(true),
     PersistChildren(false),
     AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class EditControl : WebControl
    {
        // Fields
        private bool _addBtnVisible = true;
        private bool _editBtnVisible = true;
        private string devConnStr = "Data Source=;Initial Catalog=;Persist Security Info=True;User ID=nbuser;Password=sa";
        private List<EditFieldParam> fieldList;

        // Methods
        private FieldType GetFieldType(string type)
        {
            switch (type)
            {
                case "varchar":
                case "char":
                    return FieldType.String;

                case "nvarchar":
                case "nchar":
                    return FieldType.NString;

                case "int":
                case "tinyint":
                    return FieldType.Int;

                case "bigint":
                    return FieldType.Long;

                case "decimal":
                    return FieldType.Decimal;

                case "float":
                    return FieldType.Float;

                case "datetime":
                    return FieldType.DateTime;
            }
            return FieldType.String;
        }

        private string GetHtml(string tableName, string tableDescN, DataTable data)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(
                "<style type=\"text/css\">body, table, td, th, span, p, div, a, form, input, button, select, option, textarea { font-family : simsun; font-size : 12px }");
            builder.Append("a:link {color: #0000ea; text-decoration:none}");
            builder.Append("a:visited {color: #006666; text-decoration: none}");
            builder.Append("a:active {color:#ff0000; text-decoration: underline}");
            builder.Append("a:hover {color: #ff0000; text-decoration: underline}");
            builder.Append(
                "a.btn-i1{display:block;width:120px;text-align:center;color:#E6E956;background-color:#CCEEEE; text-decoration:none; border-left:#99A52F 1px solid; border-top:#99A52F 1px solid; border-right:#61691E 1px solid; border-bottom:#3F4414 1px solid;padding:2px 0 0 0;}");
            builder.Append("a.btn-i1:link {color: #000000; text-decoration:none}");
            builder.Append("a.btn-i1:visited {color: #006666; text-decoration: none}");
            builder.Append("a.btn-i1:active {");
            builder.Append("color:#ff0000; text-decoration: underline}");
            builder.Append("a.btn-i1:hover {color: #ff0000; text-decoration: underline}");
            builder.Append(".bottomLine { border-bottom:1px solid #000000 }");
            builder.Append(".rightLine { border-right:1px solid #000000 }");
            builder.Append(".ftGreen { color : green }");
            builder.Append(".bgColor { background-color:#33CCCC }");
            builder.Append(".bgColor1 { background-color:#CCEEEE }");
            builder.Append(".bgColor2 { background-color:#66CCFF }");
            builder.Append(".bgDark { background-color:#000000 }");
            builder.Append(".bgGray { background-color:Gray }");
            builder.Append(".bgLightGray { background-color : #DDDDDD }");
            builder.Append(".bgLightBlue { background-color : lightblue }");
            builder.Append(".bgLightGreen { background-color : lightgreen }");
            builder.Append(".bgWhite { background-color:#FFFFFF }");
            builder.Append(".buttonWidth { width: 100px }");
            builder.Append(".inputWidth { width: 200px }");
            builder.Append(
                ".SortFieldLable{text-decoration:underline;background-color:#66CCFF;color:#3366ff;cursor:hand;}");
            builder.Append(".pagebar{line-height:20px;height:20px;}");
            builder.Append(
                ".pagebar a,.pagebar .now-page{padding:1px 3px 2px 3px;margin:0 2px;text-align:center;font-weight:bold;font-family:Verdana;border:1px solid #ccc;text-decoration:none;COLOR: #006699;}");
            builder.Append(
                ".pagebar a:hover{border:1px solid #c00;text-decoration:none;BACKGROUND-COLOR: #f1ffc0;COLOR: #c00;}");
            builder.Append("a.wa {COLOR: #7c7b6b;BACKGROUND-COLOR: #f1ff00;}");
            builder.Append("a.wah {COLOR: #7c7b6b;}</style>");
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("function Edit(edit) {");
            builder.Append("document.getElementById('txtEdit').value=edit;");
            builder.Append("document.forms[0].action = location.href;");
            builder.Append("document.forms[0].method = \"post\";");
            builder.Append("document.forms[0].submit();");
            builder.Append("}");
            builder.Append("</script>");
            builder.Append("<table width=\"98%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td>");
            builder.Append("<table width=\"100%\" border=\"0\" cellpadding=\"4\" cellspacing=\"1\" class=\"bgDark\">");
            builder.Append("<tr class=\"bgLightGray\">");
            builder.Append("<td colspan=\"3\" align=\"left\">");
            if (data != null)
            {
                builder.Append("<b><font color=\"#009900\">【" + tableDescN + "编辑】</font></b>");
            }
            else
            {
                builder.Append("<b><font color=\"#009900\">【" + tableDescN + "新增】</font></b>");
            }
            builder.Append("</td>");
            builder.Append("</tr>");
            if (this.fieldList != null)
            {
                foreach (EditFieldParam param in this.fieldList)
                {
                    if ((HttpContext.Current != null) &&
                        param.FieldName.Equals(
                            HttpContext.Current.Request.Url.Query.Replace("?", "").Split(new char[] {'='})[0],
                            StringComparison.OrdinalIgnoreCase))
                    {
                        param.IsEdit = false;
                    }
                    if ((param.IsEdit || (HttpContext.Current == null)) ||
                        !string.IsNullOrEmpty(
                            HttpContext.Current.Request.Url.Query.Replace("?", "").Split(new char[] {'='})[0]))
                    {
                        string defaultValue = string.Empty;
                        if ((data != null) && (data.Rows.Count == 1))
                        {
                            defaultValue = MyControl_FilterHelper.GetString(data.Rows[0][param.FieldName]);
                        }
                        else if (!string.IsNullOrEmpty(param.DefaultValue) && (param.FieldType != FieldType.DateTime))
                        {
                            defaultValue = param.DefaultValue;
                        }
                        if (string.IsNullOrEmpty(param.RowClass))
                        {
                            builder.Append("<tr>");
                        }
                        else
                        {
                            builder.Append("<tr class=\"" + param.RowClass + "\">");
                        }
                        builder.Append("<td class=\"bgColor2\" align=\"right\" width=\"15%\"><span id=\"L_" +
                                       param.FieldName + "\"> ");
                        builder.Append(param.FieldDescN + "：");
                        builder.Append("</span></td>");
                        builder.Append("<td width=\"10%\" class=\"bgWhite\">");
                        string str2 = string.Empty;
                        if (!param.IsEdit)
                        {
                            str2 = "disabled=\"disabled\"";
                        }
                        if ((param.SltList != null) && (param.SltList.Count > 0))
                        {
                            builder.Append("<select id=\"" + param.FieldName + "\" " + str2 + " name=\"" +
                                           param.FieldName + "\">");
                            builder.Append("<option value=\"\">--请选择--</option>");
                            foreach (StatusList list in param.SltList)
                            {
                                builder.Append("<option value=\"" + list.Value + "\">" + list.Text + "</option>");
                            }
                            builder.Append("</select>");
                            builder.Append("<script type=\"text/javascript\">if('" + defaultValue +
                                           "' != ''){document.getElementById('" + param.FieldName + "').value='" +
                                           defaultValue + "';}else{document.getElementById('" + param.FieldName +
                                           "').selectedIndex=0;}</script>");
                        }
                        else if (param.FieldType == FieldType.DateTime)
                        {
                            builder.Append("<input type=\"text\" " + str2 + " id=\"" + param.FieldName +
                                           "\" style=\"height: 15px; width: 160px;\" name=\"" + param.FieldName +
                                           "\" value=\"" + defaultValue +
                                           "\" onclick=\"WdatePicker({el:this,dateFmt:'yyyy-MM-dd HH:mm:ss',skin:'whyGreen'})\" class=\"inputcss\" />");
                        }
                        else if (((param.FieldType == FieldType.Int) || (param.FieldType == FieldType.Long)) ||
                                 ((param.FieldType == FieldType.Float) || (param.FieldType == FieldType.Decimal)))
                        {
                            builder.Append("<input type=\"text\" " + str2 + " value=\"" + defaultValue + "\" id=\"" +
                                           param.FieldName + "\" style=\"height: 15px; width: 160px;\" name=\"" +
                                           param.FieldName + "\" />");
                        }
                        else if (param.IsTextarea)
                        {
                            builder.Append("<textarea  " + str2 + " id=\"" + param.FieldName + "\" name=\"" +
                                           param.FieldName +
                                           "\" style=\"width:160px; height:60px;\" cols=\"20\" rows=\"100\">" +
                                           defaultValue + "</textarea>");
                        }
                        else
                        {
                            if (((param.FieldType == FieldType.Int) || (param.FieldType == FieldType.Long)) ||
                                ((param.FieldType == FieldType.Float) || (param.FieldType == FieldType.Decimal)))
                            {
                                param.FieldLen = 50;
                            }
                            builder.Append(
                                string.Concat(new object[]
                                    {
                                        "<input type=\"text\" ", str2, " value=\"", defaultValue, "\" id=\"",
                                        param.FieldName, "\" maxlength=\"", param.FieldLen,
                                        "\" style=\"height: 15px; width: 160px;\" name=\"", param.FieldName, "\" />"
                                    }));
                        }
                        builder.Append("</td>");
                        builder.Append("<td class=\"bgWhite\">");
                        builder.Append("<font color='#424242'><span id=\"M_" + param.FieldName + "\"> " +
                                       param.ValueMemo + "</span></font>");
                        builder.Append("</td>");
                        builder.Append("</tr>");
                    }
                }
            }
            builder.Append("<tr>");
            builder.Append("<td class=\"bgColor2\" align=\"right\">");
            builder.Append("相关操作：");
            builder.Append("</td>");
            builder.Append("<td class=\"bgWhite\" colspan=\"2\">");
            if ((data == null) && this.AddBtnVisible)
            {
                builder.Append(
                    "&nbsp;<input id=\"BtnEdit\" type=\"button\" value=\" 新 增 \" onclick=\"Edit('add');\" /><input type=\"hidden\" value=\"\" id=\"txtEdit\" name=\"txtEdit\" />");
            }
            if ((data != null) && this.EditBtnVisible)
            {
                builder.Append(
                    "&nbsp;<input id=\"BtnEdit\" type=\"button\" value=\" 修 改 \" onclick=\"Edit('edit');\" /><input type=\"hidden\" value=\"\" id=\"txtEdit\" name=\"txtEdit\" />");
            }
            builder.Append(
                "<input type=\"button\" style=\"margin-left:150px;\" value=\" 返  回 \" onclick=\"javascript:location.href='" +
                this.FileName + ".aspx';\" />");
            builder.Append("</td>");
            builder.Append("</tr>");
            builder.Append("</table>");
            builder.Append("</td>");
            builder.Append("</tr>");
            builder.Append("</table>");
            return builder.ToString();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(this.ConnStrName) || string.IsNullOrEmpty(this.TableName))
            {
                writer.Write("");
            }
            else
            {
                try
                {
                    string str = string.Empty;
                    if ((HttpContext.Current != null) &&
                        (HttpContext.Current.Request.Url.Query.Replace("?", "").Split(new char[] {'='}).Length > 1))
                    {
                        str =
                            MyControl_FilterHelper.GetString(
                                HttpContext.Current.Request.Url.Query.Replace("?", "").Split(new char[] {'='})[1]);
                    }
                    if (MyControl_CacheHelper.Exists(this.TableName + str))
                    {
                        writer.Write(MyControl_CacheHelper.Get(this.TableName + str));
                    }
                    else
                    {
                        string str2 = this.SelectHtml(this.ConnStrName,
                                                      this.TableName.ToLower().Replace("with(nolock)", "").Trim(),
                                                      this.TableDescN);
                        if (!string.IsNullOrEmpty(str))
                        {
                            MyControl_CacheHelper.Set(this.TableName + str, str2, 1);
                        }
                        writer.Write(str2);
                    }
                }
                catch (Exception exception)
                {
                    writer.Write(exception);
                }
            }
        }

        private string SelectHtml(string connStr, string tableName, string tableDescN)
        {
            if ((!string.IsNullOrEmpty(this.devConnStr) &&
                 (this.devConnStr.Split(new char[] {';'})[0].Split(new char[] {'='})[1] != "")) &&
                ((this.devConnStr.Split(new char[] {';'})[1].Split(new char[] {'='})[1] != "") &&
                 !string.IsNullOrEmpty(tableName)))
            {
                if (this.fieldList == null)
                {
                    this.fieldList = new List<EditFieldParam>();
                    this.SetFieldList();
                }
                else if (this.fieldList.Count == 0)
                {
                    this.SetFieldList();
                }
            }
            string whereValue = string.Empty;
            string whereFieldName = string.Empty;
            if ((HttpContext.Current != null) &&
                (HttpContext.Current.Request.Url.Query.Replace("?", "").Split(new char[] {'='}).Length > 1))
            {
                whereValue =
                    HttpUtility.UrlDecode(
                        MyControl_FilterHelper.GetString(
                            HttpContext.Current.Request.Url.Query.Replace("?", "").Split(new char[] {'='})[1]));
                whereFieldName = HttpContext.Current.Request.Url.Query.Replace("?", "").Split(new char[] {'='})[0];
            }
            return this.GetHtml(tableName, tableDescN,
                                AdminData.GetModelDataTable(connStr, tableName, this.FileName, this.ProcName,
                                                            whereFieldName, whereValue, this.fieldList));
        }

        private void SetFieldList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT");
            builder.Append(" FieldName=C.name,");
            builder.Append(" FieldDescn=ISNULL(PFD.[value],C.name),");
            builder.Append(" FieldType=T.name,");
            builder.Append(" FieldLen=C.max_length,");
            builder.Append(" DefaultValue=D.definition");
            builder.Append(" FROM sys.columns C");
            builder.Append(" INNER JOIN sys.objects O");
            builder.Append(" ON C.[object_id]=O.[object_id]");
            builder.Append(" AND O.type='U'");
            builder.Append(" AND O.is_ms_shipped=0");
            builder.Append(" INNER JOIN sys.types T");
            builder.Append(" ON C.user_type_id=T.user_type_id");
            builder.Append(" LEFT JOIN sys.default_constraints D");
            builder.Append(" ON C.[object_id]=D.parent_object_id");
            builder.Append(" AND C.column_id=D.parent_column_id");
            builder.Append(" AND C.default_object_id=D.[object_id]");
            builder.Append(" LEFT JOIN sys.extended_properties PFD");
            builder.Append(" ON PFD.class=1 ");
            builder.Append(" AND C.[object_id]=PFD.major_id");
            builder.Append(" AND C.column_id=PFD.minor_id");
            builder.Append(" LEFT JOIN sys.extended_properties PTB");
            builder.Append(" ON PTB.class=1");
            builder.Append(" AND PTB.minor_id=0 ");
            builder.Append(" AND C.[object_id]=PTB.major_id");
            builder.Append(" LEFT JOIN ");
            builder.Append(" (");
            builder.Append(" SELECT ");
            builder.Append(" IDXC.[object_id],");
            builder.Append(" IDXC.column_id,");
            builder.Append(
                " Sort=CASE INDEXKEY_PROPERTY(IDXC.[object_id],IDXC.index_id,IDXC.index_column_id,'IsDescending')");
            builder.Append(" WHEN 1 THEN 'DESC' WHEN 0 THEN 'ASC' ELSE '' END,");
            builder.Append(" PrimaryKey=CASE WHEN IDX.is_primary_key=1 THEN N'1'ELSE N'0' END,");
            builder.Append(" IndexName=IDX.Name");
            builder.Append(" FROM sys.indexes IDX");
            builder.Append(" INNER JOIN sys.index_columns IDXC");
            builder.Append(" ON IDX.[object_id]=IDXC.[object_id]");
            builder.Append(" AND IDX.index_id=IDXC.index_id");
            builder.Append(" LEFT JOIN sys.key_constraints KC");
            builder.Append(" ON IDX.[object_id]=KC.[parent_object_id]");
            builder.Append(" AND IDX.index_id=KC.unique_index_id");
            builder.Append(" INNER JOIN");
            builder.Append(" (");
            builder.Append(" SELECT [object_id], Column_id, index_id=MIN(index_id)");
            builder.Append(" FROM sys.index_columns");
            builder.Append(" GROUP BY [object_id], Column_id");
            builder.Append(" ) IDXCUQ");
            builder.Append(" ON IDXC.[object_id]=IDXCUQ.[object_id]");
            builder.Append(" AND IDXC.Column_id=IDXCUQ.Column_id");
            builder.Append(" AND IDXC.index_id=IDXCUQ.index_id");
            builder.Append(" ) IDX");
            builder.Append(" ON C.[object_id]=IDX.[object_id]");
            builder.Append(" AND C.column_id=IDX.column_id ");
            builder.Append(" WHERE O.name='" + this.TableName + "' ");
            builder.Append(" ORDER BY O.name,C.column_id");
            DataTable table = null;
            try
            {
                table =
                    MyControl_SqlHelper.ExecuteDataset(this.DevConnStr,CommandType.Text , builder.ToString(), new SqlParameter[0])
                                       .Tables[0];
                if ((table != null) && (table.Rows.Count > 0))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        EditFieldParam item = new EditFieldParam();
                        item.FieldName = row[0].ToString();
                        item.FieldDescN = row[1].ToString().Trim();
                        item.FieldType = this.GetFieldType(row[2].ToString().ToLower());
                        item.FieldLen = MyControl_FilterHelper.GetIntZero(row[3]);
                        this.fieldList.Add(item);
                    }
                }
            }
            catch
            {
            }
        }

        // Properties
        [Browsable(false), Description("新增按钮是否有权限显示"), Category("杂项")]
        public bool AddBtnVisible
        {
            get { return this._addBtnVisible; }
            set { this._addBtnVisible = value; }
        }

        [Description("数据库连接字符串的名称，与Web.config里的配置的name一致"), Browsable(true), Category("编辑")]
        public string ConnStrName { get; set; }

        [Description("开发时使用的数据库连接字符串，与开发时Web.config里的配置一样，开发完成之后可以清空"), Category("编辑"), Browsable(true)]
        public string DevConnStr
        {
            get { return this.devConnStr; }
            set { this.devConnStr = value; }
        }

        [Description("编辑按钮是否有权限显示"), Category("杂项"), Browsable(false)]
        public bool EditBtnVisible
        {
            get { return this._editBtnVisible; }
            set { this._editBtnVisible = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), Editor(typeof (EditFieldEditor), typeof (UITypeEditor)),
         Browsable(true), Description("编辑字段列表信息"), Category("编辑"),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<EditFieldParam> FieldList
        {
            get
            {
                if (this.fieldList == null)
                {
                    this.fieldList = new List<EditFieldParam>();
                }
                return this.fieldList;
            }
            set { this.fieldList = value; }
        }

        [Description("查询列表的文件名"), Category("编辑"), Browsable(true)]
        public string FileName { get; set; }

        [Description("存储过程名称(如果存储过程名不为空，则以存储过程为准)"), Browsable(true), Category("编辑")]
        public string ProcName { get; set; }

        [Category("编辑"), Browsable(true), Description("表的说明描述")]
        public string TableDescN { get; set; }

        [Category("编辑"), Browsable(true), Description("要编辑的表名")]
        public string TableName { get; set; }
    }

}
