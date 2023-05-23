using BestVia.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BestVia.Models
{
    public enum respone_code
    {
        success,
        failed,
        error,
        notfound,
    }
    public class Core
    {
        public static string RandomToken(int length)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomNumberString(int length)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomUpcase(int length)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(min, max);
        }



        public static string Sha256Hash(string Input)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Input));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static DateTime Date_GetTimeNow()//thời gian hiện tại trả về datetime
        {
            return DateTime.UtcNow.AddHours(7);
        }
        public static int Date_GetTimeNowInt()//thời gian hiện tại trả về double
        {
            return Date_DateTimeToUnitUnixTimestamp(Date_GetTimeNow());
        }
        public static DateTime Date_UnixTimestampToDateTime(int unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }

        public static int Date_DateTimeToUnitUnixTimestamp(DateTime input)
        {
            return (int)input.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }


        public static string FilePathRandom(IFormFile file)
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                + Path.GetDirectoryName(file.FileName);

        }
        public static ApiRespone CreateRespone(respone_code respone = respone_code.success, object data = null, string message = null)
        {
            switch (respone)
            {
                case respone_code.success:
                    return new ApiRespone
                    {
                        code = 0,
                        success = true,
                        message = message ?? "success",
                        data = data

                    };
                case respone_code.failed:
                    return new ApiRespone
                    {
                        code = 1,
                        message = message ?? "failed",
                        data = data

                    };
                case respone_code.error:
                    return new ApiRespone
                    {
                        code = 2,
                        message = message ?? "error",
                        data = data

                    };
                case respone_code.notfound:
                    return new ApiRespone
                    {
                        code = 3,
                        message = message ?? "not found",
                        data = data

                    };
                default:
                    return new ApiRespone
                    {
                        code = 4,
                        message = message ?? "other",
                        data = data

                    };

            }



        }


    }
}
