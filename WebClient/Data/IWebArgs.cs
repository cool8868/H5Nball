using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Games.NBall.WebClient.Data
{
    public interface IWebArgs
    {
        bool ValidateValue();
        long GetInt64(string key);
        int GetInt32(string key);
        string GetValue(string key);
        void SetValue(string key, string val);
        NameValueCollection GetCollection();
        NameValueCollection ToCollection(bool ignoreEmpty, params string[] keys);
        void FromCollection(NameValueCollection collection, bool clearFlag, params string[] keys);
    }
}
