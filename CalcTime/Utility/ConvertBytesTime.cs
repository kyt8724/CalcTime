using CalcTime.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcTime.Utility
{
    public class ConvertBytesTime
    {
        private static bool isInvalid { get; set; }

        // Windows date
        public static DateTime ConvertWindowsDate(byte[] bytes)
        {
            try
            {
                if (bytes.Length != 16)
                    throw new ArgumentException();
                return DateTime.FromFileTimeUtc(BitConverter.ToInt16(bytes, 0));
            }
            catch (ArgumentException ex)
            {
                isInvalid = true;
                return DateTime.Now;
            }
        }

        // OLE dates
        public static DateTime ConvertOLEDate(byte[] bytes)
        {
            try
            {
                if (bytes.Length != 8)
                    throw new ArgumentException();
                return DateTime.FromOADate(BitConverter.ToDouble(bytes, 0));
            }
            catch (ArgumentException ex)
            {
                isInvalid = true;
                return DateTime.Now;
            }
        }

        // Unix dates
        public static DateTime ConvertUnixDate(byte[] bytes)
        {
            try
            {
                if (bytes.Length != 4)
                    throw new ArgumentException();
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(BitConverter.ToUInt32(bytes, 0));
            }
            catch (ArgumentException ex)
            {
                isInvalid = true;
                return DateTime.Now;
            }
        }

        // HFS
        public static DateTime ConvertHFSDate(byte[] bytes)
        {
            try
            {
                if (bytes.Length != 4)
                    throw new ArgumentException();
                return new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds(BitConverter.ToUInt32(bytes, 0));
            }
            catch (ArgumentException ex)
            {
                isInvalid = true;
                return DateTime.Now;
            }
        }

        // HFS plus
        public static DateTime ConvertHFSPlusDate(byte[] bytes)
        {
            try
            {
                if (bytes.Length != 4)
                    throw new ArgumentException();
                return new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(BitConverter.ToUInt32(bytes, 0));
            }
            catch (ArgumentException ex)
            {
                isInvalid = true;
                return DateTime.Now;
            }
        }

        // Apple Mac absolute time
        public static DateTime ConvertAppleDate(byte[] bytes)
        {
            try
            {
                if (bytes.Length != 4)
                    throw new ArgumentException();
                return new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(BitConverter.ToUInt32(bytes, 0));
            }
            catch (ArgumentException ex)
            {
                isInvalid = true;
                return DateTime.Now;
            }
        }

        // Google Chrome Value to time
        public static DateTime ConvertGoogleDate(long time)
        {
            try
            {
                long rawTime = 12024081249872950;
                long convertedTime = (rawTime - time) / 1000;

                return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(convertedTime);
            }
            catch (ArgumentException ex)
            {
                isInvalid = true;
                return DateTime.Now;
            }
        }

        #region call convert method

        public static TimeValue CallConvertMethod(TimeValue timeValue)
        {
            var returnValue = timeValue.ReturnTime;
            isInvalid = timeValue.IsInvalid;

            switch (timeValue.TimeFormat)
            {
                case "w64l":
                    returnValue = ConvertWindowsDate(ConvertStringByte.GetBytesLittle(timeValue.ConvertingTime));
                    break;
                case "w64b":
                    returnValue = ConvertWindowsDate(ConvertStringByte.GetBytes(timeValue.ConvertingTime));
                    break;
                case "wcd":
                    returnValue = ConvertWindowsDate(ConvertStringByte.GetBytes(timeValue.ConvertingTime));
                    break;
                case "wf":
                    returnValue = ConvertWindowsDate(ConvertStringByte.GetBytes(timeValue.ConvertingTime));
                    break;
                case "u32l":
                    returnValue = ConvertUnixDate(ConvertStringByte.GetBytesLittle(timeValue.ConvertingTime));
                    break;
                case "u32b":
                    returnValue = ConvertUnixDate(ConvertStringByte.GetBytes(timeValue.ConvertingTime));
                    break;
                case "gcv":
                    returnValue = ConvertGoogleDate(long.Parse(timeValue.ConvertingTime));
                    break;
                case "mat":
                    returnValue = ConvertAppleDate(ConvertStringByte.GetBytes(timeValue.ConvertingTime));
                    break;
                case "m32":
                    ;
                    break;
                case "mww":
                    ;
                    break;
                case "h32l":
                    returnValue = ConvertHFSDate(ConvertStringByte.GetBytesLittle(timeValue.ConvertingTime));
                    break;
                case "h32b":
                    returnValue = ConvertHFSDate(ConvertStringByte.GetBytes(timeValue.ConvertingTime));
                    break;
                case "hp32l":
                    returnValue = ConvertHFSPlusDate(ConvertStringByte.GetBytesLittle(timeValue.ConvertingTime));
                    break;
                case "hp32b":
                    returnValue = ConvertHFSPlusDate(ConvertStringByte.GetBytes(timeValue.ConvertingTime));
                    break;
            }

            if (timeValue.TimelineValue.Substring(0, 1) == "+")
            {
                returnValue.AddHours(double.Parse(timeValue.TimelineValue.Substring(1, 2)));
                returnValue.AddMinutes(double.Parse(timeValue.TimelineValue.Substring(4, 2)));
            }
            else
            {
                returnValue.AddHours(-(double.Parse(timeValue.TimelineValue.Substring(1, 2))));
                returnValue.AddMinutes(-(double.Parse(timeValue.TimelineValue.Substring(4, 2))));
            }
            timeValue.ReturnTime = returnValue;
            timeValue.IsInvalid = isInvalid;
            return timeValue;
        }

        #endregion call convert method
    }
}
