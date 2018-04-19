using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Games.MyControl
{
internal class AdminData
{
    // Methods
    private static void Execute(string delName, string connStr, string tableName, string procName, bool isRealDel, string delFieldName, string delFieldValue, string editWhereField, bool IsCustom, string delUpdateTimeField, string tn)
    {
        string str = tableName.ToUpper().Replace("WITH(NOLOCK)", "");
        if (!string.IsNullOrEmpty(procName))
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@" + editWhereField, HttpContext.Current.Request.Form["txtDel" + tn + editWhereField]) };
            if (MyControl_SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, procName, commandParameters) > 0)
            {
                MyControl_MessageBox.Msg(delName + "成功!");
            }
        }
        else if (!isRealDel)
        {
            SqlParameter[] parameterArray2 = null;
            parameterArray2 = new SqlParameter[] { new SqlParameter("@1", delFieldValue), new SqlParameter("@2", HttpContext.Current.Request.Form["txtDel" + tn + editWhereField]) };
            if (IsCustom)
            {
                parameterArray2 = new SqlParameter[] { new SqlParameter("@1", HttpContext.Current.Request.Form["txtCustom" + tn]), new SqlParameter("@2", HttpContext.Current.Request.Form["txtDel" + tn + editWhereField]) };
            }
            if (MyControl_SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, " update " + str + " set " + delFieldName + "=@1" + (string.IsNullOrEmpty(delUpdateTimeField) ? "" : (", " + delUpdateTimeField + "=getdate()")) + " where " + editWhereField + "=@2", parameterArray2) > 0)
            {
                if (IsCustom)
                {
                    delName = "操作";
                }
                MyControl_MessageBox.Msg(delName + "成功!");
            }
        }
        else
        {
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@1", HttpContext.Current.Request.Form["txtDel" + tn + editWhereField]) };
            if (MyControl_SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, " delete from " + str + " where " + editWhereField + "=@1", parameterArray3) > 0)
            {
                MyControl_MessageBox.Msg("彻底删除成功!");
            }
        }
    }

    internal static DataSet ExecuteDataset(string delName, string connStr, string tableName, string procName, string orderBy, int pageSize, int pageIndex, List<WhereParam> whereList, bool isDel, bool isRealDel, string delFieldName, string delFieldValue, string editWhereField, string sysWhere, List<FieldParam> fieldList, bool IsCustom, string delUpdateTimeField, string tn, bool isWhereAllData, out bool isOnePage)
    {
        isOnePage = false;
        if (!string.IsNullOrEmpty(delName) && ((delFieldName.Contains("<") || delFieldName.Contains(">")) || delFieldName.Contains("'")))
        {
            throw new ArgumentException("包含有特殊符号", "delFieldName");
        }
        if (!string.IsNullOrEmpty(editWhereField) && ((editWhereField.Contains("<") || editWhereField.Contains(">")) || editWhereField.Contains("'")))
        {
            throw new ArgumentException("包含有特殊符号", "editWhereField");
        }
        //connStr = (ConfigurationManager.ConnectionStrings[connStr] == null) ? null : ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
        if ((string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(connStr)) || (string.IsNullOrEmpty(connStr.Split(new char[] { ';' })[0].Split(new char[] { '=' })[1]) || string.IsNullOrEmpty(connStr.Split(new char[] { ';' })[1].Split(new char[] { '=' })[1])))
        {
            return null;
        }
        if ((isDel || IsCustom) && (((HttpContext.Current != null) && !string.IsNullOrEmpty(HttpContext.Current.Request.Form["txtDel" + tn + editWhereField])) && string.IsNullOrEmpty(HttpContext.Current.Request.Form["txtExp" + tn])))
        {
            Execute(delName, connStr, tableName, procName, isRealDel, delFieldName, delFieldValue, editWhereField, IsCustom, delUpdateTimeField, tn);
        }
        string str = GetOrderBy(fieldList, tn);
        if (!string.IsNullOrEmpty(str))
        {
            orderBy = "ORDER BY " + str;
        }
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "ORDER BY IDX DESC";
        }
        string commandText = string.Empty;
        string queryCondition = GetQueryCondition(whereList, sysWhere);
        if ((queryCondition != " 1 = 1") && isWhereAllData)
        {
            pageIndex = 1;
            pageSize = 0x7fffffff;
            isOnePage = true;
        }
        if (pageSize != 0)
        {
            commandText = string.Concat(new object[] { "SELECT TOP ", pageSize, " * FROM (SELECT ROW_NUMBER() OVER (", orderBy, ") AS rows ,* FROM ", tableName, " WHERE ", queryCondition, ") AS main_temp WHERE [rows] > (", pageIndex, " -1)*", pageSize, ";SELECT COUNT(1) AS Total FROM ", tableName, " WHERE ", queryCondition });
        }
        else
        {
            commandText = "SELECT * FROM " + tableName + " WHERE " + queryCondition + orderBy;
        }
        HttpContext.Current.Items["txtExp" + tn] = "";
        return MyControl_SqlHelper.ExecuteDataset(connStr, CommandType.Text, commandText, new SqlParameter[0]);
    }

    internal static DataTable GetModelDataTable(string connStr, string tableName, string fileName, string procName, string whereFieldName, string whereValue, List<EditFieldParam> fieldList)
    {
        connStr = (ConfigurationManager.ConnectionStrings[connStr] == null) ? null : ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
        if ((string.IsNullOrEmpty(procName) && string.IsNullOrEmpty(tableName)) || ((string.IsNullOrEmpty(connStr) || string.IsNullOrEmpty(connStr.Split(new char[] { ';' })[0].Split(new char[] { '=' })[1])) || ((string.IsNullOrEmpty(connStr.Split(new char[] { ';' })[1].Split(new char[] { '=' })[1]) || (fieldList == null)) || (fieldList.Count == 0))))
        {
            return null;
        }
        if (!string.IsNullOrEmpty(whereValue) && ((whereValue.Contains("<") || whereValue.Contains(">")) || whereValue.Contains("'")))
        {
            throw new ArgumentException("包含有特殊符号!", "whereValue");
        }
        if ((HttpContext.Current != null) && !string.IsNullOrEmpty(HttpContext.Current.Request.Form["txtEdit"]))
        {
            if (HttpContext.Current.Request.Form["txtEdit"].Equals("edit", StringComparison.OrdinalIgnoreCase) && (UpdateOrInsert(connStr, tableName, procName, fieldList, whereFieldName, whereValue, "edit") > 0))
            {
                MyControl_MessageBox.Msg("修改成功!");
            }
            if (HttpContext.Current.Request.Form["txtEdit"].Equals("add", StringComparison.OrdinalIgnoreCase) && (UpdateOrInsert(connStr, tableName, procName, fieldList, whereFieldName, whereValue, "add") > 0))
            {
                MyControl_MessageBox.Redirect("新增成功!", fileName + "Edit.aspx");
            }
        }
        if (string.IsNullOrEmpty(whereFieldName) || string.IsNullOrEmpty(whereValue))
        {
            return null;
        }
        return MyControl_SqlHelper.ExecuteDataset(connStr, CommandType.Text, "SELECT * FROM " + tableName + " WHERE " + whereFieldName + "='" + whereValue + "'", new SqlParameter[0]).Tables[0];
    }

    private static string GetOrderBy(List<FieldParam> fieldList, string tbName)
    {
        if ((HttpContext.Current.Request.Form["txtBtnType" + tbName] == null) || !HttpContext.Current.Request.Form["txtBtnType" + tbName].Equals("btntype"))
        {
            string str = string.Empty;
            if ((fieldList != null) && (fieldList.Count > 0))
            {
                foreach (FieldParam param in fieldList)
                {
                    param.SortValue = HttpContext.Current.Request.Form["txtSort_" + tbName + param.FieldName];
                    if (param.FieldName.Equals(HttpContext.Current.Request.Form["txtSortFieldName" + tbName]))
                    {
                        if (string.IsNullOrEmpty(param.SortValue) || param.SortValue.Equals(param.FieldName + " asc"))
                        {
                            param.SortValue = param.FieldName + " desc";
                        }
                        else
                        {
                            param.SortValue = param.FieldName + " asc";
                        }
                    }
                    if (param.IsSort && !string.IsNullOrEmpty(param.SortValue))
                    {
                        str = str + param.SortValue + ",";
                    }
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                return str.Trim(new char[] { ',' });
            }
        }
        return null;
    }

    private static string GetQueryCondition(List<WhereParam> whereList, string sysWhere)
    {
        string str = " 1 = 1";
        if ((whereList != null) && (whereList.Count > 0))
        {
            foreach (WhereParam param in whereList)
            {
                if (param.FieldType == FieldType.DateTime)
                {
                    if (((HttpContext.Current != null) && (param.FieldValue != null)) && (!string.IsNullOrEmpty(param.FieldValue.ToString()) && !param.FieldValue.ToString().Equals(",", StringComparison.OrdinalIgnoreCase)))
                    {
                        string[] strArray = param.FieldValue.ToString().Split(new char[] { ',' });
                        string str2 = strArray[0];
                        string str3 = strArray[1];
                        if (string.IsNullOrEmpty(str2))
                        {
                            str2 = "2000-01-01";
                        }
                        if (string.IsNullOrEmpty(str3))
                        {
                            str3 = "2100-01-01";
                        }
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, " ", param.Connector, " [", param.FieldName, "]  BETWEEN '", str2, "' AND '", str3, "'" });
                    }
                    continue;
                }
                if (!param.FieldValue.ToString().Equals(param.DefaultValue.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    if (param.Operator == AdminCompare.Like)
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, " ", param.Connector, " [", param.FieldName, "]  LIKE '%", param.FieldValue, "%'" });
                        continue;
                    }
                    if (param.Operator == AdminCompare.In)
                    {
                        if (!string.IsNullOrEmpty(param.FieldValue.ToString()))
                        {
                            object obj4 = str;
                            str = string.Concat(new object[] { obj4, " ", param.Connector, " [", param.FieldName, "]  IN (" });
                            if (((param.FieldType == FieldType.Int) || (param.FieldType == FieldType.Long)) || ((param.FieldType == FieldType.Float) || (param.FieldType == FieldType.Decimal)))
                            {
                                str = str + param.FieldValue + ")";
                            }
                            else
                            {
                                str = str + "'" + param.FieldValue.ToString().Replace(",", "', '") + "')";
                            }
                        }
                        continue;
                    }
                    if ((param.FieldType == FieldType.String) || (param.FieldType == FieldType.NString))
                    {
                        object obj5 = str;
                        str = string.Concat(new object[] { obj5, " ", param.Connector, " [", param.FieldName, "]  ", GetValueOperator(param.Operator), " '", param.FieldValue, "'" });
                        continue;
                    }
                    object obj6 = str;
                    str = string.Concat(new object[] { obj6, " ", param.Connector, " [", param.FieldName, "]  ", GetValueOperator(param.Operator), " ", param.FieldValue });
                }
            }
        }
        if (!string.IsNullOrEmpty(sysWhere))
        {
            str = str + " and " + sysWhere;
        }
        return str;
    }

    private static string GetValueOperator(AdminCompare valueOperator)
    {
        switch (valueOperator)
        {
            case AdminCompare.Big:
                return ">";

            case AdminCompare.Small:
                return "<";

            case AdminCompare.Equal:
                return "=";

            case AdminCompare.BigEqual:
                return ">=";

            case AdminCompare.SmallEqual:
                return "<=";

            case AdminCompare.Like:
                return "LIKE";

            case AdminCompare.In:
                return "IN";
        }
        return "=";
    }

    private static int UpdateOrInsert(string connStr, string tableName, string procName, List<EditFieldParam> fieldList, string editWhereName, string editWhereValue, string type)
    {
        string commandText = string.Empty;
        string str2 = string.Empty;
        string str3 = string.Empty;
        List<EditFieldParam> list = new List<EditFieldParam>();
        string str4 = string.Empty;
        foreach (EditFieldParam param in fieldList)
        {
            if ((param.FieldType == FieldType.DateTime) && param.IsTimeUpdate)
            {
                str4 = str4 + param.FieldName + "=GETDATE(),";
            }
            if (param.IsEdit && !param.FieldName.Equals(editWhereName, StringComparison.OrdinalIgnoreCase))
            {
                list.Add(param);
            }
        }
        if (!string.IsNullOrEmpty(str4))
        {
            str4 = "," + str4.Remove(str4.Length - 1, 1);
        }
        SqlParameter[] commandParameters = new SqlParameter[list.Count];
        int index = 0;
        int count = list.Count;
        while (index < count)
        {
            string str5 = HttpContext.Current.Request.Form[list[index].FieldName];
            commandParameters[index] = new SqlParameter("@" + list[index].FieldName, string.IsNullOrEmpty(str5) ? list[index].DefaultValue : str5);
            string str6 = commandText;
            commandText = str6 + list[index].FieldName + "=@" + list[index].FieldName + ",";
            str2 = str2 + list[index].FieldName + ",";
            str3 = str3 + "@" + list[index].FieldName + ",";
            index++;
        }
        commandText = commandText.Remove(commandText.Length - 1, 1);
        str2 = str2.Remove(str2.Length - 1, 1);
        str3 = str3.Remove(str3.Length - 1, 1);
        if (type == "edit")
        {
            if (!string.IsNullOrEmpty(procName))
            {
                return MyControl_SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, procName, commandParameters);
            }
            commandText = "update " + tableName + " set " + commandText + str4 + " where " + editWhereName + "='" + editWhereValue + "'";
            return MyControl_SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, commandText, commandParameters);
        }
        if (!string.IsNullOrEmpty(procName))
        {
            return MyControl_SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, procName, commandParameters);
        }
        return MyControl_SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, "insert into " + tableName + "(" + str2 + ") values(" + str3 + ")", commandParameters);
    }
}


}
