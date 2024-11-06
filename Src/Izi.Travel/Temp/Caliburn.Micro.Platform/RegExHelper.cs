// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.RegExHelper
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///  Helper class for encoding strings to regular expression patterns
  /// </summary>
  public static class RegExHelper
  {
    /// <summary>Regular expression pattern for valid name</summary>
    public const string NameRegEx = "[A-Za-z_]\\w*";
    /// <summary>
    /// Regular expression pattern for subnamespace (including dot)
    /// </summary>
    public const string SubNamespaceRegEx = "[A-Za-z_]\\w*\\.";
    /// <summary>
    /// Regular expression pattern for namespace or namespace fragment
    /// </summary>
    public const string NamespaceRegEx = "([A-Za-z_]\\w*\\.)*";

    /// <summary>
    /// Creates a named capture group with the specified regular expression
    /// </summary>
    /// <param name="groupName">Name of capture group to create</param>
    /// <param name="regEx">Regular expression pattern to capture</param>
    /// <returns>Regular expression capture group with the specified group name</returns>
    public static string GetCaptureGroup(string groupName, string regEx)
    {
      return "(?<" + groupName + ">" + regEx + ")";
    }

    /// <summary>
    /// Converts a namespace (including wildcards) to a regular expression string
    /// </summary>
    /// <param name="srcNamespace">Source namespace to convert to regular expression</param>
    /// <returns>Namespace converted to a regular expression</returns>
    public static string NamespaceToRegEx(string srcNamespace)
    {
      return srcNamespace.Replace(".", "\\.").Replace("*\\.", "([A-Za-z_]\\w*\\.)*");
    }

    /// <summary>
    /// Creates a capture group for a valid name regular expression pattern
    /// </summary>
    /// <param name="groupName">Name of capture group to create</param>
    /// <returns>Regular expression capture group with the specified group name</returns>
    public static string GetNameCaptureGroup(string groupName)
    {
      return RegExHelper.GetCaptureGroup(groupName, "[A-Za-z_]\\w*");
    }

    /// <summary>
    /// Creates a capture group for a namespace regular expression pattern
    /// </summary>
    /// <param name="groupName">Name of capture group to create</param>
    /// <returns>Regular expression capture group with the specified group name</returns>
    public static string GetNamespaceCaptureGroup(string groupName)
    {
      return RegExHelper.GetCaptureGroup(groupName, "([A-Za-z_]\\w*\\.)*");
    }
  }
}
