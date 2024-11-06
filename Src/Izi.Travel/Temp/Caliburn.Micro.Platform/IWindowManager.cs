// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IWindowManager
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System.Collections.Generic;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A service that manages windows.</summary>
  public interface IWindowManager
  {
    /// <summary>Shows a modal dialog for the specified model.</summary>
    /// <param name="rootModel">The root model.</param>
    /// <param name="settings">The optional dialog settings.</param>
    /// <param name="context">The context.</param>
    void ShowDialog(object rootModel, object context = null, IDictionary<string, object> settings = null);

    /// <summary>Shows a popup at the current mouse position.</summary>
    /// <param name="rootModel">The root model.</param>
    /// <param name="context">The view context.</param>
    /// <param name="settings">The optional popup settings.</param>
    void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null);
  }
}
