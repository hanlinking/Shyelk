using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shyelk.Tools.Drawing
{
    public class VerificationCode
    {
        private VerificationCode() { }
        private static Color[] color = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        //private static string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
        private static string[] fonts = { "Times New Roman" };
        private static Bitmap bmp;
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="chkCode">验证码内容</param>
        /// <param name="imageFormat">图片格式（默认为Png）</param>
        /// <param name="codeWidth">图片宽度</param>
        /// <param name="codeHeight">图片高度</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns>验证码图片字节流</returns>
        public static byte[] Generate(string chkCode, DrawFormat imageFormat = DrawFormat.Png, int codeWidth = 80, int codeHeight = 30, int fontSize = 16)
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
            for (int i = 0; i < 5; i++)
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
                Font ft = new Font(fnt, fontSize,FontStyle.Bold);
                
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, (float)0);
            }
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms,GetImageFormat(imageFormat));
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
        private static ImageFormat GetImageFormat(DrawFormat imageFormat)
        {
            switch (imageFormat)
            {
                case DrawFormat.Bmp: return ImageFormat.Bmp;
                case DrawFormat.Emf: return ImageFormat.Emf;
                case DrawFormat.Exif: return ImageFormat.Exif;
                case DrawFormat.Gif: return ImageFormat.Gif;
                case DrawFormat.Icon: return ImageFormat.Icon;
                case DrawFormat.Jpeg: return ImageFormat.Jpeg;
                case DrawFormat.MemoryBmp: return ImageFormat.MemoryBmp;
                case DrawFormat.Tiff: return ImageFormat.Tiff;
                case DrawFormat.Wmf: return ImageFormat.Wmf;
                default: return ImageFormat.Png;
            }
        }
    }
}
