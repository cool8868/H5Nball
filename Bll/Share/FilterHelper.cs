using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Games.NBall.Bll.Share
{
    public class FilterHelper
    {
        private static FilterHelper _instance =null;
        static object _lockObj = new object();
        public static FilterHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new FilterHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        private char replacechar = '*';
        string[] xxxList;
        List<string>[] xxxListIndexed = new List<string>[65536];
        public int fiterCharCount = 0;

        private StringBuilder _content;

        private FilterHelper()
        {
            #region 屏蔽字过滤器初始化

            string fileName = ConfigurationManager.AppSettings["FilterText"];
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
            catch// (System.Exception err)
            {
                //Log.WriteLine(err);
            }

            InitArray(filterText);
            #endregion
        }

        public void InitArray(string postfilter)
        {
            xxxList = GetList(postfilter);
            CreateIndex();
        }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string NewContent
        {
            get
            {
                return _content.ToString();
            }
        }


        #region singleton


        public static FilterHelper GetFilter()
        {
            return _instance;
        }


        #endregion


        /// <summary>
        /// 重置索引数组
        /// <param name="postfilter">过滤字文本</param>
        /// </summary>
        public void ResetArray(string postfilter)
        {
            if (xxxList != null)
            {
                Array.Clear(xxxList, 0, xxxList.Length);
            }
            if (xxxListIndexed != null)
            {
                Array.Clear(xxxListIndexed, 0, xxxListIndexed.Length);
            }
            xxxListIndexed = new List<string>[65536];
            xxxList = GetList(postfilter);
            CreateIndex();
        }

        /// <summary>
        /// 创建索引 
        /// </summary>          
        public void CreateIndex()
        {
            foreach (string item in xxxList)
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

        /// <summary>
        /// 检查是否有屏蔽字
        /// </summary>
        /// <param name="content">过滤文本</param>
        /// <returns>有敏感字，返还true</returns>
        public bool ScanContent(string content)
        {
            _content = new StringBuilder(content);
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
                            return true;
                        }
                    }
                }
            }
            return false;
        }
       
        /// <summary>
        /// 替换敏感字
        /// </summary>
        /// <param name="startIndex">起始位置</param>
        /// <param name="count">过滤文字数量</param>
        public void ReplaceKeyWord(int startIndex, int count)
        {
            _content.Remove(startIndex, count);
            for (int i = startIndex; i < startIndex + count; i++)
            {
                _content.Insert(i, replacechar);
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
