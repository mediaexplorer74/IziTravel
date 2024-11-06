// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ViewModelLocator
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   A strategy for determining which view model to use for a given view.
  /// </summary>
  public static class ViewModelLocator
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (ViewModelLocator));
    private static string defaultSubNsViews;
    private static string defaultSubNsViewModels;
    private static bool useNameSuffixesInMappings;
    private static string nameFormat;
    private static string viewModelSuffix;
    private static readonly List<string> ViewSuffixList = new List<string>();
    private static bool includeViewSuffixInVmNames;
    /// <summary>Used to transform names.</summary>
    public static readonly NameTransformer NameTransformer = new NameTransformer();
    /// <summary>
    /// The name of the capture group used as a marker for rules that return interface types
    /// </summary>
    public static string InterfaceCaptureGroupName = "isinterface";
    /// <summary>
    /// Transforms a View type name into all of its possible ViewModel type names. Accepts a flag
    /// to include or exclude interface types.
    /// </summary>
    /// <returns>Enumeration of transformed names</returns>
    /// <remarks>Arguments:
    /// typeName = The name of the View type being resolved to its companion ViewModel.
    /// includeInterfaces = Flag to indicate if interface types are included
    /// </remarks>
    public static Func<string, bool, IEnumerable<string>> TransformName = (Func<string, bool, IEnumerable<string>>) ((typeName, includeInterfaces) =>
    {
      Func<string, string> getReplaceString;
      if (includeInterfaces)
      {
        getReplaceString = (Func<string, string>) (r => r);
      }
      else
      {
        string interfacegrpregex = "\\${" + ViewModelLocator.InterfaceCaptureGroupName + "}$";
        getReplaceString = (Func<string, string>) (r => !Regex.IsMatch(r, interfacegrpregex) ? r : string.Empty);
      }
      return ViewModelLocator.NameTransformer.Transform(typeName, getReplaceString).Where<string>((Func<string, bool>) (n => n != string.Empty));
    });
    /// <summary>
    ///   Determines the view model type based on the specified view type.
    /// </summary>
    /// <returns>The view model type.</returns>
    /// <remarks>
    ///   Pass the view type and receive a view model type. Pass true for the second parameter to search for interfaces.
    /// </remarks>
    public static Func<Type, bool, Type> LocateTypeForViewType = (Func<Type, bool, Type>) ((viewType, searchForInterface) =>
    {
      string fullName = viewType.FullName;
      IEnumerable<string> source = ViewModelLocator.TransformName(fullName, searchForInterface);
      Type type = AssemblySource.FindTypeByNames(source);
      if (type == null)
        ViewModelLocator.Log.Warn("View Model not found. Searched: {0}.", (object) string.Join(", ", source.ToArray<string>()));
      return type;
    });
    /// <summary>Locates the view model for the specified view type.</summary>
    /// <returns>The view model.</returns>
    /// <remarks>
    ///   Pass the view type as a parameter and receive a view model instance.
    /// </remarks>
    public static Func<Type, object> LocateForViewType = (Func<Type, object>) (viewType =>
    {
      Type type1 = ViewModelLocator.LocateTypeForViewType(viewType, false);
      if (type1 != null)
      {
        object obj = IoC.GetInstance(type1, (string) null);
        if (obj != null)
          return obj;
      }
      Type type2 = ViewModelLocator.LocateTypeForViewType(viewType, true);
      return type2 == null ? (object) null : IoC.GetInstance(type2, (string) null);
    });
    /// <summary>
    ///   Locates the view model for the specified view instance.
    /// </summary>
    /// <returns>The view model.</returns>
    /// <remarks>
    ///   Pass the view instance as a parameters and receive a view model instance.
    /// </remarks>
    public static Func<object, object> LocateForView = (Func<object, object>) (view =>
    {
      if (view == null)
        return (object) null;
      return view is FrameworkElement frameworkElement2 && frameworkElement2.DataContext != null ? frameworkElement2.DataContext : ViewModelLocator.LocateForViewType(view.GetType());
    });

    static ViewModelLocator()
    {
      ViewModelLocator.ConfigureTypeMappings(new TypeMappingConfiguration());
    }

    /// <summary>
    /// Specifies how type mappings are created, including default type mappings. Calling this method will
    /// clear all existing name transformation rules and create new default type mappings according to the
    /// configuration.
    /// </summary>
    /// <param name="config">An instance of TypeMappingConfiguration that provides the settings for configuration</param>
    public static void ConfigureTypeMappings(TypeMappingConfiguration config)
    {
      if (string.IsNullOrEmpty(config.DefaultSubNamespaceForViews))
        throw new ArgumentException("DefaultSubNamespaceForViews field cannot be blank.");
      if (string.IsNullOrEmpty(config.DefaultSubNamespaceForViewModels))
        throw new ArgumentException("DefaultSubNamespaceForViewModels field cannot be blank.");
      if (string.IsNullOrEmpty(config.NameFormat))
        throw new ArgumentException("NameFormat field cannot be blank.");
      ViewModelLocator.NameTransformer.Clear();
      ViewModelLocator.ViewSuffixList.Clear();
      ViewModelLocator.defaultSubNsViews = config.DefaultSubNamespaceForViews;
      ViewModelLocator.defaultSubNsViewModels = config.DefaultSubNamespaceForViewModels;
      ViewModelLocator.nameFormat = config.NameFormat;
      ViewModelLocator.useNameSuffixesInMappings = config.UseNameSuffixesInMappings;
      ViewModelLocator.viewModelSuffix = config.ViewModelSuffix;
      ViewModelLocator.ViewSuffixList.AddRange((IEnumerable<string>) config.ViewSuffixList);
      ViewModelLocator.includeViewSuffixInVmNames = config.IncludeViewSuffixInViewModelNames;
      ViewModelLocator.SetAllDefaults();
    }

    private static void SetAllDefaults()
    {
      if (ViewModelLocator.useNameSuffixesInMappings)
        ViewModelLocator.ViewSuffixList.Apply<string>(new Action<string>(ViewModelLocator.AddDefaultTypeMapping));
      else
        ViewModelLocator.AddSubNamespaceMapping(ViewModelLocator.defaultSubNsViews, ViewModelLocator.defaultSubNsViewModels);
    }

    /// <summary>
    /// Adds a default type mapping using the standard namespace mapping convention
    /// </summary>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddDefaultTypeMapping(string viewSuffix = "View")
    {
      if (!ViewModelLocator.useNameSuffixesInMappings)
        return;
      ViewModelLocator.AddNamespaceMapping(string.Empty, string.Empty, viewSuffix);
      ViewModelLocator.AddSubNamespaceMapping(ViewModelLocator.defaultSubNsViews, ViewModelLocator.defaultSubNsViewModels, viewSuffix);
    }

    /// <summary>
    /// Adds a standard type mapping based on namespace RegEx replace and filter patterns
    /// </summary>
    /// <param name="nsSourceReplaceRegEx">RegEx replace pattern for source namespace</param>
    /// <param name="nsSourceFilterRegEx">RegEx filter pattern for source namespace</param>
    /// <param name="nsTargetsRegEx">Array of RegEx replace values for target namespaces</param>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddTypeMapping(
      string nsSourceReplaceRegEx,
      string nsSourceFilterRegEx,
      string[] nsTargetsRegEx,
      string viewSuffix = "View")
    {
      List<string> replist = new List<string>();
      string interfacegrp = "${" + ViewModelLocator.InterfaceCaptureGroupName + "}";
      Action<string> func;
      if (ViewModelLocator.useNameSuffixesInMappings)
      {
        if (ViewModelLocator.viewModelSuffix.Contains(viewSuffix) || !ViewModelLocator.includeViewSuffixInVmNames)
        {
          string nameregex = string.Format(ViewModelLocator.nameFormat, (object) "${basename}", (object) ViewModelLocator.viewModelSuffix);
          func = (Action<string>) (t =>
          {
            replist.Add(t + "I" + nameregex + interfacegrp);
            replist.Add(t + "I${basename}" + interfacegrp);
            replist.Add(t + nameregex);
            replist.Add(t + "${basename}");
          });
        }
        else
        {
          string nameregex = string.Format(ViewModelLocator.nameFormat, (object) "${basename}", (object) ("${suffix}" + ViewModelLocator.viewModelSuffix));
          func = (Action<string>) (t =>
          {
            replist.Add(t + "I" + nameregex + interfacegrp);
            replist.Add(t + nameregex);
          });
        }
      }
      else
        func = (Action<string>) (t =>
        {
          replist.Add(t + "I${basename}" + interfacegrp);
          replist.Add(t + "${basename}");
        });
      ((IEnumerable<string>) nsTargetsRegEx).ToList<string>().Apply<string>((Action<string>) (t => func(t)));
      string regEx = ViewModelLocator.useNameSuffixesInMappings ? viewSuffix : string.Empty;
      string globalFilterPattern = string.IsNullOrEmpty(nsSourceFilterRegEx) ? (string) null : nsSourceFilterRegEx + string.Format(ViewModelLocator.nameFormat, (object) "[A-Za-z_]\\w*", (object) regEx) + "$";
      string nameCaptureGroup = RegExHelper.GetNameCaptureGroup("basename");
      string captureGroup1 = RegExHelper.GetCaptureGroup("suffix", regEx);
      string captureGroup2 = RegExHelper.GetCaptureGroup(ViewModelLocator.InterfaceCaptureGroupName, string.Empty);
      ViewModelLocator.NameTransformer.AddRule(nsSourceReplaceRegEx + string.Format(ViewModelLocator.nameFormat, (object) nameCaptureGroup, (object) captureGroup1) + "$" + captureGroup2, (IEnumerable<string>) replist.ToArray(), globalFilterPattern);
    }

    /// <summary>
    /// Adds a standard type mapping based on namespace RegEx replace and filter patterns
    /// </summary>
    /// <param name="nsSourceReplaceRegEx">RegEx replace pattern for source namespace</param>
    /// <param name="nsSourceFilterRegEx">RegEx filter pattern for source namespace</param>
    /// <param name="nsTargetRegEx">RegEx replace value for target namespace</param>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddTypeMapping(
      string nsSourceReplaceRegEx,
      string nsSourceFilterRegEx,
      string nsTargetRegEx,
      string viewSuffix = "View")
    {
      ViewModelLocator.AddTypeMapping(nsSourceReplaceRegEx, nsSourceFilterRegEx, new string[1]
      {
        nsTargetRegEx
      }, viewSuffix);
    }

    /// <summary>
    /// Adds a standard type mapping based on simple namespace mapping
    /// </summary>
    /// <param name="nsSource">Namespace of source type</param>
    /// <param name="nsTargets">Namespaces of target type as an array</param>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddNamespaceMapping(string nsSource, string[] nsTargets, string viewSuffix = "View")
    {
      string regEx = RegExHelper.NamespaceToRegEx(nsSource + ".");
      if (!string.IsNullOrEmpty(nsSource))
        regEx = "^" + regEx;
      ViewModelLocator.AddTypeMapping(RegExHelper.GetCaptureGroup("origns", regEx), (string) null, ((IEnumerable<string>) nsTargets).Select<string, string>((Func<string, string>) (t => t + ".")).ToArray<string>(), viewSuffix);
    }

    /// <summary>
    /// Adds a standard type mapping based on simple namespace mapping
    /// </summary>
    /// <param name="nsSource">Namespace of source type</param>
    /// <param name="nsTarget">Namespace of target type</param>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = "View")
    {
      ViewModelLocator.AddNamespaceMapping(nsSource, new string[1]
      {
        nsTarget
      }, viewSuffix);
    }

    /// <summary>
    /// Adds a standard type mapping by substituting one subnamespace for another
    /// </summary>
    /// <param name="nsSource">Subnamespace of source type</param>
    /// <param name="nsTargets">Subnamespaces of target type as an array</param>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddSubNamespaceMapping(
      string nsSource,
      string[] nsTargets,
      string viewSuffix = "View")
    {
      string regEx = RegExHelper.NamespaceToRegEx(nsSource + ".");
      string str1;
      string rxaftertgt;
      string str2 = str1 = rxaftertgt = string.Empty;
      string str3 = str1;
      string rxbeforetgt = str1;
      string str4 = str3;
      if (!string.IsNullOrEmpty(nsSource))
      {
        if (!nsSource.StartsWith("*"))
        {
          str4 = RegExHelper.GetNamespaceCaptureGroup("nsbefore");
          rxbeforetgt = "${nsbefore}";
        }
        if (!nsSource.EndsWith("*"))
        {
          str2 = RegExHelper.GetNamespaceCaptureGroup("nsafter");
          rxaftertgt = "${nsafter}";
        }
      }
      string captureGroup = RegExHelper.GetCaptureGroup("subns", regEx);
      ViewModelLocator.AddTypeMapping(str4 + captureGroup + str2, (string) null, ((IEnumerable<string>) nsTargets).Select<string, string>((Func<string, string>) (t => rxbeforetgt + t + "." + rxaftertgt)).ToArray<string>(), viewSuffix);
    }

    /// <summary>
    /// Adds a standard type mapping by substituting one subnamespace for another
    /// </summary>
    /// <param name="nsSource">Subnamespace of source type</param>
    /// <param name="nsTarget">Subnamespace of target type</param>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddSubNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = "View")
    {
      ViewModelLocator.AddSubNamespaceMapping(nsSource, new string[1]
      {
        nsTarget
      }, viewSuffix);
    }

    /// <summary>Makes a type name into an interface name.</summary>
    /// <param name="typeName">The part.</param>
    /// <returns></returns>
    public static string MakeInterface(string typeName)
    {
      string str = string.Empty;
      if (typeName.Contains("[["))
      {
        int startIndex = typeName.IndexOf("[[");
        str = typeName.Substring(startIndex);
        typeName = typeName.Remove(startIndex);
      }
      int num = typeName.LastIndexOf(".");
      return typeName.Insert(num + 1, "I") + str;
    }
  }
}
