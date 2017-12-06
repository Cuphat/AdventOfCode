using System;

namespace AdventOfCode2017
{
    internal static partial class Program
    {
        private static void Main(string[] args)
        {
            var day = args.Length >= 1 ? args[0] : null;

            if (day == null)
            {
                Console.WriteLine("Enter the day.");
                day = Console.ReadLine();
            }

            switch (day)
            {
                case "1":
                    Day1();
                    break;
                case "2":
                    Day2();
                    break;
                case "3":
                    Day3();
                    break;
                case "4":
                    Day4();
                    break;
                case "5":
                    Day5();
                    break;
                default:
                    throw new NotImplementedException("Day specified not implemented.");
            }

            Console.WriteLine("Program complete.");
            Console.ReadKey();
        }
    }
}
