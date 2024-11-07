// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.PlaybackMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Mapping.Enum;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class PlaybackMapper : MapperBase<Izi.Travel.Business.Entities.Data.Playback, Izi.Travel.Client.Entities.Playback>
  {
    private readonly PlaybackTypeMapper _playbackTypeMapper;

    public PlaybackMapper(PlaybackTypeMapper playbackTypeMapper)
    {
      this._playbackTypeMapper = playbackTypeMapper;
    }

    public override Izi.Travel.Client.Entities.Playback Convert(Izi.Travel.Business.Entities.Data.Playback source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.Playback ConvertBack(Izi.Travel.Client.Entities.Playback target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.Playback) null;
      return new Izi.Travel.Business.Entities.Data.Playback()
      {
        Type = this._playbackTypeMapper.ConvertBack(target.Type),
        Order = target.Order != null ? target.Order.ToArray() : (string[]) null
      };
    }
  }
}
