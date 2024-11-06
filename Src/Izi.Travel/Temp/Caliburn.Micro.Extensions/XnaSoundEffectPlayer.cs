// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.XnaSoundEffectPlayer
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   Default <see cref="T:Caliburn.Micro.ISoundEffectPlayer" /> implementation, using Xna Framework. The sound effect is played without interrupting the music playback on the phone (which is required for the app certification in the WP7 Marketplace. Also note that using the Xna Framework in a WP7 Silverlight app is explicitly allowed for this very purpose.
  /// </summary>
  public class XnaSoundEffectPlayer : ISoundEffectPlayer
  {
    private static readonly XnaSoundEffectPlayer.XNAFrameworkDispatcherUpdater updater = new XnaSoundEffectPlayer.XNAFrameworkDispatcherUpdater();

    /// <summary>Plays a sound effect</summary>
    /// <param name="wavResource"> The uri of the resource containing the .wav file </param>
    public void Play(Uri wavResource)
    {
      XnaSoundEffectPlayer.updater.GetType();
      using (Stream stream = Application.GetResourceStream(wavResource).Stream)
        SoundEffect.FromStream(stream).Play();
    }

    private class XNAFrameworkDispatcherUpdater
    {
      private readonly DispatcherTimer timer;

      public XNAFrameworkDispatcherUpdater()
      {
        this.timer = new DispatcherTimer()
        {
          Interval = TimeSpan.FromMilliseconds(100.0)
        };
        this.timer.Tick += new EventHandler(this.OnTick);
        FrameworkDispatcher.Update();
      }

      private void OnTick(object sender, EventArgs e) => FrameworkDispatcher.Update();
    }
  }
}
