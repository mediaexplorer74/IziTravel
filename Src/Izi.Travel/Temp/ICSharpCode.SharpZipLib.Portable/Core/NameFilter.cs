// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.NameFilter
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>
  /// NameFilter is a string matching class which allows for both positive and negative
  /// matching.
  /// A filter is a sequence of independant <see cref="T:System.Text.RegularExpressions.Regex">regular expressions</see> separated by semi-colons ';'.
  /// To include a semi-colon it may be quoted as in \;. Each expression can be prefixed by a plus '+' sign or
  /// a minus '-' sign to denote the expression is intended to include or exclude names.
  /// If neither a plus or minus sign is found include is the default.
  /// A given name is tested for inclusion before checking exclusions.  Only names matching an include spec
  /// and not matching an exclude spec are deemed to match the filter.
  /// An empty filter matches any name.
  /// </summary>
  /// <example>The following expression includes all name ending in '.dat' with the exception of 'dummy.dat'
  /// "+\.dat$;-^dummy\.dat$"
  /// </example>
  public class NameFilter : IScanFilter
  {
    private string filter_;
    private ArrayList inclusions_;
    private ArrayList exclusions_;

    /// <summary>
    /// Construct an instance based on the filter expression passed
    /// </summary>
    /// <param name="filter">The filter expression.</param>
    public NameFilter(string filter)
    {
      this.filter_ = filter;
      this.inclusions_ = new ArrayList();
      this.exclusions_ = new ArrayList();
      this.Compile();
    }

    /// <summary>
    /// Test a string to see if it is a valid regular expression.
    /// </summary>
    /// <param name="expression">The expression to test.</param>
    /// <returns>True if expression is a valid <see cref="T:System.Text.RegularExpressions.Regex" /> false otherwise.</returns>
    public static bool IsValidExpression(string expression)
    {
      bool flag = true;
      try
      {
        Regex regex = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
      }
      catch (ArgumentException ex)
      {
        flag = false;
      }
      return flag;
    }

    /// <summary>Test an expression to see if it is valid as a filter.</summary>
    /// <param name="toTest">The filter expression to test.</param>
    /// <returns>True if the expression is valid, false otherwise.</returns>
    public static bool IsValidFilterExpression(string toTest)
    {
      bool flag = true;
      try
      {
        if (toTest != null)
        {
          string[] strArray = NameFilter.SplitQuoted(toTest);
          for (int index = 0; index < strArray.Length; ++index)
          {
            if (strArray[index] != null && strArray[index].Length > 0)
            {
              Regex regex = new Regex(strArray[index][0] != '+' ? (strArray[index][0] != '-' ? strArray[index] : strArray[index].Substring(1, strArray[index].Length - 1)) : strArray[index].Substring(1, strArray[index].Length - 1), RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
          }
        }
      }
      catch (ArgumentException ex)
      {
        flag = false;
      }
      return flag;
    }

    /// <summary>Split a string into its component pieces</summary>
    /// <param name="original">The original string</param>
    /// <returns>Returns an array of <see cref="T:System.String" /> values containing the individual filter elements.</returns>
    public static string[] SplitQuoted(string original)
    {
      char ch = '\\';
      char[] array = new char[1]{ ';' };
      ArrayList arrayList = new ArrayList();
      if (original != null && original.Length > 0)
      {
        int index = -1;
        StringBuilder stringBuilder = new StringBuilder();
        while (index < original.Length)
        {
          ++index;
          if (index >= original.Length)
            arrayList.Add((object) stringBuilder.ToString());
          else if ((int) original[index] == (int) ch)
          {
            ++index;
            if (index >= original.Length)
              throw new ArgumentException("Missing terminating escape character", nameof (original));
            if (Array.IndexOf<char>(array, original[index]) < 0)
              stringBuilder.Append(ch);
            stringBuilder.Append(original[index]);
          }
          else if (Array.IndexOf<char>(array, original[index]) >= 0)
          {
            arrayList.Add((object) stringBuilder.ToString());
            stringBuilder.Length = 0;
          }
          else
            stringBuilder.Append(original[index]);
        }
      }
      return (string[]) arrayList.ToArray(typeof (string));
    }

    /// <summary>Convert this filter to its string equivalent.</summary>
    /// <returns>The string equivalent for this filter.</returns>
    public override string ToString() => this.filter_;

    /// <summary>Test a value to see if it is included by the filter.</summary>
    /// <param name="name">The value to test.</param>
    /// <returns>True if the value is included, false otherwise.</returns>
    public bool IsIncluded(string name)
    {
      bool flag = false;
      if (this.inclusions_.Count == 0)
      {
        flag = true;
      }
      else
      {
        foreach (Regex inclusion in (List<object>) this.inclusions_)
        {
          if (inclusion.IsMatch(name))
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    /// <summary>Test a value to see if it is excluded by the filter.</summary>
    /// <param name="name">The value to test.</param>
    /// <returns>True if the value is excluded, false otherwise.</returns>
    public bool IsExcluded(string name)
    {
      bool flag = false;
      foreach (Regex exclusion in (List<object>) this.exclusions_)
      {
        if (exclusion.IsMatch(name))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    /// <summary>Test a value to see if it matches the filter.</summary>
    /// <param name="name">The value to test.</param>
    /// <returns>True if the value matches, false otherwise.</returns>
    public bool IsMatch(string name) => this.IsIncluded(name) && !this.IsExcluded(name);

    /// <summary>Compile this filter.</summary>
    private void Compile()
    {
      if (this.filter_ == null)
        return;
      string[] strArray = NameFilter.SplitQuoted(this.filter_);
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index] != null && strArray[index].Length > 0)
        {
          bool flag = strArray[index][0] != '-';
          string pattern = strArray[index][0] != '+' ? (strArray[index][0] != '-' ? strArray[index] : strArray[index].Substring(1, strArray[index].Length - 1)) : strArray[index].Substring(1, strArray[index].Length - 1);
          if (flag)
            this.inclusions_.Add((object) new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline));
          else
            this.exclusions_.Add((object) new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline));
        }
      }
    }
  }
}
