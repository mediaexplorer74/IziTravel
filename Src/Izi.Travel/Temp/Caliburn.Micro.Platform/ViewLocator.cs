// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ViewLocator
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   A strategy for determining which view to use for a given model.
  /// </summary>
  public static class ViewLocator
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (ViewLocator));
    private static string defaultSubNsViews;
    private static string defaultSubNsViewModels;
    private static bool useNameSuffixesInMappings;
    private static string nameFormat;
    private static string viewModelSuffix;
    private static readonly List<string> ViewSuffixList = new List<string>();
    private static bool includeViewSuffixInVmNames;
    /// <summary>Used to transform names.</summary>
    public static NameTransformer NameTransformer = new NameTransformer();
    /// <summary>
    ///   Separator used when resolving View names for context instances.
    /// </summary>
    public static string ContextSeparator = ".";
    /// <summary>
    ///   Retrieves the view from the IoC container or tries to create it if not found.
    /// </summary>
    /// <remarks>
    ///   Pass the type of view as a parameter and recieve an instance of the view.
    /// </remarks>
    public static Func<Type, UIElement> GetOrCreateViewType = (Func<Type, UIElement>) (viewType =>
    {
      if (IoC.GetAllInstances(viewType).FirstOrDefault<object>() is UIElement element2)
      {
        ViewLocator.InitializeComponent((object) element2);
        return element2;
      }
      if (viewType.IsInterface || viewType.IsAbstract || !typeof (UIElement).IsAssignableFrom(viewType))
        return (UIElement) new TextBlock()
        {
          Text = string.Format("Cannot create {0}.", (object) viewType.FullName)
        };
      UIElement instance = (UIElement) Activator.CreateInstance(viewType);
      ViewLocator.InitializeComponent((object) instance);
      return instance;
    });
    /// <summary>
    /// Modifies the name of the type to be used at design time.
    /// </summary>
    public static Func<string, string> ModifyModelTypeAtDesignTime = (Func<string, string>) (modelTypeName =>
    {
      if (modelTypeName.StartsWith("_"))
      {
        int num1 = modelTypeName.IndexOf('.');
        modelTypeName = modelTypeName.Substring(num1 + 1);
        int num2 = modelTypeName.IndexOf('.');
        modelTypeName = modelTypeName.Substring(num2 + 1);
      }
      return modelTypeName;
    });
    /// <summary>
    /// Transforms a ViewModel type name into all of its possible View type names. Optionally accepts an instance
    /// of context object
    /// </summary>
    /// <returns>Enumeration of transformed names</returns>
    /// <remarks>Arguments:
    /// typeName = The name of the ViewModel type being resolved to its companion View.
    /// context = An instance of the context or null.
    /// </remarks>
    public static Func<string, object, IEnumerable<string>> TransformName = (Func<string, object, IEnumerable<string>>) ((typeName, context) =>
    {
      if (context == null)
      {
        Func<string, string> getReplaceString = (Func<string, string>) (r => r);
        return ViewLocator.NameTransformer.Transform(typeName, getReplaceString);
      }
      string contextstr = ViewLocator.ContextSeparator + context;
      string str = string.Empty;
      if (ViewLocator.useNameSuffixesInMappings)
        str = RegExHelper.GetCaptureGroup("suffix", "(" + string.Join("|", ViewLocator.ViewSuffixList.ToArray()) + ")");
      string patternregex = string.Format(ViewLocator.nameFormat, (object) "\\${basename}", (object) str) + "$";
      string replaceregex = "${basename}" + contextstr;
      Func<string, string> getReplaceString1 = (Func<string, string>) (r => Regex.Replace(r, patternregex, replaceregex));
      return ViewLocator.NameTransformer.Transform(typeName, getReplaceString1).Where<string>((Func<string, bool>) (n => n.EndsWith(contextstr)));
    });
    /// <summary>
    ///   Locates the view type based on the specified model type.
    /// </summary>
    /// <returns>The view.</returns>
    /// <remarks>
    ///   Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view type.
    /// </remarks>
    public static Func<Type, DependencyObject, object, Type> LocateTypeForModelType = (Func<Type, DependencyObject, object, Type>) ((modelType, displayLocation, context) =>
    {
      string str1 = modelType.FullName;
      if (View.InDesignMode)
        str1 = ViewLocator.ModifyModelTypeAtDesignTime(str1);
      string str2 = str1.Substring(0, str1.IndexOf('`') < 0 ? str1.Length : str1.IndexOf('`'));
      IEnumerable<string> source = ViewLocator.TransformName(str2, context);
      Type type = AssemblySource.FindTypeByNames(source);
      if (type == null)
        ViewLocator.Log.Warn("View not found. Searched: {0}.", (object) string.Join(", ", source.ToArray<string>()));
      return type;
    });
    /// <summary>Locates the view for the specified model type.</summary>
    /// <returns>The view.</returns>
    /// <remarks>
    ///   Pass the model type, display location (or null) and the context instance (or null) as parameters and receive a view instance.
    /// </remarks>
    public static Func<Type, DependencyObject, object, UIElement> LocateForModelType = (Func<Type, DependencyObject, object, UIElement>) ((modelType, displayLocation, context) =>
    {
      Type type = ViewLocator.LocateTypeForModelType(modelType, displayLocation, context);
      if (type != null)
        return ViewLocator.GetOrCreateViewType(type);
      return (UIElement) new TextBlock()
      {
        Text = string.Format("Cannot find view for {0}.", (object) modelType)
      };
    });
    /// <summary>Locates the view for the specified model instance.</summary>
    /// <returns>The view.</returns>
    /// <remarks>
    ///   Pass the model instance, display location (or null) and the context (or null) as parameters and receive a view instance.
    /// </remarks>
    public static Func<object, DependencyObject, object, UIElement> LocateForModel = (Func<object, DependencyObject, object, UIElement>) ((model, displayLocation, context) =>
    {
      if (!(model is IViewAware viewAware2) || !(viewAware2.GetView(context) is UIElement view2))
        return ViewLocator.LocateForModelType(model.GetType(), displayLocation, context);
      ViewLocator.Log.Info("Using cached view for {0}.", model);
      return view2;
    });
    /// <summary>Transforms a view type into a pack uri.</summary>
    public static Func<Type, Type, string> DeterminePackUriFromType = (Func<Type, Type, string>) ((viewModelType, viewType) =>
    {
      string assemblyName = viewType.Assembly.GetAssemblyName();
      string str = viewType.FullName.Replace(assemblyName, string.Empty).Replace(".", "/") + ".xaml";
      return !Application.Current.GetType().Assembly.GetAssemblyName().Equals(assemblyName) ? "/" + assemblyName + ";component" + str : str;
    });

    static ViewLocator() => ViewLocator.ConfigureTypeMappings(new TypeMappingConfiguration());

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
      ViewLocator.NameTransformer.Clear();
      ViewLocator.ViewSuffixList.Clear();
      ViewLocator.defaultSubNsViews = config.DefaultSubNamespaceForViews;
      ViewLocator.defaultSubNsViewModels = config.DefaultSubNamespaceForViewModels;
      ViewLocator.nameFormat = config.NameFormat;
      ViewLocator.useNameSuffixesInMappings = config.UseNameSuffixesInMappings;
      ViewLocator.viewModelSuffix = config.ViewModelSuffix;
      ViewLocator.ViewSuffixList.AddRange((IEnumerable<string>) config.ViewSuffixList);
      ViewLocator.includeViewSuffixInVmNames = config.IncludeViewSuffixInViewModelNames;
      ViewLocator.SetAllDefaults();
    }

    private static void SetAllDefaults()
    {
      if (ViewLocator.useNameSuffixesInMappings)
        ViewLocator.ViewSuffixList.Apply<string>(new Action<string>(ViewLocator.AddDefaultTypeMapping));
      else
        ViewLocator.AddSubNamespaceMapping(ViewLocator.defaultSubNsViewModels, ViewLocator.defaultSubNsViews);
    }

    /// <summary>
    /// Adds a default type mapping using the standard namespace mapping convention
    /// </summary>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddDefaultTypeMapping(string viewSuffix = "View")
    {
      if (!ViewLocator.useNameSuffixesInMappings)
        return;
      ViewLocator.AddNamespaceMapping(string.Empty, string.Empty, viewSuffix);
      ViewLocator.AddSubNamespaceMapping(ViewLocator.defaultSubNsViewModels, ViewLocator.defaultSubNsViews, viewSuffix);
    }

    /// <summary>
    /// This method registers a View suffix or synonym so that View Context resolution works properly.
    /// It is automatically called internally when calling AddNamespaceMapping(), AddDefaultTypeMapping(),
    /// or AddTypeMapping(). It should not need to be called explicitly unless a rule that handles synonyms
    /// is added directly through the NameTransformer.
    /// </summary>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View".</param>
    public static void RegisterViewSuffix(string viewSuffix)
    {
      if (ViewLocator.ViewSuffixList.Count<string>((Func<string, bool>) (s => s == viewSuffix)) != 0)
        return;
      ViewLocator.ViewSuffixList.Add(viewSuffix);
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
      ViewLocator.RegisterViewSuffix(viewSuffix);
      List<string> stringList = new List<string>();
      string str1 = ViewLocator.useNameSuffixesInMappings ? viewSuffix : string.Empty;
      foreach (string str2 in nsTargetsRegEx)
        stringList.Add(str2 + string.Format(ViewLocator.nameFormat, (object) "${basename}", (object) str1));
      string nameCaptureGroup = RegExHelper.GetNameCaptureGroup("basename");
      string regEx = string.Empty;
      if (ViewLocator.useNameSuffixesInMappings)
      {
        regEx = ViewLocator.viewModelSuffix;
        if (!ViewLocator.viewModelSuffix.Contains(viewSuffix) && ViewLocator.includeViewSuffixInVmNames)
          regEx = viewSuffix + regEx;
      }
      string globalFilterPattern = string.IsNullOrEmpty(nsSourceFilterRegEx) ? (string) null : nsSourceFilterRegEx + string.Format(ViewLocator.nameFormat, (object) "[A-Za-z_]\\w*", (object) regEx) + "$";
      string captureGroup = RegExHelper.GetCaptureGroup("suffix", regEx);
      ViewLocator.NameTransformer.AddRule(nsSourceReplaceRegEx + string.Format(ViewLocator.nameFormat, (object) nameCaptureGroup, (object) captureGroup) + "$", (IEnumerable<string>) stringList.ToArray(), globalFilterPattern);
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
      ViewLocator.AddTypeMapping(nsSourceReplaceRegEx, nsSourceFilterRegEx, new string[1]
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
      ViewLocator.AddTypeMapping(RegExHelper.GetCaptureGroup("origns", regEx), (string) null, ((IEnumerable<string>) nsTargets).Select<string, string>((Func<string, string>) (t => t + ".")).ToArray<string>(), viewSuffix);
    }

    /// <summary>
    /// Adds a standard type mapping based on simple namespace mapping
    /// </summary>
    /// <param name="nsSource">Namespace of source type</param>
    /// <param name="nsTarget">Namespace of target type</param>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = "View")
    {
      ViewLocator.AddNamespaceMapping(nsSource, new string[1]
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
      ViewLocator.AddTypeMapping(str4 + captureGroup + str2, (string) null, ((IEnumerable<string>) nsTargets).Select<string, string>((Func<string, string>) (t => rxbeforetgt + t + "." + rxaftertgt)).ToArray<string>(), viewSuffix);
    }

    /// <summary>
    /// Adds a standard type mapping by substituting one subnamespace for another
    /// </summary>
    /// <param name="nsSource">Subnamespace of source type</param>
    /// <param name="nsTarget">Subnamespace of target type</param>
    /// <param name="viewSuffix">Suffix for type name. Should  be "View" or synonym of "View". (Optional)</param>
    public static void AddSubNamespaceMapping(string nsSource, string nsTarget, string viewSuffix = "View")
    {
      ViewLocator.AddSubNamespaceMapping(nsSource, new string[1]
      {
        nsTarget
      }, viewSuffix);
    }

    /// <summary>
    ///   When a view does not contain a code-behind file, we need to automatically call InitializeCompoent.
    /// </summary>
    /// <param name="element">The element to initialize</param>
    public static void InitializeComponent(object element)
    {
      element.GetType().GetMethod(nameof (InitializeComponent), BindingFlags.Instance | BindingFlags.Public)?.Invoke(element, (object[]) null);
    }
  }
}
