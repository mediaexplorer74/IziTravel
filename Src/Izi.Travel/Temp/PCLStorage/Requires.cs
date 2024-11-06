// Decompiled with JetBrains decompiler
// Type: PCLStorage.Requires
// Assembly: PCLStorage, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: C962FBF1-A378-45AB-97C6-C265F8F0F86C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.xml

using System;
using System.Diagnostics;
using System.Globalization;

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Common runtime checks that throw ArgumentExceptions upon failure.
  /// </summary>
  internal static class Requires
  {
    private const string Argument_EmptyString = "'{0}' cannot be an empty string (\"\") or start with the null character.";

    /// <summary>
    /// Throws an exception if the specified parameter's value is null.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    /// <param name="value">The value of the argument.</param>
    /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
    /// <returns>The value of the parameter.</returns>
    /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="value" /> is <c>null</c></exception>
    [DebuggerStepThrough]
    public static T NotNull<T>(T value, string parameterName) where T : class
    {
      return (object) value != null ? value : throw new ArgumentNullException(parameterName);
    }

    /// <summary>
    /// Throws an exception if the specified parameter's value is null or empty.
    /// </summary>
    /// <param name="value">The value of the argument.</param>
    /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
    /// <exception cref="T:System.ArgumentException">Thrown if <paramref name="value" /> is <c>null</c> or empty.</exception>
    [DebuggerStepThrough]
    public static void NotNullOrEmpty(string value, string parameterName)
    {
      switch (value)
      {
        case null:
          throw new ArgumentNullException(parameterName);
        case "":
          throw new ArgumentException(Requires.Format("'{0}' cannot be an empty string (\"\") or start with the null character.", (object) parameterName), parameterName);
        default:
          if (value[0] != char.MinValue)
            break;
          goto case "";
      }
    }

    /// <summary>Helper method that formats string arguments.</summary>
    /// <returns>The formatted string.</returns>
    private static string Format(string format, params object[] arguments)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, format, arguments);
    }
  }
}
