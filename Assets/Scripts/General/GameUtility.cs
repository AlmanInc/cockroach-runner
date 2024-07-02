using System.Globalization;
using UnityEngine;

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

        public static string GetSpaceLessText(string text)
        {
            string result = text.Trim();
            result = result.Replace(" ", "");

            return result;
        }
    }
}