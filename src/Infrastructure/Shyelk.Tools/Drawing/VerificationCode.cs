using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shyelk.Tools.Drawing
{
    public static class VerificationCode
    {
        private static Color[] color = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        private static string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
        private static Bitmap bmp;
        public static byte[] Generate(string chkCode, int codeWidth = 80, int codeHeight = 30, int fontSize = 16)
        {
            bmp = new Bitmap(codeWidth, codeHeight);
            Random rnd = new Random();
            //颜色列表，用于验证码、噪线、噪点 
            //颜色列表，用于验证码、噪线、噪点 
            //字体列表，用于验证码 
            //字体列表，用于验证码 
            //验证码的字符集，去掉了一些容易混淆的字符 
            //验证码的字符集，去掉了一些容易混淆的字符 

            //写入Session、验证码加密
            //WebHelper.WriteSession("session_verifycode", Md5Helper.MD5(chkCode.ToLower(), 16));
            //创建画布
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 1; i++)
            {
                int x1 = rnd.Next(codeWidth);
                int y1 = rnd.Next(codeHeight);
                int x2 = rnd.Next(codeWidth);
                int y2 = rnd.Next(codeHeight);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = fonts[rnd.Next(fonts.Length)];
                Font ft = new Font(fnt, fontSize);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, (float)0);
            }
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }
    }
}
