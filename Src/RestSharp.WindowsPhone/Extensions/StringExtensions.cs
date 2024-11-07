// Decompiled with JetBrains decompiler
// Type: RestSharp.Extensions.StringExtensions
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace RestSharp.Extensions
{
  public static class StringExtensions
  {
    public static string UrlDecode(this string input) => HttpUtility.UrlDecode(input);

    /// <summary>
    /// Uses Uri.EscapeDataString() based on recommendations on MSDN
    /// http://blogs.msdn.com/b/yangxind/archive/2006/11/09/don-t-use-net-system-uri-unescapedatastring-in-url-decoding.aspx
    /// </summary>
    public static string UrlEncode(this string input)
    {
      if (input == null)
        throw new ArgumentNullException(nameof (input));
      if (input.Length <= 32766)
        return Uri.EscapeDataString(input);
      StringBuilder stringBuilder = new StringBuilder(input.Length * 2);
      string stringToEscape;
      for (int startIndex = 0; startIndex < input.Length; startIndex += stringToEscape.Length)
      {
        int length = Math.Min(input.Length - startIndex, 32766);
        stringToEscape = input.Substring(startIndex, length);
        stringBuilder.Append(Uri.EscapeDataString(stringToEscape));
      }
      return stringBuilder.ToString();
    }

    public static string HtmlDecode(this string input) => HttpUtility.HtmlDecode(input);

    public static string HtmlEncode(this string input) => HttpUtility.HtmlEncode(input);

    /// <summary>Check that a string is not null or empty</summary>
    /// <param name="input">String to check</param>
    /// <returns>bool</returns>
    public static bool HasValue(this string input) => !string.IsNullOrEmpty(input);

    /// <summary>Remove underscores from a string</summary>
    /// <param name="input">String to process</param>
    /// <returns>string</returns>
    public static string RemoveUnderscoresAndDashes(this string input)
    {
      return input.Replace("_", "").Replace("-", "");
    }

    /// <summary>Parses most common JSON date formats</summary>
    /// <param name="input">JSON value to parse</param>
    /// <param name="culture"></param>
    /// <returns>DateTime</returns>
    public static DateTime ParseJsonDate(this string input, CultureInfo culture)
    {
      input = input.Replace("\n", "");
      input = input.Replace("\r", "");
      input = input.RemoveSurroundingQuotes();
      long? nullable = new long?();
      try
      {
        nullable = new long?(long.Parse(input));
      }
      catch (Exception ex)
      {
      }
      if (nullable.HasValue)
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double) nullable.Value);
      if (input.Contains("/Date("))
        return StringExtensions.ExtractDate(input, "\\\\?/Date\\((-?\\d+)(-|\\+)?([0-9]{4})?\\)\\\\?/", culture);
      if (!input.Contains("new Date("))
        return StringExtensions.ParseFormattedDate(input, culture);
      input = input.Replace(" ", "");
      return StringExtensions.ExtractDate(input, "newDate\\((-?\\d+)*\\)", culture);
    }

    /// <summary>Remove leading and trailing " from a string</summary>
    /// <param name="input">String to parse</param>
    /// <returns>String</returns>
    public static string RemoveSurroundingQuotes(this string input)
    {
      if (input.StartsWith("\"") && input.EndsWith("\""))
        input = input.Substring(1, input.Length - 2);
      return input;
    }

    private static DateTime ParseFormattedDate(string input, CultureInfo culture)
    {
      string[] formats = new string[8]
      {
        "u",
        "s",
        "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'",
        "yyyy-MM-ddTHH:mm:ssZ",
        "yyyy-MM-dd HH:mm:ssZ",
        "yyyy-MM-ddTHH:mm:ss",
        "yyyy-MM-ddTHH:mm:sszzzzzz",
        "M/d/yyyy h:mm:ss tt"
      };
      DateTime result;
      return DateTime.TryParseExact(input, formats, (IFormatProvider) culture, DateTimeStyles.None, out result) || DateTime.TryParse(input, (IFormatProvider) culture, DateTimeStyles.None, out result) ? result : new DateTime();
    }

    private static DateTime ExtractDate(string input, string pattern, CultureInfo culture)
    {
      DateTime date = DateTime.MinValue;
      Regex regex = new Regex(pattern);
      if (regex.IsMatch(input))
      {
        Match match = regex.Matches(input)[0];
        date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((double) Convert.ToInt64(match.Groups[1].Value));
        if (match.Groups.Count > 2 && !string.IsNullOrEmpty(match.Groups[3].Value))
        {
          DateTime exact = DateTime.ParseExact(match.Groups[3].Value, "HHmm", (IFormatProvider) culture);
          date = !(match.Groups[2].Value == "+") ? date.Subtract(exact.TimeOfDay) : date.Add(exact.TimeOfDay);
        }
      }
      return date;
    }

    /// <summary>Checks a string to see if it matches a regex</summary>
    /// <param name="input">String to check</param>
    /// <param name="pattern">Pattern to match</param>
    /// <returns>bool</returns>
    public static bool Matches(this string input, string pattern) => Regex.IsMatch(input, pattern);

    /// <summary>Converts a string to pascal case</summary>
    /// <param name="lowercaseAndUnderscoredWord">String to convert</param>
    /// <param name="culture"></param>
    /// <returns>string</returns>
    public static string ToPascalCase(this string lowercaseAndUnderscoredWord, CultureInfo culture)
    {
      return lowercaseAndUnderscoredWord.ToPascalCase(true, culture);
    }

    /// <summary>
    /// Converts a string to pascal case with the option to remove underscores
    /// </summary>
    /// <param name="text">String to convert</param>
    /// <param name="removeUnderscores">Option to remove underscores</param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public static string ToPascalCase(
      this string text,
      bool removeUnderscores,
      CultureInfo culture)
    {
      if (string.IsNullOrEmpty(text))
        return text;
      text = text.Replace("_", " ");
      string separator = removeUnderscores ? string.Empty : "_";
      string[] strArray = text.Split(' ');
      if (strArray.Length <= 1 && !strArray[0].IsUpperCase())
        return strArray[0].Substring(0, 1).ToUpper(culture) + strArray[0].Substring(1);
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index].Length > 0)
        {
          string str = strArray[index];
          string inputString = str.Substring(1);
          if (inputString.IsUpperCase())
            inputString = inputString.ToLower(culture);
          char upper = char.ToUpper(str[0], culture);
          strArray[index] = upper.ToString() + inputString;
        }
      }
      return string.Join(separator, strArray);
    }

    /// <summary>Converts a string to camel case</summary>
    /// <param name="lowercaseAndUnderscoredWord">String to convert</param>
    /// <param name="culture"></param>
    /// <returns>String</returns>
    public static string ToCamelCase(this string lowercaseAndUnderscoredWord, CultureInfo culture)
    {
      return lowercaseAndUnderscoredWord.ToPascalCase(culture).MakeInitialLowerCase();
    }

    /// <summary>Convert the first letter of a string to lower case</summary>
    /// <param name="word">String to convert</param>
    /// <returns>string</returns>
    public static string MakeInitialLowerCase(this string word)
    {
      return word.Substring(0, 1).ToLower() + word.Substring(1);
    }

    /// <summary>Checks to see if a string is all uppper case</summary>
    /// <param name="inputString">String to check</param>
    /// <returns>bool</returns>
    public static bool IsUpperCase(this string inputString)
    {
      return Regex.IsMatch(inputString, "^[A-Z]+$");
    }

    /// <summary>Add underscores to a pascal-cased string</summary>
    /// <param name="pascalCasedWord">String to convert</param>
    /// <returns>string</returns>
    public static string AddUnderscores(this string pascalCasedWord)
    {
      return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, "([A-Z]+)([A-Z][a-z])", "$1_$2"), "([a-z\\d])([A-Z])", "$1_$2"), "[-\\s]", "_");
    }

    /// <summary>Add dashes to a pascal-cased string</summary>
    /// <param name="pascalCasedWord">String to convert</param>
    /// <returns>string</returns>
    public static string AddDashes(this string pascalCasedWord)
    {
      return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, "([A-Z]+)([A-Z][a-z])", "$1-$2"), "([a-z\\d])([A-Z])", "$1-$2"), "[\\s]", "-");
    }

    /// <summary>Add an undescore prefix to a pascasl-cased string</summary>
    /// <param name="pascalCasedWord"></param>
    /// <returns></returns>
    public static string AddUnderscorePrefix(this string pascalCasedWord)
    {
      return string.Format("_{0}", (object) pascalCasedWord);
    }

    /// <summary>Add spaces to a pascal-cased string</summary>
    /// <param name="pascalCasedWord">String to convert</param>
    /// <returns>string</returns>
    public static string AddSpaces(this string pascalCasedWord)
    {
      return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, "([A-Z]+)([A-Z][a-z])", "$1 $2"), "([a-z\\d])([A-Z])", "$1 $2"), "[-\\s]", " ");
    }

    /// <summary>Return possible variants of a name for name matching.</summary>
    /// <param name="name">String to convert</param>
    /// <param name="culture">The culture to use for conversion</param>
    /// <returns>IEnumerable&lt;string&gt;</returns>
    public static IEnumerable<string> GetNameVariants(this string name, CultureInfo culture)
    {
      if (!string.IsNullOrEmpty(name))
      {
        yield return name;
        yield return name.ToCamelCase(culture);
        yield return name.ToLower(culture);
        yield return name.AddUnderscores();
        yield return name.AddUnderscores().ToLower(culture);
        yield return name.AddDashes();
        yield return name.AddDashes().ToLower(culture);
        yield return name.AddUnderscorePrefix();
        yield return name.ToCamelCase(culture).AddUnderscorePrefix();
        yield return name.AddSpaces();
        yield return name.AddSpaces().ToLower(culture);
      }
    }
  }
}
