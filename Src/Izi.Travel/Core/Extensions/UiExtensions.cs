using System;
using Windows.UI.Xaml;

namespace Izi.Travel.Core.Extensions
{
    public static class UiExtensions
    {
        /// <summary>
        /// Converts a boolean to a Visibility value (Visible/Collapsed)
        /// </summary>
        public static Visibility ToVisibility(this bool value)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts a boolean to a Visibility value (Visible/Collapsed) with the option to invert the logic
        /// </summary>
        public static Visibility ToVisibility(this bool value, bool invert)
        {
            return (value ^ invert) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts a nullable boolean to a Visibility value (Visible/Collapsed)
        /// </summary>
        public static Visibility ToVisibility(this bool? value, bool whenNull = false)
        {
            return value.GetValueOrDefault(whenNull).ToVisibility();
        }
    }
}
