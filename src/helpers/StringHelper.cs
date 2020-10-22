using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TP1_Math.helpers
{
    public class StringHelper
    {
        public static IEnumerable ConvertStringToList <T>(IEnumerable<T> list)
        {
            return list.ToList();
        }
        
        public static void ChekDuplicates<T>(List<T> list)
        {
            list.Sort();
            int index = list.Count - 1;
            while (index > 0)
            {
                if (list[index].Equals(list[index - 1]))
                {
                    if (index < list.Count - 1)
                        (list[index], list[^1]) = (list[^1], list[index]);
                    list.RemoveAt(list.Count - 1);
                    index--;
                }
                else
                    index--;
            }
        }
        
        public static string ConvertListToString<T>(IEnumerable<T> list)
        {
            List<T> l = list.ToList();
            l.Sort();
            string str = l.Aggregate("", (current, e) => current + (e + ","));
            if (!string.IsNullOrEmpty(str))
                str = str.Substring(0, str.Length - 1);
            return str;
        }
    }
}