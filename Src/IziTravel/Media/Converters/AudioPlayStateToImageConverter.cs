// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.Converters.AudioPlayStateToImageConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Media.ViewModels.Audio;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Media.Converters
{
  public class AudioPlayStateToImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(value is AudioContentPlayState contentPlayState))
        return (object) null;
      switch (contentPlayState)
      {
        case AudioContentPlayState.None:
        case AudioContentPlayState.Initialized:
        case AudioContentPlayState.Paused:
        case AudioContentPlayState.Error:
          return (object) "/Assets/Icons/player.play.png";
        case AudioContentPlayState.Playing:
          return (object) "/Assets/Icons/player.pause.png";
        default:
          return (object) null;
      }
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
