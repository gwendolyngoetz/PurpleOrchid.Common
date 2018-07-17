using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using PurpleOrchid.Common.Contracts;

namespace PurpleOrchid.Common.Extensions
{
    /// <summary>
    /// Oh the joys of syntactic sugar
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines if this string and the comparison string have the same value ignoring case.
        /// </summary>
        public static bool EqualsInsensitive(this string source, string value)
        {
            if (source == null && value == null)
            {
                return true;
            }

            if (source == null)
            {
                return false;
            }

            return source.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Wraps string.IsNullOrWhiteSpace(...) to make life simpler, much much simpler.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// Truncate a string
        /// </summary>
        /// <param name="source">Extended object</param>
        /// <param name="maxLength">Maximum number of characters the string can be</param>
        /// <param name="suffix">Characters added to the end of a shortened string. Defaults to "...".</param>
        /// <returns>Full string if it is less than the maxLength. Truncated string if it is over the max length. </returns>
        /// <remarks>The maxLength is adjusted to include the suffix in the final result length</remarks>
        public static string Truncate(this string source, int maxLength, string suffix = "...")
        {
            if (source.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            maxLength = maxLength - suffix.Length;

            if (source.Length > maxLength)
            {
                source = string.Concat(source.Substring(0, maxLength), suffix);
            }

            return source;
        }

        /// <summary>
        /// Convert a string to a nullable decimal.
        /// </summary>
        /// <returns>The decimal value of its string representation or null if failed.</returns>
        public static decimal? ToNullableDecimal(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (!decimal.TryParse(source, out var value))
            {
                return null;
            }

            return value;
        }

        /// <summary>
        /// Convert a string to a decimal.
        /// </summary>
        /// <returns>The decimal value of its string representation or 0 if failed.</returns>
        public static decimal ToDecimal(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return default(decimal);
            }

            if (decimal.TryParse(source, out var result))
            {
                return result;
            }

            return default(decimal);
        }

        /// <summary>
        /// Convert a int value string to a nullable int
        /// </summary>
        /// <returns>The int value of its string representation or null if failed.</returns>
        public static int? ToNullableInt(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (!int.TryParse(source, out var value))
            {
                return null;
            }

            return value;
        }

        /// <summary>
        /// Convert a int value string to an int
        /// </summary>
        /// <returns>The int value of its string representation or 0 if failed.</returns>
        public static int ToInt(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return default(int);
            }

            if (int.TryParse(source, out var result))
            {
                return result;
            }

            return default(int);
        }

        /// <summary>
        /// Convert a date string to a nullable DateTime
        /// </summary>
        public static DateTime? ToNullableDate(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (!DateTime.TryParse(source, out var value))
            {
                return null;
            }

            return value;
        }

        /// <summary>
        /// Convert a DateTime value string to an DateTime
        /// </summary>
        /// <returns>The DateTime value of its string representation or DateTime.MinValue if failed.</returns>
        public static DateTime ToDateTime(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return DateTime.MinValue;
            }

            if (!DateTime.TryParse(source, out var value))
            {
                return DateTime.MinValue;
            }

            return value;
        }

        /// <summary>
        /// Returns null if null or empty. Otherwise the string
        /// </summary>
        public static string NullIfEmpty(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return null;
            }

            return source;
        }

        /// <summary>
        /// Sugar around string.format
        /// </summary>
        public static string FormatString(this string source, params object[] args)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return null;
            }

            Require.NotEmpty("args", args);

            return string.Format(source, args);
        }

        /// <summary>
        /// Convert a double value string to a nullable double
        /// </summary>
        /// <returns>The double value of its string representation or null if failed.</returns>
        public static double? ToNullableDouble(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (!double.TryParse(source, out var value))
            {
                return null;
            }

            return value;
        }

        /// <summary>
        /// Convert a double value string to an double
        /// </summary>
        /// <returns>The double value of its string representation or 0 if failed.</returns>
        public static double ToDouble(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return default(double);
            }

            if (double.TryParse(source, out var result))
            {
                return result;
            }

            return default(double);
        }

        /// <summary>
        /// Convert a bool value string to a nullable bool
        /// </summary>
        /// <returns>The bool value of its string representation or null if failed.  Will also consider "yes" and "1" for result of true, "no" and "0" for result of false.</returns>
        public static bool? ToNullableBool(this string source)
        {
            const string yes = "yes";
            const string no = "no";
            const string one = "1";
            const string zero = "0";

            if (source.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (bool.TryParse(source, out var value))
            {
                return value;
            }

            if (source.EqualsInsensitive(yes))
            {
                return true;
            }
            
            if (source.EqualsInsensitive(no))
            {
                return false;
            }

            if (source.EqualsInsensitive(one))
            {
                return true;
            }
            
            if(source.EqualsInsensitive(zero))
            {
                return false;
            }

            return null;
        }

        /// <summary>
        /// Convert a bool value string to an bool
        /// </summary>
        /// <returns>The bool value of its string representation or false if failed. Will also consider "yes" and "1" for result of true.</returns>
        public static bool ToBool(this string source)
        {
            const string yes = "yes";
            const string one = "1";

            if (source.IsNullOrWhiteSpace())
            {
                return false;
            }

            if (bool.TryParse(source, out var result))
            {
                return result;
            }

            return source.EqualsInsensitive(yes) || source.EqualsInsensitive(one);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string source) where T : struct
        {
            Require.NotNull(nameof(source), source);

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentOutOfRangeException($"{typeof(T).Name} is not an enum");
            }

            try
            {
                return (T)Enum.Parse(typeof(T), source, true);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException($"Unable to parse enum '{typeof(T).Name}' for {source}", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T? ToNullableEnum<T>(this string source) where T : struct
        {
            if (source == null)
            {
                return null;
            }

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentOutOfRangeException($"{typeof(T).Name} is not an enum");
            }

            try
            {
                return (T)Enum.Parse(typeof(T), source, true);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException($"Unable to parse enum '{typeof(T).Name}' for {source}", ex);
            }
        }

        /// <summary>
        /// Replace all instances of multiple oldValues with the single newValue
        /// </summary>
        /// <returns>The new string</returns>
        public static string Replace(this string source, string[] oldValues, string newValue)
        {
            return oldValues.Aggregate(source, (current, oldValue) => current.Replace(oldValue, newValue));
        }

        /// <summary>
        /// Capitalize the first letter of a string
        /// </summary>
        public static string CapitalizeFirstLetter(this string source, bool ofEveryWord = false)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            if (source.Length == 1)
            {
                return source.ToUpper();
            }

            if (!ofEveryWord)
            {
                return source[0].ToString().ToUpper() + source.Substring(1);
            }

            const string enUS = "en-US";
            var textInfo = new CultureInfo(enUS, false).TextInfo;
            
            return textInfo.ToTitleCase(source);
        }

        /// <summary>
        /// Returns true if the string has a value. Basically this is syntactic suger to increase readabillity instead of negating string.IsNullOrWhiteSpace 
        /// </summary>
        public static bool HasValue(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// Returns the value of the string or the specified default if there is no value.
        /// </summary>
        public static string OrDefault(this string source, string defaultValue)
        {
            if (!source.IsNullOrWhiteSpace())
            {
                return source;
            }

            if (!defaultValue.IsNullOrWhiteSpace())
            {
                return defaultValue;
            }

            return string.Empty;
        }

        /// <summary>
        /// Check if string is in list of possible strings
        /// </summary>
        public static bool In(this string source, params string[] values)
        {
            Require.NotNull(nameof(source), source);
            Require.NotEmpty(nameof(values), values);

            return values.Contains(source);
        }

        /// <summary>
        /// Check if string is not in list of possible strings
        /// </summary>
        public static bool NotIn(this string source, params string[] values)
        {
            Require.NotNull(nameof(source), source);
            Require.NotEmpty(nameof(values), values);

            return !values.Contains(source);
        }
    }
}