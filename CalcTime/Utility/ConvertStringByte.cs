using System;
using System.Collections.Generic;
using System.IO;
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
            // byte[] bytes = new byte[str.Length * sizeof(char)];
            // Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            // byte[] bytes = Encoding.UTF8.GetBytes(str);

            byte[] bytes = new byte[str.Length / 2];
            for (int i =0; i < str.Length / 2; i++)
            {
                bytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            }

            Array.Reverse(bytes);
            return bytes;
        }

        // Littleendian
        public static byte[] GetBytesLittle(string str)
        {
            //byte[] bytes = new byte[str.Length * sizeof(char)];
            //Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            // byte[] bytes = Encoding.UTF8.GetBytes(str);

            byte[] bytes = new byte[str.Length / 2];
            for (int i = 0; i < str.Length / 2; i++)
            {
                bytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            }
            
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
