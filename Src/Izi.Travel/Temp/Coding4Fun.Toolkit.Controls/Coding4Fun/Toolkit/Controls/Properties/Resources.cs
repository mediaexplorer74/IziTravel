// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Properties.Resources
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  public class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Coding4Fun.Toolkit.Controls.Properties.Resources.resourceMan, (object) null))
          Coding4Fun.Toolkit.Controls.Properties.Resources.resourceMan = new ResourceManager("Coding4Fun.Toolkit.Controls.Properties.Resources", typeof (Coding4Fun.Toolkit.Controls.Properties.Resources).Assembly);
        return Coding4Fun.Toolkit.Controls.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
      get => Coding4Fun.Toolkit.Controls.Properties.Resources.resourceCulture;
      set => Coding4Fun.Toolkit.Controls.Properties.Resources.resourceCulture = value;
    }

    public static string CancelText
    {
      get => Coding4Fun.Toolkit.Controls.Properties.Resources.ResourceManager.GetString(nameof (CancelText), Coding4Fun.Toolkit.Controls.Properties.Resources.resourceCulture);
    }

    public static string DoneText
    {
      get => Coding4Fun.Toolkit.Controls.Properties.Resources.ResourceManager.GetString(nameof (DoneText), Coding4Fun.Toolkit.Controls.Properties.Resources.resourceCulture);
    }

    public static string HourName
    {
      get => Coding4Fun.Toolkit.Controls.Properties.Resources.ResourceManager.GetString(nameof (HourName), Coding4Fun.Toolkit.Controls.Properties.Resources.resourceCulture);
    }

    public static string MinuteName
    {
      get => Coding4Fun.Toolkit.Controls.Properties.Resources.ResourceManager.GetString(nameof (MinuteName), Coding4Fun.Toolkit.Controls.Properties.Resources.resourceCulture);
    }

    public static string SecondName
    {
      get => Coding4Fun.Toolkit.Controls.Properties.Resources.ResourceManager.GetString(nameof (SecondName), Coding4Fun.Toolkit.Controls.Properties.Resources.resourceCulture);
    }

    public static string TimeSpanPickerTitle
    {
      get
      {
        return Coding4Fun.Toolkit.Controls.Properties.Resources.ResourceManager.GetString(nameof (TimeSpanPickerTitle), Coding4Fun.Toolkit.Controls.Properties.Resources.resourceCulture);
      }
    }
  }
}
