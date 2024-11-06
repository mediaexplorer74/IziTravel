// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ISoundEffectPlayer
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Service allowing to play a .wav sound effect</summary>
  public interface ISoundEffectPlayer
  {
    /// <summary>Plays a sound effect</summary>
    /// <param name="wavResource"> The uri of the resource containing the .wav file </param>
    void Play(Uri wavResource);
  }
}
