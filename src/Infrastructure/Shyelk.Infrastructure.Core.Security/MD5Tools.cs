using System;
using System.Security.Cryptography;
using System.Text;

namespace Shyelk.Infrastructure.Core.Security
{
    public class MD5Tools
    {
        private MD5Tools() { }
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string str)
        {
            var md5 = MD5.Create();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(str)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
        public static string MD5Encrypt64(string str)
        {
                string cl = str;
              //string pwd = "";
                MD5 md5 = MD5.Create(); //实例化一个md5对像
             // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }
    }
}