using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TP1_Math
{
    public class Helper
    {
        public static IEnumerable ConvertStringToList <T>(IEnumerable<T> list)
        {
            string str = "";
            List<T> l = new List<T>();
            foreach (var e in list)
            {
                l.Add(e);
            }
            return l;
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
            string str = "";
            List<T> l = list.ToList();
            l.Sort();
            foreach (var e in l)
            {
                str += e + ",";
            }
            if (str != null && str.Length > 0)
                str = str.Substring(0, str.Length - 1);
            return str;
        }
    }
}