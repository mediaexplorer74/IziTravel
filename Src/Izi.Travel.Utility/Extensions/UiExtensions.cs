using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace Izi.Travel.Utility
{
    /// <summary>
    /// Provides common UI-related extension methods
    /// </summary>
    public static class UiExtensions
    {
        /// <summary>
        /// Converts a boolean to a Visibility value (Visible when true, Collapsed when false)
        /// </summary>
        public static Visibility ToVisibility(this bool value)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts a nullable boolean to a Visibility value (Visible when true, Collapsed when false or null)
        /// </summary>
        public static Visibility ToVisibility(this bool? value)
        {
            return value == true ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Splits a string into parts of the specified maximum length
        /// </summary>
        public static IEnumerable<string> SplitBy(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || maxLength <= 0)
            {
                yield return value;
                yield break;
            }

            for (int i = 0; i < value.Length; i += maxLength)
            {
                int length = Math.Min(maxLength, value.Length - i);
                yield return value.Substring(i, length);
            }
        }
    }
}
