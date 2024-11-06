// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.WindowManagerExtensions
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>WindowManager extensions</summary>
  public static class WindowManagerExtensions
  {
    /// <summary>
    ///   Shows a modal dialog for the specified model, using vibrate and audio feedback
    /// </summary>
    /// <param name="windowManager">The WindowManager instance.</param>
    /// <param name="rootModel">The root model.</param>
    /// <param name="context">The context.</param>
    /// <param name="wavOpeningSound">If not null, use the specified .wav as opening sound</param>
    /// <param name="vibrate">If true, use a vibration feedback on dialog opening</param>
    public static void ShowDialogWithFeedback(
      this IWindowManager windowManager,
      object rootModel,
      object context = null,
      Uri wavOpeningSound = null,
      bool vibrate = true)
    {
      if (wavOpeningSound != (Uri) null)
        IoC.Get<ISoundEffectPlayer>().Play(wavOpeningSound);
      if (vibrate)
        IoC.Get<IVibrateController>().Start(TimeSpan.FromMilliseconds(200.0));
      windowManager.ShowDialog(rootModel, context);
    }
  }
}
