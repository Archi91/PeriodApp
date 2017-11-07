using System;

namespace Program
{
    public static class ArgumentsDateValidator
    {
        private static void CheckLength(string[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException("Program accept only 2 parametrs");
        }

        private static void CheckIfProperFormat(string[] args)
        {
            foreach (var arg in args)
                if (!DateTime.TryParse(arg, out var date))
                    throw new FormatException(
                        "Invalid date format.\nThe format should be 'dd.mm.yyyy', where dd - days(from 1 to 31), mm - month(from 1 to 12) and yyyy - years");
        }

        private static void CheckIfSecondDateBiggerThanFirst(string[] args)
        {
            var date1 = DateTime.Parse(args[0]);
            var date2 = DateTime.Parse(args[1]);

            if (date1.CompareTo(date2) > 0)
                throw new ArgumentException("The second date should be bigger than first");
        }

        public static void Validate(string[] args)
        {
            CheckLength(args);
            CheckIfProperFormat(args);
            CheckIfSecondDateBiggerThanFirst(args);
        }
    }
}