using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventLibrary.Extensions
{
    public static class Utils
    {
        /// <summary>
        /// Ensures and ienumerable collection has content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="set"></param>
        /// <returns></returns>
        public static bool HasContent<T>(this IEnumerable<T> set)
        {
            return set != null && set.Count() > 0;
        }

        /// <summary>
        /// Checks if collection contains element based on lambda filter criteria
        /// </summary>
        /// <param name="modifier">the lambda expression</param>
        /// <returns></returns>
        public static bool Contains<T>(this IEnumerable<T> collection, Func<T, bool> modifier)
        {
            return collection != null && collection.Count() > 0 && collection.Where(modifier).Any();
        }

        /// <summary>
        /// Remove duplicates from a collection based on attribute list provided in predicate
        /// quite nifty when dealing with NoSQL
        /// </summary>
        /// <typeparam name="T">Type of value in collection</typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate">
        /// Linq expression on which to filter duplicate: i.e: obj => obj.Property1, obj=> obj.Property2
        /// </param>
        public static void DedupCollection<T>(this List<T> collection, params Func<T, object>[] predicateList)
        {
            Dictionary<object, T> dedupKiller = new Dictionary<object, T>();

            collection.ForEach(el =>
            {
                string combinedPredicates = string.Empty;
                foreach (var predicate in predicateList)
                {
                    combinedPredicates += predicate(el).ToString();
                }
                if (!dedupKiller.ContainsKey(combinedPredicates))
                    dedupKiller.Add(combinedPredicates, el);
            });
            collection.Clear();
            collection.AddRange(dedupKiller.Values.ToList<T>());
        }

    }
}
