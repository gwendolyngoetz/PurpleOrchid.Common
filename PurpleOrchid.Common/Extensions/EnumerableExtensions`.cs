using PurpleOrchid.Common.Contracts;

namespace PurpleOrchid.Common.Extensions
{
    /// <summary>
    /// Oh the joys of syntactic sugar
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Simple foreach wrapper
        /// </summary>
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            Require.NotNull(nameof(source), source);
            Require.NotNull(nameof(action), action, "Method or lambda passed to extension was null");

            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Flatten string collection into a CSV string
        /// </summary>
        public static string ToCsv(this IEnumerable<string> source)
        {
            return Flatten(source, ",", x => x);
        }

        /// <summary>
        /// Flatten string collection into a delimited string using specified separator character
        /// </summary>
        public static string Flatten(this IEnumerable<string> source, string separator)
        {
            return Flatten(source, separator, x => x);
        }

        /// <summary>
        /// Flatten a collection into a delimited string using specified separator character using the mapper delegate to pick out the desired field(s)
        /// </summary>
        public static string Flatten<T>(this IEnumerable<T> source, string separator, Func<T, string> mapper)
        {
            Require.NotNull(nameof(separator), separator);
            Require.NotNull(nameof(mapper), mapper);

            var enumerable = source as T[] ?? source.ToArray();

            if (!enumerable.Any())
            {
                return string.Empty;
            }

            return string.Join(separator, enumerable.Select(mapper).ToArray());
        }

        /// <summary>
        /// Returns a list with the specified values removed
        /// </summary>
        public static IEnumerable<T> NotIn<T>(this IEnumerable<T> source, Func<T, string> map, params string[] args)
        {
            return source.Where(x => !args.Contains(map(x))).ToList();
        }

        /// <summary>
        /// Returns a list containing only items specified
        /// </summary>
        public static IEnumerable<T> In<T>(this IEnumerable<T> source, Func<T, string> map, params string[] args)
        {
            return source.Where(x => args.Contains(map(x))).ToList();
        }
    }
}
