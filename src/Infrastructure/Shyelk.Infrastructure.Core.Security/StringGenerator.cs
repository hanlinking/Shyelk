using System.Text;
using System;
namespace Shyelk.Infrastructure.Core.Security
{

    /// <summary>
    /// 字符串生成器
    /// </summary>
    public class StringGenerator
    {
        private static char[] _number = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static char[] _charLower = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static char[] _charUpper = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private StringGenerator()
        {
        }
        public static string CetNumberString(int length)
        {
            ArgumentChecker(length);
            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = rd.Next(10);
                sb.Append(_number[index]);
            }
            return sb.ToString();
        }
        public static string GetCharString(int length)
        {
            ArgumentChecker(length);
            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = rd.Next(26);
                int seed = rd.Next(26);
                if (seed % 2 == 0)
                {
                    sb.Append(_charUpper[index]);
                }
                else
                {
                    sb.Append(_charLower[index]);
                }
            }
            return sb.ToString();
        }
        public static string GetMixString(int length)
        {
            int index;
            ArgumentChecker(length);
            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                int seed = rd.Next(int.MaxValue);
                int value = seed % 3;
                if (value == 0)
                {
                    index = rd.Next(10);
                    sb.Append(_number[index]);
                }
                else
                {
                    index = rd.Next(26);
                    if (value == 1)
                    {
                        sb.Append(_charUpper[index]);
                    }
                    else
                    {
                        sb.Append(_charLower[index]);
                    }
                }
            }
            return sb.ToString();
        }
        private static void ArgumentChecker(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Length must upper than zero");
            }
        }
    }
}