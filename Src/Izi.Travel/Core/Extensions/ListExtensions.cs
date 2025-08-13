using System;
using System.Collections.Generic;
using System.Linq;

namespace Izi.Travel.Core.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Orders a collection based on the specified order list.
        /// Items not found in the order list are placed at the end.
        /// </summary>
        /// <param name="collection">The collection to order</param>
        /// <param name="order">The order in which to sort the collection</param>
        /// <returns>An ordered enumerable of strings</returns>
        public static IEnumerable<string> OrderAs(this IList<string> collection, IList<string> order)
        {
            if (collection == null)
                return null;
                
            if (order == null)
                return collection;
                
            return collection
                .OrderBy(x => IndexOf(order, x, int.MaxValue))
                .ThenBy(x => x);
        }
        
        private static int IndexOf<T>(IList<T> list, T item, int defaultIndex)
        {
            int index = list.IndexOf(item);
            return index >= 0 ? index : defaultIndex;
        }
    }
}
