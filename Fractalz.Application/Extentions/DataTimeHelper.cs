using System;

namespace Fractalz.Application.Extentions
{
    public static class DataTimeHelper
    {
        public static string ToBeautyTime(this DateTime timeValue)
        {
            /*if (DateTime.Now - timeValue <= TimeSpan.FromMilliseconds(10000))
            {
                return TimeNow();
            }
            else if (DateTime.Now - timeValue <= TimeSpan.FromMinutes(59))
            {
                if (timeValue.Minute  <= 2)
                {
                    return TimeMinutes(timeValue, "минута");
                }
                else if (timeValue.Minute  <= 6)
                {
                    return TimeMinutes(timeValue, "минут");
                }
            }
            else if (DateTime.Now - timeValue <= TimeSpan.FromHours(23))
            {
                if (timeValue.Hour < 2)
                {
                    return TimeHours(timeValue, "час");
                }
                else if (timeValue.Hour < 4)
                {
                    return TimeHours(timeValue, "часа");
                }
                return TimeToday(timeValue);
            }*/
            return Time(timeValue);
        }

        private static string TimeNow()
        {
            return "Сейчас";
        }
        
        private static string TimeMinutes(DateTime timeValue, string minutesValue)
        {
            return $"{timeValue.Minute} {minutesValue} назад";
        }
        
        private static string TimeHours(DateTime timeValue, string hoursValue)
        {
            return $"{timeValue.Hour} {hoursValue} назад";
        }
        
        private static string TimeToday(DateTime timeValue)
        {
            return $"{timeValue.Hour}:{timeValue.Minute}";
        }

        private static string Time(DateTime timeValue)
        {
            return $"{timeValue.ToString("HH:mm")} {timeValue.Day}.{timeValue.Month}.{timeValue.Year}";
        }
    }
}