using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Games.NBall.Common;

namespace Games.NBall.NB_Web.Helper
{
    public class FilterHelper
    {
        private static FilterHelper _instance = null;

        public static FilterHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FilterHelper();
                return _instance;
            }
        }

        private static char replacechar = '*';
        private static List<string>[] xxxListIndexed;

        private FilterHelper()
        {
            #region 屏蔽字过滤器初始化

            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\bin\\FilterText\\FilterText.txt";
            //System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["FilterText"]);
            string filterText = "";
            try
            {
                FileInfo fi = new FileInfo(fileName);
                FileStream fs = fi.OpenRead();
                StreamReader st = new StreamReader(fs, System.Text.Encoding.Default);
                filterText = st.ReadToEnd();
                st.Close();
                fs.Close();
            }
            catch (System.Exception err)
            {
                LogHelper.Insert(err);
            }

            InitArray(filterText);
            #endregion
        }

        private void InitArray(string postfilter)
        {
            string[] xxxList = GetList(postfilter);
            xxxListIndexed = new List<string>[65536];
            foreach (string item in xxxList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    List<string> xlist = xxxListIndexed[(ushort)item[0]];
                    if (xlist == null)
                    {
                        xlist = new List<string>();
                        xxxListIndexed[(ushort)item[0]] = xlist;
                    }
                    xlist.Add(item);
                }
            }
        }

        #region singleton


        #endregion


        /// <summary>
        /// 检查是否有屏蔽字
        /// </summary>
        /// <param name="content">过滤文本</param>
        /// <returns>有敏感字，返还true</returns>
        public string ScanContent(string content)
        {
            StringBuilder newContent = new StringBuilder(content);
            for (int i = 0; i < content.Length; i++)
            {
                // 看看当前字符是否存在首字符列表当中，如果有，在搜索首字符是这个字符的那堆关键字。
                List<string> foundKeys = xxxListIndexed[content[i]];
                if (foundKeys != null)
                {
                    foreach (string key in foundKeys)
                    {
                        int count = key.Length;
                        if ((content.Length - i) <= count)
                            count = content.Length - i;
                        int index = content.IndexOf(key, i, count);

                        if (index >= 0)
                        {
                            ReplaceKeyWord(index, count, newContent);
                            return newContent.ToString();
                        }
                    }
                }
            }
            return newContent.ToString();
        }

        /// <summary>
        /// Replaces the key word.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <param name="newContent">The new content.</param>
        private void ReplaceKeyWord(int startIndex, int count, StringBuilder newContent)
        {
            newContent.Remove(startIndex, count);
            for (int i = startIndex; i < startIndex + count; i++)
            {
                newContent.Insert(i, replacechar);
            }
        }

        /// <summary>
        /// 获取敏感字列表
        /// </summary>
        /// <param name="postfilter">敏感字符串</param>
        /// <returns></returns>
        public string[] GetList(string postfilter)
        {
            StringBuilder sb = new StringBuilder(postfilter);
            return sb.ToString().Split(',');
        }
    }
}