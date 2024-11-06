// Decompiled with JetBrains decompiler
// Type: Validation.ValidationStrings
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Validation
{
  [CompilerGenerated]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class ValidationStrings
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal ValidationStrings()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) ValidationStrings.resourceMan, (object) null))
          ValidationStrings.resourceMan = new ResourceManager("System.Collections.Immutable.Validation.ValidationStrings", typeof (ValidationStrings).GetTypeInfo().Assembly);
        return ValidationStrings.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => ValidationStrings.resourceCulture;
      set => ValidationStrings.resourceCulture = value;
    }

    internal static string Argument_EmptyArray
    {
      get
      {
        return ValidationStrings.ResourceManager.GetString(nameof (Argument_EmptyArray), ValidationStrings.resourceCulture);
      }
    }

    internal static string Argument_EmptyString
    {
      get
      {
        return ValidationStrings.ResourceManager.GetString(nameof (Argument_EmptyString), ValidationStrings.resourceCulture);
      }
    }

    internal static string Argument_NullElement
    {
      get
      {
        return ValidationStrings.ResourceManager.GetString(nameof (Argument_NullElement), ValidationStrings.resourceCulture);
      }
    }

    internal static string Argument_Whitespace
    {
      get
      {
        return ValidationStrings.ResourceManager.GetString(nameof (Argument_Whitespace), ValidationStrings.resourceCulture);
      }
    }
  }
}
