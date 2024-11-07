// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectContentMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Client.Entities;
using System;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgObjectContentMapper : MapperBase<Content, MtgObjectContent>
  {
    private readonly MediaMapper _mediaMapper;
    private readonly MtgObjectCompactMapper _mtgObjectCompactMapper;
    private readonly PlaybackMapper _playbackMapper;
    private readonly QuizMapper _quizMapper;

    public MtgObjectContentMapper(
      MediaMapper mediaMapper,
      MtgObjectCompactMapper mtgObjectCompactMapper,
      PlaybackMapper playbackMapper,
      QuizMapper quizMapper)
    {
      this._mediaMapper = mediaMapper;
      this._mtgObjectCompactMapper = mtgObjectCompactMapper;
      this._playbackMapper = playbackMapper;
      this._quizMapper = quizMapper;
    }

    public override MtgObjectContent Convert(Content source) => throw new NotImplementedException();

    public override Content ConvertBack(MtgObjectContent target)
    {
      if (target == null)
        return (Content) null;
      Content content = new Content()
      {
        Language = target.Language,
        Title = target.Title,
        Summary = target.Summary,
        Description = target.Description,
        Playback = this._playbackMapper.ConvertBack(target.Playback),
        Quiz = this._quizMapper.ConvertBack(target.Quiz),
        News = target.News,
        ChildrenCount = target.ChildrenCount,
        AudioDuration = target.AudioDuration
      };
      if (target.Images != null)
        content.Images = target.Images.Select<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>) (x => this._mediaMapper.ConvertBack(x, MediaFormat.Image))).ToArray<Izi.Travel.Business.Entities.Data.Media>();
      if (target.Audio != null)
        content.Audio = target.Audio.Select<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>) (x => this._mediaMapper.ConvertBack(x, MediaFormat.Audio))).ToArray<Izi.Travel.Business.Entities.Data.Media>();
      if (target.Video != null)
        content.Video = target.Video.Select<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>) (x => this._mediaMapper.ConvertBack(x, MediaFormat.Video))).ToArray<Izi.Travel.Business.Entities.Data.Media>();
      if (target.Children != null)
        content.Children = target.Children.Select<MtgObjectCompact, MtgObject>((Func<MtgObjectCompact, MtgObject>) (x => this._mtgObjectCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
      if (target.Collections != null)
        content.Collections = target.Collections.Select<MtgObjectCompact, MtgObject>((Func<MtgObjectCompact, MtgObject>) (x => this._mtgObjectCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
      if (target.References != null)
        content.References = target.References.Select<MtgObjectCompact, MtgObject>((Func<MtgObjectCompact, MtgObject>) (x => this._mtgObjectCompactMapper.ConvertBack(x))).ToArray<MtgObject>();
      return content;
    }
  }
}
