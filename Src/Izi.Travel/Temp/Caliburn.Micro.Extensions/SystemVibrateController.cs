// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.SystemVibrateController
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using Microsoft.Devices;
using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   The default implementation of <see cref="T:Caliburn.Micro.IVibrateController" /> , using the system controller.
  /// </summary>
  public class SystemVibrateController : IVibrateController
  {
    /// <summary>Starts vibration on the device.</summary>
    /// <param name="duration"> A TimeSpan object specifying the amount of time for which the phone vibrates. </param>
    public void Start(TimeSpan duration) => VibrateController.Default.Start(duration);

    /// <summary>Stops vibration on the device.</summary>
    public void Stop() => VibrateController.Default.Stop();
  }
}
