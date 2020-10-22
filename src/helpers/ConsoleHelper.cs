using System;
using System.Threading;

namespace TP1_Math.helpers
{
    /// <summary>
    /// Class <c>ConsoleHelper</c> This class help the console to read and write some stuff
    /// in a more conveniant and clearer way.
    /// Author: Frédérick Simard
    /// </summary>
    static class ConsoleHelper
    {
        /// <summary>
        /// This method ask for an integer by the user with some conditions.
        /// </summary>
        /// <param name="question">The question to write to the console.</param>
        /// <param name="min">The min value allowed.</param>
        /// <param name="max">The max value allowed</param>
        /// <returns></returns>
        public static int AskInteger(string question, int min, int max)
        {
            int valid = 0;
            int value = -1;

            while (valid == 0)
            {
                try
                {
                    Console.Write(question);
                    var val = Console.ReadLine();
                    value = Convert.ToInt32(val);

                    if (value <= max && value >= min)
                    {
                        valid = 1;
                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer un nombre valide.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Veuillez entrer un nombre valide.");
                }
            }

            return value;
        }

        /// <summary>
        /// This method ask for a string.
        /// </summary>
        /// <param name="question">The question to write to the console.</param>
        /// <returns>The string that the user wrote.</returns>
        public static string AskString(string question)
        {
            Console.Write(question);
            string str = Console.ReadLine();
            return str;
        }

        /// <summary>
        /// This method print a string with a delay.
        /// </summary>
        /// <param name="str">The stuff to write to the console.</param>
        /// <param name="sleep">The delay time</param>
        public static void PrintString(string str, int sleep)
        {
            Console.WriteLine(str);
            Thread.Sleep(sleep);
        }
    }
}