// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.Strings
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
namespace System.Collections.Immutable
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Strings
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Strings()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Strings.resourceMan, (object) null))
          Strings.resourceMan = new ResourceManager("System.Collections.Immutable.Strings", typeof (Strings).GetTypeInfo().Assembly);
        return Strings.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Strings.resourceCulture;
      set => Strings.resourceCulture = value;
    }

    internal static string ArrayInitializedStateNotEqual
    {
      get
      {
        return Strings.ResourceManager.GetString(nameof (ArrayInitializedStateNotEqual), Strings.resourceCulture);
      }
    }

    internal static string ArrayLengthsNotEqual
    {
      get
      {
        return Strings.ResourceManager.GetString(nameof (ArrayLengthsNotEqual), Strings.resourceCulture);
      }
    }

    internal static string CannotFindOldValue
    {
      get
      {
        return Strings.ResourceManager.GetString(nameof (CannotFindOldValue), Strings.resourceCulture);
      }
    }

    internal static string CollectionModifiedDuringEnumeration
    {
      get
      {
        return Strings.ResourceManager.GetString(nameof (CollectionModifiedDuringEnumeration), Strings.resourceCulture);
      }
    }

    internal static string DuplicateKey
    {
      get => Strings.ResourceManager.GetString(nameof (DuplicateKey), Strings.resourceCulture);
    }

    internal static string InvalidEmptyOperation
    {
      get
      {
        return Strings.ResourceManager.GetString(nameof (InvalidEmptyOperation), Strings.resourceCulture);
      }
    }
  }
}
