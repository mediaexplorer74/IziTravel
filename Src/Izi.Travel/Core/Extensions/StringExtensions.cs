using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace Izi.Travel.Shell.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SafeToUpper(this string source) => source?.ToUpper();

        public static IEnumerable<string> SplitBy(this string source, int chunkSize)
        {
            if (string.IsNullOrEmpty(source) || chunkSize <= 0)
            {
                yield break;
            }

            for (int i = 0; i < source.Length; i += chunkSize)
            {
                int length = Math.Min(chunkSize, source.Length - i);
                yield return source.Substring(i, length);
            }
        }

        public static Visibility ToVisibility(this bool? value)
        {
            return value == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public static Visibility ToVisibility(this bool value)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
