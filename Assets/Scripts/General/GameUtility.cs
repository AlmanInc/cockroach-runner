using System.Globalization;
using UnityEngine;
using Zenject.Asteroids;

namespace CockroachRunner
{
    public class GameUtility
    {
        public static string NumberToGroupedStingFormat(int number)
        {
            var format = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            format.NumberGroupSeparator = " ";
            format.NumberDecimalDigits = 0;

            return number.ToString("N", format);
        }

        public static string NumberToStringWithLeadZero(int number)
        {
            return string.Format("{0:00}", number);
        }

        public static string SecondsToFullTimeStringFormat(int time)
        {
            int leftTime = time;

            int hours = (leftTime / 60) / 60;
            leftTime -= hours * 60 * 60;
            int minutes = leftTime / 60;
            leftTime -= minutes * 60;
            int seconds = leftTime;

            string result = $"{GameUtility.NumberToStringWithLeadZero(hours)}:" +
                            $"{GameUtility.NumberToStringWithLeadZero(minutes)}:" +
                            $"{GameUtility.NumberToStringWithLeadZero(seconds)}";

            return result;
        }

        public static string GetSpaceLessText(string text)
        {
            string result = text.Trim();
            result = result.Replace(" ", "");

            return result;
        }
    }
}