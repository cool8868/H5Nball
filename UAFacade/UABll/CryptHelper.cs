using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Games.NBall.Common;

namespace Games.NBall.UAFacade
{
    public class CryptHelper
    {
        //获取输入的SHA1哈希值，需要MD5的，请自行修改
        public static string GetSHA1(string input)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] inputData = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] outputData = sha1.ComputeHash(inputData);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < outputData.Length; i++)
            {
                sb.Append(outputData[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the M d5，默认返回小写
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string GetMD5(string input)
        {
            return GetMD5(input, "x2");
        }

        /// <summary>
        /// Gets the M d5.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="outFormat">输出格式，X2|x2.</param>
        /// <returns></returns>
        public static string GetMD5(string input, string outFormat)
        {
            MD5CryptoServiceProvider sha1 = new MD5CryptoServiceProvider();
            byte[] inputData = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] outputData = sha1.ComputeHash(inputData);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < outputData.Length; i++)
            {
                sb.Append(outputData[i].ToString(outFormat));
            }
            return sb.ToString();
        }

        //参数检查和检验签名
        public static string CheckParamsAndSign(IList<string> needSignParamNames, string platformName)
        {
            //PlatformEntity platformEntity;
            //if (!CommonData.PlatformKeys.TryGetValue(platformName, out platformEntity))
            //{
            //    return UAErrorCode.ErrPlatform;
            //}
            //string sig = HttpContext.Current.Request.QueryString[CommonData.SignatureParamName];
            //if (sig == null)
            //{
            //    return UAErrorCode.ErrPara;
            //}
            //List<string> parts = new List<string>();
            //for (int i = 0; i < needSignParamNames.Count; i++)
            //{
            //    string paramName = needSignParamNames[i];
            //    string paramValue = HttpContext.Current.Request.QueryString[paramName];
            //    if (paramValue == null)
            //    {
            //        return UAErrorCode.ErrPara;
            //    }
            //    parts.Add(paramName + "=" + paramValue);
            //}
            //parts.Add(CommonData.SignKeyParamName + "=" + platformEntity.Key);
            //string signInput = string.Join("&", parts.ToArray());
            //string calcSignature = GetSHA1(signInput);
            //if (calcSignature != sig)
            //{
            //    return UAErrorCode.ErrCheckSign;
            //}
            return UAErrorCode.ErrOK;
        }

        //参数检查和检验签名
        public static string CheckParamsAndSignWithKey(IList<string> needSignParamNames, string key)
        {
            string sig = HttpContext.Current.Request.QueryString["sig"];
            if (sig == null)
            {
                return UAErrorCode.ErrPara;
            }
            List<string> parts = new List<string>();
            for (int i = 0; i < needSignParamNames.Count; i++)
            {
                string paramName = needSignParamNames[i];
                string paramValue = HttpContext.Current.Request.QueryString[paramName];
                if (paramValue == null)
                {
                    return UAErrorCode.ErrPara;
                }
                parts.Add(paramName + "=" + paramValue);
            }
            parts.Add("key=" + key);
            string signInput = string.Join("&", parts.ToArray());
            string calcSignature = GetSHA1(signInput);
            if (calcSignature != sig)
            {
                return UAErrorCode.ErrCheckSign;
            }
            return UAErrorCode.ErrOK;
        }

        /// <summary>
        /// 解码字符串
        /// </summary>
        /// <param name="sInputString">输入文本</param>
        /// <returns></returns>
        public static string Base64DecryptString(string sInputString)
        {
            char[] sInput = sInputString.ToCharArray();
            try
            {
                byte[] bOutput = System.Convert.FromBase64String(sInputString);
                return Encoding.UTF8.GetString(bOutput);
            }
            catch (System.ArgumentNullException)
            {
                //base 64 字符数组为null
                return "";
            }
            catch (System.FormatException)
            {
                //长度错误，无法整除4
                return "";
            }
        }

        /// <summary>
        /// 编码字符串
        /// </summary>
        /// <param name="sInputString">输入文本</param>
        /// <returns></returns>
        public static string Base64EncryptString(string sInputString)
        {
            byte[] bInput = Encoding.UTF8.GetBytes(sInputString);
            try
            {
                return System.Convert.ToBase64String(bInput, 0, bInput.Length);
            }
            catch (System.ArgumentNullException)
            {
                //二进制数组为NULL.
                return "";
            }
            catch (System.ArgumentOutOfRangeException)
            {
                //长度不够
                return "";
            }
        }


        /// <summary>
        /// Signatures the deformatter.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="hashDeformatter">The hash deformatter.</param>
        /// <param name="deformatterData">The deformatter data.</param>
        /// <returns></returns>
        public static bool RSASignatureDeformatter(string publicKey, string hashDeformatter, string deformatterData)
        {
            try
            {
                //SysteminfologMgr.Insert("RSASignatureDeformatter", string.Format("publicKey:[{0}] hashDeformatter:[{1}] deformatterData[{2}]", publicKey, hashDeformatter, deformatterData));
                //Log.WriteLine(string.Format("publicKey:[{0}] hashDeformatter:[{1}] deformatterData[{2}]", publicKey, hashDeformatter, deformatterData));

                byte[] rgbHash = Convert.FromBase64String(hashDeformatter);

                RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();

                rsaCryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(publicKey));

                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(rsaCryptoServiceProvider);

                deformatter.SetHashAlgorithm("MD5");

                byte[] rgbSignature = Convert.FromBase64String(deformatterData);

                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }
    }
}