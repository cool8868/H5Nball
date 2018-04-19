using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.MyControl
{
internal class AjaxPageList
{
    // Fields
    private string _condition;
    private int _count;
    private string _fields;
    private string _keyField;
    private string _orderBy;
    private int _pageCount;
    private int _pageNumber;
    private int _pageSize;
    private string _tableName;
    private string _tn;

    // Methods
    private static int Ceil(int a, int b)
    {
        int result = 0;
        long num2 = Math.DivRem(a, b, out result);
        if (result > 0)
        {
            num2 += 1L;
        }
        return (int) num2;
    }

    public static string Show(AjaxPageList pi)
    {
        if (pi.Count == 0)
        {
            return "<table width=\"98%\" border=\"0\"><tr><td width=\"98%\" align=\"right\" class=\"pagebar\">第 0/0 页, 共 0 条</td></tr></table>";
        }
        StringBuilder builder = new StringBuilder();
        pi.PageCount = Ceil(pi.Count, pi.PageSize);
        int num = (Ceil(pi.PageCount, 10) * 10) - 10;
        int num2 = Ceil(pi.PageNumber, 10) * 10;
        int pageCount = 0;
        if (num2 > pi.PageCount)
        {
            pageCount = pi.PageCount;
        }
        else
        {
            pageCount = num2;
        }

        builder.Append("<table width=\"98%\" border=\"0\"><tr>");
        builder.Append("<td width=\"30%\" align=\"left\" class=\"pagebar\">");
        builder.Append(string.Concat(new object[] { "第", pi.PageNumber, " / ", pi.PageCount, " 页, 共 ", pi.Count, " 条" }));
        builder.Append("</td>");

        builder.Append("<td width=\"70%\" align=\"right\" class=\"pagebar\">");
        if (pi.PageNumber > 1)
        {
            builder.Append("<a class=\"prev\" title='首页' href=\"javascript:void(0);\" onclick=\"try{document.getElementById('txtInputP" + pi.Tn + "').value='';}catch(err){}GetList" + pi.Tn + "(1,'','');return false;\">&lt;&lt;</a>");
        }
        else
        {
            builder.Append("<a class=\"wah\" title='首页'>&lt;&lt;</a>");
        }
        if (pi.PageNumber > 1)
        {
            builder.Append(string.Concat(new object[] { "<a class=\"prev\" href=\"javascript:void(0);\" title='上一页' onclick=\"try{document.getElementById('txtInputP", pi.Tn, "').value='';}catch(err){}GetList", pi.Tn, "(", pi.PageNumber - 1, ",'','');return false;\">&lt;</a>" }));
        }
        else
        {
            builder.Append("<a class=\"wah\" title='上一页'>&lt;</a>");
        }
        if (pi.PageNumber > 10)
        {
            builder.Append(string.Concat(new object[] { "<a href=\"javascript:void(0);\" title='上十页' onclick=\"try{document.getElementById('txtInputP", pi.Tn, "').value='';}catch(err){}GetList", pi.Tn, "(", num2 - 0x13, ",'','');return false;\">...</a>" }));
        }
        for (int i = num2 - 9; i <= pageCount; i++)
        {
            if (i == pi.PageNumber)
            {
                builder.Append("<a class=\"wa\">" + i + "</a>");
            }
            else
            {
                builder.Append(string.Concat(new object[] { "<a href=\"javascript:void(0);\" onclick=\"try{document.getElementById('txtInputP", pi.Tn, "').value='';}catch(err){}GetList", pi.Tn, "(", i, ",'','');return false;\">", i, "</a>" }));
            }
        }
        if ((pi.PageCount > 10) && (pi.PageNumber <= num))
        {
            builder.Append(string.Concat(new object[] { "<a href=\"javascript:void(0);\" title='下十页' onclick=\"try{document.getElementById('txtInputP", pi.Tn, "').value='';}catch(err){}GetList", pi.Tn, "(", num2 + 1, ",'','');return false;\">...</a>" }));
        }
        if (pi.PageNumber < pi.PageCount)
        {
            builder.Append(string.Concat(new object[] { "<a class=\"next\" title='下一页' href=\"javascript:void(0);\" onclick=\"try{document.getElementById('txtInputP", pi.Tn, "').value='';}catch(err){}GetList", pi.Tn, "(", pi.PageNumber + 1, ",'','');return false;\">&gt;</a>" }));
        }
        else
        {
            builder.Append("<a class=\"wah\" title='下一页'>&gt;</a>");
        }
        if (pi.PageNumber < pi.PageCount)
        {
            builder.Append(string.Concat(new object[] { "<a href=\"javascript:void(0);\" onclick=\"try{document.getElementById('txtInputP", pi.Tn, "').value='';}catch(err){}GetList", pi.Tn, "(", pi.PageCount, ",'','');return false;\" title='尾页' class=\"next\">&gt;&gt;</a>" }));
        }
        else
        {
            builder.Append("<a class=\"wah\" title='尾页'>&gt;&gt;</a>");
        }
        if (pi.PageCount > 10)
        {
            if (pi.IsPageInput)
            {
                builder.Append("<select id=\"txtInputP" + pi.Tn + "\" onchange=\"GetList" + pi.Tn + "(this.value,'','')\" />");
                for (int j = 1; j <= pi.PageCount; j++)
                {
                    if (pi.PageNumber == j)
                    {
                        builder.Append("<option selected value=\"" + j + "\">");
                    }
                    else
                    {
                        builder.Append("<option value=\"" + j + "\">");
                    }
                    builder.Append(j);
                    builder.Append("</option>");
                }
                builder.Append("</select>");
            }
            else
            {
                builder.Append(string.Concat(new object[] { "<input type='text' maxlength=\"", pi.PageCount.ToString().Length, "\" onmouseover=\"this.select();\" id='txtInputP", pi.Tn, "' value='", pi.PageNumber, "' size='4' />" }));
                builder.Append(string.Concat(new object[] { 
                    "<input type='button' value='GO' onclick=\"if(document.getElementById('txtInputP", pi.Tn, "').value > ", pi.PageCount, "){document.getElementById('txtInputP", pi.Tn, "').value=", pi.PageCount, ";}if(document.getElementById('txtInputP", pi.Tn, "').value < 1){document.getElementById('txtInputP", pi.Tn, "').value=1;} GetList", pi.Tn, "(document.getElementById('txtInputP", pi.Tn, 
                    "').value ,'','')\"/>"
                 }));
            }
        }
        builder.Append("</td></tr></table>");
        return builder.ToString();
    }

    // Properties
    public string Condition
    {
        get
        {
            return this._condition;
        }
        set
        {
            this._condition = value;
        }
    }

    public int Count
    {
        get
        {
            return this._count;
        }
        set
        {
            this._count = value;
        }
    }

    public string Fields
    {
        get
        {
            return this._fields;
        }
        set
        {
            this._fields = value;
        }
    }

    public bool IsPageInput { get; set; }

    public string KeyField
    {
        get
        {
            return this._keyField;
        }
        set
        {
            this._keyField = value;
        }
    }

    public int MaxValue
    {
        get
        {
            return (this.PageNumber * this.PageSize);
        }
    }

    public int MinValue
    {
        get
        {
            return ((this.PageNumber - 1) * this.PageSize);
        }
    }

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

    public int PageCount
    {
        get
        {
            return this._pageCount;
        }
        set
        {
            this._pageCount = value;
        }
    }

    public int PageNumber
    {
        get
        {
            return this._pageNumber;
        }
        set
        {
            this._pageNumber = value;
        }
    }

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

    public string TableName
    {
        get
        {
            return this._tableName;
        }
        set
        {
            this._tableName = value;
        }
    }

    public string Tn
    {
        get
        {
            return this._tn;
        }
        set
        {
            this._tn = value;
        }
    }
}
}
