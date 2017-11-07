using System;

namespace Program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var period = Period.GetInstance(args);
                Console.WriteLine(period.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}