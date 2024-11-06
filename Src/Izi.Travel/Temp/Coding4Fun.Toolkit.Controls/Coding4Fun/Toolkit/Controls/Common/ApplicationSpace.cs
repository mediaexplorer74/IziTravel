// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.ApplicationSpace
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Common
{
  public static class ApplicationSpace
  {
    public static int ScaleFactor() => Application.Current.Host.Content.ScaleFactor;

    public static Frame RootFrame => Application.Current.RootVisual as Frame;

    public static bool IsDesignMode => DesignerProperties.IsInDesignTool;

    public static Dispatcher CurrentDispatcher => Deployment.Current.Dispatcher;
  }
}
