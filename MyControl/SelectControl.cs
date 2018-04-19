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
[ParseChildren(true), ToolboxData("<{0}:SelectControl runat=server></{0}:SelectControl>"), PersistChildren(false), AspNetHostingPermission(SecurityAction.Demand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
public class SelectControl : WebControl
{
    // Fields
    private bool _addBtnVisible = true;
    private bool _addNolock = true;
    private bool _customBtnVisible = true;
    private bool _delBtnVisible = true;
    private bool _editBtnVisible = true;
    private bool _expBtnVisible = true;
    private string _orderBy = "ORDER BY IDX DESC";
    private int _pageSize = 20;
    private bool _saveCondition = true;
    private string _tn = string.Empty;
    private string _updateColumn = "UpdateColumn";
    private string devConnStr = "Data Source=;Initial Catalog=;Persist Security Info=True;User ID=nbuser;Password=sa";
    private List<FieldParam> fieldList;
    private List<WhereParam> isWhereList;

    // Methods
    private string GenClearJS()
    {
        if (HttpContext.Current == null)
        {
            return "";
        }
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<script type=\"text/javascript\">");
        builder.AppendLine("function doclear"+this.Tn+"() {");
        if (this.isWhereList != null)
        {
            foreach (WhereParam param in this.isWhereList)
            {
                if (param.FieldType == FieldType.DateTime)
                {
                    builder.AppendLine("document.getElementById('" + param.FieldNameN + "1').value='';");
                    builder.AppendLine("document.getElementById('" + param.FieldNameN + "2').value='';");
                }
                else
                {
                    if ((param.Operator == AdminCompare.In) && !string.IsNullOrEmpty(param.DefaultValue))
                    {
                        builder.AppendLine("var infields = document.getElementsByName('" + param.FieldNameN + "');");
                        builder.AppendLine("for (var i = 0; i < infields.length; i++) {");
                        builder.AppendLine("     infields[i].checked = true;");
                        builder.AppendLine("}");
                        continue;
                    }
                    if (param.IsDisable)
                    {
                        builder.AppendLine("document.getElementById('" + param.FieldNameN + "').value='clear';");
                    }
                    else
                    {
                        builder.AppendLine("document.getElementById('" + param.FieldNameN + "').value='" + param.DefaultValue + "';");
                    }
                    
                }
            }
        }
        builder.AppendLine("return false;");
        builder.AppendLine("}");
        builder.AppendLine("</script>");
        return builder.ToString();
    }

    private string GenDataRowBtnHtml(DataRow dr)
    {
        StringBuilder builder = new StringBuilder();
        if (((this.IsDel && this.DelBtnVisible) || (this.IsEdit && this.EditBtnVisible)) || (this.IsCustomBtn && this.CustomBtnVisible))
        {
            builder.AppendLine("<td>");
            if (this.IsCustomBtn && this.CustomBtnVisible)
            {
                foreach (FieldParam param in this.fieldList)
                {
                    if (param.FieldName.Equals(this.DelFieldName, StringComparison.OrdinalIgnoreCase))
                    {
                        if ((param.SltList != null) && (param.SltList.Count > 0))
                        {
                            int num = 1;
                            foreach (StatusList list in param.SltList)
                            {
                                if (dr[param.FieldName].ToString() != list.Value)
                                {
                                    string str = "&nbsp;";
                                    if (num == param.SltList.Count)
                                    {
                                        str = "";
                                    }
                                    builder.AppendLine(string.Concat(new object[] { "<a href=\"javascript:if(confirm('确定要", list.Text, "？')){GetList", this.Tn, "(document.getElementById('txtP", this.Tn, "').value,'", dr[this.EditWhereField], "','','','','", list.Value, "');}\" >", list.Text, "</a>", str }));
                                    num++;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(this.DelName))
            {
                this.DelName = "删除";
            }
            if ((this.IsDel && this.DelBtnVisible) && !this.IsRealDel)
            {
                builder.AppendLine(string.Concat(new object[] { "<a href=\"javascript:if(confirm('确定要", this.DelName, "？')){GetList", this.Tn, "(document.getElementById('txtP", this.Tn, "').value,'", dr[this.EditWhereField], "','','','','');}\" >", this.DelName, "</a>" }));
            }
            if ((this.IsDel && this.DelBtnVisible) && this.IsRealDel)
            {
                builder.AppendLine(string.Concat(new object[] { "<a href=\"javascript:if(confirm('", this.DelName, @"之后将无法恢复！\n确定要彻底", this.DelName, "？')){GetList", this.Tn, "(document.getElementById('txtP", this.Tn, "').value,'", dr[this.EditWhereField], "','','','','');}\" >", this.DelName, "</a>" }));
            }
            if (this.IsEdit && this.EditBtnVisible)
            {
                builder.AppendLine(string.Concat(new object[] { "&nbsp;<a href=\"", this.FileName, "Edit.aspx?", this.EditWhereField, "=", dr[this.EditWhereField], "\">编辑</a>" }));
            }
            builder.AppendLine("</td>");
        }
        return builder.ToString();
    }

    private string GenDataRowHtml(DataRow dr)
    {
        StringBuilder builder = new StringBuilder();
        if ((this.fieldList != null) && (this.fieldList.Count > 0))
        {
            foreach (FieldParam param in this.fieldList)
            {
                if (!param.IsOutput)
                {
                    continue;
                }
                string str = MyControl_FilterHelper.GetString(dr[param.FieldName]);
                if (str == "True")
                    str = "1";
                else if (str == "False")
                    str = "0";
                string s = str;
                if ((HttpContext.Current != null) && !param.IsImg)
                {
                    s = HttpContext.Current.Server.HtmlEncode(s);
                }
                string str3 = string.Empty;
                if (!string.IsNullOrEmpty(param.EnumName))
                {
                    param.SltList = CacheDataHelper.Instance.GetEnumData(param.EnumName);
                }
                if ((param.SltList != null) && (param.SltList.Count > 0))
                {
                    bool flag = false;
                    if (param.IsEdit && this.EditBtnVisible)
                    {
                        str3 = string.Concat(new object[] { @"<select id=\'", this.TableName, dr[this.EditWhereField], @"\' name=\'", dr[this.EditWhereField], @"\' class=\'", param.FieldName, @"\' onchange=\'Update", this.Tn, @"(this, this.name, this.className, this.value, this.innerText);\'>" });
                        foreach (StatusList list in param.SltList)
                        {
                            string str4 = string.Empty;
                            if (list.Value.Equals(dr[param.FieldName].ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                str4 = @"selected=\'selected\'";
                            }
                            string str5 = str3;
                            str3 = str5 + @"<option value=\'" + list.Value + @"\' " + str4 + ">" + list.Text + "</option>";
                        }
                        str3 = str3 + "</select>";
                    }
                    foreach (StatusList list2 in param.SltList)
                    {
                        //if (!list2.Value.Equals(dr[param.FieldName].ToString(), StringComparison.OrdinalIgnoreCase))
                        if (!list2.Value.Equals(s, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        if (param.IsEdit && this.EditBtnVisible)
                        {
                            builder.Append("<td onDblClick=\"ChangeH" + this.Tn + "(this, '" + str3 + "');\" title=\"" + HttpContext.Current.Server.HtmlEncode(list2.Value) + "\">");
                        }
                        else
                        {
                            builder.Append("<td title=\"" + HttpContext.Current.Server.HtmlEncode(list2.Value) + "\">");
                        }
                        flag = true;
                        s = list2.Text;
                        break;
                    }
                    if (!flag)
                    {
                        if (param.IsEdit && this.EditBtnVisible)
                        {
                            builder.Append("<td onDblClick=\"ChangeH" + this.Tn + "(this, '" + str3 + "');\" title=\"未知\">");
                        }
                        else
                        {
                            builder.Append("<td title=\"未知\">");
                        }
                    }
                }
                else if (param.IsEdit && this.EditBtnVisible)
                {
                    str3 = string.Concat(new object[] { @"<input id=\'", this.TableName, dr[this.EditWhereField], @"\' name=\'", dr[this.EditWhereField], @"\' class=\'", param.FieldName, @"\' onchange=\'Update", this.Tn, @"(this, this.name, this.className, this.value, this.value);\' type=\'text\' value=\'", s, @"\' />" });
                    builder.Append("<td onDblClick=\"ChangeH" + this.Tn + "(this, '" + str3 + "');\" title=\"" + s + "\">");
                }
                else
                {
                    builder.Append("<td title=\"" + s + "\">");
                }
                if (param.IsImg)
                {
                    s = "<img src=\"" + this.ImgRootUrl + s + "\" />";
                }
                else if (!string.IsNullOrEmpty(param.RenderText))
                {
                    s = param.RenderText;
                }
                else if ((s.Length > param.FieldContentLen) && (param.FieldContentLen != 0))
                {
                    s = s.Substring(0, param.FieldContentLen) + "...";
                }
                if ((!string.IsNullOrEmpty(param.Links) || param.ShowLinks) || !string.IsNullOrEmpty(param.OnClick))
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append("<a href=\"");
                    if (!string.IsNullOrEmpty(param.Links) || param.ShowLinks)
                    {
                        builder2.Append(param.Links + dr[param.FieldName] + "\"");
                    }
                    else
                    {
                        builder2.Append("javascript:void(0);\" ");
                    }
                    if (!string.IsNullOrEmpty(param.OnClick))
                    {
                        builder2.Append(" onclick=\"" + string.Format(param.OnClick, str) + "\" ");
                    }
                    builder2.Append(" target='" + param.LinkTarget + "'>" + s + "</a>");
                    s = builder2.ToString();
                }
                builder.Append(s);
                builder.AppendLine("</td>");
            }
        }
        builder.Append(this.GenDataRowBtnHtml(dr));
        return builder.ToString();
    }

    private string GenFieldDescNHtml()
    {
        string str = string.Empty;
        if ((this.fieldList != null) && (this.fieldList.Count > 0))
        {
            foreach (FieldParam param in this.fieldList)
            {
                if (!param.IsOutput)
                {
                    continue;
                }
                if (param.IsSort)
                {
                    string str2 = str;
                    str = str2 + "<th scope=\"col\" style=\"cursor:pointer;\" onclick=\"try{document.getElementById('txtInputP" + this.Tn + "').value='';}catch(err){}GetList" + this.Tn + "(1,'','', '" + param.FieldName + "','','');\"><input type=\"hidden\" value=\"" + param.SortValue + "\" id=\"txtSort_" + this.Tn + param.FieldName + "\" name=\"txtSort_" + this.Tn + param.FieldName + "\" />";
                    if (string.IsNullOrEmpty(param.SortValue))
                    {
                        str = str + param.FieldDescN + "<font color='#ffffff' size='1'>∷</font>";
                    }
                    else if (param.SortValue.Equals(param.FieldName + " asc"))
                    {
                        str = str + param.FieldDescN + "<font color='#ffffff' size='1'>▲</font>";
                    }
                    else
                    {
                        str = str + param.FieldDescN + "<font color='#ffffff' size='1'>▼</font>";
                    }
                }
                else
                {
                    str = str + "<th  scope=\"col\" title=\"" + param.Title + "\">";
                    str = str + param.FieldDescN;
                }
                str = str + "</th>";
            }
        }
        return str;
    }

    private string GenFieldWhereCheckBoxHtml(WhereParam whereField, List<StatusList> slt)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<td class=\"bgWhite\" width=\"22%\">");
        string str = "";
        if (HttpContext.Current != null)
        {
            str = "," + whereField.FieldValue.ToString() + ",";
        }
        string fieldName = whereField.FieldNameN;
        foreach (StatusList list in slt)
        {
            if (str.IndexOf("," + list.Value + ",", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                builder.AppendLine("<input type=\"checkbox\" name=\"" + fieldName + "\" value=\"" + list.Value + "\"  title=\"" + list.Text + "\" checked=\"checked\"/> " + list.Text);
            }
            else
            {
                builder.AppendLine("<input type=\"checkbox\" name=\"" + fieldName + "\" value=\"" + list.Value + "\"  title=\"" + list.Text + "\"/> " + list.Text);
            }
        }
        return builder.ToString();
    }

    private string GenFieldWhereDateHtml(WhereParam whereField)
    {
        StringBuilder builder = new StringBuilder();
        string str = string.Empty;
        string str2 = string.Empty;
        if (whereField.FieldValue != null)
        {
            string[] strArray = whereField.FieldValue.ToString().Split(new char[] { ',' });
            if (strArray.Length == 2)
            {
                str = strArray[0];
                str2 = strArray[1];
            }
        }
        if (string.IsNullOrEmpty(whereField.DateFormat))
        {
            whereField.DateFormat = "yyyy-MM-dd HH:mm:ss";
        }
        builder.AppendLine("<td class=\"bgWhite\" width=\"22%\">");
        builder.AppendLine("从<input type=\"text\" id=\"" + whereField.FieldNameN + "1\" style=\"height: 15px; width: 120px;\" name=\"" + whereField.FieldNameN + "1\" value=\"" + str + "\" onclick=\"WdatePicker({el:this,dateFmt:'" + whereField.DateFormat + "',skin:'whyGreen'})\" class=\"inputcss\" /> ");
        builder.AppendLine("至<input type=\"text\" id=\"" + whereField.FieldNameN + "2\" style=\"height: 15px; width: 120px;\" name=\"" + whereField.FieldNameN + "2\" value=\"" + str2 + "\" onclick=\"WdatePicker({el:this,dateFmt:'" + whereField.DateFormat + "',skin:'whyGreen'})\" class=\"inputcss\" />");
        return builder.ToString();
    }

    private string GetZoneWhereHtml()
    {
        if(!ShowZone)
        {
            return "";
        }
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<tr>");

        builder.AppendLine("<td class=\"bgColor2\" width=\"10%\">");
        builder.Append("选择区：");
        builder.AppendLine("</td>");
        if (!string.IsNullOrEmpty(_curZone))
        {
            builder.Append(ConnectionFactory.Instance.GetZoneHtml(_curZone));
        }
        else
        {
            builder.Append(ConnectionFactory.Instance.GetZoneHtml(GetSelectZoneFromCookie(HttpContext.Current)));
        }
        builder.AppendLine("</td>");
        builder.AppendLine("</tr>");
        return builder.ToString();
    }

    private string GenFieldWhereHtml()
    {
        string zoneHtml = GetZoneWhereHtml();
        if ((this.isWhereList == null) || (this.isWhereList.Count == 0))
        {
            return zoneHtml;
        }
        StringBuilder builder = new StringBuilder();
        int num = 0;
        string url = string.Empty;
        if ((HttpContext.Current != null) && (HttpContext.Current.Request.Url != null))
        {
            url = HttpContext.Current.Request.Url.PathAndQuery;
        }
        builder.Append(zoneHtml);
        builder.AppendLine("<tr>");
        foreach (WhereParam param in this.isWhereList)
        {
            if (param.IsDisable)
            {
                builder.Append(GenFieldWhereTextHtmlDisable(param));
                continue;
            }
            if ((num != 0) && ((num % 2) == 0))
            {
                builder.AppendLine("</tr>");
                builder.AppendLine("<tr>");
            }
            builder.AppendLine("<td class=\"bgColor2\" width=\"10%\">");
            List<StatusList> slt = null;
            FieldParam whereParamFieldInfo = this.GetWhereParamFieldInfo(param);
            if (whereParamFieldInfo != null)
            {
                slt = whereParamFieldInfo.SltList;
                if (!string.IsNullOrEmpty(whereParamFieldInfo.EnumName))
                {
                    slt = CacheDataHelper.Instance.GetEnumData(whereParamFieldInfo.EnumName);
                }
            }
            if (!string.IsNullOrEmpty(param.ConditionName))
            {
                builder.Append(param.ConditionName + "：");
            }
            else if (whereParamFieldInfo != null)
            {
                builder.Append(whereParamFieldInfo.FieldDescN + "：");
            }
            else
            {
                builder.Append(param.FieldNameN + "：");
            }
            builder.AppendLine("</td>");
            
            if ((slt != null) && (slt.Count > 0))
            {
                if (param.Operator == AdminCompare.In)
                {
                    builder.Append(this.GenFieldWhereCheckBoxHtml(param, slt));
                }
                else
                {
                    builder.Append(this.GenFieldWhereSelectHtml(url, param, slt));
                }
            }
            else if (param.FieldType == FieldType.DateTime)
            {
                builder.Append(this.GenFieldWhereDateHtml(param));
            }
            else
            {
                builder.Append(this.GenFieldWhereTextHtml(param));
            }
            builder.AppendLine("</td>");
            num++;
        }
        if ((num % 2) != 0)
        {
            builder.AppendLine("<td colspan=2 class=\"bgWhite\" width=\"50%\"></td>");
        }
        builder.AppendLine("</tr>");
        return builder.ToString();
    }

    private string GenFieldWhereSelectHtml(string url, WhereParam whereField, List<StatusList> slt)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<td class=\"bgWhite\" width=\"10%\">");
        builder.AppendLine("<select id=\"" + whereField.FieldNameN + "\" name=\"" + whereField.FieldNameN + "\">");
        builder.AppendLine("<option value=\"" + whereField.DefaultValue + "\">--请选择--</option>");
        foreach (StatusList list in slt)
        {
            if (((HttpContext.Current != null) && (HttpContext.Current.Request.Form["txtBtnType" + this.Tn] == null)) && (!url.Contains("?") && (list.Value == whereField.FieldDefaultValue)))
            {
                builder.AppendLine("<option value=\"" + list.Value + "\" selected=\"selected\">" + list.Text + "</option>");
            }
            else
            {
                builder.AppendLine("<option value=\"" + list.Value + "\">" + list.Text + "</option>");
            }
        }
        builder.AppendLine("</select>");
        if (HttpContext.Current != null)
        {
            string str = whereField.FieldValue.ToString();
            builder.AppendLine("<script type=\"text/javascript\">document.getElementById('" + whereField.FieldNameN + "').value='" + str + "';</script>");
        }
        return builder.ToString();
    }

    private string GenFieldWhereTextHtml(WhereParam whereField)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<td class=\"bgWhite\" width=\"10%\">");
        if (((whereField.FieldType == FieldType.Int) || (whereField.FieldType == FieldType.Long)) || ((whereField.FieldType == FieldType.Float) || (whereField.FieldType == FieldType.Decimal)))
        {
            whereField.FieldLen = 50;
        }
        builder.AppendLine(string.Concat(new object[] { "<input type=\"text\" value=\"", whereField.FieldValue, "\" id=\"", whereField.FieldNameN, "\" maxlength=\"", whereField.FieldLen, "\" style=\"height: 15px; width: 150px;\" name=\"", whereField.FieldNameN, "\" class=\"inputcss\" />" }));
        return builder.ToString();
    }

    private string GenFieldWhereTextHtmlDisable(WhereParam whereField)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<div style=\"display:none\">");
        if (((whereField.FieldType == FieldType.Int) || (whereField.FieldType == FieldType.Long)) || ((whereField.FieldType == FieldType.Float) || (whereField.FieldType == FieldType.Decimal)))
        {
            whereField.FieldLen = 50;
        }
        builder.AppendLine(string.Concat(new object[] { "<input type=\"text\" value=\"", whereField.FieldValue, "\" id=\"", whereField.FieldNameN, "\" maxlength=\"", whereField.FieldLen, "\" style=\"height: 15px; width: 150px;\" name=\"", whereField.FieldNameN, "\" class=\"inputcss\" />" }));
        builder.AppendLine("</div>");
        return builder.ToString();
    }

    private string GenOutputConfig()
    {
        if (HttpContext.Current == null)
        {
            return "";
        }
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<div style=\"display:none;\" ><div id=\"outputconfig" + this.Tn + "\" style=\"width:360px; margin:20px;\" >");
        builder.AppendLine("选择需要输出的列（查询|导出时生效）：<br />");
        for (int i = 0; i < this.fieldList.Count; i++)
        {
            builder.AppendLine("<div style=\"width:150px;margin-right:20px; float:left\">");
            if (this.fieldList[i].IsOutput)
            {
                builder.AppendLine("<input type=\"checkbox\" name=\"" + this.OUTPUT_COLUMNS + "\" value=\"" + i.ToString() + "\"  title=\"" + this.fieldList[i].Title + "\" checked=\"checked\"/> " + this.fieldList[i].FieldDescN);
            }
            else
            {
                builder.AppendLine("<input type=\"checkbox\" name=\"" + this.OUTPUT_COLUMNS + "\" value=\"" + i.ToString() + "\"  title=\"" + this.fieldList[i].Title + "\" /> " + this.fieldList[i].FieldDescN);
            }
            builder.AppendLine("</div>");
        }
        builder.AppendLine("<br /></div></div>");
        if (HttpContext.Current != null)
        {
            builder.AppendLine("<script type=\"text/javascript\">");
            builder.AppendLine("$(function () { ");
            builder.Append("$(\"#ShowConfig" + this.Tn + "\").colorbox({ inline:true, href:\"#outputconfig" + this.Tn + "\" ,opacity:0.5,overlayClose:true});");
            builder.AppendLine("});");
            builder.AppendLine("</script>");
        }
        return builder.ToString();
    }

    private string GenOutputCssJS()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<style type=\"text/css\">body, table, td, th, span, p, div, a, form, input, button, select, option, textarea { font-family : simsun; font-size : 12px }");
        builder.AppendLine("a:link {color: #0000ea; text-decoration:none}");
        builder.AppendLine("a:visited {color: #006666; text-decoration: none}");
        builder.AppendLine("a:active {color:#ff0000; text-decoration: underline}");
        builder.AppendLine("a:hover {color: #ff0000; text-decoration: underline}");
        builder.AppendLine("a.btn-i1{display:block;width:120px;text-align:center;color:#E6E956;background-color:#CCEEEE; text-decoration:none; border-left:#99A52F 1px solid; border-top:#99A52F 1px solid; border-right:#61691E 1px solid; border-bottom:#3F4414 1px solid;padding:2px 0 0 0;}");
        builder.AppendLine("a.btn-i1:link {color: #000000; text-decoration:none}");
        builder.AppendLine("a.btn-i1:visited {color: #006666; text-decoration: none}");
        builder.AppendLine("a.btn-i1:active {");
        builder.AppendLine("color:#ff0000; text-decoration: underline}");
        builder.AppendLine("a.btn-i1:hover {color: #ff0000; text-decoration: underline}");
        builder.AppendLine(".bottomLine { border-bottom:1px solid #000000 }");
        builder.AppendLine(".rightLine { border-right:1px solid #000000 }");
        builder.AppendLine(".ftGreen { color : green }");
        builder.AppendLine(".bgColor { background-color:#33CCCC }");
        builder.AppendLine(".bgColor1 { background-color:#CCEEEE }");
        builder.AppendLine(".bgColor2 { background-color:#66CCFF }");
        builder.AppendLine(".bgDark { background-color:#000000 }");
        builder.AppendLine(".bgGray { background-color:Gray }");
        builder.AppendLine(".bgLightGray { background-color : #DDDDDD }");
        builder.AppendLine(".bgLightBlue { background-color : lightblue }");
        builder.AppendLine(".bgLightGreen { background-color : lightgreen }");
        builder.AppendLine(".bgWhite { background-color:#FFFFFF }");
        builder.AppendLine(".buttonWidth { width: 100px }");
        builder.AppendLine(".inputWidth { width: 200px }");
        builder.AppendLine(".SortFieldLable{text-decoration:underline;background-color:#66CCFF;color:#3366ff;cursor:hand;}");
        builder.AppendLine(".pagebar{line-height:20px;height:20px;}");
        builder.AppendLine(".pagebar a,.pagebar .now-page{padding:1px 3px 2px 3px;margin:0 2px;text-align:center;font-weight:bold;font-family:Verdana;border:1px solid #ccc;text-decoration:none;COLOR: #006699;}");
        builder.AppendLine(".pagebar a:hover{border:1px solid #c00;text-decoration:none;BACKGROUND-COLOR: #f1ffc0;COLOR: #c00;}");
        builder.AppendLine("a.wa {COLOR: #7c7b6b;BACKGROUND-COLOR: #f1ff00;}");
        builder.AppendLine("a.wah {COLOR: #7c7b6b;}</style>");
        builder.AppendLine("<link href=\"\\style\\colorbox.css\" rel=\"stylesheet\" type=\"text/css\"/>");
        if (HttpContext.Current != null)
        {
            builder.AppendLine("<script src=\"\\javascript\\jquery-1.4.2.min.js\" type=\"text/javascript\"></script>");
            builder.AppendLine("<script src=\"\\javascript\\jquery.colorbox-min.js\" type=\"text/javascript\"></script>");
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("function showid" + this.Tn + "(idname){");
            builder.Append("var isIE = (document.all) ? true : false;");
            builder.Append(@"var isIE6 = isIE && ([/MSIE (\d)\.0/i.exec(navigator.userAgent)][0][1] == 6);");
            builder.Append("var newbox=document.getElementById(idname);");
            builder.Append("newbox.style.zIndex=\"9999\";");
            builder.Append("newbox.style.display=\"block\";");
            builder.Append("newbox.style.position = !isIE6 ? \"fixed\" : \"absolute\";");
            builder.Append("newbox.style.top =newbox.style.left = \"50%\";");
            builder.Append("newbox.style.marginTop = - newbox.offsetHeight / 2 + \"px\";");
            builder.Append("newbox.style.marginLeft = - newbox.offsetWidth / 2 + \"px\";");
            builder.Append("var layer=document.createElement(\"div\");");
            builder.Append("layer.id=\"layer\";");
            builder.Append("layer.style.width=layer.style.height=\"100%\";");
            builder.Append("layer.style.position= !isIE6 ? \"fixed\" : \"absolute\";");
            builder.Append("layer.style.top=layer.style.left=0;");
            builder.Append("layer.style.zIndex=\"9998\";");
            builder.Append("layer.style.opacity=\"0.6\";");
            builder.Append("document.body.appendChild(layer);");
            builder.Append("function layer_iestyle(){");
            builder.Append("layer.style.width=Math.max(document.documentElement.scrollWidth, document.documentElement.clientWidth)+ \"px\";");
            builder.Append("layer.style.height= Math.max(document.documentElement.scrollHeight, document.documentElement.clientHeight) +\"px\";");
            builder.Append("}");
            builder.Append("function newbox_iestyle(){");
            builder.Append("newbox.style.marginTop = document.documentElement.scrollTop - newbox.offsetHeight / 2 + \"px\";");
            builder.Append("newbox.style.marginLeft = document.documentElement.scrollLeft - newbox.offsetWidth / 2 + \"px\";");
            builder.Append("}");
            builder.Append("if(isIE){layer.style.filter =\"alpha(opacity=60)\";}");
            builder.Append("if(isIE6){");
            builder.Append("layer_iestyle();");
            builder.Append("newbox_iestyle();");
            builder.Append("window.attachEvent(\"onscroll\",function(){");
            builder.Append("newbox_iestyle();");
            builder.Append("});");
            builder.Append("window.attachEvent(\"onresize\",layer_iestyle)");
            builder.Append("}");
            builder.Append("layer.style.display=\"none\";}");
            builder.Append("</script>");
            builder.AppendLine("<script type=\"text/javascript\">");
            builder.AppendLine("function GetList" + this.Tn + "(p, editwherevalue,exp,fieldname, btntype, custom) {");
            builder.AppendLine("if(" + this.PageSize + "==0){");
            builder.AppendLine("var list=document.forms[0].getElementsByTagName('input');");
            builder.AppendLine("for(var i=0;i<list.length && list[i];i++){if(list[i].type=='hidden'&&(list[i].id.indexOf('txtExp') >= 0 || list[i].id.indexOf('txtDel') >= 0)){list[i].value='';}}");
            builder.AppendLine("}");
            builder.AppendLine("document.getElementById('txtSortFieldName" + this.Tn + "').value=fieldname;");
            builder.AppendLine("document.getElementById('txtBtnType" + this.Tn + "').value=btntype;");
            builder.AppendLine("document.getElementById('txtP" + this.Tn + "').value=p;");
            builder.AppendLine("document.getElementById('txtExp" + this.Tn + "').value=exp;");
            builder.AppendLine("document.getElementById('txtCustom" + this.Tn + "').value=custom;");
            builder.AppendLine("document.getElementById('txtDel" + this.Tn + this.EditWhereField + "').value=editwherevalue;");
            builder.AppendLine("if(document.getElementById('txtExp" + this.Tn + "').value=='exp'){document.getElementById('txtDel" + this.Tn + this.EditWhereField + "').value='';}");
            builder.AppendLine("if(document.getElementById('txtDel" + this.Tn + this.EditWhereField + "').value!=''){document.getElementById('txtExp" + this.Tn + "').value='';}");
            builder.AppendLine("document.forms[0].action = \"" + this.FileName + ".aspx\";");
            builder.AppendLine("document.forms[0].method = \"post\";");
            builder.AppendLine("document.forms[0].submit();");
            builder.AppendLine("}");
            builder.Append("function DisNone" + this.Tn + "(){$('#upRes" + this.Tn + "').hide();}");
            builder.Append("function Update" + this.Tn + "(kid, key, vname, val, txt){$.post('" + this.FileName + ".aspx',{pro:'" + this.UpdateColumn + "', tbname:'" + this.Tn + "', key:key, vname:vname, val:val, upid:'" + this.EditWhereField + "'},function (data){$('#upRes" + this.Tn + "').html(data);");
            builder.Append("showid" + this.Tn + "('upRes" + this.Tn + "');setTimeout('DisNone" + this.Tn + "()',800);});}");
            builder.Append("function ChangeH" + this.Tn + "(o, hml){$(o).html(hml);}");
            builder.Append("</script>");
        }
        return builder.ToString();
    }

    private string GenGroupTitle()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<table width=\"100%\" border=\"0\" cellpadding=\"4\" cellspacing=\"1\" class=\"bgDark\">");
        builder.AppendLine("<tr class=\"bgLightGray\">");
        builder.AppendLine("<td align=\"left\">");
        builder.AppendLine(this.TableDescN);
        builder.AppendLine("</td>");
        builder.AppendLine("</tr>");
        builder.AppendLine("</table>");
        return builder.ToString();
    }

    private string GenQueryHtml()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("<table width=\"100%\" border=\"0\" cellpadding=\"4\" cellspacing=\"1\" class=\"bgDark\">");
        if (!HideTitle)
        {
            builder.AppendLine("<tr class=\"bgLightGray\">");
            builder.AppendLine("<td colspan=\"4\" align=\"left\">");
            builder.AppendLine("<b><font color=\"#009900\">【" + this.TableDescN + "】</font></b>");
            builder.AppendLine("</td>");
            builder.AppendLine("</tr>");
        }
        builder.Append(this.GenFieldWhereHtml());
        bool hasWhere = false;
        if ((this.isWhereList != null) && (this.isWhereList.Count > 0))
        {
            foreach (var param in isWhereList)
            {
                if (!param.IsDisable)
                {
                    hasWhere = true;
                    break;
                }
            }
        }
        if (hasWhere || (this.IsAdd || this.IsExp))
        {
            builder.AppendLine("<tr>");
            builder.Append("<td class=\"bgColor2\" width=\"10%\">");
            builder.Append("相关操作：");
            builder.AppendLine("</td>");
            builder.AppendLine("<td class=\"bgWhite\" colspan=\"3\">");
            if ((this.isWhereList != null) && (this.isWhereList.Count > 0))
            {
                builder.AppendLine("&nbsp;<input id=\"BtnQuery\" type=\"button\" value=\" 查 询 \" onclick=\"try{document.getElementById('txtInputP" + this.Tn + "').value='';}catch(err){}GetList" + this.Tn + "(1,'','','','btntype','');\" />");
                builder.AppendLine("&nbsp;<input id=\"BtnClear\" type=\"button\" value=\" 清 空 \" onclick=\"doclear"+this.Tn+"();\" />");
            }
            if (this.IsAdd || this.IsExp)
            {
                if (this.AddBtnVisible && this.IsAdd)
                {
                    builder.AppendLine("<input id=\"BtnAdd\" style=\"margin-left:150px;\" type=\"button\" value=\" 新 增 \" onclick=\"location.href='" + this.FileName + "Edit.aspx'\" />");
                }
                if (this.ExpBtnVisible && this.IsExp)
                {
                    builder.AppendLine("<input id=\"BtnExp\" style=\"margin-left:150px;\" type=\"button\" value=\" 导 出 \" onclick=\"GetList" + this.Tn + "(1,'','exp','','','');\" />");
                }
            }
            if (this.PageSize != 0)
            {
                builder.AppendLine("<a href=\"#\" id=\"ShowConfig" + this.Tn + "\"> 输出列配置 </a>");
            }
            builder.AppendLine("</td>");
            builder.AppendLine("</tr>");
        }
        builder.AppendLine("</table>");
        return builder.ToString();
    }

    private string GetContent(DataTable dt)
    {
        StringBuilder builder = new StringBuilder();
        string str = string.Empty;
        string str2 = string.Empty;
        foreach (FieldParam param in this.fieldList)
        {
            if (string.IsNullOrEmpty(param.RenderText) && param.IsOutput)
            {
                str2 = str2 + MyControl_SaveCSV.ExcelNewLine(param.FieldDescN) + ",";
            }
        }
        str2 = str2.Trim(new char[] { ',' });
        builder.AppendLine(str2);
        foreach (DataRow row in dt.Rows)
        {
            foreach (FieldParam param2 in this.fieldList)
            {
                if (string.IsNullOrEmpty(param2.RenderText) && param2.IsOutput)
                {
                    str = str + MyControl_SaveCSV.ExcelNewLine("\t" + row[param2.FieldName].ToString()) + ",";
                }
            }
            str = str.Trim(new char[] { ',' });
            builder.AppendLine(str);
            str = "";
        }
        return builder.ToString();
    }

    private string _curZone;
    private string GetConnectionString()
    {
        string controlName = "SZone";
        string zone = "";
        if (this.ShowZone)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[controlName]))
            {
                zone = MyControl_FilterHelper.GetString(HttpContext.Current.Request.Form[controlName]);
                SetSelectZoneToCookie(HttpContext.Current,zone);
                _curZone = zone;
            }
        }
        if (string.IsNullOrEmpty(zone))
        {
            if(!string.IsNullOrEmpty(ZoneId))
                zone = ZoneId;
            else
            {
                zone = "Share";
            }
        }
        return ConnectionFactory.Instance.GetConnectionString(zone, DbCategory);
    }

    private static FieldType GetFieldType(string type)
    {
        switch (type.ToLower())
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
            case "smalldatetime":
                return FieldType.DateTime;
        }
        return FieldType.String;
    }

    private string GetHtml(int pageSize, int pageIndex, DataSet data, bool isOnePage)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(this.GenOutputCssJS());
        builder.AppendLine(this.GenClearJS());
        if ((this.WhereList.Count > 0) || this.IsExp)
        {
            builder.AppendLine(this.GenOutputConfig());
        }
        if (!OnlyGrid)
        {
            if (HideTitle)
            {
                builder.AppendLine(this.GenQueryHtml());
            }
            else
            {
                builder.AppendLine("<div id=\"upRes" + this.Tn +
                                  "\" style=\"border: 0px solid;color:red;letter-spacing:6mm;font-size:xx-large;display:none;\"></div>");
                builder.AppendLine("<table width=\"98%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                builder.AppendLine("<tr>");
                builder.AppendLine("<td>");
                builder.AppendLine(this.GenQueryHtml());
                builder.AppendLine("</td>");
                builder.AppendLine("</tr>");
                builder.AppendLine("<tr>");
                builder.AppendLine("<td>");
            }
        }
        else
        {
            builder.AppendLine(GenGroupTitle());
        }
        builder.AppendLine("<table class=\"bgDark ListTableBlue\" cellspacing=\"1\" cellpadding=\"4\" border=\"0\" style=\"width: 100%;\">");
        builder.AppendLine("<tr class=\"bgColor2\">");
        if ((data != null) || (HttpContext.Current == null))
        {
            builder.AppendLine(this.GenFieldDescNHtml());
            if (((this.IsDel && this.DelBtnVisible) || (this.IsEdit && this.EditBtnVisible)) || (this.IsCustomBtn && this.CustomBtnVisible))
            {
                if (this.IsCustomBtn && this.CustomBtnVisible)
                {
                    builder.Append("<th  scope=\"col\" width=\"10%\">");
                }
                else
                {
                    builder.Append("<th  scope=\"col\" width=\"7%\">");
                }
                builder.Append("操作");
                builder.AppendLine("</th>");
            }
        }
        builder.AppendLine("</tr>");
        if ((data != null) && (data.Tables.Count > 0))
        {
            foreach (DataRow row in data.Tables[0].Rows)
            {
                builder.AppendLine("<tr class=\"trList\" align=\"center\" bgcolor=\"#FFFFFF\" onmouseover=\"this.bgColor='yellow'\" onmouseout=\"this.bgColor='#FFFFFF'\">");
                builder.Append(this.GenDataRowHtml(row));
                builder.AppendLine("</tr>");
            }
        }
        builder.AppendLine(string.Concat(new object[] { "</table><input type=\"hidden\" value=\"", pageIndex, "\" id=\"txtP", this.Tn, "\" name=\"txtP", this.Tn, "\" /><input type=\"hidden\" value=\"\" id=\"txtDel", this.Tn, this.EditWhereField, "\" name=\"txtDel", this.Tn, this.EditWhereField, "\" />" }));
        builder.AppendLine("<input type=\"hidden\" value=\"\" id=\"txtExp" + this.Tn + "\" name=\"txtExp" + this.Tn + "\" /><input type=\"hidden\" value=\"\" id=\"txtSortFieldName" + this.Tn + "\" name=\"txtSortFieldName" + this.Tn + "\" /><input type=\"hidden\" value=\"\" id=\"txtBtnType" + this.Tn + "\" name=\"txtBtnType" + this.Tn + "\" /><input type=\"hidden\" value=\"\" id=\"txtCustom" + this.Tn + "\" name=\"txtCustom" + this.Tn + "\" />");
        if ((data != null) && (data.Tables.Count > 1))
        {
            if (this.IsWhereAllData && isOnePage)
            {
                pageIndex = 1;
                pageSize = 0x7fffffff;
            }
            AjaxPageList pi = new AjaxPageList();
            pi.PageSize = pageSize;
            pi.PageNumber = pageIndex;
            pi.Tn = this.Tn;
            pi.Count = MyControl_FilterHelper.GetIntOne(data.Tables[1].Rows[0][0]);
            pi.IsPageInput = this.IsPageInput;
            builder.AppendLine(AjaxPageList.Show(pi));
        }
        if (!OnlyGrid)
        {
            if (!HideTitle)
            {
                builder.AppendLine("</td>");
                builder.AppendLine("</tr>");
                builder.AppendLine("</table>");
            }
        }
        return builder.ToString();
    }

    private FieldParam GetWhereParamFieldInfo(WhereParam where)
    {
        if (!string.IsNullOrEmpty(where.FieldName))
        {
            foreach (FieldParam param in this.fieldList)
            {
                if (where.FieldName.Equals(param.FieldName, StringComparison.OrdinalIgnoreCase))
                {
                    return param;
                }
            }
        }
        return null;
    }

    public void BindData(HtmlTextWriter writer)
    {
        if (string.IsNullOrEmpty(this.DbCategory) || string.IsNullOrEmpty(this.TableName))
        {
            writer.Write("");
        }
        else
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    MyControl_FilterHelper.GetIntOne(HttpContext.Current.Request.Form["txtP" + this.Tn]);
                }
                string str = this.SelectHtml();
                writer.Write(str);
            }
            catch (Exception exception)
            {
                writer.Write(exception);
            }
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        BindData(writer);
    }

    private void Reprot(string fileName, HttpResponse Response, DataTable dt)
    {
        MyControl_SaveCSV ecsv = new MyControl_SaveCSV(Response);
        string fullPath = string.Format("{0}_{1}.csv", fileName, DateTime.Now.ToString("yyyyMMddHHmmss"));
        string content = this.GetContent(dt);
        ecsv.OutFile(fullPath, content);
    }

    private string SelectHtml()
    {
        if ((!string.IsNullOrEmpty(this.devConnStr) && !string.IsNullOrEmpty(this.devConnStr.Split(new char[] { ';' })[0].Split(new char[] { '=' })[1])) && (!string.IsNullOrEmpty(this.devConnStr.Split(new char[] { ';' })[1].Split(new char[] { '=' })[1]) && !string.IsNullOrEmpty(this.TableName)))
        {
            if (this.fieldList == null)
            {
                this.fieldList = new List<FieldParam>();
            }
            if (this.fieldList.Count == 0)
            {
                this.SetFieldList("U");
            }
            if (this.fieldList.Count == 0)
            {
                this.SetFieldList("V");
            }
        }
        int pageIndex = 1;
        if (HttpContext.Current != null)
        {
            pageIndex = MyControl_FilterHelper.GetIntOne(HttpContext.Current.Request.Form["txtP" + this.Tn]);
        }
        this.UpdateFieldListInfo();
        this.UpdateWhereParamFieldInfo();
        this.UpdateWhereParamValue();
        if ((this.IsInitData && (HttpContext.Current != null)) && (HttpContext.Current.Request.Form["txtBtnType" + this.Tn] == null))
        {
            return this.GetHtml(this.PageSize, pageIndex, null, false);
        }
        this.SetWhereFieldCookies();
        int pageSize = this.PageSize;
        bool flag = (((this.IsExp && this.ExpBtnVisible) && ((HttpContext.Current != null) && string.IsNullOrEmpty(HttpContext.Current.Request.Form["txtDel" + this.Tn + this.EditWhereField]))) && !string.IsNullOrEmpty(HttpContext.Current.Request.Form["txtExp" + this.Tn])) && (HttpContext.Current.Request.Form["txtExp" + this.Tn] == "exp");
        if (flag)
        {
            pageIndex = 1;
            pageSize = 0x7fffffff;
        }
        string tableName = this.TableName.ToUpper();
        if (this.AddNoLock)
        {
            tableName = tableName.Replace(" WITH(NOLOCK)", "") + " WITH(NOLOCK)";
        }
        bool isOnePage = false;
        string connStr = GetConnectionString();

        DataSet data = AdminData.ExecuteDataset(this.DelName, connStr, tableName, this.ProcName, this.OrderBy, pageSize, pageIndex, this.isWhereList, this.IsDel, this.IsRealDel, this.DelFieldName, this.DelFieldValue, this.EditWhereField, this.SysWhere, this.fieldList, this.IsCustomBtn, this.DelUpdateTimeField, this.Tn, this.IsWhereAllData, out isOnePage);
        if (flag)
        {
            if (HttpContext.Current.Request.UserAgent.IndexOf("firefox", StringComparison.OrdinalIgnoreCase) == -1)
            {
                this.Reprot(HttpUtility.UrlEncode(this.TableDescN, Encoding.UTF8), this.Page.Response, data.Tables[0]);
            }
            else
            {
                this.Reprot(this.TableDescN, this.Page.Response, data.Tables[0]);
            }
        }
        return this.GetHtml(pageSize, pageIndex, data, isOnePage);
    }

    private void SetFieldList(string schemaType)
    {
        string str2;
        StringBuilder builder = new StringBuilder();
        string str = this.TableName.ToLower().Replace(" with(nolock)", "");
        if (((str2 = schemaType) != null) && (str2 == "V"))
        {
            builder.Append("SELECT a.name N'FieldName',");
            builder.Append(" isnull(g.[value],a.name) AS N'FieldDescn',");
            builder.Append(" b.name AS N'FieldType',");
            builder.Append(" a.length AS N'FieldLen'");
            builder.Append(" FROM syscolumns a left join systypes b on a.xtype=b.xusertype inner join sysobjects d  on a.id=d.id");
            builder.Append(" and d.xtype='V'");
            builder.Append(" and d.name<>'dtproperties' left join syscomments e on a.cdefault=e.id");
            builder.Append(" left join sys.extended_properties g on a.id=g.major_id AND a.colid = g.minor_id");
            builder.Append(" WHERE d.NAME='" + str + "'");
            builder.Append(" order by object_name(a.id),a.colorder");
        }
        else
        {
            builder.Append("SELECT");
            builder.Append(" FieldName=C.name,");
            builder.Append(" FieldDescn=ISNULL(PFD.[value],C.name),");
            builder.Append(" FieldType=T.name,");
            builder.Append(" FieldLen=C.max_length");
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
            builder.Append(" Sort=CASE INDEXKEY_PROPERTY(IDXC.[object_id],IDXC.index_id,IDXC.index_column_id,'IsDescending')");
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
            builder.Append(" WHERE O.name='" + str + "' ");
            builder.Append(" ORDER BY O.name,C.column_id");
        }
        DataTable table = null;
        try
        {
            table = MyControl_SqlHelper.ExecuteDataset(this.DevConnStr, CommandType.Text, builder.ToString(), new SqlParameter[0]).Tables[0];
            if ((table != null) && (table.Rows.Count > 0))
            {
                foreach (DataRow row in table.Rows)
                {
                    FieldParam item = new FieldParam();
                    item.FieldName = row[0].ToString();
                    item.FieldDescN = row[1].ToString().Trim();
                    item.Title = row[1].ToString().Trim();
                    item.FieldType = GetFieldType(row[2].ToString().ToLower());
                    item.FieldLen = (MyControl_FilterHelper.GetIntZero(row[3]) == -1) ? 0xfa0 : MyControl_FilterHelper.GetIntZero(row[3]);
                    this.fieldList.Add(item);
                }
            }
        }
        catch
        {
        }
    }

    private void SetWhereFieldCookies()
    {
        if (this.SaveCondition && (HttpContext.Current != null))
        {
            string absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
            if ((this.isWhereList != null) && (this.isWhereList.Count > 0))
            {
                foreach (WhereParam param in this.isWhereList)
                {
                    if (!string.IsNullOrEmpty(param.FieldValue))
                    {
                        HttpCookie cookie = new HttpCookie(absolutePath + "_" + param.FieldNameN);
                        cookie.Value = param.FieldValue.ToString();
                        //cookie.Expires = DateTime.Now.AddDays(1.0);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        var cookie =HttpContext.Current.Response.Cookies.Get(absolutePath + "_" + param.FieldNameN);
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                    }
                }
            }
        }
    }

    private void UpdateFieldListInfo()
    {
        if (HttpContext.Current == null)
        {
            foreach (FieldParam param in this.fieldList)
            {
                param.IsOutput = true;
            }
        }
        else
        {
            string str = MyControl_FilterHelper.GetString(HttpContext.Current.Request.Form[this.OUTPUT_COLUMNS]);
            string str2 = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[HttpContext.Current.Request.Url.AbsolutePath + "$" + this.OUTPUT_COLUMNS];
            if (cookie != null)
            {
                str2 = cookie.Value;
            }
            if (string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
            {
                str = str2;
            }
            if (!string.IsNullOrEmpty(str))
            {
                foreach (FieldParam param2 in this.fieldList)
                {
                    param2.IsOutput = false;
                }
                string[] strArray = str.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    this.fieldList[int.Parse(strArray[i])].IsOutput = true;
                }
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(HttpContext.Current.Request.Url.AbsolutePath + "$" + this.OUTPUT_COLUMNS, str));
            }
            else
            {
                foreach (FieldParam param3 in this.fieldList)
                {
                    param3.IsOutput = true;
                }
            }
        }
    }

    private void UpdateWhereParamFieldInfo()
    {
        if ((((HttpContext.Current != null) && (this.isWhereList != null)) && ((this.isWhereList.Count > 0) && (this.fieldList != null))) && (this.fieldList.Count > 0))
        {
            foreach (WhereParam param in this.isWhereList)
            {
                FieldParam whereParamFieldInfo = this.GetWhereParamFieldInfo(param);
                if (whereParamFieldInfo != null)
                {
                    param.FieldType = whereParamFieldInfo.FieldType;
                    param.FieldLen = whereParamFieldInfo.FieldLen;
                    if (((param.Operator == AdminCompare.In) && string.IsNullOrEmpty(param.DefaultValue)) && (whereParamFieldInfo.SltList != null))
                    {
                        string str = "";
                        foreach (StatusList list in whereParamFieldInfo.SltList)
                        {
                            str = str + list.Value + ",";
                        }
                        if (str.Length > 0)
                        {
                            param.DefaultValue = str.Substring(0, str.Length - 1);
                        }
                    }
                }
            }
        }
    }

    private void UpdateWhereParamValue()
    {
        string absolutePath = string.Empty;
        bool flag = false;
        if (HttpContext.Current != null)
        {
            absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
            flag = HttpContext.Current.Request.Url.PathAndQuery.Contains("?");
        }
        else
        {
            return;
        }
        if ((this.isWhereList != null) && (this.isWhereList.Count > 0))
        {
            foreach (WhereParam param in this.isWhereList)
            {
                if (param.IsDisable)
                {
                    var f = MyControl_FilterHelper.GetString(HttpContext.Current.Request.Form[param.FieldNameN]);
                    if (!string.IsNullOrEmpty(f) && f == "clear")
                    {
                        param.FieldValue = "";
                        param.DefaultValue = "";
                        continue;
                    }
                    if (!string.IsNullOrEmpty(param.FieldValue))
                    {
                        if (param.FieldValue == "clear")
                        {
                            param.FieldValue = "";
                            param.DefaultValue = "";
                            continue;
                        }
                        param.DefaultValue = "";
                        continue;
                    }
                    if (!string.IsNullOrEmpty(param.DefaultValue) && param.DefaultValue == "clear")
                    {
                        param.FieldValue = "";
                        param.DefaultValue = "";
                        continue;
                    }
                }
                bool flag2 = (((HttpContext.Current != null) && (HttpContext.Current.Request.Form["txtBtnType" + this.Tn] == null)) && (!flag && this.SaveCondition)) && (HttpContext.Current.Request.Cookies[absolutePath + "_" + param.FieldNameN] != null);
                if (!param.IsDisable)
                {
                    param.FieldValue = "";
                }
                if (param.FieldType == FieldType.DateTime)
                {
                    if (HttpContext.Current != null)
                    {
                        if (flag2)
                        {
                            param.FieldValue = HttpContext.Current.Request.Cookies[absolutePath + "_" + param.FieldNameN].Value;
                        }
                        else
                        {
                            param.FieldValue = HttpContext.Current.Request.Form[param.FieldNameN + "1"] + "," + HttpContext.Current.Request.Form[param.FieldNameN + "2"];
                        }
                    }
                    continue;
                }
                param.DefaultValue = MyControl_FilterHelper.GetString(param.DefaultValue);
                if (HttpContext.Current != null)
                {
                    if (flag2)
                    {
                        param.FieldValue = HttpContext.Current.Request.Cookies[absolutePath + "_" + param.FieldNameN].Value;
                    }
                    else
                    {
                        param.FieldValue = MyControl_FilterHelper.GetString(HttpContext.Current.Request.Form[param.FieldNameN]);
                        if (HttpContext.Current.Request.Form["txtBtnType" + this.Tn] == null)
                        {
                            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[param.FieldNameN]))
                            {
                                param.FieldValue = MyControl_FilterHelper.GetString(HttpContext.Current.Request.QueryString[param.FieldNameN]);
                            }
                            if (string.IsNullOrEmpty(param.FieldValue.ToString()))
                            {
                                param.FieldValue = MyControl_FilterHelper.GetString(param.FieldDefaultValue);
                            }
                        }
                    }
                }
                else
                {
                    param.FieldValue = MyControl_FilterHelper.GetString(param.FieldDefaultValue);
                }
                if ((param.FieldValue.ToString().Contains("<") || param.FieldValue.ToString().Contains(">")) || ((param.Operator != AdminCompare.Like) && param.FieldValue.ToString().Contains("'")))
                {
                    throw new ArgumentException("值" + param.FieldValue + "包含有特殊符号", param.FieldName);
                }
            }
        }
    }

    public static string GetSelectZoneFromCookie(HttpContext context)
    {
        if (context.Request.Cookies["SZoneCookie"] != null)
        {
            return context.Request.Cookies["SZoneCookie"].Value;
        }
        return "";
    }

    public static void SetSelectZoneToCookie(HttpContext context,string zone)
    {
        if(string.IsNullOrEmpty(zone))
            return;
        var cookie = new HttpCookie("SZoneCookie");
        cookie.Value = zone;
        context.Response.Cookies.Add(cookie);
        if (context.Request.Cookies["SZoneCookie"] != null)
        {
            context.Request.Cookies["SZoneCookie"].Value=zone;
        }
    }

    // Properties
    [Browsable(false), Description("新增按钮是否有权限显示"), Category("权限")]
    public bool AddBtnVisible
    {
        get
        {
            return this._addBtnVisible;
        }
        set
        {
            this._addBtnVisible = value;
        }
    }

    [Category("查询"), Browsable(true), Description("自动为表名添加：WITH(NOLOCK)")]
    public bool AddNoLock
    {
        get
        {
            return this._addNolock;
        }
        set
        {
            this._addNolock = value;
        }
    }

    [Browsable(true), Description("数据库类型"), Category("查询")]
    public string DbCategory { get; set; }

    [Category("权限"), Browsable(false), Description("自定义按钮是否有权限显示")]
    public bool CustomBtnVisible
    {
        get
        {
            return this._customBtnVisible;
        }
        set
        {
            this._customBtnVisible = value;
        }
    }

    [Description("删除按钮是否有权限显示"), Category("权限"), Browsable(false)]
    public bool DelBtnVisible
    {
        get
        {
            return this._delBtnVisible;
        }
        set
        {
            this._delBtnVisible = value;
        }
    }

    [Description("需要更新的字段，删除只是做更新操作，当IsDel为True，ProcName为空，IsRealDel为False时，必填！"), Category("编辑"), Browsable(true)]
    public string DelFieldName{ get; set; }

    [Browsable(true), Description("需要更新的值，删除只是做更新操作，当IsDel为True，ProcName为空，IsRealDel为False时，必填！"), Category("编辑")]
    public string DelFieldValue{ get; set; }

    [Category("编辑"), Description("显示删除的链接名"), Browsable(true)]
    public string DelName{ get; set; }

    [Description("做删除操作时需要更新操作时间的字段，如UpdateTime"), Browsable(true), Category("编辑")]
    public string DelUpdateTimeField{ get; set; }

    [Category("查询"), Browsable(true), Description("开发时使用的数据库连接字符串，与开发时Web.config里的配置一样，开发完成之后可以清空")]
    public string DevConnStr
    {
        get
        {
            return this.devConnStr;
        }
        set
        {
            this.devConnStr = value;
        }
    }

    [Category("权限"), Browsable(false), Description("编辑按钮是否有权限显示")]
    public bool EditBtnVisible
    {
        get
        {
            return this._editBtnVisible;
        }
        set
        {
            this._editBtnVisible = value;
        }
    }

    [Category("编辑"), Browsable(true), Description("显示编辑功能时的条件参数字段，当IsDel/IsEdit/IsRealDel为True时，必填！")]
    public string EditWhereField{ get; set; }

    [Browsable(false), Category("杂项"), Description("导出按钮是否有权限显示")]
    public bool ExpBtnVisible
    {
        get
        {
            return this._expBtnVisible;
        }
        set
        {
            this._expBtnVisible = value;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), Editor(typeof(DropItemEditor), typeof(UITypeEditor)), Browsable(true), Description("列表显示字段信息集合"), Category("查询")]
    public List<FieldParam> FieldList
    {
        get
        {
            if (this.fieldList == null)
            {
                this.fieldList = new List<FieldParam>();
            }
            return this.fieldList;
        }
        set
        {
            this.fieldList = value;
        }
    }

    [Category("查询"), Browsable(true), Description("文件名")]
    public string FileName{ get; set; }

    [Category("查询"), Browsable(true), Description("如果字段中需要显示图片，此属性是图片的根域，不需要可以留空")]
    public string ImgRootUrl{ get; set; }

    [Browsable(true), Description("是否显示新增功能"), Category("编辑")]
    public bool IsAdd{ get; set; }

    [Category("编辑"), Browsable(true), Description("自定义按钮字段，留空则不显示自定义按钮，暂与IsDel不能共存")]
    public bool IsCustomBtn{ get; set; }

    [Browsable(true), Category("编辑"), Description("是否显示删除功能")]
    public bool IsDel{ get; set; }

    [Category("编辑"), Browsable(true), Description("是否显示更新功能")]
    public bool IsEdit{ get; set; }

    [Browsable(true), Category("编辑"), Description("是否显示导出功能")]
    public bool IsExp{ get; set; }

    [Description("是否查询操作后显示数据"), Browsable(true), Category("查询")]
    public bool IsInitData{ get; set; }

    [Browsable(true), Category("查询"), Description("手动输入页号(true：下拉框，false：输入框，默认为false)")]
    public bool IsPageInput{ get; set; }

    [Browsable(true), Description("是否彻底删除(True彻底删除，False只更新状态)"), Category("编辑")]
    public bool IsRealDel{ get; set; }

    [Description("是否查询操作后显示数据"), Browsable(true), Category("查询")]
    public bool IsWhereAllData { get; set; }

    [Category("查询"), Browsable(true), Description("查询排序")]
    public string OrderBy
    {
        get
        {
            return this._orderBy;
        }
        set
        {
            this._orderBy = value;
        }
    }

    public string OUTPUT_COLUMNS
    {
        get
        {
            return ("OutputColumns" + this.Tn);
        }
    }

    [Category("查询"), Browsable(true), Description("页大小")]
    public int PageSize
    {
        get
        {
            return this._pageSize;
        }
        set
        {
            this._pageSize = value;
        }
    }

    [Category("查询"), Browsable(true), Description("存储过程名(多表删除用)")]
    public string ProcName { get; set; }

    [Browsable(true), Description("自动保存查询条件"), Category("查询")]
    public bool SaveCondition
    {
        get
        {
            return this._saveCondition;
        }
        set
        {
            this._saveCondition = value;
        }
    }

    [Description("系统固定查询条件"), Browsable(true), Category("查询")]
    public string SysWhere { get; set; }

    [Category("查询"), Browsable(true), Description("表/视图的说明描述")]
    public string TableDescN { get; set; }

    [Category("查询"), Description("要查询的表名/视图名"), Browsable(true)]
    public string TableName { get; set; }

    [Category("查询"), Description("是否需要选区"), Browsable(true)]
    public bool ShowZone { get; set; }
     [Category("查询"), Description("直接设置区"), Browsable(true)]
    public string ZoneId { get; set; }

     [Category("查询"), Description("是否只显示grid"), Browsable(true)]
     public bool OnlyGrid { get; set; }

     [Category("查询"), Description("是否隐藏标题"), Browsable(true)]
     public bool HideTitle { get; set; }
    public string Tn
    {
        get
        {
            return this.TableName.ToUpper().Replace(" WITH(NOLOCK)", "").Replace("[","").Replace("]","");
        }
        set
        {
            this._tn = value;
        }
    }

    [Category("编辑"), Description("更新列时的操作关键字"), Browsable(true)]
    public string UpdateColumn
    {
        get
        {
            return this._updateColumn;
        }
        set
        {
            this._updateColumn = value;
        }
    }

    [Category("查询"), Description("查询条件字段信息集合"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), Editor(typeof(WhereItemEditor), typeof(UITypeEditor)), Browsable(true)]
    public List<WhereParam> WhereList
    {
        get
        {
            if (this.isWhereList == null)
            {
                this.isWhereList = new List<WhereParam>();
            }
            return this.isWhereList;
        }
        set
        {
            this.isWhereList = value;
        }
    }
}
}
