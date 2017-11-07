using System;
using System.Globalization;

namespace Program
{
    public class Period
    {
        private Period(DateTime firstDate, DateTime secondDate)
        {
            StartingDate = firstDate;
            EndingDate = secondDate;
        }

        public DateTime StartingDate { get; }
        public DateTime EndingDate { get; }

        private static string DateFormatWithoutYear()
        {
            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            return dateTimeFormat.ShortDatePattern.Replace("/yyyy", "").Replace("yyyy/", "").Replace(".yyyy", "")
                .Replace("yyyy.", "");
        }

        private string ShortDate()
        {
            if (StartingDate.Month.Equals(EndingDate.Month))
                return StartingDate.ToString("dd");
            var dateFormat = DateFormatWithoutYear();
            return StartingDate.ToString(dateFormat);
        }


        public override string ToString()
        {
            var firstDate = StartingDate.ToShortDateString();
            var secondDate = EndingDate.ToShortDateString();
            if (StartingDate.Equals(EndingDate))
                return firstDate;
            if (StartingDate.Year.Equals(EndingDate.Year))
                firstDate = ShortDate();
            return $"{firstDate}-{secondDate}";
        }

        public static Period GetInstance(string[] args)
        {
            ArgumentsDateValidator.Validate(args);
            var date1 = DateTime.Parse(args[0]);
            var date2 = DateTime.Parse(args[1]);
            return new Period(date1, date2);
        }
    }
}