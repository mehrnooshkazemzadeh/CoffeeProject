using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Framework.Core.Extensions
{
    public static class StringExtentions
    {
        public static string UnifyChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var output = input;
            output = output.Replace('ک', (char)1705);
            output = output.Replace('ك', (char)1705);

            output = output.Replace((char)1610, (char)1740);
            output = output.Replace('ي', (char)1740);
            output = output.Replace((char)1746, (char)1740);
            output = output.Replace('ی', (char)1740);
            output = output.Replace((char)1609, (char)1740);
            return output;
        }

        public static string ToUrlFreindly(this string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            //unreserved = ALPHA / DIGIT / "-" / "." / "_" / "~"
            var text = input;
            var reserved = new[] { ':', '/', '?', '#', '[', ']', '@', '!', '$', '&', '\'', '(', ')', '*', '+', ',', ';', '+', '=', '.' };
            foreach (var item in reserved)
            {
                text = text.Replace(item, '-');
            }
            return text.Replace(" ", "-");
        }
        public static DateTime PersianDateToDateTime(this string persianDate)
        {
            var per = new PersianCalendar();
            var dateTimePart = persianDate.Split(' ');

            var datePart = dateTimePart[0].Split('/');

            var year = !string.IsNullOrEmpty(datePart[0]) ? int.Parse(datePart[0], CultureInfo.InvariantCulture.NumberFormat) : 1350;
            var month = !string.IsNullOrEmpty(datePart[1]) ? int.Parse(datePart[1], CultureInfo.InvariantCulture.NumberFormat) : 01;
            var day = !string.IsNullOrEmpty(datePart[2]) ? int.Parse(datePart[2], CultureInfo.InvariantCulture.NumberFormat) : 01;

            var hour = 00;
            var minute = 00;
            var second = 00;

            if (dateTimePart.Length <= 1) return per.ToDateTime(year, month, day, hour, minute, second, 0);

            var timePart = dateTimePart[1].Split(':');
            hour = int.Parse(timePart[0], CultureInfo.InvariantCulture.NumberFormat);
            minute = int.Parse(timePart[1], CultureInfo.InvariantCulture.NumberFormat);

            if (timePart.Length > 2)
            {
                second = int.Parse(timePart[2], CultureInfo.InvariantCulture.NumberFormat);
            }
            return per.ToDateTime(year, month, day, hour, minute, second, 0);
        }
        public static DateTime PersianDateToDate(this string persianDate)
        {
            var per = new PersianCalendar();

            var dateTimePart = persianDate.Split(' ');
            var datePart = dateTimePart[0].Split('/');

            var year = !string.IsNullOrEmpty(datePart[0]) ? int.Parse(datePart[0], CultureInfo.InvariantCulture.NumberFormat) : 1350;
            var month = !string.IsNullOrEmpty(datePart[1]) ? int.Parse(datePart[1], CultureInfo.InvariantCulture.NumberFormat) : 01;
            var day = !string.IsNullOrEmpty(datePart[2]) ? int.Parse(datePart[2], CultureInfo.InvariantCulture.NumberFormat) : 01;

            var hour = 00;
            var minute = 00;
            var second = 00;

            return per.ToDateTime(year, month, day, hour, minute, second, 0);
        }
        public static string ToPersianDateTimeWithMilisecond(this DateTime dateTime)
        {
            var persianCalendar = new System.Globalization.PersianCalendar();
            return
                $"{persianCalendar.GetYear(dateTime)}/{persianCalendar.GetMonth(dateTime).ToString().PadLeft(2, '0')}/{persianCalendar.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0')} {dateTime.Hour.ToString().PadLeft(2, '0')}:{dateTime.Minute.ToString().PadLeft(2, '0')}:{dateTime.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')}.{dateTime.Millisecond}";
        }

        public static string TemplateParser(this string template, Dictionary<string, string> replacements)
        {
            if (replacements.Count > 0)
            {
                template = replacements.Keys
                            .Aggregate(template, (current, key) => current.Replace(key, replacements[key]));
            }
            return template;
        }
        public static T? ToNullable<T>(this string s) where T : struct
        {
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    var conv = TypeDescriptor.GetConverter(typeof(T));
                    var convertFrom = conv.ConvertFrom(s);
                    return (T?)convertFrom;
                }
            }
            catch
            {
                // ignored
            }
            return null;
        }


    }
}
