using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcTime.Utility
{
    public class ConvertStringByte
    {
        // Convert string to byte array
        // Bigendian
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        // Littleendian
        public static byte[] GetBytesLittle(string str)
        {
            byte[] bytes = new byte[str.Length];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            Array.Reverse(bytes);
            return bytes;
        }

        // Convert byte array to string
        // Bigendian
        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        // Littleendian
        public static string GetStringLittle(byte[] bytes)
        {
            Array.Reverse(bytes);
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
