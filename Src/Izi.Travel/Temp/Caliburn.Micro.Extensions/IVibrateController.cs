// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IVibrateController
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   Allows applications to start and stop vibration on the device.
  /// </summary>
  public interface IVibrateController
  {
    /// <summary>Starts vibration on the device.</summary>
    /// <param name="duration"> A TimeSpan object specifying the amount of time for which the phone vibrates. </param>
    void Start(TimeSpan duration);

    /// <summary>Stops vibration on the device.</summary>
    void Stop();
  }
}
