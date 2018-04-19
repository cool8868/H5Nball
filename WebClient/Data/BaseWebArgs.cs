using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Games.NBall.WebClient.Data
{
    public abstract class BaseWebArgs : IWebArgs
    {
        protected readonly NameValueCollection _collection = new NameValueCollection();

        #region IWebArgs
        public virtual bool ValidateValue()
        {
            return true;
        }
        public long GetInt64(string key)
        {
            long val = 0;
            long.TryParse(GetValue(key), out val);
            return val;
        }
        public int GetInt32(string key)
        {
            int val = 0;
            int.TryParse(GetValue(key), out val);
            return val;
        }
        public string GetValue(string key)
        {
            return _collection[key] ?? string.Empty;
        }
        public void SetValue(string key, string val)
        {
            _collection.Set(key, val);
        }
        public NameValueCollection GetCollection()
        {
            return _collection;
        }
        public NameValueCollection ToCollection(bool ignoreEmpty, params string[] keys)
        {
            var obj = new NameValueCollection();
            if (null == keys || keys.Length == 0)
                return obj;
            string val = string.Empty;
            foreach (var key in keys)
            {
                val = GetValue(key);
                if (!ignoreEmpty || !string.IsNullOrEmpty(val))
                    obj.Set(key, val);
            }
            return obj;
        }
        public void FromCollection(NameValueCollection collection, bool clearFlag, params string[] keys)
        {
            if (null == collection || collection.Count == 0)
                return;
            if (clearFlag)
                _collection.Clear();
            if (keys == null || keys.Length == 0)
                keys = collection.AllKeys;
            foreach (string key in keys)
            {
                this.SetValue(key, collection.Get(key));
            }
        }
        #endregion

    }
}
