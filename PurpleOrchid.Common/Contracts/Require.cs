using System;
using System.Collections.Generic;
using System.Linq;

namespace PurpleOrchid.Common.Contracts
{
    /// <summary>
    /// Contract class to make it easier and less verbose for precondition method parameter checking.
    /// 
    /// These are all named by what they do. Not going to comment each of them.
    /// </summary>
    public class Require
    {
        public static void NotNull(string paramName, object value, string message = "")
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, message);
            }

            if (value is string stringObj && string.IsNullOrWhiteSpace(stringObj))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        public static void NotEmpty<T>(string paramName, IEnumerable<T> value, string message = "")
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, message);
            }

            if (!value.Any())
            {
                throw new ArgumentOutOfRangeException(paramName, message);
            }
        }

        public static void GreaterThanOrEqual(string paramName, int minValue, int currentValue, string message = "")
        {
            if (currentValue >= minValue)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(paramName, message);
        }

        public static void GreaterThan(string paramName, int minValue, int currentValue, string message = "")
        {
            if (currentValue > minValue)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(paramName, message);
        }

        public static void LessThanOrEqual(string paramName, int maxValue, int currentValue, string message = "")
        {
            if (currentValue <= maxValue)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(paramName, message);
        }

        public static void LessThan(string paramName, int maxValue, int currentValue, string message = "")
        {
            if (currentValue < maxValue)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(paramName, message);
        }
    }
}
