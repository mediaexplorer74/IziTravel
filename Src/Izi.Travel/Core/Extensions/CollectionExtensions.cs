using System;
using System.Collections.Generic;
using System.Linq;

namespace Izi.Travel.Core.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Orders the elements of a sequence according to a specified sort order.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list</typeparam>
        /// <param name="source">The sequence to order</param>
        /// <param name="sortOrder">The order in which to sort the elements</param>
        /// <returns>An ordered list</returns>
        public static IList<T> OrderAs<T>(this IList<T> source, IList<T> sortOrder)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (sortOrder == null) return source;

            var orderedList = new List<T>();
            
            // First add items in the specified order
            foreach (var item in sortOrder)
            {
                if (source.Contains(item))
                {
                    orderedList.Add(item);
                }
            }

            // Then add any remaining items
            foreach (var item in source)
            {
                if (!orderedList.Contains(item))
                {
                    orderedList.Add(item);
                }
            }

            return orderedList;
        }

        /// <summary>
        /// Splits a string by a specified separator and trims whitespace
        /// </summary>
        public static string[] SplitBy(this string source, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            if (source == null) return Array.Empty<string>();
            return source.Split(new[] { separator }, options)
                        .Select(s => s.Trim())
                        .Where(s => options != StringSplitOptions.RemoveEmptyEntries || !string.IsNullOrEmpty(s))
                        .ToArray();
        }
    }
}
