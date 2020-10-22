using System;

namespace TP1_Math.helpers
{
    static class ConsoleHelper
    {
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
    }
}
