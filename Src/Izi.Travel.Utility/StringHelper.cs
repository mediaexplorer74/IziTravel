using System;
using System.Globalization;

namespace Izi.Travel.Utility
{
    /// <summary>
    /// Provides consistent string comparison and manipulation methods for the application.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Performs a case-insensitive string comparison optimized for internal string comparisons.
        /// </summary>
        public static bool EqualsOrdinalIgnoreCase(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Performs a culture-aware case-insensitive string comparison.
        /// Use for user-facing string comparisons that need to respect cultural rules.
        /// </summary>
        public static bool EqualsCurrentCultureIgnoreCase(string a, string b)
        {
            return string.Equals(a, b, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Performs a case-insensitive comparison for strings that represent internal identifiers or codes.
        /// Uses OrdinalIgnoreCase for UWP compatibility.
        /// </summary>
        public static bool EqualsInvariantIgnoreCase(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Compares two strings using the specified comparison type.
        /// </summary>
        public static int Compare(string strA, string strB, StringComparison comparisonType)
        {
            return string.Compare(strA, strB, comparisonType);
        }

        /// <summary>
        /// Determines whether a string starts with the specified value using ordinal comparison.
        /// </summary>
        public static bool StartsWithOrdinal(string source, string value)
        {
            if (source == null) return false;
            return source.StartsWith(value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines whether a string starts with the specified value using case-insensitive comparison.
        /// </summary>
        public static bool StartsWithOrdinalIgnoreCase(string source, string value)
        {
            if (source == null) return false;
            return source.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether a string contains the specified value using ordinal comparison.
        /// </summary>
        public static bool ContainsOrdinal(string source, string value)
        {
            if (source == null) return false;
            return source.IndexOf(value, StringComparison.Ordinal) >= 0;
        }

        /// <summary>
        /// Determines whether a string contains the specified value using case-insensitive comparison.
        /// </summary>
        public static bool ContainsOrdinalIgnoreCase(string source, string value)
        {
            if (source == null) return false;
            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
