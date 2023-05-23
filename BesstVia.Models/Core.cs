using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        public static string EncodeToBase64(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static string DecodeFromBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }
        public static string GetTimeMinify(int time)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;
            var nowDate = Date_GetTimeNow();
            var needDate = Date_UnixTimestampToDateTime(time);

            var ts = new TimeSpan(nowDate.Ticks - needDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "một giây trước" : ts.Seconds + " giây trước";

            if (delta < 2 * MINUTE)
                return "1 phút trước";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " phút trước";

            if (delta < 90 * MINUTE)
                return "1 giờ trước";

            if (delta < 24 * HOUR)
                return ts.Hours + " giờ trước";

            if (delta < 48 * HOUR)
                return ts.Hours + " giờ trước";

            if (delta < 30 * DAY)
                return ts.Days + " ngày trước";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "một tháng trước" : months + " tháng trước";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "một năm trước" : years + " năm trước";
            }
        }
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
        public static DateTime Date_GetStartTimeOfDay(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        public static DateTime Date_GetEndTimeOfDay(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }

        public static bool Date_CheckTime(int timestamp, int day)
        {
            DateTime dateTime = Date_UnixTimestampToDateTime(timestamp);
            // Lấy ngày hiện tại
            DateTime now = DateTime.UtcNow.AddHours(7);

            // So sánh ngày hiện tại với ngày trong timestamp
            TimeSpan diff = now - dateTime.Date;
            if (diff.TotalDays > day)
            {
                return true;
            }
            return false;
        }
        public static string Date_DayToMonthYear(int days)//Số ngày thành tháng hoặc năm
        {
            var years = days / 365;
            var months = (days % 365) / 30;
            if (months > 0)
            {
                return months + " tháng";
            }
            if (years > 0)
            {
                return years + " năm";

            }
            return days + " ngày";
        }
        public static DateTime Date_GetFirstDayInMount()//Ngày đầu tiên của tháng
        {
            return DateTime.UtcNow.AddHours(7).AddDays(1 - DateTime.UtcNow.AddHours(7).Day);
        }
        public static DateTime Date_GetTimeNow()//thời gian hiện tại trả về datetime
        {
            return DateTime.UtcNow.AddHours(7);
        }
        public static string ReplaceMiddleChars(string input)
        {
            if (input.Length <= 4) // Không có chữ cái nào ở giữa
            {
                return input;
            }
            else
            {
                var name = input.First() + "****" + input.Substring(input.Length - 3);
                return name;
            }
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
