// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.NameTransformer
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///  Class for managing the list of rules for doing name transformation.
  /// </summary>
  public class NameTransformer : BindableCollection<NameTransformer.Rule>
  {
    private bool useEagerRuleSelection = true;

    /// <summary>
    /// Flag to indicate if transformations from all matched rules are returned. Otherwise, transformations from only the first matched rule are returned.
    /// </summary>
    public bool UseEagerRuleSelection
    {
      get => this.useEagerRuleSelection;
      set => this.useEagerRuleSelection = value;
    }

    /// <summary>
    ///  Adds a transform using a single replacement value and a global filter pattern.
    /// </summary>
    /// <param name="replacePattern">Regular expression pattern for replacing text</param>
    /// <param name="replaceValue">The replacement value.</param>
    /// <param name="globalFilterPattern">Regular expression pattern for global filtering</param>
    public void AddRule(string replacePattern, string replaceValue, string globalFilterPattern = null)
    {
      this.AddRule(replacePattern, (IEnumerable<string>) new string[1]
      {
        replaceValue
      }, globalFilterPattern);
    }

    /// <summary>
    ///  Adds a transform using a list of replacement values and a global filter pattern.
    /// </summary>
    /// <param name="replacePattern">Regular expression pattern for replacing text</param>
    /// <param name="replaceValueList">The list of replacement values</param>
    /// <param name="globalFilterPattern">Regular expression pattern for global filtering</param>
    public void AddRule(
      string replacePattern,
      IEnumerable<string> replaceValueList,
      string globalFilterPattern = null)
    {
      this.Add(new NameTransformer.Rule()
      {
        ReplacePattern = replacePattern,
        ReplacementValues = replaceValueList,
        GlobalFilterPattern = globalFilterPattern
      });
    }

    /// <summary>Gets the list of transformations for a given name.</summary>
    /// <param name="source">The name to transform into the resolved name list</param>
    /// <returns>The transformed names.</returns>
    public IEnumerable<string> Transform(string source)
    {
      return this.Transform(source, (Func<string, string>) (r => r));
    }

    /// <summary>Gets the list of transformations for a given name.</summary>
    /// <param name="source">The name to transform into the resolved name list</param>
    /// <param name="getReplaceString">A function to do a transform on each item in the ReplaceValueList prior to applying the regular expression transform</param>
    /// <returns>The transformed names.</returns>
    public IEnumerable<string> Transform(string source, Func<string, string> getReplaceString)
    {
      List<string> stringList = new List<string>();
      foreach (NameTransformer.Rule rule1 in this.Reverse<NameTransformer.Rule>())
      {
        NameTransformer.Rule rule = rule1;
        if ((string.IsNullOrEmpty(rule.GlobalFilterPattern) || Regex.IsMatch(source, rule.GlobalFilterPattern)) && Regex.IsMatch(source, rule.ReplacePattern))
        {
          stringList.AddRange(rule.ReplacementValues.Select<string, string>(getReplaceString).Select<string, string>((Func<string, string>) (repString => Regex.Replace(source, rule.ReplacePattern, repString))));
          if (!this.useEagerRuleSelection)
            break;
        }
      }
      return (IEnumerable<string>) stringList;
    }

    /// <summary>A rule that describes a name transform.</summary>
    public class Rule
    {
      /// <summary>Regular expression pattern for global filtering</summary>
      public string GlobalFilterPattern;
      /// <summary>Regular expression pattern for replacing text</summary>
      public string ReplacePattern;
      /// <summary>The list of replacement values</summary>
      public IEnumerable<string> ReplacementValues;
    }
  }
}
