// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Execute
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Threading.Tasks;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Enables easy marshalling of code to the UI thread.</summary>
  public static class Execute
  {
    /// <summary>
    ///   Indicates whether or not the framework is in design-time mode.
    /// </summary>
    public static bool InDesignMode => PlatformProvider.Current.InDesignMode;

    /// <summary>Executes the action on the UI thread asynchronously.</summary>
    /// <param name="action">The action to execute.</param>
    public static void BeginOnUIThread(this Action action)
    {
      PlatformProvider.Current.BeginOnUIThread(action);
    }

    /// <summary>Executes the action on the UI thread asynchronously.</summary>
    /// <param name="action">The action to execute.</param>
    public static Task OnUIThreadAsync(this Action action)
    {
      return PlatformProvider.Current.OnUIThreadAsync(action);
    }

    /// <summary>Executes the action on the UI thread.</summary>
    /// <param name="action">The action to execute.</param>
    public static void OnUIThread(this Action action)
    {
      PlatformProvider.Current.OnUIThread(action);
    }
  }
}
