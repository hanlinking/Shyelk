using System;

namespace Shyelk.Infrastructure.Core.Converter
{
    public class BasicDataConverter
    {
        //public static byte[] ObjectToByteArray(Object obj)
        //{
        //     BinaryConverter bf = new BinaryConverter();
        //     return bf.Serialize(obj);
        // }
        // public static T ByteToObject<T>(byte[] arryBytes)
        // {
        //            BinaryConverter bf = new BinaryConverter();
        //     return bf.Deserialize<T>(arryBytes);
        //  }
    }
    public static class BasicDataExtensions
    {
        public static string ToShortGuidString(this Guid guid)
        {
            return guid.ToString("N");
        }
    }

}