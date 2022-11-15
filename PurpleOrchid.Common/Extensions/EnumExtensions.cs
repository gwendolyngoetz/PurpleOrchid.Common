using System.ComponentModel;
using PurpleOrchid.Common.Contracts;

namespace PurpleOrchid.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the value from the enum's DescriptionAttribute if one exists, otherwise it returns the name of the enum.
        /// </summary>
        public static string GetDescription(this Enum source)
        {
            Require.NotNull(nameof(source), source);

            var fieldInfo = source.GetType().GetField(source.ToString());

            var attributes = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[] ?? new DescriptionAttribute[] {};

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return source.ToString();
        }

        /// <summary>
        /// Returns the enum value from the string name of the enum. If no match is found it returns default(T) which would be null in this case.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">String value</param>
        public static T? ParseInsensitiveOrDefault<T>(this string value)
        {
            var result = default(T);

            var items = Enum.GetValues(typeof(T));

            foreach (var item in items)
            {
                if (!(item.ToString() ?? string.Empty).EqualsInsensitive(value))
                {
                    continue;
                }

                result = (T)item;
            }

            return result;
        }

        /// <summary>
        /// Checks if specified enum is in the list
        /// </summary>
        public static bool In(this Enum source, params Enum[] values)
        {
            Require.NotNull(nameof(source), source);
            Require.NotNull(nameof(values), values);

            return values.Contains(source);
        }
    }
}
