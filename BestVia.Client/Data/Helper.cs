using BestVia.Models;
using Radzen;

namespace BestVia.Client.Data
{

    public class Helper
    {
        public static BadgeStyle ToBadgeRole(string roleName)
        {
            switch (roleName)
            {
                case "Admin":
                    return BadgeStyle.Secondary;
                case "SuperMod":
                    return BadgeStyle.Danger;
                case "Mod":
                    return BadgeStyle.Primary;
                default:
                    return BadgeStyle.Info;
                  
            }
        }
        public static BadgeStyle ToBadgeId(int id)
        {
            switch (id)
            {
                case 1:
                    return BadgeStyle.Success;
                case 2:
                    return BadgeStyle.Warning;
                case 3:
                    return BadgeStyle.Danger;
                case 4:
                    return BadgeStyle.Primary;
                case 5:
                    return BadgeStyle.Info;
                case 6:
                    return BadgeStyle.Dark;
                case 7:
                    return BadgeStyle.Light;
                default:
                    return BadgeStyle.Secondary;

            }
        }
        public static string Date_StringTimeConvert(int dateTime)
        {

            var date = Core.Date_UnixTimestampToDateTime(dateTime);
            return date.ToString("dd/MM HH:mm:ss");

        }
        public static string Money_ConvertToString(long money_value)
        {
            if (money_value > 0)
            {
                return money_value.ToString("#,###");
            }
            else
            {
                return money_value.ToString();
            }

        }
        public static void ShowNotification(NotificationService _noti, ApiRespone respone)
        {
            string sum = "Thông báo :";
            if (respone.success)
            {
                _noti.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = sum, Detail = respone.message, Duration = 8000 });

            }
            else
            {
                switch (respone.code)
                {
                    case 1:
                        _noti.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = sum, Detail = respone.message, Duration = 8000 });
                        break;
                    case 2:
                        _noti.Notify(new NotificationMessage { Severity = NotificationSeverity.Warning, Summary = sum, Detail = respone.message, Duration = 8000 });
                        break;
                    case 3:
                        _noti.Notify(new NotificationMessage { Severity = NotificationSeverity.Info, Summary = sum, Detail = respone.message, Duration = 8000 });
                        break;
                    default:
                        _noti.Notify(new NotificationMessage { Summary = sum, Detail = respone.message, Duration = 8000 });
                        break;
                }

            }

        }

    }
}
