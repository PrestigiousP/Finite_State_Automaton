using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TP1_Math.helpers
{
    /// <summary>
    /// Class <c>StringHelper</c> This class does some operation to a string.
    /// Author: Frédérick Simard
    /// </summary>
    public class ListHelper
    {
        /// <summary>
        /// This generic method convert a string to list
        /// </summary>
        /// <param name="enumerable">Any IEnumerable to be convert to a list.</param>
        /// <typeparam name="T">Genreric type.</typeparam>
        /// <returns>The IEnumerable coverted to a list.</returns>
        public static IEnumerable ConvertToList<T>(IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }

        /// <summary>
        /// This method check for duplicate in a given list.
        /// </summary>
        /// <param name="list">A list of anytype that will check if there are duplicate value.</param>
        /// <typeparam name="T">Generic anytype</typeparam>
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

        /// <summary>
        /// This method convert a list to a string.
        /// </summary>
        /// <param name="list">An IEnumarable list that will be converted.</param>
        /// <typeparam name="T">A generic type</typeparam>
        /// <returns>A concatenated string of the list</returns>
        public static string ConvertListToString<T>(IEnumerable<T> enumerable)
        {
            List<T> list = enumerable.ToList();
            list.Sort();
            string str = list.Aggregate("", (current, e) => current + (e + ","));
            if (!string.IsNullOrEmpty(str))
                str = str.Substring(0, str.Length - 1);
            return str;
        }
    }
}