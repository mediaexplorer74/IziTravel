// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.AppBarMenuItem
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using Microsoft.Phone.Shell;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// An <see cref="T:Microsoft.Phone.Shell.ApplicationBarMenuItem" /> capable of triggering action messages.
  /// </summary>
  public class AppBarMenuItem : ApplicationBarMenuItem, IAppBarActionMessage, IApplicationBarMenuItem
  {
    /// <summary>The action message.</summary>
    public string Message { get; set; }
  }
}
