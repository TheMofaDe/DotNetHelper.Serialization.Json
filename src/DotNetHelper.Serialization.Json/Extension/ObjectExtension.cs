using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetHelper.Serialization.Json.Extension
{
    internal static class ObjectExtension
    {
        internal static void IsNullThrow(this object obj, string name, Exception error = null)
        {
            if (obj != null) return;
            if (error == null) error = new ArgumentNullException(name);
            throw error;
        }

        /// <summary>
        /// Obtains the data as a list; if it is *already* a list, the original object is returned without
        /// any duplication; otherwise, ToList() is invoked.
        /// </summary>
        /// <typeparam name="T">The type of element in the list.</typeparam>
        /// <param name="source">The enumerable to return as a list.</param>
        public static List<T> AsList<T>(this IEnumerable<T> source) => (source == null || source is List<T>) ? (List<T>)source : source.ToList();

    }
}
